
using System;
using System.Drawing;
using System.Runtime.InteropServices;
public static class GlobalMembersHBPlaySDK
{

    ////�������
    ////����ʹ��SUCCESS��FAILED���жϴ�����롣
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

    //���ļ�
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

    //�ļ�ͷ���ȷ�Χ
    //#define HBPLAY2_HEADER_LENGTH_MIN
    //#define HBPLAY2_HEADER_LENGTH_MAX (256*1024)

    //����
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_OpenStream( OUT IntPtr* phPlay,  LPCVOID pFileHeader,  uint dwHeaderLength );
    //C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    //ORIGINAL LINE: int WINAPI HB_PLAY2_OpenStream(OUT IntPtr* phPlay,  LPCVOID pFileHeader,  uint dwHeaderLength);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY2_OpenStream(ref OUT IntPtr phPlay,  LPCVOID pFileHeader,  uint dwHeaderLength);

    //�ر�ý�岥�Ŷ���
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_Close(  IntPtr hPlay );
    //C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    //ORIGINAL LINE: int WINAPI HB_PLAY2_Close( IntPtr hPlay);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY2_Close( IntPtr hPlay);

    //����/��ȡ����ʹ�ܱ�־
    //��Щ��־λ������ȫ�������á�����֮���������ϵ���ο���־λ��ע�͡�

    //ʹ������������־��
    //�ù���ʼ��ʹ�ܣ����ܱ���ֹ��
    //#define HBPLAY2_ENABLE_STREAM_PARSE
    //#define HBPLAY2_ENABLE_NONE HBPLAY2_ENABLE_STREAM_PARSE

    //ʹ����Ƶ��������־��
    //ʹ�ܸñ�־���ܽ�����Ƶ���롣
    //��ֹ�ñ�־ʱ��ͬʱҲ���ֹHBPLAY2_ENABLE_MULTITHREADING_VIDEO_CODEC��
    //HBPLAY2_ENABLE_HARDWARE_VIDEO_CODEC��HBPLAY2_ENABLE_VIDEO_QUALITY_PRIORITY��
    //HBPLAY2_ENABLE_VERIFY_CONTINUOUS_VIDEO��HBPLAY2_ENABLE_DISPLAY��־��
    //#define HBPLAY2_ENABLE_VIDEO_CODEC

    ////ʹ�ܶ��߳���Ƶ�����־��
    ////���߳̽����ܹ�������ö��CPU����Դ���ӿ�����ٶȣ������ڽ���·��Ƶͬʱ���롣
    ////��ֹ�ñ�־ʱ��һ��ý�岥�Ŷ���ֻ��ʹ��һ���߳̽�����Ƶ���룬�����ڽ϶�·
    ////��Ƶ���������16·��ͬʱ���롣
    //#define HBPLAY2_ENABLE_MULTITHREADING_VIDEO_CODEC

    ////ʹ��Ӳ����Ƶ��������־��
    ////ʹ�ܸñ�־ʱ�������Զ�̽����õ�Ӳ��������������ʹ��Ӳ����������Ӳ�������ܹ�
    //��������Կ���Ӳ����Դ����ʡCPU��Դ����ǰ֧�ֵ�Ӳ���������У�NVIDIA CUDA��
    //1. NVIDIA CUDA��
    //  ʹ��NVIDIA CUDA������������ͬʱ��������������
    //  [1] ��Ƶ�����㷨��H.264 Baseline, Main, High Profile Level 4.1��
    //      ͼ��ߴ粻����1080P��������С������45mbps��
    //  [2] ����NVIDIA G8x, G9x, MCP79, MCP89, G98, GT2xx, GF1xx����߰汾GPU��GPU��
    //      ��������������v1.1����ʾ�ڴ治����128MB��
    //    //  [3] �Կ���������汾������v286.19��CUDA��������İ汾������v4.1��
    //    #define HBPLAY2_ENABLE_HARDWARE_VIDEO_CODEC

    //    //ʹ����Ƶͼ���������ȱ�־��
    //    //ʹ�ܸñ�־ʱ������Ƶ����ʱ�����ȱ�֤ͼ��������������ܻᵼ�½ϸߵ�
    //    //CPUʹ���ʡ�
    //    //����ֹ�ñ�־������Ƶ����ʱ�����ȱ�֤��Ƶ�������ԣ�����ά�ֽϵ͵�CPUʹ���ʣ�
    //    //�����ܻή��ͼ���������
    //    #define HBPLAY2_ENABLE_VIDEO_QUALITY_PRIORITY

    //    //ʹ��У��������Ƶ��־��
    //    //ʹ�ܸñ�־ʱ���ܹ������Ƶ�������ݵ������ԡ������ֲ���������Ƶ֡ʱ����ͣ���룬
    //    //һֱ�ȵ���һ���ؼ�֡�ٻָ����롣ʹ�ܸñ�־�����Ա������ڶ�֡���µ���Ƶͼ���
    //    //���������󣬵����ܵ�����Ƶ����ͣ�١�
    //    #define HBPLAY2_ENABLE_VERIFY_CONTINUOUS_VIDEO

