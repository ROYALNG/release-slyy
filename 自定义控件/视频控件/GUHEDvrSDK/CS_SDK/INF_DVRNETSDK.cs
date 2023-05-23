
using System;
using System.Runtime.InteropServices;


namespace GHIBMS.INFDVRSDK
{

    public static class INF_DVRNETSDK
    {
        #region ί��

        //********************�ص���������***************************
        //����ʵʱ��Ƶ���ݵĻص�����
        //C++ TO C# CONVERTER TODO TASK: The original C++ function pointer contained an unconverted modifier:
        //ORIGINAL LINE: typedef void(CALLBACK *fRealDataCallBack)(int lRealHandle, uint dwDataType, sbyte *pBuffer, uint dwBufSize, IntPtr pUser);
        public delegate void fRealDataCallBack(int lRealHandle, uint dwDataType, string pBuffer, uint dwBufSize, IntPtr pUser);

        //����ʵʱ��Ƶ��׼�������ݵĻص�����
        //C++ TO C# CONVERTER TODO TASK: The original C++ function pointer contained an unconverted modifier:
        //ORIGINAL LINE: typedef void(CALLBACK *fStdDataCallBack)(int lRealHandle, uint dwDataType, sbyte *pBuffer, uint dwBufSize, IntPtr pUser);
        //public delegate void RealDataCallBack_V30(int lRealHandle, uint dwDataType, byte[] pBuffer, uint dwBufSize, IntPtr pUser);

        public delegate void fStdDataCallBack(int lRealHandle, uint dwDataType, IntPtr pBuffer, uint dwBufSize, IntPtr pUser);

        //���ڼ����Ļص�����
        //C++ TO C# CONVERTER TODO TASK: The original C++ function pointer contained an unconverted modifier:
        //ORIGINAL LINE: typedef void(CALLBACK* fAlarmCallBack)(int lAlarmType, IntPtr pAlarmInfo, uint dwBufLen, IntPtr pUser);
        public delegate void fAlarmCallBack(int lAlarmType, IntPtr pAlarmInfo, uint dwBufLen, IntPtr pUser);

        ////���ջط���Ƶ���ݻص�����
        //typedef void(CALLBACK *fPlayerDataCallBack)(
        //	INF_NET_REPFILEINFO	fileInfo,
        //	BYTE				*byBuffer,			//����������ָ��
        //	DWORD				dwBufSize,			//��������С
        //	void				*pUser				//�û�����
        //	);

        //��Ƶ���ݻص�����
        //C++ TO C# CONVERTER TODO TASK: The original C++ function pointer contained an unconverted modifier:
        //ORIGINAL LINE: typedef void(CALLBACK *fVoiceDataCallBack)(int lVoiceHandle, sbyte *pRecvDataBuffer, uint dwBufSize, byte byAudioFlag, IntPtr pUser);
        public delegate void fVoiceDataCallBack(int lVoiceHandle, string pRecvDataBuffer, uint dwBufSize, byte byAudioFlag, IntPtr pUser);

        //���ڱ�������¼��Ļص�����
        //C++ TO C# CONVERTER TODO TASK: The original C++ function pointer contained an unconverted modifier:
        //ORIGINAL LINE: typedef void(CALLBACK *fAlarmRecordCallBack)(sbyte *sDVRIP, int lChannel, uint dwDataType, sbyte *pBuffer, uint dwBufSize, IntPtr pUser);
        public delegate void fAlarmRecordCallBack(string sDVRIP, int lChannel, uint dwDataType, string pBuffer, uint dwBufSize, IntPtr pUser);
        //�طű�׼�������ݻص�����
        //C++ TO C# CONVERTER TODO TASK: The original C++ function pointer contained an unconverted modifier:
        //ORIGINAL LINE: typedef void(CALLBACK *fPlayDataCallBack)(int lPlayHandle, uint dwDataType, byte *pBuffer, uint dwBufSize, IntPtr pUser);
        public delegate void fPlayDataCallBack(int lPlayHandle, uint dwDataType, ref byte pBuffer, uint dwBufSize, IntPtr pUser);

