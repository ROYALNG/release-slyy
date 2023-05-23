using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;

using StackExchange.Redis;

namespace GHDatabase.Redis
{
    public class RedisBase : AbstractBase
    {
        public RedisBase(string primaryServer, int primaryPort, int slavePort)
            : base()
        {
            this.PrimaryServer = primaryServer;
            this.PrimaryPort = primaryPort;
            this.SlavePort = slavePort;
        }
        #region common
        private ConcurrentDictionary<string, ConcurrentQueue<string>> pubsubCache = new ConcurrentDictionary<string, ConcurrentQueue<string>>();
        public string PrimaryServer { get; set; }
        public int PrimaryPort { get; set; }
        public int SlavePort { get; set; }

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
                            //connection =  ConnectionMultiplexer.Connect("yhdcache0.redis.cache.windows.net,ssl=true,password=...");
#if DEBUG
                            //string logFile = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "redisLog.txt");
                            //System.IO.FileStream _file = new System.IO.FileStream(logFile, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite);
                            //System.IO.StreamWriter sw = new System.IO.StreamWriter(_file.);
                            connection = Create(log: Console.Out);
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
        #endregion

        #region String get/set
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
            var task = db.SetMembersAsync(key);
            var val = Connection.Wait(task);
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
                pubsubCache[channel].Enqueue(payload);
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



    }
}