    //    //ʹ����Ƶ��ʾ��־��
    //    //ʹ�ܸñ�־������ʾ��Ƶͼ��
    //    //ֻʹ�ܸñ�־��û��ʹ�ܾ����ͼ����ʾ��־ʱ�����Զ�ѡ��Ĭ�ϵ�ͼ����ʾ��ʽ��
    //    //��ֹ�ñ�־��ͬʱҲ���ֹHBPLAY2_ENABLE_DRAWDIB��HBPLAY2_ENABLE_DIRECTDRAW_7��
    //    //HBPLAY2_ENABLE_DIRECT3D_9��־��
    //    #define HBPLAY2_ENABLE_DISPLAY

    //    //ʹ����������ͼ���־��
    //    //�ñ�־�ṩ�˲�����ʾ�����Ƶ�ͼ��ˢ���ʣ������ܵ���ͼ��ġ�˺�ѡ�����
    //    //��ֹ�ñ�־ʱ����Ƶͼ����ʾ֡�ʲ��ܳ�����ʾ����ˢ���ʣ�����΢���CPUʹ���ʣ�
    //    //��������ʾ����������Ƶͼ��
    //    //�ñ�־ֻ��Direct3D 9����ʾ��ʽ��Ч��
    //    #define HBPLAY2_ENABLE_PRESENT_IMMEDIATE

    //    //ʹ��Direct3D 9��ʾͼ��
    //    //Windows Vista/2008/7���Ժ�汾��Ĭ��ʹ��Direct3D 9����ʾ��Ƶ��
    //    //��ֹ�ñ�־��ͬʱҲ���ֹHBPLAY2_ENABLE_PRESENT_IMMEDIATE��־��
    //    #define HBPLAY2_ENABLE_DIRECT3D_9

    //    //ʹ��DirectDraw 7��ʾͼ��
    //    //DirectDraw 7����ʾЧ�ʽϸߣ����Կ���Ҫ��ϵͣ������ܱ���ͼ��ġ�˺�ѡ�����
    //    //Windows 2000/XP/2003��Ĭ��ʹ��DirectDraw 7����ʾ��Ƶ��
    //    #define HBPLAY2_ENABLE_DIRECTDRAW_7

    //    //ʹ��ֱ����ʾ���豸�޹�λͼ��
    //    //�ñ�־�ṩ�ܺõļ����ԣ��ʺ���Զ���������ӣ���û�а�װ�Կ�����ʱʹ�á�
    //    //����ʾЧ�ʵͣ�ͼ�������͡�
    //    #define HBPLAY2_ENABLE_DRAWDIB

    //    //ʹ����Ƶ��������־��
    //    //ʹ�ܸñ�־���ܽ�����Ƶ���롣
    //    //��ֹ�ñ�־��ͬʱҲ���ֹHBPLAY2_ENABLE_SOUND��־��
    //    #define HBPLAY2_ENABLE_AUDIO_CODEC

    //    //ʹ����Ƶ���ű�־��
    //    //ʹ�ܸñ�־���ܽ�����Ƶ���š�
    //    //��ֹ�ñ�־��ͬʱҲ���ֹHBPLAY2_ENABLE_SOUND_PRIORITY��־��
    //    #define HBPLAY2_ENABLE_SOUND

    //    //ʹ����Ƶ�������ȱ�־��
    //    //һ������£�����Ƶ��ʱ�������Ʋ��ŵ��ٶȡ�ʹ�ܸñ�־������ʹ����Ƶ��ʱ��
    //    //�����Ʋ��ŵ��ٶȡ�
    //    //�ñ�־һ�����ڲ��Ŵ���Ƶ����
    //    #define HBPLAY2_ENABLE_SOUND_PRIORITY

    //    //ʹ��HB_PLAY2_GetDecodeFrame������־��
    //    //ʹ�ܸñ�־��ͬʱ���ֹHBPLAY2_ENABLE_DISPLAY��HBPLAY2_ENABLE_SOUND��־����ʹ��
    //    //HBPLAY2_ENABLE_VIDEO_QUALITY_PRIORITY��־��
    //    //ֻ��ͨ��HB_PLAY2_GetDecodeFrame������ȡ����������Ƶ���ݣ����Ҳ���ִ���κ�
    //    //���Ŷ�����
    //    //����߼��û���ʹ�øñ�־λ��
    //    //������ϸ��Ϣ���ο�HB_PLAY2_GetDecodeFrame������
    //    #define HBPLAY2_ENABLE_GET_DECODE_FRAME

    //    //ʹ��Ĭ�����á�
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

    //    //���Ŷ���
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

    //    //�����ٶ�
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

    //    //����
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

    //    //CPUʹ����
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

    //    //���ŷ���
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_IsPlayBackward(  IntPtr hPlay, OUT int* pbBackward );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_IsPlayBackward( IntPtr hPlay, OUT int* pbBackward);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_IsPlayBackward( IntPtr hPlay, ref OUT int pbBackward);

    //    //�ļ��������
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_IsFileIndexCompleted(  IntPtr hPlay, OUT int* pbIndexCompleted );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_IsFileIndexCompleted( IntPtr hPlay, OUT int* pbIndexCompleted);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_IsFileIndexCompleted( IntPtr hPlay, ref OUT int pbIndexCompleted);

