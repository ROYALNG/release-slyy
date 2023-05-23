
using System; 
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using GHIBMS.Common; 
namespace GHIBMS.Server
{
public class MyWebServer 
{
	private TcpListener myListener; 
	private int port = 8080;  // Web服务器端口,通常为80
    private bool active = false;

    public delegate void activeDelegate(bool actived);
    //public delegate void conncetDelegate(Socket socket);
    //public delegate void disconncetDelegate(Socket socket);
    //public delegate void receiveDelegeate(Socket socket, String msg);

    public event activeDelegate OnActive;
    //public event conncetDelegate OnConnect;
    //public event disconncetDelegate OnDisconnect;
    //public event receiveDelegeate OnReceive;
    private FormMain mainfrm;
    private string webPath="";
	public MyWebServer( FormMain form,string path)
	{
        mainfrm = form;
        webPath = path;
	}
    public int Port
    {
        get { return port; }
        set { port = value; }
    }
    /// <summary>
    /// 启动/关闭服务
    /// </summary>
    public bool Active
    {
        get { return active; }
        set
        {
            if (active && !value)
            {
                active = false;
                try
                {
                    myListener.Server.Close();
                }
                catch { }
            }
            else if (!active && value)
            {
                try
                {
                    ///创建终结点（EndPoint）
                    //IPAddress ip = IPAddress.Parse("192.168.1.106");//把ip地址字符串转换为IPAddress类型的实例
                   
                    //开始监听端口
                    myListener = new TcpListener(IPAddress.Any, port);//IPAddress.Any
                    myListener.Start();
                    Console.WriteLine("WEB服务器成功启动,监听于{0}的{1}端口......", myListener.LocalEndpoint.ToString(), port.ToString());
                    //同时启动一个监听进程
                    Thread th = new Thread(new ThreadStart(StartListen));
                    th.Start();
                    active = true;
                    if (OnActive != null)
                        OnActive(true);
                }
                catch
                {
                    try
                    {
                        myListener.Stop();
                    }
                    catch { }
                    if (OnActive != null)
                        OnActive(false);
                }
            }
        }
    }
    

