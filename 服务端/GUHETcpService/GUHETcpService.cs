using System;
using System.Collections.Generic;
using System.Text;
using GHIBMS.Common;
using GHIBMS.Server;
using System.Net.Sockets;
using System.Xml;
using System.IO;
using System.Net;
using System.Threading;
using System.Diagnostics;
using GHIBMS.Interface;
using System.Timers;

namespace GHIBMS.Server
{
    /// <summary>
    /// 客户端TCP服务
    /// </summary>
    public class GUHETcpService
    {
        #region 私有变量
        private const int MaxBuffer = 1024 * 1024 * 100; //100M
        private ServerSocket serverSocket = new ServerSocket();
        private System.Timers.Timer TimerCheckConnect;
        private static List<DataClient> dataClientList = new List<DataClient>();
        private  object synClintList = new object();
       
        private Thread threadVarPush = null;//变量发送线程
        private bool active = false;

        public GUHEDataAnalysis dataAnalysis ;
        #endregion

        #region 事件
        public delegate void activeDelegate(bool actived);
        public delegate void deactiveDelegate();
        public delegate void conncetDelegate(DataClient dc);
        public delegate void disconncetDelegate(DataClient dc);
        public delegate void receiveDelegeate(Socket socket, XmlDocument xmldoc);
        //public delegate void receiveDelegeateEx(Socket socket, String msg);

        public event activeDelegate OnActive;
        public event deactiveDelegate OnDeactive;
        public event conncetDelegate OnConnect;
        public event disconncetDelegate OnDisconnect;
        //public event receiveDelegeate OnReceive;
       // public event receiveDelegeateEx OnReceiveEx;
        public delegate void OnWrite2ClientDelegate(BaseVariable var);
        public event CommMsgDelegate OnCommMsg;
        #endregion

        #region 构造函数

        public GUHETcpService()
        {
            //分控服务
            serverSocket.Encoding = null;
            serverSocket.OnActive += new ServerSocket.activeDelegate(dataServer_OnActive);
            serverSocket.OnDeactive += new ServerSocket.deactiveDelegate(dataServer_OnDeactive);
            serverSocket.OnConnect += new ServerSocket.conncetDelegate(dataServer_OnConnect);
            serverSocket.OnDisconnect += new ServerSocket.disconncetDelegate(dataServer_OnDisconnect);
            //serverSocket.OnReceive += new ServerSocket.receiveDelegeate(dataServer_OnReceive);
            serverSocket.OnReceiveEx += new ServerSocket.receiveDelegeateEx(dataServer_OnReceiveEx);
            dataAnalysis = new GUHEDataAnalysis(this);
            dataAnalysis.OnCommMsg += new GHIBMS.Interface.CommMsgDelegate(SendCommMsg);
            TimerCheckConnect = new System.Timers.Timer();
            TimerCheckConnect.Interval = 5000;
            TimerCheckConnect.Elapsed += new System.Timers.ElapsedEventHandler(TimerCheckConnect_Elapsed);

        }
        void TimerCheckConnect_Elapsed(object sender, ElapsedEventArgs e)
        {
           
                DataClient[] arr =GetDataClientArray ();
                foreach (DataClient dc in arr)
                {
                    if (dc.HasLogin)
                    {
                        //已经登录15秒内必需收到数据检测包，否则切断连接
                        if (pubFun.DateDiff(dc.DateStamp, DateTime.Now) > 60)
                        {
                            if (OnCommMsg != null)
                            {
                                string msg = "已经登录客户端连接活动超时60秒，连接被切断。IP:" + dc.ClientIp;
                                OnCommMsg(this, Severity.错误, CommunicationEvent.COMM_INFO, msg, "");
                            }
                            dc.CloseConnection();
                            RemoveDataClient(dc);
                        }
                    }
                    else
                    {
                        //已经未登录60*5秒内必需收到数据检测包，否则切断连接
                        if (pubFun.DateDiff(dc.DateStamp, DateTime.Now) > 60*5)
                        {
                            string msg = "未登录客户端连接活动超时60*5秒，连接被切断。IP:" + dc.ClientIp;
                            OnCommMsg(this, Severity.错误, CommunicationEvent.COMM_INFO, msg, "");
                            dc.CloseConnection();
                            RemoveDataClient(dc);
                        }


                    }
                }
            
        }

        #endregion

        #region 属性
 
