using DevComponents.DotNetBar;
using GHIBMS.Common;
using System;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;


namespace GHIBMS.NetVideo
{
    public partial class NetVideoControl : UserControl
    {
        VideoEffect videoEffect;
        //重写消息处理函数，以处理自定义消息
        private const int WM_USER = 0x0400;
        //protected override void DefWndProc(ref System.Windows.Forms.Message m)
        //{
        //    if (m.Msg > WM_USER)
        //    {

        //        if (RealPlayer != null)
        //        {
        //            //自定义消息处理函数
        //            RealPlayer.OnWinMessage(ref m);
        //        }
        //       // MessageBox.Show(m.Msg.ToString());
        //       // Console.WriteLine(m.Msg.ToString());
        //    }

        //    //默认消息处理函数
        //    base.DefWndProc(ref m);
        //}

        #region 内部变量
        private VideoRealPlayArgs m_PlayArgs = new VideoRealPlayArgs();
        private VideoCommandArgs m_PTZcmd = new VideoCommandArgs();
        private int _VideoControlWndID = 0;
        private bool IsPTZShow = false;
        private bool IsConfigShow = false;
        #endregion

        #region 属性

        /// <summary>
        /// 当前选中的播放器ID号
        /// </summary>
        private static int selectedWndId = 1;
        public static int SelectedWndId
        {
            set { selectedWndId = value; }
            get { return selectedWndId; }
        }
        public int CurrentWndID
        {
            get { return _VideoControlWndID; }
            set { _VideoControlWndID = value; }
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

        // 当前视频播放状态
        private VIDEO_PLAY_STATE workStatus = VIDEO_PLAY_STATE.State_Close;
        /// <summary>
        /// 播放器运行状态 
        /// </summary>
        public VIDEO_PLAY_STATE WorkStatus
        {
            get { return workStatus; }
        }
        public VideoRealPlayArgs PlayArgs
        {
            get { return m_PlayArgs; }
        }
        public bool IsShowTooBar
        {
            set
            {
                bar1.Visible = value;
            }
        }
        private IntPtr playWinHandle;
        public IntPtr PlayWinHandle
        {
            get
            {
                return panelPlay.Handle;
            }

        }

        private IVideoRealPlayer RealPlayer = null;
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

        #region 事件委托
        public delegate void OnMouseDowndelegate(object sender, MouseEventArgs e);
        public delegate void OnPlayVideodelegate(object sender, VideoRealPlayArgs args);

        public new event OnMouseDowndelegate OnMouseDown;
        public delegate void OnMessagedelegate(object sender, string msg);
        public event OnMessagedelegate OnMessage;
        public event OnPlayVideodelegate OnPlayVideo;

        public delegate void OnMatirxPTZControldelegate(object sender, VideoCommandArgs args);
        public event OnMatirxPTZControldelegate OnMatrixPTZControl;

        private void play_OnMessage(object sender, string msg)
        {
            if (msg == RealPlayControlEnum.PLAY.ToString())
            {
                SetPlayingStatus();
            }
            else if (msg == RealPlayControlEnum.STOP.ToString())
            {
                SetStopedStatus();
            }
            else
            {

                if (OnMessage != null)
                    OnMessage(sender, msg);
            }
        }
        private void Log(string message)
        {
            if (OnMessage != null)
                OnMessage(this, message);
        }
        public delegate void OnFullPaneldelegate(object sender, bool bFull);
        public event OnFullPaneldelegate OnFullPanel;


        //云台控制  集中控制用
        //public delegate void VideoCommandDelegate(object sender, VideoCommandArgs e);
        //public event VideoCommandDelegate OnVideoCommand;

        #endregion

        #region 构造器
        public NetVideoControl()
        {

            //SetStyle(
            //       ControlStyles.OptimizedDoubleBuffer
            //       | ControlStyles.ResizeRedraw
            //       | ControlStyles.Selectable
            //       | ControlStyles.AllPaintingInWmPaint
            //       | ControlStyles.UserPaint
            //       | ControlStyles.SupportsTransparentBackColor,
            //       true);
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
              ControlStyles.UserPaint |
              ControlStyles.AllPaintingInWmPaint,
              true);
            this.UpdateStyles();

            playWinHandle = panelPlay.Handle;

            picPath = ClientConfig.PicPath;
            recPath = ClientConfig.RecPath;



        }
        #endregion

