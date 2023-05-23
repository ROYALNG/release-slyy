
using System;
using System.Drawing;
using System.Runtime.InteropServices;
public static class GlobalMembersHBPlaySDK
{

    ////错误代码
    ////可以使用SUCCESS和FAILED宏判断错误代码。
    //#define FACILITY_HBPLAY2
    ////C++ TO C# CONVERTER TODO TASK: The following #define macro was ignored, as specified on the Options dialog:
    //#define HBPLAY2_ERROR(code) MAKE_HRESULT(1, FACILITY_HBPLAY2, code)

    //#define HBPLAY2_OK S_OK
    //#define HBPLAY2_ERR_GENERIC HBPLAY2_ERROR(0x1)
    //#define HBPLAY2_ERR_NOT_SUPPORTED HBPLAY2_ERROR(0x2)
    //#define HBPLAY2_ERR_INVALID_PARAMETER HBPLAY2_ERROR(0x10)
    //#define HBPLAY2_ERR_INVALID_HANDLE HBPLAY2_ERROR(0x11)
    //#define HBPLAY2_ERR_INVALID_POINTER HBPLAY2_ERROR(0x12)
    //#define HBPLAY2_ERR_INVALID_SIZE HBPLAY2_ERROR(0x13)
    //#define HBPLAY2_ERR_INVALID_PIXEL_FORMAT HBPLAY2_ERROR(0x14)
    //#define HBPLAY2_ERR_BUFFER_TOO_SMALL HBPLAY2_ERROR(0x20)
    //#define HBPLAY2_ERR_CANNOT_OPEN_FILE HBPLAY2_ERROR(0x21)
    //#define HBPLAY2_ERR_OUT_OF_MEMORY HBPLAY2_ERROR(0x22)
    //#define HBPLAY2_ERR_BUFFER_EMPTY HBPLAY2_ERROR(0X23)
    //#define HBPLAY2_ERR_BUFFER_FULL HBPLAY2_ERROR(0X24)
    //#define HBPLAY2_ERR_BUSY HBPLAY2_ERROR(0x25)
    //#define HBPLAY2_ERR_FILE_INDEX_INCOMPLETE HBPLAY2_ERROR(0x26)
    //#define HBPLAY2_ERR_NO_KEY_FRAME HBPLAY2_ERROR(0x27)
    //#define HBPLAY2_ERR_DISK_FULL HBPLAY2_ERROR(0x28)

    //打开文件
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_OpenFileA( OUT IntPtr* phPlay,  string lpszFileName );
    //C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    //ORIGINAL LINE: int WINAPI HB_PLAY2_OpenFileA(OUT IntPtr* phPlay,  string lpszFileName);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY2_OpenFileA(ref OUT IntPtr phPlay,  string lpszFileName);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_OpenFileW( OUT IntPtr* phPlay,  string lpszFileName );
    //C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    //ORIGINAL LINE: int WINAPI HB_PLAY2_OpenFileW(OUT IntPtr* phPlay,  string lpszFileName);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY2_OpenFileW(ref OUT IntPtr phPlay,  string lpszFileName);
    //#if _UNICODE
    //#define HB_PLAY2_OpenFile HB_PLAY2_OpenFileW
    //#else
    //#define HB_PLAY2_OpenFile HB_PLAY2_OpenFileA
    //#endif

    //文件头长度范围
    //#define HBPLAY2_HEADER_LENGTH_MIN
    //#define HBPLAY2_HEADER_LENGTH_MAX (256*1024)

    //打开流
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_OpenStream( OUT IntPtr* phPlay,  LPCVOID pFileHeader,  uint dwHeaderLength );
    //C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    //ORIGINAL LINE: int WINAPI HB_PLAY2_OpenStream(OUT IntPtr* phPlay,  LPCVOID pFileHeader,  uint dwHeaderLength);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY2_OpenStream(ref OUT IntPtr phPlay,  LPCVOID pFileHeader,  uint dwHeaderLength);

    //关闭媒体播放对象
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_Close(  IntPtr hPlay );
    //C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    //ORIGINAL LINE: int WINAPI HB_PLAY2_Close( IntPtr hPlay);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY2_Close( IntPtr hPlay);

    //设置/获取播放使能标志
    //这些标志位不能完全独立设置。它们之间的依赖关系，参考标志位的注释。

    //使能码流分析标志。
    //该功能始终使能，不能被禁止。
    //#define HBPLAY2_ENABLE_STREAM_PARSE
    //#define HBPLAY2_ENABLE_NONE HBPLAY2_ENABLE_STREAM_PARSE

    //使能视频解码器标志。
    //使能该标志才能进行视频解码。
    //禁止该标志时，同时也会禁止HBPLAY2_ENABLE_MULTITHREADING_VIDEO_CODEC、
    //HBPLAY2_ENABLE_HARDWARE_VIDEO_CODEC、HBPLAY2_ENABLE_VIDEO_QUALITY_PRIORITY、
    //HBPLAY2_ENABLE_VERIFY_CONTINUOUS_VIDEO和HBPLAY2_ENABLE_DISPLAY标志。
    //#define HBPLAY2_ENABLE_VIDEO_CODEC

    ////使能多线程视频解码标志。
    ////多线程解码能够充分利用多核CPU的资源，加快解码速度，适用于较少路视频同时解码。
    ////禁止该标志时，一个媒体播放对象只会使用一个线程进行视频解码，适用于较多路
    ////视频，例如大于16路，同时解码。
    //#define HBPLAY2_ENABLE_MULTITHREADING_VIDEO_CODEC

    ////使能硬件视频解码器标志。
    ////使能该标志时，可以自动探测可用的硬件解码器并优先使用硬件解码器。硬件解码能够
    //充分利用显卡的硬件资源，节省CPU资源。当前支持的硬件解码器有：NVIDIA CUDA。
    //1. NVIDIA CUDA。
    //  使用NVIDIA CUDA解码器，必须同时满足以下条件：
    //  [1] 视频编码算法是H.264 Baseline, Main, High Profile Level 4.1，
    //      图像尺寸不超过1080P，码流大小不超过45mbps。
    //  [2] 存在NVIDIA G8x, G9x, MCP79, MCP89, G98, GT2xx, GF1xx或更高版本GPU，GPU的
    //      计算能力不低于v1.1，显示内存不少于128MB。
    //    //  [3] 显卡驱动程序版本不低于v286.19，CUDA驱动程序的版本不低于v4.1。
    //    #define HBPLAY2_ENABLE_HARDWARE_VIDEO_CODEC

    //    //使能视频图像质量优先标志。
    //    //使能该标志时，在视频解码时会优先保证图像的质量，但可能会导致较高的
    //    //CPU使用率。
    //    //若禁止该标志，在视频解码时会优先保证视频的流畅性，尽量维持较低的CPU使用率，
    //    //但可能会降低图像的质量。
    //    #define HBPLAY2_ENABLE_VIDEO_QUALITY_PRIORITY

    //    //使能校验连续视频标志。
    //    //使能该标志时，能够检查视频编码数据的连续性。当发现不连续的视频帧时，暂停解码，
    //    //一直等到下一个关键帧再恢复解码。使能该标志，可以避免由于丢帧导致的视频图像的
    //    //马赛克现象，但可能导致视频短暂停顿。
    //    #define HBPLAY2_ENABLE_VERIFY_CONTINUOUS_VIDEO

    //    //使能视频显示标志。
    //    //使能该标志才能显示视频图像。
    //    //只使能该标志而没有使能具体的图像显示标志时，会自动选择默认的图像显示方式。
    //    //禁止该标志，同时也会禁止HBPLAY2_ENABLE_DRAWDIB、HBPLAY2_ENABLE_DIRECTDRAW_7和
    //    //HBPLAY2_ENABLE_DIRECT3D_9标志。
    //    #define HBPLAY2_ENABLE_DISPLAY

    //    //使能立即更新图像标志。
    //    //该标志提供了不受显示器限制的图像刷新率，但可能导致图像的“撕裂”现象。
    //    //禁止该标志时，视频图像显示帧率不能超过显示器的刷新率，会略微提高CPU使用率，
    //    //适用于显示高质量的视频图像。
    //    //该标志只对Direct3D 9的显示方式有效。
    //    #define HBPLAY2_ENABLE_PRESENT_IMMEDIATE

    //    //使能Direct3D 9显示图像。
    //    //Windows Vista/2008/7及以后版本，默认使用Direct3D 9来显示视频。
    //    //禁止该标志，同时也会禁止HBPLAY2_ENABLE_PRESENT_IMMEDIATE标志。
    //    #define HBPLAY2_ENABLE_DIRECT3D_9

    //    //使能DirectDraw 7显示图像。
    //    //DirectDraw 7的显示效率较高，对显卡的要求较低，但不能避免图像的“撕裂”现象。
    //    //Windows 2000/XP/2003，默认使用DirectDraw 7来显示视频。
    //    #define HBPLAY2_ENABLE_DIRECTDRAW_7

    //    //使能直接显示与设备无关位图。
    //    //该标志提供很好的兼容性，适合在远程桌面连接，或没有安装显卡驱动时使用。
    //    //但显示效率低，图像质量低。
    //    #define HBPLAY2_ENABLE_DRAWDIB

    //    //使能音频解码器标志。
    //    //使能该标志才能进行音频解码。
    //    //禁止该标志，同时也会禁止HBPLAY2_ENABLE_SOUND标志。
    //    #define HBPLAY2_ENABLE_AUDIO_CODEC

    //    //使能音频播放标志。
    //    //使能该标志才能进行音频播放。
    //    //禁止该标志，同时也会禁止HBPLAY2_ENABLE_SOUND_PRIORITY标志。
    //    #define HBPLAY2_ENABLE_SOUND

    //    //使能音频播放优先标志。
    //    //一般情况下，以视频的时间来控制播放的速度。使能该标志后，优先使用音频的时间
    //    //来控制播放的速度。
    //    //该标志一般用于播放纯音频流。
    //    #define HBPLAY2_ENABLE_SOUND_PRIORITY

    //    //使能HB_PLAY2_GetDecodeFrame函数标志。
    //    //使能该标志，同时会禁止HBPLAY2_ENABLE_DISPLAY和HBPLAY2_ENABLE_SOUND标志，并使能
    //    //HBPLAY2_ENABLE_VIDEO_QUALITY_PRIORITY标志。
    //    //只能通过HB_PLAY2_GetDecodeFrame函数获取解码后的音视频数据，并且不能执行任何
    //    //播放动作。
    //    //建议高级用户才使用该标志位。
    //    //更多详细信息，参考HB_PLAY2_GetDecodeFrame函数。
    //    #define HBPLAY2_ENABLE_GET_DECODE_FRAME

    //    //使能默认设置。
    //    #define HBPLAY2_ENABLE_DEFAULT (HBPLAY2_ENABLE_STREAM_PARSE | HBPLAY2_ENABLE_VIDEO_CODEC | HBPLAY2_ENABLE_MULTITHREADING_VIDEO_CODEC | HBPLAY2_ENABLE_VERIFY_CONTINUOUS_VIDEO | HBPLAY2_ENABLE_DISPLAY | HBPLAY2_ENABLE_AUDIO_CODEC | HBPLAY2_ENABLE_SOUND)

    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_SetEnableFlag(  IntPtr hPlay,  uint dwEnableFlag );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_SetEnableFlag( IntPtr hPlay,  uint dwEnableFlag);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_SetEnableFlag( IntPtr hPlay,  uint dwEnableFlag);
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_GetEnableFlag(  IntPtr hPlay, OUT uint* pdwEnableFlag );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_GetEnableFlag( IntPtr hPlay, OUT uint* pdwEnableFlag);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_GetEnableFlag( IntPtr hPlay, ref OUT uint pdwEnableFlag);

    //    //播放动作
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_Stop(  IntPtr hPlay );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_Stop( IntPtr hPlay);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_Stop( IntPtr hPlay);
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_Pause(  IntPtr hPlay );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_Pause( IntPtr hPlay);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_Pause( IntPtr hPlay);
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_Play(  IntPtr hPlay );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_Play( IntPtr hPlay);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_Play( IntPtr hPlay);
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_PlayBackward(  IntPtr hPlay );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_PlayBackward( IntPtr hPlay);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_PlayBackward( IntPtr hPlay);

    //    //播放速度
    //    #define HBPLAY2_SPEED_MIN float(1.0/64.0)
    //    #define HBPLAY2_SPEED_MAX float(64.0)
    //    #define HBPLAY2_SPEED_1X float(1.0)
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_SetSpeed(  IntPtr hPlay,  float fSpeed );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_SetSpeed( IntPtr hPlay,  float fSpeed);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_SetSpeed( IntPtr hPlay,  float fSpeed);
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_GetSpeed(  IntPtr hPlay, OUT float* pfSpeed );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_GetSpeed( IntPtr hPlay, OUT float* pfSpeed);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_GetSpeed( IntPtr hPlay, ref OUT float pfSpeed);

    //    //音量
    //    #define HBPLAY2_VOLUME_MIN
    //    #define HBPLAY2_VOLUME_MAX
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_SetVolume(  IntPtr hPlay,  uint dwVolume );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_SetVolume( IntPtr hPlay,  uint dwVolume);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_SetVolume( IntPtr hPlay,  uint dwVolume);
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_GetVolume(  IntPtr hPlay, OUT uint* pdwVolume );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_GetVolume( IntPtr hPlay, OUT uint* pdwVolume);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_GetVolume( IntPtr hPlay, ref OUT uint pdwVolume);
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_SetVideoColor(  IntPtr hPlay,  const HBPLAY2_COLOR_SPACE* pColorSpace );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_SetVideoColor( IntPtr hPlay,  const HBPLAY2_COLOR_SPACE* pColorSpace);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_SetVideoColor( IntPtr hPlay,  HBPLAY2_COLOR_SPACE pColorSpace);
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_GetVideoColor(  IntPtr hPlay, OUT PHBPLAY2_COLOR_SPACE pColorSpace );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_GetVideoColor( IntPtr hPlay, OUT PHBPLAY2_COLOR_SPACE pColorSpace);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_GetVideoColor( IntPtr hPlay, OUT PHBPLAY2_COLOR_SPACE pColorSpace);

    //    //CPU使用率
    //    #define HBPLAY2_CPU_LIMIT_MIN
    //    #define HBPLAY2_CPU_LIMIT_MAX
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_SetCpuLimit(  IntPtr hPlay,  int nLimit );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_SetCpuLimit( IntPtr hPlay,  int nLimit);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_SetCpuLimit( IntPtr hPlay,  int nLimit);
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_GetCpuLimit(  IntPtr hPlay, OUT int* pnLimit );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_GetCpuLimit( IntPtr hPlay, OUT int* pnLimit);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_GetCpuLimit( IntPtr hPlay, ref OUT int pnLimit);
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_GetMode(  IntPtr hPlay, OUT HBPLAY2_MODE* pMode );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_GetMode( IntPtr hPlay, OUT HBPLAY2_MODE* pMode);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_GetMode( IntPtr hPlay, ref OUT HBPLAY2_MODE pMode);
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_GetState(  IntPtr hPlay, OUT HBPLAY2_STATE* pState );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_GetState( IntPtr hPlay, OUT HBPLAY2_STATE* pState);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_GetState( IntPtr hPlay, ref OUT HBPLAY2_STATE pState);

