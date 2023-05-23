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
using System.Collections.Specialized;
using System.Diagnostics;

namespace GHIBMS.IISWebServer
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
    /// 作者: 
    /// </summary>
    public class IISWebServer
    {

        public event IISAcceptSocket IISAS = null;
        public delegate void StartDelegate(object sender,EventArgs e);
        public delegate void StopDelegate(object sender, EventArgs e);
        public event StartDelegate OnStart;
        public event StopDelegate  OnStop;
        private TcpListener myListener;
        private  int m_port = 8080; // 选者任何闲置端口 
        Thread ListenThread;
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

       // Thread th = null;
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
                myListener = new TcpListener(IPAddress.Any,Port);
                myListener.Start();
                //同时启动一个兼听进程 'StartListen' 
                ListenThread = new Thread(new ThreadStart(StartListen));
                ListenThread.IsBackground = true;
                ListenThread.Start();
                Debug.WriteLine("开始兼听端口: "+Port);
                if (OnStart != null)
                    OnStart(this, null);

            }
            catch (Exception e)
            {
                Console.WriteLine("兼听端口时发生错误 :" + e.ToString());
            }
        }

        public void Stop()
        {
           
            m_Starting = false;//停止
            Thread.Sleep(200);
            try
            {
                myListener.Stop();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
           
            //if (ListenThread != null && ListenThread.IsAlive)
            //{
            //    try
            //    {
            //        ListenThread.Abort();
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.ToString());
            //    }
            //}
          
            if (OnStop != null)
                OnStop(this, null);
          
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
                        IISWebSend_ = new IISWebSend(mySocket, MyWebServerRoot);
                        IISWebSend_.IISAS += new IISAcceptSocket(IISWebSend__IISAS);
                        IISWebSend_.Start();
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                Thread.Sleep(1000);
            }
            ListenThread = null;

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
        //public string MIMEHeader_ = "text/xml";

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

        public IISWebSend(Socket mySocket_, string MyWebServerRoot_)
        {
            mySocket = mySocket_;
            MyWebServerRoot = MyWebServerRoot_;
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

        Thread th = null;
        public void Start()
        {
            th = new Thread(new ThreadStart(StartListen));
            th.IsBackground = true;
            th.Start();
        }

        public void Stop()
        {
            if (th != null && th.IsAlive)
            {
                try
                {
                    th.Abort();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
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
            //mySocket
            try
            {
                int iStartPos = 0;
                String sRequest;
                String sDirName;
                String sRequestedFile;
                String sLocalDir;
                /////////////////////////////////////注意设定你自己的虚拟目录///////////////////////////////////// 
                String sMyWebServerRoot = MyWebServerRoot; //设置你的虚拟目录 
                ////////////////////////////////////////////////////////////////////////////////////////////////// 
                String sPhysicalFilePath = "";
                String sResponse = "";

                Debug.WriteLine("Socket Type " + mySocket.SocketType);

                string sBuffer = "";

                sBuffer = ReceiveDetail(mySocket);

                if (mySocket == null || !mySocket.Connected)
                {
                    return;
                }
                Debug.WriteLine("*************接收到的数据: " + sBuffer);
                if (sBuffer.Length == 0) return;
                // 查找 "HTTP" 的位置 
                iStartPos = sBuffer.IndexOf("HTTP", 1);

                // 得到请求类型和文件目录文件名 
                sRequest = sBuffer.Substring(0, iStartPos - 1);

                sRequest.Replace("\\", "/");


                //如果结尾不是文件名也不是以"/"结尾则加"/" 
                if ((sRequest.IndexOf(".") < 1) && (!sRequest.EndsWith("/")))
                {
                    sRequest = sRequest + "/";
                }


                //得带请求文件名 
                iStartPos = sRequest.LastIndexOf("/") + 1;
                sRequestedFile = sRequest.Substring(iStartPos);
                //if (Method_ == Method.POST)//sRequestedFile == "" || 
                //{
                //    SendMessage_ = "<H2>提交通过!</H2><Br>" + DateTime.Now.ToString();

                //    //Version_ = "HTTP/1.1";

                //    MIMEHeader_ = "text/html";

                //    StatusCode_ = " 202 OK";

                //    SendHeader();

                //    SendToBrowser();

                //    mySocket.Close();
                //    Debug.WriteLine("提交通过----------------");
                //    return;
                //}

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


                //获取虚拟目录物理路径 
                sLocalDir = sMyWebServerRoot;

                Debug.WriteLine("请求文件目录 : " + sLocalDir);

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

                    mySocket.Close();

                    return;
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

                    mySocket.Close();

                    return;
                }

                //获取虚拟目录物理路径 
                sLocalDir = sMyWebServerRoot + sDirName;

                if (sRequestedFile.Length == 0)
                {
                    // 取得请求文件名 
                    sRequestedFile = "index.html";
                }


                ///////////////////////////////////////////////////////////////////// 
                // 取得请求文件类型（设定为text/html） 
                ///////////////////////////////////////////////////////////////////// 

                sPhysicalFilePath = sLocalDir + sRequestedFile;
                Debug.WriteLine("请求文件: " + sPhysicalFilePath);


                if (!File.Exists(sPhysicalFilePath))
                {
                    SendMessage_ = "<H2>Error!! Requested Directory does not exists</H2><Br>" + DateTime.Now.ToString();

                    //Version_ = "HTTP/1.1";

                    MIMEHeader_ = "text/html";

                    StatusCode_ = " 404 Not Found";

                    SendHeader();

                    SendToBrowser();

                    mySocket.Close();

                    return;
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

                mySocket.Close();
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

                Byte[] RecvBytes = new Byte[1024];
                iBytes = socket.Receive(RecvBytes, RecvBytes.Length, 0);
                HtmlHeader = Encoding_.GetString(RecvBytes, 0, iBytes);
                Debug.WriteLine("****ReceiveDetail***"+HtmlHeader);
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
                }

            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }
            try
            {
                HtmlHeader = DeCode(HtmlHeader);
                HtmlBody = DeCode(HtmlBody);
               
               // iStartPos1 = HtmlHeader.IndexOf("HTTP", 1);
                //Version_ = HtmlHeader.Substring(iStartPos1, 8);// HTTP/1.1


                if (IISAS != null)
                {
                    IISAS(mySocket, Method_, this, HtmlHeader, HtmlBody);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
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
                MIMEHeader_ = "text/xml"; // 默认 text/html 
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