        #region 公有方法
        private IntPtr mainHandle;
        public void SetWinMessage(IntPtr lWinHandle)
        {
            mainHandle = lWinHandle;

        }
        public void OnWinMessage(ref System.Windows.Forms.Message m)
        {

            if (RealPlayer != null)
            {
                // Debug.WriteLine(this.Name + "  OnWinMessage " + RealPlayer.ToString());
                //自定义消息处理函数
                RealPlayer.OnWinMessage(ref m);
            }
            else
            {
                //Debug.WriteLine(this.Name + "  OnWinMessage  RealPlayer=Null");

            }
        }
        /// <summary>
        /// 打开视频
        /// </summary>
        /// <param name="args">视频连接参数</param>
        public void OpenVideo(VideoRealPlayArgs args)
        {
            if (RealPlayer != null)
            {
                StopPlay();
            }

            //复制参数
            m_PlayArgs.Clone(args);
            m_PlayArgs.PlayWnd = playWinHandle;
            m_PlayArgs.PlayWndNo = _VideoControlWndID - 1;
            Debug.WriteLine(m_PlayArgs.PlayWnd.ToString());
            //创建对应设备类型的播放器对象，每个播放面板对应一个
            CreatePlayerInstance(args.ProtocolCode);

            if (RealPlayer != null)
            {
                RealPlayer.SetPlayAgrs(m_PlayArgs); //传递播放所需的参数
                RealPlayer.SetWinMessage(mainHandle);
                StartPlay();
                lblCamTitle.Text = args.CamName;//args.DvrIp + " " + args.DvrCh.ToString("D2");
            }
            else
            {
                string msg = String.Format("初始化网络播放器实例失败!类型{0}", args.ProtocolCode.ToString());
                Log(msg);
            }

            m_PTZcmd.MonName = m_PlayArgs.MonName;
            m_PTZcmd.DvrIp = m_PlayArgs.Ip;
            m_PTZcmd.Channel = m_PlayArgs.DvrCh;
            m_PTZcmd.Speed = (uint)sliderSpeed.Value;
            m_PTZcmd.Stop = 1;
            m_PTZcmd.VideoCommand = PTZCmdCodeEnum.MAT_VIDEO_SW;
            if (OnMatrixPTZControl != null)
                OnMatrixPTZControl(this, m_PTZcmd);


        }