    //    //�ļ����Ž���
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_IsFileEnded(  IntPtr hPlay, OUT int* pbFileEnded );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_IsFileEnded( IntPtr hPlay, OUT int* pbFileEnded);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_IsFileEnded( IntPtr hPlay, ref OUT int pbFileEnded);

    //    //��Ƶͼ��ߴ�
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_GetPictureSize(  IntPtr hPlay, OUT int* pnWidth, OUT int* pnHeight );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_GetPictureSize( IntPtr hPlay, OUT int* pnWidth, OUT int* pnHeight);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_GetPictureSize( IntPtr hPlay, ref OUT int pnWidth, ref OUT int pnHeight);

    //    //���������Ƶ֡��
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_GetBufferedFrameCount(  IntPtr hPlay, OUT uint* pdwBufferedFrameCount );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_GetBufferedFrameCount( IntPtr hPlay, OUT uint* pdwBufferedFrameCount);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_GetBufferedFrameCount( IntPtr hPlay, ref OUT uint pdwBufferedFrameCount);

    //    //�ļ��ܳ���
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_GetFileSize(  IntPtr hPlay, OUT long* pllFileSize );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_GetFileSize( IntPtr hPlay, OUT long* pllFileSize);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_GetFileSize( IntPtr hPlay, ref OUT long pllFileSize);

    //    //�������ݳ���
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_GetPlayDataSize(  IntPtr hPlay, OUT long* pllPlayDataSize );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_GetPlayDataSize( IntPtr hPlay, OUT long* pllPlayDataSize);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_GetPlayDataSize( IntPtr hPlay, ref OUT long pllPlayDataSize);

    //    //�ļ���ʱ��
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_GetFileDuration(  IntPtr hPlay, OUT uint* pdwFileDuration );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_GetFileDuration( IntPtr hPlay, OUT uint* pdwFileDuration);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_GetFileDuration( IntPtr hPlay, ref OUT uint pdwFileDuration);

    //    //���ŵ�ʱ��
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_GetPlayDuration(  IntPtr hPlay, OUT uint* pdwPlayDuration );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_GetPlayDuration( IntPtr hPlay, OUT uint* pdwPlayDuration);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_GetPlayDuration( IntPtr hPlay, ref OUT uint pdwPlayDuration);

    //    //�ļ�����Ƶ֡��
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_GetFileVideoFrameCount(  IntPtr hPlay, OUT uint* pdwFileVideoFrameCount );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_GetFileVideoFrameCount( IntPtr hPlay, OUT uint* pdwFileVideoFrameCount);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_GetFileVideoFrameCount( IntPtr hPlay, ref OUT uint pdwFileVideoFrameCount);

    //    //���ŵ���Ƶ֡���
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_GetPlayVideoFrameIndex(  IntPtr hPlay, OUT uint* pdwPlayVideoFrameIndex );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_GetPlayVideoFrameIndex( IntPtr hPlay, OUT uint* pdwPlayVideoFrameIndex);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_GetPlayVideoFrameIndex( IntPtr hPlay, ref OUT uint pdwPlayVideoFrameIndex);

    //    //��Ƶ�ؼ�֡������
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_GetKeyFrameDistance(  IntPtr hPlay, OUT uint* pdwKeyFrameDistance );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_GetKeyFrameDistance( IntPtr hPlay, OUT uint* pdwKeyFrameDistance);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_GetKeyFrameDistance( IntPtr hPlay, ref OUT uint pdwKeyFrameDistance);

    //    //��Ƶ�ؼ�֡ʱ����
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_GetKeyFrameInterval(  IntPtr hPlay, OUT uint* pdwKeyFrameInterval );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_GetKeyFrameInterval( IntPtr hPlay, OUT uint* pdwKeyFrameInterval);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_GetKeyFrameInterval( IntPtr hPlay, ref OUT uint pdwKeyFrameInterval);

    //    //��Ƶ֡��
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_GetVideoFrameRate(  IntPtr hPlay, OUT float* pfVideoFrameRate );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_GetVideoFrameRate( IntPtr hPlay, OUT float* pfVideoFrameRate);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_GetVideoFrameRate( IntPtr hPlay, ref OUT float pfVideoFrameRate);

    //    //��Ƶ����������
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_GetVideoBitrate(  IntPtr hPlay, OUT float* pfVideoBitrate );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_GetVideoBitrate( IntPtr hPlay, OUT float* pfVideoBitrate);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_GetVideoBitrate( IntPtr hPlay, ref OUT float pfVideoBitrate);

    //    //��Ƶ֡��
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_GetAudioFrameRate(  IntPtr hPlay, OUT float* pfAudioFrameRate );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_GetAudioFrameRate( IntPtr hPlay, OUT float* pfAudioFrameRate);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_GetAudioFrameRate( IntPtr hPlay, ref OUT float pfAudioFrameRate);

    //    //��Ƶ����������
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

    //    //�Ƴ��ӿ�
    //    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_RemoveViewport(  IntPtr hPlay,  uint dwViewportID );
    ////C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    ////ORIGINAL LINE: int WINAPI HB_PLAY2_RemoveViewport( IntPtr hPlay,  uint dwViewportID);
    ////C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //    //int HB_PLAY2_RemoveViewport( IntPtr hPlay,  uint dwViewportID);

