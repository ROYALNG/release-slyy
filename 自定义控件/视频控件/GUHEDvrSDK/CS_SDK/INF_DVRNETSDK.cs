
using System;
using System.Runtime.InteropServices;


namespace GHIBMS.INFDVRSDK
{

    public static class INF_DVRNETSDK
    {
        #region 委托

        //********************回调函数声明***************************
        //关于实时视频数据的回调函数
        //C++ TO C# CONVERTER TODO TASK: The original C++ function pointer contained an unconverted modifier:
        //ORIGINAL LINE: typedef void(CALLBACK *fRealDataCallBack)(int lRealHandle, uint dwDataType, sbyte *pBuffer, uint dwBufSize, IntPtr pUser);
        public delegate void fRealDataCallBack(int lRealHandle, uint dwDataType, string pBuffer, uint dwBufSize, IntPtr pUser);

        //关于实时视频标准码流数据的回调函数
        //C++ TO C# CONVERTER TODO TASK: The original C++ function pointer contained an unconverted modifier:
        //ORIGINAL LINE: typedef void(CALLBACK *fStdDataCallBack)(int lRealHandle, uint dwDataType, sbyte *pBuffer, uint dwBufSize, IntPtr pUser);
        //public delegate void RealDataCallBack_V30(int lRealHandle, uint dwDataType, byte[] pBuffer, uint dwBufSize, IntPtr pUser);

        public delegate void fStdDataCallBack(int lRealHandle, uint dwDataType, IntPtr pBuffer, uint dwBufSize, IntPtr pUser);

        //关于监听的回调函数
        //C++ TO C# CONVERTER TODO TASK: The original C++ function pointer contained an unconverted modifier:
        //ORIGINAL LINE: typedef void(CALLBACK* fAlarmCallBack)(int lAlarmType, IntPtr pAlarmInfo, uint dwBufLen, IntPtr pUser);
        public delegate void fAlarmCallBack(int lAlarmType, IntPtr pAlarmInfo, uint dwBufLen, IntPtr pUser);

        ////接收回放视频数据回调函数
        //typedef void(CALLBACK *fPlayerDataCallBack)(
        //	INF_NET_REPFILEINFO	fileInfo,
        //	BYTE				*byBuffer,			//缓冲区数据指针
        //	DWORD				dwBufSize,			//缓冲区大小
        //	void				*pUser				//用户数据
        //	);

        //音频数据回调函数
        //C++ TO C# CONVERTER TODO TASK: The original C++ function pointer contained an unconverted modifier:
        //ORIGINAL LINE: typedef void(CALLBACK *fVoiceDataCallBack)(int lVoiceHandle, sbyte *pRecvDataBuffer, uint dwBufSize, byte byAudioFlag, IntPtr pUser);
        public delegate void fVoiceDataCallBack(int lVoiceHandle, string pRecvDataBuffer, uint dwBufSize, byte byAudioFlag, IntPtr pUser);

        //关于报警联动录像的回调函数
        //C++ TO C# CONVERTER TODO TASK: The original C++ function pointer contained an unconverted modifier:
        //ORIGINAL LINE: typedef void(CALLBACK *fAlarmRecordCallBack)(sbyte *sDVRIP, int lChannel, uint dwDataType, sbyte *pBuffer, uint dwBufSize, IntPtr pUser);
        public delegate void fAlarmRecordCallBack(string sDVRIP, int lChannel, uint dwDataType, string pBuffer, uint dwBufSize, IntPtr pUser);
        //回放标准码流数据回调函数
        //C++ TO C# CONVERTER TODO TASK: The original C++ function pointer contained an unconverted modifier:
        //ORIGINAL LINE: typedef void(CALLBACK *fPlayDataCallBack)(int lPlayHandle, uint dwDataType, byte *pBuffer, uint dwBufSize, IntPtr pUser);
        public delegate void fPlayDataCallBack(int lPlayHandle, uint dwDataType, ref byte pBuffer, uint dwBufSize, IntPtr pUser);

        //回放流数据回调函数
        //C++ TO C# CONVERTER TODO TASK: The original C++ function pointer contained an unconverted modifier:
        //ORIGINAL LINE: typedef void(CALLBACK *fPlayStreamDataCallBack)(int lPlayHandle, uint dwDataType, byte *pBuffer, uint dwBufSize, IntPtr pUser);
        public delegate void fPlayStreamDataCallBack(int lPlayHandle, uint dwDataType, ref byte pBuffer, uint dwBufSize, IntPtr pUser);

        //手动录像的回调函数
        //C++ TO C# CONVERTER TODO TASK: The original C++ function pointer contained an unconverted modifier:
        //ORIGINAL LINE: typedef void(CALLBACK *fRecordFileCallBack)(int lRealHandle);
        public delegate void fRecordFileCallBack(int lRealHandle);
        //下载回调函数
        //C++ TO C# CONVERTER TODO TASK: The original C++ function pointer contained an unconverted modifier:
        //ORIGINAL LINE: typedef void(CALLBACK *fFileDataCallBack)(int lFileHandle, uint dwDataType, byte *pBuffer, uint dwBufSize, IntPtr pUser);
        public delegate void fFileDataCallBack(int lFileHandle, uint dwDataType, ref byte pBuffer, uint dwBufSize, IntPtr pUser);
        #endregion

