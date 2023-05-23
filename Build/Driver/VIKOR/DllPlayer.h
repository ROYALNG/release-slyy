// 下列 ifdef 块是创建使从 DLL 导出更简单的
// 宏的标准方法。此 DLL 中的所有文件都是用命令行上定义的 DLLPLAYER_EXPORTS
// 符号编译的。在使用此 DLL 的
// 任何其他项目上不应定义此符号。这样，源文件中包含此文件的任何其他项目都会将
// DLLPLAYER_API 函数视为是从 DLL 导入的，而此 DLL 则将用此宏定义的
// 符号视为是被导出的。

#ifndef		__DLLPLAYER_20110802_H
#define __DLLPLAYER_20110802_H

#define DLLPLAYER_API


#if DLLDECODER_EXPORTS	//只编译解码库
#define DLLPLAYER_API
#else
#ifdef DLLPLAYER_EXPORTS
#define DLLPLAYER_API __declspec(dllexport)
#else

#ifdef DLLPLAYER_IS_LIB	//使用lib版本时，全增加以下依赖库
	#define DLLPLAYER_API  
	#pragma comment(lib, "cximage.lib")
	#pragma comment(lib, "Jpeg.lib")
	#pragma comment(lib, "avutil.lib")
	#pragma comment(lib, "avcodec.lib")
	#pragma comment(lib, "avcore.lib")
	#pragma comment(lib, "dxguid.lib")
	#pragma comment(lib, "ddraw.lib")
	#pragma comment(lib, "dmoguids.lib")
	#pragma comment(lib, "strmiids.lib")
	#pragma comment(lib, "DllPlayer.lib")
#else
	#define DLLPLAYER_API  __declspec(dllimport)
	#pragma comment(lib, "DllPlayer.lib")
#endif
#endif
#endif


#define		NEEDADD_STREAM_FRAME_HEADER_FLAGHEAD		0



///播放动作1正常，2停止，3快进，4慢放，5帧进，6定点播放
enum PLAYFILE_ACTION
{
	PLAYER_ACTION_PLAY=1,
	PLAYER_ACTION_STOP,
	PLAYER_ACTION_FAST,
	PLAYER_ACTION_SLOW,
	PLAYER_ACTION_FRAMESKIP,
	PLAYER_ACTION_SEEK,
	PLAYER_ACTION_PAUSE,
	PLAYER_ACTION_RESUME,
	PLAYER_ACTION_CAPIMG=10,
	PLAYER_ACTION_CHANGE_SOUND,
	PLAYER_ACTION_RECV_DECODEPARAM,
	PLAYER_ACTION_NOSKIPFRAME_FAST,
};




//消息（LIB）/事件(COM) 代码
enum EventCode
{
	RECVDATATIMEOUT=1			//收取流时，如果10秒内都没有数据，则会发送此消息
	,FOCUSCHANGE				//播放器焦点改变时发生
	,STARTPLAY					//开始播放
	,ENTERWAITFORBUFFER			//进入缓冲
	,SETUP_VIDEO_PARAM_OK		//SetupDecoder返回成功
	,SETUP_AUDIO_PARAM_OK		//SetupDecoder返回成功
	,SNAPSHORT_FINISH			//抓图结束
	,BINDPORTERROR				//邦定端口失败
	,VSS_STOPPLAY				//播放VSS文件结束
	,VSS_STARTPLAY				//开始播放
	,VSS_PLAYNEXT				//播放下一个文件
	,VSS_SETUPERROR				//调用setup返回失败
	,CREATE_THREAD_ERROR		//创建线程失败
	,LOC_STOPPLAY				//停止本地文件播放
	,LOC_OPENFILEERROR			//本地文件播放时打开文件失败
	,LOC_PLAYERROR				//播放失败退出
	,LOC_PAUSEPLAY				//暂停播放
	,LOC_OPENFILE_ERROR			//打开本地文件失败
	,RECV_RETUN_ERROR			//RTPRECV读取数据返回少于16个字节
	,FIRSTPLAY					//第一次播放
	,RECORDEND
	,CAPTUREPICEND
	,PLAYTIMECHANGE
};



enum PLAYDLL_ERROR_CODE
{
	ERR_PLY_AUDIOPARAM_ERROR=-999991,
	ERR_PLY_VIDEOPARAM_ERROR,
	ERR_PLY_VIDEOCHANNELID_ERROR,
	ERR_PLY_NOT_DECODER_MODE,
	ERR_PLY_DECODERTHREAD_NOTSTART,
	ERR_PLY_SOUND_OFF_SKIPBUFFER,
	ERR_PLY_NOAUDIOON_ERROR,
	ERR_PLAY_NOTPLAYMODE_ERROR,
	ERR_PLY_DISPLAY_OFF_ERROR,
	ERR_PLAY_FILETYPE_ERROR,
	ERR_PLAY_AVIFILE_ERROR,
	ERR_PLAY_NOTPLAYMODE,
	ERR_PLAY_CONTROL_PARAM_ERROR,
	ERR_PLAY_CONTROLTYPE_ERROR,
	ERR_PLAYER_ISPLAYING_FILE,
	ERR_PLAYER_OPENFILEERROR,
	ERR_PLAY_STOPPLAYFIRST,
	ERR_PLAY_BUFFER_ISFULL,
	ERR_PLAY_NOT_PLAYLOCFILE_MODE,
	ERR_PLAY_NOTFIND_VIDEO_ERROR,
	ERR_PLAY_NOTREPLAY_MODE_ERROR,
	ERR_PLAY_NOPLAYING_ERROR,
};