    //    //播放方向
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_IsPlayBackward(  IntPtr hPlay, OUT int* pbBackward );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_IsPlayBackward( IntPtr hPlay, OUT int* pbBackward);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_IsPlayBackward( IntPtr hPlay, ref OUT int pbBackward);

    //    //文件索引完成
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_IsFileIndexCompleted(  IntPtr hPlay, OUT int* pbIndexCompleted );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_IsFileIndexCompleted( IntPtr hPlay, OUT int* pbIndexCompleted);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_IsFileIndexCompleted( IntPtr hPlay, ref OUT int pbIndexCompleted);

    //    //文件播放结束
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_IsFileEnded(  IntPtr hPlay, OUT int* pbFileEnded );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_IsFileEnded( IntPtr hPlay, OUT int* pbFileEnded);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_IsFileEnded( IntPtr hPlay, ref OUT int pbFileEnded);

    //    //视频图像尺寸
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_GetPictureSize(  IntPtr hPlay, OUT int* pnWidth, OUT int* pnHeight );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_GetPictureSize( IntPtr hPlay, OUT int* pnWidth, OUT int* pnHeight);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_GetPictureSize( IntPtr hPlay, ref OUT int pnWidth, ref OUT int pnHeight);

    //    //缓冲的音视频帧数
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_GetBufferedFrameCount(  IntPtr hPlay, OUT uint* pdwBufferedFrameCount );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_GetBufferedFrameCount( IntPtr hPlay, OUT uint* pdwBufferedFrameCount);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_GetBufferedFrameCount( IntPtr hPlay, ref OUT uint pdwBufferedFrameCount);

    //    //文件总长度
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_GetFileSize(  IntPtr hPlay, OUT long* pllFileSize );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_GetFileSize( IntPtr hPlay, OUT long* pllFileSize);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_GetFileSize( IntPtr hPlay, ref OUT long pllFileSize);

    //    //播放数据长度
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_GetPlayDataSize(  IntPtr hPlay, OUT long* pllPlayDataSize );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_GetPlayDataSize( IntPtr hPlay, OUT long* pllPlayDataSize);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_GetPlayDataSize( IntPtr hPlay, ref OUT long pllPlayDataSize);

    //    //文件总时长
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_GetFileDuration(  IntPtr hPlay, OUT uint* pdwFileDuration );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_GetFileDuration( IntPtr hPlay, OUT uint* pdwFileDuration);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_GetFileDuration( IntPtr hPlay, ref OUT uint pdwFileDuration);

    //    //播放的时长
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_GetPlayDuration(  IntPtr hPlay, OUT uint* pdwPlayDuration );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_GetPlayDuration( IntPtr hPlay, OUT uint* pdwPlayDuration);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_GetPlayDuration( IntPtr hPlay, ref OUT uint pdwPlayDuration);

    //    //文件总视频帧数
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_GetFileVideoFrameCount(  IntPtr hPlay, OUT uint* pdwFileVideoFrameCount );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_GetFileVideoFrameCount( IntPtr hPlay, OUT uint* pdwFileVideoFrameCount);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_GetFileVideoFrameCount( IntPtr hPlay, ref OUT uint pdwFileVideoFrameCount);

    //    //播放的视频帧序号
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_GetPlayVideoFrameIndex(  IntPtr hPlay, OUT uint* pdwPlayVideoFrameIndex );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_GetPlayVideoFrameIndex( IntPtr hPlay, OUT uint* pdwPlayVideoFrameIndex);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_GetPlayVideoFrameIndex( IntPtr hPlay, ref OUT uint pdwPlayVideoFrameIndex);

    //    //视频关键帧距离间隔
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_GetKeyFrameDistance(  IntPtr hPlay, OUT uint* pdwKeyFrameDistance );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_GetKeyFrameDistance( IntPtr hPlay, OUT uint* pdwKeyFrameDistance);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_GetKeyFrameDistance( IntPtr hPlay, ref OUT uint pdwKeyFrameDistance);

    //    //视频关键帧时间间隔
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_GetKeyFrameInterval(  IntPtr hPlay, OUT uint* pdwKeyFrameInterval );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_GetKeyFrameInterval( IntPtr hPlay, OUT uint* pdwKeyFrameInterval);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_GetKeyFrameInterval( IntPtr hPlay, ref OUT uint pdwKeyFrameInterval);

    //    //视频帧率
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_GetVideoFrameRate(  IntPtr hPlay, OUT float* pfVideoFrameRate );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_GetVideoFrameRate( IntPtr hPlay, OUT float* pfVideoFrameRate);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_GetVideoFrameRate( IntPtr hPlay, ref OUT float pfVideoFrameRate);

    //    //视频码流比特率
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_GetVideoBitrate(  IntPtr hPlay, OUT float* pfVideoBitrate );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_GetVideoBitrate( IntPtr hPlay, OUT float* pfVideoBitrate);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_GetVideoBitrate( IntPtr hPlay, ref OUT float pfVideoBitrate);

    //    //音频帧率
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_GetAudioFrameRate(  IntPtr hPlay, OUT float* pfAudioFrameRate );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_GetAudioFrameRate( IntPtr hPlay, OUT float* pfAudioFrameRate);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_GetAudioFrameRate( IntPtr hPlay, ref OUT float pfAudioFrameRate);

    //    //音频码流比特率
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_GetAudioBitrate(  IntPtr hPlay, OUT float* pfAudioBiterate );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_GetAudioBitrate( IntPtr hPlay, OUT float* pfAudioBiterate);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_GetAudioBitrate( IntPtr hPlay, ref OUT float pfAudioBiterate);
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_GetDemuxProperty(  IntPtr hPlay, OUT PHBPLAY2_DEMUX_PROPERTY pDemuxProperty );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_GetDemuxProperty( IntPtr hPlay, OUT PHBPLAY2_DEMUX_PROPERTY pDemuxProperty);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_GetDemuxProperty( IntPtr hPlay, OUT PHBPLAY2_DEMUX_PROPERTY pDemuxProperty);
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_GetDemuxStatus(  IntPtr hPlay, OUT PHBPLAY2_DEMUX_STATUS pDemuxStatus );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_GetDemuxStatus( IntPtr hPlay, OUT PHBPLAY2_DEMUX_STATUS pDemuxStatus);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_GetDemuxStatus( IntPtr hPlay, OUT PHBPLAY2_DEMUX_STATUS pDemuxStatus);
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_GetVdecStatus(  IntPtr hPlay, OUT PHBPLAY2_VDEC_STATUS pVdecStatus );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_GetVdecStatus( IntPtr hPlay, OUT PHBPLAY2_VDEC_STATUS pVdecStatus);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_GetVdecStatus( IntPtr hPlay, OUT PHBPLAY2_VDEC_STATUS pVdecStatus);
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_GetDisplayStatus(  IntPtr hPlay, OUT PHBPLAY2_DISPLAY_STATUS pDisplayStatus );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_GetDisplayStatus( IntPtr hPlay, OUT PHBPLAY2_DISPLAY_STATUS pDisplayStatus);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_GetDisplayStatus( IntPtr hPlay, OUT PHBPLAY2_DISPLAY_STATUS pDisplayStatus);
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_GetAdecStatus(  IntPtr hPlay, OUT PHBPLAY2_ADEC_STATUS pAdecStatus );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_GetAdecStatus( IntPtr hPlay, OUT PHBPLAY2_ADEC_STATUS pAdecStatus);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_GetAdecStatus( IntPtr hPlay, OUT PHBPLAY2_ADEC_STATUS pAdecStatus);
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_GetSoundStatus(  IntPtr hPlay, OUT PHBPLAY2_SOUND_STATUS pSoundStatus );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_GetSoundStatus( IntPtr hPlay, OUT PHBPLAY2_SOUND_STATUS pSoundStatus);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_GetSoundStatus( IntPtr hPlay, OUT PHBPLAY2_SOUND_STATUS pSoundStatus);
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_AddViewport(  IntPtr hPlay,  const HBPLAY2_VIEWPORT* pViewport );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_AddViewport( IntPtr hPlay,  const HBPLAY2_VIEWPORT* pViewport);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_AddViewport( IntPtr hPlay,  HBPLAY2_VIEWPORT pViewport);

    //    //移除视口
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_RemoveViewport(  IntPtr hPlay,  uint dwViewportID );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_RemoveViewport( IntPtr hPlay,  uint dwViewportID);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_RemoveViewport( IntPtr hPlay,  uint dwViewportID);

    //    //获取视口
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_GetViewport(  IntPtr hPlay, OUT OPTIONAL PHBPLAY2_VIEWPORT pViewportArray,  OUT uint* pViewportCount );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_GetViewport( IntPtr hPlay, OUT OPTIONAL PHBPLAY2_VIEWPORT pViewportArray,  OUT uint* pViewportCount);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_GetViewport( IntPtr hPlay, OUT OPTIONAL PHBPLAY2_VIEWPORT pViewportArray, ref  OUT uint pViewportCount);
    //    #define HBPLAY2_JPEG_QUALITY_MIN
    //    #define HBPLAY2_JPEG_QUALITY_MAX
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_SnapshotToMemory(  IntPtr hPlay,  OUT PHBPLAY2_SNAP_PICTURE pSnapPicture );
    //C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    //ORIGINAL LINE: int WINAPI HB_PLAY2_SnapshotToMemory( IntPtr hPlay,  OUT PHBPLAY2_SNAP_PICTURE pSnapPicture);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY2_SnapshotToMemory( IntPtr hPlay,  OUT PHBPLAY2_SNAP_PICTURE pSnapPicture);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_SnapshotToBmpFileA(  IntPtr hPlay,  string lpszFileName );
    //C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    //ORIGINAL LINE: int WINAPI HB_PLAY2_SnapshotToBmpFileA( IntPtr hPlay,  string lpszFileName);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY2_SnapshotToBmpFileA( IntPtr hPlay,  string lpszFileName);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_SnapshotToBmpFileW(  IntPtr hPlay,  string lpszFileName );
    //C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    //ORIGINAL LINE: int WINAPI HB_PLAY2_SnapshotToBmpFileW( IntPtr hPlay,  string lpszFileName);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY2_SnapshotToBmpFileW( IntPtr hPlay,  string lpszFileName);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_SnapshotToJpegFileA(  IntPtr hPlay,  string lpszFileName,  int nQuality );
    //C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    //ORIGINAL LINE: int WINAPI HB_PLAY2_SnapshotToJpegFileA( IntPtr hPlay,  string lpszFileName,  int nQuality);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY2_SnapshotToJpegFileA( IntPtr hPlay,  string lpszFileName,  int nQuality);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_SnapshotToJpegFileW(  IntPtr hPlay,  string lpszFileName,  int nQuality );
    //C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    //ORIGINAL LINE: int WINAPI HB_PLAY2_SnapshotToJpegFileW( IntPtr hPlay,  string lpszFileName,  int nQuality);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY2_SnapshotToJpegFileW( IntPtr hPlay,  string lpszFileName,  int nQuality);
    //#if _UNICODE
    //#define HB_PLAY2_SnapshotToBmpFile HB_PLAY2_SnapshotToBmpFileW
    //#define HB_PLAY2_SnapshotToJpegFile HB_PLAY2_SnapshotToJpegFileW
    //#else
    //#define HB_PLAY2_SnapshotToBmpFile HB_PLAY2_SnapshotToBmpFileA
    //#define HB_PLAY2_SnapshotToJpegFile HB_PLAY2_SnapshotToJpegFileA
    //#endif

    //输入流数据
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_InputData(  IntPtr hPlay,  LPCVOID pBuffer,  uint dwBufferLength );
    //C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    //ORIGINAL LINE: int WINAPI HB_PLAY2_InputData( IntPtr hPlay,  LPCVOID pBuffer,  uint dwBufferLength);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY2_InputData( IntPtr hPlay,  LPCVOID pBuffer,  uint dwBufferLength);

    //获取/释放解码数据
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_GetDecodeFrame(  IntPtr hPlay,  uint dwPixelFormat, OUT PHBPLAY2_FRAME pFrame );
    //C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    //ORIGINAL LINE: int WINAPI HB_PLAY2_GetDecodeFrame( IntPtr hPlay,  uint dwPixelFormat, OUT PHBPLAY2_FRAME pFrame);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY2_GetDecodeFrame( IntPtr hPlay,  uint dwPixelFormat, OUT PHBPLAY2_FRAME pFrame);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_FreeDecodeFrame(  IntPtr hPlay,  OUT PHBPLAY2_FRAME pFrame );
    //C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    //ORIGINAL LINE: int WINAPI HB_PLAY2_FreeDecodeFrame( IntPtr hPlay,  OUT PHBPLAY2_FRAME pFrame);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY2_FreeDecodeFrame( IntPtr hPlay,  OUT PHBPLAY2_FRAME pFrame);

    //设置文件的播放位置
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_SeekByRatio(  IntPtr hPlay,  float fRatio );
    //C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    //ORIGINAL LINE: int WINAPI HB_PLAY2_SeekByRatio( IntPtr hPlay,  float fRatio);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY2_SeekByRatio( IntPtr hPlay,  float fRatio);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_SeekByTime(  IntPtr hPlay,  uint dwTime );
    //C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    //ORIGINAL LINE: int WINAPI HB_PLAY2_SeekByTime( IntPtr hPlay,  uint dwTime);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY2_SeekByTime( IntPtr hPlay,  uint dwTime);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_SeekByIndex(  IntPtr hPlay,  uint dwIndex );
    //C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    //ORIGINAL LINE: int WINAPI HB_PLAY2_SeekByIndex( IntPtr hPlay,  uint dwIndex);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY2_SeekByIndex( IntPtr hPlay,  uint dwIndex);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_SeekNextIndex(  IntPtr hPlay );
    //C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    //ORIGINAL LINE: int WINAPI HB_PLAY2_SeekNextIndex( IntPtr hPlay);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY2_SeekNextIndex( IntPtr hPlay);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_SeekPreviousIndex(  IntPtr hPlay );
    //C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    //ORIGINAL LINE: int WINAPI HB_PLAY2_SeekPreviousIndex( IntPtr hPlay);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY2_SeekPreviousIndex( IntPtr hPlay);

