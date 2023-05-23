using GHCore.Serialization;
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

        private bool checkLogin()
        {
            if(!string.IsNullOrEmpty(OAUTH.OAuthHelper.SingletonInstance.UUID))
            {
                return true;
            }
            return false;
        }
        public string checkUrl(string org)
        {
            return org.Replace("?", "").Replace(".", "").Replace("&", "").Replace("=", "")
                .Replace("/"," ").Replace("\\"," ");
        }

        /// <summary>
        /// 增加通道
        /// </summary>
        /// <param name="rtdbBaseUrl">实时库基地址</param>
        /// <param name="chl">通道名称</param>
        /// <param name="value">写入的值</param>
        /// <returns></returns>
        //public bool AddChannel(string rtdbBaseUrl, string chl, string value)
        public bool AddChannel(string chl, string value)
        {
            if (!checkLogin())
                return false;

            //GHRestClient.GHRestClient clent = new GHRestClient.GHRestClient(rtdbBaseUrl, OAUTH.OAuthHelper.SingletonInstance.UUID, "");
            //var devinfo = clent.GetClientInfo<DeviceInfo>(string.Format("/RTDB/{0}", checkUrl(chl)));
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
                return RTDBController.SingletonInstance.AddChannel(OAUTH.OAuthHelper.SingletonInstance.UUID, checkUrl(chl), value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// 增加通道
        /// </summary>
        /// <param name="rtdbBaseUrl">实时库基地址</param>
        /// <param name="chl">通道名称</param>
        /// <param name="value">写入的值</param>
        /// <returns></returns>
        //public bool AddChannel(string rtdbBaseUrl, string chl, object value)
        public bool AddChannel(string chl, object value)
        {
            if (!checkLogin())
                return false;

            try
            {
                string jsonValue = JSONFormatter.Serialize(value);
                return AddChannel(chl, jsonValue);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 增加控制器
        /// </summary>
        /// <param name="rtdbBaseUrl">实时库基地址</param>
        /// <param name="chl">通道名称</param>
        /// <param name="ctrl">控制器名称</param>
        /// <param name="value">写入的值</param>
        /// <returns></returns>
        //public bool AddController(string rtdbBaseUrl, string chl, string ctrl, string value)
        public bool AddController(string chl, string ctrl, string value)
        {
            if (!checkLogin())
                return false;

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
                return RTDBController.SingletonInstance.AddController(OAUTH.OAuthHelper.SingletonInstance.UUID, checkUrl(chl), checkUrl(ctrl), value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return false;
            }
        }
        //public bool AddController(string rtdbBaseUrl, string chl, string ctrl, object value)
        public bool AddController(string chl, string ctrl, object value)
        {
            if (!checkLogin())
                return false;

            try
            {
                string jsonValue = JSONFormatter.Serialize(value);
                return AddController(chl,ctrl, jsonValue);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 增加变量
        /// </summary>
        /// <param name="rtdbBaseUrl">实时库基地址</param>
        /// <param name="chl">通道名称</param>
        /// <param name="ctrl">控制器名称</param>
        /// <param name="var">变量名称</param>
        /// <param name="value">写入的值</param>
        /// <returns></returns>
        //public bool AddVariable(string rtdbBaseUrl, string chl, string ctrl, string var, string value)
        public bool AddVariable(string chl, string ctrl, string var, string value)
        {
            if (!checkLogin())
                return false;

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
                return RTDBController.SingletonInstance.AddVariable(OAUTH.OAuthHelper.SingletonInstance.UUID, checkUrl(chl), checkUrl(ctrl), checkUrl(var), value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// 增加变量
        /// </summary>
        /// <param name="rtdbBaseUrl">实时库基地址</param>
        /// <param name="chl">通道名称</param>
        /// <param name="ctrl">控制器名称</param>
        /// <param name="var">变量名称</param>
        /// <param name="value">写入的值</param>
        /// <returns></returns>
        //public bool AddVariable(string rtdbBaseUrl, string chl, string ctrl, string var, object value)
        public bool AddVariable(string chl, string ctrl, string var, object value)
        {
            if (!checkLogin())
                return false;

            try
            {
                string jsonValue = JSONFormatter.Serialize(value);
                return AddVariable(chl, ctrl, var, jsonValue);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message + ex.StackTrace);
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
            if (!checkLogin())
                return false;
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
                return RTDBController.SingletonInstance.MutlWriteVariable(OAUTH.OAuthHelper.SingletonInstance.UUID, value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);

                return false;
            }
        }

        /// <summary>
        /// 读取单个变量值
        /// </summary>
        /// <param name="rtdbBaseUrl">实时库基地址</param>
        /// <param name="chl">通道名称</param>
        /// <param name="ctrl">控制器名称</param>
        /// <param name="var">变量名称</param>
        /// <returns>String</returns>
        //public string ReadVariableValue(string rtdbBaseUrl, string chl, string ctrl, string var)
        public string ReadVariableValue(string chl, string ctrl, string var)
        {
            if (!checkLogin())
                return "";

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
                return RTDBController.SingletonInstance.GetVariable(OAUTH.OAuthHelper.SingletonInstance.UUID, checkUrl(chl), checkUrl(ctrl), checkUrl(var));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);

                return "";
            }
        }

        /// <summary>
        /// 读取单个变量值
        /// </summary>
        /// <param name="rtdbBaseUrl">实时库基地址</param>
        /// <param name="chl">通道名称</param>
        /// <param name="ctrl">控制器名称</param>
        /// <param name="var">变量名称</param>
        /// <returns>typeof T object</returns>
        //public T ReadVariableValue<T>(string rtdbBaseUrl, string chl, string ctrl, string var)
        public T ReadVariableValue<T>(string chl, string ctrl, string var)
        {
            if (!checkLogin())
                return default(T);

            try
            {
                string json = ReadVariableValue(chl, ctrl, var);
                if (string.IsNullOrEmpty(json))
                    return default(T);
                return JSONFormatter.Deserialize<T>(json);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
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
            if (!checkLogin())
                return new List<Models.DevVariable>();

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
                return RTDBController.SingletonInstance.GetMultVariables(OAUTH.OAuthHelper.SingletonInstance.UUID, var);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);

                return new List<Models.DevVariable>();
            }
        }

        /// <summary>
        /// 写入变量值
        /// </summary>
        /// <param name="rtdbBaseUrl">实时库基地址</param>
        /// <param name="chl">通道名称</param>
        /// <param name="ctrl">控制器名称</param>
        /// <param name="var">变量名称</param>
        /// <param name="value">写入的值</param>
        /// <returns></returns>
        //public bool WriteVariableValue(string rtdbBaseUrl, string chl, string ctrl, string var, string value)
        public bool WriteVariableValue(string chl, string ctrl, string var, string value)
        {
            if (!checkLogin())
                return false;

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
                return RTDBController.SingletonInstance.AddVariable(OAUTH.OAuthHelper.SingletonInstance.UUID, checkUrl(chl), checkUrl(ctrl), checkUrl(var), value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);

                return false;
            }
        }
        /// <summary>
        /// 写入变量值
        /// </summary>
        /// <param name="rtdbBaseUrl">实时库基地址</param>
        /// <param name="chl">通道名称</param>
        /// <param name="ctrl">控制器名称</param>
        /// <param name="var">变量名称</param>
        /// <param name="value">写入对象</param>
        /// <returns></returns>
        //public bool WriteVariableValue(string rtdbBaseUrl, string chl, string ctrl, string var, object value)
        public bool WriteVariableValue(string chl, string ctrl, string var, object value)
        {
            if (!checkLogin())
                return false;

            try
            {
                string jsonValue = JSONFormatter.Serialize(value);
                return WriteVariableValue(chl, ctrl, var, jsonValue);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message + ex.StackTrace);
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
            if (!checkLogin())
                return false;

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
                return RTDBController.SingletonInstance.MutlWriteVariable(OAUTH.OAuthHelper.SingletonInstance.UUID, value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);

                return false;
            }
        }

        /// <summary>
        /// 写入报警数据
        /// </summary>
        /// <param name="rtdbBaseUrl">报警实时库基地址</param>
        /// <param name="source">报警来源</param>
        /// <param name="level">报警级别</param>
        /// <param name="guid">报警Guid</param>
        /// <param name="value">写入字符串</param>
        /// <returns></returns>
        //public bool AddAlarm(string rtdbBaseUrl, int source, int level, string guid, string value)
        public bool AddAlarm(int source, int level, string guid, string value)
        {
            if (!checkLogin())
                return false;

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
                return RTDBController.SingletonInstance.AddAlarm(OAUTH.OAuthHelper.SingletonInstance.UUID, source, level, guid, value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);

                return false;
            }
        }
        /// <summary>
        /// 写入报警数据
        /// </summary>
        /// <param name="rtdbBaseUrl">报警实时库基地址</param>
        /// <param name="source">报警来源</param>
        /// <param name="level">报警级别</param>
        /// <param name="guid">报警Guid</param>
        /// <param name="value">写入对象 AlarmMessage</param>
        /// <returns></returns>
        //public bool AddAlarm(string rtdbBaseUrl, int source, int level, string guid, object value)
        public bool AddAlarm(int source, int level, string guid, object value)
        {
            if (!checkLogin())
                return false;

            try
            {
                string jsonValue = JSONFormatter.Serialize(value);
                return AddAlarm(source, level, guid, jsonValue);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message + ex.StackTrace);
                return false;
            }
        }

    }
}
