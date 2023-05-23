using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using GHIBMS.DvrSDK;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using GHIBMS.Common;
using SSNetSdk;
using SSNetSdk.PLAYBACK;
using System.Diagnostics;

namespace GHIBMS.VideoPlayback
{
    public class VIKORPlayback : IVideoPlayback
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
        ~VIKORPlayback()
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
        private static IClient client = null;
        private IPlayback backplayer = null;
        public static bool hasInit = false;
        public static Hashtable logidTable = new Hashtable();
        private static UInt16 videoWndNubs = 1;
        //private static Thread aliveThread = null;
        private static System.Threading.Timer aliveTimer;
        private VideoPlaybackArgs m_playbackArgs = new VideoPlaybackArgs();
        private object synObject = new object();
        private string protocolCode = "JK_PLAT_VIKOR";
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
            //创建IClient实例
            if (client == null)
            {
                client = ObjectFactory.CreateClient();
                //保持心跳
                if (aliveTimer == null)
                {
                    aliveTimer = new System.Threading.Timer(delegate(object state)
                    {
                        try
                        {
                            if (client != null)
                                client.KeepAlive();
                        }
                        catch { }
                    }, aliveTimer, 2000, 25000);
                }
                //保持心跳
                /*if (aliveThread == null)
                {
                    aliveThread = new Thread(new ThreadStart(delegate()
                    {
                        while (true)
                        {
                            if (client != null)
                                 client.KeepAlive();
                            Thread.Sleep(20000);
                        }

                    }));
                    aliveThread.IsBackground = true;
                    aliveThread.Start();
                }*/
            }
            return true;
        }
        //清理资源
        public  void DVR_Cleanup()
        {
            DVR_Logout();
            if (client != null)
                client.Logout();
            
        }
     


        #endregion

        #region 设备注册
        private bool DVR_Login()
        {
            try
            {
                VIDEO_Init();
                VideoPlaybackArgs e = m_playbackArgs;
                if (e.Ip == "0.0.0.0" || e.Ip == "")
                {
                    SentMessage("实时视频播放失败，没有正确设置播放参数！");
                    return false;
                }

                if (!logidTable.ContainsKey(e.Ip))
                {
                    logidTable.Clear();
                    bool b=   client.Login(e.Ip, e.Port, e.UserName, e.Password);
                    if (b)
                        logidTable.Add(e.Ip, client);
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            return true;
        }
      
        public  bool DVR_Logout()
        {
            if (client != null)
                client.Logout();
            return true;
        }
        #endregion

        #region 查找文件

        public bool VIDEO_FindFile(uint dwFileType, byte isLock, DateTime dtStar, DateTime dtEnd)
        {

            return false;
        }

        private void ThreadSearchFile()
        {
               
        }
        private void FileSearch()
        {
            if (OnFileSearch != null)
            {
            }
        }
        public bool VIDEO_FindClose()
        {
            return true;
        }
        public bool VIDEO_PlayBackByName(string sPlayBackFileName, DateTime dtStar)
        {
             return true;
        }
        public bool VIDEO_PlayBackByTime(DateTime dtStar, DateTime dtEnd)
        {
            DVR_Login();
            if (backplayer == null)
            {
                backplayer = client.CreatePlayBack();
                //注册播放事件 接收视频播放时的信息 如异常信息
                backplayer.PlaybackEvent -= PlayBackHandler;
                backplayer.PlaybackEvent += PlayBackHandler;
            }
            else
                backplayer.Stop();
            //backplayer.Play(m_playbackArgs.CamID, m_playbackArgs.PlayWnd, dtStar, dtEnd);
            m_playbackArgs.StartTime = dtStar;
            m_playbackArgs.EndTime = dtEnd;
            return true;
        }
        void PlayBackHandler(PlaybackEventArgs args)
        {
                switch (args.Flag)
                {
                    case PlaybackEventz.Started:
                       
                        //TimeSpan dts = backplayer.PlayTimeTo.Subtract(backplayer.PlayTimeFrom);
                        //trackPlayback.Maximum = (int)dts.TotalMilliseconds;
                        //trackPlayback.Minimum = 0;
                        //string s1 = string.Format("视频回放开始，{0}/{1}", new TimeSpan(0).ToString(), dts.ToString());
                       string s1 = string.Format("视频回放,开始");
                        SentMessage(s1);    
                    //playPostionTimer.Start();
                        //MsgAlert(string.Format("速度:{0}", this.playBack.Speed));
                        break;
                    case PlaybackEventz.Disconnected:
                      
                        SentMessage("视频回放，设备连接失败");
                        //playPostionTimer.Stop();
                       
                        return;
                    case PlaybackEventz.Stopped:
                        SentMessage("视频回放，停止");
                        //playPostionTimer.Stop();
                        return;
                    case PlaybackEventz.NotFound:
                        SentMessage("视频回放，指定时间内没有视频");
                        //playPostionTimer.Stop();
                        return;
                    case PlaybackEventz.Paused:
                        SentMessage("视频回放，暂停");
                        //playPostionTimer.Stop();
                        break;
                    case PlaybackEventz.SpeedChanged:
                        SentMessage(String.Format("视频回放，速度:{0}", backplayer.Speed));
                        break;
                    case PlaybackEventz.Postion:
                       // playPostionTimer_Tick(null, null);
                        break;
                }
        }
        public bool VIDEO_PlayBackSaveData()
        {
            
            return false;
        }
        public bool VIDEO_StopPlayBackSave()
        {
           
            return false;
        }
        public bool VIDEO_PlayBackCaptureFile()
        {
          
            return false;
        }
        public bool VIDEO_GetFileByName(string sDVRFileName)
        {
           
            return true;
        }
        public bool VIDEO_GetFileByTime(DateTime dtStar, DateTime dtEnd)
        {
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
            if (backplayer != null)
            {
                backplayer.DownloadEvent -= DownLoadEventHandler;
                backplayer.DownloadEvent += DownLoadEventHandler;
                backplayer.Download(m_playbackArgs.CamID, dtStar, dtEnd, sFileName);
            }


            return true;
        }
        void DownLoadEventHandler(DownloadEventArgs args)
        {
   
            switch (args.Flag)
            {
                case DownloadEventz.Complete:
                    SentMessage("下载完成");
                    break;
                case DownloadEventz.Disconnected:
                    break;
                case DownloadEventz.Error:
                    SentMessage("下载失败");
                    break;
                case DownloadEventz.NotFound:
                    SentMessage("未查询到记录");
                    break;
                case DownloadEventz.Progress:
                    SentMessage(string.Format("下载进度：{0}%", backplayer.Progress));
                    break;
                case DownloadEventz.Started:
                    break;
            }
        }
        public bool VIDEO_StopGetFile()
        {
            backplayer.Stop();
            return true;
        }
        private int downPos = 0;
        public int VIDEO_GetDownloadPos()
        {
            if (backplayer != null)
                downPos = backplayer.Progress;
            return downPos;

        }
        public bool VIDEO_PlayBackControl(uint dwControlCode, uint dwInValue, out uint lpOutValue)
        {
            lpOutValue = 0;
            switch (dwControlCode)
            {
                case strConst.NET_DVR_PLAYFAST:
                    if (backplayer != null)
                        backplayer.Faster();
                    break;
                case strConst.NET_DVR_PLAYSLOW:
                    if (backplayer != null)
                        backplayer.Slower();
                    break;
                case strConst.NET_DVR_PLAYNORMAL:
                    if (backplayer != null)
                        backplayer.NormalSpeed();
                    break;
                case strConst.NET_DVR_PLAYPAUSE:
                    if (backplayer != null)
                        backplayer.Pause();
                    break;
                case strConst.NET_DVR_PLAYSTOP:
                    if (backplayer != null)
                        backplayer.Stop();
                    break;
                case strConst.NET_DVR_PLAYSTART:
                    {
                        if (backplayer != null)
                        {
                            backplayer.Play(m_playbackArgs.CamID, m_playbackArgs.PlayWnd, m_playbackArgs.StartTime, m_playbackArgs.EndTime);

                        }
                    }
                    break;
                case strConst.NET_DVR_PLAYGETTIME:
                    {
                        if (backplayer != null)
                        {
                            lpOutValue = (uint)backplayer.Progress;
                            downPos = backplayer.Progress;
                        }

                    }
                    break;
            }

           
            return true;
        }
        public bool VIDEO_StopPlayBack()
        {
            if (backplayer != null)
                backplayer.Stop();
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
