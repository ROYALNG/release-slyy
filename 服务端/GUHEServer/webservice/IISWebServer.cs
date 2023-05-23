using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using System.Net.Sockets;
using System.Web;
using System.Collections;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System.Text.RegularExpressions;
using GHIBMS.Common;
using System.Collections.Specialized;


namespace GHIBMS.Server
{
    /// <summary>
    ///  访问事件
    /// </summary>
    public delegate void IISAcceptSocket(Socket Socket_, Method Method_, IISWebSend IISWebSend_,string Header_, string Body_);
    /// <summary>
    /// 访问方式
    /// </summary>
    public enum Method
    {
        GET = 0,//获取方式
        POST = 1,//提交方式
        NULL = 2//未知
    }


    /// <summary>
    /// 建立IIS服务器(方式:POST GET)  
    /// 建于: 2010-12-12 
    /// 作者: kevery / 
    /// </summary>
    public class IISWebServer
    {
        public event IISAcceptSocket IISAS = null;

        private TcpListener myListener;
        private int m_port = 8080; // 选者任何闲置端口 
        private FormMain mainfrm;

        public FormMain frmmain
        {
            set { mainfrm = value; }
        }
    
        /// <summary>
        /// 端口
        /// </summary>
        public int Port
        {
            get
            {
                return m_port;
            }
            set
            {
                m_port = value;
            }
        }

        string m_MyWebServerRoot = "";
        /// <summary>
        /// 本地网站虚拟目录
        /// </summary>
        public string MyWebServerRoot
        {
            get
            {
                return m_MyWebServerRoot;
            }
            set
            {
                m_MyWebServerRoot = value;
            }
        }

        bool m_Starting = false;
        /// <summary>
        /// 正在运行中.
        /// </summary>
        public bool Starting
        {
            get
            {
                return m_Starting;
            }
        }

        Thread th = null;
        public void Start()
        {
            if (Starting)
            {
                return;
            }

            m_Starting = true;//开始运行

            try
            {
                //开始兼听端口 
                //IPAddress IPAddress_=new IPAddress()
                myListener = new TcpListener(Port);
                myListener.Start();

                //同时启动一个兼听进程 'StartListen' 
                Thread th = new Thread(new ThreadStart(StartListen));
                th.IsBackground = true;
                th.Start();

            }
            catch (Exception e)
            {
                Console.WriteLine("监听端口时发生错误 :" + e.ToString());
            }
        }

        public void Stop()
        {
            m_Starting = false;//停止
            try
            {
                th.Abort();
            }
            catch
            {
            }

            try
            {
                myListener.Stop();
            }
            catch { }
        }

        private void StartListen()
        {
            IISWebSend IISWebSend_ = null;
            while (Starting)
            {
                try
                {
                    //接受新连接 
                    Socket mySocket = myListener.AcceptSocket();

                    if (mySocket.Connected)
                    {
                        IISWebSend_ = new IISWebSend(mySocket, MyWebServerRoot,mainfrm);
                        IISWebSend_.IISAS += new IISAcceptSocket(IISWebSend__IISAS);
                        IISWebSend_.Start();
                    }
                }
                catch
                {
                }
            }

        }

        void IISWebSend__IISAS(Socket Socket_, Method Method_, IISWebSend IISWebSend_, string Header_, string Body_)
        {
            if (IISAS != null)
            {
                IISAS(Socket_, Method_, IISWebSend_,Header_, Body_);
            }
        }
    }

    /// <summary>
    /// 读取远程端提交数据及向远程端发数据(方式:POST GET)  
    /// 建于: 2010-12-12 
    /// 作者: kevery 
    /// </summary>
    public class IISWebSend
    {
        /// <summary>
        /// 版本,如:HTTP/1.1  一般情况下系统自动设置
        /// </summary>
        public string Version_ = "HTTP/1.1";

        /// <summary>
        /// 发送的数据文件类型,如:text/html   默认为:text/html
        /// </summary>
        public string MIMEHeader_ = "text/html";

        /// <summary>
        /// IIS反应状态代号,如: 404 Not Found  默认为: 202 OK
        /// </summary>
        public string StatusCode_ = " 202 OK";

        /// <summary>
        /// 发送的数据
        /// </summary>
        public string SendMessage_ = "";

        /// <summary>
        /// 访问方式
        /// </summary>
        Method Method_ = Method.NULL;

        /// <summary>
        /// 使用编码
        /// </summary>
        Encoding Encoding_ = Encoding.UTF8;

        /// <summary>
        /// 远程访问事件
        /// </summary>
        public event IISAcceptSocket IISAS = null;

        /// <summary>
        /// 连接对像
        /// </summary>
        public Socket mySocket = null;

        public IISWebSend(Socket mySocket_, string MyWebServerRoot_, FormMain frm)
        {
            mySocket = mySocket_;
            MyWebServerRoot = MyWebServerRoot_;
            frmmain = frm;
        }