        #region 常量
        public const int SERIALNO_LEN = 48;
        public const int NAME_LEN = 15;
        public const int PASSWD_LEN = 15;
        public const int PASSWD_MIN_LEN = 8;
        public const int PPPOE_NAMELEN = 19;
        public const int PPPOE_PWDLEN = 19;
        public const int PATHNAME_LEN = 128;
        public const int CHANNAME_LEN = 32;
        public const int TEXT_LEN = 32;
        public const int STRING_LEN = 288;
        public const int ALARM_LEN = 32;
        public const int OSDWIDTH = 720;
        public const int OSDHEIGHT = 528;
        public const int OSDHETGHTNTSC = 432;
        public const int MAX_USER_NUM = 16;
        public const int WEEK_DAYS = 7;
        public const int TIME_PLANS = 48;
        public const int MAX_AREA = 4;
        public const int MAX_ALARMOUT = 4;
        public const int MAX_TEXT_NUM = 2;
        public const int MAX_DISKNUM = 8;
        public const int MAX_CHANNUM = 16;
        public const int MAX_VOICE_NUM = 1;
        public const int MAX_ALARMTHREAD_NUM = 1;
        public const int MAX_USER = 256;
        public const int MAX_PLAYTHREAD_NUM = 128;
        public const int MAX_FINDFILE = 3;
        public const int MAX_FINDLOG = 3;
        public const int MAX_CODER = 20;
        public const int MAX_DOWNLOAD = 1;
        public const int MAX_REMOTEPLAYBACKWNDNUM = 4;
        public const int MODULE_NET = 1;
        public const int NET_SUB_MODULE_GENERAL = 0;
        public const int NET_SUB_MODULE_LOGIN = 1;
        public const int NET_SUB_MODULE_LISTEN = 2;
        public const int NET_SUB_MODULE_PLAY = 3;
        public const int NET_SUB_MODULE_SEARCH = 4;
        public const int NET_SUB_MODULE_LOG = 5;
        public const int NET_SUB_MODULE_VOICE = 6;
        public const int NET_SUB_MODULE_DEVICE = 7;
        public const int NET_SUB_MODULE_DOWNLOAD = 8;
        public const int NET_SUB_MODULE_PLAYBACK = 9;
        public const int NET_SUB_MODULE_CODE = 10;
        public const int NET_SUB_MODULE_CFG = 11;
        public const int INF_DVR_SET_NETCARD1CFG = 1;
        public const int INF_DVR_SET_NETCARD2CFG = 2;
        public const int INF_DVR_SET_NETPUBLICCFG = 3;
        public const int INF_DVR_SET_CHANINFOCFG = 4;
        public const int INF_DVR_SET_MAJORINFOCFG = 5;
        public const int INF_DVR_SET_MONIRINFOCFG = 6;
        public const int INF_DVR_SET_OSDINFOCFG = 7;
        public const int INF_DVR_SET_MOTIONINFOCFG = 8;
        public const int INF_DVR_SET_OBSTRUCTCFG = 9;
        public const int INF_DVR_SET_SIGLOSTCFG = 10;
        public const int INF_DVR_SET_RECORDCFG = 11;
        public const int INF_DVR_SET_PRIVACYCFG = 12;
        public const int INF_DVR_SET_ALARMINCFG = 13;
        public const int INF_DVR_SET_ALARMOUTCFG = 14;
        public const int INF_DVR_SET_ABNORALARMCFG = 15;
        public const int INF_DVR_SET_COM232CFG = 16;
        public const int INF_DVR_SET_COM485CFG = 17;
        public const int INF_DVR_SET_MANNUALSTATECFG = 18;
        public const int INF_DVR_SET_ADDUSERCFG = 19;
        public const int INF_DVR_SET_MODIFYUSERCFG = 20;
        public const int INF_DVR_SET_DELETEUSERCFG = 21;
        public const int INF_DVR_SET_SYSTIME = 22;
        public const int INF_DVR_GET_DEVICECFG = 51;
        public const int INF_DVR_GET_NETCARD1CFG = 52;
        public const int INF_DVR_GET_NETCARD2CFG = 53;
        public const int INF_DVR_GET_NETPUBLICCFG = 54;
        public const int INF_DVR_GET_CHANINFOCFG = 55;
        public const int INF_DVR_GET_MAJORINFOCFG = 56;
        public const int INF_DVR_GET_MONIRINFOCFG = 57;
        public const int INF_DVR_GET_OSDINFOCFG = 58;
        public const int INF_DVR_GET_MOTIONINFOCFG = 59;
        public const int INF_DVR_GET_OBSTRUCTCFG = 60;
        public const int INF_DVR_GET_SIGLOSTCFG = 61;
        public const int INF_DVR_GET_RECORDCFG = 62;
        public const int INF_DVR_GET_PRIVACYCFG = 63;
        public const int INF_DVR_GET_ALARMINCFG = 64;
        public const int INF_DVR_GET_ALARMOUTCFG = 65;
        public const int INF_DVR_GET_ABNORALARMCFG = 66;
        public const int INF_DVR_GET_CHANSTATECFG = 67;
        public const int INF_DVR_GET_HARDDISKCFG = 68;
        public const int INF_DVR_GET_COM232CFG = 69;
        public const int INF_DVR_GET_COM485CFG = 70;
        public const int INF_DVR_GET_MANNUALSTATECFG = 71;
        public const int INF_DVR_GET_USERINFOCFG = 72;
        public const int INF_DVR_GET_CURUSERINFOCFG = 73;
        public const int INF_DVR_GET_NEXTUSERINFOCFG = 74;
        public const int INF_DVR_GET_SYSTIME = 75;
        public const int INF_DVR_GET_DVRTIME = 76;
        public const int VIDEO_FRAME_IVOP = 0xD1;
        public const int VIDEO_FRAME_PVOP = 0xD2;
        public const int AUDIO_FRAME = 0xD3;
        public const int NO_FRAME = 0xD4;
        public const int STREAM_HEADER = 0xD5;
        public const int INF_DVR_FILE_SUCCESS = 1000;
        public const int INF_DVR_FILE_NOFIND = 1001;
        public const int INF_DVR_ISFINDING = 1002;
        public const int INF_DVR_NOMOREFILE = 1003;
        public const int INF_DVR_FILE_EXCEPTION = 1004;
        public const int INF_DVR_PLAYSTART = 1;
        public const int INF_DVR_PLAYSTOP = 2;
        public const int INF_DVR_PLAYPAUSE = 3;
        public const int INF_DVR_PLAYRESTART = 4;
        public const int INF_DVR_PLAYFAST = 5;
        public const int INF_DVR_PLAYSLOW = 6;
        public const int INF_DVR_PLAYNORMAL = 7;
        public const int INF_DVR_PLAYFRAME = 8;
        public const int INF_DVR_PLAYSTARTAUDIO = 9;
        public const int INF_DVR_PLAYSTOPAUDIO = 10;
        public const int INF_DVR_PLAYAUDIOVOLUME = 11;
        public const int INF_DVR_PLAYSETPOS = 12;
        public const int INF_DVR_PLAYGETPOS = 13;
        public const int INF_DVR_PLAYGETTIME = 14;
        public const int INF_DVR_PLAYGETFRAME = 15;
        public const int INF_DVR_GETTOTALFRAMES = 16;
        public const int INF_DVR_GETTOTALTIME = 17;
        public const int INF_DVR_THROWBFRAME = 18;
        public const int INF_DVR_SETSPEED = 19;
        public const int INF_DVR_KEEPALIVE = 20;
        public const int INF_DVR_PLAYSETTIME = 21;
        public const int INF_DVR_PLAYGETTOTALLEN = 22;
        public const int INF_DVR_PLAYGETTIMESTAMP = 23;
        public const int INF_DVR_PLAYSTART_BARBARISM = 24;
        #endregion

