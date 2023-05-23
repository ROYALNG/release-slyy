
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
	private int port = 8080;  // Web�������˿�,ͨ��Ϊ80
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
    /// ����/�رշ���
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
                    ///�����ս�㣨EndPoint��
                    //IPAddress ip = IPAddress.Parse("192.168.1.106");//��ip��ַ�ַ���ת��ΪIPAddress���͵�ʵ��
                   
                    //��ʼ�����˿�
                    myListener = new TcpListener(IPAddress.Any, port);//IPAddress.Any
                    myListener.Start();
                    Console.WriteLine("WEB�������ɹ�����,������{0}��{1}�˿�......", myListener.LocalEndpoint.ToString(), port.ToString());
                    //ͬʱ����һ����������
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
                sMIMEHeader = "text/html"; // Ĭ�� text/html
            }
            sBuffer = sBuffer + sHttpVersion + sStatusCode + "\r\n";
            sBuffer = sBuffer + "Server: MyWebServer\r\n";
            sBuffer = sBuffer + "Content-Type: " + sMIMEHeader + "\r\n";
            sBuffer = sBuffer + "Accept-Ranges: bytes\r\n";
            sBuffer = sBuffer + "Content-Length: " + iTotBytes + "\r\n\r\n";
            Byte[] bSendData = Encoding.ASCII.GetBytes(sBuffer);
            SendToBrowser(bSendData, ref mySocket);
            Console.WriteLine("���ֽ���: " + iTotBytes.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine("��������" + ex.ToString());
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
            Console.WriteLine("��������: {0}", e);
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
					Console.WriteLine("�׽��ִ���,���ܷ������ݱ�");
				else
				{
					Console.WriteLine("���͵��ֽ���Ϊ: {0}", numBytes);
				}
			}
			else
				Console.WriteLine("����ʧ��");
		}
		catch (Exception e)
		{
			Console.WriteLine("��������: {0}", e);
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
        //����Ŀ¼
        String sMyWebServerRoot = webPath;       //Environment.CurrentDirectory;
        sMyWebServerRoot = sMyWebServerRoot.Replace("\\", "/");
        sMyWebServerRoot = sMyWebServerRoot + "/";
        String sFormattedMessage = "";
        String sResponse = "";
        while (active)
        {
            try
            {

                //����������
                Socket mySocket = myListener.AcceptSocket();
                Console.WriteLine("\n\n�׽�������: " + mySocket.SocketType);
                if (mySocket.Connected)
                {
                    Console.WriteLine("�ͻ�������!\n==================\n�ͻ���IP: {0}", mySocket.RemoteEndPoint);
                    Byte[] bReceive = new Byte[1024];
                    int i = mySocket.Receive(bReceive, bReceive.Length, 0);
                    //ת�����ַ�������
                    string sBuffer = Encoding.ASCII.GetString(bReceive);
                    //����post��������
                    if (sBuffer.Substring(0, 4) == "POST")
                    {
                        //�����ͻ�post��xml��Ϣ���õ�Ҫд������name��value������APMC����д���豸
                        //����Ľ�����GET��࣬������ɵõ��ͻ��ύ�������Ϣ�������ƺ�ֵ��Ȼ��
                        //����mainform�е�д�������ɣ��������������

                        //��������������ݵõ����ֵ��
                        //string name = "abc";
                        //string value = "12.3";
                        //mainfrm.WriteToDevicebyXML(name, value);
                    }
                    //����"get"��������
                    if (sBuffer.Substring(0, 3) == "GET")
                    {
                        Console.WriteLine("����get��������...");
                        // ���� "HTTP" ��λ��
                        iStartPos = sBuffer.IndexOf("HTTP", 1);
                        string sHttpVersion = sBuffer.Substring(iStartPos, 8);
                        // �õ��������ͺ��ļ�Ŀ¼�ļ���
                        sRequest = sBuffer.Substring(0, iStartPos - 1);
                        sRequest.Replace("\\", "/");
                        //�����β�����ļ���Ҳ������"/"��β���"/"
                        //if ((sRequest.IndexOf(".") < 1) && (!sRequest.EndsWith("/")))
                        //{
                        //    sRequest = sRequest + "/";
                        //}
                        //�ô������ļ���
                        iStartPos = sRequest.IndexOf("/") + 1;
                        sRequestedFile = sRequest.Substring(iStartPos);
                        sRequestedFile = DecodeUrl(sRequestedFile);   //url ����
                        //�õ������ļ�Ŀ¼
                        sDirName = sRequest.Substring(sRequest.IndexOf("/"), sRequest.LastIndexOf("/") - 3);
                        //��ȡ����Ŀ¼����·��
                        sLocalDir = sMyWebServerRoot;
                        Console.WriteLine("�����ļ�Ŀ¼ : " + sLocalDir);
                        if (sLocalDir.Length == 0)
                        {
                            sErrorMessage = "<H2>����! �����Ŀ¼������...</H2><Br>";
                            SendHeader(sHttpVersion, "", sErrorMessage.Length, " 404 Not Found", ref mySocket);
                            SendToBrowser(sErrorMessage, ref mySocket);
                            mySocket.Close();
                            continue;
                        }
                        if (sRequestedFile.Length == 0)
                        {
                            // ȡ�������ļ���
                            sRequestedFile = "main.htm";
                        }
                        // ȡ�������ļ����ͣ��趨Ϊtext/html��
                        String sMimeType = "text/html";
                        sPhysicalFilePath = (sLocalDir + sDirName + sRequestedFile).Replace("//", "/"); ;
                        Console.WriteLine("�����ļ�: " + sPhysicalFilePath);
                       //ˢ�»�����������
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
                        //���ݵ��趨ֵ
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
                        if (sRequestedFile.Contains(CommandEnum.GetAlarmCount.ToString()))//����ǰ����������
                        {
                            string c = mainfrm.GetAlarmCount().ToString();
                            SendHeader(sHttpVersion, sMimeType, c.Length, " 200 OK", ref mySocket);
                            SendToBrowser(c, ref mySocket);

                        }
                        if (sRequestedFile.Contains(CommandEnum.GetAlarmList.ToString()))//����ǰ�������б�
                        {
                            string c = mainfrm.AlarmList2XML();
                            SendHeader(sHttpVersion, sMimeType, c.Length, " 200 OK", ref mySocket);
                            SendToBrowser(c, ref mySocket);
                        }
                        if (sRequestedFile.Contains(CommandEnum.GetOneAlarm.ToString()))//��������ϸ��Ϣ
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
                        if (sRequestedFile.Contains(CommandEnum.GetNewAlarm.ToString()))//�����±���
                        {
                            string c = mainfrm.S_OnGetNewAlarm();
                            SendHeader(sHttpVersion, sMimeType, c.Length, " 200 OK", ref mySocket);
                            SendToBrowser(c, ref mySocket);

                        }
                        //����һ������
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
                        else//����http������
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
    /// ���� url �ַ����еĲ�����Ϣ
    /// </summary>
    /// <param name="url">����� URL</param>
    /// <param name="baseUrl">��� URL �Ļ�������</param>
    /// <param name="nvc">���������õ��� (������,����ֵ) �ļ���</param>
   
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

        // ��ʼ����������    
        Regex re = new Regex(@"(^|&)?(\w+)=([^&]+)(&|$)?", RegexOptions.Compiled);
        MatchCollection mc = re.Matches(ps);

        foreach (Match m in mc) {                
            nvc.Add(m.Result("$2"), m.Result("$3"));
        }        
    }

}  //class MyWebServer
} //namespace webserver
