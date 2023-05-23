using GHCore;
using GHIBMS.Common;
using System;
using System.Collections.Generic;


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

        public bool AddIOServer(string ioServerID, string value)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ioServerID))
                {
                    GHLogger.Log("ioServerID不存在", LogCategory.Warn);
                    return false;
                }
                else
                {
                    //var iosvrvalue = DeviceManagement.SingletonInstance.IOServerUpdate(ioServerID, value);
                    //if (!iosvrvalue)
                    var iosvrvalue = DeviceManagement.SingletonInstance.IOServerAdd(ioServerID, value);

                    return iosvrvalue;
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
                return false;
            }

        }

        // GET api/RTDB/{chl} 新建、更新通道
        public bool AddChannel(string ioServerID,string chl, string value)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ioServerID))
                {
                    GHLogger.Log("ioServerID不存在", LogCategory.Warn);
                    return false;
                }
                else
                {
                    //var chlvalue = DeviceManagement.SingletonInstance.ChlUpdate(ioServerID, chl, value);
                    //if (!chlvalue)
                    var chlvalue = DeviceManagement.SingletonInstance.ChlAdd(ioServerID, chl, value);

                    return chlvalue;
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
                return false;
            }

        }

        // GET api/RTDB/{chl}/{ctrl} 新建、更新控制器
        public bool AddController(string ioServerID, string chl, string ctrl, string value)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ioServerID))
                {
                    GHLogger.Log("ioServerID不存在", LogCategory.Warn);
                    return false;
                }
                else
                {
                    // var ctrlvalue = DeviceManagement.SingletonInstance.CtrlUpdate(ioServerID, chl, ctrl, value);
                    //if (!ctrlvalue)
                    var ctrlvalue = DeviceManagement.SingletonInstance.CtrlAdd(ioServerID, chl, ctrl, value);

                    return ctrlvalue;
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogMsg("云同步添加控制器失败！" + ctrl);
                Logger.GetInstance().LogError(ex.ToString());
                return false;
            }

        }

        // GET api/RTDB/{chl}/{ctrl}/{var} 新建、更新变量
        public bool AddVariable(string ioServerID, string chl, string ctrl, string var, string value)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ioServerID))
                {
                    GHLogger.Log("ioServerID不存在", LogCategory.Warn);
                    return false;
                }
                else
                {
                    //var varvalue = DeviceManagement.SingletonInstance.VarUpdate(ioServerID, chl, ctrl, var, value);
                    //if (!varvalue)
                    var varvalue = DeviceManagement.SingletonInstance.VarAdd(ioServerID, chl, ctrl, var, value);

                    return varvalue;
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogMsg("云同步添加变量失败！" + var);
                Logger.GetInstance().LogError(ex.ToString());
                return false;
            }

        }

        public bool MutlWriteVariable( List<GHNETBASE.RTDB.Models.DevVariable> value)
        {
            try
            {

                    var varvalue = false;

                    Dictionary<GHNETBASE.RTDB.Models.DevVariable, string> dic = new Dictionary<Models.DevVariable, string>();
                    //System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
                   // watch.Start();
                    
                    foreach (var jsonV in value)
                    {
                        string json = GHCore.Serialization.JSONFormatter.Serialize(jsonV);
                        dic.Add(jsonV, json);
                    }
                   // watch.Stop();
                    //System.Diagnostics.Debug.WriteLine("序列化时间:" + watch.ElapsedMilliseconds / 1000f + "/" + value.Count);


                    //watch.Reset();
                    //watch.Start();
                    foreach (var jsonV in dic.Keys)
                    {
                        //string json = GHCore.Serialization.JSONFormatter.Serialize(jsonV);
                        //varvalue = DeviceManagement.SingletonInstance.VarUpdate(jsonV.IOServerID, jsonV.ChlID, jsonV.CtrlID, jsonV.ID, json);
                        //if (!varvalue)
                        try
                        {
                            varvalue = DeviceManagement.SingletonInstance.VarAdd(jsonV.IOServerID, jsonV.ChlID, jsonV.CtrlID, jsonV.ID, dic[jsonV]);
                        }catch (Exception ex)
                        {
                               Logger.GetInstance().LogMsg("云同步添加变量失败！" + jsonV.ID);
                               Logger.GetInstance().LogError(ex.ToString());
                        }
                    }
                    //watch.Stop();
                   // System.Diagnostics.Debug.WriteLine("写入实时库时间:" + watch.ElapsedMilliseconds / 1000f + "/" + dic.Keys.Count);

                    return varvalue;
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogMsg("云同步添加变量失败！" );
                Logger.GetInstance().LogError(ex.ToString());
                return false;
            }

        }

        // GET api/RTDB/{chl}/{ctrl}/{var} 读单个变量的值
        public string GetVariable(string ioServerID, string chl, string ctrl, string var)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ioServerID))
                {
                    GHLogger.Log("ioServerID不存在", LogCategory.Warn);
                    return "";
                }
                else
                {
                    var val = DeviceManagement.SingletonInstance.VarGetValue(ioServerID, chl, ctrl, var);
                    return val;
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
                return "";
            }
        }

        public List<GHNETBASE.RTDB.Models.DevVariable> GetMultVariables( List<GHNETBASE.RTDB.Models.DevVariable> value)
        {
            try
            {
                    List<GHNETBASE.RTDB.Models.DevVariable> ret = new List<GHNETBASE.RTDB.Models.DevVariable>();
                    foreach (var jsonV in value)
                    {
                        string varvalue = DeviceManagement.SingletonInstance.VarGetValue(jsonV.IOServerID, jsonV.ChlID, jsonV.CtrlID, jsonV.ID);
                        if (string.IsNullOrEmpty(varvalue))
                            continue;
                        ret.Add(GHCore.Serialization.JSONFormatter.Deserialize<GHNETBASE.RTDB.Models.DevVariable>(varvalue));
                    }

                    return ret;
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
                return new List<Models.DevVariable>();
            }

        }

        #region 报警

        // GET api/RTDB/{chl} 新建、更新通道
        public bool AddAlarm(int source, int level, string guid, long alarmTime,string value)
        {
            try
            {
                    var ret = DeviceManagement.SingletonInstance.AmsAdd(source, level, guid,alarmTime, value);

                    return ret;
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
              //  GHLogger.Log(ex.ToString(), LogCategory.Warn);
                return false;
            }

        }
        public long AmsStatsIncrement(int source, int level, long value = 1)
        {
            try
            {
                var ret = DeviceManagement.SingletonInstance.AmsStatsIncrement(source, level, value);

                return ret;
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
                //  GHLogger.Log(ex.ToString(), LogCategory.Warn);
                return -1;
            }
        }
        public long AmsStatsGet(int source, int level)
        {
            try
            {
                var ret = DeviceManagement.SingletonInstance.AmsStatsGet(source, level);

                return long.Parse(ret);
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
                //  GHLogger.Log(ex.ToString(), LogCategory.Warn);
                return -1;
            }
        }
        #endregion
    }
}