        public VideoRealPlayArgs GetVideArgs()
        {
            return m_PlayArgs;
        }
        /// <summary>
        /// 播放视频
        /// </summary>
        public void StartPlay()
        {
            if (OnPlayVideo != null) OnPlayVideo(this, m_PlayArgs);
            if (RealPlayer != null)
            {
                if (RealPlayer.VIDEO_StartRealPlay())
                    SetPlayingStatus();
                else
                    SetStopedStatus();

            }

        }
        /// <summary>
        /// 停止播放
        /// </summary>
        public void StopPlay()
        {
            lblCamTitle.Text = "";
            if (workStatus != VIDEO_PLAY_STATE.State_Stop)
            {
                if (RealPlayer != null)
                {

                    if (RealPlayer.VIDEO_StopRealPlay())
                    {
                        SetStopedStatus();
                        panelPlay.Invalidate();
                        //Debug.WriteLine("StopPlay" + this.Name);

                    }
                }
            }
        }
        /// <summary>
        /// 抓拍
        /// </summary>
        public void CapturePic()
        {
            if (RealPlayer != null)
            {
                RealPlayer.VIDEO_CaptureJPEGPicture(m_PlayArgs.CamName);

            }
        }
        /// <summary>
        /// 抓拍
        /// </summary>
        public void CapturePic(string fileName)
        {
            if (fileName == "")
                fileName = m_PlayArgs.CamName;
            if (RealPlayer != null)
            {
                RealPlayer.VIDEO_CaptureJPEGPicture(fileName);
            }



        }
        /// <summary>
        /// 开始录像
        /// </summary>
        public void StartRecord()
        {
            if (RealPlayer != null)
            {
                if (RealPlayer.VIDEO_SaveRealData(m_PlayArgs.CamName))
                    SetRecordingStatus();
            }
        }
        /// <summary>
        /// 开始录像
        /// </summary>
        public void StartRecord(string fileName)
        {
            if (RealPlayer != null)
            {
                if (RealPlayer.VIDEO_SaveRealData(fileName))
                    SetRecordingStatus();
            }
        }
        /// <summary>
        /// 停止录像
        /// </summary>
        public void StopRecord()
        {
            if (RealPlayer != null)
            {
                if (RealPlayer.VIDEO_StopSaveRealData())
                    SetPlayingStatus();
            }

        }
        /// <summary>
        /// 云台控制
        /// </summary>
        /// <param name="cmd"></param>
        public void PTZControl(VideoCommandArgs cmd)
        {
            // MessageBox.Show(((VideoCommandEnum)cmd.VideoCommand).ToString());
            if (RealPlayer != null)
            {
                RealPlayer.VIDEO_PTZControlWithSpeed((uint)cmd.VideoCommand, cmd.Stop, cmd.Speed);

            }
            m_PTZcmd.VideoCommand = cmd.VideoCommand;
            m_PTZcmd.Speed = cmd.Speed;
            m_PTZcmd.Stop = cmd.Stop;

            if (OnMatrixPTZControl != null)
                OnMatrixPTZControl(this, m_PTZcmd);



        }
        /// <summary>
        /// 预置位
        /// </summary>
        /// <param name="cmd"></param>
        public bool PTZPreset(VideoCommandArgs cmd)
        {
            if (RealPlayer != null)
            {
                RealPlayer.VIDEO_PTZPreset((uint)cmd.VideoCommand, cmd.PresetIndex);


            }
            m_PTZcmd.VideoCommand = cmd.VideoCommand;
            m_PTZcmd.PresetIndex = cmd.PresetIndex;
            if (OnMatrixPTZControl != null)
                OnMatrixPTZControl(this, m_PTZcmd);

            return true;
        }
        /// <summary>
        /// 巡航
        /// </summary>
        /// <param name="dwPTZCommand"></param>
        /// <param name="CruiseRoute"></param>
        /// <param name="CruisePoint"></param>
        /// <param name="Input"></param>
        /// <returns></returns>
        public bool PTZCruise(VideoCommandArgs cmd)
        {
            if (RealPlayer != null)
            {
                RealPlayer.VIDEO_PTZCruise((uint)cmd.VideoCommand, cmd.CruiseRoute, cmd.CruisePoint, cmd.Input);

            }
            m_PTZcmd.CruiseRoute = cmd.CruiseRoute;
            m_PTZcmd.CruisePoint = cmd.CruisePoint;
            m_PTZcmd.Input = cmd.Input;
            m_PTZcmd.VideoCommand = cmd.VideoCommand;
            if (OnMatrixPTZControl != null)
                OnMatrixPTZControl(this, m_PTZcmd);
            return true;
        }
        /// <summary>
        /// 轨迹
        /// </summary>
        /// <param name="dwPTZCommand"></param>
        /// <param name="dwTrackIndex"></param>
        /// <returns></returns>
        public bool PTZTrack(VideoCommandArgs cmd)
        {
            if (RealPlayer != null)
            {
                RealPlayer.VIDEO_PTZTrack((uint)cmd.VideoCommand, cmd.TrackIndex);

            }
            m_PTZcmd.TrackIndex = cmd.TrackIndex;
            m_PTZcmd.VideoCommand = cmd.VideoCommand;
            if (OnMatrixPTZControl != null)
                OnMatrixPTZControl(this, m_PTZcmd);
            return true;

        }
        /// <summary>
        /// 获得视频参数
        /// </summary>
        /// <returns></returns>
        public VideoEffect GetVideoEffect()
        {
            if (RealPlayer != null)
            {
                return RealPlayer.VIDEO_GetVideoEffect();
            }
            return null;
        }
        /// <summary>
        /// 设置视频参数
        /// </summary>
        /// <param name="effect"></param>
        /// <returns></returns>
        public bool SetVideoEffect(VideoEffect effect)
        {
            if (RealPlayer != null)
            {
                return RealPlayer.VIDEO_SetVideoEffect(effect);
            }
            return false;
        }
        #region 声音、对讲
        /// <summary>
        /// 独占方式打开声音
        /// </summary>
        /// <returns></returns>
        public bool OpenSound()
        {
            bool ret = false;
            if (RealPlayer != null)
            {
                ret = RealPlayer.VIDEO_OpenSound();
            }
            if (ret)
                btnSound.Image = Properties.Resources.sound_high;
            return ret;
        }
        /// <summary>
        /// 关闭声音
        /// </summary>
        /// <returns></returns>
        public bool CloseSound()
        {
            bool ret = false;
            if (RealPlayer != null)
            {
                ret = RealPlayer.VIDEO_CloseSound();
            }
            if (ret)
                btnSound.Image = Properties.Resources.sound_mute;
            return ret;

        }
        /// <summary>
        /// 音量
        /// </summary>
        /// <param name="wVolume"></param>
        /// <returns></returns>
        public bool SetVolume(ushort wVolume)
        {
            if (RealPlayer != null)
            {
                return (RealPlayer.VIDEO_Volume(wVolume));
            }
            return false;
        }
        public bool StartVoiceCom()
        {
            bool ret = false;
            if (RealPlayer != null)
            {
                ret = (RealPlayer.VIDEO_StartVoiceCom());
            }
            if (ret)
                btnPhone.Image = Properties.Resources.phone_start;
            return false;
        }
        public bool StopVoiceCom()
        {
            bool ret = false;
            if (RealPlayer != null)
            {
                ret = RealPlayer.VIDEO_StopVoiceCom();

            }
            if (ret)
                btnPhone.Image = Properties.Resources.phone_stop;
            return ret;

        }
        public bool SetVoiceComClientVolume(ushort wVolume)
        {
            if (RealPlayer != null)
            {
                return RealPlayer.VIDEO_SetVoiceComClientVolume(wVolume);

            }
            return false;

        }
        #endregion

