using GHIBMS.Common;
using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using TandiNVSSDK;

namespace GHIBMS.NetVideo
{
    public class TandiDvrRealPlayer : IVideoRealPlayer, IDisposable
    {

        #region 内部变量与属性
        private const int WM_USER = 0x0400; //
        private const int WM_MAIN_MESSAGE = WM_USER + 1001;
        private const int MSG_PARACHG = WM_USER + 90910;
        private const int MSG_ALARM = WM_USER + 90911;
        private const int WCM_ERR_ORDER = 2;
        private const int WCM_ERR_DATANET = 3;
        private const int WCM_LOGON_NOTIFY = 7;
        private const int WCM_VIDEO_HEAD = 8;
        private const int WCM_VIDEO_DISCONNECT = 9;
        private const int WCM_RECORD_ERR = 13;
        private const int LOGON_SUCCESS = 0;
        private const int LOGON_ING = 1;
        private const int LOGON_RETRY = 2;
        private const int LOGON_DSMING = 3;
        private const int LOGON_FAILED = 4;
        private const int LOGON_TIMEOUT = 5;
        private const int NOT_LOGON = 6;
        private const int LOGON_DSMFAILED = 7;
        private const int LOGON_DSMTIMEOUT = 8;
        private const int PLAYER_PLAYING = 0x02;
        private const int USER_ERROR = 0x10000000;

        public static Hashtable logidTable = new Hashtable();
        private static UInt16 videoWndNubs = 1;

        private VideoRealPlayArgs playArgs = new VideoRealPlayArgs();
        private UInt32 connID = UInt32.MaxValue;
        private bool bPlaying = false;
        private bool bRecording = false;
        //private int VoiceComHandle = -1;
        private const int MOVE = 60;
        private const int MOVE_STOP = 61;
        private const int MOVE_UP = 1;
        private const int MOVE_DOWN = 2;
        private const int MOVE_LEFT = 3;
        private const int MOVE_RIGHT = 4;
        private const int MOVE_UP_LEFT = 6;
        private const int MOVE_UP_RIGHT = 5;
        private const int MOVE_DOWN_LEFT = 8;
        private const int MOVE_DOWN_RIGHT = 7;
        private const int ZOOM_BIG = 10;
        private const int ZOOM_SMALL = 11;
        private const int FOCUS_NEAR = 13;
        private const int FOCUS_FAR = 14;
        private const int IRIS_OPEN = 17;
        private const int IRIS_CLOSE = 18;
        private const int RAIN_ON = 19;
        private const int RAIN_OFF = 20;
        private const int LIGHT_ON = 21;
        private const int LIGHT_OFF = 22;
        private const int HOR_AUTO = 23;
        private const int HOR_AUTO_STOP = 24;
        private const int CALL_VIEW = 25;
        private const int SET_VIEW = 28;
        private const int POWER_ON = 29;
        private const int POWER_OFF = 30;
        private const int ZOOM_BIG_STOP = 32;
        private const int ZOOM_SMALL_STOP = 34;
        private const int FOCUS_FAR_STOP = 36;
        private const int FOCUS_NEAR_STOP = 38;
        private const int IRIS_OPEN_STOP = 40;
        private const int IRIS_CLOSE_STOP = 42;
        private string protocolCode = "JK_DVR_TANDI";
        //private NVSDATA_NOTIFY cbkDataArriveDelegate; 
        public string ProtocolCode
        {
            get { return protocolCode; }

        }


        public UInt16 VideoWndNubs
        {
            set { videoWndNubs = value; }
            get { return videoWndNubs; }
        }
        public UInt32 PlayID
        {
            get { return connID; }
            set { connID = value; }
        }
        private IntPtr mainHandle;
        private int loginID = -1;
        #endregion