typedef struct
{
	long nWidth;
	long nHeight;
	long nStamp;
	long nType;
	long nFrameRate;
	long bIsVideo;
	int  nLinseSize[4];
}FRAME_INFO; 


 

#define MAX_VIDEO_IN_BUFFER_SIZE		(2<<20)   //高清时差不多要2M的数据，所以这个值不能太小 


typedef int (CALLBACK* fDecCallBackFunction)(long nPort,char * pBuf,long nSize,FRAME_INFO * pFrameInfo, void * pUser,long nReserved2);
typedef LONG(CALLBACK* fStatusEventCallBack)(long nPort,LONG nStateCode,char *pResponse,void *pUser);




DLLPLAYER_API 	int			__stdcall 		IP_TPS_OpenStream(LONG nPort,PBYTE pParam,DWORD pSize,int isAudioParam,DWORD nMaxBufFrameCount);
DLLPLAYER_API 	int 		__stdcall 		IP_TPS_Play(LONG nPort, HWND hWnd);
DLLPLAYER_API 	int 		__stdcall 		IP_TPS_PlaySound(LONG nPort);
DLLPLAYER_API 	int 		__stdcall 		IP_TPS_InputAudioData(LONG nPort,PBYTE pBuf,DWORD nSize,DWORD timestamp);
DLLPLAYER_API 	int 		__stdcall 		IP_TPS_InputVideoData(LONG nPort,PBYTE pBuf,DWORD nSize,int isKey,DWORD timestamp);
DLLPLAYER_API 	int 		__stdcall 		IP_TPS_CatchPic(LONG Port,char* sDirName);
DLLPLAYER_API 	int 		__stdcall 		IP_TPS_CatchPicByFileName(LONG nPort,char* sFileName,int isJpg);
DLLPLAYER_API 	int 		__stdcall 		IP_TPS_StopSound();
DLLPLAYER_API 	int 		__stdcall 		IP_TPS_Stop(LONG nPort);
DLLPLAYER_API 	int 		__stdcall 		IP_TPS_CloseStream(LONG nPort);
DLLPLAYER_API 	int 		__stdcall 		IP_TPS_DeleteStream(LONG nPort);
DLLPLAYER_API 	int 		__stdcall 		IP_TPS_CloseAll();
DLLPLAYER_API 	int 		__stdcall 		IP_TPS_ReleaseAll();
DLLPLAYER_API 	int			__stdcall		IP_TPS_SetDecCallBack(LONG nPort,fDecCallBackFunction func,void * pUser);
DLLPLAYER_API 	LONG 		__stdcall 		IP_TPS_GetVersion();
DLLPLAYER_API	LONG		__stdcall		IP_TPS_SetStatusEventCallBack(LONG nPort,fStatusEventCallBack func,void * pUser);


#if !DLLDECODER_EXPORTS
DLLPLAYER_API	int			__stdcall		IP_TPS_PlayLocFile(LONG nPort,HWND hWnd,const char * filename,int fileType);
DLLPLAYER_API	int			__stdcall		IP_TPS_StopPlayLocFile(LONG nPort);
DLLPLAYER_API	int			__stdcall		IP_TPS_GetPlayTime(LONG nPort,int * time);
DLLPLAYER_API	int			__stdcall		IP_TPS_GetFileTime(LONG nPort,int * time);
DLLPLAYER_API	int			__stdcall		IP_TPS_ControlPlay(LONG nPort,int nAction,int param);

#endif



//传入鼠标动作，以处理，注意不要传双击事件
DLLPLAYER_API	int			__stdcall		IP_TPS_InputMouseEvent(LONG nPort,LONG nMsgType,WPARAM wp,LPARAM lp);
//设置缩放类型
DLLPLAYER_API	int			__stdcall		IP_TPS_SetZoomRectOn(LONG nPort,LONG nType);
//取当前状态，是否启用电子缩放
DLLPLAYER_API	int			__stdcall		IP_TPS_GetZoomRectStatus(LONG nPort);
DLLPLAYER_API	int			__stdcall		IP_TPS_SetFullFillStatus(LONG nPort,int bIsFullFill);
DLLPLAYER_API	int			__stdcall		IP_TPS_GetFullFillStatus(LONG nPort);
//设置视频是否播放
DLLPLAYER_API 	int 		__stdcall 		IP_TPS_SetVideoOn(LONG nPort,int bIsOn);
DLLPLAYER_API	int			__stdcall		IP_TPS_SwitchVideo(LONG from,LONG to);

DLLPLAYER_API	int			__stdcall		IP_TPS_SetContrast(LONG nPort,int nBrightness,int nContrast,int bIsEnable);
DLLPLAYER_API	int			__stdcall		IP_TPS_SetGamma(LONG nPort,int nGammaValue,int bIsEnable);

DLLPLAYER_API	int			__stdcall		IP_TPS_GetBufferCount(LONG nPort,LONG * pRetCount);
DLLPLAYER_API	int			__stdcall		IP_TPS_ClearBuffer(LONG nPort);



DLLPLAYER_API	int			__stdcall		IP_TPS_SetShowTitle(int nPort,char * TitleMsg, int x, int y , int bNeedShow);
 


DLLPLAYER_API	LONG		__stdcall		IP_TPS_SetLogToFile(DWORD bLogEnable,char *strLogDir,BOOL bAutoDel);

 


#endif
