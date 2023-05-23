using GHDatabase.Redis;
using GHDatabase.Redis.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace GHNETBASE.RTDB
{
    public class QueueList
    {
        #region Instance
        private static readonly QueueList _instance = new QueueList();
        RedisMqHelper redisMqHelper;
        int PrimaryPort = 6379, SlavePort = 6380, SecurePort = 6381;
        string PrimaryServer = "127.0.0.1", SecurePassword = "changeme", PrimaryPortString = "6379", SlavePortString = "6380", SecurePortString = "6381";


        private QueueList()
        {
            try
            {
                //PrimaryServer = System.Configuration.ConfigurationManager.AppSettings["ghRDSPrimaryServer"];
                PrimaryServer = GHIBMS.Common.ServerConfig.CloudMNSUrl.Split(':')[0];
                //int.TryParse(System.Configuration.ConfigurationManager.AppSettings["ghRDSPrimaryPort"], out PrimaryPort);
                //int.TryParse(System.Configuration.ConfigurationManager.AppSettings["ghRDSSlavePort"], out SlavePort);
                int.TryParse(GHIBMS.Common.ServerConfig.CloudMNSUrl.Split(':')[1], out PrimaryPort);
                int.TryParse(GHIBMS.Common.ServerConfig.CloudMNSUrl.Split(':')[1], out SlavePort);

                redisMqHelper = new RedisMqHelper(PrimaryServer, PrimaryPort, SlavePort);
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("实时库地址配置不正确.");
            }
        }
        public static QueueList SingletonInstance
        {
            get
            {
                return _instance;
            }
        }

        /// <summary>
        /// Redis关闭所有连接，释放资源
        /// </summary>
        public void Dispose()
        {
            if (redisMqHelper != null)
            {
                redisMqHelper.Close();
            }
        }
        #endregion

        public IEnumerable<QueueModelExt> GetAllQueueList(string authkey)
        {
            //授权信息
            dynamic ret = redisMqHelper.Authorize("", authkey);
            if (!ret.Auth)//未通过授权验证
                return new List<QueueModelExt>();
            IEnumerable<QueueModel> qlist = ret.Queues as IEnumerable<QueueModel>;

            return qlist.Select(m => new QueueModelExt()
            {
                CreateDate = m.CreateDate,
                DelaySeconds = m.DelaySeconds,
                MaximumMessageSize = m.MaximumMessageSize,
                MessageRetentionPeriod = m.MessageRetentionPeriod,
                MsgActiveCount = redisMqHelper.QueueLen(authkey, m.Name),//活跃消息数
                MsgUnActiveCount = redisMqHelper.QueueCountElementsUnActive(authkey, m.Name),//非活跃消息 
                Name = m.Name,
                PollingWaitSeconds = m.PollingWaitSeconds,
                UpdateDate = m.UpdateDate,
                VisibilityTimeout = m.VisibilityTimeout,
                Status = redisMqHelper.QueueStatus(authkey, m.Name).ToString()
            });
        }

        public bool QueueAdd(string authkey, string queueName, int maximumMessageSize, int messageRetentionPeriod)
        {
            //授权信息
            dynamic ret = redisMqHelper.Authorize(queueName, authkey);
            
            IEnumerable<QueueModel> qlist = ret.Queues as IEnumerable<QueueModel>;
            if (qlist == null)
                qlist = new List<QueueModel>();
            if (qlist.Count(t => t.Name == queueName) > 0)
                return false;//同名存在

            var lst = qlist.ToList();
            lst.Add(new GHDatabase.Redis.Models.QueueModel()
            {
                Name = queueName,
                MaximumMessageSize = maximumMessageSize,
                MessageRetentionPeriod = messageRetentionPeriod,
                CreateDate = DateTime.Now.ToUniversalTime(),
                UpdateDate = DateTime.Now.ToUniversalTime()
            });
            //if (!ret.Auth)

            return redisMqHelper.CreateAuthRecord(authkey, lst, new string[] { "create" });
        }

        public bool QueueUpdate(string authkey, string queueName, int maximumMessageSize, int messageRetentionPeriod)
        {
            //授权信息
            dynamic ret = redisMqHelper.Authorize(queueName, authkey);
            if (!ret.Auth)
                return false;

            IEnumerable<QueueModel> qlist = ret.Queues as IEnumerable<QueueModel>;
            if (qlist == null)
                return false;
            var model = qlist.FirstOrDefault(t => t.Name == queueName);
            if (model == null)
                return false;

            model.Name = queueName;
            model.MaximumMessageSize = maximumMessageSize;
            model.MessageRetentionPeriod = messageRetentionPeriod;
            //model.CreateDate = DateTime.Now.ToUniversalTime();
            model.UpdateDate = DateTime.Now.ToUniversalTime();

            return redisMqHelper.CreateAuthRecord(authkey, qlist, new string[] { "create" });
        }

        public bool QueueDelete(string authkey, string queueName)
        {
            return redisMqHelper.QueuePurge(authkey, queueName);
        }

        public bool MessageAdd(string authkey, string queueName, string value, TimeSpan? ttl = null)
        {
            return redisMqHelper.QueueAdd(authkey, queueName, value, ttl);
        }

        public string MessageGetDel(string authkey, string queueName)
        {
            dynamic ret = redisMqHelper.QueueGetDel(authkey, queueName);
            if (ret.Queue != null)
            {
                string key = ret.Queue.key;
                string value = ret.Queue.value;
                return value;
            }
            return "";
        }
        /// <summary>
        /// 获取多条消息
        /// </summary>
        /// <param name="authkey"></param>
        /// <param name="queueName"></param>
        /// <param name="len">长度</param>
        /// <returns>消息集合</returns>
        public List<string> MessageGetDel(string authkey, string queueName, int len = 10)
        {
            Tuple<int, Dictionary<string, string>> ret = redisMqHelper.QueueTail(authkey, queueName, len, true);
            var msgList = ret.Item2.Values.ToList();
            return msgList;
        }

        public string MessagePeek(string authkey, string queueName, out long refcount, out string key)
        {
            refcount = 0;
            key = "";
            dynamic ret = redisMqHelper.QueueGet(authkey, queueName, true);
            if (ret == null)
                return "";

            if (ret.Queue != null)
            {
                key = ret.Queue.key;
                string value = ret.Queue.value;
                refcount = ret.Queue.count;//引用消息次数
                return value;
            }
            return "";
        }

        public bool MessageChangeVisibility(string messagekey, TimeSpan? ttl)
        {
            return redisMqHelper.QueueMessageChangeVisbility(messagekey, ttl);
        }

        public bool MessageDel(string authkey, string queueName, string key)
        {
           return redisMqHelper.QueueDel(authkey, queueName, key);
        }
    }

    public class QueueModelExt : QueueModel
    {
        public long MsgActiveCount { get; set; }
        public long MsgUnActiveCount { get; set; }

        public string Status { get; set; }
    }
}