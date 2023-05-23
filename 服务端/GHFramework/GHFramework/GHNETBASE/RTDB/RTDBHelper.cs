using GHCore.Serialization;
using GHIBMS.Common;
using GHIBMS.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GHNETBASE.RTDB
{
    public class RTDBHelper
    {
        #region Instance
        private static readonly RTDBHelper _instance = new RTDBHelper();
        private RTDBHelper()
        {
        }
        public static RTDBHelper SingletonInstance
        {
            get
            {
                return _instance;
            }
        }
        #endregion

      
      
        /// <summary>
        /// 增加IO服务器
        /// </summary>
        /// <param name="ioServerID"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool AddIOServer(string ioServerID, string value)
        {

            //GHRestClient.GHRestClient clent = new GHRestClient.GHRestClient(rtdbBaseUrl, OAUTH.OAuthHelper.SingletonInstance.UUID, "");
            //var devinfo = clent.GetClientInfo<DeviceInfo>(string.Format("/RTDB/{0}", checkUrl(ioServerID)));
            //GHRTDBObjectRequest input = new GHRTDBObjectRequest { input = value };

            //var ret = devinfo.AddChannel(input);

            //if (ret.success)
            //    return true;
            //else
            //{
            //    System.Diagnostics.Debug.WriteLine(ret.Message);

            //    return false;
            //}
            try
            {
                return RTDBController.SingletonInstance.AddIOServer(pubFun.checkUrl(ioServerID), value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                Logger.GetInstance().LogError(ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// 增加IO服务器
        /// </summary>
        public bool AddIOServer(string ioServerID, object value)
        {

            try
            {
                string jsonValue = JSONFormatter.Serialize(value);
                return AddIOServer(ioServerID, jsonValue);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                Logger.GetInstance().LogError(ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// 更新IOServer的在线状态
        /// </summary>
        /// <param name="ioServerID"></param>
        /// <param name="ttl"></param>
        /// <returns></returns>
        public bool UpdateIOServerState(string ioServerID, string value, TimeSpan ttl)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ioServerID))
                {
                    Logger.GetInstance().LogError("ioServerID不存在");
                    return false;
                }
                else
                {
                    //var iosvrvalue = DeviceManagement.SingletonInstance.IOServerUpdate(ioServerID, value);
                    //if (!iosvrvalue)
                    var iosvrvalue = DeviceManagement.SingletonInstance.IOServerUpdateState(ioServerID,value,ttl);

                    return iosvrvalue;
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
                return false;
            }

           
        }

        /// <summary>
        /// 增加通道
        /// </summary>
        /// <param name="ioServerID">ioServerID</param>
        /// <param name="chl">通道名称</param>
        /// <param name="value">写入的值</param>
        /// <returns></returns>
        //public bool AddChannel(string rtdbBaseUrl, string chl, string value)
        public bool AddChannel(string ioServerID, string chl, string value)
        {

            //GHRestClient.GHRestClient clent = new GHRestClient.GHRestClient(rtdbBaseUrl, OAUTH.OAuthHelper.SingletonInstance.UUID, "");
            //var devinfo = clent.GetClientInfo<DeviceInfo>(string.Format("/RTDB/{0}/{1}", checkUrl(ioServerID), checkUrl(chl)));
            //GHRTDBObjectRequest input = new GHRTDBObjectRequest { input = value };

            //var ret = devinfo.AddChannel(input);

            //if (ret.success)
            //    return true;
            //else
            //{
            //    System.Diagnostics.Debug.WriteLine(ret.Message);

            //    return false;
            //}
            try
            {
                return RTDBController.SingletonInstance.AddChannel(pubFun.checkUrl(ioServerID), pubFun.checkUrl(chl), value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                Logger.GetInstance().LogError(ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// 增加通道
        /// </summary>
        /// <param name="ioServerID">ioServerID</param>
        /// <param name="chl">通道名称</param>
        /// <param name="value">写入的值</param>
        /// <returns></returns>
        //public bool AddChannel(string rtdbBaseUrl, string chl, object value)
        public bool AddChannel(string ioServerID, string chl, object value)
        {

            try
            {
                string jsonValue = JSONFormatter.Serialize(value);
                return AddChannel(ioServerID, chl, jsonValue);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                Logger.GetInstance().LogError(ex.ToString());
                return false;
            }
        }
       /// <summary>
       /// 更新通道状态
       /// </summary>
       /// <param name="ioServerID"></param>
       /// <param name="chlName"></param>
       /// <param name="value"></param>
       /// <param name="ttl"></param>
       /// <returns></returns>
        public bool UpdateChannelState(string ioServerID, string chlName, string value, TimeSpan? ttl = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ioServerID))
                {
                    Logger.GetInstance().LogError("ioServerID不存在");
                    return false;
                }
                else
                {
                    //var iosvrvalue = DeviceManagement.SingletonInstance.IOServerUpdate(ioServerID, value);
                    //if (!iosvrvalue)
                    return DeviceManagement.SingletonInstance.ChlUpdateState(ioServerID, chlName, value, ttl);

                     
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
                return false;
            }


        }

        /// <summary>
        /// 增加控制器
        /// </summary>
        /// <param name="ioServerID">ioServerID</param>
        /// <param name="chl">通道名称</param>
        /// <param name="ctrl">控制器名称</param>
        /// <param name="value">写入的值</param>
        /// <returns></returns>
        //public bool AddController(string rtdbBaseUrl, string chl, string ctrl, string value)
        public bool AddController(string ioServerID, string chl, string ctrl, string value)
        {

            //GHRestClient.GHRestClient clent = new GHRestClient.GHRestClient(rtdbBaseUrl, OAUTH.OAuthHelper.SingletonInstance.UUID, "");
            //var devinfo = clent.GetClientInfo<DeviceInfo>(string.Format("/RTDB/{0}/{1}", checkUrl(chl), checkUrl(ctrl)));
            //GHRTDBObjectRequest input = new GHRTDBObjectRequest { input = value };

            //var ret = devinfo.AddController(input);

            //if (ret.success)
            //    return true;
            //else
            //{
            //    System.Diagnostics.Debug.WriteLine(ret.Message);

            //    return false;
            //}
            try
            {
                return RTDBController.SingletonInstance.AddController(pubFun.checkUrl(ioServerID), pubFun.checkUrl(chl), pubFun.checkUrl(ctrl), value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                Logger.GetInstance().LogError(ex.ToString());
                return false;
            }
        }
        //public bool AddController(string rtdbBaseUrl, string chl, string ctrl, object value)
        public bool AddController(string ioServerID, string chl, string ctrl, object value)
        {

            try
            {
                string jsonValue = JSONFormatter.Serialize(value);
                return AddController(ioServerID, chl, ctrl, jsonValue);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                Logger.GetInstance().LogError(ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// 更新控制器状态
        /// </summary>
        /// <param name="ioServerID"></param>
        /// <param name="chlName"></param>
        /// <param name="value"></param>
        /// <param name="ttl"></param>
        /// <returns></returns>
        public bool UpdateCtrlState(string ioServerID, string chlName, string ctrlName, string value, TimeSpan? ttl = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ioServerID))
                {
                    Logger.GetInstance().LogError("ioServerID不存在");
                    return false;
                }
                else
                {
                    //var iosvrvalue = DeviceManagement.SingletonInstance.IOServerUpdate(ioServerID, value);
                    //if (!iosvrvalue)
                    return DeviceManagement.SingletonInstance.CtrlUpdateState(ioServerID, chlName,ctrlName, value, ttl);

                   
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
                return false;
            }


        }
        /// <summary>
        /// 增加变量
        /// </summary>
        /// <param name="ioServerID">ioServerID</param>
        /// <param name="chl">通道名称</param>
        /// <param name="ctrl">控制器名称</param>
        /// <param name="var">变量名称</param>
        /// <param name="value">写入的值</param>
        /// <returns></returns>
        //public bool AddVariable(string rtdbBaseUrl, string chl, string ctrl, string var, string value)
        public bool AddVariable(string ioServerID, string chl, string ctrl, string var, string value)
        {

            //GHRestClient.GHRestClient clent = new GHRestClient.GHRestClient(rtdbBaseUrl, OAUTH.OAuthHelper.SingletonInstance.UUID, "");
            //var devinfo = clent.GetClientInfo<DeviceInfo>(string.Format("/RTDB/{0}/{1}/{2}", checkUrl(chl), checkUrl(ctrl), checkUrl(var)));
            //GHRTDBObjectRequest input = new GHRTDBObjectRequest { input = value };

            //var ret = devinfo.AddVariable(input);

            //if (ret.success)
            //    return true;
            //else
            //{
            //    System.Diagnostics.Debug.WriteLine(ret.Message);

            //    return false;
            //}
            try
            {
                return RTDBController.SingletonInstance.AddVariable(pubFun.checkUrl(ioServerID), pubFun.checkUrl(chl), pubFun.checkUrl(ctrl), pubFun.checkUrl(var), value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                Logger.GetInstance().LogError(ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// 增加变量
        /// </summary>
        /// <param name="ioServerID">ioServerID</param>
        /// <param name="chl">通道名称</param>
        /// <param name="ctrl">控制器名称</param>
        /// <param name="var">变量名称</param>
        /// <param name="value">写入的值</param>
        /// <returns></returns>
        //public bool AddVariable(string rtdbBaseUrl, string chl, string ctrl, string var, object value)
        public bool AddVariable(string ioServerID, string chl, string ctrl, string var, object value)
        {
            try
            {
                string jsonValue = JSONFormatter.Serialize(value);
                return AddVariable(ioServerID , chl, ctrl, var, jsonValue);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message + ex.StackTrace);
                Logger.GetInstance().LogError(ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// 增加多个变量
        /// </summary>
        /// <param name="rtdbBaseUrl">实时库基地址</param>
        /// <param name="chl">通道名称</param>
        /// <param name="ctrl">控制器名称</param>
        /// <param name="var">变量名称</param>
        /// <param name="value">写入的值</param>
        /// <returns></returns>
        //public bool AddVariableMult(string rtdbBaseUrl, string chl, string ctrl, string var, List<GHNETBASE.RTDB.Models.DevVariable> value)
        public bool AddVariableMult(List<GHNETBASE.RTDB.Models.DevVariable> value)
        {
           
            try
            {
                //GHRestClient.GHRestClient clent = new GHRestClient.GHRestClient(rtdbBaseUrl, OAUTH.OAuthHelper.SingletonInstance.UUID, "");
                //var devinfo = clent.GetClientInfo<DeviceInfo>(string.Format("/RTDB/mw/{0}/{1}/{2}", checkUrl(chl), checkUrl(ctrl), checkUrl(var)));

                //var jsonstr = GHCore.Serialization.JSONFormatter.Serialize(value);
                //GHRTDBObjectRequest input = new GHRTDBObjectRequest { input = jsonstr };

                //var ret = devinfo.AddVariable(input);

                //if (ret.success)
                //    return true;
                //else
                //{
                //    System.Diagnostics.Debug.WriteLine(ret.Message);

                //    return false;
                //}
                return RTDBController.SingletonInstance.MutlWriteVariable(value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                Logger.GetInstance().LogError(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 读取单个变量值
        /// </summary>
        /// <param name="ioServerID">ioServerID</param>
        /// <param name="chl">通道名称</param>
        /// <param name="ctrl">控制器名称</param>
        /// <param name="var">变量名称</param>
        /// <returns>String</returns>
        //public string ReadVariableValue(string rtdbBaseUrl, string chl, string ctrl, string var)
        public string ReadVariableValue(string ioServerID, string chl, string ctrl, string var)
        {
           

            //GHRestClient.GHRestClient clent = new GHRestClient.GHRestClient(rtdbBaseUrl, OAUTH.OAuthHelper.SingletonInstance.UUID, "");
            //var devinfo = clent.GetClientInfo<DeviceInfo>(string.Format("/RTDB/{0}/{1}/{2}", checkUrl(chl), checkUrl(ctrl), checkUrl(var)));
            //var ret = devinfo.ReadVariable();

            //if (ret.success)
            //    return ret.data.ToString();
            //else
            //{
            //    System.Diagnostics.Debug.WriteLine(ret.Message);

            //    return "";
            //}
            try
            {
                return RTDBController.SingletonInstance.GetVariable(pubFun.checkUrl(ioServerID), pubFun.checkUrl(chl), pubFun.checkUrl(ctrl), pubFun.checkUrl(var));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                Logger.GetInstance().LogError(ex.ToString());

                return "";
            }
        }

        /// <summary>
        /// 读取单个变量值
        /// </summary>
        /// <param name="ioServerID">ioServerID</param>
        /// <param name="chl">通道名称</param>
        /// <param name="ctrl">控制器名称</param>
        /// <param name="var">变量名称</param>
        /// <returns>typeof T object</returns>
        //public T ReadVariableValue<T>(string rtdbBaseUrl, string chl, string ctrl, string var)
        public T ReadVariableValue<T>(string ioServerID, string chl, string ctrl, string var)
        {
           

            try
            {
                string json = ReadVariableValue(ioServerID, chl, ctrl, var);
                if (string.IsNullOrEmpty(json))
                    return default(T);
                return JSONFormatter.Deserialize<T>(json);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                Logger.GetInstance().LogError(ex.ToString());
            }

            return default(T);
        }
        /// <summary>
        /// 读取多个变量值
        /// </summary>
        /// <param name="rtdbBaseUrl">实时库基地址</param>
        /// <param name="chl">通道名称</param>
        /// <param name="ctrl">控制器名称</param>
        /// <param name="var">变量名称</param>
        /// <returns>typeof T object</returns>
        //public List<GHNETBASE.RTDB.Models.DevVariable> ReadVariableMultValue(string rtdbBaseUrl, string chl, string ctrl, List<GHNETBASE.RTDB.Models.DevVariable> var)
        public List<GHNETBASE.RTDB.Models.DevVariable> ReadVariableMultValue(List<GHNETBASE.RTDB.Models.DevVariable> var)
        {
          

            //GHRestClient.GHRestClient clent = new GHRestClient.GHRestClient(rtdbBaseUrl, OAUTH.OAuthHelper.SingletonInstance.UUID, "");
            //var devinfo = clent.GetClientInfo<DeviceInfo>(string.Format("/RTDB/mr/{0}/{1}/var", checkUrl(chl), checkUrl(ctrl)));
            //var jsonstr = GHCore.Serialization.JSONFormatter.Serialize(var);
            //GHRTDBObjectRequest input = new GHRTDBObjectRequest { input = jsonstr };

            //var ret = devinfo.ReadVariableMult(input);

            //if (ret.success)
            //{
            //    try
            //    {
            //       return GHCore.Serialization.JSONFormatter.Deserialize<List<Models.DevVariable>>(ret.data.ToString());
            //    }
            //    catch (Exception ex)
            //    {
            //        return new List<Models.DevVariable>();
            //    }
            //}
            //else
            //{
            //    System.Diagnostics.Debug.WriteLine(ret.Message);

            //    return new List<Models.DevVariable>();
            //}
            try
            {
                return RTDBController.SingletonInstance.GetMultVariables(var);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                Logger.GetInstance().LogError(ex.ToString());

                return new List<Models.DevVariable>();
            }
        }

        /// <summary>
        /// 写入变量值
        /// </summary>
        /// <param name="ioServerID">ioServerID</param>
        /// <param name="chl">通道名称</param>
        /// <param name="ctrl">控制器名称</param>
        /// <param name="var">变量名称</param>
        /// <param name="value">写入的值</param>
        /// <returns></returns>
        //public bool WriteVariableValue(string rtdbBaseUrl, string chl, string ctrl, string var, string value)
        public bool WriteVariableValue(string ioServerID, string chl, string ctrl, string var, string value)
        {
          

            //GHRestClient.GHRestClient clent = new GHRestClient.GHRestClient(rtdbBaseUrl, OAUTH.OAuthHelper.SingletonInstance.UUID, "");
            //var devinfo = clent.GetClientInfo<DeviceInfo>(string.Format("/RTDB/{0}/{1}/{2}", checkUrl(chl), checkUrl(ctrl), checkUrl(var)));
            //GHRTDBObjectRequest input = new GHRTDBObjectRequest { input = value };

            //var ret = devinfo.WriteVariable(input);

            //if (ret.success)
            //    return true;
            //else
            //{
            //    System.Diagnostics.Debug.WriteLine(ret.Message);

            //    return false;
            //}
            try
            {
                return RTDBController.SingletonInstance.AddVariable(pubFun.checkUrl(ioServerID), pubFun.checkUrl(chl), pubFun.checkUrl(ctrl), pubFun.checkUrl(var), value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                Logger.GetInstance().LogError(ex.ToString());

                return false;
            }
        }
        /// <summary>
        /// 写入变量值
        /// </summary>
        /// <param name="ioServerID">ioServerID</param>
        /// <param name="chl">通道名称</param>
        /// <param name="ctrl">控制器名称</param>
        /// <param name="var">变量名称</param>
        /// <param name="value">写入对象</param>
        /// <returns></returns>
        //public bool WriteVariableValue(string rtdbBaseUrl, string chl, string ctrl, string var, object value)
        public bool WriteVariableValue(string ioServerID, string chl, string ctrl, string var, object value)
        {
           

            try
            {
                string jsonValue = JSONFormatter.Serialize(value);
                return WriteVariableValue(ioServerID, chl, ctrl, var, jsonValue);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message + ex.StackTrace);
                Logger.GetInstance().LogError(ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// 写入多个变量值
        /// </summary>
        /// <param name="rtdbBaseUrl">实时库基地址</param>
        /// <param name="chl">通道名称</param>
        /// <param name="ctrl">控制器名称</param>
        /// <param name="var">变量名称</param>
        /// <param name="value">写入对象</param>
        /// <returns></returns>
        //public bool WriteVariableMultValue(string rtdbBaseUrl, string chl, string ctrl, string var, List<GHNETBASE.RTDB.Models.DevVariable> value)
        public bool WriteVariableMultValue(List<GHNETBASE.RTDB.Models.DevVariable> value)
        {
          

            //GHRestClient.GHRestClient clent = new GHRestClient.GHRestClient(rtdbBaseUrl, OAUTH.OAuthHelper.SingletonInstance.UUID, "");
            //var devinfo = clent.GetClientInfo<DeviceInfo>(string.Format("/RTDB/mw/{0}/{1}/{2}", checkUrl(chl), checkUrl(ctrl), checkUrl(var)));
            //var jsonStr = GHCore.Serialization.JSONFormatter.Serialize(value);
            //GHRTDBObjectRequest input = new GHRTDBObjectRequest { input = jsonStr };

            //var ret = devinfo.WriteVariable(input);

            //if (ret.success)
            //    return true;
            //else
            //{
            //    System.Diagnostics.Debug.WriteLine(ret.Message);

            //    return false;
            //}

            try
            {
                return RTDBController.SingletonInstance.MutlWriteVariable(value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                Logger.GetInstance().LogError(ex.ToString());

                return false;
            }
        }

        /// <summary>
        /// 写入报警数据
        /// </summary>
        /// <param name="source">报警来源</param>
        /// <param name="level">报警级别</param>
        /// <param name="guid">报警Guid</param>
        /// <param name="value">写入字符串</param>
        /// <returns></returns>
        //public bool AddAlarm(string rtdbBaseUrl, int source, int level, string guid, string value)
        public bool AddAlarm(int source, int level, string guid,long alarmTime,string value)
        {
            //if (!checkLogin())
            //    return false;

            //GHRestClient.GHRestClient clent = new GHRestClient.GHRestClient(rtdbBaseUrl, OAUTH.OAuthHelper.SingletonInstance.UUID, "");
            //var devinfo = clent.GetClientInfo<DeviceInfo>(string.Format("/RTDB/{0}/{1}/{2}", source, level, guid.Replace("-","").ToUpper()));
            //GHRTDBObjectRequest input = new GHRTDBObjectRequest { input = value };

            //var ret = devinfo.WriteAlarm(input);

            //if (ret.success)
            //    return true;
            //else
            //{
            //    System.Diagnostics.Debug.WriteLine(ret.Message);

            //    return false;
            //}
            try
            {
                return RTDBController.SingletonInstance.AddAlarm(source, level, guid,alarmTime, value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                Logger.GetInstance().LogError(ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// 写入报警数据
        /// </summary>
        /// <param name="source">报警来源</param>
        /// <param name="level">报警级别</param>
        /// <param name="guid">报警Guid</param>
        /// <param name="value">写入对象 AlarmMessage</param>
        /// <returns></returns>
        //public bool AddAlarm(string rtdbBaseUrl, int source, int level, string guid, object value)
        public bool AddAlarm(int source, int level, string guid, long alarmTime, AlarmMessage value)//object value
        {
            //if (!checkLogin())
            //    return false;

            try
            {
                string jsonValue = JSONFormatter.Serialize(value);
                //AddAlarmLinkageVideo(value.AlarmVariableID);
                return AddAlarm(source, level, guid,alarmTime, jsonValue);

            }
            catch (Exception ex)
            {
               // System.Diagnostics.Debug.WriteLine(ex.Message + ex.StackTrace);
                Logger.GetInstance().LogError(ex.ToString());
                return false;
            }
        }



        #region 报警联动

        //public void AddAlarmLinkage(string varId,AlarmMessage alm)
        //{
        //    WriteVars(varId);
        //    //VideoCapture(varId); //dong 2018/1/5 改由PostAlarmMessage实现
        //    //增加报警联动地图和视频推送到WEB
        //    PostAlarmMessage(varId, alm);
        //    //try
        //    //{
        //    //    var lurl = ServerConfig.CloudLogin;
        //    //    var url = lurl.Substring(0, lurl.LastIndexOf(':')) + ":9027";
        //    //    var address = url + "/Alarm/VideoLinkage";
        //    //    using (System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient())
        //    //    {
        //    //        httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        //    //        //var json = GHCore.Serialization.JSONFormatter.Serialize(value);
        //    //        System.Net.Http.StringContent content = new System.Net.Http.StringContent(json);
        //    //        content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
        //    //        var responseMsg = httpClient.PostAsync(address, content).Result;
        //    //        if (responseMsg.IsSuccessStatusCode)
        //    //        {
        //    //            var ret = responseMsg.Content.ReadAsStringAsync().Result;
        //    //            if (ret == "OK")
        //    //                return true;
        //    //        }
        //    //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    Logger.GetInstance().LogError(ex.ToString());
        //    //}
        //    //return false;
        //}

        public static Dictionary<string, List<string>> LinkageWriteVar = new Dictionary<string, List<string>>();
        public static Dictionary<string, List<string>> LinkageVideo = new Dictionary<string, List<string>>();
        public static Dictionary<string, List<string>> LinkageMap = new Dictionary<string, List<string>>();
        public static Dictionary<string, List<string>> LinkageMTS = new Dictionary<string, List<string>>();
        //public bool InitLinkageConfig()
        //{
        //    try
        //    {
        //        LinkageWriteVar.Clear();
        //        LinkageVideo.Clear();
        //        LinkageMap.Clear();
        //       // LinkageMTS.Clear();

        //        var lurl = ServerConfig.CloudLogin;
        //        var url = lurl.Substring(0, lurl.LastIndexOf(':')) + ":9027";
        //        var address = url + "/Alarm/GetLinkageConfig/" + pubFun.checkUrl(GHIBMS.Common.ServerConfig.CloudClientID);
        //        using (System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient())
        //        {
        //            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        //            var responseMsg = httpClient.GetAsync(address).Result;
        //            if (responseMsg.IsSuccessStatusCode)
        //            {
        //                var ret = responseMsg.Content.ReadAsStringAsync().Result;
        //                Newtonsoft.Json.Linq.JArray jo2 = (Newtonsoft.Json.Linq.JArray)Newtonsoft.Json.JsonConvert.DeserializeObject(ret);
        //                foreach (var obj in jo2)
        //                {
        //                    List<string> writeVar = new List<string>();
        //                    foreach (var wvar in obj["WriteVar"])
        //                    {
        //                        writeVar.Add((string)wvar);
        //                        if (ServerConfig.WriteLog)
        //                            Logger.GetInstance().LogMsg("报警联动变量：" + (string)wvar);
        //                    }
                            
        //                    List<string> video = new List<string>();
        //                    foreach (var v in obj["Video"])
        //                    {
        //                        video.Add((string)v);
        //                        if (ServerConfig.WriteLog)
        //                            Logger.GetInstance().LogMsg("报警联动视频：" + (string)v);
        //                    }
        //                    //地图联动 dong 2018/1/4新增
        //                    List<string> map = new List<string>();
        //                    foreach (var m in obj["Map"])
        //                    {
        //                        map.Add((string)m);
        //                    }
        //                    if (writeVar.Count > 0)
        //                    {
        //                        LinkageWriteVar[(string)obj["VarId"]] = writeVar;   //变量写入
                               
        //                    }
        //                    if (video.Count > 0)
        //                        LinkageVideo[(string)obj["VarId"]] = video;         //视频抓拍
                            
        //                    if(map.Count>0)
        //                        LinkageMap[(string)obj["VarId"]] = map;
        //                }
        //            }
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //        Logger.GetInstance().LogError(ex.ToString());
        //    }
        //}

        //public bool WriteVars(string varId)
        //{
        //    try
        //    {
        //        if (LinkageWriteVar.ContainsKey(varId) && LinkageWriteVar[varId] != null && LinkageWriteVar[varId].Count > 0)
        //        {
        //            //1.检查是否有本地写入变量
        //            List<string> uploadCloud = new List<string>();
        //            foreach(var cmd in LinkageWriteVar[varId])
        //            {
        //                var arr = cmd.Split(':');
        //                if (arr.Length > 4)
        //                {

        //                    var io = cmd.Split(':')[0];
        //                    if (io ==pubFun.checkUrl(ServerConfig.CloudClientKEY))
        //                    {
        //                        var chid = cmd.Split(':')[1];
        //                        var conid = cmd.Split(':')[2];
        //                        var vid = cmd.Split(':')[3];
        //                        var value = cmd.Split(':')[4];

        //                        IVariable var = Rtdb.GetVariableByID(chid, conid, vid);
        //                        if (var != null)
        //                        {
        //                            var.WriteValue(value);
        //                        }
        //                        else
        //                        {
        //                           uploadCloud.Add(cmd);
        //                        }
                                  
        //                    }
        //                }
        //            }
        //            if (uploadCloud.Count == 0)
        //                return true;
        //                //2.非本地变量上传给云平台执行
        //                var lurl = ServerConfig.CloudLogin;
        //                var url = lurl.Substring(0, lurl.LastIndexOf(':')) + ":9027";
        //                var address = url + "/Alarm/WriteVars";
        //                using (System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient())
        //                {
        //                    httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        //                    var json = GHCore.Serialization.JSONFormatter.Serialize(uploadCloud);
        //                    Dictionary<string, string> post = new Dictionary<string, string>();
        //                    post.Add("", json);
        //                    System.Net.Http.FormUrlEncodedContent content = new System.Net.Http.FormUrlEncodedContent(post);
        //                    var responseMsg = httpClient.PostAsync(address, content).Result;
        //                    if (responseMsg.IsSuccessStatusCode)
        //                    {
        //                        var ret = responseMsg.Content.ReadAsStringAsync().Result;
        //                        return true;
        //                    }
        //                }
        //        }
                
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.GetInstance().LogError(ex.ToString());
        //    }
        //    return false;
        //}

        //public bool VideoCapture(string varId)
        //{
        //    try
        //    {
        //        if (LinkageVideo.ContainsKey(varId) && LinkageVideo[varId] != null && LinkageVideo[varId].Count > 0)
        //        {
        //            var lurl = ServerConfig.CloudLogin;
        //            var url = lurl.Substring(0, lurl.LastIndexOf(':')) + ":9027";
        //            var address = url + "/Alarm/VideoCapture";
        //            using (System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient())
        //            {
        //                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        //                var json = GHCore.Serialization.JSONFormatter.Serialize(LinkageVideo[varId]);
        //                Dictionary<string, string> post = new Dictionary<string, string>();
        //                post.Add("", json);
        //                System.Net.Http.FormUrlEncodedContent content = new System.Net.Http.FormUrlEncodedContent(post);
        //                var responseMsg = httpClient.PostAsync(address, content).Result;
        //                if (responseMsg.IsSuccessStatusCode)
        //                {
        //                    var ret = responseMsg.Content.ReadAsStringAsync().Result;
        //                    return true;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.GetInstance().LogError(ex.ToString());
        //    }
        //    return false;
        //}
        ////地图联动dong 2018/1/4新增 //最多推送2路报警视频 2个报警地图
        //public bool PostAlarmMessage(string varId,AlarmMessage alm)
        //{
        //    try
        //    {
        //        //if (LinkageMap.ContainsKey(varId))
        //        //{
        //        //    if (LinkageMap[varId].Count > 0)
        //        //    {
        //        //        alm.AlarmForm = LinkageMap[varId][0];
        //        //        if (LinkageMap[varId].Count > 1)
        //        //        {
        //        //            alm.AlarmForm2 = LinkageMap[varId][1];
        //        //        }
        //        //    }

        //            //}
        //            //if (LinkageVideo.ContainsKey(varId))
        //            //{
        //            //    if (LinkageVideo[varId].Count > 0)
        //            //    {
        //            //        alm.AlarmVideo = LinkageVideo[varId][0];
        //            //        if (LinkageVideo[varId].Count > 1)
        //            //        {
        //            //            alm.AlarmVideo2 = LinkageVideo[varId][1];
        //            //        }
        //            //    }

        //            //}
        //            //dong 2018/1/24 取消没有联动不上报
        //            //if (!string.IsNullOrEmpty(alm.AlarmVideo) ||
        //            //!string.IsNullOrEmpty(alm.AlarmVideo2) ||
        //            //!string.IsNullOrEmpty(alm.AlarmForm) ||
        //            // !string.IsNullOrEmpty(alm.AlarmForm2))
        //        {

        //            var lurl = ServerConfig.CloudLogin;
        //            var url = lurl.Substring(0, lurl.LastIndexOf(':')) + ":9027";
        //            var address = url + "/Alarm/PostAlarmMessage";
        //            using (System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient())
        //            {
        //                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        //                var json = GHCore.Serialization.JSONFormatter.Serialize(alm);
        //                Dictionary<string, string> post = new Dictionary<string, string>();
        //                post.Add("", json);
        //                //System.Net.Http.FormUrlEncodedContent content = new System.Net.Http.FormUrlEncodedContent(post);

        //                System.Net.Http.StringContent content = new System.Net.Http.StringContent(json);
        //                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
        //                var responseMsg = httpClient.PostAsync(address, content).Result;
        //                if (responseMsg.IsSuccessStatusCode)
        //                {
        //                    var ret = responseMsg.Content.ReadAsStringAsync().Result;
        //                    return true;
        //                }
        //            }


        //        }

        //        /*if (LinkageVideo.ContainsKey(varId) && LinkageVideo[varId] != null && LinkageVideo[varId].Count > 0)
        //        {
        //            var lurl = ServerConfig.CloudLogin;
        //            var url = lurl.Substring(0, lurl.LastIndexOf(':')) + ":9027";
        //            var address = url + "/Alarm/PostAlarmMessage";
        //            using (System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient())
        //            {
        //                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        //                var json = GHCore.Serialization.JSONFormatter.Serialize(LinkageVideo[varId]);
        //                Dictionary<string, string> post = new Dictionary<string, string>();
        //                post.Add("", json);
        //                System.Net.Http.FormUrlEncodedContent content = new System.Net.Http.FormUrlEncodedContent(post);
        //                var responseMsg = httpClient.PostAsync(address, content).Result;
        //                if (responseMsg.IsSuccessStatusCode)
        //                {
        //                    var ret = responseMsg.Content.ReadAsStringAsync().Result;
        //                    return true;
        //                }
        //            }
        //        }*/
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.GetInstance().LogError(ex.ToString());
        //    }
        //    return false;
        //}
        #endregion

    

    }
}