    //    //��ȡ�ӿ�
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

    //����������
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_InputData(  IntPtr hPlay,  LPCVOID pBuffer,  uint dwBufferLength );
    //C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    //ORIGINAL LINE: int WINAPI HB_PLAY2_InputData( IntPtr hPlay,  LPCVOID pBuffer,  uint dwBufferLength);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY2_InputData( IntPtr hPlay,  LPCVOID pBuffer,  uint dwBufferLength);

    //��ȡ/�ͷŽ�������
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

    //�����ļ��Ĳ���λ��
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

    //ע���ļ�������ɻص�����
    //C++ TO C# CONVERTER TODO TASK: The original C++ function pointer contained an unconverted modifier:
    //ORIGINAL LINE: typedef void (CALLBACK* PHB_PLAY2_FILE_INDEX_COMPLETED_PROC)( IntPtr hPlay,  IntPtr  pContext);
    public delegate void PHB_PLAY2_FILE_INDEX_COMPLETED_PROC(IntPtr hPlay, IntPtr pContext);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_RegisterFileIndexCompletedCallback(  IntPtr hPlay,  PHB_PLAY2_FILE_INDEX_COMPLETED_PROC pfnCallback,  IntPtr  pContext );
    //C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    //ORIGINAL LINE: int WINAPI HB_PLAY2_RegisterFileIndexCompletedCallback( IntPtr hPlay,  PHB_PLAY2_FILE_INDEX_COMPLETED_PROC pfnCallback,  IntPtr  pContext);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY2_RegisterFileIndexCompletedCallback( IntPtr hPlay,  PHB_PLAY2_FILE_INDEX_COMPLETED_PROC pfnCallback,  IntPtr  pContext);

    //ע���ļ������ص�����
    //C++ TO C# CONVERTER TODO TASK: The original C++ function pointer contained an unconverted modifier:
    //ORIGINAL LINE: typedef void (CALLBACK* PHB_PLAY2_FILE_ENDED_PROC)( IntPtr hPlay,  IntPtr  pContext);
    public delegate void PHB_PLAY2_FILE_ENDED_PROC(IntPtr hPlay, IntPtr pContext);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_RegisterFileEndedCallback(  IntPtr hPlay,  PHB_PLAY2_FILE_ENDED_PROC pfnCallback,  IntPtr  pContext );
    //C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    //ORIGINAL LINE: int WINAPI HB_PLAY2_RegisterFileEndedCallback( IntPtr hPlay,  PHB_PLAY2_FILE_ENDED_PROC pfnCallback,  IntPtr  pContext);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY2_RegisterFileEndedCallback( IntPtr hPlay,  PHB_PLAY2_FILE_ENDED_PROC pfnCallback,  IntPtr  pContext);

    //    //ע�Ỻ�����ӽ��ջص�����
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

    //ע�����������ص�����
    //C++ TO C# CONVERTER TODO TASK: The original C++ function pointer contained an unconverted modifier:
    //ORIGINAL LINE: typedef void (CALLBACK* PHB_PLAY2_STREAM_PARSE_PROC)( IntPtr hPlay,  const HBPLAY2_FRAME* pFrame,  IntPtr  pContext);
    public delegate void PHB_PLAY2_STREAM_PARSE_PROC(IntPtr hPlay, HBPLAY2_FRAME pFrame, IntPtr pContext);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_RegisterStreamParseCallback(  IntPtr hPlay,  PHB_PLAY2_STREAM_PARSE_PROC pfnCallback,  IntPtr  pContext );
    //C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    //ORIGINAL LINE: int WINAPI HB_PLAY2_RegisterStreamParseCallback( IntPtr hPlay,  PHB_PLAY2_STREAM_PARSE_PROC pfnCallback,  IntPtr  pContext);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY2_RegisterStreamParseCallback( IntPtr hPlay,  PHB_PLAY2_STREAM_PARSE_PROC pfnCallback,  IntPtr  pContext);

    //ע�����ص�����
    //C++ TO C# CONVERTER TODO TASK: The original C++ function pointer contained an unconverted modifier:
    //ORIGINAL LINE: typedef void (CALLBACK* PHB_PLAY2_DECODE_PROC)( IntPtr hPlay,  const HBPLAY2_FRAME* pFrame,  IntPtr  pContext);
    public delegate void PHB_PLAY2_DECODE_PROC(IntPtr hPlay, HBPLAY2_FRAME pFrame, IntPtr pContext);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_RegisterDecodeCallback(  IntPtr hPlay,  uint dwPixelFormat,  PHB_PLAY2_DECODE_PROC pfnCallback,  IntPtr  pContext );
    //C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    //ORIGINAL LINE: int WINAPI HB_PLAY2_RegisterDecodeCallback( IntPtr hPlay,  uint dwPixelFormat,  PHB_PLAY2_DECODE_PROC pfnCallback,  IntPtr  pContext);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY2_RegisterDecodeCallback( IntPtr hPlay,  uint dwPixelFormat,  PHB_PLAY2_DECODE_PROC pfnCallback,  IntPtr  pContext);