        #region 事件
        //log事件
        public event OnMessagedelegate OnMessage;
        private void Log(string message)
        {
            if (OnMessage != null)
                OnMessage(this, message);
        }
        public void OnWinMessage(ref System.Windows.Forms.Message m)
        {
            //WM_MAIN_MESSAGE为自定义的系统消息
            if (m.Msg == WM_MAIN_MESSAGE)
            {
                //自定义消息处理函数
                OnMainMessage(m.WParam, m.LParam);
            }

        }
        public void SetWinMessage(IntPtr lWinHandle)
        {
            mainHandle = lWinHandle;

        }
        //处理SDK系统消息
        private void OnMainMessage(IntPtr wParam, IntPtr lParam)
        {
            try
            {
                //wParam的低16位是消息的类型；
                int iMsgType = wParam.ToInt32() & 0xFFFF;
                Debug.WriteLine("Tandi OnMainMessage:" + iMsgType);
                //lParam，网络视频服务器NVS的信息结构体NVS_IPAndID地址
                //Marshal.PtrToStructure函数将Intptr地址转化为结构体
                switch (iMsgType)
                {
                    //登陆状态消息 
                    //param1 登陆IP
                    //param2 登陆ID
                    //param3 登陆状态
                    case WCM_LOGON_NOTIFY:
                        {
                            NVS_IPAndID ipAndID = (NVS_IPAndID)Marshal.PtrToStructure(lParam, typeof(NVS_IPAndID));
                            LogonNotify(ipAndID.m_pIP, ipAndID.m_pID, wParam.ToInt32() >> 16);
                        }
                        break;

                    //视频头消息，当收到视频头时产生。
                    //lParam，网络视频服务器NVS的信息结构体NVS_IPAndID地址；
                    //wParamHi低8位表示通道号；
                    //wParamHi高8位表示码流类型；
                    case WCM_VIDEO_HEAD:
                        {

                            NVS_IPAndID ipAndID = (NVS_IPAndID)Marshal.PtrToStructure(lParam, typeof(NVS_IPAndID));
                            int ich = (wParam.ToInt32() >> 16) & 0xFF;
                            string ip = ipAndID.m_pIP;
                            Debug.WriteLine("Tandi OnMainMessage WCM_VIDEO_HEAD:ip:" + ip + "  " + ich);
                            if ((playArgs.Ip == ip) && (playArgs.DvrCh == (ich + 1)))
                                VideoArrive();
                        }
                        break;

                    //视频被强制断开消息，当前的视频连接被代理强制断开后产生该消息。
                    //param1,视频连接ID号
                    case WCM_VIDEO_DISCONNECT:

                        Log("DVR/NVR网络连接意外断开,IP:" + playArgs.Ip + " 通道号：" + playArgs.DvrCh);
                        break;

                    //网络命令断开消息，当网络连接意外断开时产生。
                    //param1，网络视频服务器的IP地址；
                    case WCM_ERR_ORDER:
                        {
                            Log("DVR/NVR网络连接意外断开。");
                        }
                        break;

                    //网络数据错误，当连接超过最大数后将产生此消息。
                    //param1，网络视频服务器的IP地址；
                    case WCM_ERR_DATANET:
                        {
                            Log("DVR/NVR网络数据错误，当连接超过最大数。");
                        }
                        break;

                    //录像错误消息，当视频录像出现错误时产生。
                    //param1，视频连接ID号
                    case WCM_RECORD_ERR:
                        Log("DVR/NVR录像错误消息。");
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }
        }
        //bool blogin = false;
        //WCM_LOGON_NOTIFY消息处理函数
        private void LogonNotify(string _cIP, string _strID, int iLogonState)
        {
            //iLogonState 登陆状态
            switch (iLogonState)
            {
                case LOGON_SUCCESS://登陆成功显示设备ID号
                    {
                        Log("DVR/NVR登陆成功:" + _cIP.ToString() + "  id:" + _strID);
                        //blogin = true;
                        break;
                    }
                case LOGON_FAILED:
                case LOGON_ING:
                case LOGON_RETRY:
                case NOT_LOGON:
                case LOGON_TIMEOUT://登陆失败
                    {
                        Log("DVR登陆失败:" + _cIP.ToString() + "  id:" + _strID);
                        break;
                    }
            }
        }

        //WCM_VIDEO_HEAD消息处理函数
        private void VideoArrive()
        {
            NVS_RECT rect = new NVS_RECT();

            //视频到达后开始播放   
            int i = NVSSDK.NetClient_StartPlay(connID, playArgs.PlayWnd, rect, 0);
            if (i == 0)
            {
                if (OnMessage != null)
                    OnMessage(this, RealPlayControlEnum.PLAY.ToString());
                bPlaying = true;
            }
            else
                bPlaying = false;
            Debug.WriteLine("VideoArrive 事件NetClient_StartPlay:" + i.ToString());
        }

        //WCM_VIDEO_DISCONNECT消息处理函数
        private void VideoDisconnect(UInt32 _uiConID)
        {

        }

        //WCM_ERR_ORDER消息处理函数
        private void NetDisconnect(string _strIP)
        {
            //string strMSG = "连接到网络视频服务器";
            //strMSG += _strIP;
            //strMSG += "的网络意外断开！";
            // MessageBox.Show(strMSG);
        }

        //WCM_ERR_DATANET消息处理函数
        private void NetDataError(string _strIP)
        {
            //string strMSG = "网络视频服务器";
            //strMSG += _strIP;
            //strMSG += "的连接数达到最大！";
            //MessageBox.Show(strMSG);
        }

        //WCM_RECORD_ERR消息处理函数
        private void RecordError(UInt32 _uiConID)
        {
            //bool isCurrentFrame = false;
            ////连接ID为_uiCon的窗口停止录像
            //for (int i = 0; i < CONST_iFrameNum; i++)
            //{
            //    if (m_conState[i].m_uiConID == _uiConID)
            //    {
            //        //停止将收到的数据写入文件
            //        NVSSDK.NetClient_StopCaptureFile(_uiConID);
            //        if (i == m_iCurrentFrame)
            //        {
            //            isCurrentFrame = true;
            //        }
            //    }
            //}
            ////如果当前窗口录像错误，则更新录像按钮的Caption为Record
            //if (isCurrentFrame == true)
            //{
            //    btnRecord.Text = "Record";
            //}
            //MessageBox.Show("Record error !");
        }
        #endregion

        #region 构造与晰构
        //private static IntPtr lptr;
        public TandiDvrRealPlayer()
        {
            // voiceDataCallBack = new HCNetSDK.VoiceDataCallBack(fVoiceDataCallBack);
        }

        private bool disposed = false;
        ///
        /// 实现IDisposable中的Dispose方法
        ///

        public void Dispose()
        {
            //必须为true
            Dispose(true);
            //通知垃圾回收机制不再调用终结器(析构器)
            GC.SuppressFinalize(this);

        }
        ///
        /// 必须，以备程序员忘记了显式调用Dispose方法
        ///
        ~TandiDvrRealPlayer()
        {
            //必须为false
            Dispose(false);

        }
        ///
        /// 非密封类修饰用protected virtual
        /// 密封类修饰用private
        ///
        ///
        private void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            if (disposing)
            {
                // 此处添加清理托管资源
            }

            //------- 此处添加清理非托管资源------------------//

            //if (PlayID > 01)  //停止播放
            //{
            //      DVR_StopRealPlay();
            //}

            //------------------------------------------------//

            //让类型知道自己已经被释放
            disposed = true;
        }

        #endregion

        #region 设备初始化
        /// <summary>
        /// 设置播放参数
        /// </summary>
        /// <param name="args"></param>
        public void SetPlayAgrs(VideoRealPlayArgs args)
        {
            playArgs.Clone(args);
        }
        public bool VIDEO_Init()
        {
            //MessageBox.Show("海康初始化成功");

            if (!NVSSDK.bInit)
            {
                NVSSDK.NetClient_SetMSGHandle(WM_MAIN_MESSAGE, mainHandle, MSG_PARACHG, MSG_ALARM);
                NVSSDK.NetClient_SetPort(playArgs.Port, 6000);

                if (!NVSSDK.NetClient_Startup2())
                {
                    Log("DVR SDK初始化失败！");
                    return false;
                }
                NVSSDK.bInit = true;

            }
            return true;
        }
        //清理资源
        public void DVR_Cleanup()
        {
            DVR_Logout();
            if (NVSSDK.bInit)
            {
                NVSSDK.bInit = false;
                NVSSDK.NetClient_Cleanup2();
            }
        }


        #endregion