    //注册文件索引完成回调函数
    //C++ TO C# CONVERTER TODO TASK: The original C++ function pointer contained an unconverted modifier:
    //ORIGINAL LINE: typedef void (CALLBACK* PHB_PLAY2_FILE_INDEX_COMPLETED_PROC)( IntPtr hPlay,  IntPtr  pContext);
    public delegate void PHB_PLAY2_FILE_INDEX_COMPLETED_PROC(IntPtr hPlay, IntPtr pContext);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_RegisterFileIndexCompletedCallback(  IntPtr hPlay,  PHB_PLAY2_FILE_INDEX_COMPLETED_PROC pfnCallback,  IntPtr  pContext );
    //C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    //ORIGINAL LINE: int WINAPI HB_PLAY2_RegisterFileIndexCompletedCallback( IntPtr hPlay,  PHB_PLAY2_FILE_INDEX_COMPLETED_PROC pfnCallback,  IntPtr  pContext);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY2_RegisterFileIndexCompletedCallback( IntPtr hPlay,  PHB_PLAY2_FILE_INDEX_COMPLETED_PROC pfnCallback,  IntPtr  pContext);

    //注册文件结束回调函数
    //C++ TO C# CONVERTER TODO TASK: The original C++ function pointer contained an unconverted modifier:
    //ORIGINAL LINE: typedef void (CALLBACK* PHB_PLAY2_FILE_ENDED_PROC)( IntPtr hPlay,  IntPtr  pContext);
    public delegate void PHB_PLAY2_FILE_ENDED_PROC(IntPtr hPlay, IntPtr pContext);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_RegisterFileEndedCallback(  IntPtr hPlay,  PHB_PLAY2_FILE_ENDED_PROC pfnCallback,  IntPtr  pContext );
    //C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    //ORIGINAL LINE: int WINAPI HB_PLAY2_RegisterFileEndedCallback( IntPtr hPlay,  PHB_PLAY2_FILE_ENDED_PROC pfnCallback,  IntPtr  pContext);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY2_RegisterFileEndedCallback( IntPtr hPlay,  PHB_PLAY2_FILE_ENDED_PROC pfnCallback,  IntPtr  pContext);

    //    //注册缓冲区接近空回调函数
    //    #define HBPLAY2_BUFFER_EMPTY_THRESHOLD_MIN
    //    #define HBPLAY2_BUFFER_EMPTY_THRESHOLD_MAX
    ////C++ TO C# CONVERTER TODO TASK: The original C++ function pointer contained an unconverted modifier:
    //ORIGINAL LINE: typedef void (CALLBACK* PHB_PLAY2_BUFFER_ALMOST_EMPTY_PROC)( IntPtr hPlay,  uint dwBufferedFrameCount,  IntPtr  pContext);
    public delegate void PHB_PLAY2_BUFFER_ALMOST_EMPTY_PROC(IntPtr hPlay, uint dwBufferedFrameCount, IntPtr pContext);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_RegisterBufferAlmostEmptyCallback(  IntPtr hPlay,  uint dwThreshold,  PHB_PLAY2_BUFFER_ALMOST_EMPTY_PROC pfnCallback,  IntPtr  pContext );
    //C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    //ORIGINAL LINE: int WINAPI HB_PLAY2_RegisterBufferAlmostEmptyCallback( IntPtr hPlay,  uint dwThreshold,  PHB_PLAY2_BUFFER_ALMOST_EMPTY_PROC pfnCallback,  IntPtr  pContext);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY2_RegisterBufferAlmostEmptyCallback( IntPtr hPlay,  uint dwThreshold,  PHB_PLAY2_BUFFER_ALMOST_EMPTY_PROC pfnCallback,  IntPtr  pContext);

    //注册码流分析回调函数
    //C++ TO C# CONVERTER TODO TASK: The original C++ function pointer contained an unconverted modifier:
    //ORIGINAL LINE: typedef void (CALLBACK* PHB_PLAY2_STREAM_PARSE_PROC)( IntPtr hPlay,  const HBPLAY2_FRAME* pFrame,  IntPtr  pContext);
    public delegate void PHB_PLAY2_STREAM_PARSE_PROC(IntPtr hPlay, HBPLAY2_FRAME pFrame, IntPtr pContext);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_RegisterStreamParseCallback(  IntPtr hPlay,  PHB_PLAY2_STREAM_PARSE_PROC pfnCallback,  IntPtr  pContext );
    //C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    //ORIGINAL LINE: int WINAPI HB_PLAY2_RegisterStreamParseCallback( IntPtr hPlay,  PHB_PLAY2_STREAM_PARSE_PROC pfnCallback,  IntPtr  pContext);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY2_RegisterStreamParseCallback( IntPtr hPlay,  PHB_PLAY2_STREAM_PARSE_PROC pfnCallback,  IntPtr  pContext);

    //注册解码回调函数
    //C++ TO C# CONVERTER TODO TASK: The original C++ function pointer contained an unconverted modifier:
    //ORIGINAL LINE: typedef void (CALLBACK* PHB_PLAY2_DECODE_PROC)( IntPtr hPlay,  const HBPLAY2_FRAME* pFrame,  IntPtr  pContext);
    public delegate void PHB_PLAY2_DECODE_PROC(IntPtr hPlay, HBPLAY2_FRAME pFrame, IntPtr pContext);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_RegisterDecodeCallback(  IntPtr hPlay,  uint dwPixelFormat,  PHB_PLAY2_DECODE_PROC pfnCallback,  IntPtr  pContext );
    //C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    //ORIGINAL LINE: int WINAPI HB_PLAY2_RegisterDecodeCallback( IntPtr hPlay,  uint dwPixelFormat,  PHB_PLAY2_DECODE_PROC pfnCallback,  IntPtr  pContext);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY2_RegisterDecodeCallback( IntPtr hPlay,  uint dwPixelFormat,  PHB_PLAY2_DECODE_PROC pfnCallback,  IntPtr  pContext);

    //注册DC绘图回调函数
    //C++ TO C# CONVERTER TODO TASK: The original C++ function pointer contained an unconverted modifier:
    //ORIGINAL LINE: typedef void (CALLBACK* PHB_PLAY2_DC_RENDER_PROC)( IntPtr hPlay,  IntPtr hDC,  const HBPLAY2_VIEWPORT* pViewport,  IntPtr  pContext);
    public delegate void PHB_PLAY2_DC_RENDER_PROC(IntPtr hPlay, IntPtr hDC, HBPLAY2_VIEWPORT pViewport, IntPtr pContext);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_RegisterDcRenderCallback(  IntPtr hPlay,  PHB_PLAY2_DC_RENDER_PROC pfnCallback,  IntPtr  pContext );
    //C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    //ORIGINAL LINE: int WINAPI HB_PLAY2_RegisterDcRenderCallback( IntPtr hPlay,  PHB_PLAY2_DC_RENDER_PROC pfnCallback,  IntPtr  pContext);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY2_RegisterDcRenderCallback( IntPtr hPlay,  PHB_PLAY2_DC_RENDER_PROC pfnCallback,  IntPtr  pContext);

    //合并录像文件
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_MergeFileA(  string lpszDestFile,  string* lpSrcFileArray,  uint dwSrcFileCount );
    //C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    //ORIGINAL LINE: int WINAPI HB_PLAY2_MergeFileA( string lpszDestFile,  string* lpSrcFileArray,  uint dwSrcFileCount);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY2_MergeFileA( string lpszDestFile, ref  string lpSrcFileArray,  uint dwSrcFileCount);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_MergeFileW(  string lpszDestFile,  string* lpSrcFileArray,  uint dwSrcFileCount );
    //C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    //ORIGINAL LINE: int WINAPI HB_PLAY2_MergeFileW( string lpszDestFile,  string* lpSrcFileArray,  uint dwSrcFileCount);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY2_MergeFileW( string lpszDestFile, ref  string lpSrcFileArray,  uint dwSrcFileCount);
    //#if _UNICODE
    //#define HB_PLAY2_MergeFile HB_PLAY2_MergeFileW
    //#else
    //#define HB_PLAY2_MergeFile HB_PLAY2_MergeFileA
    //#endif

    ////////////////////////////////////////////////////////////////////////////////
    // Callback functions definition.
    ////////////////////////////////////////////////////////////////////////////////

    // For HB_PLAY_SetParseCallBackEx.
    //C++ TO C# CONVERTER TODO TASK: The original C++ function pointer contained an unconverted modifier:
    //ORIGINAL LINE: typedef void (CALLBACK* LPSRCDATAPARSECBFUNEX)(int nChl, const _HB_FRAME* pFrame, IntPtr  pContext);
    public delegate void LPSRCDATAPARSECBFUNEX(int nChl, _HB_FRAME pFrame, IntPtr pContext);

    // For HB_PLAY_SetParseCallBack.
    //C++ TO C# CONVERTER TODO TASK: The original C++ function pointer contained an unconverted modifier:
    //ORIGINAL LINE: typedef void (CALLBACK* LPSRCDATAPARSECBFUN)(int nChl, sbyte* SrcDataBuf, int nFrameType, int nReserved1, int nReserved2, _HB_VIDEO_TIME ets);
    public delegate void LPSRCDATAPARSECBFUN(int nChl, ref string SrcDataBuf, int nFrameType, int nReserved1, int nReserved2, _HB_VIDEO_TIME ets);

    // For HB_PLAY_SetDecCallBackEx.
    //C++ TO C# CONVERTER TODO TASK: The original C++ function pointer contained an unconverted modifier:
    //ORIGINAL LINE: typedef void (CALLBACK* LPDECCBFUN)(int nChl, sbyte* pBuf, int nSize, _HB_FRAME_INFO* pFrameInfo, IntPtr  pContext, _HB_VIDEO_TIME* pVideoTime);
    public delegate void LPDECCBFUN(int nChl, ref string pBuf, int nSize, _HB_FRAME_INFO pFrameInfo, IntPtr pContext, _HB_VIDEO_TIME pVideoTime);

    // For HB_PLAY_RegisterDrawDCFun
    //C++ TO C# CONVERTER TODO TASK: The original C++ function pointer contained an unconverted modifier:
    //ORIGINAL LINE: typedef void (CALLBACK* LPDRAWDCFUN)(int nChl, IntPtr hDC, int nUserData);
    public delegate void LPDRAWDCFUN(int nChl, IntPtr hDC, int nUserData);

    // For HB_PLAY_SetSnapShotCallBack
    //C++ TO C# CONVERTER TODO TASK: The original C++ function pointer contained an unconverted modifier:
    //ORIGINAL LINE: typedef void (CALLBACK* LPSNAPSHOTCBFUN)(int nChl, uint nSize, sbyte* pBuf, int nWidth, int hHeight, int nType);
    public delegate void LPSNAPSHOTCBFUN(int nChl, uint nSize, ref string pBuf, int nWidth, int hHeight, int nType);

    // For HB_PLAY_SetIndexInfoCallBack
    //C++ TO C# CONVERTER TODO TASK: The original C++ function pointer contained an unconverted modifier:
    //ORIGINAL LINE: typedef void (CALLBACK* LPINDEXCBFUN)(int nChl, int nUserData);
    public delegate void LPINDEXCBFUN(int nChl, int nUserData);

    // For HB_PLAY_SetSourceBufferCB.
    //C++ TO C# CONVERTER TODO TASK: The original C++ function pointer contained an unconverted modifier:
    //ORIGINAL LINE: typedef void (CALLBACK* LPSOURCEBUFCALLBACK)(int nChl, uint dwBufSize, IntPtr pUser, IntPtr pResvered);
    public delegate void LPSOURCEBUFCALLBACK(int nChl, uint dwBufSize, IntPtr pUser, IntPtr pResvered);

    // For HB_PLAY_SetDisplayCallBack.
    //C++ TO C# CONVERTER TODO TASK: The original C++ function pointer contained an unconverted modifier:
    //ORIGINAL LINE: typedef void (CALLBACK* LPDISPLAYCBFUN)(int nChl, sbyte* pBuf, int nSize, int nWidth, int hHeight, int nStamp, int nType, int nReserved);
    public delegate void LPDISPLAYCBFUN(int nChl, ref string pBuf, int nSize, int nWidth, int hHeight, int nStamp, int nType, int nReserved);


    ////////////////////////////////////////////////////////////////////////////////
    // Functions definition.
    ////////////////////////////////////////////////////////////////////////////////

    ////////////////////////////////////////////////////////////////////////////////
    // Function: 
    //      HB_PLAY_OpenFile.
    // Description:
    //      Opens a Hangban video file.
    // Parameters:
    //      [in] nChl - Channel number.
    //      [in] sFile - File name.
    // Return:
    //      Nonzero if successful; otherwise 0.
    // Remarks:
    //      Must call this function before operating a channel. When the channel is
    //      no longer needed, close it by HB_PLAY_CloseFile.
    //      There are two versions of the function, ANSI and Unicode:
    //      HB_PLAY_OpenFileA, ANSI version.
    //      HB_PLAY_OpenFileW, Unicode version.
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_OpenFile(int nChl, string sFile);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_OpenFile(int nChl, string sFile);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_OpenFile(int nChl, string sFile);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_OpenFileA(int nChl, string sFile);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_OpenFileA(int nChl, string sFile);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_OpenFileA(int nChl, string sFile);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_OpenFileW(int nChl, string sFile);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_OpenFileW(int nChl, string sFile);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_OpenFileW(int nChl, string sFile);


    ////////////////////////////////////////////////////////////////////////////////
    // Function: 
    //      HB_PLAY_OpenFileEx.
    // Description:
    //      Opens a Hangban video file.
    // Parameters:
    //      [in] sFile - File name.
    // Return:
    //      If the channel is opened successfully, this function returns a valid channel
    //      number, which can also be considered as channel handle; otherwise returns 0.
    // Remarks:
    //      Must call this function before operating a channel. When the channel is
    //      no longer needed, close it by HB_PLAY_CloseFile.
    //      There are two versions of the function, ANSI and Unicode:
    //      HB_PLAY_OpenFileExA, ANSI version.
    //      HB_PLAY_OpenFileExW, Unicode version.
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_OpenFileEx(string sFile);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_OpenFileEx(string sFile);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_OpenFileEx(string sFile);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_OpenFileExA(string sFile);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_OpenFileExA(string sFile);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_OpenFileExA(string sFile);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_OpenFileExW(string sFile);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_OpenFileExW(string sFile);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_OpenFileExW(string sFile);


    ////////////////////////////////////////////////////////////////////////////////
    // Function: 
    //      HB_PLAY_CloseFile.
    // Description:
    //      Closes the channel, which was opened by HB_PLAY_OpenFile or HB_PLAY_OpenFileEx.
    // Parameters: 
    //      [in] nChl - Channel number.
    // Return:
    //      Nonzero if successful; otherwise 0.
    // Remarks:
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_CloseFile(int nChl);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_CloseFile(int nChl);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_CloseFile(int nChl);


    ////////////////////////////////////////////////////////////////////////////////
    // Function:
    //      HB_PLAY_Play.
    // Description:
    //      Start to play a file or stream.
    // Parameters: 
    //      [in] nChl - Channel number.
    //      [in] hWnd - Handle to the window for displaying video.
    // Return:
    //      Nonzero if successful; otherwise 0.
    // Remarks:
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_Play(int nChl, IntPtr hWnd);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_Play(int nChl, IntPtr hWnd);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_Play(int nChl, IntPtr hWnd);