    //ע��DC��ͼ�ص�����
    //C++ TO C# CONVERTER TODO TASK: The original C++ function pointer contained an unconverted modifier:
    //ORIGINAL LINE: typedef void (CALLBACK* PHB_PLAY2_DC_RENDER_PROC)( IntPtr hPlay,  IntPtr hDC,  const HBPLAY2_VIEWPORT* pViewport,  IntPtr  pContext);
    public delegate void PHB_PLAY2_DC_RENDER_PROC(IntPtr hPlay, IntPtr hDC, HBPLAY2_VIEWPORT pViewport, IntPtr pContext);
    //C++ TO C# CONVERTER WARNING: Most '__declspec' modifiers cannot be converted to C#.
    //ORIGINAL LINE: __declspec(dllexport)int WINAPI HB_PLAY2_RegisterDcRenderCallback(  IntPtr hPlay,  PHB_PLAY2_DC_RENDER_PROC pfnCallback,  IntPtr  pContext );
    //C++ TO C# CONVERTER NOTE: WINAPI is not available in C#:
    //ORIGINAL LINE: int WINAPI HB_PLAY2_RegisterDcRenderCallback( IntPtr hPlay,  PHB_PLAY2_DC_RENDER_PROC pfnCallback,  IntPtr  pContext);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_PLAY2_RegisterDcRenderCallback( IntPtr hPlay,  PHB_PLAY2_DC_RENDER_PROC pfnCallback,  IntPtr  pContext);

    //�ϲ�¼���ļ�
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
// ��Ȩ���У�2009-2012����������߿����ּ������޹�˾��
////////////////////////////////////////////////////////////////////////////////
// �ļ����� HBPlaySDK.h
// ���ߣ�   HBGK
// �汾��   3.4
// ���ڣ�   2012-08-09
// ������   ����߿ƽ����C/C++ͷ�ļ�
////////////////////////////////////////////////////////////////////////////////
//#if ! HBPLAYSDK_H
//#define HBPLAYSDK_H

//#if __cplusplus
////C++ TO C# CONVERTER TODO TASK: Extern blocks are not supported in C#.
//extern "C"
//{
//#endif


////DLL����
//#define HBDLLAPI __declspec(dllexport)


////////////////////////////////////////////////////////////////////////////////
//����HB_PLAY2 API
////////////////////////////////////////////////////////////////////////////////


//ý�岥�Ŷ���
//C++ TO C# CONVERTER TODO TASK: The following statement was not recognized, possibly due to an unrecognized macro:
//DECLARE_HANDLE(IntPtr);


//��Ƶ��ɫ
//#define HBPLAY2_VIDEO_COLOR_MIN
//#define HBPLAY2_VIDEO_COLOR_MAX
//#define HBPLAY2_VIDEO_COLOR_DEFAULT
public class HBPLAY2_COLOR_SPACE
{
    //�ṹ�峤�ȣ����ֽڣ�byte��Ϊ��λ��
    //�����߱������øó�Ա����sizeof(HBPLAY2_COLOR_SPACE)��
    public uint dwSize;

    //���ȣ�ȡֵ��Χ[HBPLAY2_VIDEO_COLOR_MIN, HBPLAY2_VIDEO_COLOR_MAX]��
    //Ĭ��ֵHBPLAY2_VIDEO_COLOR_DEFAULT��
    public uint dwBrightness;

    //�Աȶȣ�ȡֵ��Χ[HBPLAY2_VIDEO_COLOR_MIN, HBPLAY2_VIDEO_COLOR_MAX]��
    //Ĭ��ֵHBPLAY2_VIDEO_COLOR_DEFAULT��
    public uint dwContrast;

    //���Ͷȣ�ȡֵ��Χ[HBPLAY2_VIDEO_COLOR_MIN, HBPLAY2_VIDEO_COLOR_MAX]��
    //Ĭ��ֵHBPLAY2_VIDEO_COLOR_DEFAULT��
    public uint dwSaturation;

    //ɫ����ȡֵ��Χ[HBPLAY2_VIDEO_COLOR_MIN, HBPLAY2_VIDEO_COLOR_MAX]��
    //Ĭ��ֵHBPLAY2_VIDEO_COLOR_DEFAULT��
    public uint dwHue;

    //������
    public uint dwReserved;

}

//����ģʽ
public enum HBPLAY2_MODE : int
{
    HBPLAY2_MODE_UNKNOWN = 0, //δ֪��ģʽ
    HBPLAY2_MODE_FILE, //�ļ�ģʽ
    HBPLAY2_MODE_STREAM, //��ģʽ
    HBPLAY2_MODE_COUNT, //����ģʽ����
}

//����״̬
public enum HBPLAY2_STATE : int
{
    //ֹͣ��
    HBPLAY2_STATE_STOPPED = 0,

    //��ͣ��
    HBPLAY2_STATE_PAUSED,

    //�����С�
    //��HB_PLAY2_IsPlayBackward�������ڵĲ��ŷ���
    HBPLAY2_STATE_PLAYING,

    //������
    HBPLAY2_STATE_REQUIRE_TO_PAUSE,

    //������
    HBPLAY2_STATE_REQUIRE_TO_SEEK,

    //������
    HBPLAY2_STATE_SEEKING,

