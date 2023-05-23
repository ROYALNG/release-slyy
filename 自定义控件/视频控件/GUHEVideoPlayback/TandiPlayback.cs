using GHIBMS.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using TandiNVSSDK;
using TandiPlayCSharp;

namespace GHIBMS.VideoPlayback
{
    public class TandiPlayback : IVideoPlayback
    {
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

        /************************************************************************
        *	TC_PutStreamToPlayer返回值说明                                                                     
        ************************************************************************/
        private const int RET_BUFFER_FULL = -11;		//	缓冲区满，数据没有放入缓冲区
        private const int RET_BUFFER_WILL_BE_FULL = -18;		//	即将满，降低送入数据的频率
        private const int RET_BUFFER_WILL_BE_EMPTY = -19;		//	即将空，提高送入数据的频率
        private const int RET_BUFFER_IS_OK = -20;       //  正常可以放数据


        //Message of Download logfile
        private const int WCM_LOGFILE_FINISHED = 17; //When logfile download finished

        //Message of query file
        private const int WCM_QUERYFILE_FINISHED = 18; //Query finished record files
        private const int WCM_DWONLOAD_FINISHED = 19; //Download finished
        private const int WCM_DWONLOAD_FAULT = 20;  //Download fault
        private const int WCM_REBUILDINDEX = 22;  //Finished of rebuild index file

        private System.Windows.Forms.Timer PlayingTimer = new System.Windows.Forms.Timer();
        //处理文件播放完毕
        private delegatePlayEndFun m_ReadEnd;
        private RECVDATA_NOTIFY_EX DownloadNotifyDelegate;

