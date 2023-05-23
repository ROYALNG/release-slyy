using DevComponents.DotNetBar;
using GHIBMS.Common;
using GHIBMS.DvrSDK;
using justin.time.axis;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GHIBMS.VideoPlayback
{
    /// <summary>
    ///等同于IMOS XP_PLAY_STATUS_E
    /// </summary>
    public enum PLAY_SPEED
    {
        PLAY_SPEED_16_BACKWARD = 0,     /**< 16倍速后退播放 */
        PLAY_SPEED_8_BACKWARD = 1,      /**< 8倍速后退播放 */
        PLAY_SPEED_4_BACKWARD = 2,      /**< 4倍速后退播放 */
        PLAY_SPEED_2_BACKWARD = 3,      /**< 2倍速后退播放 */
        PLAY_SPEED_1_BACKWARD = 4,      /**< 正常速度后退播放 */
        PLAY_SPEED_HALF_BACKWARD = 5,   /**< 1/2倍速后退播放 */
        PLAY_SPEED_QUARTER_BACKWARD = 6,/**< 1/4倍速后退播放 */
        PLAY_SPEED_QUARTER_FORWARD = 7, /**< 1/4倍速播放 */
        PLAY_SPEED_HALF_FORWARD = 8,    /**< 1/2倍速播放 */
        PLAY_SPEED_1_FORWARD = 9,       /**< 正常速度前进播放 */
        PLAY_SPEED_2_FORWARD = 10,      /**< 2倍速前进播放 */
        PLAY_SPEED_4_FORWARD = 11,      /**< 4倍速前进播放 */
        PLAY_SPEED_8_FORWARD = 12,      /**< 8倍速前进播放 */
        PLAY_SPEED_16_FORWARD = 13      /**< 16倍速前进播放 */
    }
    enum PlayStatus
    {
        Forward_Play,
        Backward_Play
    }
    public partial class VideoPlaybackControl : UserControl
    {
        public delegate void OnMessagedelegate(object sender, string msg);
        public event OnMessagedelegate OnMessage;
        public VideoPlaybackControl()
        {
            InitializeComponent();
            // Set the value of the double-buffering style bits to true.
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
               ControlStyles.UserPaint |
               ControlStyles.AllPaintingInWmPaint,
               true);
            this.UpdateStyles();
            InstanceNumbs++;
            cmbProtocol.KeyPress += NoKey_KeyPress;
            txtDvrPort.KeyPress += KeyPress_NumInput;
            txtDvrCH.KeyPress += KeyPress_NumInput;
            //cmbProtocol.DataSource = System.Enum.GetNames(typeof(string));
            //string[] item = System.Enum.GetNames(typeof(string));
            //foreach (string s in item)
            //{
            //    cmbProtocol.Items.Add(s);
            //}
            initExPanelVideoSource();
            timeline1.TimeItemClicked += new Timeline.TimeItemEventHandler(timeline1_TimeItemClicked);
            downloadPath = ClientConfig.DownPath;
            picPath = ClientConfig.PicPath;
            recPath = ClientConfig.RecPath;
        }

        #region 变量   
        public static int InstanceNumbs = 0;
        private VideoPlaybackArgs m_playbackArgs = new VideoPlaybackArgs();
        private IVideoPlayback VodPlayer;
        private bool bSoundOpened = false;
        private bool bRecording = false;
        private bool bPause = false;
        private uint m_TotalTime = 0;
        private uint uPlaySpeed = (uint)PLAY_SPEED.PLAY_SPEED_1_FORWARD;
        private PlayStatus playStauts = PlayStatus.Forward_Play;
        #endregion

        #region 属性
        private bool bDownloading = false;
        public bool BDowloading
        {
            get { return bDownloading; }
        }
        private bool bSearching = false;
        public bool BSearching
        {
            get { return bSearching; }
            set
            {
                bSearching = value;
            }
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
        public VideoPlaybackArgs CurrentPlayArgs
        {
            get { return m_playbackArgs; }
        }

        /// <summary>
        /// 当前选中的播放器ID号
        /// </summary>
        private static int selectedWndId = 1;
        public static int SelectedWndId
        {
            set { selectedWndId = value; }
            get { return selectedWndId; }
        }
        /// <summary>
        /// 抓拍照片保存的路径
        /// </summary>
        private static string picPath = "c:\\";
        public static string PicPath
        {
            set { picPath = value; }
            get { return picPath; }
        }
        /// <summary>
        /// 录像保存的路径
        /// </summary>
        private static string recPath = "c:\\";
        public static string RecPath
        {
            set { recPath = value; }
            get { return recPath; }
        }
        /// <summary>
        /// 录像下载的路径
        /// </summary>
        private static string downloadPath = "c:\\";
        public static string DownloadPath
        {
            set { downloadPath = value; }
            get { return downloadPath; }
        }



        private static PicQualityEnum picQuality = PicQualityEnum.较好;
        public static PicQualityEnum PicQuality
        {
            set { picQuality = value; }
            get { return picQuality; }
        }
        private static PicSizeEnum picSize = PicSizeEnum.VGA;
        public static PicSizeEnum PicSize
        {
            set { picSize = value; }
            get { return picSize; }
        }

        private VIDEO_PLAY_STATE workStatus = VIDEO_PLAY_STATE.State_Close;
        /// <summary>
        /// 播放器运行状态 0 停止 1 播放 2 录像
        /// </summary>
        public VIDEO_PLAY_STATE WorkStatus
        {
            get { return workStatus; }
        }
        //播放参数
        private PlaybackModeEnum playbackMode = PlaybackModeEnum.按文件;
        public PlaybackModeEnum PlaybackMode
        {
            set
            {
                switch (value)
                {
                    case PlaybackModeEnum.按文件:
                        colorSliderPos.Visible = true;
                        break;
                    case PlaybackModeEnum.按时间:
                        colorSliderPos.Visible = true;
                        break;
                    default:
                        colorSliderPos.Visible = false;
                        break;
                }
                playbackMode = value;
            }
            get { return playbackMode; }
        }
        private string playFileName = "";
        public string PlayFileName
        {
            get { return playFileName; }
            set
            {
                if (playFileName == value)
                    return;
                else
                {
                    playFileName = value;
                    StopPlay();
                    SetOpenStatus();
                }
            }
        }
        private string saveFileName = "";
        public string SaveFileName
        {
            get { return saveFileName; }
            set { saveFileName = value; }
        }
        private DateTime startTime = DateTime.Now;
        public DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }
        private DateTime endTime = DateTime.Now;
        public DateTime EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }
        private uint fileType = 0xFF;
        public uint FileType
        {
            get { return fileType; }
            set { fileType = value; }
        }
        private bool bFullPanel = false;
        public bool FullPanel
        {
            set
            {
                if (value)
                    btnFullScreen.Image = Properties.Resources.smallscreen;
                else
                    btnFullScreen.Image = Properties.Resources.fullscreen;
                bFullPanel = value;

            }
            get { return bFullPanel; }

        }

        #endregion

        #region 事件
        //log事件

        private void SentMessage(string message)
        {
            if (OnMessage != null)
                OnMessage(this, message);
        }
        private delegate void OnMessageDelegate(object sender, string message);
        private void OnMessageHandle(object sender, string message)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new OnMessagedelegate(OnMessageHandle), new object[] { sender, message });
            }
            else
            {
                if (message == "播放完成")
                {
                    if (VodPlayer != null)
                        VodPlayer.VIDEO_StopPlayBack();
                }
                SentMessage(message);
            }
        }
        public delegate void OnSelectedWnddelegate(object sender, EventArgs e);
        public event OnSelectedWnddelegate OnSelectedWnd;
        public delegate void OnFullPaneldelegate(object sender, bool bFull);
        public event OnFullPaneldelegate OnFullPanel;
        public delegate void OnFileSearchdelegate(object sender, EventArgs e);
        public event OnFileSearchdelegate OnFileSearch;

        public delegate void OnTimeItemClickeddelegate(object sender, Timeline.TimeItemEventArgs e);
        public event OnTimeItemClickeddelegate OnTimeItemClicked;

        #endregion

        #region 公共方法
        private IntPtr mainHandle;
        public void SetWinMessage(IntPtr lWinHandle)
        {
            mainHandle = lWinHandle;

        }
        public void OnWinMessage(ref System.Windows.Forms.Message m)
        {

            if (VodPlayer != null)
            {
                // Debug.WriteLine(this.Name + "  OnWinMessage " + RealPlayer.ToString());
                //自定义消息处理函数
                VodPlayer.OnWinMessage(ref m);
            }
            else
            {
                //Debug.WriteLine(this.Name + "  OnWinMessage  RealPlayer=Null");

            }
        }
        public bool OpenVideo(VideoPlaybackArgs args)
        {
            //test-----------------------------------------
            //args.DvrIp = "202.197.111.161";
            ////args.DvrIp = "192.168.88.11";
            //args.DvrCh = 2;
            //args.DvrPort = 8000;
            //args.UserName = "admin";
            //args.Password = "12345";

            //args.VodMode = TransferTypeEnum.DVR直播;
            //args.PlayWnd = panelPlayer.Handle;
            //----------------------------------------test//

            if (args.Ip == "0.0.0.0" || args.Ip == "")
            {
                string msg = String.Format("打开视频失败！DVR IP地址格式不正确");
                SentMessage(msg);
                return false;
            }
            //复制参数
            m_playbackArgs.Clone(args);
            m_playbackArgs.PlayWnd = panelPlayer.Handle;
            lblCamInfo.Text = m_playbackArgs.CamName; // m_playbackArgs.DvrIp + " " + m_playbackArgs.DvrCh.ToString("D2");
            //创建对应设备类型的播放器类
            CreatePlayerInstance(args.ProtocolCode);

            if (VodPlayer != null)
            {
                VodPlayer.SetPlayAgrs(m_playbackArgs); //传递播放所需的参数
                VodPlayer.SetWinMessage(mainHandle);
                switch (playbackMode)
                {
                    case PlaybackModeEnum.按时间:
                        SetStopedStatus();
                        break;
                    case PlaybackModeEnum.按文件:
                        SetCloseStatus();
                        break;
                }
            }
            else
            {
                string msg = String.Format("初始化网络播放器实例失败!类型{0}", args.ProtocolCode.ToString());
                SentMessage(msg);
            }
            return true;
        }
        public bool StartPlay()
        {
            uPlaySpeed = 9;
            ShowPlaySpeed(uPlaySpeed);
            lblCamInfo.Text = m_playbackArgs.Ip + " " + m_playbackArgs.DvrCh.ToString("D2");
            //if (workStatus == VIDEO_PLAY_STATE.State_Play)
            StopPlay();

            bool ret = false;
            uint time = 0;
            if (workStatus == VIDEO_PLAY_STATE.State_Pause)
            {
                ret = PlayBackControl(HCNetSDK.NET_DVR_PLAYNORMAL, 0, out time);
                if (ret)
                    SetPlayingStatus();
            }
            else if (workStatus == VIDEO_PLAY_STATE.State_Play)
            {
                ret = PlayBackControl(strConst.NET_DVR_PLAYPAUSE, 0, out time);
                if (ret)
                    SetPauseStatus();
            }
            else if (workStatus == VIDEO_PLAY_STATE.State_Open || workStatus == VIDEO_PLAY_STATE.State_Stop
                || workStatus == VIDEO_PLAY_STATE.State_Close)
            {

                switch (playbackMode)
                {
                    case PlaybackModeEnum.按时间:
                        ret = PlayBackByTime(StartTime, EndTime);
                        lblPlayPos.Visible = false;
                        break;
                    case PlaybackModeEnum.按文件:
                        ret = PlayBackByName(PlayFileName, StartTime);
                        lblPlayPos.Visible = true;
                        break;

                }
                if (ret)
                {
                    ret = PlayBackControl(HCNetSDK.NET_DVR_PLAYSTART, 0, out time);
                    if (ret)
                    {
                        workStatus = VIDEO_PLAY_STATE.State_Play;
                        if (PlayBackControl(strConst.NET_DVR_GETTOTALTIME, 0, out time))
                        {
                            if (time > 0)
                                m_TotalTime = time;
                        }
                        timerGetPlayPos.Enabled = true;
                        //标识文件状态
                        SetPlayingStatus();
                    }

                }
            }

            return ret;
        }
        public bool StopPlay()
        {
            if (workStatus == VIDEO_PLAY_STATE.State_Record)
                StopPlayBackSave();
            if (workStatus == VIDEO_PLAY_STATE.State_Play || workStatus == VIDEO_PLAY_STATE.State_Pause)
            {
                if (VodPlayer != null)
                {
                    if (VodPlayer.VIDEO_StopPlayBack())
                    {
                        SetStopedStatus();
                        panelPlayer.Invalidate();
                        workStatus = VIDEO_PLAY_STATE.State_Close;
                        return true;
                    }
                }
            }
            workStatus = VIDEO_PLAY_STATE.State_Close;
            return true;
        }

        public bool FindFile(uint dwFileType, byte isLock, DateTime dtStar, DateTime dtEnd)
        {

            if (VodPlayer != null)
            {
                picFinding.Visible = true;
                bSearching = true;
                return VodPlayer.VIDEO_FindFile(dwFileType, isLock, dtStar, dtEnd);
            }
            return false;
        }
        public bool FindClose()
        {

            bSearching = false;
            if (VodPlayer != null)
            {
                picFinding.Visible = false;

                if (VodPlayer.VIDEO_FindClose())
                {
                    return true;
                }
            }
            return false;

        }
        public bool PlayBackByName(string sPlayBackFileName, DateTime dtStar)
        {

            if (VodPlayer != null)
            {
                return VodPlayer.VIDEO_PlayBackByName(sPlayBackFileName, dtStar);
            }
            return false;

        }
        public bool PlayBackByTime(DateTime dtStar, DateTime dtEnd)
        {
            if (VodPlayer != null)
            {
                return VodPlayer.VIDEO_PlayBackByTime(dtStar, dtEnd);
            }
            return false;
        }
        public bool PlayBackSaveData()
        {
            if (VodPlayer != null)
            {
                return VodPlayer.VIDEO_PlayBackSaveData();
            }
            return false;

        }
        public bool StopPlayBackSave()
        {
            if (VodPlayer != null)
            {
                if (VodPlayer.VIDEO_StopPlayBackSave())
                {
                    SetPauseStatus();
                    return true;
                }
            }
            return false;
        }
        public bool PlayBackCaptureFile()
        {
            if (VodPlayer != null)
            {
                return VodPlayer.VIDEO_PlayBackCaptureFile();
            }
            return false;

        }
        private bool GetFileByName(string sDVRFileName)
        {
            if (VodPlayer != null)
            {
                return VodPlayer.VIDEO_GetFileByName(sDVRFileName);
            }
            return false;
        }
        private bool GetFileByTime(DateTime dtStar, DateTime dtEnd)
        {
            if (VodPlayer != null)
            {
                return VodPlayer.VIDEO_GetFileByTime(dtStar, dtEnd);
            }
            return false;
        }
        public bool StopGetFile()
        {
            if (VodPlayer != null)
            {
                return VodPlayer.VIDEO_StopGetFile();
            }
            return false;

        }
        public int GetDownloadPos()
        {
            if (VodPlayer != null)
            {
                return VodPlayer.VIDEO_GetDownloadPos();
            }
            return 0;
        }
        public bool PlayBackControl(uint dwControlCode, uint dwInValue, out uint lpOutValue)
        {
            lpOutValue = 0;
            if (VodPlayer != null)
            {
                return VodPlayer.VIDEO_PlayBackControl(dwControlCode, dwInValue, out lpOutValue);
            }
            return false;
        }
        public bool StartDownload()
        {
            bool ret = false;
            if (!bDownloading)
            {
                switch (playbackMode)
                {
                    case PlaybackModeEnum.按时间:
                        ret = GetFileByTime(StartTime, EndTime);

                        break;
                    case PlaybackModeEnum.按文件:
                        ret = GetFileByName(PlayFileName);

                        break;

                }
                if (ret)
                {
                    picDownloading.Visible = true;
                    lblDownLoadPos.Visible = true;
                    btnDownload.Image = Properties.Resources.download_Cancel;
                    bDownloading = true;
                    btnDownload.Visible = true;
                    timerGetDownloadPos.Enabled = true;
                }
            }
            return ret;
        }
        public bool StopDownload()
        {
            bool ret = false;
            if (bDownloading)
            {
                ret = StopGetFile();
                if (ret)
                {
                    picDownloading.Visible = false;
                    lblDownLoadPos.Visible = false;
                    btnDownload.Image = Properties.Resources.download_start;
                    bDownloading = false;
                    timerGetDownloadPos.Enabled = false;
                }
            }
            return ret;
        }
        private bool bDownLoadEnable = false;
        public void DowloadEnable(bool bEnable)
        {
            bDownLoadEnable = bEnable;
        }

        //分类清除所有SDK占用的资源 
        //此除要注意用户登出、清除SDK一定要静态，属于类才可以
        public void DisposeNetVideoSDK()
        {

            if (VodPlayer != null)
            {
                VodPlayer.DVR_Cleanup();
            }

        }

        #endregion

        #region 私有方法
        //生成播放类实例
        private void CreatePlayerInstance(string sdk)
        {
            if (VodPlayer != null)
            {
                if (VodPlayer.ProtocolCode == sdk)
                {
                    return;
                }
                else
                {
                    VodPlayer.Dispose();
                    VodPlayer = null;
                    VodPlayer = CreateNewPlayback(sdk);
                }
            }
            else
            {
                VodPlayer = CreateNewPlayback(sdk);
            }
        }
        private IVideoPlayback CreateNewPlayback(string sdk)
        {
            IVideoPlayback back;
            switch (sdk)
            {
                case "JK_DVR_HK":
                    {
                        if (m_playbackArgs.PlatIpAddress != "" && m_playbackArgs.SerialNumber != "")
                        {
                            HIKPlatPlayback plat = new HIKPlatPlayback();
                            plat.OnFileSearch += new HIKPlatPlayback.OnFileSearchdelegate(DvrOnFileSearch);
                            plat.OnMessage += new HIKPlatPlayback.OnMessagedelegate(OnMessageHandle);
                            back = plat;
                        }
                        else
                        {
                            HikDvrPlayback hik = new HikDvrPlayback();
                            hik.OnFileSearch += new HikDvrPlayback.OnFileSearchdelegate(DvrOnFileSearch);
                            hik.OnMessage += new HikDvrPlayback.OnMessagedelegate(OnMessageHandle);
                            back = hik;
                        }
                    }
                    break;
                case "JK_DVR_DH":
                    DHDvrPlayback dh = new DHDvrPlayback();
                    dh.OnFileSearch += new DHDvrPlayback.OnFileSearchdelegate(DvrOnFileSearch);
                    dh.OnMessage += new DHDvrPlayback.OnMessagedelegate(OnMessageHandle);
                    back = dh;
                    break;
                case "JK_DVR_INF":
                    INFDvrPlayback INF = new INFDvrPlayback();
                    INF.OnFileSearch += new INFDvrPlayback.OnFileSearchdelegate(DvrOnFileSearch);
                    INF.OnMessage += new INFDvrPlayback.OnMessagedelegate(OnMessageHandle);
                    back = INF;
                    break;
                case "JK_DVR_HB":
                    HBDvrPlayback HB = new HBDvrPlayback();
                    HB.OnFileSearch += new HBDvrPlayback.OnFileSearchdelegate(DvrOnFileSearch);
                    HB.OnMessage += new HBDvrPlayback.OnMessagedelegate(OnMessageHandle);
                    back = HB;
                    break;
                //case "JK_PLAT_VIKOR":
                //    VIKORPlayback VIK = new VIKORPlayback();
                //    VIK.OnFileSearch += new VIKORPlayback.OnFileSearchdelegate(DvrOnFileSearch);
                //    VIK.OnMessage+=new VIKORPlayback.OnMessagedelegate(OnMessageHandle);
                //    back = VIK;
                //    break;
                case "JK_PLAT_IMOS":
                    IMOSPlayback IMOS = new IMOSPlayback();
                    IMOS.OnFileSearch += new IMOSPlayback.OnFileSearchdelegate(DvrOnFileSearch);
                    IMOS.OnMessage += new IMOSPlayback.OnMessagedelegate(OnMessageHandle);
                    back = IMOS;
                    break;
                case "JK_DVR_TANDI":
                    TandiPlayback tandi = new TandiPlayback();
                    tandi.OnFileSearch += new TandiPlayback.OnFileSearchdelegate(DvrOnFileSearch);
                    tandi.OnMessage += new TandiPlayback.OnMessagedelegate(OnMessageHandle);
                    back = tandi;
                    break;

                default:
                    {
                        HikDvrPlayback hik = new HikDvrPlayback();
                        hik.OnFileSearch += new HikDvrPlayback.OnFileSearchdelegate(DvrOnFileSearch);
                        hik.OnMessage += new HikDvrPlayback.OnMessagedelegate(OnMessageHandle);
                        back = hik;
                    }
                    break;

            }
            return back;

        }


        //初始化视频源参数
        private void initExPanelVideoSource()
        {
            if (m_playbackArgs.ProtocolCode == null) return;
            //cmbProtocol.Items.Clear();
            cmbProtocol.SelectedIndex = this.cmbProtocol.FindString(m_playbackArgs.ProtocolCode.ToString());
            ipInputDvr.Text = m_playbackArgs.Ip;
            txtDvrPort.Text = m_playbackArgs.Port.ToString();
            txtUserName.Text = m_playbackArgs.UserName;
            txtPassword.Text = m_playbackArgs.Password.ToString();
            ipInputVod.Text = m_playbackArgs.VODIp;
            txtVodPort.Text = m_playbackArgs.VODPort.ToString();
            txtDvrCH.Text = m_playbackArgs.DvrCh.ToString();
        }
        //保存视频源连接参数
        private void SaveExPanelVideoSource()
        {

            VideoPlaybackArgs args = new VideoPlaybackArgs();

            args.ProtocolCode = cmbProtocol.Text;
            args.Ip = ipInputDvr.Text;
            args.Port = ushort.Parse(txtDvrPort.Text);
            args.UserName = txtUserName.Text;
            args.Password = txtPassword.Text;
            args.VODIp = ipInputVod.Text;
            args.VODPort = int.Parse(txtVodPort.Text);
            args.DvrCh = int.Parse(txtDvrCH.Text);
            OpenVideo(args);

        }

        //播放状态
        private void SetPlayingStatus()
        {
            btnPlay.Enabled = true;
            btnStop.Enabled = true;
            btnForwardFast.Enabled = true;
            btnBackwordFast.Enabled = true;
            btnPlayNormal.Enabled = true;
            btnForwardSlow.Enabled = true;
            btnForwardFast.Enabled = true;
            btnBackwardSlow.Enabled = true;
            btnForwardFrame.Enabled = true;

            btnRec.Enabled = true;
            btnDownload.Enabled = bDownLoadEnable;
            btnCapture.Enabled = true;
            btnMute.Enabled = true;
            sliderVolume.Enabled = true;
            colorSliderPos.Enabled = true;
            picRecordShow.Visible = false;
            workStatus = VIDEO_PLAY_STATE.State_Play;
            btnPlay.Image = Properties.Resources.pause_blue;
        }
        //PAUSE状态
        private void SetPauseStatus()
        {
            btnPlay.Enabled = true;
            btnStop.Enabled = true;
            btnForwardFast.Enabled = true;
            btnBackwordFast.Enabled = true;
            btnPlayNormal.Enabled = true;
            btnForwardSlow.Enabled = true;
            btnForwardFast.Enabled = true;
            btnForwardFrame.Enabled = true;
            btnBackwardSlow.Enabled = true;
            btnForwardFrame.Enabled = true;
            btnRec.Enabled = true;
            btnDownload.Enabled = bDownLoadEnable;
            btnCapture.Enabled = true;
            btnMute.Enabled = true;
            sliderVolume.Enabled = true;
            colorSliderPos.Enabled = true;
            picRecordShow.Visible = false;
            workStatus = VIDEO_PLAY_STATE.State_Pause;
            btnPlay.Image = Properties.Resources.play_blue;
        }
        private void SetOpenStatus()
        {
            btnPlay.Enabled = true;
            btnStop.Enabled = false;
            btnForwardFast.Enabled = false;
            btnBackwordFast.Enabled = false;
            btnPlayNormal.Enabled = false;
            btnForwardSlow.Enabled = false;
            btnForwardFrame.Enabled = false;
            btnBackwardSlow.Enabled = false;
            btnRec.Enabled = false;
            btnDownload.Enabled = false;
            btnCapture.Enabled = false;
            btnMute.Enabled = false;
            sliderVolume.Enabled = false;
            colorSliderPos.Enabled = false;
            picRecordShow.Visible = false;
            workStatus = VIDEO_PLAY_STATE.State_Open;
            btnPlay.Image = Properties.Resources.play_blue;
        }
        private void SetCloseStatus()
        {
            btnPlay.Enabled = false;
            btnStop.Enabled = false;
            btnForwardFast.Enabled = false;
            btnBackwordFast.Enabled = false;
            btnPlayNormal.Enabled = false;
            btnForwardSlow.Enabled = false;
            btnForwardFrame.Enabled = false;
            btnBackwardSlow.Enabled = false;
            btnRec.Enabled = false;
            btnDownload.Enabled = false;
            btnCapture.Enabled = false;
            btnMute.Enabled = false;
            sliderVolume.Enabled = false;
            colorSliderPos.Enabled = false;
            picRecordShow.Visible = false;
            workStatus = VIDEO_PLAY_STATE.State_Close;
            btnPlay.Image = Properties.Resources.play_blue;
        }
        private void SetStopedStatus()
        {
            btnPlay.Enabled = true;
            btnStop.Enabled = false;
            btnForwardFast.Enabled = false;
            btnBackwordFast.Enabled = false;
            btnPlayNormal.Enabled = false;
            btnForwardSlow.Enabled = false;
            btnRec.Enabled = false;
            btnDownload.Enabled = false;
            btnCapture.Enabled = false;
            btnMute.Enabled = false;
            sliderVolume.Enabled = false;
            colorSliderPos.Enabled = false;
            picRecordShow.Visible = false;
            workStatus = VIDEO_PLAY_STATE.State_Stop;
            btnPlay.Image = Properties.Resources.play_blue;
        }
        private void SetRecordingStatus()
        {
            picRecordShow.Visible = true;
            workStatus = VIDEO_PLAY_STATE.State_Record;
        }
        private void AppcommandPlayControl_Executed(object sender, EventArgs e)
        {

            uint o;
            ICommandSource source = sender as ICommandSource;
            if (source.CommandParameter is string)
            {
                switch (source.CommandParameter.ToString())
                {
                    case "OPEN":
                        exPanelVideoSource.ExpandButton.Visible = false;
                        exPanelVideoSource.Expanded = true;
                        exPanelVideoSource.Top = (int)((panelPlayer.Height - exPanelVideoSource.Height) / 2);
                        exPanelVideoSource.Left = (int)((panelPlayer.Width - exPanelVideoSource.Width) / 2);
                        exPanelVideoSource.Visible = !exPanelVideoSource.Visible;

                        SaveExPanelVideoSource();
                        break;
                    case "PLAY":
                        StartPlay();
                        playStauts = PlayStatus.Forward_Play;
                        break;
                    case "STOP":
                        StopPlay();
                        break;
                    case "BACKWORDSLOW": //向后慢放
                        playStauts = PlayStatus.Backward_Play;
                        if (uPlaySpeed != 5 && uPlaySpeed != 6 && uPlaySpeed != 7) uPlaySpeed = 7;

                        uPlaySpeed--;
                        PlayBackControl(strConst.NET_DVR_PLAYSLOW, uPlaySpeed, out o);
                        ShowPlaySpeed(uPlaySpeed);

                        break;
                    case "BACKWORDFAST": //向后快放
                        playStauts = PlayStatus.Backward_Play;
                        if (uPlaySpeed > 5) uPlaySpeed = 5;
                        if (uPlaySpeed == 0) uPlaySpeed = 5;
                        uPlaySpeed--;
                        PlayBackControl(strConst.NET_DVR_PLAYSLOW, uPlaySpeed, out o);
                        ShowPlaySpeed(uPlaySpeed);
                        break;
                    case "FORWARDSLOW": //向前慢放
                        playStauts = PlayStatus.Forward_Play;
                        if ((uPlaySpeed < 7) || (uPlaySpeed > 8)) uPlaySpeed = 6;
                        if (uPlaySpeed < 9)
                        {
                            uPlaySpeed++;
                            PlayBackControl(strConst.NET_DVR_PLAYFAST, uPlaySpeed, out o);
                            ShowPlaySpeed(uPlaySpeed);
                        }
                        break;

                    case "FORWARDFAST": //向前快放
                        playStauts = PlayStatus.Forward_Play;
                        if (uPlaySpeed < 8) uPlaySpeed = 8;
                        if (uPlaySpeed == 13) uPlaySpeed = 8;
                        uPlaySpeed++;
                        PlayBackControl(strConst.NET_DVR_PLAYFAST, uPlaySpeed, out o);
                        ShowPlaySpeed(uPlaySpeed);
                        break;

                    case "NORMAL": //正常播放
                        if (playStauts == PlayStatus.Forward_Play)
                        {
                            uPlaySpeed = 9;
                            PlayBackControl(strConst.NET_DVR_PLAYFAST, uPlaySpeed, out o);
                        }
                        else
                        {
                            uPlaySpeed = 4;
                            PlayBackControl(strConst.NET_DVR_PLAYSLOW, uPlaySpeed, out o);
                        }
                        PlayBackControl(strConst.NET_DVR_PLAYNORMAL, 0, out o);
                        ShowPlaySpeed(uPlaySpeed);
                        break;
                    case "FORWARDFRAME": //向前单帧

                        playStauts = PlayStatus.Forward_Play;
                        PlayBackControl(strConst.NET_DVR_PLAYFRAME, 0, out o);
                        break;
                    case "PAUSE":
                        if (!bPause)
                        {
                            if (PlayBackControl(strConst.NET_DVR_PLAYPAUSE, 0, out o))
                                bPause = true;
                        }
                        else
                        {
                            if (PlayBackControl(strConst.NET_DVR_PLAYNORMAL, 0, out o))
                                bPause = false;
                        }
                        break;
                    case "SOUND":
                        if (!bSoundOpened)
                        {
                            if (PlayBackControl(strConst.NET_DVR_PLAYSTARTAUDIO, 0, out o))
                            {
                                btnMute.Image = Properties.Resources.sound_high;
                                bSoundOpened = true;
                            }
                        }
                        else
                        {
                            if (PlayBackControl(strConst.NET_DVR_PLAYSTOPAUDIO, 0, out o))
                            {
                                btnMute.Image = Properties.Resources.sound_mute;
                                bSoundOpened = false;
                            }

                        }
                        break;
                    case "REC":
                        {
                            if (!bRecording)
                            {
                                if (PlayBackSaveData())
                                {
                                    SetRecordingStatus();
                                    bRecording = true;
                                    btnRec.Image = Properties.Resources.record_stop;

                                }
                            }
                            else
                            {
                                if (StopPlayBackSave())
                                {
                                    SetPlayingStatus();
                                    bRecording = false;
                                    btnRec.Image = Properties.Resources.record_start;

                                }
                            }
                        }

                        break;
                    case "DOWNLOAD":
                        if (!bDownloading)
                        {
                            StartDownload();
                        }
                        else
                        {
                            StopDownload();
                        }
                        break;
                    case "PICTURE":
                        PlayBackCaptureFile();
                        break;
                    case "FULLSCREEN":
                        if (OnFullPanel != null)
                            OnFullPanel(this, !bFullPanel);
                        break;
                }
            }
            if (OnSelectedWnd != null)
                OnSelectedWnd(this, e);
        }
        private void ShowPlaySpeed(uint speed)
        {
            switch (speed)
            {
                case 0:
                    lblPlaySpeed.Text = "播放速度：快退16倍速";
                    break;
                case 1:
                    lblPlaySpeed.Text = "播放速度：快退8倍速";
                    break;
                case 2:
                    lblPlaySpeed.Text = "播放速度：快退4倍速";
                    break;
                case 3:
                    lblPlaySpeed.Text = "播放速度：快退2倍速";
                    break;
                case 4:
                    lblPlaySpeed.Text = "播放速度：快退1倍速";
                    break;
                case 5:
                    lblPlaySpeed.Text = "播放速度：慢退1/2倍速";
                    break;
                case 6:
                    lblPlaySpeed.Text = "播放速度：慢退1/4倍速";
                    break;
                case 7:
                    lblPlaySpeed.Text = "播放速度：慢进1/4倍速";
                    break;
                case 8:
                    lblPlaySpeed.Text = "播放速度：慢进1/2倍速";
                    break;
                case 9:
                    lblPlaySpeed.Text = "播放速度：快进1倍速";
                    break;
                case 10:
                    lblPlaySpeed.Text = "播放速度：快进2倍速";
                    break;
                case 11:
                    lblPlaySpeed.Text = "播放速度：快进4倍速";
                    break;
                case 12:
                    lblPlaySpeed.Text = "播放速度：快进8倍速";
                    break;
                case 13:
                    lblPlaySpeed.Text = "播放速度：快进16倍速";
                    break;

            }
        }
        private delegate void OnFileSearchdelegateCallback(object sender, int result, List<DvrFindData> datalist);
        private void DvrOnFileSearch(object sender, int result, List<DvrFindData> datalist)
        {
            this.Invoke(new OnFileSearchdelegateCallback(OnFileSearchCallback), new object[] { sender, result, datalist });
        }
        private void OnFileSearchCallback(object sender, int result, List<DvrFindData> datalist)
        {
            if (result == strConst.NET_DVR_NOMOREFILE || result == strConst.NET_DVR_FILE_EXCEPTION)
            {
                picFinding.Visible = false;
                bSearching = false;
                FindDataList.Clear();
                timeline1.ClearItems();
                if (datalist.Count > 0)
                {
                    TimelineItem item = new TimelineItem
                    {
                        BackColor = Color.LightGray,
                        Height = 20
                    };
                    foreach (DvrFindData data in datalist)
                    {

                        DvrFindData newdata = new DvrFindData();
                        newdata.Clone(data);
                        FindDataList.Add(newdata);


                        SourceInfo sr = new SourceInfo
                        {
                            FileName = newdata.FileName,
                            TimeInterval = new TimeInterval(newdata.StartTime, newdata.StopTime)
                        };
                        item.AddSource(sr);
                    }

                    timeline1.Add(item);
                    timeline1.Position = datalist[0].StartTime;
                    timeline1.Range = new TimeInterval(datalist[0].StartTime, datalist[datalist.Count - 1].StopTime);
                }


                EventArgs e = new EventArgs();
                if (OnFileSearch != null)
                    OnFileSearch(this, e);


                if (OnSelectedWnd != null)
                    OnSelectedWnd(this, e);
            }


        }
        private void sliderVolume_ValueChanging(object sender, CancelIntValueEventArgs e)
        {
            uint o;
            uint volum = (uint)(0xffff / 100 * e.NewValue);
            if (PlayBackControl(strConst.NET_DVR_PLAYAUDIOVOLUME, volum, out o))
            {
            }
            else
            {
                e.Cancel = true;
            }
        }
        private void colorSliderPos_MouseUp(object sender, MouseEventArgs e)
        {
            uint o;
            uint pos = (uint)colorSliderPos.Value;

            PlayBackControl(strConst.NET_DVR_PLAYSETPOS, pos, out o);
            timerGetPlayPos.Enabled = true;
        }
        private void colorSliderPos_MouseDown(object sender, MouseEventArgs e)
        {
            timerGetPlayPos.Enabled = false;
        }
        private void timerGetPlayPos_Tick(object sender, EventArgs e)
        {
            //按文件播放有效
            if (PlaybackMode == PlaybackModeEnum.按文件)
            {
                uint time;
                if (PlayBackControl(strConst.NET_DVR_PLAYGETTIME, 0, out time))
                {
                    if (m_TotalTime > 0)
                    {
                        lblPlayPos.Text = string.Format("进度[秒]:{0}/{1}", time, m_TotalTime);
                    }
                    if (time >= m_TotalTime)
                    {
                        lblPlayPos.Text = string.Format("进度[秒]:{0}/{1}", 0, m_TotalTime);

                    }
                }
                uint pos;
                if (PlayBackControl(strConst.NET_DVR_PLAYGETPOS, 0, out pos))
                {
                    if (pos >= 100)
                    {
                        timerGetPlayPos.Enabled = false;
                        colorSliderPos.Value = 0;
                        StopPlay();
                        if (OnMessage != null)
                            OnMessage(this, "播放完成");
                    }
                    else
                        colorSliderPos.Value = (int)pos;
                }
            }


        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            SaveExPanelVideoSource();
            exPanelVideoSource.Visible = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            exPanelVideoSource.Visible = false;
        }
        private void panelPlayer_Click(object sender, EventArgs e)
        {
            if (OnSelectedWnd != null)
                OnSelectedWnd(this, e);
        }
        private void timeline1_TimeItemClicked(object sender, Timeline.TimeItemEventArgs e)
        {
            StopPlay();
            if (OnTimeItemClicked != null)
                OnTimeItemClicked(sender, e);
            PlayFileName = e.m_fileName;
            startTime = e.m_time;
            endTime = e.m_timeInterval.end;


        }

        #endregion

        #region 通用方法
        public static void KeyPress_NumInput(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != 8)
                e.KeyChar = (char)0;
        }
        public static void NoKey_KeyPress(object sender, KeyPressEventArgs e)
        {
            //阻止从键盘输入键
            e.Handled = true;

        }
        #endregion

        private void timerGetDownloadPos_Tick(object sender, EventArgs e)
        {
            int i = GetDownloadPos();
            lblDownLoadPos.Text = string.Format("下载进度:{0}%", i);
            if (i >= 100)
            {
                StopDownload();
                timerGetDownloadPos.Enabled = false;
                if (OnMessage != null)
                    OnMessage(this, "下载完成");
            }
        }

        private void btnSlow_Click(object sender, EventArgs e)
        {

        }






    }
}