        #region 清理资料
        //分类清除所有SDK占用的资源 
        //此除要注意用户登出、清除SDK一定要静态，属于类才可以
        public void DisposeNetVideoSDK()
        {

            if (RealPlayer != null)
            {
                RealPlayer.DVR_Cleanup();
            }

        }
        #endregion
        #endregion

        #region 私有方法

        //生成播放类实例
        private void CreatePlayerInstance(string sdk)
        {
            if (RealPlayer != null)
            {
                if (RealPlayer.ProtocolCode == sdk)
                {
                    return;
                }
                else
                {
                    RealPlayer.Dispose();
                    RealPlayer = CreateNewPlayer(sdk);

                }
            }
            else
            {
                RealPlayer = CreateNewPlayer(sdk);
            }
            if (RealPlayer != null)
            {
                RealPlayer.OnMessage -= new GHIBMS.NetVideo.OnMessagedelegate(play_OnMessage);
                RealPlayer.OnMessage += new GHIBMS.NetVideo.OnMessagedelegate(play_OnMessage);
            }
        }
        private IVideoRealPlayer CreateNewPlayer(string sdk)
        {
            IVideoRealPlayer player;
            switch (sdk)
            {
                case "JK_DVR_HK":
                    player = new HikDvrRealPlayer();
                    break;
                case "JK_DVR_DH":
                    player = new DHDvrRealPlayer();
                    break;
                case "JK_DVR_INF":
                    player = new INFDvrRealPlayer();
                    break;
                case "JK_DVR_HB":
                    player = new HBDvrRealPlayer();
                    break;
                case "JK_PLAT_VIKOR":
                    player = new VIKORRealPlayer();
                    break;
                case "JK_PLAT_IMOS":
                    player = new IMOSRealPlayer();
                    break;
                case "JK_CAM_ONVIF":
                    player = new RTSPRealPlayer();
                    break;
                case "JK_DVR_TANDI":
                    player = new TandiDvrRealPlayer();
                    break;
                case "JK_DVR_FC":
                    player = new FCDvrRealPlayer();
                    break;
                default:
                    player = new HikDvrRealPlayer();
                    break;
            }

            return player;
        }
        private void panelPlay_MouseDown(object sender, MouseEventArgs e)
        {
            if (OnMouseDown != null)
                OnMouseDown(this, e);
        }


