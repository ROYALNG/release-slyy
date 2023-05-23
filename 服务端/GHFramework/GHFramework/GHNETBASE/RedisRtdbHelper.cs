using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;

using StackExchange.Redis;
using GHCore.Serialization;
using System.Reflection;
using GHIBMS.Common.Serialization;
using GHIBMS.Common;

namespace GHDatabase.Redis
{
    public class RedisRtdbHelper : AbstractBase
    {
        //用于实时数据库
        public RedisRtdbHelper(string primaryServer, int primaryPort, int slavePort,string password)
            : base()
        {
            this.PrimaryServer = primaryServer;
            this.PrimaryPort = primaryPort;
            this.SlavePort = slavePort;
            this.Password = password;
        }
        #region common
        private ConcurrentDictionary<string, ConcurrentQueue<string>> pubsubCache = new ConcurrentDictionary<string, ConcurrentQueue<string>>();
        public string PrimaryServer { get; set; }
        public int PrimaryPort { get; set; }
        public int SlavePort { get; set; }
        public string Password { get; set; }

        protected override string GetConfiguration()
        {
            return this.PrimaryServer + ":" + this.PrimaryPort + "," + PrimaryServer + ":" + SlavePort;
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
                            if (connection != null) {
                                connection.Close();
                                connection.Dispose();
                            }
                            //connection =  ConnectionMultiplexer.Connect("yhdcache0.redis.cache.windows.net,ssl=true,password=...");
#if DEBUG
                            //string logFile = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "redisLog.txt");
                            //System.IO.FileStream _file = new System.IO.FileStream(logFile, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite);
                            //System.IO.StreamWriter sw = new System.IO.StreamWriter(_file.);
                            connection = Create(password:this.Password,log: Console.Out, connectTimeout:5000);
#else
                    connection =  Create(password:this.Password,connectTimeout:5000);
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
        #endregion

        #region String get/set
        public long StringIncrement(int dbInx, string key, long value = 1)
        {
            var db = Connection.GetDatabase(dbInx);
            var task = db.StringIncrementAsync(key, value);
            var count = Connection.Wait(task);
            return count;
        }
        public long StringDecrement(int dbInx, string key, long value = 1)
        {
            var db = Connection.GetDatabase(dbInx);
            var task = db.StringDecrementAsync(key, value);
            var count = Connection.Wait(task);
            return count;
        }

        
        public bool StringSet(int dbInx, string key, string value, TimeSpan? ttl = null)
        {
            var db = Connection.GetDatabase(dbInx);
            var task = db.StringSetAsync(key, value, ttl);
            var val = Connection.Wait(task);
            return val;
        }

        public string StringGet(int dbInx, string key)
        {
            var db = Connection.GetDatabase(dbInx);
            var task = db.StringGetAsync(key);
            var val = Connection.Wait(task);
            return val.HasValue ? (string)val : "";
        }

        public IEnumerable<string> StringGet(int dbInx, IEnumerable<string> key)
        {
            var db = Connection.GetDatabase(dbInx);
            var rkey = key.Select(k => (RedisKey)k);
            var task = db.StringGetAsync(rkey.ToArray());
            var val = Connection.Wait(task);
            return val.Select(t => t.HasValue ? (string)t : "");
        }
        public bool StringRemove(int dbInx, string key)
        {
            var db = Connection.GetDatabase(dbInx);
            var task = db.KeyDeleteAsync(key);
            var val = Connection.Wait(task);
            return val;
        }

        public long DeleteKeys(int dbInx, string pattern)
        {
            var server = GetServer(Connection);
            var ll = server.Keys(dbInx, pattern: pattern);
            var kk = ll.ToArray();

            var db = Connection.GetDatabase(dbInx);
            var task = db.KeyDeleteAsync(kk);
            var res = Connection.Wait(task);
            return res;
        }


        #endregion

        #region Set add/remove/list
        /// <summary>
        /// Set Add
        /// </summary>
        public bool SetAdd(int dbInx, string key, string value)
        {
            var db = Connection.GetDatabase(dbInx);
            var task = db.SetAddAsync(key, value);
            var val = Connection.Wait(task);
            return val;
        }
        /// <summary>
        /// Set List all for key
        /// </summary>
        public IEnumerable<string> SetMembers(int dbInx, string key)
        {
            var db = Connection.GetDatabase(dbInx);
            //var task = db.SetMembersAsync(key);
            //var val = Connection.Wait(task);

            var val = db.SetScan(key);
            return val.Select(t => t.HasValue ? (string)t : "");
        }
        /// <summary>
        /// Set Remove from key
        /// </summary>
        public bool SetRemove(int dbInx, string key, string value)
        {
            var db = Connection.GetDatabase(dbInx);
            var task = db.SetRemoveAsync(key, value);
            var val = Connection.Wait(task);
            return val;
        }

        public IEnumerable<string> SetSearch(int dbInx, string key, string pattern)
        {
            var db = Connection.GetDatabase(dbInx);
            var val = db.SetScan(key, pattern: pattern);
            return val.Select(t=>(string)t);
        }
        public bool SetContains(int dbInx,string key,string value)
        {
            var db = Connection.GetDatabase(dbInx);
            return db.SetContains(key, value);
          
        }
        /// <summary>
        /// search set keys
        /// </summary>
        public IEnumerable<string> SetKeys(int dbInx, string pattern)
        {
            var server = GetServer(Connection);
            var ll = server.Keys(dbInx, pattern: pattern);
            var ar = ll.ToArray();

            return ll.Select(t => (string)t);
        }
        /// <summary>
        /// Set List Length for key
        /// </summary>
        public long SetLength(int dbInx, string key)
        {
            var db = Connection.GetDatabase(dbInx);
            var task = db.SetLengthAsync(key);
            var val = Connection.Wait(task);
            return val;
        }
        #endregion

        #region SortedSet add/remove/list
        public bool SortedSetAdd(int dbInx, string key, string value, double score)
        {
            var db = Connection.GetDatabase(dbInx);
            var task = db.SortedSetAddAsync(key: key, member: value, score: score);
            var val = Connection.Wait(task);
            return val;
        }

        public bool SortedSetRemove(int dbInx, string key, string value)
        {
            var db = Connection.GetDatabase(dbInx);
            var task = db.SortedSetRemoveAsync(key, value);
            var val = Connection.Wait(task);
            return val;
        }

        public long SortedSetLength(int dbInx, string key)
        {
            var db = Connection.GetDatabase(dbInx);
            var task = db.SortedSetLengthAsync(key);
            var val = Connection.Wait(task);
            return val;
        }

        public IEnumerable<string> SortedSetSearch(int dbInx, string key, string pattern)
        {
            var db = Connection.GetDatabase(dbInx);
            var val = db.SortedSetScan(key, pattern: pattern);
            return val.Select(t => (string)t.Element);
        }

        public IEnumerable<string> SortedSetMembers(int dbInx, string key)
        {
            var db = Connection.GetDatabase(dbInx);
            //var task = db.SetMembersAsync(key);
            //var val = Connection.Wait(task);

            var val = db.SortedSetScan(key);
            return val.Where(w => w.Element.HasValue).Select(t => t.Element.HasValue ? (string)t.Element : "");
        }

        #endregion

        #region pub/sub
        public void Subscribe(string channel, Action<string, string> handler=null)
        {
            var sub = Connection.GetSubscriber();
            Action<RedisChannel, RedisValue> handler1 = (rchannel, payload) =>
            {
                if (handler == null)
                    pubsubCacheAdd(rchannel, payload);
                else
                    handler((string)rchannel, (string)payload);
            };
            sub.Subscribe(channel, handler1);
        }

        public void Unsubscribe(string channel)
        {
            var sub = Connection.GetSubscriber();
            sub.Unsubscribe(channel);

            ConcurrentQueue<string> q = new ConcurrentQueue<string>();
            pubsubCache.TryRemove(channel, out q);
        }

        public void UnsubscribeAll()
        {
            var sub = Connection.GetSubscriber();
            sub.UnsubscribeAll();

            pubsubCache.Clear();
        }

        public long Publish(string channel, string message)
        {
            var sub = Connection.GetSubscriber();
            return sub.Publish(channel, message);
        }

        private void pubsubCacheAdd(string channel, string payload)
        {
            if (pubsubCache.ContainsKey(channel))
            {
                if (pubsubCache[channel].Count>10) //暂存10条
                    pubsubCache[channel].TryDequeue(out string result);
                pubsubCache[channel].Enqueue(payload);
            }
            else
            {
                pubsubCache[channel] = new ConcurrentQueue<string>(new string[] { payload });
            }
        }
        /// <summary>
        /// 获取一条 订阅 接收到的消息
        /// </summary>
        /// <param name="channel">channel</param>
        /// <returns>string</returns>
        public string GetSubActionValue(string channel)
        {
            string ret = "";
            if (pubsubCache.ContainsKey(channel))
                pubsubCache[channel].TryDequeue(out ret);
            return ret;
        }
        #endregion

        #region Redis Hash散列数据类型操作
        /// <summary>
        /// Redis散列数据类型  批量新增
        /// </summary>
        public void HashSet(int dbInx, string key, List<HashEntry> hashEntrys, CommandFlags flags = CommandFlags.None)
        {
            var _db = Connection.GetDatabase(dbInx);
            _db.HashSet(key, hashEntrys.ToArray(), flags);
        }
        /// <summary>
        /// Redis散列数据类型  新增一个
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="val"></param>
        public void HashSet<T>(int dbInx, string key, string field, T val, When when = When.Always, CommandFlags flags = CommandFlags.None)
        {
            var _db = Connection.GetDatabase(dbInx);
            _db.HashSet(key, field, JSONFormatter.Serialize(val), when, flags);
        }
        /// <summary>
        /// 复杂类的对象 转化为List<HashEntry>  此方法不支持List等对象，需另外封装
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public List<HashEntry> HashSetObject<T>(int dbInx, string key, T obj) where T : class, new()
        {
            //var people = new People() { Name = "ws", Age = 18 };
            List<HashEntry> list = new List<HashEntry>();
            foreach (PropertyInfo p in obj.GetType().GetProperties())
            {
                var name = p.Name.ToString();
                var val = p.GetValue(obj);
                Type dbType = p.PropertyType;
                if (dbType.Name.ToLower() == "string" || dbType.IsPrimitive || dbType.IsEnum || dbType.Name.ToLower() == "string[]")
                {

                    list.Add(new HashEntry(name, PropertyFormatter.Serialize(p, obj)));
                }
            }

            HashSet(dbInx, key, list);
            return list;
        }

        public List<HashEntry> HashSetObject(int dbInx, string key, Dictionary<string ,string> obj)
        {
            //var people = new People() { Name = "ws", Age = 18 };
            List<HashEntry> list = new List<HashEntry>();
            foreach (var p in obj)
            {
                list.Add(new HashEntry(p.Key,p.Value));
            }

            //foreach (PropertyInfo p in obj.GetType().GetProperties())
            //{
            //    var name = p.Name.ToString();
            //    var val = p.GetValue(obj);
            //    Type dbType = p.PropertyType;
            //    if (dbType.Name.ToLower() == "string" || dbType.IsPrimitive || dbType.IsEnum || dbType.Name.ToLower() == "string[]")
            //    {

            //        list.Add(new HashEntry(name, PropertyFormatter.Serialize(p, obj)));
            //    }
            //}

            HashSet(dbInx, key, list);
            return list;
        }
        /// <summary>
        ///  Redis散列数据类型 获取指定key的指定field
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public T HashGet<T>(int dbInx, string key, string field)
        {
            var _db = Connection.GetDatabase(dbInx);
            return JSONFormatter.Deserialize<T>(_db.HashGet(key, field));
        }
        /// <summary>
        ///  Redis散列数据类型 获取所有field所有值,以 HashEntry[]形式返回
        /// </summary>
        /// <param name="key"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public HashEntry[] HashGetAll(int dbInx, string key, CommandFlags flags = CommandFlags.None)
        {
            var _db = Connection.GetDatabase(dbInx);
            return _db.HashGetAll(key, flags);
        }
        public T HashGetObject<T>(int dbInx, string key) where T : class, new()
        {
            T obj= new T();
            HashEntry[] items =HashGetAll(dbInx, key);

            System.Reflection.PropertyInfo[] propertyInfo = typeof(T).GetProperties(System.Reflection.BindingFlags.Public |
                                                         System.Reflection.BindingFlags.Instance);
            foreach (var attr in items)
            {
                string attrName = attr.Name.ToString();
                string attrValue = attr.Value.ToString();
                foreach (System.Reflection.PropertyInfo pinfo in propertyInfo)
                {

                    if (pinfo != null && pinfo.CanWrite)
                    {
                        string name = pinfo.Name.ToLower();
                        Type dbType = pinfo.PropertyType;
                        if (name == attrName)
                        {
                            if (String.IsNullOrEmpty(attrValue))
                                continue;
                            try
                            {
                                if (dbType.Name.ToLower() == "string" || dbType.IsPrimitive || dbType.IsEnum || dbType.Name.ToLower() == "string[]")
                                {
                                    object p = PropertyFormatter.ChangeType(attrValue, dbType);
                                    pinfo.SetValue(obj, p, null);
                                }
                                else
                                {
                                    //Debug.WriteLine(dbType.Name);
                                }
                            }
                            catch (Exception ex)
                            {
                                Logger.GetInstance().LogError(ex.ToString());
                            }
                        }
                    }
                }
            }
            return obj;
        }
        public Dictionary<string,string> HashGetObject(int dbInx, string key)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            HashEntry[] items = HashGetAll(dbInx, key);
            foreach (HashEntry he in items)
            {
                dic.Add(he.Name, he.Value);
            }
            return dic;

        }
        /// <summary>
        /// Redis散列数据类型 获取key中所有field的值。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public List<T> HashGetAllValues<T>(int dbInx, string key, CommandFlags flags = CommandFlags.None)
        {
            var _db = Connection.GetDatabase(dbInx);
            List<T> list = new List<T>();
            var hashVals = _db.HashValues(key, flags).ToArray();
            foreach (var item in hashVals)
            {
                
                list.Add(JSONFormatter.Deserialize<T>(item));
            }
            return list;
        }