        #region 枚举
        public enum ALARMTYPE : int
        {
            INPUTALARM = 1, //输入报警
            MOTIONALARM, //移动帧测报警
            COVERALARM, //遮挡报警
            LOSTALARM, //视频丢失报警
            DEVICEEXPALARM, //设备异常报警
            OUTPUTALARM = 255 //控制信息
        }

        public enum INF_PixelFormat : int
        {
            //	INF_PIX_FMT_NONE      = -1,
            INF_PIX_FMT_YUV420P = 0, ///< Planar YUV 4:2:0, 12bpp, (1 Cr & Cb sample per 2x2 Y samples)
            INF_PIX_FMT_YUYV422 = 1, ///< Packed YUV 4:2:2, 16bpp, Y0 Cb Y1 Cr
            INF_PIX_FMT_RGB24 = 2, ///< Packed RGB 8:8:8, 24bpp, RGBRGB...
            INF_PIX_FMT_BGR24 = 3, ///< Packed RGB 8:8:8, 24bpp, BGRBGR...
            INF_PIX_FMT_YUV422P = 4, ///< Planar YUV 4:2:2, 16bpp, (1 Cr & Cb sample per 2x1 Y samples)
            INF_PIX_FMT_YUV444P = 5, ///< Planar YUV 4:4:4, 24bpp, (1 Cr & Cb sample per 1x1 Y samples)
            INF_PIX_FMT_RGB32 = 6, ///< Packed RGB 8:8:8, 32bpp, (msb)8A 8R 8G 8B(lsb), in cpu endianness
            INF_PIX_FMT_YUV410P = 7, ///< Planar YUV 4:1:0, 9bpp, (1 Cr & Cb sample per 4x4 Y samples)
            INF_PIX_FMT_YUV411P = 8, ///< Planar YUV 4:1:1, 12bpp, (1 Cr & Cb sample per 4x1 Y samples)
            INF_PIX_FMT_RGB565 = 9, ///< Packed RGB 5:6:5, 16bpp, (msb) 5R 6G 5B(lsb), in cpu endianness
            INF_PIX_FMT_RGB555 = 10, ///< Packed RGB 5:5:5, 16bpp, (msb)1A 5R 5G 5B(lsb), in cpu endianness most significant bit to 0
            INF_PIX_FMT_GRAY8 = 11, ///< Y , 8bpp
            //	INF_PIX_FMT_MONOWHITE = 12, ///<        Y        ,  1bpp, 0 is white, 1 is black
            INF_PIX_FMT_MONOBLACK = 13, ///< Y , 1bpp, 0 is black, 1 is white
            //	INF_PIX_FMT_PAL8      = 14,      ///< 8 bit with PIX_FMT_RGB32 palette
            //	INF_PIX_FMT_YUVJ420P  = 15,  ///< Planar YUV 4:2:0, 12bpp, full scale (jpeg)
            INF_PIX_FMT_YUVJ422P = 16, ///< Planar YUV 4:2:2, 16bpp, full scale (jpeg)
            INF_PIX_FMT_YUVJ444P = 17, ///< Planar YUV 4:4:4, 24bpp, full scale (jpeg)
            //	INF_PIX_FMT_XVMC_MPEG2_MC   = 18,///< XVideo Motion Acceleration via common packet passing(xvmc_render.h)
            //	INF_PIX_FMT_XVMC_MPEG2_IDCT = 19,
            INF_PIX_FMT_UYVY422 = 20, ///< Packed YUV 4:2:2, 16bpp, Cb Y0 Cr Y1
            //	INF_PIX_FMT_UYYVYY411 = 21, ///< Packed YUV 4:1:1, 12bpp, Cb Y0 Y1 Cr Y2 Y3
            INF_PIX_FMT_BGR32 = 22, ///< Packed RGB 8:8:8, 32bpp, (msb)8A 8B 8G 8R(lsb), in cpu endianness
            INF_PIX_FMT_BGR565 = 23, ///< Packed RGB 5:6:5, 16bpp, (msb) 5B 6G 5R(lsb), in cpu endianness
            INF_PIX_FMT_BGR555 = 24, ///< Packed RGB 5:5:5, 16bpp, (msb)1A 5B 5G 5R(lsb), in cpu endianness most significant bit to 1
            INF_PIX_FMT_BGR8 = 25, ///< Packed RGB 3:3:2, 8bpp, (msb)2B 3G 3R(lsb)
            INF_PIX_FMT_BGR4 = 26, ///< Packed RGB 1:2:1, 4bpp, (msb)1B 2G 1R(lsb)
            INF_PIX_FMT_BGR4_BYTE = 27, ///< Packed RGB 1:2:1, 8bpp, (msb)1B 2G 1R(lsb)
            INF_PIX_FMT_RGB8 = 28, ///< Packed RGB 3:3:2, 8bpp, (msb)2R 3G 3B(lsb)
            INF_PIX_FMT_RGB4 = 29, ///< Packed RGB 1:2:1, 4bpp, (msb)2R 3G 3B(lsb)
            INF_PIX_FMT_RGB4_BYTE = 30, ///< Packed RGB 1:2:1, 8bpp, (msb)2R 3G 3B(lsb)
            INF_PIX_FMT_NV12 = 31, ///< Planar YUV 4:2:0, 12bpp, 1 plane for Y and 1 for UV
            INF_PIX_FMT_NV21 = 32, ///< as above, but U and V bytes are swapped

            //	INF_PIX_FMT_RGB32_1   = 33,   ///< Packed RGB 8:8:8, 32bpp, (msb)8R 8G 8B 8A(lsb), in cpu endianness
            //	INF_PIX_FMT_BGR32_1   = 34,   ///< Packed RGB 8:8:8, 32bpp, (msb)8B 8G 8R 8A(lsb), in cpu endianness