	private void SendHeader(string sHttpVersion, string sMIMEHeader, int iTotBytes, string sStatusCode, ref Socket mySocket)
	{
        try
        {
            String sBuffer = "";
            if (sMIMEHeader.Length == 0)
            {
                sMIMEHeader = "text/html"; // 默认 text/html
            }
            sBuffer = sBuffer + sHttpVersion + sStatusCode + "\r\n";
            sBuffer = sBuffer + "Server: MyWebServer\r\n";
            sBuffer = sBuffer + "Content-Type: " + sMIMEHeader + "\r\n";
            sBuffer = sBuffer + "Accept-Ranges: bytes\r\n";
            sBuffer = sBuffer + "Content-Length: " + iTotBytes + "\r\n\r\n";
            Byte[] bSendData = Encoding.ASCII.GetBytes(sBuffer);
            SendToBrowser(bSendData, ref mySocket);
            Console.WriteLine("总字节数: " + iTotBytes.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine("发生错误！" + ex.ToString());
        }
	}

    private void SendToBrowser(String sData, ref Socket mySocket)
	{
        try
        {
            Byte[] buffer = Encoding.Default.GetBytes(sData);
            SendToBrowser(buffer, ref mySocket);
        }
        catch(Exception e)
        {
            Console.WriteLine("发生错误: {0}", e);
        }
	}

    private void SendToBrowser(Byte[] bSendData, ref Socket mySocket)
	{
		int numBytes = 0;
		try
		{
			if (mySocket.Connected)
			{
				if (( numBytes = mySocket.Send(bSendData, bSendData.Length,0)) == -1)
					Console.WriteLine("套接字错误,不能发送数据报");
				else
				{
					Console.WriteLine("发送的字节数为: {0}", numBytes);
				}
			}
			else
				Console.WriteLine("连接失败");
		}
		catch (Exception e)
		{
			Console.WriteLine("发生错误: {0}", e);
		}
	}
    private void StartListen()
    {
        int iStartPos = 0;
        String sRequest;
        String sDirName;
        String sRequestedFile;
        String sErrorMessage;
        String sLocalDir;
        String sPhysicalFilePath;
        //虚拟目录
        String sMyWebServerRoot = webPath;       //Environment.CurrentDirectory;
        sMyWebServerRoot = sMyWebServerRoot.Replace("\\", "/");
        sMyWebServerRoot = sMyWebServerRoot + "/";
        String sFormattedMessage = "";
        String sResponse = "";
        while (active)
        {
            try
            {

                //接受新连接
                Socket mySocket = myListener.AcceptSocket();
                Console.WriteLine("\n\n套接字类型: " + mySocket.SocketType);
                if (mySocket.Connected)
                {
                    Console.WriteLine("客户端连接!\n==================\n客户端IP: {0}", mySocket.RemoteEndPoint);
                    Byte[] bReceive = new Byte[1024];
                    int i = mySocket.Receive(bReceive, bReceive.Length, 0);
                    //转换成字符串类型
                    string sBuffer = Encoding.ASCII.GetString(bReceive);
                    //处理post请求类型
                    if (sBuffer.Substring(0, 4) == "POST")
                    {
                        //解析客户post的xml信息，得到要写变量的name和value，调用APMC驱动写向设备
                        //具体的解析和GET差不多，解析后可得到客户提交的相关信息，如名称和值，然后
                        //调用mainform中的写驱动即可，下面给出了例子

                        //解析ｐｏｓｔ的数据得到相关值：
                        //string name = "abc";
                        //string value = "12.3";
                        //mainfrm.WriteToDevicebyXML(name, value);
                    }
                    //处理"get"请求类型
                    if (sBuffer.Substring(0, 3) == "GET")
                    {
                        Console.WriteLine("处理get请求类型...");
                        // 查找 "HTTP" 的位置
                        iStartPos = sBuffer.IndexOf("HTTP", 1);
                        string sHttpVersion = sBuffer.Substring(iStartPos, 8);
                        // 得到请求类型和文件目录文件名
                        sRequest = sBuffer.Substring(0, iStartPos - 1);
                        sRequest.Replace("\\", "/");
                        //如果结尾不是文件名也不是以"/"结尾则加"/"
                        //if ((sRequest.IndexOf(".") < 1) && (!sRequest.EndsWith("/")))
                        //{
                        //    sRequest = sRequest + "/";
                        //}
                        //得带请求文件名
                        iStartPos = sRequest.IndexOf("/") + 1;
                        sRequestedFile = sRequest.Substring(iStartPos);
                        sRequestedFile = DecodeUrl(sRequestedFile);   //url 解码
                        //得到请求文件目录
                        sDirName = sRequest.Substring(sRequest.IndexOf("/"), sRequest.LastIndexOf("/") - 3);
                        //获取虚拟目录物理路径
                        sLocalDir = sMyWebServerRoot;
                        Console.WriteLine("请求文件目录 : " + sLocalDir);
                        if (sLocalDir.Length == 0)
                        {
                            sErrorMessage = "<H2>错误! 请求的目录不存在...</H2><Br>";
                            SendHeader(sHttpVersion, "", sErrorMessage.Length, " 404 Not Found", ref mySocket);
                            SendToBrowser(sErrorMessage, ref mySocket);
                            mySocket.Close();
                            continue;
                        }
                        if (sRequestedFile.Length == 0)
                        {
                            // 取得请求文件名
                            sRequestedFile = "main.htm";
                        }
                        // 取得请求文件类型（设定为text/html）
                        String sMimeType = "text/html";
                        sPhysicalFilePath = (sLocalDir + sDirName + sRequestedFile).Replace("//", "/"); ;
                        Console.WriteLine("请求文件: " + sPhysicalFilePath);
                       //刷新画面数据请求
                        if (sRequestedFile.Contains(CommandEnum.GetFormData.ToString()))
                        {
                            string baseUrl;
                            NameValueCollection nvc;

                            ParseUrl(sRequestedFile, out baseUrl, out nvc);
                            Console.WriteLine(baseUrl);
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
                                SendHeader(sHttpVersion, sMimeType, xml.Length, " 200 OK", ref mySocket);
                                SendToBrowser(xml, ref mySocket);
                            }
                        }
                        //数据点设定值
                        else if (sRequestedFile.Contains(CommandEnum.SetPointData.ToString()))
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
                            
                                mainfrm.Write2Device(pointName,pointValue);
                                string sOKMessage = "<H2>Write2Device OK " + pointName + "=" + pointValue + "</H2>";
                                SendHeader(sHttpVersion, sMimeType, sOKMessage.Length, " 200 OK", ref mySocket);
                                SendToBrowser(sOKMessage, ref mySocket);
                           
                            }
                        }
                        if (sRequestedFile.Contains(CommandEnum.GetAlarmCount.ToString()))//读当前报警的数量
                        {
                            string c = mainfrm.GetAlarmCount().ToString();
                            SendHeader(sHttpVersion, sMimeType, c.Length, " 200 OK", ref mySocket);
                            SendToBrowser(c, ref mySocket);

                        }
                        if (sRequestedFile.Contains(CommandEnum.GetAlarmList.ToString()))//读当前报警的列表
                        {
                            string c = mainfrm.AlarmList2XML();
                            SendHeader(sHttpVersion, sMimeType, c.Length, " 200 OK", ref mySocket);
                            SendToBrowser(c, ref mySocket);
                        }
                        if (sRequestedFile.Contains(CommandEnum.GetOneAlarm.ToString()))//读报警详细信息
                        {
                            string baseUrl;
                            NameValueCollection nvc;

                            ParseUrl(sRequestedFile, out baseUrl, out nvc);
                            Console.WriteLine(baseUrl);
                            string guid = "";
                          
                            foreach (string key in nvc.Keys)
                            {
                                if (key == "guid")  //Console.WriteLine("{0}:{1}", key, nvc[key]);
                                    guid = nvc[key];
                            }
                            string c = mainfrm.S_OnGetOneAlarm(guid);
                            SendHeader(sHttpVersion, sMimeType, c.Length, " 200 OK", ref mySocket);
                            SendToBrowser(c, ref mySocket);

                        }
                        if (sRequestedFile.Contains(CommandEnum.GetNewAlarm.ToString()))//读最新报警
                        {
                            string c = mainfrm.S_OnGetNewAlarm();
                            SendHeader(sHttpVersion, sMimeType, c.Length, " 200 OK", ref mySocket);
                            SendToBrowser(c, ref mySocket);

                        }
                        //消除一个报警
                        else if (sRequestedFile.Contains(CommandEnum.ClearAlarm.ToString()))
                        {
                            string baseUrl;
                            NameValueCollection nvc;

                            ParseUrl(sRequestedFile, out baseUrl, out nvc);
                            Console.WriteLine(baseUrl);
                          
                            string userName = "";
                            string alarmGuid = "";
                            string result="";
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
                                string sOKMessage = "<H2>AlarmClear OK </H2>";
                                SendHeader(sHttpVersion, sMimeType, sOKMessage.Length, " 200 OK", ref mySocket);
                                SendToBrowser(sOKMessage, ref mySocket);

                            }
                        }
                        else//正常http请求处理
                        {
                            if (File.Exists(sPhysicalFilePath) == false)
                            {
                                sErrorMessage = "<H2>404 Error! File not found!...</H2>";
                                SendHeader(sHttpVersion, "", sErrorMessage.Length, " 404 Not Found", ref mySocket);
                                SendToBrowser(sErrorMessage, ref mySocket);
                                Console.WriteLine(sFormattedMessage);
                            }
                            else
                            {
                                int iTotBytes = 0;
                                sResponse = "";
                                FileStream fs = new FileStream(sPhysicalFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                                BinaryReader reader = new BinaryReader(fs);
                                byte[] bytes = new byte[fs.Length];
                                int read;
                                while ((read = reader.Read(bytes, 0, bytes.Length)) != 0)
                                {
                                    sResponse = sResponse + Encoding.ASCII.GetString(bytes, 0, read);
                                    iTotBytes = iTotBytes + read;
                                }
                                reader.Close();
                                fs.Close();
                                SendHeader(sHttpVersion, sMimeType, iTotBytes, " 200 OK", ref mySocket);
                                SendToBrowser(bytes, ref mySocket);
                            }
                        }
                    }
                    mySocket.Close();
                } //if(mySocket.Connected)
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        } //while(true)
    } //public void StartListen()

