using GHCore.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GHNETBASE.MNS
{
    public class MNSHelper
    {
        #region Instance
        private static readonly MNSHelper _instance = new MNSHelper();
        private MNSHelper()
        {
        }
        public static MNSHelper SingletonInstance
        {
            get
            {
                return _instance;
            }
        }
        #endregion

        private bool checkLogin()
        {
            if (!string.IsNullOrEmpty(OAUTH.OAuthHelper.SingletonInstance.UUID))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 写入队列数据
        /// </summary>
        /// <param name="queueBaseUrl">MNS消息队列基地址</param>
        /// <param name="queueName">消息队列名称</param>
        /// <param name="value">写入队列的值</param>
        /// <returns></returns>
        public bool WriteQueueMessage(string queueBaseUrl, string queueName, string value)
        {
            if (!checkLogin())
                return false;

            //OAUTH.OAuthHelper.SingletonInstance.UUID; 
            //todo

            return true;
        }
        /// <summary>
        /// 写入队列数据
        /// </summary>
        /// <param name="queueBaseUrl">MNS消息队列基地址</param>
        /// <param name="queueName">消息队列名称</param>
        /// <param name="value">写入队列的值</param>
        public bool WriteQueueMessage(string queueBaseUrl, string queueName, object value)
        {
            if (!checkLogin())
                return false;

            string jsonValue = JSONFormatter.Serialize(value);
            return WriteQueueMessage(queueBaseUrl, queueName, jsonValue);
        }

        /// <summary>
        /// 读取一个队列消息，并从队列里删除
        /// </summary>
        /// <param name="queueBaseUrl">MNS消息队列基地址</param>
        /// <param name="queueName">消息队列名称</param>
        /// <returns></returns>
        public string ReadQueueMessage(string queueBaseUrl, string queueName)
        {
            if (!checkLogin())
                return "";

            //todol
            return "";
        }
        /// <summary>
        /// 读取一个队列消息，并从队列里删除
        /// </summary>
        /// <param name="queueBaseUrl">MNS消息队列基地址</param>
        /// <param name="queueName">消息队列名称</param>
        /// <returns></returns>
        public T ReadQueueMessage<T>(string queueBaseUrl, string queueName)
        {
            if (!checkLogin())
                return default(T);

            string json = ReadQueueMessage(queueBaseUrl, queueName);
            return JSONFormatter.Deserialize<T>(json);
        }
        /// <summary>
        /// 读取一个队列消息，不从队列删除
        /// </summary>
        /// <param name="queueBaseUrl">MNS消息队列基地址</param>
        /// <param name="queueName">消息队列名称</param>
        /// <returns></returns>
        public string PopQueueMessage(string queueBaseUrl, string queueName)
        {
            if (!checkLogin())
                return "";

            //todol
            return "";
        }
        /// <summary>
        /// 读取一个队列消息，不从队列删除
        /// </summary>
        /// <param name="queueBaseUrl">MNS消息队列基地址</param>
        /// <param name="queueName">消息队列名称</param>
        /// <returns></returns>
        public T PopQueueMessage<T>(string queueBaseUrl, string queueName)
        {
            if (!checkLogin())
                return default(T);

            string json = ReadQueueMessage(queueBaseUrl, queueName);
            return JSONFormatter.Deserialize<T>(json);
        }

    }
}