    //����״̬������
    HBPLAY2_STATE_COUNT,
}

//��������������
public enum HBPLAY2_DEMUX_STREAM_VERSION : int
{
    HBPLAY2_DEMUX_STREAM_UNKNOWN = 0, //δ֪������
    HBPLAY2_DEMUX_STREAM_15 = 1, //����������15����
    HBPLAY2_DEMUX_STREAM_6000 = 2, //����������6000����
    HBPLAY2_DEMUX_STREAM_V10 = 10, //��һ������
    HBPLAY2_DEMUX_STREAM_V20 = 20, //�ڶ�������
    HBPLAY2_DEMUX_STREAM_V30 = 30, //����������
}
public class HBPLAY2_DEMUX_PROPERTY
{
    //�ṹ�峤�ȣ����ֽڣ�byte��Ϊ��λ��
    //�����߱������øó�Ա����sizeof(HBPLAY2_DEMUX_PROPERTY)��
    public uint dwSize;

    //�����汾��
    public HBPLAY2_DEMUX_STREAM_VERSION StreamVersion;

    //������
    public uint[] dwReserved = new uint[2];

    //�ļ�ͷ��
    //ע�⣺ ����������������ļ�ͷ����HBPLAY2_DEMUX_STREAM_6000����û���ļ�ͷ��
    public string szFileHeader = new string(new char[64]);

}

//��������������
public class HBPLAY2_DEMUX_STATUS
{
    //�ṹ�峤�ȣ����ֽڣ�byte��Ϊ��λ��
    //�����߱������øó�Ա����sizeof(HBPLAY2_DEMUX_STATUS)��
    public uint dwSize;

    //��Ƶ֡����
    public uint dwAudioFrameCount;

    //��ƵI֡����
    public uint dwVideoIFrameCount;

    //��ƵP֡����
    public uint dwVideoPFrameCount;

    //��ƵB֡����
    public uint dwVideoBFrameCount;

    //��ƵE֡����
    public uint dwVideoEFrameCount;

    //������ʱ�䣬�Ժ��루ms��Ϊ��λ��
    public uint dwStreamDuration;

    //������
    public uint dwReserved0;

    //����������ʱ�䣬��΢�루us��Ϊ��λ��
    public long llParseDuration;

    //������
    public uint[] dwReserved1 = new uint[2];

}

//��Ƶ����������
public enum HBPLAY2_VDEC_CODEC_TYPE : int
{
    HBPLAY2_VDEC_CODEC_UNKNOWN = 0, //δ֪����������
    HBPLAY2_VDEC_CODEC_MPEG4, //MPEG4������
    HBPLAY2_VDEC_CODEC_MPEG4_ISO, //MPEG4 ISO������
    HBPLAY2_VDEC_CODEC_FFMPEG, //FFMPEG H.264������
    HBPLAY2_VDEC_CODEC_HISILICON, //Hisilicon H.264������
    HBPLAY2_VDEC_CODEC_NVIDIA_CUDA, //NVIDIA CUDA H.264������
    HBPLAY2_VDEC_CODEC_JPEG, //JPEG������
    HBPLAY2_VDEC_CODEC_AMD_OVD, //AMD_OVD H.264������
    HBPLAY2_VDEC_CODEC_COUNT, //��Ƶ����������
}
public class HBPLAY2_VDEC_STATUS
{
    //�ṹ�峤�ȣ����ֽڣ�byte��Ϊ��λ��
    //�����߱������øó�Ա����sizeof(HBPLAY2_VDEC_STATUS)��
    public uint dwSize;

    //��Ƶ�������֡����
    public uint dwFrameCount;

    //��Ƶ�������ʱ�䣬��΢�루us��Ϊ��λ��
    public long llDecodeDuration;

    //ʵʱ�����ٶȣ���֡/��Ϊ��λ��
    public uint dwRealtimeSpeed;

    //�����߳�������
    //��dwThreadCount == 0����ʾ��ǰû��ʹ��CPU���룬����ʹ��GPU���롣
    public uint dwThreadCount;

    //��Ƶ���������͡�
    public HBPLAY2_VDEC_CODEC_TYPE CodecType;

    //������
    public uint dwReserved;

}

//��Ƶ��ʾ����
public enum HBPLAY2_DISPLAY_TYPE : int
{
    HBPLAY2_DISPLAY_NONE = 0, //δʹ���ض�����ʾ��ʽ
    HBPLAY2_DISPLAY_DRAWDIB = 1, //ֱ����ʾ���豸�޹�λͼ
    HBPLAY2_DISPLAY_DIRECTDRAW_7 = 2, //ͨ��DirectDraw 7��ʾͼ��
    HBPLAY2_DISPLAY_DIRECT_3D_9 = 4, //ͨ��Direct3D 9��ʾͼ��
    HBPLAY2_DISPLAY_DIRECT_3D_11 = 8, //ͨ��Direct3D 11��ʾͼ��
}
public class HBPLAY2_DISPLAY_STATUS
{
    //�ṹ�峤�ȣ����ֽڣ�byte��Ϊ��λ��
    //�����߱������øó�Ա����sizeof(HBPLAY2_DISPLAY_STATUS)��
    public uint dwSize;