    static string DecodeUrl(string oldUrl)
    {
        string newUrl = string.Empty;
        oldUrl=oldUrl.Replace("%2B","+");
        oldUrl=oldUrl.Replace("%2F","/");
        oldUrl=oldUrl.Replace("%3F","?");
        oldUrl=oldUrl.Replace("%25","%");
        oldUrl=oldUrl.Replace("%23","#");
        oldUrl=oldUrl.Replace("%26","&");
        oldUrl=oldUrl.Replace("%3D","=");
        oldUrl=oldUrl.Replace("%20"," ");
        newUrl = oldUrl;
        return newUrl;
    }
    static string EecodeUrl(string oldUrl)
    {
        string newUrl = string.Empty;
        oldUrl = oldUrl.Replace( "+","%2B");
        oldUrl = oldUrl.Replace( "/","%2F");
        oldUrl = oldUrl.Replace( "?","%3F");
        oldUrl = oldUrl.Replace( "%","%25");
        oldUrl = oldUrl.Replace( "#","%23");
        oldUrl = oldUrl.Replace( "&","%26");
        oldUrl = oldUrl.Replace( "=","%3D");
        oldUrl = oldUrl.Replace( " ","%20");
        newUrl = oldUrl;
        return newUrl;
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
        
        if (questionMarkIndex == -1) {
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

        foreach (Match m in mc) {                
            nvc.Add(m.Result("$2"), m.Result("$3"));
        }        
    }

}  //class MyWebServer
} //namespace webserver