        #endregion

        #region 按钮事件与状态
        private void PlayerCommand_Executed(object sender, EventArgs e)
        {

            ICommandSource source = sender as ICommandSource;
            if (source.CommandParameter is string)
            {
                switch (source.CommandParameter.ToString())
                {
                    case "OPENVIDEO":

                        exPanelVideoSource.ExpandButton.Visible = false;
                        exPanelVideoSource.Expanded = true;
                        exPanelVideoSource.Top = (int)((panelPlay.Height - exPanelVideoSource.Height) / 2);
                        exPanelVideoSource.Left = (int)((panelPlay.Width - exPanelVideoSource.Width) / 2);
                        exPanelVideoSource.Visible = !exPanelVideoSource.Visible;
                        initExPanelVideoSource();
                        break;
                    case "PLAY":
                        StartPlay();
                        //if (RealPlayer != null)
                        //{
                        //    if (RealPlayer.VIDEO_PlayControl(RealPlayControlEnum.PLAY))
                        //        SetPlayingStatus();

                        //}
                        break;
                    case "STOP":
                        StopPlay();
                        panelPlay.Refresh();
                        //if (RealPlayer != null)
                        //{
                        //    if (RealPlayer.VIDEO_PlayControl(RealPlayControlEnum.STOP))
                        //       SetStopedStatus();

                        //}
                        break;
                    case "STARTREC":
                        StartRecord();
                        break;
                    case "STOPREC":
                        StopRecord();
                        break;
                    case "PICTURE":
                        CapturePic();
                        break;
                    case "OPENSOUND":
                        OpenSound();
                        break;
                    case "CLOSESOUND":
                        CloseSound();
                        break;
                    case "OPENPHONE":
                        StartVoiceCom();
                        break;
                    case "CLOSEPHONE":
                        StopVoiceCom();
                        break;
                    case "VOICE":
                        SetVolume((ushort)sldVoice.Value);
                        break;
                    case "PTZ":
                        if (IsPTZShow)
                        {
                            SetPTZStatusOFF();
                        }
                        else
                        {
                            SetPTZStatusON();
                        }
                        break;
                    case "CONFIG":
                        if (IsConfigShow)
                        {
                            IsConfigShow = false;
                            exPanelConfig.Visible = false;
                        }
                        else
                        {

                            if (RealPlayer != null)
                            {
                                videoEffect = RealPlayer.VIDEO_GetVideoEffect();
                                if (videoEffect != null)
                                {
                                    sliderBright.Value = (int)videoEffect.BrightValue;
                                    sliderContrast.Value = (int)videoEffect.ContrastValue;
                                    sliderSaturation.Value = (int)videoEffect.SaturationValue;
                                    sliderHue.Value = (int)videoEffect.HueValue;
                                }
                            }
                            IsConfigShow = true;
                            exPanelConfig.Visible = true;

                        }
                        break;
                    case "FULLSCREEN":
                        if (OnFullPanel != null)
                            OnFullPanel(this, !bFullPanel);

                        break;

                }
            }
        }
        //设置视频源连接
        private void btnConnect_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("");
            SaveExPanelVideoSource();
            exPanelVideoSource.Visible = false;
        }
        //设置视频源取消
        private void btnCancel_Click(object sender, EventArgs e)
        {
            exPanelVideoSource.Visible = false;
        }
        //初始化视频源参数
        private void initExPanelVideoSource()
        {

            if (m_PlayArgs != null)
            {
                //cmbProtocol.Items.Clear();
                cmbProtocol.Text = m_PlayArgs.ProtocolCode.ToString();
                ipInputDvr.Text = m_PlayArgs.Ip;
                txtDvrPort.Text = m_PlayArgs.Port.ToString();
                txtUserName.Text = m_PlayArgs.UserName;
                txtPassword.Text = m_PlayArgs.Password.ToString();
                ipInputVod.Text = m_PlayArgs.VODIp;
                txtVodPort.Text = m_PlayArgs.VODPort.ToString();
                txtDvrCH.Text = m_PlayArgs.DvrCh.ToString();
                chkCodeType.Checked = ((int)m_PlayArgs.EncodeMode == 0);
            }
        }
        //保存视频源连接参数
        private void SaveExPanelVideoSource()
        {
            VideoRealPlayArgs args = new VideoRealPlayArgs();

            try
            {
                args.ProtocolCode = (string)Enum.Parse(typeof(string), cmbProtocol.Text);
            }
            catch { }
            args.Ip = ipInputDvr.Text;
            args.Port = ushort.Parse(txtDvrPort.Text);
            args.UserName = txtUserName.Text;
            args.Password = txtPassword.Text;
            args.VODIp = ipInputVod.Text;
            args.VODPort = int.Parse(txtVodPort.Text);
            args.DvrCh = int.Parse(txtDvrCH.Text);
            args.EncodeMode = (EncodeTypeEnum)(chkCodeType.Checked ? 0 : 1);
            OpenVideo(args);

        }