    //��ǰ����ʾ��ʽ
    public HBPLAY2_DISPLAY_TYPE DisplayType;

    //��ʾ��֡����
    public uint dwFrameCount;

    //ץͼ��֡����
    public uint dwSnapshotCount;

    //�ƻ���ʾ��ʱ�䣬��΢�루us��Ϊ��λ��
    public long llPlannedDuration;

    //ʵ����ʾ��ʱ�䣬��΢�루us��Ϊ��λ��
    public long llActualDuration;

    //ץͼ��ʱ�䣬��΢�루us��Ϊ��λ��
    public long llSnapshotDuration;

    //������
    public uint[] dwReserved = new uint[2];

}

//��Ƶ����������
public enum HBPLAY2_ADEC_CODEC_TYPE : int
{
    HBPLAY2_ADEC_CODEC_UNKNOWN = 0, //δ֪����������
    HBPLAY2_ADEC_CODEC_PCM_16_BIT, //δѹ����16λPCM
    HBPLAY2_ADEC_CODEC_PCM_LINEAR_8_BIT, //8λ����PCM����Ҫ��չ��16λ
    HBPLAY2_ADEC_CODEC_G711_ALAW, //G.711 A��
    HBPLAY2_ADEC_CODEC_G722, //G.722
    HBPLAY2_ADEC_CODEC_G726, //Hisilicon G.726 16kbps
    HBPLAY2_ADEC_CODEC_COUNT, //��Ƶ����������
}
public class HBPLAY2_ADEC_STATUS
{
    //�ṹ�峤�ȣ����ֽڣ�byte��Ϊ��λ��
    //�����߱������øó�Ա����sizeof(HBPLAY2_ADEC_STATUS)��
    public uint dwSize;

    //��Ƶ�������֡����
    public uint dwFrameCount;

    //��Ƶ�������ʱ�䣬��΢�루us��Ϊ��λ��
    public long llDecodeDuration;

    //��Ƶ���������͡�
    public HBPLAY2_ADEC_CODEC_TYPE CodecType;

    //������
    public uint dwReserved;

}

//��Ƶ��������
public class HBPLAY2_SOUND_STATUS
{
    //�ṹ�峤�ȣ����ֽڣ�byte��Ϊ��λ��
    //�����߱������øó�Ա��С��sizeof(HBPLAY2_SOUND_STATUS)��
    public uint dwSize;

    //���ű�־��
    //��־ΪFLASEʱ��ֹͣ�����������������ڲ���������
    public int bPlaying;

    //�������ݵĳ��ȣ����ֽ�Ϊ��λ��
    public uint dwBufferedDataLength;

    //�������ݵ�ʱ�䣬�Ժ���Ϊ��λ��
    public uint dwBufferedTime;

    //������
    public uint[] dwReserved = new uint[2];

}

//���ӻ��޸��ӿ�
public class HBPLAY2_VIEWPORT
{
    //�ṹ�峤�ȣ����ֽڣ�byte��Ϊ��λ��
    //�����߱������øó�Ա����sizeof(HBPLAY2_VIEWPORT)��
    public uint dwSize;

    //�ӿڱ�ʶ����
    //�ɵ�����ָ����Ψһ��־���������ֶ���ӿڡ�
    public uint dwID;

    //��ʾ���ھ����
    //��������Ч�Ĵ��ھ����
    public IntPtr hDestWnd;

    //������
    public uint[] dwReserved = new uint[3];

    //ԭʼ���ݾ��Σ��þ��β��ܴ���ԭʼͼ��ĳߴ硣
    //������Ϊ��ʱ����ͨ��IsRectEmpty�����жϷ���ֵ��TRUE������ʾ����ԭʼͼ��
    //������Ϊ�ǿ�ʱ����ʾָ�����ֵ�ͼ��һ�����ھֲ��Ŵ���ʾ��
    public Rectangle rcSrc = new Rectangle();

    //��������ʾλ�á�
    //������Ϊ��ʱ����ͨ��IsRectEmpty�����жϷ���ֵ��TRUE����ͼ����ʾ�ڴ��ڵ������ͻ�����
    //������Ϊ�ǿ�ʱ��ͼ����ʾ�ڴ�����ָ����λ�á�һ��������һ����������ʾ���ͼ��
    public Rectangle rcDest = new Rectangle();

}

//ץͼ
public class HBPLAY2_SNAP_PICTURE
{
    //�ṹ�峤�ȣ����ֽڣ�byte��Ϊ��λ��
    //�����߱������øó�Ա����sizeof(HBPLAY2_SNAP_PICTURE)��
    public uint dwSize;

    //DIB���ݻ��������ȣ����ֽ�Ϊ��λ��
    public uint dwBitsLength;

    //DIB���ݻ�����ָ�롣
    //����ͼ��ĳߴ磬�ȷ����㹻����ڴ档
    public IntPtr pBits;

    //������
    public uint dwReserved;

