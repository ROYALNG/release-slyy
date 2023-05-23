using GHIBMS.DvrSDK;
using GHIBMS.INFDVRSDK;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace GHIBMS.VideoPlayback
{
    public class INFDvrPlayback : IVideoPlayback
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
        ~INFDvrPlayback()
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

        public static Hashtable logidTable = new Hashtable();
        private static UInt16 videoWndNubs = 1;
        private bool m_bSearching = false;
        private int m_lFileHandle = -1;
        private int m_lPlayHandle = -1;
        private int m_lDownloadHanle = -1;
        private int loginID = -1;

        private INF_DVRNETSDK.INF_NET_FILECOND m_struFileCond = new INF_DVRNETSDK.INF_NET_FILECOND();
        private VideoPlaybackArgs m_playbackArgs = new VideoPlaybackArgs();
        private Thread Thread_SearchFile;

        private string protocolCode = "JK_DVR_INF";
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
            args.DvrCh--;
            m_playbackArgs.Clone(args);
        }
        public bool VIDEO_Init()
        {
            //MessageBox.Show("海康初始化成功");
            if (!INF_DVRNETSDK.bInt)
            {
                INF_DVRNETSDK.bInt = true;
                if (INF_DVRNETSDK.INF_NET_DVR_Init() == 0)
                {
                    Log("海康DVR SDK初始化失败！");
                    return false;
                }
            }
            return true;
        }
        //清理资源
        public void DVR_Cleanup()
        {
            DVR_Logout();
            if (INF_DVRNETSDK.bInt)
            {
                INF_DVRNETSDK.bInt = false;
                INF_DVRNETSDK.INF_NET_DVR_Cleanup();
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
            if (logidTable.ContainsKey(e.Ip) && int.Parse(logidTable[e.Ip].ToString()) == -1)
            {
                logidTable.Remove(e.Ip);
            }
            if (!logidTable.ContainsKey(e.Ip))
            {

                //INF_NET_DVR_DEVICEINFO_V30 lpDeviceInfo = new INF_NET_DVR_DEVICEINFO_V30();
                //int userId = INF_DVRNETSDK.INF_NET_DVR_Login_V30(e.DvrIp, e.DvrPort, e.UserName, e.Password, out lpDeviceInfo);
                //if (userId > -1)
                //{
                //    logidTable.Add(e.DvrIp, userId);
                //    string msg = String.Format("DVR登录成功！IP：{0} 返回的用户ID:{1}", e.DvrIp, userId);
                //    Log(msg);
                //    return true;
                //}
                //else
                //{
                //    Log("DVR登录失败！ IP:" + e.DvrIp);
                //    return false;
                //}
                int nDeviceType = INF_DVRNETSDK.INF_NET_DVR_CheckDeviceType(e.Ip, 5000, e.UserName, e.Password);
                if (nDeviceType < 0 || nDeviceType > 1)
                {
                    string msg = string.Format("DVR设备类型失败!IP:{0}。", e.Ip);
                    Log(msg);
                    return false;
                }

                //设备用户信息获得
                INF_DVRNETSDK.INF_NET_DVR_LONGININFO lpLoginInfo = new INF_DVRNETSDK.INF_NET_DVR_LONGININFO();
                lpLoginInfo.sDevIp = e.Ip;
                lpLoginInfo.sUserName = e.UserName;
                lpLoginInfo.sAuthKey = e.Password;
                if (nDeviceType == 0)
                    lpLoginInfo.wServerPort = 0;
                else
                    lpLoginInfo.wServerPort = (ushort)e.Port;
                lpLoginInfo.sPrivKey = "";
                lpLoginInfo.wDeviceType = (ushort)nDeviceType;

                //设备用户登录  
                int userId = INF_DVRNETSDK.INF_NET_DVR_Login_V20(ref lpLoginInfo);
                if (userId > -1)
                {
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
                if (id > -1)
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
                if (id > -1)
                    return id;
            }
            return -1;
        }
        public static bool DVR_Logout()
        {
            foreach (DictionaryEntry de in logidTable)
            {
                int logid = int.Parse(de.Value.ToString());
                string ip = de.Key.ToString();
                if (logid > -1)
                {
                    if (INF_DVRNETSDK.INF_NET_DVR_Logout(logid) == 0)
                    {

                    }
                }

            }
            logidTable.Clear();
            return true;
        }
        #endregion

        #region 查找文件

        public bool VIDEO_FindFile(uint dwFileType, byte isLock, DateTime dtStar, DateTime dtEnd)
        {

            VideoPlaybackArgs e = m_playbackArgs;
            if (e.Ip == "0.0.0.0")
            {
                Log("视频回放失败，没有正确设置播放参数！");
                return false;
            }
            loginID = GetLogID();
            if (loginID == -1)
            {
                Log("视频回放失败，没有正确登录DVR！");
                return false;
            }
            if (!m_bSearching)
            {

                INF_DVRNETSDK.INF_NET_TIME StartTime = new INF_DVRNETSDK.INF_NET_TIME();
                INF_DVRNETSDK.INF_NET_TIME StopTime = new INF_DVRNETSDK.INF_NET_TIME();

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

                byte[] sCardNumber = new byte[32];
                m_struFileCond.lChannel = e.DvrCh;
                m_struFileCond.dwFileType = dwFileType;

                m_struFileCond.struStartTime = StartTime;
                m_struFileCond.struStopTime = StopTime;
                FindDataList.Clear();

                m_lFileHandle = INF_DVRNETSDK.INF_NET_DVR_FindFile(loginID, ref m_struFileCond);
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
            return false;
        }

        private void ThreadSearchFile()
        {

            INF_DVRNETSDK.INF_NET_FINDDATA struFileInfo = new INF_DVRNETSDK.INF_NET_FINDDATA();
            while (true)
            {
                int lRet = INF_DVRNETSDK.INF_NET_DVR_FindFileNext(m_lFileHandle, ref struFileInfo);
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
                        INF_DVRNETSDK.INF_NET_DVR_FindClose(m_lFileHandle);
                        //获取文件结束
                        if (OnFileSearch != null)
                            OnFileSearch(this, strConst.NET_DVR_NOMOREFILE, FindDataList);
                        m_bSearching = false;
                        break;

                    }
                    else
                    {
                        INF_DVRNETSDK.INF_NET_DVR_FindClose(m_lFileHandle);
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
        public bool VIDEO_FindClose()
        {
            //FindDataList.Clear();
            if (OnFileSearch != null)
                OnFileSearch(this, strConst.NET_DVR_FILE_EXCEPTION, FindDataList);

            INF_DVRNETSDK.INF_NET_DVR_FindClose(m_lFileHandle);
            m_lFileHandle = -1;
            m_bSearching = false;
            try
            {
                if (Thread_SearchFile != null && Thread_SearchFile.IsAlive)
                {
                    Thread_SearchFile.Abort();
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
            loginID = GetLogID();
            if (loginID == -1)
            {
                Log("视频按文件回放失败，没有正确登录DVR！");
                return false;
            }
            m_lPlayHandle = INF_DVRNETSDK.INF_NET_DVR_PlayBackByName(loginID, sPlayBackFileName, e.PlayWnd);

            if (m_lPlayHandle == -1)
                return false;
            else return true;
        }
        public bool VIDEO_PlayBackByTime(DateTime dtStar, DateTime dtEnd)
        {
            /*
            VideoPlaybackArgs e = m_playbackArgs;
            if (e.DvrIp == "0.0.0.0")
            {
                Log("视频按时间回放失败，没有正确设置播放参数！");
                return false;
            }
            int logID = GetLogID();
            if (logID == -1)
            {
                Log("视频按时间回放失败，没有正确登录DVR！");
                return false;
            }
            INF_DVRNETSDK.INF_NET_TIME StartTime ;
            INF_DVRNETSDK.INF_NET_TIME StopTime ;
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

            m_lPlayHandle = INF_DVRNETSDK.INF_NET_DVR_PlayBackByTime(logID, e.DvrCh,  StartTime,  StopTime, e.PlayWnd);
          
            if (m_lPlayHandle == -1) return false; 
            uint nil;
            INF_DVRNETSDK.INF_NET_DVR_PlayBackControl(m_lPlayHandle, strConst.NET_DVR_PLAYSTART, 0, out nil);
            if (m_lPlayHandle == -1)
                return false;
            else */
            return true;
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



                if (INF_DVRNETSDK.INF_NET_DVR_PlayBackSaveData(m_lPlayHandle, sFileName) != 0)
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
                if (INF_DVRNETSDK.INF_NET_DVR_StopPlayBackSave(m_lPlayHandle) != 0)
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
            string sPicFileName = String.Format("{0}{1}_{2}_{3}.", path, m_playbackArgs.Ip, m_playbackArgs.DvrCh, time);

            if (m_lPlayHandle > -1)
            {
                if (INF_DVRNETSDK.INF_NET_DVR_PlayBackCaptureFile(m_lPlayHandle, sPicFileName) != 0)
                    return true;
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

            m_lDownloadHanle = INF_DVRNETSDK.INF_NET_DVR_GetFileByName(loginID, sDVRFileName, sFileName);
            if (m_lDownloadHanle > -1)
            {
                uint nil;
                INF_DVRNETSDK.INF_NET_DVR_DownloadControl(m_lDownloadHanle, strConst.NET_DVR_PLAYSTART, 0, out nil);
            }
            return (m_lDownloadHanle == -1) ? false : true;
        }
        public bool VIDEO_GetFileByTime(DateTime dtStar, DateTime dtEnd)
        {
            return true;
            /* VideoPlaybackArgs e = m_playbackArgs;
             if (e.DvrIp == "0.0.0.0")
             {
                 Log("视频按时间回放失败，没有正确设置播放参数！");
                 return false;
             }
             int logID = GetLogID();
             if (logID == -1)
             {
                 Log("视频按时间回放失败，没有正确登录DVR！");
                 return false;
             }
             INF_DVRNETSDK.INF_NET_TIME StartTime = new INF_DVRNETSDK.INF_NET_TIME();
             INF_DVRNETSDK.INF_NET_TIME StopTime = new INF_DVRNETSDK.INF_NET_TIME();

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

             string sFileName = String.Format("{0}{1}_{2}_{3}.mp4", path, m_playbackArgs.DvrIp, m_playbackArgs.DvrCh, time);

             m_lDownloadHanle = INF_DVRNETSDK.INF_NET_DVR_GetFileByTime(logID, e.DvrCh, ref StartTime, ref StopTime, sFileName);
             if (m_lDownloadHanle > -1)
             {
                 uint nil;
                 INF_DVRNETSDK.INF_NET_DVR_PlayBackControl(m_lDownloadHanle, strConst.NET_DVR_PLAYSTART, 0, out nil);
             }

             return (m_lDownloadHanle == -1) ? false : true;*/
        }
        public bool VIDEO_StopGetFile()
        {
            if (m_lDownloadHanle > -1)
            {
                if (INF_DVRNETSDK.INF_NET_DVR_StopGetFile(m_lDownloadHanle) != 0)
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
                pos = INF_DVRNETSDK.INF_NET_DVR_GetDownloadPos(m_lDownloadHanle);
            return pos;

        }
        public bool VIDEO_PlayBackControl(uint dwControlCode, uint dwInValue, out uint lpOutValue)
        {
            lpOutValue = 0;
            if (m_lPlayHandle > -1)
                return INF_DVRNETSDK.INF_NET_DVR_PlayBackControl(m_lPlayHandle, dwControlCode, (uint)dwInValue, out lpOutValue) == 0 ? false : true;
            else
                return false;
        }
        public bool VIDEO_StopPlayBack()
        {
            if (m_lPlayHandle > -1)
                return INF_DVRNETSDK.INF_NET_DVR_StopPlayBack(m_lPlayHandle) == 0 ? false : true;
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
    }
}