        //�ط������ݻص�����
        //C++ TO C# CONVERTER TODO TASK: The original C++ function pointer contained an unconverted modifier:
        //ORIGINAL LINE: typedef void(CALLBACK *fPlayStreamDataCallBack)(int lPlayHandle, uint dwDataType, byte *pBuffer, uint dwBufSize, IntPtr pUser);
        public delegate void fPlayStreamDataCallBack(int lPlayHandle, uint dwDataType, ref byte pBuffer, uint dwBufSize, IntPtr pUser);

        //�ֶ�¼��Ļص�����
        //C++ TO C# CONVERTER TODO TASK: The original C++ function pointer contained an unconverted modifier:
        //ORIGINAL LINE: typedef void(CALLBACK *fRecordFileCallBack)(int lRealHandle);
        public delegate void fRecordFileCallBack(int lRealHandle);
        //���ػص�����
        //C++ TO C# CONVERTER TODO TASK: The original C++ function pointer contained an unconverted modifier:
        //ORIGINAL LINE: typedef void(CALLBACK *fFileDataCallBack)(int lFileHandle, uint dwDataType, byte *pBuffer, uint dwBufSize, IntPtr pUser);
        public delegate void fFileDataCallBack(int lFileHandle, uint dwDataType, ref byte pBuffer, uint dwBufSize, IntPtr pUser);
        #endregion

        #region ����
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

        #region ö��
        public enum ALARMTYPE : int
        {
            INPUTALARM = 1, //���뱨��
            MOTIONALARM, //�ƶ�֡�ⱨ��
            COVERALARM, //�ڵ�����
            LOSTALARM, //��Ƶ��ʧ����
            DEVICEEXPALARM, //�豸�쳣����
            OUTPUTALARM = 255 //������Ϣ
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
        //ʱ��ֵ
        public enum TIME_ZONE : int
        {
            TIME_ZONE_0 = 0, //(GMT-12:00) �ս�����;
            TIME_ZONE_1, //(GMT-11:00) ��;������Ħ��Ⱥ��;
            TIME_ZONE_2, //(GMT-10:00) ������;
            TIME_ZONE_3, //(GMT-09:00) ����˹��;
            TIME_ZONE_4, //(GMT-08:00) �ٻ��ɣ��¼����������ݣ�̫ƽ��ʱ�䣨�����ͼ��ô�;
            TIME_ZONE_5, //(GMT-07:00) �����ߣ�����˹������������ɽ��ʱ�䣨�����ͼ��ô󣩣�����ɣ��;
            TIME_ZONE_6, //(GMT-06:00) �ϴ���������ī����ǣ�������;
            TIME_ZONE_7, //(GMT-05:00) �����������²��ɿ⣬����ʱ�䣨�����ͼ��ô󣩣�ӡ�ذ����ݣ�������;
            TIME_ZONE_8, //(GMT-04:00) ������˹������˹��ʥ���Ǹ磬���˹;
            TIME_ZONE_9, //(GMT-03:30) Ŧ����;
            TIME_ZONE_10, //(GMT-03:00) �������ǣ�����ŵ˹����˹�����ζأ����������ɵ�ά����;
            TIME_ZONE_11, //(GMT-02:00) �д�����;
            TIME_ZONE_12, //(GMT-01:00) ��ý�Ⱥ�������ٶ�Ⱥ��;
            TIME_ZONE_13, //(GMT) �������α�׼ʱ��:�����֣����������׶أ���˹������������������ά�ǣ��׿���δ��;
            TIME_ZONE_14, //(GMT+01:00) ��ķ˹�ص������֣������ᣬ����˹�¸��Ħ�����������£�������˹������������˹����³�������籾�������������裬�������ѣ�˹�������ɳ�������ղ����з�����;
            TIME_ZONE_15, //(GMT+02:00) ��������³�أ������ף����������ǣ��ն���������������ӣ������ǣ����֣����ޣ���˹�ˣ��µúͿˣ��ŵ䣬������˹�أ���˹̹������Ү·����;
            TIME_ZONE_16, //(GMT+03:00) �͸��ڱ���˹�������أ����ŵã�Ī˹�ƣ�ʥ�˵ñ��������Ӹ��գ����ޱ�;
            TIME_ZONE_17, //(GMT+03:30) �º���;
            TIME_ZONE_18, //(GMT+04:00) �������ȣ������£��Ϳ�;
            TIME_ZONE_19, //(GMT+04:30) ������;
            TIME_ZONE_20, //(GMT+05:00) Ҷ�����ձ�����˹�����������棬��ʲ��;
            TIME_ZONE_21, //(GMT+05:30) �����˹���Ӷ����������µ��˹����ǻ���������;
            TIME_ZONE_22, //(GMT+05:45) �ӵ�����;
            TIME_ZONE_23, //(GMT+06:00) ����ľͼ�����������ǣ���˹���ɣ��￨;
            TIME_ZONE_24, //(GMT+06:30) ����;
            TIME_ZONE_25, //(GMT+07:00) ����˹ŵ�Ƕ�˹�ˣ����ȣ����ڣ��żӴ�;
            TIME_ZONE_26, //(GMT+08:00) ���������죬����ر�����������³ľ�룬��¡�£��¼��£�̨����������Ŀˣ�������ͼ;
            TIME_ZONE_27, //(GMT+09:00) ���࣬���ϣ����������ǣ��ſ�Ŀ�;
            TIME_ZONE_28, //(GMT+09:30) �������£������;
            TIME_ZONE_29, //(GMT+10:00) ����˹�࣬��������˹�пˣ��ص���Ħ���ȱȸۣ������أ�ղ������Ħ������Ϥ��;
            TIME_ZONE_30, //(GMT+11:00) ��ӵ���������Ⱥ��;
            TIME_ZONE_31, //(GMT+12:00) �¿���������٣�쳼ã�����Ӱ뵺�����ܶ�Ⱥ��;
            TIME_ZONE_32 //(GMT+13:00) Ŭ�Ⱒ�巨;
        }
        #endregion