        //播放状态
        private delegate void SetPlayingStatusdelgate();
        private void SetPlayingStatus()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new SetPlayingStatusdelgate(SetPlayingStatus), new object[] { });


            }
            else
            {
                btnPlay.Enabled = false;
                btnStop.Enabled = true;
                btnStartRecord.Enabled = true;
                btnStopRecord.Enabled = false;
                btnCapture.Enabled = true;
                btnSound.Enabled = true;
                btnPTZ.Enabled = true;

                picRecordShow.Visible = false;
                workStatus = VIDEO_PLAY_STATE.State_Play;
            }
        }
        private delegate void SetStopedStatusdelgate();
        private void SetStopedStatus()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new SetStopedStatusdelgate(SetStopedStatus), new object[] { });


            }
            else
            {
                btnPlay.Enabled = true;
                btnStop.Enabled = false;
                btnStartRecord.Enabled = false;
                btnStopRecord.Enabled = false;
                btnCapture.Enabled = false;
                btnSound.Enabled = false;
                btnPTZ.Enabled = false;
                picRecordShow.Visible = false;
                workStatus = VIDEO_PLAY_STATE.State_Stop;
            }
        }
        private void SetRecordingStatus()
        {
            btnPlay.Enabled = false;
            btnStop.Enabled = true;
            btnStartRecord.Enabled = false;
            btnStopRecord.Enabled = true;
            btnCapture.Enabled = true;
            btnSound.Enabled = true;
            btnPTZ.Enabled = true;
            picRecordShow.Visible = true;
            workStatus = VIDEO_PLAY_STATE.State_Record;
        }
        private void SetPTZStatusON()
        {
            btnPTZUP.Visible = true;
            btnPTZDOWN.Visible = true;
            btnPTZLEFT.Visible = true;
            btnPTZRIGHT.Visible = true;
            btnZOOMIN.Visible = true;
            btnZOOMOUT.Visible = true;
            sliderSpeed.Visible = true;
            IsPTZShow = true;
        }
        private void SetPTZStatusOFF()
        {
            btnPTZUP.Visible = false;
            btnPTZDOWN.Visible = false;
            btnPTZLEFT.Visible = false;
            btnPTZRIGHT.Visible = false;
            btnZOOMIN.Visible = false;
            btnZOOMOUT.Visible = false;
            sliderSpeed.Visible = false;
            IsPTZShow = false;
        }
        private void SetMeTop(object obj)
        {
            //原理:先添加的控件会在最上面,即可见次序是由index决定的.
            int index = this.Parent.Controls.GetChildIndex((Control)obj);//取得要置顶控件的index
            ArrayList AL = new ArrayList();//用来装入控件的容器
            for (int i = 0; i < index; i++)//把要置顶控件上面的控件都装入容器
                AL.Add(this.Controls[i]);
            for (int i = 0; i < AL.Count; i++)
            {
                //用一次删除和一次添加操作,让它上面的控件排到下面去.
                this.Controls.Remove((Control)AL[i]);
                this.Controls.Add((Control)AL[i]);
            }
        }
        //全屏
        private void SetVideoMax(object sender, System.EventArgs e)
        {
            SetMeTop(sender);
            ((UserControl)this).Dock = DockStyle.Fill;

        }
        //云台控制
        private void PTZButtonMouseDown(object sender, MouseEventArgs e)
        {
            if (OnMouseDown != null)
                OnMouseDown(this, e);

            m_PTZcmd.MonName = m_PlayArgs.MonName;
            m_PTZcmd.DvrIp = m_PlayArgs.Ip;
            m_PTZcmd.Channel = m_PlayArgs.DvrCh;
            m_PTZcmd.Speed = (uint)sliderSpeed.Value;
            m_PTZcmd.Stop = 0;
            ICommandSource source = sender as ICommandSource;
            if (source.CommandParameter is string)
            {
                m_PTZcmd.VideoCommand = ((PTZCmdCodeEnum)Enum.Parse(typeof(PTZCmdCodeEnum), source.CommandParameter.ToString()));
            }
            PTZControl(m_PTZcmd);
            if (OnMatrixPTZControl != null)
                OnMatrixPTZControl(this, m_PTZcmd);


        }
        private void PTZButtonMouseUp(object sender, MouseEventArgs e)
        {
            if (OnMouseDown != null)
                OnMouseDown(this, e);

            m_PTZcmd.MonName = m_PlayArgs.MonName;
            m_PTZcmd.DvrIp = m_PlayArgs.Ip;
            m_PTZcmd.Channel = m_PlayArgs.DvrCh;
            m_PTZcmd.Speed = (uint)sliderSpeed.Value;
            m_PTZcmd.Stop = 1;
            ICommandSource source = sender as ICommandSource;
            if (source.CommandParameter is string)
            {
                m_PTZcmd.VideoCommand = ((PTZCmdCodeEnum)Enum.Parse(typeof(PTZCmdCodeEnum), source.CommandParameter.ToString()));
            }
            PTZControl(m_PTZcmd);
            if (OnMatrixPTZControl != null)
                OnMatrixPTZControl(this, m_PTZcmd);

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

        private void NetVideoControl_Load(object sender, EventArgs e)
        {
            //cmbProtocol.Items.Clear();
            //string[] item = System.Enum.GetNames(typeof(string));
            //foreach (string s in item)
            //{
            //    cmbProtocol.Items.Add(s);
            //}

            cmbProtocol.KeyPress += NoKey_KeyPress;
            txtDvrPort.KeyPress += KeyPress_NumInput;
            txtDvrCH.KeyPress += KeyPress_NumInput;


        }

        private void btnConfigCancel_Click(object sender, EventArgs e)
        {
            exPanelConfig.Visible = false;
        }

        private void ipInputVod_Click(object sender, EventArgs e)
        {

        }

        private void btnSaveConfig_Click(object sender, EventArgs e)
        {
            //this.SetVideoEffect();

            VideoEffect v = new VideoEffect();
            v.BrightValue = (uint)sliderBright.Value;
            v.ContrastValue = (uint)sliderContrast.Value;
            v.SaturationValue = (uint)sliderSaturation.Value;
            v.HueValue = (uint)sliderHue.Value;
            if (RealPlayer != null)
                RealPlayer.VIDEO_SetVideoEffect(v);
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {

        }


    }
}
