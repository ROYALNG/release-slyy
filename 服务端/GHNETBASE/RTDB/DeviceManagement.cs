using GHDatabase.Redis;
using GHDatabase.Redis.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace GHNETBASE.RTDB
{
    /// <summary>
    /// 设备管理 - 实时库
    /// </summary>
    public class DeviceManagement
    {
        #region Instance
        private static readonly DeviceManagement _instance = new DeviceManagement();
        RedisBase redisHelper;
        int PrimaryPort = 6379, SlavePort = 6380, SecurePort = 6381;
        string PrimaryServer = "127.0.0.1", SecurePassword = "changeme", PrimaryPortString = "6379", SlavePortString = "6380", SecurePortString = "6381";


        private DeviceManagement()
        {
            try
            {
                //PrimaryServer = System.Configuration.ConfigurationManager.AppSettings["ghRDSPrimaryServer"];
                PrimaryServer = GHIBMS.Common.ServerConfig.CloudRtdbUrl.Split(':')[0];
                //int.TryParse(System.Configuration.ConfigurationManager.AppSettings["ghRDSPrimaryPort"], out PrimaryPort);
                //int.TryParse(System.Configuration.ConfigurationManager.AppSettings["ghRDSSlavePort"], out SlavePort);
                int.TryParse(GHIBMS.Common.ServerConfig.CloudRtdbUrl.Split(':')[1], out PrimaryPort);
                int.TryParse(GHIBMS.Common.ServerConfig.CloudRtdbUrl.Split(':')[1], out SlavePort);

                redisHelper = new RedisBase(PrimaryServer, PrimaryPort, SlavePort);
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("实时库地址配置不正确.");
            }
        }
        public static DeviceManagement SingletonInstance
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
            if (redisHelper != null)
            {
                redisHelper.Close();
            }
        }
        #endregion

        private int channelDbInx = 0; //通道数据蔟
        private string channelKeyPrefix = "chl:{0}:{1}"; //0 authkey, 1 通道名称
        private int controlDbInx = 1; //控制器数据蔟
        private string controlKeyPrefix = "ctrl:{0}:{1}:{2}"; //0 authkey, 1 通道名称, 2 控制器名称
        private int variableDbInx = 3; //变量数据蔟
        private string variableKeyPrefix = "var:{0}:{1}:{2}:{3}"; //0 authkey, 1 通道名称, 2 控制器名称, 3 变量名称

        private int channelSetDbInx = 4;//键名集合数据蔟
        private string channelSetKeyName = "CHLSET:{0}";//保存通道key 0 authkey
        private string controlSetKeyName = "CHLSET:{0}:{1}";//保存控制器key 0 authkey, 1 通道名称
        private string variableSetKeyName = "CHLSET:{0}:{1}:{2}";//保存变量key 0 authkey, 1 通道名称, 2 控制器名称

        private string pubSubChannelName = "DeviceManagement";


        private int amsDbInx = 8; //实时报警
        private string amsKeyPrefix = "AMS:{0}:{1}:{2}:{3}"; //0 authkey, 1 报警来源, 2 报警级别， 3 报警GUID

        private int amsSetDbInx = 9;//键名集合数据蔟
        private string amsSetKeyName = "AMSSET:{0}";//保存实时库key 0 authkey

        #region 通道
        /// <summary>
        /// 添加通道
        /// </summary>
        /// <param name="authkey">authkey</param>
        /// <param name="chlName">通道名称</param>
        /// <param name="value">通道属性(序列化json数据)</param>
        /// <param name="ttl">设置是否使用超时</param>
        /// <returns>bool</returns>
        public bool ChlAdd(string authkey, string chlName, string value, TimeSpan? ttl=null)
        {
            //if (ChlSearchKey(authkey, chlName).Count() == 0)
            {
                redisHelper.SetAdd(channelSetDbInx, string.Format(channelSetKeyName, authkey), string.Format(channelKeyPrefix, authkey, chlName));
                return redisHelper.StringSet(channelDbInx, string.Format(channelKeyPrefix, authkey, chlName), value, ttl);
            }
            return false;//重名
        }
        /// <summary>
        /// 修改通道
        /// </summary>
        /// <param name="authkey">authkey</param>
        /// <param name="chlName">通道名称</param>
        /// <param name="value">通道属性(序列化json数据)</param>
        /// <param name="ttl">设置是否使用超时</param>
        /// <returns>bool</returns>
        public bool ChlUpdate(string authkey, string chlName, string value, TimeSpan? ttl = null)
        {
            //if (ChlSearchKey(authkey, chlName).Count() > 0)
            {
                return redisHelper.StringSet(channelDbInx, string.Format(channelKeyPrefix, authkey, chlName), value, ttl);
            }
            return false;//键名不存在
        }

        /// <summary>
        /// 删除通道
        /// </summary>
        /// <param name="authkey">authkey</param>
        /// <param name="chlName">通道名称</param>
        /// <returns></returns>
        public bool ChlRemove(string authkey, string chlName)
        {
            try
            {
                redisHelper.DeleteKeys(variableDbInx, string.Format("var:{0}:{1}*", authkey, chlName));//变量
                redisHelper.DeleteKeys(controlDbInx, string.Format("ctrl:{0}:{1}*", authkey, chlName));//控制器
                foreach (var ctl in CtrlGetAll(authkey, chlName))
                {
                    foreach (var v in VarGetAll(authkey, chlName, ctl))
                    {//删除所有变量索引
                        redisHelper.SetRemove(channelSetDbInx, string.Format(variableSetKeyName, authkey, chlName, ctl), string.Format(variableKeyPrefix, authkey, chlName, ctl, v));//变量列表
                    }
                    //删除所有控制器索引
                    redisHelper.SetRemove(channelSetDbInx, string.Format(controlSetKeyName, authkey, chlName), string.Format(controlKeyPrefix, authkey, chlName, ctl));//控制器列表
                }

                var val = redisHelper.StringRemove(channelDbInx, string.Format(channelKeyPrefix, authkey, chlName));//通道
                val = redisHelper.SetRemove(channelSetDbInx, string.Format(channelSetKeyName, authkey), string.Format(channelKeyPrefix, authkey, chlName));//通道列表
                if (val)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取通道values
        /// </summary>
        /// <param name="keys">通道keys</param>
        /// <returns><![CDATA[IEnumerable<string>]]></returns>
        public IEnumerable<string> ChlGetValues(string authkey, IEnumerable<string> keys)
        {
            var ks = keys.Select(t => string.Format(channelKeyPrefix, authkey, t)).ToList();

            return redisHelper.StringGet(channelDbInx, ks);
        }
        /// <summary>
        /// 获取通道value
        /// </summary>
        /// <param name="keys">通道key</param>
        /// <returns><![CDATA[IEnumerable<string>]]></returns>
        public string ChlGetValue(string authkey, string key)
        {
            var ks = string.Format(channelKeyPrefix, authkey, key);

            return redisHelper.StringGet(channelDbInx, ks);
        }

        /// <summary>
        /// 获取所有通道名称
        /// </summary>
        /// <param name="authkey">authkey</param>
        /// <returns></returns>
        public IEnumerable<string> ChlGetAll(string authkey)
        {
            //"chl:{0}:{1}"; //0 authkey, 1 通道名称(含通道 或 IO:通道)
            return redisHelper.SetMembers(channelSetDbInx, string.Format(channelSetKeyName, authkey))
                .Select(t => string.IsNullOrEmpty(t) ? "" : (t.Split(':').Length == 3 ? t.Split(':')[2] : (t.Split(':')[2] + ":" + t.Split(':')[3])));
        }

        /// <summary>
        /// 通道名称搜索 支持模糊匹配"* ?"
        /// </summary>
        /// <param name="authkey">authkey</param>
        /// <param name="chlName">通道名称</param>
        /// <returns>IEnumerable<string></returns>
        public IEnumerable<string> ChlSearchKey(string authkey, string chlName)
        {
            //"chl:{0}:{1}"; //0 authkey, 1 通道名称
            return redisHelper.SetSearch(channelSetDbInx, string.Format(channelSetKeyName, authkey), string.Format(channelKeyPrefix, authkey, chlName))
                                .Select(t => string.IsNullOrEmpty(t) ? "" : t.Split(':')[2]);
        }
        #endregion

        #region 控制器
        /// <summary>
        /// 添加控制器
        /// </summary>
        /// <param name="authkey">authkey</param>
        /// <param name="chlName">通道名称</param>
        /// <param name="ctrlName">控制器名称</param>
        /// <param name="value">控制器属性(序列化json数据)</param>
        /// <param name="ttl">设置是否使用超时</param>
        /// <returns>bool</returns>
        public bool CtrlAdd(string authkey, string chlName, string ctrlName, string value, TimeSpan? ttl = null)
        {
            //if (CtrlSearchKey(authkey, chlName, ctrlName).Count() == 0)
            {
                redisHelper.SetAdd(channelSetDbInx, string.Format(controlSetKeyName, authkey, chlName), string.Format(controlKeyPrefix, authkey, chlName, ctrlName));
                return redisHelper.StringSet(controlDbInx, string.Format(controlKeyPrefix, authkey, chlName, ctrlName), value, ttl);
            }
            return false;//重名
        }
        /// <summary>
        /// 修改控制器
        /// </summary>
        /// <param name="authkey">authkey</param>
        /// <param name="chlName">通道名称</param>
        /// <param name="ctrlName">控制器名称</param>
        /// <param name="value">控制器属性(序列化json数据)</param>
        /// <param name="ttl">设置是否使用超时</param>
        /// <returns>bool</returns>
        public bool CtrlUpdate(string authkey, string chlName, string ctrlName, string value, TimeSpan? ttl = null)
        {
            //if (CtrlSearchKey(authkey, chlName, ctrlName).Count() > 0)
            {
                return redisHelper.StringSet(controlDbInx, string.Format(controlKeyPrefix, authkey, chlName, ctrlName), value, ttl);
            }
            return false;//键名不存在
        }

        /// <summary>
        /// 移除控制器
        /// </summary>
        /// <param name="authkey">authkey</param>
        /// <param name="chlName">通道名称</param>
        /// <param name="ctrlName">控制器名称</param>
        /// <returns>bool</returns>
        public bool CtrlRemove(string authkey, string chlName, string ctrlName)
        {
            try
            {
                redisHelper.DeleteKeys(variableDbInx, string.Format("var:{0}:{1}:{2}*", authkey, chlName, ctrlName));//变量
                foreach (var v in VarGetAll(authkey, chlName, ctrlName))
                {//删除所有变量索引
                    redisHelper.SetRemove(channelSetDbInx, string.Format(variableSetKeyName, authkey, chlName, ctrlName), string.Format(variableKeyPrefix, authkey, chlName, ctrlName, v));//变量列表
                }
                var val = redisHelper.StringRemove(controlDbInx, string.Format(controlKeyPrefix, authkey, chlName, ctrlName));//控制器
                val = redisHelper.SetRemove(channelSetDbInx, string.Format(controlSetKeyName, authkey, chlName), string.Format(controlKeyPrefix, authkey, chlName, ctrlName));//控制器列表
                if (val)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取控制器values
        /// </summary>
        /// <param name="keys">控制器keys</param>
        public IEnumerable<string> CtrlGetValues(string authkey, string chlName, IEnumerable<string> keys)
        {
            var ks = keys.Select(t => string.Format(controlKeyPrefix, authkey, chlName, t)).ToList();

            return redisHelper.StringGet(controlDbInx, ks);
        }
        /// <summary>
        /// 获取控制器value
        /// </summary>
        /// <param name="keys">控制器key</param>
        public string CtrlGetValue(string authkey, string chlName, string key)
        {
            var ks = string.Format(controlKeyPrefix, authkey, chlName, key);

            return redisHelper.StringGet(controlDbInx, ks);
        }
        /// <summary>
        /// 获取所有控制器
        /// </summary>
        /// <param name="authkey"></param>
        /// <param name="chlName"></param>
        public IEnumerable<string> CtrlGetAll(string authkey, string chlName)
        {
            // "ctrl:{0}:{1}:{2}"; //0 authkey, 1 通道名称(IO:Chl), 2 控制器名称
            return redisHelper.SetMembers(channelSetDbInx, string.Format(controlSetKeyName, authkey, chlName))
                .Select(t => string.IsNullOrEmpty(t) ? "" : (t.Split(':').Length == 4 ? t.Split(':')[3] : t.Split(':')[4]));
        }

        /// <summary>
        /// 控制器名称搜索 支持模糊匹配"* ?"
        /// </summary>
        /// <param name="authkey">authkey</param>
        /// <param name="chlName">通道名称</param>
        /// <param name="ctrlName">控制器名称</param>
        /// <returns>IEnumerable<string></returns>
        public IEnumerable<string> CtrlSearchKey(string authkey, string chlName, string ctrlName)
        {
            // "ctrl:{0}:{1}:{2}"; //0 authkey, 1 通道名称, 2 控制器名称
            return redisHelper.SetSearch(channelSetDbInx, string.Format(controlSetKeyName, authkey, chlName), string.Format(controlKeyPrefix, authkey, chlName, ctrlName))
                                .Select(t => string.IsNullOrEmpty(t) ? "" : t.Split(':')[3]); 
        }
        #endregion

        #region 变量
        public bool VarAdd(string authkey, string chlName, string ctrlName, string varName, string value, TimeSpan? ttl = null)
        {
            //if (VarSearchKey(authkey, chlName, ctrlName, varName).Count() == 0)
            {
                redisHelper.SetAdd(channelSetDbInx, string.Format(variableSetKeyName, authkey, chlName, ctrlName), string.Format(variableKeyPrefix, authkey, chlName, ctrlName, varName));
                return redisHelper.StringSet(variableDbInx, string.Format(variableKeyPrefix, authkey, chlName, ctrlName, varName), value, ttl);
            }
            return false;//重名
        }
        public bool VarUpdate(string authkey, string chlName, string ctrlName, string varName, string value, TimeSpan? ttl = null)
        {
            //if (VarSearchKey(authkey, chlName, ctrlName, varName).Count() > 0)
            {
                return redisHelper.StringSet(variableDbInx, string.Format(variableKeyPrefix, authkey, chlName, ctrlName, varName), value, ttl);
            }
            return false;//键名不存在
        }
        public bool VarRemove(string authkey, string chlName, string ctrlName, string varName)
        {
            try
            {
                var val = redisHelper.StringRemove(variableDbInx, string.Format(variableKeyPrefix, authkey, chlName, ctrlName, varName));//变量
                val = redisHelper.SetRemove(channelSetDbInx, string.Format(variableSetKeyName, authkey, chlName, ctrlName), string.Format(variableKeyPrefix, authkey, chlName, ctrlName, varName));//变量列表
                if (val)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取变量values
        /// </summary>
        /// <param name="keys">变量keys</param>
        public IEnumerable<string> VarGetValues(string authkey, string chlName, string ctrlName, IEnumerable<string> keys)
        {
            var ks = keys.Select(t => string.Format(variableKeyPrefix, authkey, chlName, ctrlName, t)).ToList();

            return redisHelper.StringGet(variableDbInx, ks);
        }
        /// <summary>
        /// 获取变量value
        /// </summary>
        /// <param name="keys">变量key</param>
        public string VarGetValue(string authkey, string chlName, string ctrlName, string key)
        {
            var ks = string.Format(variableKeyPrefix, authkey, chlName, ctrlName, key);

            return redisHelper.StringGet(variableDbInx, ks);
        }
        public IEnumerable<string> VarGetAll(string authkey, string chlName, string ctrlName)
        {
            // "var:{0}:{1}:{2}:{3}"; //0 authkey, 1 通道名称(IO:Chle), 2 控制器名称, 3 变量名称
            return redisHelper.SetMembers(channelSetDbInx, string.Format(variableSetKeyName, authkey, chlName, ctrlName))
                .Select(t => string.IsNullOrEmpty(t) ? "" : (t.Split(':').Length==5?t.Split(':')[4]: t.Split(':')[5]));
        }
        public IEnumerable<string> VarSearchKey(string authkey, string chlName, string ctrlName, string varName)
        {
            // "var:{0}:{1}:{2}:{3}"; //0 authkey, 1 通道名称, 2 控制器名称, 3 变量名称
            return redisHelper.SetSearch(channelSetDbInx, string.Format(variableSetKeyName, authkey, chlName, ctrlName), string.Format(variableKeyPrefix, authkey, chlName, ctrlName, varName))
                                .Select(t => string.IsNullOrEmpty(t) ? "" : t.Split(':')[4]);
        }
        #endregion

        #region pub/sub
        /// <summary>
        /// 获取一条订阅消息
        /// </summary>
        /// <param name="channel">channel</param>
        /// <returns>string</returns>
        public string PopSubMessage(string channel)
        {
            return redisHelper.GetSubActionValue(channel);
        }
        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="channel">channel</param>
        /// <param name="handler">handler</param>
        public void Subscribe(string channel, Action<string, string> handler=null)
        {
            redisHelper.Subscribe(channel, handler);
        }
        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="channel">channel</param>
        public void Unsubscribe(string channel)
        {
            redisHelper.Unsubscribe(channel);
        }
        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="channel">channel</param>
        /// <param name="message">消息内容</param>
        /// <returns>long</returns>
        public long Publish(string channel, string message)
        {
            return redisHelper.Publish(channel, message);
        }
        #endregion

        #region 报警
        /// <summary>
        /// 添加实时报警
        /// </summary>
        /// <param name="authkey">authkey</param>
        /// <param name="source">报警来源</param>
        /// <param name="level">报警级别</param>
        /// <param name="guid">报警GUID</param>
        /// <param name="value">实时报警(序列化json数据)</param>
        /// <param name="ttl">设置是否使用超时</param>
        /// <returns>bool</returns>
        public bool AmsAdd(string authkey, int source, int level, string guid, string value, TimeSpan? ttl = null)
        {
            //if (AmsSearchKey(authkey, source, level, guid).Count() == 0)
            {
                redisHelper.SetAdd(amsSetDbInx, string.Format(amsSetKeyName, authkey), string.Format(amsKeyPrefix, authkey, source, level, guid));
                return redisHelper.StringSet(amsDbInx, string.Format(amsKeyPrefix, authkey, source, level, guid), value, ttl);
            }
            return false;//重名
        }
        /// <summary>
        /// 修改实时报警
        /// </summary>
        /// <param name="authkey">authkey</param>
        /// <param name="source">报警来源</param>
        /// <param name="level">报警级别</param>
        /// <param name="guid">报警GUID</param>
        /// <param name="value">实时报警(序列化json数据)</param>
        /// <param name="ttl">设置是否使用超时</param>
        /// <returns>bool</returns>
        public bool AmsUpdate(string authkey, int source, int level, string guid, string value, TimeSpan? ttl = null)
        {
            //if (AmsSearchKey(authkey, source, level, guid).Count() > 0)
            {
                return redisHelper.StringSet(amsDbInx, string.Format(amsKeyPrefix, authkey, source, level, guid), value, ttl);
            }
            return false;//键名不存在
        }

        /// <summary>
        /// 删除实时报警
        /// </summary>
        /// <param name="authkey">authkey</param>
        /// <param name="source">报警来源</param>
        /// <param name="level">报警级别</param>
        /// <param name="guid">报警GUID</param>
        /// <returns></returns>
        public bool AmsRemove(string authkey, int source, int level, string guid)
        {
            try
            {
                var val = redisHelper.StringRemove(amsDbInx, string.Format(amsKeyPrefix, authkey, source, level, guid));
                val = redisHelper.SetRemove(amsSetDbInx, string.Format(amsSetKeyName, authkey), string.Format(amsKeyPrefix, authkey, source, level, guid));
                if (val)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// 删除实时报警
        /// </summary>
        /// <param name="authkey">authkey</param>
        /// <param name="guid">报警GUID</param>
        /// <returns></returns>
        public bool AmsRemove(string authkey, string guid)
        {
            try
            {
                redisHelper.DeleteKeys(amsDbInx, string.Format(amsKeyPrefix, authkey, "*", "*", guid));
                bool val = false;
                foreach (var k in AmsGetAll(authkey))
                {
                    if (k.Contains(guid))
                    {
                        val = redisHelper.SetRemove(amsSetDbInx, string.Format(amsSetKeyName, authkey), k);
                    }
                }

                if (val)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取实时库values
        /// </summary>
        /// <param name="keys">实时库 keys</param>
        /// <returns><![CDATA[IEnumerable<string>]]></returns>
        public IEnumerable<string> AmsGetValues(IEnumerable<string> keys)
        {
            return redisHelper.StringGet(amsDbInx, keys);
        }
        /// <summary>
        /// 获取实时库value
        /// </summary>
        /// <param name="keys">实时库key</param>
        public string AmsGetValue(string key)
        {
            return redisHelper.StringGet(amsDbInx, key);
        }
        /// <summary>
        /// 获取实时库value
        /// </summary>
        /// <param name="authkey">authkey</param>
        /// <param name="source">报警来源</param>
        /// <param name="level">报警级别</param>
        /// <param name="guid">报警GUID</param>
        public string AmsGetValue(string authkey, int source, int level, string guid)
        {
            string key = string.Format(amsKeyPrefix, authkey, source, level, guid);
            return redisHelper.StringGet(amsDbInx, key);
        }

        /// <summary>
        /// 获取所有实时库key
        /// </summary>
        /// <param name="authkey">authkey</param>
        /// <returns></returns>
        public IEnumerable<string> AmsGetAll(string authkey)
        {
            //"AMS:{0}:{1}:{2}:{3}"; //0 authkey, 1 报警来源, 2 报警级别， 3 报警GUID
            return redisHelper.SetMembers(amsSetDbInx, string.Format(amsSetKeyName, authkey));
        }

        /// <summary>
        /// key名称搜索 支持模糊匹配"* ?"
        /// </summary>
        /// <param name="authkey">authkey</param>
        /// <param name="source">报警来源</param>
        /// <param name="level">报警级别</param>
        /// <param name="guid">报警GUID</param>
        /// <returns>IEnumerable<string></returns>
        public IEnumerable<string> AmsSearchKey(string authkey, int source, int level, string guid)
        {
            //"AMS:{0}:{1}:{2}:{3}"; //0 authkey, 1 报警来源, 2 报警级别， 3 报警GUID
            return redisHelper.SetSearch(amsSetDbInx, string.Format(amsSetKeyName, authkey), string.Format(amsKeyPrefix, authkey, source, level, guid))
                                .Select(t => string.IsNullOrEmpty(t) ? "" : t.Split(':')[4]);//返加GUID
        }
        /// <summary>
        /// 获取所有用户的keys
        /// </summary>
        /// <returns>"AMS:authkey:报警来源:报警级别:报警GUID"</returns>
        public IEnumerable<string> AmsGetGlobalKeys()
        {
            List<string> keylist = new List<string>();
            foreach (string setkey in redisHelper.SetKeys(amsSetDbInx, "*"))//"AMSSET:{0}";//保存实时库key 0 authkey
            {
                keylist.AddRange(redisHelper.SetMembers(amsSetDbInx, setkey));//"AMS:{0}:{1}:{2}:{3}"; //0 authkey, 1 报警来源, 2 报警级别， 3 报警GUID
            }
            return keylist;
        }
        #endregion

    }

}