        private int loginID = -1;
        private int playID = -1;
        private uint connID = uint.MaxValue;
        private int iPageNo = 0;
        private int m_iDataLen;
        #region 构造析造
        public TandiPlayback()
        {
            m_ReadEnd = new delegatePlayEndFun(m_PlayEndFun);
            DownloadNotifyDelegate = new RECVDATA_NOTIFY_EX(DownloadDateNotify);
            PlayingTimer.Interval = 100;
            PlayingTimer.Stop();
            PlayingTimer.Tick += new EventHandler(PlayingTimer_Tick);
            TandiPlaySDK.TC_CreateSystem(new IntPtr(0));
            TandiPlaySDK.TC_RegisterNotifyPlayToEnd(m_ReadEnd);

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
        ~TandiPlayback()
        {
            TandiPlaySDK.TC_DeleteSystem();
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

        #region 内部变量与属性
        public static bool hasInit = false;
        public static Hashtable logidTable = new Hashtable();
        private static UInt16 videoWndNubs = 1;

        private bool m_bSearching = false;
        private uint m_uConnID = uint.MaxValue;
        private int m_iPlayerID = -1;
        private int m_lDownloadHanle = -1;
        private bool g_bIsPlaying = true;
        byte[] m_cDataBuffer = new byte[1600];
        private VideoPlaybackArgs m_playbackArgs = new VideoPlaybackArgs();

        private string protocolCode = "JK_DVR_TANDI";
        public string ProtocolCode
        {
            get { return protocolCode; }

        }
        private List<DvrFindData> findDataList = new List<DvrFindData>();
        public object objFind = new object();
        public List<DvrFindData> FindDataList
        {
            get
            {
                lock (objFind)
                {
                    return findDataList;
                }
            }
            set
            {
                lock (objFind)
                {
                    findDataList = value;
                }
            }
        }

        public UInt16 VideoWndNubs
        {
            set { videoWndNubs = value; }
            get { return videoWndNubs; }
        }
        private IntPtr mainHandle;
        private const uint PLAYSDKMSG = 5678;
        public void SetWinMessage(IntPtr lWinHandle)
        {
            mainHandle = lWinHandle;
            TandiPlaySDK.TC_RegisterEventMsg(mainHandle, PLAYSDKMSG);

        }

        #endregion

        #region 事件
        //log事件
        public delegate void OnMessagedelegate(object sender, string msg);
        public event OnMessagedelegate OnMessage;
        private void Log(string message)
        {
            if (OnMessage != null)
                OnMessage(this, message);
        }
        public delegate void OnFileSearchdelegate(object sender, int result, List<DvrFindData> data);
        public event OnFileSearchdelegate OnFileSearch;
        public void OnWinMessage(ref System.Windows.Forms.Message m)
        {
            //WM_MAIN_MESSAGE为自定义的系统消息
            if (m.Msg == WM_MAIN_MESSAGE)
            {
                //自定义消息处理函数
                OnMainMessage(m.WParam, m.LParam);
            }

        }
        //处理SDK系统消息
        private void OnMainMessage(IntPtr wParam, IntPtr lParam)
        {
            try
            {
                //wParam的低16位是消息的类型；
                int iMsgType = wParam.ToInt32() & 0xFFFF;
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
                            LogonNotify(ipAndID.m_pIP.ToCharArray(), ipAndID.m_pID, wParam.ToInt32() >> 16);
                        }
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
                    //查询录像文件结束时产生
                    case WCM_QUERYFILE_FINISHED:
                        //Console.WriteLine("**********WCM_QUERYFILE_FINISHED");
                        QueryFileFinished();
                        break;
                    case WCM_DWONLOAD_FINISHED:
                        {
                            // Console.WriteLine("**********WCM_DWONLOAD_FINISHED");
                            break;
                        }
                    case WCM_DWONLOAD_FAULT:
                        {
                            //Console.WriteLine("**********WCM_DWONLOAD_FAULT");
                            break;
                        }
                    //case WCM_DOWNLOAD_INTERRUPT:
                    //    {
                    //        Console.WriteLine("**********WCM_DOWNLOAD_INTERRUPT");
                    //        break;
                    //    }
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }
        }
        //WCM_LOGON_NOTIFY消息处理函数
        private void LogonNotify(char[] _cIP, string _strID, int iLogonState)
        {
            //iLogonState 登陆状态
            switch (iLogonState)
            {
                case LOGON_SUCCESS://登陆成功显示设备ID号
                    {
                        Debug.WriteLine("登陆成功:" + _cIP.ToString() + "  id:" + _strID);
                        break;
                    }
                case LOGON_FAILED:
                case LOGON_ING:
                case LOGON_RETRY:
                case NOT_LOGON:
                case LOGON_TIMEOUT://登陆失败
                    {
                        break;
                    }
            }
        }

        //WCM_VIDEO_HEAD消息处理函数
        private void VideoArrive()
        {
            //NVS_RECT rect = new NVS_RECT();

            ////视频到达后开始播放   
            //int i = NVSSDK.NetClient_StartPlay(playID, m_playbackArgs.PlayWnd, rect, 0);
            ////if (i == 0)
            ////{
            ////    if (OnMessage != null)
            ////        OnMessage(this, RealPlayControlEnum.PLAY.ToString());
            ////}
            //Debug.WriteLine("NetClient_StartPlay:" );
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
            //MessageBox.Show(strMSG);
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

        private void QueryFileFinished()
        {
            int iTotalCount = 0;
            int iCurrentCount = 0;
            NVSSDK.NetClient_NetFileGetFileCount(loginID, ref iTotalCount, ref iCurrentCount);//查询文件条数
            Debug.WriteLine(string.Format("TotalCount{0},CurrentCount{1})\n", iTotalCount, iCurrentCount));
            double fPages = (double)iTotalCount / 20;
            int MaxPages = (int)Math.Ceiling(fPages);

            if (iCurrentCount > 0)
            {
                for (int i = 0; i < iCurrentCount; i++)
                {
                    NVS_FILE_DATA fileInfo = new NVS_FILE_DATA();
                    int iRet = NVSSDK.NetClient_NetFileGetQueryfile(loginID, i, ref fileInfo);//获取文件信息
                    if (0 == iRet)
                    {
                        Debug.WriteLine(string.Format("file{0}:name{1},ty pe{2},chan{3},size{4}\n",
                           fileInfo.struStartTime, fileInfo.cFileName, fileInfo.iType, fileInfo.iChannel, fileInfo.iFileSize));
                        DvrFindData newFind = new DvrFindData();
                        newFind.FileName = new string(fileInfo.cFileName);
                        newFind.FileSize = fileInfo.iFileSize.ToString();
                        string starttime = fileInfo.struStartTime.iYear + "-" + fileInfo.struStartTime.iMonth + "-" + fileInfo.struStartTime.iDay
                                        + " " + fileInfo.struStartTime.iHour + ":" + fileInfo.struStartTime.iMinute + ":" + fileInfo.struStartTime.iSecond;
                        string endtime = fileInfo.struStoptime.iYear + "-" + fileInfo.struStoptime.iMonth + "-" + fileInfo.struStoptime.iDay
                                           + " " + fileInfo.struStoptime.iHour + ":" + fileInfo.struStoptime.iMinute + ":" + fileInfo.struStoptime.iSecond;
                        newFind.StartTime = Convert.ToDateTime(starttime);
                        newFind.StopTime = Convert.ToDateTime(endtime);
                        findDataList.Add(newFind);
                    }
                    else
                    {
                        Debug.WriteLine("NetFileGetQueryfile failed\n!");
                    }
                }
            }
            else
            {
            }
            m_bSearching = false;
            if (iPageNo >= MaxPages - 1)
            {
                //获取文件结束
                if (OnFileSearch != null)
                    OnFileSearch(this, strConst.NET_DVR_NOMOREFILE, FindDataList);
            }
            else
            {
                iPageNo++;
                findFileNextPage();
            }

        }
        int DownloadFile(int _iLogonID, string _pcFileName)
        {
            int iRet = NVSSDK.NetClient_NetFileDownloadFile(ref m_uConnID, _iLogonID, _pcFileName, "", 0, -1, 1);//录像下载
            if (0 == iRet)
            {
                NVSSDK.NetClient_SetNetFileDownloadFileCallBack(m_uConnID, DownloadNotifyDelegate, IntPtr.Zero);//设置回调
            }
            else
            {
                Debug.WriteLine("NetFileDownloadFile failed!\n");
            }
            PlayingTimer.Start();

            return 0;
        }
        void DownloadDateNotify(uint _ulID, IntPtr _ucData, int _iLen, int _iFlag, int _lpUserData)
        {
            //Debug.WriteLine("DownloadNotify _iFlag" + _iFlag);
            if (_ulID == connID)
            {

                IntPtr hWnd = m_playbackArgs.PlayWnd;
                if (_iFlag == 1)//处理文件头
                {
                    if (m_iPlayerID == -1)
                    {
                        Debug.WriteLine("DownloadNotify _iFlag" + _iFlag);
                        m_iPlayerID = TandiPlaySDK.TC_CreatePlayerFromVoD(hWnd, _ucData, _iLen);//创建VOD播放器
                        Debug.WriteLine(m_iPlayerID);
                        if (m_iPlayerID >= 0)
                        {
                            TandiPlaySDK.TC_Play(m_iPlayerID);//播放
                        }
                        else
                        {
                            Debug.WriteLine("CreatePlayerFromVoD failed!\n");
                        }
                    }
                }
                else//处理数据
                {
                    TandiPlaySDK.TC_PutStreamToPlayer(m_iPlayerID, _ucData, _iLen);
                    CheckStatus();
                    /*
                    if (this.m_iDataLen > 0)
                    {
                        TandiPlaySDK.TC_PutStreamToPlayer(this.m_iPlayerID, m_cDataBuffer, m_iDataLen);
                        m_iDataLen = 0;
                    }
                    int iPushStreamStatus = TandiPlaySDK.TC_PutStreamToPlayer(m_iPlayerID, _ucData, _iLen);
                    Debug.WriteLine("iPushStreamStatus" + iPushStreamStatus);
                    if (iPushStreamStatus == RET_BUFFER_FULL)
                    {
                        if (_iLen < this.m_cDataBuffer.Length)
                        {
                            Array.Copy(this.m_cDataBuffer, _ucData, _iLen);
                            m_iDataLen = _iLen;
                        }
                        PauseDownload();
                    }*/
                }
            }
        }
        private void m_PlayEndFun(int _iID)
        {
            //Trace.WriteLine(_iID.ToString());
            //子线程内访问FORM窗体元素使用invoke方法实现
            /*if (this.InvokeRequired)
            {
                //代理,有参代理
                dePlayEndClient dePEC = new dePlayEndClient(m_PlayEndFun);
                m_panDisPlay[_iID].Invoke(dePEC, _iID);
            }
            else
            {
                //向主线程发送消息
                m_PlayHelper.m_MyPostMessage(this.Handle, WM_CUSTOMMSG, 0, _iID);
                //MyPostMsg(this.Handle, WM_CUSTOMMSG, 0, _iID);
            }*/
            //播放完毕
            if (OnMessage != null)
                OnMessage(this, "播放完成");

        }

        void CheckStatus()
        {
            int iState = 0;
            TandiPlaySDK.TC_GetStreamPlayBufferState(m_iPlayerID, ref iState);//检查缓冲区状态
            switch (iState)
            {
                case RET_BUFFER_WILL_BE_FULL:
                    {
                        NVSSDK.NetClient_NetFileDownloadFile(ref m_uConnID, loginID, "", "", 1, -1, 0);//暂停下载
                    }
                    break;
                case RET_BUFFER_WILL_BE_EMPTY:
                    {
                        int iRet = NVSSDK.NetClient_NetFileDownloadFile(ref m_uConnID, loginID, "", "", 1, -1, 1);//恢复下载
                        if (iRet != 0)
                        {
                            // g_bIsPlaying = false;
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        void PauseDownload()
        {
            //if (m_iLogonID ==  INVALID_ID || m_ulConnID == INVALID_ID)
            //{
            //    return;
            //}
            int iRet = NVSSDK.NetClient_NetFileDownloadFile(ref connID, loginID, "", "", 1, -1, 0);
            if (iRet >= 0)
            {
                // m_iDownloadPause = 1;
            }
        }





        #endregion

        #region 设备初始化
        /// <summary>
        /// 设置播放参数
        /// </summary>
        /// <param name="args"></param>
        public void SetPlayAgrs(VideoPlaybackArgs args)
        {
            m_playbackArgs.Clone(args);
        }
        public bool VIDEO_Init()
        {
            if (!NVSSDK.bInit)
            {
                int i = -1;
                i = NVSSDK.NetClient_SetPort(m_playbackArgs.Port, 6000);
                Debug.WriteLine("NetClient_SetPort:" + i.ToString());
                i = NVSSDK.NetClient_SetMSGHandle(WM_MAIN_MESSAGE, mainHandle, MSG_PARACHG, MSG_ALARM);
                Debug.WriteLine("NetClient_SetMSGHandle:" + i.ToString());

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
            VideoPlaybackArgs e = m_playbackArgs;

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

                string strProxy = "";
                string strIP = e.Ip;
                string strUser = e.UserName;
                string strPwd = e.Password;
                string strProxyID = "";
                int iPort = e.Port;
                int iRet;

                //登录指定的网络视频服务器
                iRet = NVSSDK.NetClient_Logon(strProxy, strIP, strUser, strPwd, strProxyID, iPort);
                if (iRet < 0)
                {

                    return false;
                }

                loginID = iRet;
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

                    return false;
                }
                else
                {
                    loginID = iRet;
                    DvrLoginInfo info = new DvrLoginInfo();
                    info.loginId = iRet;

                    logidTable.Add(key, info);
                    string msg = String.Format("DVR登录成功！IP：{0} 返回的用户ID:{1}", e.Ip, iRet);
                    Log(msg);
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

            VideoPlaybackArgs e = m_playbackArgs;
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
            VideoPlaybackArgs e = m_playbackArgs; ;
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

        #region 查找文件
        uint NextPageFileType;
        byte NextPageIsLock;
        DateTime NextPageStartTime;
        DateTime NextPageEndTime;
        public bool VIDEO_FindFile(uint dwFileType, byte isLock, DateTime dtStar, DateTime dtEnd)
        {
            NextPageFileType = dwFileType;
            NextPageIsLock = isLock;
            NextPageStartTime = dtStar;
            NextPageEndTime = dtEnd;
            FindDataList.Clear();
            return findFileNextPage();

        }
        private bool findFileNextPage()
        {
            VideoPlaybackArgs e = m_playbackArgs;
            if (e.Ip == "0.0.0.0" || e.Ip == "")
            {
                Log("视频回放失败，没有正确设置播放参数！");
                return false;
            }
            loginID = Login();
            if (loginID == -1)
            {
                Log("视频回放失败，没有正确登录DVR！");
                return false;
            }


            if (!m_bSearching)
            {
                NVS_FILE_QUERY query = new NVS_FILE_QUERY();

                NVS_FILE_TIME StartTime = new NVS_FILE_TIME();
                NVS_FILE_TIME StopTime = new NVS_FILE_TIME();

                //设置录像查找的开始时间 Set the starting time to search video files
                StartTime.iYear = (ushort)NextPageStartTime.Year;
                StartTime.iMonth = (ushort)NextPageStartTime.Month;
                StartTime.iDay = (ushort)NextPageStartTime.Day;
                StartTime.iHour = (ushort)NextPageStartTime.Hour;
                StartTime.iMinute = (ushort)NextPageStartTime.Minute;
                StartTime.iSecond = (ushort)NextPageStartTime.Second;

                //设置录像查找的结束时间 Set the stopping time to search video files
                StopTime.iYear = (ushort)NextPageEndTime.Year;
                StopTime.iMonth = (ushort)NextPageEndTime.Month;
                StopTime.iDay = (ushort)NextPageEndTime.Day;
                StopTime.iHour = (ushort)NextPageEndTime.Hour;
                StopTime.iMinute = (ushort)NextPageEndTime.Minute;
                StopTime.iSecond = (ushort)NextPageEndTime.Second;

                query.iChannel = e.DvrCh - 1;
                query.iFiletype = 1;
                query.iPageNo = iPageNo;
                query.iPageSize = 20;
                query.iType = 0xFF;
                query.iDevType = 0xFF;
                query.struStartTime = StartTime;
                query.struStopTime = StopTime;


                m_bSearching = true;
                int ret = NVSSDK.NetClient_NetFileQuery(loginID, ref query);
                if (ret == 0)
                {
                    Debug.WriteLine("NetClient_NetFileQuery successed!");
                    return true;
                }
                else
                {
                    Debug.WriteLine("NetClient_NetFileQuery failed!");
                    return false;
                }


            }
            return false;
        }
        public bool VIDEO_FindClose()
        {
            //FindDataList.Clear();
            if (OnFileSearch != null)
                OnFileSearch(this, strConst.NET_DVR_FILE_EXCEPTION, FindDataList);

            m_uConnID = uint.MaxValue;
            m_bSearching = false;

            return true;
        }
        public bool VIDEO_PlayBackByName(string sPlayBackFileName, DateTime dtStar)
        {

            VideoPlaybackArgs e = m_playbackArgs;
            if (e.Ip == "0.0.0.0")
            {
                Log("视频按文件回放失败，没有正确设置播放参数！");
                return false;
            }
            loginID = Login();
            if (loginID == -1)
            {
                Log("视频按文件回放失败，没有正确登录DVR！");
                return false;
            }
            int iRet = NVSSDK.NetClient_NetFileDownloadFile(ref connID, loginID, sPlayBackFileName, "", 0, 1, 1);

            if (iRet >= 0)
            {
                NVSSDK.NetClient_SetNetFileDownloadFileCallBack(connID, DownloadNotifyDelegate, new IntPtr(0));
                PlayingTimer.Start();
                return true;
            }
            else
            {
                if (connID != uint.MaxValue)
                {
                    NVSSDK.NetClient_NetFileStopDownloadFile(connID);
                    connID = uint.MaxValue;
                }
            }
            return false;

        }
        public bool VIDEO_PlayBackByTime(DateTime dtStar, DateTime dtEnd)
        {
            return true;
        }
        public bool VIDEO_PlayBackSaveData()
        {
            if (m_iPlayerID > -1)
            {
                string time = System.DateTime.Now.ToString("yyyyMMdd_HHmmss_fff");
                string path;
                if (!VideoPlaybackControl.RecPath.EndsWith("\\"))
                    path = VideoPlaybackControl.RecPath + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\";
                else
                    path = VideoPlaybackControl.RecPath + DateTime.Now.ToString("yyyy-MM-dd") + "\\";

                if (!GetDriver(path.Substring(0, 1)))
                {
                    MessageBox.Show("该磁盘不存在!");
                    return false;
                }
                if (Directory.Exists(path) == false)
                {
                    // MessageBox.Show("该目录不存在，自动创建‘" + textBox1.Text + "\\’" + "目录");
                    Directory.CreateDirectory(path);
                }
                string sFileName = String.Format("{0}{1}_{2}_{3}.mp4", path, m_playbackArgs.Ip, m_playbackArgs.DvrCh, time);


                //if (HCNetSDK.NET_DVR_PlayBackSaveData(m_lPlayHandle, sFileName))
                {
                    return true;
                }
            }
            return false;
        }
        public bool VIDEO_StopPlayBackSave()
        {
            if (m_iPlayerID > -1)
            {
                //if (HCNetSDK.NET_DVR_StopPlayBackSave(m_lPlayHandle))
                {
                    return true;
                }
            }
            return false;
        }
        public bool VIDEO_PlayBackCaptureFile()
        {
            string time = System.DateTime.Now.ToString("yyyyMMdd_HHmmss_fff");
            string path;
            if (!VideoPlaybackControl.PicPath.EndsWith("\\"))
                path = VideoPlaybackControl.PicPath + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\";
            else
                path = VideoPlaybackControl.PicPath + DateTime.Now.ToString("yyyy-MM-dd") + "\\";
            if (!GetDriver(path.Substring(0, 1)))
            {
                MessageBox.Show("该磁盘不存在!");
                return false;
            }
            if (Directory.Exists(path) == false)
            {
                // MessageBox.Show("该目录不存在，自动创建‘" + textBox1.Text + "\\’" + "目录");
                Directory.CreateDirectory(path);
            }
            string sPicFileName = String.Format("{0}{1}_{2}_{3}.bmp", path, m_playbackArgs.Ip, m_playbackArgs.DvrCh, time);

            if (m_iPlayerID > -1)
            {
                TandiPlaySDK.TC_CaptureBmpPic(m_iPlayerID, sPicFileName);
                return true;
            }
            //pubFun.ShowFileDirectory(sPicFileName);
            return false;
        }
        public bool VIDEO_GetFileByName(string sDVRFileName)
        {
            VideoPlaybackArgs e = m_playbackArgs;
            if (e.Ip == "0.0.0.0")
            {
                Log("视频按时间回放失败，没有正确设置播放参数！");
                return false;
            }
            loginID = Login();
            if (loginID == -1)
            {
                Log("视频按时间回放失败，没有正确登录DVR！");
                return false;
            }

            string time = System.DateTime.Now.ToString("yyyyMMdd_HHmmss_fff");
            string path;
            if (!VideoPlaybackControl.DownloadPath.EndsWith("\\"))
                path = VideoPlaybackControl.DownloadPath + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\";
            else
                path = VideoPlaybackControl.DownloadPath + DateTime.Now.ToString("yyyy-MM-dd") + "\\";
            if (!GetDriver(path.Substring(0, 1)))
            {
                MessageBox.Show("该磁盘不存在!");
                return false;
            }
            if (Directory.Exists(path) == false)
            {
                // MessageBox.Show("该目录不存在，自动创建‘" + textBox1.Text + "\\’" + "目录");
                Directory.CreateDirectory(path);
            }
            string sFileName = String.Format("{0}{1}_{2}_{3}.mp4", path, m_playbackArgs.Ip, m_playbackArgs.DvrCh, time);
            int ret = NVSSDK.NetClient_NetFileDownloadFile(ref m_uConnID, loginID, sDVRFileName, sFileName, 0, -1, 1);

            return (ret == 0) ? true : false;
        }
        public bool VIDEO_GetFileByTime(DateTime dtStar, DateTime dtEnd)

        {
            return false;
            // VideoPlaybackArgs e = m_playbackArgs;
            //if (e.Ip == "0.0.0.0")
            //{
            //    Log("视频按时间回放失败，没有正确设置播放参数！");
            //    return false;
            //}
            //int logID = Login();
            //if (logID == -1)
            //{
            //    Log("视频按时间回放失败，没有正确登录DVR！");
            //    return false;
            //}

            // HikDvrLoginInfo devInfo = GetLoginDvrInfo();

            ////设备模拟通道个数
            //int AChanNum = devInfo.DeviceInfo.byChanNum;
            ////模拟通道的起始通道号，目前设备模拟通道号从1开始
            //int AStartChan = devInfo.DeviceInfo.byStartChan;
            ////设备最大数字通道个数
            //int IPChanNum = devInfo.DeviceInfo.byIPChanNum;
            ////起始数字通道号，0表示无效
            //int IPStartChan = devInfo.DeviceInfo.byStartDChan;


            //HCNetSDK.NET_DVR_PLAYCOND struDownPara = new HCNetSDK.NET_DVR_PLAYCOND();

            //HCNetSDK.NET_DVR_TIME StartTime = new HCNetSDK.NET_DVR_TIME();
            //HCNetSDK.NET_DVR_TIME StopTime = new HCNetSDK.NET_DVR_TIME();

            ////设置录像查找的开始时间 Set the starting time to search video files
            //StartTime.dwYear = (uint)dtStar.Year;
            //StartTime.dwMonth = (uint)dtStar.Month;
            //StartTime.dwDay = (uint)dtStar.Day;
            //StartTime.dwHour = (uint)dtStar.Hour;
            //StartTime.dwMinute = (uint)dtStar.Minute;
            //StartTime.dwSecond = (uint)dtStar.Second;

            ////设置录像查找的结束时间 Set the stopping time to search video files
            //StopTime.dwYear = (uint)dtEnd.Year;
            //StopTime.dwMonth = (uint)dtEnd.Month;
            //StopTime.dwDay = (uint)dtEnd.Day;
            //StopTime.dwHour = (uint)dtEnd.Hour;
            //StopTime.dwMinute = (uint)dtEnd.Minute;
            //StopTime.dwSecond = (uint)dtEnd.Second;

            //byte[] sCardNumber = new byte[32];
            ////通道号 Channel number

            //if (AChanNum == 0 && IPChanNum > 0) //只有数字通道
            //{
            //    struDownPara.dwChannel = (uint)(e.DvrCh + IPStartChan - 1);//预览的设备通道 the device channel number
            //}
            //else
            //{
            //    //如果是混合录像机，数字通道要自行加上数字的起始通道，通常是加32;
            //    struDownPara.dwChannel =(uint)(e.DvrCh + AStartChan - 1);
            //}


            //struDownPara.struStartTime = StartTime;
            //struDownPara.struStopTime = StopTime;


            //string time = System.DateTime.Now.ToString("yyyyMMdd_HHmmss_fff");
            //string path;
            //if (!VideoPlaybackControl.DownloadPath.EndsWith("\\"))
            //    path = VideoPlaybackControl.DownloadPath + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\";
            //else
            //    path = VideoPlaybackControl.DownloadPath + DateTime.Now.ToString("yyyy-MM-dd") + "\\";
            //if (!GetDriver(path.Substring(0, 1)))
            //{
            //    MessageBox.Show("该磁盘不存在!");
            //    return false;
            //}
            //if (Directory.Exists(path) == false)
            //{
            //    // MessageBox.Show("该目录不存在，自动创建‘" + textBox1.Text + "\\’" + "目录");
            //    Directory.CreateDirectory(path);
            //}

            //string sFileName = String.Format("{0}{1}_{2}_{3}.mp4", path, m_playbackArgs.Ip, m_playbackArgs.DvrCh, time);

            //m_lDownloadHanle = HCNetSDK.NET_DVR_GetFileByTime_V40(logID, sFileName, ref struDownPara);
            //if (m_lDownloadHanle > -1)
            //{
            //    uint nil=0;
            //    HCNetSDK.NET_DVR_PlayBackControl(m_lDownloadHanle, strConst.NET_DVR_PLAYSTART, 0, ref nil);
            //}

            //return (m_lDownloadHanle == -1) ? false : true;
        }
        public bool VIDEO_StopGetFile()
        {
            //if (m_lDownloadHanle>-1)
            //{
            //    if (HCNetSDK.NET_DVR_StopGetFile(m_lDownloadHanle))
            //    {
            //        m_lDownloadHanle = -1;
            //        return true;
            //    }
            //}
            return false;
        }
        public int VIDEO_GetDownloadPos()
        {
            int pos = 0;
            //if (m_lDownloadHanle > -1)
            //    pos= HCNetSDK.NET_DVR_GetDownloadPos(m_lDownloadHanle);
            return pos;

        }
        public bool VIDEO_PlayBackControl(uint dwControlCode, uint dwInValue, out uint lpOutValue)
        {
            lpOutValue = 0;
            switch (dwControlCode)
            {

                case strConst.NET_DVR_PLAYFAST: //快进 dwInValue(9-13)
                    {
                        if (dwInValue >= 9 && dwInValue <= 13) //快快
                        {
                            Debug.WriteLine("NET_DVR_PLAYFAST:" + dwInValue);
                            int speed = (int)(dwInValue - 8);
                            if (speed > 4)
                                speed = 4;
                            TandiPlaySDK.TC_FastForward(m_iPlayerID, speed); //(1-4)
                        }
                        else
                        {
                            int speed = (int)(dwInValue - 6);
                            if (speed > 4)
                                speed = 4;
                            TandiPlaySDK.TC_SlowForward(m_iPlayerID, speed);
                        }
                    }
                    break;
                case strConst.NET_DVR_PLAYSLOW: // dwInValue(7-9)
                    {
                        Debug.WriteLine("NET_DVR_PLAYSLOW:" + dwInValue);
                        //if (dwInValue < 6)
                        //{
                        TandiPlaySDK.TC_FastBackward(m_iPlayerID);
                        //}
                        //else
                        //{
                        //    int speed = (int)(dwInValue - 6);
                        //    if (speed > 4)
                        //        speed = 4;
                        //    TandiPlaySDK.TC_SlowForward(m_iPlayerID, speed); //(1-4)
                        //}
                    }
                    break;
                case strConst.NET_DVR_PLAYNORMAL: //正常播放
                    TandiPlaySDK.TC_Play(m_iPlayerID); //(1-4)
                    break;
                case strConst.NET_DVR_PLAYPAUSE:  //暂停
                    TandiPlaySDK.TC_Pause(m_iPlayerID);
                    break;
                case strConst.NET_DVR_PLAYSTOP: //停止
                    TandiPlaySDK.TC_Stop(m_iPlayerID);
                    break;
                case strConst.NET_DVR_PLAYSTART: //开始播放
                    TandiPlaySDK.TC_Play(m_iPlayerID);
                    break;
                case strConst.NET_DVR_PLAYGETTIME: //获得播放时间
                    {
                        lpOutValue = (uint)TandiPlaySDK.TC_GetPlayTime(m_iPlayerID);

                    }
                    break;
                case strConst.NET_DVR_GETTOTALTIME: //获得播放时间
                    {

                    }
                    break;
                case strConst.NET_DVR_PLAYGETPOS:
                    {
                        int iTotalNum = TandiPlaySDK.TC_GetFrameCount(m_iPlayerID);
                        int iPlayNum = TandiPlaySDK.TC_GetPlayingFrameNum(m_iPlayerID);

                        lpOutValue = (uint)((iPlayNum * 100) / iTotalNum);
                    }
                    break;
                case strConst.NET_DVR_PLAYSETPOS:
                    {
                        int iTotalNum = TandiPlaySDK.TC_GetFrameCount(m_iPlayerID);
                        TandiPlaySDK.TC_SeekEx(m_iPlayerID, (int)((dwInValue / 100) * iTotalNum));
                    }
                    break;
                case strConst.NET_DVR_PLAYGETFRAME:
                    {
                        lpOutValue = (uint)TandiPlaySDK.TC_GetPlayingFrameNum(m_iPlayerID);
                    }
                    break;
                case strConst.NET_DVR_GETTOTALFRAMES:
                    {
                        lpOutValue = (uint)TandiPlaySDK.TC_GetFrameCount(m_iPlayerID);

                    }
                    break;
            }

            return true;
        }
        public bool VIDEO_StopPlayBack()
        {
            PlayingTimer.Stop();
            TandiPlaySDK.TC_Stop(m_iPlayerID);
            TandiPlaySDK.TC_DeletePlayer(m_iPlayerID);
            m_iPlayerID = -1;
            //if (m_lPlayHandle > -1)
            //    return HCNetSDK.NET_DVR_StopPlayBack(m_lPlayHandle);
            //else
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

        #endregion

        private void PlayingTimer_Tick(object sender, EventArgs e)
        {
            CheckStatus();

        }

        struct DvrLoginInfo
        {
            public int loginId;
        }
    }
}
