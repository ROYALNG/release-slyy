using GHIBMS.Common;
using GHIBMS.Common.BaseClass;
using GHIBMS.Common.Encryption;
using GHIBMS.Interface;
using GHIBMS.Server.Models;
using GHIBMS.Server.Pub;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using MQTTnet.Client.Subscribing;
using MQTTnet.Formatter;
using MQTTnet.Protocol;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace GHIBMS.Server
{
    public class MqttController
    {
        //发布启动后上报变量初始值一次
        private static readonly string topic_up_var_init = "_sys_{0}_variable_initial_post";  //{0} ioserverkey
        //发布运行中变量变化上报
        private static readonly string topic_up_var_change = "_sys_{0}_variable_change_post";  //{0} ioserverkey
        //订阅优先通讯变量
        private static readonly string topic_down_var_realtime = "_sys_{0}_variable_realtime_set";  //{0} ioserverkey
        //发布实时报警
        private static readonly string topic_up_alm_realtime = "_sys_{0}_alarm_realtime_post";  //{0} ioserverkey
        //订阅报警复位
        private static readonly string topic_down_alm_reset = "_sys_{0}_alarm_reset_set";  //{0} ioserverkey
        //发布报警复位回复
        private static readonly string topic_up_alm_reset_reply = "_sys_{0}_alarm_reset_set_reply";  //{0} ioserverkey
        //订阅变量当前值设置
        private static readonly string topic_down_var_value_set = "_sys_{0}_variable_value_set";  //{0} ioserverkey
        //发布变量当前值设置回复
        private static readonly string topic_up_var_value_set_reply = "_sys_{0}_variable_value_set_reply";  //{0} ioserverkey

        private static readonly string topic_up_config = "_sys_{0}_config_post";//{0} ioserverkey 
        private static readonly string topic_up_io_prop_change = "_sys_{0}_ioserver_propery_change_post";
        private static readonly string topic_up_chl_propp_change = "_sys_{0}_channel_propery_change_post";
        private static readonly string topic_up_ctrl_prop_change = "_sys_{0}_control_propery_change_post";
        private static readonly string topic_up_var_prop_change = "_sys_{0}_variable_propery_change_post";
        private static readonly string topic_down_ExecCommand = "_sys_{0}_ExecCommand_set";

        private static readonly string topic_down_alarm_rule = "_sys_{0}_alarm_rule_set";
        private static readonly string topic_up_alarm_rule_reply = "_sys_{0}_alarm_rule_set_reply";

        private static readonly string topic_down_event_set = "_sys_{0}_event_set";
        private static readonly string topic_up_event_set_reply = "_sys_{0}_event_post";
        private static readonly string topic_down_connect_replay= "_sys_{0}_connect_reply";
        private static readonly string topic_up_variabe_record_post = "_sys_{0}_variable_record_post";

        private static readonly string topic_up_ntp_post = "_sys_{0}_ntp_post";
        private static readonly string topic_down_ntp_reply = "_sys_{0}_ntp_reply";

        private Thread thread1;
        private Thread thread2;
        public bool active = false;
        private bool resetActive = false;
        //static List<DevVariable> pubvars = new List<DevVariable>();
        private static Dictionary<string, DevVariable> pubvars = new Dictionary<string, DevVariable>();
        private uint RetryCounter = 0;

        private Action<MqttApplicationMessageReceivedEventArgs> mqtt_received;
        private Action<MqttClientConnectedEventArgs> mqtt_connected;
        private Action<MqttClientDisconnectedEventArgs> mqtt_disconnected;

        private static IMqttClient mqttClient = null;
        private static bool isConnect = false;
        static DriverMng mng;
        public static ConcurrentQueue<History> dataLocalDB = new ConcurrentQueue<History>();
        //加密传输头
        private const string EncryptHead = "v2m1";

        [StructLayout(LayoutKind.Sequential)]
        public struct SystemTime
        {
            public ushort wYear;
            public ushort wMonth;
            public ushort wDayOfWeek;
            public ushort wDay;
            public ushort wHour;
            public ushort wMinute;
            public ushort wSecond;
            public ushort wMiliseconds;
        }

#if !IOPLUS
        [DllImport("kernel32.dll")]
        private static extern bool SetLocalTime(ref SystemTime time);
#endif
        public MqttController(DriverMng _mng)
        {
            mng = _mng;
            mqtt_received = new Action<MqttApplicationMessageReceivedEventArgs>(OnMqttClientReceived);
            mqtt_connected = new Action<MqttClientConnectedEventArgs>(OnMqttClientConnected);
            mqtt_disconnected = new Action<MqttClientDisconnectedEventArgs>(OnMqttClientDisConnected);
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "record/history.hex";
            try
            {
                if (File.Exists(path))
                {
                    dataLocalDB = ReadFromBinaryFile<ConcurrentQueue<History>>(path);
                    File.Delete(path);

                }
            }
            catch { }

        }
        public static void Save()
        {
            try
            {

                string path = System.AppDomain.CurrentDomain.BaseDirectory + "record";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                if (dataLocalDB.Count > 0)
                    WriteToBinaryFile<ConcurrentQueue<History>>(path + "/history.hex", dataLocalDB, false);
                else if (File.Exists(path + "/history.hex"))
                    File.Delete(path + "/history.hex");
            }
            catch { }
        }
        public event CommMsgDelegate OnCommMsg;
        public  void SendCommEvent(Severity severity, CommunicationEvent commMsgType, string wParamm, string lParamm)
        {
            if (OnCommMsg != null)
                OnCommMsg(this, severity, commMsgType, wParamm, lParamm);
        }
        public void Start()
        {
            try
            {
                Stop();
                active = true;

                if (thread1 != null)
                    return;
                thread1 = new Thread(ConnectMqttServer)
                {
                    //线程名，调试用
                    Name = "ConnectMqtt_thread",
                    IsBackground = true
                };
                thread1.Start();

                if (thread2 != null)
                    return;
                thread2 = new Thread(ProduceThread);
                //线程名，调试用
                thread2.Name = "PerformMqtt_thread";
                thread2.IsBackground = true;
                thread2.Start();
            }
            catch (Exception ex)
            {

                Logger.GetInstance().LogError(ex.Message);
            }
        }

        public void Stop()
        {
            try
            {
                if (active)
                {
                    Logger.GetInstance().LogMsg("物联网平台强制断开！");
                    SendCommEvent(Severity.信息, CommunicationEvent.COMM_INFO, "系统用户", "物联网平台强制断开！");
                }
                active = false;


                for (int i = 0; i < 20; i++)
                {
                    if (thread1 != null)
                        Thread.Sleep(100);
                    else
                        break;
                }
                if (thread1 != null)
                {
                    try
                    {
                        thread1.Interrupt();
                    }
                    catch { }
                }
                thread1 = null;
                for (int i = 0; i < 20; i++)
                {
                    if (thread2 != null)
                        Thread.Sleep(100);
                    else
                        break;
                }
                if (thread2 != null)
                {
                    try
                    {
                        thread2.Interrupt();
                    }
                    catch { }
                }
                thread2 = null;
                if (mqttClient != null)
                {
                    if (mqttClient.IsConnected)
                    {
                        mqttClient.DisconnectAsync();
                    }
                }
                RetryCounter = 0;
                mqttClient = null;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void ConnectMqttServer()
        {
            //string str = ServerConfig.CloudClientID + '|' + ServerConfig.CloudClientKEY + '|' + pubFun.checkUrl(Dns.GetHostName());

            //byte[] bytes = Encoding.UTF8.GetBytes(str);
            //string str2 = Convert.ToBase64String(bytes);
            //string clientid =UrlEncode(str2, Encoding.UTF8);
            try
            {
                string clientid = ServerConfig.CloudClientID + '|' + ServerConfig.CloudClientKEY + '|' + "IOPLUS";//+ pubFun.checkUrl(Dns.GetHostName());
                DateTime syncNtp = DateTime.Now;
                while (active)
                {

                    try
                    {

                        if (mqttClient == null)
                        {
                            var factory = new MqttFactory();
                            mqttClient = factory.CreateMqttClient();
                            mqttClient.UseApplicationMessageReceivedHandler(mqtt_received);
                            mqttClient.UseConnectedHandler(mqtt_connected);
                            mqttClient.UseDisconnectedHandler(mqtt_disconnected);
                        }
                        if (!IsConnected())
                        {
                            Logger.GetInstance().LogMsg("尝试连接物联网平台，" + ServerConfig.MqttServerIp + ":" + ServerConfig.MqttServerPort);
                            IMqttClientOptions options = new MqttClientOptionsBuilder()
                              .WithClientId(clientid)
                              .WithTcpServer(ServerConfig.MqttServerIp, ServerConfig.MqttServerPort)
                              .WithCredentials(ServerConfig.MqttUserName, ServerConfig.MqttPassword)
                              .WithCleanSession()
                              .WithProtocolVersion(MqttProtocolVersion.V311)
                              .WithKeepAlivePeriod(TimeSpan.FromMinutes(1))
                              .Build();
                            mqttClient.ConnectAsync(options).Wait();


                            if (IsConnected())
                            {

                                MqttClientSubscribeOptions topics = new MqttClientSubscribeOptions();
                                List<MqttTopicFilter> lst = new List<MqttTopicFilter>();
                                MqttTopicFilter f1 = new MqttTopicFilter();
                                f1.Topic = string.Format(topic_down_alm_reset, ServerConfig.CloudClientID);
                                f1.QualityOfServiceLevel = MqttQualityOfServiceLevel.ExactlyOnce;
                                lst.Add(f1);
                                MqttTopicFilter f2 = new MqttTopicFilter();
                                f2.Topic = string.Format(topic_down_var_realtime, ServerConfig.CloudClientID);
                                f2.QualityOfServiceLevel = MqttQualityOfServiceLevel.ExactlyOnce;
                                lst.Add(f2);
                                MqttTopicFilter f3 = new MqttTopicFilter();
                                f3.Topic = string.Format(topic_down_var_value_set, ServerConfig.CloudClientID);
                                f3.QualityOfServiceLevel = MqttQualityOfServiceLevel.ExactlyOnce;
                                lst.Add(f3);

                                MqttTopicFilter f4 = new MqttTopicFilter();
                                f4.Topic = string.Format(topic_down_ExecCommand, ServerConfig.CloudClientID);
                                f4.QualityOfServiceLevel = MqttQualityOfServiceLevel.ExactlyOnce;
                                lst.Add(f4);

                                MqttTopicFilter f5 = new MqttTopicFilter();
                                f5.Topic = string.Format(topic_down_event_set, ServerConfig.CloudClientID);
                                f5.QualityOfServiceLevel = MqttQualityOfServiceLevel.ExactlyOnce;
                                lst.Add(f5);

                                MqttTopicFilter f6 = new MqttTopicFilter();
                                f6.Topic = string.Format(topic_down_alarm_rule, ServerConfig.CloudClientID);
                                f6.QualityOfServiceLevel = MqttQualityOfServiceLevel.ExactlyOnce;
                                lst.Add(f6);

                                MqttTopicFilter f7 = new MqttTopicFilter();
                                f7.Topic = string.Format(topic_down_connect_replay, ServerConfig.CloudClientID);
                                f7.QualityOfServiceLevel = MqttQualityOfServiceLevel.ExactlyOnce;
                                lst.Add(f7);

                                MqttTopicFilter f8 = new MqttTopicFilter();
                                f8.Topic = string.Format(topic_down_ntp_reply, ServerConfig.CloudClientID);
                                f8.QualityOfServiceLevel = MqttQualityOfServiceLevel.ExactlyOnce;
                                lst.Add(f8);

                                topics.TopicFilters = lst;

                                mqttClient.SubscribeAsync(topics).Wait();
                                Logger.GetInstance().LogMsg("物联网平台连接成功，完成TOPIC订阅！" + ServerConfig.MqttServerIp + ":" + ServerConfig.MqttServerPort);

                                publishDevID();
                                PublishIOServerState();
                            }
                        }

                    }
                    catch (System.Threading.ThreadInterruptedException)
                    {
                        throw;
                    }
                    catch (Exception ex)
                    {
                        isConnect = false;
                        try
                        {
                            Logger.GetInstance().LogError("物联网平台连接失败！" + clientid + "," + ServerConfig.MqttServerIp + ":" + ServerConfig.MqttServerPort + "  " + ex.InnerException.ToString());
                            SendCommEvent(Severity.信息, CommunicationEvent.COMM_INFO, "系统用户", "物联网平台连接失败！" + clientid + "," + ServerConfig.MqttServerIp + ":" + ServerConfig.MqttServerPort);
                        }
                        catch (System.Threading.ThreadInterruptedException)
                        {
                            throw;
                        }
                        catch { }
                    }
                    try
                    {
                        if (!IsConnected())
                        {
                            RetryCounter++;
                        }
                        else
                        {
                            RetryCounter = 0;
                        }

                    }
                    catch (System.Threading.ThreadInterruptedException)
                    {
                        throw;
                    }
                    catch { }

                    try
                    {
                        if (IsConnected())
                        {
                            if (syncNtp < DateTime.Now) //24小时同步一次时间                            {
                            { 
                                RequestNtp();
                                syncNtp.AddHours(24);
                            }

                        }

                    }
                    catch (System.Threading.ThreadInterruptedException)
                    {
                        throw;
                    }
                    catch { }
                  
                    if (RetryCounter <= 3) //失败前三次
                    {
                        for (int i = 0; i < 100; i++) //延时10s
                        {
                            if (!active)
                            {
                                thread1 = null;
                                return;
                            }
                            Thread.Sleep(100);
                            if (resetActive)
                            {
                                resetActive = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 300; i++) //延时30s
                        {
                            if (!active)
                            {
                                thread1 = null;
                                return;
                            }
                            Thread.Sleep(100);
                            if (resetActive)
                            {
                                resetActive = false;
                                break;
                            }
                        }
                    }

                }
                thread1 = null;
            }
            catch
            {
                thread1 = null;
            }
        }
        public void Reset()
        {
            try
            {
                if (mqttClient != null)
                    mqttClient.DisconnectAsync();
                RetryCounter = 0;
                resetActive = true;
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.Message);
            }

        }
        public static bool IsConnected()
        {
            if (mqttClient == null) return false;
            else
                return isConnect;
        }
        void OnMqttClientReceived(MqttApplicationMessageReceivedEventArgs e)
        {
            //Debug.WriteLine("### RECEIVED APPLICATION MESSAGE ###");
            //Debug.WriteLine($"+ Topic = {e.ApplicationMessage.Topic}");
            //Debug.WriteLine($"+ Payload = {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}");
            //Debug.WriteLine($"+ QoS = {e.ApplicationMessage.QualityOfServiceLevel}");
            //Debug.WriteLine($"+ Retain = {e.ApplicationMessage.Retain}");

            string topic_down_var_realtime2 = string.Format(topic_down_var_realtime, ServerConfig.CloudClientID);
            string topic_down_alm_reset2 = string.Format(topic_down_alm_reset, ServerConfig.CloudClientID);
            string topic_down_var_value_set2 = string.Format(topic_down_var_value_set, ServerConfig.CloudClientID);
            string topic_down_alarm_rule2 = string.Format(topic_down_alarm_rule, ServerConfig.CloudClientID);
            string topic_down_event_set2 = string.Format(topic_down_event_set, ServerConfig.CloudClientID);
            string topic_down_connect_replay2 = string.Format(topic_down_connect_replay, ServerConfig.CloudClientID);


            string Topic = e.ApplicationMessage.Topic;
            Console.WriteLine(Topic);

            string topic_down_ExecCommand2 = string.Format(topic_down_ExecCommand, ServerConfig.CloudClientID);
            string topic_down_ntp_replay2 = string.Format(topic_down_ntp_reply, ServerConfig.CloudClientID);
            string Playload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

            if(Playload.Substring(0,4)=="v2m1")
            {
                string s = Playload.Substring(4);
                Playload=DesUtil.DecodeDES(s,ServerConfig.MqttEncryptKey);
            }

            if (Topic == topic_down_var_realtime2)
            {
                SubscribeLiveValueCallback(Playload);
            }
            else if (Topic == topic_down_alm_reset2)
            {
                SubscribeAlarmProcessCallback(Playload);
            }
            else if (Topic == topic_down_var_value_set2)
            {
                SubscribeWriteValueCallback(Playload);
            }
            else if (Topic == topic_down_ExecCommand2)
            {
                SubscribeExecCommandCallback(Playload);
            }
            else if (Topic == topic_down_alarm_rule2)
            {
                SubscribeAlmRuleSetCallback(Playload);
            }
            else if (Topic == topic_down_event_set2)
            {
                SubscribeLogGetCallback(Playload);
            }
            else if(Topic== topic_down_connect_replay2)
            {
                JObject jo = JsonConvert.DeserializeObject<JObject>(Playload);
                if(jo["data"]!=null)
                {
                    ServerConfig.ProjectId = pubFun.IsInt(jo["data"].ToString(), 0);
                }
               
            }
            else if(Topic==topic_down_ntp_replay2) 
            {
                ResponeNtp(Playload);
            }
        }
        static bool Publish(string topic, string payload)
        {
            try
            {
                string playload2 = payload;
                if (ServerConfig.MqttEncrypt)
                    playload2 =EncryptHead+ DesUtil.EncodeDES(payload,ServerConfig.MqttEncryptKey);
                //Console.WriteLine(topic);
                if (mqttClient == null) return false;
                var message = new MqttApplicationMessageBuilder()
                    .WithAtMostOnceQoS()
                    .WithTopic(topic)
                    .WithPayload(playload2)
                    .WithRetainFlag()
                    .Build();

                if (IsConnected())
                {
                    mqttClient.PublishAsync(message);
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                //if(ServerConfig.WriteLog)
                Logger.GetInstance().LogMsg("MQTT Publish失败，topic:" + topic);
                return false;
            }

        }
        
        public bool PublishUploadConfig(string code,string key,string data)
        {
            string topic = string.Format(topic_up_config, ServerConfig.CloudClientID);
            bool ret = false;
            string playload="";
            if (ServerConfig.MqttVer == 1)
            {
                var obj = new JObject { { "function", code }, { "key", key } };
                obj.Add("data", data);
                playload = JsonConvert.SerializeObject(obj);
             
            }
            else if (ServerConfig.MqttVer == 2)
            {
                var msg = new GHIOTMsg()
                {
                    code = code,
                    msgId = IdWorker.GetId(),
                    msgTime = IdWorker.GetTime(),
                    key = key,
                    data = data

                };
                playload = JsonConvert.SerializeObject(msg);

            }
            ret= Publish(topic, playload);
            if (ret)
                Logger.GetInstance().LogMsg($"PublishUploadConfig/{code}成功！");

            else
                Logger.GetInstance().LogMsg($"PublishUploadConfig/{code}失败！");

            return ret;
        }

        //上报机器ID,ioserver_add主题
        public void publishDevID()
        {
            try
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                BaseIOServer io = new BaseIOServer()
                {
                    IPAddress = GetInternalIP(),
                    Host = Dns.GetHostName(),
                    Name = ServerConfig.CloudClientID,
                    LastTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ID = ServerConfig.CloudClientID,
                    KEY = ServerConfig.CloudClientKEY,
                    AvailableMem = "",
                    CpuUsage ="",
                    LicenceState = LicenceStateEnum.AUTH,
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
                    var data =JsonConvert.SerializeObject(dic);
                    PublishUploadConfig("ioserver_online", ServerConfig.CloudClientID, data);
                }


            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogMsg("publishDevID出错" + ex.ToString());
            }
            // SendCommEvent(Severity.信息, CommunicationEvent.COMM_INFO, ServerConfig.CloudClientID, "定时同步采集器状态成功！");
            //Logger.GetInstance().LogMsg("定时同步采集器状态成功");

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
        public bool PublishIOServerState()
        {
            try

            {
                JObject jo = new JObject { { "Name", ServerConfig.CloudClientID },
                                                       { "Propery", "State" },
                                                       { "Value", ServerConfig.RunningState.ToString() },
                                                       { "ValueType", 3 },
                                                       { "Timestamp", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") },
                                                       { "IOServerID",ServerConfig.CloudClientID }
                                                      };
                JArray ja = new JArray();
                ja.Add(jo);

                if (ServerConfig.MqttVer == 1)
                {
                    GHMNSResponse res = new GHMNSResponse();
                    res.success = true;
                    res.HostId = Dns.GetHostName();
                    res.Message = "采集器状态变化推送";
                    res.ClientId = ServerConfig.CloudClientID;
                    res.RequestID = DriverMng._RequestID++.ToString();
                    res.data = ja;
                   return MqttController.PublishIoPropChanged(JsonConvert.SerializeObject(res));
                }else if (ServerConfig.MqttVer == 2)
                {
                    var msg = new GHIOTMsg()
                    {
                        code = "",
                        msgId = IdWorker.GetId(),
                        msgTime = IdWorker.GetTime(),
                        key = ServerConfig.CloudClientID,
                        data = ja

                    };
                    return MqttController.PublishIoPropChanged(JsonConvert.SerializeObject(msg));
                }
              
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogMsg("PublishIOServerState失败!" + ex.ToString());
                return false;
            }
            return false;
        }
        public bool PublishVarValueSetReply(string payload)
        {

            string topic = string.Format(topic_up_var_value_set_reply, ServerConfig.CloudClientID);
            return Publish(topic, payload);

        }

        public static void addPubVars(List<DevVariable> vars)
        {
            lock (pubvars)
            {
                foreach(DevVariable var in vars)
                {
                    string key = $"{var.IOServerID}:{var.ChlID}:{var.CtrlID}:{var.ID}";
                    if(pubvars.ContainsKey(key))
                    {
                        pubvars[key].Value = var.Value;
                        pubvars[key].Timestamp = var.Timestamp;
                        if (!pubvars[key].EnableSave)//变化记录和报警记录确保上传一次
                            pubvars[key].EnableSave = var.EnableSave;
                    }
                    else
                    {
                        pubvars.Add(key, var);
                    }
                }

                //pubvars.AddRange(vars);
            }
        }
        public static void addPubVar(IVariable var,bool bSave)
        {
            DevVariable dvVar = new DevVariable
            {
                ID = var.ID,
                Name = var.Name,
                IOServerID = ServerConfig.CloudClientID,
                ChlID = var.ControllerObject.ChannelObject.ID,
                CtrlID = var.ControllerObject.ID,
                Active = var.Active,
                ValueType = pubFun.ValueCode2Type(var.ValueType),
                Value = var.Value.ToString(),
                Timestamp = var.DateStamp.ToString("yyyy/MM/dd HH:mm:ss"),
                Area = var.Area,
                Level = var.OperLevel,
                Enable = var.Enable,
                Address = var.Address,
                EnableSave = bSave
            };
            lock (pubvars)
            {

                    string key = $"{dvVar.IOServerID}:{dvVar.ChlID}:{dvVar.CtrlID}:{dvVar.ID}";
                    if (pubvars.ContainsKey(key))
                    {
                        pubvars[key].Value = dvVar.Value;
                        pubvars[key].Timestamp = dvVar.Timestamp;
                        if (!pubvars[key].EnableSave)//变化记录和报警记录确保上传一次
                            pubvars[key].EnableSave = dvVar.EnableSave;
                    }
                    else
                    {
                        pubvars.Add(key, dvVar);
                    }
            
            }

        }

        public List<DevVariable> getPubVars()
        {
            List<DevVariable> d = new List<DevVariable>();
            lock (pubvars)
            {
                d.AddRange(pubvars.Values);
                pubvars.Clear();
            }
            return d;
        }
        public bool PublishVarChanged(List<DevVariable> vars)
        {
            if (!active) return false;
            addPubVars(vars);


            return true;
        }
        public void ProduceThread()
        {
            try
            {
                while (active)
                {

                    try
                    {
                        if (IsConnected())
                        {
                            string topic = string.Format(topic_up_var_change, ServerConfig.CloudClientID);

                            if (ServerConfig.MqttVer == 1)
                            {

                                GHMNSResponse responseModel = new GHMNSResponse();

                                responseModel.ObjectID = "3";
                                responseModel.Code = "3";
                                responseModel.Message = "变化上报";
                                responseModel.HostId = Dns.GetHostName();
                                //responseModel.data ;
                                responseModel.success = true;
                                responseModel.RequestID = CloudController._RequestID++.ToString();
                                responseModel.ClientId = "0";


                                List<DevVariable> data = new List<DevVariable>();
                                List<DevVariable> vars = getPubVars();
                                for (int i = 1; i <= vars.Count; i++)
                                {
                                    data.Add(vars[i - 1]);
                                    if (i % 500 == 0 || i == vars.Count)
                                    {
                                        if (!active)
                                        {
                                            thread2 = null;
                                            return;
                                        }
                                        responseModel.data = data;
                                        string payload = JsonConvert.SerializeObject(responseModel);
                                        Publish(topic, payload);
                                        data.Clear();

                                    }
                                }
                            }else if(ServerConfig.MqttVer==2)
                            {
                                var msg = new GHIOTMsg()
                                {
                                    code = "",
                                    msgId = IdWorker.GetId(),
                                    msgTime = IdWorker.GetTime(),
                                    key = ServerConfig.CloudClientID,
                                };


                                List<DevVariable> data = new List<DevVariable>();
                                List<DevVariable> vars = getPubVars();
                                for (int i = 1; i <= vars.Count; i++)
                                {
                                    data.Add(vars[i - 1]);
                                    if (i % 500 == 0 || i == vars.Count)
                                    {
                                        if (!active)
                                        {
                                            thread2 = null;
                                            return;
                                        }
                                        msg.data = data;
                                        string payload = JsonConvert.SerializeObject(msg);
                                        Publish(topic, payload);
                                        data.Clear();

                                    }
                                }

                            }
                            
                        }
                        else
                        {
                            if (ServerConfig.WriteLog)
                                Logger.GetInstance().LogMsg("变化上报发送数据失败，物联网平台未连接！");
                        }

                        //利用空闲时间上传历史记录 最长时间500ms,不到500ms就sleep
                        if (dataLocalDB.Count > 0)
                        {
                            long N1 = DateTime.Now.Ticks;
                            string topic2 = string.Format(topic_up_variabe_record_post, ServerConfig.CloudClientID);
                            while (DateTime.Now.Ticks - N1 < 500 * 10000)
                            {
                                if (dataLocalDB.Count > 0)
                                {
                                    int counter = dataLocalDB.Count > 500 ? 500 : dataLocalDB.Count;
                                    List<string> lst = new List<string>();
                                    for (int i = 0; i < counter; i++)
                                    {
                                        History hs;
                                        if (dataLocalDB.TryPeek(out hs))
                                        {
                                            string pl = $"{hs.time}&{hs.server}:{hs.channel}:{hs.controller}:{hs.varKey}&{hs.value}";
                                            lst.Add(pl);
                                        }
                                    }
                                    GHIOTMsg msg = new GHIOTMsg();
                                    msg.msgId = IdWorker.GetId();
                                    msg.msgTime = IdWorker.GetTime();
                                    msg.data = lst;
                                    msg.key = ServerConfig.CloudClientID;
                                    string str = JsonConvert.SerializeObject(msg);

                                    if (Publish(topic2, str))
                                    {
                                        for (int i = 0; i < counter; i++)
                                        {
                                            History hs;
                                            dataLocalDB.TryDequeue(out hs);
                                        }
                                    }
                                }
                                else
                                {
                                    if (DateTime.Now.Ticks - N1 < 500 * 10000)
                                        Thread.Sleep(10);
                                }

                            }
                        }else
                        {
                            //sleep 500ms
                            int delayCounter = 0;
                            do
                            {
                                ++delayCounter;
                                if (!active)
                                {
                                    thread2 = null;
                                    return;
                                }
                                Thread.Sleep(50);
                            } while (delayCounter * 50 < ServerConfig.MqttDelay);
                        }
                    }
                    catch (System.Threading.ThreadInterruptedException)
                    {
                        throw;
                    }
                    catch (Exception ex)
                    {
                        Logger.GetInstance().LogError(ex.Message);
                    }
                  

                }//end while
                thread2 = null;
            }
            catch(Exception ex)
            {
                thread2 = null;
                Logger.GetInstance().LogError(ex.ToString());
            }

        }
        
        //同步变量数值
        public bool PublishVarInit(List<DevVariable> vars)
        {

            try
            {
                bool ret = false;
                if (IsConnected())
                {
                    string topic = string.Format(topic_up_var_init, ServerConfig.CloudClientID);
                    if (ServerConfig.MqttVer == 1)
                    {
                        GHMNSResponse responseModel = new GHMNSResponse();

                        responseModel.ObjectID = "3";
                        responseModel.Code = "3";
                        responseModel.Message = "实时数据初始化上报";
                        responseModel.HostId = Dns.GetHostName();
                        //responseModel.data ;
                        responseModel.success = true;
                        responseModel.RequestID = CloudController._RequestID++.ToString();
                        responseModel.ClientId = "0";
                        List<DevVariable> data = new List<DevVariable>();
                        for (int i = 1; i <= vars.Count; i++)
                        {
                            data.Add(vars[i - 1]);
                            if (i % 500 == 0 || i == vars.Count)
                            {
                                responseModel.data = data;
                                string payload = JsonConvert.SerializeObject(responseModel);
                                ret = Publish(topic, payload);
                                data.Clear();
                            }
                        }
                    }
                    else if (ServerConfig.MqttVer == 2)
                    {
                        var msg = new GHIOTMsg()
                        {
                            code = "",
                            msgId = IdWorker.GetId(),
                            msgTime = IdWorker.GetTime(),
                            key = ServerConfig.CloudClientID,
                        };
                        List<DevVariable> data = new List<DevVariable>();
                        for (int i = 1; i <= vars.Count; i++)
                        {
                            data.Add(vars[i - 1]);
                            if (i % 500 == 0 || i == vars.Count)
                            {
                                msg.data = data;
                                string payload = JsonConvert.SerializeObject(msg);
                                ret = Publish(topic, payload);
                                data.Clear();
                            }
                        }

                    }

                }
                else
                {
                    if (ServerConfig.WriteLog)
                        Logger.GetInstance().LogMsg("初始化上报发送数据失败，物联网平台未连接！");
                    return false;
                }
                return ret;
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.Message);
                return false;
            }
        }
        public static bool PublishAlarmReal(List<AlarmMessage> almmsg)
        {

            try
            {
 
                if (ServerConfig.MqttVer == 1)
                {
                    GHMNSResponse responseModel = new GHMNSResponse();

                    responseModel.ObjectID = "2";
                    responseModel.Code = "2";
                    responseModel.Message = "上传实时报警";
                    responseModel.HostId = "";
                    responseModel.data = almmsg;
                    responseModel.success = true;

                    //msgs.Add(responseModel);

                    string topic = string.Format(topic_up_alm_realtime, ServerConfig.CloudClientID);
                    string s = JsonConvert.SerializeObject(responseModel);
                    return Publish(topic, s);
                }
                else if (ServerConfig.MqttVer == 2)
                {
                    var msg = new GHIOTMsg()
                    {
                        code = "",
                        msgId = IdWorker.GetId(),
                        msgTime = IdWorker.GetTime(),
                        key = ServerConfig.CloudClientID,
                    };
                    List < AlarmMsg > data= new List<AlarmMsg>() ;
                    foreach (AlarmMessage m in almmsg)
                    {
                        data.Add(
                            new AlarmMsg()
                            {
                                 AlarmChannel=m.AlarmChannel,
                                 AlarmController=m.AlarmController,
                                 AlarmIOServer=m.AlarmIOServer,
                                 AlarmValue=m.AlarmValue,
                                 AlarmVariableDesc=m.AlarmVariableDesc,
                                 AlarmVariableID=m.AlarmVariableID 

                            });
                    }
                    msg.data = data;
                    string topic = string.Format(topic_up_alm_realtime, ServerConfig.CloudClientID);
                    string s = JsonConvert.SerializeObject(msg);
                    return Publish(topic, s);
                }

            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.Message);
                return false;
            }
            return false;
        }
        static bool PublishAlarmResetReply(string payload)
        {

            string topic = string.Format(topic_up_alm_reset_reply, ServerConfig.CloudClientID);
            return Publish(topic, payload);
        }
        public static bool PublishVarPropChanged(string payload)
        {
            string topic = string.Format(topic_up_var_prop_change, ServerConfig.CloudClientID);
            return Publish(topic, payload);
        }
        public static bool PublishIoPropChanged(string payload)
        {
            string topic = string.Format(topic_up_io_prop_change, ServerConfig.CloudClientID);
            return Publish(topic, payload);
        }
        public static bool PublishChlPropChanged(string payload)
        {
            string topic = string.Format(topic_up_chl_propp_change, ServerConfig.CloudClientID);
            return Publish(topic, payload);
        }
        public static bool PublishEvent(string payload)
        {
            string topic = string.Format(topic_up_event_set_reply, ServerConfig.CloudClientID);
            return Publish(topic, payload);
        }
        public static bool PublishCommEvent(string deviceId,string deviceName,string deviceType,string eventType,string eventDesc)
        {
            JObject jo = new JObject {  { "deviceId", deviceId },
                                        { "deviceName", deviceName},
                                        { "deviceType", deviceType },
                                        { "eventType",eventType },
                                        { "eventDesc",eventDesc },
                                        { "projectId", ServerConfig.ProjectId}
                                     };
            GHIOTMsg msg = new GHIOTMsg
            {
                msgId = IdWorker.GetId(),
                msgTime = IdWorker.GetTime(),
                data = jo,
                key = ServerConfig.CloudClientID,
                code = "comm"
            };
            return  MqttController.PublishEvent(JsonConvert.SerializeObject(msg));
        }
        public static bool PublishChlEnable(IChannel chan)
        {
            if (ServerConfig.MqttVer == 1)
            {
                GHMNSResponse res = new GHMNSResponse();
                res.success = true;
                res.HostId = Dns.GetHostName();
                res.Message = "通道状态变化推送";
                res.ClientId = ServerConfig.CloudClientID;
                res.RequestID = DriverMng._RequestID++.ToString();
                JObject jo = new JObject { { "Name", chan.Name },
                                                       { "Propery", "Enable" },
                                                       { "Value", chan.Enable },
                                                       { "ValueType", 3 },
                                                       { "Timestamp", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") },
                                                       { "IOServerID",ServerConfig.CloudClientID },
                                                       { "ChlID", chan.ID}
                                                     };
                JArray ja = new JArray();
                ja.Add(jo);
                res.data = ja;
                return MqttController.PublishChlPropChanged(JsonConvert.SerializeObject(res));
            }else
            {

                var msg = new GHIOTMsg()
                {
                    code = "",
                    msgId = IdWorker.GetId(),
                    msgTime = IdWorker.GetTime(),
                    key = ServerConfig.CloudClientID,
                };
                JObject jo = new JObject { { "Name", chan.Name },
                                                       { "Propery", "Enable" },
                                                       { "Value", chan.Enable },
                                                       { "ValueType", 3 },
                                                       { "Timestamp", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") },
                                                       { "IOServerID",ServerConfig.CloudClientID },
                                                       { "ChlID", chan.ID}
                                                     };
                JArray ja = new JArray();
                ja.Add(jo);
                msg.data = ja;
                return MqttController.PublishChlPropChanged(JsonConvert.SerializeObject(msg));

            }
        }
        public static bool PublishCtrlEnable(IController con)
        {
            if (ServerConfig.MqttVer == 1)
            {
             
                JObject jo = new JObject { { "Name", con.Name },
                                                           { "Propery", "Enable" },
                                                           { "Value", con.Enable },
                                                           { "ValueType", 3 },
                                                           { "Timestamp", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") },
                                                           { "IOServerID",ServerConfig.CloudClientID },
                                                           { "ChlID", con.ChannelObject.ID},
                                                           { "CtrlID", con.ID}

                                                            };
                JArray ja = new JArray();
                ja.Add(jo);

                GHMNSResponse res = new GHMNSResponse();
                res.success = true;
                res.HostId = Dns.GetHostName();
                res.ClientId = ServerConfig.CloudClientID;
                res.RequestID = CloudController._RequestID++.ToString();
                res.Message = "控制器状态变化推送";
                res.data = ja;
                return MqttController.PublishCtrlPropChanged(JsonConvert.SerializeObject(res));
            }else
            {
                var msg = new GHIOTMsg()
                {
                    code = "",
                    msgId = IdWorker.GetId(),
                    msgTime = IdWorker.GetTime(),
                    key = ServerConfig.CloudClientID,
                };
                JObject jo = new JObject { { "Name", con.Name },
                                                           { "Propery", "Enable" },
                                                           { "Value", con.Enable },
                                                           { "ValueType", 3 },
                                                           { "Timestamp", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") },
                                                           { "IOServerID",ServerConfig.CloudClientID },
                                                           { "ChlID", con.ChannelObject.ID},
                                                           { "CtrlID", con.ID}

                                                            };
                JArray ja = new JArray();
                ja.Add(jo);
                msg.data = ja;
                return MqttController.PublishCtrlPropChanged(JsonConvert.SerializeObject(msg));

            }

        }
        public static bool PublishCtrlPropChanged(string payload)
        {
            string topic = string.Format(topic_up_ctrl_prop_change, ServerConfig.CloudClientID);
            return Publish(topic, payload);
        }



        static void SubscribeLogGetCallback(string Playload)
        {
            try
            {
                JObject jo = JObject.Parse(Playload);
                if (jo["Enable"].ToString().ToLower() == "true")
                {
                    mng.sentLogEnable = true;
                    mng.SentHisLogs();
                }
                else
                {
                    mng.sentLogEnable = false;
                }

            }
            catch(Exception ex) {
                Console.WriteLine(ex.ToString());
            }
        }
        static void SubscribeWriteValueCallback(string content)
        {
            try
            {
                if(ServerConfig.WriteLog)
                    Logger.GetInstance().LogMsg("收到变量写值批令(原始内容)：" + content);

                if (ServerConfig.RunningState != ServerStateEnum.RUNING) return;

                JObject jo = JsonConvert.DeserializeObject<JObject>(content);

                if (jo["msgId"] == null)//V1.0
                {

                    GHRTDBWriteValueRequest req = JsonConvert.DeserializeObject<GHRTDBWriteValueRequest>(content);
                    if (req != null)
                    {

                        foreach (var item in req.RequestItems)
                        {
                            IVariable var = Rtdb.GetVariableByID(item.chlKey, item.ctrlKey, item.varKey);
                            if (var != null && !string.IsNullOrEmpty(item.propName))
                            {
                                if (ServerConfig.WriteLog)
                                    Logger.GetInstance().LogMsg("收到变量写值批令（解码到变量）：" + var.ControllerObject.ChannelObject.Name + "/" + var.ControllerObject.Name + "/" + var.Name + " 值：" + item.value);
                                // string clientId, string batchDefinitionId,string id, int area, int level, string propName, string value, string desc
                                var.WriteValue(req.clientId, req.batchDefinitionId, item.id, req.area, req.level, item.propName, item.value, item.desc);
                            }
                        }
                    }
                }else  //V2.0
                {
                    if(jo["data"]!=null)
                    {
                        List<GHRTDBWriteValueV2> datas = JsonConvert.DeserializeObject<List<GHRTDBWriteValueV2>>(jo["data"].ToString());
                        if(datas!=null)
                        {
                            foreach(var item in datas)
                            {
                                IVariable var = Rtdb.GetVariableByID(item.chlKey, item.ctrlKey, item.varKey);
                                if (var != null && !string.IsNullOrEmpty(item.propName))
                                {
                                    if (ServerConfig.WriteLog)
                                        Logger.GetInstance().LogMsg("收到变量写值批令（解码到变量）：" + var.ControllerObject.ChannelObject.Name + "/" + var.ControllerObject.Name + "/" + var.Name + " 值：" + item.value);
                                    // string clientId, string batchDefinitionId,string id, int area, int level, string propName, string value, string desc
                                     var.WriteValue(item.clientId, item.batchDefinitionId, item.id,0, item.level, item.propName, item.value, item.desc);
                                }

                            }
                        }


                    }

                }
                Logger.GetInstance().LogMsg("写值成功:" + content);
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError("写值操作失败:" + ex.ToString());
            }
        }
        static void SubscribeExecCommandCallback(string content)
        {
            try
            {
                // if (ServerConfig.RunningState == false) return;
                if(ServerConfig.WriteLog)
                     Logger.GetInstance().LogMsg("远程自定义指令:" + content);
                JObject jo = JsonConvert.DeserializeObject<JObject>(content);
                if (jo != null)
                {
                    if (jo["requestItems"] != null)//V1协议
                    {
                        GHCMDResponse req = JsonConvert.DeserializeObject<GHCMDResponse>(content);
                        if (req != null)
                        {

                            foreach (var item in req.requestItems)
                            {
                                GHCMDResponseV2 v = new GHCMDResponseV2();
                                v.cmdCode = item.cmdCode;
                                v.chlKey = item.chlKey;
                                v.ctrlKey = item.ctrlKey;
                                v.varKey = item.varKey;
                                v.iosvrKey = item.iosvrKey;
                                v.level = req.level;
                                v.cmdParams = item.cmdParams;
                                v.clientId = req.clientId;
                                v.batchDefinitionId = req.batchDefinitionId;
                                HandleUserCmd(v);
                            }
                        }
                    }
                    else
                    {
                        List<GHCMDResponseV2> datas = JsonConvert.DeserializeObject<List<GHCMDResponseV2>>(jo["data"].ToString());
                        if (datas != null)
                        {
                            foreach (var item in datas)
                            {

                                HandleUserCmd(item);

                            }

                        }
                    }

                }

               

            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError("远程自定义指令:" + ex.ToString());
            }
        }
       static void HandleUserCmd(GHCMDResponseV2 item)
        {
            switch (item.cmdCode)
            {
                case 100:
                    {
                        mng.AddOperationLog(Severity.信息.ToString(), StrConst.TITLE_SYS, "远程指令", "远程指令:清除变量优先级，" + item.chlKey + ":" + item.ctrlKey + ":" + item.varKey);
                        IVariable var = Rtdb.GetVariableByID(item.chlKey, item.ctrlKey, item.varKey);
                        if (var != null)
                        {

                            var.ControllerObject.ChannelObject.ExecCommand(var.ControllerObject, item.cmdCode, item.cmdParams);
                        }
                        break;
                    }
                case 101:
                    {
                        mng.AddOperationLog(Severity.信息.ToString(), StrConst.TITLE_SYS, "远程指令", "远程指令:设置变量优先级，" + item.chlKey + ":" + item.ctrlKey + ":" + item.varKey);

                        IVariable var = Rtdb.GetVariableByID(item.chlKey, item.ctrlKey, item.varKey);
                        if (var != null)
                        {

                            var.ControllerObject.ChannelObject.ExecCommand(var.ControllerObject, item.cmdCode, item.cmdParams);
                        }
                    }
                    break;
                case 200://下载
                    if (ServerConfig.RunningState != ServerStateEnum.STOPED)
                    {
                        mng.AddOperationLog(Severity.信息.ToString(), StrConst.TITLE_SYS, "远程指令", "远程指令失败，运行状态禁止载入配置");
                        Logger.GetInstance().LogError("远程指令失败，运行状态禁止载入配置，文件");
                    }
                    else
                    {
                        mng.CmdLoadConfig(item.cmdParams[0], item.cmdParams[1]);
                        Logger.GetInstance().LogMsg("远程指令成功:载入配置，文件:" + item.cmdParams[1]);

                    }
                    break;
                case 201://停止运行
                    if (ServerConfig.RunningState == ServerStateEnum.RUNING)
                    {
                        ProjectMng.SaveToXml();
                        mng.AddOperationLog(Severity.信息.ToString(), StrConst.TITLE_SYS, "远程指令", "执行远程指令:停止运行");
                        mng.DisConnectedDrive();
                        Logger.GetInstance().LogMsg("执行远程指令:停止运行");
                    }
                    else
                    {
                        mng.AddOperationLog(Severity.信息.ToString(), StrConst.TITLE_SYS, "远程指令", "远程指令失败:停止运行，程序已经处于停止状态！");
                        Logger.GetInstance().LogMsg("远程指令失败:停止运行，程序已经处于停止状态！");
                    }
                    break;
                case 202://启动
                    if (ServerConfig.RunningState == ServerStateEnum.STOPED)
                    {
                        mng.AddOperationLog(Severity.信息.ToString(), StrConst.TITLE_SYS, "远程指令", "执行远程指令:启动运行");
                        mng.ConnectedDrive();
                        Logger.GetInstance().LogMsg("执行远程指令:启动运行！");

                    }
                    else
                    {
                        mng.AddOperationLog(Severity.信息.ToString(), StrConst.TITLE_SYS, "远程指令", "远程指令失败:启动运行，程序已经处于运行状态！");
                        Logger.GetInstance().LogMsg("远程指令失败:启动运行，程序已经处于运行状态！");
                    }

                    break;
                case 203://重启
                    {
                        System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
                        System.Diagnostics.Process.GetCurrentProcess().Kill();
                    }
                    break;
                case 204://通讯启用
                    {
                        mng.AddOperationLog(Severity.信息.ToString(), StrConst.TITLE_SYS, "远程指令", "执行远程指令:启用通道");
                        IChannel chl = Rtdb.GetChannelByID(item.chlKey);
                        if (chl != null)
                        {
                            chl.Enable = true;
                            PublishChlEnable(chl);
                            Logger.GetInstance().LogMsg("执行远程指令:启用通道！" + chl.Name);
                        }


                    }
                    break;
                case 205://通讯禁用
                    {
                        mng.AddOperationLog(Severity.信息.ToString(), StrConst.TITLE_SYS, "远程指令", "执行远程指令:禁用通道");
                        IChannel chl = Rtdb.GetChannelByID(item.chlKey);
                        if (chl != null)
                        {
                            chl.Enable = false;
                            PublishChlEnable(chl);
                            Logger.GetInstance().LogMsg("执行远程指令:禁用通道！" + chl.Name);

                        }
                    }
                    break;
                case 206://通讯控制器启用
                    {
                        mng.AddOperationLog(Severity.信息.ToString(), StrConst.TITLE_SYS, "远程指令", "执行远程指令:启用控制器");
                        IController con = Rtdb.GetControllerByID(item.chlKey, item.ctrlKey);
                        if (con != null)
                        {
                            con.Enable = true;
                            PublishCtrlEnable(con);
                            Logger.GetInstance().LogMsg("执行远程指令:启用控制器！" + con.Name);
                        }
                    }
                    break;
                case 207://通讯控制器禁用
                    {
                        mng.AddOperationLog(Severity.信息.ToString(), StrConst.TITLE_SYS, "远程指令", "执行远程指令:禁用控制器");
                        IController con = Rtdb.GetControllerByID(item.chlKey, item.ctrlKey);
                        if (con != null)
                        {
                            con.Enable = false;
                            PublishCtrlEnable(con);
                            Logger.GetInstance().LogMsg("执行远程指令:停用控制器！" + con.Name);
                        }
                    }
                    break;
                case 208://上载
                    {
                        if (ServerConfig.RunningState == ServerStateEnum.STOPED)
                        {
                            mng.AddOperationLog(Severity.信息.ToString(), StrConst.TITLE_SYS, "远程指令", "执行远程指令:上载");
                            mng.UpLoadToCloud_V2();
                            Logger.GetInstance().LogMsg("执行远程指令: 上载!");
                        }
                        else
                        {
                            mng.AddOperationLog(Severity.信息.ToString(), StrConst.TITLE_SYS, "远程指令", "远程指令失败:上载,采集器处于非停止状态");
                            Logger.GetInstance().LogMsg("远程指令失败:上载,采集器处于非停止状态!");
                        }
                    }
                    break;
                case 209://保存
                    {
                        ProjectMng.SaveToXml();
                        mng.AddOperationLog(Severity.信息.ToString(), StrConst.TITLE_SYS, "远程指令", "执行远程指令:保存配置");
                        ProjectMng.SaveToXml();
                        Logger.GetInstance().LogMsg("执行远程指令: 保存配置!");

                    }
                    break;
                case 301://报警主机子系统布防、撤防、消警
                case 320://报警主机防区布防、撤防、旁路、旁路恢复
                    {
                        IChannel ch = Rtdb.GetChannelByID(item.chlKey);
                        IController con = Rtdb.GetControllerByID(item.chlKey, item.ctrlKey);
                        if (ch != null && con != null)
                            ch.ExecCommand(con, item.cmdCode, item.cmdParams);
                        break;

                    }
                default:
                    {
                        IChannel ch = Rtdb.GetChannelByID(item.chlKey);
                        IController con = Rtdb.GetControllerByID(item.chlKey, item.ctrlKey);
                        if (ch != null && con != null)
                            ch.ExecCommand(con, item.cmdCode, item.cmdParams);
                    }
                    break;

            }
        }
        void SubscribeLiveValueCallback(string content)
        {
            if (ServerConfig.RunningState != ServerStateEnum.RUNING) return;
            try
            {

                List<string> strlist = JsonConvert.DeserializeObject<List<string>>(content);

                UpdateClientSubscribeVars(strlist);

            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }
        }
        void UpdateClientSubscribeVars(List<string> list)
        {
            {

                string[] arr = CloudController._clientSubscribeVars.Keys.ToArray();
                foreach (string s in arr)
                {
                    if (!list.Contains(s))
                    {
                        CloudController._clientSubscribeVars.TryRemove(s, out DateTime dt);
                    }
                }

                foreach (string s in list)
                {
                    CloudController._clientSubscribeVars.AddOrUpdate(s, DateTime.Now, (key, value) => value = DateTime.Now); //加一个时间，没有更新订阅1分钟后自动移除

                }

            }
        }

        void SubscribeAlarmProcessCallback(string content)
        {
            //Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss fff") + " SubscribeAlarmProcessCallback:" + content);
            if (ServerConfig.RunningState != ServerStateEnum.RUNING) return;
            try
            {
                // GHBaseResponse res = JSONFormatter.Deserialize<GHBaseResponse>(content);

                JObject jo = JObject.Parse(content);

                var chal = jo["AlarmChannel"].ToString();
                var contr = jo["AlarmController"].ToString();
                var varkey = jo["AlarmVariable"].ToString();
                IVariable variable = Rtdb.GetVariableByID(chal, contr, varkey);
                if (variable != null)
                {
                    if (variable is IAlarmVariable || variable.DeviceLabel == DeviceLabelEnum.通讯报警.ToString() || variable.DeviceLabel == DeviceLabelEnum.报警输入.ToString() || variable.DeviceLabel == DeviceLabelEnum.报警输出.ToString())
                        variable.SetDefaultValue();
                }
                if (ServerConfig.MqttVer == 1)
                {
                    GHMNSResponse responseModel = new GHMNSResponse();
                    responseModel.Code = "SubscribeAlarmProcessCallback";
                    responseModel.RequestID = chal + ":" + contr + ":" + varkey;
                    responseModel.Message = "报警复位成功";
                    //responseModel.message = "报警复位成功";
                    responseModel.HostId = ServerConfig.CloudClientID;
                    responseModel.success = true;
                    string msg = JsonConvert.SerializeObject(responseModel);
                    //string channel = $"up_reset_{ServerConfig.CloudClientID.ToLower()}";
                    //DeviceManagement.SingletonInstance.Publish(channel, msg);
                    PublishAlarmResetReply(msg);
                }else
                {
                    var msg = new GHIOTMsgReplay()
                    {
                        code = "",
                        msgId = IdWorker.GetId(),
                        msgTime = IdWorker.GetTime(),
                        key = ServerConfig.CloudClientID,
                        success=true
                    };
                    PublishAlarmResetReply(JsonConvert.SerializeObject(msg));

                }
            }
            catch (Exception ex)
            {
                if (ServerConfig.WriteLog)
                    Logger.GetInstance().LogError(ex.ToString());
            }
        }
        void SubscribeAlmRuleSetCallback(string content)
        {
            try
            {
                JObject jo = JObject.Parse(content);

                var data = jo["data"];
                var ChlID = data["ChlID"]?.ToString();
                var CtrlID = data["CtrlID"]?.ToString();
                var VarID = data["VarID"]?.ToString();
                var HH = data["HH"];
                var HHEnable = HH["enable"]?.ToString();
                var HHValue = HH["value"]?.ToString();
                var HHLevel = HH["level"]?.ToString();

                var H = data["H"];
                var HEnable = H["enable"]?.ToString();
                var HValue = H["value"]?.ToString();
                var HLevel = H["level"]?.ToString();

                var L = data["L"];
                var LEnable = L["enable"]?.ToString();
                var LValue = L["value"]?.ToString();
                var LLevel = L["level"]?.ToString();

                var LL = data["LL"];
                var LLEnable = LL["enable"]?.ToString();
                var LLValue = LL["value"]?.ToString();
                var LLLevel = LL["level"]?.ToString();

                var state = data["state"];

                var value0 = state["value0"]?.ToString();
                var value1=  state["value1"]?.ToString();
                var value2 = state["value2"]?.ToString();
                var value3 = state["value3"]?.ToString();
                var stLevel = state["level"]?.ToString();

                //var code = jo["code"] != null ? jo["code"].ToString() : "";
                var msgId = jo["msgId"] != null ? jo["msgId"].ToString() : "";
               // var key = jo["key"] != null ? jo["key"].ToString() : "";
                //var RequestID = jo["RequestID"] != null ? jo["RequestID"].ToString() : "";
                //var ClientId = jo["ClientId"] != null ? jo["ClientId"].ToString() : "";
                //var ObjectID = jo["ObjectID"] != null ? jo["ObjectID"].ToString() : "";

                var v = Rtdb.GetVariableByID(ChlID, CtrlID, VarID);
                if (v != null)
                {
                    v.AlarmHHEnable = pubFun.IsBool(HHEnable, false);
                    v.AlarmHH = pubFun.IsDouble(HHValue, 0);
                    v.AlarmLevelHH = (_AlarmLevel)pubFun.IsInt(HHLevel, 1);

                    v.AlarmHEnable = pubFun.IsBool(HEnable, false);
                    v.AlarmH = pubFun.IsDouble(HValue, 0);
                    v.AlarmLevelH = (_AlarmLevel)pubFun.IsInt(HLevel, 1);

                    v.AlarmLEnable = pubFun.IsBool(LEnable, false);
                    v.AlarmL = pubFun.IsDouble(LValue, 0);
                    v.AlarmLevelL = (_AlarmLevel)pubFun.IsInt(LLevel, 1);

                    v.AlarmLLEnable = pubFun.IsBool(LLEnable, false);
                    v.AlarmLL = pubFun.IsDouble(LLValue, 0);
                    v.AlarmLevelLL = (_AlarmLevel)pubFun.IsInt(LLLevel, 1);

                    v.AlarmLevelST = (_AlarmLevel)pubFun.IsInt(stLevel, 1);
                    v.AlarmST0 = pubFun.IsBool(value0, false);
                    v.AlarmST1 = pubFun.IsBool(value1, false);
                    v.AlarmST2 = pubFun.IsBool(value2, false);
                    v.AlarmST3 = pubFun.IsBool(value3, false);
                    //同步写入REDIS
                    //GHMNSResponse res = new GHMNSResponse();
                    //res.success = true;
                    //res.HostId = Dns.GetHostName();
                    //res.Message = "变量属性变化推送";
                    //res.ClientId = ServerConfig.CloudClientID;
                    //res.RequestID = RequestID;


                    //MqttController.PublishCtrlPropChanged(JsonConvert.SerializeObject(res));


                    Dictionary<string, string> dic = GetVarProperty(v);

                    string chanID = v.ControllerObject.ChannelObject.ID;
                    string conID = v.ControllerObject.ID;
                    var json =JsonConvert.SerializeObject(dic);
                    string key = $"{ServerConfig.CloudClientID}:{chanID}:{conID}:{v.ID}";
                    this.PublishUploadConfig("variable_add",key,json);
                    if (ServerConfig.MqttVer == 1)
                    {
                        GHMNSResponse responseModel = new GHMNSResponse();
                        responseModel.Code = "";
                        //responseModel.RequestID = RequestID;
                        responseModel.Message = "报警设置成功";
                        responseModel.HostId = ServerConfig.CloudClientID;
                        //responseModel.ObjectID = ObjectID;
                        responseModel.success = true;
                        string msg = JsonConvert.SerializeObject(responseModel);

                        string topic = string.Format(topic_up_alarm_rule_reply, ServerConfig.CloudClientID);
                        Publish(topic, msg);
                    }
                    else
                    {
                        var iotmsg = new GHIOTMsgReplay()
                        {
                            code = "",
                            key = key,
                            success = true
                        };
                        string topic = string.Format(topic_up_alarm_rule_reply, ServerConfig.CloudClientID);
                        Publish(topic, JsonConvert.SerializeObject(iotmsg));

                    }
                    SendCommEvent(Severity.信息, CommunicationEvent.COMM_INFO, "远程指令:", v.Name + "变量报警远程设置成功！");
                }
                else
                    SendCommEvent(Severity.信息, CommunicationEvent.COMM_INFO, "远程指令:" , v.Name + "变量报警远程设置失败！");
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
                SendCommEvent(Severity.信息, CommunicationEvent.COMM_INFO, "远程指令", "变量报警远程设置失败！"+ content);
            }

        }
        Dictionary<string, string> GetVarProperty(IVariable v)
        {

            Dictionary<string, string> dic = new Dictionary<string, string>();

            foreach (System.Reflection.PropertyInfo pinfo in v.GetType().GetProperties(System.Reflection.BindingFlags.Public |
    System.Reflection.BindingFlags.Instance))
            {
            
                Type dbType = pinfo.PropertyType;



                if (dbType.Name.ToLower() == "string" || dbType.IsValueType || dbType.IsPrimitive || dbType.IsEnum || dbType.Name.ToLower() == "string[]" || dbType.Name.ToLower() == "object")

                //if (pinfo != null && pinfo.CanRead && !pinfo.PropertyType.FullName.Contains("System.Collections.Generic.List`1"))
                {


                    if (pinfo.PropertyType.FullName.Contains("System.String[]"))
                    {
                        //对象属性名称
                        string name = pinfo.Name;
                        //对象属性值
                        string value = String.Empty;
                        object o = pinfo.GetValue(v, null);
                        if (o != null)
                        {
                            string[] s = o as string[];
                            foreach (string t in s)
                                value = value + t + "|";//获取对象属性值
                        }
                        //设置元素的属性值
                        dic.Add(name, value);
                    }
                    if (pinfo.PropertyType.FullName.Contains("DateTime"))
                    {
                        //对象属性名称
                        string name = pinfo.Name;
                        //对象属性值
                        string value = String.Empty;
                        DateTime o = (DateTime)pinfo.GetValue(v, null);
                        if (o != null)
                            value = o.ToString("yyyy/MM/dd HH:mm:ss");//获取对象属性值
                        else
                            value = "";
                        //设置元素的属性值
                        dic.Add(name, value);

                    }
                    else
                    {
                        //对象属性名称
                        string name = pinfo.Name;
                        //对象属性值
                        string value = String.Empty;
                        object o = pinfo.GetValue(v, null);
                        if (o != null)
                            value = o.ToString();//获取对象属性值
                        else
                            value = "";
                        //设置元素的属性值
                        dic.Add(name, value);
                    }
                }
                else if (pinfo.Name == "ChannelObject")
                {
                    IController con = v as IController;
                    dic.Add(pinfo.Name, con.ChannelObject.ID);
                    //Console.WriteLine(pinfo.Name);
                    //Console.WriteLine(pinfo.GetValue(item));
                }
                else if (pinfo.Name == "ControllerObject")
                {
                    IVariable var = v as IVariable;
                    dic.Add(pinfo.Name, v.ControllerObject.ID);
                    //Console.WriteLine(pinfo.Name);
                    //Console.WriteLine(pinfo.GetValue(item));
                }
            }
            return dic;

        }
        void OnMqttClientConnected(MqttClientConnectedEventArgs args)
        {
            isConnect = true;
            Logger.GetInstance().LogMsg("物联网平台连接成功！");
            SendCommEvent(Severity.信息, CommunicationEvent.COMM_INFO, "系统用户", "物联网平台连接成功！");
        }

        void OnMqttClientDisConnected(MqttClientDisconnectedEventArgs args)
        {
            isConnect = false;
            Logger.GetInstance().LogMsg("物联网平台连接断开！");
            SendCommEvent(Severity.信息, CommunicationEvent.COMM_INFO, "系统用户", "物联网平台连接断开！");

        }
       
        
        void RequestNtp()
        {
            try
            {
                long deviceSendTime = IdWorker.GetTime();
                JObject jo = new JObject { { "deviceSendTime", deviceSendTime } };
                GHIOTMsg msg = new GHIOTMsg
                {
                    msgId = IdWorker.GetId(),
                    msgTime = IdWorker.GetTime(),
                    data = jo,
                    key = ServerConfig.CloudClientID,
                    code = "ntp"
                };
                string topic = string.Format(topic_up_ntp_post, ServerConfig.CloudClientID);
                string payload = JsonConvert.SerializeObject(msg);
                Publish(topic, payload);
            }
            catch(Exception ex) {
                Console.WriteLine(ex.ToString());
            }

        }
        void ResponeNtp(string payload)
        {
            //{
            //    "deviceSendTime":"1571724098000",
            //    "serverRecvTime":"1571724098110",
            //    "serverSendTime":"1571724098115",
            //}
            JObject jo = JsonConvert.DeserializeObject<JObject>(payload);
            if(jo["data"]!=null)
            {
                long deviceRecvTime = IdWorker.GetTime();
                long deviceSendTime;
                long serverRecvTime;
                long serverSendTime;

                long.TryParse(jo["data"]["deviceSendTime"].ToString(), out deviceSendTime);
                long.TryParse(jo["data"]["serverRecvTime"].ToString(), out serverRecvTime);
                long.TryParse(jo["data"]["serverSendTime"].ToString(), out serverSendTime);

                long nptTime = (serverRecvTime + serverSendTime + deviceRecvTime - deviceSendTime) / 2;
                SystemTime sysTime = new SystemTime();
                DateTime datetime = IdWorker.Utc2Datetime(nptTime);
                sysTime.wYear = (ushort)datetime.Year;
                sysTime.wMonth = (ushort)datetime.Month;
                sysTime.wDay = (ushort)datetime.Day;
                sysTime.wDayOfWeek = (ushort)datetime.DayOfWeek;
                sysTime.wHour = (ushort)datetime.Hour;
                sysTime.wMinute = (ushort)datetime.Minute;
                sysTime.wSecond = (ushort)datetime.Second;
                sysTime.wMiliseconds = (ushort)datetime.Millisecond;


#if !IOPLUS

                if (IdWorker.GetTime()!= nptTime)
                    SetLocalTime(ref sysTime);

#else
                string dt = datetime.ToString("yyyy-MM-dd HH:mm:ss");
                LinuxOSHelper.SetLocaleDateTime(dt);

#endif
            }

        }
        /// <summary>
        /// Url编码
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <param name="encoding">编码格式</param>
        /// <param name="upper">特殊字符编码为大写</param>
        /// <returns></returns>
        static string UrlEncode(string str, Encoding encoding)
        {

            if (encoding == null)
            {
                encoding = UTF8Encoding.UTF8;
            }
            byte[] bytes = encoding.GetBytes(str);
            int num = 0;
            int num2 = 0;

            for (int i = 0; i < bytes.Length; i++)
            {
                char ch = (char)bytes[i];
                if (ch == ' ')
                {
                    num++;
                }
                else if (!IsUrlSafeChar(ch))
                {
                    num2++;  //非url安全字符
                }
            }

            if (num == 0 && num2 == 0)
            {
                return str;  //不包含空格和特殊字符
            }

            byte[] buffer = new byte[bytes.Length + (num2 * 2)];  //包含特殊字符，每个特殊字符转为3个字符，所以长度+2x
            int num3 = 0;
            for (int j = 0; j < bytes.Length; j++)
            {
                byte num6 = bytes[j];
                char ch2 = (char)num6;
                if (IsUrlSafeChar(ch2))
                {
                    buffer[num3++] = num6;
                }
                else if (ch2 == ' ')
                {
                    buffer[num3++] = 0x2B;  //0x2B代表 ascii码中的+，url编码时候会把空格编写为+
                }
                else
                {
                    //特殊符号转换
                    buffer[num3++] = 0x25;  //代表  %
                    buffer[num3++] = (byte)IntToHex((num6 >> 4) & 15);
                    buffer[num3++] = (byte)IntToHex(num6 & 15);

                }
            }

            return encoding.GetString(buffer);



        }

        static bool IsUrlSafeChar(char ch)
        {
            if ((((ch < 'a') || (ch > 'z')) && ((ch < 'A') || (ch > 'Z'))) && ((ch < '0') || (ch > '9')))
            {

                switch (ch)
                {
                    case '(':
                    case ')':
                    case '*':
                    case '-':
                    case '.':
                    case '!':
                        break;  //安全字符

                    case '+':
                    case ',':
                        return false;  //非安全字符
                    default:   //非安全字符
                        if (ch != '_')
                        {
                            return false;
                        }
                        break;
                }
            }
            return true;
        }

        static char IntToHex(int n)
        {
            if (n <= 9)
            {
                return (char)(n + 0x30);
            }
            return (char)((n - 10) + 0x41);
        }
        /// <summary>
        /// Writes the given object instance to a binary file.
        /// <para>Object type (and all child types) must be decorated with the [Serializable] attribute.</para>
        /// <para>To prevent a variable from being serialized, decorate it with the [NonSerialized] attribute; cannot be applied to properties.</para>
        /// </summary>
        /// <typeparam name="T">The type of object being written to the binary file.</typeparam>
        /// <param name="filePath">The file path to write the object instance to.</param>
        /// <param name="objectToWrite">The object instance to write to the binary file.</param>
        /// <param name="append">If false the file will be overwritten if it already exists. If true the contents will be appended to the file.</param>
        public static void WriteToBinaryFile<T>(string filePath, T objectToWrite, bool append = false)
        {
            using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }

        /// <summary>
        /// Reads an object instance from a binary file.
        /// </summary>
        /// <typeparam name="T">The type of object to read from the binary file.</typeparam>
        /// <param name="filePath">The file path to read the object instance from.</param>
        /// <returns>Returns a new instance of the object read from the binary file.</returns>
        public static T ReadFromBinaryFile<T>(string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (T)binaryFormatter.Deserialize(stream);
            }
        }

    }
}