        string m_MyWebServerRoot = "";
        /// <summary>
        /// 本地网站虚拟目录
        /// </summary>
        public string MyWebServerRoot
        {
            get
            {
                return m_MyWebServerRoot;
            }
            set
            {
                m_MyWebServerRoot = value;
            }
        }
        private FormMain mainfrm;

        public FormMain frmmain
        {
            set { mainfrm = value; }
        }

        Thread th = null;
        public void Start()
        {
            th = new Thread(new ThreadStart(StartListen));
            th.IsBackground = true;
            th.Start();
        }

        public void Stop()
        {
            try
            {
                th.Abort();
            }
            catch
            {
            }
        }

        private void StartListen()
        {
            try
            {
                StartListen_Evnt();
            }
            catch
            {
                mySocket.Close();
            }
        }

        private void StartListen_Evnt()
        {
            try
            {
                //mySocket

                int iStartPos = 0;
                String sRequest;  //WEB请求头 0--HTTP/1.1
                String sDirName;  //WEB请求目录名
                String sRequestedFile; //WEB请求文件
             
                /////////////////////////////////////注意设定你自己的虚拟目录///////////////////////////////////// 
                String sMyWebServerRoot = MyWebServerRoot; //设置你的虚拟目录 
                ////////////////////////////////////////////////////////////////////////////////////////////////// 
                String sLocalDir; //请求的磁盘文件目录 sMyWebServerRoot+sDirName
                String sPhysicalFilePath = ""; //请求的磁盘文件物理路径
                String sResponse = "";
                String sPostRequst = "";
                //Console.WriteLine("Socket Type " + mySocket.SocketType);

                string sBuffer = "";

                if (!mySocket.Connected)
                {
                    return;
                }
                sBuffer = ReceiveDetail(mySocket);

                Console.WriteLine("Receive buffer:" + sBuffer);
                // 查找 "HTTP" 的位置 
                iStartPos = sBuffer.IndexOf("HTTP", 1);
                // 得到请求类型（GET/POST）和文件目录文件名 
                sRequest = sBuffer.Substring(0, iStartPos - 1);

                //得带请求文件名(命令码+参数)
                iStartPos = sRequest.LastIndexOf("/") + 1;
                sRequestedFile = sRequest.Substring(iStartPos).Trim();
               
                if (Method_ == Method.POST)//sRequestedFile == "" || 
                {
                    int iSart = sBuffer.IndexOf("FFFF");
                    int iEnd  = sBuffer.LastIndexOf("EEEE");
                    sPostRequst = sBuffer.Substring(iSart+4,iEnd-iSart-4);
                    Console.WriteLine("Receive Post:" + sPostRequst);
                    
                    //SendMessage_ = "<H2>提交通过!</H2><Br>" + DateTime.Now.ToString();
                    //Version_ = "HTTP/1.1";
                    //MIMEHeader_ = "text/html";
                    //StatusCode_ = " 202 OK";
                    //SendHeader();
                    //SendToBrowser();
                   
                }

                //得到请求文件目录 
                if (Method_ == Method.GET)
                {
                    sDirName = sRequest.Substring(sRequest.IndexOf("/") + 1, sRequest.LastIndexOf("/") - 4);
                }
                else if (Method_ == Method.POST)
                {
                    sDirName = sRequest.Substring(sRequest.IndexOf("/") + 1, sRequest.LastIndexOf("/") - 5);
                }
                else
                {
                    sDirName = "";
                }

                if (!sMyWebServerRoot.EndsWith("\\"))
                {
                    sMyWebServerRoot = sMyWebServerRoot + "\\";
                }

                //获取虚拟目录物理路径 
                sLocalDir = (sMyWebServerRoot + sDirName).Replace("/", "\\");

                Console.WriteLine("请求文件目录 : " + sLocalDir);

                // 取得请求文件类型（设定为text/html）
                String sMimeType = "text/html";
                sPhysicalFilePath = (sLocalDir + sDirName + sRequestedFile); ;

                Console.WriteLine("请求文件: " + sPhysicalFilePath);
              
                //刷新画面数据请求 改为POST方法
                if (sRequestedFile.Contains(CommandEnum.GetFormData.ToString()))
                {
                    try
                    {
                        string baseUrl;
                        NameValueCollection nvc;

                        ParseUrl(sRequestedFile, out baseUrl, out nvc);
                        // Console.WriteLine(baseUrl);
                        string formName = "";
                        foreach (string key in nvc.Keys)
                        {
                            if (key == "name")  //Console.WriteLine("{0}:{1}", key, nvc[key]);
                                formName = nvc[key];
                        }
                        if (formName != "")
                        {
                            string strFormId = formName;//sRequestedFile.Substring(0, sRequestedFile.Length - 4);
                            string xml = mainfrm.GetFormWebXml(strFormId);
                            string xmlheader = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + "\r\n" + "<!--Form objects xml,GHIBMS make for 1.0 version!-->" + "\r\n";

                            xml = xmlheader + xml;
                            SendMessage_ = xml;
                            MIMEHeader_ = "text/html";

                            StatusCode_ = " 200 OK";
                            SendHeader();
                            //SendToBrowser();

                            Console.WriteLine("回复GetFormData: " + SendMessage_);
                        }
                        try
                        {
                            if (mySocket != null)
                            {
                                mySocket.Close();
                            }
                            return;
                        }
                        catch
                        {
                            mySocket = null;
                        }
                    }
                   catch (Exception e)
                   {
                        Console.WriteLine("发生错误 : {0} ", e);
                        return;
                   }
                }
                //数据点设定值
                else if (sRequestedFile.Contains(CommandEnum.SetPointData.ToString()))
                {
                    try
                    {
                        string baseUrl;
                        NameValueCollection nvc;

                        ParseUrl(sRequestedFile, out baseUrl, out nvc);
                        Console.WriteLine(baseUrl);
                        string pointName = "";
                        string pointValue = "";
                        foreach (string key in nvc.Keys)
                        {
                            if (key == "name")  //Console.WriteLine("{0}:{1}", key, nvc[key]);
                                pointName = nvc[key];
                            if (key == "value")  //Console.WriteLine("{0}:{1}", key, nvc[key]);
                                pointValue = nvc[key];
                        }
                        if (pointName != "")
                        {

                            mainfrm.Write2Device(pointName, pointValue);
                            SendMessage_ = "<H2>Write2Device OK " + pointName + "=" + pointValue + "</H2>";
                            MIMEHeader_ = "text/html";

                            StatusCode_ = " 200 OK";
                            SendHeader();
                            SendToBrowser();

                        }
                        try
                        {
                            if (mySocket != null)
                            {
                                mySocket.Close();
                            }
                            return;
                        }
                        catch
                        {
                            mySocket = null;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("发生错误 : {0} ", e);
                        return;
                    }
                }
                if (sRequestedFile.Contains(CommandEnum.GetAlarmCount.ToString()))//读当前报警的数量
                {
                    try
                    {
                        SendMessage_ = mainfrm.GetAlarmCount().ToString();
                        //Version_ = "HTTP/1.1";

                        MIMEHeader_ = "text/html";

                        StatusCode_ = " 200 OK";

                        SendHeader();

                        SendToBrowser();
                        try
                        {
                            if (mySocket != null)
                            {
                                mySocket.Close();
                            }
                            return;
                        }
                        catch
                        {
                            mySocket = null;
                        }

                    }

                    catch (Exception e)
                    {
                        Console.WriteLine("发生错误 : {0} ", e);
                        return;
                    }
                }
                if (sRequestedFile.Contains(CommandEnum.GetAlarmList.ToString()))//读当前报警的列表
                {
                    try
                    {
                        SendMessage_ = mainfrm.AlarmList2XML();
                        //Version_ = "HTTP/1.1";

                        MIMEHeader_ = "text/html";

                        StatusCode_ = " 200 OK";

                        SendHeader();

                        SendToBrowser();
                        try
                        {
                            if (mySocket != null)
                            {
                                mySocket.Close();
                            }
                            return;
                        }
                        catch
                        {
                            mySocket = null;
                        }

       
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("发生错误 : {0} ", e);
                        return;
                    }
                }
                if (sRequestedFile.Contains(CommandEnum.GetOneAlarm.ToString()))//读报警详细信息
                {
                    try
                    {
                        string baseUrl;
                        NameValueCollection nvc;

                        ParseUrl(sPhysicalFilePath, out baseUrl, out nvc);
                        Console.WriteLine(baseUrl);
                        string guid = "";

                        foreach (string key in nvc.Keys)
                        {
                            if (key == "guid")  //Console.WriteLine("{0}:{1}", key, nvc[key]);
                                guid = nvc[key];
                        }
                        SendMessage_ = mainfrm.S_OnGetOneAlarm(guid);

                        //Version_ = "HTTP/1.1";

                        MIMEHeader_ = "text/html";

                        StatusCode_ = " 200 OK";

                        SendHeader();

                        SendToBrowser();
                        try
                        {
                            if (mySocket != null)
                            {
                                mySocket.Close();
                            }
                            return;
                        }
                        catch
                        {
                            mySocket = null;
                        }

           
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("发生错误 : {0} ", e);
                        return;
                    }
                }
                if (sRequestedFile.Contains(CommandEnum.GetNewAlarm.ToString()))//读最新报警
                {
                    try
                    {
                        SendMessage_ = mainfrm.S_OnGetNewAlarm();

                        //Version_ = "HTTP/1.1";

                        MIMEHeader_ = "text/html";

                        StatusCode_ = " 200 OK";

                        SendHeader();

                        SendToBrowser();
                        try
                        {
                            if (mySocket != null)
                            {
                                mySocket.Close();
                            }
                            return;
                        }
                        catch
                        {
                            mySocket = null;
                        }

                      
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("发生错误 : {0} ", e);
                        return;
                    }

                }
                //消除一个报警
                else if (sRequestedFile.Contains(CommandEnum.ClearAlarm.ToString()))
                {
                    try
                    {
                        string baseUrl;
                        NameValueCollection nvc;

                        ParseUrl(sRequestedFile, out baseUrl, out nvc);
                        Console.WriteLine(baseUrl);

                        string userName = "";
                        string alarmGuid = "";
                        string result = "";
                        foreach (string key in nvc.Keys)
                        {
                            if (key == "userName")  //Console.WriteLine("{0}:{1}", key, nvc[key]);
                                userName = nvc[key];
                            if (key == "alarmGuid")  //Console.WriteLine("{0}:{1}", key, nvc[key]);
                                alarmGuid = nvc[key];

                            if (key == "result")  //Console.WriteLine("{0}:{1}", key, nvc[key]);
                                result = nvc[key];
                        }
                        if (alarmGuid != "")
                        {
                            mainfrm.C_AlarmClear(userName, alarmGuid, result);
                            SendMessage_ = "<H2>AlarmClear OK </H2><Br>" + DateTime.Now.ToString();
                            SendMessage_ = sResponse;

                            //Version_ = "HTTP/1.1";

                            MIMEHeader_ = "text/html";

                            StatusCode_ = " 200 OK";

                            SendHeader();

                            SendToBrowser();
                            try
                            {
                                if (mySocket != null)
                                {
                                    mySocket.Close();
                                }
                                return;
                            }
                            catch
                            {
                                mySocket = null;
                            }

                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("发生错误 : {0} ", e);
                        return;
                    }
                }
                //矩阵云台控制
                else if (sRequestedFile.Contains(CommandEnum.VideoControl.ToString()))
                {
                    try
                    {
                        string baseUrl;
                        NameValueCollection nvc;
                        ParseUrl(sRequestedFile, out baseUrl, out nvc);
                        //Console.WriteLine(baseUrl);
                        string parm = "";
                        foreach (string key in nvc.Keys)
                        {
                            if (key == "parm")  //Console.WriteLine("{0}:{1}", key, nvc[key]);
                                parm = nvc[key];
                        }
                        string[] strArray = parm.Split(new char[] { '|' });
                        if (strArray.Length == 8)
                        {
                            string cmd = strArray[0];
                            string camName = strArray[1];
                            string matrixName = strArray[2];
                            string videoIn = strArray[3];
                            string videoOut = strArray[4];
                            string vparm1 = strArray[5];
                            string vparm2 = strArray[6];
                            string vparm3 = strArray[7];

                            VideoCommandArgs args = new VideoCommandArgs();
                            args.CamName = camName;
                            args.MatrixName = matrixName;
                            args.VideoIn = pubFun.IsNumeric(videoIn);
                            args.VideoOut = pubFun.IsNumeric(videoOut);

                            switch (cmd.Trim())
                            {
                                //云台镜头 
                                case "6": //((int)VideoCommandEnum.AUX_PWRON1).ToString():
                                case "7"://((int)VideoCommandEnum.AUX_PWRON2).ToString():
                                case "27"://((int)VideoCommandEnum.DOWN_LEFT).ToString():
                                case "28"://((int)VideoCommandEnum.DOWN_RIGHT).ToString():
                                case "4"://((int)VideoCommandEnum.FAN_PWRON).ToString():
                                case "14"://((int)VideoCommandEnum.FOCUS_FAR).ToString():
                                case "13"://((int)VideoCommandEnum.FOCUS_NEAR).ToString():
                                case "5"://((int)VideoCommandEnum.HEATER_PWRON).ToString():
                                case "16"://((int)VideoCommandEnum.IRIS_CLOSE).ToString():
                                case "15"://((int)VideoCommandEnum.IRIS_OPEN).ToString():
                                case "2"://((int)VideoCommandEnum.PAN_AUTO).ToString():
                                case "23"://((int)VideoCommandEnum.PAN_LEFT).ToString():
                                case "24"://((int)VideoCommandEnum.PAN_RIGHT).ToString():
                                case "22"://((int)VideoCommandEnum.TILT_DOWN).ToString():
                                case "21"://((int)VideoCommandEnum.TILT_UP).ToString():
                                case "25"://((int)VideoCommandEnum.UP_LEFT).ToString():
                                case "26"://((int)VideoCommandEnum.UP_RIGHT).ToString():
                                case "3"://((int)VideoCommandEnum.WIPER_PWROFF).ToString():
                                case "11"://((int)VideoCommandEnum.ZOOM_IN).ToString():
                                case "12"://((int)VideoCommandEnum.ZOOM_OUT).ToString():
                                    args.VideoCommand = (VideoCommandEnum)int.Parse(cmd);
                                    args.Stop = uint.Parse(vparm1);
                                    args.Speed = uint.Parse(vparm1);
                                    break;
                                //预置位
                                case "9"://VideoCommandEnum.CLE_PRESET:
                                case "39"://VideoCommandEnum.GOTO_PRESET:
                                case "8"://VideoCommandEnum.SET_PRESET:
                                    args.VideoCommand = (VideoCommandEnum)int.Parse(cmd);
                                    args.PresetIndex = uint.Parse(vparm1);

                                    break;
                                //巡航
                                case "30"://VideoCommandEnum.FILL_PRE_SEQ:
                                case "31"://VideoCommandEnum.SET_SEQ_DWELL:
                                case "32"://VideoCommandEnum.SET_SEQ_SPEED:
                                case "33"://VideoCommandEnum.CLE_PRE_SEQ:
                                case "37"://VideoCommandEnum.RUN_SEQ:
                                case "38"://VideoCommandEnum.STOP_SEQ:
                                    args.VideoCommand = (VideoCommandEnum)int.Parse(cmd);
                                    args.CruiseRoute = Convert.ToByte(vparm1);
                                    args.CruisePoint = Convert.ToByte(vparm2);
                                    args.Input = Convert.ToUInt16(vparm3);
                                    break;
                                case "34"://VideoCommandEnum.STA_MEM_CRUISE:
                                case "35"://VideoCommandEnum.STO_MEM_CRUISE:
                                case "36"://VideoCommandEnum.RUN_CRUISE:
                                    args.VideoCommand = (VideoCommandEnum)int.Parse(cmd);
                                    break;
                                case "101":
                                case "102":
                                case "104":
                                    args.VideoCommand = (VideoCommandEnum)int.Parse(cmd);
                                    break;
                                case "103"://VideoCommandEnum.MAT_RUN:
                                    args.VideoCommand = (VideoCommandEnum)int.Parse(cmd);
                                    args.AutoRunIndex = uint.Parse(vparm1);
                                    break;
                                case "105"://VideoCommandEnum.MAT_GROUP:
                                    args.VideoCommand = (VideoCommandEnum)int.Parse(cmd);
                                    args.GroupIndex = uint.Parse(vparm1);
                                    break;
                            }
                            mainfrm.C_VideoControl(args);

                            SendMessage_ = "<H2>VideoControl OK </H2><Br>" + DateTime.Now.ToString();
                            SendMessage_ = sResponse;

                            //Version_ = "HTTP/1.1";

                            MIMEHeader_ = "text/html";

                            StatusCode_ = " 200 OK";

                            SendHeader();

                            SendToBrowser();
                            try
                            {
                                if (mySocket != null)
                                {
                                    mySocket.Close();
                                }
                                return;
                            }
                            catch
                            {
                                mySocket = null;
                            }

                        }
                          
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("发生错误 : {0} ", e);
                        return;
                    }

                }
                else
                {
                    if (sRequestedFile.Length == 0)
                    {

                        if (!sPhysicalFilePath.EndsWith("\\"))
                        {
                            sPhysicalFilePath = sPhysicalFilePath + "\\";
                        }
                        sRequestedFile = "main.htm";
                        sPhysicalFilePath += "main.htm";
                    }

                    if (!File.Exists(sPhysicalFilePath))
                    {
                        SendMessage_ = "<H2>Error!! Requested Directory does not exists</H2><Br>" + DateTime.Now.ToString();

                        //Version_ = "HTTP/1.1";

                        MIMEHeader_ = "text/html";
                        StatusCode_ = " 404 Not Found,文件不存在！";
                        SendHeader();
                        SendToBrowser();
                        try
                        {
                            if (mySocket != null)
                            {
                                mySocket.Close();
                            }
                            return;
                        }
                        catch
                        {
                            mySocket = null;
                        }

      
                    }

                    //禁止访问系统目录和文件
                    if (sLocalDir.Length == 0 ||
                      sRequest.ToLower().IndexOf("system") != -1 ||
                      sRequest.ToLower().IndexOf("windows") != -1 ||
                      sRequest.ToLower().IndexOf("..") != -1 ||
                      sRequest.ToLower().IndexOf("?") != -1 ||
                      sRequest.Length >= 60 ||

                      sRequestedFile.ToLower().IndexOf("?") != -1 ||
                      sRequestedFile.ToLower().IndexOf(".exe") != -1 ||
                      sRequestedFile.ToLower().IndexOf(".dll") != -1 ||
                      sRequestedFile.ToLower().IndexOf(".com") != -1 ||
                      sRequestedFile.ToLower().IndexOf("..") != -1 ||
                      sRequestedFile.ToLower().IndexOf(".bat") != -1 ||
                      sRequestedFile.Length >= 30
                      )
                    {

                        SendMessage_ = "<H2>Error!! Requested Directory does not exists</H2><Br>" + DateTime.Now.ToString();

                        //Version_ = "HTTP/1.1";

                        MIMEHeader_ = "text/html";

                        StatusCode_ = " 404 Not Found";

                        SendHeader();

                        SendToBrowser();
                        try
                        {
                            if (mySocket != null)
                            {
                                mySocket.Close();
                            }
                            return;
                        }
                        catch
                        {
                            mySocket = null;
                        }

        
                    }

                    if (
                        sRequestedFile.Replace(" ", "") != "" &&
                        sRequestedFile.ToLower().IndexOf(".html") == -1 &&
                        sRequestedFile.ToLower().IndexOf(".htm") == -1 &&
                        sRequestedFile.ToLower().IndexOf(".txt") == -1 &&
                        sRequestedFile.ToLower().IndexOf(".rar") == -1 &&
                        sRequestedFile.ToLower().IndexOf(".xml") == -1
                        )
                    {

                        SendMessage_ = "<H2>Error!! Requested Directory does not exists</H2><Br>" + DateTime.Now.ToString();

                        //Version_ = "HTTP/1.1";

                        MIMEHeader_ = "text/html";
                        StatusCode_ = " 404 Not Found";
                        SendHeader();
                        SendToBrowser();

                    }


                    int iTotBytes = 0;

                    sResponse = "";

                    byte[] bytes = null;

                    FileStream fs = new FileStream(sPhysicalFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);

                    BinaryReader reader = new BinaryReader(fs);

                    bytes = new byte[fs.Length];

                    int read;
                    while ((read = reader.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        sResponse = sResponse + Encoding_.GetString(bytes, 0, read);

                        iTotBytes = iTotBytes + read;

                    }
                    reader.Close();
                    fs.Close();

                    SendMessage_ = sResponse;

                    //Version_ = "HTTP/1.1";

                    MIMEHeader_ = "text/html";

                    StatusCode_ = " 200 OK";

                    SendHeader();

                    SendToBrowser();
                    try
                    {
                        if (mySocket != null)
                        {
                            mySocket.Close();
                        }
                        return;
                    }
                    catch
                    {
                        mySocket = null;
                    }
                  
                }
                try
                {
                    if (mySocket != null)
                    {
                        mySocket.Close();
                    }
                    return;
                }
                catch
                {
                    mySocket = null;
                }
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public string ReceiveDetail(Socket socket)
        {
            string HtmlHeader = "";
            string HtmlBody= "";
            int iStartPos1 = 0;
            try
            {
                int iBytes = 0;//当前缓冲区可读字节
                int Bytes = 0;//提交体已读缓冲区总字节
                int Content_Length = 0;

                Byte[] RecvBytes = new Byte[1024*1000];
                iBytes = socket.Receive(RecvBytes, RecvBytes.Length, 0);
                HtmlHeader = Encoding_.GetString(RecvBytes, 0, iBytes);

                if (HtmlHeader.Substring(0, 3) == "GET")
                {
                    Method_ = Method.GET;
                }
                else if (HtmlHeader.Substring(0, 4) == "POST")
                {
                    Method_ = Method.POST;
                }
                else
                {
                    Method_ = Method.NULL;
                }

                if (Method_ == Method.POST)
                {

                    Content_Length = 0;
                    iBytes = 0;
                    Bytes = 0;

                    MatchCollection matchs = null;
                    matchs = Regex.Matches(HtmlHeader, @"Content-Length:(.*?)\r\n", RegexOptions.Singleline);
                    if (matchs.Count>0)
                    {
                        try
                        {
                            Content_Length = int.Parse( matchs[0].Groups[1].Value);
                            RecvBytes = new Byte[Content_Length];
                        }
                        catch { Content_Length = 0; }
                    }

                    string tag_ = "\r\n\r\n";
                    
                    iStartPos1 = HtmlHeader.IndexOf(tag_);
                    int iStartPos2 = HtmlHeader.IndexOf("Connection: Close");//存在,
                    if (iStartPos2 != -1)//可能为一次性读取完毕
                    {
                        //Content_Length = 0;
                        HtmlBody = HtmlHeader.Substring(iStartPos1 + tag_.Length);
                        HtmlHeader = HtmlHeader.Substring(0, iStartPos1 + tag_.Length);

                        Bytes = Encoding_.GetBytes(HtmlBody).Length;
                        //HtmlBody += "\r\n\r\n*********************************************\r\n\r\n";
                    }
                    else
                    {
                        iStartPos2 = HtmlHeader.IndexOf("Connection: Keep-Alive");
                        if (iStartPos2 == -1)//可能一次性读取完毕
                        {
                            //Content_Length = 0;
                            HtmlBody = HtmlHeader.Substring(iStartPos1 + tag_.Length );
                            HtmlHeader = HtmlHeader.Substring(0, iStartPos1 + tag_.Length);

                            Bytes = Encoding_.GetBytes(HtmlBody).Length;

                            //HtmlBody+="\r\n\r\n*********************************************\r\n\r\n";
                        }
                    }


                    while (Content_Length > Bytes)
                    {
                        iBytes = socket.Receive(RecvBytes, RecvBytes.Length, 0);
                        HtmlBody += Encoding_.GetString(RecvBytes, 0, iBytes);
                        Bytes += iBytes;
                        if (iBytes < 1)
                        {
                            break;
                        }
                    }
                    Console.WriteLine("ReceiveDetail:" + HtmlBody);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            HtmlHeader = DeCode(HtmlHeader);
            HtmlBody = DeCode(HtmlBody);

            iStartPos1 = HtmlHeader.IndexOf("HTTP", 1);
            Version_ = HtmlHeader.Substring(iStartPos1, 8);// HTTP/1.1


            if (IISAS != null)
            {
                IISAS(mySocket, Method_,this, HtmlHeader, HtmlBody);
            }

            return HtmlHeader + HtmlBody;

        } 


        #region 解网站返回数据编码
        /// <summary>
        /// 解网站返回的乱码
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string DeCode(string src)
        {
            if (src.Replace(" ", "") == "")
            {
                return src;
            }
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HttpUtility.HtmlDecode(src, sw);
            sw.Close();
            src = sb.ToString();
            src = HttpUtility.UrlDecode(src);
            return src;
        }
        /// <summary>
        /// 分析 url 字符串中的参数信息
        /// </summary>
        /// <param name="url">输入的 URL</param>
        /// <param name="baseUrl">输出 URL 的基础部分</param>
        /// <param name="nvc">输出分析后得到的 (参数名,参数值) 的集合</param>

        static void ParseUrl(string url, out string baseUrl, out NameValueCollection nvc)
        {
            if (url == null)
            {
                baseUrl = "";
                nvc = null;
                return;
            }

            nvc = new NameValueCollection();
            baseUrl = "";

            if (url == "")
                return;

            int questionMarkIndex = url.IndexOf('?');

            if (questionMarkIndex == -1)
            {
                baseUrl = url;
                return;
            }
            baseUrl = url.Substring(0, questionMarkIndex);
            if (questionMarkIndex == url.Length - 1)
                return;
            string ps = url.Substring(questionMarkIndex + 1);

            // 开始分析参数对    
            Regex re = new Regex(@"(^|&)?(\w+)=([^&]+)(&|$)?", RegexOptions.Compiled);
            MatchCollection mc = re.Matches(ps);

            foreach (Match m in mc)
            {
                nvc.Add(m.Result("$2"), m.Result("$3"));
            }
        }

        #endregion

        #region 发送数据到客户浏览

        /// <summary>
        /// 发送的HTTP头信息
        /// </summary>
        public void SendHeader()
        {
            int iTotBytes = Encoding_.GetBytes(SendMessage_).Length;

            String sBuffer = "";
            MIMEHeader_ = MIMEHeader_.Replace(" ", "");
            if (MIMEHeader_ == null || MIMEHeader_.Length == 0)
            {
                MIMEHeader_ = "text/html"; // 默认 text/html 
            }

            sBuffer = sBuffer + Version_ + StatusCode_ + "\r\n";
            sBuffer = sBuffer + "Server: cx1193719-b\r\n";
            sBuffer = sBuffer + "Content-Type: " + MIMEHeader_ + "; charset=utf-8\r\n";
            sBuffer = sBuffer + "Accept-Ranges: bytes\r\n";
            sBuffer = sBuffer + "Content-Length: " + iTotBytes + "\r\n\r\n";

            Byte[] bSendData = Encoding_.GetBytes(sBuffer);

            SendToBrowser(bSendData);

        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="mySocket"></param>
        public void SendToBrowser()
        {
            SendToBrowser(Encoding_.GetBytes(SendMessage_));
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="sData">发送的数据字符串</param>
        /// <param name="mySocket"></param>
        public void SendToBrowser(String sData )
        {
            SendToBrowser(Encoding_.GetBytes(sData));
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="bSendData">发送的数据字节</param>
        /// <param name="mySocket"></param>
        public void SendToBrowser(Byte[] bSendData )
        {
            int numBytes = 0;
            try
            {
                if (mySocket.Connected)
                {
                    if ((numBytes = mySocket.Send(bSendData, bSendData.Length, 0)) == -1)
                        Console.WriteLine("Socket Error cannot Send Packet");
                    else
                    {
                        Console.WriteLine("No. of bytes send {0}", numBytes);
                    }
                }
                else
                { Console.WriteLine("连接失败...."); }
            }
            catch (Exception e)
            {
                Console.WriteLine("发生错误 : {0} ", e);
            }
        }


        #endregion
    }

    /// <summary>
    /// 请求或提交网站(方式:POST GET)   
    /// 建于: 2010-12-12 
    /// 作者: kevery  
    /// </summary>
    public class WebPostGet
    {
        public static string Get(string Create_Url, string Referer_Url, CookieContainer CC, string EncodingName)
        {
            string text1 = "";
            if (EncodingName == "")
            {
                EncodingName = "gb2312";//= "Big5";
            }
            try
            {
                byte[] buffer1 = new byte[0x7d000];
                HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create(Create_Url);
                request1.Timeout = 0x2710;
                request1.ReadWriteTimeout = 0x7530;
                request1.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; Tablet PC 2.0; InfoPath.2)";
                request1.Accept = "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, application/x-shockwave-flash, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, application/QVOD, application/QVOD, */*";
                //request1.KeepAlive = true;
                request1.Headers.Add("Accept-Language", "zh-cn");
                request1.Headers.Add("Accept-Encoding", "gzip, deflate");
                //request1.Headers.Add("UA-CPU", "x86");
                //request1.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                request1.Referer = Referer_Url;

                if (CC == null)
                {
                    CC = new CookieContainer();
                }

                request1.CookieContainer = CC;

                HttpWebResponse HWResp = (HttpWebResponse)request1.GetResponse();
                Stream stream1 = Gzip(HWResp);
                //foreach (string kkk in HWResp.Headers)
                //{
                //    if (kkk == "Set-Cookie")
                //    {
                //        string kkk1 = HWResp.Headers[kkk];
                //    }

                //}
                CC.Add(HWResp.Cookies);

                text1 = HWResp.ContentEncoding;
                StreamReader reader1 = new StreamReader(stream1, Encoding.GetEncoding(EncodingName));
                text1 = reader1.ReadToEnd();
                reader1.Close();
                stream1.Close();
            }
            catch (Exception exception1)
            {
                text1 = "Exception-001Pr:" + exception1.Message;
            }

            return DeCode(text1);
        }


        /// <summary>
        /// 提交数据  异常时返回带:Exception-001Pr 字样
        /// </summary>
        /// <param name="Create_Url">提交地址</param>
        /// <param name="Referer_Url">当前地址</param>
        /// <param name="SendBody_">提交数据</param>
        /// <param name="CC">Cookie</param>
        /// <param name="EncodingName">使用的编码</param>
        /// <returns></returns>
        public static string Post(string Create_Url, string Referer_Url, string SendBody_, CookieContainer CC, string EncodingName)
        {
            string text1 = "";
            if (EncodingName == "")
            {
                EncodingName = "gb2312";//= "Big5";
            }
            try
            {
                byte[] buffer1 = new byte[0x7d000];
                HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create(Create_Url);
                request1.AllowAutoRedirect = false;
                request1.Method = "POST";
                request1.Timeout = 10000;
                request1.ReadWriteTimeout = 10000;
                request1.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; Tablet PC 2.0; InfoPath.2)";
                request1.Accept = "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, application/x-shockwave-flash, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, application/QVOD, application/QVOD, */*";
                request1.KeepAlive = true;
                request1.ContentType = "application/x-www-form-urlencoded";
                request1.Headers.Add("Accept-Language", "zh-cn");
                request1.Headers.Add("Accept-Encoding", "gzip, deflate");
                //request1.Headers.Add("UA-CPU", "x86");
                //request1.Headers.Add("Cache-Control", "no-cache");
                //request1.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                request1.Referer = Referer_Url;
                if (CC == null)
                {
                    CC = new CookieContainer();
                }
                request1.CookieContainer = CC;
                byte[] buffer2 = Encoding.GetEncoding(EncodingName).GetBytes(SendBody_);
                request1.ContentLength = buffer2.Length;
                Stream stream1 = request1.GetRequestStream();
                stream1.Write(buffer2, 0, buffer2.Length);
                stream1.Close();


                Thread.CurrentThread.Join(100);
                HttpWebResponse HWResp = (HttpWebResponse)request1.GetResponse();
                CC.Add(HWResp.Cookies);
                Stream stream2 = Gzip(HWResp);
                StreamReader reader1 = new StreamReader(stream2, Encoding.GetEncoding(EncodingName));
                text1 = reader1.ReadToEnd();
                reader1.Close();
                stream2.Close();
            }
            catch (Exception exception1)
            {
                text1 = "Exception-001Pr:" + exception1.Message;
            }

            return DeCode(text1);
        }

        public static Stream Gzip(HttpWebResponse HWResp)
        {
            Stream stream1 = null;
            if (HWResp.ContentEncoding == "gzip")
            {
                stream1 = new GZipInputStream(HWResp.GetResponseStream());
            }
            else
            {
                if (HWResp.ContentEncoding == "deflate")
                {
                    stream1 = new InflaterInputStream(HWResp.GetResponseStream());
                }
            }
            if (stream1 == null)
            {
                return HWResp.GetResponseStream();
            }
            MemoryStream stream2 = new MemoryStream();
            int count = 0x800;
            byte[] buffer = new byte[0x800];
            goto Label_005C;
        Label_005C:
            count = stream1.Read(buffer, 0, count);
            if (count > 0)
            {
                stream2.Write(buffer, 0, count);
                goto Label_005C;
            }
            stream2.Seek((long)0, SeekOrigin.Begin);
            return stream2;
        }

        /// <summary>
        /// 解网站返回的乱码
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string DeCode(string src)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HttpUtility.HtmlDecode(src, sw);
            sw.Close();
            src = sb.ToString();
            src = HttpUtility.UrlDecode(src);
            return src;
        }





    }

}
