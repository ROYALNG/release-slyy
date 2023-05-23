using GHDatabase.Redis;
using GHDatabase.Redis.Models;
using GHIBMS.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace GHNETBASE.RTDB
{
    /// <summary>
    /// 设备管理 - 实时库
    /// </summary>
    public class DeviceManagement
    {
        #region Instance
        private static readonly DeviceManagement _instance = new DeviceManagement();
        RedisRtdbHelper redisHelper;
        int PrimaryPort = 6379, SlavePort = 6380;
        string PrimaryServer = "127.0.0.1";
        string Password = "";

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
                Password = ServerConfig.RedisPassword;
                if(ServerConfig.EnableRedis)
                   redisHelper = new RedisRtdbHelper(PrimaryServer, PrimaryPort, SlavePort,ServerConfig.RedisPassword);
            }
            catch
            {
                Logger.GetInstance().LogError("实时库地址配置不正确." + GHIBMS.Common.ServerConfig.CloudRtdbUrl);
            }
        }
        public static DeviceManagement SingletonInstance
        {
            get
            {
                return _instance;
            }
        }
        public  void  ReSet()
        {
            if (!ServerConfig.EnableRedis) return;
            if (redisHelper != null)
            {
                redisHelper.Close();
            }
            int.TryParse(GHIBMS.Common.ServerConfig.CloudRtdbUrl.Split(':')[1], out PrimaryPort);
            int.TryParse(GHIBMS.Common.ServerConfig.CloudRtdbUrl.Split(':')[1], out SlavePort);

            redisHelper = new RedisRtdbHelper(PrimaryServer, PrimaryPort, SlavePort,ServerConfig.RedisPassword);

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

        private int ioserverDbInx = 0; //IO服务器数据蔟
        private string ioserverKeyPrefix = "iosvr:{0}"; //0 IO服务器ID
        private int channelDbInx = 1; //通道数据蔟
        private string channelKeyPrefix = "chl:{0}:{1}"; //0 IO服务器ID, 1 通道名称
        private int controlDbInx = 2; //控制器数据蔟
        private string controlKeyPrefix = "ctrl:{0}:{1}:{2}"; //0 IO服务器ID, 1 通道名称, 2 控制器名称
        private int variableDbInx = 3; //变量数据蔟
        private string variableKeyPrefix = "var:{0}:{1}:{2}:{3}"; //0 IO服务器ID, 1 通道名称, 2 控制器名称, 3 变量名称

        private int setDbInx = 4;//键名集合数据蔟
        private string ioserverSetKeyName = "RTDBSET";//保存IO服务器key
        private string channelSetKeyName = "RTDBSET:{0}";//保存通道key 0 IO服务器ID
        private string controlSetKeyName = "RTDBSET:{0}:{1}";//保存控制器key 0 IO服务器ID, 1 通道名称
        private string variableSetKeyName = "RTDBSET:{0}:{1}:{2}";//保存变量key 0 IO服务器ID, 1 通道名称, 2 控制器名称

        //private string pubSubChannelName = "DeviceManagement";


        private int amsDbInx = 8; //实时报警
        private string amsKeyPrefix = "AMS:{0}:{1}:{2}:{3}"; //0 报警来源, 1 报警级别， 2 报警GUID,3 报警时间
        private string amsStatsKeyName = "STATS:{0}:{1}";//0 报警来源, 1 报警级别
        private string LastTimestamp = "STATS:LastTimestamp";//最后报警时间
        private string amsStatsPerHour = "STATS:{0}:{1}";//0 年月日时, 1 报警级别 绘制每小时报警统计曲线用
        private int amsSetDbInx = 9;//键名集合数据蔟
        private string amsSetKeyName = "AMSSET";//保存实时库key
        private string amsSet2KeyName = "AMSWAYSET";//按报警条件索引
        private string amsIconKEY= "ICON:{0}";//0 报警图标名称, 用于统计每种类型图标当前报警数量

        private int stateDbInx = 10;//设备在线状态监控

        private int ioserverDbInx_V2 = 11; //IO服务器数据蔟
        private int channelDbInx_V2 = 12; //通道数据蔟
        private int controlDbInx_V2 = 13; //控制器数据蔟
        private int variableDbInx_V2 = 14; //变量数据蔟
        private int keyDbInx = 5;//授权数据蔟

        #region IO服务器
        /// <summary>
        /// 添加IO服务器
        /// </summary>
        /// <param name="ioServerID">io服务器ID</param>
        /// <param name="value">IO服务器属性(序列化json数据)</param>
        /// <param name="ttl">设置是否使用超时</param>
        /// <returns>bool</returns>
        public bool IOServerAdd(string ioServerID, string value, TimeSpan? ttl = null)
        {
            if (redisHelper!=null)
            {
                redisHelper.SetAdd(setDbInx, ioserverSetKeyName, string.Format(ioserverKeyPrefix, ioServerID));
                return redisHelper.StringSet(ioserverDbInx, string.Format(ioserverKeyPrefix, ioServerID), value, ttl);
            }
            return false;//重名
        }
        /// <summary>
        /// 修改IO服务器
        /// </summary>
        /// <param name="ioServerID">IO服务器ID</param>
        /// <param name="value">IO服务器属性(序列化json数据)</param>
        /// <param name="ttl">设置是否使用超时</param>
        /// <returns>bool</returns>
        public bool IOServerUpdate(string ioServerID, string value, TimeSpan? ttl = null)
        {
            if (redisHelper != null)
            {
                return redisHelper.StringSet(ioserverDbInx, string.Format(ioserverKeyPrefix, ioServerID), value, ttl);
            }
            return false;//键名不存在
        }

        /// <summary>
        /// 更新服务器在线状态 dong 2017/10/30新增
        /// </summary>
        /// <param name="ioServerID">IO服务器ID</param>
        /// <param name="value">IO服务器属性(序列化json数据)</param>
        /// <param name="ttl">设置是否使用超时</param>
        /// <returns>bool</returns>
        public bool IOServerUpdateState(string ioServerID, string value, TimeSpan? ttl = null)
        {
            if (redisHelper != null)
            {
                return redisHelper.StringSet(stateDbInx, string.Format(ioserverKeyPrefix, ioServerID), value, ttl);
            }
            return false;//键名不存在
        }
        /// <summary>
        /// 删除IO服务器
        /// </summary>
        /// <param name="ioServerID">通道名称</param>
        /// <returns></returns>
        public bool IOServerRemove(string ioServerID)
        {
            try
            {
                if (redisHelper == null) return false;
                redisHelper.DeleteKeys(variableDbInx_V2, string.Format("var:{0}*", ioServerID));//变量
                redisHelper.DeleteKeys(controlDbInx_V2, string.Format("ctrl:{0}*", ioServerID));//控制器
                redisHelper.DeleteKeys(channelDbInx_V2, string.Format("chl:{0}*", ioServerID));//通道

                foreach (var chl in ChlGetAll(ioServerID))
                {
                    foreach (var ctl in CtrlGetAll(ioServerID, chl))
                    {
                        foreach (var v in VarGetAll(ioServerID, chl, ctl))
                        {//删除所有变量索引
                            redisHelper.SetRemove(setDbInx, string.Format(variableSetKeyName, ioServerID, chl, ctl), string.Format(variableKeyPrefix, ioServerID, chl, ctl, v));//变量列表
                        }
                        //删除所有控制器索引
                        redisHelper.SetRemove(setDbInx, string.Format(controlSetKeyName, ioServerID, chl), string.Format(controlKeyPrefix, ioServerID, chl, ctl));//控制器列表
                    }
                    //删除所有通道索引
                    redisHelper.SetRemove(setDbInx, string.Format(channelSetKeyName, ioServerID), string.Format(channelKeyPrefix, ioServerID, chl));//通道列表
                }

                var val = redisHelper.StringRemove(ioserverDbInx, string.Format(ioserverKeyPrefix, ioServerID));//IO服务器
                val = redisHelper.SetRemove(setDbInx, ioserverSetKeyName, string.Format(ioserverKeyPrefix, ioServerID));//IO服务器列表
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
        /// 获取IO服务器values
        /// </summary>
        /// <param name="keys">IO服务器ID</param>
        /// <returns><![CDATA[IEnumerable<string>]]></returns>
        public IEnumerable<string> IOServerGetValues(IEnumerable<string> keys)
        {
            if (redisHelper == null) return null;
            var ks = keys.Select(t => string.Format(ioserverKeyPrefix, t)).ToList();

            return redisHelper.StringGet(ioserverDbInx, ks);
        }
        /// <summary>
        /// 获取IO服务器value
        /// </summary>
        /// <param name="keys">IO服务器ID</param>
        /// <returns><![CDATA[IEnumerable<string>]]></returns>
        public string IOServerGetValue(string key)
        {
            if (redisHelper == null) return "";
            var ks = string.Format(ioserverKeyPrefix, key);

            return redisHelper.StringGet(ioserverDbInx, ks);
        }

        /// <summary>
        /// 获取IO服务器
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> IOServerGetAll()
        {
            if (redisHelper == null) return null;
            //"iosvr:{0}"; //0 IO服务器ID
            return redisHelper.SetMembers(setDbInx, ioserverSetKeyName)
                .Select(t => string.IsNullOrEmpty(t) ? "" :  t.Split(':')[1]);
        }

        /// <summary>
        /// IO服务器搜索 支持模糊匹配"* ?"
        /// </summary>
        /// <param name="ioServerID">io服务器ID</param>
        /// <returns>IEnumerable<string></returns>
        public IEnumerable<string> IOServerSearchKey(string ioServerID)
        {
            if (redisHelper == null) return null;
            return redisHelper.SetSearch(setDbInx, ioserverSetKeyName, string.Format(ioserverKeyPrefix, ioServerID))
                                .Select(t => string.IsNullOrEmpty(t) ? "" : t.Split(':')[1]);
        }
        #endregion

        #region 通道
        /// <summary>
        /// 添加通道
        /// </summary>
        /// <param name="ioServerID">ioServerID</param>
        /// <param name="chlName">通道名称</param>
        /// <param name="value">通道属性(序列化json数据)</param>
        /// <param name="ttl">设置是否使用超时</param>
        /// <returns>bool</returns>
        public bool ChlAdd(string ioServerID, string chlName, string value, TimeSpan? ttl=null)
        {
            if (redisHelper != null) 
            {
                redisHelper.SetAdd(setDbInx, string.Format(channelSetKeyName, ioServerID), string.Format(channelKeyPrefix, ioServerID, chlName));
                return redisHelper.StringSet(channelDbInx, string.Format(channelKeyPrefix, ioServerID, chlName), value, ttl);
            }
            return false;//重名
        }
        /// <summary>
        /// 修改通道
        /// </summary>
        /// <param name="ioServerID">ioServerID</param>
        /// <param name="chlName">通道名称</param>
        /// <param name="value">通道属性(序列化json数据)</param>
        /// <param name="ttl">设置是否使用超时</param>
        /// <returns>bool</returns>
        public bool ChlUpdate(string ioServerID, string chlName, string value, TimeSpan? ttl = null)
        {
            if (redisHelper != null)
            {
                return redisHelper.StringSet(channelDbInx, string.Format(channelKeyPrefix, ioServerID, chlName), value, ttl);
            }
            return false;//键名不存在
        }
        /// <summary>
        /// 修改通道状态 dong 2017/10/30修改
        /// </summary>
        /// <param name="ioServerID">ioServerID</param>
        /// <param name="chlName">通道名称</param>
        /// <param name="value">通道属性(序列化json数据)</param>
        /// <param name="ttl">设置是否使用超时</param>
        /// <returns>bool</returns>
        public bool ChlUpdateState(string ioServerID, string chlName, string value, TimeSpan? ttl = null)
        {
            if (redisHelper != null)
            {
                return redisHelper.StringSet(stateDbInx, string.Format(channelKeyPrefix, ioServerID, chlName), value, ttl);
            }
            return false;//键名不存在
        }
        /// <summary>
        /// 删除通道
        /// </summary>
        /// <param name="ioServerID">ioServerID</param>
        /// <param name="chlName">通道名称</param>
        /// <returns></returns>
        public bool ChlRemove(string ioServerID, string chlName)
        {
            try
            {
                if (redisHelper == null) return false;
                redisHelper.DeleteKeys(variableDbInx, string.Format("var:{0}:{1}*", ioServerID, chlName));//变量
                redisHelper.DeleteKeys(controlDbInx, string.Format("ctrl:{0}:{1}*", ioServerID, chlName));//控制器
                foreach (var ctl in CtrlGetAll(ioServerID, chlName))
                {
                    foreach (var v in VarGetAll(ioServerID, chlName, ctl))
                    {//删除所有变量索引
                        redisHelper.SetRemove(setDbInx, string.Format(variableSetKeyName, ioServerID, chlName, ctl), string.Format(variableKeyPrefix, ioServerID, chlName, ctl, v));//变量列表
                    }
                    //删除所有控制器索引
                    redisHelper.SetRemove(setDbInx, string.Format(controlSetKeyName, ioServerID, chlName), string.Format(controlKeyPrefix, ioServerID, chlName, ctl));//控制器列表
                }

                var val = redisHelper.StringRemove(channelDbInx, string.Format(channelKeyPrefix, ioServerID, chlName));//通道
                val = redisHelper.SetRemove(setDbInx, string.Format(channelSetKeyName, ioServerID), string.Format(channelKeyPrefix, ioServerID, chlName));//通道列表
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
        public IEnumerable<string> ChlGetValues(string ioServerID, IEnumerable<string> keys)
        {
            if (redisHelper == null) return null;
            var ks = keys.Select(t => string.Format(channelKeyPrefix, ioServerID, t)).ToList();

            return redisHelper.StringGet(channelDbInx, ks);
        }
        /// <summary>
        /// 获取通道value
        /// </summary>
        /// <param name="keys">通道key</param>
        /// <returns><![CDATA[IEnumerable<string>]]></returns>
        public string ChlGetValue(string ioServerID, string key)
        {
            if (redisHelper == null) return "";
            var ks = string.Format(channelKeyPrefix, ioServerID, key);

            return redisHelper.StringGet(channelDbInx, ks);
        }

        /// <summary>
        /// 获取所有通道名称
        /// </summary>
        /// <param name="ioServerID">ioServerID</param>
        /// <returns></returns>
        public IEnumerable<string> ChlGetAll(string ioServerID)
        {
            if (redisHelper == null) return null;
            return redisHelper.SetMembers(setDbInx, string.Format(channelSetKeyName, ioServerID))
                .Select(t => string.IsNullOrEmpty(t) ? "" : t.Split(':')[2]);
        }

        /// <summary>
        /// 通道名称搜索 支持模糊匹配"* ?" 
        /// </summary>
        /// <param name="ioServerID">ioServerID</param>
        /// <param name="chlName">通道名称</param>
        /// <returns>IEnumerable<string></returns>
        public IEnumerable<string> ChlSearchKey(string ioServerID, string chlName)
        {
            if (redisHelper == null) return null;
            return redisHelper.SetSearch(setDbInx, string.Format(channelSetKeyName, ioServerID), string.Format(channelKeyPrefix, ioServerID, chlName))
                                .Select(t => string.IsNullOrEmpty(t) ? "" : t.Split(':')[2]);
        }
        #endregion

        #region 控制器
        /// <summary>
        /// 添加控制器
        /// </summary>
        /// <param name="ioServerID">ioServerID</param>
        /// <param name="chlName">通道名称</param>
        /// <param name="ctrlName">控制器名称</param>
        /// <param name="value">控制器属性(序列化json数据)</param>
        /// <param name="ttl">设置是否使用超时</param>
        /// <returns>bool</returns>
        public bool CtrlAdd(string ioServerID, string chlName, string ctrlName, string value, TimeSpan? ttl = null)
        {
            if (redisHelper != null)
            {
                redisHelper.SetAdd(setDbInx, string.Format(controlSetKeyName, ioServerID, chlName), string.Format(controlKeyPrefix, ioServerID, chlName, ctrlName));
                return redisHelper.StringSet(controlDbInx, string.Format(controlKeyPrefix, ioServerID, chlName, ctrlName), value, ttl);
            }
            return false;//重名
        }
        /// <summary>
        /// 修改控制器
        /// </summary>
        /// <param name="ioServerID">ioServerID</param>
        /// <param name="chlName">通道名称</param>
        /// <param name="ctrlName">控制器名称</param>
        /// <param name="value">控制器属性(序列化json数据)</param>
        /// <param name="ttl">设置是否使用超时</param>
        /// <returns>bool</returns>
        public bool CtrlUpdate(string ioServerID, string chlName, string ctrlName, string value, TimeSpan? ttl = null)
        {
            if (redisHelper != null)
            {
                return redisHelper.StringSet(controlDbInx, string.Format(controlKeyPrefix, ioServerID, chlName, ctrlName), value, ttl);
            }
            return false;//键名不存在
        }
        /// <summary>
        /// 修改控制器状态 //dong 2017/10/30新增
        /// </summary>
        /// <param name="ioServerID">ioServerID</param>
        /// <param name="chlName">通道名称</param>
        /// <param name="ctrlName">控制器名称</param>
        /// <param name="value">控制器属性(序列化json数据)</param>
        /// <param name="ttl">设置是否使用超时</param>
        /// <returns>bool</returns>
        public bool CtrlUpdateState(string ioServerID, string chlName, string ctrlName, string value, TimeSpan? ttl = null)
        {
            if (redisHelper != null)
            {
                return redisHelper.StringSet(stateDbInx, string.Format(controlKeyPrefix, ioServerID, chlName, ctrlName), value, ttl);
            }
            return false;//键名不存在
        }
        /// <summary>
        /// 移除控制器
        /// </summary>
        /// <param name="ioServerID">ioServerID</param>
        /// <param name="chlName">通道名称</param>
        /// <param name="ctrlName">控制器名称</param>
        /// <returns>bool</returns>
        public bool CtrlRemove(string ioServerID, string chlName, string ctrlName)
        {
            try
            {
                if (redisHelper != null)
                {
                    redisHelper.DeleteKeys(variableDbInx, string.Format("var:{0}:{1}:{2}*", ioServerID, chlName, ctrlName));//变量
                    foreach (var v in VarGetAll(ioServerID, chlName, ctrlName))
                    {//删除所有变量索引
                        redisHelper.SetRemove(setDbInx, string.Format(variableSetKeyName, ioServerID, chlName, ctrlName), string.Format(variableKeyPrefix, ioServerID, chlName, ctrlName, v));//变量列表
                    }
                    var val = redisHelper.StringRemove(controlDbInx, string.Format(controlKeyPrefix, ioServerID, chlName, ctrlName));//控制器
                    val = redisHelper.SetRemove(setDbInx, string.Format(controlSetKeyName, ioServerID, chlName), string.Format(controlKeyPrefix, ioServerID, chlName, ctrlName));//控制器列表
                    if (val)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }
        /// <summary>
        /// 获取控制器values
        /// </summary>
        /// <param name="keys">控制器keys</param>
        public IEnumerable<string> CtrlGetValues(string ioServerID, string chlName, IEnumerable<string> keys)
        {
           if (redisHelper != null) return null;
           var ks = keys.Select(t => string.Format(controlKeyPrefix, ioServerID, chlName, t)).ToList();

            return redisHelper.StringGet(controlDbInx, ks);
        }
        /// <summary>
        /// 获取控制器value
        /// </summary>
        /// <param name="keys">控制器key</param>
        public string CtrlGetValue(string ioServerID, string chlName, string key)
        {
            if (redisHelper != null) return "";
            var ks = string.Format(controlKeyPrefix, ioServerID, chlName, key);

            return redisHelper.StringGet(controlDbInx, ks);
        }
        /// <summary>
        /// 获取所有控制器
        /// </summary>
        /// <param name="ioServerID"></param>
        /// <param name="chlName"></param>
        public IEnumerable<string> CtrlGetAll(string ioServerID, string chlName)
        {
            if (redisHelper != null) return null;
            return redisHelper.SetMembers(setDbInx, string.Format(controlSetKeyName, ioServerID, chlName))
                .Select(t => string.IsNullOrEmpty(t) ? "" :  t.Split(':')[3]);
        }

        /// <summary>
        /// 控制器名称搜索 支持模糊匹配"* ?"
        /// </summary>
        /// <param name="ioServerID">ioServerID</param>
        /// <param name="chlName">通道名称</param>
        /// <param name="ctrlName">控制器名称</param>
        /// <returns>IEnumerable<string></returns>
        public IEnumerable<string> CtrlSearchKey(string ioServerID, string chlName, string ctrlName)
        {
            if (redisHelper != null) return null;
            return redisHelper.SetSearch(setDbInx, string.Format(controlSetKeyName, ioServerID, chlName), string.Format(controlKeyPrefix, ioServerID, chlName, ctrlName))
                                .Select(t => string.IsNullOrEmpty(t) ? "" : t.Split(':')[3]); 
        }
        #endregion

        #region 变量
        public bool VarAdd(string ioServerID, string chlName, string ctrlName, string varName, string value, TimeSpan? ttl = null)
        {
            if (redisHelper != null)
            {
                redisHelper.SetAdd(setDbInx, string.Format(variableSetKeyName, ioServerID, chlName, ctrlName), string.Format(variableKeyPrefix, ioServerID, chlName, ctrlName, varName));
                return redisHelper.StringSet(variableDbInx, string.Format(variableKeyPrefix, ioServerID, chlName, ctrlName, varName), value, ttl);
            }
            return false;//重名
        }
        public bool VarUpdate(string ioServerID, string chlName, string ctrlName, string varName, string value, TimeSpan? ttl = null)
        {
            if (redisHelper != null)
            {
                return redisHelper.StringSet(variableDbInx, string.Format(variableKeyPrefix, ioServerID, chlName, ctrlName, varName), value, ttl);
            }
            return false;//键名不存在
        }
        public bool VarUpdate_V2(string ioServerID, string chlName, string ctrlName, string varName, string value, TimeSpan? ttl = null)
        {

            if (redisHelper != null)
            {
                return redisHelper.StringSet(variableDbInx, string.Format(variableKeyPrefix, ioServerID, chlName, ctrlName, varName), value, ttl);
            }
            return false;//键名不存在
        }
        public bool VarRemove(string ioServerID, string chlName, string ctrlName, string varName)
        {
            try
            {
                if (redisHelper != null)
                {
                    var val = redisHelper.StringRemove(variableDbInx, string.Format(variableKeyPrefix, ioServerID, chlName, ctrlName, varName));//变量
                    val = redisHelper.SetRemove(setDbInx, string.Format(variableSetKeyName, ioServerID, chlName, ctrlName), string.Format(variableKeyPrefix, ioServerID, chlName, ctrlName, varName));//变量列表
                    if (val)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }
       
        /// <summary>
        /// 获取变量values
        /// </summary>
        /// <param name="keys">变量keys</param>
        public IEnumerable<string> VarGetValues(string ioServerID, string chlName, string ctrlName, IEnumerable<string> keys)
        {
            if (redisHelper == null) return null;
            var ks = keys.Select(t => string.Format(variableKeyPrefix, ioServerID, chlName, ctrlName, t)).ToList();

            return redisHelper.StringGet(variableDbInx, ks);
        }
        /// <summary>
        /// 获取变量value
        /// </summary>
        /// <param name="keys">变量key</param>
        public string VarGetValue(string ioServerID, string chlName, string ctrlName, string key)
        {
            if (redisHelper == null) return "";
            var ks = string.Format(variableKeyPrefix, ioServerID, chlName, ctrlName, key);

            return redisHelper.StringGet(variableDbInx, ks);
        }
        public IEnumerable<string> VarGetAll(string ioServerID, string chlName, string ctrlName)
        {
            if (redisHelper == null) return null;
            return redisHelper.SetMembers(setDbInx, string.Format(variableSetKeyName, ioServerID, chlName, ctrlName))
                .Select(t => string.IsNullOrEmpty(t) ? "" : t.Split(':')[4]);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ioServerID"></param>
        /// <param name="chlName"></param>
        /// <param name="ctrlName"></param>
        /// <param name="varName"></param>
        /// <returns></returns>
        [Obsolete("SET Search方法很慢")]
        public IEnumerable<string> VarSearchKey(string ioServerID, string chlName, string ctrlName, string varName)
        {
            if (redisHelper == null) return null;
            return redisHelper.SetSearch(setDbInx, string.Format(variableSetKeyName, ioServerID, chlName, ctrlName), string.Format(variableKeyPrefix, ioServerID, chlName, ctrlName, varName))
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
            if (redisHelper != null)
                return redisHelper.GetSubActionValue(channel);
            else return "";
        }
        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="channel">channel</param>
        /// <param name="handler">handler</param>
        public void Subscribe(string channel, Action<string, string> handler=null)
        {
            if (redisHelper == null)
                redisHelper.Subscribe(channel, handler);
        }
        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="channel">channel</param>
        public void Unsubscribe(string channel)
        {
            if (redisHelper == null)
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
            if (redisHelper == null) return 0;
            return redisHelper.Publish(channel, message);
        }
        #endregion

        #region 报警
        public long AmsStatsIncrement(int source, int level, long value = 1)
        {
            if (redisHelper == null) return 0;
            redisHelper.StringSet(amsDbInx, LastTimestamp, DateTime.Now.Ticks.ToString());//最后报警时间
            redisHelper.StringIncrement(amsDbInx, string.Format(amsStatsPerHour, DateTime.Now.ToString("yyyyMMddHH"), level));//小时统计
            return redisHelper.StringIncrement(amsDbInx, string.Format(amsStatsKeyName,source,level), value); //分类分级统计
        }
        public string AmsStatsGet(int source, int level)
        {
            if (redisHelper == null) return "";
            return redisHelper.StringGet(amsDbInx, string.Format(amsStatsKeyName, source, level));
        }
        public long AmsIconIncrement(string icon, long value = 1)
        {
            if (redisHelper == null) return 0;
            return redisHelper.StringIncrement(amsDbInx, string.Format(amsIconKEY, icon), value); //分类分级统计
        }

        public long AmsIconDecrement(string icon, long value = 1)
        {
            if (redisHelper == null) return 0;

            return redisHelper.StringDecrement(amsDbInx, string.Format(amsIconKEY, icon), value); //分类分级统计
        }
        /// <summary>
        /// 添加实时报警
        /// </summary>
        /// <param name="source">报警来源</param>
        /// <param name="level">报警级别</param>
        /// <param name="guid">报警GUID</param>
        /// <param name="value">实时报警(序列化json数据)</param>
        /// <param name="ttl">设置是否使用超时</param>
        /// <returns>bool</returns>
        public bool AmsAdd(int source, int level, string guid,long alarmTime, string value, TimeSpan? ttl = null)
        {
            if (redisHelper != null)
            {
                AmsStatsIncrement(source, level);//计数
                redisHelper.SortedSetAdd(amsSetDbInx, amsSetKeyName, string.Format(amsKeyPrefix, source, level, guid, alarmTime), alarmTime);
                return redisHelper.StringSet(amsDbInx, string.Format(amsKeyPrefix, source, level, guid,alarmTime), value, ttl);
            }
            return false;//重名
        }
        public bool AmsAdd_V2(string io,string ch,string ctrl,string var,string way)
        {
            if (redisHelper != null)
                return redisHelper.SetAdd(amsSetDbInx, amsSet2KeyName, $"{io}:{ch}:{ctrl}:{var}:{way}");
            else
                return false;
        }
        public bool AmsExist_V2(string io, string ch, string ctrl, string var, string way)
        {
            if (redisHelper != null)
                return redisHelper.SetContains(amsSetDbInx, amsSet2KeyName, $"{io}:{ch}:{ctrl}:{var}:{way}");
            return false;
        }
        public bool AmsWayRemove_V2(string io, string ch, string ctrl, string var, string way)
        {
            if (redisHelper != null)
                return redisHelper.SetRemove (amsSetDbInx, amsSet2KeyName, $"{io}:{ch}:{ctrl}:{var}:{way}");
            return false;
        }
        public bool AmsIconAdd_V2(string icon)
        {
            if (redisHelper != null)
                return redisHelper.SetAdd(amsSetDbInx, amsSet2KeyName, icon);
            return false;
        }
        /// <summary>
        /// 修改实时报警
        /// </summary>
        /// <param name="source">报警来源</param>
        /// <param name="level">报警级别</param>
        /// <param name="guid">报警GUID</param>
        /// <param name="value">实时报警(序列化json数据)</param>
        /// <param name="ttl">设置是否使用超时</param>
        /// <returns>bool</returns>
        public bool AmsUpdate(int source, int level, string guid, long alarmTime,string value, TimeSpan? ttl = null)
        {
            if (redisHelper != null)
            {
                redisHelper.SortedSetAdd(amsSetDbInx, amsSetKeyName, string.Format(amsKeyPrefix, source, level, guid, alarmTime), alarmTime);
                return redisHelper.StringSet(amsDbInx, string.Format(amsKeyPrefix, source, level, guid,alarmTime), value, ttl);
            }
            return false;//键名不存在
        }

        /// <summary>
        /// 删除实时报警
        /// </summary>
        /// <param name="source">报警来源</param>
        /// <param name="level">报警级别</param>
        /// <param name="guid">报警GUID</param>
        /// <returns></returns>
        public bool AmsRemove(int source, int level, string guid,long alarmTime)
        {
            try
            {
                if (redisHelper != null)
                { 
                    var val = redisHelper.StringRemove(amsDbInx, string.Format(amsKeyPrefix, source, level, guid, alarmTime));
                    val = redisHelper.SortedSetRemove(amsSetDbInx, amsSetKeyName, string.Format(amsKeyPrefix, source, level, guid, alarmTime));
                    if (val)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }
        /// <summary>
        /// 删除实时报警
        /// </summary>
        /// <param name="guid">报警GUID</param>
        /// <returns></returns>
        public bool AmsRemove(string guid)
        {
            try
            {
                if (redisHelper == null) return false;
                redisHelper.DeleteKeys(amsDbInx, string.Format(amsKeyPrefix, "*", "*", guid,"*"));
                var keys = redisHelper.SortedSetSearch(amsSetDbInx, amsSetKeyName, string.Format(amsKeyPrefix, "*", "*", guid, "*"));
                bool val = false;
                foreach (var k in keys)
                {
                    val = redisHelper.SortedSetRemove(amsSetDbInx, amsSetKeyName, k);
                }

                //bool val = false;
                //foreach (var k in AmsGetAll())
                //{
                //    if (k.Contains(guid))
                //    {
                //        val = redisHelper.SetRemove(amsSetDbInx, amsSetKeyName, k);
                //    }
                //}

                if (val)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 获取实时库values
        /// </summary>
        /// <param name="keys">实时库 keys</param>
        /// <returns><![CDATA[IEnumerable<string>]]></returns>
        public IEnumerable<string> AmsGetValues(IEnumerable<string> keys)
        {
            if (redisHelper == null) return null;
            return redisHelper.StringGet(amsDbInx, keys);
        }
        /// <summary>
        /// 获取实时库value
        /// </summary>
        /// <param name="keys">实时库key</param>
        public string AmsGetValue(string key)
        {
            if (redisHelper == null) return "";
            return redisHelper.StringGet(amsDbInx, key);
        }
        /// <summary>
        /// 获取实时库value
        /// </summary>
        /// <param name="source">报警来源</param>
        /// <param name="level">报警级别</param>
        /// <param name="guid">报警GUID</param>
        public string AmsGetValue(int source, int level, string guid,long alarmTime)
        {
            if (redisHelper == null) return "";
            string key = string.Format(amsKeyPrefix, source, level, guid,alarmTime);
            return redisHelper.StringGet(amsDbInx, key);
        }

        /// <summary>
        /// 获取所有实时库key
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> AmsGetAll()
        {
            if (redisHelper == null) return null;
            return redisHelper.SortedSetMembers(amsSetDbInx, amsSetKeyName);
        }

        /// <summary>
        /// key名称搜索 支持模糊匹配"* ?"
        /// </summary>
        /// <param name="authkey">authkey</param>
        /// <param name="source">报警来源</param>
        /// <param name="level">报警级别</param>
        /// <param name="guid">报警GUID</param>
        /// <returns>IEnumerable<string></returns>
        public IEnumerable<string> AmsSearchKey(int source, int level, string guid,long alarmTime)
        {
            if (redisHelper == null) return null;
            //"AMS:{0}:{1}:{2}"; //0 报警来源, 1 报警级别， 2 报警GUID
            return redisHelper.SortedSetSearch(amsSetDbInx, amsSetKeyName, string.Format(amsKeyPrefix, source, level, guid,alarmTime))
                                .Select(t => string.IsNullOrEmpty(t) ? "" : t.Split(':')[3]);//返加GUID
        }
        /// <summary>
        /// 获取所有用户的keys
        /// </summary>
        /// <returns>"AMS:authkey:报警来源:报警级别:报警GUID"</returns>
        public IEnumerable<string> AmsGetGlobalKeys()
        {
            if (redisHelper == null) return null;
            //List<string> keylist = new List<string>();
            //foreach (string setkey in redisHelper.SetKeys(amsSetDbInx, "*"))//"AMSSET";//保存实时库key
            //{
            //    keylist.AddRange(redisHelper.SetMembers(amsSetDbInx, setkey));//"AMS:{0}:{1}:{2}"; //0 报警来源, 1 报警级别， 2 报警GUID
            //}
            //return keylist;
            return AmsGetAll();
        }
        #endregion

        #region V2.0新版数据结构 2018-3-1新增
 
        #region IO服务器
        /// <summary>
        /// 添加IO服务器
        /// </summary>
        /// <param name="ioServerID">io服务器ID</param>
        /// <param name="value">IO服务器属性(序列化json数据)</param>
        /// <param name="ttl">设置是否使用超时</param>
        /// <returns>bool</returns>
        public bool IOServerAdd_V2(string ioServerID, Dictionary<string,string>values, TimeSpan? ttl = null)
        {
            try
            {
                if (redisHelper == null) return false;
                redisHelper.SetAdd(setDbInx, ioserverSetKeyName, string.Format(ioserverKeyPrefix, ioServerID));
                redisHelper.HashSetObject(ioserverDbInx_V2, string.Format(ioserverKeyPrefix, ioServerID), values);
                //return redisHelper.StringSet(ioserverDbInx, string.Format(ioserverKeyPrefix, ioServerID), value, ttl);
                return true;//重名
            }
            catch(Exception ex)
            {
                return false;//重名
            }
        }

        /// <summary>
        /// 获取IO的所有属性
        /// </summary>
        /// <param name="keys">通道key</param>
        /// <returns><![CDATA[IEnumerable<string>]]></returns>
        public Dictionary<string,string> IOServerGetPropertyAll_V2( string key)
        {
            //var ks = string.Format(channelKeyPrefix, ioServerID, key);
            if (redisHelper == null) return null;
            return redisHelper.HashGetObject(ioserverDbInx_V2, key);
        }
        #endregion
        #region 通道
        /// <summary>
        /// 添加通道
        /// </summary>
        /// <param name="ioServerID">ioServerID</param>
        /// <param name="chlName">通道名称</param>
        /// <param name="value">通道属性(序列化json数据)</param>
        /// <param name="ttl">设置是否使用超时</param>
        /// <returns>bool</returns>
        public bool ChlAdd_V2(string ioServerID, string chlID, Dictionary<string, string> values, TimeSpan? ttl = null)
        {
            try
            {
                if (redisHelper == null) return false;
                redisHelper.SetAdd(setDbInx, string.Format(channelSetKeyName, ioServerID), string.Format(channelKeyPrefix, ioServerID, chlID));
                redisHelper.HashSetObject(channelDbInx_V2, string.Format(channelKeyPrefix, ioServerID, chlID), values);
                //return redisHelper.StringSet(channelDbInx_V2, string.Format(channelKeyPrefix, ioServerID, chlName), value, ttl);
                return true;
            }
            catch
            {
                return false;//重名
            }
        }
        /// <summary>
        /// 获取通道的所有属性
        /// </summary>
        /// <param name="keys">通道key</param>
        /// <returns><![CDATA[IEnumerable<string>]]></returns>
        public Dictionary<string, string> ChlGetPropertyAll_V2(string ioServerID, string chrlID)
        {
            if (redisHelper == null) return null;
            var key = string.Format(channelKeyPrefix, ioServerID, chrlID);

            return redisHelper.HashGetObject(channelDbInx_V2, key);
        }
        #endregion

        #region 控制器
        /// <summary>
        /// 添加控制器
        /// </summary>
        /// <param name="ioServerID">ioServerID</param>
        /// <param name="chlName">通道名称</param>
        /// <param name="ctrlName">控制器名称</param>
        /// <param name="value">控制器属性(序列化json数据)</param>
        /// <param name="ttl">设置是否使用超时</param>
        /// <returns>bool</returns>
        public bool CtrlAdd_V2(string ioServerID, string chlID, string ctrlID, Dictionary<string, string> values, TimeSpan? ttl = null)
        {
            try
            {
                if (redisHelper == null) return false;
                redisHelper.SetAdd(setDbInx, string.Format(controlSetKeyName, ioServerID, chlID), string.Format(controlKeyPrefix, ioServerID, chlID, ctrlID));
                redisHelper.HashSetObject(controlDbInx_V2, string.Format(controlKeyPrefix, ioServerID, chlID, ctrlID), values);
                return true;
            }
            catch
            {
                return false;//重名
            }
        }
        /// <summary>
        /// 获取控制器的所有属性
        /// </summary>
        /// <param name="keys">通道key</param>
        /// <returns><![CDATA[IEnumerable<string>]]></returns>
        public Dictionary<string, string> CtrlGetPropertyAll_V2(string ioServerID,string chlID,string ctrlID)
        {
            if (redisHelper == null) return null;
            var key = string.Format(controlKeyPrefix, ioServerID,chlID, ctrlID);

            return redisHelper.HashGetObject(controlDbInx_V2, key);
        }
        #endregion

        #region 变量
        public bool VarAdd_V2(string ioServerID, string chlID, string ctrlID, string varID, Dictionary<string, string> values, TimeSpan? ttl = null)
        {
            try
            {
                if (redisHelper == null) return false;
                redisHelper.SetAdd(setDbInx, string.Format(variableSetKeyName, ioServerID, chlID, ctrlID), string.Format(variableKeyPrefix, ioServerID, chlID, ctrlID, varID));
                redisHelper.HashSetObject(variableDbInx_V2, string.Format(variableKeyPrefix, ioServerID, chlID, ctrlID, varID), values);
                return true;
            }
            catch
            {
                return false;//重名
            }
        }
        /// <summary>
        /// 获取变量的所有属性
        /// </summary>
        /// <param name="keys">通道key</param>
        /// <returns><![CDATA[IEnumerable<string>]]></returns>
        public Dictionary<string, string> VarGetPropertyAll_V2(string ioServerID,string chlID,string ctrlID,string varID)
        {
            if (redisHelper == null) return null;
            var key = string.Format(variableKeyPrefix, ioServerID,chlID ,ctrlID, varID);

            return redisHelper.HashGetObject(variableDbInx_V2, key);
        }
        public bool VarUpdate_V2(string ioServerID, string chlID, string ctrlID, string varID, Dictionary<string, string> values, TimeSpan? ttl = null)
        {
            try
            {
                if (redisHelper == null) return false;
                //redisHelper.SetAdd(setDbInx, string.Format(variableSetKeyName, ioServerID, chlID, ctrlID), string.Format(variableKeyPrefix, ioServerID, chlID, ctrlID, varID));
                redisHelper.HashSetObject(variableDbInx_V2, string.Format(variableKeyPrefix, ioServerID, chlID, ctrlID, varID), values);
                return true;
            }
            catch(Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
                return false;//重名
            }
        }
        #endregion
        /// <summary>
        /// 是否存在授权 0：有授权 -1：无授权
        /// </summary>
        /// <param name="value"></param>
        /// <param name="ttl"></param>
        /// <returns></returns>
        public bool KeyAdd(string value, TimeSpan? ttl = null)
        {
            if (redisHelper == null) return false;
            return redisHelper.StringSet(keyDbInx, "KO", value, ttl);
        }

        #endregion

    }

}