    ////////////////////////////////////////////////////////////////////////////////
    // Function: 
    //      HB_PLAY_Pause.
    // Description:
    //      Pauses the play of the file.
    // Parameters: 
    //      [in] nChl - Channel number.
    // Return:
    //      Nonzero if successful; otherwise 0.
    // Remarks:
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_Pause(int nChl);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_Pause(int nChl);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_Pause(int nChl);


    ////////////////////////////////////////////////////////////////////////////////
    // Function: 
    //      HB_PLAY_Stop.
    // Description:
    //      Stops the play of the file.
    // Parameters: 
    //      [in] nChl - Channel number.
    // Return:
    //      Nonzero if successful; otherwise 0.
    // Remarks:
    //      Valid when the channel is opened by HB_PLAY_OpenFile or HB_PLAY_OpenFileEx.
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_Stop(int nChl);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_Stop(int nChl);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_Stop(int nChl);


    ////////////////////////////////////////////////////////////////////////////////
    // Function: 
    //      HB_PLAY_Fast.
    // Description:
    //      Speeds up the play rate.
    // Parameters: 
    //      [in] nChl - Channel number.
    // Return:
    //      Nonzero if successful; otherwise 0.
    // Remarks:
    //      For each call, the play rate speeds up to the twice of the original rate.
    //      Maintains the maximum rate if it has already speeded up to the 16 times
    //      of the normal rate.
    //      Call HB_PLAY_Play to resume the normal rate from current frame.
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_Fast(int nChl);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_Fast(int nChl);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_Fast(int nChl);


    ////////////////////////////////////////////////////////////////////////////////
    // Function: 
    //      HB_PLAY_Slow.
    // Description:
    //      Slows down the play rate.
    // Parameters: 
    //      [in] nChl - Channel number.
    // Return:
    //      Nonzero if successful; otherwise 0.
    // Remarks:
    //      For each call, the play rate slows down to the half of the original rate.
    //      Maintains the minimum rate if it has already slowed down to 1/16 of the normal rate.
    //      Call HB_PLAY_Play to resume the normal rate from current frame.
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_Slow(int nChl);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_Slow(int nChl);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_Slow(int nChl);


    ////////////////////////////////////////////////////////////////////////////////
    // Function: 
    //      HB_PLAY_OneByOne.
    // Description:
    //      Plays one single frame forward.
    // Parameters: 
    //      [in] nChl - Channel number.
    // Return:
    //      Nonzero if successful; otherwise 0.
    // Remarks:
    //      For each call, plays one frame forward.
    //      HB_PLAY_PlayBySingleFrame is the same as HB_PLAY_OneByOne.
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_OneByOne(int nChl);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_OneByOne(int nChl);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_OneByOne(int nChl);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_PlayBySingleFrame(int nChl);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_PlayBySingleFrame(int nChl);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_PlayBySingleFrame(int nChl);


    ////////////////////////////////////////////////////////////////////////////////
    // Function: 
    //      HB_PLAY_OneByOneBack.
    // Description:
    //      Plays one single frame backwards.
    // Parameters: 
    //      [in] nChl - Channel number.
    // Return:
    //      Nonzero if successful; otherwise 0.
    // Remarks:
    //      For each call, plays one frame backwards.
    //      Valid when the index has been created.
    //      HB_PLAY_PlayBySingleFrameBack is the same as HB_PLAY_OneByOneBack.
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_OneByOneBack(int nChl);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_OneByOneBack(int nChl);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_OneByOneBack(int nChl);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_PlayBySingleFrameBack(int nChl);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_PlayBySingleFrameBack(int nChl);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_PlayBySingleFrameBack(int nChl);


    ////////////////////////////////////////////////////////////////////////////////
    // Function: 
    //      HB_PLAY_PlayBack.
    // Description:
    //      Plays backwards continuously from current frame.
    // Parameters: 
    //      [in] nChl - Channel number.
    // Return:
    //      Nonzero if successful; otherwise 0.
    // Remarks:
    //      Valid when the index has been created.
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_PlayBack(int nChl);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_PlayBack(int nChl);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_PlayBack(int nChl);


