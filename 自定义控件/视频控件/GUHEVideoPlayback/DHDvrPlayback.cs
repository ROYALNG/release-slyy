using DHNetSDK;
using GHIBMS.DvrSDK;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace GHIBMS.VideoPlayback
{
    public class DHDvrPlayback : IVideoPlayback
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
        ~DHDvrPlayback()
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
        /// <summary>
        /// 选择的文件信息
        /// </summary>
        public NET_RECORDFILE_INFO gFileInfo;
        private bool m_bSearching = false;

        /// <summary>
        /// 文件信息查询结果
        /// </summary>
        private NET_RECORDFILE_INFO[] nriFileInfo;

        /// <summary>
        /// 最多查询文件数
        /// </summary>
        private const int intFilesMaxCount = 50;
        private fDownLoadPosCallBack downLoadFun;
        private fDownLoadPosCallBack playbackPosFun;
        /// <summary>
        /// 下载进度百分比
        /// </summary>
        private int iDownLoadPos;
        private int iPlaybackPos;
        private UInt32 TotalSize;
        private fDisConnect disConnect;
        private fHaveReConnect haveReConnect;
        private DHNetSDK.NET_DEVICEINFO deviceInfo;
        public static bool hasInit = false;
        public static Hashtable logidTable = new Hashtable();
        private static UInt16 videoWndNubs = 1;
        private int m_lFileHandle = 0;
        private int m_lPlayHandle = 0;
        private int m_lDownloadHanle = 0;
        private int loginID = 0;
        private DateTime dtStartTime, dtEndTime;
        //private NET_RECORDFILE_INFO m_struFileCond = new NET_RECORDFILE_INFO();
        private VideoPlaybackArgs m_playbackArgs = new VideoPlaybackArgs();
        private Thread Thread_SearchFile;
        private PlaybackModeEnum playbackMode = PlaybackModeEnum.按文件;
        private string protocolCode = "JK_DVR_DH";
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
        public void OnWinMessage(ref System.Windows.Forms.Message m)
        {
        }
        public void SetWinMessage(IntPtr lWinHandle)
        {
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
        private void DisConnectEvent(int lLoginID, StringBuilder pchDVRIP, int nDVRPort, IntPtr dwUser)
        {
            //设备断开连接处理            
            // MessageBox.Show("设备用户断开连接", pMsgTitle);
            if (OnMessage != null)
                OnMessage(this, "设备用户断开连接!IP:" + pchDVRIP + " Port:" + nDVRPort.ToString());
        }
        private void HaveReConnectEvent(int lLoginID, StringBuilder pchDVRIP, int nDVRPort, IntPtr dwUser)
        {
            //设备断开连接处理            
            // MessageBox.Show("设备用户断开连接", pMsgTitle);
            if (OnMessage != null)
                OnMessage(this, "设备用户重新连接!IP:" + pchDVRIP + " Port:" + nDVRPort.ToString());
        }


        #endregion

        public DHDvrPlayback()
        {
            downLoadFun = new fDownLoadPosCallBack(DownLoadPosFun);
            playbackPosFun = new fDownLoadPosCallBack(PlaybackPosFun);
        }
        /// <summary>
        /// 下载回调
        /// </summary>
        /// <param name="lPlayHandle">播放句柄</param>
        /// <param name="dwTotalSize">累计大小</param>
        /// <param name="dwDownLoadSize">下载大小</param>
        /// <param name="dwUser">用户数据</param>
        private void DownLoadPosFun(int lPlayHandle, UInt32 dwTotalSize, UInt32 dwDownLoadSize, IntPtr dwUser)
        {
            if (0xFFFFFFFF == dwDownLoadSize)
            {
                iDownLoadPos = 100;
            }
            else
            {
                double pos = ((double)dwDownLoadSize / (double)dwTotalSize);
                iDownLoadPos = (int)(pos * 100);
            }
            Debug.WriteLine("DownLoadPosFun" + iDownLoadPos);
            //Debug.WriteLine("POS:" + iDownLoadPos.ToString());
            //psbMain.Maximum = (int)dwTotalSize;
            //psbMain.Value = (int)(dwDownLoadSize != 0xFFFFFFFF && dwDownLoadSize != 0xFFFFFFFE && dwDownLoadSize <= dwTotalSize ? dwDownLoadSize : 0);

            //if ((0xFFFFFFFF == dwDownLoadSize) /*|| (dwDownLoadSize == dwTotalSize)*/)
            //{
            //    btnDownLoad1.Tag = "";
            //    psbMain.Value = 0;
            //    DHClient.DHStopDownload(pDownloadHandle);
            //    MessageBox.Show("下载结束！");
            //}
            //else if (0xFFFFFFFE == dwDownLoadSize)
            //{
            //    MessageBox.Show("磁盘空间不足！");
            //    DHClient.DHStopDownload(pDownloadHandle);
            //}

        }
        /// <summary>
        /// 回放进度回调
        /// </summary>
        /// <param name="lPlayHandle">播放句柄</param>
        /// <param name="dwTotalSize">累计大小</param>
        /// <param name="dwDownLoadSize">下载大小</param>
        /// <param name="dwUser">用户数据</param>
        private void PlaybackPosFun(int lPlayHandle, UInt32 dwTotalSize, UInt32 dwDownLoadSize, IntPtr dwUser)
        {

            //dblDownLoadPos = ((double)(dwDownLoadSize / dwTotalSize) * 100);
            //psbMain.Maximum = (int)dwTotalSize;
            //psbMain.Value = (int)(dwDownLoadSize != 0xFFFFFFFF && dwDownLoadSize != 0xFFFFFFFE && dwDownLoadSize <= dwTotalSize ? dwDownLoadSize : 0);
            TotalSize = dwTotalSize;
            if ((0xFFFFFFFF == dwDownLoadSize) /*|| (dwDownLoadSize == dwTotalSize)*/)
            {

                DHClient.DHPlayBackControl(m_lPlayHandle, PLAY_CONTROL.Stop);
                Log("下载结束！");
                iPlaybackPos = 100;
                return;

            }
            //Debug.WriteLine("dwDownLoadSize:" + dwDownLoadSize + "----dwTotalSize" + dwTotalSize);
            double pos = ((double)dwDownLoadSize / (double)dwTotalSize);
            iPlaybackPos = (int)(pos * 100);
            //Debug.WriteLine("iPlaybackPos:" + iPlaybackPos.ToString());

            //else if (0xFFFFFFFE == dwDownLoadSize)
            //{
            //    MessageBox.Show("磁盘空间不足！");
            //    DHClient.DHStopDownload(pDownloadHandle);
            //}
        }
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
                disConnect = new fDisConnect(DisConnectEvent);
                if (!DHClient.DHInit(null, IntPtr.Zero))
                {
                    Log("大华DVR SDK初始化失败！");
                    return false;
                }
                haveReConnect = new fHaveReConnect(HaveReConnectEvent);
                DHClient.DHSetAutoReconnect(haveReConnect, IntPtr.Zero);
                DHClient.DHSetEncoding(LANGUAGE_ENCODING.gb2312);//字符编码格式设置，默认为gb2312字符编码，如果为其他字符编码请设置            
                DHClient.DHSetConnectTime(5000, 1);
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
                DHClient.DHCleanup();
            }
        }


        #endregion

        #region 设备注册
        private bool DVR_Login()
        {
            VIDEO_Init();
            VideoPlaybackArgs e = m_playbackArgs;
            if (e.Ip == "0.0.0.0")
            {
                Log("视频回放失败，没有正确设置播放参数！");
                return false;

            }
            if (logidTable.ContainsKey(e.Ip) && int.Parse(logidTable[e.Ip].ToString()) == 0)
            {
                logidTable.Remove(e.Ip);
            }
            if (!logidTable.ContainsKey(e.Ip))
            {

                deviceInfo = new DHNetSDK.NET_DEVICEINFO();
                int error = 0;

                int userId = DHClient.DHLogin(e.Ip, e.Port, e.UserName, e.Password, out deviceInfo, out error);
                if (userId > 0)
                {
                    loginID = userId;
                    logidTable.Add(e.Ip, userId);
                    string msg = String.Format("DVR登录成功！IP：{0} 返回的用户ID:{1}", e.Ip, userId);
                    Log(msg);
                    return true;
                }
                else
                {
                    Log("DVR登录失败！ IP:" + e.Ip);
                    return false;
                }
            }
            return false;
        }
        private int GetLogID()
        {

            VideoPlaybackArgs e = m_playbackArgs;

            if (logidTable.ContainsKey(e.Ip))
            {
                //存在正确的登录ID，返回
                int id = int.Parse(logidTable[e.Ip].ToString());
                if (id > 0)
                    return id;
                else
                {
                    //登录ID不正确，移出HT,再次调用自我
                    logidTable.Remove(e.Ip);
                    DVR_Login();
                }
            }
            else //没有登录
            {
                DVR_Login();
            }
            //登录后再次查找logID
            if (logidTable.ContainsKey(e.Ip))
            {
                //存在正确的登录ID，返回
                int id = int.Parse(logidTable[e.Ip].ToString());
                if (id > 0)
                    return id;
            }
            return 0;
        }
        public static bool DVR_Logout()
        {
            foreach (DictionaryEntry de in logidTable)
            {
                int logid = int.Parse(de.Value.ToString());
                string ip = de.Key.ToString();
                if (logid > 0)
                {
                    if (!DHClient.DHLogout(logid))
                    {

                    }
                }

            }
            logidTable.Clear();
            return true;
        }
        #endregion

        #region 查找文件
        /*dwFileType 海康
        录象文件类型（根据dwUseCardNo参数是否带卡号查找分为两类）：
        不带卡号查找时类型：0xff－全部，0－定时录像，1-移动侦测，2－报警触发，3-报警触发或移动侦测，4-报警触发和移动侦测，5-命令触发，6-手动录像，7-智能录像
        带卡号查找时类型：0xff－全部，0－定时录像，1—移动侦测，2－接近报警，3－出钞报警，4－进钞报警，5—命令触发，6－手动录像，7－震动报警，8-环境报警，9-智能报警 
        dwIsLocked 
        是否锁定：0-未锁定文件，1-锁定文件，0xff表示所有文件（包括锁定和未锁定） 
        */

        /*  对应大华为
        [in]nRecordFileType 
        录像文件类型，如下表: 数值 录象文件类型 
        0 所有录像文件  
        1 外部报警 
        2 动态检测报警 
        3 所有报警 
        4 卡号查询  
        5 组合条件查询 
        6 录像位置与偏移量长度 
        8 按卡号查询图片(目前仅HB-U和NVS特殊型号的设备支持) 
        9 查询图片(目前仅HB-U和NVS特殊型号的设备支持)  
        10 按字段查询 
        15 返回网络数据结构（金桥网吧） 
        16 查询所有透明串数据录像文件 
        */
        public bool VIDEO_FindFile(uint dwFileType, byte isLock, DateTime dtStar, DateTime dtEnd)
        {
            RECORD_FILE_TYPE dhFileType = 0;
            switch (dwFileType)
            {
                case 0xFF:
                case 0:
                    dhFileType = RECORD_FILE_TYPE.ALLRECORDFILE;
                    break;
                case 1:
                    dhFileType = RECORD_FILE_TYPE.DYNAMICSCANALARM;
                    break;
                case 2:
                    dhFileType = RECORD_FILE_TYPE.ALLALARM;
                    break;
                default:
                    dhFileType = RECORD_FILE_TYPE.ALLRECORDFILE;
                    break;
            }

            VideoPlaybackArgs e = m_playbackArgs;
            if (e.Ip == "0.0.0.0")
            {
                Log("视频回放失败，没有正确设置播放参数！");
                return false;
            }
            loginID = GetLogID();
            if (loginID == 0)
            {
                Log("视频回放失败，没有正确登录DVR！");
                return false;
            }
            if (!m_bSearching)
            {


                DHNetSDK.NET_TIME StartTime = new DHNetSDK.NET_TIME();
                DHNetSDK.NET_TIME StopTime = new DHNetSDK.NET_TIME();

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

                //byte[] sCardNumber = new byte[32];
                //m_struFileCond.lChannel = e.DvrCh;
                //m_struFileCond.dwFileType = dwFileType;
                //m_struFileCond.dwIsLocked = isLock;
                //m_struFileCond.sCardNumber = sCardNumber;
                //m_struFileCond.dwUseCardNo = 0;
                //m_struFileCond.struStartTime = StartTime;
                //m_struFileCond.struStopTime = StopTime;

                //FindDataList.Clear();
                //m_lFileHandle = DHClient.DHFindFile(logID, e.DvrCh, (int)dhFileType, null, StartTime,StopTime,false,5000);
                //if (m_lFileHandle != -1)
                //{
                //    m_bSearching = true;
                //    Thread_SearchFile = new Thread(new ThreadStart(ThreadSearchFile));
                //    Thread_SearchFile.Start();
                //    return true;
                //}
                //else
                //{
                //    Log("文件查找失败！");
                //    m_bSearching = false;
                //    if (OnFileSearch != null)
                //        OnFileSearch(this, strConst.NET_DVR_FILE_EXCEPTION, FindDataList);
                //    return false;
                //}

                #region << 大华查询操作2 >>

                m_bSearching = true;
                nriFileInfo = new NET_RECORDFILE_INFO[intFilesMaxCount];
                string strTimeFormatStyle = "yyyy年mm月dd日 hh:MM:ss";//日期时间格式化字符，具体定义请参见NET_TIME结构的ToSting方法说明
                int intFileCount = 0;
                bool blnQueryRecordFile = false;
                //查询录像
                blnQueryRecordFile = DHClient.DHQueryRecordFile(loginID, m_playbackArgs.DvrCh - 1, dhFileType, dtStar, dtEnd, null, ref nriFileInfo, intFilesMaxCount * Marshal.SizeOf(typeof(NET_RECORDFILE_INFO)), out intFileCount, 5000, false);
                if (blnQueryRecordFile == true)
                {
                    FindDataList.Clear();
                    for (int i = 0; i < intFileCount; i++)
                    {
                        DvrFindData newFind = new DvrFindData();
                        newFind.FileName = Guid.NewGuid().ToString();
                        newFind.StartTime = Convert.ToDateTime(nriFileInfo[i].starttime.ToString(strTimeFormatStyle));
                        newFind.StopTime = Convert.ToDateTime(nriFileInfo[i].endtime.ToString(strTimeFormatStyle));
                        newFind.FileSize = nriFileInfo[i].size.ToString();
                        newFind.RecordFile = nriFileInfo[i];
                        FindDataList.Add(newFind);
                    }
                    //获取文件结束
                    if (OnFileSearch != null)
                        OnFileSearch(this, strConst.NET_DVR_NOMOREFILE, FindDataList);
                    m_bSearching = false;
                }
                else
                {
                    Log("文件查找失败！");
                    m_bSearching = false;
                    if (OnFileSearch != null)
                        OnFileSearch(this, strConst.NET_DVR_FILE_EXCEPTION, FindDataList);
                    return false;
                }
                #endregion


            }
            return false;
        }

        private void ThreadSearchFile()
        {
            try
            {

                NET_RECORDFILE_INFO struFileInfo = new NET_RECORDFILE_INFO();
                while (true)
                {
                    int lRet = DHClient.DHFindNextFile(m_lFileHandle, struFileInfo);
                    //1：成功取回一条录像记录，0：录像记录已取完，-1：参数出错。
                    if (lRet == 1)
                    {
                        DvrFindData newFind = new DvrFindData();
                        newFind.FileName = Guid.NewGuid().ToString();

                        string starttime = struFileInfo.starttime.dwYear + "-" + struFileInfo.starttime.dwMonth + "-" + struFileInfo.starttime.dwDay
                                           + " " + struFileInfo.starttime.dwHour + ":" + struFileInfo.starttime.dwMinute + ":" + struFileInfo.starttime.dwSecond;
                        string endtime = struFileInfo.endtime.dwYear + "-" + struFileInfo.endtime.dwMonth + "-" + struFileInfo.endtime.dwDay
                                           + " " + struFileInfo.endtime.dwHour + ":" + struFileInfo.endtime.dwMinute + ":" + struFileInfo.endtime.dwSecond;
                        newFind.StartTime = Convert.ToDateTime(starttime);
                        newFind.StopTime = Convert.ToDateTime(endtime);

                        string filesize;
                        if (struFileInfo.size / 1024 == 0)
                        {
                            filesize = struFileInfo.size.ToString();
                        }
                        else if (struFileInfo.size / 1024 > 0 && struFileInfo.size / (1024 * 1024) == 0)
                        {
                            filesize = (struFileInfo.size / 1024).ToString() + "K";
                        }
                        else
                        {
                            filesize = (struFileInfo.size / 1024 / 1024).ToString() + "M";
                        }
                        newFind.FileSize = filesize;
                        newFind.RecordFile = struFileInfo;
                        FindDataList.Add(newFind);
                    }
                    else if (lRet == 0)
                    {
                        DHClient.DHFindClose(m_lFileHandle);
                        m_lFileHandle = -1;
                        //获取文件结束
                        if (OnFileSearch != null)
                            OnFileSearch(this, strConst.NET_DVR_NOMOREFILE, FindDataList);
                        m_bSearching = false;
                        break;

                    }
                    else if (lRet == -1)
                    {
                        DHClient.DHFindClose(m_lFileHandle);
                        m_lFileHandle = -1;
                        //获取文件异常终止
                        if (OnFileSearch != null)
                            OnFileSearch(this, strConst.NET_DVR_FILE_EXCEPTION, FindDataList);
                        m_bSearching = false;
                        break;
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


            DHClient.DHFindClose(m_lFileHandle);
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
            playbackMode = PlaybackModeEnum.按文件;
            VideoPlaybackArgs e = m_playbackArgs;
            if (e.Ip == "0.0.0.0")
            {
                Log("视频按文件回放失败，没有正确设置播放参数！");
                return false;
            }
            loginID = GetLogID();
            if (loginID == 0)
            {
                Log("视频按文件回放失败，没有正确登录DVR！");
                return false;
            }
            NET_RECORDFILE_INFO fileInfo = new NET_RECORDFILE_INFO();
            foreach (DvrFindData f in FindDataList)
            {
                if (f.FileName == sPlayBackFileName)
                {
                    fileInfo = f.RecordFile;
                    break;
                }
            }
            m_lPlayHandle = DHClient.DHPlayBackByRecordFile(loginID, ref fileInfo, e.PlayWnd, playbackPosFun, IntPtr.Zero);

            if (m_lPlayHandle == 0)
            {
                return false;
            }
            return true;
        }
        public bool VIDEO_PlayBackByTime(DateTime dtStar, DateTime dtEnd)
        {
            dtStartTime = dtStar;
            dtEndTime = dtEnd;
            playbackMode = PlaybackModeEnum.按时间;
            VideoPlaybackArgs e = m_playbackArgs;
            if (e.Ip == "0.0.0.0")
            {
                Log("视频按时间回放失败，没有正确设置播放参数！");
                return false;
            }
            loginID = GetLogID();
            if (loginID == 0)
            {
                Log("视频按时间回放失败，没有正确登录DVR！");
                return false;
            }
            bool blnQueryRecordFile = false;

            DHNetSDK.NET_TIME StartTime = new DHNetSDK.NET_TIME();
            DHNetSDK.NET_TIME StopTime = new DHNetSDK.NET_TIME();

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
            NET_RECORDFILE_INFO fileInfo = new NET_RECORDFILE_INFO();
            int fileCount;
            blnQueryRecordFile = DHClient.DHQueryRecordFile(loginID, e.DvrCh - 1, RECORD_FILE_TYPE.ALLRECORDFILE,
                                dtStar, dtEnd, null, ref fileInfo, Marshal.SizeOf(typeof(NET_RECORDFILE_INFO)),
                                out fileCount, 5000, false);//按时间回放
            if (blnQueryRecordFile == true)
            {

                m_lPlayHandle = DHClient.DHPlayBackByTime(loginID, e.DvrCh - 1, dtStar, dtEnd, e.PlayWnd, null, IntPtr.Zero);
                if (m_lPlayHandle == 0)
                {
                    Log("按时间回放失败！IP:" + e.Ip);
                    return false;
                }
            }
            return true;

        }
        public bool VIDEO_PlayBackSaveData()
        {
            if (m_lPlayHandle > 0)
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

                if (!DHClient.DHStartSaveRealData(m_lPlayHandle, sFileName))
                {
                    string msg = String.Format("视频录像失败！IP：{0} 通道号:{1}", m_playbackArgs.Ip, m_playbackArgs.DvrCh);
                    Log(msg);
                    // bRecording = false;
                    return false;
                }
                else
                {
                    string msg = String.Format("视频录像开始！IP：{0} 通道号:{1}", m_playbackArgs.Ip, m_playbackArgs.DvrCh);
                    Log(msg);
                    // bRecording = true;
                    return true;
                }
            }
            return false;
        }
        public bool VIDEO_StopPlayBackSave()
        {
            if (m_lPlayHandle > 0)
            {
                if (DHClient.DHStopSaveRealData(m_lPlayHandle))
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
                Log("该磁盘不存在!");
                return false;
            }


            if (Directory.Exists(path) == false)
            {
                // MessageBox.Show("该目录不存在，自动创建‘" + textBox1.Text + "\\’" + "目录");
                Directory.CreateDirectory(path);
            }

            string sPicFileName = String.Format("{0}{1}_{2}_{3}.bmp", path, m_playbackArgs.Ip, m_playbackArgs.DvrCh, time);

            if (m_lPlayHandle > 0)
            {
                if (DHClient.DHCapturePicture(m_lPlayHandle, sPicFileName))
                {
                    //pubFun.ShowFileDirectory(sPicFileName);
                    return true;
                }
                else
                    Log("图像拍照失败！");

            }

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
            loginID = GetLogID();
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

            NET_RECORDFILE_INFO fileInfo = new NET_RECORDFILE_INFO();
            foreach (DvrFindData f in FindDataList)
            {
                if (f.FileName == sDVRFileName)
                {
                    fileInfo = f.RecordFile;
                    break;
                }
            }

            m_lDownloadHanle = DHClient.DHDownloadByRecordFile(loginID, fileInfo, sFileName, downLoadFun, IntPtr.Zero);

            return (m_lDownloadHanle == 0) ? false : true;
        }
        public bool VIDEO_GetFileByTime(DateTime dtStar, DateTime dtEnd)
        {
            VideoPlaybackArgs e = m_playbackArgs;
            if (e.Ip == "0.0.0.0")
            {
                Log("视频按时间回放失败，没有正确设置播放参数！");
                return false;
            }
            loginID = GetLogID();
            if (loginID == -1)
            {
                Log("视频按时间回放失败，没有正确登录DVR！");
                return false;
            }
            //DHNetSDK.NET_TIME StartTime = new HCNetSDK.NET_DVR_TIME();
            //DHNetSDK.NET_TIME StopTime = new HCNetSDK.NET_DVR_TIME();

            //StartTime.dwYear = (uint)dtStar.Year;
            //StartTime.dwMonth = (uint)dtStar.Month;
            //StartTime.dwDay = (uint)dtStar.Day;
            //StartTime.dwHour = (uint)dtStar.Hour;
            //StartTime.dwMinute = (uint)dtStar.Minute;
            //StartTime.dwSecond = (uint)dtStar.Second;
            //StopTime.dwYear = (uint)dtEnd.Year;
            //StopTime.dwMonth = (uint)dtEnd.Month;
            //StopTime.dwDay = (uint)dtEnd.Day;
            //StopTime.dwHour = (uint)dtEnd.Hour;
            //StopTime.dwMinute = (uint)dtEnd.Minute;
            //StopTime.dwSecond = (uint)dtEnd.Second;

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

            m_lDownloadHanle = DHClient.DHDownloadByTime(loginID, e.DvrCh - 1, (int)RECORD_FILE_TYPE.ALLRECORDFILE, dtStar, dtEnd, sFileName, downLoadFun, IntPtr.Zero);
            //if (m_lDownloadHanle > -1)
            //{
            //    uint nil;
            //    //DVR_PlayBackControl(strConst.NET_DVR_PLAYSTART, 0, out nil);
            //}

            return (m_lDownloadHanle == 0) ? false : true;
        }
        public bool VIDEO_StopGetFile()
        {
            if (m_lDownloadHanle > 0)
            {
                if (DHClient.DHStopDownload(m_lDownloadHanle))
                {
                    m_lDownloadHanle = 0;
                    return true;
                }
            }
            return false;
        }
        public int VIDEO_GetDownloadPos()
        {
            /* int pos = 0;
             if (m_lDownloadHanle > 0)
             {
                 int totalSize, downSize;
                 DHClient.DHGetDownloadPos(m_lDownloadHanle, out totalSize, out downSize);

                 if (totalSize != 0)
                 {
                     pos = (int)(((double)downSize / (double)totalSize) * 100);
                 }
                 Debug.WriteLine("VIDEO_GetDownloadPos:" + pos);

             }
             return pos;*/
            return iDownLoadPos;
        }
        public bool VIDEO_PlayBackControl(uint dwControlCode, uint dwInValue, out uint lpOutValue)
        {
            lpOutValue = 0;

            PLAY_CONTROL cmd = PLAY_CONTROL.Play;
            switch (dwControlCode)
            {
                case HCNetSDK.NET_DVR_PLAYSTART:
                    cmd = PLAY_CONTROL.Play;
                    break;
                case HCNetSDK.NET_DVR_PLAYSTOP:
                    cmd = PLAY_CONTROL.Stop;
                    break;
                case HCNetSDK.NET_DVR_PLAYPAUSE:
                    cmd = PLAY_CONTROL.Pause;
                    break;
                case HCNetSDK.NET_DVR_PLAYRESTART:
                    cmd = PLAY_CONTROL.Play;
                    break;
                case HCNetSDK.NET_DVR_PLAYFAST:
                    cmd = PLAY_CONTROL.Fast;
                    break;
                case HCNetSDK.NET_DVR_PLAYSLOW:
                    cmd = PLAY_CONTROL.Slow;
                    break;
                case HCNetSDK.NET_DVR_PLAYNORMAL:
                    cmd = PLAY_CONTROL.Normal;
                    break;
                case HCNetSDK.NET_DVR_PLAYFRAME:
                    cmd = PLAY_CONTROL.StepPlay;
                    break;
                case HCNetSDK.NET_DVR_PLAYSETPOS:
                    {
                        if (playbackMode == PlaybackModeEnum.按文件)
                            cmd = PLAY_CONTROL.SeekByBit;
                        else
                            cmd = PLAY_CONTROL.SeekByTime;
                    }
                    break;
                case HCNetSDK.NET_DVR_PLAYGETPOS:
                    lpOutValue = (uint)iPlaybackPos;
                    return true;
                case HCNetSDK.NET_DVR_PLAYGETTIME:
                    {
                        int dwTotal = 0;
                        int dwSize = 0;
                        bool blnGetPosSucced = DHClient.DHGetDownloadPos(m_lDownloadHanle, out dwTotal, out dwSize);
                        if (blnGetPosSucced)
                        {
                            lpOutValue = (uint)(((double)dwSize / (double)dwTotal) * 100);
                        }
                        else
                        {
                            // MessageBox.Show(DHClient.LastOperationInfo.ToString(pErrInfoFormatStyle), pMsgTitle);
                        }
                        return true;
                    }

                default:
                    return false;
            }

            if (cmd == PLAY_CONTROL.SeekByBit)
            {
                if (m_lPlayHandle > 0)
                {
                    DHClient.DHPlayBackControl(m_lPlayHandle, PLAY_CONTROL.StepStop);
                    return DHClient.DHPlayBackControl(m_lPlayHandle, cmd, (uint)(dwInValue * (TotalSize / 100)));
                }
                else
                    return false;
            }
            else if (cmd == PLAY_CONTROL.SeekByTime)
            {
                if (m_lPlayHandle > 0)
                {
                    DHClient.DHPlayBackControl(m_lPlayHandle, PLAY_CONTROL.StepStop);
                    return DHClient.DHPlayBackControl(m_lPlayHandle, cmd, (uint)(dwInValue * ((dtStartTime.TimeOfDay.TotalSeconds - dtEndTime.TimeOfDay.TotalSeconds) / 100)));
                }
                else
                    return false;
            }
            else
            {
                if (m_lPlayHandle > 0)
                    return DHClient.DHPlayBackControl(m_lPlayHandle, cmd);
                else
                    return false;

            }
        }
        public bool VIDEO_StopPlayBack()
        {
            if (m_lPlayHandle > 0)
                return DHClient.DHPlayBackControl(m_lPlayHandle, PLAY_CONTROL.Stop);
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
    }
}
