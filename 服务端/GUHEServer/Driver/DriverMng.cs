
using GHIBMS.Common;
using GHIBMS.Common.BaseClass;
using GHIBMS.Interface;
#if !IOPLUS
using GHIBMS.Pub;
#endif
using GHIBMS.Server.Controller;
using GHIBMS.Server.Models;
using GHIBMS.Server.Pub;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Xml;

namespace GHIBMS.Server
{
    public class DriverMng
    {
        private static TaskController taskController; //定时任务线程 
        private static DBAssistant dbAssistant;//辅助数据库线程
        private static AutoResetController autoResetController; //变量自动复位线程
        private static PushMsgController pushMsgController;//通知推送
        private static MailController mailController;//通知推送
        public  ModbusSlaveController modbusSlaveController;
        private XVBAEngine VarEventVBAEngine;
        private XVBAEngine GlobalVBAEngine;
        CloudController _CloudController;
        private List<string> g_VbsList = new List<string>();
        private Thread VBSThread;
        private bool VBSThreadActive = false;
        private AccidenceAnalyzer express;
        public delegate void AddOperationLogDel(String Severity, String Title, String MachineName, String Message);
        public event AddOperationLogDel OnAddOperationLog;
        public MqttController mqttController = null;

        public delegate void UpdateTreeMsgdelegate(object sender, string nodetext, NodeStateEnum NodeStatus);
        public event UpdateTreeMsgdelegate OnUpdateTree;
        private UDPServer _udpServer;

        private DateTime DatestampFromMaster;

        private System.Threading.Timer backupTimer;
        public bool StandbyActive = false;
        private static readonly object runLock = new object();
        //public static System.Diagnostics.PerformanceCounter CpuPerformanceCounter = new System.Diagnostics.PerformanceCounter("Processor", "% Processor Time", "_Total");
        //private PerformanceCounter MemoPerformanceCounter = new PerformanceCounter("Memory", "Available MBytes");
        private bool licOK = false;
        public bool sentLogEnable = false;
        private static RecordController recordController; //历史记录线程

        private static List<commLog> commLogs = new List<commLog>();
        private void UpdateTree(object sender, string nodetext, NodeStateEnum NodeStatus)
        {
            if (OnUpdateTree != null)
            {
                OnUpdateTree(sender, nodetext, NodeStatus);
            }
        }
        #region Formmain.DrvMng
        public void AddOperationLog(String Severity, String Title, String MachineName, String Message)
        {
            if (OnAddOperationLog != null)
                OnAddOperationLog(Severity, Title, MachineName, Message);
            commLog log = new commLog
            {
                DateStamp = DateTime.Now.ToString("yyyy-MM-dd HHmmss"),
                Severity = Severity,
                Type = Title,
                Title = MachineName,
                Content = Message
            };
            AddCommLog(log);
            if (sentLogEnable)
            {
                SentLog(log);
            }
            
        }

        public DriverMng()
        {

            if (ServerConfig.MqttEnable)
            {
                mqttController = new MqttController(this);
                mqttController.OnCommMsg += new CommMsgDelegate(ShowCommMsg);
                if (!ServerConfig.StandbyEnable || ServerConfig.StandbyEnable && ServerConfig.StandbyMaster || ServerConfig.StandbyEnable && !ServerConfig.StandbyMaster && StandbyActive)
                    mqttController.Start();
                else
                {
                    AddOperationLog(Severity.错误.ToString(), "提示", "系统", "双机热备备机状态禁止连接物联网平台！");
                    Logger.GetInstance().LogMsg("双机热备备机状态禁止连接物联网平台！！");
                }
            }
            _CloudController = new CloudController(this);
            //公式解析
            express = new AccidenceAnalyzer();
            express.OnAccidenceAnalysis += new AccidenceAnalyzer.AccidenceAnalysis(express_OnAccidenceAnalysis);
            express.OnCallBack += new AccidenceAnalyzer.CallBack(express_OnCallBack);

            //初始化全局线程控制器
            dbAssistant = new DBAssistant(ServerConfig.DbHost, ServerConfig.LogdbName, ServerConfig.DbUser, ServerConfig.DbPw);//辅助数据库线程,写log数据库

            // recordController = new RecordController();

            taskController = new TaskController();

            autoResetController = new AutoResetController();
            pushMsgController = new PushMsgController();
            mailController = new MailController();
            if (ServerConfig.ModbusServerEnable)
                modbusSlaveController = new ModbusSlaveController();
            if (ServerConfig.StandbyEnable)
            {

                _udpServer = new UDPServer();
                _udpServer.Start(ServerConfig.StandbyMasterPort);
                _udpServer.OnReceive += _udpServer_OnReceive;

                if (!ServerConfig.StandbyMaster)
                    backupTimer = new System.Threading.Timer(new TimerCallback(timerCall), this, 10000, 10000);

                DatestampFromMaster = DateTime.Now;
            }


        }
        private void timerCall(object obj)
        {
            if (!ServerConfig.StandbyMaster)
            {
                string msg = "FFFFCCCC";

                byte[] bmsg = System.Text.Encoding.Default.GetBytes(msg);
                if (_udpServer != null)
                    _udpServer.SendUseServer(ServerConfig.StandbyMasterIp, ServerConfig.StandbyMasterPort, bmsg);
                if (DatestampFromMaster.AddMinutes(1) < DateTime.Now)
                {
                    if (!StandbyActive)
                    {
                        SwitchActiveMode();
                    }
                }
                else
                {
                    if (StandbyActive)
                    {
                        SwitchStandbyMode();
                    }
                }
            }


        }
        private void SwitchActiveMode()
        {
            StandbyActive = true;
            if (ServerConfig.MqttEnable && mqttController != null)
                if (!mqttController.active)
                    mqttController.Start();
            if (ServerConfig.RunningState == ServerStateEnum.STOPED)
            {


                AddOperationLog(Severity.错误.ToString(), "提示", "系统", "双机热备备机启动！");
                Logger.GetInstance().LogMsg("双机热备备机启动！");
                ConnectedDrive();
            }
        }
        private void SwitchStandbyMode()
        {
            StandbyActive = false;
            if (ServerConfig.MqttEnable && mqttController != null)
                if (mqttController.active)
                    mqttController.Stop();
            if (ServerConfig.RunningState == ServerStateEnum.RUNING)
            {

                AddOperationLog(Severity.错误.ToString(), "提示", "系统", "双机热备备机待机！");
                Logger.GetInstance().LogMsg("双机热备备机待机！");
                DisConnectedDrive();
            }
        }

        private void _udpServer_OnReceive(UDPServer uc, string remote, int rPort, string msg)
        {
            if (ServerConfig.StandbyMaster)
            {
                if (msg == "FFFFCCCC")
                {
                    if (ServerConfig.RunningState == ServerStateEnum.RUNING)
                    {
                        string msg2 = "FFFFSSSS";
                        byte[] bmsg = System.Text.Encoding.Default.GetBytes(msg2);
                        _udpServer.SendUseServer(remote, rPort, bmsg);
                    }
                }
            }
            else
            {
                if (msg == "FFFFSSSS")
                {
                    DatestampFromMaster = DateTime.Now;
                }
            }
        }
        public void standbyReset()
        {
            if (ServerConfig.StandbyEnable)
            {
                if (_udpServer != null)
                {
                    _udpServer.Stop();
                    _udpServer = null;
                }
                if (backupTimer != null)
                    backupTimer.Dispose();

                _udpServer = new UDPServer();
                _udpServer.Start(ServerConfig.StandbyMasterPort);
                _udpServer.OnReceive += _udpServer_OnReceive;
                backupTimer = new System.Threading.Timer(new TimerCallback(timerCall), this, 10000, 10000);
            }
            else
            {
                if (_udpServer != null)
                {
                    _udpServer.Stop();
                    _udpServer = null;
                }
                if (backupTimer != null)
                    backupTimer.Dispose();

            }

        }
        public void mqttReset()
        {
            StopMqtt();
            if (ServerConfig.MqttEnable)
            {

                mqttController = new MqttController(this);
                mqttController.OnCommMsg += new CommMsgDelegate(ShowCommMsg);
                if (!ServerConfig.StandbyEnable || ServerConfig.StandbyEnable && ServerConfig.StandbyMaster || ServerConfig.StandbyEnable && !ServerConfig.StandbyMaster && StandbyActive)
                    mqttController.Start();
                else
                {
                    AddOperationLog(Severity.错误.ToString(), "提示", "系统", "双机热备备机状态禁止连接物联网平台！");
                    Logger.GetInstance().LogMsg("双机热备备机状态禁止连接物联网平台！");
                }
            }

        }
        public void StopMqtt()
        {
            try
            {
                if (mqttController != null)
                {


                    mqttController.Stop();
                    mqttController.OnCommMsg -= new CommMsgDelegate(ShowCommMsg);
                }
                mqttController = null;
            }
            catch { }
        }


        #region 驱动启停