    ////////////////////////////////////////////////////////////////////////////////
    // Function: 
    //      HB_PLAY_GetFileTime.
    // Description:
    //      Gets the total time for playing the opened file.
    // Parameters: 
    //      [in] nChl - Channel number.
    // Return:
    //      The total time for playing the opened file in seconds.
    // Remarks:
    //      Valid when the index has been created.
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)uint __stdcall HB_PLAY_GetFileTime(int nChl);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: uint __stdcall HB_PLAY_GetFileTime(int nChl);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //uint HB_PLAY_GetFileTime(int nChl);


    ////////////////////////////////////////////////////////////////////////////////
    // Function: 
    //      HB_PLAY_GetFileTimeMilliSec.
    // Description:
    //      Gets the total time for playing the opened file.
    // Parameters: 
    //      [in] nChl - Channel number.
    // Return:
    //      The total time for playing the opened file in milliseconds.
    // Remarks:
    //      Valid when the index has been created.
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)uint __stdcall HB_PLAY_GetFileTimeMilliSec(int nChl);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: uint __stdcall HB_PLAY_GetFileTimeMilliSec(int nChl);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //uint HB_PLAY_GetFileTimeMilliSec(int nChl);


    ////////////////////////////////////////////////////////////////////////////////
    // Function: 
    //      HB_PLAY_GetFileFrames.
    // Description:
    //      Gets the total frames of the opened file.
    // Parameters: 
    //      [in] nChl - Channel number.
    // Return:
    //      The total frames of the opened file.
    // Remarks:
    //      Valid when the index has been created.
    //      HB_PLAY_GetFileTotalFrames is the same as HB_PLAY_GetFileFrames.
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)uint __stdcall HB_PLAY_GetFileFrames(int nChl);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: uint __stdcall HB_PLAY_GetFileFrames(int nChl);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //uint HB_PLAY_GetFileFrames(int nChl);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)uint __stdcall HB_PLAY_GetFileTotalFrames(int nChl);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: uint __stdcall HB_PLAY_GetFileTotalFrames(int nChl);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //uint HB_PLAY_GetFileTotalFrames(int nChl);


    ////////////////////////////////////////////////////////////////////////////////
    // Function: 
    //      HB_PLAY_GetPlayedFrames.
    // Description:
    //      Gets the frames already decoded.
    // Parameters: 
    //      [in] nChl - Channel number.
    // Return:
    //      The frames already decoded.
    // Remarks:
    //      HB_PLAY_GetPlayedFrameNum is the same as HB_PLAY_GetPlayedFrames.
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_GetPlayedFrames(int nChl);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_GetPlayedFrames(int nChl);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_GetPlayedFrames(int nChl);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_GetPlayedFrameNum(int nChl);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_GetPlayedFrameNum(int nChl);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_GetPlayedFrameNum(int nChl);


    ////////////////////////////////////////////////////////////////////////////////
    // Function: 
    //      HB_PLAY_GetCurrentFrameRate.
    // Description:
    //      Gets the current frame rate.
    // Parameters: 
    //      [in] nChl - Channel number.
    // Return:
    //      The current frame rate.
    // Remarks:
    //      HB_PLAY_GetCurrFrameRate is the same as HB_PLAY_GetCurrentFrameRate.
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)uint __stdcall HB_PLAY_GetCurrentFrameRate(int nChl);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: uint __stdcall HB_PLAY_GetCurrentFrameRate(int nChl);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //uint HB_PLAY_GetCurrentFrameRate(int nChl);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)uint __stdcall HB_PLAY_GetCurrFrameRate(int nChl);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: uint __stdcall HB_PLAY_GetCurrFrameRate(int nChl);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //uint HB_PLAY_GetCurrFrameRate(int nChl);


    ////////////////////////////////////////////////////////////////////////////////
    // Function: 
    //      HB_PLAY_SetPlaySpeed.
    // Description:
    //      Sets play rate for speeding up or slowing down.
    // Parameters: 
    //      [in] nChl - Channel number.
    //      [in] dwSpeed - Effective values: 2, 4, 8 and 16.
    //      [in] bSlow - If TRUE, slow down; otherwise speed up.
    // Return:
    //      Nonzero if successful; otherwise 0.
    // Remarks:
    //      Call HB_PLAY_Play to play normally.
    //      If bSlow is FALSE, the play rate equals the value of dwSpeed.
    //      If bSlow is TRUE, the play rate equals the reciprocal of the value of dwSpeed.
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_SetPlaySpeed(int nChl, int dwSpeed, int bSlow);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_SetPlaySpeed(int nChl, int dwSpeed, int bSlow);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_SetPlaySpeed(int nChl, int dwSpeed, int bSlow);


    ////////////////////////////////////////////////////////////////////////////////
    // Function: 
    //      HB_PLAY_GetCurrentFrameNum.
    // Description:
    //      Gets current frame number.
    // Parameters: 
    //      [in] nChl - Channel number.
    // Return:
    //      Current frame number.
    // Remarks:
    //      HB_PLAY_GetCurrFrameNum is the same as HB_PLAY_GetCurrentFrameNum.
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)uint __stdcall HB_PLAY_GetCurrentFrameNum(int nChl);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: uint __stdcall HB_PLAY_GetCurrentFrameNum(int nChl);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //uint HB_PLAY_GetCurrentFrameNum(int nChl);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_GetCurrFrameNum(int nChl);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_GetCurrFrameNum(int nChl);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_GetCurrFrameNum(int nChl);


    ////////////////////////////////////////////////////////////////////////////////
    // Function: 
    //      HB_PLAY_SetCurrentFrameNum.
    // Description:
    //      Sets current frame number.
    // Parameters: 
    //      [in] nChl - Channel number.
    //      [in] dwFrameNum - Current frame number.
    // Return:
    //      Nonzero if successful; otherwise 0.
    // Remarks:
    //      Valid when the index has been created.
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_SetCurrentFrameNum(int nChl, uint dwFrameNum);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_SetCurrentFrameNum(int nChl, uint dwFrameNum);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_SetCurrentFrameNum(int nChl, uint dwFrameNum);


    ////////////////////////////////////////////////////////////////////////////////
    // Function: 
    //      HB_PLAY_GetPlayedTimeEx.
    // Description:
    //      Gets current playing time.
    // Parameters: 
    //      [in] nChl - Channel number.
    // Return:
    //      Nonzero if successful; otherwise 0.
    // Remarks:
    //      HB_PLAY_GetPlayTime is the same as HB_PLAY_GetPlayedTimeEx.
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)uint __stdcall HB_PLAY_GetPlayedTimeEx(int nChl);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: uint __stdcall HB_PLAY_GetPlayedTimeEx(int nChl);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //uint HB_PLAY_GetPlayedTimeEx(int nChl);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)uint __stdcall HB_PLAY_GetPlayTime(int nChl);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: uint __stdcall HB_PLAY_GetPlayTime(int nChl);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //uint HB_PLAY_GetPlayTime(int nChl);


    ////////////////////////////////////////////////////////////////////////////////
    // Function: 
    //      HB_PLAY_SetPlayedTimeEx.
    // Description:
    //      Sets current playing time.
    // Parameters: 
    //      [in] nChl - Channel number.
    //      [in] dwTime - Current playing time in milliseconds.
    // Return:
    //      Nonzero if successful; otherwise 0.
    // Remarks:
    //      Both HB_PLAY_GetCurrentFrameNum and HB_PLAY_SetPlayTime are the same
    //      as HB_PLAY_SetPlayedTimeEx.
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_SetPlayedTimeEx(int nChl, uint dwTime);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_SetPlayedTimeEx(int nChl, uint dwTime);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_SetPlayedTimeEx(int nChl, uint dwTime);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_SetPlayedTime(int nChl, uint dwTime);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_SetPlayedTime(int nChl, uint dwTime);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_SetPlayedTime(int nChl, uint dwTime);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_SetPlayTime(int nChl, uint dwTime);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_SetPlayTime(int nChl, uint dwTime);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_SetPlayTime(int nChl, uint dwTime);


    ////////////////////////////////////////////////////////////////////////////////
    // Function: 
    //      HB_PLAY_GetPlayPos.
    // Description:
    //      Gets play position.
    // Parameters: 
    //      [in] nChl - Channel number.
    // Return:
    //      Play position in percentage.
    // Remarks:
    //      Valid when the index has been created.
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)float __stdcall HB_PLAY_GetPlayPos(int nChl);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: float __stdcall HB_PLAY_GetPlayPos(int nChl);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //float HB_PLAY_GetPlayPos(int nChl);


    ////////////////////////////////////////////////////////////////////////////////
    // Function: 
    //      HB_PLAY_SetPlayPos.
    // Description:
    //      Sets play position.
    // Parameters: 
    //      [in] nChl - Channel number.
    //      [in] dPos - Play position in percentage.
    // Return:
    //      Nonzero if successful; otherwise 0.
    // Remarks:
    //      Valid when the index has been created.
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_SetPlayPos(int nChl, float dPos);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_SetPlayPos(int nChl, float dPos);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_SetPlayPos(int nChl, float dPos);


    ////////////////////////////////////////////////////////////////////////////////
    // Function: 
    //      HB_PLAY_SetFileEndMsg.
    // Description:
    //      Sets the window specified by hWnd to receive the message specified by nMsg
    //      when the file is played to the end.
    // Parameters: 
    //      [in] nChl - Channel number.
    //      [in] hWnd - The handle of the window.
    //      [in] nMsg - The message ID.
    // Return:
    //      Nonzero if successful; otherwise 0.
    // Remarks:
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_SetFileEndMsg(int nChl, IntPtr hWnd, uint nMsg);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_SetFileEndMsg(int nChl, IntPtr hWnd, uint nMsg);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_SetFileEndMsg(int nChl, IntPtr hWnd, uint nMsg);


    ////////////////////////////////////////////////////////////////////////////////
    // Function: 
    //      HB_PLAY_OpenSound.
    // Description:
    //      Opens sound.
    // Parameters: 
    //      [in] nChl - Channel number.
    // Return:
    //      Nonzero if successful; otherwise 0.
    // Remarks:
    //      HB_PLAY_PlaySound is the same as HB_PLAY_OpenSound.
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_OpenSound(int nChl);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_OpenSound(int nChl);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_OpenSound(int nChl);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_PlaySound(int nChl);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_PlaySound(int nChl);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_PlaySound(int nChl);


    ////////////////////////////////////////////////////////////////////////////////
    // Function: 
    //      HB_PLAY_CloseSound.
    // Description:
    //      Closes sound.
    // Parameters: 
    //      [in] nChl - Channel number.
    // Return:
    //      Nonzero if successful; otherwise 0.
    // Remarks:
    //      If the sound has been opened by HB_PLAY_OpenSound or HB_PLAY_PlaySound,
    //      call this function to stop playing audio. 
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_CloseSound(int nChl);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_CloseSound(int nChl);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_CloseSound(int nChl);


    ////////////////////////////////////////////////////////////////////////////////
    // Function: 
    //      HB_PLAY_GetVolume.
    // Description:
    //      Gets the volume of the sound.
    // Parameters: 
    //      [in] nChl - Channel number.
    // Return:
    //      The volume of the sound.
    // Remarks:
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)ushort __stdcall HB_PLAY_GetVolume(int nChl);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: ushort __stdcall HB_PLAY_GetVolume(int nChl);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //ushort HB_PLAY_GetVolume(int nChl);


    ////////////////////////////////////////////////////////////////////////////////
    // Function: 
    //      HB_PLAY_SetVolume.
    // Description:
    //      Sets the volume of the sound.
    // Parameters: 
    //      [in] nChl - Channel number.
    //      [in] nVolume - The volume of the sound, ranging from 0 to 65535.
    // Return:
    //      Nonzero if successful; otherwise 0.
    // Remarks:
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_SetVolume(int nChl, ushort nVolume);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_SetVolume(int nChl, ushort nVolume);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_SetVolume(int nChl, ushort nVolume);


    ////////////////////////////////////////////////////////////////////////////////
    // Function: 
    //      HB_PLAY_GetPictureSize.
    // Description:
    //      Gets the original size of the picture.
    // Parameters: 
    //      [in] nChl - Channel number.
    //      [out] pWidth - The pointer to the original picture width .
    //      [out] pHeight - The pointer to the original picture height.
    // Return:
    //      Nonzero if successful; otherwise 0.
    // Remarks:
    //      Couldn't get correct value until a frame is decoded successfully.
    //      If the picture size changes, call this function to get the new value.
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_GetPictureSize(int nChl, int* pWidth, int* pHeight);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_GetPictureSize(int nChl, int* pWidth, int* pHeight);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_GetPictureSize(int nChl, ref int pWidth, ref int pHeight);


    ////////////////////////////////////////////////////////////////////////////////
    // Function: 
    //      HB_PLAY_RegisterDrawDCFun.
    // Description: 
    //      Sets callback function.
    // Parameters: 
    //      [in] nChl - Channel number.
    //      [in] DrawDCFun - Callback function pointer.
    //           void CALLBACK DrawDCFun(long nChl, HDC hDC, long nUserData);
    //              - [in] nChl - Channel number.
    //              - [in] hDC - The handle of DC.
    //              - [in] nUserData - User data.
    //      [in] nUserData - User data.
    // Return:
    //      Nonzero if successful; otherwise 0.
    // Remarks:
    //      This function would be called when the index has been created.
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_RegisterDrawDCFun(int nChl, LPDRAWDCFUN DrawDCFun, int nUserData);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_RegisterDrawDCFun(int nChl, LPDRAWDCFUN DrawDCFun, int nUserData);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_RegisterDrawDCFun(int nChl, LPDRAWDCFUN DrawDCFun, int nUserData);


    ////////////////////////////////////////////////////////////////////////////////
    // Function: 
    //      HB_PLAY_SetDecCallBackEx.
    // Description:
    //      Sets callback function after decoding.
    // Parameters: 
    //      [in] nChl - Channel number.
    //      [in] DecCBFun - The callback function pointer.
    //           void CALLBACK DecCBFun(long nChl, char* pBuf, long nSize, 
    //              FRAME_INFO* pFrameInfo, long nReserved1, long nReserved2);
    //              - [in] nChl - Channel number.
    //              - [in] pBuf - The pointer to the output YUV data.
    //              - [in] nSize - The size of the output YUV data.
    //              - [in] pFrameInfo - The pointer to the frame information.
    //              - [in] pContext - Pointer to a user defined variable.
    //              - [in] nReserved - Reserved.
    //      [in] pContext - Pointer to a user defined variable.
    //      [in] nReserved - Reserved.
    //      [in] nOutFormat - The formats of output YUV data. For format details, refer
    //           to the decoding formats for the output in Macro definition.
    // Return:
    //      Nonzero if successful; otherwise 0.
    // Remarks:
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_SetDecCallBackEx(int nChl, LPDECCBFUN DecCBFun, IntPtr  pContext, int nReserved, int nOutFormat);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_SetDecCallBackEx(int nChl, LPDECCBFUN DecCBFun, IntPtr  pContext, int nReserved, int nOutFormat);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_SetDecCallBackEx(int nChl, LPDECCBFUN DecCBFun, IntPtr  pContext, int nReserved, int nOutFormat);

    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_SetDecCallBack(int nChl, LPDECCBFUN DecCBFun,int nOutFormat);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_SetDecCallBack(int nChl, LPDECCBFUN DecCBFun,int nOutFormat);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_SetDecCallBack(int nChl, LPDECCBFUN DecCBFun, int nOutFormat);


    ////////////////////////////////////////////////////////////////////////////////
    // Function: 
    //      HB_PLAY_SetParseCallBackEx.
    // Description:
    //      Sets callback function.
    // Parameters: 
    //      [in] nChl - Channel number.
    //      [in] pfnParse - Callback function pointer.
    //           void CALLBACK SrcDataParseCBFun(long nChl, const HB_FRAME* pFrame, LPVOID pContext);
    //              - [in] nChl - Channel number.
    //              - [in] pFrame - The pointer to the original data of a frame.
    //              - [in] pContext - Reserved.
    //      [in] pContext - Reserved.
    // Return:
    //      Nonzero if successful; otherwise 0.
    // Remarks:
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_SetParseCallBackEx(int nChl, LPSRCDATAPARSECBFUNEX pfnParse, IntPtr  pContext );
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_SetParseCallBackEx(int nChl, LPSRCDATAPARSECBFUNEX pfnParse, IntPtr  pContext);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_SetParseCallBackEx(int nChl, LPSRCDATAPARSECBFUNEX pfnParse, IntPtr  pContext);


    ////////////////////////////////////////////////////////////////////////////////
    // Function: 
    //      HB_PLAY_SetParseOnly.
    // Description:
    //      Sets whether to decode or not.
    // Parameters: 
    //      [in] nChl - Channel number.
    //      [in] bParseOnly - Decode or not.
    // Return:
    //      Nonzero if successful; otherwise 0.
    // Remarks:
    //      If bParseOnly is TRUE, the SDK does not execute decoding.
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_SetParseOnly(int nChl, int bParseOnly);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_SetParseOnly(int nChl, int bParseOnly);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_SetParseOnly(int nChl, int bParseOnly);


    ////////////////////////////////////////////////////////////////////////////////
    // Function: 
    //      HB_PLAY_SetPlayOption.
    // Description:
    //      Sets play options, including displaying or not, and playing audio or not.
    // Parameters: 
    //      [in] nChl - Channel number.
    //      [in] bEnableDisplay - Display or not.
    //      [in] bEnableSound - Play audio or not.
    // Return:
    //      Nonzero if successful; otherwise 0.
    // Remarks:
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_SetPlayOption(int nChl, int bEnableDisplay, int bEnableSound);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_SetPlayOption(int nChl, int bEnableDisplay, int bEnableSound);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_SetPlayOption(int nChl, int bEnableDisplay, int bEnableSound);


    ////////////////////////////////////////////////////////////////////////////////
    // Function: 
    //      HB_PLAY_SetDisplayRegion.
    // Description:
    //      Sets display region.
    // Parameters: 
    //      [in] nChl - Channel number.
    //      [in] nRegionNum - Display region number from 1 to (MAX_DISPLAY_WND-1).
    //      [in] pSrcRect - The pointer to the original picture region.
    //      [in] hDestWnd - Handle of the destination window.
    //      [in] bEnable - Enable or disable the display region.
    // Return:
    //      Nonzero if successful; otherwise 0.
    // Remarks:
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_SetDisplayRegion(int nChl, uint nRegionNum, RECT* pSrcRect, IntPtr hDestWnd, int bEnable);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_SetDisplayRegion(int nChl, uint nRegionNum, RECT* pSrcRect, IntPtr hDestWnd, int bEnable);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_SetDisplayRegion(int nChl, uint nRegionNum, ref RECT pSrcRect, IntPtr hDestWnd, int bEnable);


    ////////////////////////////////////////////////////////////////////////////////
    // Function: 
    //      HB_PLAY_OpenStream.
    // Description:
    //      Opens stream.
    // Parameters: 
    //      [in] nChl - Channel number.
    //      [in] pFileHeadBuf - The pointer to the header buffer of the stream data.
    //      [in] dwSize - The size of the header buffer of the stream data.
    //      [in] dwReserved - Reserved.
    // Return:
    //      Nonzero if successful; otherwise 0.
    // Remarks:
    //      When the channel is no longer needed, close it by HB_PLAY_CloseStream.
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_OpenStream(int nChl, sbyte* pFileHeadBuf, uint dwSize, uint dwReserved);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_OpenStream(int nChl, sbyte* pFileHeadBuf, uint dwSize, uint dwReserved);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_OpenStream(int nChl, ref string pFileHeadBuf, uint dwSize, uint dwReserved);


    ////////////////////////////////////////////////////////////////////////////////
    // Function: 
    //      HB_PLAY_OpenStreamEx.
    // Description:
    //      Searches for an unoccupied channel to open stream.
    // Parameters: 
    //      [in] pFileHeadBuf - The pointer to the header buffer of the stream data.
    //      [in] dwSize - The size of the header buffer of the stream data.
    //      [in] dwReserved - Reserved.
    // Return:
    //      If the channel is opened successfully, this function returns a valid channel
    //      number, which can also be considered as channel handle; otherwise returns 0.
    // Remarks:
    //      When the channel is no longer needed, close it by HB_PLAY_CloseStream.
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_OpenStreamEx(sbyte* pFileHeadBuf, uint dwSize, uint dwReserved);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_OpenStreamEx(sbyte* pFileHeadBuf, uint dwSize, uint dwReserved);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_OpenStreamEx(ref string pFileHeadBuf, uint dwSize, uint dwReserved);


    ////////////////////////////////////////////////////////////////////////////////
    // Function: 
    //      HB_PLAY_InputData.
    // Description:
    //      Inputs stream data.
    // Parameters: 
    //      [in] nChl - Channel number.
    //      [in] pBuf - Stream data.
    //      [in] dwnSize - Stream data size.
    //      [in] dwReserved - Reserved, must be zero.
    // Return:
    //      Nonzero if successful; otherwise 0.
    // Remarks:
    //      Valid when HB_PLAY_OpenStream or HB_PLAY_OpenStreamEx is called.
    //      This function would return FALSE if the stream data is input too fast.
    //      In this case, the stream data should be input once more.
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_InputData(int nChl, sbyte* pBuf, uint dwSize, uint dwReserved);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_InputData(int nChl, sbyte* pBuf, uint dwSize, uint dwReserved);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_InputData(int nChl, ref string pBuf, uint dwSize, uint dwReserved);


    ////////////////////////////////////////////////////////////////////////////////
    // Function: 
    //      HB_PLAY_CloseStream.
    // Description:
    //      Closes the stream, which is opened by HB_PLAY_OpenStream or HB_PLAY_OpenStreamEx..
    // Parameters: 
    //      [in] nChl - Channel number.
    // Return:
    //      Nonzero if successful; otherwise 0.
    // Remarks:
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_CloseStream(int nChl);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_CloseStream(int nChl);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_CloseStream(int nChl);


    ////////////////////////////////////////////////////////////////////////////////
    // Function: 
    //      HB_PLAY_SetMsgWnd.
    // Description:
    //      Sets the window to receive the message when the index has been created.
    // Parameters: 
    //      [in] nChl - Channel number.
    //      [in] hWnd - Handle to the window to receive the MSG_INDEX_OK message.
    // Return:
    //      Nonzero if successful; otherwise 0.
    // Remarks:
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_SetMsgWnd(int nChl, IntPtr hWnd);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_SetMsgWnd(int nChl, IntPtr hWnd);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_SetMsgWnd(int nChl, IntPtr hWnd);


    ////////////////////////////////////////////////////////////////////////////////
    // Function: 
    //      HB_PLAY_SnapShot.
    // Description:
    //      Catches a picture, and saves it to an assigned file.
    // Parameters:
    //      [in] nChl - Channel number.
    //      [in] sFile - File name.
    // Return:
    //      Nonzero if successful; otherwise 0.
    // Remarks:
    //      There are two versions of the function, ANSI and Unicode:
    //      HB_PLAY_SnapshotA, ANSI version.
    //      HB_PLAY_SnapshotW, Unicode version.
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_SnapShot(int nChl, string sFile);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_SnapShot(int nChl, string sFile);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_SnapShot(int nChl, string sFile);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_SnapshotA(int nChl, string sFile);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_SnapshotA(int nChl, string sFile);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_SnapshotA(int nChl, string sFile);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_SnapshotW(int nChl, string sFile);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_SnapshotW(int nChl, string sFile);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_SnapshotW(int nChl, string sFile);

    //#if _UNICODE
    //#define HB_PLAY_Snapshot HB_PLAY_SnapshotW
    //#else
    //#define HB_PLAY_Snapshot HB_PLAY_SnapshotA
    //#endif


    ////////////////////////////////////////////////////////////////////////////////
    // Function: 
    //      HB_PLAY_GetDynamicBPS.
    // Description:
    //      Gets the real-time bit rate of the stream.
    // Parameters: 
    //      [in] nChl - Channel number.
    // Return:
    //      The real-time bit rate of the stream in kbps(kilobits per second).
    // Remarks:
    //      Returns nonzero values when played for a while.
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)double __stdcall HB_PLAY_GetDynamicBPS(int nChl);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: double __stdcall HB_PLAY_GetDynamicBPS(int nChl);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //double HB_PLAY_GetDynamicBPS(int nChl);


    ////////////////////////////////////////////////////////////////////////////////
    // Function: 
    //      HB_PLAY_GetAverageBPS.
    // Description:
    //      Gets the average bit rate of the stream.
    // Parameters: 
    //      [in] nChl - Channel number.
    // Return:
    //      The average bit rate of the stream in kbps.
    // Remarks:
    //      Returns nonzero values when played for a while.
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)double __stdcall HB_PLAY_GetAverageBPS(int nChl);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: double __stdcall HB_PLAY_GetAverageBPS(int nChl);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //double HB_PLAY_GetAverageBPS(int nChl);


    ////////////////////////////////////////////////////////////////////////////////
    // Function: 
    //      HB_PLAY_SetVideoColor.
    // Description: 
    //      Sets video color.
    // Parameters: 
    //      [in] nChl - Channel number.
    //      [in] nBrightness - Brightness, ranging from 0 to 127, 64 by default.
    //      [in] nContrast - Contrast, ranging from 0 to 127, 64 by default.
    //      [in] nSaturation - Saturation, ranging from 0 to 127, 64 by default4.
    //      [in] nHue - Hue, ranging from 0 to 127, 64 by default.
    //      [in] nReserved - Reserved.
    // Return:
    //      Nonzero if successful; otherwise 0.
    // Remarks:
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_SetVideoColor(int nChl, int nBrightness, int nContrast, int nSaturation , int nHue, int nReserved);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_SetVideoColor(int nChl, int nBrightness, int nContrast, int nSaturation, int nHue, int nReserved);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_SetVideoColor(int nChl, int nBrightness, int nContrast, int nSaturation, int nHue, int nReserved);


    ////////////////////////////////////////////////////////////////////////////////
    // Function: 
    //      HB_PLAY_GetVideoColor.
    // Description: 
    //      Gets video color.
    // Parameters: 
    //      [in] nChl - Channel number.
    //      [out] pBrightness - Pointer to brightness.
    //      [out] pContrast - Pointer to contrast.
    //      [out] pSaturation - Pointer to saturation.
    //      [out] pHue - Pointer to hue.
    //      [in] nReserved - Reserved.
    // Return:
    //      Nonzero if successful; otherwise 0.
    // Remarks:
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_GetVideoColor(int nChl, int* pBrightness, int *pContrast, int *pSaturation , int *pHue, int nReserved);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_GetVideoColor(int nChl, int* pBrightness, int *pContrast, int *pSaturation, int *pHue, int nReserved);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_GetVideoColor(int nChl, ref int pBrightness, ref int pContrast, ref int pSaturation, ref int pHue, int nReserved);


    ////////////////////////////////////////////////////////////////////////////////
    // Function: 
    //      HB_PLAY_SetIndexInfoCallBack.
    // Description: 
    //      Sets callback function.
    // Parameters: 
    //      [in] nChl - Channel number.
    //      [in] IndexCBFunc - Callback function pointer.
    //           void CALLBACK IndexCBFunc(long nChl, long nReserved);
    //              - [in] nChl - Channel number.
    //              - [in] nUserData - User data.
    //      [in] nUserData - User data.
    // Return:
    //      Nonzero if successful; otherwise 0.
    // Remarks:
    //	    This function would be called when the index has been creation.
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_SetIndexInfoCallBack(int nChl, LPINDEXCBFUN IndexCBFun, int nUserData);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_SetIndexInfoCallBack(int nChl, LPINDEXCBFUN IndexCBFun, int nUserData);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_SetIndexInfoCallBack(int nChl, LPINDEXCBFUN IndexCBFun, int nUserData);


    ////////////////////////////////////////////////////////////////////////////////
    // Function:
    //      HB_PLAY_GetFileHead.
    // Description:
    //      Retrieves file header size and file header information.
    // Parameters: 
    //      [in] nChl - Channel number.
    //      [out] pBuffer - The pointer to the file header information.
    //      [in/out] pSize - The pointer to the size of the file header.
    // Return:
    //      Nonzero if successful; otherwise 0.
    // Remarks:
    //      Call this function for twice. Retrives the file header size for the 
    //      first call, and the file header information for the second call.
    //      The second call would be valid only when the return value of the 
    //      first call is nonzero.
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_GetFileHead(int nChl, byte* pBuffer, uint* pSize);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_GetFileHead(int nChl, byte* pBuffer, uint* pSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_GetFileHead(int nChl, ref byte pBuffer, ref uint pSize);


    ////////////////////////////////////////////////////////////////////////////////
    // Function: 
    //      HB_PLAY_GetCPULimitEx.
    // Description: 
    //      Retrieves CPU limitation.
    // Parameters: 
    //      [in] nChl - Channel number.
    //      [out] pCpuUse - Pointer to CPU usage limitation.
    // Return:
    //      Nonzero if successful; otherwise 0.
    // Remarks:
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_GetCPULimitEx(int nChl, int* pCpuUse);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_GetCPULimitEx(int nChl, int* pCpuUse);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_GetCPULimitEx(int nChl, ref int pCpuUse);


    ////////////////////////////////////////////////////////////////////////////////
    // Function:
    //      HB_PLAY_SetCPULimitEx.
    // Description:
    //      Limits CPU usage. If the CPU usage is larger than the limitation,
    //      decoding will be delayed.
    // Parameters: 
    //      [in] nChl - Channel number.
    //      [in] nCpuUse - CPU usage limitation, from 50 to 100.
    // Return:
    //      Nonzero if successful; otherwise 0.
    // Remarks:
    //      The default limitation is 100, which means that CPU usage is unlimited.
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_SetCPULimitEx(int nChl, int nCpuUse);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_SetCPULimitEx(int nChl, int nCpuUse);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_SetCPULimitEx(int nChl, int nCpuUse);


    ////////////////////////////////////////////////////////////////////////////////
    // Function: 
    //      HB_PLAY_DecodeOneFrame.
    // Description: 
    //      Decode an audio or video frame.
    // Parameters: 
    //      [in] nChl - Channel number.
    //      [in] pInputFrame - Stream data, including audio or video.
    //      [in] dwInputSize - Stream size, in bytes.
    //      [in] nOutFormat - The formats of output YUV data. For format details, refer
    //           to the decoding formats for the output in Macro definition.
    // Return:
    //      Nonzero if successful; otherwise 0.
    //      This function would return FALSE if the stream data is input too fast.
    //      In this case, the stream data should be input once more.
    // Remarks:
    //      
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_DecodeOneFrame(int nChl, LPCVOID pInputFrame, uint dwInputSize, int nOutFormat, PHB_FRAME_INFO_EX pFrameInfo, int* pRemainderCount);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_DecodeOneFrame(int nChl, LPCVOID pInputFrame, uint dwInputSize, int nOutFormat, PHB_FRAME_INFO_EX pFrameInfo, int* pRemainderCount);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_DecodeOneFrame(int nChl, LPCVOID pInputFrame, uint dwInputSize, int nOutFormat, PHB_FRAME_INFO_EX pFrameInfo, ref int pRemainderCount);


    ////////////////////////////////////////////////////////////////////////////////
    // Function: 
    //      HB_PLAY_ResetSourceBuffer.
    // Description: 
    //      Clear the data in the source buffer.
    // Parameters: 
    //      [in] nChl - Channel number.
    // Return:
    //      Nonzero if successful; otherwise 0.
    // Remarks:
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_ResetSourceBuffer(int nChl);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_ResetSourceBuffer(int nChl);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_ResetSourceBuffer(int nChl);


    ////////////////////////////////////////////////////////////////////////////////
    // Function: 
    //      HB_PLAY_SetSourceBufferCB.
    // Description: 
    //      Sets callback function.
    // Parameters: 
    //      [in] nChl - Channel number.
    //      [in] dwThreshold - Buffered frame threshold.
    //      [in] pfnSourceBufCallback - Callback function pointer.
    //           void CALLBACK pfnSourceBufCallback(long nChl, DWORD dwBufSize,
    //              void* pUser, void* pResvered);
    //              - [in] nChl - Channel number.
    //              - [in] dwBufSize - The number of frames remains in the source buffer.
    //              - [in] pUser - User context.
    //              - [in] pResvered - Reserved.
    //      [in] pUser - User context.
    //      [in] pReserved - Reserved.
    // Return:
    //      Nonzero if successful; otherwise 0.
    // Remarks:
    //	    This function would be called when the source buffer data is less than the
    //      threshold value.
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_SetSourceBufferCB(int nChl, uint dwThreshold, LPSOURCEBUFCALLBACK pfnSourceBufCallback, IntPtr pUser, IntPtr pReserved);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_SetSourceBufferCB(int nChl, uint dwThreshold, LPSOURCEBUFCALLBACK pfnSourceBufCallback, IntPtr pUser, IntPtr pReserved);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_SetSourceBufferCB(int nChl, uint dwThreshold, LPSOURCEBUFCALLBACK pfnSourceBufCallback, IntPtr pUser, IntPtr pReserved);


    ////////////////////////////////////////////////////////////////////////////////
    // Function:
    //      HB_PLAY_SetDecodeMode.
    // Description:
    //      Sets the type of hardware accelerated decoder.
    // Parameters: 
    //      [in] nChl - Channel number.
    //      [in] nMode - The type of hardware accelerated decoder. For more details,
    //           refer to enum type HB_HD_TYPE.
    // Return:
    //      Nonzero if successful; otherwise 0.
    // Remarks:
    //      Currently only HB_HD_NVIDIA_CUDA is supported.
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_SetDecodeMode(int nChl, HB_HD_TYPE nMode);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_SetDecodeMode(int nChl, HB_HD_TYPE nMode);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_SetDecodeMode(int nChl, HB_HD_TYPE nMode);


    ////////////////////////////////////////////////////////////////////////////////
    // Function: 
    //      HB_PLAY_GetDecodeMode.
    // Description: 
    //      Retrieves the type of hardware accelerated decoder.
    // Parameters: 
    //      [in] nChl - Channel number.
    // Return:
    //      The type of hardware accelerated decoder.
    // Remarks:
    //      For more details of the return value, refer to enum type HB_HD_TYPE.
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)HB_HD_TYPE __stdcall HB_PLAY_GetDecodeMode(int nChl);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: HB_HD_TYPE __stdcall HB_PLAY_GetDecodeMode(int nChl);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //HB_HD_TYPE HB_PLAY_GetDecodeMode(int nChl);


    ////////////////////////////////////////////////////////////////////////////////
    //Obsolete Functions
    ////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_SetStreamOpenMode(int nChl, uint nMode);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_SetStreamOpenMode(int nChl, uint nMode);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_SetStreamOpenMode(int nChl, uint nMode);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)uint __stdcall HB_PLAY_GetStreamOpenMode(int nChl);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: uint __stdcall HB_PLAY_GetStreamOpenMode(int nChl);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //uint HB_PLAY_GetStreamOpenMode(int nChl);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_ConvertToBMPFile(int nChl, sbyte* pBuf, int nSize, int nWidth, int nHeight, int nType, sbyte* sFileName);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_ConvertToBMPFile(int nChl, sbyte* pBuf, int nSize, int nWidth, int nHeight, int nType, sbyte* sFileName);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_ConvertToBMPFile(int nChl, ref string pBuf, int nSize, int nWidth, int nHeight, int nType, ref string sFileName);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_ConvertToBMPFileEx(int nChl, sbyte* pBuf, int nSize, int nWidth, int nHeight, int nType, byte* pBufDestination, int* len, int* pBMPHeadLen);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_ConvertToBMPFileEx(int nChl, sbyte* pBuf, int nSize, int nWidth, int nHeight, int nType, byte* pBufDestination, int* len, int* pBMPHeadLen);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_ConvertToBMPFileEx(int nChl, ref string pBuf, int nSize, int nWidth, int nHeight, int nType, ref byte pBufDestination, ref int len, ref int pBMPHeadLen);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_SetParseCallBack(int nChl, LPSRCDATAPARSECBFUN SrcDataParseCBFun, int nReserved1,int nReserved2);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_SetParseCallBack(int nChl, LPSRCDATAPARSECBFUN SrcDataParseCBFun, int nReserved1,int nReserved2);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_SetParseCallBack(int nChl, LPSRCDATAPARSECBFUN SrcDataParseCBFun, int nReserved1, int nReserved2);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_SetPicQuality(int nChl, int bHighQuality);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_SetPicQuality(int nChl, int bHighQuality);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_SetPicQuality(int nChl, int bHighQuality);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_GetPicQuality(int nChl, int* bHighQuality);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_GetPicQuality(int nChl, int* bHighQuality);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_GetPicQuality(int nChl, ref int bHighQuality);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_SetOverlayMode(int nChl, int bOverlayMode, uint colorKey);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_SetOverlayMode(int nChl, int bOverlayMode, uint colorKey);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_SetOverlayMode(int nChl, int bOverlayMode, uint colorKey);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_SetSnapShotCallBack(int nChl, LPSNAPSHOTCBFUN SnapshotCBFun);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_SetSnapShotCallBack(int nChl, LPSNAPSHOTCBFUN SnapshotCBFun);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_SetSnapShotCallBack(int nChl, LPSNAPSHOTCBFUN SnapshotCBFun);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)void __stdcall HB_PLAY_EnableIndexThread(int bEnable);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: void __stdcall HB_PLAY_EnableIndexThread(int bEnable);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //void HB_PLAY_EnableIndexThread(int bEnable);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_GetIndexInfo(int nChl, HB_FRAME_POS* pBuffer, uint* pSize);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_GetIndexInfo(int nChl, _HB_FRAME_POS* pBuffer, uint* pSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_GetIndexInfo(int nChl, _HB_FRAME_POS pBuffer, ref uint pSize);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_SetCPULimit(int nCpuUse);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_SetCPULimit(int nCpuUse);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_SetCPULimit(int nCpuUse);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_GetCPULimit(int* pCpuUse);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_GetCPULimit(int* pCpuUse);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_GetCPULimit(ref int pCpuUse);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)uint __stdcall HB_PLAY_GetColorKey(int nChl);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: uint __stdcall HB_PLAY_GetColorKey(int nChl);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //uint HB_PLAY_GetColorKey(int nChl);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)uint __stdcall HB_PLAY_GetLastError(int nChl);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: uint __stdcall HB_PLAY_GetLastError(int nChl);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //uint HB_PLAY_GetLastError(int nChl);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)uint __stdcall HB_PLAY_GetSourceBufferRemain(int nChl);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: uint __stdcall HB_PLAY_GetSourceBufferRemain(int nChl);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //uint HB_PLAY_GetSourceBufferRemain(int nChl);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_RefreshPlay(int nChl);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_RefreshPlay(int nChl);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_RefreshPlay(int nChl);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_InputVideoData(int nChl,sbyte* pBuf,uint nSize);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_InputVideoData(int nChl,sbyte* pBuf,uint nSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_InputVideoData(int nChl, ref string pBuf, uint nSize);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_InputAudioData(int nChl,sbyte* pBuf,uint nSize);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_InputAudioData(int nChl,sbyte* pBuf,uint nSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_InputAudioData(int nChl, ref string pBuf, uint nSize);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)uint __stdcall HB_PLAY_GetCurrentFrameTime(int nChl);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: uint __stdcall HB_PLAY_GetCurrentFrameTime(int nChl);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //uint HB_PLAY_GetCurrentFrameTime(int nChl);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_SetDisplayCallBack(int nChl, LPDISPLAYCBFUN DisplayCBFun);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_SetDisplayCallBack(int nChl, LPDISPLAYCBFUN DisplayCBFun);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_SetDisplayCallBack(int nChl, LPDISPLAYCBFUN DisplayCBFun);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_InitDDraw(IntPtr hWnd);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_InitDDraw(IntPtr hWnd);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_InitDDraw(IntPtr hWnd);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int __stdcall HB_PLAY_RealeseDDraw();
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: int __stdcall HB_PLAY_RealeseDDraw();
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY_RealeseDDraw();

    ////////////////////////////////////////////////////////////////////////////////
    //End HB_PLAY API
    ////////////////////////////////////////////////////////////////////////////////

    //#if __cplusplus
    //}
    //#endif

    //#endif // HBPLAYSDK_H
}
////////////////////////////////////////////////////////////////////////////////
// 版权所有，2009-2012，北京汉邦高科数字技术有限公司。
////////////////////////////////////////////////////////////////////////////////
// 文件名： HBPlaySDK.h
// 作者：   HBGK
// 版本：   3.4
// 日期：   2012-08-09
// 描述：   汉邦高科解码库C/C++头文件
////////////////////////////////////////////////////////////////////////////////
//#if ! HBPLAYSDK_H
//#define HBPLAYSDK_H

