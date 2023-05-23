#define V4
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace TandiNVSSDK
{


    [StructLayout(LayoutKind.Sequential)]
    public struct NVS_FILE_QUERY
    {
        public int iType;
        public int iChannel;
        public NVS_FILE_TIME struStartTime;
        public NVS_FILE_TIME struStopTime;
        public int iPageSize;
        public int iPageNo;
        public int iFiletype;
        public int iDevType;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct NVS_FILE_TIME
    {
        public ushort iYear;
        public ushort iMonth;
        public ushort iDay;
        public ushort iHour;
        public ushort iMinute;
        public ushort iSecond;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct NVS_FILE_DATA
    {
        public int iType;
        public int iChannel;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 250)]
        public char[] cFileName;
        public NVS_FILE_TIME struStartTime;
        public NVS_FILE_TIME struStoptime;
        public int iFileSize;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct CONNECT_STATE
    {
        public int m_iLogonID;
        public int m_iChannelNO;
        public int m_iStreamNO;
        public UInt32 m_uiConID;
    };
    [StructLayout(LayoutKind.Sequential)]
    public struct CLIENTINFO
    {
        public int m_iServerID;        //NVS ID,NetClient_Logon 返回值
        public int m_iChannelNo;	    //Remote host to be connected video channel number (Begin from 0)
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 255)]
        public Char[] m_cNetFile;    //Play the file on net, not used temporarily
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public Char[] m_cRemoteIP;  //IP address of remote host
        public int m_iNetMode;          //Select net mode 1--TCP  2--UDP  3--Multicast
        public int m_iTimeout;          //Timeout length for data receipt
        public int m_iTTL;			    //TTL value when Multicast
        public int m_iBufferCount;     //Buffer number
        public int m_iDelayNum;        //Start to call play progress after which buffer is filled
        public int m_iDelayTime;       //Delay time(second), reserve
        public int m_iStreamNO;        //Stream type
        public int m_iFlag;         //0，首次请求该录像文件；1，操作录像文件
        public int m_iPosition;     //0～100，定位文件播放位置；-1，不进行定位
        public int m_iSpeed;			//1，2，4，8，控制文件播放速度        
    };
    [StructLayout(LayoutKind.Sequential)]
    public struct st_ConnectInfo
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public Char[] m_cRemoteIP;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public Char[] m_cDeviceID;
        public int m_iLogonID;
        public int m_iChannel;
        public int m_iStreamNO;
        public int m_iDrawIndex;
    }


    [StructLayout(LayoutKind.Sequential)]
    public struct NVS_RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct NVS_IPAndID
    {
        public string m_pIP;
        public string m_pID;
        public UInt32 m_puiLogonID;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct NVS_SCHEDTIME
    {
        public UInt16 m_ustStartHour;
        public UInt16 m_usStartMin;
        public UInt16 m_ustStopHour;
        public UInt16 m_ustStopMin;
        public UInt16 m_ustRecordMode;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct STR_VideoParam
    {
        public UInt16 m_ustBrightness;             //亮度
        public UInt16 m_usHue;                     //色度
        public UInt16 m_ustContrast;               //对比度
        public UInt16 m_ustSaturation;             //饱和度
        [MarshalAs(UnmanagedType.Struct)]
        public NVS_SCHEDTIME m_strctTempletTime;   //时间模板        
    }

    //Ctrl param
    [StructLayout(LayoutKind.Sequential)]
    public struct CONTROL_PARAM
    {
        public Int32 m_iAddress;   //device address
        public Int32 m_iPreset;	   //preset pos
        [MarshalAs(UnmanagedType.Struct)]
        public POINT m_ptMove;     //move pos
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public byte[] m_btBuf;     //Ctrl-Code(OUT)
        public Int32 m_iCount;     //Ctrl-Code count(OUT)
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public Int32 x;
        public Int32 y;
    };

    [StructLayout(LayoutKind.Sequential)]
    public class Reserve
    {
        public Int32 m_iReserved1;
        public UInt32 m_ustReserved2;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] m_btReserved1;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public byte[] m_btReserved2;
        public Reserve()
        {
            m_iReserved1 = new Int32();
            m_ustReserved2 = new UInt32();
            m_btReserved1 = new byte[32];
            m_btReserved2 = new byte[64];
        }
    };

    [StructLayout(LayoutKind.Sequential)]
    public class NvsSingle
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] m_btNvsIP;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] m_btNvsName;
        public Int32 m_iNvsType;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] m_btFactoryID;
        [MarshalAs(UnmanagedType.Struct)]
        public Reserve m_stReserve;
        public NvsSingle()
        {
            m_btNvsIP = new byte[32];
            m_btNvsName = new byte[32];
            m_btFactoryID = new byte[32];
            m_stReserve = new Reserve();
        }

    };

    [StructLayout(LayoutKind.Sequential)]
    public class DNSRegInfo
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] m_btUserName;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] m_btPwd;
        [MarshalAs(UnmanagedType.Struct)]
        public NvsSingle m_stNvs;
        public Int32 m_iPort;
        public Int32 m_iChannel;
        [MarshalAs(UnmanagedType.Struct)]
        public Reserve m_stReserve;
        public DNSRegInfo()
        {
            m_btUserName = new byte[32];
            m_btPwd = new byte[32];
            m_stNvs = new NvsSingle();
            m_stReserve = new Reserve();
        }
    };

    [StructLayout(LayoutKind.Sequential)]
    public class REG_DNS
    {
        [MarshalAs(UnmanagedType.Struct)]
        public DNSRegInfo m_stDNSInfo;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] m_btRegTime;
        [MarshalAs(UnmanagedType.Struct)]
        public Reserve m_stReserve;
        public REG_DNS()
        {
            m_stDNSInfo = new DNSRegInfo();
            m_btRegTime = new byte[32];
            m_stReserve = new Reserve();
        }
    };

    [StructLayout(LayoutKind.Sequential)]
    public class REG_NVS
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] m_btPrimaryDS;
        [MarshalAs(UnmanagedType.Struct)]
        public NvsSingle m_stNvs;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] m_btRegTime;
        public UInt32 m_uiClientConnNum;
        public Boolean m_blRegister;
        [MarshalAs(UnmanagedType.Struct)]
        public Reserve m_stReserve;
        public REG_NVS()
        {
            m_btPrimaryDS = new byte[32];
            m_stNvs = new NvsSingle();
            m_btRegTime = new byte[32];
            m_stReserve = new Reserve();
        }
    };

    public delegate void RECVDATA_NOTIFY(uint _ulID, string _strData, int _iLen);
    public delegate void DNSList_NOTIFY(Int32 _iCount, IntPtr _pDns);
    public delegate void NVSList_NOTIFY(Int32 _iCount, IntPtr _pNvs);
    public delegate void NVSDATA_NOTIFY(uint _ulID, string _strData, int _iLen, int _iUser);
    public delegate void RECVDATA_NOTIFY_EX(uint _ulID, IntPtr _strData, int _iLen, int _iFlag, int _lpUserData);

    public class NVSSDK
    {

        public const string SDK_PATH = @".\Driver\TandiDvr\NVSSDK.dll";
        public static bool bInit = false;
        public static bool bCleaup = false;
        public static bool NetClient_Cleanup2()
        {
            if (bCleaup)
            {
                if (NetClient_Cleanup() == 0)
                {
                    NVSSDK.NSLook_Cleanup();

                    bCleaup = true;
                    bInit = false;
                }
            }
            return true;
        }
        public static bool NetClient_Startup2()
        {
            if (!bInit)
            {
                int i = NetClient_Startup();
                Debug.WriteLine("NetClient_Startup:" + i.ToString());
                if (i == 0)
                {
                    i = NVSSDK.NSLook_Startup();
                    Debug.WriteLine("NSLook_Startup:" + i.ToString());
                    bInit = true;
                    bCleaup = false;
                    return true;
                }
                else
                    return false;
            }
            return true;
        }

        public const int TYPE_NVS = 0;       //nvs
        public const int TYPE_PROXY = 1;       //代理服务器
        public const int TYPE_CLIENT = 2;       //待连接的客户端
        public const int TYPE_TRANSFER = 3;       //视频转发关系
        public const int TYPE_ASSIGN = 4;       //代理分配
        public const int TYPE_DNS = 5;       //域名解析
        public const int TYPE_DS = 6;       //二级注册中心
        public const int TYPE_P2P_NVS = 7;       //p2p nvs
        public const int TPYE_P2P_CLIENT = 8;       //使用P2P连接方式的客户端

        public const int MOVE_UP = 1; //云台向上 
        public const int MOVE_UP_STOP = 2; //云台向上停 
        public const int MOVE_DOWN = 3; //云台向下 
        public const int MOVE_DOWN_STOP = 4; //云台向下停 
        public const int MOVE_LEFT = 5; //云台向左 
        public const int MOVE_LEFT_STOP = 6; //云台向左停 
        public const int MOVE_RIGHT = 7; //云台向右 
        public const int MOVE_RIGHT_STOP = 8; //云台向右停 
        public const int MOVE_UP_LEFT = 9; //云台左上 
        public const int MOVE_UP_LEFT_STOP = 10; //云台左上停 
        public const int MOVE_UP_RIGHT = 11; //云台右上 
        public const int MOVE_UP_RIGHT_STOP = 12; //云台右上停 
        public const int MOVE_DOWN_LEFT = 13; //云台左下 
        public const int MOVE_DOWN_LEFT_STOP = 14; //云台左下停 
        public const int MOVE_DOWN_RIGHT = 15; //云台右下 
        public const int MOVE_DOWN_RIGHT_STOP = 16; //云台右下停 
        [DllImport(SDK_PATH)]
        public static extern Int32 NetClient_GetInfoByConnectID(UInt32 _iConnectID, ref st_ConnectInfo _stConnectInfo);

        [DllImport(SDK_PATH)]
        public static extern Int32 NetClient_SetPort(Int32 _iServerPort, Int32 _iClientPort);
        [DllImport(SDK_PATH)]
        public static extern Int32 NetClient_SetMSGHandle(UInt32 _uiMessage, IntPtr _hWnd, UInt32 _uiParaMsg, UInt32 _uiAlarmMsg);
        [DllImport(SDK_PATH)]
        public static extern Int32 NetClient_Startup();
        [DllImport(SDK_PATH)]
        public static extern Int32 NetClient_Startup_V4(int _iServerPort, int _iClientPort, int _iWnd);
        [DllImport(SDK_PATH)]
        public static extern Int32 NetClient_Cleanup();
        [DllImport(SDK_PATH)]
        public static extern Int32 NetClient_Logon(String _cProxy, String _cIP, String _cUserName, String _cPassword, String _pcProID, Int32 _iPort);
        [DllImport(SDK_PATH)]
        public static extern Int32 NetClient_Logoff(Int32 _iLogonID);
        [DllImport(SDK_PATH)]
        public static extern Int32 NetClient_GetLogonStatus(Int32 _iLogonID);
        [DllImport(SDK_PATH)]
        public static extern Int32 NetClient_StartRecv(ref UInt32 _uiConID, ref CLIENTINFO _cltInfo, RECVDATA_NOTIFY _cbkDataArrive);
        [DllImport(SDK_PATH)]
        public static extern Int32 NetClient_StartRecv_V4(ref UInt32 _uiConID, ref CLIENTINFO _cltInfo, NVSDATA_NOTIFY _cbkDataArrive, int _iUserData);

        [DllImport(SDK_PATH)]
        public static extern Int32 NetClient_StopRecv(UInt32 _uiConID);
        [DllImport(SDK_PATH)]
        public static extern Int32 NetClient_StartCaptureData(UInt32 _uiConID);
        [DllImport(SDK_PATH, EntryPoint = "NetClient_StopCaptureDate")]
        public static extern Int32 NetClient_StopCaptureData(UInt32 _uiConID);
        [DllImport(SDK_PATH)]
        public static extern Int32 NetClient_StartPlay(UInt32 _uiConID, IntPtr _hWnd, NVS_RECT _rcShow, UInt32 _iDecflag);
        [DllImport(SDK_PATH)]
        public static extern Int32 NetClient_StopPlay(UInt32 _uiConID);
        [DllImport(SDK_PATH)]
        public static extern Int32 NetClient_GetPlayingStatus(UInt32 _uiConID);
        [DllImport(SDK_PATH)]
        public static extern Int32 NetClient_StartCaptureFile(UInt32 _uiConID, string _strFileName, Int32 _iRecFileType);
        [DllImport(SDK_PATH)]
        public static extern Int32 NetClient_StopCaptureFile(UInt32 _uiConID);
        [DllImport(SDK_PATH)]
        public static extern Int32 NetClient_CaptureBmpPic(UInt32 _uiConID, string _strFileName);
        [DllImport(SDK_PATH)]
        public static extern Int32 NetClient_GetOsdText(Int32 _iLogonID, Int32 _iChannelNum, byte[] _btOSDText, ref UInt32 _ulTextColor);
        [DllImport(SDK_PATH, SetLastError = true)]
        public static extern Int32 NetClient_SetOsdText(Int32 _iLogonID, Int32 _iChannelNum, byte[] _btOSDText, UInt32 _ulTextColor);
        [DllImport(SDK_PATH)]
        public static extern Int32 NetClient_GetOsdType(Int32 _iLogonID, Int32 _iChannelNum, Int32 _iPositionX, ref Int32 _iPositionY, ref Int32 _iOSDType, ref Int32 _iEnabled);
        [DllImport(SDK_PATH, SetLastError = true)]
        public static extern Int32 NetClient_SetOsdType(Int32 _iLogonID, Int32 _iChannelNum, Int32 _iPositionX, Int32 _iPositionY, Int32 _iOSDType, Int32 _iEnabled);
        [DllImport(SDK_PATH)]
        public static extern Int32 NetClient_GetComPortCounts(Int32 _iLogonID, ref Int32 _iComPortCounts, ref Int32 _iComPortEnabledStatus);
        [DllImport(SDK_PATH)]
        public static extern Int32 NetClient_GetDeviceType(Int32 _iLogonID, Int32 _iChannelNum, ref Int32 _iComNo, ref Int32 _iDevAddress, StringBuilder _strDevType);
        [DllImport(SDK_PATH)]
        public static extern Int32 NetClient_SetDeviceType(Int32 _iLogonID, Int32 _iChannelNum, Int32 _iComNo, Int32 _iDevAddress, byte[] _btDevType);
        [DllImport(SDK_PATH, SetLastError = true)]
        public static extern Int32 NetClient_GetComFormat(Int32 _iLogonID, Int32 _iComNo, StringBuilder _strComFormat, ref Int32 _iWorkMode);
        [DllImport(SDK_PATH, SetLastError = true)]
        public static extern Int32 NetClient_SetComFormat(Int32 _iLogonID, Int32 _iComNo, byte[] _btDeviceType, byte[] _btComFormat, Int32 _iWorkMode);
        [DllImport(SDK_PATH, EntryPoint = "NetClient_GetVideoPara")]
        public static extern Int32 NetClient_GetVideoParam(Int32 _iLogonID, Int32 _iChannelNum, ref STR_VideoParam _structVideoParam);
        [DllImport(SDK_PATH, EntryPoint = "NetClient_SetVideoPara", SetLastError = true)]
        public static extern Int32 NetClient_SetVideoParam(Int32 _iLogonID, Int32 _iChannelNum, ref STR_VideoParam _structVideoParam);
        [DllImport(SDK_PATH)]
        public static extern Int32 NetClient_ResetPlayerWnd(UInt32 _uiConID, IntPtr _hwnd);
        [DllImport(SDK_PATH)]
        public static extern Int32 NetClient_GetChannelNum(Int32 _iLogonID, ref Int32 _iChannelNum);
        [DllImport(SDK_PATH)]
        public static extern Int32 NetClient_GetCaptureStatus(UInt32 _uiConID);
        [DllImport(SDK_PATH, SetLastError = true)]
        public static extern Int32 NetClient_DeviceCtrlEx(Int32 _iLogonID, Int32 _iChannelNum, Int32 _iActionType, Int32 _iParam1, Int32 _iParam2, Int32 _iControlType);
        [DllImport(SDK_PATH, SetLastError = true)]
        public static extern Int32 NetClient_ComSend(Int32 _iLogonID, byte[] _btBuf, Int32 _iLength, Int32 _iComNo);
        [DllImport(@".\Driver\TandiDvr\DeviceDll\DOME_PELCO_D.dll", EntryPoint = "GetControlCode", SetLastError = true)]
        private static extern Int32 GetControlCode_DOME_PELCO_D(Int32 _iAction, ref CONTROL_PARAM _cParam);
        [DllImport(@".\Driver\TandiDvr\DeviceDll\DOME_PELCO_P.dll", EntryPoint = "GetControlCode", SetLastError = true)]
        private static extern Int32 GetControlCode_DOME_PELCO_P(Int32 _iAction, ref CONTROL_PARAM _cParam);
        [DllImport(@".\Driver\TandiDvr\DeviceDll\DOME_TIANDY.dll", EntryPoint = "GetControlCode", SetLastError = true)]
        private static extern Int32 GetControlCode_DOME_TIANDY(Int32 _iAction, ref CONTROL_PARAM _cParam);
        [DllImport(@".\Driver\TandiDvr\DeviceDll\PTZ_PELCO_D.dll", EntryPoint = "GetControlCode", SetLastError = true)]
        private static extern Int32 GetControlCode_PTZ_PELCO_D(Int32 _iAction, ref CONTROL_PARAM _cParam);
        [DllImport(@".\Driver\TandiDvr\DeviceDll\PTZ_PELCO_P.dll", EntryPoint = "GetControlCode", SetLastError = true)]
        private static extern Int32 GetControlCode_PTZ_PELCO_P(Int32 _iAction, ref CONTROL_PARAM _cParam);
        [DllImport(@".\Driver\TandiDvr\DeviceDll\PTZ_TC615_P.dll", EntryPoint = "GetControlCode", SetLastError = true)]
        private static extern Int32 GetControlCode_PTZ_TC615_P(Int32 _iAction, ref CONTROL_PARAM _cParam);
        public static Int32 NetClient_GetControlCode(string strDevType, Int32 _iAction, ref CONTROL_PARAM _cParam)
        {
            if (strDevType == "DOME_PELCO_D")
            {
                return GetControlCode_DOME_PELCO_D(_iAction, ref _cParam);
            }
            if (strDevType == "DOME_PELCO_P")
            {
                return GetControlCode_DOME_PELCO_P(_iAction, ref _cParam);
            }
            if (strDevType == "DOME_TIANDY")
            {
                return GetControlCode_DOME_TIANDY(_iAction, ref _cParam);
            }
            if (strDevType == "PTZ_PELCO_D")
            {
                return GetControlCode_PTZ_PELCO_D(_iAction, ref _cParam);
            }
            if (strDevType == "PTZ_PELCO_P")
            {
                return GetControlCode_PTZ_PELCO_P(_iAction, ref _cParam);
            }
            if (strDevType == "PTZ_TC615_P")
            {
                return GetControlCode_PTZ_TC615_P(_iAction, ref _cParam);
            }
            return -1;
        }

        //NSLook
        [DllImport(@".\Driver\TandiDvr\nslook.dll")]
        public static extern Int32 NSLook_Startup();
        [DllImport(@".\Driver\TandiDvr\nslook.dll")]
        public static extern Int32 NSLook_Cleanup();
        [DllImport(@".\Driver\TandiDvr\nslook.dll")]
        public static extern Int32 NSLook_LogonServer(byte[] _btServer, Int32 _iServerPort, Boolean _blRepeat);
        [DllImport(@".\Driver\TandiDvr\nslook.dll")]
        public static extern Int32 NSLook_LogoffServer(Int32 _iID);
        [DllImport(@".\Driver\TandiDvr\nslook.dll", SetLastError = true)]
        public static extern Int32 NSLook_Query(Int32 _iID, IntPtr _pDvs, IntPtr _pNvs, Int32 _iType);
        [DllImport(@".\Driver\TandiDvr\nslook.dll")]
        public static extern Int32 NSLook_GetCount(Int32 _iID, byte[] _btUserName, byte[] _btPwd, ref Int32 _iCount, Int32 _iType);
        [DllImport(@".\Driver\TandiDvr\nslook.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 NSLook_GetList(Int32 _iID, byte[] _btUserName, byte[] _btPwd, Int32 _iPageIndex, DNSList_NOTIFY _pGetDNS, NVSList_NOTIFY _pGetNVS, Int32 _iType);

        [DllImport(SDK_PATH)]
        public static extern Int32 NetClient_NetFileQuery(int _iLogonID, ref NVS_FILE_QUERY _fileQuery);
        [DllImport(SDK_PATH)]
        public static extern Int32 NetClient_NetFileGetFileCount(int _iLogonID, ref int _iTotalCount, ref int _iCurrentCount);
        [DllImport(SDK_PATH)]
        public static extern Int32 NetClient_NetFileGetQueryfile(int _iLogonID, int _iFileIndex, ref NVS_FILE_DATA _fileInfo);
        [DllImport(SDK_PATH)]
        public static extern Int32 NetClient_NetFileDownloadFile(ref uint _ulConID, int _iLogonID, string _cRemoteFilename, string _cLocalFilename, int _iFlag, int _iPosition, int _iSpeed);
        [DllImport(SDK_PATH)]
        public static extern Int32 NetClient_SetNetFileDownloadFileCallBack(uint _ulConID, RECVDATA_NOTIFY_EX _cbkDataNotify, IntPtr _lpUserData);
        [DllImport(SDK_PATH)]
        public static extern Int32 NetClient_NetFileStopDownloadFile(uint _ulConID);
    }
}