    //����DIB�ߴ����ɫ��Ϣ��
    public BITMAPINFOHEADER bmiHeader = new BITMAPINFOHEADER();

}
//֡����
public enum HBPLAY2_FRAME_TYPE : int
{
    HBPLAY2_FRAME_UNKNOWN, //δ֪֡����
    HBPLAY2_FRAME_AUDIO, //��Ƶ֡
    HBPLAY2_FRAME_VIDEO_I, //��ƵI֡
    HBPLAY2_FRAME_VIDEO_P, //��ƵP֡
    HBPLAY2_FRAME_VIDEO_B, //��ƵB֡
    HBPLAY2_FRAME_VIDEO_E, //��ƵE֡
    HBPLAY2_FRAME_COUNT, //֡��������
}

//���ظ�ʽ
//�� http://www.fourcc.org/yuv.php �鿴FOURCC�����YUV���ڴ����С�
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
////֡����
//{
//    HBPLAY2_FRAME_UNKNOWN, //δ֪֡����
//    HBPLAY2_FRAME_AUDIO, //��Ƶ֡
//    HBPLAY2_FRAME_VIDEO_I, //��ƵI֡
//    HBPLAY2_FRAME_VIDEO_P, //��ƵP֡
//    HBPLAY2_FRAME_VIDEO_B, //��ƵB֡
//    HBPLAY2_FRAME_VIDEO_E, //��ƵE֡
//    HBPLAY2_FRAME_COUNT, //֡��������
//}

////�ж�֡����
////C++ TO C# CONVERTER TODO TASK: The following #define macro was ignored, as specified on the Options dialog:
//#define HB_PLAY2_IsAudioFrame(FrameType) (HBPLAY2_FRAME_AUDIO == (FrameType))
////C++ TO C# CONVERTER TODO TASK: The following #define macro was ignored, as specified on the Options dialog:
//#define HB_PLAY2_IsVideoFrame(FrameType) ((FrameType) >= HBPLAY2_FRAME_VIDEO_I && (FrameType) <= HBPLAY2_FRAME_VIDEO_E)

//֡��Ϣ
public class HBPLAY2_FRAME
{
    //����
    public uint dwReserved0;

    //����
    public uint[] dwReserved1 = new uint[2];

    //֡����
    //֡����ΪHBPLAY2_FRAME_AUDIOʱ��u.Audio��Ч��
    //֡����ΪHBPLAY2_FRAME_VIDEO_*ʱ��u.Video��Ч��
    public HBPLAY2_FRAME_TYPE FrameType;

    //C++ TO C# CONVERTER TODO TASK: Unions are not supported in C#.
    //ORIGINAL LINE: union FRAME
    public struct FRAME
    {
        public class FRAME_AUDIO
        {
            //֡ͷ���ȣ����ֽ�Ϊ��λ��
            public uint dwHeaderSize;

            //֡ͷ��ַ��
            public IntPtr pHeader;

            //�������ݳ��ȣ����ֽ�Ϊ��λ��
            public uint dwDataSize;

            //�������ݵ�ַ��
            public IntPtr pData;

            //��Ƶͨ������
            public ushort wChannels;

            //�������ȡ�
            public ushort wBitsPerSample;

            //�����ʣ��Ժ��ȣ�Hz��Ϊ��λ��
            public uint dwSampleRate;

            //���뻺������
            public IntPtr pBuffer;

            //���뻺�����ĳ��ȣ����ֽ�Ϊ��λ��
            //����ʱ��˵�����������ܳ��ȡ�
            //���ʱ��˵����������ʵ��ʹ�ó��ȡ�
            public uint dwBufferLength;

            //������
            public uint[] dwReserved = new uint[4];

        }
        public FRAME_AUDIO Audio;

        public class FRAME_VIDEO
        {
            //֡ͷ���ȣ����ֽ�Ϊ��λ��
            public uint dwHeaderSize;

            //֡ͷ��ַ��
            public IntPtr pHeader;

            //�������ݳ��ȣ����ֽ�Ϊ��λ��
            public uint dwDataSize;

            //�������ݵ�ַ��
            public IntPtr pData;

            //ͼ���ȣ�������Ϊ��λ��
            public int nWidth;

            //ͼ��߶ȣ�������Ϊ��λ��
            public int nHeight;

            //���ļ���ʼ��ַ��ƫ�����������ļ�������
            public long llOffset;

            //��Ƶ֡����ʱ�䡣
            public SYSTEMTIME TimeStamp = new SYSTEMTIME();

            //֡��š�
            //֡������𽥵����ģ������ж���Ƶ֡�������ԡ�
            public uint dwIndex;

            //��Ƶ֡���ʱ�䣬�Ժ���Ϊ��λ��
            //������֮֡��Ĳ�ֵ����������֮֡���ʱ������
            public uint dwTickCount;

            //ʵ����������ظ�ʽ������Ƶ���������øø�ʽ��HBPLAY2_PIXEL_*�꣩��
            public uint dwPixelFormat;

            //���뻺������
            public IntPtr pBuffer;

            //���뻺�����ĳ��ȣ����ֽ�Ϊ��λ��
            public uint dwBufferLength;

            //������
            public uint[] dwReserved = new uint[3];

        }
        public FRAME_VIDEO Video;
    }
    public FRAME u = new FRAME();

}


////////////////////////////////////////////////////////////////////////////////
//����HB_PLAY2 API
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
//BitmapInfoHeader������λͼ��ͷ����Ϣ
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