        #region �Զ���ṹ
        //********************���ݽṹ************************************

        public struct INF_NET_CLIENTINFO
        {
            public int lChannel; //ͨ����
            public int lLinkMode; //1��ʾΪ��������2Ϊ������
            public IntPtr hPlayWnd; //���Ŵ��ڵľ��
            public string snmpUsername; //SNMP�û���,�˲���Ϊ��Ч���������¼�ṹ��LPINF_NET_DVR_LONGININFO�в���*sUserName������ͬ����������汾ɾ���˲���
            public string strSMTIP; //SMTIP
            public ushort wSMTPort; //SMT Port
        }

        //*****************�������******************************
        //���뱨��
        public struct INF_NET_INPUTALARM
        {
            public string cSendAlarmIP; //���ͱ�����IP
            public int iSendIPLength; //���ͱ���IP�ĳ���
            public uint channel; //���ִ����Ӧ��ͨ���ţ�1��ʾ1ͨ����16��ʾ16ͨ��
            public byte flag; //1-�б�����0-�ޱ���
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] res; //����
        }

        //�ƶ�֡�ⱨ��
        public struct INF_NET_MOTIONALARM
        {
            public string cSendAlarmIP; //���ͱ�����IP
            public int iSendIPLength; //���ͱ���IP�ĳ���
            public uint channel; //���ִ����Ӧ��ͨ����
            public byte flag; //1-�б�����0-�ޱ���
            public byte area; //�ĸ��������ִ����Ӧ������
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] res; //������
        }