        #region 设备注册
        private bool DVR_Login()
        {
            VIDEO_Init();
            VideoRealPlayArgs e = playArgs;
            if (e.Ip == "0.0.0.0" || e.Ip == "")
            {
                Log("实时视频播放失败，没有正确设置播放参数！");
                return false;

            }
            string key = e.Ip + ":" + e.Port.ToString();
            //已经登录，但登录ID不正确，直接删除重新登录
            if (logidTable.ContainsKey(key) && ((DvrLoginInfo)logidTable[key]).loginId == -1)
            {
                logidTable.Remove(key);
            }

            if (!logidTable.ContainsKey(key))
            {

                //GHIBMS.DvrSDK.HCNetSDK.NET_DVR_DEVICEINFO_V30 lpDeviceInfo = new GHIBMS.DvrSDK.HCNetSDK.NET_DVR_DEVICEINFO_V30();
                ////NET_DVR_DEVICEINFO lpDeviceInfo = new NET_DVR_DEVICEINFO();
                //int userId = HCNetSDK.NET_DVR_Login_V30(e.Ip, e.Port, e.UserName, e.Password, ref lpDeviceInfo);
                //if (userId > -1)
                //{
                //    loginID = userId;
                //    DvrLoginInfo info = new DvrLoginInfo();
                //    info.loginId = userId;
                //    info.DeviceInfo = lpDeviceInfo;
                //    logidTable.Add(key, info);
                //    string msg = String.Format("DVR登录成功！IP：{0} 返回的用户ID:{1}", e.Ip, userId);
                //    Log(msg);
                //    return true;
                //}
                //else
                //{
                //    Log("DVR登录失败！ IP:" + e.Ip + "  " + e.Port);
                //    return false;
                //}


                string strProxy = "";
                string strIP = playArgs.Ip;
                string strUser = playArgs.UserName;
                string strPwd = playArgs.Password;
                string strProxyID = "";
                int iPort = playArgs.Port;
                int iRet;

                //登录指定的网络视频服务器
                iRet = NVSSDK.NetClient_Logon(strProxy, strIP, strUser, strPwd, strProxyID, iPort);
                if (iRet < 0)
                {

                    return false;
                }

                int state = int.MaxValue;
                int i = 0;
                while (state != 0)
                {
                    state = NVSSDK.NetClient_GetLogonStatus(iRet);
                    pubFun.DelayMilliseconds(100);
                    i++;
                    if (i > 50)
                        break;

                }


                if (iRet < 0)
                {

                    Log("DVR登录失败！ IP:" + e.Ip + "  " + e.Port);
                    string msg = String.Format("DVR登录失败！IP：{0} 错误码:{1}", e.Ip, (Marshal.GetLastWin32Error() - USER_ERROR));
                    Log(msg);
                    //MessageBox.Show(msg);
                    return false;
                }
                else
                {
                    DvrLoginInfo info = new DvrLoginInfo();
                    info.loginId = iRet;

                    logidTable.Add(key, info);
                    string msg = String.Format("DVR登录成功！IP：{0} 返回的用户ID:{1}", e.Ip, iRet);
                    Log(msg);
                    // MessageBox.Show(msg);
                    return true;
                }

            }
            else //已经登录过了，直接返回
            {
                return true;
            }
        }
        private int Login()
        {

            VideoRealPlayArgs e = playArgs;
            string key = e.Ip + ":" + e.Port.ToString();
            if (logidTable.ContainsKey(key))
            {
                //存在正确的登录ID，返回
                int id = ((DvrLoginInfo)logidTable[key]).loginId;
                if (id > -1)
                    return id;
                else
                {
                    //登录ID不正确，移出HT,再次调用自我
                    logidTable.Remove(key);
                    DVR_Login();
                }
            }
            else //没有登录
            {
                DVR_Login();
            }
            //登录后再次查找logID
            if (logidTable.ContainsKey(key))
            {
                //存在正确的登录ID，返回
                int id = ((DvrLoginInfo)logidTable[key]).loginId;
                if (id > -1)
                    return id;
            }
            return -1;
        }
        private DvrLoginInfo GetLoginDvrInfo()
        {
            VideoRealPlayArgs e = playArgs;
            string key = e.Ip + ":" + e.Port.ToString();
            if (logidTable.ContainsKey(key))
            {
                return (DvrLoginInfo)logidTable[key];
            }
            return new DvrLoginInfo();
        }
        public static bool DVR_Logout()
        {
            foreach (DictionaryEntry de in logidTable)
            {
                int id = ((DvrLoginInfo)de.Value).loginId;

                if (id > -1)
                {
                    if (NVSSDK.NetClient_Logoff(id) != 0)
                    {

                    }
                }

            }
            logidTable.Clear();
            return true;
        }
        #endregion

        #region 视频预览

