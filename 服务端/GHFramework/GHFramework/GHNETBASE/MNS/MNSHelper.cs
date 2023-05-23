using GHCore;
using GHCore.Serialization;
using GHIBMS.Common;
using GHNETBASE.RTDB;
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
        //public bool WriteQueueMessage(string queueBaseUrl, string queueName, string value)
        public bool WriteQueueMessage(string queueName, string value)
        {
            if (!checkLogin())
                return false;

            //GHRESTFul.Client.MQSClient mnsclient = new GHRESTFul.Client.MQSClient(queueBaseUrl, OAUTH.OAuthHelper.SingletonInstance.UUID, "");
            //var mnsQueue = mnsclient.getQueue("/api/" + queueName);
            ////string sendmsg = string.Format("{0}:{1}:{2},{3}", ChlKey, CtrlKey, VarKey, Value);
            //var ret = mnsQueue.sendMessage(value);

            //if (string.IsNullOrWhiteSpace(ret.Code))
            //    return true;
            //else
            //{
            //    System.Diagnostics.Debug.WriteLine(ret.Message);

            //    return false;
            //}
            try
            {
                if (string.IsNullOrWhiteSpace(OAUTH.OAuthHelper.SingletonInstance.UUID))
                {
                    GHLogger.Log("authkey不存在", LogCategory.Warn);
                    return false;
                }
                else
                {
                    return QueueList.SingletonInstance.MessageAdd(OAUTH.OAuthHelper.SingletonInstance.UUID, queueName, value);
                }
            }
            catch (Exception ex)
            {
                GHLogger.Log(ex.Message, LogCategory.Warn);
                return false;
            }
            
        }
        /// <summary>
        /// 写入队列数据
        /// </summary>
        /// <param name="queueBaseUrl">MNS消息队列基地址</param>
        /// <param name="queueName">消息队列名称</param>
        /// <param name="value">写入队列的值</param>
        //public bool WriteQueueMessage(string queueBaseUrl, string queueName, object value)
        public bool WriteQueueMessage(string queueName, object value)
        {
            if (!checkLogin())
                return false;

            string jsonValue = JSONFormatter.Serialize(value);
            return WriteQueueMessage(queueName, jsonValue);
        }

        /// <summary>
        /// 读取一个队列消息，并从队列里删除
        /// </summary>
        /// <param name="queueBaseUrl">MNS消息队列基地址</param>
        /// <param name="queueName">消息队列名称</param>
        /// <returns></returns>
        //public string ReadQueueMessage(string queueBaseUrl, string queueName)
        public string ReadQueueMessage(string queueName)
        {
            if (!checkLogin())
                return "";

            //GHRESTFul.Client.MQSClient mnsclient = new GHRESTFul.Client.MQSClient(queueBaseUrl, OAUTH.OAuthHelper.SingletonInstance.UUID, "");
            //var mnsQueue = mnsclient.getQueue("/api/" + queueName);
            ////string sendmsg = string.Format("{0}:{1}:{2},{3}", ChlKey, CtrlKey, VarKey, Value);
            //var ret = mnsQueue.popMessage();

            //if (string.IsNullOrWhiteSpace(ret.Code))
            //    return ret.MessageBody;
            //else
            //{
            //    System.Diagnostics.Debug.WriteLine(ret.Message);

            //    return "";
            //}
            try
            {
                if (string.IsNullOrWhiteSpace(OAUTH.OAuthHelper.SingletonInstance.UUID))
                {
                    GHLogger.Log("authkey不存在", LogCategory.Warn);
                    return "";
                }
                else
                {
                    return QueueList.SingletonInstance.MessageGetDel(OAUTH.OAuthHelper.SingletonInstance.UUID, queueName);
                }
            }
            catch (Exception ex)
            {
                GHLogger.Log(ex.Message, LogCategory.Warn);
                return "";
            }
        }
        /// <summary>
        /// 读取多个队列消息，并从队列里删除
        /// </summary>
        /// <param name="queueBaseUrl">MNS消息队列基地址</param>
        /// <param name="queueName">消息队列名称</param>
        /// <returns></returns>
        public List<string> ReadQueueMessageTail(string queueName,int len = 100)
        {
            if (!checkLogin())
                return new List<string>();

            try
            {
                //if (string.IsNullOrWhiteSpace(OAUTH.OAuthHelper.SingletonInstance.UUID))
                //{
                //    GHLogger.Log("authkey不存在", LogCategory.Warn);
                //    return new List<string>();
                //}
                //else
                //{
                //    return QueueList.SingletonInstance.MessageGetDel(OAUTH.OAuthHelper.SingletonInstance.UUID, queueName, len);
                //}

                if (string.IsNullOrWhiteSpace(ServerConfig.MnsAuthKey))
                {
                    GHLogger.Log("authkey不存在", LogCategory.Warn);
                    return new List<string>();
                }
                else
                {
                    return QueueList.SingletonInstance.MessageGetDel(ServerConfig.MnsAuthKey, queueName, len);
                }
            }
            catch (Exception ex)
            {
                GHLogger.Log(ex.Message, LogCategory.Warn);
                return null;
            }
        }
        /// <summary>
        /// 读取一个队列消息，并从队列里删除
        /// </summary>
        /// <param name="queueBaseUrl">MNS消息队列基地址</param>
        /// <param name="queueName">消息队列名称</param>
        /// <returns></returns>
        //public T ReadQueueMessage<T>(string queueBaseUrl, string queueName)
        public T ReadQueueMessage<T>(string queueName)
        {
            if (!checkLogin())
                return default(T);

            string json = ReadQueueMessage(queueName);
            if (string.IsNullOrEmpty(json))
                return default(T);
            return JSONFormatter.Deserialize<T>(json);
        }
        /// <summary>
        /// 读取一个队列消息，不从队列删除
        /// </summary>
        /// <param name="queueBaseUrl">MNS消息队列基地址</param>
        /// <param name="queueName">消息队列名称</param>
        /// <returns></returns>
        //public string PopQueueMessage(string queueBaseUrl, string queueName)
        public string PopQueueMessage(string queueName)
        {
            if (!checkLogin())
                return "";

            //GHRESTFul.Client.MQSClient mnsclient = new GHRESTFul.Client.MQSClient(queueBaseUrl, OAUTH.OAuthHelper.SingletonInstance.UUID, "");
            //var mnsQueue = mnsclient.getQueue("/api/" + queueName);
            ////string sendmsg = string.Format("{0}:{1}:{2},{3}", ChlKey, CtrlKey, VarKey, Value);
            //var ret = mnsQueue.peekMessage();

            //if (string.IsNullOrWhiteSpace(ret.Code))
            //    return ret.MessageBody;
            //else
            //{
            //    System.Diagnostics.Debug.WriteLine(ret.Message);

            //    return "";
            //}
            try
            {
                if (string.IsNullOrWhiteSpace(OAUTH.OAuthHelper.SingletonInstance.UUID))
                {
                    GHLogger.Log("authkey不存在", LogCategory.Warn);
                    return "";
                }
                else
                {
                    long refcount = 0;//消息被引用的次数
                    string receiptHandle = "";//取出的 message key
                    return QueueList.SingletonInstance.MessagePeek(OAUTH.OAuthHelper.SingletonInstance.UUID, queueName, out refcount, out receiptHandle);
                }
            }
            catch (Exception ex)
            {
                GHLogger.Log(ex.Message, LogCategory.Warn);
                return "";
            }
        }
        /// <summary>
        /// 读取一个队列消息，不从队列删除
        /// </summary>
        /// <param name="queueBaseUrl">MNS消息队列基地址</param>
        /// <param name="queueName">消息队列名称</param>
        /// <returns></returns>
        //public T PopQueueMessage<T>(string queueBaseUrl, string queueName)
        public T PopQueueMessage<T>(string queueName)
        {
            if (!checkLogin())
                return default(T);

            string json = PopQueueMessage(queueName);
            if (string.IsNullOrEmpty(json))
                return default(T);
            return JSONFormatter.Deserialize<T>(json);
        }

    }
}
