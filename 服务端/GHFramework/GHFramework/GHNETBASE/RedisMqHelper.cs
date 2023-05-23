using GHDatabase.Redis.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GHDatabase.Redis
{
    //用于消息队列
    /*
    add element to the queue:
        - increments a UUID record 
        - store the object using a key as <queuename>:uuid
        - push this key into a list named <queuename>:queue
        - push this list name into the general QUEUESET
    get element from queue:
        - pop a key from the list
        - get and return, along with its key

    del element from the queue:
        - tricky part. there must be a queue_get() before. The object is out of the queue already. delete it.
        
    - TODO: the object may have an expiration instead of straight deletion 用超时替代删除 
    - TODO: RPOPLPUSH can be used to put it in another queue as a backlog 入出队列操作放入一个待办等待队列缓冲??
    - TODO: persistence management (on/off/status) 执久化管理 （启、停、状态等）
    */
    internal class RedisMqHelper : AbstractBase
    {
        public int POLICY_BROADCAST = 1;
        //广播策略 我们的策略对应： 由业务系统去订阅 识别 队列数据响应性质（p2p 群发 loop等） 这里只作基础性缓冲、交换数据目的
        public int POLICY_ROUNDROBIN = 2;
        //循环策略

        public string QUEUE_STATUS = "queuestat:";
        public string QUEUE_POLICY = "{0}:{1}:queuepolicy";
        public string QUEUE_NAME = "{0}:{1}:queue";

        int STOPQUEUE = 0;
        int STARTQUEUE = 1;
        Dictionary<string, int> policies = new Dictionary<string, int>();
        string QUEUESET = "QUEUESET"; // the set which holds all queues  全局队列标识 集合
        string PUBSUB_SUFIX = "PUBSUB";

        //redis 默认有16个数据蔟
        //key value 数据蔟 编号
        const int DB_KEYVALUE = 0;          //数据压力大
        //key json value 数据蔟 编号
        const int DB_KEYVALUEJSON = 1;
        //list queue数据蔟 编号
        const int DB_QUEUE = 2;
        //set 数据蔟 编号
        const int DB_SET = 3;
        //PUBSUB 数据蔟 编号
        const int DB_PUBSUB = 4;

        public RedisMqHelper(string primaryServer, int primaryPort, int slavePort,string password)
            : base()
        {
            this.PrimaryServer = primaryServer;
            this.PrimaryPort = primaryPort;
            this.SlavePort = slavePort;
            this.Password = password;
            //self.STOPQUEUE = 0
            //self.STARTQUEUE = 1 
            //self.redis = redis
            //self.policies = {
            //    "broadcast": POLICY_BROADCAST,
            //    "roundrobin": POLICY_ROUNDROBIN,
            //}
            policies.Add("broadcast", POLICY_BROADCAST);
            policies.Add("roundrobin", POLICY_ROUNDROBIN);

            //self.inverted_policies = dict([[v, k] for k, v in self.policies.items()])
            //self.QUEUESET = 'QUEUESET' # the set which holds all queues
            //self.PUBSUB_SUFIX = 'PUBSUB'
        }

        public string PrimaryServer { get; set; }
        public int PrimaryPort { get; set; }
        public int SlavePort { get; set; }

        public string Password { get; set; }

        protected override string GetConfiguration()
        {
            return this.PrimaryServer + ":" + this.PrimaryPort +"," + PrimaryServer + ":" + SlavePort;
        }
        private readonly object _lockobj = new object();
        private ConnectionMultiplexer connection;
        public ConnectionMultiplexer Connection
        {
            get
            {
                if (connection == null || !connection.IsConnected)
                {
                    lock (_lockobj)
                    {
                        if (connection == null || !connection.IsConnected)
                        {
                            //connection =  ConnectionMultiplexer.Connect("yhdcache0.redis.cache.windows.net,ssl=true,password=...");
#if DEBUG
                            //string logFile = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "redisLog.txt");
                            //System.IO.FileStream _file = new System.IO.FileStream(logFile, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite);
                            //System.IO.StreamWriter sw = new System.IO.StreamWriter(_file.);
                            connection = Create(password:this.Password,log: Console.Out);
#else
                            connection =  Create();
#endif
                            connection.PreserveAsyncOrder = true;//异步
                            //var task = db.PingAsync();
                            //var duration = connection.Wait(task);
                        }
                    }
                }


                return connection;
            }
        }

        /// <summary>
        /// 关闭所有连接，释放资源
        /// </summary>
        public void Close()
        {
            if (connection != null)
                connection.Dispose();
            base.Dispose();
        }

        private object normalize(object item)
        {
            if (item is string)
                return item;
            //else if (item is types.UnicodeType) 
            //{
            //    try{
            //        return item.encode("utf-8");
            //    }
            //    catch(Exception ex)
            //    {
            //        throw ValueError("strings must be utf-8");
            //    }
            //}else{
            //        throw ValueError("data must be utf-8");
            //}
            return null;
        }

        public dynamic Authorize(string queue, string authkey)
        {
            /*
            Authorize an operation for a given queue using an authentication key
            The basic mechanism is a check against Redis to see if key named AUTHKEY:<authkey value> exists
            If it exists, check against its content to see wheter the queue is authorized. 
            Authorization is either read/write a queue and create new queues
            queues and priv are lists in the authorization record
            returns boolean 
            */

            //queue, authkey = self.normalize(queue), self.normalize(authkey)
            // get key and analyze {'queues': ['q1','q2','q3'], 'privs': ['create']}
            RedisKey avkey = string.Format("AUTHKEY:{0}", authkey);
            var db = Connection.GetDatabase(db: DB_KEYVALUEJSON);
            //authval = yield self.redis.get(avkey.encode('utf-8'))
            var task = db.StringGetAsync(avkey);
            var authval = Connection.Wait(task);

            if (!authval.HasValue)
                return new { Auth = (bool)false, Queues = (IEnumerable<QueueModel>)null };

            AUTHModel adata = null;

            try
            {
                //adata = cyclone.escape.json_decode(authval)
                adata = GHCore.Serialization.JSONFormatter.Deserialize<AUTHModel>(authval);
            }
            catch (Exception ex)
            {
                GHCore.GHLogger.Log(ex.ToString(), GHCore.LogCategory.Warn);
                return new { Auth = (bool)false, Queues = (IEnumerable<QueueModel>)null };
            }
           
           if (adata.queues != null && adata.queues.Count(t => t.Name == queue) > 0)
               return new { Auth = (bool)true, Queues = adata.queues };
            else if (adata.privs != null && adata.privs.Contains("create"))
               return new { Auth = (bool)true, Queues = adata.queues };

           return new { Auth = (bool)false, Queues = (IEnumerable<QueueModel>)null }; 
        }

        public bool CreateAuthRecord(string authkey, IEnumerable<QueueModel> queues = null, IEnumerable<string> privs = null)
        {
            //create a authorization record. queues and privs are lists

            //authkey = self.normalize(authkey)
            RedisKey avkey = string.Format("AUTHKEY:{0}", authkey);
            //avkey = self.normalize(avkey)
            if (queues == null)
                queues = new List<QueueModel>();
            if (privs == null)
                privs = new List<string>();

            AUTHModel authrecord = new AUTHModel { queues = queues, privs = privs };
            RedisValue json = GHCore.Serialization.JSONFormatter.Serialize(authrecord);

            var db = Connection.GetDatabase(db: DB_KEYVALUEJSON);
            var task = db.StringSetAsync(avkey, json);
            var res = Connection.Wait(task);

            return res;
        }

        public bool QueueAdd(string authkey, string queue, string value, TimeSpan? ttl = null)
        {
            //queue, value = self.normalize(queue), self.normalize(value)
            var db0 = Connection.GetDatabase(db: DB_KEYVALUE);

            var task0 = db0.StringIncrementAsync(string.Format("{0}:{1}:UUID", authkey, queue));//某用户的 某个队列 长度计数+1
            var uuid = Connection.Wait(task0);

            var key = string.Format("{0}:{1}:{2}", authkey, queue, uuid);//设定用户 队列内的 项 生存周期
            var task1 = db0.StringSetAsync(key, value, expiry: ttl);
            var res = Connection.Wait(task1);

            if (uuid == 1)  // TODO: use ismember()//用户 新队列 加入队列集合
            {
                // either by checking uuid or by ismember, this is where you must know if the queue is a new one.
                // add to queues set
                var db3 = Connection.GetDatabase(db: DB_SET);
                var task2 = db3.SetAddAsync(QUEUESET, string.Format("{0}:{1}", authkey, queue));//全局队列集合
                res = Connection.Wait(task2);

                RedisKey ckey = string.Format("{0}{1}:{2}", QUEUE_STATUS, authkey, queue);//某用户 某个队列 状态
                var task3 = db0.StringSetAsync(ckey, STARTQUEUE);//用户的第一个队列
                res = Connection.Wait(task3);
            }

            RedisKey internal_queue_name = string.Format(QUEUE_NAME, authkey, queue);
            var db2 = Connection.GetDatabase(db: DB_QUEUE);
            var task4 = db2.ListLeftPushAsync(internal_queue_name, key);
            res = Connection.Wait(task4) > 0;
            return res;
        }

        public bool QueueMessageChangeVisbility(string messagekey, TimeSpan? ttl)
        {
            var db0 = Connection.GetDatabase(db: DB_KEYVALUE);
            var task1 = db0.KeyExistsAsync(messagekey);
            var res = Connection.Wait(task1);
            if (res)
            {
                var task = db0.KeyExpireAsync(messagekey, ttl);
                var ret = Connection.Wait(task);
                return ret;
            }
            else
                return false;
        }

        public dynamic QueueGet(string authkey, string queue, bool softget = false)
        {
            /*
                GET can be either soft or hard. 
                SOFTGET means that the object is not POP'ed from its queue list. It only gets a refcounter which is incremente for each GET
                HARDGET is the default behaviour. It POPs the key from its queue list.
                NoSQL dbs as mongodb would have other ways to deal with it. May be an interesting port.
                The reasoning behing refcounters is that they are important in some job scheduler patterns.
                To really cleanup the queue, one would have to issue a DEL after a hard GET.
            */
            //var policy;
            //queue = self.normalize(queue)

            RedisKey lkey = string.Format(QUEUE_NAME, authkey, queue);
            var db2 = Connection.GetDatabase(db: DB_QUEUE);
            RedisValue okey;
            if (!softget)
            {
                var task = db2.ListRightPopAsync(lkey);
                okey = Connection.Wait(task);
            }
            else
            {
                var task1 = db2.ListGetByIndexAsync(lkey, -1);   //-1 表示列表的最后一个元素 不移除key
                okey = Connection.Wait(task1);
            }

            if (!okey.HasValue)
                return (object)null;
            //defer.returnValue((None, None))

            var qpkey = string.Format(QUEUE_POLICY, authkey, queue);
            var db0 = Connection.GetDatabase(db: DB_KEYVALUE);
            var task2 = db0.StringGetAsync(new RedisKey[] { qpkey, (string)okey });
            var vals = Connection.Wait(task2);
            var policy = vals[0];
            //(policy, val) = yield self.redis.mget([qpkey, okey.encode('utf-8')])
            long c = 0;
            if (softget)
            {
                var task3 = db0.StringIncrementAsync(string.Format("{0}:refcount", okey));//消息被消费次数
                c = Connection.Wait(task3);
            }

            return new
            {
                Policy = (policy.HasValue ? (int)policy : POLICY_BROADCAST),
                Queue = new
                {
                    key = (string)okey,
                    value = (string)vals[1],
                    count = c   //消息被消费次数
                }
            };
        }

        public bool QueueDel(string authkey, string queue, string okey)
        {
            /*
                DELetes an element from redis (not from the queue).
                Its important to make sure a GET was issued before a DEL. Its a kinda hard to guess the direct object key w/o a GET tho.
                the return value contains the key and value, which is a del return code from Redis. > 1 success and N keys where deleted, 0 == failure
            */
            //queue, okey = self.normalize(queue), self.normalize(okey)

            var db0 = Connection.GetDatabase(db: DB_KEYVALUE);
            var task = db0.KeyDeleteAsync(okey);    //删除用户 队列 某项键值
            var val = Connection.Wait(task);

            //val = yield self.redis.delete(okey)
            return val;
        }

        public long QueueLen(string authkey, string queue)
        {
            var lkey = string.Format(QUEUE_NAME, authkey, queue);
            var db2 = Connection.GetDatabase(db: DB_QUEUE);
            var task = db2.ListLengthAsync(lkey);
            var ll = Connection.Wait(task);
            return ll;
        }
        //所有队列
        public IEnumerable<string> QueueAll(string authkey)
        {
            var db3 = Connection.GetDatabase(db: DB_SET);
            var task = db3.SetMembersAsync(QUEUESET);
            var sm = Connection.Wait(task);
            //var task = db3.SetAddAsync(QUEUESET, string.Format("{0}:{1}", authkey, queue));//全局队列集合
            if (string.IsNullOrEmpty(authkey))
                return sm.Cast<string>();
            else
                return sm.Where(v => v.HasValue && ((string)v).StartsWith(authkey + ":")).Cast<string>();

            //defer.returnValue({'queues': sm})
        }

        public dynamic QueueGetDel(string authkey, string queue)
        {
            //policy = None
            //queue = self.normalize(queue)
            var lkey = string.Format(QUEUE_NAME, authkey, queue);
            var db2 = Connection.GetDatabase(db: DB_QUEUE);
            var task = db2.ListRightPopAsync(lkey); // take from queue's list
            var okey = Connection.Wait(task);
            /* var key = string.Format("{0}:{1}:{2}", authkey, queue, uuid);//设定用户 队列内的 项 生存周期*/
            if (!okey.HasValue)
                return new { Policy = (int?)null, Queue = (object)null };
            //defer.returnValue((None, False))

            //okey = self.normalize(okey)
            var nkey = string.Format("{0}:lock", okey);
            var db0 = Connection.GetDatabase(db: DB_KEYVALUE);
            var task1 = db0.KeyRenameAsync((string)okey, nkey);// rename key
            var ren = Connection.Wait(task1);

            if (ren == false)
                return new { Policy = (int?)null, Queue = (object)null };
            //defer.returnValue((None,None))

            var qpkey = string.Format(QUEUE_POLICY, authkey, queue);
            var task2 = db0.StringGetAsync(new RedisKey[] { qpkey, nkey });
            //(policy, val) = yield self.redis.mget(qpkey, nkey)
            var vals = Connection.Wait(task2);
            var policy = vals[0];

            var task3 = db0.KeyDeleteAsync(nkey);
            var delk = Connection.Wait(task3);
            //delk = yield self.redis.delete(nkey)
            if (delk == false)
                return new { Policy = (int?)null, Queue = (object)null };
            //defer.returnValue((None, None))
            else
                return new
                {
                    Policy = (int?)policy,
                    Queue = new
                    {
                        key = okey,
                        value = (string)vals[1]
                    }
                };
            //defer.returnValue((policy, {'key':okey, 'value':val}))
        }

        //设置用户队列的策略
        public bool QueuePolicySet(string authkey, string queue, string policy)
        {
            //queue, policy = self.normalize(queue), self.normalize(policy)

            if (this.policies.ContainsKey(policy))
            {
                var policy_id = this.policies[policy];
                var qpkey = string.Format(QUEUE_POLICY, authkey, queue);
                var db0 = Connection.GetDatabase(db: DB_KEYVALUE);
                var task = db0.StringSetAsync(qpkey, policy_id);
                var res = Connection.Wait(task);

                //defer.returnValue(res)
                //defer.returnValue({'queue': queue, 'response': res})
                return res;
            }
            else
                return false;
            //defer.returnValue(None)
        }

        //读取用户队列的策略
        public string QueuePolicyGet(string authkey, string queue)
        {
            //queue = self.normalize(queue)

            var qpkey = string.Format(QUEUE_POLICY, authkey, queue);
            var db0 = Connection.GetDatabase(db: DB_KEYVALUE);
            var task = db0.StringGetAsync(qpkey);
            var val = Connection.Wait(task); //policy_id
            //val = yield self.redis.get(qpkey)
            var name = "unknown";
            var kv = this.policies.FirstOrDefault(t => t.Value.ToString() == val);
            if (kv.Key != null)
                name = kv.Key;

            return name; //策略名
        }

        //获取队列多条message
        public Tuple<int, Dictionary<string, string>> QueueTail(string authkey, string queue, int keyno = 10, bool delete_obj = false)
        {
            /*
                TAIL follows on GET, but returns keyno keys instead of only one key.
                keyno could be a LLEN function over the queue list, but it lends almost the same effect.
                LRANGE could too fetch the latest keys, even if there was less than keyno keys. MGET could be used too.
                TODO: does DELete belongs here ?
                returns a tuple (policy, returnvalues[])
            */
            //policy = None
            //queue = self.normalize(queue)
            var lkey = string.Format(QUEUE_NAME, authkey, queue);
            Dictionary<RedisKey, RedisValue> multivalue = new Dictionary<RedisKey, RedisValue>();

            var db0 = Connection.GetDatabase(db: DB_KEYVALUE);
            var db2 = Connection.GetDatabase(db: DB_QUEUE);
            //for a in range(keyno):
            for (int i = 0; i < keyno; i++)
            {
                var task = db2.ListRightPopAsync(lkey);
                var nk = Connection.Wait(task);
                //nk = yield self.redis.rpop(lkey)
                string t = "";
                RedisValue v;
                RedisKey okey = "";

                if (nk.HasValue)
                    t = (string)nk;
                else
                    continue;

                if (delete_obj == true)
                {
                    okey = t;//self.normalize(t)
                    t = string.Format("{0}:lock", (string)okey);
                    var task1 = db0.KeyRenameAsync(okey, t);
                    var ren = Connection.Wait(task1);
                    //ren = yield self.redis.rename(okey, t)
                    if (ren == false) //重命名 不成功
                        continue;

                    var task2 = db0.StringGetAsync(t);
                    v = Connection.Wait(task2); //取出
                    //v = yield self.redis.get(t)
                    var task3 = db0.KeyDeleteAsync(t);
                    var delk = Connection.Wait(task3);//删除
                    //delk = yield self.redis.delete(t)
                    if (delk == false)
                        continue;
                }
                else
                {
                    var task4 = db0.StringGetAsync(t);
                    v = Connection.Wait(task4); //取出
                    //v = yield self.redis.get(t)
                }

                multivalue[okey] = v;
            }

            var dic = multivalue.ToDictionary(k => (string)k.Key, vle => (string)vle.Value);

            var qpkey = string.Format(QUEUE_POLICY, authkey, queue);
            var task5 = db0.StringGetAsync(qpkey);
            var policy = Connection.Wait(task5); //policy_id
            //policy = yield self.redis.get(qpkey)
            return new Tuple<int, Dictionary<string, string>>(
                (policy.HasValue ? (int)policy : POLICY_BROADCAST),
                dic);
            //defer.returnValue((policy or POLICY_BROADCAST, multivalue))
        }

        public int QueueCountElements(string authkey, string queue)
        {
            // this is necessary to evaluate how many objects still undeleted on redis.
            // seems like it triggers a condition which the client disconnects from redis
            /*
               查找所有符合给定模式 pattern 的 key 。
               KEYS * 匹配数据库中所有 key 。
               KEYS h?llo 匹配 hello ， hallo 和 hxllo 等。
               KEYS h*llo 匹配 hllo 和 heeeeello 等。
               KEYS h[ae]llo 匹配 hello 和 hallo ，但不匹配 hillo 。
               特殊符号用 \ 隔开
             */
            try
            {
                var lkey = string.Format("{0}:{1}*", authkey, queue);
                var server = GetServer(Connection);
                var ll = server.Keys(database: DB_KEYVALUE, pattern: lkey).Count();
                //ll = yield self.redis.keys(lkey)
                //defer.returnValue({"objects":len(ll)})
                return ll;
            }
            catch (Exception ex)
            {
                //defer.returnValue({"error":str(e)})
                throw;
            }
        }

        public int QueueCountElementsUnActive(string authkey, string queue)
        {
            // this is necessary to evaluate how many objects still undeleted on redis.
            // seems like it triggers a condition which the client disconnects from redis
            /*
               查找所有符合给定模式 pattern 的 key 。
               KEYS * 匹配数据库中所有 key 。
               KEYS h?llo 匹配 hello ， hallo 和 hxllo 等。
               KEYS h*llo 匹配 hllo 和 heeeeello 等。
               KEYS h[ae]llo 匹配 hello 和 hallo ，但不匹配 hillo 。
               特殊符号用 \ 隔开
             */
            try
            {
                var lkey = string.Format("{0}:{1}*:lock", authkey, queue);
                var server = GetServer(Connection);
                var ll = server.Keys(database: DB_KEYVALUE, pattern: lkey).Count();
                //ll = yield self.redis.keys(lkey)
                //defer.returnValue({"objects":len(ll)})
                return ll;
            }
            catch (Exception ex)
            {
                
                //defer.returnValue({"error":str(e)})
                throw;
            }
        }

        public IEnumerable<string> QueueLastItems(string authkey, string queue, int count = 10)
        {
            /*
                returns a list with the last count items in the queue
            */
            //queue = self.normalize(queue)
            var lkey = string.Format(QUEUE_NAME, authkey, queue);
            var db2 = Connection.GetDatabase(db: DB_QUEUE);
            var task = db2.ListRangeAsync(lkey, start: 0, stop: count - 1);
            var multivalue = Connection.Wait(task);
            //multivalue = yield self.redis.lrange(lkey, 0, count-1)

            //defer.returnValue( multivalue)
            return multivalue.Select(v => (string)v);
        }

        //启。停
        public bool QueueChangestatus(string authkey, string queue, int status)
        {
            //Statuses: core.STOPQUEUE/core.STARTQUEUE"""
            if (status != this.STOPQUEUE && status != this.STARTQUEUE)
                return false;

            var key = string.Format("{0}{1}:{2}", QUEUE_STATUS, authkey, queue);
            var db0 = Connection.GetDatabase(db: DB_KEYVALUE);
            var task = db0.StringSetAsync(key, status);
            var res = Connection.Wait(task);
            //res = yield self.redis.set(key, status)
            //defer.returnValue({'queue':queue, 'status':status})
            return res;
        }

        public int QueueStatus(string authkey, string queue)
        {
            var key = string.Format("{0}{1}:{2}", QUEUE_STATUS, authkey, queue);
            var db0 = Connection.GetDatabase(db: DB_KEYVALUE);
            var task = db0.StringGetAsync(key);
            var res = Connection.Wait(task);
            //res = yield self.redis.get(key)
            //defer.returnValue({'queue':queue, 'status':res})
            if (res.HasValue)
                return (int)res;
            else
                return -1;
        }
        //删除 队列
        public bool QueuePurge(string authkey, string queue)
        {
            //TODO Must del all keys (or set expire)
            //it could rename the queue list, add to a deletion SET and use a task to clean it

            try
            {
                var db0 = Connection.GetDatabase(db: DB_KEYVALUE);
                var db2 = Connection.GetDatabase(db: DB_QUEUE);
                var db3 = Connection.GetDatabase(db: DB_SET);

                var server = GetServer(Connection);

                var lkey = string.Format("{0}:{1}*", authkey, queue);
                var ll = server.Keys(database: DB_KEYVALUE, pattern: lkey);
                var kk = ll.ToArray();

                var task1 = db0.KeyDeleteAsync(kk);
                var res = Connection.Wait(task1);

                lkey = string.Format("{0}{1}:{2}", QUEUE_STATUS, authkey, queue);
                var task2 = db0.KeyDeleteAsync(lkey);
                var res3 = Connection.Wait(task2);


                lkey = string.Format(QUEUE_NAME, authkey, queue);
                var task = db2.KeyDeleteAsync(lkey);
                var res2 = Connection.Wait(task);

                task = db3.SetRemoveAsync(QUEUESET, string.Format("{0}:{1}", authkey, queue));
                res2 = Connection.Wait(task);

                dynamic ret = Authorize(queue, authkey);
                IEnumerable<QueueModel> qlist = ret.Queues as IEnumerable<QueueModel>;
                if (qlist != null)
                {
                    var ruse = qlist.Where(t => t.Name != queue);
                    res2 = CreateAuthRecord(authkey, ruse, new string[] { "create" });
                }

                return res2;
            }
            catch (Exception ex)
            {
                //defer.returnValue({"error":str(e)})
                throw;
            }

            //res = yield self.redis.delete(lkey)
            //defer.returnValue({'queue':queue, 'status':res})
        }

        public long PubSub(string authkey, string queue_name, string content)
        {
            var key = string.Format("{0}:{1}:{2}", authkey, queue_name, this.PUBSUB_SUFIX);
            //var db4 = Connection.GetDatabase(db: DB_PUBSUB);
            //var task = db4.PublishAsync(key, content);
            //var r =  Connection.Wait(task);

            var sub = Connection.GetSubscriber();
            var task = sub.PublishAsync(key, content);
            var r = Connection.Wait(task);

            //r = yield self.redis.publish(key, content)
            return r;
        }
        //轮询用户所有队列数据 =》 用QueueTail代替
        [Obsolete("不建议使用阻塞轮询,请用QueueTail代替.")]
        public void QueueBlockMultiGet(string authkey, IEnumerable<string> queue_list)
        {
            /*
                waits on a list of queues, get back with the first queue that
                received data.
                this makes the redis locallity very important as if there are other
                instances doing the same the policy wont be respected. OTOH it makes
                it fast by not polling lists and waiting x seconds
            */

            /*
            sub.Subscribe(channel, delegate {
                string work = db.ListRightPop(key);
                if (work != null) Process(work);
            });
            //...
            db.ListLeftPush(key, newWork, flags: CommandFlags.FireAndForget);
            sub.Publish(channel, "");
             * stackExchage.Redis不建议使用Block阻塞方式调用 
             */

            //var ql = queue_list.Select(t=> string.Format(QUEUE_NAME, authkey, t));

            /*
            ql = [QUEUE_NAME % self.normalize(queue) for queue in queue_list]
            res = yield self.redis.brpop(ql) 
            db2.ListRightPopAsync(
            if res is not None:
                q = self.normalize(res[1])                                            
                qpkey = QUEUE_POLICY % q
                (p, v) = yield self.redis.mget([qpkey, q])
                defer.returnValue((q, p, {'key':q, 'value':v}))
            else:
                defer.returnValue(None)
            */
            throw new NotSupportedException("不建议使用阻塞轮询");
        }

        //队列状态查询
        public IEnumerable<string> MultiQueueByStatus(string authkey, IEnumerable<string> queue_list, int? filter_by = null)
        {
            if (!filter_by.HasValue)
                filter_by = this.STARTQUEUE;


            var query = queue_list.Select(t => string.Format("{0}{1}:{2}", QUEUE_STATUS, authkey, t));
            //ql = ["%s:%s" % (QUEUE_STATUS, self.normalize(queue)) for queue in queue_list]
            var ql = query.Cast<RedisKey>().ToArray();
            var db0 = Connection.GetDatabase(db: DB_KEYVALUE);
            var task = db0.StringGetAsync(ql);
            var res = Connection.Wait(task);
            //res = yield self.redis.mget(ql)
            var qs = res.Select(t => (t != filter_by.Value ? false : true));
            return query.Where((k, i) => qs.ElementAt(i));

            //qs = [True if r != filter_by else False for r in res]
            //r = itertools.compress(ql, qs)
            //defer.returnValue(list(r))
        }

    }
}