//#if __cplusplus
////C++ TO C# CONVERTER TODO TASK: Extern blocks are not supported in C#.
//extern "C"
//{
//#endif


////DLL导出
//#define HBDLLAPI __declspec(dllexport)


////////////////////////////////////////////////////////////////////////////////
//定义HB_PLAY2 API
////////////////////////////////////////////////////////////////////////////////


//媒体播放对象
//C++ TO C# CONVERTER TODO TASK: The following statement was not recognized, possibly due to an unrecognized macro:
//DECLARE_HANDLE(IntPtr);


//视频颜色
//#define HBPLAY2_VIDEO_COLOR_MIN
//#define HBPLAY2_VIDEO_COLOR_MAX
//#define HBPLAY2_VIDEO_COLOR_DEFAULT
public class HBPLAY2_COLOR_SPACE
{
    //结构体长度，以字节（byte）为单位。
    //调用者必须设置该成员等于sizeof(HBPLAY2_COLOR_SPACE)。
    public uint dwSize;

    //亮度，取值范围[HBPLAY2_VIDEO_COLOR_MIN, HBPLAY2_VIDEO_COLOR_MAX]，
    //默认值HBPLAY2_VIDEO_COLOR_DEFAULT。
    public uint dwBrightness;

    //对比度，取值范围[HBPLAY2_VIDEO_COLOR_MIN, HBPLAY2_VIDEO_COLOR_MAX]，
    //默认值HBPLAY2_VIDEO_COLOR_DEFAULT。
    public uint dwContrast;

