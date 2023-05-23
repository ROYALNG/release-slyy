using System;

namespace GHIBMS.VideoPlayback
{
    /// <summary>
    /// 播放状态
    /// </summary>
    public enum VIDEO_PLAY_STATE
    {
        State_Close = 0,
        State_Open = 1,
        State_Stop = 2,
        State_Play = 3,
        State_Pause = 4,
        State_Record = 5,
    }
    public enum DvrFileTypeEnum
    {
        全部 = 0xff,
        定时录像 = 0,
        移动侦测 = 1,
        报警触发 = 2,
        报警触发或移动侦测 = 3,
        报警触发和移动侦测 = 4,
        命令触发 = 5,
        手动录像 = 6,
        智能录像 = 7

    }
    public enum PlaybackModeEnum
    {
        按文件,
        按时间,
        按事件,
        智能
    }
    public enum TransferTypeEnum
    {
        DVR直播,
        一级VOD,
        二级VOD
    }

    public enum VideoSplitNubEnum
    {
        画面1_1 = 0,
        画面2_2,
        画面3_2,
        画面3_3,
        画面4_3,
        画面4_4,
        画面5_4,
        画面5_5,
        画面6_5,
        画面6_6
    }
    public enum PicQualityEnum
    {
        最好 = 0,
        较好 = 1,
        一般 = 2
    }
    public enum PicSizeEnum
    {
        CIF = 0,
        QCIF = 1,
        D1 = 2,
        UXGA = 32,
        SVGA = 4,
        HD720p = 5,
        VGA = 6,
        XVGA = 7,
        HD900p = 8,
        HD1080 = 9,
        W2560_1920 = 10,
        W1600_304 = 11,
        W2048_1536 = 12,
        W2448_2048 = 13

    }
    public class VideoPlaybackArgs
    {
        private string camName = "";
        private string ip = "";
        private ushort port = 8000;
        private int dvrCh = 1;
        private string userName = "admin";
        private string password = "123456";
        private string protocolCode_ = "";
        private TransferTypeEnum vodMode = TransferTypeEnum.DVR直播;
        private IntPtr playWnd;
        private UInt32 throwBFrame = 1;
        private string camID = "";
        private string vodIp = "";
        private int vodPort = 556;
        private DateTime startTime;
        private DateTime endTime;
        //private string platIp = "";
        //private int platPort = 0;

        public string CamName
        {
            get { return camName; }
            set { camName = value; }
        }
        public string CamID
        {
            get { return camID; }
            set
            {
                camID = value;
            }
        }
        public string Ip
        {
            set { ip = value; }
            get { return ip; }
        }
        public ushort Port
        {
            set { port = value; }
            get { return port; }
        }
        public int DvrCh
        {
            set { dvrCh = value; }
            get { return dvrCh; }
        }
        public string UserName
        {
            set { userName = value; }
            get { return userName; }
        }
        public string Password
        {
            set { password = value; }
            get { return password; }
        }

        public TransferTypeEnum VodMode
        {
            set { vodMode = value; }
            get { return vodMode; }
        }
        public IntPtr PlayWnd
        {
            set { playWnd = value; }
            get { return playWnd; }
        }

        public UInt32 ThrowBFrame
        {
            set { throwBFrame = value; }
            get { return throwBFrame; }
        }
        public string ProtocolCode
        {
            set { protocolCode_ = value; }
            get { return protocolCode_; }

        }
        public string VODIp
        {
            set { vodIp = value; }
            get { return vodIp; }
        }
        public int VODPort
        {
            set { vodPort = value; }
            get { return vodPort; }
        }
        public DateTime StartTime
        {
            set { startTime = value; }
            get { return startTime; }
        }
        public DateTime EndTime
        {
            set { endTime = value; }
            get { return endTime; }
        }

        private string _Platipaddress = "";
        /// <summary>
        /// IP地址
        /// </summary>

        public string PlatIpAddress
        {
            get { return _Platipaddress; }
            set { _Platipaddress = value; }
        }
        private int platport = 80;
        /// <summary>
        /// 网络端口号
        /// </summary>

        public int PlatPort
        {
            set { platport = value; }
            get { return platport; }
        }

        private string platUsername = "";
        /// <summary>
        /// 登录用户名
        /// </summary>

        public string PlatUsername
        {
            get { return platUsername; }
            set { platUsername = value; }
        }
        private string platPassword = "12345";
        /// <summary>
        /// 登录密码
        /// </summary>


        public string PlatPassword
        {
            get { return platPassword; }
            set { platPassword = value; }
        }
        private string _SerialNumber = "";


        /// <summary>
        /// 摄像机在平台中的资源码
        /// </summary>
        public string SerialNumber
        {
            get { return _SerialNumber; }
            set { _SerialNumber = value; }
        }

        public void Clone(VideoPlaybackArgs args)
        {
            this.camName = args.camName; ;
            this.ip = args.ip;
            this.port = args.port;
            this.dvrCh = args.dvrCh;
            this.userName = args.userName;
            this.password = args.password;
            this.vodMode = args.vodMode;
            this.protocolCode_ = args.protocolCode_;
            this.playWnd = args.playWnd;
            this.throwBFrame = args.throwBFrame;
            this.camID = args.camID;
            this.vodIp = args.vodIp;
            this.vodPort = args.vodPort;
            this.startTime = args.startTime;
            this.endTime = args.endTime;
            this.PlatIpAddress = args.PlatIpAddress;
            this.platPassword = args.platPassword;
            this.platport = args.platport;
            this.PlatUsername = args.PlatUsername;
            this.SerialNumber = args.SerialNumber;

        }
    }

}
