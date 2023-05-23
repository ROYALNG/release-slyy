using GHIBMS.Common;
using GHIBMS.SDK.IMOS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace GHIBMS.VideoPlayback
{

    public class IMOSPlayback : IVideoPlayback
    {
        enum MediaFileFormat
        {
            XP_MEDIA_FILE_TS,  //TS格式的媒体文件 
            XP_MEDIA_FILE_FLV  //FLV格式的媒体文件 
        }
        enum PictureFormat
        {
            XP_PICTURE_BMP,//  图片格式为bmp格式 
            XP_PICTURE_JPG  //图片格式为jpg格式 
        }
        #region 构造析造
        private bool disposed = false;
        private static IMOSSDK.XP_RUN_INFO_CALLBACK_EX_PF callBackMsg;
        public static IMOSSDK.CALL_BACK_PROC_EX_PF CallBackFuncEx;

        //private static bool bLogin = false;
        private List<RECORD_FILE_INFO_S> m_RecFileList = new List<RECORD_FILE_INFO_S>();
        public static Hashtable logidTable = new Hashtable();
        //private string ChannelCode;
        /// <summary>
        /// 回放信息格式
        /// </summary>
        private string m_strDateFormat = "yyyy/MM/dd HH:mm:ss";
        /// <summary>
        /// 开启实况回放用的通道号
        /// </summary>
        public string channelCode;
        private int playID = -1;
        private bool bRecording = false;
        ///
        /// 实现IDisposable中的Dispose方法
        ///
        public int PlayID
        {
            get { return playID; }
            // set { playID = value; }
        }

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
        ~IMOSPlayback()
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

        #region 内部变量与属性

        public static bool hasInit = false;

        private static UInt16 videoWndNubs = 1;

        private VideoPlaybackArgs m_playbackArgs = new VideoPlaybackArgs();
        private object synObject = new object();
        private string protocolCode = "JK_PLAT_IMOS";
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

        #endregion

        #region 事件
        //log事件
        public delegate void OnMessagedelegate(object sender, string msg);
        public event OnMessagedelegate OnMessage;
        private void SentMessage(string message)
        {
            if (OnMessage != null)
                OnMessage(this, message);
        }
        private void Log(string message)
        {
            if (OnMessage != null)
                OnMessage(this, message);
        }
        public void OnWinMessage(ref System.Windows.Forms.Message m)
        {
        }
        public void SetWinMessage(IntPtr lWinHandle)
        {
        }
        public delegate void OnFileSearchdelegate(object sender, int result, List<DvrFindData> data);
        public event OnFileSearchdelegate OnFileSearch;
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

            //设置XP回调
            callBackMsg = XpInfoRush;
            IntPtr ptrCallbakc = Marshal.GetFunctionPointerForDelegate(callBackMsg);
            uint ulResult = IMOSSDK.IMOS_SetRunMsgCB(ptrCallbakc);
            if (0 != ulResult)
            {
                //Log.Write.Error("IMOS_SetRunMsgCB fail errorCode:" + ulResult);
                //  MessageBox.Show("IMOS_SetRunMsgCB fail errorCode:" + ulResult);
            }
            if (!IMOSSDK.bInit)
            {
                UInt32 ulRet = 0;
                ulRet = IMOSSDK.IMOS_Initiate("N/A", m_playbackArgs.Port, 1, 1);
                if (0 != ulRet)
                {
                    Log("IMOS SDK初始化失败！错误代码：" + ulRet.ToString());

                    return false;
                }
                IMOSSDK.bInit = true;
            }

            return true;
        }
        public static bool DVR_Logout()
        {
            return true;
        }
        //清理资源
        public void DVR_Cleanup()
        {

            DVR_Logout();
        }


        #endregion

        #region 设备注册
        private bool VIDEO_Login()
        {
            VideoPlaybackArgs e = m_playbackArgs;
            string key = e.Ip + ":" + e.Port.ToString();
            if (logidTable.ContainsKey(key) && !(logidTable[key] as LoginInfo).LoginSucess)
            {
                logidTable.Remove(key);
            }
            if (!logidTable.ContainsKey(key))
            {
                LoginInfo info = new LoginInfo();
                info.ServerIP = e.Ip;
                info.ServerPort = e.Port;
                info.InitSucess = false;


                //1.初始化

                info.InitSucess = VIDEO_Init();
                if (info.InitSucess)
                {
                    //2.登录
                    info.LoginSucess = LoginMethod(out info.LoginInfo_S);
                    if (info.LoginSucess)
                        logidTable.Add(key, info);
                }
            }

            return true;
        }


        /// <summary>
        /// 注销方法
        /// </summary>
        /// <returns></returns>
        private void LogoutMethod()
        {
            LOGIN_INFO_S loginInfo;
            if (GetLoginSucceedState())
            {
                VideoPlaybackArgs e = m_playbackArgs;
                string key = e.Ip + ":" + e.Port.ToString();
                loginInfo = (logidTable[key] as LoginInfo).LoginInfo_S;
                //1.注销登录
                if (null != loginInfo.stUserLoginIDInfo.szUserLoginCode)
                {
                    IMOSSDK.IMOS_LogoutEx(ref loginInfo.stUserLoginIDInfo);
                    loginInfo.stUserLoginIDInfo.szUserLoginCode = null;
                    //IMOSSDK.IMOS_CleanUp()
                    (logidTable[key] as LoginInfo).LoginSucess = false;
                    logidTable.Remove(key);

                }
            }
            //ChannelCode = "";


        }
        private bool GetLoginInfo(out LOGIN_INFO_S loginInfo)
        {
            loginInfo = new LOGIN_INFO_S();
            VideoPlaybackArgs e = m_playbackArgs;
            string key = e.Ip + ":" + e.Port.ToString();
            if (logidTable.ContainsKey(key) && (logidTable[key] as LoginInfo).LoginSucess)
            {
                //存在正确的登录ID，返回
                LoginInfo info = (LoginInfo)logidTable[key];
                if (info.LoginSucess)
                {
                    loginInfo = info.LoginInfo_S;
                    return true;
                }
                else
                {
                    //登录ID不正确，移出HT,再次调用自我
                    logidTable.Remove(key);
                    VIDEO_Login();
                }
            }
            else //没有登录
            {
                VIDEO_Login();
            }
            //登录后再次查找logID
            if (logidTable.ContainsKey(key))
            {
                //存在正确的登录ID，返回
                LoginInfo info = (LoginInfo)logidTable[key];
                if (info.LoginSucess)
                {
                    loginInfo = info.LoginInfo_S;
                    return true;
                }
                else

                    return false;
            }
            return false;
        }
        private bool LoginMethod(out LOGIN_INFO_S loginInfo)
        {
            try
            {
                loginInfo = new LOGIN_INFO_S();
                //2.加密密码
                uint ulRet;
                IntPtr ptr_MD_Pwd = Marshal.AllocHGlobal(sizeof(char) * IMOSSDK.IMOS_PASSWD_ENCRYPT_LEN);
                ulRet = IMOSSDK.IMOS_Encrypt(m_playbackArgs.Password, (UInt32)m_playbackArgs.Password.Length, ptr_MD_Pwd);

                if (0 != ulRet)
                {
                    Log("加密密码失败!" + ulRet.ToString());
                    return false;
                }

                String MD_PWD = Marshal.PtrToStringAnsi(ptr_MD_Pwd);
                Marshal.FreeHGlobal(ptr_MD_Pwd);

                //3.登录方法
                IntPtr ptrLoginInfo = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(LOGIN_INFO_S)));
                ulRet = IMOSSDK.IMOS_LoginEx(m_playbackArgs.UserName, MD_PWD, m_playbackArgs.Ip, "N/A", ptrLoginInfo);
                if (0 != ulRet)
                {
                    Log("IMOS_Encrypt" + ulRet.ToString());
                    Debug.WriteLine("登录失败");
                    return false;
                }
                Debug.WriteLine("登录成功");

                loginInfo = (LOGIN_INFO_S)Marshal.PtrToStructure(ptrLoginInfo, typeof(LOGIN_INFO_S));
                Marshal.FreeHGlobal(ptrLoginInfo);

                int MaxPanel = ClientConfig.MaxPlaybackPannel;
                IntPtr ptrPlayWndInfo = Marshal.AllocHGlobal((MaxPanel + 2) * Marshal.SizeOf(typeof(PLAY_WND_INFO_S)));
                ulRet = IMOSSDK.IMOS_StartPlayer(ref loginInfo.stUserLoginIDInfo, (uint)MaxPanel + 2, ptrPlayWndInfo);
                Marshal.FreeHGlobal(ptrPlayWndInfo);
                if (0 != ulRet)
                {
                    Log("IMOS_StartPlayer" + ulRet.ToString());
                    return false;
                }

                //for (int i = 0; i < MaxPanel; i++)
                //{
                //    IntPtr ptrTemp = new IntPtr(ptrPlayWndInfo.ToInt32() + i * Marshal.SizeOf(typeof(PLAY_WND_INFO_S)));
                //    astPlayWndInfo[i] = (PLAY_WND_INFO_S)Marshal.PtrToStructure(ptrTemp, typeof(PLAY_WND_INFO_S));
                //}


                //4.保活
                IMOSSDK.IMOS_AutoKeepAlive(ref loginInfo.stUserLoginIDInfo);

                RegCallBackFunc(loginInfo.stUserLoginIDInfo);
                return true;
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
                loginInfo = new LOGIN_INFO_S();
                return false;
            }

        }
        private uint RegCallBackFunc(USER_LOGIN_ID_INFO_S stLoginInfo)
        {
            uint ulResult = 0;
            //先订阅推送
            ulResult = IMOSSDK.IMOS_SubscribePushInfo(ref stLoginInfo, (uint)SUBSCRIBE_PUSH_TYPE_E.SUBSCRIBE_PUSH_TYPE_ALL);
            if (0 != ulResult)
            {
                // MessageBox.Show("启动告警接受失败。");
                return ulResult;
            }

            CallBackFuncEx = ProcessCallBack;
            IntPtr ptrCB = Marshal.GetFunctionPointerForDelegate(CallBackFuncEx);
            //注册回调
            ulResult = IMOSSDK.IMOS_RegCallBackPrcFuncEx(ref stLoginInfo, ptrCB);
            if (0 != ulResult)
            {
                // MessageBox.Show("注册回调函数出错！");
                return ulResult;
            }
            return ulResult;
        }

        //回调处理函数
        private void ProcessCallBack(ref USER_LOGIN_ID_INFO_S pstUserLoginIDInfo, UInt32 ulProcType, IntPtr ptrParam)
        {
            Debug.WriteLine("ulProcType:" + ulProcType + "   ptrParam:" + ptrParam);
            try
            {
                switch (ulProcType)
                {
                    case (uint)CALL_BACK_PROC_TYPE_E.PROC_TYPE_LOGOUT:
                        {
                            Debug.WriteLine("断线回调");
                            VideoPlaybackArgs e = m_playbackArgs;
                            string key = e.Ip + ":" + e.Port.ToString();
                            if (logidTable.ContainsKey(key))
                            {
                                //存在正确的登录ID，返回
                                LoginInfo info = (LoginInfo)logidTable[key];
                                info.LoginSucess = false;
                                playID = -1;
                                logidTable.Remove(key);
                            }

                            //if (!backgroundWorkerRelogin.IsBusy)
                            //    backgroundWorkerRelogin.RunWorkerAsync();
                        }
                        break;

                }
            }
            catch (SystemException ex)
            {
                Debug.WriteLine(ex.ToString());
            }

        }
        /// <summary>
        /// XP回调处理函数
        /// </summary>
        /// <param name="stUserLoginIDInfo"></param>
        /// <param name="ulRunInfoType"></param>
        /// <param name="ptrInfo"></param>
        public void XpInfoRush(ref USER_LOGIN_ID_INFO_S stUserLoginIDInfo, uint ulRunInfoType, IntPtr ptrInfo)
        {
            if (ulRunInfoType == (uint)XP_RUN_INFO_TYPE_E.XP_RUN_INFO_DOWN_RTSP_PROTOCOL)
            {
                //下载rtsp消息
                //DownLoadComplete(ptrInfo);
            }
            else if (ulRunInfoType == (uint)XP_RUN_INFO_TYPE_E.XP_RUN_INFO_PASSIVE_MONITOR)
            {
                Log("被动实况停止操作信息");
            }
            else if (ulRunInfoType == (uint)XP_RUN_INFO_TYPE_E.XP_RUN_INFO_PASSIVE_START_MONITOR)
            {
                //下载rtsp消息
                Log("被动实况启动操作信息");
            }
        }
        private bool GetLoginSucceedState()
        {
            VideoPlaybackArgs e = m_playbackArgs;
            string key = e.Ip + ":" + e.Port.ToString();
            if (logidTable.ContainsKey(key))
            {
                return ((LoginInfo)logidTable[key]).LoginSucess;
            }
            return false;
        }
        private USER_LOGIN_ID_INFO_S GetUserLoginIDInf()
        {
            VideoPlaybackArgs e = m_playbackArgs;
            string key = e.Ip + ":" + e.Port.ToString();
            if (logidTable.ContainsKey(key) && (logidTable[key] as LoginInfo).LoginSucess)
            {
                return ((LoginInfo)logidTable[key]).LoginInfo_S.stUserLoginIDInfo;
            }
            return new USER_LOGIN_ID_INFO_S();

        }
        #endregion

        #region 查找文件

        public bool VIDEO_FindFile(uint dwFileType, byte isLock, DateTime dtStar, DateTime dtEnd)
        {

            VIDEO_Login();

            if (!GetLoginSucceedState())
                return false;

            m_RecFileList.Clear();

            string strStar = dtStar.ToString(m_strDateFormat); ;
            string strEnd = dtEnd.ToString(m_strDateFormat); ;

            m_RecFileList = queryRecord(strStar, strEnd, m_playbackArgs.CamID);

            List<DvrFindData> dataList = new List<DvrFindData>();
            findDataList.Clear();
            if (m_RecFileList.Count > 0)
            {

                foreach (RECORD_FILE_INFO_S fs in m_RecFileList)
                {
                    DvrFindData data = new DvrFindData();
                    data.FileName = Encoding.Default.GetString(fs.szFileName);
                    data.FileSize = fs.ulSize.ToString();
                    data.StartTime = DateTime.Parse(Encoding.Default.GetString(fs.szStartTime));
                    data.StopTime = DateTime.Parse(Encoding.Default.GetString(fs.szEndTime));
                    dataList.Add(data);
                    findDataList.Add(data);
                }
                if (OnFileSearch != null)
                    OnFileSearch(this, strConst.NET_DVR_NOMOREFILE, dataList);

            }
            else
                if (OnFileSearch != null)
                OnFileSearch(this, strConst.NET_DVR_FILE_NOFIND, dataList);




            return false;
        }
        /// <summary>
        /// 播放录像方法
        /// </summary>
        /// <param name="RecFileList">录像列表</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="m_cameraCode">摄像机编码</param>
        private bool PlayRecord(string szFileName, byte[] beginTime, byte[] endTime, String m_cameraCode)
        {
            string b = beginTime.ToString();

            GET_URL_INFO_S GetURLInfo = new GET_URL_INFO_S();

            GetURLInfo.szFileName = Encoding.Default.GetBytes(szFileName);
            GetURLInfo.szCamCode = new byte[48];
            Encoding.Default.GetBytes(m_cameraCode, 0, Encoding.Default.GetByteCount(m_cameraCode), GetURLInfo.szCamCode, 0);

            USER_LOGIN_ID_INFO_S stLoginInfo = GetUserLoginIDInf();
            if (stLoginInfo.szUserCode == null) return false;
            GetURLInfo.szClientIp = stLoginInfo.szUserIpAddress;

            GetURLInfo.stRecTimeSlice.szBeginTime = new byte[IMOSSDK.IMOS_TIME_LEN];
            GetURLInfo.stRecTimeSlice.szEndTime = new byte[IMOSSDK.IMOS_TIME_LEN];

            Byte[] timeBytes = beginTime;
            timeBytes.CopyTo(GetURLInfo.stRecTimeSlice.szBeginTime, 0);
            timeBytes = endTime;
            timeBytes.CopyTo(GetURLInfo.stRecTimeSlice.szEndTime, 0);



            //Encoding.Default.GetBytes(dtpTimeTag.Value.ToString(m_strDateFormat), 0, 19, GetURLInfo.stRecTimeSlice.szBeginTime, 0);
            //GetURLInfo.stRecTimeSlice.szEndTime = Encoding.Default.GetBytes(timeEnd);

            IntPtr ptrSDKURLInfo = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(URL_INFO_S)));
            URL_INFO_S urlInfo = new URL_INFO_S();
            UInt32 ulRet = IMOSSDK.IMOS_GetRecordFileURL(ref stLoginInfo, ref GetURLInfo, ref urlInfo);
            if (0 != ulRet)
            {
                return false;
            }

            ulRet = StartVod(urlInfo);
            if (0 != ulRet)
            {
                return false;
            }
            return true;

        }
        public UInt32 StartVod(URL_INFO_S stURLInfo)
        {
            USER_LOGIN_ID_INFO_S stLoginInfo = GetUserLoginIDInf();
            if (stLoginInfo.szUserCode == null) return 1;
            UInt32 ulRet;
            IntPtr pchannelCode = new IntPtr();
            pchannelCode = Marshal.AllocHGlobal(25 * Marshal.SizeOf(typeof(PLAY_WND_INFO_S)));
            IMOSSDK.IMOS_GetChannelCode(ref stLoginInfo, pchannelCode);
            channelCode = Marshal.PtrToStringAnsi(pchannelCode);
            Marshal.FreeHGlobal(pchannelCode);

            ulRet = IMOSSDK.IMOS_OpenVodStream(ref stLoginInfo, Encoding.Default.GetBytes(channelCode), stURLInfo.szURL, stURLInfo.stVodSeverIP.szServerIp, stURLInfo.stVodSeverIP.usServerPort, 1);
            if (0 != ulRet)
            {
                return ulRet;
            }

            ulRet = IMOSSDK.IMOS_SetDecoderTag(ref stLoginInfo, Encoding.Default.GetBytes(channelCode), stURLInfo.szDecoderTag);

            ulRet = IMOSSDK.IMOS_SetPlayWnd(ref stLoginInfo, Encoding.Default.GetBytes(channelCode), m_playbackArgs.PlayWnd);
            ulRet = IMOSSDK.IMOS_StartPlay(ref stLoginInfo, Encoding.Default.GetBytes(channelCode));
            if (0 == ulRet)
            {


            }
            return ulRet;



        }

        private void ThreadSearchFile()
        {

        }
        public bool VIDEO_FindClose()
        {
            return true;
        }
        private string curPlayBackFileName = "";
        private DateTime curStartTime;
        public bool VIDEO_PlayBackByName(string sPlayBackFileName, DateTime dtStar)
        {

            curPlayBackFileName = sPlayBackFileName;
            curStartTime = dtStar;
            foreach (DvrFindData data in this.findDataList)
            {
                if (data.FileName == sPlayBackFileName)
                {
                    PlayRecord(sPlayBackFileName,
                             Encoding.UTF8.GetBytes(dtStar.ToString(m_strDateFormat)),
                             Encoding.UTF8.GetBytes(data.StopTime.ToString(m_strDateFormat)),
                             m_playbackArgs.CamID);

                }
            }
            playID = 1;
            return true;
        }
        public bool VIDEO_PlayBackByTime(DateTime dtStar, DateTime dtEnd)
        {

            return true;
        }

        public bool VIDEO_GetFileByTime(DateTime dtStar, DateTime dtEnd)
        {

            return true;
        }

        public bool VIDEO_StopGetFile()
        {

            return true;
        }
        private int downPos = 0;
        public int VIDEO_GetDownloadPos()
        {

            return downPos;

        }
        public bool VIDEO_PlayBackControl(uint dwControlCode, uint dwInValue, out uint lpOutValue)
        {
            lpOutValue = 0;
            USER_LOGIN_ID_INFO_S stLoginInfo = GetUserLoginIDInf();
            if (stLoginInfo.szUserCode == null) return false;

            switch (dwControlCode)
            {
                case strConst.NET_DVR_PLAYFAST:
                    IMOSSDK.IMOS_SetPlaySpeed(ref stLoginInfo, Encoding.Default.GetBytes(channelCode), dwInValue);
                    break;
                case strConst.NET_DVR_PLAYSLOW:
                    IMOSSDK.IMOS_SetPlaySpeed(ref stLoginInfo, Encoding.Default.GetBytes(channelCode), dwInValue);
                    break;
                case strConst.NET_DVR_PLAYNORMAL:
                    //IMOSSDK.IMOS_SetPlaySpeed(ref stLoginInfo, Encoding.Default.GetBytes(channelCode), dwInValue);
                    break;
                case strConst.NET_DVR_PLAYPAUSE:
                    //VIDEO_StopPlayBack();
                    break;
                case strConst.NET_DVR_PLAYSTOP:
                    VIDEO_StopPlayBack();
                    break;
                case strConst.NET_DVR_PLAYSTART:
                    {
                    }
                    break;
                case strConst.NET_DVR_PLAYGETTIME:
                    {


                    }
                    break;
            }
            return true;

        }

        public bool VIDEO_StopPlayBack()
        {
            playID = -1;
            USER_LOGIN_ID_INFO_S stLoginInfo = GetUserLoginIDInf();
            if (stLoginInfo.szUserCode == null) return true;
            uint ulRet;
            if (null != channelCode)
            {
                ulRet = IMOSSDK.IMOS_StopPlay(ref stLoginInfo, Encoding.Default.GetBytes(channelCode));
                ulRet = IMOSSDK.IMOS_FreeChannelCode(ref stLoginInfo, Encoding.Default.GetBytes(channelCode));
            }
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
        public List<RECORD_FILE_INFO_S> queryRecord(string strBegin, string strEnd, String m_cameraCode)
        {

            List<RECORD_FILE_INFO_S> RecFileList = new List<RECORD_FILE_INFO_S>();
            USER_LOGIN_ID_INFO_S stLoginInfo = GetUserLoginIDInf();
            if (stLoginInfo.szUserCode == null) return RecFileList;

            try
            {
                UInt32 ulRet = 0;
                UInt32 ulBeginNum = 0;
                UInt32 ulTotalNum = 0;
                QUERY_PAGE_INFO_S stPageInfo = new QUERY_PAGE_INFO_S();
                RSP_PAGE_INFO_S stRspPageInfo;


                REC_QUERY_INFO_S stRecQueryInfo = new REC_QUERY_INFO_S();
                stRecQueryInfo.szReserve = new byte[32];
                //stRecQueryInfo.stQueryTimeSlice.szBeginTime = new byte[IMOSSDK.IMOS_TIME_LEN];
                stRecQueryInfo.szCamCode = new byte[48];
                Encoding.Default.GetBytes(m_cameraCode, 0, Encoding.Default.GetByteCount(m_cameraCode), stRecQueryInfo.szCamCode, 0);

                stRecQueryInfo.stQueryTimeSlice.szBeginTime = new byte[IMOSSDK.IMOS_TIME_LEN];
                Encoding.Default.GetBytes(strBegin, 0, Encoding.Default.GetByteCount(strBegin), stRecQueryInfo.stQueryTimeSlice.szBeginTime, 0);
                stRecQueryInfo.stQueryTimeSlice.szEndTime = new byte[IMOSSDK.IMOS_TIME_LEN];
                Encoding.Default.GetBytes(strEnd, 0, Encoding.Default.GetByteCount(strEnd), stRecQueryInfo.stQueryTimeSlice.szEndTime, 0);

                //stRecQueryInfo.stQueryTimeSlice.szBeginTime =  Encoding.Default.GetBytes(strBegin);
                //stRecQueryInfo.stQueryTimeSlice.szEndTime = Encoding.Default.GetBytes(strEnd);

                IntPtr ptrRecFileList = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(RECORD_FILE_INFO_S)) * 30);
                IntPtr ptrRspPage = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(RSP_PAGE_INFO_S)));
                do
                {
                    stPageInfo.ulPageFirstRowNumber = ulBeginNum;
                    stPageInfo.ulPageRowNum = 30;
                    ulRet = IMOSSDK.IMOS_RecordRetrieval(ref stLoginInfo, ref stRecQueryInfo, ref stPageInfo, ptrRspPage, ptrRecFileList);
                    if (0 != ulRet)
                    {
                        return RecFileList;
                    }

                    stRspPageInfo = (RSP_PAGE_INFO_S)Marshal.PtrToStructure(ptrRspPage, typeof(RSP_PAGE_INFO_S));
                    ulTotalNum = stRspPageInfo.ulTotalRowNum;

                    RECORD_FILE_INFO_S stRecFileItem;

                    for (int i = 0; i < stRspPageInfo.ulRowNum; ++i)
                    {
                        IntPtr ptrTemp = new IntPtr(ptrRecFileList.ToInt32() + Marshal.SizeOf(typeof(RECORD_FILE_INFO_S)) * i);
                        stRecFileItem = (RECORD_FILE_INFO_S)Marshal.PtrToStructure(ptrTemp, typeof(RECORD_FILE_INFO_S));
                        RecFileList.Add(stRecFileItem);
                    }
                    ulBeginNum += stRspPageInfo.ulRowNum;

                } while (ulTotalNum > ulBeginNum);

                return RecFileList;
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return RecFileList;
            }
        }

        #endregion

        #region IVideoPlayback 成员


        public bool VIDEO_PlayBackSaveData()
        {
            if (PlayID == -1)
            {
                Log("视频录像失败，视频没有正常播放！");
                return false;
            }
            VideoPlaybackArgs e = m_playbackArgs;
            string key = e.Ip + ":" + e.Port.ToString();
            if (logidTable.ContainsKey(key) && (logidTable[key] as LoginInfo).LoginSucess)
            {
                //存在正确的登录ID，返回
                LoginInfo info = (LoginInfo)logidTable[key];
                if (info.LoginSucess)
                {
                    LOGIN_INFO_S loginInfo = info.LoginInfo_S;

                    string time = System.DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    string path;
                    if (!VideoPlaybackControl.RecPath.EndsWith("\\"))
                        path = VideoPlaybackControl.RecPath + "\\" + pubFun.DateStr + "\\";
                    else
                        path = VideoPlaybackControl.RecPath + pubFun.DateStr + "\\";
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

                    string sPicFileName = String.Format("{0}{1}{2}_{3}_{4}", path, "", e.Ip, e.DvrCh, time);

                    uint ret = IMOSSDK.IMOS_StartRecordEx(ref loginInfo.stUserLoginIDInfo, Encoding.Default.GetBytes(channelCode), Encoding.Default.GetBytes(sPicFileName), (UInt32)MediaFileFormat.XP_MEDIA_FILE_TS, IntPtr.Zero);


                    if (ret != 0)
                    {
                        string msg = String.Format("视频录像失败！IP：{0} 摄像机:{1}", e.Ip, e.CamName);
                        Log(msg);
                        bRecording = false;
                        return false;
                    }
                    else
                    {
                        bRecording = true;
                        string msg = String.Format("视频录像开始！IP：{0} 摄像机:{1}", e.Ip, e.CamName);
                        Log(msg);
                        //pubFun.ShowFileDirectory(sPicFileName + ".ts");
                        return true;
                    }

                }
            }

            return true;


        }

        public bool VIDEO_StopPlayBackSave()
        {
            if (PlayID > -1 && bRecording)
            {
                VideoPlaybackArgs e = m_playbackArgs;
                string key = e.Ip + ":" + e.Port.ToString();
                if (logidTable.ContainsKey(key) && (logidTable[key] as LoginInfo).LoginSucess)
                {
                    //存在正确的登录ID，返回
                    LoginInfo info = (LoginInfo)logidTable[key];
                    if (info.LoginSucess)
                    {
                        LOGIN_INFO_S loginInfo = info.LoginInfo_S;
                        uint ret = IMOSSDK.IMOS_StopRecord(ref loginInfo.stUserLoginIDInfo, Encoding.Default.GetBytes(channelCode));
                        if (ret != 0)
                        {
                            string msg = String.Format("视频录像停止失败！IP：{0} 摄像机:{1}", e.Ip, e.CamName);
                            Log(msg);
                            bRecording = false;
                            return false;
                        }
                        else
                        {
                            string msg = String.Format("视频录像停止成功！IP：{0} 摄像机:{1}", e.Ip, e.CamName);
                            Log(msg);
                            bRecording = false;
                            return true;

                        }


                    }
                }
            }
            return true;

        }

        public bool VIDEO_PlayBackCaptureFile()
        {

            VideoPlaybackArgs e = m_playbackArgs;
            string key = e.Ip + ":" + e.Port.ToString();
            if (logidTable.ContainsKey(key) && (logidTable[key] as LoginInfo).LoginSucess)
            {
                //存在正确的登录ID，返回
                LoginInfo info = (LoginInfo)logidTable[key];
                if (info.LoginSucess)
                {
                    LOGIN_INFO_S loginInfo = info.LoginInfo_S;

                    string time = System.DateTime.Now.ToString("yyyyMMdd_HHmmss_fff");
                    string path;
                    if (!VideoPlaybackControl.PicPath.EndsWith("\\"))
                        path = VideoPlaybackControl.PicPath + "\\" + pubFun.DateStr + "\\";
                    else
                        path = VideoPlaybackControl.PicPath + pubFun.DateStr + "\\";
                    if (!GetDriver(path.Substring(0, 1)))
                    {
                        MessageBox.Show("该磁盘不存在!");
                        return false;
                    }
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

                    string sPicFileName = String.Format("{0}{1}_{2}_{3}_{4}", path, "", m_playbackArgs.Ip, m_playbackArgs.DvrCh, time);


                    uint ret = IMOSSDK.IMOS_SnatchOnceEx(ref loginInfo.stUserLoginIDInfo, Encoding.Default.GetBytes(channelCode), Encoding.Default.GetBytes(sPicFileName), (uint)PictureFormat.XP_PICTURE_BMP);

                    if (ret != 0)
                    {
                        string msg = String.Format("图片抓拍失败！IP：{0} 通道号:{1}", m_playbackArgs.Ip, m_playbackArgs.DvrCh);
                        Log(msg);
                        return false;
                    }
                    //pubFun.ShowFileDirectory(sPicFileName + ".bmp");

                    return true;
                }
            }
            return false;
        }

        public bool VIDEO_GetFileByName(string sDVRFileName)
        {

            return true;

        }
        /// <summary>
        /// 回放下载
        /// </summary>
        /// <param name="recFile">录像信息</param>
        /// <param name="camCode">摄像机编码</param>
        /// <param name="downloadLoc">用户下载存储位置</param>
        /// <returns></returns>
        public byte[] startDownLoad(RECORD_FILE_INFO_S fileInfo, byte[] camCode, XP_PROTOCOL_E vodProtocol, String downloadLoc, XP_DOWN_MEDIA_SPEED_E speed, uint downloadFormat, DateTime beginTime, DateTime endTime)
        {
            UInt32 ulRet = 0;
            IntPtr ptrSDKURLInfo = IntPtr.Zero;
            //IntPtr pcChannelCode = IntPtr.Zero;

            try
            {
                USER_LOGIN_ID_INFO_S stLoginInfo = GetUserLoginIDInf();
                if (stLoginInfo.szUserCode == null) return new byte[] { 0 };

                GET_URL_INFO_S getUrlInfo = new GET_URL_INFO_S();
                TIME_SLICE_S timeSlice = new TIME_SLICE_S();
                URL_INFO_S urlInfo = new URL_INFO_S();

                byte[] begin = new byte[IMOSSDK.IMOS_TIME_LEN];
                String strBeginTime = beginTime.ToString("yyyy/MM/dd HH:mm:ss");
                Encoding.UTF8.GetBytes(strBeginTime).CopyTo(begin, 0);
                byte[] end = new byte[IMOSSDK.IMOS_TIME_LEN];
                String strEndTime = endTime.ToString("yyyy/MM/dd HH:mm:ss");
                Encoding.UTF8.GetBytes(strEndTime).CopyTo(end, 0);

                timeSlice.szBeginTime = begin;
                timeSlice.szEndTime = end;

                getUrlInfo.szCamCode = camCode;
                getUrlInfo.szFileName = fileInfo.szFileName;
                getUrlInfo.stRecTimeSlice = timeSlice;
                getUrlInfo.szClientIp = stLoginInfo.szUserIpAddress;

                //ptrSDKURLInfo = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(URL_INFO_S)));
                //这个下载通道号，是录像下载的唯一标志，以后查询录像都要用到这个通道号
                byte[] byPcChannel = new byte[IMOSSDK.IMOS_RES_CODE_LEN];

                ulRet = IMOSSDK.IMOS_GetRecordFileURL(ref stLoginInfo, ref getUrlInfo, ref urlInfo);

                //URL_INFO_S URLInfo = (URL_INFO_S)Marshal.PtrToStructure(ptrSDKURLInfo, typeof(URL_INFO_S));

                byte[] pcFileName = Encoding.UTF8.GetBytes(downloadLoc);
                ulRet = IMOSSDK.IMOS_OpenDownload(ref stLoginInfo,
                    urlInfo.szURL, urlInfo.stVodSeverIP.szServerIp, urlInfo.stVodSeverIP.usServerPort,
                    (uint)vodProtocol, (uint)speed,
                    pcFileName, downloadFormat, byPcChannel);

                ulRet = IMOSSDK.IMOS_SetDecoderTag(ref stLoginInfo, byPcChannel, urlInfo.szDecoderTag);

                ulRet = IMOSSDK.IMOS_StartDownload(ref stLoginInfo, byPcChannel);

                return byPcChannel;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                Marshal.FreeHGlobal(ptrSDKURLInfo);
            }
        }

        #endregion
    }
    class LoginInfo
    {
        public string ServerIP;
        public int ServerPort;
        public LOGIN_INFO_S LoginInfo_S;
        public bool InitSucess;
        public bool LoginSucess;
    }
}
