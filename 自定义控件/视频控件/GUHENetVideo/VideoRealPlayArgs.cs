using System;

namespace GHIBMS.NetVideo
{
    /// <summary>
    /// 播放状态
    /// </summary>
    public enum VIDEO_PLAY_STATE
    {
        State_Close = 0,
        State_Play = 1,
        State_Pause = 2,
        State_Stop = 3,
        State_Record = 4

    }
    public enum TCPModeEnum
    {
        TCP = 0,
        UDP,
        MULTI,
        RTP
    }
    public enum EncodeTypeEnum
    {
        主码流 = 0,
        子码流
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

    //0-CIF，1-QCIF，2-D1，3-UXGA，4-SVGA，5-HD720p，6-VGA，7-XVGA，8-HD900p，9-HD1080，10-2560*1920，11-1600*304，12-2048*1536，13-2448*2048 

    public class VideoRealPlayArgs
    {
        private string camName = "";
        private string ip = "";
        private ushort port = 8000;
        private int dvrCh = 1;
        private string userName = "admin";
        private string password = "123456";
        private TCPModeEnum tcpMode = TCPModeEnum.TCP;
        private TransferTypeEnum vodMode = TransferTypeEnum.DVR直播;
        private EncodeTypeEnum encodeMode = EncodeTypeEnum.主码流;
        private string protocolCode = "";
        private IntPtr playWnd;
        private int playWndNo;
        private string multiCastIP = "";
        private UInt32 throwBFrame = 1;
        private string monName = "";
        private string camID = "1";

        private string vodIp = "";
        private int vodPort = 556;
        private string alarmVarialbe = "";



        public string CamName
        {
            get { return camName; }
            set { camName = value; }
        }
        public string MonName
        {
            get { return monName; }
            set { monName = value; }
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
        public TCPModeEnum TCPMode
        {
            set { tcpMode = value; }
            get { return tcpMode; }
        }
        public TransferTypeEnum VodMode
        {
            set { vodMode = value; }
            get { return vodMode; }
        }
        public EncodeTypeEnum EncodeMode
        {
            set { encodeMode = value; }
            get { return encodeMode; }
        }

        public IntPtr PlayWnd
        {
            set { playWnd = value; }
            get { return playWnd; }
        }
        public int PlayWndNo
        {
            set { playWndNo = value; }
            get { return playWndNo; }
        }
        public string MultiCastIP
        {
            set { multiCastIP = value; }
            get { return multiCastIP; }
        }
        public UInt32 ThrowBFrame
        {
            set { throwBFrame = value; }
            get { return throwBFrame; }
        }
        public string ProtocolCode
        {
            set { protocolCode = value; }
            get { return protocolCode; }

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
        public string AlarmVariable
        {
            set { alarmVarialbe = value; }
            get { return alarmVarialbe; }
        }

        #region 2016-5-26增加了平台

        #endregion



        public void Clone(VideoRealPlayArgs args)
        {
            this.camName = args.CamName;
            this.monName = args.monName;
            this.ip = args.Ip;
            this.port = args.Port;
            this.dvrCh = args.DvrCh;
            this.userName = args.UserName;
            this.password = args.Password;
            this.tcpMode = args.TCPMode;
            this.vodMode = args.VodMode;
            this.encodeMode = args.EncodeMode;
            this.protocolCode = args.ProtocolCode;
            this.playWnd = args.PlayWnd;
            this.multiCastIP = args.MultiCastIP;
            this.throwBFrame = args.ThrowBFrame;
            this.camID = args.CamID;
            this.vodIp = args.VODIp;
            this.vodPort = args.VODPort;
            this.alarmVarialbe = args.alarmVarialbe;



        }
    }

}