            INF_PIX_FMT_GRAY16BE = 35, ///< Y , 16bpp, big-endian
            INF_PIX_FMT_GRAY16LE = 36, ///< Y , 16bpp, little-endian
            //	INF_PIX_FMT_YUV440P   = 37,   ///< Planar YUV 4:4:0 (1 Cr & Cb sample per 1x2 Y samples)
            //	INF_PIX_FMT_YUVJ440P  = 38,  ///< Planar YUV 4:4:0 full scale (jpeg)
            //	INF_PIX_FMT_YUYVJ422  = 39,   ///< Packed YUV 4:2:2, 16bpp, Y0,Cb, Y1, Cr
            //	INF_PIX_FMT_YUVA420P  = 40,  ///< Planar YUV 4:2:0, 20bpp, (1 Cr & Cb sample per 2x2 Y & A samples)
            //	INF_PIX_FMT_NB        = 41,        ///< number of pixel formats, DO NOT USE THIS if you want to link with shared libav* because the number of formats might differ between versions
        }
        //时区值
        public enum TIME_ZONE : int
        {
            TIME_ZONE_0 = 0, //(GMT-12:00) 日界线西;
            TIME_ZONE_1, //(GMT-11:00) 中途岛，萨摩亚群岛;
            TIME_ZONE_2, //(GMT-10:00) 夏威夷;
            TIME_ZONE_3, //(GMT-09:00) 阿拉斯加;
            TIME_ZONE_4, //(GMT-08:00) 蒂华纳，下加利福尼亚州，太平洋时间（美国和加拿大）;
            TIME_ZONE_5, //(GMT-07:00) 奇瓦瓦，拉巴斯，马萨特兰，山地时间（美国和加拿大），亚利桑那;
            TIME_ZONE_6, //(GMT-06:00) 瓜达拉哈拉，墨西哥城，中美洲;
            TIME_ZONE_7, //(GMT-05:00) 波哥大，利马，里奥布郎库，东部时间（美国和加拿大），印地安那州（东部）;
            TIME_ZONE_8, //(GMT-04:00) 加拉加斯，拉巴斯，圣地亚哥，马瑙斯;
            TIME_ZONE_9, //(GMT-03:30) 纽芬兰;
            TIME_ZONE_10, //(GMT-03:00) 巴西利亚，布宜诺斯艾利斯，乔治敦，格陵兰，蒙得维的亚;
            TIME_ZONE_11, //(GMT-02:00) 中大西洋;
            TIME_ZONE_12, //(GMT-01:00) 佛得角群岛，亚速尔群岛;
            TIME_ZONE_13, //(GMT) 格林威治标准时间:都柏林，爱丁堡，伦敦，里斯本，卡萨布兰卡，罗维亚，雷克雅未克;
            TIME_ZONE_14, //(GMT+01:00) 阿姆斯特丹，柏林，伯尔尼，罗马，斯德哥尔摩，贝尔格莱德，布拉迪斯拉发，布达佩斯，布鲁塞尔，哥本哈根，马德里，巴黎，萨拉热窝，斯科普里，华沙，萨格勒布，中非西部;
            TIME_ZONE_15, //(GMT+02:00) 安曼，贝鲁特，哈拉雷，比勒陀利亚，赫尔辛基，基辅，里加，索非亚，塔林，开罗，明斯克，温得和克，雅典，布加勒斯特，伊斯坦布尔，耶路撒冷;
            TIME_ZONE_16, //(GMT+03:00) 巴格达，第比利斯，科威特，利雅得，莫斯科，圣彼得堡，伏尔加格勒，内罗毕;
            TIME_ZONE_17, //(GMT+03:30) 德黑兰;
            TIME_ZONE_18, //(GMT+04:00) 阿布扎比，埃里温，巴库;
            TIME_ZONE_19, //(GMT+04:30) 喀布尔;
            TIME_ZONE_20, //(GMT+05:00) 叶卡捷琳堡，伊斯兰堡，卡拉奇，塔什干;
            TIME_ZONE_21, //(GMT+05:30) 马德拉斯，加尔各答，孟买，新德里，斯里哈亚华登尼普拉;
            TIME_ZONE_22, //(GMT+05:45) 加德曼都;
            TIME_ZONE_23, //(GMT+06:00) 阿拉木图，新西伯利亚，阿斯塔纳，达卡;
            TIME_ZONE_24, //(GMT+06:30) 仰光;
            TIME_ZONE_25, //(GMT+07:00) 克拉斯诺亚尔斯克，曼谷，河内，雅加达;
            TIME_ZONE_26, //(GMT+08:00) 北京，重庆，香港特别行政区，乌鲁木齐，吉隆坡，新加坡，台北，伊尔库茨克，乌兰巴图;
            TIME_ZONE_27, //(GMT+09:00) 大坂，札幌，东京，汉城，雅库茨克;
            TIME_ZONE_28, //(GMT+09:30) 阿德莱德，达尔文;
            TIME_ZONE_29, //(GMT+10:00) 布里斯班，符拉迪沃斯托克，关岛，摩尔兹比港，霍巴特，詹培拉，摩尔本，悉尼;
            TIME_ZONE_30, //(GMT+11:00) 马加丹，所罗门群岛;
            TIME_ZONE_31, //(GMT+12:00) 奥克兰，惠灵顿，斐济，堪察加半岛，马绍尔群岛;
            TIME_ZONE_32 //(GMT+13:00) 努库阿洛法;
        }
        #endregion

        #region 自定义结构
        //********************数据结构************************************

        public struct INF_NET_CLIENTINFO
        {
            public int lChannel; //通道号
            public int lLinkMode; //1表示为主码流，2为子码流
            public IntPtr hPlayWnd; //播放窗口的句柄
            public string snmpUsername; //SNMP用户名,此参数为无效参数，与登录结构体LPINF_NET_DVR_LONGININFO中参数*sUserName意义相同，建议后续版本删除此参数
            public string strSMTIP; //SMTIP
            public ushort wSMTPort; //SMT Port
        }

        //*****************报警相关******************************
        //输入报警
        public struct INF_NET_INPUTALARM
        {
            public string cSendAlarmIP; //发送报警的IP
            public int iSendIPLength; //发送报警IP的长度
            public uint channel; //数字代表对应的通道号，1表示1通道，16表示16通道
            public byte flag; //1-有报警，0-无报警
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] res; //保留
        }