    //饱和度，取值范围[HBPLAY2_VIDEO_COLOR_MIN, HBPLAY2_VIDEO_COLOR_MAX]，
    //默认值HBPLAY2_VIDEO_COLOR_DEFAULT。
    public uint dwSaturation;

    //色调，取值范围[HBPLAY2_VIDEO_COLOR_MIN, HBPLAY2_VIDEO_COLOR_MAX]，
    //默认值HBPLAY2_VIDEO_COLOR_DEFAULT。
    public uint dwHue;

    //保留。
    public uint dwReserved;

}

//播放模式
public enum HBPLAY2_MODE : int
{
    HBPLAY2_MODE_UNKNOWN = 0, //未知的模式
    HBPLAY2_MODE_FILE, //文件模式
    HBPLAY2_MODE_STREAM, //流模式
    HBPLAY2_MODE_COUNT, //播放模式数量
}

//播放状态
public enum HBPLAY2_STATE : int
{
    //停止。
    HBPLAY2_STATE_STOPPED = 0,

    //暂停。
    HBPLAY2_STATE_PAUSED,

    //播放中。
    //由HB_PLAY2_IsPlayBackward表明现在的播放方向。
    HBPLAY2_STATE_PLAYING,

    //保留。
    HBPLAY2_STATE_REQUIRE_TO_PAUSE,

    //保留。
    HBPLAY2_STATE_REQUIRE_TO_SEEK,

    //保留。
    HBPLAY2_STATE_SEEKING,

    //播放状态总数。
    HBPLAY2_STATE_COUNT,
}

//码流分析器特性
public enum HBPLAY2_DEMUX_STREAM_VERSION : int
{
    HBPLAY2_DEMUX_STREAM_UNKNOWN = 0, //未知流类型
    HBPLAY2_DEMUX_STREAM_15 = 1, //早期码流，15码流
    HBPLAY2_DEMUX_STREAM_6000 = 2, //早期码流，6000码流
    HBPLAY2_DEMUX_STREAM_V10 = 10, //第一代码流
    HBPLAY2_DEMUX_STREAM_V20 = 20, //第二代码流
    HBPLAY2_DEMUX_STREAM_V30 = 30, //第三代码流
}
public class HBPLAY2_DEMUX_PROPERTY
{
    //结构体长度，以字节（byte）为单位。
    //调用者必须设置该成员等于sizeof(HBPLAY2_DEMUX_PROPERTY)。
    public uint dwSize;

    //码流版本。
    public HBPLAY2_DEMUX_STREAM_VERSION StreamVersion;

    //保留。
    public uint[] dwReserved = new uint[2];

    //文件头。
    //注意： 绝大多数码流都有文件头，但HBPLAY2_DEMUX_STREAM_6000码流没有文件头。
    public string szFileHeader = new string(new char[64]);

}

//码流分析器性能
public class HBPLAY2_DEMUX_STATUS
{
    //结构体长度，以字节（byte）为单位。
    //调用者必须设置该成员等于sizeof(HBPLAY2_DEMUX_STATUS)。
    public uint dwSize;

    //音频帧数。
    public uint dwAudioFrameCount;

    //视频I帧数。
    public uint dwVideoIFrameCount;

    //视频P帧数。
    public uint dwVideoPFrameCount;

    //视频B帧数。
    public uint dwVideoBFrameCount;

    //视频E帧数。
    public uint dwVideoEFrameCount;

    //码流总时间，以毫秒（ms）为单位。
    public uint dwStreamDuration;

    //保留。
    public uint dwReserved0;

    //分析码流总时间，以微秒（us）为单位。
    public long llParseDuration;

    //保留。
    public uint[] dwReserved1 = new uint[2];

}

//视频解码器性能
public enum HBPLAY2_VDEC_CODEC_TYPE : int
{
    HBPLAY2_VDEC_CODEC_UNKNOWN = 0, //未知解码器类型
    HBPLAY2_VDEC_CODEC_MPEG4, //MPEG4解码器
    HBPLAY2_VDEC_CODEC_MPEG4_ISO, //MPEG4 ISO解码器
    HBPLAY2_VDEC_CODEC_FFMPEG, //FFMPEG H.264解码器
    HBPLAY2_VDEC_CODEC_HISILICON, //Hisilicon H.264解码器
    HBPLAY2_VDEC_CODEC_NVIDIA_CUDA, //NVIDIA CUDA H.264解码器
    HBPLAY2_VDEC_CODEC_JPEG, //JPEG解码器
    HBPLAY2_VDEC_CODEC_AMD_OVD, //AMD_OVD H.264解码器
    HBPLAY2_VDEC_CODEC_COUNT, //视频解码器数量
}
public class HBPLAY2_VDEC_STATUS
{
    //结构体长度，以字节（byte）为单位。
    //调用者必须设置该成员等于sizeof(HBPLAY2_VDEC_STATUS)。
    public uint dwSize;

    //视频解码的总帧数。
    public uint dwFrameCount;

    //视频解码的总时间，以微秒（us）为单位。
    public long llDecodeDuration;

    //实时解码速度，以帧/秒为单位。
    public uint dwRealtimeSpeed;

    //解码线程数量。
    //若dwThreadCount == 0，表示当前没有使用CPU解码，而是使用GPU解码。
    public uint dwThreadCount;

    //视频解码器类型。
    public HBPLAY2_VDEC_CODEC_TYPE CodecType;

    //保留。
    public uint dwReserved;

}

//视频显示性能
public enum HBPLAY2_DISPLAY_TYPE : int
{
    HBPLAY2_DISPLAY_NONE = 0, //未使用特定的显示方式
    HBPLAY2_DISPLAY_DRAWDIB = 1, //直接显示与设备无关位图
    HBPLAY2_DISPLAY_DIRECTDRAW_7 = 2, //通过DirectDraw 7显示图像
    HBPLAY2_DISPLAY_DIRECT_3D_9 = 4, //通过Direct3D 9显示图像
    HBPLAY2_DISPLAY_DIRECT_3D_11 = 8, //通过Direct3D 11显示图像
}
public class HBPLAY2_DISPLAY_STATUS
{
    //结构体长度，以字节（byte）为单位。
    //调用者必须设置该成员等于sizeof(HBPLAY2_DISPLAY_STATUS)。
    public uint dwSize;

    //当前的显示方式
    public HBPLAY2_DISPLAY_TYPE DisplayType;

    //显示的帧数。
    public uint dwFrameCount;

    //抓图的帧数。
    public uint dwSnapshotCount;

    //计划显示总时间，以微秒（us）为单位。
    public long llPlannedDuration;

    //实际显示总时间，以微秒（us）为单位。
    public long llActualDuration;

    //抓图总时间，以微秒（us）为单位。
    public long llSnapshotDuration;

    //保留。
    public uint[] dwReserved = new uint[2];

}

//音频解码器性能
public enum HBPLAY2_ADEC_CODEC_TYPE : int
{
    HBPLAY2_ADEC_CODEC_UNKNOWN = 0, //未知解码器类型
    HBPLAY2_ADEC_CODEC_PCM_16_BIT, //未压缩的16位PCM
    HBPLAY2_ADEC_CODEC_PCM_LINEAR_8_BIT, //8位线性PCM，需要扩展到16位
    HBPLAY2_ADEC_CODEC_G711_ALAW, //G.711 A律
    HBPLAY2_ADEC_CODEC_G722, //G.722
    HBPLAY2_ADEC_CODEC_G726, //Hisilicon G.726 16kbps
    HBPLAY2_ADEC_CODEC_COUNT, //音频解码器数量
}
public class HBPLAY2_ADEC_STATUS
{
    //结构体长度，以字节（byte）为单位。
    //调用者必须设置该成员等于sizeof(HBPLAY2_ADEC_STATUS)。
    public uint dwSize;

    //音频解码的总帧数。
    public uint dwFrameCount;

    //音频解码的总时间，以微秒（us）为单位。
    public long llDecodeDuration;

    //音频解码器类型。
    public HBPLAY2_ADEC_CODEC_TYPE CodecType;

    //保留。
    public uint dwReserved;

}

//音频播放性能
public class HBPLAY2_SOUND_STATUS
{
    //结构体长度，以字节（byte）为单位。
    //调用者必须设置该成员不小于sizeof(HBPLAY2_SOUND_STATUS)。
    public uint dwSize;

    //播放标志。
    //标志为FLASE时，停止播放声音。否则，正在播放声音。
    public int bPlaying;

    //缓冲数据的长度，以字节为单位。
    public uint dwBufferedDataLength;

    //缓冲数据的时间，以毫秒为单位。
    public uint dwBufferedTime;

    //保留。
    public uint[] dwReserved = new uint[2];

}

//增加或修改视口
public class HBPLAY2_VIEWPORT
{
    //结构体长度，以字节（byte）为单位。
    //调用者必须设置该成员等于sizeof(HBPLAY2_VIEWPORT)。
    public uint dwSize;

    //视口标识符。
    //由调用者指定的唯一标志，用于区分多个视口。
    public uint dwID;

    //显示窗口句柄。
    //必须是有效的窗口句柄。
    public IntPtr hDestWnd;

    //保留。
    public uint[] dwReserved = new uint[3];

    //原始数据矩形，该矩形不能大于原始图像的尺寸。
    //当矩形为空时（即通过IsRectEmpty函数判断返回值是TRUE），显示整个原始图像。
    //当矩形为非空时，显示指定部分的图像。一般用于局部放大显示。
    public Rectangle rcSrc = new Rectangle();

    //窗口中显示位置。
    //当矩形为空时（即通过IsRectEmpty函数判断返回值是TRUE），图像显示在窗口的整个客户区。
    //当矩形为非空时，图像显示在窗口中指定的位置。一般用于在一个窗口上显示多个图像。
    public Rectangle rcDest = new Rectangle();

}

//抓图
public class HBPLAY2_SNAP_PICTURE
{
    //结构体长度，以字节（byte）为单位。
    //调用者必须设置该成员等于sizeof(HBPLAY2_SNAP_PICTURE)。
    public uint dwSize;

    //DIB数据缓冲区长度，以字节为单位。
    public uint dwBitsLength;

    //DIB数据缓冲区指针。
    //根据图像的尺寸，先分配足够大的内存。
    public IntPtr pBits;

    //保留。
    public uint dwReserved;

    //返回DIB尺寸和颜色信息。
    public BITMAPINFOHEADER bmiHeader = new BITMAPINFOHEADER();

}
//帧类型
public enum HBPLAY2_FRAME_TYPE : int
{
    HBPLAY2_FRAME_UNKNOWN, //未知帧类型
    HBPLAY2_FRAME_AUDIO, //音频帧
    HBPLAY2_FRAME_VIDEO_I, //视频I帧
    HBPLAY2_FRAME_VIDEO_P, //视频P帧
    HBPLAY2_FRAME_VIDEO_B, //视频B帧
    HBPLAY2_FRAME_VIDEO_E, //视频E帧
    HBPLAY2_FRAME_COUNT, //帧类型数量
}

//像素格式
//在 http://www.fourcc.org/yuv.php 查看FOURCC定义和YUV的内存排列。
//#if ! MAKEFOURCC
////C++ TO C# CONVERTER TODO TASK: The following #define macro was ignored, as specified on the Options dialog:
//#define MAKEFOURCC(ch0, ch1, ch2, ch3) ((DWORD)(BYTE)(ch0) | ((DWORD)(BYTE)(ch1) << 8) | ((DWORD)(BYTE)(ch2) << 16) | ((DWORD)(BYTE)(ch3) << 24 )) enum HBPLAY2_FRAME_TYPE
//#else
//public enum HBPLAY2_FRAME_TYPE: int
//#endif //defined(MAKEFOURCC)


//#define HBPLAY2_PIXEL_UNCHANGE
//#define HBPLAY2_PIXEL_RGB24
//#define HBPLAY2_PIXEL_BGR24
//#define HBPLAY2_PIXEL_UYVY MAKEFOURCC('U', 'Y', 'V', 'Y')
//#define HBPLAY2_PIXEL_YUY2 MAKEFOURCC('Y', 'U', 'Y', '2')
//#define HBPLAY2_PIXEL_YV12 MAKEFOURCC('Y', 'V', '1', '2')
//#define HBPLAY2_PIXEL_I420 MAKEFOURCC('I', '4', '2', '0')
//#define HBPLAY2_PIXEL_NV12 MAKEFOURCC('N', 'V', '1', '2')
//#define HBPLAY2_PIXEL_NV21 MAKEFOURCC('N', 'V', '2', '1')
////帧类型
//{
//    HBPLAY2_FRAME_UNKNOWN, //未知帧类型
//    HBPLAY2_FRAME_AUDIO, //音频帧
//    HBPLAY2_FRAME_VIDEO_I, //视频I帧
//    HBPLAY2_FRAME_VIDEO_P, //视频P帧
//    HBPLAY2_FRAME_VIDEO_B, //视频B帧
//    HBPLAY2_FRAME_VIDEO_E, //视频E帧
//    HBPLAY2_FRAME_COUNT, //帧类型数量
//}

////判断帧类型
////C++ TO C# CONVERTER TODO TASK: The following #define macro was ignored, as specified on the Options dialog:
//#define HB_PLAY2_IsAudioFrame(FrameType) (HBPLAY2_FRAME_AUDIO == (FrameType))
////C++ TO C# CONVERTER TODO TASK: The following #define macro was ignored, as specified on the Options dialog:
//#define HB_PLAY2_IsVideoFrame(FrameType) ((FrameType) >= HBPLAY2_FRAME_VIDEO_I && (FrameType) <= HBPLAY2_FRAME_VIDEO_E)

