using GHIBMS.Common;
using SSNetSdk;
using System;
using System.Collections;
using System.IO;

namespace GHIBMS.NetVideo
{
    public class VIKORRealPlayer : IVideoRealPlayer, IDisposable
    {

        #region 内部变量与属性
        private static IClient client = null;
        public static Hashtable logidTable = new Hashtable();
        private static UInt16 videoWndNubs = 1;
        private static System.Threading.Timer aliveTimer;

        private VideoRealPlayArgs playArgs = new VideoRealPlayArgs();
        private IRealPlayer realPlayer = null;
        //private bool bRecording = false;
        //private int  VoiceComHandle = -1;

        private string protocolCode = "JK_PLAT_VIKOR";
        public string ProtocolCode
        {
            get { return protocolCode; }

        }


        public UInt16 VideoWndNubs
        {
            set { videoWndNubs = value; }
            get { return videoWndNubs; }
        }
        //public int PlayID
        //{
        //    get { return playID; }
        //    set { playID = value; }
        //}
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
        public VIKORRealPlayer()
        {
            // voiceDataCallBack = new HCNetSDK.VoiceDataCallBack(fVoiceDataCallBack);
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
        ~VIKORRealPlayer()
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
            //创建IClient实例
            if (client == null)
            {
                client = ObjectFactory.CreateClient();
                VideoRealPlayArgs e = playArgs;
                client.Login(e.Ip, e.Port, e.UserName, e.Password);
            }
            //保持心跳
            if (aliveTimer == null)
            {
                aliveTimer = new System.Threading.Timer(delegate (object state)
                {
                    if (client != null)
                        client.KeepAlive();
                }, aliveTimer, 2000, 25000);
            }
            return true;
        }
        //清理资源
        public void DVR_Cleanup()
        {
            if (client != null)
            {
                client.Logout();
                client = null;
            }
        }


        #endregion

        #region 设备注册


        public static bool VIDEO_Logout()
        {
            if (client != null)
                client.Logout();
            return true;
        }
        #endregion

        #region 视频预览
        public bool VIDEO_StartRealPlay()
        {

            try
            {
                if (realPlayer != null)
                {
                    VIDEO_StopRealPlay();
                }


                VIDEO_Init();
                if (client != null)
                {
                    if (realPlayer == null)
                        realPlayer = client.CreateRealPlayer(playArgs.PlayWnd);
                    realPlayer.Play(playArgs.CamID, PlayStreamz.Auto);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return true;

        }
        public bool VIDEO_StopRealPlay()
        {
            if (realPlayer != null)
            {
                realPlayer.Stop();
            }
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
            return true;
        }
        public bool VIDEO_CaptureJPEGPicture(string fileName)
        {

            return true;
        }

        #endregion

        #region 录像文件回放、下载

        #endregion

        #region 云台控制
        public bool VIDEO_PTZControlWithSpeed(uint dwPTZCommand, uint dwStop, uint dwSpeed)
        {
            PtzActionz vcode;
            PTZCmdCodeEnum code = (PTZCmdCodeEnum)dwPTZCommand;
            switch (code)
            {
                case PTZCmdCodeEnum.PAN_LEFT:
                    vcode = PtzActionz.turn_left;
                    break;
                case PTZCmdCodeEnum.PAN_RIGHT:
                    vcode = PtzActionz.turn_right;
                    break;
                case PTZCmdCodeEnum.TILT_DOWN:
                    vcode = PtzActionz.turn_down;
                    break;
                case PTZCmdCodeEnum.TILT_UP:
                    vcode = PtzActionz.turn_up;
                    break;
                case PTZCmdCodeEnum.ZOOM_IN:
                    vcode = PtzActionz.zoom_in;
                    break;
                case PTZCmdCodeEnum.ZOOM_OUT:
                    vcode = PtzActionz.zoom_out;
                    break;
                default:
                    vcode = PtzActionz.turn_left;
                    break;
            }
            bool vAct = (dwStop == 0 ? true : false);



            if (realPlayer != null)
            {
                realPlayer.PtzControl(vcode, vAct, (int)dwSpeed * 36, 0);
            }
            return true;
        }
        public bool VIDEO_PTZPreset(uint dwPTZCommand, uint PresetIndex)
        {
            PtzActionz vcode;
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
            }
            if (realPlayer != null)
            {
                realPlayer.PtzControl(vcode, true, (int)PresetIndex, 0);
            }
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