        public bool VIDEO_StartRealPlay()
        {
            try
            {
                if (PlayID != UInt32.MaxValue)
                {
                    VIDEO_StopRealPlay();
                }

                VideoRealPlayArgs e = playArgs;
                if (e.Ip == "0.0.0.0")
                {
                    Log("实时视频播放失败，没有正确设置播放参数！CAM:" + playArgs.CamName);
                    return false;
                }
                loginID = Login();
                if (loginID == -1)
                {
                    Log("实时视频播放失败，没有正确登录DVR！IP" + playArgs.Ip);
                    return false;
                }
                //当前登录状态结构体
                CLIENTINFO m_cltInfo = new CLIENTINFO();
                m_cltInfo.m_iServerID = loginID;
                m_cltInfo.m_iChannelNo = e.DvrCh - 1;
                switch (playArgs.TCPMode)
                {
                    case TCPModeEnum.TCP:
                        m_cltInfo.m_iNetMode = 1;
                        break;
                    case TCPModeEnum.UDP:
                        m_cltInfo.m_iNetMode = 2;
                        break;
                    case TCPModeEnum.MULTI:
                        m_cltInfo.m_iNetMode = 3;
                        break;
                    default:
                        m_cltInfo.m_iNetMode = 1;
                        break;

                }
                m_cltInfo.m_iStreamNO = (playArgs.EncodeMode == EncodeTypeEnum.主码流 ? 0 : 1);

                m_cltInfo.m_cNetFile = new char[255];
                m_cltInfo.m_cRemoteIP = new char[16];
                System.Array.Copy(playArgs.Ip.ToCharArray(), m_cltInfo.m_cRemoteIP, playArgs.Ip.Length);
                UInt32 uiConID = UInt32.MaxValue;
                int iRet;

                //UInt32 uiConID = m_conState[m_iCurrentFrame].m_uiConID;
                // cbkDataArriveDelegate = new NVSDATA_NOTIFY(NvsDataNotify);
                //获得当前窗口对应的视频播放状态
                // int iRet = NVSSDK.NetClient_GetPlayingStatus(uiConID);
                //开始接收一路视频数据	

                iRet = NVSSDK.NetClient_StartRecv(ref uiConID, ref m_cltInfo, null);
                Debug.WriteLine("NetClient_StartRecv:" + iRet.ToString());

                if (iRet < 0)
                {
                    connID = UInt32.MaxValue;
                    string msg = String.Format("实时视频流接收失败！IP：{0} 通道号:{1}错误码：{2}", e.Ip, e.DvrCh, (Marshal.GetLastWin32Error() - USER_ERROR));
                    Log(msg);
                    return false;
                }
                else
                {
                    connID = uiConID;
                }

                //开始导出收到的数据
                NVSSDK.NetClient_StartCaptureData(uiConID);
                //Debug.WriteLine("NetClient_StartCaptureData:"+i.ToString());
                if (iRet == 1)
                {
                    NVS_RECT rect = new NVS_RECT();
                    //开始播放某路视频
                    NVSSDK.NetClient_StartPlay(uiConID, e.PlayWnd, rect, 0);
                }

            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
                Console.WriteLine(ex.ToString());
            }
            return false;
        }
        private void NvsDataNotify(uint _ulID, string _strData, int _iLen, int _iUser)
        {
            Debug.WriteLine(_ulID);
            Debug.WriteLine(_strData);
        }
        public bool VIDEO_StopRealPlay()
        {

            try
            {
                if (connID == Int32.MaxValue)
                {
                    PlayID = Int32.MaxValue;
                    bPlaying = false;
                    bRecording = false;
                    return true;
                }
                //如果正在录像，先停止
                if (bRecording)
                    VIDEO_StopSaveRealData();

                VideoRealPlayArgs e = playArgs;

                if (PlayID >= 0)
                {
                    int r = int.MaxValue;
                    r = NVSSDK.NetClient_StopCaptureData(PlayID);
                    if (r != 0)
                    {
                        Console.WriteLine("Error:NetClient_StopCaptureData" + PlayID);
                    }
                    else
                    {
                        Console.WriteLine("Suceed:NetClient_StopCaptureData" + PlayID);
                    }
                    r = NVSSDK.NetClient_StopPlay(PlayID);
                    if (r != 0)
                    {
                        Console.WriteLine("Error:NetClient_StopPlay" + PlayID);
                    }
                    else
                    {
                        Console.WriteLine("Suceed:NetClient_StopPlay" + PlayID);
                    }
                    r = NVSSDK.NetClient_StopRecv(PlayID);
                    if (r != 0)
                    {
                        Console.WriteLine("Error:NetClient_StopRecv" + PlayID);
                    }
                    else
                    {
                        Console.WriteLine("Suceed:NetClient_StopRecv" + PlayID);
                    }


                }
                connID = Int32.MaxValue;
                PlayID = Int32.MaxValue;
                bRecording = false;
                bPlaying = false;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return true;
        }
        public void RealDataCallBack_V30(int lRealHandle, uint dwDataType, byte[] pBuffer, uint dwBufSize, IntPtr pUser)
        {
        }


        #endregion

        #region 抓拍与录像
        public bool VIDEO_SaveRealData(string fileName)
        {
            VideoRealPlayArgs e = playArgs;

            int logid = Login();
            if (logid == -1)
            {
                Log("视频录像失败，没有正确登录设备！");
                return false;
            }
            if (PlayID == UInt32.MaxValue)
            {
                Log("视频录像失败，视频没有正常播放！");
                return false;
            }

            string time = System.DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string path;
            if (!NetVideoControl.RecPath.EndsWith("\\"))
                path = NetVideoControl.RecPath + "\\" + pubFun.DateStr + "\\";
            else
                path = NetVideoControl.RecPath + pubFun.DateStr + "\\";
            if (!GetDriver(path.Substring(0, 1)))
            {
                MessageBox.Show("该磁盘不存在!");
                return false;
            }
            if (Directory.Exists(path) == false)
            {
                // MessageBox.Show("该目录不存在，自动创建‘" + textBox1.Text + "\\’" + "目录");
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch { MessageBox.Show("创建录像文件失败!"); }
            }




            string sPicFileName = String.Format("{0}{1}_{2}_{3}_{4}.sdv", path, fileName, playArgs.Ip, playArgs.DvrCh, time);
            if (NVSSDK.NetClient_StartCaptureFile(PlayID, sPicFileName, 0) != 0)
            {
                string msg = String.Format("视频录像失败！IP：{0} 通道号:{1}", playArgs.Ip, playArgs.DvrCh);
                Log(msg);
                bRecording = false;
                return false;
            }
            else
            {
                bRecording = true;
                string msg = String.Format("视频录像开始！IP：{0} 通道号:{1}", playArgs.Ip, playArgs.DvrCh);
                Log(msg);
                return true;
            }
            // HCNetSDK.NET_DVR_SaveRealData_V30(lRealHandle, dwTransType, sFileName);
        }
        public bool VIDEO_StopSaveRealData()
        {
            if (PlayID != UInt32.MaxValue && bRecording)
            {
                NVSSDK.NetClient_StopCaptureFile(PlayID);

                string msg = String.Format("视频录像停止成功！IP：{0} 通道号:{1}", playArgs.Ip, playArgs.DvrCh);
                Log(msg);
                bRecording = false;
                return true;

            }
            return true;
        }
        public bool VIDEO_CapturePicture(string fileName)
        {
            if (PlayID != UInt32.MaxValue)
            {
                VideoRealPlayArgs e = playArgs;
                if (e == null)
                {
                    Log("实时视频抓拍失败，没有正确设置播放参数！");
                    return false;
                }

                string time = System.DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string path;
                if (!NetVideoControl.PicPath.EndsWith("\\"))
                    path = NetVideoControl.PicPath + "\\" + pubFun.DateStr + "\\";
                else
                    path = NetVideoControl.PicPath + pubFun.DateStr + "\\";
                string date = pubFun.DateStr;
                if (Directory.Exists(path) == false)
                {
                    // MessageBox.Show("该目录不存在，自动创建‘" + textBox1.Text + "\\’" + "目录");
                    try
                    {
                        Directory.CreateDirectory(path);
                    }
                    catch { }
                }

                string sPicFileName = String.Format("{0}{1}_{2}_{3}_{4}.bmp", path, fileName, playArgs.Ip, playArgs.DvrCh, time);

                if (NVSSDK.NetClient_CaptureBmpPic(PlayID, sPicFileName) != 0)
                {
                    string msg = String.Format("图片抓拍失败！IP：{0} 通道号:{1}", playArgs.Ip, playArgs.DvrCh);
                    Log(msg);
                    return false;
                }
                //pubFun.ShowFileDirectory(sPicFileName);
                return false;
            }
            else
            {
                Log("视频没有播放不能抓拍！");
                return false;
            }
        }
        public bool VIDEO_CaptureJPEGPicture(string fileName)
        {
            return VIDEO_CapturePicture(fileName);
            //VideoRealPlayArgs e = playArgs;

            //int logid = Login();
            //if (logid == -1) return false;
            //string time = System.DateTime.Now.ToString("yyyyMMdd_HHmmss_fff");
            //string path;
            //if (!NetVideoControl.PicPath.EndsWith("\\"))
            //    path = NetVideoControl.PicPath + "\\" + pubFun.DateStr + "\\";
            //else
            //    path = NetVideoControl.PicPath + pubFun.DateStr + "\\";
            //if (!GetDriver(path.Substring(0, 1)))
            //{
            //    MessageBox.Show("该磁盘不存在!");
            //    return false;
            //}
            //string date = pubFun.DateStr;
            //if (Directory.Exists(path) == false)
            //{
            //    // MessageBox.Show("该目录不存在，自动创建‘" + textBox1.Text + "\\’" + "目录");
            //    try
            //    {
            //        Directory.CreateDirectory(path);
            //    }
            //    catch { }
            //}

            //string sPicFileName = String.Format("{0}{1}_{2}_{3}_{4}.jpg", path, fileName, playArgs.Ip, playArgs.DvrCh, time);
            ////Console.WriteLine("___"+sPicFileName);
            //HCNetSDK.NET_DVR_JPEGPARA lpJpegPara = new HCNetSDK.NET_DVR_JPEGPARA();

            //lpJpegPara.wPicQuality = 0; //图像质量 Image quality
            //lpJpegPara.wPicSize = 0xff; //抓图分辨率 Picture size: 0xff-Auto(使用当前码流分辨率) 
            ////抓图分辨率需要设备支持，更多取值请参考SDK文档


            //DvrLoginInfo devInfo = GetLoginDvrInfo();

            ////设备模拟通道个数
            //int AChanNum = devInfo.DeviceInfo.byChanNum;
            ////模拟通道的起始通道号，目前设备模拟通道号从1开始
            //int AStartChan = devInfo.DeviceInfo.byStartChan;
            ////设备最大数字通道个数
            //int IPChanNum = devInfo.DeviceInfo.byIPChanNum;
            ////起始数字通道号，0表示无效
            //int IPStartChan = devInfo.DeviceInfo.byStartDChan;
            //int dvrCH = 0;
            //if (AChanNum == 0 && IPChanNum > 0) //只有数字通道
            //{
            //    dvrCH = e.DvrCh + IPStartChan - 1;//预览的设备通道 the device channel number
            //}
            //else
            //{
            //    //如果是混合录像机，数字通道要自行加上数字的起始通道，通常是加32;
            //    dvrCH = e.DvrCh + AStartChan - 1;
            //}


            //if (!HCNetSDK.NET_DVR_CaptureJPEGPicture(logid, dvrCH, ref lpJpegPara, sPicFileName))
            //{
            //    string msg = String.Format("图片抓拍失败！IP：{0} 通道号:{1},错误码：{2}", playArgs.Ip, playArgs.DvrCh, HCNetSDK.NET_DVR_GetLastError());
            //    Log(msg);
            //    return false;
            //}
            ///pubFun.ShowFileDirectory(sPicFileName);
            //return false;
        }

        #endregion

        #region 录像文件回放、下载

        /*public  int DVR_SearchFile(int UerID, int lChannel, uint m_iFileType, DateTime dtStar, DateTime dtEnd, ListView list,Label lblSearchState,Button btnSearch)
        {
          
             lUserID = UerID;
             lsv = list;
             _lblSearch = lblSearchState;
             _btnSearch = btnSearch;
             if (!m_bSearching)
             {

                 HCNetSDK.NET_DVR_TIME StartTime = new HCNetSDK.NET_DVR_TIME();
                 HCNetSDK.NET_DVR_TIME StopTime = new HCNetSDK.NET_DVR_TIME();

                 StartTime.dwYear = (uint)dtStar.Year;
                 StartTime.dwMonth = (uint)dtStar.Month;
                 StartTime.dwDay = (uint)dtStar.Day;
                 StartTime.dwHour = (uint)dtStar.Hour;
                 StartTime.dwMinute = (uint)dtStar.Minute;
                 StartTime.dwSecond = (uint)dtStar.Second;
                 StopTime.dwYear = (uint)dtEnd.Year;
                 StopTime.dwMonth = (uint)dtEnd.Month;
                 StopTime.dwDay = (uint)dtEnd.Day;
                 StopTime.dwHour = (uint)dtEnd.Hour;
                 StopTime.dwMinute = (uint)dtEnd.Minute;
                 StopTime.dwSecond = (uint)dtEnd.Second;
                 uint dwFileType = 0xFF;
                 switch (m_iFileType)
                 {
                     case 0:
                         dwFileType = 0xFF;
                         break;
                     case 1:
                         dwFileType = 0;
                         break;

                     case 2:
                         dwFileType = 1;
                         break;
                     case 3:
                         dwFileType = 2;
                         break;
                 }

                 byte[] sCardNumber = new byte[32];
                 m_struFileCond.lChannel = lChannel;
                 m_struFileCond.dwFileType = dwFileType;
                 m_struFileCond.dwIsLocked = 0xFF;
                 m_struFileCond.sCardNumber = sCardNumber;
                 m_struFileCond.dwUseCardNo = 0;
                 m_struFileCond.struStartTime = StartTime;
                 m_struFileCond.struStopTime = StopTime;
                 m_lFileHandle = HCNetSDK.NET_DVR_FindFile_V30(lUserID, ref m_struFileCond);
                 if (m_lFileHandle != -1)
                 {
                     Thread_SearchFile = new Thread(new ThreadStart(ThreadSearchFile));
                     Thread_SearchFile.Start();
                 }

             }
             else
             {
                 if (Thread_SearchFile != null)
                     Thread_SearchFile.Abort();
             
             }
             return m_lFileHandle;
        }
        public override bool DVR_FindClose(int lFindHandle)
        {
            return HCNetSDK.NET_DVR_FindClose(lFindHandle);
        }
        private void ThreadSearchFile()
        {
            /*
         
            //  int m_lFileHandle = HCNetSDK.NET_DVR_FindFile(lUserID, 1, 0xFF, ref StartTime,ref  StopTime);

            if (m_lFileHandle != -1)
            {
                HCNetSDK.NET_DVR_FINDDATA_V30 struFileInfo = new HCNetSDK.NET_DVR_FINDDATA_V30();
                while (true)
                {
                    int lRet = HCNetSDK.NET_DVR_FindNextFile_V30(m_lFileHandle, out struFileInfo);
                    if (lRet == HCNetSDK.NET_DVR_FILE_SUCCESS)
                    {
                        //显示文件列表
                        UpdateListView updateListView = new UpdateListView();
                        updateListView.lsv = lsv;
                        string starttime = struFileInfo.struStartTime.dwYear + "-" + struFileInfo.struStartTime.dwMonth + "-" + struFileInfo.struStartTime.dwDay
                                           + " " + struFileInfo.struStartTime.dwHour + ":" + struFileInfo.struStartTime.dwMinute + ":" + struFileInfo.struStartTime.dwSecond;
                        string endtime = struFileInfo.struStopTime.dwYear + "-" + struFileInfo.struStopTime.dwMonth + "-" + struFileInfo.struStopTime.dwDay
                                           + " " + struFileInfo.struStopTime.dwHour + ":" + struFileInfo.struStopTime.dwMinute + ":" + struFileInfo.struStopTime.dwSecond;

                        string filesize;
                        if (struFileInfo.dwFileSize / 1024 == 0)
                        {
                            filesize = struFileInfo.dwFileSize.ToString();
                        }
                        else if (struFileInfo.dwFileSize / 1024 > 0 && struFileInfo.dwFileSize / (1024 * 1024) == 0)
                        {
                            filesize = (struFileInfo.dwFileSize / 1024).ToString() + "K";
                        }
                        else// if ()
                        {
                            filesize = (struFileInfo.dwFileSize / 1024 / 1024).ToString() + "M";
                        }

                        string filename = struFileInfo.sFileName;
                        updateListView.AddItem(Convert.ToDateTime(starttime).ToString(), Convert.ToDateTime(endtime).ToString(), filesize, filename);

                    }
                    else
                    {
                        if (lRet == HCNetSDK.NET_DVR_ISFINDING)
                        {
                            Thread.Sleep(5);
                            continue;
                        }
                        if ((lRet == HCNetSDK.NET_DVR_NOMOREFILE) || (lRet == HCNetSDK.NET_DVR_FILE_NOFIND))
                        {
                            UpdateListView updateListView = new UpdateListView();
                            updateListView.lbl = _lblSearch;
                            updateListView.btn = _btnSearch;
                            updateListView.SetLabelText("获取文件列表结束");
                            updateListView.SetButtonText("查找");
                            m_bSearching = false;
                            break;

                        }
                        else
                        {
                            UpdateListView updateListView = new UpdateListView();
                            updateListView.lbl = _lblSearch;
                            updateListView.btn = _btnSearch;
                            updateListView.SetLabelText("获取文件列表异常终止");
                            updateListView.SetButtonText("查找");
                            m_bSearching = false;
                            break;

                        }

                    }
                }
            }
            else 
            { 
                   UpdateListView updateListView = new UpdateListView();
                   updateListView.lbl = _lblSearch;
                   updateListView.SetLabelText(" 获取文件列表失败!");
                   updateListView.btn = _btnSearch;
                   updateListView.SetButtonText("查找");
                   m_bSearching = false;
            
            }
        
        
        }
        public override int  DVR_PlayBackByName(int lUserID, string sPlayBackFileName, IntPtr hWnd)
        {
           return HCNetSDK.NET_DVR_PlayBackByName(lUserID, sPlayBackFileName, hWnd);
        
        }
        public override bool DVR_StopPlayBack(int lPlayHandle)
        {
            return HCNetSDK.NET_DVR_StopPlayBack(lPlayHandle);
        }
        public override bool DVR_PlayBackControl(int lPlayHandle, uint dwControlCode, uint dwInValue, out uint LPOutValue)
        {
            return HCNetSDK.NET_DVR_PlayBackControl(lPlayHandle, dwControlCode, dwInValue, out  LPOutValue);
        }
        public override bool DVR_RefreshPlay(int lPlayHandle)
        { 
            return HCNetSDK.NET_DVR_RefreshPlay(lPlayHandle);
        }
        public override bool DVR_PlayBackCaptureFile(int lPlayHandle, string sFileName)
        {
            return HCNetSDK.NET_DVR_PlayBackCaptureFile(lPlayHandle, sFileName);
        
        }
        public override int DVR_GetFileByName(int lUserID, string sDVRFileName, string sSavedFileName)
        {
            return HCNetSDK.NET_DVR_GetFileByName(lUserID, sDVRFileName, sSavedFileName);
        }
        public override bool DVR_StopGetFile(int lFileHandle)
        { 
            return HCNetSDK.NET_DVR_StopGetFile( lFileHandle);
        }
        public override int DVR_GetDownloadPos(int lFileHandle)
        { 
            return HCNetSDK.NET_DVR_GetDownloadPos(lFileHandle);
          
        }
        public void DVR_SetShow(int lUserID, int lChannel, string strShow, int iLine)
        {
            byte[] source = new byte[44];
            byte[] destin = new byte[44];
            for (int i = 0; i < 8; i++)
            {
                if (i == iLine)
                {
                    source = System.Text.Encoding.Default.GetBytes(strShow);
                    int len;
                    if (source.Length > 44)
                        len = 44;
                    else
                        len = source.Length;
                    System.Array.Copy(source, destin, len);
                    m_ShowStringInfo[i].wStringSize = Convert.ToUInt16(destin.Length);
                    m_ShowStringInfo[i].sString = destin;
                    PICCFG.struStringInfo[i] = m_ShowStringInfo[i];
                    break;
                }
            }
           
            PICCFG.dwSize = (uint)Marshal.SizeOf(PICCFG);

            int size = Marshal.SizeOf(PICCFG);//返回对象的大小
            lptr = Marshal.AllocHGlobal(size);//根据大小分配内存
            Marshal.StructureToPtr(PICCFG, lptr, false);   // 将数据送到内存块
            if (HCNetSDK.NET_DVR_SetDVRConfig(lUserID, 1031, 1, lptr, (uint)size)) ;
            {
                // MessageBox.Show("OK");
                //log.Debug("叠加字符：:" + var.Name + "value:" + var.Value);

            }
           // Marshal.FreeHGlobal(lptr);

        }*/
        #endregion

        #region 云台控制
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dwPTZCommand"></param>
        /// <param name="dwStop">1：停止，0：运行</param>
        /// <param name="dwSpeed">速度1-7</param>
        /// <returns></returns>
        public bool VIDEO_PTZControlWithSpeed(uint dwPTZCommand, uint dwStop, uint dwSpeed)
        {
            Debug.WriteLine("VIDEO_PTZControlWithSpeed speed:" + dwSpeed);
            if (connID == uint.MaxValue) return false;
            int iParam1 = (int)dwSpeed * 14;
            int iParam2 = (int)dwSpeed * 14;
            int TandiPTZCode = ConverPTZCommand(dwPTZCommand, dwStop);

            DevControl(TandiPTZCode, iParam1, iParam2);

            return true;


            //if (NVSSDK.NetClient_DeviceCtrlEx(loginID,  playArgs.DvrCh-1, ConverPTZCommand(dwPTZCommand), iParam1, iParam2,0)!=0)
            //{
            //    string msg = String.Format("云台控制失败！IP：{0} 通道号:{1}", playArgs.Ip, playArgs.DvrCh);
            //    Log(msg);
            //    return false;
            //}
            //else
            //    return true;
        }
        public bool VIDEO_PTZPreset(uint dwPTZCommand, uint PresetIndex)
        {
            int TandiPTZCode = ConverPTZCommand(dwPTZCommand, 0);

            DevControl(TandiPTZCode, (int)PresetIndex, 0);
            return true;

        }
        public bool VIDEO_PTZCruise(uint dwPTZCommand, byte CruiseRoute, byte CruisePoint, ushort Input)
        {

            return true;
        }
        public bool VIDEO_PTZTrack(uint dwPTZCommand, uint dwTrackIndex)
        {

            return true;

        }
        public static bool GetDriver(string disk)
        {
            string[] s = Directory.GetLogicalDrives();

            foreach (string str in s)
            {
                if (str.ToLower().StartsWith(disk.ToLower()))
                    return true;
            }
            return false;
        }
        //通过协议对设备进行控制
        //_iAction 动作码
        //_iParam1 水平速度或预置位号
        //_iParam2 垂直速度
        private int ProtocalControl(int _iAction, int _iParam1, int _iParam2)
        {
            int iLogonID = GetLoginDvrInfo().loginId;
            int iChannelNo = playArgs.DvrCh - 1;
            int iRet = -1;

            //电子云台的参数,控制类型(Normal,e_PTZ)
            int iControlType = 0;

            //控制网络视频服务器某一通道所连接的设备动作
            iRet = NVSSDK.NetClient_DeviceCtrlEx
            (
                iLogonID,
                iChannelNo,
                _iAction,
                _iParam1,
                _iParam2,
                iControlType
            );
            if (iRet < 0)//设备控制失败
            {
                Log("NetClient_DeviceCtrlEx Failed ! USER_ERROR+" + (Marshal.GetLastWin32Error() - USER_ERROR));
            }
            return iRet;
        }

        //通过透明通道对设备进行控制
        //_iAction 动作码
        //_iParam1 水平速度或预置位号
        //_iParam2 垂直速度
        private int ChannelControl(int _iAction, int _iParam1, int _iParam2)
        {
            int iLogonID = GetLoginDvrInfo().loginId;
            string strDevType = "DOME_PELCO_D";
            if (strDevType.Substring(0, 4) != "DOME")
            {
                return -1;
            }
            int iComNo = 1;
            int iRet = -1;

            //创建并设置控制参数结构体
            CONTROL_PARAM cParam = new CONTROL_PARAM();
            cParam.m_ptMove.x = _iParam1;//水平速度
            cParam.m_ptMove.y = _iParam2;//垂直速度

            //CALL_VIEW : 62  调用预置位 
            //SET_VIEW  : 63  设置预置位
            if (_iAction == 62 || _iAction == 63)
            {
                cParam.m_iPreset = _iParam1; //预置位号
            }

            cParam.m_btBuf = new byte[256];//需要处理的数据
            try
            {
                //设备地址
                cParam.m_iAddress = Int32.Parse(playArgs.CamID);
            }
            catch (System.Exception ex)
            {
                //转换失败
                //  MessageBox.Show(ex.Message);
                return -1;
            }

            //调用设备类型对应的dll文件中的控制码处理函数
            iRet = NVSSDK.NetClient_GetControlCode(strDevType, _iAction, ref cParam);
            if (iRet != 1)//控制码处理失败
            {
                MessageBox.Show("NetClient_GetControlCode Failed ! ");
                return -1;
            }

            //通过透明通道进行串口的数据发送操作
            iRet = NVSSDK.NetClient_ComSend
            (
                iLogonID,
                cParam.m_btBuf,
                cParam.m_iCount,
                iComNo
            );
            if (iRet < 0) //数据发送失败
            {
                // MessageBox.Show("NetClient_ComSend Failed ! USER_ERROR+" + (Marshal.GetLastWin32Error() - USER_ERROR));
            }
            return iRet;
        }

        //对设备进行控制
        //_iAction 动作码
        //_iParam1 水平速度或预置位号
        //_iParam2 垂直速度
        private int DevControl(int _iAction, int _iParam1, int _iParam2)
        {
            UInt32 uiConID = connID;

            //当前视频窗口没有播放
            if (uiConID == UInt32.MaxValue)
            {
                return -1;
            }
            //0 协议方式 1： 透明通道 
            int iWorkMode = 0;
            int iParam1 = _iParam1;
            int iParam2 = _iParam2;
            int iSpeed = _iParam1;
            int iPreset = 0;
            switch (_iAction)
            {
                case MOVE_UP://向上移动
                    iParam1 = 0;
                    iParam2 = iSpeed;

                    //MOVE 透明通道移动码
                    _iAction = iWorkMode == 0 ? _iAction : MOVE;
                    break;
                case MOVE_DOWN://向下移动
                    iParam1 = 0;
                    iParam2 = iSpeed;
                    _iAction = iWorkMode == 0 ? _iAction : MOVE;
                    break;
                case MOVE_LEFT://向左移动
                    iParam1 = iSpeed;
                    iParam2 = 0;
                    _iAction = iWorkMode == 0 ? _iAction : MOVE;
                    break;
                case MOVE_RIGHT://向右移动
                    iParam1 = iSpeed;
                    iParam2 = 0;
                    _iAction = iWorkMode == 0 ? _iAction : MOVE;
                    break;
                case MOVE_UP_LEFT://向左上移动
                    iParam1 = iSpeed;
                    iParam2 = iSpeed;
                    _iAction = iWorkMode == 0 ? _iAction : MOVE;
                    break;
                case MOVE_UP_RIGHT://向右上移动
                    iParam1 = iSpeed;
                    iParam2 = iSpeed;
                    _iAction = iWorkMode == 0 ? _iAction : MOVE;
                    break;
                case MOVE_DOWN_LEFT://向左下移动
                    iParam1 = iSpeed;
                    iParam2 = iSpeed;
                    _iAction = iWorkMode == 0 ? _iAction : MOVE;
                    break;
                case MOVE_DOWN_RIGHT://向右下移动
                    iParam1 = iSpeed;
                    iParam2 = iSpeed;
                    _iAction = iWorkMode == 0 ? _iAction : MOVE;
                    break;
                case ZOOM_BIG://变倍大
                    iParam1 = iSpeed;
                    iParam2 = iSpeed;
                    _iAction = iWorkMode == 0 ? _iAction : 31;
                    break;
                case ZOOM_SMALL://变倍小
                    iParam1 = iSpeed;
                    iParam2 = iSpeed;
                    _iAction = iWorkMode == 0 ? _iAction : 33;
                    break;
                case FOCUS_NEAR://聚焦近
                    iParam1 = iSpeed;
                    iParam2 = iSpeed;
                    _iAction = iWorkMode == 0 ? _iAction : 37;
                    break;
                case FOCUS_FAR://聚焦远
                    iParam1 = iSpeed;
                    iParam2 = iSpeed;
                    _iAction = iWorkMode == 0 ? _iAction : 35;
                    break;
                case IRIS_OPEN://光圈开
                    iParam1 = iSpeed;
                    iParam2 = iSpeed;
                    _iAction = iWorkMode == 0 ? _iAction : 39;
                    break;
                case IRIS_CLOSE://光圈关
                    iParam1 = iSpeed;
                    iParam2 = iSpeed;
                    _iAction = iWorkMode == 0 ? _iAction : 41;
                    break;
                case RAIN_ON://雨刷开
                    iParam1 = iSpeed;
                    iParam2 = iSpeed;
                    _iAction = iWorkMode == 0 ? _iAction : 47;
                    break;
                case RAIN_OFF://雨刷关
                    iParam1 = iSpeed;
                    iParam2 = iSpeed;
                    _iAction = iWorkMode == 0 ? _iAction : 48;
                    break;
                case LIGHT_ON://背光开
                    iParam1 = iSpeed;
                    iParam2 = iSpeed;
                    _iAction = iWorkMode == 0 ? _iAction : 43;
                    break;
                case LIGHT_OFF://背光关
                    iParam1 = iSpeed;
                    iParam2 = iSpeed;
                    _iAction = iWorkMode == 0 ? _iAction : 44;
                    break;
                case HOR_AUTO://自动巡航
                    iParam1 = 0;
                    iParam2 = 0;
                    _iAction = iWorkMode == 0 ? _iAction : 21;
                    break;
                case HOR_AUTO_STOP://停止自动巡航
                    iParam1 = 0;
                    iParam2 = 0;
                    _iAction = iWorkMode == 0 ? _iAction : 22;
                    break;
                case CALL_VIEW://调用预置位                    

                    iPreset = _iParam1;

                    iParam1 = iPreset;
                    iParam2 = 0;
                    _iAction = iWorkMode == 0 ? _iAction : 62;
                    break;
                case SET_VIEW://设置预置位
                    iPreset = _iParam1;
                    iParam1 = iPreset;
                    iParam2 = 0;
                    _iAction = iWorkMode == 0 ? _iAction : 63;
                    break;
                case POWER_ON://打开电源
                    iParam1 = iSpeed;
                    iParam2 = iSpeed;
                    _iAction = iWorkMode == 0 ? _iAction : 45;
                    break;
                case POWER_OFF://关闭电源
                    iParam1 = iSpeed;
                    iParam2 = iSpeed;
                    _iAction = iWorkMode == 0 ? _iAction : 46;
                    break;
                default:
                    iParam1 = iSpeed;
                    iParam2 = iSpeed;

                    //9 协议控制停止码
                    _iAction = iWorkMode == 0 ? 9 : _iAction;
                    break;
            }

            if (iWorkMode == 0)// 串口工作方式为协议
            {
                //通过协议方式对设备进行控制
                return ProtocalControl(_iAction, iParam1, iParam2);
            }
            else if (iWorkMode == 1)// 串口工作方式为透明通道
            {
                //通过透明通道对设备进行控制
                return ChannelControl(_iAction, iParam1, iParam2);
            }
            return -1;
        }
        #endregion

        #region 字符叠加

        private void iniShowStringPos()
        {
            ////初始化
            //PICCFG.struStringInfo = new HCNetSDK.NET_DVR_SHOWSTRINGINFO[8];

            //for (int i = 0; i < 4; i++)
            //{
            //string s = "";

            /*byte[] source = new byte[44];
            byte[] destin = new byte[44];

            source = System.Text.Encoding.Default.GetBytes(s);
            int len;
            if (source.Length > 44)
                len = 44;
            else
                len = source.Length;
            System.Array.Copy(source, destin, len);
            */

            //m_ShowStringInfo[i].wShowString = 1;
            //m_ShowStringInfo[i].wStringSize = Convert.ToUInt16(destin.Length);
            //m_ShowStringInfo[i].wShowStringTopLeftX = 0;
            //m_ShowStringInfo[i].wShowStringTopLeftY = Convert.ToUInt16(350 + i * 50);
            //m_ShowStringInfo[i].sString = "test";

            //PICCFG.struStringInfo[i] = m_ShowStringInfo[i];


            //}
            //string ll = Encoding.Default.GetString(PICCFG.struStringInfo[0].sString);

            //MessageBox.Show(ll);

        }
        #region 视频参数

        public VideoEffect VIDEO_GetVideoEffect()
        {
            //if (PlayID == -1) return null;
            //uint pBrightValue = 0;
            //uint pContrastValue = 0;
            //uint pSaturationValue = 0;
            //uint pHueValue = 0;
            VideoEffect ef = new VideoEffect();
            //if (!HCNetSDK.NET_DVR_ClientGetVideoEffect(PlayID, ref pBrightValue, ref pContrastValue, ref pSaturationValue, ref pHueValue))
            //{
            //    return null;
            //}
            //ef.BrightValue = pBrightValue;
            //ef.ContrastValue = pContrastValue;
            //ef.SaturationValue = pSaturationValue;
            //ef.HueValue = pHueValue;
            return ef;
        }
        public bool VIDEO_SetVideoEffect(VideoEffect effect)
        {
            //if (PlayID == -1) return false;
            //if (HCNetSDK.NET_DVR_ClientSetVideoEffect(PlayID, effect.BrightValue, effect.ContrastValue, effect.SaturationValue, effect.HueValue))
            //    return true;
            //else
            return false;
        }

        #endregion

        #region 声音、对讲

        public bool VIDEO_OpenSound()
        {
            //if (PlayID >0)
            //{
            //    if (NVSSDK.NetClient_AudioStart(PlayID))
            //    {
            //        return true;
            //    }
            //}
            return false;

        }
        public bool VIDEO_CloseSound()
        {
            //if (NVSSDK.NetClient_AudioStop(PlayID))
            //{
            //    return true;
            //}
            //else
            return false;

        }
        public bool VIDEO_Volume(ushort wVolume)
        {
            //if (PlayID > 0)
            //{
            //    if (NVSSDK.NetClient_SetLocalAudioVolumeEx(PlayID, wVolume))
            //        return true;
            //}
            return false;
        }
        public bool VIDEO_StartVoiceCom()
        {

            //int logID = Login();
            //if (logID == -1)
            //{
            //    Log("语音对讲失败，没有正确登录DVR！");
            //    return false;
            //}
            //IntPtr pUser = new IntPtr();
            //VoiceComHandle = HCNetSDK.NET_DVR_StartVoiceCom_V30(logID, 1, false, voiceDataCallBack, pUser);
            //// VoiceComHandle = HCNetSDK.NET_DVR_StartVoiceCom(logID,voiceDataCallBack, 0);
            //if (VoiceComHandle > -1)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            return false;

        }
        public bool VIDEO_StopVoiceCom()
        {
            //if (VoiceComHandle > -1)
            //{
            //    if (HCNetSDK.NET_DVR_StopVoiceCom(VoiceComHandle))
            //        return true;
            //}
            return false;

        }
        public bool VIDEO_SetVoiceComClientVolume(ushort wVolume)
        {
            //if (VoiceComHandle > -1)
            //{
            //    if (HCNetSDK.NET_DVR_SetVoiceComClientVolume(VoiceComHandle, wVolume))
            //        return true;
            //}
            return false;

        }
        public void fVoiceDataCallBack(int lVoiceComHandle, string pRecvDataBuffer, uint dwBufSize, byte byAudioFlag, uint dwUser)
        {

        }
        private int ConverPTZCommand(uint hikPTZCommand, uint dwStop)
        {
            int cmd = 0;

            if (dwStop == 0) //运行
            {
                switch (hikPTZCommand)
                {
                    case (uint)PTZCmdCodeEnum.PAN_LEFT:
                        cmd = MOVE_LEFT;
                        break;
                    case (uint)PTZCmdCodeEnum.PAN_RIGHT:
                        cmd = MOVE_RIGHT;
                        break;
                    case (uint)PTZCmdCodeEnum.TILT_UP:
                        cmd = MOVE_UP;
                        break;
                    case (uint)PTZCmdCodeEnum.TILT_DOWN:
                        cmd = MOVE_DOWN;
                        break;
                    case (uint)PTZCmdCodeEnum.UP_LEFT:
                        cmd = MOVE_UP_LEFT;
                        break;
                    case (uint)PTZCmdCodeEnum.UP_RIGHT:
                        cmd = MOVE_UP_RIGHT;
                        break;
                    case (uint)PTZCmdCodeEnum.DOWN_LEFT:
                        cmd = MOVE_DOWN_LEFT;
                        break;
                    case (uint)PTZCmdCodeEnum.DOWN_RIGHT:
                        cmd = MOVE_DOWN_RIGHT;
                        break;
                    case (uint)PTZCmdCodeEnum.ZOOM_IN:
                        cmd = ZOOM_BIG;
                        break;
                    case (uint)PTZCmdCodeEnum.ZOOM_OUT:
                        cmd = ZOOM_SMALL;
                        break;
                    case (uint)PTZCmdCodeEnum.GOTO_PRESET:
                        cmd = CALL_VIEW;
                        break;
                    case (uint)PTZCmdCodeEnum.SET_PRESET:
                        cmd = SET_VIEW;
                        break;
                    case (uint)PTZCmdCodeEnum.CLE_PRESET:
                        cmd = SET_VIEW;
                        break;
                    case (uint)PTZCmdCodeEnum.FILL_PRE_SEQ:

                        break;
                    case (uint)PTZCmdCodeEnum.CLE_PRE_SEQ:

                        break;
                    case (uint)PTZCmdCodeEnum.RUN_SEQ:

                        break;
                    case (uint)PTZCmdCodeEnum.STOP_SEQ:

                        break;
                    default:

                        break;

                }
            }
            else //停止
            {
                switch (hikPTZCommand)
                {
                    case (uint)PTZCmdCodeEnum.PAN_LEFT:
                    case (uint)PTZCmdCodeEnum.PAN_RIGHT:
                    case (uint)PTZCmdCodeEnum.TILT_UP:
                    case (uint)PTZCmdCodeEnum.TILT_DOWN:
                    case (uint)PTZCmdCodeEnum.UP_LEFT:
                    case (uint)PTZCmdCodeEnum.UP_RIGHT:
                    case (uint)PTZCmdCodeEnum.DOWN_LEFT:
                    case (uint)PTZCmdCodeEnum.DOWN_RIGHT:
                        cmd = MOVE_STOP;
                        break;
                    case (uint)PTZCmdCodeEnum.ZOOM_IN:
                        cmd = ZOOM_BIG_STOP;
                        break;
                    case (uint)PTZCmdCodeEnum.ZOOM_OUT:
                        cmd = ZOOM_SMALL_STOP;
                        break;

                }

            }
            return cmd;
        }

        public bool VIDEO_PlayControl(RealPlayControlEnum State)
        {
            if (State == RealPlayControlEnum.PLAY)
            {
                NVS_RECT rect = new NVS_RECT();
                st_ConnectInfo info = new st_ConnectInfo();
                int i = NVSSDK.NetClient_GetInfoByConnectID(connID, ref info);

                //开始播放视频
                int iRet = NVSSDK.NetClient_StartPlay
                (
                    connID,
                    playArgs.PlayWnd,
                    rect,
                    0
                );
                Console.WriteLine("NetClient_StartPlay:" + iRet.ToString());
                return true;// iRet == 0 ? true : false;
            }
            else
            {
                //停止接受视频数据
                int iRet = NVSSDK.NetClient_StopCaptureData(connID);

                //停止播放某路视频
                iRet = NVSSDK.NetClient_StopPlay(connID);
                // playID = UInt32.MaxValue;
                return true;
            }
        }


        #endregion
        #endregion
        struct DvrLoginInfo
        {
            public int loginId;
        }
    }

}