        /// <summary>
        /// Redis散列数据类型 获取所有Key名称
        /// </summary>
        /// <param name="key"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public string[] HashGetAllKeys(int dbInx, string key, CommandFlags flags = CommandFlags.None)
        {
            var _db = Connection.GetDatabase(dbInx);
            return _db.HashKeys(key, flags).ToStringArray();
        }
        /// <summary>
        ///  Redis散列数据类型  单个删除field
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public bool HashDelete(int dbInx, string key, string hashField, CommandFlags flags = CommandFlags.None)
        {
            var _db = Connection.GetDatabase(dbInx);
            return _db.HashDelete(key, hashField, flags);
        }
        /// <summary>
        ///  Redis散列数据类型  批量删除field
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashFields"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public long HashDelete(int dbInx, string key, string[] hashFields, CommandFlags flags = CommandFlags.None)
        {
            var _db = Connection.GetDatabase(dbInx);
            List<RedisValue> list = new List<RedisValue>();
            for (int i = 0; i < hashFields.Length; i++)
            {
                list.Add(hashFields[i]);
            }
            return _db.HashDelete(key, list.ToArray(), flags);
        }
        /// <summary>
        ///  Redis散列数据类型 判断指定键中是否存在此field
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public bool HashExists(int dbInx, string key, string field, CommandFlags flags = CommandFlags.None)
        {
            var _db = Connection.GetDatabase(dbInx);
            return _db.HashExists(key, field, flags);
        }
        /// <summary>
        /// Redis散列数据类型  获取指定key中field数量
        /// </summary>
        /// <param name="key"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public long HashLength(int dbInx, string key, CommandFlags flags = CommandFlags.None)
        {
            var _db = Connection.GetDatabase(dbInx);
            return _db.HashLength(key, flags);
        }
        /// <summary>
        /// Redis散列数据类型  为key中指定field增加incrVal值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="incrVal"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public double HashIncrement(int dbInx, string key, string field, double incrVal, CommandFlags flags = CommandFlags.None)
        {
            var _db = Connection.GetDatabase(dbInx);
            return _db.HashIncrement(key, field, incrVal, flags);
        }
       
        #endregion



    }
}