//帧信息
public class HBPLAY2_FRAME
{
    //保留
    public uint dwReserved0;

    //保留
    public uint[] dwReserved1 = new uint[2];

    //帧类型
    //帧类型为HBPLAY2_FRAME_AUDIO时，u.Audio有效。
    //帧类型为HBPLAY2_FRAME_VIDEO_*时，u.Video有效。
    public HBPLAY2_FRAME_TYPE FrameType;

    //C++ TO C# CONVERTER TODO TASK: Unions are not supported in C#.
    //ORIGINAL LINE: union FRAME
    public struct FRAME
    {
        public class FRAME_AUDIO
        {
            //帧头长度，以字节为单位。
            public uint dwHeaderSize;

            //帧头地址。
            public IntPtr pHeader;

            //编码数据长度，以字节为单位。
            public uint dwDataSize;

            //编码数据地址。
            public IntPtr pData;

            //音频通道数。
            public ushort wChannels;

            //采样精度。
            public ushort wBitsPerSample;

            //采样率，以赫兹（Hz）为单位。
            public uint dwSampleRate;

            //解码缓冲区。
            public IntPtr pBuffer;

            //解码缓冲区的长度，以字节为单位。
            //输入时，说明缓冲区的总长度。
            //输出时，说明缓冲区的实际使用长度。
            public uint dwBufferLength;

            //保留。
            public uint[] dwReserved = new uint[4];

        }
        public FRAME_AUDIO Audio;

        public class FRAME_VIDEO
        {
            //帧头长度，以字节为单位。
            public uint dwHeaderSize;

            //帧头地址。
            public IntPtr pHeader;

            //编码数据长度，以字节为单位。
            public uint dwDataSize;

            //编码数据地址。
            public IntPtr pData;

            //图像宽度，以像素为单位。
            public int nWidth;

            //图像高度，以像素为单位。
            public int nHeight;

            //距文件起始地址的偏移量，用于文件索引。
            public long llOffset;

            //视频帧绝对时间。
            public SYSTEMTIME TimeStamp = new SYSTEMTIME();

            //帧序号。
            //帧序号是逐渐递增的，用于判断视频帧的连续性。
            public uint dwIndex;

            //视频帧相对时间，以毫秒为单位。
            //相邻两帧之间的差值，就是这两帧之间的时间间隔。
            public uint dwTickCount;

            //实际输出的像素格式，由视频解码器设置该格式（HBPLAY2_PIXEL_*宏）。
            public uint dwPixelFormat;

            //解码缓冲区。
            public IntPtr pBuffer;

            //解码缓冲区的长度，以字节为单位。
            public uint dwBufferLength;

            //保留。
            public uint[] dwReserved = new uint[3];

        }
        public FRAME_VIDEO Video;
    }
    public FRAME u = new FRAME();

}


////////////////////////////////////////////////////////////////////////////////
//结束HB_PLAY2 API
////////////////////////////////////////////////////////////////////////////////








////////////////////////////////////////////////////////////////////////////////
//Define HB_PLAY API
////////////////////////////////////////////////////////////////////////////////


////////////////////////////////////////////////////////////////////////////////
// Macro definition.
////////////////////////////////////////////////////////////////////////////////


//// the message sent when the index has been created
//#define MSG_INDEX_OK (WM_USER+154)

//// the maximum number of regions for display in a channel.
//#define MAX_DISPLAY_WND

//// frame type
//#define UNKOWN_FRAME_TYPE
//#define VIDEO_FRAME_I
//#define VIDEO_FRAME_P
//#define VIDEO_FRAME_B
//#define VIDEO_FRAME_E
//#define AUDIO_FRAME_ALAW
//#define AUDIO_FRAME_G722
//#define AUDIO_FRAME_G726
//#define AUDIO_FRAME_PCM8_16

//// the decoding formats for the output
//#define OUT_FMT_YUV_420
//#define OUT_FMT_YUV_422
//#define OUT_FMT_AUDIO

//// pixel format
//// see http://www.fourcc.org/yuv.php for more information
//#if ! MAKEFOURCC
////C++ TO C# CONVERTER TODO TASK: The following #define macro was ignored, as specified on the Options dialog:
//#define MAKEFOURCC(ch0, ch1, ch2, ch3) ((DWORD)(BYTE)(ch0) | ((DWORD)(BYTE)(ch1) << 8) | ((DWORD)(BYTE)(ch2) << 16) | ((DWORD)(BYTE)(ch3) << 24 )) enum HB_PLAY_MODE
//#else
//public enum HB_PLAY_MODE: int
//#endif //defined(MAKEFOURCC)



//#define OUT_FMT_UYVY MAKEFOURCC('U', 'Y', 'V', 'Y')
//#define OUT_FMT_YUY2 MAKEFOURCC('Y', 'U', 'Y', '2')
//#define OUT_FMT_YV12 MAKEFOURCC('Y', 'V', '1', '2')
//#define OUT_FMT_I420 MAKEFOURCC('I', '4', '2', '0')
//#define OUT_FMT_NV12 MAKEFOURCC('N', 'V', '1', '2')
//#define OUT_FMT_NV21 MAKEFOURCC('N', 'V', '2', '1')
//#define OUT_FMT_RGB24
//#define OUT_FMT_BGR24
////////////////////////////////////////////////////////////////////////////////
// Structure definition.
////////////////////////////////////////////////////////////////////////////////

// The data type of input.
public enum HB_PLAY_MODE
{
    HB_FILE_MODE, // Read data from a file.
    HB_STREAM_REALTIME_MODE, // Receive real-time data.
    HB_STREAM_FILE_MODE // Input data from a file as stream.
}

// The type of hardware accelerated decoder.
public enum HB_HD_TYPE
{
    HB_HD_NOT_SUPPORTED = 1,
    HB_HD_AUTO, // Determines the decoder according to graphics type.
    HB_HD_NVIDIA_CUDA, // NVIDIA graphics solution.
    HB_HD_MS_DXVA // Microsoft solution.
}

// detailed information of the frame
public class _HB_FRAME
{
    public uint dwFrameNumber; // frame number, incremental

    public ushort wFrameType; // frame type
    public byte cbAlgorithm; // algorithm
    public byte cbVersion; // version

    public ushort wWidth; // picture width in pixels
    public ushort wHeight; // picture height in pixels

    public uint dwDTS; // the time stamp of the frame, in milliseconds
    public SYSTEMTIME ETS = new SYSTEMTIME(); // the system time when encoded

    public uint dwEncryptFlag; // encrypt flag
    public uint dwEncryptKey; // encrypt key
    public uint dwEncryptText; // encrypt text

    public ushort wOsdFlag; // OSD flag, valid only for video frame
    public ushort wOsdLines; // Represents the OSD lines if the wOsdFlag
                             // parameter is not null, otherwise null. 
    public IntPtr pOsd; // the long pointer to the OSD information
    public uint dwReserved; // reserved for alignment

    public ULARGE_INTEGER ulOffset = new ULARGE_INTEGER(); // the offset of bytes from the beginning of file
                                                           // to the current position

    public uint dwHeaderSize; // the size of the frame header
    public IntPtr pHeader; // the pointer to the frame header

    public uint dwDataSize; // the size of the frame data
    public IntPtr pData; // the pointer to the frame data

    public uint[] Reserved = new uint[8]; // reserved
}

// brief information of the frame
public class _HB_FRAME_INFO
{
    public int nWidth; // picture width in pixels, 0 if audio
    public int nHeight; // picture height in pixels, 0 if audio
    public int nStamp; // the time stamp of the frame in milliseconds
    public int nType; // the decoding format for the output
    public int nFrameRate; // frame rate
}

// time information of the frame
public class _HB_VIDEO_TIME
{
    public short year; // year
    public short month; // month
    public short day; // day
    public short hour; // hour
    public short minute; // minute
    public short second; // second
    public short milli; // millisecond
    public short week; // week
}

// position information of the frame
public class _HB_FRAME_POS
{
    // the offset of bytes from the beginning of file to the current position
    public ULARGE_INTEGER nFilePos = new ULARGE_INTEGER();
    public int nFrameNum; // frame number
    public int nFrameTime; // the time stamp of the frame in milliseconds
}

// single frame information
public class _HB_FRAME_INFO_EX
{
    public uint dwFrameType; // frame type, OUT_FMT_AUDIO or pixel format
    public uint dwFrameSize; // the frame size, in bytes
    public IntPtr pFrame; // the pointer to the frame buffer

    // Only for video frame.
    public uint dwWidth; // picture width in pixels
    public uint dwHeight; // picture height in pixels
    public uint dwDTS; // the time stamp of the frame in milliseconds

    // Only for audio frame.
    public uint dwSampleRate; // audio sampling rate
    public uint dwSampleBit; // audio sampling bits

    public uint[] dwReserved = new uint[4]; // reserved
}

internal sealed class DefineConstantsHBPlaySDK
{
    public const int FACILITY_HBPLAY2 = 0xAB5;
    public const int HBPLAY2_HEADER_LENGTH_MIN = 64;
    public const int HBPLAY2_ENABLE_STREAM_PARSE = 0x00000000;
    public const int HBPLAY2_ENABLE_VIDEO_CODEC = 0x00000001;
    public const int HBPLAY2_ENABLE_MULTITHREADING_VIDEO_CODEC = 0x00000002;
    public const int HBPLAY2_ENABLE_HARDWARE_VIDEO_CODEC = 0x00000004;
    public const int HBPLAY2_ENABLE_VIDEO_QUALITY_PRIORITY = 0x00000008;
    public const int HBPLAY2_ENABLE_VERIFY_CONTINUOUS_VIDEO = 0x00000010;
    public const int HBPLAY2_ENABLE_DISPLAY = 0x00000100;
    public const int HBPLAY2_ENABLE_PRESENT_IMMEDIATE = 0x00000200;
    public const int HBPLAY2_ENABLE_DIRECT3D_9 = 0x00000400;
    public const int HBPLAY2_ENABLE_DIRECTDRAW_7 = 0x00000800;
    public const int HBPLAY2_ENABLE_DRAWDIB = 0x00010000;
    public const int HBPLAY2_ENABLE_AUDIO_CODEC = 0x00001000;
    public const int HBPLAY2_ENABLE_SOUND = 0x00002000;
    public const int HBPLAY2_ENABLE_SOUND_PRIORITY = 0x00004000;
    public const int HBPLAY2_ENABLE_GET_DECODE_FRAME = 0x10000000;
    public const int HBPLAY2_VOLUME_MIN = 0x0;
    public const int HBPLAY2_VOLUME_MAX = 0xFFFF;
    public const int HBPLAY2_VIDEO_COLOR_MIN = 0;
    public const int HBPLAY2_VIDEO_COLOR_MAX = 127;
    public const int HBPLAY2_VIDEO_COLOR_DEFAULT = 64;
    public const int HBPLAY2_CPU_LIMIT_MIN = 50;
    public const int HBPLAY2_CPU_LIMIT_MAX = 100;
    public const int HBPLAY2_JPEG_QUALITY_MIN = 1;
    public const int HBPLAY2_JPEG_QUALITY_MAX = 100;
    public const int HBPLAY2_PIXEL_UNCHANGE = 0;
    public const int HBPLAY2_PIXEL_RGB24 = 2;
    public const int HBPLAY2_PIXEL_BGR24 = 3;
    public const int HBPLAY2_BUFFER_EMPTY_THRESHOLD_MIN = 0;
    public const int HBPLAY2_BUFFER_EMPTY_THRESHOLD_MAX = 150;
    public const int MAX_DISPLAY_WND = 4;
    public const int UNKOWN_FRAME_TYPE = 0x0000;
    public const int VIDEO_FRAME_I = 0x2000;
    public const int VIDEO_FRAME_P = 0x2001;
    public const int VIDEO_FRAME_B = 0x2002;
    public const int VIDEO_FRAME_E = 0x2003;
    public const int AUDIO_FRAME_ALAW = 0x1000;
    public const int AUDIO_FRAME_G722 = 0x1001;
    public const int AUDIO_FRAME_G726 = 0x1002;
    public const int AUDIO_FRAME_PCM8_16 = 0x1003;
    public const int OUT_FMT_YUV_420 = 0x00000601;
    public const int OUT_FMT_YUV_422 = 0x00000102;
    public const int OUT_FMT_AUDIO = 0x00000602;
    public const int OUT_FMT_RGB24 = 2;
    public const int OUT_FMT_BGR24 = 3;
}
//BitmapInfoHeader定义了位图的头部信息
[StructLayout(LayoutKind.Sequential)]
public struct BITMAPINFOHEADER
{
    public int biSize;
    public int biWidth;
    public int biHeight;
    public short biPlanes;
    public short biBitCount;
    public int biCompression;
    public int biSizeImage;
    public int biXPelsPerMeter;
    public int biYPelsPerMeter;
    public int biClrUsed;
    public int biClrImportant;
}
public class SYSTEMTIME
{
    public ushort wYear;
    public ushort wMonth;
    public ushort wDayOfWeek;
    public ushort wDay;
    public ushort wHour;
    public ushort wMinute;
    public ushort wSecond;
    public ushort wMilliseconds;
}
//C++ TO C# CONVERTER TODO TASK: Unions are not supported in C#.
//ORIGINAL LINE: typedef union _ULARGE_INTEGER
public struct ULARGE_INTEGER
{
    //C++ TO C# CONVERTER NOTE: Classes must be named in C#, so the following class has been named AnonymousClass:
    public class AnonymousClass
    {
        public uint LowPart;
        public uint HighPart;
    }
    //C++ TO C# CONVERTER NOTE: Classes must be named in C#, so the following class has been named AnonymousClass2:
    public class AnonymousClass2
    {
        public uint LowPart;
        public uint HighPart;
    }
    public AnonymousClass2 u;
    public ulong QuadPart;
}
public class HB_FRAME_INFO
{
    public int nWidth; // picture width in pixels, 0 if audio
    public int nHeight; // picture height in pixels, 0 if audio
    public int nStamp; // the time stamp of the frame in milliseconds
    public int nType; // the decoding format for the output
    public int nFrameRate; // frame rate
}
public class HB_VIDEO_TIME
{
    public short year; // year
    public short month; // month
    public short day; // day
    public short hour; // hour
    public short minute; // minute
    public short second; // second
    public short milli; // millisecond
    public short week; // week
}
public class ST_HB_SDVR_INITIATIVE_LOGIN
{
    public string sDVRID = new string(new char[48]);
    public string sSerialNumber = new string(new char[48]);
    public byte byAlarmInPortNum;
    public byte byAlarmOutPortNum;
    public byte byDiskNum;
    public byte byProtocol;
    public byte byChanNum;
    public byte byEncodeType;
    public byte[] reserve = new byte[26];
    public string sDvrName = new string(new char[32]);
    public string[] sChanName = new string[128];
}