        //��Ƶ�ڵ�����
        public struct INF_NET_OBSTRUCTALARM
        {
            public string cSendAlarmIP; //���ͱ�����IP
            public int iSendIPLength; //���ͱ���IP�ĳ���
            public uint channel; //�ĸ�ͨ�������ִ����Ӧ��ͨ����
            public byte flag; //1-�б�����0-�ޱ���
            public byte area; //�ĸ��������ִ����Ӧ������
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] res; //����
        }

        //��Ƶ��ʧ����
        public struct INF_NET_VIDEOLOSTALARM
        {
            public string cSendAlarmIP; //���ͱ�����IP
            public int iSendIPLength; //���ͱ���IP�ĳ���
            public uint channel; // ���ִ����Ӧ��ͨ����
            public byte flag; //1-�б�����0-�ޱ���
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] res; //����
        }

        //�豸�쳣����
        public struct INF_NET_DEVEXPALARM
        {
            public string cSendAlarmIP; //���ͱ�����IP
            public int iSendIPLength; //���ͱ���IP�ĳ���
            public uint mintype; // 1-Ӳ������ 2-Ӳ�̳��� 3-����Ͽ��� 4-�Ƿ����ʣ� 5-�����ͻ
            public byte flag; //1-�б�����0-�ޱ���
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] res; //����
        }

        //�����������豸�Ŀ�����Ϣ��
        public struct INF_NET_OUTPUTALARM
        {
            public string cSendAlarmIP; //���ͱ�����IP
            public int iSendIPLength; //���ͱ���IP�ĳ���
            public uint channel; //���ִ����Ӧ��ͨ����
            public byte flag; //1��ʾ�п������ ��0��
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] res; //����
        } //�������

        //**********************�������*************************
        //�豸״̬��Ϣ      					
        public struct INF_NET_DEVICEINFO
        {
            public byte byTotalCHs; //ͨ������
            public byte byTotalHDs; //Ӳ������
            //	BYTE	byBoots;					//�������ܴ���
            public byte byDevId; //�豸ID
            public byte byOganic; //��ʽ��1--PAL ��2--NTSC ��ֻ����
            [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = 32)]
            public string sName; //�豸����
            [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = SERIALNO_LEN)]
            public string sdevSN; //�豸���к�
            [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = 32)]
            public string sdecodeVer; //����汾
            [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = 32)]
            public string sencodeVer; //����汾
            [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = 32)]
            public string shWareVer; //Ӳ���汾
            [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = 32)]
            public string sSoftVer; //����汾
            [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = 32)]
            public string sbootTime; //ϵͳ����ʱ�� (��ʽ��2008-11-07 11:45:00;)
        }

        //����������Ϣ      					
        public struct INF_NET_CARD
        {
            public byte byNetworkCard; //�������� 0-100M/1000M����Ӧ��1-1000M��2-100M��3-10M
            public byte byNetType; //������������ 0-static��1-DHCP��2-3PoE
            public byte byAutoDns; //�����Զ�Dns�������� 1-�Զ�DNS��0-��ֹ�Զ�DNS
            public uint dwPort; //�����˿�
            [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = 16)]
            public string sDns; //Dns��������ַ
            [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = 16)]
            public string sbCast; //�㲥���ַ��������
            [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = 16)]
            public string smCast; //�ಥ���ַ��������
            [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = 16)]
            public string sPppoeIp; //3PoE��ַ
            [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = PPPOE_NAMELEN + 1)]
            public string sPppoeUser; //3PoE�û���
            [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = PPPOE_PWDLEN + 1)]
            public string sPppoePsw; //3PoE����
            [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = 16)]
            public string sMac; //���������ַ
            [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = 16)]
            public string sIp; //����IP��ַ
            [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = 16)]
            public string sMask; //��������
            [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = 16)]
            public string sGateway; //���ص�ַ
        }

        //���繫��������Ϣ        				
        public struct INF_NET_PUBLICINFO
        {
            public uint dwWebPort; //web�˿�
            public uint dwSnmpPort; //snmp�˿�
            public uint dwHostPort; //�����˿�
            [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = 16)]
            public string sNasIp; //Nas������ַ
            [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = 16)]
            public string sHostIp; //������ַ
            [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = PATHNAME_LEN)]
            public string sNasPath; //Nas·����������
        }

        //ͨ��������Ϣ      					
        public struct INF_NET_CHINFO
        {
            [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = CHANNAME_LEN + 1)]
            public string sChDevName; //ͨ����
        }

        //����������Ϣ      					
        public struct INF_NET_STREAMINFO
        {
            public byte byMixStream; //�������͡���0: video stream������1: V & A stream
            public uint dwVopRate; //֡�ʡ�������PAL�ƣ�1~25����������NETS�ƣ�1 ~ 30
            public uint dwIpRate; //IP���� I֡���10 ~ 255
            public byte byQuality; //ͼ������ ͼ��������0: lowest��1: low��2: middle��3: high��4: highest
            public byte byResolution; //�ֱ��� ������ʱ��0: QCIF��1: CIF��2: 2CIF��3: 4CIF�����������ֱ��ʲ���С���������ֱ��� ������ʱ�� 0: QCIF��1: CIF
            public uint dwBitRate; //λ������ 0: �����ʣ�1: ������
            public uint dwMaxBitRate; //λ������ 0��256kps��1��512kps;2��640kps;3:768kps;4:896kps;5:1024kps;6:1280kps;7:1536kps;8:1792kps;9:2048kps;10:2560kps
        }

        //�ַ�����ʾ��Ϣ
        public struct INF_NET_TEXT
        {
            public byte byTextSta; //��ʾ״̬ 1����ʾ�� 0������ʾ
            public int iTextX; //�ַ���X����
            public int iTextY; //�ַ���Y����
            [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = TEXT_LEN + 1)]
            public string sText; //�ַ�������
        }

        //OSD������Ϣ    						
        public struct INF_NET_OSDINFO
        {
            //	BYTE		  byAttribute;			//OSD����  0:͸�������� 1:͸����������  2:��������͸�� 3: ��͸����������
            public byte byWeekStatus; //������ʾ 1����ʾ�� 0������ʾ
            public byte byTimeFormat; //ʱ���ʽ 0: YYYY-MM-DD, 1: YYYY MM DD 2:YYYY/MM/DD, 3: MM/DD/YYYY
            public byte byTimeSta; //ʱ����ʾ 1����ʾ�� 0������ʾ
            public int iTimeX; //x����
            public int iTimeY; //y����
            public byte byNameSta; //ͨ��������ʾ
            public int iNameX; //ͨ�����Ƶ�x����
            public int iNameY; //ͨ�����Ƶ�y����
            [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = MAX_TEXT_NUM)]
            public INF_NET_TEXT[] struText; //�ַ�����ʾ
        }

        //����������ʽ��Ϣ
        public struct INF_NET_ALARMTRIG
        {
            public byte byPlayAlarm; //Ԥ������ 1-ѡ��0-��ѡ��
            public byte byVoiceAlarm; //�������� 1-ѡ��0-��ѡ��
            public byte byCenterAlarm; //�ϴ��������� 1-ѡ��0-��ѡ��
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_TEXT_NUM)]
            public byte[] byAlarmOut; //����������� 1-ѡ��0-��ѡ��
        }


        //����������Ϣ
        public struct INF_NET_AREASET
        {
            public int iX; //�����귶Χ(0,22),�����Ͻ�Ϊԭ�㣬һ����Ϊ ��22 ,��18�����ӣ��ú�������ȷ��һ�㡣
            public int iY; //�����귶Χ(0,18)��
            public int iWidth; //�����
            public int iHeight; //�ݸ���
        }

        //PTZ������Ϣ
        public struct INF_NET_PTZ
        {
            public byte bySelect; // ѡ��״̬��1��Ԥ��λ��2���������ã�3:���߶�ûѡ
            public int iPtzValue; //�� bySelect = 1ʱ����ʾԤ��λ����Χ��1��255
            //�� bySelect = 2ʱ����ʾ�������ã���ΧΪ��1 �C 4
            //�� bySelect = 3ʱ����ʾ��ֵ��ȡֵΪ0
        }


        //�ƶ����������Ϣ        				
        public struct INF_NET_MOTION
        {
            public byte bySensitivity; //���������� 0:�������� 1���������� 2����������
            public byte byStatus; //ѡ��״̬ 1-ѡ��0-��ѡ��
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM)]
            public byte[] byRecChSta; //����ͨ��״̬ 0--δѡ�� ��1--ѡ��
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = WEEK_DAYS * TIME_PLANS)]
            public byte[,] byAlarmTime; //ʱ�䲼�� 0--��ʾ�ò���ʱ��Ϊδѡ��״̬��1--��ʾ�ò���ʱ��Ϊѡ��״̬
            //WEEK_DAYS��0---6��ʾ���ա�һ����.....�� ��TIME_PLANS:0--47��ʾ00��00--00��30��00��30--01��00.....          		
            public INF_NET_ALARMTRIG struTrig; //������ʽ
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM)]
            public INF_NET_PTZ[] struPtz; //PTZ����
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_AREA)]
            public INF_NET_AREASET[] struAreaSet; //��������

        }

        //�ڵ�����������Ϣ        				
        public struct INF_NET_OBSTRUCT
        {
            public byte bySensitivity; //���� �ƶ����������Ϣ
            public byte byStatus;
            //	BYTE    byRecChSta[MAX_CHANNUM];			 //����ͨ��״̬  0--δѡ�� ��1--ѡ��   //���޸ù���
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = WEEK_DAYS * TIME_PLANS)]
            public byte[,] byAlarmTime; //ʱ�䲼�� 0--��ʾ�ò���ʱ��Ϊδѡ��״̬��1--��ʾ�ò���ʱ��Ϊѡ��״̬
            //WEEK_DAYS��0---6��ʾ���ա�һ����.....�� ��TIME_PLANS:0--47��ʾ00��00--00��30��00��30--01��00.....  
            public INF_NET_ALARMTRIG struTrig; //������ʽ
            public INF_NET_AREASET[] struAreaSet; //��������
        }

        //��Ƶ��ʧ��������������Ϣ				
        public struct INF_NET_SIGLOST
        {
            public byte byStatus;
            //	BYTE    byRecChSta[MAX_CHANNUM];             //����ͨ��״̬  0--δѡ�� ��1--ѡ��      //���޸ù���
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = WEEK_DAYS * TIME_PLANS)]
            public byte[,] byAlarmTime; //ʱ�䲼�� 0--��ʾ�ò���ʱ��Ϊδѡ��״̬��1--��ʾ�ò���ʱ��Ϊѡ��״̬
            //WEEK_DAYS��0---6��ʾ���ա�һ����.....�� ��TIME_PLANS:0--47��ʾ00��00--00��30��00��30--01��00.....          
            public INF_NET_ALARMTRIG struTrig; //������ʽ
        }

        //¼�񲼷�״̬
        public struct INF_NET_REPLAN
        {
            public byte byIsRecord; //¼��״̬��0--��ʾδ¼��1--��ʾ¼��
            public byte byRecordType; //¼�����ͣ�0--2δ¼��1--��ʱ¼��2--����¼��3--��ʱ�ͱ���¼��
        }

        //¼��ƻ�������Ϣ		
        public struct INF_NET_RECORD
        {
            public byte byRecSta; //ѡ��״̬��1 ѡ��0 �෴
            //	BYTE			byTecTo;								//�洢��ʽ��0 ����, 1: ����, 2; ����     //�ݲ�֧�ָù���   
            public int iPreTime; //Ԥ¼ʱ�� 0����Ԥ¼ 1��2s 2:4s 3:6s 4:8s 5:���
            public int iDelayTime; //��ʱʱ�� 0������ʱ 1��5s 2:10s 3:30s 4:1���� 5:2���� 6:5���� 7:10����
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = WEEK_DAYS * TIME_PLANS)]
            public INF_NET_REPLAN[,] struRecordTime; //ʱ�䲼��
            //WEEK_DAYS��0---6��ʾ���ա�һ����.....�� ��TIME_PLANS:0--47��ʾ00��00--00��30��00��30--01��00.....          
        }

        //��˽����������Ϣ						
        public struct INF_NET_PRIVACYAREA
        {
            public byte byStatus; //״̬��1 ѡ��0 �෴
            public INF_NET_AREASET[] struAreaSet; //��������
        }

        //��������������Ϣ						
        public struct INF_NET_ALARMIN
        {
            public byte byStatus; //�������� 0�������� 1������
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = ALARM_LEN + 1)]
            public string sName; //��������
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM)]
            public byte[] byRecChSta; //����ͨ��״̬ 0--δѡ�� ��1--ѡ��
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = WEEK_DAYS * TIME_PLANS)]
            public byte[,] byAlarmTime; //ʱ�䲼�� 0--��ʾ�ò���ʱ��Ϊδѡ��״̬��1--��ʾ�ò���ʱ��Ϊѡ��״̬
            //WEEK_DAYS��0---6��ʾ���ա�һ����.....�� ��TIME_PLANS:0--47��ʾ00��00--00��30��00��30--01��00.....          
            public INF_NET_ALARMTRIG struTrig; //������ʽ
            public INF_NET_PTZ[] struPtz; //PTZ����
        }

        //�������������Ϣ						
        public struct INF_NET_ALARMOUT
        {
            public int iDelayTime; //��������ӳ�ʱ�䣺 0��5��;1:10��;2:30��;3:1����;4:2����;5:5����;6:10����;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = WEEK_DAYS * TIME_PLANS)]
            public byte[,] byAlarmTime; //ʱ�䲼�� 0--��ʾ�ò���ʱ��Ϊδѡ��״̬��1--��ʾ�ò���ʱ��Ϊѡ��״̬
            //WEEK_DAYS��0---6��ʾ���ա�һ����.....�� ��TIME_PLANS:0--47��ʾ00��00--00��30��00��30--01��00.....          
        }

        //�쳣����������Ϣ
        public struct INF_NET_ALARMNOR
        {
            public byte byExceptType; //�쳣��������0-Ӳ����,1- Ӳ�̳���,2-����Ͽ�,3-�Ƿ�����, 4-�����ͻ
            public INF_NET_ALARMTRIG struTrig; //������ʽ
        }

        //���ڲ���������Ϣ
        public struct INF_NET_COM232INFO
        {
            public int iBaudRate; //������ 0:50bps,1:75bps,2:110bps,3:150bps,4:300bps,5:600bps,6:1200bps,7:2400bps,8:4800bps,9:9600bps,10:19200bps,11:38400bps,12:57600bps,13:76800bps,14:115200bps
            public int iDataBit; //����λ 0:5bit; 1:6bit; 2:7bit; 3:8bit
            public int iStopBit; //ֹͣλ 0:1bit; 1:2bit
            public int iParity; //��żУ�� 0:no; 1:odd; 2:even
            public int iFlowCtrl; //���������� 0:no; 1:software; 2:hardware
        }

        public struct INF_NET_COM485INFO
        {
            public int iBaudRate; //������ 0:2400bps,1:4800bps,2:9600bps,3:19200bps,4:38400bps,5:57600bps,6:76800bps,7:115200bps
            public int iDataBit; //����λ 0:5bit; 1:6bit; 2:7bit; 3:8bit
            public int iStopBit; //ֹͣλ 0:1bit; 1:2bit
            public int iParity; //��żУ�� 0:no; 1:odd; 2:even
            public int iFlowCtrl; //���������� 0:no; 1:software; 2:hardware
            public int iProtocolType; //PTZЭ�� 0:infinoval; 1:pelco-D;2:pelco-P
            public int iDecodeAddress; //PTZ��ַ ��Χ��1--255
        }

        //�û�����������Ϣ					
        public struct INF_NET_USERINFO
        {
            public byte byUsertype; //�û����� R 0-����Ա�û���1-��ͨ�û�
            public byte byUserSN; //�û���� R
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = NAME_LEN + 1)]
            public string sUserName; //�û��� R/W ��Χ��1--15
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PASSWD_LEN + 1)]
            public string sPswd; //�û����� W ��Χ��8--15
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
            public string sUserPrivilege; //�û�Ȩ�� R/W
        }

        //Ӳ��״̬��Ϣ
        public struct INF_NET_DISKSTATE
        {
            public byte byDiskID; //Ӳ��ID 1----8
            public byte byUseMode; //Ӳ�̸��ǲ��ԣ�1��ѭ�����ǣ�2����ѭ������
            public byte byDiskStat; // 1:���� ��2������ 3��Ӳ���� 4������ 5����Ӳ�� ��6�����ڸ�ʽ�� ��7��δ��ʽ��
            public uint dwDiskCapacity; //Ӳ�������� ��λΪ�� G
            public uint dwDiskFree; //Ӳ��ʣ����������λΪ�� G
        }

        //ͨ��״̬��Ϣ
        public struct INF_NET_CHANNELSTATE
        {
            public byte byRecordState; //ͨ���Ƿ���¼��,0-��¼��,1-�ֶ�¼��2����ʱ¼��3������¼��
            public byte bySignalState; //ͨ���Ƿ���Ƶ��ʧ,0-��Ƶ����,1-��Ƶ��ʧ
            public byte byAlarmState; //����״̬,0-û�б���,1-�ƶ���ⱨ����2���ڵ�������3���ź�������
        }

        //Զ��¼��״̬��Ϣ
        public struct INF_NET_MANNUALSTATE
        {
            public byte byMannualState; //0--δ¼�� 1--¼��
        }

        //////////////////////////////////////////////////

        //�豸״̬��Ϣ�ṹ��
        public struct INF_NET_WORKSTATE
        {
            //	DWORD					dwRootNum;							//�豸��������	�ò�����Ч
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string sRootTime; //�豸����ʱ�䣺2008-11-07 11:45:00

            public INF_NET_DISKSTATE[] struHDStatic; //Ӳ�̵�״̬
            public INF_NET_CHANNELSTATE[] struChanState; //ͨ����״̬
        }


        //ʱ������ṹ��
        public struct INF_NET_TIME
        {
            public uint dwYear;
            public uint dwMonth;
            public uint dwDay;
            public uint dwHour;
            public uint dwMinute;
            public uint dwSecond;
        }

        //������־��������Ϣ�ṹ
        public struct INF_NET_LOGSEARCHINFO
        {

            public INF_NET_TIME struBeginTime;
            public INF_NET_TIME struEndTime;
            public uint dwMajorType;
            public uint dwMinorType;
        }

        //��־����Ϣ�ṹ
        public struct INF_NET_LOGINFO
        {
            public INF_NET_TIME strLogTime; //�¼�ʱ��
            public uint dwMajorType; //������
            public uint dwMinorType; //������
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string sDescription; //�¼�
        }

        //����¼���ļ���Ϣ�ṹ��
        public struct INF_NET_FILECOND
        {
            public int lChannel; // 1ͨ�� �� 0�� 2ͨ����1��...16ͨ����15 ,ȫ��ͨ����-1
            public uint dwFileType;
            public INF_NET_TIME struStartTime;
            public INF_NET_TIME struStopTime;
        }

        public struct INF_NET_FINDDATA
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string sFileName; //�ļ���
            public INF_NET_TIME struStartTime; //�ļ��Ŀ�ʼʱ��
            public INF_NET_TIME struStopTime; //�ļ��Ľ���ʱ��
            public uint dwFileSize; //�ļ��Ĵ�С
            public byte byFileType; //1����ʱ��2���ֶ���3������ 0XFFFF : ȫ��
        }

        //��¼��Ϣ�ṹ��						
        public struct INF_NET_DVR_LONGININFO
        {
            public string sDevIp; //������IP
            public string sUserName; //�������û���
            public string sPrivKey; //�û�˽Կ
            public string sAuthKey; //�û���Կ
            public ushort wServerPort; //�������˿� wDeviceType = 0ʱ��wServerPortΪ��Чֵ0�� wDeviceType = 1ʱ��wServerPortΪ�������˿ںţ���ЧֵΪ0--65535
            public ushort wDeviceType; //�豸���� 0:�����汾DVR 1:ͳһ�˿�DVR
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
            public string Res; //�����ֶ�
        }

        //ʱ����Ϣ�ṹ��
        public struct INF_NET_SYSTIME
        {
            public byte byNTP; //�Ƿ�����NTPУʱʱ, 1:�ǣ�0����
            public byte byPtz; //ʱ��(��ֵ��0~32)���ο�TIME_ZONE
            public byte byDayLight; //�Ƿ�������ʱ�0:�ر� 1:����
            public byte byRes; //--�����ֶ�--
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public string cIP; //NTP������IP
        }
        #endregion

        #region ����
        public static bool bInt = false;

        public const string SDK_INF = @".\Driver\infinova\INF_DVRNETSDK.dll";
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_Init();
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_Cleanup();
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_CheckDeviceType(string sDevIp, uint dwServerPort, string sUserName, string sAuthKey);
        [DllImport(SDK_INF)]//ע���û�
        public static extern int INF_NET_DVR_Login_V20(ref INF_NET_DVR_LONGININFO lpLoginInfo); //ע���û�ͳһ�汾
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_Logout(int lUserID);//ע���û�
        [DllImport(SDK_INF)]
        public static extern uint INF_NET_DVR_GetLastError();//��ȡ������
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_RealPlay(int lUserID, ref INF_NET_CLIENTINFO lpClientInfo, fStdDataCallBack cbStdDataCallBack, IntPtr pUser);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_StopRealPlay(int lRealHandle);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_SetDVRConfig(int lUserID, uint dwCommand, int lChannel, IntPtr lpInBuffer, uint dwInBufferSize);
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_GetDVRConfig(int lUserID, uint dwCommand, int lChannel, IntPtr lpOutBuffer, ref uint lpBytesReturned);   //�����û���Ϣ
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_StartListen(string sLocalIP, ushort wLocalPort, fAlarmCallBack cbMessageCallBack, IntPtr pUser);      //��ʼ����
        [DllImport(SDK_INF)]
        public static extern int INF_NET_DVR_StopListen(int lListenHandle);      //ֹͣ����
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





