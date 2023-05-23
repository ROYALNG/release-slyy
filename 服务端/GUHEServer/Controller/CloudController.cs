
using GHIBMS.Common;
using GHIBMS.Interface;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace GHIBMS.Server
{
    /*
     * 发布的数据对象定义
     * GHMNSResponse ObjectID=1 设置变量属性 =2 实时报警上传 =3 数据变量上传
     */
    public class CloudController
    {
        public event CommMsgDelegate OnCommMsg;
        //private static List<AlarmMessage> AlarmMessageList = new List<AlarmMessage>();
        private static List<IVariable> VarChangedList = new List<IVariable>();
        int max_varlist_cache = 10000;

        private bool active = false;

        private Thread threadChangeVar;
        private Thread threadUpdataAllVar;
        private Thread threadPostAlarm;
        //private Thread threadSubscribe;
        //private Thread threadGetMNS;
        private const int SLEEP_ALARM = 500;
        private const int SLEEP_VAR_COV = 500;
        private const int UP_STATE_INTERVAL = 60;//秒
        private const int UP_VARIALBE_ALL_SYNC = 24;//小时，定时同步所有变量的时间

        private DateTime DateStampUpData = DateTime.Now; //云同步数据时间
        private DateTime DateStampUpState = DateTime.Now;

        public static ConcurrentDictionary<string, DateTime> _clientSubscribeVars = new ConcurrentDictionary<string, DateTime>();



        public DriverMng driverMng = null;
        public static uint _RequestID = 0;



        public CloudController(DriverMng mng)
        {
            driverMng = mng;
        }


        /// <summary>
        /// 是否已启动
        /// </summary>
        public bool Active
        {
            get
            {
                return active;
            }
            set
            {
                active = value;
            }
        }

        /// <summary>
        /// 
        /// 派发通道通讯过程中的事件信息
        /// </summary>
        /// <param name="severity">事件分类，通常可指示事件的严重程度</param>
        /// <param name="commMsgType">事件的类型，比如在线、离线</param>
        /// <param name="wParamm">事件内容1</param>
        /// <param name="lParamm">事件内容2</param>
        public void SendCommEvent(Severity severity, CommunicationEvent commMsgType, string wParamm, string lParamm)
        {
            if (OnCommMsg != null)
                OnCommMsg(this, severity, commMsgType, wParamm, lParamm);
        }
        private object synObj = new object();


        private object synLockVar = new object();
        public void AddVarChangedList(IVariable var)
        {
            if (!active) return;
            {
                lock (synLockVar)
                {
                    if (!VarChangedList.Contains(var))
                    {
                        checkvarlist();
                        var.DateStampSync = DateTime.Now;
                        VarChangedList.Add(var);
                    }
                }
            }
        }
        private void checkvarlist()
        {
            if (VarChangedList.Count >= max_varlist_cache)
            {
                int len = VarChangedList.Count;
                for (int i = 0; i <= (len - max_varlist_cache); i++)
                {
                    VarChangedList.RemoveAt(0);
                }
            }
        }
        //取得信息记录列表
        private List<IVariable> GetChangeVariables()
        {

            lock (synLockVar)//锁定变化信息记录列表
            {
                List<IVariable> lst = new List<IVariable>();
                lst.AddRange(VarChangedList);
                VarChangedList.Clear();
                return lst;

            }
        }

        /// <summary>
        /// 开始线程
        /// </summary>
        public void Start()
        {
            active = true;

            //upload change variable

            //return;
            threadChangeVar = new Thread(new ThreadStart(PerformUploadChangeVarTask));
            //线程名，调试用
            threadChangeVar.Name = "PerformUploadVarTask";

            threadChangeVar.IsBackground = true;
            threadChangeVar.Start();

            //upload change variable

            threadUpdataAllVar = new Thread(new ThreadStart(PerformUpLoadAllVariable));
            //线程名，调试用
            threadUpdataAllVar.Name = "PerformUthreadAllVarTask";
            threadUpdataAllVar.IsBackground = true;
            threadUpdataAllVar.Start();


            threadPostAlarm = new Thread(new ThreadStart(PostAlarm));
            //线程名，调试用
            threadPostAlarm.Name = "threadPostAlarm";
            threadPostAlarm.IsBackground = true;
            threadPostAlarm.Start();

        }
        /// <summary>
        /// 停止线程
        /// </summary>
        public void Stop()
        {

            active = false;
            for (int i = 0; i < 50; i++)
            {

                if (threadChangeVar != null || threadUpdataAllVar != null || threadPostAlarm!=null)
                {
                    Thread.Sleep(100);
                }
                else
                    break;

            }
            try
            {
                if (threadChangeVar != null)
                {
                    threadChangeVar.Interrupt();
                }
            }
            catch { }
            try
            {
                if (threadUpdataAllVar != null)
                {
                    threadUpdataAllVar.Interrupt();
                }
            }
            catch { }
            try
            {
                if (threadPostAlarm != null)
                {
                    threadPostAlarm.Interrupt();
                }
            }
            catch { }

            threadChangeVar = null;
            threadUpdataAllVar = null;
            threadPostAlarm = null;

        }
        //内部线程

        private void PerformUploadChangeVarTask()
        {
            try
            {
                while (active)
                {
                    try
                    {

                        List<IVariable> lst = GetChangeVariables();

                        string ioSvr = pubFun.checkUrl(ServerConfig.CloudClientID);
                        // Dictionary<string, string> dic = new Dictionary<string, string>();
                        List<DevVariable> pubvars = new List<DevVariable>();
                        Dictionary<string, List<IVariable>> devs = new Dictionary<string, List<IVariable>>();
                        foreach (IVariable var in lst)
                        {
                            if (!active) break;
                            if (!var.Enable) continue;
                            string chID = var.ControllerObject.ChannelObject.ID;
                            string conID = var.ControllerObject.ID;

                            DevVariable dvVar = new DevVariable
                            {
                                ID = var.ID,
                                Name = var.Name,
                                IOServerID = ioSvr,
                                ChlID = chID,
                                CtrlID = conID,
                                Active = var.Active,
                                ValueType = pubFun.ValueCode2Type(var.ValueType),
                                Value = var.Value.ToString(),
                                Timestamp = var.DateStamp.ToString("yyyy/MM/dd HH:mm:ss"),
                                Area = var.Area,
                                Level = var.OperLevel,
                                Enable = var.Enable,
                                Address = var.Address,
                                EnableSave=var.EnableSaveChanged
                            };
                            pubvars.Add(dvVar);
                            var.DateStampSync = DateTime.Now;
                            //dic.Clear();
                            //dic.Add("Value", dvVar.Value);  //-V2新增
                            //dic.Add("DateStamp", dvVar.Timestamp);
                            //dic.Add("Active", dvVar.Status.ToString());

                        }


                        if (pubvars.Count > 0)
                        {

                            //if (ServerConfig.WriteLog)
                            //{
                            //    StringBuilder sbe = new StringBuilder();
                            //    foreach (var v in pubvars)
                            //        sbe.Append(v.Name + "   ");
                            //    Logger.GetInstance().LogMsg("PublishChangeVar:" + sbe.ToString());
                            //}
                            //将所有变化全部发送MQTT
                            try
                            {
                                if (ServerConfig.MqttEnable)
                                {
                                    if (driverMng.mqttController.PublishVarChanged(pubvars))
                                    {
                                        if (ServerConfig.WriteLog)
                                        {
                                            StringBuilder sbe = new StringBuilder();
                                            foreach (var v in pubvars)
                                                sbe.Append(v.Name + "   ");
                                            //SendCommEvent(Severity.事件, CommunicationEvent.COMM_INFO, "system", "发送MQTT成功：" + sbe.ToString());
                                        }

                                    }
                                    else
                                    {
                                        if (ServerConfig.WriteLog)
                                        {
                                            StringBuilder sbe = new StringBuilder();
                                            foreach (var v in pubvars)
                                                sbe.Append(v.Name + "   ");
                                            SendCommEvent(Severity.事件, CommunicationEvent.COMM_INFO, "system", "发送MQTT失败：" + sbe.ToString());
                                        }
                                    }


                                }
                            }
                            catch (Exception ex)
                            {
                                Logger.GetInstance().LogError(ex.ToString());
                                Thread.Sleep(100);
                            }

                        }


                    }
                    catch (Exception e)
                    {
                        Logger.GetInstance().LogError(e.ToString());

                        System.Threading.Thread.Sleep(100);
                    }

                    try
                    {

                        for (int i = 0; i < (int)(SLEEP_VAR_COV / 100); i++)
                        {
                            if (!active)
                            {
                                threadChangeVar = null;
                                return;
                            }
                            System.Threading.Thread.Sleep(100);
                        }
                    }
                    catch { }

                    //Thread.Sleep(SLEEP_VAR_COV);
                }
                threadChangeVar = null;
            } catch
            {
                threadChangeVar = null;
            }

        }
        private void PerformUpLoadAllVariable()
        {
            try
            {
                //bool suceedFirst = false;
                DateStampUpData = DateTime.Now;
                DateStampUpState = DateTime.Now;
                string ioSvr = ServerConfig.CloudClientID.Trim().Replace("-", "").Replace(".", "");
                Thread.Sleep(1);
                while (active)
                {

                    try
                    {
                        
                        if ( DateStampUpData < DateTime.Now) //默认24小时执行一次
                        {   //同步数据到MODBUS SERVER寄存器
                            try
                            {
                                
                                if (ServerConfig.ModbusServerEnable)
                                {
                                    foreach (IChannel ch in Rtdb.ChanList)
                                    {
                                        if (ch.Enable)
                                        {
                                            foreach (IController ctrl in ch.ConList)
                                            {
                                                if (ctrl.Enable)
                                                {
                                                    foreach (IVariable var in ctrl.VarList)
                                                    {
                                                        if (var.Enable)
                                                        {
                                                            if (var.UniteAddress > 0)
                                                            {
                                                                //要求地址间隔4个字，0，4，8
                                                                driverMng.modbusSlaveController.setHoldRegister(var.UniteAddress, var.ValueType, var.Value.ToString());
                                                            }
                                                        }
                                                    }
                                                }

                                            }
                                        }
                                    }
                                }
                              
                            }
                            catch (Exception ex)
                            {
                                Logger.GetInstance().LogError(ex.ToString());
                            }
                            try
                            {
                              
                                foreach (IChannel ch in Rtdb.ChanList)
                                {
                                    //if (!ch.Enable) continue;
                                    foreach (IController ctrl in ch.ConList)
                                    {
                                        //if (!ctrl.Enable) continue;
                                        if (ctrl.VarList.Count == 0) continue;
                                        List<DevVariable> post = new List<DevVariable>();
                                        //-V2新增
                                        //Dictionary<string, string> dic = new Dictionary<string, string>();
                                        //--
                                        foreach (IVariable var in ctrl.VarList)
                                        {

                                            if (!active)
                                            {
                                                threadUpdataAllVar = null;
                                                return;
                                            }
                                            if (!var.DisableInitUpdate&&  pubFun.DateDiff(var.DateStampSync,DateTime.Now)>5*60)
                                                AddVarChangedList(var);
                                          
                                        } //var
                                    }//con

                                }

                           
                            }
                            catch { }

                            //suceedFirst = true;
                            DateStampUpData = DateTime.Now.AddHours(UP_VARIALBE_ALL_SYNC);//默认每天同步一次所有数据
                        }
                    }
                    catch (Exception ex)
                    {
                        SendCommEvent(Severity.信息, CommunicationEvent.COMM_INFO, "系统用户", "定时云同步变量失败！" + ex.ToString());
                        Logger.GetInstance().LogError(ex.ToString());

                    }
                    //定时上传设备状态
                    try
                    { 
                        if (DateStampUpState < DateTime.Now) //30秒定时执行
                        {

                            driverMng.publishDevState();
                            DateStampUpState = DateTime.Now.AddSeconds(UP_STATE_INTERVAL);
                        }
   
                    }
                    catch(Exception ex)
                    {
                        SendCommEvent(Severity.信息, CommunicationEvent.COMM_INFO, "系统用户", "定时云同步状态失败！");
                        Logger.GetInstance().LogError(ex.ToString());
                    }
                    //报警变量5分钟定时同步  
                    //定时上传状态

                    foreach (IChannel ch in Rtdb.ChanList)
                    {
                        //if (!ch.Enable) continue;
                        foreach (IController ctrl in ch.ConList)
                        {
                            //if (!ctrl.Enable) continue;
                            if (ctrl.VarList.Count == 0) continue;
                            List<DevVariable> post = new List<DevVariable>();
                            //-V2新增
                            //Dictionary<string, string> dic = new Dictionary<string, string>();
                            //--
                            foreach (IVariable var in ctrl.VarList)
                            {

                                if (!active)
                                {
                                    threadUpdataAllVar = null;
                                    return;
                                }
                                if ((var.TimeSync > 0 && pubFun.DateDiff(var.DateStampSync, DateTime.Now) > var.TimeSync)
                                    ||(((var is IAlarmChannel || var.DeviceLabel == DeviceLabelEnum.通讯报警.ToString() || var.DeviceLabel == DeviceLabelEnum.报警输入.ToString()))&& pubFun.DateDiff(var.DateStampSync, DateTime.Now) >5*60)
                                    )
                                {
                                        AddVarChangedList(var);
                                }

                            } //var
                        }//con

                    }

                  
                    Thread.Sleep(1000);
                }//end while
                threadUpdataAllVar = null;
            } catch
            {
                threadUpdataAllVar = null;
            }
        }


        private void PostAlarm()
        {
            try
            {
                while (active)
                {
                    
                    List<AlarmMessage> almArr = Almdb.GetAlarm();

                    //将所有变化全部发送MQTT
                    try
                    {
                        if (ServerConfig.MqttEnable)
                        {
                            if(almArr.Count>0)
                                 MqttController.PublishAlarmReal(almArr);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.GetInstance().LogError(ex.ToString());
                        Thread.Sleep(100);
                    }

                    //延时
                    try
                    {

                        for (int i = 0; i < (int)(SLEEP_ALARM / 100); i++)
                        {
                            if (!active)
                            {
                                threadPostAlarm = null;
                                return;
                            }
                            System.Threading.Thread.Sleep(100);
                        }
                    }
                    catch { }

                   
                }//while
                threadPostAlarm = null;
            }
            catch { }
        }
    }
}
  