        private object synDateClient = new object();
        /// <summary>
        /// 添加一个客户连接
        /// </summary>
        /// <param name="dc"></param>
        private void AddDataClient(DataClient dc)
        {
            lock(synDateClient)
            {
                dataClientList.Add(dc);
            }
        }
        /// <summary>
        /// 删除一个客户连接
        /// </summary>
        /// <param name="socket"></param>
        private void RemoveDataClient(DataClient client)
        {
            try
            {
                lock (synDateClient)
                {
                    if (dataClientList.Contains(client))
                            dataClientList.Remove(client);
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }
        }
        /// <summary>
        /// 清除所有客户连接
        /// </summary>
        private void RemoveAllDataClient()
        {
            lock (synDateClient)
            {
                dataClientList.Clear();
            }
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 派发服务启动事件
        /// </summary>
        /// <param name="actived"></param>
        private void dataServer_OnActive(bool actived)
        {
            if (OnActive!=null)
                OnActive(actived);
        }
        /// <summary>
        /// 派发服务关闭事件
        /// </summary>
        private void dataServer_OnDeactive()
        {
            if (OnDeactive != null)
                OnDeactive();
        }
        /// <summary>
        /// 客户端连接事件处理
        /// </summary>
        /// <param name="serverScocket"></param>
        /// <param name="socket"></param>
        private void dataServer_OnConnect(ServerSocket serverScocket, Socket socket)
        {
            try
            {
                DataClient dc = new DataClient(serverScocket,socket);
                dc.DateStamp = DateTime.Now;
                //dc.OnSentError +=new DataClient.sentErrorDelegate(dc_OnSentError);
                IPAddress ip = ((System.Net.IPEndPoint)socket.RemoteEndPoint).Address;
                int port = ((System.Net.IPEndPoint)socket.RemoteEndPoint).Port;
                dc.ClientIp = ip.ToString();
                dc.ClientPort = port;
                AddDataClient(dc);
                if (OnConnect != null)
                    OnConnect(dc);
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());  
            }
        }
       /// <summary>
       /// 客户端断开连接事件处理
       /// </summary>
       /// <param name="socket"></param>
        private void dataServer_OnDisconnect(Socket socket)
        {
            try
            {
                
                DataClient dc = GetDataClient(socket);
                if (dc != null)
                {
                    if (OnDisconnect != null)
                    OnDisconnect(dc);
                    RemoveDataClient(dc);
                }

            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
                Console.WriteLine(ex.ToString());
            }
        }
        /// <summary>
        /// 字节流内关键字的定位
        /// </summary>
        /// <param name="data"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public  List<int> indexOf(byte[] data, byte[] pattern)
        {
            List<int> matchedPos = new List<int>();

            if (data.Length == 0 || data.Length < pattern.Length) return matchedPos;

            int end = data.Length - pattern.Length;
            bool matched = false;

            for (int i = 0; i <= end; i++)
            {
                for (int j = 0; j < pattern.Length || !(matched = (j == pattern.Length)); j++)
                {
                    if (data[i + j] != pattern[j]) break;
                }
                if (matched)
                {
                    matched = false;
                    matchedPos.Add(i);
                }
            }
            return matchedPos;
        }
        /// <summary>
        /// 客户端数据接收
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="data"></param>
        private void dataServer_OnReceiveEx(Socket socket, byte[] data)
        {
            //Console.WriteLine(data);
            byte[] HEAD = Encoding.UTF8.GetBytes("<?xml");
            byte[] FOOT = Encoding.UTF8.GetBytes("</GHISMS>");
            string sHEAD = "<?xml";
            string sFOOT = "</GHISMS>";
            try
            {
                //查找到对应的DataClient对象，一个客户端连接对应一个对象
                DataClient dc = GetDataClient(socket);
                if (dc == null) return;
                //每个DataClient对应一个接收缓冲区
                List<byte> bufferList = dc.ReceiveBuffer;
                bufferList.AddRange(data);
                //数据合法性分析，是否包括协议报文的结尾标识
                if (indexOf(data, FOOT).Count > 0)
                {
                    string strReciev = Encoding.UTF8.GetString(bufferList.ToArray());
                    bufferList.Clear();
                    if (strReciev.IndexOf(sHEAD) >= 0)
                    {
                        //修剪数据包至开始标识
                        while (strReciev.IndexOf(sHEAD) != 0)
                            strReciev = strReciev.Substring(strReciev.IndexOf(sHEAD));

                        //截取开始和结束之间的合法包
                        string goodReciev = strReciev.Substring(0, strReciev.LastIndexOf(sFOOT) + FOOT.Length);
                        //截取合法包后剩余的字符
                        string notgootReciev = strReciev.Substring(strReciev.LastIndexOf(sFOOT) + FOOT.Length);
                        //以开始字符为准，分割合法的报文
                        String[] lines = goodReciev.Split(new string[] { sHEAD }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (String line in lines)
                            if (line != "")
                            {
                                XmlDocument doc = new XmlDocument();
                                doc.LoadXml("<?xml" + line);

                                if (doc.DocumentElement.Name == "GHISMS")
                                {
                                    if (doc.GetElementsByTagName("command") != null)
                                    {
                                        //处理一个合法报文
                                        dataAnalysis.AnalysisReceive(socket, doc);
                                    }
                                }
                            }
                        //将未处理报文再次加入
                        if (!string.IsNullOrEmpty(notgootReciev))
                            bufferList.AddRange(Encoding.UTF8.GetBytes(notgootReciev));
                    }
                    else
                    {
                        bufferList.Clear();
                        strReciev = "";

                    }

                }
                else  //缓冲区内无结束标识
                {
                    //如果也不包括开始标识，认为是无效包，清除
                    if (indexOf(bufferList.ToArray(), HEAD).Count == 0)
                    {
                        bufferList.Clear();
                    }
                    else
                    {
                        //无包尾超过最大值100M清除
                        if (bufferList.Count > MaxBuffer)
                            bufferList.Clear();

                    }
                }


            }
            catch (Exception ex)
            {
               Logger.GetInstance().LogError(ex.ToString());
            }

        }
        /*private void dataServer_OnReceiveEx(Socket socket, String msg)
        {
            DataClient dc = GetDataClient(socket);
            if (dc == null) return;
            dc.RecStr.Append(msg);

            string strReciev = dc.RecStr.ToString();
            if (strReciev.IndexOf(StrConst.HEAD) >= 0)
            { 
                //没有FOOT，未完整报文，下次处理
                if (strReciev.IndexOf(StrConst.FOOT) <= 0) return;
                while (strReciev.IndexOf(StrConst.HEAD) != 0)
                    strReciev = strReciev.Substring(strReciev.IndexOf(StrConst.HEAD));
              
                //合法报文
                string goodReciev = strReciev.Substring(0, strReciev.LastIndexOf(StrConst.FOOT) + StrConst.FOOT.Length);
                //未完整报文
                string notgootReciev = strReciev.Substring(strReciev.LastIndexOf(StrConst.FOOT) + StrConst.FOOT.Length);

                String[] lines = goodReciev.Split(new string[] { StrConst.HEAD }, StringSplitOptions.RemoveEmptyEntries);
                foreach (String line in lines)
                    if (line != "")
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(StrConst.HEAD + line);

                        if (doc.DocumentElement.Name == "GHISMS")
                        {
                            if (doc.GetElementsByTagName("command") != null)
                            {
                                dataAnalysis.AnalysisReceive(socket, doc);
                            }
                        }
                    }
                strReciev = string.Empty;
                dc.RecStr.Remove(0, dc.RecStr.Length);
                dc.RecStr.Append(notgootReciev);
            }
            else
            {
                //清除无HEAD的报文
                dc.RecStr.Remove(0, dc.RecStr.Length);
            }


           
        }*/
        /// <summary>
        /// 向UI届面发送信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="severity">信息严重程序</param>
        /// <param name="commMsgType">信息类型</param>
        /// <param name="wParamm">内容</param>
        /// <param name="lParamm">内容</param>
        private void SendCommMsg(object sender,Severity severity, CommunicationEvent commMsgType, string wParamm, string lParamm)
        {
            if (OnCommMsg != null)
            {
                OnCommMsg(this,severity, commMsgType, wParamm, lParamm);
            }
        }
        #endregion

        #region 公有方法
        /// <summary>
        /// 端口
        /// </summary>
        public int Port
        {
            get { return serverSocket.Port; }
            set
            {
                serverSocket.Port = value;
            }
        }
        /// <summary>
        /// 启动服务
        /// </summary>
        public void Start()
        {
            active = true;
            serverSocket.Port = ServerConfig.DataPort;
            serverSocket.Active = true;
            try
            {
                threadVarPush = new Thread(new ThreadStart(PushDataToClient));
                threadVarPush.IsBackground = true;
                //线程名，调试用
                threadVarPush.Name = "varPush_thread";
                threadVarPush.Start();
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
                Console.WriteLine(ex.ToString());
            }
            TimerCheckConnect.Start();

        }
        /// <summary>
        /// 停止服务
        /// </summary>
        public void Stop()
        {
            try
            {

                TimerCheckConnect.Stop();
                active = false;
                serverSocket.Active = false;
                Thread.Sleep(500);
                //if (threadVarPush != null && threadVarPush.IsAlive)
                //{
                //    try
                //    {
                //        threadVarPush.Abort();
                //    }
                //    catch { }
                //}

                RemoveAllDataClient();
                //threadVarPush = null;
            }
            catch
            {

            }

        }
       
        /// <summary>
        /// 线程内方法：发送变量到客户端
        /// </summary>
        private void PushDataToClient()
        {
            try
            {
                while (active)
                {
                    //Debug.WriteLine("varPush_thread start");

                    DataClient[] tempList=GetDataClientArray();
                   
                    foreach (DataClient dc in tempList)
                    {
                        try
                        {
                            //先发送报警
                            dc.SendAlarmToClient();
                            dc.Send();
                        }
                        catch { }
                        }
                    try
                    {
                        Thread.Sleep(200);
                    }
                    catch { }
                }
                threadVarPush = null;
            }catch(Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }
        }
        /// <summary>
        /// 判断该变量客户端是否订阅
        /// </summary>
        /// <param name="var"></param>
        public void JugdgeIsClientSubscrible(IVariable var)
        {
            try
            {
                DataClient[] tempList = GetDataClientArray();
                foreach (DataClient dc in tempList)
                {
                    if (dc.IsSubscribePoint(var.Name))
                    {
                        //Debug.WriteLine("NewService JugdgeIsClientSubscrible" + var.Name);
                        //如果在则发送到客户端
                        dc.AddVarPushList(var);
                    }

                }
            }
            catch (Exception e)
            {
                Logger.GetInstance().LogError(e.ToString());
                Console.WriteLine(e.ToString());
            }

          

        }
        /// <summary>
        /// 客户端群发
        /// </summary>
        /// <param name="s"></param>
        public void Send2AllClient(string s)
        {
            try
            {
                DataClient[] tempList = GetDataClientArray();
                foreach (DataClient dc in tempList)
                {
                    try
                    {
                        dc.Send(Encoding.UTF8.GetBytes(s));
                    }
                    catch (SocketException ex)
                    {
                        dc.CloseConnection();
                        Logger.GetInstance().LogError(ex.ToString());

                    }
                }
               
            }
            catch (Exception e)
            {
                Logger.GetInstance().LogError(e.ToString());
                Console.WriteLine(e.ToString());
            }
        }
        /// <summary>
        /// 报警客户端群发
        /// </summary>
        /// <param name="alm"></param>
        public void Send2AllClient(AlarmMessage alm)
        {
            try
            {
                DataClient[] tempList = GetDataClientArray();
                foreach (DataClient dc in tempList)
                {
                    dc.AddAlmPushList(alm);
                }

            }
            catch (Exception e)
            {
                Logger.GetInstance().LogError(e.ToString());
                Console.WriteLine(e.ToString());
            }
        }
        //public void Send(Socket socket,string s)
        //{
        //    try
        //    {
        //        DataClient[] tempList;
        //        lock (synDateClient)
        //        {
        //            tempList = dataClientList.ToArray();
        //        }
        //        foreach (DataClient dc in tempList)
        //        {
        //            if (dc.Socket == socket)
        //            {

        //                try
        //                {
        //                    dc.Send(s);
        //                }
        //                catch (SocketException ex)
        //                {
        //                    try
        //                    {
        //                        dataClientList.Remove(dc);
        //                        if (dc != null && dc.Socket != null)
        //                            dc.Socket.Close();


        //                    }
        //                    catch { }
        //                }
        //                break;
        //            }
        //        }
               
        //    }  
        //    catch (Exception e)
        //    {
        //        Logger.GetInstance().LogError(e.ToString());
        //        Console.WriteLine(e.ToString());
        //    }
        //}
        /// <summary>
        /// 获取所有客户端
        /// </summary>
        /// <returns></returns>
        public DataClient[] GetDataClientArray()
        {
            lock (synDateClient)
            {
                return dataClientList.ToArray();
            }
        }
        /// <summary>
        /// 获取指定客户端
        /// </summary>
        /// <param name="st"></param>
        /// <returns></returns>
        public DataClient GetDataClient(Socket st)
        {
            try
            {
                if (st == null) return null;
                lock (synDateClient)
                {
                    
                    DataClient[] dcList = dataClientList.ToArray();
                    foreach (DataClient dc in dcList)
                    {
                        if (object.ReferenceEquals(dc.Socket,st))
                            return dc;

                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        /// <summary>
        /// 向所有客户端群发事件信息
        /// </summary>
        /// <param name="title"></param>
        /// <param name="type"></param>
        /// <param name="msg"></param>
        public void SendEventMsg(string title, string type, string msg)
        {
            try
            { 
               
                DataClient[] dcList = GetDataClientArray();
                foreach (DataClient dc in dcList)
                {
                    dc.SendMessageToClientEventList(title, type, msg);

                }
            }
            catch (Exception e)
            {
                Logger.GetInstance().LogError(e.ToString());
                Console.WriteLine(e.ToString());
            }

        }
     
        #endregion

    }
}