        public void ConnectedDrive()
        {
            lock (runLock)
            {

                if (ServerConfig.RunningState != ServerStateEnum.STOPED) return;
                if (Rtdb.ChanList.Count == 0)
                {
                    Logger.GetInstance().LogMsg("通讯驱动启动失败！通道数量：0");
                    AddOperationLog(Severity.错误.ToString(), "提示", "系统", "通讯驱动启动失败！通道数量：0");

                    return;
                }
                if (ServerConfig.StandbyEnable)
                {
                    if (!ServerConfig.StandbyMaster)
                    {
                        if (!StandbyActive)
                        {
                            AddOperationLog(Severity.错误.ToString(), "提示", "系统", "双机热备处于备机状态下禁止启动！");
                            Logger.GetInstance().LogMsg("双机热备处于备机状态下禁止启动！");
                            ServerConfig.RunningState = ServerStateEnum.STOPED;
                            return;
                        }

                    }
                }
                //if(ServerConfig.MqttEnable&&mqttController!=null)
                //     mqttController.Start();
                ServerConfig.RunningState = ServerStateEnum.BUSYING;
                try
                {
                    if (ServerConfig.ModbusServerEnable)
                    {
                        if (modbusSlaveController == null)
                            modbusSlaveController = new ModbusSlaveController();
                        modbusSlaveController.Start();
                    }
                    //frmWait.SetText("正在登录云服务器......");
                    _CloudController.OnCommMsg += new CommMsgDelegate(ShowCommMsg);
                    _CloudController.Start();
                    //启动定时任务
                    taskController.Start();
                    //启动自动复位
                    autoResetController.Start();
                    //启动通知推送
                    pushMsgController.Start();
                    mailController.Start();
                    if (recordController == null)
                        recordController = new RecordController();
                    recordController.Start();
                }
                catch { }
                //if (mqttController != null)
                //    mqttController.PublishIOServerState();

                foreach (IChannel ch in Rtdb.ChanList)
                {
                    ch.StartState = true;
                    ch.Active = true;
                    foreach (IController con in ch.ConList)
                    {
                        con.Active = true;
                        foreach (IVariable var in con.VarList)
                        {
                            var.Quality = (short)COMM_QUALITY_STATUS.NOT_CONNECTED;
                            var.Counter = 0;
                            var.Active = true;
                            var.OnValueChange += new OnValueChangeDelegate(var_OnValueChange);
                            var.OnWriteValueCallback += new OnWriteValueCallbackDelegate(var_OnWriteValueCallback);
                            var.OnPropertyChange += new OnPropertyChangeDelegate(var_OnProrertyChange);
                        }
                    }
                }
                foreach (BaseChannel chan in Rtdb.ChanList)
                {
                    try
                    {
                        //订阅通讯事件
                        chan.OnCommMsg += new CommMsgDelegate(ShowCommMsg);
                        //try
                        //{
                        if (chan.Enable)
                        {
                            try
                            {
                                //frmWait.SetText("正在启动通讯通道" + chan.Name + ".....");
                                chan.Start();

                                Logger.GetInstance().LogMsg("通讯通道启动成功：" + chan.Name);
                            }
                            catch (Exception ex)
                            {
                                AddOperationLog(Severity.错误.ToString(), "通讯", chan.Name, "通讯通道启动错误！");
                                Logger.GetInstance().LogError(ex.ToString());
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.GetInstance().LogError(ex.ToString());
                        AddOperationLog(Severity.错误.ToString(), "通讯", chan.Name, "通讯通道启动错误！");

                    }
                }
                try
                {




                    #region VBS
                    // 设置脚本引擎全局对象
                    // XVBAGlobalObject.myWindow = new XVBAWindowObject(this, this.Text);
                    // 创建脚本引擎
                    VarEventVBAEngine = new XVBAEngine();
                    VarEventVBAEngine.AddReferenceAssemblyByType(this.GetType());
                    VarEventVBAEngine.VBCompilerImports.Add("GHIBMS.Server.Global");


                    GlobalVBAEngine = new XVBAEngine();
                    GlobalVBAEngine.AddReferenceAssemblyByType(this.GetType());
                    GlobalVBAEngine.VBCompilerImports.Add("GHIBMS.Server.Global");
                    #endregion
                    LoadVBS();
                    StartVBS();
                }
                catch (Exception ex)
                {
                    AddOperationLog(Severity.错误.ToString(), "通讯", "", "通讯启动错误！");
                    Logger.GetInstance().LogError(ex.ToString());

                }
                ServerConfig.RunningState = ServerStateEnum.RUNING;
                Thread.Sleep(1000);
            }
            publishDevState();
        }

        private void Var_OnPropertyChange(BaseVariable sender, string propName, object value, int valueType)
        {
            //throw new NotImplementedException();
        }

        public void DisConnectedDrive()
        {
            lock (runLock)
            {
                if (ServerConfig.RunningState != ServerStateEnum.RUNING) return;
                ServerConfig.RunningState = ServerStateEnum.BUSYING;
                //if (mqttController != null)
                //    mqttController.PublishIOServerState();
                StopVBS();
                //recordController.Stop();
                taskController.Stop();
                autoResetController.Stop();
                pushMsgController.Stop();
                mailController.Stop();
                if (modbusSlaveController != null)
                    modbusSlaveController.Stop();
                _CloudController.OnCommMsg -= new CommMsgDelegate(ShowCommMsg);
                _CloudController.Stop();
                if (recordController != null)
                    recordController.Stop();

                //if(ServerConfig.MqttEnable&&mqttController!=null)
                //     mqttController.Stop();

                try
                {
                    if (VarEventVBAEngine != null)
                        VarEventVBAEngine.Close();
                    if (GlobalVBAEngine != null)
                        GlobalVBAEngine.Close();
                }
                catch { }
                foreach (IChannel ch in Rtdb.ChanList)
                {
                    ch.Active = false;
                    ch.StartState = false;
                    foreach (IController con in ch.ConList)
                    {
                        con.Active = false;
                        foreach (IVariable var in con.VarList)
                        {
                            var.Active = false;
                            var.Counter = 0;
                            var.OnValueChange -= new OnValueChangeDelegate(var_OnValueChange);
                            var.OnWriteValueCallback -= new OnWriteValueCallbackDelegate(var_OnWriteValueCallback);
                            var.OnPropertyChange -= new OnPropertyChangeDelegate(var_OnProrertyChange);
                        }
                    }
                }
                foreach (BaseChannel chan in Rtdb.ChanList)
                {
                    try
                    {
                        chan.RunTime = 0;

                        if (chan.Enable)
                        {
                            Logger.GetInstance().LogMsg("开始停止驱动：" + chan.Name);
                            chan.Stop();

                            Logger.GetInstance().LogMsg("完成停止驱动：" + chan.Name);

                        }
                    }
                    catch { }
                    chan.OnCommMsg -= new CommMsgDelegate(ShowCommMsg);
                }
      
                ServerConfig.RunningState = ServerStateEnum.STOPED;

            }
            publishDevState();
        }

        #endregion

        #region 通信信息处理
        public void CmdLoadConfig(string filename, string url)
        {

            try
            {

                if (ProjectMng.LoadFromXml(url))
                {

                    AddOperationLog(Severity.信息.ToString(), StrConst.TITLE_SYS, "", "远程载入文件成功！文件名：data.xml");
                    //Thread.Sleep(5000);
                    ProjectMng.SaveToXml(ServerConfig.APP_PATH + "data.xml");
                    ServerConfig.ProjectPath = ServerConfig.APP_PATH + "data.xml";
                    UpdateTree(null, ServerConfig.ProjectPath, NodeStateEnum.None);
                    //ProjectMng.SaveToXml(ServerConfig.ProjectPath);
                }
                else
                {

                    AddOperationLog(Severity.信息.ToString(), StrConst.TITLE_SYS, "", "远程载入文件失败：" + filename);
                }

            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
                string msg = string.Format("加载数据文件出错！数据文件：{0}", url);
                AddOperationLog(StrConst.SERVERITY_ERR, StrConst.TITLE_SYS, "", msg);
            }

        }




        /// <summary>
        /// 通讯插件上报的通讯信息
        /// </summary>
        /// <param name="sender">信息发生的对象</param>
        /// <param name="severity">紧急程序</param>
        /// <param name="CommunicationEvent">信息类型</param>
        /// <param name="wParamm">参数1</param>
        /// <param name="lParamm">参数2</param>
        void ShowCommMsg(object sender, Severity severity, CommunicationEvent commMsgType, string wParamm, string lParamm)
        {
            try
            {
                //通讯
                if (commMsgType == CommunicationEvent.COMM_ALARM)
                {
                    //2017年2月24日新增，设备驱动中可直接发送报警到云端
                    if (wParamm == StrConst.ALARM_EVENT_DEV)
                    {
                        AlarmMessage newAlarm = GHCore.Serialization.JSONFormatter.Deserialize<AlarmMessage>(lParamm);
                        Almdb.InsertAlarm(newAlarm);
                    }
                    else
                    {
                        AlarmMessage newAlarm = new AlarmMessage();
                        newAlarm.AlarmVariable = "";
                        newAlarm.DateStamp = DateTime.Now;
                        newAlarm.AlarmWay = wParamm;
                        newAlarm.AlarmDesc = lParamm;
                        newAlarm.AlarmValue = "1";

                        //S_OnAlarm(newAlarm);
                        AddOperationLog(severity.ToString(), "通讯报警", wParamm, lParamm);
                    }

                }
                else if (commMsgType == CommunicationEvent.COMM_UPDATE_TREE)
                {
                    UpdateTree(sender, wParamm, NodeStateEnum.ReName);
                }
                //发送信息到客户端
                else if (commMsgType == CommunicationEvent.COMM_CLIENT)
                {
                    //netService.SendEventMsg(wParamm, "通讯事件", lParamm);
                    //AddOperationLog(severity.ToString(), "通讯", wParamm, lParamm);
                }
                else if (commMsgType == CommunicationEvent.COMM_INFO)
                {
                    if (lParamm != "")
                        AddOperationLog(severity.ToString(), "通讯信息", wParamm, lParamm);
                }
                else if (sender is IChannel)
                {
                    IChannel ch = sender as IChannel;
                    switch (commMsgType)
                    {
                        case CommunicationEvent.COMM_ONLINE:
                            UpdateTree(sender, wParamm, NodeStateEnum.OnLine);
                            if (lParamm != "")
                                AddOperationLog(severity.ToString(), "通讯事件", "系统用户", lParamm);
                            UpdataCloudDeviceState(ch, wParamm);
                            break;
                        case CommunicationEvent.COMM_OFFLINE:
                            UpdateTree(sender, wParamm, NodeStateEnum.OffLine);
                            if (lParamm != "")
                                AddOperationLog(severity.ToString(), "通讯事件", "系统用户", lParamm);
                            UpdataCloudDeviceState(ch, wParamm);
                            break;
                        case CommunicationEvent.COMM_ENABLE:
                            UpdateTree(sender, wParamm, NodeStateEnum.Enable);
                            if (lParamm != "")
                                AddOperationLog(severity.ToString(), "通讯事件", "系统用户", lParamm);
                            break;
                        case CommunicationEvent.COMM_DISABLE:
                            UpdateTree(sender, wParamm, NodeStateEnum.Disable);
                            if (lParamm != "")
                                AddOperationLog(severity.ToString(), "通讯事件", "系统用户", lParamm);
                            break;
                        default:
                            if (lParamm != "")
                                AddOperationLog(severity.ToString(), "通讯事件", "系统用户", lParamm);
                            break;
                    }
                }
                else
                {
                    if (lParamm != "")
                        AddOperationLog(severity.ToString(), "通讯", wParamm, lParamm);
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }

        }
        public static uint _RequestID = 0;
       
        public void publishDevState()
        {
            try
            {
                if (MqttController.IsConnected() == false) return;
           


                JObject jo = new JObject { { "Name", ServerConfig.CloudClientID },
                                                       { "Propery", "State" },
                                                       { "Value", ServerConfig.RunningState.ToString() },
                                                       { "ValueType", 3 },
                                                       { "Timestamp", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") },
                                                       { "IOServerID",ServerConfig.CloudClientID }
                                                      };
                JObject jo2 = new JObject { { "Name", ServerConfig.CloudClientID },
                                                       { "Propery", "OnlineState" },
                                                       { "Value", "true" },
                                                       { "ValueType", 3 },
                                                       { "Timestamp", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") },
                                                       { "IOServerID",ServerConfig.CloudClientID }
                                                      };
                JObject jo3 = new JObject { { "Name", ServerConfig.CloudClientID },
                                                       { "Propery", "Active" },
                                                       { "Value", "true" },
                                                       { "ValueType", 3 },
                                                       { "Timestamp", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") },
                                                       { "IOServerID",ServerConfig.CloudClientID }
                                                      };
                JArray ja = new JArray();
                ja.Add(jo);
                ja.Add(jo2);
                ja.Add(jo3);

                if (ServerConfig.MqttVer == 1)
                {
                    GHMNSResponse res = new GHMNSResponse();
                    res.success = true;
                    res.HostId = Dns.GetHostName();
                    res.Message = "采集器状态变化推送";
                    res.ClientId = ServerConfig.CloudClientID;
                    res.RequestID = _RequestID++.ToString();
                    res.data = ja;
                    MqttController.PublishIoPropChanged(JsonConvert.SerializeObject(res));
                }
                else
                {
                    GHIOTMsg msg = new GHIOTMsg
                    {
                        msgId=IdWorker.GetId(),
                        msgTime=IdWorker.GetTime(),
                        key= ServerConfig.CloudClientID,
                        data=ja

                    };
                    MqttController.PublishIoPropChanged(JsonConvert.SerializeObject(msg));

                }


                foreach (IChannel chan in Rtdb.ChanList)
                {
                 

                    jo = new JObject { { "Name", chan.Name },
                                                       { "Propery", "Active" },
                                                       { "Value", chan.Active },
                                                       { "ValueType", 3 },
                                                       { "Timestamp", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") },
                                                       { "IOServerID",ServerConfig.CloudClientID },
                                                       { "ChlID", chan.ID}
                                                     };
                    ja = new JArray();
                    ja.Add(jo);
                    if(ServerConfig.MqttVer==1)
                    {
                        GHMNSResponse res = new GHMNSResponse();
                        res.success = true;
                        res.HostId = Dns.GetHostName();
                        res.Message = "通道状态变化推送";
                        res.ClientId = ServerConfig.CloudClientID;
                        res.RequestID = _RequestID++.ToString();
                        res.data = ja;
                        MqttController.PublishChlPropChanged(JsonConvert.SerializeObject(res));

                    }else
                    {
                        GHIOTMsg msg = new GHIOTMsg
                        {
                            msgId = IdWorker.GetId(),
                            msgTime = IdWorker.GetTime(),
                            key = ServerConfig.CloudClientID,
                            data = ja

                        };
                        MqttController.PublishChlPropChanged(JsonConvert.SerializeObject(msg));

                    }
                
                    foreach (IController con in chan.ConList)
                    {
                      
                        jo = new JObject { { "Name", chan.Name },
                                                           { "Propery", "Active" },
                                                           { "Value", con.Active },
                                                           { "ValueType", 3 },
                                                           { "Timestamp", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") },
                                                           { "IOServerID",ServerConfig.CloudClientID },
                                                           { "ChlID", chan.ID},
                                                           { "CtrlID", con.ID}

                                                        };
                        ja = new JArray();
                        ja.Add(jo);

                        if (ServerConfig.MqttVer == 1)
                        {
                            GHMNSResponse res = new GHMNSResponse();
                            res.success = true;
                            res.HostId = Dns.GetHostName();
                            res.Message = "控制器状态变化推送";
                            res.ClientId = ServerConfig.CloudClientID;
                            res.RequestID = _RequestID++.ToString();
                            res.data = ja;
                            MqttController.PublishCtrlPropChanged(JsonConvert.SerializeObject(res));
                        }else
                        {
                            GHIOTMsg msg = new GHIOTMsg
                            {
                                msgId = IdWorker.GetId(),
                                msgTime = IdWorker.GetTime(),
                                key = ServerConfig.CloudClientID,
                                data = ja

                            };
                            MqttController.PublishCtrlPropChanged(JsonConvert.SerializeObject(msg));

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogMsg("publishDevState出错" + ex.ToString());
            }
            // SendCommEvent(Severity.信息, CommunicationEvent.COMM_INFO, ServerConfig.CloudClientID, "定时同步采集器状态成功！");
            //Logger.GetInstance().LogMsg("定时同步采集器状态成功");

        }
        public void UpdataCloudDeviceState(IChannel ch, string devName)
        {
            if (ch.Name == devName)
            {
                if (ServerConfig.MqttEnable)
                {
               
                    JObject jo = new JObject { { "Name", ch.Name },
                                                { "Propery", "Active" },
                                                { "Value", ch.Active },
                                                { "ValueType", 3 },
                                                { "Timestamp", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") },
                                                { "IOServerID",ServerConfig.CloudClientID },
                                                { "ChlID", ch.ID}
                                                };
                    JArray ja = new JArray();
                    ja.Add(jo);
                    if (ServerConfig.MqttVer == 1)
                    {
                        GHMNSResponse res = new GHMNSResponse();
                        res.success = true;
                        res.HostId = Dns.GetHostName();
                        res.ClientId = ServerConfig.CloudClientID;
                        res.RequestID = CloudController._RequestID++.ToString();
                        res.Message = "通道状态变化推送";
                        res.data = ja;
                        MqttController.PublishChlPropChanged(JsonConvert.SerializeObject(res));
                    }else
                    {
                        GHIOTMsg msg = new GHIOTMsg
                        {
                            msgId = IdWorker.GetId(),
                            msgTime = IdWorker.GetTime(),
                            data = ja,
                            key = ServerConfig.CloudClientID
                        };
                        MqttController.PublishChlPropChanged(JsonConvert.SerializeObject(msg));
                    }
                    string deviceId = ServerConfig.CloudClientID + ":" + ch.ID;
                    string deviceName = ServerConfig.CloudClientID + "/" + ch.Name;
                    string deviceType = "通道";
                    string eventType =ch.Active? "上线":"离线";
                    string eventDesc = "通讯事件";
                    //上报通讯事件
                    MqttController.PublishCommEvent(deviceId, deviceName, deviceType, eventType, eventDesc);

                }
            }
            else
            {
                foreach (IController con in ch.ConList)
                {
                    if (con.Name == devName)
                    {
                     


                        if (ServerConfig.MqttEnable)
                        {  
                          
                            JObject jo = new JObject { { "Name", con.Name },
                                                           { "Propery", "Active" },
                                                           { "Value", con.Active },
                                                           { "ValueType", 3 },
                                                           { "Timestamp", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") },
                                                           { "IOServerID",ServerConfig.CloudClientID },
                                                           { "ChlID", ch.ID},
                                                           { "CtrlID", con.ID}

                                                            };
                            JArray ja = new JArray();
                            ja.Add(jo);

                            if (ServerConfig.MqttVer == 1)
                            {
                                GHMNSResponse res = new GHMNSResponse();
                                res.success = true;
                                res.HostId = Dns.GetHostName();
                                res.ClientId = ServerConfig.CloudClientID;
                                res.RequestID = CloudController._RequestID++.ToString();
                                res.Message = "控制器状态变化推送";
                                res.data = ja;
                                MqttController.PublishCtrlPropChanged(JsonConvert.SerializeObject(res));
                            }else
                            {
                                GHIOTMsg msg = new GHIOTMsg
                                {
                                    msgId = IdWorker.GetId(),
                                    msgTime = IdWorker.GetTime(),
                                    data = ja,
                                    key = ServerConfig.CloudClientID
                                };
                                MqttController.PublishChlPropChanged(JsonConvert.SerializeObject(msg));

                            }
                            string deviceId = ServerConfig.CloudClientID + ":" + ch.ID+":"+con.ID;
                            string deviceName = ServerConfig.CloudClientID + "/" + ch.Name+"/"+con.Name;
                            string deviceType = "控制器";
                            string eventType = con.Active ? "上线" : "离线";
                            string eventDesc = "通讯事件";
                            //上报通讯事件
                            MqttController.PublishCommEvent(deviceId, deviceName, deviceType, eventType, eventDesc);

                        }
                    }
                }
            }
        }



        #endregion


        #region 通用方法


        #endregion
        #endregion

        #region Formmain.DrvSvr
        #region 报警事件判定
        /// <summary>
        /// 判断变量是否触发报警事件
        /// </summary>
        /// <param name="var"></param>
        private void VarOnEventTrigger(IVariable var)
        {
            //Console.WriteLine(Thread.CurrentThread);
            if (var.WayList.Count > 0)
            {
                try
                {
                    //自定义事件条件判断，同时判断自定义条件是不包括了数值：1的默认报警
                    foreach (VariableTrigger way in var.WayList)
                    {
                        //时限检查 为空时不检查时限
                        if (!string.IsNullOrEmpty(way.TimZone))
                        {
                            //根据配置的名称，查找时限对象
                            TimeZoneClt timeZone = TimeZoneDb.GetTimeZonebyName(way.TimZone);
                            if (timeZone != null)
                            {
                                //如果不在时限内，直接跳过事件联动
                                if (!timeZone.IsActive())
                                {
                                    continue;
                                }
                            }
                        }

                        //数值改变事件检查
                        if (way.Express == "数值改变")
                        {
                            //报警联动
                            VarEventActionHandle(new AlarmMessage(var, way), var);
                            //执行脚本
                            if (!string.IsNullOrEmpty(way.ScriptText.Trim()))
                                Var_ActionScript(var, way.ScriptText);
                        }
                        else
                        {
                            #region 超限检查
                            string[] arr = way.Express.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string s in arr)
                            {
                                string[] s1 = s.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                                if (s1.Length == 2)
                                {
                                    //超过上限检查
                                    if (s1[0] == "上限")
                                    {
                                        if (pubFun.IsDouble(var.Value.ToString(), -1) >= pubFun.IsDouble(s1[1], 0))
                                        {

                                            if (string.IsNullOrEmpty(way.EventDesc))
                                                way.EventDesc = "超上限报警";
                                            //报警联动
                                            VarEventActionHandle(new AlarmMessage(var, way), var);
                                            //执行脚本
                                            if (!string.IsNullOrEmpty(way.ScriptText.Trim()))
                                                Var_ActionScript(var, way.ScriptText);
                                        }
                                    }
                                    //超过下限检查
                                    else if (s1[0] == "下限")
                                    {
                                        if (pubFun.IsDouble(var.Value.ToString(), 0) <= pubFun.IsDouble(s1[1], -1))
                                        {
                                            if (string.IsNullOrEmpty(way.EventDesc))
                                                way.EventDesc = "超下限报警";
                                            //报警联动
                                            VarEventActionHandle(new AlarmMessage(var, way), var);
                                            //执行脚本
                                            if (!string.IsNullOrEmpty(way.ScriptText.Trim()))
                                                Var_ActionScript(var, way.ScriptText);

                                        }
                                    }
                                    //数值相等检查
                                    else if (s1[0] == "数值")
                                    {
                                        if (pubFun.IsInt(var.Value.ToString(), 0) == pubFun.IsInt(s1[1], -1))
                                        {
                                            if (string.IsNullOrEmpty(way.EventDesc))
                                                way.EventDesc = "自定义报警";
                                            //报警联动
                                            VarEventActionHandle(new AlarmMessage(var, way), var);
                                            //执行脚本
                                            if (!string.IsNullOrEmpty(way.ScriptText.Trim()))
                                                Var_ActionScript(var, way.ScriptText);

                                        }
                                    }
                                    //数值不等检查
                                    else if (s1[0] == "不等")
                                    {
                                        //披克门禁OPC 去除没有监控的误报
                                        if (var.Value.ToString().Trim() != "没有监控")
                                        {
                                            if (s1[1] == "空")
                                            {
                                                if (!string.IsNullOrEmpty(var.Value.ToString()))
                                                {
                                                    if (string.IsNullOrEmpty(way.EventDesc))
                                                        way.EventDesc = "自定义报警";
                                                    //报警联动
                                                    VarEventActionHandle(new AlarmMessage(var, way), var);
                                                    //执行脚本
                                                    if (!string.IsNullOrEmpty(way.ScriptText.Trim()))
                                                        Var_ActionScript(var, way.ScriptText);
                                                }
                                            }
                                            else if (pubFun.IsInt(var.Value.ToString(), -1) != pubFun.IsInt(s1[1], 0))
                                            {
                                                if (string.IsNullOrEmpty(way.EventDesc))
                                                    way.EventDesc = "自定义报警";
                                                //报警联动
                                                VarEventActionHandle(new AlarmMessage(var, way), var);
                                                //执行脚本
                                                if (!string.IsNullOrEmpty(way.ScriptText.Trim()))
                                                    Var_ActionScript(var, way.ScriptText);
                                            }
                                        }

                                    }

                                }

                            }
                            #endregion
                        }

                    } //end foreach
                }
                catch (Exception ex)
                {
                    Logger.GetInstance().LogError(ex.ToString());
                }
            }
            //所有报警需要通过自定义报警条件
            //自定义报警中定义了状态1报警则不再处理缺省报警
            //缺省报警仅用于变量的DeviceLabel为报警输入变量和通讯报警变量
            /*else
            {
                if (var.DeviceLabel == DeviceLabelEnum.通讯报警.ToString())
                {
                    if (var.Value != null && var.Value.ToString() != "" && var.Value.ToString() == "1")
                    {
                        //创建一个通讯报警的触发器
                        string Express = "数值:1";
                        string EventDesc = "通讯报警";
                        int EventType = (int)EventTypeEnum.故障报警;
                        uint Priority = 2;
                        string ScriptText = "";
                        string timezone = "";
                        VariableTrigger way = new VariableTrigger(Express,
                                   EventType,
                                   EventDesc,
                                   Priority,
                                   ScriptText,
                                   timezone);

                        VarEventActionHandle(new AlarmMessage(var, way), var);
                    }
                }
                else if (var is IAlarmVariable && var.DeviceLabel == (DeviceLabelEnum.报警输入.ToString()))
                {
                    //处理报警 1
                    if (var.Value != null && var.Value.ToString() != "" && var.Value.ToString() == "1")
                    {

                        string Express = "数值:1";
                        string EventDesc = "普通报警";
                        int EventType = (int)EventTypeEnum.普通报警;
                        uint Priority = 3;
                        string ScriptText = "";
                        string timezone = "";
                        VariableTrigger way = new VariableTrigger(Express,
                                   EventType,
                                   EventDesc,
                                   Priority,
                                   ScriptText,
                                   timezone);


                        VarEventActionHandle(new AlarmMessage(var, way), var);

                    }

                }
            }*/
        }

        /// <summary>
        /// 2.0
        /// </summary>
        /// <param name="var"></param>
        private void VarOnEventTriggerV2(IVariable var)
        {
            double value = pubFun.IsDouble(var.Value.ToString(), -1);

            if (var.AlarmHHEnable)
            {
                BaseVariable baseVar = var as BaseVariable;
                if (value >= var.AlarmHH && (value - var.AlarmHH) > var.AlarmHHFloat)
                {
                    String AlarmDesc =  "超高限报警";
                    uint alarmLevel = (uint)var.AlarmLevelHH; //报警级别
                    VariableTrigger trigger = new VariableTrigger("HH", (int)var.AlarmSource, AlarmDesc, alarmLevel, "", "");

                    AlarmMessage msg = new AlarmMessage(var, trigger);
                    msg.AlarmLevelValue = var.AlarmHH;
                    msg.EventType = 1;

                    if (var.AlarmHHdelay > 0)
                        VarEventActionHandle_V2(msg, var, true);
                    else
                        VarEventActionHandle_V2(msg, var, false);
                }
            }

            if (var.AlarmHEnable)
            {
                if (value >= var.AlarmH && (value - var.AlarmH) > var.AlarmHFloat)
                {
                    String AlarmDesc =  "高限报警";
                    uint alarmLevel = (uint)var.AlarmLevelH; //报警级别
                    VariableTrigger trigger = new VariableTrigger("H", (int)var.AlarmSource, AlarmDesc, alarmLevel, "", "");

                    AlarmMessage msg = new AlarmMessage(var, trigger);
                    msg.AlarmLevelValue = var.AlarmH;
                    msg.EventType = 1;
                    if (var.AlarmHdelay > 0)
                        VarEventActionHandle_V2(msg, var, true);
                    else
                        VarEventActionHandle_V2(msg, var, false);

                }

            }

            if (var.AlarmLEnable)
            {
                if (value <= var.AlarmL && (var.AlarmL - value) > var.AlarmLFloat)
                {
                    String AlarmDesc =  "低限报警";
                    uint alarmLevel = (uint)var.AlarmLevelL; //报警级别
                    VariableTrigger trigger = new VariableTrigger("L", (int)var.AlarmSource, AlarmDesc, alarmLevel, "", "");

                    AlarmMessage msg = new AlarmMessage(var, trigger);
                    msg.AlarmLevelValue = var.AlarmL;
                    msg.EventType = 1;
                    if (var.AlarmLdelay > 0)
                        VarEventActionHandle_V2(msg, var, true);
                    else
                        VarEventActionHandle_V2(msg, var, false);
                }
            }

            if (var.AlarmLLEnable)
            {
                if (value <= var.AlarmLL && (var.AlarmLL - value) > var.AlarmLLFloat)
                {
                    String AlarmDesc = "超低限报警";
                    uint alarmLevel = (uint)var.AlarmLevelLL; //报警级别
                    VariableTrigger trigger = new VariableTrigger("LL", (int)var.AlarmSource, AlarmDesc, alarmLevel, "", "");
                    AlarmMessage msg = new AlarmMessage(var, trigger);
                    msg.EventType = 1;
                    msg.AlarmLevelValue = var.AlarmLL;
                    if (var.AlarmLLdelay > 0)
                        VarEventActionHandle_V2(msg, var, true);
                    else
                        VarEventActionHandle_V2(msg, var, false);
                }
            }

            if (var.AlarmST0)
            {
                if (value == 0)
                {
                    String AlarmDesc =  "状态0报警";
                    uint alarmLevel = (uint)var.AlarmLevelST; //报警级别
                    VariableTrigger trigger = new VariableTrigger("ST0", (int)var.AlarmSource, AlarmDesc, alarmLevel, "", "");
                    AlarmMessage msg = new AlarmMessage(var, trigger);
                    msg.EventType = 1;
                    VarEventActionHandle_V2(msg, var, false);
                }
            }
            if (var.AlarmST1)
            {
                if (value == 1)
                {
                    String AlarmDesc =  "状态1报警";
                    uint alarmLevel = (uint)var.AlarmLevelST; //报警级别
                    VariableTrigger trigger = new VariableTrigger("ST1", (int)var.AlarmSource, AlarmDesc, alarmLevel, "", "");
                    AlarmMessage msg = new AlarmMessage(var, trigger);
                    msg.EventType = 1;
                    VarEventActionHandle_V2(msg, var, false);
                }
            }
            if (var.AlarmST2)
            {
                if (value == 2)
                {
                    String AlarmDesc =  "状态2报警";
                    uint alarmLevel = (uint)var.AlarmLevelST; //报警级别
                    VariableTrigger trigger = new VariableTrigger("ST2", (int)var.AlarmSource, AlarmDesc, alarmLevel, "", "");
                    AlarmMessage msg = new AlarmMessage(var, trigger);
                    msg.EventType = 1;
                    VarEventActionHandle_V2(msg, var, false);
                }
            }
            if (var.AlarmST3)
            {
                if (value == 3)
                {
                    String AlarmDesc =  "状态3报警";
                    uint alarmLevel = (uint)var.AlarmLevelST; //报警级别
                    VariableTrigger trigger = new VariableTrigger("ST3", (int)var.AlarmSource, AlarmDesc, alarmLevel, "", "");
                    AlarmMessage msg = new AlarmMessage(var, trigger);
                    msg.EventType = 1;
                    VarEventActionHandle_V2(msg, var, false);
                }
            }
        }

        #endregion

        #region 报警事件处理
        /*
         *   报警处理机制
         *   1、每次设备收到数据，更新内存变量内都做报警检查，无论数据是否变化；
         *   2、定义一个报警内存链表，报警检查结果如果存在报警则和报警内存链表比较
         *      比较变量名称、报警条件，时间，如果三者相同则不再加入报警链表，以及发送给客户端，
         *      如果变量名称、报警条件相同则为同一个报警，如果时间差ALARM_REPEAT_TIME内不再重发
         */
        //存在线程内执行 
        /// <summary>
        /// 报警事件处理
        /// </summary>
        /// <param name="newAlarm">报警事件消息</param>
        /// <param name="var">报警变量</param>
        private void VarEventActionHandle(AlarmMessage newAlarm, IVariable var)
        {
            try
            {
                //if (newAlarm.ExpressType == (int)EventTypeEnum.报警事件)
                if (newAlarm.EventType > 0) //2016,612 修改，除了普通事件外，其他事件都作为报警处理
                {

                    Almdb.InsertAlarm(newAlarm);

                   // string msg = "变量:" + newAlarm.AlarmVariable +" 报警信息：" +newAlarm.AlarmDesc + "  " + newAlarm.AlarmWay;
                    AddOperationLog(Severity.警告.ToString(), StrConst.TITLE_SYS, "变量报警", newAlarm.AlarmVariableDesc);

                }
                //已经采用异步处理了
                Var_SMSLink(var, newAlarm);
                //处理事件联动 新事件后处理，在后面的设备驱动中需要采用异步处理
                //Var_VideoLink(var, newAlarm);
                //每次事件发生处理，在后面的设备驱动中需要采用异步处理
                Var_ActionLink(var, newAlarm);
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }
        }

        private void VarEventActionHandle_V2(AlarmMessage newAlarm, IVariable var, bool bDelay)
        {
            try
            {
                //if (newAlarm.ExpressType == (int)EventTypeEnum.报警事件)
                if (newAlarm.EventType > 0) //2016,612 修改，除了普通事件外，其他事件都作为报警处理
                {
                    if (bDelay)
                        Almdb.InsertDelayAms(newAlarm);
                    else
                        Almdb.InsertAlarm(newAlarm);


                    // GHLogger.Log("当前位置:插入延时报警，时间：" + DateTime.Now.ToString() + "报警值：" + newAlarm.AlarmValue, LogCategory.Debug);
                    //DebugLogger.WriteLog("当前位置:插入延时报警，时间：" + DateTime.Now.ToString() + "报警值：" + newAlarm.AlarmValue);
                    //string msg = "";
                    //if (bDelay)
                    //    msg = "变量延时报警:" + newAlarm.AlarmVariable + newAlarm.AlarmDesc + "  " + newAlarm.AlarmWay;
                    //else
                    //    msg = "变量报警:" + newAlarm.AlarmVariable + newAlarm.AlarmDesc + "  " + newAlarm.AlarmWay;

                    AddOperationLog(Severity.警告.ToString(), StrConst.TITLE_SYS, "变量报警", newAlarm.AlarmVariableDesc);


                }
                //已经采用异步处理了
                Var_SMSLink(var, newAlarm);
                //处理事件联动 新事件后处理，在后面的设备驱动中需要采用异步处理
                //Var_VideoLink(var, newAlarm);
                //每次事件发生处理，在后面的设备驱动中需要采用异步处理
                Var_ActionLink_V2(var, newAlarm);
                //报警云端记录变化
                MqttController.addPubVar(var, true);
                //本地报警
                if(var.AlarmRecorderEnable && !var.DataChangedRecorderEnable)//如果变化记录已经记录了，就不再重复写入连续数据库了
                {
                    History his = new History
                    {
                        time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                        server = ServerConfig.CloudClientID,
                        channel = var.ControllerObject.ChannelObject.ID,
                        controller = var.ControllerObject.ID,
                        varKey = var.ID,
                        value = var.Value.ToString()

                    };
                    RecordController.VariableChangedRecord(his);
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }
        }
        private void Var_ActionScript(IVariable var, string scriptText)
        {
            VarEventVBAEngine.ScriptText = scriptText;
            string[] scriptMethodNames = VarEventVBAEngine.ScriptMethodNames;
            if ((scriptMethodNames != null) && (scriptMethodNames.Length > 0))
            {
                try
                {
                    VarEventVBAEngine.ExecuteSub(scriptMethodNames[0]);
                }
                catch (Exception exception)
                {
                    string msg = "执行脚本 " + scriptMethodNames[0] + " 变量：" + var.Name + "错误:" + exception.Message;
                    AddOperationLog(StrConst.SERVERITY_MSG, StrConst.TITLE_SYS, "", msg);
                }
            }
        }
        private void Var_ActionLink_V2(IVariable var, AlarmMessage newAlarm)
        {
            try
            {
                if (var.Action1 != "")
                {
                    string id = var.Action1.Split('|')[0];
                    string value = var.Action1.Split('|')[1];
                    string io = id.Split(':')[0];
                    string ch = id.Split(':')[1];
                    string con = id.Split(':')[2];
                    string varID = id.Split(':')[3];

                    if (ServerConfig.CloudClientID == io)
                    {
                        IChannel cha = Rtdb.GetChannelByID(ch);
                        if (cha != null)
                        {
                            foreach (IController ctrl in cha.ConList)
                            {
                                if (ctrl.ID == con)
                                    foreach (IVariable v in ctrl.VarList)
                                    {
                                        if (v.ID == varID)
                                        {
                                            v.WriteValue(value);
                                            break;
                                        }
                                    }
                            }

                        }

                    }
                    else
                    {
                        string upAction = $"down_write_{io.ToLower()}";

                        JObject js = new JObject();
                        js.Add(new JProperty("IOSvrKey", io));
                        js.Add(new JProperty("ChlKey", ch));
                        js.Add(new JProperty("CtrlKey", con));
                        js.Add(new JProperty("VarKey", varID));
                        js.Add(new JProperty("Value", value));
                        js.Add(new JProperty("Level", 0));
                        js.Add(new JProperty("Area", ""));
                        string s = JsonConvert.SerializeObject(js);

                        //if (ServerConfig.EnableRedis)
                        //      DeviceManagement.SingletonInstance.Publish(upAction, s);

                    }
                }

                if (var.Action2 != "")
                {
                    string id = var.Action2.Split('|')[0];
                    string value = var.Action2.Split('|')[1];
                    string io = id.Split(':')[0];
                    string ch = id.Split(':')[1];
                    string con = id.Split(':')[2];
                    string varID = id.Split(':')[3];

                    if (ServerConfig.CloudClientID == io)
                    {
                        IChannel cha = Rtdb.GetChannelByID(io);
                        if (cha != null)
                        {
                            foreach (IController ctrl in cha.ConList)
                            {
                                if (ctrl.ID == con)
                                    foreach (IVariable v in ctrl.VarList)
                                    {
                                        if (v.ID == varID)
                                        {
                                            v.WriteValue(value);
                                        }
                                    }
                            }

                        }

                    }
                    else
                    {
                        string upAction = $"down_write_{io.ToLower()}";

                        JObject js = new JObject();
                        js.Add(new JProperty("IOSvrKey", io));
                        js.Add(new JProperty("ChlKey", ch));
                        js.Add(new JProperty("CtrlKey", con));
                        js.Add(new JProperty("VarKey", varID));
                        js.Add(new JProperty("Value", value));
                        js.Add(new JProperty("Level", 0));
                        js.Add(new JProperty("Area", ""));
                        string s = JsonConvert.SerializeObject(js);
                        //if(ServerConfig.EnableRedis)
                        //     DeviceManagement.SingletonInstance.Publish(upAction, s);


                    }
                }
                // Write2Device(alarmAction.VarName, alarmAction.VarValue.ToString());
                //IVariable var = Rtdb.GetVariableByID(alarmAction.VarTarget.Split(':')[0], alarmAction.VarTarget.Split(':')[1], alarmAction.VarTarget.Split(':')[2]);
                //if (var != null)
                //    var.WriteValue(alarmAction.VarValue);
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }

        }
        private void Var_ActionLink(IVariable var, AlarmMessage newAlarm)
        {
            string way = newAlarm.AlarmWay;
            foreach (VariableAction act in var.ActionList)
            {
                if (act.Way == way)
                    var_OnAlarmAction(var, act);
            }
        }
        //private void Var_VideoLink(IVariable var, AlarmMessage newAlarm)
        //{
        //    string way = newAlarm.AlarmWay;
        //    foreach (VariableVideo vdo in var.VideoList)
        //    {

        //        if (vdo.Way == way)
        //            var_OnAlarmVideoLink(var, vdo);
        //    }

        //}
        private void Var_SMSLink(IVariable var, AlarmMessage newAlarm)
        {
            string way = newAlarm.AlarmWay;
            foreach (VariableSMS sms in var.SmsList)
            {
                if (sms.Way == way)
                    var_OnAlarmSms(var, sms);
            }
        }

        private void var_OnAlarmAction(IVariable sender, VariableAction alarmAction)
        {
            try
            {
                // Write2Device(alarmAction.VarName, alarmAction.VarValue.ToString());
                IVariable var = Rtdb.GetVariableByID(alarmAction.VarTarget.Split(':')[0], alarmAction.VarTarget.Split(':')[1], alarmAction.VarTarget.Split(':')[2]);
                if (var != null)
                    var.WriteValue(alarmAction.VarValue);
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }

        }
        ////报警视频联动
        //private void var_OnAlarmVideoLink(IVariable sender, VariableVideo videoLink)
        //{
        //    //切换视频
        //    if (videoLink.MonName.Trim() != "" && videoLink.CamID.Trim() != "")
        //    {
        //        ICamVariable cam = Rtdb.GetCamVariableByIDRun(videoLink.CamID);
        //        if (cam != null)
        //        {
        //            VideoCommandArgs args = new VideoCommandArgs();
        //            args.CamName = videoLink.CamID;
        //            args.MonName = videoLink.MonName;
        //            args.SubVideoOut = videoLink.SubVideoOut;
        //            if (cam is IDvrCamVariable)
        //                args.VideoIn = ((IDvrCamVariable)cam).MatrixInchannel;
        //            args.VideoCommand = PTZCmdCodeEnum.MAT_VIDEO_SW;
        //            //netService.dataAnalysis.C_VideoControl(args);
        //            string msg = ("报警联动视频： 矩阵：" + videoLink.MatrixName + "  监视器：" + videoLink.MonName
        //      + "摄像机：" + videoLink.CamID + " 预置位：" + videoLink.PreSetID.ToString());

        //            AddOperationLog(StrConst.SERVERITY_MSG, StrConst.TITLE_OPER, "", msg);
        //        }
        //    }
        //    else
        //    {
        //        this.AddOperationLog(Severity.信息.ToString(), "", "", "联动关系缺少摄像机名称或监控器名称视频联动没有被执行！");
        //    }
        //    //切换预置位
        //    if (videoLink.CamID.Trim() != "" && videoLink.PreSetID != 0)
        //    {
        //        // PTZPreset(videoLink.MatrixID, videoLink.CamID, videoLink.CamID,(int)PtzPresetEnum.调用);
        //        ICamVariable cam = Rtdb.GetCamVariableByIDRun(videoLink.CamID);
        //        if (cam != null)
        //        {
        //            VideoCommandArgs args = new VideoCommandArgs();
        //            args.CamName = videoLink.CamID;
        //            args.MonName = videoLink.MonName;
        //            args.SubVideoOut = videoLink.SubVideoOut;
        //            if (cam is IDvrCamVariable)
        //                args.VideoIn = ((IDvrCamVariable)cam).MatrixInchannel;
        //            args.PresetIndex = (uint)videoLink.PreSetID;
        //            args.VideoCommand = PTZCmdCodeEnum.GOTO_PRESET;
        //            //netService.dataAnalysis.C_VideoControl(args);
        //            string msg = ("报警联动视频： 矩阵：" + videoLink.MatrixName + "  监视器：" + videoLink.MonName
        //      + "摄像机：" + videoLink.CamID + " 预置位：" + videoLink.PreSetID.ToString());

        //            AddOperationLog(StrConst.SERVERITY_MSG, StrConst.TITLE_OPER, "", msg);
        //        }

        //    }
        //    else
        //    {
        //        //this.AddOperationLog(Severity.信息.ToString(), "", "", "缺少矩阵名称、摄像机名称、监控器名称视频联动没有被执行！");
        //    }
        //}

        private void var_OnAlarmSms(IVariable sender, VariableSMS msg)
        {
            if (msg.Type == 0) //短信
            {
                SmsMsg sms = new SmsMsg();
                sms.Phone = msg.Phone;
                sms.Msg = msg.Msg;
                Smsdb.IntoSmsQueue(sms);
                string s = ("报警联动短信：号码：" + msg.Phone + "  内容：" + msg.Msg);
                AddOperationLog(StrConst.SERVERITY_MSG, StrConst.TITLE_SYS, "", s);
            }
            else if (msg.Type == 1) //固话
            {
                PSTNMsg p = new PSTNMsg();
                p.Phone = msg.Phone;
                p.Msg = msg.Msg;
                PSTNdb.IntoPSTNQueue(p);
                string s = ("报警电话拨号：号码：" + msg.Phone + "  语音文件名：" + msg.Msg);
                AddOperationLog(StrConst.SERVERITY_MSG, StrConst.TITLE_SYS, "", s);

            }
            else if (msg.Type == 2)//通知
            {
                pushMsgController.AddMessage(new PushMessage(msg.Phone, msg.Msg));
            }
            else if (msg.Type == 3)//邮件
            {
                mailController.AddMessage(new mailContent(msg.Phone, msg.Msg));
            }
        }

        #endregion

        #region 公式运算
        private object ExpressValue(string sExpress)
        {
            bool bok = express.Parse(sExpress);
            if (bok)
            {
                bool ok = false;
                object d = express.Evaluate(ref ok);
                if (ok)
                {
                    return d;
                }
            }
            return null;
        }
        #endregion

        #region 公式解析回调
        //变量传到这个回调函数里
        public bool express_OnAccidenceAnalysis(ref Operand opd)
        {
            foreach (BaseChannel chan in Rtdb.ChanList)
            {
                foreach (BaseController con in chan.ConList)
                {
                    foreach (BaseVariable var in con.VarList)
                    {  //变量名是value值
                        if (var.Name.Equals(opd.Value.ToString()))
                        {
                            opd.Type = OperandType.NUMBER;
                            opd.Value = var.Value.ToString();
                            return true;
                        }
                    }
                }
            }
            opd.Type = OperandType.NUMBER;
            opd.Value = 0;
            return false;
        }
        //函数传到这个回调函数里
        public static object express_OnCallBack(string funcName, object[] param, ref bool isOk)
        {
            isOk = true;
            string fun = funcName.ToUpper();
            if (fun.Equals("SIN"))
            {
                return Math.Sin(Convert.ToDouble(param[0]));
            }
            else if (fun.Equals("COS"))
            {
                return Math.Cos(Convert.ToDouble(param[0]));
            }
            else if (fun.Equals("LOG10"))
            {
                return Math.Log10(Convert.ToDouble(param[0]));
            }
            else if (fun.Equals("EXP"))
            {
                return Math.Exp(Convert.ToDouble(param[0]));
            }
            else if (fun.Equals("ASIN"))
            {
                return Math.Asin(Convert.ToDouble(param[0]));
            }
            else if (fun.Equals("ACOS"))
            {
                return Math.Acos(Convert.ToDouble(param[0]));
            }
            else if (fun.Equals("ABS"))
            {
                return Math.Abs(Convert.ToDecimal(param[0]));
            }
            else if (fun.Equals("ATAN"))
            {
                return Math.Atan(Convert.ToDouble(param[0]));
            }
            else if (fun.Equals("CEILING"))
            {
                return Math.Ceiling(Convert.ToDouble(param[0]));
            }
            else if (fun.Equals("COSH"))
            {
                return Math.Cosh(Convert.ToDouble(param[0]));
            }
            else if (fun.Equals("FLOOR"))
            {
                return Math.Floor(Convert.ToDouble(param[0]));
            }
            else if (fun.Equals("SINH"))
            {
                return Math.Sinh(Convert.ToDouble(param[0]));
            }
            else if (fun.Equals("SQRT"))
            {
                return Math.Sqrt(Convert.ToDouble(param[0]));
            }
            else if (fun.Equals("TAN"))
            {
                return Math.Tan(Convert.ToDouble(param[0]));
            }
            else if (fun.Equals("TANH"))
            {
                return Math.Tanh(Convert.ToDouble(param[0]));
            }
            else if (fun.Equals("TRUNCATE"))
            {
                return Math.Truncate(Convert.ToDouble(param[0]));
            }
            isOk = false;
            return 0;
        }
        #endregion

        #region 数值改变事件处理
        //private delegate void DelegateOnVariableChange(IVariable var);
        /// <summary>
        /// 变量值变化事件响应
        /// </summary>
        private void var_OnValueChange(IVariable sender)
        {
            variable_OnValueChange(sender); //线程内执行
            _CloudController.AddVarChangedList(sender);

            if (ServerConfig.ModbusServerEnable)
            {
                if (sender.UniteAddress > 0)
                {
                    //要求地址间隔4个字，0，4，8
                    modbusSlaveController.setHoldRegister(sender.UniteAddress, sender.ValueType, sender.Value.ToString());
                }
            }
        }
        private void var_OnProrertyChange(BaseVariable sender, string propName, object value, int valueType)
        {
            if (ServerConfig.MqttEnable)
            {
       

               // MqttController.PublishCtrlPropChanged(JsonConvert.SerializeObject(res));


                List<DevVariablePorperty> pubvars = new List<DevVariablePorperty>();
                DevVariablePorperty var = new DevVariablePorperty();
                var.ID = sender.ID;
                var.Address = sender.Address;
                var.Area = sender.Area;
                var.Category = sender.DeviceLabel;
                var.ChlID = sender.ControllerObject.ChannelObject.ID;
                var.CtrlID = sender.ControllerObject.ID;
                var.IOServerID = ServerConfig.CloudClientID;
                var.Level = sender.OperLevel;
                var.Name = sender.Name;
                var.Active = sender.Active;
                var.Timestamp = sender.DateStamp.ToString("yyyy/MM/dd HH:mm:ss");
                var.Value = value.ToString();
                var.Propery = propName;
                var.Enable = sender.Enable;
                pubvars.Add(var);

                if (ServerConfig.MqttVer == 1)
                {
                    GHMNSResponse res = new GHMNSResponse();
                    res.success = true;
                    res.HostId = Dns.GetHostName();
                    res.Message = "变量属性变化推送";
                    res.ClientId = ServerConfig.CloudClientID;
                    res.RequestID = _RequestID++.ToString();

                    res.data = pubvars;
                    MqttController.PublishVarPropChanged(JsonConvert.SerializeObject(res));
                }else
                {
                    var msg = new GHIOTMsg
                    {
                        msgId = IdWorker.GetId(),
                        msgTime = IdWorker.GetTime(),
                        data = pubvars,
                        key = ServerConfig.CloudClientID
                    };
                    MqttController.PublishVarPropChanged(JsonConvert.SerializeObject(msg));
                }

               
            }
        }
        private void var_OnWriteValueCallback(BaseVariable sender, string clientId, string batchDefinitionId, string id, string timestamp, int valueType, int itemStatus, string value, string desc, bool success)
        {
            if (ServerConfig.MqttVer == 1)
            {
                var item = sender;
                GHRTDBWriteValueCallback responseModel = new GHRTDBWriteValueCallback();
                responseModel.data = new List<GHRTDBWriteValueCallbackData>();

                responseModel.clientId = clientId;
                responseModel.batchDefinitionId = batchDefinitionId;
                responseModel.success = success;
                responseModel.message = "";
                responseModel.code = 200;
                GHRTDBWriteValueCallbackData data = new GHRTDBWriteValueCallbackData
                {
                    id = id,
                    desc = desc,
                    itemStatus = itemStatus,
                    success = success,
                    timestamp = timestamp,
                    value = value,
                    valueType = valueType
                };
                responseModel.data.Add(data);

                if (ServerConfig.MqttEnable)
                {
                    mqttController.PublishVarValueSetReply(JsonConvert.SerializeObject(responseModel));
                }
            }else
            {
                GHRTDBWriteValueCallbackV2 data = new GHRTDBWriteValueCallbackV2
                {
                    id = id,
                    desc = desc,
                    itemStatus = itemStatus,
                    success = success,
                    timestamp = timestamp,
                    value = value,
                    valueType = valueType,
                    clientId=clientId,
                    batchDefinitionId=batchDefinitionId
                    
                };
                GHIOTMsg msg = new GHIOTMsg
                {
                    msgId = IdWorker.GetId(),
                    msgTime = IdWorker.GetTime(),
                    data = data,
                    key = ServerConfig.CloudClientID
                };
                if (ServerConfig.MqttEnable)
                {
                    mqttController.PublishVarValueSetReply(JsonConvert.SerializeObject(msg));
                }

            }
        }
        /// <summary>
        /// 变量更新事件响应
        /// </summary>
        private void var_OnCounterChange(IVariable sender)
        {
            //variable_OnCounterChange(sender); //线程内执行

        }

        //private void variable_OnCounterChange(IVariable var)
        //{
        //    //服务器端显示更新
        //    if (FormIsShow && (ListViewDevice.Tag != null))
        //    {
        //        // object selectDevice = ((TreeNode)(ListViewDevice.Tag)).Tag;
        //        //if (selectDevice is BaseController)
        //        //{
        //        BaseController con = ((TreeNode)(ListViewDevice.Tag)).Tag as BaseController;
        //        if (con != null && con.Name == var.ControllerObject.Name)
        //        {
        //            // UpdateVarView(var);

        //        }
        //        //}
        //    }
        //}
        /// <summary>
        /// 变量值变化事件响应
        /// </summary>
        /// <param name="sender"></param>
        private void variable_OnValueChange(IVariable sender)
        {
            try
            {
                //Debug.WriteLine("server variable_OnValueChange " + sender.Name);
                IVariable var = sender;

                //报警条件是否满足检查与事件联动

                //兼容判断
                if (ServerConfig.CompatibleWithV1)
                {
                    VarOnEventTrigger(var);
                }
                    VarOnEventTriggerV2(var);



                //画面数据订阅分发 改为另起一个发送线程
                //netService.JugdgeIsClientSubscrible(var);

                //上报平台记录到平台数据库或保存到边缘
                History his = new History
                {
                    time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    server = ServerConfig.CloudClientID,
                    channel = var.ControllerObject.ChannelObject.ID,
                    controller = var.ControllerObject.ID,
                    varKey = var.ID,
                    value = var.Value.ToString()

                };
                //数值变化事件历史记录
                if (var.DataChangedRecorderEnable )
                {
                    RecordController.VariableChangedRecord(his);
               
                }
                //断线续传,仅续传启动变化存储的变量
                if (!MqttController.IsConnected() && var.EnableSaveChanged && ServerConfig.EnableMqttHisRecord)
                {
                    if (MqttController.dataLocalDB.Count > ServerConfig.MaxHisCount)
                    {
                        History temp;
                        MqttController.dataLocalDB.TryDequeue(out temp);
                    }
                    MqttController.dataLocalDB.Enqueue(his);

                }

                //自动复位变量
                if (var.AutoReset && var.Value.ToString() != var.DefaultValue)
                {
                    autoResetController.AddVariable(var);
                }
                

            }
            catch (Exception e)
            {
                Logger.GetInstance().LogError(e.ToString());
                // Console.WriteLine(e.ToString());
            }

        }


        #endregion

        #region 数据服务
        /*
        //服务启动 事件
        void dataServer_OnActive(bool actived)
        {
            try
            {
                this.Invoke(new data_OnActiveCallback(data_OnActive), new object[] { actived });
            }
            catch { }
        }
        public delegate void data_OnActiveCallback(bool actived);
        public void data_OnActive(bool actived)
        {
            if (actived)
            {
                //btnStartServer.Enabled = false;
                //btnStopServer.Enabled = true;
                //timerTest.Enabled = false;
                //string msg = ("网络数据服务已经启动！端口号：" + netService.Port.ToString());
                //AddOperationLog(StrConst.SERVERITY_MSG, StrConst.TITLE_OPER, "", msg);

                SetRunningState();
            }
            else
            {
                //btnStartServer.Enabled  = true;
                //btnStopServer.Enabled = false;

                //string msg = ("网络数据服务已经启动出错！端口号：" + netService.Port.ToString());
                //AddOperationLog(StrConst.SERVERITY_MSG, StrConst.TITLE_OPER, "", msg);


            }
        }
        public delegate void WebServer_OnActiveCallback(bool active);
        void WebServer_OnActive(bool active)
        {
            if (active)
            {
                string msg = ("WEB数据服务已经启动！端口号：" + ServerConfig.webDataPort.ToString());
                AddOperationLog(StrConst.SERVERITY_MSG, StrConst.TITLE_SYS, "", msg);
            }
            else
            {
                string msg = ("WEB数据服务已经关闭！端口号：" + ServerConfig.webDataPort.ToString());
                AddOperationLog(StrConst.SERVERITY_MSG, StrConst.TITLE_SYS, "", msg);
            }
        }
        void myWebServer_OnActive(bool active)
        {
            this.Invoke(new WebServer_OnActiveCallback(WebServer_OnActive), new object[] { active });
        }
        //服务关闭 事件

        public void dataServer_OnDeactive()
        {
            try
            {
                string msg = ("网络数据服务已经停止！端口号：" + netService.Port.ToString());
                AddOperationLog(StrConst.SERVERITY_MSG, StrConst.TITLE_SYS, "", msg);
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }

        }
        //客户端连接 事件

        public void dataServer_OnConnect(DataClient dc)
        {
            try
            {
                IPAddress ip = ((System.Net.IPEndPoint)dc.Socket.RemoteEndPoint).Address;
                int port = ((System.Net.IPEndPoint)dc.Socket.RemoteEndPoint).Port;
                string msg = ("客户端连接成功！IP:" + ip + " 端口号：" + port.ToString());
                AddOperationLog(StrConst.SERVERITY_MSG, StrConst.TITLE_OPER, "", msg);
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }

        }
        //客户端断开连接 事件

        public void dataServer_OnDisconnect(DataClient dc)
        {
              
            try
            {
                string msg = "客户端连接断开！IP:" + dc.ClientIp+ " 端口号：" + dc.ClientPort.ToString();
                AddOperationLog(StrConst.SERVERITY_MSG, StrConst.TITLE_OPER, "", msg);
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }
     
        }
        //接收信息 事件

        public void S_OnAlarm(AlarmMessage alarm)
        {
            netService.Send2AllClient(alarm);

        }
        */
        #endregion
        /////////////////////////////////////////
        #endregion
        #region VBS
        private void LoadVBS()
        {
            g_VbsList.Clear();
            DirectoryInfo di = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "/" + "全局脚本");
            if (di.Exists)
            {
                foreach (FileInfo fi in di.GetFiles("*.vbs"))
                {
                    using (FileStream stream = new FileStream(fi.FullName, FileMode.Open))
                    {
                        int fsLen = (int)stream.Length;
                        byte[] heByte = new byte[fsLen];
                        int r = stream.Read(heByte, 0, heByte.Length);
                        string myStr = System.Text.Encoding.UTF8.GetString(heByte);
                        g_VbsList.Add(myStr);
                        //stream.Close();
                        string msg = string.Format("加载全局脚本成功！数据文件：{0}", fi.FullName);
                        AddOperationLog(StrConst.SERVERITY_MSG, StrConst.TITLE_SYS, "", msg);

                    }
                }
            }
            else
            {
                try
                {
                    di.Create();
                }
                catch (Exception ex)
                {
                    Logger.GetInstance().LogError(ex.ToString());
                }
            }
            VBSThread = null;

        }
        // int i = 0;

        private void ThreadVBSInterp()
        {
            //i++;
            try
            {
                string[] names = GlobalVBAEngine.ScriptMethodNames;
                if (names.Length == 0) return;
                while (VBSThreadActive)
                {
                    try
                    {
                        foreach (string s in names)
                        {
                            if (!DicVbsSub.ContainsKey(s))
                            {
                                //XVBAGlobalObject.myWindow.UserInteractive = false;
                                GlobalVBAEngine.ExecuteSub(s);
                            }
                            else
                            {
                                DicVbsSub[s].Count++;
                                if (DicVbsSub[s].Count > DicVbsSub[s].Interval)
                                {
                                    DicVbsSub[s].Count = 0;
                                    //XVBAGlobalObject.myWindow.UserInteractive = false;
                                    GlobalVBAEngine.ExecuteSub(s);
                                    //Console.WriteLine("GlobalVBAEngine.ExecuteSub:" + s);

                                }
                            }
                        }

                    }
                    catch (System.Threading.ThreadInterruptedException)
                    {
                        throw;
                    }
                    catch (Exception ex)
                    {
                        Logger.GetInstance().LogError(ex.ToString());
                    }
                    //Console.WriteLine("GlobalVBAEngine.Sleep:" + i);
                    for (int i = 0; i < 10; i++)
                    {
                        if (!VBSThreadActive)
                        {
                            break;
                        }
                        Thread.Sleep(100);
                    }
                }
                VBSThread = null;
            }
            catch
            {
                VBSThread = null;
            }
        }

        Dictionary<string, VbsSub> DicVbsSub = new Dictionary<string, VbsSub>();
        private void StartVBS()
        {
            if (g_VbsList.Count > 0)
            {
                try
                {
                    string str1 = "";
                    foreach (string s in g_VbsList)
                        str1 = str1 + "\r\n" + s;
                    if (str1 == "") return;
                    this.GlobalVBAEngine.ScriptText = str1;
                    if (!this.GlobalVBAEngine.Compile())
                    {
                        string msg = "全局脚本编译出错，将不会被执行,请检查语法或方法名称是否重复";
                        AddOperationLog(StrConst.SERVERITY_MSG, StrConst.TITLE_OPER, "", msg);
                        return;
                    }
                    DicVbsSub.Clear();
                    string[] names = GlobalVBAEngine.ScriptMethodNames;
                    foreach (string s in names)
                    {
                        if (s.Contains("_"))
                        {
                            string[] s1 = s.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
                            if (s1.Length > 1)
                            {
                                if (!DicVbsSub.ContainsKey(s))
                                {
                                    VbsSub sub = new VbsSub();
                                    sub.Name = s;
                                    sub.Interval = pubFun.IsInt(s1[1], 1);
                                    sub.Count = 0;
                                    DicVbsSub.Add(s, sub);
                                }
                            }

                        }
                    }


                    if (VBSThread == null)
                    {
                        VBSThread = new Thread(new ThreadStart(ThreadVBSInterp));
                        VBSThread.IsBackground = true;
                        VBSThread.Name = "VBSThread";
                        VBSThreadActive = true;
                        VBSThread.Start();
                    }

                }
                catch (Exception ex)
                {
                    Logger.GetInstance().LogError(ex.ToString());
                }
            }

        }
        private void StopVBS()
        {
            VBSThreadActive = false;
            for (int i = 0; i < 20; i++)
            {
                if (VBSThread != null)
                    Thread.Sleep(100);
                else
                    break;
            }
            if (VBSThread != null && VBSThread.IsAlive)
            {
                try
                {
                    VBSThread.Interrupt();
                }
                catch { }
            }
            VBSThread = null;

        }

        #endregion
        private bool loading = false;
        public bool UpLoadToCloud_V2()
        {
            if (loading) return false;
            loading = true;
            bool ret = false;
            try
            {
                foreach (IChannel ch in Rtdb.ChanList)
                {
                    ch.DateStamp = DateTime.Now;
                    ch.Active = false;
                    foreach (IController con in ch.ConList)
                    {
                        con.DateStamp = DateTime.Now;
                        con.Active = false;
                        foreach (IVariable v in con.VarList)
                        {
                            v.Active = false;
                            v.Quality = (short)COMM_QUALITY_STATUS.NOT_CONNECTED;
                            v.DateStamp = DateTime.Now;
                        }
                    }
                }

                Dictionary<string, string> dic = new Dictionary<string, string>();
                BaseIOServer io = new BaseIOServer()
                {
                    IPAddress = GetInternalIP(),
                    Host = Dns.GetHostName(),
                    Name = ServerConfig.CloudClientID,
                    LastTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ID = ServerConfig.CloudClientID,
                    KEY = ServerConfig.CloudClientKEY,
                    AvailableMem = GetAvailableMem(),
                    CpuUsage = GetCpuUsage(),
                    LicenceState = GetLicenceState(),
                    OnlineState = true,
                    State = ServerConfig.RunningState,
                    TotalTime = pubFun.DateDiff2(DateTime.Now, ServerConfig.TotalTime)

                };
                dic.Add("ID", io.ID);
                dic.Add("Name", io.Name);
                dic.Add("Host", io.Host);
                dic.Add("LastTime", io.LastTime);
                dic.Add("IPAddress", io.IPAddress);

                dic.Add("KEY", io.KEY);
                dic.Add("AvailableMem", io.AvailableMem);
                dic.Add("CpuUsage", io.CpuUsage);
                dic.Add("LicenceState", io.LicenceState.ToString());
                dic.Add("OnlineState", io.OnlineState.ToString());
                dic.Add("State", io.State.ToString());
                dic.Add("TotalTime", io.TotalTime);
                if (ServerConfig.MqttEnable)
                {
                    string key = ServerConfig.CloudClientID;
                    var data = JsonConvert.SerializeObject(dic);
                    ret=  mqttController.PublishUploadConfig("ioserver_add",key, data);

                }
                Thread.Sleep(3000);
                dic.Clear();
                //创建XmlDocument文档
                XmlDocument doc = new XmlDocument();
                //创建根元素
                doc.LoadXml("<Channels></Channels>");
                XmlNode root = doc.DocumentElement;
                doc.InsertBefore(doc.CreateXmlDeclaration("1.0", "utf-8", "yes"), root);

                foreach (IChannel chan in Rtdb.ChanList)
                {

                    ICommunicationPlug plug = chan.Plugin;
                    XmlElement xmlChan = plug.SaveChannel(doc, chan);

                    dic.Add("PlugID", xmlChan.Name);
                    foreach (XmlAttribute arrchan in xmlChan.Attributes)
                    {
                        dic.Add(arrchan.Name, arrchan.Value);

                    }

                    if (ServerConfig.MqttEnable)
                    {
                        var data =JsonConvert.SerializeObject(dic);
                        string key = $"{ServerConfig.CloudClientID}:{chan.ID}";
                        ret = mqttController.PublishUploadConfig("channel_add",key,data);
                      
                    }

                    dic.Clear();
                    //控制器
                    foreach (XmlNode ctrlnode in xmlChan.ChildNodes)
                    {
                        foreach (XmlAttribute arrCtrl in ctrlnode.Attributes)
                        {
                            dic.Add(arrCtrl.Name, arrCtrl.Value);

                        }
                        if (ServerConfig.MqttEnable)
                        {
                            var data = JsonConvert.SerializeObject(dic);
                            string key = $"{ServerConfig.CloudClientID}:{chan.ID}:{ctrlnode.Attributes["ID"].Value}";
                            ret =  mqttController.PublishUploadConfig("controller_add",key,data);

                        }
                        dic.Clear();
                        //变量
                        foreach (XmlNode varnode in ctrlnode.ChildNodes)
                        {
                            foreach (XmlAttribute arrVar in varnode.Attributes)
                            {
                                dic.Add(arrVar.Name, arrVar.Value);

                            }
                            if (ServerConfig.MqttEnable)
                            {
                                var data = JsonConvert.SerializeObject(dic);
                                string key = $"{ServerConfig.CloudClientID}:{chan.ID}:{ctrlnode.Attributes["ID"].Value}:{varnode.Attributes["ID"].Value}";
                                ret =  mqttController.PublishUploadConfig("variable_add",key, data);
                            }
                            dic.Clear();

                        }



                    }

                }

            }
            catch
            {
                ret = false;
            }
            loading = false;
            return ret;
        }
        private string GetInternalIP()
        {
            IPHostEntry host;
            string localIP = "?";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }
        private string GetAvailableMem()
        {
            //try
            //{
            //    string usedMem = Math.Round(MemoPerformanceCounter.NextValue() / 1024, 2, MidpointRounding.AwayFromZero).ToString() + "G";
            //    return usedMem;
            //}
            //catch { }
            return "";
        }
        private string GetCpuUsage()
        {
            //try
            //{
            //    string usedCpu = Math.Round(CpuPerformanceCounter.NextValue(), 2, MidpointRounding.AwayFromZero).ToString() + "%";
            //    return usedCpu;
            //}
            //catch { }
            return "";
        }
        private LicenceStateEnum GetLicenceState()
        {
#if !IOPLUS
            RSAHelper rsa = new RSAHelper();

            //机器码||当前时间||授权类型||注册码||试运行时间||最大点数

            string code = ServerConfig.Expire;
            string code1 = rsa.RSADecrypt(code);
            var licMachaneCode = "";
            var licStartDate = "";
            var licType = "";
            var licRegCode = "";
            var licMaxTime = "";
            var licMaxPoint = "";
            string[] arr = code1.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            if (arr.Length == 6)
            {
                licMachaneCode = arr[0];
                licStartDate = arr[1];
                licType = arr[2];
                licRegCode = arr[3];
                licMaxTime = arr[4];
                licMaxPoint = arr[5];
                //licOK = rsa.CheckSoftRegCodeByInput(StrConst.SOFTNAME, licRegCode);
                licOK = rsa.CheckSoftRegCodeByInput2(StrConst.SOFTNAME, licMachaneCode);
                string mCode = rsa.CreateGomputerbit(StrConst.SOFTNAME);

                if (licMachaneCode != mCode)
                {
                    licType = "0";
                    licMaxTime = "30";
                    licMaxPoint = "500";
                }
            }
            LicenceStateEnum state = new LicenceStateEnum();
            if (licOK) state = LicenceStateEnum.AUTH;
            else
            {
                if (ExpireCheck(licStartDate, licMaxTime))
                {
                    state = LicenceStateEnum.NONO;
                }
                else
                {
                    state = LicenceStateEnum.TRIAL;
                }

            }
            return state;
#else
            return LicenceStateEnum.AUTH;
#endif

        }
        private bool ExpireCheck(string licStartDate, string licMaxTime)
        {
            try
            {
                DateTime start = DateTime.Parse(licStartDate);
                if (DateTime.Now < start)
                {
                    //DeviceManagement.SingletonInstance.KeyAdd("-1");
                    return true;
                }
                if (DateTime.Now > start.AddDays(int.Parse(licMaxTime)))
                {
                    //DeviceManagement.SingletonInstance.KeyAdd("-1");
                    return true;
                }
                // DeviceManagement.SingletonInstance.KeyAdd("0");
                return false;
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
                //DeviceManagement.SingletonInstance.KeyAdd("-1");
                return true;
            }
            //DeviceManagement.SingletonInstance.KeyAdd("-1");
            return true;
        }

        private List<commLog> GetCommLogs()
        {
            List<commLog> logs = new List<commLog>();
            lock (commLogs)
            {
                if (commLogs.Count > 0)
                {
                    logs.AddRange(commLogs.ToArray());
                    //logs.Clear();
                }
            }
            return logs;
        }
        private void ClearCommLogs()
        {
            lock (commLogs)
            {
                if (commLogs.Count > 0)
                {
                    //logs.Clear();
                }
            }
        }
        private void AddCommLog(commLog log)
        {

            lock (commLogs)
            {
                if (commLogs.Count > 100)
                {
                    commLogs.RemoveAt(commLogs.Count - 1);
                }
                commLogs.Insert(0, log);
            }
        }
        public void SentHisLogs()
        {
            List<commLog> logs = GetCommLogs();
            if (ServerConfig.MqttVer == 1)
            {
                GHMNSResponse responseModel = new GHMNSResponse();
                responseModel.Code = "SentHisLogs";
                responseModel.RequestID = _RequestID++.ToString();
                responseModel.Message = "SentHisLogs";
                responseModel.HostId = ServerConfig.CloudClientID;
                responseModel.success = true;
                responseModel.data = logs;
                string msg = JsonConvert.SerializeObject(responseModel);
                MqttController.PublishEvent(msg);
            }else
            {
                GHIOTMsg msg = new GHIOTMsg
                {
                    msgId = IdWorker.GetId(),
                    msgTime = IdWorker.GetTime(),
                    data = logs,
                    key = ServerConfig.CloudClientID,
                    code="logs"
                    
                };
                MqttController.PublishEvent(JsonConvert.SerializeObject(msg));

            }
        }
        private void SentLog(commLog lg)
        {
            List<commLog> logs = new List<commLog>();
            logs.Add(lg);

            if (ServerConfig.MqttVer == 1)
            {

                GHMNSResponse responseModel = new GHMNSResponse();
                responseModel.Code = "SentHisLogs";
                responseModel.RequestID = _RequestID++.ToString();
                responseModel.Message = "SentHisLogs";
                responseModel.HostId = ServerConfig.CloudClientID;
                responseModel.success = true;
                responseModel.data = logs;
                string msg = JsonConvert.SerializeObject(responseModel);
                MqttController.PublishEvent(msg);
            }
            else
            {
                GHIOTMsg msg = new GHIOTMsg
                {
                    msgId = IdWorker.GetId(),
                    msgTime = IdWorker.GetTime(),
                    data = logs,
                    key = ServerConfig.CloudClientID,
                    code="logs"
                };
                MqttController.PublishEvent(JsonConvert.SerializeObject(msg));

            }
        }
    }

    class VbsSub
    {
        public string Name;
        public int Interval;
        public int Count;
    }
    class commLog
    {
        public string DateStamp;
        public string Severity;
        public string Type;
        public string Title;
        public string Content;

    }
}
