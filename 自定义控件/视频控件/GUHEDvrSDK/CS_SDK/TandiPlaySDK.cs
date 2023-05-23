using System;
using System.Runtime.InteropServices;

namespace TandiPlayCSharp
{
    //速度控制类型
    enum eSpeedType
    {
        SPEED_DEC = 0,
        SPEED_ADD,
        SPEED_SET
    }

    //矩形结构
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public Int32 left;
        public Int32 top;
        public Int32 right;
        public Int32 bottom;
    }

    //版本信息结构
    [StructLayout(LayoutKind.Sequential)]
    public struct SDK_VERSION
    {
        public UInt16 m_ulMajorVersion;
        public UInt16 m_ulMinorVersion;
        public UInt16 m_ulBuider;
        public String m_cVerInfo;
    }

    //文件播放完毕事件委托
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]//C接口声明
    public delegate void delegatePlayEndFun(int _iID);
    public class TandiPlaySDK
    {
        public const string SDK_PATH = @".\Driver\TandiDvr\PlaySdkM4.dll";
        //发送消息API
        [DllImport("User32.dll")]
        public static extern bool PostMessage(IntPtr _hWnd, Int32 _msg, Int32 _wParam, Int32 _lParam);
        //系统创建与释放
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_CreateSystem(IntPtr _hWnd);
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_DeleteSystem();

        //创建和关闭播放实例
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_CreatePlayerFromFile(IntPtr _hWnd, String _pcFileName, Int32 _iDownloadBufSz,
            Int32 _iFileTrueSz, ref Int32 _piNowSz, Int32 _iLastFrmNo, ref Int32 _piCompleteFlag);
        //IntPtr形式
        //[DllImport(SDK_PATH)]
        //public static extern Int32 TC_CreatePlayerFromFile(IntPtr _hWnd, String _pcFileName, Int32 _iDownloadBufSz,
        //    Int32 _iFileTrueSz, IntPtr _piNowSz, Int32 _iLastFrmNo, IntPtr _piCompleteFlag);


        [DllImport(SDK_PATH)]
        public static extern Int32 TC_DeletePlayer(Int32 _iID);


        //注册消息和回调
        [DllImport(SDK_PATH)]
        public static extern void TC_RegisterEventMsg(IntPtr _hEventWnd, UInt32 _uiEventMsg);
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_RegisterNotifyPlayToEnd(delegatePlayEndFun _PlayEndFun);
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_RegisterNotifyGetDecAV(Int32 _iID, IntPtr _GetDecAVCbk, bool _blDisplay);

        //播放控制
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_Play(Int32 _iID);
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_Pause(Int32 _iID);
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_Stop(Int32 _iID);
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_FastForward(Int32 _iID, Int32 _iSpeed);
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_FastBackward(Int32 _iID);
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_GoBegin(Int32 _iID);
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_GoEnd(Int32 _iID);
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_PlayAudio(Int32 _iID);
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_StopAudio(Int32 _iID);
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_Seek(Int32 _iID, Int32 _iFrameNO);
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_SeekEx(Int32 _iID, Int32 _iFrameNo);//按帧精确定位，速度比TC_Seek快
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_SetAudioVolumn(UInt16 _ustVolumn);
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_SetPlayRect(Int32 _iID, ref RECT _pDrawRect);
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_SetPlayRectEx(Int32 _iID, ref RECT _pDrawRect, Int32 _dwMask);
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_SlowForward(Int32 _iID, Int32 _iSpeed);
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_StepBackward(Int32 _iID);
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_StepForward(Int32 _iID);

        //获取播放信息
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_GetBeginEnd(Int32 _iID, ref Int32 _piBegin, ref Int32 _piEnd);
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_GetFileName(Int32 _iID, String _pcFileName);
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_GetFrameCount(Int32 _iID);
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_GetFrameRate(Int32 _iID);
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_GetPlayingFrameNum(Int32 _iID);
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_GetPlayTime(Int32 _iID);//单位ms,得到当前播放的时间
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_GetVideoParam(Int32 _iID, IntPtr _pWidth, IntPtr _pHeight,
            IntPtr _pFrameRate);
        //重载
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_GetVideoParam(Int32 _iID, ref Int32 _pWidth, ref Int32 _pHeight,
            ref Int32 _pFrameRate);


        //抓拍
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_CaptureBmpPic(Int32 _iID, String _pcSaveFile);
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_CapturePic(Int32 _iID, [In, Out] Byte[] _ppucYUV);

        //流播放
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_PutStreamToPlayer(Int32 _iID, IntPtr _pucStreamData, Int32 _iSize);
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_CreatePlayerFromStreamEx(IntPtr _hWnd, Byte[] _pucVideoHeadBuf,
            Int32 _iHeadSize, Int32 _iStreamBufferSize);
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_CreatePlayerFromStream(IntPtr _hWnd, Byte[] _pucVideoHeadBuf,
            Int32 _iHeadSize);
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_CleanStreamBuffer(Int32 _iID);

        //电子放大
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_SetEZoom(Int32 _iID, ref RECT _pRectInVideo,
            Int32 _iCountOfRectInVideo, IntPtr _hWndShow, ref RECT _pRectShow, Int32 _iCountOfRectShow);
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_ResetEZoom(Int32 _iID);

        //CPU监控
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_StartMonitorCPU();
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_StopMonitorCPU();

        //视频文件编辑
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_VECleanup();
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_VEAbort();
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_SegmentToFile(String _pcSaveFile, String _pcSourceFile,
            Int32 _iBeginFrmNo, Int32 _iEndFrmNo);
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_RemoveAudio(String _pcSaveFile, String _pcSourceFile,
            Int32 _iBeginFrmNo, Int32 _iEndFrmNo);
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_RegisterVECallBack(IntPtr _VECallBack);
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_Locate(Int32 _iID, ref Int32 _piFrameNo);
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_GetVEState(ref Int32 _piState, ref Int32 _piProgress);
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_Combine(String _pcSaveFile, String _pcSource1,
            Int32 _iBeginFrmNo1, Int32 _iEndFrmNo1, String _pcSource2, Int32 _iBeginFrmNo2, Int32 _iEndFrmNo2);

        //版本信息
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_GetVersion(ref SDK_VERSION _pVer);

        //VoD播放控制
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_CreatePlayerFromVoD(IntPtr _hWnd, IntPtr _pucVideoHeadBuf, Int32 _iHeadSize);
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_SetVoDPlayerOver(Int32 _iID);
        [DllImport(SDK_PATH)]
        public static extern Int32 TC_SetCleanVoDBuffer(Int32 _iID);

        [DllImport(SDK_PATH)]
        public static extern Int32 TC_GetStreamPlayBufferState(int _iPlayerID, ref int _piStreamBufferState);
    }
}
