using GHIBMS.Common;
using GHIBMS.SDK.IMOS;
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace GHIBMS.NetVideo
{
    public class IMOSRealPlayer : IVideoRealPlayer, IDisposable
    {

        #region 内部变量与属性
        private static UInt16 videoWndNubs = 1;
        private static IMOSSDK.XP_RUN_INFO_CALLBACK_EX_PF callBackMsg;
        public static IMOSSDK.CALL_BACK_PROC_EX_PF CallBackFuncEx;

        public static Hashtable logidTable = new Hashtable();

        private VideoRealPlayArgs playArgs = new VideoRealPlayArgs();
        private int playID = -1;
        private BackgroundWorker backgroundWorkerPlay = new BackgroundWorker();
        private BackgroundWorker backgroundWorkerStop = new BackgroundWorker();
        private BackgroundWorker backgroundWorkerRelogin = new BackgroundWorker();
        private bool bRecording = false;
        //登录成功返回信息

        private string ChannelCode;

        private string protocolCode = "JK_PLAT_IMOS";
        public string ProtocolCode
        {
            get { return protocolCode; }

        }

        public UInt16 VideoWndNubs
        {
            set { videoWndNubs = value; }
            get { return videoWndNubs; }
        }
        public int PlayID
        {
            get { return playID; }
            // set { playID = value; }
        }
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
        }
        public void SetWinMessage(IntPtr lWinHandle)
        {
        }
        #endregion

        #region 构造与晰构
        //private static IntPtr lptr;
        public IMOSRealPlayer()
        {
            // voiceDataCallBack = new HCNetSDK.VoiceDataCallBack(fVoiceDataCallBack);

            backgroundWorkerPlay.WorkerReportsProgress = true;
            backgroundWorkerPlay.WorkerSupportsCancellation = true;
            backgroundWorkerPlay.DoWork += new DoWorkEventHandler(backgroundWorkerPlay_DoWork);

            backgroundWorkerStop.WorkerReportsProgress = true;
            backgroundWorkerStop.WorkerSupportsCancellation = true;
            backgroundWorkerStop.DoWork += new DoWorkEventHandler(backgroundWorkerStop_DoWork);

            backgroundWorkerRelogin.WorkerReportsProgress = true;
            backgroundWorkerRelogin.WorkerSupportsCancellation = true;
            backgroundWorkerRelogin.DoWork += new DoWorkEventHandler(backgroundWorkerRelogin_DoWork);

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
        ~IMOSRealPlayer()
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
            //设置XP回调
            callBackMsg = XpInfoRush;
            IntPtr ptrCallbakc = Marshal.GetFunctionPointerForDelegate(callBackMsg);
            uint ulResult = IMOSSDK.IMOS_SetRunMsgCB(ptrCallbakc);
            if (0 != ulResult)
            {
                //Log.Write.Error("IMOS_SetRunMsgCB fail errorCode:" + ulResult);
                MessageBox.Show("IMOS_SetRunMsgCB fail errorCode:" + ulResult);
            }
            if (!IMOSSDK.bInit)
            {
                UInt32 ulRet = 0;
                ulRet = IMOSSDK.IMOS_Initiate("N/A", playArgs.Port, 1, 1);
                if (0 != ulRet)
                {
                    Log("IMOS SDK初始化失败！错误代码：" + ulRet.ToString());

                    return false;
                }
                IMOSSDK.bInit = true;
            }


            return true;
        }
        private bool VIDEO_Login()
        {

            VideoRealPlayArgs e = playArgs;
            if (e.Ip == "") return false;
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
                VideoRealPlayArgs e = playArgs;
                if (e.Ip == "") return;
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
            ChannelCode = "";


        }
        private bool GetLoginInfo(out LOGIN_INFO_S loginInfo)
        {
            loginInfo = new LOGIN_INFO_S();
            VideoRealPlayArgs e = playArgs;
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
                ulRet = IMOSSDK.IMOS_Encrypt(playArgs.Password, (UInt32)playArgs.Password.Length, ptr_MD_Pwd);
                String MD_PWD = Marshal.PtrToStringAnsi(ptr_MD_Pwd);
                Marshal.FreeHGlobal(ptr_MD_Pwd);
                if (0 != ulRet)
                {
                    Log("加密密码失败!" + ulRet.ToString());
                    return false;
                }


                //3.登录方法
                IntPtr ptrLoginInfo = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(LOGIN_INFO_S)));
                ulRet = IMOSSDK.IMOS_LoginEx(playArgs.UserName, MD_PWD, playArgs.Ip, "N/A", ptrLoginInfo);
                loginInfo = (LOGIN_INFO_S)Marshal.PtrToStructure(ptrLoginInfo, typeof(LOGIN_INFO_S));
                Marshal.FreeHGlobal(ptrLoginInfo);

                if (0 != ulRet)
                {
                    Log("IMOS_Encrypt" + ulRet.ToString());
                    Debug.WriteLine("登录失败");
                    return false;
                }
                Debug.WriteLine("登录成功");



                int MaxPanel = ClientConfig.MaxRealVideoPannel;
                int cXp = MaxPanel + 2;
                IntPtr ptrPlayWndInfo = Marshal.AllocHGlobal(cXp * Marshal.SizeOf(typeof(PLAY_WND_INFO_S)));
                ulRet = IMOSSDK.IMOS_StartPlayer(ref loginInfo.stUserLoginIDInfo, (uint)cXp, ptrPlayWndInfo);
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
                // IMOSSDK.IMOS_AutoKeepAlive(ref loginInfo.stUserLoginIDInfo);

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
                            VideoRealPlayArgs e = playArgs;
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
            catch
            {
                //MessageBox.Show(ex.Message);
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


        #endregion

        #region 设备注册

        public void DVR_Cleanup()
        {
            // DVR_Logout();
        }

        void backgroundWorkerPlay_DoWork(object sender, DoWorkEventArgs e)
        {

            DoWorkerType arg = (DoWorkerType)(e.Argument);
            switch (arg)
            {
                case DoWorkerType.Play:
                    StartMonitor();
                    break;

                case DoWorkerType.Stop:
                    StopRealPlay();
                    break;

            }

        }
        void backgroundWorkerStop_DoWork(object sender, DoWorkEventArgs e)
        {

            StopRealPlay();

        }
        //断线重连
        void backgroundWorkerRelogin_DoWork(object sender, DoWorkEventArgs e)
        {
            //Debug.WriteLine("断线重连");
            //bool b = GetLoginSucceedState();
            //while (!b)
            //{
            //    LogoutMethod();
            //    StartMonitor();
            //    b = GetLoginSucceedState();
            //    Thread.Sleep(5000);
            //}
        }

        #endregion

        #region 视频预览
        public bool VIDEO_StartRealPlay()
        {
            //if (!backgroundWorkerPlay.IsBusy && !backgroundWorkerRelogin.IsBusy)
            //{
            //    backgroundWorkerPlay.RunWorkerAsync(DoWorkerType.Play);
            //}
            StartMonitor();
            return true;

        }
        private void StartMonitor()
        {
            if (PlayID > -1)
            {
                StopRealPlay();
            }

            VideoRealPlayArgs e = playArgs;
            if (e.Ip == "" || e.Ip == "0.0.0.0")
            {
                Log("实时视频播放失败，没有正确设置播放参数！");
            }
            LOGIN_INFO_S stLoginInfo;
            bool bSucceed = GetLoginInfo(out stLoginInfo);
            if (!bSucceed)
            {
                Log("实时视频播放失败，登录失败！");
                return;
            }

            UInt32 ulRet = 0;
            int i = e.PlayWndNo;

            //if (string.IsNullOrEmpty(ChannelCode))
            //{
            IntPtr ptrChannelCode = new IntPtr();
            ptrChannelCode = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(PLAY_WND_INFO_S)));
            IMOSSDK.IMOS_GetChannelCode(ref stLoginInfo.stUserLoginIDInfo, ptrChannelCode);
            //将通道号和选中窗格绑定
            ChannelCode = Marshal.PtrToStringAnsi(ptrChannelCode);
            Marshal.FreeHGlobal(ptrChannelCode);

            //}


            ulRet = IMOSSDK.IMOS_SetPlayWnd(ref stLoginInfo.stUserLoginIDInfo, Encoding.Default.GetBytes(ChannelCode), e.PlayWnd);
            if (0 != ulRet)
            {
                Log("IMOS_SetPlayerWnd" + ulRet.ToString());
            }

            uint iCodeStream;
            if (e.EncodeMode == EncodeTypeEnum.主码流)//主码流
            {
                iCodeStream = (uint)IMOS_FAVORITE_STREAM_E.IMOS_FAVORITE_STREAM_PRIMARY;

            }
            else
                iCodeStream = (uint)IMOS_FAVORITE_STREAM_E.IMOS_FAVORITE_STREAM_SECONDERY;




            ulRet = IMOSSDK.IMOS_StartMonitor(ref stLoginInfo.stUserLoginIDInfo, Encoding.Default.GetBytes(e.CamID), Encoding.Default.GetBytes(ChannelCode), iCodeStream, 0);
            if (0 != ulRet)
            {
                playID = -1;
                Debug.WriteLine("视频播放失败！");
            }
            else
            {
                playID = 1;
                Debug.WriteLine("视频播放成功！");
            }


            // Console.WriteLine("316L" + playID);
        }
        public bool VIDEO_StopRealPlay()
        {
            if (PlayID == -1 || playArgs.Ip == "" || playArgs.Ip == "0.0.0.0")
                return true;
            //if (!backgroundWorkerStop.IsBusy && !backgroundWorkerRelogin.IsBusy)
            //{
            //    backgroundWorkerStop.RunWorkerAsync();
            //}
            StopRealPlay();
            playID = -1;
            return true;

        }
        private bool StopRealPlay()
        {
            if (playID != -1)
            {
                VideoRealPlayArgs e = playArgs;
                string key = e.Ip + ":" + e.Port.ToString();
                if (logidTable.ContainsKey(key))
                {
                    LoginInfo info = (LoginInfo)logidTable[key];
                    if (info.LoginSucess)
                    {
                        //存在正确的登录ID，返回
                        LOGIN_INFO_S id = info.LoginInfo_S;
                        UInt32 ulRet = 0;
                        ulRet = IMOSSDK.IMOS_StopMonitor(ref id.stUserLoginIDInfo, Encoding.Default.GetBytes(ChannelCode), 0);
                        ulRet = IMOSSDK.IMOS_FreeChannelCode(ref id.stUserLoginIDInfo, Encoding.Default.GetBytes(ChannelCode));
                        ChannelCode = "";

                    }
                }
            }

            playID = -1;

            return true;
        }

        private bool GetLoginSucceedState()
        {
            VideoRealPlayArgs e = playArgs;
            string key = e.Ip + ":" + e.Port.ToString();
            if (logidTable.ContainsKey(key))
            {
                return ((LoginInfo)logidTable[key]).LoginSucess;
            }
            return false;
        }



        #endregion

        #region 抓拍与录像
        public bool VIDEO_SaveRealData(string fileName)
        {

            if (PlayID == -1)
            {
                Log("视频录像失败，视频没有正常播放！");
                return false;
            }
            VideoRealPlayArgs e = playArgs;
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

                    string sPicFileName = String.Format("{0}{1}_{2}_{3}_{4}", path, fileName, playArgs.Ip, playArgs.DvrCh, time);

                    uint ret = IMOSSDK.IMOS_StartRecordEx(ref loginInfo.stUserLoginIDInfo, Encoding.Default.GetBytes(ChannelCode), Encoding.Default.GetBytes(sPicFileName), (UInt32)MediaFileFormat.XP_MEDIA_FILE_TS, IntPtr.Zero);


                    if (ret != 0)
                    {
                        string msg = String.Format("视频录像失败！IP：{0} 摄像机:{1}", playArgs.Ip, playArgs.CamName);
                        Log(msg);
                        bRecording = false;
                        return false;
                    }
                    else
                    {
                        bRecording = true;
                        string msg = String.Format("视频录像开始！IP：{0} 摄像机:{1}", playArgs.Ip, playArgs.CamName);
                        Log(msg);
                        //pubFun.ShowFileDirectory(sPicFileName + ".ts");
                        return true;
                    }

                }
            }

            return true;

        }
        public bool VIDEO_StopSaveRealData()
        {
            if (PlayID > -1 && bRecording)
            {
                VideoRealPlayArgs e = playArgs;
                string key = e.Ip + ":" + e.Port.ToString();
                if (logidTable.ContainsKey(key) && (logidTable[key] as LoginInfo).LoginSucess)
                {
                    //存在正确的登录ID，返回
                    LoginInfo info = (LoginInfo)logidTable[key];
                    if (info.LoginSucess)
                    {
                        LOGIN_INFO_S loginInfo = info.LoginInfo_S;
                        uint ret = IMOSSDK.IMOS_StopRecord(ref loginInfo.stUserLoginIDInfo, Encoding.Default.GetBytes(ChannelCode));
                        if (ret != 0)
                        {
                            string msg = String.Format("视频录像停止失败！IP：{0} 摄像机:{1}", playArgs.Ip, playArgs.CamName);
                            Log(msg);
                            bRecording = false;
                            return false;
                        }
                        else
                        {
                            string msg = String.Format("视频录像停止成功！IP：{0} 摄像机:{1}", playArgs.Ip, playArgs.CamName);
                            Log(msg);
                            bRecording = false;
                            return true;

                        }


                    }
                }
            }
            return true;
        }
        public bool VIDEO_CapturePicture(string fileName)
        {
            if (PlayID == -1)
            {
                Log("操作失败，视频没有正常播放！");
                return false;
            }
            VideoRealPlayArgs e = playArgs;
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
                    if (!NetVideoControl.PicPath.EndsWith("\\"))
                        path = NetVideoControl.PicPath + "\\" + pubFun.DateStr + "\\";
                    else
                        path = NetVideoControl.PicPath + pubFun.DateStr + "\\";
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

                    string sPicFileName = String.Format("{0}{1}_{2}_{3}_{4}", path, fileName, playArgs.Ip, playArgs.DvrCh, time);


                    uint ret = IMOSSDK.IMOS_SnatchOnceEx(ref loginInfo.stUserLoginIDInfo, Encoding.Default.GetBytes(ChannelCode), Encoding.Default.GetBytes(sPicFileName), (uint)PictureFormat.XP_PICTURE_BMP);

                    if (ret != 0)
                    {
                        string msg = String.Format("图片抓拍失败！IP：{0} 通道号:{1}", playArgs.Ip, playArgs.DvrCh);
                        Log(msg);
                        return false;
                    }
                    //pubFun.ShowFileDirectory(sPicFileName + ".bmp");

                    return true;
                }
            }
            return true;
        }
        public bool VIDEO_CaptureJPEGPicture(string fileName)
        {
            if (PlayID == -1)
            {
                Log("操作失败，视频没有正常播放！");
                return false;
            }
            VideoRealPlayArgs e = playArgs;
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
                    if (!NetVideoControl.PicPath.EndsWith("\\"))
                        path = NetVideoControl.PicPath + "\\" + pubFun.DateStr + "\\";
                    else
                        path = NetVideoControl.PicPath + pubFun.DateStr + "\\";
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

                    string sPicFileName = String.Format("{0}{1}_{2}_{3}_{4}", path, fileName, playArgs.Ip, playArgs.DvrCh, time);


                    uint ret = IMOSSDK.IMOS_SnatchOnceEx(ref loginInfo.stUserLoginIDInfo, Encoding.Default.GetBytes(ChannelCode), Encoding.Default.GetBytes(sPicFileName), (uint)PictureFormat.XP_PICTURE_JPG);

                    if (ret != 0)
                    {
                        string msg = String.Format("图片抓拍失败！IP：{0} 通道号:{1}", playArgs.Ip, playArgs.DvrCh);
                        Log(msg);
                        return false;
                    }
                    //pubFun.ShowFileDirectory(sPicFileName+".jpg");

                    return true;


                }
            }

            return true;
        }

        #endregion

        #region 录像文件回放、下载

        #endregion

        #region 云台控制
        public bool VIDEO_PTZControlWithSpeed(uint dwPTZCommand, uint dwStop, uint dwSpeed)
        {
            MW_PTZ_CMD_E vcode;
            PTZCmdCodeEnum code = (PTZCmdCodeEnum)dwPTZCommand;
            if (dwStop == 0)
            {
                switch (code)
                {
                    case PTZCmdCodeEnum.PAN_LEFT:
                        vcode = MW_PTZ_CMD_E.MW_PTZ_PANLEFT;
                        break;
                    case PTZCmdCodeEnum.PAN_RIGHT:
                        vcode = MW_PTZ_CMD_E.MW_PTZ_PANRIGHT;
                        break;
                    case PTZCmdCodeEnum.TILT_DOWN:
                        vcode = MW_PTZ_CMD_E.MW_PTZ_TILTDOWN;
                        break;
                    case PTZCmdCodeEnum.TILT_UP:
                        vcode = MW_PTZ_CMD_E.MW_PTZ_TILTUP;
                        break;
                    case PTZCmdCodeEnum.ZOOM_IN:
                        vcode = MW_PTZ_CMD_E.MW_PTZ_ZOOMTELE;
                        break;
                    case PTZCmdCodeEnum.ZOOM_OUT:
                        vcode = MW_PTZ_CMD_E.MW_PTZ_ZOOMWIDE;
                        break;
                    default:
                        vcode = MW_PTZ_CMD_E.MW_PTZ_PANLEFT;
                        break;
                }
            }
            else
                vcode = MW_PTZ_CMD_E.MW_PTZ_ALLSTOP;


            PTZ_CTRL_COMMAND_S stPTZCommand = new PTZ_CTRL_COMMAND_S();
            stPTZCommand.ulPTZCmdID = (UInt32)vcode;
            stPTZCommand.ulPTZCmdPara1 = (UInt32)dwSpeed + 2;
            stPTZCommand.ulPTZCmdPara2 = (UInt32)dwSpeed;

            VideoRealPlayArgs e = playArgs;
            string key = e.Ip + ":" + e.Port.ToString();
            if (logidTable.ContainsKey(key))
            {
                LoginInfo info = logidTable[key] as LoginInfo;
                LOGIN_INFO_S stLoginInfo = info.LoginInfo_S;


                uint ulret = IMOSSDK.IMOS_PtzCtrlCommand(ref stLoginInfo.stUserLoginIDInfo, Encoding.Default.GetBytes(playArgs.CamID), ref stPTZCommand);
                if (0 != ulret)
                {
                    MessageBox.Show("云台操作失败！" + ulret);
                }
            }
            return true;

        }
        public bool VIDEO_PTZPreset(uint dwPTZCommand, uint PresetIndex)
        {
            /*PtzActionz vcode;
            PTZCmdCodeEnum code = (PTZCmdCodeEnum)dwPTZCommand;
            switch (code)
            {

                case PTZCmdCodeEnum.GOTO_PRESET:
                    vcode = PtzActionz.preset_call;
                    break;
                case PTZCmdCodeEnum.SET_PRESET:
                    vcode = PtzActionz.preset_save;
                    break;
                default:
                    vcode = PtzActionz.preset_call;
                    break;
            }*/

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
        #endregion

        #region 视频参数

        public VideoEffect VIDEO_GetVideoEffect()
        {
            VideoEffect ef = new VideoEffect();
            return ef;
        }
        public bool VIDEO_SetVideoEffect(VideoEffect effect)
        {
            return false;
        }

        #endregion

        #region 声音、对讲

        public bool VIDEO_OpenSound()
        {
            return false;

        }
        public bool VIDEO_CloseSound()
        {
            return false;
        }
        public bool VIDEO_Volume(ushort wVolume)
        {
            return false;
        }
        public bool VIDEO_StartVoiceCom()
        {
            return false;

        }
        public bool VIDEO_StopVoiceCom()
        {
            return false;

        }
        public bool VIDEO_SetVoiceComClientVolume(ushort wVolume)
        {
            return false;

        }



        #endregion


        public bool VIDEO_PlayControl(RealPlayControlEnum State)
        {
            if (State == RealPlayControlEnum.PLAY)
                return VIDEO_StartRealPlay();
            else
                return VIDEO_StopRealPlay();
        }
    }
    class LoginInfo
    {
        public string ServerIP;
        public int ServerPort;
        public LOGIN_INFO_S LoginInfo_S;
        public bool InitSucess;
        public bool LoginSucess;
    }
    enum DoWorkerType
    {
        Play,
        Stop,
        Ptz
    }
    enum PictureFormat
    {
        XP_PICTURE_BMP,//  图片格式为bmp格式 
        XP_PICTURE_JPG  //图片格式为jpg格式 
    }
    enum MediaFileFormat
    {
        XP_MEDIA_FILE_TS,  //TS格式的媒体文件 
        XP_MEDIA_FILE_FLV  //FLV格式的媒体文件 
    }




}
