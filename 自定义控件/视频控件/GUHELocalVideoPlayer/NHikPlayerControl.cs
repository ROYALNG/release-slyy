using DevComponents.DotNetBar;
using DHPlaySDK;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace GHIBMS.NHikPlayer
{

    public partial class NHikPlayerControl : UserControl
    {
        public static void No_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        public NHikPlayerControl()
        {

            InitializeComponent();
            // Set the value of the double-buffering style bits to true.
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
               ControlStyles.UserPaint |
               ControlStyles.AllPaintingInWmPaint,
               true);
            this.UpdateStyles();
            Tem_TView = new TreeView();
            Tem_TView = treeView1;
            combType.SelectedIndex = 0;
            txtRecDir.KeyPress += NHikPlayerControl.No_KeyPress;
            combType.KeyPress += NHikPlayerControl.No_KeyPress;
        }

        #region 私有变量
        /*
         * 回调函数异常(检测到 CallbackOnCollectedDelegate)
         * 解决方法：
         * 1.将委托的引用保存为成员变量。（无效？）
         * 2.使用 GC.KeepAlive 来确保特定实例保持活动状态一段时间。
         * http://www.microsoft.com/china/MSDN/library/netFramework/netframework/issuesBugBash.mspx?mfr=true
         * */
        private DisplayCBFun dcbf;
        private Audio ad;
        private FileRefDone frd;

        // 播放器端口，选择范围从0到15
        private int nPort = 0;
        // 视频文件(.mp4,.264)路径
        private string strPlayFileName = string.Empty;
        // 当前视频播放状态
        private VIDEO_PLAY_STATE enumState;

        // 0表示为bmp格式，1表示为jpeg格式
        private int nCapPicType = 1;
        // bmp数量
        private int nBmp = 1;
        // jpeg数量
        private int nJpeg = 1;

        // 是否已经打开文件
        private bool bOpen = false;
        // 是否启用声音
        private bool bSound = false;
        // 是否建立了文件索引  
        private bool bFileRefCreated = false;
        // 是否全屏显示
        private bool bFullScreen = false;
        // 是否启用去闪烁功能
        private bool bDeflash = true;
        // 是否使用高质量图像
        //private bool bPicQuality = true;
        // 是否循环播放
        // private bool bRepeatPlay = false;

        // 视频宽度
        private int nWidth = 0;
        // 视频高度
        private int nHeight = 0;
        // 总帧数
        private int dwTotalFrames = 0;
        // 总时长
        private int dwMaxFileTime = 0;
        // 视频播放速度
        private int nSpeed = 0;
        // 当前播放进度条刻度数
        private int nPrePlayPos = 0;
        //JPEG 图像质量
        private int nQuality = 100;
        #endregion

        #region 属性

        // 图片保存路径
        private string strCapPicPath = "c:\\";
        public string CapPicPath
        {
            get { return strCapPicPath; }
            set { strCapPicPath = value; }
        }


        #endregion

        #region CallBack
        /// <summary>
        /// 回调函数
        /// </summary>
        /// <param name="nPort">通道号</param>
        /// <param name="pAudioBuf">wave格式音频数据</param>
        /// <param name="nSize">音频数据长度</param>
        /// <param name="nStamp"> 时标(ms) </param>
        /// <param name="nType">音频类型T_AUDIO16, 采样率16khz，单声道，每个采样点16位表示 </param>
        /// <param name="nUser">用户自定义数据</param>
        public void CB_Audio(int nPort, string pAudioBuf, int nSize, int nStamp, int nType, int nUser)
        {
        }

        /// <summary>
        /// 解码后回调函数，如果此回调函数被设置，那么显示部分由用户自己控制（包括显示速度）。
        /// </summary>
        /// <param name="nPort">播放器通道号</param>
        /// <param name="pBuf">解码后的音视频数据</param>
        /// <param name="nSize">解码后的音视频数据pBuf的长度</param>
        /// <param name="pFrameInfo">图像和声音信息</param>
        /// <param name="nReserved1">保留参数</param>
        /// <param name="nReserved2">保留参数</param>
        public static void CB_DecCBFun(int nPort, IntPtr pBuf, int nSize, ref FRAME_INFO pFrameInfo, int nReserved1, int nReserved2)
        {
        }


        /// <summary>
        /// 抓图回调函数
        /// </summary>
        /// <param name="nPort">通道号</param>
        /// <param name="pBuf">返回图像数据</param>
        /// <param name="nSize">返回图像数据大小</param>
        /// <param name="nWidth">画面宽，单位像素</param>
        /// <param name="nHeight">画面高</param>
        /// <param name="nStamp">时标信息，单位毫秒</param>
        /// <param name="nType">数据类型，T_YV12，T_RGB32，T_UYVY</param>
        /// <param name="nReceaved">保留</param>
        public void CB_DisplayCBFun(int nPort, IntPtr pBuf, int nSize, int nWidth, int nHeight, int nStamp, int nType, int nReceaved)
        {
        }

        /// <summary>
        /// 回调函数
        /// </summary>
        /// <param name="nPort">通道号</param>
        /// <param name="hDc">hDc OffScreen表面设备上下文，你可以像操作显示窗口客户区DC那样操作它。</param>
        /// <param name="nUser">用户数据，就是上面输入的用户数据</param>
        public void CB_DrawFun(int nPort, IntPtr hDc, int nUser)
        {

        }

        /// <summary>
        /// 回调函数
        /// </summary>
        /// <param name="nPort">通道号</param>
        /// <param name="nUser">用户自定义数据</param>
        public void CB_EncChange(int nPort, int nUser)
        {
        }

        /// <summary>
        /// 文件索引回调函数
        /// </summary>
        /// <param name="nPort">播放器通道号</param>
        /// <param name="nUser">用户数据</param>
        public void CB_FileRefDone(int nPort, ushort nUser)
        {
            //完成文件索引
            bFileRefCreated = true;

        }

        /// <summary>
        /// 回调函数
        /// </summary>
        /// <param name="nPort">通道号</param>
        /// <param name="frameType">有关数据帧的信息</param>
        /// <param name="nUser"></param>
        public void CB_GetOrignalFrame(int nPort, ref FRAME_TYPE frameType, int nUser)
        {
        }

        /// <summary>
        /// 回调函数
        /// </summary>
        /// <param name="nPort">播放器通道号</param>
        /// <param name="nBufSize">缓冲区中剩余数据</param>
        /// <param name="dwUser">用户数据</param>
        /// <param name="pContext">保留数据</param>
        public void CB_SourceBufCallBack(int nPort, ushort nBufSize, ushort dwUser, IntPtr pContext)
        {
        }

        /// <summary>
        /// 回调函数
        /// </summary>
        /// <param name="nPort">通道号</param>
        /// <param name="pFilePos">文件位置</param>
        /// <param name="bIsVideo">是否视频数据，1视频，0音频</param>
        /// <param name="nUser">用户数据</param>
        public void CB_Verify(int nPort, ref PFRAME_POS pFilePos, ushort bIsVideo, ushort nUser)
        {
        }
        #endregion

        #region Player
        private bool BrowseFile()
        {
            OpenFileDialog opfileDlg = new OpenFileDialog();
            opfileDlg.CheckFileExists = true;
            opfileDlg.Filter = "海康、大华录像文件(*.mp4;*.264)|*.mp4;*.264|所有文件(*.*)|*.*";
            if (opfileDlg.ShowDialog() == DialogResult.OK)
            {
                strPlayFileName = opfileDlg.FileName;
                if (opfileDlg.SafeFileName.Length > 12)
                    lblFileName.Text = opfileDlg.SafeFileName.Substring(0, 12) + "...";
                else
                    lblFileName.Text = opfileDlg.SafeFileName;
                return true;
            }
            else
            {
                return false;
            }
        }

        private void OpenFile()
        {
            if (combType.SelectedIndex == 0) //海康
            {
                //this.pVideo.AllowDrop = true;
                enumState = VIDEO_PLAY_STATE.State_Close;
                // 抓图回调函数
                dcbf = new DisplayCBFun(CB_DisplayCBFun);
                HikPlayer.PlayM4_SetDisplayCallBack(nPort, dcbf);
                GC.KeepAlive(dcbf);
                //// 音频回调函数
                ad = new Audio(CB_Audio);
                HikPlayer.PlayM4_SetAudioCallBack(nPort, ad, 0);
                GC.KeepAlive(ad);

                this.cSliderTime.Minimum = 0;
                this.cSliderTime.Maximum = HikPlayer.PLAYER_SLIDER_MAX;
                this.cSliderTime.Value = 0;

                this.cSliderVolume.Maximum = 0;
                this.cSliderVolume.Maximum = 0xffff - 1;
                this.cSliderVolume.Value = this.cSliderVolume.Maximum * 3 / 4;

                //设置音量
                HikPlayer.PlayM4_SetVolume(nPort, this.cSliderVolume.Value);
                HikPlayer.PlayM4_GetPort(out nPort);


                frd = new FileRefDone(CB_FileRefDone);
                HikPlayer.PlayM4_SetFileRefCallBack(nPort, frd, 0);
                GC.KeepAlive(frd);
                //设置设置JPEG 图像质量
                HikPlayer.PlayM4_SetJpegQuality(nQuality);
                //打开文件
                HikPlayer.PlayM4_OpenFile(nPort, strPlayFileName);
                //设置Overlay模式，此模式下只能有一路播放器可得到显卡支持，使用Overlay画面和进行缩放
                //HikPlayer.PlayM4_SetOverlayMode(nPort, true, Color.FromArgb(255, 0, 255).ToArgb());

                //获取视频总时长
                dwMaxFileTime = HikPlayer.PlayM4_GetFileTime(nPort);
                if (dwMaxFileTime == 0)
                    return;
                //获取视频总帧数
                dwTotalFrames = HikPlayer.PlayM4_GetFileTotalFrames(nPort);

                bOpen = true;

                //播放
                Play();
            }
            else if (combType.SelectedIndex == 1) //大华
            {
                if (DHPlay.DHPlayControl(PLAY_COMMAND.OpenFile, 0, strPlayFileName))
                {
                    SetOpenStatus();
                    string TotalTime = DHPlay.DHConvertToTime(DHPlay.DHPlayControl(PLAY_COMMAND.GetFileTotalTime, 0, true), 1, "HH:mm:ss");
                    int SoundValue = (int)DHPlay.DHPlayControl(PLAY_COMMAND.GetVolume, 0, true);
                }
                else
                {
                    MessageBox.Show("打开文件失败！");
                }

            }
        }

        private void CloseFile()
        {
            if (combType.SelectedIndex == 0) //海康
            {
                //停止
                Stop();

                HikPlayer.PlayM4_CloseFile(nPort);
                enumState = VIDEO_PLAY_STATE.State_Close;
                bOpen = false;
                bFileRefCreated = false;
            }
            else if (combType.SelectedIndex == 1) //大华
            {
                if (!DHPlay.DHPlayControl(PLAY_COMMAND.CloseFile, 0))
                {
                    MessageBox.Show("关闭文件失败！");
                }
                else
                {
                    this.pVideo.Invalidate();//刷新画面
                    SetStopedStatus();
                }

            }
        }

        private void Play()
        {
            if (combType.SelectedIndex == 0) //海康
            {
                if (enumState == VIDEO_PLAY_STATE.State_Play)
                    return;

                if (enumState == VIDEO_PLAY_STATE.State_Pause)
                {
                    int nPreSpeed = nSpeed;
                    enumState = VIDEO_PLAY_STATE.State_Play;
                    HikPlayer.PlayM4_Pause(nPort, false);
                    HikPlayer.PlayM4_Play(nPort, pVideo.Handle);

                    nSpeed = 0;
                    AdjustSpeed(nPreSpeed);
                    ThrowBFrame(0);
                }
                else
                {
                    nSpeed = 0;
                    ThrowBFrame(0);

                    if (HikPlayer.PlayM4_Play(nPort, pVideo.Handle))
                    {
                        enumState = VIDEO_PLAY_STATE.State_Play;
                        HikPlayer.PlayM4_RefreshPlay(nPort);
                    }
                    bSound = HikPlayer.PlayM4_PlaySound(nPort);
                }

                this.timer1.Enabled = true;
                //this.pInfo.Visible = false;
                //this.tsslblStatus.Text = "播放";
                //this.menuItemTimer.Enabled = false;
                this.btnPlay.Image = Properties.Resources.pause_blue;
                SetPlayingStatus();
            }
            else if (combType.SelectedIndex == 1) //大华
            {
                if (enumState == VIDEO_PLAY_STATE.State_Play)
                    return;

                if (enumState == VIDEO_PLAY_STATE.State_Pause) //暂停后再播放
                {
                    int nPreSpeed = nSpeed;
                    enumState = VIDEO_PLAY_STATE.State_Play;
                    if (DHPlay.DHPlayControl(PLAY_COMMAND.ReSume, 0))
                    {
                        SetPlayingStatus();
                    }
                    else
                    {
                        MessageBox.Show("继续播放失败！");
                    }

                    nSpeed = 0;
                    AdjustSpeed(nPreSpeed);

                }
                else  //播放
                {
                    nSpeed = 0;
                    if (!DHPlay.DHPlayControl(PLAY_COMMAND.Start, 0, this.pVideo.Handle))
                    {
                        MessageBox.Show("播放文件失败！");
                        SetStopedStatus();
                    }
                    else
                    {
                        SetPlayingStatus();
                        enumState = VIDEO_PLAY_STATE.State_Play;
                        //chkEnableSound_CheckedChanged(sender, e);//处理声音开关6
                    }
                }

                this.timer1.Enabled = true;
                //this.pInfo.Visible = false;
                //this.tsslblStatus.Text = "播放";
                //this.menuItemTimer.Enabled = false;
                this.btnPlay.Image = Properties.Resources.pause_blue;
                SetPlayingStatus();

            }
        }

        private void Pause()
        {
            if (combType.SelectedIndex == 0) //海康
            {
                if (enumState == VIDEO_PLAY_STATE.State_Play)
                {
                    HikPlayer.PlayM4_Pause(nPort, true);
                    enumState = VIDEO_PLAY_STATE.State_Pause;
                    this.timer1.Enabled = false;
                }
            }
            else if (combType.SelectedIndex == 1) //大华
            {
                if (DHPlay.DHPlayControl(PLAY_COMMAND.Pause, 0))
                {
                    SetPauseStatus();
                    enumState = VIDEO_PLAY_STATE.State_Pause;
                }
                else
                {
                    MessageBox.Show("暂停播放失败！");
                }
            }
        }

        private void Stop()
        {
            if (combType.SelectedIndex == 0) //海康
            {
                if (enumState == VIDEO_PLAY_STATE.State_Stop)
                    return;

                //HikPlayer.PlayM4_SetFileEndMsg(nPort, pVideo.Handle, 0);
                if (HikPlayer.PlayM4_StopSound())
                {
                    bSound = false;
                }
                if (HikPlayer.PlayM4_Stop(nPort))
                {
                }
                enumState = VIDEO_PLAY_STATE.State_Stop;

                this.timer1.Enabled = false;
                SetStopedStatus();
                //this.cSliderTime.Value = 0;
                //this.tsbtnPlayOrPause.Image = global::NHikPlayer.Properties.Resources.Play;
                //this.tsslblStatus.Text = "停止";
                //this.menuItemTimer.Enabled = true;
                //this.tsslblFrame.Text = string.Empty;
                //this.tsslblTime.Text = string.Empty;
                //this.pInfo.Left = (this.pVideo.Width - this.pInfo.Width) / 2;
                //this.pInfo.Top = (this.pVideo.Height - this.pInfo.Height) / 2;
                //this.pInfo.Visible = true;
                this.pVideo.Invalidate();
            }
            else if (combType.SelectedIndex == 1) //大华
            {
                if (!DHPlay.DHPlayControl(PLAY_COMMAND.Stop, 0))
                {
                    MessageBox.Show("停止播放失败！");
                }
                else
                {
                    enumState = VIDEO_PLAY_STATE.State_Stop;
                    SetStopedStatus();
                    this.timer1.Enabled = false;
                    SetStopedStatus();
                    this.pVideo.Invalidate(); ;//刷新画面
                }


            }
        }

        private void GotoStart()
        {
            if (combType.SelectedIndex == 0) //海康
            {
                if (bFileRefCreated)
                    HikPlayer.PlayM4_SetCurrentFrameNum(nPort, 0);
                else
                    HikPlayer.PlayM4_SetPlayPos(nPort, 0);
            }
            else if (combType.SelectedIndex == 1) //大华
            {
                if (!DHPlay.DHPlayControl(PLAY_COMMAND.ToBegin, 0))
                {
                    MessageBox.Show("定位到起始帧失败！");
                }
                else
                {
                    this.tsslblFrame.Text = string.Format("帧数：{0,5} / {1,5}", 0, dwTotalFrames);
                    this.tsslblTime.Text = string.Format("时间：{0} / {1}", 0, FormatTimeStatus(dwMaxFileTime));
                }


            }
        }

        private void GotoEnd()
        {
            if (combType.SelectedIndex == 0) //海康
            {
                if (bFileRefCreated)
                {
                    int nEndFrame = dwTotalFrames;
                    int nCurFrame = HikPlayer.PlayM4_GetCurrentFrameNum(nPort);
                    while (!HikPlayer.PlayM4_SetCurrentFrameNum(nPort, nEndFrame--))
                    {
                        if (nEndFrame <= Math.Max(0, dwTotalFrames - 3))
                        {
                            HikPlayer.PlayM4_SetCurrentFrameNum(nPort, nCurFrame);
                            break;
                        }
                    }
                }
                else
                {
                    HikPlayer.PlayM4_SetPlayPos(nPort, 1);
                }
            }
            else if (combType.SelectedIndex == 1) //大华
            {
                if (!DHPlay.DHPlayControl(PLAY_COMMAND.ToEnd, 0))
                {
                    MessageBox.Show("定位最后一帧失败！");
                }
                else
                {

                    this.tsslblFrame.Text = string.Format("帧数：{0,5} / {1,5}", dwTotalFrames, dwTotalFrames);
                    this.tsslblTime.Text = string.Format("时间：{0} / {1}", FormatTimeStatus(dwMaxFileTime), FormatTimeStatus(dwMaxFileTime));
                }

            }
        }

        private void Fast()
        {
            if (combType.SelectedIndex == 0) //海康
            {
                if (HikPlayer.PlayM4_Fast(nPort))
                {
                    nSpeed++;
                    if (nSpeed > 0)
                        ThrowBFrame(2);
                }
            }
            else if (combType.SelectedIndex == 1) //大华
            {
                if (!DHPlay.DHPlayControl(PLAY_COMMAND.Fast, 0))
                {
                    MessageBox.Show("快放失败！");
                }

            }
        }

        private void Slow()
        {
            if (combType.SelectedIndex == 0) //海康
            {
                if (HikPlayer.PlayM4_Slow(nPort))
                {
                    nSpeed--;
                    if (nSpeed <= 0)
                        ThrowBFrame(0);
                }
            }
            else if (combType.SelectedIndex == 1) //大华
            {
                if (!DHPlay.DHPlayControl(PLAY_COMMAND.Slow, 0))
                {
                    MessageBox.Show("慢放失败！");
                }

            }
        }

        private void RestoreSpeed()
        {
            if (combType.SelectedIndex == 0) //海康
            {
                if (HikPlayer.PlayM4_Play(nPort, pVideo.Handle))
                {
                    nSpeed = 0;
                    ThrowBFrame(0);
                }
            }
            else if (combType.SelectedIndex == 1) //大华
            {


            }
        }

        private void AdjustSpeed(int aSpeed)
        {
            int nCyc = 0;
            while (nSpeed != aSpeed)
            {
                if (nSpeed > aSpeed)
                    Fast();
                if (nSpeed < aSpeed)
                    Slow();

                nCyc++;
                if (nCyc >= 10)
                    break;
            }
        }

        private void StepForward()
        {
            if (combType.SelectedIndex == 0) //海康
            {
                ThrowBFrame(0);
                HikPlayer.PlayM4_OneByOne(nPort);
                enumState = VIDEO_PLAY_STATE.State_Pause;
            }
            else if (combType.SelectedIndex == 1) //大华
            {


            }
        }

        private void StepBackward()
        {
            if (combType.SelectedIndex == 0) //海康
            {
                if (bFileRefCreated)
                {
                    HikPlayer.PlayM4_OneByOneBack(nPort);
                    enumState = VIDEO_PLAY_STATE.State_Pause;
                }
                else
                    MessageBox.Show("未创建文件索引", "错误", MessageBoxButtons.OK);
            }
            else if (combType.SelectedIndex == 1) //大华
            {


            }
        }

        private void Cappic()
        {
            if (combType.SelectedIndex == 0) //海康
            {
                string sFilePath = string.Empty;
                int nBufSize;
                byte[] pImage;
                int pImageSize = 0;

                if (nCapPicType == 0)
                {
                    nBufSize = nWidth * nHeight * 5;
                    pImage = new byte[nBufSize];
                    if (!HikPlayer.PlayM4_GetBMP(nPort, pImage, nBufSize, out pImageSize))
                    {
                        int code = HikPlayer.PlayM4_GetLastError(nPort);
                        return;
                    }

                    if (strCapPicPath.Length < 0)
                        strCapPicPath = Application.StartupPath;

                    sFilePath = string.Format(@"{0}\{1}.bmp", strCapPicPath, DateTime.Now.ToString("yyyyMMddHHmmss_fff"));
                    nBmp++;

                }
                else
                {
                    nBufSize = nWidth * nHeight * 3 / 2;
                    pImage = new byte[nBufSize];
                    if (!HikPlayer.PlayM4_GetJPEG(nPort, pImage, nBufSize, out pImageSize))
                        return;

                    if (strCapPicPath.Length < 0)
                        strCapPicPath = Application.StartupPath;

                    sFilePath = string.Format(@"{0}\{1}.jpeg", strCapPicPath, DateTime.Now.ToString("yyyyMMddHHmmss_fff"));
                    nJpeg++;
                }

                if (pImageSize > 0)
                {
                    try
                    {
                        FileStream fs = new FileStream(sFilePath, FileMode.CreateNew);
                        //开始写入 
                        fs.Write(pImage, 0, pImageSize);
                        //清空缓冲区、关闭流 
                        fs.Flush();
                        fs.Close();
                    }
                    catch (System.Exception e)
                    {
                        throw e;
                    }
                }
            }
            else if (combType.SelectedIndex == 1) //大华
            {
                if (strCapPicPath.Length < 0)
                    strCapPicPath = Application.StartupPath;
                string sFilePath = string.Empty;
                sFilePath = string.Format(@"{0}\{1}.bmp", strCapPicPath, DateTime.Now.ToString("yyyyMMddHHmmss_fff"));
                //抓图处理代码
                bool blnSavePic = false;
                // string saveFilePath = strSavePicPath + @"\" + (DateTime.Now.ToString("yyyyMMdd_HHmmss")) + ".bmp";
                blnSavePic = DHPlay.DHPlayControl(PLAY_COMMAND.CatchPic, 0, sFilePath);
                if (!blnSavePic)
                {
                    MessageBox.Show("保存图片失败!\n" + sFilePath);
                }

            }
        }

        private void Sound()
        {
            if (combType.SelectedIndex == 0) //海康
            {

                if (bSound)
                {
                    if (HikPlayer.PlayM4_StopSound())
                    {
                        bSound = false;
                        this.btnMute.Image = Properties.Resources.sound_high;
                    }
                }
                else
                {
                    if (HikPlayer.PlayM4_PlaySound(nPort))
                    {
                        bSound = true;
                        this.btnMute.Image = Properties.Resources.sound_mute;
                    }
                }
            }
            else if (combType.SelectedIndex == 1) //大华
            {


            }
        }



        private void PlayOrPause()
        {

            if (!bOpen)
                return;

            if (enumState == VIDEO_PLAY_STATE.State_Play)
            {
                Pause();
                // Console.WriteLine("*************PAUSE");
                this.btnPlay.Image = Properties.Resources.play_blue;
            }
            else if (enumState == VIDEO_PLAY_STATE.State_Pause ||
                enumState == VIDEO_PLAY_STATE.State_Stop ||
                enumState == VIDEO_PLAY_STATE.State_Close)
            {
                Play();
                // Console.WriteLine("*************PLAY");
                this.btnPlay.Image = Properties.Resources.pause_blue;
            }

        }

        private void Open()
        {
            if (combType.SelectedIndex == 0) //海康
            {
                Close();
                try
                {
                    OpenFile();
                    bOpen = true;

                    HikPlayer.PlayM4_SetDeflash(nPort, bDeflash);
                    //如果CPU处理速度较快，那么第一帧还没播放，就执行PlayM4_GetPictureSize，会导致获取视频大小失败。
                    System.Threading.Thread.Sleep(100);
                    HikPlayer.PlayM4_GetPictureSize(nPort, out nWidth, out nHeight);

                    // 如果视频格式是 HCIF, 那么高度要乘以2
                    if ((nWidth == HikPlayer.WIDTH_PAL * 2) && (nHeight <= HikPlayer.HEIGHT_PAL))
                        nHeight *= 2;

                    //InitWindowSize(100);
                }
                catch
                {
                    Close();
                }
            }
            else if (combType.SelectedIndex == 1) //大华
            {
                Close();
                try
                {
                    OpenFile();
                    bOpen = true;
                }
                catch
                {
                    Close();
                }

            }
            else
            {
                MessageBoxEx.Show("不支持的视频格式，请用厂家的播放器播放");
            }
        }

        private void Close()
        {
            if (bOpen)
            {
                CloseFile();
            }
        }

        private void ViewFullScreen()
        {
            bFullScreen = !bFullScreen;
            if (bFullScreen)
            {

            }
            else
            {

            }

            HikPlayer.PlayM4_RefreshPlay(nPort);
        }

        private void ViewZoom(int nItem)
        {
            if (bFullScreen)
            {
                ViewFullScreen();
            }

            switch (nItem)
            {
                case 50:
                    InitWindowSize(50);
                    break;
                case 200:
                    InitWindowSize(200);
                    break;
                case 100:
                default:
                    InitWindowSize(100);
                    break;
            }
        }

        private void ThrowBFrame(int iFrame)
        {
            HikPlayer.PlayM4_ThrowBFrameNum(nPort, iFrame);
        }
        //播放状态
        private void SetPlayingStatus()
        {
            btnPlay.Enabled = true;
            btnStop.Enabled = true;
            btnFast.Enabled = true;
            btnSlow.Enabled = true;
            btnPlayNormal.Enabled = true;
            btnFrame.Enabled = true;
            btnCapture.Enabled = true;
            btnMute.Enabled = true;
            cSliderVolume.Enabled = true;
            btnPlay.Image = Properties.Resources.pause_blue;
        }
        private void SetOpenStatus()
        {
            btnPlay.Enabled = true;
            btnStop.Enabled = false;
            btnFast.Enabled = false;
            btnSlow.Enabled = false;
            btnPlayNormal.Enabled = false;
            btnFrame.Enabled = false;
            btnCapture.Enabled = false;
            btnMute.Enabled = false;
            cSliderVolume.Enabled = false;
            btnPlay.Image = Properties.Resources.play_blue;

        }
        private void SetStopedStatus()
        {
            btnPlay.Enabled = true;
            btnStop.Enabled = false;
            btnFast.Enabled = false;
            btnSlow.Enabled = false;
            btnPlayNormal.Enabled = false;
            btnFrame.Enabled = false;
            btnCapture.Enabled = false;
            btnMute.Enabled = false;
            cSliderVolume.Enabled = false;
            btnPlay.Image = Properties.Resources.play_blue;
            cSliderTime.Value = 0;
            timer1.Enabled = false;
            this.tsslblFrame.Text = string.Format("帧数：{0,5} / {1,5}", 0, 0);
            this.tsslblTime.Text = string.Format("时间：{0} / {1}", FormatTimeStatus(0), FormatTimeStatus(0));
            nSpeed = 0;
            FormatSpeedStatus();

        }
        private void SetPauseStatus()
        {
            btnPlay.Enabled = true;
            btnStop.Enabled = true;
            btnFast.Enabled = true;
            btnSlow.Enabled = true;
            btnPlayNormal.Enabled = true;
            btnFrame.Enabled = true;
            btnCapture.Enabled = true;
            btnMute.Enabled = true;
            cSliderVolume.Enabled = true;
            btnPlay.Image = Properties.Resources.play_blue;
        }
        #endregion

        #region 方法

        private void initFileListCaption()
        {
            //lsvFileList.Clear();

            //System.Windows.Forms.ColumnHeader columnHeader1 = new System.Windows.Forms.ColumnHeader();
            //System.Windows.Forms.ColumnHeader columnHeader2 = new System.Windows.Forms.ColumnHeader();
            //System.Windows.Forms.ColumnHeader columnHeader3 = new System.Windows.Forms.ColumnHeader();

            //columnHeader1.Text = "文件名称";
            //columnHeader2.Text = "创建时间";
            //columnHeader3.Text = "大小";
            //columnHeader1.Width = 150;
            //columnHeader2.Width = 65;
            //columnHeader3.Width = 65;

            //columnHeader1.TextAlign = HorizontalAlignment.Center;
            //columnHeader2.TextAlign = HorizontalAlignment.Center;
            //columnHeader3.TextAlign = HorizontalAlignment.Center;
            //lsvFileList.Border.Class = "ListViewBorder";
            //lsvFileList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            //        columnHeader1,
            //        columnHeader2,
            //        columnHeader3,

            //});
            //lsvFileList.FullRowSelect = true;

            //i++;
            //ListViewItem listViewItem = new ListViewItem(new string[]
            //    {
            //        i.ToString("000"),
            //        file.StartTime.ToString("HH:mm:ss"),
            //        file.StopTime.ToString("HH:mm:ss"),
            //        file.FileSize.ToString()

            //     }, 0);
            //listViewItem.Tag = file;
            //lsvFileList.Items.Add(listViewItem);
        }
        private void SetDisplayRegion(Rectangle pSrcRect, IntPtr hDestWnd)
        {
            if (enumState != VIDEO_PLAY_STATE.State_Pause)
            {
                return;
            }
            HikPlayer.PlayM4_SetDisplayRegion(nPort, 0, ref pSrcRect, hDestWnd, true);
            Win32API.UpdateWindow(this.Handle);
            HikPlayer.PlayM4_RefreshPlayEx(nPort, 0);
        }

        private void InitWindowSize(int p)
        {
            int width = nWidth;
            int height = nHeight;

            switch (p)
            {
                case 50:
                    width = nWidth / 2;
                    height = nHeight / 2;
                    break;
                case 200:
                    width = nWidth * 2;
                    height = nHeight * 2;
                    break;
            }

            //用Spy++测得主窗体比pVideo控件宽8px,高116px
            this.Width = width + 8;
            this.Height = height + 116;

            this.pVideo.Width = width;
            this.pVideo.Height = height;

            //在中央显示
            this.Left = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;
            this.Top = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;
        }

        private void UpdateStatus()
        {
            if (combType.SelectedIndex == 0) //海康
            {
                int nCurrentTime = HikPlayer.PlayM4_GetPlayedTime(nPort);
                int nCurrentFrame = HikPlayer.PlayM4_GetCurrentFrameNum(nPort);
                float nScrollPos = HikPlayer.PlayM4_GetPlayPos(nPort) * 100;
                //int nScrollPos = nCurrentFrame * HikPlayer.PLAYER_SLIDER_MAX / (dwTotalFrames - 1);
                //if (nPrePlayPos == nScrollPos)
                //    return;
                nPrePlayPos = (int)nScrollPos;
                //Console.WriteLine("************11*" + nScrollPos.ToString());
                //Console.WriteLine("************22*" + nPrePlayPos.ToString());
                this.cSliderTime.Value = (int)nScrollPos;
                this.cSliderTime.Invalidate();

                this.tsslblFrame.Text = string.Format("帧数：{0,5} / {1,5}", nCurrentFrame + 1, dwTotalFrames);
                this.tsslblTime.Text = string.Format("时间：{0} / {1}", FormatTimeStatus(nCurrentTime), FormatTimeStatus(dwMaxFileTime));

                if (nScrollPos >= 100)
                {
                    //停止播放
                    // HikPlayer.PlayM4_SetFileEndMsg(nPort, pVideo.Handle, 0);
                    bSound = HikPlayer.PlayM4_StopSound();
                    HikPlayer.PlayM4_Stop(nPort);
                    enumState = VIDEO_PLAY_STATE.State_Stop;

                    this.timer1.Enabled = false;
                    this.btnPlay.Image = Properties.Resources.play_blue;
                    //this.tsslblStatus.Text = "停止";
                }
            }
            else if (combType.SelectedIndex == 1) //大华
            {
                //FRAME_INFO pFrameInfo = new FRAME_INFO();
                uint totalFrames = DHPlay.DHPlayControl(PLAY_COMMAND.GetFileTotalFrames, 0, true);
                // DHPlay.DHPlayControl(PLAY_COMMAND.GetPictureSize, 0, ref pFrameInfo);
                uint currentFrame = DHPlay.DHPlayControl(PLAY_COMMAND.GetCurrentFrameNum, 0, true);
                uint currentPlayTime = DHPlay.DHPlayControl(PLAY_COMMAND.GetPlayedTime, 0, true);
                uint TotalTime = DHPlay.DHPlayControl(PLAY_COMMAND.GetFileTotalTime, 0, true);

                // stlTotalFrames.Text = Convert.ToString(totalFrames);
                this.cSliderTime.Maximum = (int)(totalFrames > 0 ? totalFrames : 0);
                this.cSliderTime.Value = (int)(currentFrame < cSliderTime.Maximum ? currentFrame : 0);
                this.tsslblFrame.Text = string.Format("帧数：{0,5} / {1,5}", currentFrame + 1, totalFrames);
                this.tsslblTime.Text = string.Format("时间：{0} / {1}", FormatTimeStatus((int)currentPlayTime), FormatTimeStatus((int)TotalTime));



            }
        }

        private string FormatSpeedStatus()
        {
            string dwValue = "速度";
            switch (nSpeed)
            {
                case 1:
                    dwValue += " X2"; break;
                case 2:
                    dwValue += " X4"; break;
                case 3:
                    dwValue += " X8"; break;
                case 4:
                    dwValue += " X16"; break;
                case -1:
                    dwValue += " /2"; break;
                case -2:
                    dwValue += " /4"; break;
                case -3:
                    dwValue += " /8"; break;
                case -4:
                    dwValue += " /16"; break;
                case 0:
                default:
                    dwValue += " X1"; break;
            }
            return dwValue;
        }

        private string FormatTimeStatus(int nSeconds)
        {
            int mHour = nSeconds / 3600;
            int mMinute = (nSeconds % 3600) / 60;
            int mSecond = nSeconds % 60;
            return string.Format("{0:00}:{1:00}:{2:00}", mHour, mMinute, mSecond);
        }

        #endregion

        #region  事件
        private void btnEject_Click(object sender, EventArgs e)
        {
            if (BrowseFile())
            {
                Open();
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            PlayOrPause();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void btnSlow_Click(object sender, EventArgs e)
        {
            Play();
            Slow();
            this.tsslblSpeed.Text = FormatSpeedStatus();
        }

        private void btnPlayNormal_Click(object sender, EventArgs e)
        {
            RestoreSpeed();
            this.tsslblSpeed.Text = FormatSpeedStatus();
        }

        private void btnFast_Click(object sender, EventArgs e)
        {
            Play();
            Fast();
            this.tsslblSpeed.Text = FormatSpeedStatus();
        }

        private void btnFrame_Click(object sender, EventArgs e)
        {
            StepForward();
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            Cappic();
        }

        private void btnMute_Click(object sender, EventArgs e)
        {
            Sound();
        }


        private void NHikPlayerControl_Load(object sender, EventArgs e)
        {


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateStatus();
        }

        private void cSliderTime_MouseDown(object sender, MouseEventArgs e)
        {
            timer1.Enabled = false;
        }

        private void cSliderTime_MouseUp(object sender, MouseEventArgs e)
        {
            if (combType.SelectedIndex == 0) //海康
            {
                int nPlayPos = this.cSliderTime.Value;
                //if (Math.Abs(nPlayPos - nPrePlayPos) < 3)
                //    return;

                if (bFileRefCreated)
                {
                    HikPlayer.PlayM4_SetPlayPos(nPort, (float)nPlayPos / (float)HikPlayer.PLAYER_SLIDER_MAX);
                }
                else
                {
                    int nTime = nPlayPos * 1000 / HikPlayer.PLAYER_SLIDER_MAX * dwMaxFileTime;
                    HikPlayer.PlayM4_SetPlayedTimeEx(nPort, nTime);
                }
                timer1.Enabled = true;
            }
            else if (combType.SelectedIndex == 1)
            {
                int nPlayPos = this.cSliderTime.Value;
                //if (Math.Abs(nPlayPos - nPrePlayPos) < 3)
                //    return;
                DHPlay.DHPlayControl(PLAY_COMMAND.ReSume, 0);
                SetPlayingStatus();
                DHPlay.DHPlayControl(PLAY_COMMAND.SetCurrentFrameNum, 0, (uint)(nPlayPos));
                timer1.Enabled = true;

            }

        }

        private void cSliderVolume_ValueChanged(object sender, EventArgs e)
        {

        }

        private void expPanelList_Click(object sender, EventArgs e)
        {

        }

        private void pVideo_Click(object sender, EventArgs e)
        {

        }

        private void panelExTop_Click(object sender, EventArgs e)
        {

        }

        private void btnFileListAdd_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtRecDir.Text = folderBrowserDialog1.SelectedPath;
                tempstr = folderBrowserDialog1.SelectedPath;
                thdAddFile = new Thread(new ThreadStart(SetAddFile));   //创建一个线程
                thdAddFile.IsBackground = true;
                thdAddFile.Start(); //执行当前线程
            }

        }
        #endregion

        #region 文件列表
        public static string tempstr = "";
        // string Tem_Dir = "";
        private System.Threading.Thread thdAddFile; //创建一个线程
        //private System.Threading.Thread thdOddDocument; //创建一个线程
        public static TreeNode TN_Docu = new TreeNode();//单个文件的节点
        private static TreeView Tem_TView;

        public delegate void AddFile();//定义托管线程
        /// <summary>
        /// 设置托管线程
        /// </summary>
        public void SetAddFile()
        {
            this.Invoke(new AddFile(RunAddFile));//对指定的线程进行托管
        }

        /// <summary>
        /// 设置线程
        /// </summary>
        public void RunAddFile()
        {
            TreeNode TNode = new TreeNode();//实例化一个线程

            Files_Copy(treeView1, tempstr, TNode, 0);
            treeView1.ExpandAll();
            try
            {
                Thread.Sleep(0);//持起主线程
                thdAddFile.Abort();//执行线程    
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

        }

        #region  返回上一级目录
        /// <summary>
        /// 返回上一级目录
        /// </summary>
        /// <param dir="string">目录</param>
        /// <returns>返回String对象</returns>
        public string UpAndDown_Dir(string dir)
        {
            string Change_dir = "";
            Change_dir = Directory.GetParent(dir).FullName;
            return Change_dir;
        }
        #endregion

        #region  显示文件夹下所有子文件夹及文件的名称
        /// <summary>
        /// 显示文件夹下所有子文件夹及文件的名称
        /// </summary>
        /// <param Sdir="string">文件夹的目录</param>
        /// <param TNode="TreeNode">节点</param>
        /// <param n="int">标识，判断当前是文件夹，还是文件</param>
        private void Files_Copy(TreeView TV, string Sdir, TreeNode TNode, int n)
        {
            DirectoryInfo dir = new DirectoryInfo(Sdir);
            try
            {
                if (!dir.Exists)//判断所指的文件或文件夹是否存在
                {
                    return;
                }
                DirectoryInfo dirD = dir as DirectoryInfo;//如果给定参数不是文件夹则退出
                if (dirD == null)//判断文件夹是否为空
                {
                    return;
                }
                else
                {
                    if (n == 0)
                    {
                        TNode = TV.Nodes.Add(dirD.Name);//添加文件夹的名称
                        TNode.Tag = dirD;
                        TNode.ImageIndex = 0;
                        TNode.SelectedImageIndex = 1;
                    }
                    else
                    {
                        TNode = TNode.Nodes.Add(dirD.Name);//添加文件夹里面各文件夹的名称
                        TNode.Tag = dirD;
                        TNode.ImageIndex = 0;
                        TNode.SelectedImageIndex = 1;
                    }
                }
                FileSystemInfo[] files = dirD.GetFileSystemInfos();//获取文件夹中所有文件和文件夹
                //对单个FileSystemInfo进行判断,如果是文件夹则进行递归操作
                foreach (FileSystemInfo FSys in files)
                {
                    FileInfo file = FSys as FileInfo;
                    if (file != null)//如果是文件的话，进行文件的复制操作
                    {
                        FileInfo SFInfo = new FileInfo(file.DirectoryName + "\\" + file.Name);//获取文件所在的原始路径
                        if (SFInfo.Extension.ToLower() == ".mp4" || SFInfo.Extension.ToLower() == ".264" || SFInfo.Extension.ToLower() == ".ts")
                        {
                            TreeNode newNode = TNode.Nodes.Add(file.Name);//添加文件
                            newNode.Tag = SFInfo;
                            newNode.ImageIndex = 2;
                            newNode.SelectedImageIndex = 2;
                        }
                    }
                    else
                    {
                        //string pp = FSys.Name;//获取当前搜索到的文件夹名称
                        //Files_Copy(TV, Sdir + "\\" + FSys.ToString(), TNode, 1);//如果是文件夹，则进行递归调用
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        private void btnRemoveFolder_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                treeView1.Nodes.Remove(treeView1.SelectedNode);
            }
        }
        private void btnFileListClear_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
        }

        private void btnFileListSearch_Click(object sender, EventArgs e)
        {
            if (txtRecDir.Text.Trim() != "")
            {
                treeView1.Nodes.Clear();
                tempstr = txtRecDir.Text;
                thdAddFile = new Thread(new ThreadStart(SetAddFile));   //创建一个线程
                thdAddFile.IsBackground = true;
                thdAddFile.Start(); //执行当前线程
            }
            else
            {
                MessageBox.Show("请选择文件夹后再查找！");
            }

        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            FileInfo SFInfo = e.Node.Tag as FileInfo;
            if (SFInfo != null)
            {
                if (SFInfo.Name.Length > 12)
                    lblFileName.Text = SFInfo.Name.Substring(0, 12) + "...";
                else
                    lblFileName.Text = SFInfo.Name;
                strPlayFileName = SFInfo.FullName;
                Stop();
                Open();
            }

        }


        #endregion


        #endregion


    }
}
