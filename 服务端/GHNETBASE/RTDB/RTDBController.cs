using GHCore;
using GHCore.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace GHNETBASE.RTDB
{
    public class RTDBController 
    {
        #region Instance
        private static readonly RTDBController _instance = new RTDBController();
        private RTDBController()
        {
        }
        public static RTDBController SingletonInstance
        {
            get
            {
                return _instance;
            }
        }
        #endregion

        // GET api/RTDB/{chl} 新建、更新通道
        public bool AddChannel(string authkey,string chl, string value)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(authkey))
                {
                    GHLogger.Log("authkey不存在", LogCategory.Warn);
                    return false;
                }
                else
                {
                    //var chlvalue = DeviceManagement.SingletonInstance.ChlUpdate(authkey, chl, value);
                    //if (!chlvalue)
                     var   chlvalue = DeviceManagement.SingletonInstance.ChlAdd(authkey, chl, value);

                    return chlvalue;
                }
            }
            catch (Exception ex)
            {
                GHLogger.Log(ex.Message, LogCategory.Warn);
                return false;
            }

        }

        // GET api/RTDB/{chl}/{ctrl} 新建、更新控制器
        public bool AddController(string authkey, string chl, string ctrl, string value)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(authkey))
                {
                    GHLogger.Log("authkey不存在", LogCategory.Warn);
                    return false;
                }
                else
                {
                   // var ctrlvalue = DeviceManagement.SingletonInstance.CtrlUpdate(authkey, chl, ctrl, value);
                    //if (!ctrlvalue)
                    var    ctrlvalue = DeviceManagement.SingletonInstance.CtrlAdd(authkey, chl, ctrl, value);

                    return ctrlvalue;
                }
            }
            catch (Exception ex)
            {
                GHLogger.Log(ex.Message, LogCategory.Warn);
                return false;
            }

        }

        // GET api/RTDB/{chl}/{ctrl}/{var} 新建、更新变量
        public bool AddVariable(string authkey, string chl, string ctrl, string var, string value)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(authkey))
                {
                    GHLogger.Log("authkey不存在", LogCategory.Warn);
                    return false;
                }
                else
                {
                    //var varvalue = DeviceManagement.SingletonInstance.VarUpdate(authkey, chl, ctrl, var, value);
                    //if (!varvalue)
                   var     varvalue = DeviceManagement.SingletonInstance.VarAdd(authkey, chl, ctrl, var, value);

                    return varvalue;
                }
            }
            catch (Exception ex)
            {
                GHLogger.Log(ex.Message, LogCategory.Warn);
                return false;
            }

        }

        public bool MutlWriteVariable(string authkey, List<GHNETBASE.RTDB.Models.DevVariable> value)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(authkey))
                {
                    GHLogger.Log("authkey不存在", LogCategory.Warn);
                    return false;
                }
                else
                {
                    var varvalue = false;

                    Dictionary<GHNETBASE.RTDB.Models.DevVariable, string> dic = new Dictionary<Models.DevVariable, string>();
                    System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
                    watch.Start();
                    
                    foreach (var jsonV in value)
                    {
                        string json = GHCore.Serialization.JSONFormatter.Serialize(jsonV);
                        dic.Add(jsonV, json);
                    }
                    watch.Stop();
                    System.Diagnostics.Debug.WriteLine("序列化时间:" + watch.ElapsedMilliseconds / 1000f + "/" + value.Count);


                    watch.Reset();
                    watch.Start();
                    foreach (var jsonV in dic.Keys)
                    {
                        //string json = GHCore.Serialization.JSONFormatter.Serialize(jsonV);
                        //varvalue = DeviceManagement.SingletonInstance.VarUpdate(authkey, jsonV.ChlID, jsonV.CtrlID, jsonV.ID, json);
                        //if (!varvalue)
                            varvalue = DeviceManagement.SingletonInstance.VarAdd(authkey, jsonV.ChlID, jsonV.CtrlID, jsonV.ID, dic[jsonV]);
                    }
                    watch.Stop();
                    System.Diagnostics.Debug.WriteLine("写入实时库时间:" + watch.ElapsedMilliseconds / 1000f + "/" + dic.Keys.Count);

                    return varvalue;
                }
            }
            catch (Exception ex)
            {
                GHLogger.Log(ex.Message, LogCategory.Warn);
                return false;
            }

        }

        // GET api/RTDB/{chl}/{ctrl}/{var} 读单个变量的值
        public string GetVariable(string authkey,string chl, string ctrl, string var)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(authkey))
                {
                    GHLogger.Log("authkey不存在", LogCategory.Warn);
                    return "";
                }
                else
                {
                    var val = DeviceManagement.SingletonInstance.VarGetValue(authkey, chl, ctrl, var);
                    return val;
                }
            }
            catch (Exception ex)
            {
                GHLogger.Log(ex.Message, LogCategory.Warn);
                return "";
            }
        }

        public List<GHNETBASE.RTDB.Models.DevVariable> GetMultVariables(string authkey, List<GHNETBASE.RTDB.Models.DevVariable> value)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(authkey))
                {
                    GHLogger.Log("authkey不存在", LogCategory.Warn);
                    return new List<Models.DevVariable>();
                }
                else
                {
                    List<GHNETBASE.RTDB.Models.DevVariable> ret = new List<GHNETBASE.RTDB.Models.DevVariable>();
                    foreach (var jsonV in value)
                    {
                        string varvalue = DeviceManagement.SingletonInstance.VarGetValue(authkey, jsonV.ChlID, jsonV.CtrlID, jsonV.ID);
                        if (string.IsNullOrEmpty(varvalue))
                            continue;
                        ret.Add(GHCore.Serialization.JSONFormatter.Deserialize<GHNETBASE.RTDB.Models.DevVariable>(varvalue));
                    }

                    return ret;
                }
            }
            catch (Exception ex)
            {
                GHLogger.Log(ex.Message, LogCategory.Warn);
                return new List<Models.DevVariable>();
            }

        }

        #region 报警

        // GET api/RTDB/{chl} 新建、更新通道
        public bool AddAlarm(string authkey,int source, int level, string guid, string value)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(authkey))
                {
                    GHLogger.Log("authkey不存在", LogCategory.Warn);
                    return false;
                }
                else
                {
                    var ret = DeviceManagement.SingletonInstance.AmsAdd(authkey, source, level, guid, value);

                    return ret;
                }
            }
            catch (Exception ex)
            {
                GHLogger.Log(ex.Message, LogCategory.Warn);
                return false;
            }

        }
        #endregion
    }
}