        //移动帧测报警
        public struct INF_NET_MOTIONALARM
        {
            public string cSendAlarmIP; //发送报警的IP
            public int iSendIPLength; //发送报警IP的长度
            public uint channel; //数字代表对应的通道号
            public byte flag; //1-有报警，0-无报警
            public byte area; //哪个区域，数字代表对应的区域
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] res; //保留。
        }

        //视频遮挡报警
        public struct INF_NET_OBSTRUCTALARM
        {
            public string cSendAlarmIP; //发送报警的IP
            public int iSendIPLength; //发送报警IP的长度
            public uint channel; //哪个通道，数字代表对应的通道号
            public byte flag; //1-有报警，0-无报警
            public byte area; //哪个区域，数字代表对应的区域
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] res; //保留
        }

        //视频丢失报警
        public struct INF_NET_VIDEOLOSTALARM
        {
            public string cSendAlarmIP; //发送报警的IP
            public int iSendIPLength; //发送报警IP的长度
            public uint channel; // 数字代表对应的通道号
            public byte flag; //1-有报警，0-无报警
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] res; //保留
        }

        //设备异常报警
        public struct INF_NET_DEVEXPALARM
        {
            public string cSendAlarmIP; //发送报警的IP
            public int iSendIPLength; //发送报警IP的长度
            public uint mintype; // 1-硬盘满， 2-硬盘出错， 3-网络断开， 4-非法访问， 5-网络冲突
            public byte flag; //1-有报警，0-无报警
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] res; //保留
        }

        //服务器发给设备的控制信息。
        public struct INF_NET_OUTPUTALARM
        {
            public string cSendAlarmIP; //发送报警的IP
            public int iSendIPLength; //发送报警IP的长度
            public uint channel; //数字代表对应的通道号
            public byte flag; //1表示有控制输出 ，0无
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] res; //保留
        } //输出控制

        //**********************配置相关*************************
        //设备状态信息      					
        public struct INF_NET_DEVICEINFO
        {
            public byte byTotalCHs; //通道总数
            public byte byTotalHDs; //硬盘总数
            //	BYTE	byBoots;					//启动的总次数
            public byte byDevId; //设备ID
            public byte byOganic; //制式：1--PAL ，2--NTSC （只读）
            [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = 32)]
            public string sName; //设备名称
            [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = SERIALNO_LEN)]
            public string sdevSN; //设备序列号
            [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = 32)]
            public string sdecodeVer; //解码版本
            [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = 32)]
            public string sencodeVer; //编码版本
            [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = 32)]
            public string shWareVer; //硬件版本
            [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = 32)]
            public string sSoftVer; //软件版本
            [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = 32)]
            public string sbootTime; //系统启动时间 (格式：2008-11-07 11:45:00;)
        }

        //网卡配置信息      					
        public struct INF_NET_CARD
        {
            public byte byNetworkCard; //网卡类型 0-100M/1000M自适应；1-1000M；2-100M；3-10M
            public byte byNetType; //网卡网络类型 0-static；1-DHCP；2-3PoE
            public byte byAutoDns; //网卡自动Dns（保留） 1-自动DNS；0-禁止自动DNS
            public uint dwPort; //网卡端口
            [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = 16)]
            public string sDns; //Dns服务器地址
            [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = 16)]
            public string sbCast; //广播组地址（保留）
            [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = 16)]
            public string smCast; //多播组地址（保留）
            [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = 16)]
            public string sPppoeIp; //3PoE地址
            [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = PPPOE_NAMELEN + 1)]
            public string sPppoeUser; //3PoE用户名
            [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = PPPOE_PWDLEN + 1)]
            public string sPppoePsw; //3PoE密码
            [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = 16)]
            public string sMac; //网卡物理地址
            [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = 16)]
            public string sIp; //网卡IP地址
            [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = 16)]
            public string sMask; //子网掩码
            [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = 16)]
            public string sGateway; //网关地址
        }

        //网络公共配置信息        				
        public struct INF_NET_PUBLICINFO
        {
            public uint dwWebPort; //web端口
            public uint dwSnmpPort; //snmp端口
            public uint dwHostPort; //主机端口
            [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = 16)]
            public string sNasIp; //Nas主机地址
            [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = 16)]
            public string sHostIp; //主机地址
            [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = PATHNAME_LEN)]
            public string sNasPath; //Nas路径（保留）
        }

        //通道配置信息      					
        public struct INF_NET_CHINFO
        {
            [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = CHANNAME_LEN + 1)]
            public string sChDevName; //通道名
        }

        //码流配置信息      					
        public struct INF_NET_STREAMINFO
        {
            public byte byMixStream; //码流类型　　0: video stream　　　1: V & A stream
            public uint dwVopRate; //帧率　　　　PAL制：1~25　　　　　NETS制：1 ~ 30
            public uint dwIpRate; //IP比率 I帧间隔10 ~ 255
            public byte byQuality; //图像质量 图像质量：0: lowest；1: low；2: middle；3: high；4: highest
            public byte byResolution; //分辨率 主码流时：0: QCIF；1: CIF；2: 2CIF；3: 4CIF，且主码流分辨率不能小于子码流分辨率 子码流时： 0: QCIF；1: CIF
            public uint dwBitRate; //位率类型 0: 变码率；1: 定码率
            public uint dwMaxBitRate; //位率上限 0：256kps；1：512kps;2：640kps;3:768kps;4:896kps;5:1024kps;6:1280kps;7:1536kps;8:1792kps;9:2048kps;10:2560kps
        }

        //字符串显示信息
        public struct INF_NET_TEXT
        {
            public byte byTextSta; //显示状态 1：显示， 0：不显示
            public int iTextX; //字符串X坐标
            public int iTextY; //字符串Y坐标
            [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = TEXT_LEN + 1)]
            public string sText; //字符串内容
        }

        //OSD配置信息    						
        public struct INF_NET_OSDINFO
        {
            //	BYTE		  byAttribute;			//OSD属性  0:透明、闪亮 1:透明、不闪亮  2:闪亮、不透明 3: 不透明、不闪亮
            public byte byWeekStatus; //星期显示 1：显示， 0：不显示
            public byte byTimeFormat; //时间格式 0: YYYY-MM-DD, 1: YYYY MM DD 2:YYYY/MM/DD, 3: MM/DD/YYYY
            public byte byTimeSta; //时间显示 1：显示， 0：不显示
            public int iTimeX; //x坐标
            public int iTimeY; //y坐标
            public byte byNameSta; //通道名称显示
            public int iNameX; //通道名称的x坐标
            public int iNameY; //通道名称的y坐标
            [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = MAX_TEXT_NUM)]
            public INF_NET_TEXT[] struText; //字符串显示
        }

        //报警触发方式信息
        public struct INF_NET_ALARMTRIG
        {
            public byte byPlayAlarm; //预览报警 1-选择；0-不选择
            public byte byVoiceAlarm; //声音报警 1-选择；0-不选择
            public byte byCenterAlarm; //上传控制中心 1-选择；0-不选择
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_TEXT_NUM)]
            public byte[] byAlarmOut; //报警触发输出 1-选择；0-不选择
        }


        //区域坐标信息
        public struct INF_NET_AREASET
        {
            public int iX; //横坐标范围(0,22),以左上角为原点，一屏分为 横22 ,竖18个格子，用横竖坐标确定一点。
            public int iY; //纵坐标范围(0,18)。
            public int iWidth; //横格数
            public int iHeight; //纵格数
        }

        //PTZ联动信息
        public struct INF_NET_PTZ
        {
            public byte bySelect; // 选择状态：1：预置位，2：花样设置，3:两者都没选
            public int iPtzValue; //当 bySelect = 1时，表示预置位，范围：1—255
            //当 bySelect = 2时，表示花样设置，范围为：1 – 4
            //当 bySelect = 3时，表示无值，取值为0
        }


        //移动侦测配置信息        				
        public struct INF_NET_MOTION
        {
            public byte bySensitivity; //灵敏度设置 0:高灵敏度 1：中灵敏度 2：低灵敏度
            public byte byStatus; //选择状态 1-选择；0-不选择
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM)]
            public byte[] byRecChSta; //触发通道状态 0--未选择 ，1--选择
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = WEEK_DAYS * TIME_PLANS)]
            public byte[,] byAlarmTime; //时间布防 0--表示该布防时间为未选择状态，1--表示该布防时间为选择状态
            //WEEK_DAYS：0---6表示周日、一、二.....六 ，TIME_PLANS:0--47表示00：00--00：30，00：30--01：00.....          		
            public INF_NET_ALARMTRIG struTrig; //报警方式
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM)]
            public INF_NET_PTZ[] struPtz; //PTZ联动
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_AREA)]
            public INF_NET_AREASET[] struAreaSet; //区域设置

        }

        //遮挡报警配置信息        				
        public struct INF_NET_OBSTRUCT
        {
            public byte bySensitivity; //参照 移动侦测配置信息
            public byte byStatus;
            //	BYTE    byRecChSta[MAX_CHANNUM];			 //触发通道状态  0--未选择 ，1--选择   //暂无该功能
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = WEEK_DAYS * TIME_PLANS)]
            public byte[,] byAlarmTime; //时间布防 0--表示该布防时间为未选择状态，1--表示该布防时间为选择状态
            //WEEK_DAYS：0---6表示周日、一、二.....六 ，TIME_PLANS:0--47表示00：00--00：30，00：30--01：00.....  
            public INF_NET_ALARMTRIG struTrig; //报警方式
            public INF_NET_AREASET[] struAreaSet; //区域设置
        }

        //视频丢失报警报警配置信息				
        public struct INF_NET_SIGLOST
        {
            public byte byStatus;
            //	BYTE    byRecChSta[MAX_CHANNUM];             //触发通道状态  0--未选择 ，1--选择      //暂无该功能
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = WEEK_DAYS * TIME_PLANS)]
            public byte[,] byAlarmTime; //时间布防 0--表示该布防时间为未选择状态，1--表示该布防时间为选择状态
            //WEEK_DAYS：0---6表示周日、一、二.....六 ，TIME_PLANS:0--47表示00：00--00：30，00：30--01：00.....          
            public INF_NET_ALARMTRIG struTrig; //报警方式
        }

        //录像布防状态
        public struct INF_NET_REPLAN
        {
            public byte byIsRecord; //录像状态：0--表示未录像，1--表示录像
            public byte byRecordType; //录像类型：0--2未录像，1--定时录像，2--报警录像，3--定时和报警录像
        }

        //录像计划配置信息		
        public struct INF_NET_RECORD
        {
            public byte byRecSta; //选择状态：1 选择，0 相反
            //	BYTE			byTecTo;								//存储方式：0 本地, 1: 网络, 2; 两者     //暂不支持该功能   
            public int iPreTime; //预录时间 0：不预录 1：2s 2:4s 3:6s 4:8s 5:最大化
            public int iDelayTime; //延时时间 0：不延时 1：5s 2:10s 3:30s 4:1分钟 5:2分钟 6:5分钟 7:10分钟
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = WEEK_DAYS * TIME_PLANS)]
            public INF_NET_REPLAN[,] struRecordTime; //时间布防
            //WEEK_DAYS：0---6表示周日、一、二.....六 ，TIME_PLANS:0--47表示00：00--00：30，00：30--01：00.....          
        }

        //隐私区域设置信息						
        public struct INF_NET_PRIVACYAREA
        {
            public byte byStatus; //状态：1 选择，0 相反
            public INF_NET_AREASET[] struAreaSet; //设置区域
        }

        //报警输入配置信息						
        public struct INF_NET_ALARMIN
        {
            public byte byStatus; //报警类型 0：常开； 1：常闭
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = ALARM_LEN + 1)]
            public string sName; //报警名称
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM)]
            public byte[] byRecChSta; //触发通道状态 0--未选择 ，1--选择
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = WEEK_DAYS * TIME_PLANS)]
            public byte[,] byAlarmTime; //时间布防 0--表示该布防时间为未选择状态，1--表示该布防时间为选择状态
            //WEEK_DAYS：0---6表示周日、一、二.....六 ，TIME_PLANS:0--47表示00：00--00：30，00：30--01：00.....          
            public INF_NET_ALARMTRIG struTrig; //报警方式
            public INF_NET_PTZ[] struPtz; //PTZ联动
        }

        //报警输出配置信息						
        public struct INF_NET_ALARMOUT
        {
            public int iDelayTime; //报警输出延迟时间： 0：5秒;1:10秒;2:30秒;3:1分钟;4:2分钟;5:5分钟;6:10分钟;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = WEEK_DAYS * TIME_PLANS)]
            public byte[,] byAlarmTime; //时间布防 0--表示该布防时间为未选择状态，1--表示该布防时间为选择状态
            //WEEK_DAYS：0---6表示周日、一、二.....六 ，TIME_PLANS:0--47表示00：00--00：30，00：30--01：00.....          
        }

        //异常报警配置信息
        public struct INF_NET_ALARMNOR
        {
            public byte byExceptType; //异常报警类型0-硬盘满,1- 硬盘出错,2-网络断开,3-非法访问, 4-网络冲突
            public INF_NET_ALARMTRIG struTrig; //报警方式
        }

        //串口参数配置信息
        public struct INF_NET_COM232INFO
        {
            public int iBaudRate; //波特率 0:50bps,1:75bps,2:110bps,3:150bps,4:300bps,5:600bps,6:1200bps,7:2400bps,8:4800bps,9:9600bps,10:19200bps,11:38400bps,12:57600bps,13:76800bps,14:115200bps
            public int iDataBit; //数据位 0:5bit; 1:6bit; 2:7bit; 3:8bit
            public int iStopBit; //停止位 0:1bit; 1:2bit
            public int iParity; //奇偶校验 0:no; 1:odd; 2:even
            public int iFlowCtrl; //数据流控制 0:no; 1:software; 2:hardware
        }

        public struct INF_NET_COM485INFO
        {
            public int iBaudRate; //波特率 0:2400bps,1:4800bps,2:9600bps,3:19200bps,4:38400bps,5:57600bps,6:76800bps,7:115200bps
            public int iDataBit; //数据位 0:5bit; 1:6bit; 2:7bit; 3:8bit
            public int iStopBit; //停止位 0:1bit; 1:2bit
            public int iParity; //奇偶校验 0:no; 1:odd; 2:even
            public int iFlowCtrl; //数据流控制 0:no; 1:software; 2:hardware
            public int iProtocolType; //PTZ协议 0:infinoval; 1:pelco-D;2:pelco-P
            public int iDecodeAddress; //PTZ地址 范围：1--255
        }

        //用户管理配置信息					
        public struct INF_NET_USERINFO
        {
            public byte byUsertype; //用户类型 R 0-管理员用户；1-普通用户
            public byte byUserSN; //用户序号 R
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = NAME_LEN + 1)]
            public string sUserName; //用户名 R/W 范围：1--15
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PASSWD_LEN + 1)]
            public string sPswd; //用户密码 W 范围：8--15
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
            public string sUserPrivilege; //用户权限 R/W
        }

        //硬盘状态信息
        public struct INF_NET_DISKSTATE
        {
            public byte byDiskID; //硬盘ID 1----8
            public byte byUseMode; //硬盘覆盖策略：1、循环覆盖；2、不循环覆盖
            public byte byDiskStat; // 1:空闲 ，2：工作 3：硬盘满 4：出错 5：无硬盘 、6：正在格式化 、7：未格式化
            public uint dwDiskCapacity; //硬盘容量： 单位为： G
            public uint dwDiskFree; //硬盘剩余容量：单位为： G
        }

        //通道状态信息
        public struct INF_NET_CHANNELSTATE
        {
            public byte byRecordState; //通道是否在录像,0-不录像,1-手动录像，2－定时录像，3－报警录像
            public byte bySignalState; //通道是否视频丢失,0-视频正常,1-视频丢失
            public byte byAlarmState; //报警状态,0-没有报警,1-移动侦测报警，2－遮挡报警，3－信号量报警
        }

        //远程录像状态信息
        public struct INF_NET_MANNUALSTATE
        {
            public byte byMannualState; //0--未录像 1--录像
        }

        //////////////////////////////////////////////////

        //设备状态信息结构体
        public struct INF_NET_WORKSTATE
        {
            //	DWORD					dwRootNum;							//设备启动次数	该参数无效
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string sRootTime; //设备启动时间：2008-11-07 11:45:00

            public INF_NET_DISKSTATE[] struHDStatic; //硬盘的状态
            public INF_NET_CHANNELSTATE[] struChanState; //通道的状态
        }


        //时间参数结构体
        public struct INF_NET_TIME
        {
            public uint dwYear;
            public uint dwMonth;
            public uint dwDay;
            public uint dwHour;
            public uint dwMinute;
            public uint dwSecond;
        }

        //搜索日志的条件信息结构
        public struct INF_NET_LOGSEARCHINFO
        {

            public INF_NET_TIME struBeginTime;
            public INF_NET_TIME struEndTime;
            public uint dwMajorType;
            public uint dwMinorType;
        }

        //日志的信息结构
        public struct INF_NET_LOGINFO
        {
            public INF_NET_TIME strLogTime; //事件时间
            public uint dwMajorType; //主类型
            public uint dwMinorType; //次类型
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string sDescription; //事件
        }

        //查找录像文件信息结构体
        public struct INF_NET_FILECOND
        {
            public int lChannel; // 1通道 ： 0， 2通道：1，...16通道：15 ,全部通道：-1
            public uint dwFileType;
            public INF_NET_TIME struStartTime;
            public INF_NET_TIME struStopTime;
        }

        public struct INF_NET_FINDDATA
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string sFileName; //文件名
            public INF_NET_TIME struStartTime; //文件的开始时间
            public INF_NET_TIME struStopTime; //文件的结束时间
            public uint dwFileSize; //文件的大小
            public byte byFileType; //1：定时；2：手动；3：报警 0XFFFF : 全部
        }

        //登录信息结构体						
        public struct INF_NET_DVR_LONGININFO
        {
            public string sDevIp; //服务器IP
            public string sUserName; //服务器用户名
            public string sPrivKey; //用户私钥
            public string sAuthKey; //用户公钥
            public ushort wServerPort; //服务器端口 wDeviceType = 0时：wServerPort为无效值0， wDeviceType = 1时：wServerPort为服务器端口号，有效值为0--65535
            public ushort wDeviceType; //设备类型 0:基础版本DVR 1:统一端口DVR
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
            public string Res; //保留字段
        }

        //时区信息结构体
        public struct INF_NET_SYSTIME
        {
            public byte byNTP; //是否启用NTP校时时, 1:是，0：否
            public byte byPtz; //时区(赋值：0~32)，参考TIME_ZONE
            public byte byDayLight; //是否启用夏时令，0:关闭 1:开启
            public byte byRes; //--保留字段--
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public string cIP; //NTP服务器IP
        }
        #endregion

        #region 函数
        public static bool bInt = false;

        public const string SDK_INF = @".\Driver\infinova\INF_DVRNETSDK.dll";
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_Init();
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_Cleanup();
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_CheckDeviceType(string sDevIp, uint dwServerPort, string sUserName, string sAuthKey);
        [DllImport(SDK_INF)]//注册用户
        public static extern int INF_NET_DVR_Login_V20(ref INF_NET_DVR_LONGININFO lpLoginInfo); //注册用户统一版本
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_Logout(int lUserID);//注销用户
        [DllImport(SDK_INF)]
        public static extern uint INF_NET_DVR_GetLastError();//获取最后错误
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_RealPlay(int lUserID, ref INF_NET_CLIENTINFO lpClientInfo, fStdDataCallBack cbStdDataCallBack, IntPtr pUser);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_StopRealPlay(int lRealHandle);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_SetDVRConfig(int lUserID, uint dwCommand, int lChannel, IntPtr lpInBuffer, uint dwInBufferSize);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_GetDVRConfig(int lUserID, uint dwCommand, int lChannel, IntPtr lpOutBuffer, ref uint lpBytesReturned);   //配置用户信息
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_StartListen(string sLocalIP, ushort wLocalPort, fAlarmCallBack cbMessageCallBack, IntPtr pUser);      //开始监听
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_StopListen(int lListenHandle);      //停止监听
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_PTZControl(int lRealHandle, uint dwPTZCommand, uint dwSpeed, uint dwStop);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_PTZPreset(int lRealHandle, uint dwPTZPresetCmd, uint dwPresetIndex);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_PTZPattern(int lRealHandle, uint dwPTZPatternCmd, uint dwPatternIndex);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_ClientGetVideoEffect(int lRealHandle, ref uint pBrightValue, ref uint pContrastValue, ref uint pSaturationValue, ref uint pHueValue);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_ClientSetVideoEffect(int lRealHandle, uint dwBrightValue, uint dwContrastValue, uint dwSaturationValue, uint dwHueValue);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_FindFile(int lUserID, ref INF_NET_FILECOND pFindCond);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_FindFileNext(int lFindHandle, ref INF_NET_FINDDATA pFileInfo);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_FindClose(int lFindHandle);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_GetDVRWorkState(int lUserID, ref INF_NET_WORKSTATE lpWorkState);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_FindDVRLog(int lUserID, ref INF_NET_LOGSEARCHINFO pSearchInfo);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_FindNextLog(int lLogHandle, ref INF_NET_LOGINFO lpLogData);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_FindLogClose(int lLogHandle);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_RebootDVR(int lUserID);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_ShutDownDVR(int lUserID);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_Upgrade(int lUserID, string sFileName);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_FormatDisk(int lUserID, int lDiskNumber);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_ExcuteCoverPolicy(int lUserID, int iCoverPolicy);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_StartVoiceCom(int lTalkUserID, uint dwVoiceChan, int bNeedCBNoEncData, fVoiceDataCallBack cbVoiceDataCallBack, IntPtr pUser);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_StopVoiceCom(int lVoiceHandle);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_UpdateTime(int lUserID, ref INF_NET_TIME struCurDate);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_OpenSound(int lRealHandle);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_CloseSound(int lRealHandle);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_SaveRealData(int lRealHandle, string sFileName, fRecordFileCallBack cbRecordFileCallBack);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_StopSaveRealData(int lRealHandle);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_CapturePicture(int lRealHandle, string sPicFileName);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_PlayBackByName(int lUserID, string sPlayBackFileName, IntPtr hWnd);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_PlayBackControl(int lPlayHandle, uint dwControlCode, uint dwInValue, out uint lpOutValue);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_PlayBackControl_V20(int lPlayHandle, uint dwControlCode, string lpInBuffer, uint dwInLen, string lpOutBuffer, ref uint lpOutLen);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_StopPlayBack(int lPlayHandle);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_PlayBackCaptureFile(int lPlayHandle, string sFileName);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_RefreshPlay(int lPlayHandle);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_GetFileByName(int lUserID, string sDVRFileName, string sSavedFileName);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_DownloadControl(int lFileHandle, uint dwControlCode, uint dwInValue, out uint lpOutValue);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_StopGetFile(int lFileHandle);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_GetDownloadPos(int lFileHandle);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_StartDVRRecord(int lUserID, int lChannel);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_StopDVRRecord(int lUserID, int lChannel);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_SetDVRMessage(uint nMessage, IntPtr hWnd);
        [DllImport(SDK_INF)]
        public static extern uint INF_NET_DVR_GetSDKVersion();
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_PlayBackByTime(int lUserID, int lChannel, ref INF_NET_TIME struStartTime, ref INF_NET_TIME struStopTime, IntPtr hWnd);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_PlayBackSaveData(int lPlayHandle, string sFileName);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_StopPlayBackSave(int lPlayHandle);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_GetFileByTime(int lUserID, int lChannel, ref INF_NET_TIME struStartTime, ref INF_NET_TIME struStopTime, string sSavedFileName);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_SetPlayDataCallBack(int lPlayHandle, fPlayDataCallBack cbPlayDataCallBack, IntPtr pUser);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_SetPlayStreamDataCallBack(int lPlayHandle, fPlayStreamDataCallBack cbPlayStreamDataCallBack, IntPtr pUser);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_DecodeBegin();
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_DecodeEnd(int lCodeHandle);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_Decode(int lCodeHandle, ref byte pBufInput, uint dwBytesInput, ref byte pBufOutput, ref uint pBytesOutput, ref uint pWidth, ref uint pHeight, ref uint pFrameType, int iFormat);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_SetFileDataCallBack(int lFileHandle, fFileDataCallBack cbFileDataCallBack, IntPtr pUser);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_GetFileByTime_V20(int lUserID, int lChannel, INF_NET_TIME struStartTime, INF_NET_TIME struStopTime, string sSavedFileName, uint dwFileType);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_SetRealDataCallBack(int lRealHandle, fRealDataCallBack cbRealDataCallBack, IntPtr pUser);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_SetStdDataCallBack(int lRealHandle, fStdDataCallBack cbStdDataCallBack, IntPtr pUser);

        #endregion
    }
}





