using GHIBMS.Common;
using GHIBMS.DvrSDK;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace GHIBMS.VideoPlayback
{
    public class HikDvrPlayback : IVideoPlayback
    {
        #region 构造析造
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
        ~HikDvrPlayback()
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
        public static Hashtable logidTable = new Hashtable();
        private static UInt16 videoWndNubs = 1;
        private bool m_bSearching = false;
        private int m_lFileHandle = -1;
        private int m_lPlayHandle = -1;
        private int m_lDownloadHanle = -1;

        private VideoPlaybackArgs m_playbackArgs = new VideoPlaybackArgs();
        private Thread Thread_SearchFile;

        private string protocolCode = "JK_DVR_HK";
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
        private void Log(string message)
        {
            if (OnMessage != null)
                OnMessage(this, message);
        }
        public delegate void OnFileSearchdelegate(object sender, int result, List<DvrFindData> data);
        public event OnFileSearchdelegate OnFileSearch;
        public void OnWinMessage(ref System.Windows.Forms.Message m)
        {
        }
        public void SetWinMessage(IntPtr lWinHandle)
        {
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
            //MessageBox.Show("海康初始化成功");
            if (!hasInit)
            {
                hasInit = true;
                if (!HCNetSDK.NET_DVR_Init2())
                {
                    Log("海康DVR SDK初始化失败！");
                    return false;
                }
                HCNetSDK.NET_DVR_SetConnectTime(5000, 1);
            }
            return true;
        }
        //清理资源
        public void DVR_Cleanup()
        {
            DVR_Logout();
            if (hasInit)
            {
                hasInit = false;
                HCNetSDK.NET_DVR_Cleanup2();
            }
        }
        private int loginID = -1;

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
            if (logidTable.ContainsKey(key) && ((HikDvrLoginInfo)logidTable[key]).loginId == -1)
            {
                logidTable.Remove(key);
            }

            if (!logidTable.ContainsKey(key))
            {

                GHIBMS.DvrSDK.HCNetSDK.NET_DVR_DEVICEINFO_V30 lpDeviceInfo = new GHIBMS.DvrSDK.HCNetSDK.NET_DVR_DEVICEINFO_V30();
                //NET_DVR_DEVICEINFO lpDeviceInfo = new NET_DVR_DEVICEINFO();
                int userId = HCNetSDK.NET_DVR_Login_V30(e.Ip, e.Port, e.UserName, e.Password, ref lpDeviceInfo);
                if (userId > -1)
                {
                    HikDvrLoginInfo info = new HikDvrLoginInfo();
                    info.loginId = userId;
                    info.DeviceInfo = lpDeviceInfo;
                    logidTable.Add(key, info);
                    string msg = String.Format("DVR登录成功！IP：{0} 返回的用户ID:{1}", e.Ip, userId);
                    Log(msg);
                    return true;
                }
                else
                {
                    Log("DVR登录失败！ IP:" + e.Ip + "  " + e.Port);
                    return false;
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
                int id = ((HikDvrLoginInfo)logidTable[key]).loginId;
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
                int id = ((HikDvrLoginInfo)logidTable[key]).loginId;
                if (id > -1)
                    return id;
            }
            return -1;
        }
        public static bool DVR_Logout()
        {
            foreach (DictionaryEntry de in logidTable)
            {
                int id = ((HikDvrLoginInfo)de.Value).loginId;

                if (id > -1)
                {
                    if (!HCNetSDK.NET_DVR_Logout_V30(id))
                    {

                    }
                }

            }
            logidTable.Clear();
            return true;
        }
        private HikDvrLoginInfo GetLoginDvrInfo()
        {
            VideoPlaybackArgs e = m_playbackArgs;
            string key = e.Ip + ":" + e.Port.ToString();
            if (logidTable.ContainsKey(key))
            {
                return (HikDvrLoginInfo)logidTable[key];
            }
            return new HikDvrLoginInfo();
        }
        #endregion

        #region 查找文件

        public bool VIDEO_FindFile(uint dwFileType, byte isLock, DateTime dtStar, DateTime dtEnd)
        {
            try
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
                HikDvrLoginInfo devInfo = GetLoginDvrInfo();

                //设备模拟通道个数
                int AChanNum = devInfo.DeviceInfo.byChanNum;
                //模拟通道的起始通道号，目前设备模拟通道号从1开始
                int AStartChan = devInfo.DeviceInfo.byStartChan;
                //设备最大数字通道个数
                int IPChanNum = devInfo.DeviceInfo.byIPChanNum;
                //起始数字通道号，0表示无效
                int IPStartChan = devInfo.DeviceInfo.byStartDChan;
                HCNetSDK.NET_DVR_FILECOND_V40 m_struFileCond = new HCNetSDK.NET_DVR_FILECOND_V40();

                if (!m_bSearching)
                {

                    HCNetSDK.NET_DVR_TIME StartTime = new HCNetSDK.NET_DVR_TIME();
                    HCNetSDK.NET_DVR_TIME StopTime = new HCNetSDK.NET_DVR_TIME();

                    //设置录像查找的开始时间 Set the starting time to search video files
                    StartTime.dwYear = (uint)dtStar.Year;
                    StartTime.dwMonth = (uint)dtStar.Month;
                    StartTime.dwDay = (uint)dtStar.Day;
                    StartTime.dwHour = (uint)dtStar.Hour;
                    StartTime.dwMinute = (uint)dtStar.Minute;
                    StartTime.dwSecond = (uint)dtStar.Second;

                    //设置录像查找的结束时间 Set the stopping time to search video files
                    StopTime.dwYear = (uint)dtEnd.Year;
                    StopTime.dwMonth = (uint)dtEnd.Month;
                    StopTime.dwDay = (uint)dtEnd.Day;
                    StopTime.dwHour = (uint)dtEnd.Hour;
                    StopTime.dwMinute = (uint)dtEnd.Minute;
                    StopTime.dwSecond = (uint)dtEnd.Second;

                    byte[] sCardNumber = new byte[32];
                    //通道号 Channel number

                    if (AChanNum == 0 && IPChanNum > 0) //只有数字通道
                    {
                        m_struFileCond.lChannel = e.DvrCh + IPStartChan - 1;//预览的设备通道 the device channel number
                    }
                    else
                    {
                        //如果是混合录像机，数字通道要自行加上数字的起始通道，通常是加32;
                        m_struFileCond.lChannel = e.DvrCh + AStartChan - 1;
                    }

                    m_struFileCond.dwFileType = dwFileType;
                    m_struFileCond.dwIsLocked = isLock;
                    m_struFileCond.sCardNumber = sCardNumber;
                    m_struFileCond.dwUseCardNo = 0;
                    m_struFileCond.struStartTime = StartTime;
                    m_struFileCond.struStopTime = StopTime;
                    FindDataList.Clear();

                    m_lFileHandle = HCNetSDK.NET_DVR_FindFile_V40(loginID, ref m_struFileCond);
                    if (m_lFileHandle != -1)
                    {

                        m_bSearching = true;
                        Thread_SearchFile = new Thread(new ThreadStart(ThreadSearchFile));
                        Thread_SearchFile.IsBackground = true;
                        Thread_SearchFile.Start();
                        return true;
                    }
                    else
                    {
                        Log("文件查找失败！");
                        m_bSearching = false;
                        if (OnFileSearch != null)
                            OnFileSearch(this, strConst.NET_DVR_FILE_EXCEPTION, FindDataList);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Logger.GetInstance().LogError(ex.ToString());
            }
            return false;

        }

        private void ThreadSearchFile()
        {
            try
            {
                HCNetSDK.NET_DVR_FINDDATA_V30 struFileInfo = new HCNetSDK.NET_DVR_FINDDATA_V30();
                while (true)
                {
                    int lRet = HCNetSDK.NET_DVR_FindNextFile_V30(m_lFileHandle, ref struFileInfo);
                    if (lRet == HCNetSDK.NET_DVR_FILE_SUCCESS)
                    {
                        DvrFindData newFind = new DvrFindData();
                        newFind.FileName = struFileInfo.sFileName;

                        string starttime = struFileInfo.struStartTime.dwYear + "-" + struFileInfo.struStartTime.dwMonth + "-" + struFileInfo.struStartTime.dwDay
                                           + " " + struFileInfo.struStartTime.dwHour + ":" + struFileInfo.struStartTime.dwMinute + ":" + struFileInfo.struStartTime.dwSecond;
                        string endtime = struFileInfo.struStopTime.dwYear + "-" + struFileInfo.struStopTime.dwMonth + "-" + struFileInfo.struStopTime.dwDay
                                           + " " + struFileInfo.struStopTime.dwHour + ":" + struFileInfo.struStopTime.dwMinute + ":" + struFileInfo.struStopTime.dwSecond;
                        newFind.StartTime = Convert.ToDateTime(starttime);
                        newFind.StopTime = Convert.ToDateTime(endtime);

                        string filesize;
                        if (struFileInfo.dwFileSize / 1024 == 0)
                        {
                            filesize = struFileInfo.dwFileSize.ToString();
                        }
                        else if (struFileInfo.dwFileSize / 1024 > 0 && struFileInfo.dwFileSize / (1024 * 1024) == 0)
                        {
                            filesize = (struFileInfo.dwFileSize / 1024).ToString() + "K";
                        }
                        else
                        {
                            filesize = (struFileInfo.dwFileSize / 1024 / 1024).ToString() + "M";
                        }
                        newFind.FileSize = filesize;
                        FindDataList.Add(newFind);
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
                            HCNetSDK.NET_DVR_FindClose_V30(m_lFileHandle);
                            m_lFileHandle = -1;
                            //获取文件结束
                            if (OnFileSearch != null)
                                OnFileSearch(this, strConst.NET_DVR_NOMOREFILE, FindDataList);
                            m_bSearching = false;
                            break;

                        }
                        else
                        {

                            HCNetSDK.NET_DVR_FindClose_V30(m_lFileHandle);
                            m_lFileHandle = -1;
                            //获取文件异常终止
                            if (OnFileSearch != null)
                                OnFileSearch(this, strConst.NET_DVR_FILE_EXCEPTION, FindDataList);
                            m_bSearching = false;
                            break;
                        }
                    }
                    Thread.Sleep(1);

                }
            }
            catch { }
        }
        public bool VIDEO_FindClose()
        {
            //FindDataList.Clear();
            if (OnFileSearch != null)
                OnFileSearch(this, strConst.NET_DVR_FILE_EXCEPTION, FindDataList);

            HCNetSDK.NET_DVR_FindClose_V30(m_lFileHandle);
            m_lFileHandle = -1;
            m_bSearching = false;
            try
            {
                if (Thread_SearchFile != null && Thread_SearchFile.IsAlive)
                {
                    Thread_SearchFile.Abort();
                    Thread_SearchFile = null;
                }
            }
            catch
            {
                Console.WriteLine("线程人工终止成功！");
            }
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
            m_lPlayHandle = HCNetSDK.NET_DVR_PlayBackByName(loginID, sPlayBackFileName, e.PlayWnd);

            if (m_lPlayHandle == -1)
                return false;
            else return true;
        }
        public bool VIDEO_PlayBackByTime(DateTime dtStar, DateTime dtEnd)
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

            HikDvrLoginInfo devInfo = GetLoginDvrInfo();

            //设备模拟通道个数
            int AChanNum = devInfo.DeviceInfo.byChanNum;
            //模拟通道的起始通道号，目前设备模拟通道号从1开始
            int AStartChan = devInfo.DeviceInfo.byStartChan;
            //设备最大数字通道个数
            int IPChanNum = devInfo.DeviceInfo.byIPChanNum;
            //起始数字通道号，0表示无效
            int IPStartChan = devInfo.DeviceInfo.byStartDChan;


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


            HCNetSDK.NET_DVR_VOD_PARA struVodPara = new HCNetSDK.NET_DVR_VOD_PARA();
            struVodPara.dwSize = (uint)Marshal.SizeOf(struVodPara);
            struVodPara.hWnd = e.PlayWnd;//回放窗口句柄
            struVodPara.struBeginTime = StartTime;
            struVodPara.struEndTime = StopTime;
            if (AChanNum == 0 && IPChanNum > 0) //只有数字通道
            {
                struVodPara.struIDInfo.dwChannel = (uint)(e.DvrCh + IPStartChan - 1);//预览的设备通道 the device channel number
            }
            else
            {
                //如果是混合录像机，数字通道要自行加上数字的起始通道，通常是加32;
                struVodPara.struIDInfo.dwChannel = (uint)(e.DvrCh + AStartChan - 1);
            }

            m_lPlayHandle = HCNetSDK.NET_DVR_PlayBackByTime_V40(loginID, ref struVodPara);
            if (m_lPlayHandle == -1)
            {
                uint iLastErr = HCNetSDK.NET_DVR_GetLastError();
                Log("视频按时间回放失败，错误码：" + iLastErr.ToString());
                Logger.GetInstance().LogMsg("视频按时间回放失败，错误码：" + iLastErr.ToString());

            }
            uint nil = 0;
            HCNetSDK.NET_DVR_PlayBackControl(m_lPlayHandle, strConst.NET_DVR_PLAYSTART, 0, ref nil);
            if (m_lPlayHandle == -1)
                return false;
            else return true;
        }
        public bool VIDEO_PlayBackSaveData()
        {
            if (m_lPlayHandle > -1)
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



                if (HCNetSDK.NET_DVR_PlayBackSaveData(m_lPlayHandle, sFileName))
                {
                    return true;
                }
            }
            return false;
        }
        public bool VIDEO_StopPlayBackSave()
        {
            if (m_lPlayHandle > -1)
            {
                if (HCNetSDK.NET_DVR_StopPlayBackSave(m_lPlayHandle))
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

            if (m_lPlayHandle > -1)
            {
                if (HCNetSDK.NET_DVR_PlayBackCaptureFile(m_lPlayHandle, sPicFileName))
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

            m_lDownloadHanle = HCNetSDK.NET_DVR_GetFileByName(loginID, sDVRFileName, sFileName);
            if (m_lDownloadHanle > -1)
            {
                uint nil = 0;
                HCNetSDK.NET_DVR_PlayBackControl(m_lDownloadHanle, strConst.NET_DVR_PLAYSTART, 0, ref nil);
            }
            return (m_lDownloadHanle == -1) ? false : true;
        }
        public bool VIDEO_GetFileByTime(DateTime dtStar, DateTime dtEnd)
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

            HikDvrLoginInfo devInfo = GetLoginDvrInfo();

            //设备模拟通道个数
            int AChanNum = devInfo.DeviceInfo.byChanNum;
            //模拟通道的起始通道号，目前设备模拟通道号从1开始
            int AStartChan = devInfo.DeviceInfo.byStartChan;
            //设备最大数字通道个数
            int IPChanNum = devInfo.DeviceInfo.byIPChanNum;
            //起始数字通道号，0表示无效
            int IPStartChan = devInfo.DeviceInfo.byStartDChan;


            HCNetSDK.NET_DVR_PLAYCOND struDownPara = new HCNetSDK.NET_DVR_PLAYCOND();

            HCNetSDK.NET_DVR_TIME StartTime = new HCNetSDK.NET_DVR_TIME();
            HCNetSDK.NET_DVR_TIME StopTime = new HCNetSDK.NET_DVR_TIME();

            //设置录像查找的开始时间 Set the starting time to search video files
            StartTime.dwYear = (uint)dtStar.Year;
            StartTime.dwMonth = (uint)dtStar.Month;
            StartTime.dwDay = (uint)dtStar.Day;
            StartTime.dwHour = (uint)dtStar.Hour;
            StartTime.dwMinute = (uint)dtStar.Minute;
            StartTime.dwSecond = (uint)dtStar.Second;

            //设置录像查找的结束时间 Set the stopping time to search video files
            StopTime.dwYear = (uint)dtEnd.Year;
            StopTime.dwMonth = (uint)dtEnd.Month;
            StopTime.dwDay = (uint)dtEnd.Day;
            StopTime.dwHour = (uint)dtEnd.Hour;
            StopTime.dwMinute = (uint)dtEnd.Minute;
            StopTime.dwSecond = (uint)dtEnd.Second;

            byte[] sCardNumber = new byte[32];
            //通道号 Channel number

            if (AChanNum == 0 && IPChanNum > 0) //只有数字通道
            {
                struDownPara.dwChannel = (uint)(e.DvrCh + IPStartChan - 1);//预览的设备通道 the device channel number
            }
            else
            {
                //如果是混合录像机，数字通道要自行加上数字的起始通道，通常是加32;
                struDownPara.dwChannel = (uint)(e.DvrCh + AStartChan - 1);
            }


            struDownPara.struStartTime = StartTime;
            struDownPara.struStopTime = StopTime;


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

            m_lDownloadHanle = HCNetSDK.NET_DVR_GetFileByTime_V40(loginID, sFileName, ref struDownPara);
            if (m_lDownloadHanle > -1)
            {
                uint nil = 0;
                HCNetSDK.NET_DVR_PlayBackControl(m_lDownloadHanle, strConst.NET_DVR_PLAYSTART, 0, ref nil);
            }

            return (m_lDownloadHanle == -1) ? false : true;
        }
        public bool VIDEO_StopGetFile()
        {
            if (m_lDownloadHanle > -1)
            {
                if (HCNetSDK.NET_DVR_StopGetFile(m_lDownloadHanle))
                {
                    m_lDownloadHanle = -1;
                    return true;
                }
            }
            return false;
        }
        public int VIDEO_GetDownloadPos()
        {
            int pos = 0;
            if (m_lDownloadHanle > -1)
                pos = HCNetSDK.NET_DVR_GetDownloadPos(m_lDownloadHanle);
            return pos;

        }
        public bool VIDEO_PlayBackControl(uint dwControlCode, uint dwInValue, out uint lpOutValue)
        {
            lpOutValue = 0;
            if (m_lPlayHandle > -1)
                return HCNetSDK.NET_DVR_PlayBackControl_V40(m_lPlayHandle, dwControlCode, IntPtr.Zero, (uint)dwInValue, IntPtr.Zero, ref lpOutValue);
            else
                return false;
        }
        public bool VIDEO_StopPlayBack()
        {
            if (m_lPlayHandle > -1)
                return HCNetSDK.NET_DVR_StopPlayBack(m_lPlayHandle);
            else
                return false;
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

        struct HikDvrLoginInfo
        {
            public int loginId;
            public HCNetSDK.NET_DVR_DEVICEINFO_V30 DeviceInfo;
        }
    }
}
