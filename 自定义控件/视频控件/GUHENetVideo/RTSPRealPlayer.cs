using DvrSDK;
using GHIBMS.Common;
using System;
using System.IO;
using System.Windows.Forms;

namespace GHIBMS.NetVideo
{
    public class RTSPRealPlayer : IVideoRealPlayer, IDisposable
    {

        #region 内部变量与属性
        private VlcPlayer vlc_player_;
        private static UInt16 videoWndNubs = 1;
        private VideoRealPlayArgs playArgs = new VideoRealPlayArgs();
        private int playID = -1;

        private PTZController onvifPtz;

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
            set { playID = value; }
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
        public RTSPRealPlayer()
        {
            vlc_player_ = new VlcPlayer();
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
        ~RTSPRealPlayer()
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
            vlc_player_.SetRenderWindow((int)playArgs.PlayWnd);

        }
        public bool VIDEO_Init()
        {
            return true;
        }
        //清理资源
        public void DVR_Cleanup()
        {
        }


        #endregion

        #region 设备注册
        private bool DVR_Login()
        {
            try
            {
                VIDEO_Init();
                VideoRealPlayArgs e = playArgs;
                if (e.Ip == "0.0.0.0" || e.Ip == "")
                {
                    Log("实时视频播放失败，没有正确设置播放参数！");
                    return false;

                }

                Login();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            return true;
        }

        public static bool DVR_Logout()
        {
            return true;
        }
        private bool Login()
        {


            return true;
        }


        void ThreadProc(object StartInfo)
        {


        }


        #endregion

        #region 视频预览
        public bool VIDEO_StartRealPlay()
        {
            if (playID == 0)
                vlc_player_.Stop();
            string[] args = playArgs.CamID.Split('|');

            bool ret = vlc_player_.PlayUrl(args[0]);
            playID = ret ? 0 : -1;
            if (args.Length == 3)
            {
                int profileId = 0;
                if (int.TryParse(args[2], out profileId))
                    onvifPtz = new PTZController(args[1], profileId, playArgs.UserName, playArgs.Password, 20);
            }
            return ret;

        }
        public bool VIDEO_StopRealPlay()
        {
            vlc_player_.Stop();
            playID = -1;
            if (onvifPtz != null)
                onvifPtz.ResetONVIF();
            return true;
        }



        #endregion

        #region 抓拍与录像
        public bool VIDEO_SaveRealData(string fileName)
        {
            return false;
        }
        public bool VIDEO_StopSaveRealData()
        {
            return true;
        }
        public bool VIDEO_CapturePicture(string fileName)
        {
            VIDEO_CaptureJPEGPicture(fileName);
            return true;
        }
        public bool VIDEO_CaptureJPEGPicture(string fileName)
        {
            string time = System.DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string path;
            if (!NetVideoControl.PicPath.EndsWith("\\"))
                path = NetVideoControl.PicPath + "\\" + pubFun.DateStr + "\\";
            else
                path = NetVideoControl.PicPath + pubFun.DateStr + "\\";
            string date = pubFun.DateStr;
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
            string sPicFileName = String.Format("{0}{1}_{2}_{3}.png", path, fileName, playArgs.Ip, time);
            vlc_player_.TakeSnapShot(0, sPicFileName);

            //pubFun.ShowFileDirectory(sPicFileName);
            return true;
        }

        #endregion

        #region 录像文件回放、下载

        #endregion

        #region 云台控制
        public bool VIDEO_PTZControlWithSpeed(uint dwPTZCommand, uint dwStop, uint dwSpeed)
        {
            if (onvifPtz == null) return false;

            onvifPtz.Speed = (int)dwSpeed * 5;

            //PtzActionz vcode;
            PTZCmdCodeEnum code = (PTZCmdCodeEnum)dwPTZCommand;
            //switch (code)
            //{
            //    case PTZCmdCodeEnum.PAN_LEFT:
            //        vcode = PtzActionz.turn_left;
            //        break;
            //    case PTZCmdCodeEnum.PAN_RIGHT:
            //        vcode = PtzActionz.turn_right;
            //        break;
            //    case PTZCmdCodeEnum.TILT_DOWN:
            //        vcode = PtzActionz.turn_down;
            //        break;
            //    case PTZCmdCodeEnum.TILT_UP:
            //        vcode = PtzActionz.turn_up;
            //        break;
            //    case PTZCmdCodeEnum.ZOOM_IN:
            //        vcode = PtzActionz.zoom_in;
            //        break;
            //    case PTZCmdCodeEnum.ZOOM_OUT:
            //        vcode = PtzActionz.zoom_out;
            //        break;
            //    default:
            //        vcode = PtzActionz.turn_left;
            //        break;
            //}
            bool vAct = (dwStop == 0 ? true : false);
            if (!vAct)
                onvifPtz.SendPTZCommand(OnvifPtzCommand.Stop);
            else
                onvifPtz.SendPTZCommand(code);

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
}
