//显示模式
#if ! DISPLAY_MODE_defined
#define DISPLAY_MODE_defined
//发送模式
#if ! SEND_MODE_defined
#define SEND_MODE_defined
using System;
using System.Runtime.InteropServices;
public class HBSDVRSDK
{
    public static bool bInit = false;
    public const string SDK_HB = @".\Driver\hanbang\7000sdk.dll";
    /////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////
    // 接口函数定义
    //3. 登陆以及返回设备信息
    //SDK初始化 

    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_Init();

    //SDK释放

    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_Cleanup();

    //报警信息接收窗口
    //
    //功  能：设置接收两种消息的句柄和方式 （回调或者向线程发消息）  
    //参  数：
    //nMessage：COMM_ALARM(报警信息)或COMM_CONNECT(连接状态) 
    //		hWnd：句柄(可以是窗口，可以是线程句柄，建议使用线程以防止阻塞) 
    //		返回值：TRUE-成功	FASLE-失败	

    // 注意使用HB_SDVR_SetDVRMessage，HB_SDVR_SetDVRMessage_Nvs时最好不要同时使用，同时使用时注意使用不同的窗口句柄，否则无法区分
    // 使用HB_SDVR_SetDVRMessage时请用HB_SDVR_ALARMINFO对LPARAM进行解析
    // 使用HB_SDVR_SetDVRMessage_Nvs时请用HB_SDVR_ALARMINFO_EX对LPARAM进行解析
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetDVRMessage(uint nMessage, IntPtr hWnd);
    //扩展接口，为了兼容最多128路机器。by cui 10.05.20
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetDVRMessage_Nvs(uint nMessage, IntPtr hWnd);
    public delegate int fMessCallBackDelegate(int lCommand, ref string sDVRIP, ref string pBuf, uint dwBufLen);

    //设置报警信息回调-基于窗口消息
    //
    //功  能：设置接收消息的回调函数 （IP区分） 
    //参  数：
    //#define COMM_ALARM				0x1100	//报警信息
    //#define COMM_CONNECT				0x1200	//主机网络断开
    //fMessCallBack： 消息回调函数
    //lCommand： 消息的类型 例COMM_ALARM
    //sDVRIP： ip地址
    //pBuf：存放信息的缓冲区
    //dwBufLen：缓冲区的大小
    //返回值：TRUE-成功	FASLE-失败
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetDVRMessCallBack(fMessCallBackDelegate fMessCallBack);
    //public delegate int fMessCallBackDelegate(int lCommand, ref string sDVRIP, ref string pBuf, uint dwBufLen);
    //兼容128路接口
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetDVRMessCallBack_Nvs(fMessCallBackDelegate fMessCallBack);
    //public delegate int fMessCallBack_EXDelegate(int lCommand, int lUserID, ref string pBuf, uint dwBufLen);


    //设置报警信息回调-基于线程消息
    //
    //功  能：设置接收消息的回调函数 （ID区分） 
    //参  数：
    //#define COMM_ALARM				0x1100	//报警信息
    //#define COMM_CONNECT				0x1200	//主机网络断开
    //fMessCallBack： 消息回调函数
    //lCommand： 消息的类型 例COMM_ALARM
    //lUserID ：由HB_SDVR _Login 返回
    //pBuf：存放信息的缓冲区
    //dwBufLen：缓冲区的大小
    //返回值：TRUE-成功	FASLE-失败
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetDVRMessCallBack_EX(fMessCallBack_EXDelegate fMessCallBack_EX);
    public delegate int fMessCallBack_EXDelegate(int lCommand, int lUserID, ref string pBuf, uint dwBufLen);
    //兼容128路接口
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetDVRMessCallBack_EX_Nvs(fMessCallBack_EXDelegate fMessCallBack_EX);
    //public delegate int fMessCallBackDelegate(int lCommand, ref string sDVRIP, int lUserID, ref string pBuf, uint dwBufLen, uint dwUser);

    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetDEVMessCallBack(int lUserID, fMessCallBackDelegate fMessCallBack, uint dwUser);


    //设置连接超时时间
    //DWORD dwTryTimes保留
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetConnectTime(uint dwWaitTime, uint dwTryTimes);

    //SDK版本
    [DllImport(SDK_HB)]
    public static extern uint HB_SDVR_GetSDKVersion();

    //系统是否支持
    //返回：成功-0xFF   失败-0
    //[DllImport(SDK_HB)]
    //public static extern int HB_SDVR_IsSupport();
    //[DllImport(SDK_HB)]
    //public static extern int HB_SDVR_Login(ref string sDVRIP, ushort wDVRPort, ref string sUserName, ref string sPassword, LPHB_SDVR_DEVICEINFO lpDeviceInfo);
    //[DllImport(SDK_HB)]
    //public static extern int HB_SDVR_LoginA(ref string sDVRIP, ushort wDVRPort, ref string sUserName, ref string sPassword, LPHB_SDVR_DEVICEINFO lpDeviceInfo);
    //[DllImport(SDK_HB)]
    //public static extern int HB_SDVR_LoginW(ref string sDVRIP, ushort wDVRPort, ref string sUserName, ref string sPassword, LPHB_SDVR_DEVICEINFO lpDeviceInfo);
    //    //为兼容最多128路机器类型的扩展接口 10.05.25
    //[DllImport(SDK_HB)]
    //public static extern int HB_SDVR_Login_Nvs(ref string sDVRIP, ushort wDVRPort, ref string sUserName, ref string sPassword, HB_SDVR_DEVICEINFO_EX lpDeviceInfo);
    //    //*********************************************************************************
    //    //函数名：HB_SDVR_Login_Ex
    //    //功  能:登录扩展函数，可以使用户多次登录同一个dvr。
    //    //其  他：用法同HB_SDVR_Login
    //    //**********************************************************************************
    //[DllImport(SDK_HB)]
    //public static extern int HB_SDVR_Login_Ex(ref string sDVRIP, ushort wDVRPort, ref string sUserName, ref string sPassword, LPHB_SDVR_DEVICEINFO lpDeviceInfo);
    //可以使用户多次登录并且兼容最多128路机器类型的扩展接口 10.05.25
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_Login_Ex_Nvs(string sDVRIP, ushort wDVRPort, string sUserName, string sPassword, out HB_SDVR_DEVICEINFO_EX lpDeviceInfo);

    //4. 注销
    //
    //功  能：注销
    //参  数：
    //sDVRIP： IP地址用户ID 值，由HB_SDVR _Login 返回
    //返回值：TRUE-成功	FASLE-失败
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_Logout(int lUserID);


    //5. 实时视频
    //
    //功  能：开启远程视频
    //参  数：
    //lDVRIP： IP地址用户ID 值，由NET_DVR_Login 返回
    //lWindows：通道号
    //lpClientInfo：指向HB_SDVR_CLIENTINFO结构的指针
    //返回值：FASLE-失败 ，其他值表示返回视频流的ID值，该ID值是由SDK分配，作为HB_SDVR _StopRealPlay等函数的参数
    //

    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_RealPlay(int lUserID, int lWindows, ref HB_SDVR_CLIENTINFO lpClientInfo);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_UpdataRealPlayWnd(int lRealHandle, IntPtr hPlayWnd);
    //6. 关闭视频
    //
    //功  能：关闭远程视频
    //参  数：
    //lRealHandle： 视频流的ID值，由HB_SDVR_RealPlay返回
    //返回值：TRUE-成功	FASLE-失败

    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_StopRealPlay(int lRealHandle);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_StopRealPlayEx(int lRealHandle);

    //7. 视频参数
    //
    //功  能：调整视频参数
    //参  数：
    //lRealHandle： 视频流的ID值，由HB_SDVR_RealPlay返回
    //videoeff：指向HB_SDVR_VIDEOEFFECT结构的指针
    //返回值：TRUE-成功	FASLE-失败

    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_ClientSetVideoEffect(int lRealHandle, HB_SDVR_VIDEOEFFECT videoeff);

    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_ClientGetVideoEffect(int lRealHandle, HB_SDVR_VIDEOEFFECT videoeff);
    //参  数：
    //lUserID： sDVRIP： IP地址用户ID 值，由HB_SDVR _Login 返回
    //lChannel：通道号
    //返回值：TRUE-成功	FASLE-失败
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_ClientSetVideoEffect_Ex(int lUserID, int lChannel, HB_SDVR_VIDEOEFFECT videoeff);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_ClientGetVideoEffect_Ex(int lUserID, int lChannel, HB_SDVR_VIDEOEFFECT videoeff);


    //8. 请求关键帧
    //
    //功  能：实时视频中产生关键帧
    //参  数：
    //lUserID： sDVRIP： IP地址用户ID 值，由HB_SDVR _Login 返回
    //lChannel：通道号
    //返回值：TRUE-成功	FASLE-失败
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_MakeKeyFrame(int lUserID, int lChannel);

    //9. 云台控制(透明云台) 
    //功  能：云台控制命令（默认速度）根据视频流ID来控制
    //参  数：
    //lRealHandle： 视频流的ID值，由HB_SDVR_RealPlay返回
    //dwPTZCommand：控制命令
    //#define TM_COM_GUI_BRUSH   0x0001002e   //雨刷
    //#define TILT_UP			0x0001000c	/* 云台以SS的速度上仰 */
    //#define TILT_DOWN		0x0001000d	/* 云台以SS的速度下俯 */
    //#define PAN_LEFT		0x0001000e 	/* 云台以SS的速度左转 */
    //#define PAN_RIGHT		0x0001000f	/* 云台以SS的速度右转 */
    //#define ZOOM_IN			0x00010016	/* 焦距以速度SS变大(倍率变大) */
    //#define ZOOM_OUT		0x00010017 	/* 焦距以速度SS变小(倍率变小) */
    //#define IRIS_OPEN		0x00010018 	/* 光圈以速度SS扩大 */
    //#define IRIS_CLOSE		0x00010019	/* 光圈以速度SS缩小 */
    //#define FOCUS_FAR		0x00010015 	/* 焦点以速度SS后调 */
    //#define FOCUS_NEAR		0x00010014  /* 焦点以速度SS前调 */
    //#define LIGHT_PWRON		0x00010024	/* 接通灯光电源 */
    //#define WIPER_PWRON		0x00010025	/* 接通雨刷开关 */
    //#define PAN_AUTO		0x0001001c 	/* 云台以SS的速度左右自动扫描 */
    //dwStop：让云台停止还是开始
    //返回值：TRUE-成功	FASLE-失败
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_PTZControl(int lRealHandle, uint dwPTZCommand, uint dwStop);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_PTZControl_Other(int lUserID, int lChannel, uint dwPTZCommand, uint dwStop);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_PTZControlWithSpeed(int lRealHandle, uint dwPTZCommand, uint dwStop, uint dwSpeed);
    //透明云台
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_TransPTZ(int lRealHandle, ref string pPTZCodeBuf, uint dwBufSize);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_TransPTZ_Other(int lUserID, int lChannel, ref string pPTZCodeBuf, uint dwBufSize);

    //10.主机控制
    //重启
    //
    //功  能：远程重启主机 
    //参  数：
    //lUserID：  IP地址用户ID 值，由HB_SDVR _Login 返回
    //返回值： FASLE-失败 TRUE-成功
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_RebootDVR(int lUserID);

    //关闭DVR
    //
    //功  能：远程关闭主机 
    //参  数：
    //lUserID：  IP地址用户ID 值，由HB_SDVR _Login 返回
    //返回值： FASLE-失败 TRUE-成功
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_ShutDownDVR(int lUserID);


    //11.升级控制
    //
    //功  能：远程升级主机 
    //参  数：
    //lUserID：  IP地址用户ID 值，由HB_SDVR _Login 返回
    //sFileName：升级的文件名及路径
    //返回值： FASLE-失败  其他返回升级的ID
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_Upgrade(int lUserID, ref string sFileName);

    //
    //功  能：远程升级主机状态 
    //参  数：
    //lUpgradeHandle： 升级的ID，由HB_SDVR_Upgrade返回
    //返回值： FASLE-失败 
    //其他值：
    //-1 版本不对，升级失败
    //100 数据传输完成，等待主机更新  
    //101 主机更新成功
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_GetUpgradeState(int lUpgradeHandle);

    //升级不允许中断 此接口保留
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_CloseUpgradeHandle(int lUpgradeHandle);
    //12.对讲操作 
    //语音对讲
    //
    //功  能：开始语音对讲
    //参  数：
    //lUserID：NET_DVR_Login ()的返回值
    //fVoiceDataCallBack：回调函数，回调音频数据
    //dwUser：用户数据
    //回调函数说明：
    //lVoiceComHandle：HB_SDVR_StartVoiceCom ()的返回值
    //pRecvDataBuffer：存放数据的缓冲区指针
    //dwBufSize：缓冲区的大小
    //byAudioFlag：数据类型
    //0- 客户端采集的音频数据
    //1- 客户端收到设备端的音频数据
    //dwUser：用户数据，就是上面输入的用户数据
    //返回值：-1表示失败，其他值作为HB_SDVR _SetVoiceComClientVolume ()等函数的参数 
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_StartVoiceCom(int lUserID, fVoiceDataCallBackDelegate fVoiceDataCallBack, uint dwUser);


    //
    //功  能：设置语音对讲PC 端的音量
    //参  数：
    //lVoiceComHandle：HB_SDVR _StartVoiceCom 的返回值
    //wVolume：设置后的音量，从0-0xffff
    //返回值：TRUE 表示成功，FALSE 表示失败。
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetVoiceComClientVolume(int lVoiceComHandle, ushort wVolume);

    //
    //功  能：结束对讲
    //参  数：
    //lVoiceComHandle：HB_SDVR _StartVoiceCom 的返回值
    //返回值：TRUE 表示成功，FALSE 表示失败
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_StopVoiceCom(int lVoiceComHandle);

    //开启语音广播
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_ClientAudioStart();
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_ClientAudioStop();
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_AddDVR(int lUserID);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_DelDVR(int lUserID);
    public delegate void fSerialDataCallBackDelegate(int lSerialHandle, ref string pRecvDataBuffer, uint dwBufSize, uint dwUser);

    //13.透明通道
    ////*特别注意 开启485通道的时候一定要在232上channel=2设置一下才可以
    //功  能：建立透明通道
    //参  数： 
    //lUserID：NET_DVR_Login ()的返回值
    //lSerialPort：串口号，1-232 串口，2-485 串口
    //dwUser：用户数据
    //fSerialDataCallBack：回调函数
    //回调函数说明：
    //lSerialHandle：NET_DVR_SerialStart()的返回值
    //pRecvDataBuffer：存放接收到数据的缓冲区指针
    //dwBufSize：缓冲区的大小
    //dwUser：上面的用户数据
    //返回值：-1表示失败，其他值作为HB_SDVR_SerialSend()等函数的参数
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SerialStart(int lUserID, int lSerialPort, fSerialDataCallBackDelegate fSerialDataCallBack, uint dwUser);

    //
    //功  能：通过透明通道向DVR 串口发送数据
    //参  数：lSerialHandle：NET_DVR_SerialStart 的返回值
    //lChannel：硬盘录像机的通道号, 以485 建立透明通道时有效,指明往哪个通道送数据,以232 建立透明通道时设置成0;
    //pSendBuf：要发送的缓冲区的指针
    //dwBufSize：缓冲区的大小
    //返回值：TRUE 表示成功，FALSE 表示失败
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SerialSend(int lSerialHandle, int lChannel, ref string pSendBuf, uint dwBufSize);

    //
    //功能：断开透明通道
    //参数说明：
    //lSerialHandle：NET_DVR_SerialStart 的返回值
    //返回值：TRUE 表示成功，FALSE 表示失败。
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SerialStop(int lSerialHandle);

    //14.网络键盘
    //
    //功  能：网络键盘
    //参  数：
    //lUserID：  用户ID 值，由HB_SDVR _Login 返回
    //lKeyIndex：键值
    /*#define TM_COM_GUI_LOGIN
	#define TM_COM_GUI_RECORD
	#define TM_COM_GUI_PLAYBACK
	#define TM_COM_GUI_SETUP
	#define TM_COM_GUI_BACKUP
	#define TM_COM_GUI_DN
	#define TM_COM_GUI_CN
	#define TM_COM_GUI_BKSPACE
	#define TM_COM_GUI_UP
	#define TM_COM_GUI_DOWN
	#define TM_COM_GUI_LEFT
	#define TM_COM_GUI_RIGHT
	#define TM_COM_GUI_PAGEUP
	#define TM_COM_GUI_PAGEDOWN
	#define TM_COM_GUI_RETURN
	#define TM_COM_GUI_ENTER
	#define TM_COM_GUI_NEAR
	#define TM_COM_GUI_FAR
	#define TM_COM_GUI_ZOOMIN
	#define TM_COM_GUI_ZOOMOUT
	#define TM_COM_GUI_APER_INC
	#define TM_COM_GUI_APER_DEC
	#define TM_COM_GUI_PRESET_SET
	#define TM_COM_GUI_PRESET_GET
	#define TM_COM_GUI_AUTO
	#define TM_COM_GUI_MUTE
	#define TM_COM_GUI_INFO
	#define TM_COM_GUI_STEP
	#define TM_COM_GUI_PLAY
	#define TM_COM_GUI_FASTF
	#define TM_COM_GUI_FASTB
	#define TM_COM_GUI_CLRALT
	#define TM_COM_GUI_F1
	#define TM_COM_GUI_F2
	#define TM_COM_GUI_IGEFORMAT
	#define TM_COM_GUI_IGESWITCH
	#define TM_COM_GUI_STOP
	#define TM_COM_GUI_SHUTDOWN
	#define TM_COM_GUI_PWDRST
	#define TM_COM_GUI_F1VGA2TV
	#define TM_COM_GUI_MOUSESTATUS
	#define TM_COM_GUI_VIDEOPARAM
	#define TM_COM_GUI_DIGIT
	#define TM_COM_GUI_CHAR*/
    public const int TM_COM_GUI_LOGIN = 0x00010000;
    public const int TM_COM_GUI_RECORD = 0x00010001;
    public const int TM_COM_GUI_PLAYBACK = 0x00010002;
    public const int TM_COM_GUI_SETUP = 0x00010003;
    public const int TM_COM_GUI_BACKUP = 0x00010004;
    public const int TM_COM_GUI_DN = 0x00010007;
    public const int TM_COM_GUI_CN = 0x00010008;
    public const int TM_COM_GUI_BKSPACE = 0x0001000b;
    public const int TM_COM_GUI_UP = 0x0001000c;
    public const int TM_COM_GUI_DOWN = 0x0001000d;
    public const int TM_COM_GUI_LEFT = 0x0001000e;
    public const int TM_COM_GUI_RIGHT = 0x0001000f;
    public const int TM_COM_GUI_PAGEUP = 0x00010010;
    public const int TM_COM_GUI_PAGEDOWN = 0x00010011;
    public const int TM_COM_GUI_RETURN = 0x00010012;
    public const int TM_COM_GUI_ENTER = 0x00010013;
    public const int TM_COM_GUI_NEAR = 0x00010014;
    public const int TM_COM_GUI_FAR = 0x00010015;
    public const int TM_COM_GUI_ZOOMIN = 0x00010016;
    public const int TM_COM_GUI_ZOOMOUT = 0x00010017;
    public const int TM_COM_GUI_APER_INC = 0x00010018;
    public const int TM_COM_GUI_APER_DEC = 0x00010019;
    public const int TM_COM_GUI_PRESET_SET = 0x0001001a;
    public const int TM_COM_GUI_PRESET_GET = 0x0001001b;
    public const int TM_COM_GUI_AUTO = 0x0001001c;
    public const int TM_COM_GUI_MUTE = 0x0001001d;
    public const int TM_COM_GUI_INFO = 0x0001001e;
    public const int TM_COM_GUI_STEP = 0x0001001f;
    public const int TM_COM_GUI_PLAY = 0x00010020;
    public const int TM_COM_GUI_FASTF = 0x00010021;
    public const int TM_COM_GUI_FASTB = 0x00010022;
    public const int TM_COM_GUI_CLRALT = 0x00010023;
    public const int TM_COM_GUI_F1 = 0x00010024;
    public const int TM_COM_GUI_F2 = 0x00010025;
    public const int TM_COM_GUI_IGEFORMAT = 0x00010026;
    public const int TM_COM_GUI_IGESWITCH = 0x00010027;
    public const int TM_COM_GUI_STOP = 0x00010028;
    public const int TM_COM_GUI_SHUTDOWN = 0x00010029;
    public const int TM_COM_GUI_PWDRST = 0x0001002a;
    public const int TM_COM_GUI_F1VGA2TV = 0x0001002b;
    public const int TM_COM_GUI_MOUSESTATUS = 0x0001002c;
    public const int TM_COM_GUI_VIDEOPARAM = 0x0001002d;
    public const int TM_COM_GUI_DIGIT = 0x00011000;
    public const int TM_COM_GUI_CHAR = 0x00012000;
    //
    //返回值：：FALSE 表示失败，TRUE表示成功
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_ClickKey(int lUserID, int lKeyIndex);

    //15.手动录像
    //
    //功  能：获取远程手动录像
    //参  数：
    //lUserID：   用户ID 值，由HB_SDVR _Login 返回
    //lChannel：按照低位到高位的顺序表示通道号
    //lRecordType：保留
    //返回值：：FALSE 表示失败，TRUE表示成功
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_GetDVRRecord(int lUserID, ref ushort lChannel, int lRecordType);

    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_GetDVRRecord_Nvs(int lUserID, HB_SDVR_REMOTERECORDCHAN lChannel, int lRecordType);

    //
    //功  能：设置远程手动录像
    //参  数：
    //lUserID：  IP地址用户ID 值，由HB_SDVR _Login 返回
    //lChannel：按照低位到高位的顺序表示通道号
    //返回值：：FALSE 表示失败，TRUE表示成功
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetDVRRecord(int lUserID, ushort lChannel);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetDVRRecord_Nvs(int lUserID, HB_SDVR_REMOTERECORDCHAN lChannel);

    //16.设备类型
    //返回：7004  8004  2004机器型号

    //
    // typedef struct {
    // 	ULONG  dvrtype;    //7004  8004  2004机器型号
    // 	ULONG  nNULL1;         
    // 	ULONG  nNULL2;
    //  }HB_SDVR_INFO,*LPHB_SDVR_INFO; 


    //超宇版本

    //[DllImport(SDK_HB)]
    //public static extern int HB_SDVR_GetDeviceType(int lUserID, LPHB_SDVR_INFO device);
    //[DllImport(SDK_HB)]
    //public static extern int HB_SDVR_SetDeviceType(int lUserID, LPHB_SDVR_INFO device);
    //17.设备硬件信息
    //18.设备网络信息
    //19.通道参数(包括报警录像,移动录像等)
    //20.编码压缩参数
    //21.录像参数
    //22.解码器参数(云台参数)
    //23.报警输入
    //24.报警输出
    //25.用户权限
    //26.DNS
    //27.PPPoE
    //28.平台信息
    //DVR设备参数
    //32.报警输出状态




    /////////////////////////////////////////////////////////////////////////////////////
    //
    //功  能：获取远程主机配置
    //参  数：
    //lUserID：  IP地址用户ID 值，由HB_SDVR _Login 返回
    //dwCommand：命令
    //HB_SDVR_SET_DEVICECFG:   设备硬件信息
    //HB_SDVR_SET_NETCFG:		 设备网络信息
    //HB_SDVR_SET_PICCFG_EX:	 通道参数(包括报警录像,移动录像等)	 
    //HB_SDVR_SET_COMPRESSCFG: 编码压缩参数
    //HB_SDVR_SET_RECORDCFG:	 录像参数
    //HB_SDVR_SET_DECODERCFG:  解码器参数(云台参数)
    //HB_SDVR_SET_RS232CFG:    RS232
    //HB_SDVR_SET_ALARMINCFG:	 报警输入
    //HB_SDVR_SET_ALARMOUTCFG: 报警输出	
    //HB_SDVR_SET_TIMECFG:     时间
    //HB_SDVR_SET_USERCFG:	 用户权限
    //HB_SDVR_SET_DNS:         DNS
    //HB_SDVR_SET_PPPoE:		 PPPoE	
    //HB_SDVR_SERVERCFG_SET:   平台信息
    //lChannel：通道
    //lpOutBuffer：缓冲区数据
    //dwInBufferSize：缓冲区数据大小
    //lpBytesReturned：保留
    //type：保留
    //返回值：：FALSE 表示失败，TRUE表示成功
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_GetDVRConfig(int lUserID, uint dwCommand, int lChannel, IntPtr lpOutBuffer, uint dwOutBufferSize, ref uint lpBytesReturned, uint type);

    //
    //功  能：远程主机配置
    //参  数：
    //lUserID：  IP地址用户ID 值，由HB_SDVR _Login 返回
    //dwCommand：命令
    //HB_SDVR_SET_DEVICECFG:
    //HB_SDVR_SET_NETCFG:
    //HB_SDVR_SET_PICCFG_EX:
    //HB_SDVR_SET_COMPRESSCFG:
    //HB_SDVR_SET_RECORDCFG:
    //HB_SDVR_SET_DECODERCFG:
    //HB_SDVR_SET_RS232CFG:
    //HB_SDVR_SET_ALARMINCFG:
    //HB_SDVR_SET_ALARMOUTCFG:
    //HB_SDVR_SET_TIMECFG:
    //HB_SDVR_SET_USERCFG:
    //HB_SDVR_SET_DNS:
    //HB_SDVR_SET_PPPoE:
    //HB_SDVR_SERVERCFG_SET:
    //lChannel：通道
    //lpInBuffer：缓冲区数据
    //dwInBufferSize：缓冲区数据大小
    //返回值：：FALSE 表示失败，TRUE表示成功
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetDVRConfig(int lUserID, uint dwCommand, int lChannel, IntPtr lpInBuffer, uint dwInBufferSize);

    //29.串口信息
    //30.远程录像文件查询和点播  下载
    //
    //功  能：远程文件回放
    //参  数：
    //lUserID：  IP地址用户ID 值，由HB_SDVR _Login 返回
    //lChannel：通道号
    //dwPTZCommand：控制命令
    //dwStop：让云台停止还是开始
    //dwSpeed：速度
    //返回值：：FALSE 表示失败，其他值作为HB_SDVR _FindClose 等函数的参数
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_FindFile(int lUserID, int lChannel, uint dwFileType, HB_SDVR_TIME lpStartTime, HB_SDVR_TIME lpStopTime);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_FindFile_ByCard(int lUserID, ref string lpcard, uint dwFileType, HB_SDVR_TIME lpStartTime, HB_SDVR_TIME lpStopTime);

    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_FindFileWithChl(int lUserID, int lChannel, uint dwFileType, ref HB_SDVR_TIME lpStartTime, ref HB_SDVR_TIME lpStopTime);
    //
    //功  能：获取远程文件的信息
    //参  数：
    //lRealHandle： 视频流的ID值，由HB_SDVR_RealPlay返回 
    //lpFindData： 指向HB_SDVR_FIND_DATA的结构体的指针
    //返回值：返回下列值
    //#define HB_SDVR_FILE_SUCCESS				1000	//获得文件信息
    //#define HB_SDVR_FILE_NOFIND					1001	//没有文件
    //#define HB_SDVR_ISFINDING					1002	//正在查找文件
    //#define	HB_SDVR_NOMOREFILE	 1003	//查找文件时没有更多的文件
    //#define	HB_SDVR_FILE_EXCEPTION				1004	//查找文件时异常
    //录像类型

    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_FindNextFile(int lFindHandle, out HB_SDVR_FIND_DATA lpFindData);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_FindNextFile_ByCard(int lFindHandle, HB_SDVR_FIND_DATA lpFindData);

    //
    //功  能：结束获取远程文件的信息
    //参  数：
    //lFindHandle： 文件流的ID值，由HB_SDVR_FindFile返回 
    //返回值：TRUE-成功	FASLE-失败
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_FindClose(int lFindHandle);

    //
    //功  能：根据文件名回放
    //参  数：
    //lUserID：  IP地址用户ID 值，由HB_SDVR _Login 返回
    //Channel：通道号
    //sPlayBackFileName： 要保存的文件名及路径
    //hWnd：显示的窗口句柄
    //返回值：FASLE-失败 ，其他值表示返回视频流的ID值，该ID值是由SDK分配 
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_PlayBackByName(int lUserID, string sPlayBackFileName, IntPtr hWnd);

    //
    //功  能：根据时间段回放
    //参  数：
    //lUserID：  IP地址用户ID 值，由HB_SDVR _Login 返回
    //Channel：通道号
    //lpStartTime： 开始时间
    //lpStopTime：结束时间
    //hWnd：显示的窗口句柄
    //返回值：FASLE-失败 ，其他值表示返回视频流的ID值，该ID值是由SDK分配 

    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_PlayBackByTime(int lUserID, int lChannel, ref HB_SDVR_TIME lpStartTime, ref HB_SDVR_TIME lpStopTime, IntPtr hWnd);
    //add by cui for feilong 08.12.19
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_PlayBackByTime_ex(int lUserID, int lChannel, ref HB_SDVR_TIME lpStartTime, ref HB_SDVR_TIME lpStopTime, IntPtr hWnd);
    //add by xuyao for ATM 2010
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_PlayBackByTime_Name(int lUserID, int lChannel, ref HB_SDVR_TIME lpStartTime, ref HB_SDVR_TIME lpStopTime, IntPtr hWnd, IntPtr pName);

    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_PlayBackByTimeWithChl(int lUserID, int lChannel, ref HB_SDVR_TIME lpStartTime, ref HB_SDVR_TIME lpStopTime, IntPtr hWnd);

    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_PlayBackByTimeWithChl_ex(int lUserID, int lChannel, ref HB_SDVR_TIME lpStartTime, ref HB_SDVR_TIME lpStopTime, IntPtr hWnd);
    //
    //功  能：回放控制
    //参  数：
    //lRealHandle： 视频流的ID值，由HB_SDVR_RealPlay返回 
    //dwControlCode：控制命令
    //#define HB_SDVR_PLAYSTART		1//开始播放
    //#define HB_SDVR_PLAYSTOP		2//停止播放
    //#define HB_SDVR_PLAYPAUSE		3//暂停播放
    //#define HB_SDVR_PLAYRESTART		4//恢复播放
    //#define HB_SDVR_PLAYFAST		5//快放
    //#define HB_SDVR_PLAYSLOW		6//慢放
    //#define HB_SDVR_PLAYNORMAL		7//正常速度
    //#define HB_SDVR_PLAYFRAME		8//单帧放
    //#define HB_SDVR_PLAYSTARTAUDIO		9//打开声音
    //#define HB_SDVR_PLAYSTOPAUDIO		10//关闭声音
    //dwInValue：HB_SDVR_PLAYFAST和HB_SDVR_PLAYSLOW需要速度倍数  范围0-3
    //lpOutValue：保留
    //返回值：TRUE-成功	FASLE-失败
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_PlayBackControl(int lPlayHandle, uint dwControlCode, uint dwInValue, out uint lpOutValue);



    //
    //功  能：停止回放 
    //参  数：
    //lRealHandle： 视频流的ID值，由HB_SDVR_RealPlay返回 
    //返回值：TRUE-成功	FASLE-失败
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_StopPlayBack(int lPlayHandle);

    //
    //功  能：根据文件名备份 
    //参  数：
    //lUserID：  IP地址用户ID 值，由HB_SDVR _Login 返回
    //sDVRFileName：远程文件的文件名
    //sSavedFileName：要保存本地的文件名及路径
    //返回值：FALSE 表示失败，其他值表示返回备份流的ID值，该ID值是由SDK分配
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_GetFileByName(int lUserID, string sDVRFileName, string sSavedFileName);

    //
    //功  能：根据文件名备份 
    //参  数：
    //lUserID：  IP地址用户ID 值，由HB_SDVR _Login 返回
    //sDVRFileName：远程文件的文件名
    //sSavedFileName：要保存本地的文件名及路径,如果为空的话,表示下载文件不保存
    //返回值：FALSE 表示失败，其他值表示返回备份流的ID值，该ID值是由SDK分配
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_GetFileByName_EX(int lUserID, ref string sDVRFileName, ref string sSavedFileName);

    //
    //功  能：根据时间段备份
    //参  数：
    //lUserID：  IP地址用户ID 值，由HB_SDVR _Login 返回
    //lChannel：通道号
    //lpStartTime：开始时间
    //lpStopTime：结束时间
    //sSavedFileName：要保存本地的文件名
    //返回值：FALSE 表示失败，其他值表示返回备份流的ID值，该ID值是由SDK分配
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_GetFileByTime(int lUserID, int lChannel, HB_SDVR_TIME lpStartTime, HB_SDVR_TIME lpStopTime, string sSavedFileName);

    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_GetFileByTimeEx(int lUserID, int lChannel, byte byType, HB_SDVR_TIME lpStartTime, HB_SDVR_TIME lpStopTime, ref string sSavedFileName);

    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_GetFileByTimeWithChl(int lUserID, int lChannel, HB_SDVR_TIME lpStartTime, HB_SDVR_TIME lpStopTime, ref string sSavedFileName);


    // 使用结构体参数
    //C++ TO C# CONVERTER TODO TASK: The original C++ function pointer contained an unconverted modifier:
    //ORIGINAL LINE: typedef void (CALLBACK* PHB_SDVR_STREAMDATA_PROC)(int lRealHandle,uint dwDataType, byte *pBuffer,uint dwBufSize,uint dwUser);
    public delegate void PHB_SDVR_STREAMDATA_PROC(int lRealHandle, uint dwDataType, ref byte pBuffer, uint dwBufSize, uint dwUser);

    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_GetFile(int lUserID, HB_SDVR_FILEGETCOND pGetFile);

    //
    //功  能：停止备份 
    //参  数：
    //lFileHandle： 备份流的ID值，由HB_SDVR_GetFileByTime或HB_SDVR_GetFileByName返回 
    //返回值：TRUE-成功	FASLE-失败
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_StopGetFile(int lFileHandle);

    //
    //功  能：备份总的数据大小  
    //参  数：
    //lFileHandle： 备份流的ID值，由HB_SDVR_GetFileByTime或HB_SDVR_GetFileByName返回 
    //返回值： FASLE-失败 其他值为数据量 单位为 K
    [DllImport(SDK_HB)]
    public static extern uint HB_SDVR_GetDownloadTotalSize(int lFileHandle);

    //
    //功  能：备份已经接受的数据量  
    //参  数：
    //lFileHandle： 备份流的ID值，由HB_SDVR_GetFileByTime或HB_SDVR_GetFileByName返回 
    //返回值： FASLE-失败 其他值为数据量 单位为 K
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_GetDownloadPos(int lFileHandle);

    //
    //功  能：备份已经接受的数据量  
    //参  数：
    //lFileHandle： 备份流的ID值，由HB_SDVR_GetFileByTime或HB_SDVR_GetFileByName返回 
    //返回值： FASLE-失败 其他值为数据量 单位为 B
    [DllImport(SDK_HB)]
    public static extern double HB_SDVR_GetDownloadBytesSize(int lFileHandle); //cwh 20080806


    //31.远程服务器工作状态
    //
    //功  能：服务器状态
    //参  数：
    //lUserID：  IP地址用户ID 值，由HB_SDVR _Login 返回
    //lpWorkState：指向HB_SDVR_WORKSTATE的结构体指针
    //返回值：：FALSE 表示失败，TRUE表示成功

    //
    //功  能：服务器状态
    //参  数：
    //lUserID：  IP地址用户ID 值，由HB_SDVR _Login 返回
    //lpWorkState：指向HB_SDVR_WORKSTATE的结构体指针
    //返回值：：FALSE 表示失败，TRUE表示成功
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_GetDVRWorkState(int lUserID, ref HB_SDVR_WORKSTATE lpWorkState);
    //兼容最多128路的扩展接口
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_GetDVRWorkState_Nvs(int lUserID, ref HB_SDVR_WORKSTATE_EX lpWorkState);


    //33.日志
    //
    //功  能：查询日志
    //参  数：
    //lUserID：  IP地址用户ID 值，由HB_SDVR _Login 返回
    //lpStartTime：开始时间
    //lpStopTime：结束时间
    //返回值：：FALSE 表示失败，其他返回日志ID号
    //
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_FindDVRLog(int lUserID, HB_SDVR_TIME lpStartTime, HB_SDVR_TIME lpStopTime);

    //
    //功  能：下一条日志
    //参  数：
    //lLogHandle：   日志ID 值，由HB_SDVR_FindDVRLog返回
    //lpLogData：一条日志信息
    //nlanguage:语言 0：中文，1：英文。
    //返回值：：FALSE 表示失败，TRUE表示成功
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_FindNextLog(int lLogHandle, ref string lpLogData, int nlanguage);

    //
    //功  能：结束日至查找
    //参  数：
    //lLogHandle：   日志ID 值，由HB_SDVR_FindDVRLog返回
    //返回值：：FALSE 表示失败，TRUE表示成功
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_FindLogClose(int lLogHandle);



    //34.N/P制切换
    //#define   NET_SDVR_SET_PRESETPOLL   0x73
    //#define   NET_SDVR_GET_PRESETPOLL   0x74
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_GET_PRESETPOLL(int lUserID, HB_SDVR_PRESETPOLL presetpoll);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SET_PRESETPOLL(int lUserID, HB_SDVR_PRESETPOLL presetpoll);


    //35.多预置点轮巡
    //#define   NET_SDVR_SET_VIDEOSYS       0x75
    //#define   NET_SDVR_GET_VIDEOSYS       0x76
    //只用一个字节代表视频制式值，1---PAL，2---NTSC4.43  3--NTSC3.58
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_GET_VIDEOSYS(int lUserID, ref byte videosys);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SET_VIDEOSYS(int lUserID, byte videosys);


    //其他

    //刷新主机保存配置
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_REFRESH_FLASH(int lUserID);

    //开启声音
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_OpenSound(int lRealHandle);

    //关闭声音
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_CloseSound();

    //开启声音
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_OpenSoundShare(int lRealHandle);

    //关闭声音
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_CloseSoundShare(int lRealHandle);

    //设置音量 
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_Volume(int lRealHandle, ushort wVolume);

    //本地录像
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SaveRealData(int lRealHandle, string sFileName);

    //停止本地录像
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_StopSaveRealData(int lRealHandle);
    public delegate void fRealDataCallBackDelegate(int lRealHandle, uint dwDataType, ref byte pBuffer, uint dwBufSize, uint dwUser);

    //设置实时流数据回调
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetRealDataCallBack(int lRealHandle, fRealDataCallBackDelegate fRealDataCallBack, uint dwUser);
    //public delegate void fRealDataCallBackDelegate(int lRealHandle, PFRAMEINFO pFarmeInfo, ref byte pBuffer, uint dwBufSize, uint dwUser);

    //设置实时原始数据带帧信息回调
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetRealDataCallBack_Ex(int lRealHandle, fRealDataCallBackDelegate fRealDataCallBack, uint dwUser);
    //抓图
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_CapturePicture(int lRealHandle, string sPicFileName);

    //不解码
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetDecMode(int bSDKDec); //全局，在7000SDK中，不使用内部解码函数

    //报警清除
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_ClearAlarm(int lUserID);

    //设置获取序列号
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_GetSEQ(int lUserID, ref string buff, uint dwBufSize);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetSEQ(int lUserID, ref string buff, uint dwBufSize);

    //视频丢失状态  按位
    [DllImport(SDK_HB)]
    public static extern ushort HB_SDVR_GET_VideoLostStatus(int lUserID);
    //兼容最多128路类型机器 add by cui for 7024 or nvs 100325
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_GET_VideoLostStatus_Nvs(int lUserID, HB_SDVR_VODLOST lpVodLost);
    //public delegate void fRealDataCallBackDelegate(int lFileHandle, uint dwDataType, ref byte pBuffer, uint dwBufSize, uint dwUser);

    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetDownDataCallBack(int lFileHandle, fRealDataCallBackDelegate fRealDataCallBack, uint dwUser);

    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_GetPicFromDVR(int lUserID, ushort channel, ref string sPicFileName);

    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_GetDelayPicFromDVR(int lUserID, ushort channel, ref string sPicFileName);
    public delegate void DrawDCFunDelegate(int nChl, IntPtr hDc, int nReserved);

    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_RegisterDrawDCFun(int lRealHandle, DrawDCFunDelegate DrawDCFun, int nReserved);
    public delegate void DecCBFunDelegate(int nChl, ref string pBuf, int nSize, ref HB_FRAME_INFO pFrameInfo, int nReserved1, int nReserved2, int dwUser);

    //回调时指定输出格式
    //#define SDVR_OUT_FMT_YUV_420
    //#define SDVR_OUT_FMT_YUV_422
    public const int SDVR_OUT_FMT_YUV_420 = 0x00000601;
    public const int SDVR_OUT_FMT_YUV_422 = 0x00000102;
    //解码后回调 获取解码后数据和时间，帧
    //实时数据回调  解码后的数据 帧信息包括时间
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetRealDecCallBack(int lRealHandle, DecCBFunDelegate DecCBFun, int nOutFormat, int dwUser);
    //public delegate void DecCBFunDelegate(int nChl, ref string pBuf, int nSize, ref HB_FRAME_INFO pFrameInfo, int nReserved1, int nReserved2, int dwUser);

    //点播数据回调
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetPlayDecCallBack(int lRealHandle, DecCBFunDelegate DecCBFun, int nOutFormat, int dwUser);
    public delegate void SrcDataParseCBFunDelegate(int nChl, ref string SrcDataBuf, int nSize, int nFrameType, HB_VIDEO_TIME ets, int user);
    //原始数据流 分帧 且包含帧信息
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetParseCallBack(int lRealHandle, SrcDataParseCBFunDelegate SrcDataParseCBFun, int nRseserved);
    //public delegate void  SrcDataParseCBFunDelegate(int nChl, HB_FRAME pFrame, IntPtr  pContext);

    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetParseCallBack_Ex(int lRealHandle, SrcDataParseCBFunDelegate SrcDataParseCBFun, int dwUser);
    //public delegate void  SrcDataParseCBFunDelegate(int nChl, ref string SrcDataBuf, int nSize, int nFrameType, HB_VIDEO_TIME ets, int user);
    //下载回调
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetDownloadCallBack(int lFileHandle, SrcDataParseCBFunDelegate SrcDataParseCBFun, int nReserved1);
    //public delegate void  SrcDataParseCBFunDelegate(int nChl, HB_FRAME pFrame, IntPtr  pContext);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetDownloadCallBackEx(int lFileHandle, SrcDataParseCBFunDelegate SrcDataParseCBFun, int dwUser);
    public delegate void fDrawFunDelegate(int lRealHandle, IntPtr hDc, int dwUser);


    ////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //
    //*后面接口保留   20080702
    //
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_RegisterDrawFun(int lRealHandle,void (CALLBACK* fDrawFun)(int lRealHandle,IntPtr hDc,int dwUser),uint dwUser);
    //C++ TO C# CONVERTER TODO TASK: The original C++ function pointer contained an unconverted modifier:
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_RegisterDrawFun(int lRealHandle, fDrawFunDelegate fDrawFun, uint dwUser);
    //public delegate void  fDrawFunDelegate(int lRealHandle, IntPtr hDc, int dwUser);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_RigisterDrawFun(int lRealHandle,void (CALLBACK* fDrawFun)(int lRealHandle,IntPtr hDc,int dwUser),uint dwUser);
    //C++ TO C# CONVERTER TODO TASK: The original C++ function pointer contained an unconverted modifier:
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_RigisterDrawFun(int lRealHandle, fDrawFunDelegate fDrawFun, uint dwUser);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetPlayerBufNumber(int lRealHandle, uint dwBufNum);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_ThrowBFrame(int lRealHandle, uint dwNum);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetAudioMode(uint dwMode);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_PTZPreset(int lRealHandle, uint dwPTZPresetCmd, uint dwPresetIndex);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_PTZPreset_Other(int lUserID, int lChannel, uint dwPTZPresetCmd, uint dwPresetIndex);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_TransPTZ_EX(int lRealHandle, ref string pPTZCodeBuf, uint dwBufSize);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_PTZControl_EX(int lRealHandle, uint dwPTZCommand, uint dwStop);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_PTZPreset_EX(int lRealHandle, uint dwPTZPresetCmd, uint dwPresetIndex);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_PTZCruise(int lRealHandle, uint dwPTZCruiseCmd, byte byCruiseRoute, byte byCruisePoint, ushort wInput);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_PTZCruise_Other(int lUserID, int lChannel, uint dwPTZCruiseCmd, byte byCruiseRoute, byte byCruisePoint, ushort wInput);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_PTZCruise_EX(int lRealHandle, uint dwPTZCruiseCmd, byte byCruiseRoute, byte byCruisePoint, ushort wInput);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_PTZTrack(int lRealHandle, uint dwPTZTrackCmd);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_PTZTrack_Other(int lUserID, int lChannel, uint dwPTZTrackCmd);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_PTZTrack_EX(int lRealHandle, uint dwPTZTrackCmd);
    public delegate void fPlayDataCallBackDelegate(int lPlayHandle, uint dwDataType, ref byte pBuffer, uint dwBufSize, uint dwUser);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetPlayDataCallBack(int lPlayHandle, fPlayDataCallBackDelegate fPlayDataCallBack, uint dwUser);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_PlayBackSaveData(int lPlayHandle, string sFileName);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_StopPlayBackSave(int lPlayHandle);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_GetPlayBackOsdTime(int lPlayHandle, HB_SDVR_TIME lpOsdTime);
    [DllImport(SDK_HB)]
    public static extern uint HB_SDVR_GetPlayBackDTS(int lPlayHandle); //返回当前帧的DTS时间
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_PlayBackCaptureFile(int lPlayHandle, string sFileName);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_RestoreConfig(int lUserID);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SaveConfig(int lUserID);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_FormatDisk(int lUserID, int lDiskNumber);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_GetFormatProgress(int lFormatHandle, ref int pCurrentFormatDisk, ref int pCurrentDiskPos, ref int pFormatStatic);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_CloseFormatHandle(int lFormatHandle);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetupAlarmChan(int lUserID);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_CloseAlarmChan(int lAlarmHandle);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_GetAlarmOut(int lUserID, HB_SDVR_ALARMOUTSTATUS lpAlarmOutState);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetAlarmOut(int lUserID, int lAlarmOutPort, int lAlarmOutStatic);
    [DllImport(SDK_HB)]
    public static extern IntPtr HB_SDVR_InitG722Decoder(int nBitrate);
    [DllImport(SDK_HB)]
    public static extern void HB_SDVR_ReleaseG722Decoder(IntPtr pDecHandle);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_DecodeG722Frame(IntPtr pDecHandle, ref byte pInBuffer, ref byte pOutBuffer);
    [DllImport(SDK_HB)]
    public static extern IntPtr HB_SDVR_InitG722Encoder();
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_EncodeG722Frame(IntPtr pEncodeHandle, ref byte pInBuffer, ref byte pOutBuffer);
    [DllImport(SDK_HB)]
    public static extern void HB_SDVR_ReleaseG722Encoder(IntPtr pEncodeHandle);

    // 该两函数没有实现，与HB_SDVR_ClientSetVideoEffect/HB_SDVR_ClientGetVideoEffect有何区别？
    // HB_SDVR_API BOOL __stdcall HB_SDVR_SetVideoEffect(LONG lUserID,LONG lChannel,DWORD dwBrightValue,DWORD dwContrastValue, DWORD dwSaturationValue,DWORD dwHueValue);
    // HB_SDVR_API BOOL __stdcall HB_SDVR_GetVideoEffect(LONG lUserID,LONG lChannel,DWORD *pBrightValue,DWORD *pContrastValue, DWORD *pSaturationValue,DWORD *pHueValue);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_PTZControlWithSpeed_Other(int lUserID, int lChannel, uint dwPTZCommand, uint dwStop, uint dwSpeed);
    [DllImport(SDK_HB)]
    public static extern uint HB_SDVR_GetLastError();

    ///////////////////////////////////////////////////////////////////////////////////////////////////


    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_InitDevice_Card(ref int pDeviceTotalChan);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_ReleaseDevice_Card();
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_InitDDraw_Card(IntPtr hParent, uint colorKey);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_ReleaseDDraw_Card();
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_RealPlay_Card(int lUserID, HB_SDVR_CARDINFO lpCardInfo, int lChannelNum);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_ResetPara_Card(int lRealHandle, HB_SDVR_DISPLAY_PARA lpDisplayPara);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_RefreshSurface_Card();
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_ClearSurface_Card();
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_RestoreSurface_Card();
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_OpenSound_Card(int lRealHandle);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_CloseSound_Card(int lRealHandle);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetVolume_Card(int lRealHandle, ushort wVolume);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_AudioPreview_Card(int lRealHandle, int bEnable);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_CapturePicture_Card(int lRealHandle, ref string sPicFileName);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetDspErrMsg_Card(uint nMessage, IntPtr hWnd);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_ResetDSP_Card(int lChannelNum);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_GetSerialNum_Card(int lChannelNum, ref uint pDeviceSerialNo);
    [DllImport(SDK_HB)]
    public static extern IntPtr HB_SDVR_GetChanHandle_Card(int lRealHandle);

    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_CaptureJPEGPicture(int lUserID, int lChannel, HB_SDVR_JPEGPARA lpJpegPara, ref string sPicFileName);

    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_GetConfigFile(int lUserID, ref string sFileName);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetConfigFile(int lUserID, ref string sFileName);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_GetConfigFile_EX(int lUserID, ref string sOutBuffer, uint dwOutSize);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetConfigFile_EX(int lUserID, ref string sInBuffer, uint dwInSize);

    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetTrainInfo(int lUserID, IntPtr lpTrainInfoBuf, uint dwTrainInfolen);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_GetTrainInfo(int lUserID, IntPtr lpTainOutInfoBuf, uint dwTrainOutInfolen, ref uint lpBytesReturned);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_FindFileByTrainRunFlag(int lUserID, int lChannel, uint dwFileType, HB_SDVR_TIME lpStartTime, HB_SDVR_TIME lpStopTime);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_FindFileByTrainRunFlag(int lUserID, int lChannel, uint dwFileType, HB_SDVR_TIME lpStartTime, HB_SDVR_TIME lpStopTime);
    // HB_SDVR_API LONG __stdcall HB_SDVR_FindPicture(LONG lUserID,LONG lChannel,DWORD dwFileType, BOOL bNeedCardNum, BYTE *sCardNumber, HB_SDVR_TIME lpStartTime, HB_SDVR_TIME lpStopTime);
    // HB_SDVR_API LONG __stdcall HB_SDVR_FindNextPicture(LONG lFindHandle,LPHB_SDVR_FIND_PICTURE lpFindData);
    // HB_SDVR_API BOOL __stdcall HB_SDVR_CloseFindPicture(LONG lFindHandle);
    // HB_SDVR_API BOOL __stdcall HB_SDVR_GetPicture(LONG lUserID,char *sDVRFileName,char *sSavedFileName);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_SetOEMInfo(int lUserID,IntPtr  lpOEMInfoBuf,uint dwOEMInfolen);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_SetOEMInfo(int lUserID, IntPtr  lpOEMInfoBuf, uint dwOEMInfolen);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_GetOEMInfo(int lUserID,IntPtr  lpOEMOutInfoBuf,uint dwOEMOutInfolen, uint * lpBytesReturned);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_GetOEMInfo(int lUserID, IntPtr  lpOEMOutInfoBuf, uint dwOEMOutInfolen, ref uint lpBytesReturned);
    //public delegate void fVoiceDataCallBackDelegate(int lVoiceComHandle, ref string pRecvDataBuffer, uint dwBufSize, byte byAudioFlag, uint dwUser);



    //C++ TO C# CONVERTER NOTE: CALLBACK is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_StartVoiceCom_MR(int lUserID, void(CALLBACK *fVoiceDataCallBack)(int lVoiceComHandle,sbyte *pRecvDataBuffer,uint dwBufSize,byte byAudioFlag,uint dwUser), uint dwUser);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_StartVoiceCom_MR(int lUserID, fVoiceDataCallBackDelegate fVoiceDataCallBack, uint dwUser);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_VoiceComSendData(int lVoiceComHandle,sbyte *pSendBuf,uint dwBufSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_VoiceComSendData(int lVoiceComHandle, ref string pSendBuf, uint dwBufSize);


    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_SetDecInfo(int lUserID, int lChannel, LPHB_SDVR_DECCFG lpDecoderinfo);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_SetDecInfo(int lUserID, int lChannel, LPHB_SDVR_DECCFG lpDecoderinfo);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_GetDecInfo(int lUserID, int lChannel, LPHB_SDVR_DECCFG lpDecoderinfo);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_GetDecInfo(int lUserID, int lChannel, LPHB_SDVR_DECCFG lpDecoderinfo);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_SetDecTransPort(int lUserID, LPHB_SDVR_PORTCFG lpTransPort);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_SetDecTransPort(int lUserID, LPHB_SDVR_PORTCFG lpTransPort);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_GetDecTransPort(int lUserID, LPHB_SDVR_PORTCFG lpTransPort);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_GetDecTransPort(int lUserID, LPHB_SDVR_PORTCFG lpTransPort);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_DecPlayBackCtrl(int lUserID, int lChannel, uint dwControlCode, uint dwInValue,uint *lpOutValue, LPHB_SDVR_PLAYREMOTEFILE lpRemoteFileInfo);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_DecPlayBackCtrl(int lUserID, int lChannel, uint dwControlCode, uint dwInValue, ref uint lpOutValue, LPHB_SDVR_PLAYREMOTEFILE lpRemoteFileInfo);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_StartDecSpecialCon(int lUserID, int lChannel, LPHB_SDVR_DECCHANINFO lpDecChanInfo);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_StartDecSpecialCon(int lUserID, int lChannel, LPHB_SDVR_DECCHANINFO lpDecChanInfo);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_StopDecSpecialCon(int lUserID, int lChannel, LPHB_SDVR_DECCHANINFO lpDecChanInfo);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_StopDecSpecialCon(int lUserID, int lChannel, LPHB_SDVR_DECCHANINFO lpDecChanInfo);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_DecCtrlDec(int lUserID, int lChannel, uint dwControlCode);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_DecCtrlDec(int lUserID, int lChannel, uint dwControlCode);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_DecCtrlScreen(int lUserID, int lChannel, uint dwControl);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_DecCtrlScreen(int lUserID, int lChannel, uint dwControl);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_GetDecCurLinkStatus(int lUserID, int lChannel, LPHB_SDVR_DECSTATUS lpDecStatus);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_GetDecCurLinkStatus(int lUserID, int lChannel, LPHB_SDVR_DECSTATUS lpDecStatus);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_ClientGetframeformat(int lUserID, LPHB_SDVR_FRAMEFORMAT lpFrameFormat);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_ClientGetframeformat(int lUserID, LPHB_SDVR_FRAMEFORMAT lpFrameFormat);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_ClientSetframeformat(int lUserID, LPHB_SDVR_FRAMEFORMAT lpFrameFormat);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_ClientSetframeformat(int lUserID, LPHB_SDVR_FRAMEFORMAT lpFrameFormat);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_FindFileByCard(int lUserID,int lChannel,uint dwFileType, int bNeedCardNum, byte *sCardNumber, HB_SDVR_TIME lpStartTime, HB_SDVR_TIME lpStopTime);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_FindFileByCard(int lUserID, int lChannel, uint dwFileType, int bNeedCardNum, ref byte sCardNumber, HB_SDVR_TIME lpStartTime, HB_SDVR_TIME lpStopTime);


    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_StartListen(sbyte *sLocalIP,ushort wLocalPort);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_StartListen(ref string sLocalIP, ushort wLocalPort);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_StopListen();
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_StopListen();

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_SetShowMode(uint dwShowType,uint colorKey);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_SetShowMode(uint dwShowType, uint colorKey);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_GetDVRIPByResolveSvr(sbyte *sServerIP, ushort wServerPort, byte *sDVRName,ushort wDVRNameLen,byte *sDVRSerialNumber,ushort wDVRSerialLen,sbyte* sGetIP);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_GetDVRIPByResolveSvr(ref string sServerIP, ushort wServerPort, ref byte sDVRName, ushort wDVRNameLen, ref byte sDVRSerialNumber, ushort wDVRSerialLen, ref string sGetIP);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_RealPlayPause(int lRealHandle);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_RealPlayPause(int lRealHandle);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_RealPlayRestart(int lRealHandle, IntPtr hPlayWnd);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_RealPlayRestart(int lRealHandle, IntPtr hPlayWnd);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_SendTo232Port(int lUserID,sbyte *pSendBuf,uint dwBufSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_SendTo232Port(int lUserID, ref string pSendBuf, uint dwBufSize);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_GET_PRESETPOLL (int lUserID, LPHB_SDVR_PRESETPOLL presetpoll);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_GET_PRESETPOLL(int lUserID, LPHB_SDVR_PRESETPOLL presetpoll);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_SET_PRESETPOLL (int lUserID, LPHB_SDVR_PRESETPOLL presetpoll);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_SET_PRESETPOLL(int lUserID, LPHB_SDVR_PRESETPOLL presetpoll);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_GET_VIDEOSYS (int lUserID,byte* videosys);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_GET_VIDEOSYS(int lUserID, ref byte videosys);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_SET_VIDEOSYS (int lUserID,byte videosys);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_SET_VIDEOSYS(int lUserID, byte videosys);
    public delegate void endplayCallBkDelegate(uint playwnd, int nReserved1, int nReserved2);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_RegisterEndPlayCallback (void (CALLBACK* endplayCallBk)(uint playwnd,int nReserved1, int nReserved2),int nReserved);
    //C++ TO C# CONVERTER TODO TASK: The original C++ function pointer contained an unconverted modifier:
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_RegisterEndPlayCallback(endplayCallBkDelegate endplayCallBk, int nReserved);

    //add by cui 09.01.05
    //
    //	功能：画面的局部放大。
    //	输入：lPlayHandle 播放句柄，nRegionNum 显示的区域号（传入1—3），pSrcRect 要放大的区域，
    //			hdestWnd 显示视频的窗口句柄，bEnable 1显示 0不显示。
    //
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_SetDisplayRegion(int lPlayHandle,uint nRegionNum,RECT *pSrcRect,IntPtr hdestWnd,int bEnable);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_SetDisplayRegion(int lPlayHandle, uint nRegionNum, ref RECT pSrcRect, IntPtr hdestWnd, int bEnable);
    //add by cui 09.05.15
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_GetPictureSize(int lPlayHandle, int *pWidth, int *pHeight);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_GetPictureSize(int lPlayHandle, ref int pWidth, ref int pHeight);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_SetPicQuality(int lPlayHandle,int bHighQuality);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_SetPicQuality(int lPlayHandle, int bHighQuality);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_GetPicQuality(int lPlayHandle,int *bHighQuality);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_GetPicQuality(int lPlayHandle, ref int bHighQuality);



    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_SetPTZProtocol(int lUserID, LPSTRUCT_SDVR_DECODERCUSTOMIZE lpStructPtzProtocol, int lSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_SetPTZProtocol(int lUserID, LPSTRUCT_SDVR_DECODERCUSTOMIZE lpStructPtzProtocol, int lSize);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_GetPTZProtocol(int lUserID, LPSTRUCT_SDVR_DECODERCUSTOMIZE lpStructPtzProtocol, int lSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_GetPTZProtocol(int lUserID, LPSTRUCT_SDVR_DECODERCUSTOMIZE lpStructPtzProtocol, int lSize);
    //恢复DVR缺省参数设置
    //nType: 0:部分缺省;1:所有缺省.
    //cwh 20090318
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_RecoverDefault(int lUserID,byte nType);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_RecoverDefault(int lUserID, byte nType);



    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_GetPTZType(int lUserID, LPSTRUCT_SDVR_PTZTYPE lpStructPtzType, int lSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_GetPTZType(int lUserID, LPSTRUCT_SDVR_PTZTYPE lpStructPtzType, int lSize);



    //延时布防 //add by cwh 20090803
    //*********************************************************
    //参数：
    //	lUserID：登陆ID
    //	struParam：STRUCT_ALARMIN_WAIT结构体
    //返回值：
    //	TRUE：成功；
    //	FALSE：失败。
    //**********************************************************
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_SetDelayDefence(int lUserID,STRUCT_ALARMIN_WAIT struParam);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_SetDelayDefence(int lUserID, STRUCT_ALARMIN_WAIT struParam);

    //获取布防状态 //add by cwh 20090810
    //*********************************************************
    //参数：
    //	lUserID：登陆ID
    //	struParam：STRUCT_ALARMIN_WAIT结构体
    //返回值：
    //	TRUE：成功；
    //	FALSE：失败。
    //**********************************************************
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_GetDelayDefence(int lUserID,LPSTRUCT_ALARMIN_WAIT pStruParam);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_GetDelayDefence(int lUserID, LPSTRUCT_ALARMIN_WAIT pStruParam);

    //
    //功能：获取IPC参数
    //结构体：STRUCT_SDVR_IPCCONFIG
    //参数：登录id lUserID 
    //返回值：成功ture 失败false
    //

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_GetIpcConfig(int lUserID,LPSTRUCT_SDVR_IPCCONFIG pStruIpcParam);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_GetIpcConfig(int lUserID, LPSTRUCT_SDVR_IPCCONFIG pStruIpcParam);
    //
    //功能：设置IPC参数
    //结构体：STRUCT_SDVR_IPCCONFIG
    //参数：登录id lUserID 
    //返回值：成功ture 失败false
    //

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_SetIpcConfig(int lUserID,LPSTRUCT_SDVR_IPCCONFIG pStruIpcParam);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_SetIpcConfig(int lUserID, LPSTRUCT_SDVR_IPCCONFIG pStruIpcParam);
    //
    //功能：设置IPC快门
    //参数：登录id lUserID 
    //返回值：成功ture 失败false
    //


    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_GetIpcPicConfig(int lUserID,LPSTRUCT_SDVR_IPCPIC pStruIpcPic);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_GetIpcPicConfig(int lUserID, LPSTRUCT_SDVR_IPCPIC pStruIpcPic);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_SetIpcPicConfig(int lUserID,LPSTRUCT_SDVR_IPCPIC pStruIpcPic);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_SetIpcPicConfig(int lUserID, LPSTRUCT_SDVR_IPCPIC pStruIpcPic);
    //
    //功能：设置IPCAGC
    //参数：登录id lUserID 
    //返回值：成功ture 失败false
    //


    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_GetIpcAgcConfig(int lUserID,LPSTRUCT_SDVR_IPCAGC pStruIpcAgc);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_GetIpcAgcConfig(int lUserID, LPSTRUCT_SDVR_IPCAGC pStruIpcAgc);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_SetIpcAgcConfig(int lUserID,LPSTRUCT_SDVR_IPCAGC pStruIpcAgc);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_SetIpcAgcConfig(int lUserID, LPSTRUCT_SDVR_IPCAGC pStruIpcAgc);


    //
    //功能：设置IPC无线参数设置
    //STRUCT_SDVR_IPCWIRELESS
    //参数： 
    //返回值：成功ture 失败false
    //

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_SetIPCWirelessSet(int lUserID, LPSTRUCT_SDVR_IPCWIRELESS pStruIpcWireless);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_SetIPCWirelessSet(int lUserID, LPSTRUCT_SDVR_IPCWIRELESS pStruIpcWireless);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_GetIPCWirelessSet(int lUserID,LPSTRUCT_SDVR_IPCWIRELESS pStruIpcWireless);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_GetIPCWirelessSet(int lUserID, LPSTRUCT_SDVR_IPCWIRELESS pStruIpcWireless);
    // 老7000sdk接口！神马玩意
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_SetIPCWirelessGet(int lUserID,LPSTRUCT_SDVR_IPCWIRELESS pStruIpcWireless);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_SetIPCWirelessGet(int lUserID, LPSTRUCT_SDVR_IPCWIRELESS pStruIpcWireless);
    //********************************************************************************
    //add by cui 09.10.09
    //功能：设置ouerlay模式。
    //参数: bOverlayMode[In] : 为TRUE 表示采用Overlay 模式, 如果创建Overlay
    //		平面失败,将自动采用其它显示模式.
    //		colorKey [In] : 要采用的透明色. 透明色相当于一层透视膜，显示的
    //		画面只能穿过这种颜色，而其他的颜色将挡住显示的画面.用户应该在显示窗口
    //		中涂上这种颜色，那样才能看到显示画面.一般应该使用一种不常用的颜色作为
    //		透明色.这是一个DWORD 值:0x00rrggbb,最高字节为0，后三个字节分别表示
    //		r,g,b 的值.
    //
    //********************************************************************************
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_SetOverlayMode(int lPlayHandle, int bOverlayMode, uint colorKey);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_SetOverlayMode(int lPlayHandle, int bOverlayMode, uint colorKey);
    //add by njt for 泰晨
    //
    //功能：获取底层UserID
    //参数：
    //   lUserID ：由HB_SDVR_Login返回
    //返回值：
    //    成功:返回底层UserID
    //	失败:返回false
    //
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_GetUserID(int lUserID);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_GetUserID(int lUserID);
    //ADD by njt
    // 功能： 设置7000T的帧间隔
    // 参数： lUserID ：登录id 
    //       iFrameRate为STRUCT_SDVR_IFRAMERATE结构体
    // 


    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_SetIFrameRate(int lUserID,LPSTRUCT_SDVR_IFRAMERATE pStruiFrameRate);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_SetIFrameRate(int lUserID, LPSTRUCT_SDVR_IFRAMERATE pStruiFrameRate);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_GetIFrameRate(int lUserID, int lChannel, LPSTRUCT_SDVR_IFRAMERATE pStruiFrameRate);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_GetIFrameRate(int lUserID, int lChannel, LPSTRUCT_SDVR_IFRAMERATE pStruiFrameRate);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" VOID __stdcall HB_SDVR_SendAudio(sbyte *pAuidoBuffer,int length);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //VOID HB_SDVR_SendAudio(ref string pAuidoBuffer, int length);

    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////以下是8000TH新添加的协议接口////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////


    //********************************************************************************
    ////add by cuigc for 8000TH 100401
    //功能：8000TH  NTP配置参数的设置和查询。
    //参数: lUserID 用户ID，用户登录时返回的。pStruNtpPara NTP配置参数。
    //返回：true 成功   false 失败。
    //********************************************************************************


    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_NTPConfigSet(int lUserID,LPHB_SDVR_NTPCONFIG pStruNtpPara);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_NTPConfigSet(int lUserID, LPHB_SDVR_NTPCONFIG pStruNtpPara);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_NTPConfigGet(int lUserID,LPHB_SDVR_NTPCONFIG pStruNtpPara);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_NTPConfigGet(int lUserID, LPHB_SDVR_NTPCONFIG pStruNtpPara);

    //********************************************************************************
    ////add by cuigc for 8000TH 100401
    //功能：8000TH 邮件服务参数的设置和查询。
    //参数: lUserID 用户ID，用户登录时返回的。pStruSmtpPara 邮件服务配置参数。
    //返回：true 成功   false 失败。
    //********************************************************************************



    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_SMTPConfigSet(int lUserID,LPHB_SDVR_SMTPCONFIG pStruSmtpPara);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_SMTPConfigSet(int lUserID, LPHB_SDVR_SMTPCONFIG pStruSmtpPara);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_SMTPConfigGet(int lUserID,LPHB_SDVR_SMTPCONFIG pStruSmtpPara);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_SMTPConfigGet(int lUserID, LPHB_SDVR_SMTPCONFIG pStruSmtpPara);

    //********************************************************************************
    ////add by cuigc for 8000TH 100401
    //功能：8000TH 轮巡配置参数的设置和查询。
    //参数: lUserID 用户ID，用户登录时返回的。pStruPollPara 轮巡配置参数。
    //返回：true 成功   false 失败。
    //********************************************************************************



    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_PollConfigSet(int lUserID,LPHB_SDVR_POLLCONFIG pStruPollPara);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_PollConfigSet(int lUserID, LPHB_SDVR_POLLCONFIG pStruPollPara);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_PollConfigGet(int lUserID,LPHB_SDVR_POLLCONFIG pStruPollPara);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_PollConfigGet(int lUserID, LPHB_SDVR_POLLCONFIG pStruPollPara);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_SetPollConfig(int lUserID, IntPtr  lpBuf, uint dwSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_SetPollConfig(int lUserID, IntPtr  lpBuf, uint dwSize);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_GetPollConfig(int lUserID, IntPtr  lpBuf, uint* pSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_GetPollConfig(int lUserID, IntPtr  lpBuf, ref uint pSize);

    //********************************************************************************
    ////add by cuigc for 8000TH 100401
    //功能：8000TH 视频矩阵参数的设置和查询。
    //参数: lUserID 用户ID，用户登录时返回的。pStruVideoMatrixPara 视频矩阵配置参数。
    //返回：true 成功   false 失败。
    //********************************************************************************




    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_VideoMatrixSet(int lUserID,LPHB_SDVR_VIDEOMATRIX pStruVideoMatrixPara);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_VideoMatrixSet(int lUserID, LPHB_SDVR_VIDEOMATRIX pStruVideoMatrixPara);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_VideoMatrixGet(int lUserID,LPHB_SDVR_VIDEOMATRIX pStruVideoMatrixPara);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_VideoMatrixGet(int lUserID, LPHB_SDVR_VIDEOMATRIX pStruVideoMatrixPara);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_SetVideoMatrixConfig(int lUserID, IntPtr  lpBuf, uint dwSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_SetVideoMatrixConfig(int lUserID, IntPtr  lpBuf, uint dwSize);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_GetVideoMatrixConfig(int lUserID, IntPtr  lpBuf, uint* lpSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_GetVideoMatrixConfig(int lUserID, IntPtr  lpBuf, ref uint lpSize);

    //********************************************************************************
    ////add by njt for 8000TH 
    //功能：平台信息的设置和查询。
    //参数: lUserID 用户ID，用户登录时返回的。
    //返回：true 成功   false 失败。
    //********************************************************************************


    // typedef struct
    // {
    // 	BYTE plat_type[MAX_PLATNUM];//平台类型
    // 	BYTE reserve[32];
    // }HB_SDVR_PLATPARAM,*LPHB_SDVR_PLATPARAM;


    //HB_SDVR_API BOOL __stdcall  HB_SDVR_GETPLATPARAM(LONG lUserID,LPHB_SDVR_PLATPARAM pStrplatparam);
    //HB_SDVR_API BOOL __stdcall  HB_SDVR_SETPLATPARAM(LONG lUserID,LPHB_SDVR_PLATPARAM pStrplatparam);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_GETPLATPARAM(int lUserID, IntPtr  lpBuffer,uint dwBufferSize,uint type);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_GETPLATPARAM(int lUserID, IntPtr  lpBuffer, uint dwBufferSize, uint type);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_SETPLATPARAM(int lUserID, IntPtr  lpBuffer,uint dwBufferSize,uint type);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_SETPLATPARAM(int lUserID, IntPtr  lpBuffer, uint dwBufferSize, uint type);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_SetBuzzerState(int lUserID, BUSZZERSTATE WorkState);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_SetBuzzerState(int lUserID, BUSZZERSTATE WorkState);


    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_GetBuzzerState(int lUserID, BUSZZERSTATE *pWorkState);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_GetBuzzerState(int lUserID, ref BUSZZERSTATE pWorkState);
    public delegate void fAlarmDataCallBackDelegate(int lAlarmHandle, ref string pRecvDataBuffer, uint dwBufSize, uint dwUser);

    //报警通道
    //C++ TO C# CONVERTER NOTE: CALLBACK is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_AlarmStart(int lUserID,void(CALLBACK *fAlarmDataCallBack)(int lAlarmHandle,sbyte *pRecvDataBuffer,uint dwBufSize,uint dwUser),uint dwUser);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_AlarmStart(int lUserID, fAlarmDataCallBackDelegate fAlarmDataCallBack, uint dwUser);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_AlarmStop(int lUserID);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_AlarmStop(int lUserID);


    // 智能ATM
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_GetAlarmConfig(int lUserID, IntPtr  lpOutBuffer, int channel);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_GetAlarmConfig(int lUserID, IntPtr  lpOutBuffer, int channel);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_SetAlarmConfig(int lUserID, IntPtr  lpInBuffer, int channel);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_SetAlarmConfig(int lUserID, IntPtr  lpInBuffer, int channel);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_ResetAlarmEnvi(int lUserID);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_ResetAlarmEnvi(int lUserID);

    // 获取实时布防状态
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_GetRealDefenceCfg(int lUserID, LPHB_SDVR_REAL_DEFENCE lpCfg, uint* pDataSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_GetRealDefenceCfg(int lUserID, LPHB_SDVR_REAL_DEFENCE lpCfg, ref uint pDataSize);
    // 实时布防、撤防
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_SetRealDefenceCfg(int lUserID, LPHB_SDVR_REAL_DEFENCE lpCfg, uint dwDataSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_SetRealDefenceCfg(int lUserID, LPHB_SDVR_REAL_DEFENCE lpCfg, uint dwDataSize);
    // 获取主机掉线期间的报警信息
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_GetDisconAlarmInfo(int lUserID, LPHB_SDVR_DISCONN_ALMSTAT lpInfo, uint* pDataSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_GetDisconAlarmInfo(int lUserID, LPHB_SDVR_DISCONN_ALMSTAT lpInfo, ref uint pDataSize);

    // 接口函数
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_GetVcoverAlarmCfg(int lUserID, LPHB_SDVR_VCOVER_ALM lpCfg, uint* pDataSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_GetVcoverAlarmCfg(int lUserID, LPHB_SDVR_VCOVER_ALM lpCfg, ref uint pDataSize);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_SetVcoverAlarmCfg(int lUserID, LPHB_SDVR_VCOVER_ALM lpCfg, uint dwDataSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_SetVcoverAlarmCfg(int lUserID, LPHB_SDVR_VCOVER_ALM lpCfg, uint dwDataSize);

    // 接口函数
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_IpcGetWorkParam(int lUserID, LPHB_SDVR_IPCWORKMODE lpCfg, uint* pDataSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_IpcGetWorkParam(int lUserID, LPHB_SDVR_IPCWORKMODE lpCfg, ref uint pDataSize);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_IpcSetWorkParam(int lUserID, LPHB_SDVR_IPCWORKMODE lpCfg, uint dwDataSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_IpcSetWorkParam(int lUserID, LPHB_SDVR_IPCWORKMODE lpCfg, uint dwDataSize);
    // 映射列表：
    //      0-HDMI 1080P X 60HZ
    // 		1-HDMI 1080P X 50HZ
    // 		2-HDMI 720P X 60HZ 
    // 		3-HDMI 720P X 50HZ
    // 		4-VGA  1024 X 768


    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_NvrGetWorkStatus(int lUserID, LPHB_SDVR_WORKSTATE_V2 lpWorkStatus, uint* lpDataSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_NvrGetWorkStatus(int lUserID, LPHB_SDVR_WORKSTATE_V2 lpWorkStatus, ref uint lpDataSize);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_NvrGetChnClientIP(int lUserID, LPHB_CLIENT_IP_INFO lpClientIP, uint* lpDataSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_NvrGetChnClientIP(int lUserID, LPHB_CLIENT_IP_INFO lpClientIP, ref uint lpDataSize);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_NvrFindLog(int lUserID, LPHB_LOG_REQ_PARAM lpParam);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_NvrFindLog(int lUserID, LPHB_LOG_REQ_PARAM lpParam);

    // //查找文件和日志函数返回值
    // #define HB_SDVR_FILE_SUCCESS				1000	//获得文件信息
    // #define HB_SDVR_FILE_NOFIND				    1001	//没有文件
    // #define HB_SDVR_ISFINDING				    1002	//正在查找文件
    // #define	HB_SDVR_NOMOREFILE				    1003	//查找文件时没有更多的文件
    // #define	HB_SDVR_FILE_EXCEPTION				1004	//查找文件时异常
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_NvrFindNextLog(int lLogHandle, sbyte* pLogData, byte* pEncType);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_NvrFindNextLog(int lLogHandle, ref string pLogData, ref byte pEncType);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_NvrFindLogClose(int lLogHandle);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_NvrFindLogClose(int lLogHandle);
    //public delegate void fSerialDataCallBackDelegate(int lSerialHandle, ref string pRecvDataBuffer, uint dwBufSize, uint dwUser);

    //C++ TO C# CONVERTER NOTE: CALLBACK is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_NvrSerialStart(int lUserID, LPHB_NVR_SERIAL_START lpSerial, uint dwDataSize, void(CALLBACK *fSerialDataCallBack)(int lSerialHandle,sbyte *pRecvDataBuffer,uint dwBufSize,uint dwUser),uint dwUser);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_NvrSerialStart(int lUserID, LPHB_NVR_SERIAL_START lpSerial, uint dwDataSize, fSerialDataCallBackDelegate fSerialDataCallBack, uint dwUser);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_NvrSerialSend(int lSerialHandle, sbyte* pSendData, uint dwDataSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_NvrSerialSend(int lSerialHandle, ref string pSendData, uint dwDataSize);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_NvrSerialStop(int lSerialHandle);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_NvrSerialStop(int lSerialHandle);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_NvrGetDevInfo(int lUserID, LPHB_DEVICEINFO_V2 lpDevInfo, uint* lpDataSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_NvrGetDevInfo(int lUserID, LPHB_DEVICEINFO_V2 lpDevInfo, ref uint lpDataSize);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_NvrSetDevInfo(int lUserID, LPHB_DEVICEINFO_V2 lpDevInfo, uint dwDataSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_NvrSetDevInfo(int lUserID, LPHB_DEVICEINFO_V2 lpDevInfo, uint dwDataSize);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_NvrGetPtzList(int lUserID, LPHB_NVR_PTZLIST lpPtz, uint dwDataSize, LPHB_NVR_PTZLIST_INFO lpPtzInfo, uint* lpDataSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_NvrGetPtzList(int lUserID, LPHB_NVR_PTZLIST lpPtz, uint dwDataSize, LPHB_NVR_PTZLIST_INFO lpPtzInfo, ref uint lpDataSize);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_NvrGetAlarmAttr(int lUserID, LPHB_NVR_ALRM_PORT_ATTR lpAlarmAttr, uint* lpDataSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_NvrGetAlarmAttr(int lUserID, LPHB_NVR_ALRM_PORT_ATTR lpAlarmAttr, ref uint lpDataSize);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_NvrGetAlarmInCfg(int lUserID, LPHB_NVR_ALRMININFO lpAlarmIn, uint* lpDataSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_NvrGetAlarmInCfg(int lUserID, LPHB_NVR_ALRMININFO lpAlarmIn, ref uint lpDataSize);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_NvrSetAlarmInCfg(int lUserID, LPHB_NVR_ALRMININFO lpAlarmIn, uint dwDataSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_NvrSetAlarmInCfg(int lUserID, LPHB_NVR_ALRMININFO lpAlarmIn, uint dwDataSize);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_NvrGetAlarmOutCfg(int lUserID, LPHB_NVR_ALARMOUTINFO lpAlarmOut, uint* lpDataSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_NvrGetAlarmOutCfg(int lUserID, LPHB_NVR_ALARMOUTINFO lpAlarmOut, ref uint lpDataSize);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_NvrSetAlarmOutCfg(int lUserID, LPHB_NVR_ALARMOUTINFO lpAlarmOut, uint dwDataSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_NvrSetAlarmOutCfg(int lUserID, LPHB_NVR_ALARMOUTINFO lpAlarmOut, uint dwDataSize);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_NvrGetAlarmInStatus(int lUserID, LPHB_NVR_ALRMIN_STATUS lpAlarmInStat, uint* lpDataSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_NvrGetAlarmInStatus(int lUserID, LPHB_NVR_ALRMIN_STATUS lpAlarmInStat, ref uint lpDataSize);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_NvrGetAlarmOutStatus(int lUserID, LPHB_NVR_ALRMOUT_STATUS lpAlarmOutStat, uint* lpDataSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_NvrGetAlarmOutStatus(int lUserID, LPHB_NVR_ALRMOUT_STATUS lpAlarmOutStat, ref uint lpDataSize);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_NvrSetAlarmOutStatus(int lUserID, LPHB_NVR_ALRMOUT_STATUS lpAlarmOutStat, uint dwDataSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_NvrSetAlarmOutStatus(int lUserID, LPHB_NVR_ALRMOUT_STATUS lpAlarmOutStat, uint dwDataSize);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_NvrGetPicCfg(int lUserID, byte byChnl, LPHB_NVR_CHN_ATTR_INFO lpChlInfo, uint* lpDataSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_NvrGetPicCfg(int lUserID, byte byChnl, LPHB_NVR_CHN_ATTR_INFO lpChlInfo, ref uint lpDataSize);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_NvrSetPicCfg(int lUserID, LPHB_NVR_CHN_ATTR_INFO lpChlInfo, uint dwDataSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_NvrSetPicCfg(int lUserID, LPHB_NVR_CHN_ATTR_INFO lpChlInfo, uint dwDataSize);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_NvrGetRecordCfg(int lUserID, LPHB_RECORD_SET lpRecordSet, uint* lpDataSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_NvrGetRecordCfg(int lUserID, LPHB_RECORD_SET lpRecordSet, ref uint lpDataSize);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_NvrSetRecordCfg(int lUserID, LPHB_RECORD_SET lpRecordSet, uint dwDataSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_NvrSetRecordCfg(int lUserID, LPHB_RECORD_SET lpRecordSet, uint dwDataSize);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_NvrGetMotDetCfg(int lUserID, LPHB_NVR_MOTION lpMotion, uint* lpDataSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_NvrGetMotDetCfg(int lUserID, LPHB_NVR_MOTION lpMotion, ref uint lpDataSize);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_NvrSetMotDetCfg(int lUserID, LPHB_NVR_MOTION lpMotion, uint dwDataSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_NvrSetMotDetCfg(int lUserID, LPHB_NVR_MOTION lpMotion, uint dwDataSize);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_NvrGetAbnorAlarmCfg(int lUserID, LPHB_NVR_ABNORMAL lpAbnor, uint* lpDataSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_NvrGetAbnorAlarmCfg(int lUserID, LPHB_NVR_ABNORMAL lpAbnor, ref uint lpDataSize);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_NvrSetAbnorAlarmCfg(int lUserID, LPHB_NVR_ABNORMAL lpAbnor, uint dwDataSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_NvrSetAbnorAlarmCfg(int lUserID, LPHB_NVR_ABNORMAL lpAbnor, uint dwDataSize);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_NvrExpParamFile(int lUserID, sbyte* pSaveFile);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_NvrExpParamFile(int lUserID, ref string pSaveFile);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_NvrExpParamFileStatus(int lExpHandle, LPHB_PARAMFILE_STATUS lpExpStatus);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_NvrExpParamFileStatus(int lExpHandle, LPHB_PARAMFILE_STATUS lpExpStatus);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_NvrExpParamFileClose(int lExpHandle);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_NvrExpParamFileClose(int lExpHandle);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_NvrImpParamFile(int lUserID, sbyte* pImpFileName);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_NvrImpParamFile(int lUserID, ref string pImpFileName);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_NvrImpParamFileStatus(int lImpHandle, LPHB_PARAMFILE_STATUS lpImpStatus);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_NvrImpParamFileStatus(int lImpHandle, LPHB_PARAMFILE_STATUS lpImpStatus);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_NvrImpParamFileClose(int lImpHandle);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_NvrImpParamFileClose(int lImpHandle);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_NvrGetResolution(int lUserID, LPHB_NVR_RESOLUTION lpResolution, uint* lpDataSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_NvrGetResolution(int lUserID, LPHB_NVR_RESOLUTION lpResolution, ref uint lpDataSize);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_NvrSetResolution(int lUserID, LPHB_NVR_RESOLUTION lpResolution, uint dwDataSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_NvrSetResolution(int lUserID, LPHB_NVR_RESOLUTION lpResolution, uint dwDataSize);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_ZeroRealPlay(int lUserID, LPHB_SDVR_CLIENTINFO lpClientInfo);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_ZeroRealPlay(int lUserID, LPHB_SDVR_CLIENTINFO lpClientInfo);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_ZeroStopRealPlay(int lRealHandle);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_ZeroStopRealPlay(int lRealHandle);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_ZeroGetConfig(int lUserID, uint dwCommand, IntPtr  lpOutBuffer, uint * lpOutBufferSize, uint * lpReturned);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_ZeroGetConfig(int lUserID, uint dwCommand, IntPtr  lpOutBuffer, ref uint lpOutBufferSize, ref uint lpReturned);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_ZeroSetConfig(int lUserID, uint dwCommand, IntPtr  lpInBuffer, uint dwBufferSize, uint * lpReturned);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_ZeroSetConfig(int lUserID, uint dwCommand, IntPtr  lpInBuffer, uint dwBufferSize, ref uint lpReturned);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_Vod(int lUserID, LPHB_SDVR_VOD_PARAM pVodParam, IntPtr hWnd);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_Vod(int lUserID, LPHB_SDVR_VOD_PARAM pVodParam, IntPtr hWnd);

    //#define HB_SDVR_VOD_PLAYSTART
    //#define HB_SDVR_VOD_PLAYPAUSE
    //#define HB_SDVR_VOD_PLAYFAST
    //#define HB_SDVR_VOD_PLAYSLOW
    //#define HB_SDVR_VOD_PLAYNORMAL
    //#define HB_SDVR_VOD_PLAYFRAME
    //#define HB_SDVR_VOD_PLAYSETPOS
    //#define HB_SDVR_VOD_PLAYGETPOS
    public const int HB_SDVR_VOD_PLAYSTART = 1;
    public const int HB_SDVR_VOD_PLAYPAUSE = 2;
    public const int HB_SDVR_VOD_PLAYFAST = 3;
    public const int HB_SDVR_VOD_PLAYSLOW = 4;
    public const int HB_SDVR_VOD_PLAYNORMAL = 5;
    public const int HB_SDVR_VOD_PLAYFRAME = 6;
    public const int HB_SDVR_VOD_PLAYSETPOS = 7;
    public const int HB_SDVR_VOD_PLAYGETPOS = 8;

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_VodControl(int lVodHandle, uint dwControlCode, uint dwInValue, uint *lpOutValue);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_VodControl(int lVodHandle, uint dwControlCode, uint dwInValue, ref uint lpOutValue);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_StopVod(int lVodHandle);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_StopVod(int lVodHandle);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_SetIPCCfg(int lUserID, LPHB_SDVR_IPC_CFG lpCfg, uint dwSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_SetIPCCfg(int lUserID, LPHB_SDVR_IPC_CFG lpCfg, uint dwSize);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_GetIPCCfg(int lUserID, LPHB_SDVR_IPC_CFG lpCfg, uint* pSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_GetIPCCfg(int lUserID, LPHB_SDVR_IPC_CFG lpCfg, ref uint pSize);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_SetIPServer(int lUserID, LPHB_SDVR_IPSERVER lpIPServer);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_SetIPServer(int lUserID, LPHB_SDVR_IPSERVER lpIPServer);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_GetIPServer(int lUserID, LPHB_SDVR_IPSERVER lpIPServer);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_GetIPServer(int lUserID, LPHB_SDVR_IPSERVER lpIPServer);



    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_SetIPCParameterConfig(IN int lUserID, IN const HB_SDVR_IPC_CONFIG* pIpcConfig);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_SetIPCParameterConfig(IN int lUserID, IN HB_SDVR_IPC_CONFIG pIpcConfig);

    // HB_SDVR_API BOOL __stdcall HB_SDVR_GetIPCParameterConfig( LONG lUserID, IN const HB_SDVR_IPC_CMD* pIpcCmd,
    //                                                          OUT HB_SDVR_IPC_CONFIG* pIpcConfig );

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_GetIPCParameterConfig(int lUserID,IN OUT HB_SDVR_IPC_CONFIG* pIpcConfig);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_GetIPCParameterConfig(int lUserID, ref IN OUT HB_SDVR_IPC_CONFIG pIpcConfig);


    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_SetEndecryptPassword(int lUserID, sbyte* pStrPwd);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_SetEndecryptPassword(int lUserID, ref string pStrPwd);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_QueryRecFileByMonth(int lUserID, LPHB_SDVR_QUERY_MONTH lpQuery, LPHB_SDVR_RECFILE_MONTHINFO lpInfo);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_QueryRecFileByMonth(int lUserID, LPHB_SDVR_QUERY_MONTH lpQuery, LPHB_SDVR_RECFILE_MONTHINFO lpInfo);



    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_GetChlParamSupport(IN int lUserID, IN OUT LPSTRUCT_SDVR_COMPRESSINFO_SUPPORT pCompressinfoSupport);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_GetChlParamSupport(IN int lUserID, IN OUT LPSTRUCT_SDVR_COMPRESSINFO_SUPPORT pCompressinfoSupport);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_GetUserInfo(IN int lUserID, OUT PSTRUCT_SDVR_USER_INFO_EX1 pUserInfoEx1);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_GetUserInfo(IN int lUserID, OUT PSTRUCT_SDVR_USER_INFO_EX1 pUserInfoEx1);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_SetUserInfo(IN int lUserID, IN PSTRUCT_SDVR_USER_INFO_EX1 pUserInfoEx1);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_SetUserInfo(IN int lUserID, IN PSTRUCT_SDVR_USER_INFO_EX1 pUserInfoEx1);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_GetDstTime(IN int lUserID, OUT STRUCT_SDVR_DST_TIME_S* pDstTime);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_GetDstTime(IN int lUserID, ref OUT STRUCT_SDVR_DST_TIME_S pDstTime);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_SetDstTime(IN int lUserID, IN STRUCT_SDVR_DST_TIME_S* pDstTime);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_SetDstTime(IN int lUserID, ref IN STRUCT_SDVR_DST_TIME_S pDstTime);


    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_GetOsdCfg(IN int lUserID, IN OUT HB_SDVR_STRUCT_SDVR_OSDCFG* pOsdCfg);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_GetOsdCfg(IN int lUserID, ref IN OUT HB_SDVR_STRUCT_SDVR_OSDCFG pOsdCfg);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_SetOsdCfg(IN int lUserID, IN const HB_SDVR_STRUCT_SDVR_OSDCFG* pOsdCfg);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_SetOsdCfg(IN int lUserID, IN HB_SDVR_STRUCT_SDVR_OSDCFG pOsdCfg);

    //C++ TO C# CONVERTER TODO TASK: The original C++ function pointer contained an unconverted modifier:
    //ORIGINAL LINE: typedef int (CALLBACK *PInitiativeAlarmCallBack)(int lUserID, HB_SDVR_ALARM_REQ Alarm, IntPtr  pContext);
    //public  delegate int PInitiativeAlarmCallBack(int lUserID, HB_SDVR_ALARM_REQ Alarm, IntPtr  pContext);
    //C++ TO C# CONVERTER TODO TASK: The original C++ function pointer contained an unconverted modifier:
    //ORIGINAL LINE: typedef int (CALLBACK *PInitiativeLoginCallBack)(int lUserID, sbyte *sDVRIP, LPHB_SDVR_INITIATIVE_LOGIN pDeviceInfo, IntPtr  pContext);
    public delegate int PInitiativeLoginCallBack(int lUserID, ref string sDVRIP, HB_SDVR_INITIATIVE_LOGIN pDeviceInfo, IntPtr pContext);

    //设置主动登录回调函数
    //pLoginCallBack == NULL关闭主动登录。
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_SetInitiativeLoginCallBack(ushort wDVRPort, PInitiativeLoginCallBack pLoginCallBack, IntPtr  pContext);

    //打开视频请求 主动模式
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_InitiativeRealPlay(int lUserID, uint dwMsgID, LPHB_SDVR_REALPLAYCON pRealPlay);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_InitiativeRealPlay(int lUserID, uint dwMsgID, LPHB_SDVR_REALPLAYCON pRealPlay);

    //远程录像点播扩展 主动模式
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_InitiativePlayBack(int lUserID, uint dwMsgid, LPHB_SDVR_PLAYBACKCON pPlayBack);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_InitiativePlayBack(int lUserID, uint dwMsgid, LPHB_SDVR_PLAYBACKCON pPlayBack);

    //远程录像备份扩展 主动模式
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_InitiativeGetFile(int lUserID, uint dwMsgid, LPHB_SDVR_FILEGETCOND pGetFile);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_InitiativeGetFile(int lUserID, uint dwMsgid, LPHB_SDVR_FILEGETCOND pGetFile);
    //public delegate void fVoiceDataCallBackDelegate(int lVoiceComHandle, ref string pRecvDataBuffer, uint dwBufSize, byte byAudioFlag, uint dwUser);

    //语音对讲协议扩展 主动模式
    //C++ TO C# CONVERTER NOTE: CALLBACK is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_InitiativeStartVoiceCom(int lUserID, uint dwMsgid, void(CALLBACK *fVoiceDataCallBack)(int lVoiceComHandle, sbyte *pRecvDataBuffer,uint dwBufSize,byte byAudioFlag,uint dwUser), uint dwUser);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_InitiativeStartVoiceCom(int lUserID, uint dwMsgid, fVoiceDataCallBackDelegate fVoiceDataCallBack, uint dwUser);

    //主机端抓图扩展 主动模式
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_InitiativeGetPicFromDev(int lUserID, uint dwMsgid, ushort channel,sbyte *sPicFileName);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_InitiativeGetPicFromDev(int lUserID, uint dwMsgid, ushort channel, ref string sPicFileName);

    //DDNS测试
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_TestDDNS(int lUserID);

    //邮件测试
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_TestEMAIL(int lUserID);

    //
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_DONGLE_GetEncInfo(IN int lUserID, OUT STRUCT_SDVR_DONGLEINFO* pDongleInfo);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_DONGLE_GetEncInfo(IN int lUserID, ref OUT STRUCT_SDVR_DONGLEINFO pDongleInfo);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_DONGLE_OpenKey(IN string pKeyPath, OUT IntPtr& pHKey);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_DONGLE_OpenKey(IN string pKeyPath, ref OUT IntPtr pHKey);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_DONGLE_SetEncInfo(IN int lUserID, IN IntPtr hKey, IN STRUCT_SDVR_DONGLE_CHANNEL_INFO* pDongleInfo);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_DONGLE_SetEncInfo(IN int lUserID, IN IntPtr hKey, ref IN STRUCT_SDVR_DONGLE_CHANNEL_INFO pDongleInfo);


    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_DONGLE_SetEncStatus(IN int lUserID, IN STRUCT_SDVR_DONGLE_ENABLE* pDonleEnable);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_DONGLE_SetEncStatus(IN int lUserID, ref IN STRUCT_SDVR_DONGLE_ENABLE pDonleEnable);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_DONGLE_GetEncStatus(IN int lUserID, OUT STRUCT_SDVR_DONGLE_ENABLE* pDonleEnable);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_DONGLE_GetEncStatus(IN int lUserID, ref OUT STRUCT_SDVR_DONGLE_ENABLE pDonleEnable);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_DONGLE_CloseKey(IN IntPtr hKey);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_DONGLE_CloseKey(IN IntPtr hKey);
    //C++ TO C# CONVERTER TODO TASK: There is no equivalent to most C++ 'pragma' directives in C#:
    //#pragma pack()
    // #ifdef __cplusplus
    // };
    // #endif


    //#endif // HB_NETSDK_H
}
////////////////////////////////////////////////////////////////////////////////
// Copyright (C), 2009-2010, Beijing Hanbang Technology, Inc.
////////////////////////////////////////////////////////////////////////////////
//
// File Name:   NetSDK.h
// Author:      
// Version:     3.00
// Date:        
// Description: Header file of 7000SDK.dll and 7000SDK.lib.
// History:
//
////////////////////////////////////////////////////////////////////////////////

// 原汉邦接口
//#if ! HB_NETSDK_H
//#define HB_NETSDK_H

////#include "../include/HBPlaySDK.h"

//#define HB_SDVR_API extern "C"

// #ifdef __cplusplus
// extern "C" {
// #endif

// 宏定义
//#define TM_COM_GUI_BRUSH
//#define TILT_UP
//#define TILT_DOWN
//#define PAN_LEFT
//#define PAN_RIGHT
//#define ZOOM_IN
//#define ZOOM_OUT
//#define IRIS_OPEN
//#define IRIS_CLOSE
//#define FOCUS_FAR
//#define FOCUS_NEAR

//#define LIGHT_PWRON
//#define WIPER_PWRON
//#define PAN_AUTO
//#define SET_PRESET
//#define CLE_PRESET
//#define FAN_PWRON
//#define HEATER_PWRON
//#define AUX_PWRON

//#define FILL_PRE_SEQ
//#define SET_SEQ_DWELL
//#define SET_SEQ_SPEED
//#define CLE_PRE_SEQ
//#define STA_MEM_CRUISE
//#define STO_MEM_CRUISE
//#define RUN_CRUISE
//#define RUN_SEQ
//#define STOP_SEQ
//#define GOTO_PRESET
//#define SYSTEM_RESET

////////////////////

//C++ TO C# CONVERTER NOTE: Enums must be named in C#, so the following enum has been named AnonymousEnum:
public enum AnonymousEnum : int
{
    NORMALMODE = 0,
    OVERLAYMODE
}
#endif


//C++ TO C# CONVERTER NOTE: Enums must be named in C#, so the following enum has been named AnonymousEnum2:
public enum AnonymousEnum2 : int
{
    PTOPTCPMODE,
    PTOPUDPMODE,
    MULTIMODE,
    RTPMODE,
    AUDIODETACH,
    NOUSEMODE
}
#endif

/*

#define HB_SDVR_SYSHEAD
#define HB_SDVR_STREAMDATA
#define HB_SDVR_BACKUPEND

//播放控制命令宏定义 HB_SDVR_PlayBackControl,HB_SDVR_PlayControlLocDisplay,HB_SDVR_DecPlayBackCtrl的宏定义
#define HB_SDVR_PLAYSTART
#define HB_SDVR_PLAYSTOP
#define HB_SDVR_PLAYPAUSE
#define HB_SDVR_PLAYRESTART
#define HB_SDVR_PLAYFAST
#define HB_SDVR_PLAYSLOW
#define HB_SDVR_PLAYBACK
#define HB_SDVR_PLAYNORMAL
#define HB_SDVR_PLAYFRAME
#define HB_SDVR_PLAYSTARTAUDIO
#define HB_SDVR_PLAYSTOPAUDIO
#define HB_SDVR_PLAYAUDIOVOLUME
#define HB_SDVR_PLAYGETPOS
#define HB_SDVR_PLAYBYSLIDER

#define HB_SDVR_SET_SERIALID
#define HB_SDVR_GET_SERIALID
#define HB_SDVR_GET_VLostStatus


//HB_SDVR_GetDVRConfig,HB_SDVR_GetDVRConfig的命令定义
#define HB_SDVR_GET_DEVICECFG
#define HB_SDVR_SET_DEVICECFG
#define HB_SDVR_GET_NETCFG
#define HB_SDVR_SET_NETCFG
#define HB_SDVR_GET_PICCFG
#define HB_SDVR_SET_PICCFG
#define HB_SDVR_GET_COMPRESSCFG
#define HB_SDVR_SET_COMPRESSCFG
#define HB_SDVR_GET_RECORDCFG
#define HB_SDVR_SET_RECORDCFG
#define HB_SDVR_GET_DECODERCFG
#define HB_SDVR_SET_DECODERCFG
#define HB_SDVR_GET_RS232CFG
#define HB_SDVR_SET_RS232CFG
#define HB_SDVR_GET_ALARMINCFG
#define HB_SDVR_SET_ALARMINCFG
//兼容最多128路机器 
#define HB_SDVR_GET_ALARMINCFG_NVS
#define HB_SDVR_SET_ALARMINCFG_NVS

#define HB_SDVR_GET_ALARMOUTCFG
#define HB_SDVR_SET_ALARMOUTCFG
#define HB_SDVR_GET_TIMECFG
#define HB_SDVR_SET_TIMECFG
#define HB_SDVR_GET_PREVIEWCFG
#define HB_SDVR_SET_PREVIEWCFG
#define HB_SDVR_GET_VIDEOOUTCFG
#define HB_SDVR_SET_VIDEOOUTCFG
#define HB_SDVR_GET_USERCFG
#define HB_SDVR_SET_USERCFG
//兼容最多128路机器类型的扩展命令
#define HB_SDVR_GET_USERCFG_NVS
#define HB_SDVR_SET_USERCFG_NVS

#define HB_SDVR_GET_EXCEPTIONCFG
#define HB_SDVR_SET_EXCEPTIONCFG
//叠加字符
#define HB_SDVR_GET_SHOWSTRING
#define HB_SDVR_SET_SHOWSTRING
//GE ODM
#define HB_SDVR_GET_EVENTCOMPCFG
#define HB_SDVR_SET_EVENTCOMPCFG

#define HB_SDVR_GET_FTPCFG
#define HB_SDVR_SET_FTPCFG
#define HB_SDVR_GET_JPEGCFG
#define HB_SDVR_SET_JPEGCFG
//北京华纬讯
#define HB_SDVR_GET_PPPOECFG
#define HB_SDVR_SET_PPPOECFG
//HS设备辅助输出
#define HB_SDVR_GET_AUXOUTCFG
#define HB_SDVR_SET_AUXOUTCFG

#define HB_SDVR_GET_PICCFG_EX
#define HB_SDVR_SET_PICCFG_EX

#define HB_SDVR_GET_PICCFG_EX_NVS
#define HB_SDVR_SET_PICCFG_EX_NVS

//SDK_V15 扩展命令
#define HB_SDVR_GET_USERCFG_EX
#define HB_SDVR_SET_USERCFG_EX
#define HB_SDVR_GET_DNS
#define HB_SDVR_SET_DNS
#define HB_SDVR_GET_DNS_NVS
#define HB_SDVR_SET_DNS_NVS
#define HB_SDVR_GET_PPPoE
#define HB_SDVR_SET_PPPoE


#define HB_SDVR_SERVERCFG_GET
#define HB_SDVR_SERVERCFG_SET


//消息方式
//异常类型
#define EXCEPTION_EXCHANGE
#define EXCEPTION_AUDIOEXCHANGE
#define EXCEPTION_ALARM
#define EXCEPTION_PREVIEW
#define EXCEPTION_SERIAL
#define EXCEPTION_RECONNECT

#define NAME_LEN
#define SERIALNO_LEN
#define MACADDR_LEN
#define MAX_ETHERNET
#define PATHNAME_LEN
#define PASSWD_LEN
#define MAX_CHANNUM
#define MAX_CHANNUM_EX
#define MAX_ALARMOUT
#define MAX_ALARMOUT_EX
#define MAX_TIMESEGMENT
#define MAX_PRESET
#define MAX_DAYS
#define PHONENUMBER_LEN
#define MAX_DISKNUM
#define MAX_WINDOW
#define MAX_VGA
#define MAX_USERNUM
#define MAX_EXCEPTIONNUM
#define MAX_LINK
#define MAX_ALARMIN
#define MAX_ALARMIN_EX
#define MAX_VIDEOOUT
#define MAX_NAMELEN
#define MAX_RIGHT
#define CARDNUM_LEN
#define MAX_SHELTERNUM
#define MAX_DECPOOLNUM
#define MAX_DECNUM
#define MAX_TRANSPARENTNUM
#define MAX_STRINGNUM
#define MAX_AUXOUT

// 网络接口定义 
#define NET_IF_10M_HALF
#define NET_IF_10M_FULL
#define NET_IF_100M_HALF
#define NET_IF_100M_FULL
#define NET_IF_AUTO

//设备类型		
#define DVR
#define ATMDVR
#define DVS
#define DEC
#define ENC_DEC
#define DVR_HC
#define DVR_HT
#define DVR_HF
#define DVR_HS
#define DVR_HTS
#define DVR_HB
#define DVR_HCS
#define DVS_A
#define MAX_LOG_NUM
/////
#define MFS_REC_TYPE_MANUAL
#define MFS_REC_TYPE_SCHEDULE
#define MFS_REC_TYPE_MOTION
#define MFS_REC_TYPE_ALARM
#define MFS_REC_TYPE_ALL
////////
#define MAX_REC_NUM
//处理方式
#define NOACTION
#define WARNONMONITOR
#define WARNONAUDIOOUT
#define UPTOCENTER
#define TRIGGERALARMOUT









// PTZ type 
#define YOULI
#define LILIN_1016
#define LILIN_820
#define PELCO_P
#define DM_QUICKBALL
#define HD600
#define JC4116
#define PELCO_DWX
#define PELCO_D
#define VCOM_VC_2000
#define NETSTREAMER
#define SAE
#define SAMSUNG
#define KALATEL_KTD_312
#define CELOTEX
#define TLPELCO_P
#define TL_HHX2000
#define BBV
#define RM110
#define KC3360S
#define ACES
#define ALSON
#define INV3609HD
#define HOWELL
#define TC_PELCO_P
#define TC_PELCO_D
#define AUTO_M
#define AUTO_H
#define ANTEN
#define CHANGLIN
#define DELTADOME
#define XYM_12
#define ADR8060
#define EVI
#define Demo_Speed
#define DM_PELCO_D
#define ST_832
#define LC_D2104
#define HUNTER
#define A01
#define TECHWIN
#define WEIHAN
#define LG
#define D_MAX
#define PANASONIC
#define KTD_348
#define INFINOVA
#define PIH717
#define IDOME_IVIEW_LCU
#define DENNARD_DOME
#define PHLIPS
#define SAMPLE
#define PLD
#define PARCO
#define HY
#define NAIJIE
#define CAT_KING
#define YH_06
#define SP9096X
#define M_PANEL
#define M_MV2050
#define SAE_QUICKBALL
#define RED_APPLE
#define NKO8G
#define DH_CC440
///////////////////////////////////////
//DVR日志
// 报警 
//主类型
#define MAJOR_ALARM
//次类型
#define MINOR_ALARM_IN
#define MINOR_ALARM_OUT
#define MINOR_MOTDET_START
#define MINOR_MOTDET_STOP
#define MINOR_HIDE_ALARM_START
#define MINOR_HIDE_ALARM_STOP

// 异常 
//主类型
#define MAJOR_EXCEPTION
//次类型
#define MINOR_VI_LOST
#define MINOR_ILLEGAL_ACCESS
#define MINOR_HD_FULL
#define MINOR_HD_ERROR
#define MINOR_DCD_LOST
#define MINOR_IP_CONFLICT
#define MINOR_NET_BROKEN
// 操作 
//主类型
#define MAJOR_OPERATION
//次类型
#define MINOR_START_DVR
#define MINOR_STOP_DVR
#define MINOR_STOP_ABNORMAL

#define MINOR_LOCAL_LOGIN
#define MINOR_LOCAL_LOGOUT
#define MINOR_LOCAL_CFG_PARM
#define MINOR_LOCAL_PLAYBYFILE
#define MINOR_LOCAL_PLAYBYTIME
#define MINOR_LOCAL_START_REC
#define MINOR_LOCAL_STOP_REC
#define MINOR_LOCAL_PTZCTRL
#define MINOR_LOCAL_PREVIEW
#define MINOR_LOCAL_MODIFY_TIME
#define MINOR_LOCAL_UPGRADE
#define MINOR_LOCAL_COPYFILE

#define MINOR_REMOTE_LOGIN
#define MINOR_REMOTE_LOGOUT
#define MINOR_REMOTE_START_REC
#define MINOR_REMOTE_STOP_REC
#define MINOR_START_TRANS_CHAN
#define MINOR_STOP_TRANS_CHAN
#define MINOR_REMOTE_GET_PARM
#define MINOR_REMOTE_CFG_PARM
#define MINOR_REMOTE_GET_STATUS
#define MINOR_REMOTE_ARM
#define MINOR_REMOTE_DISARM
#define MINOR_REMOTE_REBOOT
#define MINOR_START_VT
#define MINOR_STOP_VT
#define MINOR_REMOTE_UPGRADE
#define MINOR_REMOTE_PLAYBYFILE
#define MINOR_REMOTE_PLAYBYTIME
#define MINOR_REMOTE_PTZCTRL*/
//////////////////////
public enum BUSZZERSTATE : int
{
    BUZZER_CLOSE,
    BUZZER_OPEN
}
////////////////////////////////////////
//#define INFO_LEN
//#define INFO_SEQ
////////////////////////////////
//#define COMM_ALARM
//#define COMM_CONNECT
//////////////////////////////
//#define PRESETNUM
////////////////////////////
//DS-6001D/F 
//解码设备控制码定义
//#define NET_DEC_STARTDEC
//#define NET_DEC_STOPDEC
//#define NET_DEC_STOPCYCLE
//#define NET_DEC_CONTINUECYCLE
////////////////////////////////////////////
////
//#define MAX_KEYNUM
///////////////////////////////////
////ptz协议
//#define MAXPTZNUM
/////////////////////////////////////////////////////

//////////////////////////////////////////////
//#define MAX_SMTP_HOST
//#define MAX_SMTP_ADDR
//#define MAX_STRING
/////////////////////////////////////////////

//#define PT_ATMI_MAX_ALARM_NUM
//// 错误码
//#define HB_SDVR_NOERROR
//#define HB_SDVR_PASSWORD_ERROR -1
//#define HB_SDVR_NOENOUGHPRI -2
//#define HB_SDVR_NOINIT -3
//#define HB_SDVR_CHANNEL_ERROR -4
//#define HB_SDVR_OVER_MAXLINK -5
//#define HB_SDVR_VERSIONNOMATCH -6
//#define HB_SDVR_NETWORK_FAIL_CONNECT -7
//#define HB_SDVR_NETWORK_SEND_ERROR -8
//#define HB_SDVR_NETWORK_RECV_ERROR -9
//#define HB_SDVR_NETWORK_RECV_TIMEOUT -10
//#define HB_SDVR_NETWORK_ERRORDATA -11
//#define HB_SDVR_ORDER_ERROR -12
//#define HB_SDVR_OPERNOPERMIT -13
//#define HB_SDVR_COMMANDTIMEOUT -14
//#define HB_SDVR_ERRORSERIALPORT -15
//#define HB_SDVR_ERRORALARMPORT -16
//#define HB_SDVR_PARAMETER_ERROR -17
//#define HB_SDVR_CHAN_EXCEPTION -18
//#define HB_SDVR_NODISK -19
//#define HB_SDVR_ERRORDISKNUM -20
//#define HB_SDVR_DISK_FULL -21
//#define HB_SDVR_DISK_ERROR -22
//#define HB_SDVR_NOSUPPORT -23
//#define HB_SDVR_BUSY -24
//#define HB_SDVR_MODIFY_FAIL -25
//#define HB_SDVR_PASSWORD_FORMAT_ERROR -26
//#define HB_SDVR_DISK_FORMATING -27
//#define HB_SDVR_DVRNORESOURCE -28
//#define HB_SDVR_DVROPRATEFAILED -29
//#define HB_SDVR_OPENHOSTSOUND_FAIL -30
//#define HB_SDVR_DVRVOICEOPENED -31
//#define HB_SDVR_TIMEINPUTERROR -32
//#define HB_SDVR_NOSPECFILE -33
//#define HB_SDVR_CREATEFILE_ERROR -34
//#define HB_SDVR_FILEOPENFAIL -35
//#define HB_SDVR_OPERNOTFINISH -36
//#define HB_SDVR_GETPLAYTIMEFAIL -37
//#define HB_SDVR_PLAYFAIL -38
//#define HB_SDVR_FILEFORMAT_ERROR -39
//#define HB_SDVR_DIR_ERROR -40
//#define HB_SDVR_ALLOC_RESOUCE_ERROR -41
//#define HB_SDVR_AUDIO_MODE_ERROR -42
//#define HB_SDVR_NOENOUGH_BUF -43
//#define HB_SDVR_CREATESOCKET_ERROR -44
//#define HB_SDVR_SETSOCKET_ERROR -45
//#define HB_SDVR_MAX_NUM -46
//#define HB_SDVR_USERNOTEXIST -47
//#define HB_SDVR_WRITEFLASHERROR -48
//#define HB_SDVR_UPGRADEFAIL -49
//#define HB_SDVR_CARDHAVEINIT -50
//#define HB_SDVR_PLAYERFAILED -51
//#define HB_SDVR_MAX_USERNUM -52
//#define HB_SDVR_GETLOCALIPANDMACFAIL -53
//#define HB_SDVR_NOENCODEING -54
//#define HB_SDVR_IPMISMATCH -55
//#define HB_SDVR_MACMISMATCH -56
//#define HB_SDVR_UPGRADELANGMISMATCH -57
//#define HB_SDVR_USERISALIVE -58
//#define HB_SDVR_UNKNOWNERROR -59
//#define HB_SDVR_KEYVERIFYFAIL -60
//#define HB_SDVR_IPERR -101
//#define HB_SDVR_MACERR -102
//#define HB_SDVR_PSWERR -103
//#define HB_SDVR_USERERR -104
//#define HB_SDVR_USERISFULL -105
//#define NO_PERMISSION

////查找文件和日志函数返回值
//#define HB_SDVR_FILE_SUCCESS
//#define HB_SDVR_FILE_NOFIND
//#define HB_SDVR_ISFINDING
//#define HB_SDVR_NOMOREFILE
//#define HB_SDVR_FILE_EXCEPTION
// 云台控制命令

// 回放控制命令

// 参数配置命令

//C++ TO C# CONVERTER TODO TASK: There is no equivalent to most C++ 'pragma' directives in C#:
//#pragma pack(1)
//////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////
// 参数配置结构体定义
//为世纪瑞尔需要详细帧信息添加
public class FRAMEINFO
{
    public int frame_type; //帧类型 1：I帧 2：P帧 8：音频帧
                           //C++ TO C# CONVERTER NOTE: Classes must be named in C#, so the following class has been named AnonymousClass:
    public class AnonymousClass
    {
        public short year;
        public short month;
        public short day;
        public short hour;
        public short minute;
        public short second;
        public short milli;
        public short res1; //保留
    } //时间
    public AnonymousClass ets = new AnonymousClass();
    public uint frame_num; //帧号
    public uint width; //图像宽度
    public uint height; //图像高度
    public short frame_rate; //帧率
    public short reserve1; //保留
    public int reserve2; //保留
}
//通道参数
public class HB_SDVR_HANDLEEXCEPTION
{
    public uint dwHandleType; //按位 2-声音报警 5-监视器最大化
    public ushort wAlarmOut; //报警输出触发通道 按位对应通道
}
public class HB_SDVR_HANDLEEXCEPTION_EX
{
    public uint dwHandleType; //按位 2-声音报警 5-监视器最大化 6-邮件上传 7-音频报警
    public byte[] wAlarmOut = new byte[HBConst.MAX_CHANNUM_EX]; //报警输出触发通道 按位对应通道
}

public class HB_SDVR_VODLOST
{
    public byte[] wVodLost = new byte[HBConst.MAX_CHANNUM_EX]; //对应通道 0：视频丢失 1：有视频
    public uint dwres; //保留
}

//上传报警信息
public class HB_SDVR_ALARMINFO
{
    public byte[] byAlarm = new byte[HBConst.MAX_CHANNUM]; //探头报警
    public byte[] byVlost = new byte[HBConst.MAX_CHANNUM]; //信号丢失
    public byte[] byMotion = new byte[HBConst.MAX_CHANNUM]; //移动报警
    public byte[] byHide = new byte[HBConst.MAX_CHANNUM]; //遮挡报警
    public byte[] byDisk = new byte[HBConst.MAX_DISKNUM]; //硬盘状态
}

public class HB_SDVR_ALARMINFO_EX
{
    public byte[] byAlarm = new byte[HBConst.MAX_CHANNUM_EX]; //探头报警 0-无报价 1-有报警
    public byte[] byVlost = new byte[HBConst.MAX_CHANNUM_EX]; //视频丢失 ...
    public byte[] byMotion = new byte[HBConst.MAX_CHANNUM_EX]; //移动报警 ...
    public byte[] byHide = new byte[HBConst.MAX_CHANNUM_EX]; //遮挡报警 ...
    public byte[] byDisk = new byte[HBConst.MAX_DISKNUM]; //硬盘状态
}

public struct HB_SDVR_TIME
{
    public uint dwYear; //年
    public uint dwMonth; //月
    public uint dwDay; //日
    public uint dwHour; //时
    public uint dwMinute; //分
    public uint dwSecond; //秒
}

public struct HB_SDVR_SCHEDTIME
{
    public byte byEnable; //激活
                          //开始时间
    public byte byStartHour;
    public byte byStartMin;
    //结束时间
    public byte byStopHour;
    public byte byStopMin;
}

//  保留
public class HB_SDVR_ALARMOUTSTATUS
{
    public byte[] Output = new byte[HBConst.MAX_ALARMOUT];
}

//图片质量 保留
public class HB_SDVR_JPEGPARA
{
    public ushort wPicSize; // 0=CIF, 1=QCIF, 2=D1 
    public ushort wPicQuality; // 图片质量系数 0-最好 1-较好 2-一般 
}

public struct HB_SDVR_DEVICEINFO
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = HBConst.SERIALNO_LEN)]
    public byte[] sSerialNumber; //保留
    public byte byAlarmInPortNum; //DVR报警输入个数
    public byte byAlarmOutPortNum; //DVR报警输出个数
    public byte byDiskNum; //DVR 硬盘个数
    public byte byProtocol; //新类型产品该值定为0x20，按协议二处理
    public byte byChanNum; //DVR 通道个数
    public byte byStartChan; //保留
    [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = HBConst.NAME_LEN)]
    public byte[] sDvrName; //主机名
    [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = HBConst.MAX_CHANNUM * HBConst.NAME_LEN)]
    public byte[,] sChanName; //通道名称
}


//add by cui for 7024 or nvs 100325
public struct HB_SDVR_DEVICEINFO_EX
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = HBConst.SERIALNO_LEN)]
    public byte[] sSerialNumber; //序列号：主机端必须返回，从前往后处理，其余补零
    public byte byAlarmInPortNum; //DVR报警输入个数
    public byte byAlarmOutPortNum; //DVR报警输出个数
    public byte byDiskNum; //存储设备个数：硬盘/SD卡个数
    public byte byProtocol; //协议版本 0x20
    public byte byChanNum; //DVR 通道个数
    public byte byEncodeType; //主机编码格式：1为ANSI字符串，中文采用GB2312编码；2为UTF8
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = HBConst.NAME_LEN)]
    public byte[] sDvrName; //主机名：须以’\0’结束,与主机编码格式有关
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = HBConst.MAX_CHANNUM_EX * HBConst.NAME_LEN)]
    public byte[,] sChanName; //通道名称：须以’\0’结束,与主机编码格式有关
}
//end add

//设备硬件信息
public class HB_SDVR_DEVICECFG
{
    public uint dwSize;
    public byte[] sDVRName = new byte[HBConst.NAME_LEN]; //DVR名称
    public uint dwDVRID; //保留
    public uint dwRecycleRecord; //保留
                                 //以下不能更改
    public byte[] sSerialNumber = new byte[HBConst.SERIALNO_LEN]; //保留
    public byte[] sSoftwareVersion = new byte[16]; //软件版本号
    public byte[] sSoftwareBuildDate = new byte[16]; //软件生成日期
    public uint dwDSPSoftwareVersion; //DSP软件版本
                                      //	BYTE sDSPSoftwareBuildDate[16];		// DSP软件生成日期
    public byte[] sPanelVersion = new byte[16]; // 前面板版本
    public byte[] sHardwareVersion = new byte[16]; //保留
    public byte byAlarmInPortNum; //DVR报警输入个数
    public byte byAlarmOutPortNum; //DVR报警输出个数
    public byte byRS232Num; //保留
    public byte byRS485Num; //保留
    public byte byNetworkPortNum; //保留
    public byte byDiskCtrlNum; //保留
    public byte byDiskNum; //DVR 硬盘个数
    public byte byDVRType; //DVR类型, 1:DVR 2:ATM DVR 3:DVS 建议使用HB_SDVR_GetDeviceType
    public byte byChanNum; //DVR 通道个数
    public byte byStartChan; //保留
    public byte byDecordChans; //保留
    public byte byVGANum; //保留
    public byte byUSBNum; //保留
    public string reservedData = new string(new char[3]); //保留
}

//设备网络信息
public class HB_SDVR_ETHERNET
{
    public string sDVRIP = new string(new char[16]); //DVR IP地址
    public string sDVRIPMask = new string(new char[16]); //DVR IP地址掩码
    public uint dwNetInterface; //网络接口 1-10MBase-T 2-10MBase-T全双工 3-100MBase-TX 4-100M全双工 5-10M/100M自适应 6-100M半双工 7-1000M半双工
                                // 8-1000M全双工 9-100M/1000M自适应 10-10000M半双工 11-10000M全双工 12-100M/1000M/10000M自适应(扩展)
    public ushort wDVRPort; //端口号
    public byte[] byMACAddr = new byte[HBConst.MACADDR_LEN]; //服务器的物理地址
}

//网络配置结构
public class HB_SDVR_NETCFG
{
    public uint dwSize;
    public HB_SDVR_ETHERNET[] struEtherNet = new HB_SDVR_ETHERNET[HBConst.MAX_ETHERNET]; // 以太网口 
    public string sManageHostIP = new string(new char[16]); //远程管理主机地址
    public ushort wManageHostPort; //保留
    public string sDNSIP = new string(new char[16]); //DNS服务器地址
    public string sMultiCastIP = new string(new char[16]); //多播组地址
    public string sGatewayIP = new string(new char[16]); //网关地址
    public string sNFSIP = new string(new char[16]); //保留
    public byte[] sNFSDirectory = new byte[HBConst.PATHNAME_LEN]; //保留
    public uint dwPPPOE; //0-不启用,1-启用
    public byte[] sPPPoEUser = new byte[HBConst.NAME_LEN]; //PPPoE用户名
    public string sPPPoEPassword = new string(new char[HBConst.PASSWD_LEN]); // PPPoE密码
    public string sPPPoEIP = new string(new char[16]); //PPPoE IP地址
    public ushort wHttpPort; //HTTP端口号
}

public struct HB_SDVR_CLIENTINFO
{
    public int lChannel; //通道号
    public int lLinkMode; //最高位(31)为0表示主码流，为1表示子码流，0－30位表示码流连接方式: 0：TCP方式,1：UDP方式,2：多播方式
    public IntPtr hPlayWnd; //播放窗口的句柄,为NULL表示不播放图象
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
    public char[] sMultiCastIP; //保留
}


public class HB_SDVR_CLIENTINFO_EX
{
    public int lChannel; //通道号
    public int lLinkMode; //最高位(31)为0表示主码流，为1表示子码流，0－30位表示码流连接方式: 0：TCP方式,1：UDP方式,2：多播方式
    public IntPtr hPlayWnd; //播放窗口的句柄,为NULL表示不播放图象
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
    public char[] sMultiCastIP; //保留
    public uint msgid;
}




public class HB_SDVR_SCHEDULE_VIDEOPARAM
{
    public ushort wStartTime; //高8位表示小时 低8位表示分钟
    public ushort wEndTime; //高8位表示小时 低8位表示分钟
    public HB_SDVR_VIDEOPARAM VideoParam = new HB_SDVR_VIDEOPARAM();
}



public class HB_SDVR_REMOTERECORDCHAN
{
    public byte[] Channel = new byte[HBConst.MAX_CHANNUM_EX];
}

public enum HB_SDVR_TYPE_E : int
{
    NET_DEVTYPE_7000T = 0,
    NET_DEVTYPE_8000T,
    NET_DEVTYPE_8200T,
    NET_DEVTYPE_8000ATM,
    NET_DEVTYPE_8600T, //8600T
    NET_DEVTYPE_6200T,
    NET_DEVTYPE_8004AH,
    NET_DEVTYPE_8004AI,
    NET_DEVTYPE_7000H,
    NET_DEVTYPE_7200H,
    NET_DEVTYPE_7000M = 12,
    NET_DEVTYPE_8000M,
    NET_DEVTYPE_8200M,
    NET_DEVTYPE_7000L,
    NET_DEVTYPE_2201TL = 16,
    NET_DEVTYPE_2600T,
    NET_DEVTYPE_2600TB, //人流统计智能分析盒
    NET_DEVTYPE_2600TC, //车牌识别智能分析盒
    NET_DEVTYPE_9300,
    NET_DEVTYPE_9400,

    HB9824N16H = 1000,
    HB9832N16H,
    HB9904,
    HB9908,
    HB9912,
    HB9916,
    HB9932,
    HB7904,
    HB7908,
    HB7912,
    HB7916,
}

public enum HB_SDVR_MEMSIZE_E : int
{
    NET_SIZE_8M = 0, // 8M
    NET_SIZE_16M, // 16M
    NET_SIZE_32M, // 32M
    NET_SIZE_64M, // 64M
    NET_SIZE_128M, // 128M
    NET_SIZE_256M, // 256M
    NET_SIZE_512M, // 512M
    NET_SIZE_1024M, // 1024M
}

public class HB_SDVR_INFO
{
    public uint dvrtype; // 7004 8004 2004机器型号
    public ushort dwDevice_type; // HB_SDVR_TYPE_E
    public ushort memory_size; // HB_SDVR_MEMSIZE_E
    public uint dwReserve;
}

//// 移动侦测区域，分22*18个小宏块
//#define MOTION_SCOPE_WIDTH
//#define MOTION_SCOPE_HIGHT
//通道图象结构
//移动侦测
public class HB_SDVR_MOTION
{
    public byte[,] byMotionScope = new byte[18, 22]; //侦测区域,共有22*18个小宏块,为1表示该宏块是移动侦测区域,0-表示不是
    public byte byMotionSensitive; //移动侦测灵敏度, 0 - 5,越高越灵敏,0xff关闭*/
    public byte byEnableHandleMotion; // 是否处理移动侦测 */
    public HB_SDVR_HANDLEEXCEPTION struMotionHandleType = new HB_SDVR_HANDLEEXCEPTION(); // 处理方式 */
    public HB_SDVR_SCHEDTIME[,] struAlarmTime = new HB_SDVR_SCHEDTIME[HBConst.MAX_DAYS, HBConst.MAX_TIMESEGMENT]; //布防时间
    public byte[] byRelRecordChan = new byte[HBConst.MAX_CHANNUM]; //报警触发的录象通道,为1表示触发该通道
}

//add by cui for 7024 or nvs 100329
public class HB_SDVR_MOTION_EX
{
    public byte[,] byMotionScope = new byte[18, 22]; //侦测区域,共有22*18个小宏块,为1表示该宏块是移动侦测区域,0-表示不是
    public byte byMotionSensitive; //移动侦测灵敏度, 0 - 5,越高越灵敏,0xff关闭*/
    public byte byEnableHandleMotion; // 是否处理移动侦测 */
    public HB_SDVR_HANDLEEXCEPTION_EX struMotionHandleType = new HB_SDVR_HANDLEEXCEPTION_EX(); // 处理方式 */
    public HB_SDVR_SCHEDTIME[,] struAlarmTime = new HB_SDVR_SCHEDTIME[HBConst.MAX_DAYS, HBConst.MAX_TIMESEGMENT]; //布防时间
    public byte[] byRelRecordChan = new byte[HBConst.MAX_CHANNUM_EX]; //报警触发的录象通道,为1表示触发该通道
}
//end add

//遮挡报警区域为 
public class HB_SDVR_HIDEALARM
{
    public uint dwEnableHideAlarm; //保留
    public ushort wHideAlarmAreaTopLeftX; //保留
    public ushort wHideAlarmAreaTopLeftY; //保留
    public ushort wHideAlarmAreaWidth; //保留
    public ushort wHideAlarmAreaHeight; //保留
    public HB_SDVR_HANDLEEXCEPTION struHideAlarmHandleType = new HB_SDVR_HANDLEEXCEPTION(); //保留
    public HB_SDVR_SCHEDTIME[,] struAlarmTime = new HB_SDVR_SCHEDTIME[HBConst.MAX_DAYS, HBConst.MAX_TIMESEGMENT]; //保留
}

//遮挡报警区域为 
public class HB_SDVR_HIDEALARM_EX
{
    public uint dwEnableHideAlarm; //保留
    public ushort wHideAlarmAreaTopLeftX; //保留
    public ushort wHideAlarmAreaTopLeftY; //保留
    public ushort wHideAlarmAreaWidth; //保留
    public ushort wHideAlarmAreaHeight; //保留
    public HB_SDVR_HANDLEEXCEPTION_EX struHideAlarmHandleType = new HB_SDVR_HANDLEEXCEPTION_EX(); //保留
    public HB_SDVR_SCHEDTIME[,] struAlarmTime = new HB_SDVR_SCHEDTIME[HBConst.MAX_DAYS, HBConst.MAX_TIMESEGMENT]; //保留
}

//信号丢失报警
public class HB_SDVR_VILOST
{
    public byte byEnableHandleVILost; // 是否处理信号丢失报警
    public HB_SDVR_HANDLEEXCEPTION strVILostHandleType = new HB_SDVR_HANDLEEXCEPTION(); //处理方式
    public HB_SDVR_SCHEDTIME[,] struAlarmTime = new HB_SDVR_SCHEDTIME[HBConst.MAX_DAYS, HBConst.MAX_TIMESEGMENT]; //保留
}

public class HB_SDVR_VILOST_EX
{
    public byte byEnableHandleVILost; // 是否处理信号丢失报警
    public HB_SDVR_HANDLEEXCEPTION_EX strVILostHandleType = new HB_SDVR_HANDLEEXCEPTION_EX(); //处理方式
    public HB_SDVR_SCHEDTIME[,] struAlarmTime = new HB_SDVR_SCHEDTIME[HBConst.MAX_DAYS, HBConst.MAX_TIMESEGMENT]; //保留
}

public class HB_SDVR_SHELTER
{
    public ushort wHideAreaTopLeftX; // 遮盖区域的x坐标
    public ushort wHideAreaTopLeftY; // 遮盖区域的y坐标
    public ushort wHideAreaWidth; // 遮盖区域的宽
    public ushort wHideAreaHeight; //遮盖区域的高
}


public class HB_SDVR_PICCFG_EX
{
    public uint dwSize;
    public byte[] sChanName = new byte[HBConst.NAME_LEN]; // 通道名
    public uint dwVideoFormat; // 保留
    public byte byBrightness; // 保留
    public byte byContrast; // 保留
    public byte bySaturation; // 保留
    public byte byHue; // 保留
                       //显示通道名
    public uint dwShowChanName; // 保留
    public ushort wShowNameTopLeftX; // 通道名称显示位置的x坐标
    public ushort wShowNameTopLeftY; // 通道名称显示位置的y坐标
    public HB_SDVR_VILOST struVILost = new HB_SDVR_VILOST(); // 信号丢失报警
    public HB_SDVR_MOTION struMotion = new HB_SDVR_MOTION(); // 移动侦测
    public HB_SDVR_HIDEALARM struHideAlarm = new HB_SDVR_HIDEALARM(); // 保留
    public uint dwEnableHide; // 是否启动遮盖 ,0-否,1-是
    public HB_SDVR_SHELTER[] struShelter = new HB_SDVR_SHELTER[HBConst.MAX_SHELTERNUM];
    public uint dwShowOsd; // 保留
    public ushort wOSDTopLeftX; // 保留
    public ushort wOSDTopLeftY; // 保留
    public byte byOSDType; // 保留
    public byte byDispWeek; // 是否显示星期 
    public byte byOSDAttrib; //通道名 1-不透明 2-透明

}

//add by cui for 7024 or nvs 100329
public class HB_SDVR_PICCFG_EX_EX
{
    public uint dwSize;
    public byte[] sChanName = new byte[HBConst.NAME_LEN]; // 通道名
    public uint dwVideoFormat; // 保留
    public byte byBrightness; // 保留
    public byte byContrast; // 保留
    public byte bySaturation; // 保留
    public byte byHue; // 保留
                       //显示通道名
    public uint dwShowChanName; // 保留
    public ushort wShowNameTopLeftX; // 通道名称显示位置的x坐标
    public ushort wShowNameTopLeftY; // 通道名称显示位置的y坐标
    public HB_SDVR_VILOST_EX struVILost = new HB_SDVR_VILOST_EX(); // 信号丢失报警
    public HB_SDVR_MOTION_EX struMotion = new HB_SDVR_MOTION_EX(); // 移动侦测
    public HB_SDVR_HIDEALARM_EX struHideAlarm = new HB_SDVR_HIDEALARM_EX(); // 保留
    public uint dwEnableHide; // 是否启动遮盖 ,0-否,1-是
    public HB_SDVR_SHELTER[] struShelter = new HB_SDVR_SHELTER[HBConst.MAX_SHELTERNUM];
    public uint dwShowOsd; // 保留
    public ushort wOSDTopLeftX; // 保留
    public ushort wOSDTopLeftY; // 保留
    public byte byOSDType; // 格式及语言，最高位为0表示解码后叠加，为1表示前端叠加；默认为0，设为0x80时表示将osd设为前端叠加
    public byte byDispWeek; // 是否显示星期 
    public byte byOSDAttrib; //通道名 1-不透明 2-透明

}
//end add

//兼容只有一个遮挡区域客户端版本 20100504
public class HB_SDVR_PICCFG_EX_TMP
{
    public uint dwSize;
    public byte[] sChanName = new byte[HBConst.NAME_LEN]; // 通道名
    public uint dwVideoFormat; // 保留
    public byte byBrightness; // 保留
    public byte byContrast; // 保留
    public byte bySaturation; // 保留
    public byte byHue; // 保留
                       //显示通道名
    public uint dwShowChanName; // 保留
    public ushort wShowNameTopLeftX; // 通道名称显示位置的x坐标
    public ushort wShowNameTopLeftY; // 通道名称显示位置的y坐标
    public HB_SDVR_VILOST struVILost = new HB_SDVR_VILOST(); // 信号丢失报警
    public HB_SDVR_MOTION struMotion = new HB_SDVR_MOTION(); // 移动侦测
    public HB_SDVR_HIDEALARM struHideAlarm = new HB_SDVR_HIDEALARM(); // 保留
    public uint dwEnableHide; // 是否启动遮盖 ,0-否,1-是
    public HB_SDVR_SHELTER[] struShelter = new HB_SDVR_SHELTER[1];
    public uint dwShowOsd; // 保留
    public ushort wOSDTopLeftX; // 保留
    public ushort wOSDTopLeftY; // 保留
    public byte byOSDType; // 保留
    public byte byDispWeek; // 是否显示星期 
    public byte byOSDAttrib; //通道名 1-不透明 2-透明

}

//编码压缩参数
//压缩参数
public class HB_SDVR_COMPRESSION_INFO
{
    public byte byStreamType; //码流类型0-无音频,1-有音频
    public byte byResolution; //分辨率 0-CIF 1-HD1, 2-D1；协议二:增加 3-QCIF、 4-720p、5-1080p、6-960H、7-Q960H、8-QQ960H
    public byte byBitrateType; //码率类型 0:变码率，1:定码率 2：定画质
    public byte byPicQuality; //图象质量 1-最好 2-次好 3-较好 4-一般5-较差 6-差
    public uint dwVideoBitrate; //协议一:视频码率 0-100K 1-128K，2-256K，3-512K，4-1M，5-2M，6-3M，7-4M，8-6M，9-8M，10-12M ,11-自定义
                                //协议二:视频码率 0-100K、1-128K、2-256K、3-512K、4-1M、5-1.5M、6-2M、7-3M、8-4M 其他:码率值(kbps)有效范围 30~2^32，大于等于32，以K为单位
    public uint dwVideoFrameRate; //帧率 2 至 25
}

public class HB_SDVR_COMPRESSIONCFG
{
    public uint dwSize;
    public byte byRecordType; //0x0:手动录像，0x1:定时录象，0x2:移动侦测，0x3:报警，0x0f:所有类型
    public HB_SDVR_COMPRESSION_INFO struRecordPara = new HB_SDVR_COMPRESSION_INFO(); //录像流（主码流）
    public HB_SDVR_COMPRESSION_INFO struNetPara = new HB_SDVR_COMPRESSION_INFO(); //网传流（子码流）
}


//录像参数
public class HB_SDVR_RECORDSCHED
{
    public HB_SDVR_SCHEDTIME struRecordTime = new HB_SDVR_SCHEDTIME();
    public byte byRecordType; //保留
    public string reservedData = new string(new char[3]); //保留
}

public class HB_SDVR_RECORDDAY
{
    public ushort wAllDayRecord; //保留
    public byte byRecordType; //保留
    public sbyte reservedData; //保留
}

public class HB_SDVR_RECORD
{
    public uint dwSize;
    public uint dwRecord; //是否录像 0-否 1-是
    public HB_SDVR_RECORDDAY[] struRecAllDay = new HB_SDVR_RECORDDAY[HBConst.MAX_DAYS]; //星期
    public HB_SDVR_RECORDSCHED[,] struRecordSched = new HB_SDVR_RECORDSCHED[HBConst.MAX_DAYS, HBConst.MAX_TIMESEGMENT]; //时间段
    public uint dwPreRecordTime; //保留
}


//解码器
public class HB_SDVR_DECODERCFG
{
    public uint dwSize;
    public uint dwBaudRate; // 波特率(bps)
                            // 协议一，50 75 110 150 300 600 1200 2400 4800 9600 19200 38400 57600 76800 115.2k 
                            // 协议二，0-default,1-2400,2-4800,3-9600,4-19200,5-38400； 自定义取值范围[300，115200]
    public byte byDataBit; // 数据位 5 6 7 8
    public byte byStopBit; // 停止位 1 2
    public byte byParity; // 校验位 (0-NONE,1-ODD,2-EVEN,3-SPACE)
    public byte byFlowcontrol; // 流控(0-无,1-Xon/Xoff,2-硬件)
    public ushort wDecoderType; // 云台协议值，最好先通过HB_SDVR_GetPTZType获取该列表
                                //	 0-unknow 1-RV800  2-TOTA120 3-S1601 4-CLT-168 5-TD-500  6-V1200 7-ZION 8-ANT 9-CBC 10-CS850A 
                                //	 11-CONCORD 12-HD600 13-SAMSUNG 14-YAAN 15-PIH 16-MG-CS160 17-WISDOM 18-PELCOD1 19-PELCOD2 20-PELCOD3 
                                //	 21-PELCOD4 22-PELCOP1 23-PELCOP2 24-PELCOP3 25-Philips 26-NEOCAM  27-ZHCD 28-DongTian 29-PELCOD5 30-PELCOD6
                                //	 31-Emerson 32-TOTA160 33-PELCOP5
    public ushort wDecoderAddress; // 解码器地址:0 - 255
    public byte[] bySetPreset = new byte[HBConst.MAX_PRESET]; // 保留
    public byte[] bySetCruise = new byte[HBConst.MAX_PRESET]; // 保留
    public byte[] bySetTrack = new byte[HBConst.MAX_PRESET]; // 保留
}


//RS232 
public class HB_SDVR_PPPCFG
{
    public string sRemoteIP = new string(new char[16]); //远端IP地址
    public string sLocalIP = new string(new char[16]); //本地IP地址
    public string sLocalIPMask = new string(new char[16]); //本地IP地址掩码
    public byte[] sUsername = new byte[HBConst.NAME_LEN]; // 用户名 
    public byte[] sPassword = new byte[HBConst.PASSWD_LEN]; // 密码 
    public byte byPPPMode; //PPP模式, 0－主动，1－被动
    public byte byRedial; //是否回拨 ：0-否,1-是
    public byte byRedialMode; //回拨模式,0-由拨入者指定,1-预置回拨号码
    public byte byDataEncrypt; //数据加密,0-否,1-是
    public uint dwMTU; //MTU
    public string sTelephoneNumber = new string(new char[HBConst.PHONENUMBER_LEN]); //电话号码
}

public class HB_SDVR_RS232CFG
{
    public uint dwSize;
    public uint dwBaudRate; // 波特率(bps)
    public byte byDataBit; // 数据有几位 5－8
    public byte byStopBit; // 停止位 1-2
    public byte byParity; // 校验 0－无校验，1－奇校验，2－偶校验;
    public byte byFlowcontrol; // 0－无，1－软流控,2-硬流控
    public uint dwWorkMode; // 保留
    public HB_SDVR_PPPCFG struPPPConfig = new HB_SDVR_PPPCFG(); // 保留
}

//报警输入
public class HB_SDVR_ALARMINCFG
{
    public uint dwSize;
    public byte[] sAlarmInName = new byte[HBConst.NAME_LEN]; // 报警通道名
    public byte byAlarmType; // 保留
    public byte byAlarmInHandle; // 是否处理 0-1
    public HB_SDVR_HANDLEEXCEPTION struAlarmHandleType = new HB_SDVR_HANDLEEXCEPTION(); //处理方式
    public HB_SDVR_SCHEDTIME[,] struAlarmTime = new HB_SDVR_SCHEDTIME[HBConst.MAX_DAYS, HBConst.MAX_TIMESEGMENT]; //布防时间
    public byte[] byRelRecordChan = new byte[HBConst.MAX_CHANNUM]; //报警触发的录象通道,为1表示触发该通道
    public byte[] byEnablePreset = new byte[HBConst.MAX_CHANNUM]; // 是否调用预置点 仅用byEnablePreset[0]来判断;
    public byte[] byPresetNo = new byte[HBConst.MAX_CHANNUM]; // 调用的云台预置点序号,一个报警输入可以调用多个通道的云台预置点, 0xff表示不调用预置点
    public byte[] byEnableCruise = new byte[HBConst.MAX_CHANNUM]; // 保留
    public byte[] byCruiseNo = new byte[HBConst.MAX_CHANNUM]; // 保留
    public byte[] byEnablePtzTrack = new byte[HBConst.MAX_CHANNUM]; // 保留
    public byte[] byPTZTrack = new byte[HBConst.MAX_CHANNUM]; // 保留
    public byte byRecordTm; // 报警录像时间 1-99秒
}

//add by cui for 7024 or nvs 100325
public class HB_SDVR_ALARMINCFG_EX
{
    public uint dwSize;
    public byte[] sAlarmInName = new byte[HBConst.NAME_LEN]; // 报警通道名
    public byte byAlarmType; // 保留
    public byte byAlarmInHandle; // 是否处理 0-1
    public HB_SDVR_HANDLEEXCEPTION_EX struAlarmHandleType = new HB_SDVR_HANDLEEXCEPTION_EX(); //处理方式
    public HB_SDVR_SCHEDTIME[,] struAlarmTime = new HB_SDVR_SCHEDTIME[HBConst.MAX_DAYS, HBConst.MAX_TIMESEGMENT]; //布防时间
    public byte[] byRelRecordChan = new byte[HBConst.MAX_CHANNUM_EX]; //报警触发的录象通道,为1表示触发该通道
    public byte[] byEnablePreset = new byte[HBConst.MAX_CHANNUM_EX]; // 是否调用预置点 仅用byEnablePreset[0]来判断;
    public byte[] byPresetNo = new byte[HBConst.MAX_CHANNUM_EX]; // 调用的云台预置点序号,一个报警输入可以调用多个通道的云台预置点, 0xff表示不调用预置点
    public byte[] byEnableCruise = new byte[HBConst.MAX_CHANNUM_EX]; // 保留
    public byte[] byCruiseNo = new byte[HBConst.MAX_CHANNUM_EX]; // 保留
    public byte[] byEnablePtzTrack = new byte[HBConst.MAX_CHANNUM_EX]; // 保留
    public byte[] byPTZTrack = new byte[HBConst.MAX_CHANNUM_EX]; // 保留
    public byte byRecordTm; // 报警录像时间 1-99秒
}
//end add

//DVR报警输出
public class HB_SDVR_ALARMOUTCFG
{
    public uint dwSize;
    public byte[] sAlarmOutName = new byte[HBConst.NAME_LEN]; // 名称
    public uint dwAlarmOutDelay; // 输出保持时间 单位秒
    public byte byEnSchedule; // 报警输出布防时间激活 0-屏蔽 1-激活
    public HB_SDVR_SCHEDTIME[,] struAlarmOutTime = new HB_SDVR_SCHEDTIME[HBConst.MAX_DAYS, HBConst.MAX_TIMESEGMENT]; // 报警输出激活时间段
}


//用户权限
public class HB_SDVR_USER_INFO
{
    public byte[] sUserName = new byte[HBConst.NAME_LEN]; // 用户名 
    public byte[] sPassword = new byte[HBConst.PASSWD_LEN]; // 密码 
    public uint[] dwLocalRight = new uint[HBConst.MAX_RIGHT]; // 本地权限 
    public uint[] dwRemoteRight = new uint[HBConst.MAX_RIGHT]; // 远程权限 
                                                               //数组 0: 通道权限
                                                               //数组 1: 显示设置
                                                               //数组 2: 录像参数
                                                               //数组 3: 定时录像
                                                               //数组 4: 移动录像
                                                               //数组 5: 报警录像
                                                               //数组 6: 网络参数
                                                               //数组 7: 云台设置
                                                               //数组 8: 存储管理
                                                               //数组 9: 系统管理
                                                               //数组 10: 信息查询
                                                               //数组 11: 手动录像
                                                               //数组 12: 回放
                                                               //数组 13: 备份
                                                               //数组 14: 视频参数
                                                               //数组 15: 报警清除
                                                               //数组 16: 远程预览
    public string sUserIP = new string(new char[16]); // 用户IP地址(为0时表示允许任何地址) 
    public byte[] byMACAddr = new byte[HBConst.MACADDR_LEN]; // 物理地址 
}

public class HB_SDVR_USER_INFO_EX
{
    public byte[] sUserName = new byte[HBConst.NAME_LEN]; // 用户名 
    public byte[] sPassword = new byte[HBConst.PASSWD_LEN]; // 密码 
    public byte[] dwLocalRight = new byte[HBConst.MAX_RIGHT]; //本地权限 1.数组0未使用；2.取值：0-无权限，1-有权限
                                                              //数组 1: 常规设置
                                                              //数组 2: 录像设置
                                                              //数组 3: 输出设置
                                                              //数组 4: 报警设置
                                                              //数组 5: 串口设置
                                                              //数组 6: 网络设置
                                                              //数组 7: 录像回放
                                                              //数组 8: 系统管理
                                                              //数组 9: 系统信息
                                                              //数组 10: 报警清除
                                                              //数组 11: 云台控制
                                                              //数组 12: 关机重启
                                                              //数组 13: USB升级
                                                              //数组 14：备份
    public byte[] LocalChannel = new byte[HBConst.MAX_CHANNUM_EX]; //本地用户对通道的操作权限，最大128个通道，0-无权限，1-有权限
    public byte[] dwRemoteRight = new byte[HBConst.MAX_RIGHT]; //远程登陆用户所具备的权限 1.数组0未使用；2.取值：0-无权限，1-有权限
                                                               //数组 1: 远程预览
                                                               //数组 2: 参数设置
                                                               //数组 3: 远程回放
                                                               //数组 4: 远程备份
                                                               //数组 5: 查看日志
                                                               //数组 6: 语音对讲
                                                               //数组 7: 远程升级
                                                               //数组 8：远程重启
    public byte[] RemoteChannel = new byte[HBConst.MAX_CHANNUM_EX]; // 远程通道权限
    public string sUserIP = new string(new char[16]); // 用户IP地址(为0时表示允许任何地址) 
    public byte[] byMACAddr = new byte[HBConst.MACADDR_LEN]; // 物理地址 
}

public class HB_SDVR_USER
{
    public uint dwSize;
    public HB_SDVR_USER_INFO[] struUser = new HB_SDVR_USER_INFO[HBConst.MAX_USERNUM];
}

public class HB_SDVR_USER_EX
{
    public uint dwSize;
    public HB_SDVR_USER_INFO_EX[] struUser = new HB_SDVR_USER_INFO_EX[HBConst.MAX_USERNUM];
}

//DNS
public class HB_SDVR_DNS
{
    public uint dwSize;
    public string sDNSUser = new string(new char[HBConst.INFO_LEN]); // DNS账号
    public string sDNSPassword = new string(new char[HBConst.INFO_LEN]); // DNS密码
    public string[] sDNSAddress = new string[HBConst.INFO_SEQ]; // DNS解析地址
    public byte sDNSALoginddress; //DNS解析地址中sDNSAddress数组中的指定解析地址的行数
    public byte sDNSAutoCon; //DNS自动重连
    public byte sDNSState; //DNS登陆 0-注销 1-登陆
    public byte sDNSSave; //DNS信息保存
    public ushort sDNServer; // 0-- hanbang.org.cn 1--oray.net 2--dyndns.com
    public ushort reserve; //1--重启主机,0--不重启
}
public class HB_SDVR_DNS_EX
{
    public uint dwSize;
    public string sDNSUser = new string(new char[HBConst.INFO_LEN]); // DNS账号
    public string sDNSPassword = new string(new char[HBConst.INFO_LEN]); // DNS密码
    public string[] sDNSAddress = new string[HBConst.INFO_SEQ]; // DNS解析地址
    public byte sDNSALoginddress; //DNS解析地址中sDNSAddress数组中的指定解析地址的行数
    public byte sDNSAutoCon; //DNS自动重连
    public byte sDNSState; //DNS登陆 0-注销 1-登陆
    public byte sDNSSave; //DNS信息保存
    public ushort sDNServer; // 0-- hanbang.org.cn 1--oray.net 2--dyndns.com
    public ushort reserve; //1--重启主机,0--不重启
    public byte[] sDNSname = new byte[128]; //域名服务器
}

//PPPoE
public class HB_SDVR_PPPoE
{
    public uint dwSize;
    public byte[] sPPPoEUser = new byte[HBConst.INFO_LEN]; //PPPoE用户名
    public string sPPPoEPassword = new string(new char[HBConst.INFO_LEN]); // PPPoE密码
    public byte sPPPoEAutoCon; //PPPoE自动重连
    public byte sPPPoEState; //PPPoE登陆 0-注销 1-登陆
    public byte sPPPoESave; //DNS信息保存
    public sbyte reservedData;
}

//平台信息
public class HB_SDVR_SERVERCFG
{
    public string sServerIP = new string(new char[16]); //接入服务器IP地址
    public uint nPort; //接入服务器端口号
    public string puId = new string(new char[HBConst.NAME_LEN]); //设备注册ID号
    public uint nInternetIp; // 本地外网IP
    public uint nVideoPort; //视频端口
    public uint nTalkPort; //对讲端口
    public uint nCmdPort; //命令端口
    public uint nVodPort; //点播端口
    public uint tran_mode; // 1 子码流 0 主码流
                           // 以下新增
    public uint ftp_mode; // 以FTP方式进行中心存储 1 开启 0 关闭
    public uint max_link; // 最大连接数 0 - 32
}

public struct HB_SDVR_FIND_DATA
{
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
    public string sFileName; //文件名
    public HB_SDVR_TIME struStartTime; //文件的开始时间
    public HB_SDVR_TIME struStopTime; //文件的结束时间
    public uint dwFileSize; //文件的大小
    public byte nCh; //通道号
    public byte nType; //录像类型
} //cwh 20080730


public struct HB_SDVR_CHANNELSTATE
{
    public byte byRecordStatic; //通道是否在录像,0-不录像,1-录像
    public byte bySignalStatic; //连接的信号状态,0-正常,1-信号丢失
    public byte byHardwareStatic; //保留
    public sbyte reservedData;
    public uint dwBitRate; //实际码率
    public uint dwLinkNum; //客户端连接的个数
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = HBConst.MAX_LINK)]
    public uint[] dwClientIP; //保留
}

public struct HB_SDVR_DISKSTATE_ST
{
    public uint dwVolume; //硬盘的容量（MB）
    public uint dwFreeSpace; //硬盘的剩余空间（MB）
    public uint dwHardDiskStatic; //硬盘状态（dwVolume有值时有效） 0-正常 1-磁盘错误 2-文件系统出错
}

public struct HB_SDVR_WORKSTATE
{
    public uint dwDeviceStatic; //保留
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = HBConst.MAX_DISKNUM)]
    public HB_SDVR_DISKSTATE_ST[] struHardDiskStatic; //硬盘状态
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = HBConst.MAX_CHANNUM)]
    public HB_SDVR_CHANNELSTATE[] struChanStatic; //通道的状态	
    public uint byAlarmInStatic; //报警端口的状态 按位表示
    public uint byAlarmOutStatic; //报警输出端口的状态 按位表示
    public uint dwLocalDisplay; //保留
}

//add by cui for 7024 or nvs 100325
public struct HB_SDVR_WORKSTATE_EX
{
    public uint dwDeviceStatic; //保留
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = HBConst.MAX_DISKNUM)]
    public HB_SDVR_DISKSTATE_ST[] struHardDiskStatic; //硬盘状态
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = HBConst.MAX_CHANNUM_EX)]
    public HB_SDVR_CHANNELSTATE[] struChanStatic; //通道的状态
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = HBConst.MAX_ALARMIN_EX)]
    public uint[] byAlarmInStatic; //报警端口的状态 按位表示
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = HBConst.MAX_ALARMOUT_EX)]
    public uint[] byAlarmOutStatic; //报警输出端口的状态 按位表示
    public uint dwLocalDisplay; //保留
}
//end add


public class HB_SDVR_PRESETPOLL
{
    public uint byChannel; //设置通道
    public ushort[] Preset = new ushort[HBConst.PRESETNUM];
    public ushort PresetPoll; //多预置点轮巡开启或关闭表示
    public ushort presettime; //多预置点轮巡时间
}

//预览参数
public class HB_SDVR_DISPLAY_PARA
{
    public int bToScreen;
    public int bToVideoOut;
    public int nLeft;
    public int nTop;
    public int nWidth;
    public int nHeight;
    public int nReserved;
}
public class HB_SDVR_CARDINFO
{
    public int lChannel; //通道号
    public int lLinkMode; //最高位(31)为0表示主码流，为1表示子码流，0－30位表示码流连接方式:0：TCP方式,1：UDP方式,2：多播方式,3 - RTP方式，4-电话线，5－128k宽带，6－256k宽带，7－384k宽带，8－512k宽带；
    public string sMultiCastIP;
    public HB_SDVR_DISPLAY_PARA struDisplayPara = new HB_SDVR_DISPLAY_PARA();
}
//2005-08-01
// 解码设备透明通道设置 
public class HB_SDVR_PORTINFO
{
    public uint dwEnableTransPort; // 是否启动透明通道 0－不启用 1－启用
    public string sDecoderIP = new string(new char[16]); // DVR IP地址 
    public ushort wDecoderPort; // 端口号 
    public ushort wDVRTransPort; // 配置前端DVR是从485/232输出，1表示485串口,2表示232串口 
    public string cReserve = new string(new char[4]);
}

//连接的通道配置
public class HB_SDVR_DECCHANINFO
{
    public string sDVRIP = new string(new char[16]); // DVR IP地址 
    public ushort wDVRPort; // 端口号 
    public byte[] sUserName = new byte[HBConst.NAME_LEN]; // 用户名 
    public byte[] sPassword = new byte[HBConst.PASSWD_LEN]; // 密码 
    public byte byChannel; // 通道号 
    public byte byLinkMode; // 连接模式 
    public byte byLinkType; // 连接类型 0－主码流 1－子码流 
}

//每个解码通道的配置
public class HB_SDVR_DECINFO
{
    public byte byPoolChans; //每路解码通道上的循环通道数量, 最多4通道
    public HB_SDVR_DECCHANINFO[] struchanConInfo = new HB_SDVR_DECCHANINFO[HBConst.MAX_DECPOOLNUM];
    public byte byEnablePoll; //是否轮巡 0-否 1-是
    public byte byPoolTime; //轮巡时间 0-保留 1-10秒 2-15秒 3-20秒 4-30秒 5-45秒 6-1分钟 7-2分钟 8-5分钟 
}

//整个设备解码配置
public class HB_SDVR_DECCFG
{
    public uint dwSize;
    public uint dwDecChanNum; //解码通道的数量
    public HB_SDVR_DECINFO[] struDecInfo = new HB_SDVR_DECINFO[HBConst.MAX_DECNUM];
}


public class HB_SDVR_TRADEINFO
{
    public ushort m_Year;
    public ushort m_Month;
    public ushort m_Day;
    public ushort m_Hour;
    public ushort m_Minute;
    public ushort m_Second;
    public byte[] DeviceName = new byte[24]; //设备名称
    public uint dwChannelNumer; //通道号
    public byte[] CardNumber = new byte[32]; //卡号
    public string cTradeType = new string(new char[12]); //交易类型
    public uint dwCash; //交易金额
}

//以下为ATM专用
//帧格式
public class HB_SDVR_FRAMETYPECODE
{
    public byte[] code = new byte[12]; // 代码 
}

public class HB_SDVR_FRAMEFORMAT
{
    public uint dwSize;
    public string sATMIP = new string(new char[16]); // ATM IP地址 
    public uint dwATMType; // ATM类型 
    public uint dwInputMode; // 输入方式 
    public uint dwFrameSignBeginPos; // 报文标志位的起始位置
    public uint dwFrameSignLength; // 报文标志位的长度 
    public byte[] byFrameSignContent = new byte[12]; // 报文标志位的内容 
    public uint dwCardLengthInfoBeginPos; // 卡号长度信息的起始位置 
    public uint dwCardLengthInfoLength; // 卡号长度信息的长度 
    public uint dwCardNumberInfoBeginPos; // 卡号信息的起始位置 
    public uint dwCardNumberInfoLength; // 卡号信息的长度 
    public uint dwBusinessTypeBeginPos; // 交易类型的起始位置 
    public uint dwBusinessTypeLength; // 交易类型的长度 
    public HB_SDVR_FRAMETYPECODE[] frameTypeCode = new HB_SDVR_FRAMETYPECODE[10]; // 类型 
}

//武汉网吧 2005-06-29
public class HB_SDVR_FIND_PICTURE
{
    public string sFileName = new string(new char[100]); //图片名
    public HB_SDVR_TIME struTime = new HB_SDVR_TIME(); //图片的时间
    public uint dwFileSize; //图片的大小
    public string sCardNum = new string(new char[32]); //卡号
}



// 控制网络文件回放 
public class HB_SDVR_PLAYREMOTEFILE
{
    public uint dwSize;
    public string sDecoderIP = new string(new char[16]); // DVR IP地址 
    public ushort wDecoderPort; // 端口号 
    public ushort wLoadMode; // 回放下载模式 1－按名字 2－按时间 
                             //C++ TO C# CONVERTER TODO TASK: Unions are not supported in C#.
                             //ORIGINAL LINE: union
                             //C++ TO C# CONVERTER NOTE: Structs must be named in C#, so the following struct has been named AnonymousStruct:
    public struct AnonymousStruct
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
        public byte[] byFile; // 回放的文件名 
                              //C++ TO C# CONVERTER NOTE: Classes must be named in C#, so the following class has been named AnonymousClass2:
        public class AnonymousClass2
        {
            public uint dwChannel;
            public byte[] sUserName = new byte[HBConst.NAME_LEN]; //请求视频用户名
            public byte[] sPassword = new byte[HBConst.PASSWD_LEN]; // 密码 
            public HB_SDVR_TIME struStartTime = new HB_SDVR_TIME(); // 按时间回放的开始时间 
            public HB_SDVR_TIME struStopTime = new HB_SDVR_TIME(); // 按时间回放的结束时间 
        }
    }
    public AnonymousStruct mode_size = new AnonymousStruct();
}

//当前设备解码连接状态
public class HB_SDVR_DECCHANSTATUS
{
    public uint dwWorkType; //工作方式：1：轮巡、2：动态连接解码、3：按文件回放 4：按时间回放
    public string sDVRIP = new string(new char[16]); //连接的设备ip
    public ushort wDVRPort; //连接端口号
    public byte byChannel; // 通道号 
    public byte byLinkMode; // 连接模式 
    public uint dwLinkType; //连接类型 0－主码流 1－子码流
                            //C++ TO C# CONVERTER TODO TASK: Unions are not supported in C#.
                            //ORIGINAL LINE: union
                            //C++ TO C# CONVERTER NOTE: Structs must be named in C#, so the following struct has been named AnonymousStruct2:
    public struct AnonymousStruct2
    {
        //C++ TO C# CONVERTER NOTE: Classes must be named in C#, so the following class has been named AnonymousClass3:
        public class AnonymousClass3
        {
            public byte[] sUserName = new byte[HBConst.NAME_LEN]; //请求视频用户名
            public byte[] sPassword = new byte[HBConst.PASSWD_LEN]; // 密码 
            public string cReserve = new string(new char[52]);
        }
        //C++ TO C# CONVERTER NOTE: Classes must be named in C#, so the following class has been named AnonymousClass4:
        public class AnonymousClass4
        {
            public byte[] fileName = new byte[100];
        }
        //C++ TO C# CONVERTER NOTE: Classes must be named in C#, so the following class has been named AnonymousClass5:
        public class AnonymousClass5
        {
            public uint dwChannel;
            public byte[] sUserName = new byte[HBConst.NAME_LEN]; //请求视频用户名
            public byte[] sPassword = new byte[HBConst.PASSWD_LEN]; // 密码 
            public HB_SDVR_TIME struStartTime = new HB_SDVR_TIME(); // 按时间回放的开始时间 
            public HB_SDVR_TIME struStopTime = new HB_SDVR_TIME(); // 按时间回放的结束时间 
        }
    }
    public AnonymousStruct2 objectInfo = new AnonymousStruct2();
}

public class HB_SDVR_DECSTATUS
{
    public uint dwSize;
    public HB_SDVR_DECCHANSTATUS[] struDecState = new HB_SDVR_DECCHANSTATUS[HBConst.MAX_DECNUM];
}
public class HB_SDVR_PORTCFG
{
    public uint dwSize;
    public HB_SDVR_PORTINFO[] struTransPortInfo = new HB_SDVR_PORTINFO[HBConst.MAX_TRANSPARENTNUM]; // 数组0表示485 数组1表示232 
}

//自定义云台协议
public class STRUCT_SDVR_DECODERCUSTOMIZE
{
    public byte CheckSum; //效验码位置
    public byte PortNum; //地址码位置
    public byte PreSet; //预制点位置
    public byte CheckSumMode; //效验码计算模式
    public byte KeyLen; //码长度
    public byte KeyNum; //发码数
    public byte[,] CommandKey = new byte[HBConst.MAX_KEYNUM, 24];
}


public class STRUCT_SDVR_PTZTYPE
{
    public int ptznum;
    public string[] ptztype = new string[HBConst.MAXPTZNUM];
}

public class STRUCT_ALARMIN_WAIT
{
    public ushort enable; //使能
    public ushort time; //延迟时间
}


//add by cui for ipc 
public class STRUCT_SDVR_IPCCONFIG
{
    public byte bVideoOut; //视频输出
    public byte bTemperature; //温度探测
    public byte bVoltage; //电压探测
    public byte bSubStream; //子码流
    public uint Rserve1; //保留
    public uint Rserve2; //保留

}


public class STRUCT_SDVR_IPCPIC
{
    public ushort selIndex;
    public ushort Rserve;
}


public class STRUCT_SDVR_IPCAGC
{
    public ushort selIndex;
    public ushort Rserve;
}

//add by njt 091026 for ipc
public class STRUCT_SDVR_IPCWEP
{
    public byte safeoption; // 安全选项设置，取值范围[0,2] 0:自动选择 1：开放系统 2：共享密钥
    public byte pswformat; // 密钥格式设置，取值范围[0,1] 0：16进制 1：ASCII码
    public byte pswtype; // 密 钥 类 型设置，取值范围[0,3] 0：禁用 1：64位 2:128位 3:152位
    public byte[] pswword = new byte[62]; // 密码，以’\0’结尾，定义62byte是为了与STRUCT_SDVR_IPCWPAPSK等大小。
                                          //【备注：密码长度说明，选择64位密钥需输入16进制数字符10个，或者ASCII码字符5个。选择128位密钥需
                                          // 输入16进制数字符26个，或者ASCII码字符13个。选择152位密钥需输入16进制数字符32个，或者ASCII
                                          // 码字符16个。】
    public byte[] reserve = new byte[3]; //保留
}

public class STRUCT_SDVR_IPCWPAPSK
{
    public byte safeoption; // 安全选项设置，取值范围[0,2] 0：自动选择 1：WPA-PSK 2:WPA2-PSK
    public byte pswmod; // 加密方法设置,取值范围[0,2] 0：自动选择 1：TKIP 2:AES
    public byte[] pawword = new byte[64]; // psk密码，8到63个字符，以’\0’结尾
    public byte[] reserve = new byte[2]; // 保留
}

public class STRUCT_SDVR_IPCWIRELESS
{
    public uint nSecCmdParaSize; // 建议添加，结构体长度。
    public byte[] ssid = new byte[50]; // SSID号以’\0’结尾
    public byte[] wirelessIP = new byte[16]; // 无线ip以’\0’结尾
    public byte safetype; // 安全类型设置，取值范围[0,2] 0：WEB、1：WPA-PSK/WPA2-PSK、2：无加密
    public byte[] reserve = new byte[3]; // 保留
                                         //C++ TO C# CONVERTER TODO TASK: Unions are not supported in C#, but the following union can be simulated with the StructLayout and FieldOffset attributes.
                                         //ORIGINAL LINE: union
                                         //C++ TO C# CONVERTER NOTE: Structs must be named in C#, so the following struct has been named AnonymousStruct3:
                                         //[StructLayout(LayoutKind.Explicit)]
                                         //public struct AnonymousStruct3
                                         //{ // 因为以下两个结构体不可能同时使用，建议用联合体。
                                         //    [FieldOffset(0)]
                                         //    public STRUCT_SDVR_IPCWEP ipcwep = new STRUCT_SDVR_IPCWEP(); // 安全类型为WEP时参数结构体
                                         //    [FieldOffset(0)]
                                         //    public STRUCT_SDVR_IPCWPAPSK ipcwpapsk = new STRUCT_SDVR_IPCWPAPSK(); // 安全类型为WPA-PSK/WPA2-PSK时参数结构体
                                         //}
                                         //public AnonymousStruct3 u = new AnonymousStruct3();
}


public class STRUCT_SDVR_IFRAMERATE
{
    public byte channel; //通道号
    public short iframerate; //帧间隔
    public byte Reserve; //保留

}
public delegate void PHB_SDVR_STREAMDATA_PROC(int lRealHandle, uint dwDataType, ref byte pBuffer, uint dwBufSize, uint dwUser);
public struct HB_SDVR_FILEGETCOND
{
    public uint dwSize;
    public uint dwChannel; // 通道号
    public HB_SDVR_RECTYPE_E dwFileType; // 文件类型
    public HB_SDVR_TIME struStartTime; // 下载时间段开始时间
    public HB_SDVR_TIME struStopTime; // 结束时间
    public PHB_SDVR_STREAMDATA_PROC pfnDataProc;
    public IntPtr pContext;
    public string pSaveFilePath;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public uint[] dwResever;
}



public class HB_SDVR_NTPCONFIG
{
    public string server = new string(new char[32]); // 服务器
    public uint port; // 端口
    public uint auto_enbale; // 开启ntp服务,0-表示手动,1-表示自动
    public uint server_index; // 服务器索引号
    public uint sync_period; // 同步周期，
    public uint sync_unit; // 同步周期，0-分钟 1-小时 2-天 3-星期 4-月
    public uint sync_time; //保留
    public uint time_zone; //时区
    public uint reserve; // 保留
}

public class HB_SDVR_SMTPCONFIG
{
    public string host = new string(new char[HBConst.MAX_SMTP_HOST]); //发送邮件的SMTP服务器，例如：126信箱的是smtp.126.com
    public uint port; // 服务器端口，发送邮件(SMTP)端口：默认值25
    public string user = new string(new char[32]); // 邮件用户名
    public string pwd = new string(new char[32]); // 邮件用户密码
    public string send_addr = new string(new char[HBConst.MAX_SMTP_HOST]); // FROM：邮件地址
    public string recv_addr = new string(new char[HBConst.MAX_SMTP_ADDR]); // TO：邮件地址，如果是多个邮件地址，以';'隔开
    public uint send_period; // 上传周期,单位(分钟)
    public uint snap_enable; // 是否抓拍上传
    public string reserve = new string(new char[HBConst.MAX_STRING]);
}

public class HB_SDVR_POLLCONFIG
{
    public uint poll_type; //轮训类型：0：轮训；1：spot轮巡
    public uint enable; // 启用？0-禁用，1-启用
    public uint period; // 轮训间隔，单位秒
    public uint format; // 画面格式：0-0ff, 1-1, 2-2, 4-2x2, 9-3x3, 16-4x4
    public byte[] ch_list = new byte[HBConst.MAX_CHANNUM];
}

public class HB_SDVR_POLLCONFIG128
{
    public uint poll_type; //轮训类型：0：轮训；1：spot轮巡
    public uint enable; // 启用？0-禁用，1-启用
    public uint period; // 轮训间隔，单位秒
    public uint format; // 画面格式：0-0ff, 1-1, 2-2, 4-2x2, 9-3x3, 16-4x4
    public byte[] ch_list = new byte[HBConst.MAX_CHANNUM_EX];
}

public class HB_SDVR_VIDEOMATRIX
{
    public byte[] matrix_channel = new byte[HBConst.MAX_CHANNUM]; // 视频矩阵对应通道 从1开始，0xff表示关闭
    public byte[] reserve = new byte[32]; // 保留位
}

public class HB_SDVR_VIDEOMATRIX128
{
    public byte[] matrix_channel = new byte[HBConst.MAX_CHANNUM_EX]; // 视频矩阵对应通道 从1开始，0xff表示关闭
    public byte[] reserve = new byte[32]; // 保留位
}


// 系统时间定义
public class ASYSTIME
{
    //C++ TO C# CONVERTER TODO TASK: C# does not allow bit fields:
    public uint sec;
    //C++ TO C# CONVERTER TODO TASK: C# does not allow bit fields:
    public uint min;
    //C++ TO C# CONVERTER TODO TASK: C# does not allow bit fields:
    public uint hour;
    //C++ TO C# CONVERTER TODO TASK: C# does not allow bit fields:
    public uint day;
    //C++ TO C# CONVERTER TODO TASK: C# does not allow bit fields:
    public uint month;
    //C++ TO C# CONVERTER TODO TASK: C# does not allow bit fields:
    public uint year;
}


//********************************************************************
////
//// 智能
////
//*******************************************************************
//一个点的坐标
public class PT_ATMI_POINT_S
{
    public ushort x; //横坐标
    public ushort y; //纵坐标
}

// 客流统计参数
public class PT_HDC_CTRLPARAM_ST
{
    public uint dwWidth; // 处理视频的宽度，默认值352
    public uint dwHeight; // 处理视频高度，默认值288
    public uint objWidth; // 单个目标的宽度，单位为像素，默认值55
    public PT_ATMI_POINT_S ptStart = new PT_ATMI_POINT_S(); // 检测线起点，默认值（5,216)
    public PT_ATMI_POINT_S ptEnd = new PT_ATMI_POINT_S(); // 检测线终点，默认值(347,216)
    public PT_ATMI_POINT_S ptDirection = new PT_ATMI_POINT_S(); // 检测线的方向，默认值(290, 205)
    public uint dwPassFrames; // 初始化的单目标在合成图中的高度，即目标通过检测线的帧数，默认值15
    public uint dwMutiObjWidth; // 三个以上目标并行时矩形框的宽度，默认值110
    public uint dwMutiObjWidthEdge; // 与dwMutiObjWidth有关，dwMutiObjWidthEdge = （dwMutiObjWidth / 2 - 5）/ 2，默认值25
    public uint dwThreshBackDiff; // 背景差阀值，默认值50，比较敏感
    public uint dwThreshFrameDiff; // 帧间差阀值，默认值20
    public byte byStartPtLabel; // 起点检测标记，0表示任何目标均计数，1表示小于阀值的目标不计数，默认值为0
    public byte byEndPtLable; // 终点检测标记，0表示任何目标均计数，1表示小于阀值的目标不计数，默认值为0
    public byte[] byReserve = new byte[2]; // 保留字段
    public uint dwHalfObjW; // 阀值，与前两项相关，宽度小于该阀值不计数，默认值为20
}

//****************************************************************
//
//以下为四路智能的报警通信结构
//
//****************************************************************
//矩形坐标
public class PT_ATMI_RECT
{
    public int left; //矩形左坐标
    public int top; //矩形上坐标
    public int right; //矩形右坐标
    public int bottom; //矩形下坐标
}

//报警类型及位置信息
public class PT_ATMI_ALARM_POSITION_S
{
    public int alarm_type; //类型,GUI_ATMI_ALARM_TYPE_E
    public PT_ATMI_RECT position = new PT_ATMI_RECT(); //坐标位置
}

// 1.人脸通道报警结构体
public class PT_ATMI_FACE_ALARM_S
{
    public int alarm_num; //报警个数
    public PT_ATMI_ALARM_POSITION_S[] alarm_area = new PT_ATMI_ALARM_POSITION_S[10]; //报警坐标值,一共有alarm_num个，后面的全为0
}

// 2.面板通道报警结构体
public class PT_ATMI_PANEL_ALARM_S
{
    public int alarm_num; //报警个数
    public PT_ATMI_ALARM_POSITION_S[] alarm_area = new PT_ATMI_ALARM_POSITION_S[10]; //报警坐标值,一共有alarm_num个，后面的全为0
}

// 3.加钞间检测输出信息
public class PT_ATMI_MONEY_ALARM_S
{
    public int type; //是否有人闯入，0表示无，1表示有
}

// 4.环境报警结构体,alarm_num所对应的区域在前，track_num所对应的区域紧跟在alarm_num区域后
public class PT_ATMI_ENVI_ALARM_S
{
    public int alarm_num; //报警目标数量
    public int track_num; //跟踪目标数量
    public PT_ATMI_ALARM_POSITION_S[] envi_alarm_region = new PT_ATMI_ALARM_POSITION_S[25];
}

public enum PT_ATMI_ALARM_TYPE_E : int
{
    PT_ATMI_FACE_BLOCK = 0, //人脸遮挡
    PT_ATMI_FACE_NOSIGNAL, //有脸通道视频丢失
    PT_ATMI_FACE_UNUSUAL, //人脸异常
    PT_ATMI_FACE_NORMAL, //人脸正常
    PT_ATMI_PANEL_BLOCK = 40, //面板遮挡
    PT_ATMI_PANEL_NOSIGNAL, //面板通道视频丢失
    PT_ATMI_PANEL_PASTE, //贴条
    PT_ATMI_PANEL_KEYBOARD, //装假键盘
    PT_ATMI_PANEL_KEYMASK, //破坏密码防护罩
    PT_ATMI_PANEL_CARDREADER, //破坏读卡器
    PT_ATMI_PANEL_TMIEOUT, //超时报警
    PT_ATMI_ENTER, //有人进入
    PT_ATMI_EXIT, //人撤离
    PT_ATMI_MONEY_BLOCK = 80, //加钞间视频遮挡
    PT_ATMI_MONEY_NOSIGNAL, //加钞间通道视频丢失
    PT_ATMI_MONEY_UNUSUAL, //加钞间异常,即有人闯入加钞间
    PT_ATMI_ENVI_BLOCK = 120, //环境通道视频遮挡
    PT_ATMI_ENVI_NOSIGNAL, //环境通道视频丢失
    PT_ATMI_ENVI_GATHER, //多人聚集
    PT_ATMI_ENVI_MANYPEOPLEINREGION, //违规取款
    PT_ATMI_ENVI_LINGERING, //人员徘徊
    PT_ATMI_ENVI_LIEDOWN, //人员倒地
    PT_ATMI_ENVI_FORBIDREGION, //非法进入警戒区
    PT_ATMI_ENVI_LEAVEOBJECT, //物品遗留
}

//报警图片结构体
public class PT_ATMI_ALARM_PICINFO_S
{
    public uint pic_alarm_type; // PT_ATMI_ALARM_TYPE_E;
    public uint pic_format; // 图片格式CIF:0 D1:1
    public uint pic_size;
}

public enum PT_HDCCOUNT_DIRECTION_E : int
{
    HBGK_HDCCOUNT_DIR1 = 0, //与标记方向相同
    HBGK_HDCCOUNT_DIR2 //与标记方向相反
}

public class PT_HDC_RESULT_ST
{
    public uint dwResultType; // 输出结果总类型
    public uint dwSubType; // 输出结果子类型，表示人员流动统计的方向见PT_HDCCOUNT_DIRECTION_E
    public uint dwTrackNum; // 当前输出统计的ID编号(已统计人数)
    public PT_ATMI_RECT rcPos = new PT_ATMI_RECT(); // 当前输出编号的外接矩形框
}

//报警时主机传给客户端的总结构体
public class PT_ATMI_ALARM_INFO_S
{
    // int chn;    //通道号,每次报警后，根据通道号，去读取下面四个结构体中对应的那一个
    public byte byChn;
    public byte byReserve1;
    public byte byInfoType; // 上传信息类型
                            // 0, PT_ATMI_FACE_ALARM_S
                            // 1, PT_ATMI_PANEL_ALARM_S
                            // 2, PT_ATMI_MONEY_ALARM_S
                            // 3, PT_ATMI_ENVI_ALARM_S
                            // 4, PT_HDC_RESULT
    public byte byReserve2;

    //C++ TO C# CONVERTER TODO TASK: Unions are not supported in C#, but the following union can be simulated with the StructLayout and FieldOffset attributes.
    //ORIGINAL LINE: union
    //C++ TO C# CONVERTER NOTE: Structs must be named in C#, so the following struct has been named AnonymousStruct4:
    //[StructLayout(LayoutKind.Explicit)]
    //public struct AnonymousStruct4
    //{
    //    [FieldOffset(0)]
    //    public PT_ATMI_FACE_ALARM_S atmi_face_alarm = new PT_ATMI_FACE_ALARM_S(); // 1.人脸通道报警结构体
    //    [FieldOffset(0)]
    //    public PT_ATMI_PANEL_ALARM_S atmi_panel_alarm = new PT_ATMI_PANEL_ALARM_S(); // 2.面板通道报警结构体
    //    [FieldOffset(0)]
    //    public PT_ATMI_MONEY_ALARM_S atmi_money_alarm = new PT_ATMI_MONEY_ALARM_S(); // 3.加钞间通道报警结构体
    //    [FieldOffset(0)]
    //    public PT_ATMI_ENVI_ALARM_S atmi_envi_alarm = new PT_ATMI_ENVI_ALARM_S(); // 4.环境通道报警结构体
    //    [FieldOffset(0)]
    //    public PT_HDC_RESULT_ST hdc = new PT_HDC_RESULT_ST();
    //}
    //public AnonymousStruct4 info = new AnonymousStruct4();

    public PT_ATMI_ALARM_PICINFO_S alarm_picinfo = new PT_ATMI_ALARM_PICINFO_S();
    public ASYSTIME alarmtime = new ASYSTIME(); //报警时间
}
//****************************************************************
//
//以下为四路智能的设置或获取的界面结构
//
//****************************************************************
//多边形表示结构体，带区域类型
public class PT_ATMI_POLYGON_S
{
    public PT_ATMI_POINT_S[] point = new PT_ATMI_POINT_S[10]; //多边形顶点坐标
    public int point_num; //点的个数
    public int region_type; //区域类型
}

//矩形区域，带区域类型
public class PT_ATMI_RECT_S
{
    public PT_ATMI_RECT region = new PT_ATMI_RECT(); //矩形区域坐标
    public int region_type; //区域类型
}

//人脸感兴趣区域以及该区域中人脸的大小
public class PT_ATMI_FACE_ROI_S
{
    public PT_ATMI_RECT_S roi = new PT_ATMI_RECT_S(); //坐标
    public int min_face; //最小尺寸
    public int max_face; //最大尺寸
}

// 1.人脸通道中所设置的区域
public class PT_ATMI_FACEROI_ALL_S
{
    public int num;
    public PT_ATMI_FACE_ROI_S[] face_roi = new PT_ATMI_FACE_ROI_S[10];
}

// 2.面板通道中所设置的区域
public class PT_ATMI_PANEL_REGION_S
{
    public int num;
    public PT_ATMI_POLYGON_S[] atmi_panel_region = new PT_ATMI_POLYGON_S[20];
}

// 3.加钞间通道中所设置的区域及参数
public class PT_ATMI_DISTRICTPARA_S
{
    public PT_ATMI_POLYGON_S pol_proc_region = new PT_ATMI_POLYGON_S(); // 处理区域，默认4个点，包含全图
    public PT_ATMI_RECT_S[] no_process = new PT_ATMI_RECT_S[10]; // 不处理区域
    public int no_process_num; // 不处理区域个数 (0)
    public int warn_interval; // 两次报警时间间隔，(100 秒)
}

// 4.场景通道中所设置的区域
public class PT_ATMI_SCENE_COMMONPARA_S
{
    public PT_ATMI_POLYGON_S pol_proc_region = new PT_ATMI_POLYGON_S(); // 图像中的处理区域，多边形表示

    //用于ATM机前尾随取款检测的参数，标识ATM前人站立的区域
    public PT_ATMI_POLYGON_S[] tail_region = new PT_ATMI_POLYGON_S[10]; // Region in polygon.
    public int tail_num; // Region number. default: 0

    //用于禁止区域进入报警，标识选定的禁止进入的区域
    public PT_ATMI_POLYGON_S[] forbid_region = new PT_ATMI_POLYGON_S[10]; // Region in polygon.
    public int forbid_num; // Region number. default: 0

    public PT_ATMI_POLYGON_S obj_height = new PT_ATMI_POLYGON_S(); // 目标（人）在图像中的高度，默认85
}

// 5.环境通道设置的参数,以下以帧为单位的，我们在界面上做为秒，然后在内部再转化为帧数
public class PT_ATMI_SCENE_WARN_PARAM_S
{
    //物品遗留算法相关参数
    public int objlv_frames_th; // 物品遗留时间阈值(帧) (30)

    //人员徘徊算法相关参数
    public int mv_block_cnt; // 移动距离(20，0表示此规则无效)
    public short mv_stay_frames; // 场景中出现时间阈值(帧),存在总时间(0表示此规则无效)
    public short mv_stay_valid_frames; // ATM区域停留时间阈值(帧),ATM区域前停留时间(200, 0表示此规则无效)

    //多人聚集算法相关参数
    public short gather_cnt; // 最多聚集人数(4)
    public short gather_interval_frames; // 报警间隔时间(帧)(1000 frames,约100秒)
    public int gather_frame_cnt; // 多人聚集时间阈值(帧) (100)

    //人员躺卧算法相关参数
    public int liedown_frame_cnt; // 躺卧时间阈值(帧).(20 frames)

    //尾随取款算法相关参数
    public short after_frame_cnt; // 可疑行为时间阈值(帧)(20 frames)
    public short after_interval_frames; // 报警间隔时间(帧)(1000 frames,约100秒)

    //禁止进入算法相关参数
    public short forbid_frame_cnt; // 禁止站立时间阈值(帧)(20 frames)
    public short reserve; // 保留
}

// 1.人脸通道设置结构体
public class PT_ATMI_SET_FACE_S
{
    public short face_unusual; //是否打开异常人脸（戴口罩、蒙面等）检测功能，1 为打开，0 为关闭。默认为 0
    public short output_oneface; //设置人脸只输出一次与否，0为否，1为是，默认为1
    public int fd_rate; //设置人脸检测跟踪间隔
    public PT_ATMI_FACEROI_ALL_S face_roi = new PT_ATMI_FACEROI_ALL_S(); //人脸通道的区域及其它参数
    public int abnormalface_alarmtime; //报警触发时间阀值（秒）
}

//面板告警参数结构体
public class STRUCT_SDVR_PANELALARMCFG
{
    public int AlphaVal; //检测库alpha值(5)
    public int BetaVal; //检测库Beta值(3)
    public int MetinThVal; //前景融背景阈值(4500)
    public int LBTWTriggerVal; //取走遗留报警阈值(75)

    public int AppearCntThdVal; //区域入侵报警基数(40)
    public int AppearCntTriggerVal; //区域入侵报警阈值(40)
    public int LBTWCntThdVal; //取走遗留报警基数(75)
    public int LBTWCntTriggerVal; //取走遗留报警阈值(75)

    public int PanelTimeOutTriggerVal; //超时报警阈值(1500)

    public int OpenLightTriggerVal; //进变化报警阈值(45)
    public int CloseLightTriggerVal; //出变化报警阈值(55)

    public uint AppearMinWidth; //区域入侵最小目标宽度(10)
    public uint AppearMinHeight; //区域入侵最小目标高度(10)
    public uint AppearMaxWidth; //区域入侵最大目标宽度(200)
    public uint AppearMaxHeight; //区域入侵最大目标高度(200)

    public uint LBTWMinWidth; //取走遗留最小目标宽度(10)
    public uint LBTWMinHeight; //取走遗留最小目标高度(10)
    public uint LBTWMaxWidth; //取走遗留最大目标宽度(200)
    public uint LBTWMaxHeight; //取走遗留最大目标高度(200)
}


// 2.面板通道设置结构体
public class PT_ATMI_SET_PANEL_S
{
    public int timeout_enable; //超时时间
    public PT_ATMI_PANEL_REGION_S panel_region = new PT_ATMI_PANEL_REGION_S(); //面板通道区域及其它参数
    public STRUCT_SDVR_PANELALARMCFG panel_alarm_param = new STRUCT_SDVR_PANELALARMCFG(); //面板通道报警设置参数
}

// 3.加钞间通道设置结构体
public class PT_ATMI_SET_MONEY_S
{
    public PT_ATMI_DISTRICTPARA_S money_para = new PT_ATMI_DISTRICTPARA_S(); //加钞间通道区域及其它参数
}

// 4.环境通道设置结构体
public class PT_ATMI_SET_ENVI_S
{
    public PT_ATMI_SCENE_WARN_PARAM_S envi_para = new PT_ATMI_SCENE_WARN_PARAM_S(); //环境通道参数
    public PT_ATMI_SCENE_COMMONPARA_S envi_region = new PT_ATMI_SCENE_COMMONPARA_S(); //环境通道区域
}

//客户端设置或获取到主机四路智能总的结构体
public class STRUCT_SDVR_INTELLECTCFG
{
    // int chn;                                      //通道号
    public byte byChn; // 通道号
    public byte byReserve1; // 保留
    public byte bySetInfoType; // 设置参数类型，
                               // 0, PT_ATMI_SET_FACE_S;
                               // 1, PT_ATMI_SET_PANEL_S;
                               // 2, PT_ATMI_SET_MONEY_S;
                               // 3, PT_ATMI_SET_ENVI_S;
                               // 4, PT_HDC_CTRLPARAM;
    public byte byReserve2; // 保留

    public int chn_attri; // 通道属性(人脸、面板、加钞、环境)，目前未用，防止以后用
    public short channel_enable; // 通道开关，0关闭，1打开
    public short if_pic; // 是否需要抓取图片
    public short enc_type; // 抓取图片的格式
    public short email_linkage; // 联动email
    public uint sensor_num; // 探头输出,位表示
    public uint rec_linkage; // 联动录像，位表示

    //C++ TO C# CONVERTER TODO TASK: Unions are not supported in C#, but the following union can be simulated with the StructLayout and FieldOffset attributes.
    //ORIGINAL LINE: union
    //C++ TO C# CONVERTER NOTE: Structs must be named in C#, so the following struct has been named AnonymousStruct5:
    //    [StructLayout(LayoutKind.Explicit)]
    //    public struct AnonymousStruct5
    //    {
    //        [FieldOffset(0)]
    //        public PT_ATMI_SET_FACE_S face_set_para = new PT_ATMI_SET_FACE_S(); // 人脸通道设置结构体
    //        [FieldOffset(0)]
    //        public PT_ATMI_SET_PANEL_S panel_set_para = new PT_ATMI_SET_PANEL_S(); // 面板通道设置结构体
    //        [FieldOffset(0)]
    //        public PT_ATMI_SET_MONEY_S money_set_para = new PT_ATMI_SET_MONEY_S(); // 加钞间通道设置结构体
    //        [FieldOffset(0)]
    //        public PT_ATMI_SET_ENVI_S envi_set_para = new PT_ATMI_SET_ENVI_S(); // 环境通道设置结构体
    //        [FieldOffset(0)]
    //        public PT_HDC_CTRLPARAM_ST hdc = new PT_HDC_CTRLPARAM_ST(); // 人流统计参数设置
    //    }
    //    public AnonymousStruct5 setInfo = new AnonymousStruct5();
}

public enum HB_SDVR_RECTYPE_E
{
    HB_SDVR_RECMANUAL = 1, // 手动录像
    HB_SDVR_RECSCHEDULE = 2, // 定时录像
    HB_SDVR_RECMOTION = 4, // 移动侦测录像
    HB_SDVR_RECALARM = 8, // 报警录像
    HB_SDVR_REC_ALL = 0xff, // 所有录像
}

public struct ST_HB_SDVR_FILEGETCOND
{
    public uint dwSize;
    public uint dwChannel; // 通道号
    public HB_SDVR_RECTYPE_E dwFileType; // 文件类型
    public HB_SDVR_TIME struStartTime; // 下载时间段开始时间
    public HB_SDVR_TIME struStopTime; // 结束时间
    public IntPtr pfnDataProc;
    public IntPtr pContext;
    public string pSaveFilePath;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public uint dwResever;
}


// add 2010/08/04
//
//功能：设置蜂鸣器状态
//    参数：lUserID：由HB_SDVR_Login返回
//返回值：
//    成功：返回TRUE
//    失败：返回FALSE
//
// 2010/08/04 设置蜂鸣器报警
public class _STRUCT_BUZZER_CTRL
{
    public int ctrl; //1.开 0.关
}

public class MFS_Field_Time
{
    //C++ TO C# CONVERTER TODO TASK: C# does not allow bit fields:
    public uint second; // 秒: 0~59
                        //C++ TO C# CONVERTER TODO TASK: C# does not allow bit fields:
    public uint minute; // 分: 0~59
                        //C++ TO C# CONVERTER TODO TASK: C# does not allow bit fields:
    public uint hour; // 时: 0~23
                      //C++ TO C# CONVERTER TODO TASK: C# does not allow bit fields:
    public uint day; // 日: 1~31
                     //C++ TO C# CONVERTER TODO TASK: C# does not allow bit fields:
    public uint month; // 月: 1~12
                       //C++ TO C# CONVERTER TODO TASK: C# does not allow bit fields:
    public uint year; // 年: 2000~2063
}

// 泸州智能
//#define HB_MAX_AM_COUNT

public class HB_SDVR_REAL_DEFENCE
{
    public ushort enable; //0-撤防，1-布防
    public ushort time; //布防延时时间（此变量暂时无用）
    public uint[] reserver = new uint[4]; // 保留
}

public class HB_SDVR_ALMINFO
{
    public ushort alm_ch; //报警通道
    public ushort alm_type; //报警类型0-无报警1-探头报警2-移动侦测3-视频丢失
    public MFS_Field_Time time = new MFS_Field_Time(); //报警发生时间点
}

public class HB_SDVR_DISCONN_ALMSTAT
{
    public uint alm_count; //掉线期间报警总数
    public HB_SDVR_ALMINFO[] alminfo = new HB_SDVR_ALMINFO[HBConst.HB_MAX_AM_COUNT]; //每次报警的相关信息，只存储最新的16次报警
    public uint[] reserver = new uint[4]; // 保留
}

// 2011/09/21 T系列机器增加遮挡报警设置的设置/获取
// 命令号:
//#define NET_SDVR_GET_VCOVER_DETECT
//#define NET_SDVR_SET_VCOVER_DETECT

// 数据体结构
public class HB_SDVR_VCOVER_ALM_ST
{
    public byte byChannel; // 通道号 [0,n]
    public uint dwVcover_Enable; // 遮挡报警使能， 1-启用；0-不启用；
    public uint dwSensor_Out; // 联动报警输出， 按位表示， 1-联动；0-不联动；
}

// 2011/09/21 IPC相关接口变动
// 1.无线参数配置 数据体结构变动
// 2.工作模式参数去掉，使用工作参数替换，命令号变更为0xB0/0xB1
// 3.原0xB0-0xB6废弃

// 命令号
//#define NET_SDVR_IPCWORKPARAM_GET
//#define NET_SDVR_IPCWORKPARAM_SET

public class HB_SDVR_REQIPCWORKPARAM_ST
{
    public byte cbStreamType; // 码流类型 0-主码流 1-子码流 2-第三码流
    public byte[] cbReserve = new byte[3];
}

// 数据体结构
// typedef struct	{
//     BYTE	byEnable;  		// 布防时间使能  0-关闭，1-启动	
//     BYTE	byStartHour;  	//开始小时 0-23
//     BYTE	byStartMin;  		//开始分钟 0-59
//     BYTE	byStopHour; 		//结束小时  0-23
//     BYTE	byStopMin;  		//结束分钟  0-59
// }STRUCT_SDVR_SCHEDTIME; 


public class HB_SDVR_SCHEDTIME_V2_ST
{
    public byte byStartHour; //开始小时 0-23
    public byte byStartMin; //开始分钟 0-59
    public byte byStopHour; //结束小时 0-23
    public byte byStopMin; //结束分钟 0-59
}

public class HB_SDVR_ICRTIME_ST
{
    public ushort wLightRange; //ICR亮度切换临界值，取值范围0 < wLightRange< 2^16;
    public ushort wEnable; // 0--亮度值有效 1--时间段有效
    public HB_SDVR_SCHEDTIME_V2_ST[] stSchedTime = new HB_SDVR_SCHEDTIME_V2_ST[2];
}

public class HB_SDVR_SHUTTERVAL_ST
{
    public uint dwShutterIndex; //当前快门时间索引值,表示基于dwShutterVal中的位置，例如dwShutterIndex = 2，
                                //则当前快门时间为dwShutterVal[2];
    public uint[] dwShutterVal = new uint[32]; //获取快门时间的支持参数列表,当dwShutterVal[1]=0,表示是一个取值范围，如
                                               //dwShutterVal[0]= 4096,则表示取值范围为：1/[1,4096*2],当dwShutterVal[1] != 0时，
                                               //表示有多个具体的索引值，比如：dwShutterVal[0]= 2048，dwShutterVal[1]=4096，当dwShutterVal[x]=0则表示数据共有x个。
}

public class HB_SDVR_SCENEVAL_ST
{
    public uint dwSceneIndex; //当前镜头索引值，表示基于cbSceneVal中的位置，例如dwSceneIndex = 2，
                              //则当前镜头为：cbSceneVal[2] = “JCD661 lens”，当cbSceneVal[x] =”\0”表示总共有
                              //x个数据项；
    public byte[,] cbSceneVal = new byte[8, 32]; //该机型支持的镜头种类,//0 - Full Maual lens,1 - DC Iris lens, //2 - JCD661 lens,
                                                 //3 - Ricom NL3XZD lens,4 - Tamron 18X lens，
}

public class HB_SDVR_RESOLUTION_ST
{
    public uint dwResoluIndex; //当前分辨率索引值，表示基于dwResolution中的位置，例如dwResoluIndex= 2，
                               //则当前分辨率为dwResolution[2]中所指定的分辨率
    public uint[] dwResolution = new uint[16]; //该机型支持的分辨率，用DWORD来表示支持的分辨率，例如：
                                               //dwResolution[0]=0x07800438，高两字节（0x0780=1920）、低两字节（0x0438=1080）；
}

public class HB_SDVR_AGCVAL_ST
{
    public uint dwAgcIndex; //当前AGC的索引值，表示基于cbAgcVal中的位置，例如cbAgcVal =2，则表示AGC
                            //值为cbAgcVal[2]中的值；
    public byte[] cbAgcVal = new byte[32]; //AGC（自动增益）的支持参数列表,当cbAgcVal[1]= 0时表示cbAgcVal[0]中存储的是
                                           //一个取值范围，如cbAgcVal[0]= 128,则表示取值范围为：[1,128],当cbAgcVal[1]!=0
                                           //时，则表示cbAgcVal数组中存储的是具体的值，例如 cbAgcVal[0]= 32，//cbAgcVal[1]=64等，当cbAgcVal [x] =0表示总共有x个数据项。
}

public class HB_SDVR_FRAMERATE_ST
{
    public byte cbMinFrameRate; //该机型支持的最小编码帧率值;，取值范围为：1—2^8，下同。
    public byte cbMaxFrameRate; //该机型支持的最大编码帧率值;
    public byte cbCurFrameRate; //该机型设置的当前编码帧率值;
    public byte cbreserve; //保留
}

public class HB_SDVR_IPCWORKMODE_ST
{
    public uint dwLength; //结构体长度
    public byte cbStreamEnable; //是否开启当前码流
    public byte cbStreamType; //码流类型 0-主流1-子流 2-第三码流
    public byte cbAudioEnable; //音频使能 0-无音频 ,1-有音频
    public byte cbAntiFlicker; //抗闪烁设置 0-60HZ 1-50HZ
    public HB_SDVR_FRAMERATE_ST stFrameRate = new HB_SDVR_FRAMERATE_ST(); //编码帧率设置;
    public HB_SDVR_SHUTTERVAL_ST stShutterVal = new HB_SDVR_SHUTTERVAL_ST(); //快门相关参数获取
    public HB_SDVR_SCENEVAL_ST stSceneVal = new HB_SDVR_SCENEVAL_ST(); //镜头相关参数获取
    public HB_SDVR_RESOLUTION_ST stResolution = new HB_SDVR_RESOLUTION_ST(); //解析度相关
    public HB_SDVR_AGCVAL_ST stAgcVal = new HB_SDVR_AGCVAL_ST(); //Agc相关
    public uint dwBitRateVal; //视频码率 0-100K 1-128K，2-256K，3-512K，4-1M，5-1.5M，6-2M，7-3M, 8-4M
                              //9-5M，10-6M，11-7M，12-8M, 13-9M，14-10M，15-11 M，16-12M
                              //其他：码率值（kbps）有效范围 32~2^32,大于等于32，以K为单位；
    public byte cbFoucusSpeedVal; //光学变焦速度值，0：不支持
    public byte cbDigitalFoucusVal; //数字变焦值，0：不支持
    public byte cbImageTurnVal; //当前图像翻转设置 //1-不翻转,2-水平翻转 3-垂直翻转, 4-水平&垂直,0-不支持
    public byte cbBlackWhiteCtrlVal; //当前黑白模式设置 //1- Off, 2- On, 3–Auto, 0-不支持
    public byte cbIRISCtrl; //Iris control mode 光圈控制模式设置，1-Off,2-Basic, 3-Advanced,0-不支持
    public byte cbAutoFoucusVal; //是否支持自动对焦，//0-不支持,1-支持
    public byte cbAWBVal; //白平衡场景模式设置，1-auto_wide, 2-auto_normal, 3-sunny, 4-shadow, 5-indoor,
                          //6-lamp, 7-FL1, 8-FL2,0-不支持
    public byte cbA3Ctrl; //3A控制0-off; 1-Auto Exposure; 2-Auto White Balance; 3-both, (Auto Focus no support)
    public HB_SDVR_ICRTIME_ST stICRTime = new HB_SDVR_ICRTIME_ST(); //ircut(滤光片切换模式设置)仅在模式4时才设置获取该值
    public byte cbFNRSuppVal; //当前帧降噪设置，1-开,2-关,0-不支持
    public byte cbStreamKindVal; //当前码流类型，1-定码流,2-变码流
    public byte cbVideoOutKindVal; //vout视频输出设置：0-disable, 1-CVBS, 2-HDMI,3-YPbPr等等
    public byte cbWDRVal; //该机型是否支持宽动态设置,0-不支持,1-支持
    public byte cbColorMode; //色彩风格设置 0-TV 1--PC
    public byte cbSharpNess; //锐度设置，取值范围为：[1,255]
    public byte cbPlatformType; //平台类别
    public byte[] cbReserve = new byte[17]; //保留
}

// 2011/09/22 NVR新增协议
// 命令号
//nvr新增28个协议
//#define NET_SDVR_STREAM_TYPE_NVR
//#define NET_SDVR_WORK_STATE_EX
//#define NET_SDVR_GET_CH_CLIENT_IP
//#define NET_SDVR_LOG_QUERY_EX
//#define NET_SDVR_SERIAL_START_NVR
//#define NET_SDVR_SERIAL_STOP_NVR
//#define NET_SDVR_DEVICECFG_GET_EX
//#define NET_SDVR_DEVICECFG_SET_EX
//#define NET_SDVR_PTZLIST_GET_NVR
//#define NET_SDVR_ALRM_ATTR_NVR
//#define NET_SDVR_ALRMIN_GET_NVR
//#define NET_SDVR_ALRMIN_SET_NVR
//#define NET_SDVR_ALRMOUT_GET_NVR
//#define NET_SDVR_ALRMOUT_SET_NVR
//#define NET_SDVR_ALRMIN_STATUS_GET_NVR
//#define NET_SDVR_ALRMOUT_STATUS_GET_NVR
//#define NET_SDVR_ALRMOUT_STATUS_SET_NVR
//#define NET_SDVR_PICCFG_GET_EX_NVR
//#define NET_SDVR_PICCFG_SET_EX_NVR
//#define NET_SDVR_RECORD_GET_EX_NVR
//#define NET_SDVR_RECORD_SET_EX_NVR
//#define NET_SDVR_MOTION_DETECT_GET_NVR
//#define NET_SDVR_MOTION_DETECT_SET_NVR
//#define NET_SDVR_ABNORMAL_ALRM_GET_NVR
//#define NET_SDVR_ABNORMAL_ALRM_SET_NVR
//#define NET_SDVR_PARAM_FILE_EXPORT
//#define NET_SDVR_PARAM_FILE_IMPORT
//#define NET_SDVR_RESOLUTION_GET_NVR
//#define NET_SDVR_RESOLUTION_SET_NVR
//#define NET_SDVR_QUERY_MONTH_RECFILE


//数据流解码类型(实时视频、回放、备份)
//#define NET_SDVR_STREAM_TYPE_NVR       0xC0  
public class HB_STREAM_TYPE
{
    public byte[] sBuff = new byte[64]; // 文件头类型
}


//获取主机设备工作状态
//#define NET_SDVR_WORK_STATE_EX          0xC1 
// typedef struct	
// {
//     DWORD dwVolume;				//硬盘的容量（MB）
//     DWORD dwFreeSpace;			//硬盘的剩余空间（MB）
//     DWORD dwHardDiskStatic;		//硬盘状态（dwVolume有值时有效） 0-正常 1-磁盘错误 2-文件系统出错
// } STRUCT_SDVR_DISKSTATE;

public class HB_SDVR_CHANNELSTATE_EX
{
    public byte byRecordStatic; //通道是否在录像,0-不录像,1-录像
    public byte bySignalStatic; //连接的信号状态,0-正常,1-信号丢失
    public byte byHardwareStatic; //保留
    public byte byLinkNum; //客户端连接的个数：同一通道当前时间的实时流的连接数。不分主子码流，同一IP多个连接算多个连接
    public uint dwBitRate; //实际码率
}

public class HB_SDVR_WORKSTATE_V2
{
    public uint dwSize; //结构体大小
    public HB_SDVR_DISKSTATE_ST[] struHardDiskStatic = new HB_SDVR_DISKSTATE_ST[16]; //硬盘状态
    public HB_SDVR_CHANNELSTATE_EX[] struChanStatic = new HB_SDVR_CHANNELSTATE_EX[128]; //通道的状态
    public byte[] byAlarminStatusLocal = new byte[128]; //本地报警输入端口的状态
    public byte[] byAlarmoutStatusLocal = new byte[128]; //本地报警输出端口的状态
    public uint[] dwReserve = new uint[4]; //保留
}


//获取各通道实时视频流连接的客户端ip列表
//#define NET_SDVR_GET_CH_CLIENT_IP       0xC2
public class HB_CLIENT_IP_INFO
{
    public uint dwSize; //结构体大小
    public byte byChannel; //通道号[0, n-1],n:通道数
    public byte byClientIpNum; //此通道连接客户端IP个数
    public byte[] byReserve1 = new byte[2]; //保留
    public uint[] dwDwClientIP = new uint[64]; //客户端IP列表
    public uint[] dwReserve2 = new uint[4]; //保留
}


//日志命令结构体
//#define NET_SDVR_LOG_QUERY_EX           0xC3
public class HB_REQLOG_EX
{
    public ushort wYear; //年: 2000~2063
    public byte byMonth; //月: 1~12
    public byte byDay; //日: 1~31
    public ushort wStart; //查询从第几条开始，一般为0。
    public ushort wnum; //一次查询个数，最多为100。
    public byte byPriType; //主类型 （需扩展所有）
    public byte bySecType; //次类型
    public byte[] byReserve = new byte[6]; //保留
}

public class HB_SDVR_LOGINFO_EX
{
    public uint dwSize; //结构体大小
    public uint dwTotalLogNum; //日志总条数
    public uint dwCurLogNum; //本次查到的条数
    public uint dwStart; //本次查询到的日志的起始编号
    public byte byEncType; //编码格式 1- UTF-8 2-gb2312
    public byte[] byReserve = new byte[3]; //保留
    public byte[,] sSigalLogData = new byte[100, 128]; // 日志信息 (每次查询最多支持100条日志，日志多于100条 // 需要多次调用，每条128字节，每条以‘\0’结束)
}

public enum LOG_PRI_TYPE : int //主类型
{
    LOG_PRI_ALL = -1, //全部
    LOG_PRI_ERROR, // 异常
    LOG_PRI_ALARM, // 报警
    LOG_PRI_OPERATE, // 操作
    LOG_PRI_MAX
}

public enum LOG_OPERATE_TYPE : int //操作次类型
{
    LOG_OP_ALL = -1,
    LOG_OP_POWERON, // 开机
    LOG_OP_SHUTDOWN, // 关机
    LOG_OP_EXC_SHUTDOWN, //异常关机
    LOG_OP_REBOOT, // 重启
    LOG_OP_LOGIN, // 登陆
    LOG_OP_LOGOUT, // 注销
    LOG_OP_SETCONFIG, // 配置
    LOG_OP_FRONTEND_SETCONFIG, //前端设备配置
    LOG_OP_UPGRADE, // 升级
    LOG_OP_FRONTEND_UPGRADE, //前端设备升级
    LOG_OP_RECORD_START, // 本地启动手动录像
    LOG_OP_RECORD_STOP, // 本地停止手动录像
    LOG_OP_PTZ, // 云台控制
    LOG_OP_MANUAL_ALARM, //本地手动报警
    LOG_OP_FORMAT_DISK, // 格式化硬盘
    LOG_OP_FILE_PLAYBACK, // 本地回放文件
    LOG_OP_EXPORT_CONFIGFILE, //导出本地配置文件
    LOG_OP_LMPORT_CONFIGFILE, //导入本地配置文件
    LOG_OP_FREXPORT_CONFIGFILE, //导出前端设备配置文件
    LOG_OP_FRLMPORT_CONFIGFILE, //导入前端设备配置文件
    LOG_OP_BACKUP_REC, //本地备份录像文件
    LOG_OP_DEFAULT, //本地恢复缺省
    LOG_OP_SETTIME, // 本地设置系统时间
    LOG_OP_TRANSCOM_OPEN, // 建立透明通道
    LOG_OP_TRANSCOM_CLOSE, // 断开透明通道
    LOG_OP_TALKBACK_START, // 对讲开始
    LOG_OP_TALKBACK_STOP, // 对讲结束

    LOG_OP_TYPE_NONE, // 无录像
    LOG_OP_TYPE_MANUAL, // 手动录像
    LOG_OP_TYPE_TIME, // 定时录像
    LOG_OP_TYPE_MOTION, // 移动录像
    LOG_OP_TYPE_SENSOR, // 探头报警
    LOG_OP_TYPE_MOTION_OR_SENSOR, // 移动或报警录像
    LOG_OP_TYPE_MOTION_AND_SENSOR, // 移动与探头报警
    LOG_OP_REMOTE_LOGIN, // 前端设备登陆
    LOG_OP_REMOTE_LOGOUT, // 前端设备注销

    LOG_OP_TYPE_MAX
}

public enum LOG_ALARM_TYPE : int //报警次类型
{
    LOG_ALM_ALL = -1,
    LOG_ALM_LOCAL_SENSORIN, // 本地报警输入
    LOG_ALM_LOCAL_SENSOROUT, // 本地报警输出
    LOG_ALM_FRONTEND_SENSORIN, //前端设备报警输入
    LOG_ALM_FRONTEND_SENSOROUT, //前端设备报警输出
    LOG_ALM_MOTION_START, // 移动侦测开始
    LOG_ALM_MOTION_STOP, // 移动侦测结束
    LOG_ALM_MAIL_UPLOAD, // 邮件上传
    LOG_ALM_TYPE_MAX
}

public enum LOG_ERROR_TYPE : int //错误次类型
{
    LOG_ERR_ALL = -1,
    LOG_ERR_VIDEOLOST, // 视频丢失
    LOG_ERR_HD_ERROR, // 磁盘错误
    LOG_ERR_HD_FULL, // 磁盘满
    LOG_ERR_LOGIN_FAIL, // 登陆失败
    LOG_ERR_TEMP_HI, // 温度过高
    LOG_ERR_HD_PFILE_INDEX, // 磁盘主索引错误
    LOG_ERR_HD_DEV_FATAL, // 磁盘设备致命错误
    LOG_ERR_IP_CONFLICT, //ip冲突
    LOG_ERR_NET_EXCEPTION, //网络异常
    LOG_ERR_REC_EXCEPTION, //录像异常
    LOG_ERR_FRONTEND_EXCEPTION, //前端设备异常
    LOG_ERR_TIME_EXCEPTION, //时间异常
    LOG_ERR_FRONTBOARD_EXCEPTION, //前面板异常
    LOG_ERR_TYPE_MAX
}

//建立nvr透明通道
//#define NET_SDVR_SERIAL_START_NVR       0xC4
//关闭NVR透明通道
//#define NET_SDVR_SERIAL_STOP_NVR        0xC5  
public class HB_NVR_SERIAL_START
{
    public byte byOpType; //0-（混合DVR,设置本地），1-前端设备所属的 (为1时byChannel生效)
    public byte bySeriaType; //一个字节的串口类型，1：232 2：485。
    public byte byChannel; //[0, n-1],n:通道数
    public byte[] byReserve = new byte[5]; //保留
}


//获取设备信息扩展
//#define NET_SDVR_DEVICECFG_GET_EX       0xC6
//设置设备信息(扩展)
//#define NET_SDVR_DEVICECFG_SET_EX       0xC7  
public class HB_DEVICEINFO_V2
{
    public uint dwSize; //结构体大小
    public byte[] sDVRName = new byte[32]; //设备, 以’\0’结束字符串
    public uint dwDVRID; //保留
    public uint dwRecycleRecord; //协议二: //录像覆盖策略 0-循环覆盖 1-提示覆盖
    public byte[] sSerialNumber = new byte[48]; //序列号
    public byte[] sSoftwareVersion = new byte[64]; //软件版本号以’\0’结束字符串协议二: （主机型号 软件版本号）
    public byte[] sSoftwareBuildDate = new byte[32]; //软件生成日期以’\0’结束字符串协议二:（Build 100112）
    public uint dwDSPSoftwareVersion; //DSP软件版本
    public byte[] sPanelVersion = new byte[32]; //前面板版本，以’\0’结束字符串，IPC无
    public byte[] sHardWareVersion = new byte[32]; //(保留)协议二: 当软件版本号超过16字节时会使用作为主机型号显示
    public byte byAlarmInPortNum; //报警输入个数, NVR只取本地报警输入
    public byte byAlarmOutPortNum; //报警输出个数, NVR只取本地报警输出
    public byte byRS232Num; //保留
    public byte byRS485Num; //保留
    public byte byNetworkPortNum; //保留
    public byte byDiskCtrlNum; //保留
    public byte byDiskNum; //硬盘个数
    public byte byDVRType; //DVR类型, 1:NVR 2:ATM NVR 3:DVS 4:IPC 5:NVR （建议使用//NET_SDVR_GET_DVRTYPE命令）
    public byte byChanNum; //通道个数[0, 128]
    public byte byStartChan; //保留
    public byte byDecordChans; //保留
    public byte byVGANum; //保留
    public byte byUSBNum; //保留
    public byte[] byReserve = new byte[3]; //保留
}


//获取nvr云台协议列表
//#define NET_SDVR_PTZLIST_GET_NVR        0xC8
public class HB_NVR_PTZLIST
{
    public byte byType; //0-NVR本地云台，1-前端设备云台 (为1时byChannel生效)
    public byte byChannel; //[0, n-1],n:通道数
    public byte[] byReserve = new byte[2]; //保留
}

public class HB_NVR_PTZLIST_INFO
{
    public uint dwPtznum; //协议个数（限制为最多100个）
    public byte[] byReserve = new byte[4]; //保留
    public byte[,] sPtzType = new byte[100, 10]; //协议名列表―――0，unknow;
}

//获取NVR报警输入输出端口属性
//#define NET_SDVR_ALRM_ATTR_NVR                  0xC9 
public class HB_NVR_ALRM_PORT_ATTR
{
    public uint dwSize; // 结构体大小
    public byte byOpType; // 0-本地 1-前端 (为1时,byChannel有效)
    public byte byChannel; // 操作前端某通道设备 [0, n-1], n:通道个数
    public byte byAlarmInNum; // 此设备拥有报警输入个数
    public byte byAlarmOutNum; // 此设备拥有报警输出个数
    public byte[,] sAlarmInPortName = new byte[128, 32]; // 报警输入端口名， 以’\0’结束字符串
    public byte[,] sAlarmOutPortName = new byte[16, 32]; // 报警输出端口名， 以’\0’结束字符串
    public byte[] sDevName = new byte[32]; // 通道对应设备名称
    public uint dwIP; // 设备IP
    public byte[] byReserve = new byte[4]; // 保留
}


//获取nvr报警输入参数
//#define NET_SDVR_ALRMIN_GET_NVR         0xCA
//设置nvr报警输入参数
//#define NET_SDVR_ALRMIN_SET_NVR         0xCB	

public class HB_NVR_ALRMININFO
{
    public uint dwSize; //结构体大小
    public byte byOpType; //0-本地 1-前端 (为1时,byChannel有效)
    public byte byChannel; //操作前端某通道设备 [0, n-1], n:通道个数
    public byte byAlarmInPort; //报警输入端口号[0, n-1], n:报警输入个数
    public byte[] sAlarmInName = new byte[32]; //报警输入端口名， 以’\0’结束字符串
    public byte byAlarmType; //探头类型 0-常闭1-常开
    public byte byEnSchedule; //报警输入布防时间激活 0-屏蔽 1-激活
    public byte byWeekEnable; //每天使能位0-不使能 1-使能(若使能,只取struAlarmTime[0][0~7]来设置每一天)
    public byte[] byAllDayEnable = new byte[8]; //全天使能 ,0-不使能 1-使能若此项使能,则对应的天为全天布防,不用判断时间段
    public HB_SDVR_SCHEDTIME_V2_ST[,] struAlarmTime = new HB_SDVR_SCHEDTIME_V2_ST[8, 8]; //布防时间段
    public uint dwHandleType; //按位 2-声音报警 5-监视器最大化 //6-邮件上传

    // 联动报警输出
    public byte[] byAlarmOutLocal = new byte[16]; //报警输出端口(本地)
    public byte[,] byAlarmOutRemote = new byte[128, 16]; //报警输出端口(前端设备)

    // 联动录像
    public byte[] byRelRecordChan = new byte[128]; //报警触发的录象通道,为1表示触发该通道 0为不触发

    // 联动其他
    public byte[] byEnablePreset = new byte[128]; //是否调用预置点 仅用byEnablePreset[0]来判断
    public byte[] byPresetNo = new byte[128]; //调用的云台预置点序号,一个报警输入可以调用多个通道的云台预置点, 0xff表示不调用预置点 [1, 254]
    public byte[] byReserve = new byte[32]; //保留
}


//获取nvr报警输出参数
//#define NET_SDVR_ALRMOUT_GET_NVR        0xCC
//设置nvr报警输出参数
//#define NET_SDVR_ALRMOUT_SET_NVR        0xCD
public class HB_NVR_ALARMOUTINFO
{
    public uint dwSize; // 结构体大小
    public byte byOpType; // 0-本地 1-前端 (为1时,byChannel有效)
    public byte byChannel; // 操作前端某通道设备 [0, n-1], n:通道个数
    public byte byALarmOutPort; // 报警输出通道号 [0, n-1], n:报警输出端口数
    public byte[] sAlarmOutName = new byte[32]; // 名称 以’\0’结束字符串
    public uint dwAlarmOutDelay; // 输出保持时间 单位秒 [2, 300]
    public byte byAlarmType; // 探头类型 0-常闭1-常开 (保留)
    public byte byEnSchedule; // 报警输出布防时间激活 0-屏蔽 1-激活
    public byte byWeekEnable; // 每天使能位0-不使能 1-使能(若使能,只取struAlarmTime[0][0~7]对每天做设置)
    public byte[] byFullDayEnable = new byte[8]; // 完整天录像 0-不使能(缺省值) 1-使能
    public HB_SDVR_SCHEDTIME_V2_ST[,] struAlarmTime = new HB_SDVR_SCHEDTIME_V2_ST[8, 8]; //布防时间段, 8个时间段

    public byte[] byReserve = new byte[32]; // 保留
}


//获取nvr报警输入状态
//#define NET_SDVR_ALRMIN_STATUS_GET_NVR  0xCE
public class HB_NVR_ALRMIN_STATUS
{
    public byte byOpType; // 0-本地 1-前端 (为1时,byChannel有效)
    public byte byChannel; // 操作前端某通道设备 [0, n-1], n:通道个数
    public byte byAlarm; // 保留
    public byte byReserve1; // 保留
    public byte[] byAlarmIn = new byte[128]; // 报警输入状态 128个报警输入, 0-无输入 1-有输入
    public byte[] byRreserve2 = new byte[32]; // 保留
}

//获取nvr报警输出状态
//#define NET_SDVR_ALRMOUT_STATUS_GET_NVR 0xCF
//设置nvr报警输出状态
//#define NET_SDVR_ALRMOUT_STATUS_SET_NVR 0xD1
public class HB_NVR_ALRMOUT_STATUS
{
    public byte byOpType; // 0-本地 1-前端 (为1时,byChannel有效)
    public byte byChannel; // 操作前端某通道设备 [0, n-1], n:通道个数
    public byte byAlarm; // 报警输出状态 0-不报警 1-报警
    public byte byReserve1; // 保留
    public byte[] byAlarmOut = new byte[16]; // 报警输出状态 16个报警输出, 获取命令时,byAlarm无效,byAlarmOut[16]中为1的是有输出,0为无输出；设置命令时byAlarm有效，byAlarmOut[16]中0-状态不变 1-执行byAlarm操作
    public byte[] byReserve2 = new byte[32];
}


//获取nvr通道参数
//#define NET_SDVR_PICCFG_GET_EX_NVR      0xD2
//设置nvr通道参数
//#define NET_SDVR_PICCFG_SET_EX_NVR      0xD3 
// typedef struct	
// {
//     WORD wHideAreaTopLeftX;				    // 遮盖区域的x坐标  0~704
//     WORD wHideAreaTopLeftY;				    // 遮盖区域的y坐标  0~576
//     WORD wHideAreaWidth;					// 遮盖区域的宽 0~704
//     WORD wHideAreaHeight;					// 遮盖区域的高 0~576
// } HB_SDVR_SHELTER;

public class VIDEO_INFO
{
    public byte byBrightness; // 亮度 取值范围[0，255] 缺省值128
    public byte byConstrast; // 对比度 取值范围[0，255] 缺省值128
    public byte bySaturation; // 饱和度 取值范围[0，255] 缺省值128
    public byte byHue; // 色度 取值范围[0，255] 缺省值128
    public byte bySharp; // 锐度 0（缺省值）或1
    public uint dwReserved; // 预留
}

public class HB_NVR_CHN_ATTR_INFO
{
    public uint dwSize; // 长度(结构体大小)

    // 通道名相关
    public byte[] sChanName = new byte[32]; // 通道名 以’\0’结束字符串
    public byte byChannel; // 通道号 [0, n－1] n:通道数
    public uint dwShowChanName; // 是否显示通道名 0-显示 1-不显示
    public byte byOSDAttrib; // 通道名 1-不透明 2-透明（只针对PC端显示）
    public ushort wShowNameTopLeftX; // 通道名称显示位置的x坐标 左->右 0~视频实际宽度－通道名长度
    public ushort wShowNameTopLeftY; // 通道名称显示位置的y坐标 上->下 0~视频实际高度－字体高度

    // 日期相关
    public uint dwShowTime; // 是否显示时间 0-显示 1-不显示
    public ushort wOSDTopLeftX; // 时间osd坐标X [0, 实际宽－时码长度]
    public ushort wOSDTopLeftY; // 时间osd坐标Y[0, 实际高－字体高度]
    public byte byDateFormat; // 日期格式
                              //  0 - YYYY-MM-DD    （缺省值）
                              //  1 - MM-DD-YYYY
                              //  2 - YYYY年MM月DD日
                              //  3 - MM月DD日YYYY年

    // 星期相关
    public byte byDispWeek; // 是否显示星期 0-显示 1-不显示
    public byte byOSDLanguage; // 星期语言 0-中文 1-英文 (可扩展)

    // 亮度色度相关
    public VIDEO_INFO struVideoInfo = new VIDEO_INFO(); // 视频信息

    // 遮挡区域相关
    public uint dwEnableHide; // 视频遮挡使能 ,0-不遮挡,1-遮挡(遮挡区域全黑) 2-遮挡(遮挡区域马赛克)
    public HB_SDVR_SHELTER[] struShelter = new HB_SDVR_SHELTER[16]; // 视频遮挡区域
    public uint dwOsdOverType; // osd叠加类型 0-不叠加 1-前端叠加 2-后端叠加
    public uint[] dwReserve = new uint[32]; // 保留
}



//获取nvr录像参数
//#define NET_SDVR_RECORD_GET_EX_NVR      0xD4
//设置nvr录像参数
//#define NET_SDVR_RECORD_SET_EX_NVR      0xD5
public class HB_RECORD_PARAM
{
    public byte byStreamtype; // 流类型 0-变码流（缺省值） 1-定码流
    public byte byQuality; // 视频质量 1-最高 2-较高 3-高（缺省值） 4-中 5-低 6-最低
    public byte byResolution; // 主码流 0-DIF 1-D1（缺省值） 2-720P 3-1080P
                              // 子码流     0-CIF 1-D1(缺省值)
    public byte byFramerate; // 帧率 取值范围[2,25] 缺省值25
    public byte byMaxbitrate; // 码流(主) 0-100K 1-128K 2-256K 3-512K 4-1M 5-1.5M 6-2M（缺省值） 7-3M 8-4M 9-6M 10-8M
                              // 码流（子） 0-30K 1-45K 2-60K 3-75K 4-90K 5-100K 6-128K 7-256K 8-512K(缺省值) 9-1M 10-2M
    public byte byAudio; // 音频标识 0-无音频 1-有音频（缺省值）
    public uint dwReserved; // 预留
}

public class HB_REC_TIME_PERIOD
{
    public byte byStarth; // 起始时间-时
    public byte byStartm; // 起始时间-分
    public byte byStoph; // 结束时间-时
    public byte byStopm; // 结束时间-分
    public byte byRecType; // 录像类型 0 - 无 1-手动(无效) 2-定时 3-移动 4-报警 5-移动 | 报警 6 -移动 & 报警
    public byte[] byReserve = new byte[3]; // 保留
}

public class HB_FULL_DAY_S
{
    public byte byEnable; // 完整天使能 0-不使能(缺省值) 1-使能
    public byte byRecType; // 完整天对应的录像类型 0 - 无 1-手动(无效) 2-定时 3-移动 4-报警 5-移动 | 报警 6- 移动 & 报警
    public byte[] byReserve = new byte[2]; // 保留
}

public class HB_REC_TIME_SCHEDULE
{
    public byte byEnable; // 使能时间表 0-不使能(缺省值) 1-使能
    public byte byWeekEnable; // 每天使能位 0-不使能 1-使能(若使能,只取struAlarmTime[0][0~7]对每天做设置)
    public HB_FULL_DAY_S[] struFullDayEnable = new HB_FULL_DAY_S[8]; // 完整天录像
    public HB_REC_TIME_PERIOD[,] struAlarmTime = new HB_REC_TIME_PERIOD[8, 8]; // [0-7][0]代表全天使能的设置项

    public uint dwReserved; // 预留
}

public class HB_RECORD_SET
{
    public uint dwSize; // 结构体大小
    public byte byChannel; // 通道号
    public ushort wPreRecTime; // 预录时间 取值范围[5，30]秒 缺省值10
    public uint dwDelayRecTime; // 录像持续时间 取值范围[0,180]秒 缺省值30 (对3-移动录像 4-报警录像 5-移动 | 报警 6-移动 & 报警 有效)
    public HB_REC_TIME_SCHEDULE struTimeSchedule = new HB_REC_TIME_SCHEDULE(); // 录像时间表与录像类型设置

    public HB_RECORD_PARAM struTimeRecord = new HB_RECORD_PARAM(); // 定时 录像参数
    public HB_RECORD_PARAM struMoveRecord = new HB_RECORD_PARAM(); // 移动 录像参数
    public HB_RECORD_PARAM struAlarmRecord = new HB_RECORD_PARAM(); // 报警 录像参数
    public HB_RECORD_PARAM struMoveOrAlarmRecord = new HB_RECORD_PARAM(); // 移动 | 报警 录像参数
    public HB_RECORD_PARAM struMoveAndAlarmRecord = new HB_RECORD_PARAM(); // 移动 & 报警 录像参数
    public HB_RECORD_PARAM[] struNetRecParam = new HB_RECORD_PARAM[4]; // 子码流 录像参数 (保留)

    public uint byLinkMode; // 码流类型(0-主码流， 1-第一子码流，2-第二子码流....)
    public uint[] dwReserved = new uint[31]; // 预留
}


//获取nvr移动侦测参数
//#define NET_SDVR_MOTION_DETECT_GET_NVR  0xD6
//设置nvr移动侦测参数
//#define NET_SDVR_MOTION_DETECT_SET_NVR  0xD7
public class HB_NVR_MOTION
{
    public uint dwSize; // 长度（结构体大小）
    public byte byChannel; // 通道号 [0, n－1] n:通道数

    public byte[,] byMotionScope = new byte[18, 22]; // 侦测区域,共有32*32个小宏块,为1表示该宏块是移动侦测区域,0-表示不是
    public byte byMotionSensitive; // 移动侦测灵敏度, 0 - 5,越高越灵敏

    // 时间表相关
    public byte byEnableHandleMotion; // 移动侦测布防使能 0-撤防 1-布防
    public byte byWeekEnable; // 设置每天0-不使能 1-使能(若使能,只取struAlarmTime[0][0~7]对每天做设置)
    public byte[] byFullDayEnable = new byte[8]; // 完整天录像 0-不使能(缺省值) 1-使能,若此项使能,则对应的天为全天布防,不用判断时间段
    public HB_SDVR_SCHEDTIME_V2_ST[,] struAlarmTime = new HB_SDVR_SCHEDTIME_V2_ST[8, 8]; // 布防时间段, 8个时间段
    public uint dwHandleType; // 按位 2-声音报警5-监视器最大化 //6-邮件上传

    // 联动报警输出
    public byte[] byAlarmOutLocal = new byte[16]; // 报警输出端口(本地)
    public byte[,] byAlarmOutRemote = new byte[128, 16]; // 报警输出端口(前端设备)

    // 联动录像    
    public byte[] byRecordChannel = new byte[128]; // 联动的录像通道，为0-不联动 1-联动

    // 联动其他  
    public byte[] byEnablePreset = new byte[128]; // 是否调用预置点 仅用byEnablePreset[0]来判断;
    public byte[] byPresetNo = new byte[128]; // 调用的云台预置点序号,一个报警输入可以调用多个通道的云台预置点, 0xff表示不调用预置点 [1, 254]
    public uint[] dwReserve = new uint[32]; // 保留
}




//获取nvr异常报警参数
//#define NET_SDVR_ABNORMAL_ALRM_GET_NVR  0xD8
//设置nvr异常报警参数
//#define NET_SDVR_ABNORMAL_ALRM_SET_NVR  0xD9
public class HB_NVR_ABNORMAL
{
    public uint dwSize; // 长度（结构体大小）
    public byte byAbnormalAlarmType; // 1-视频丢失 2-网络断开 3-温度过高 4-遮挡报警...
    public byte byChannel; // 通道号 (对视频丢失, 遮挡报警, 网络断开…有效)

    public byte byEnableAbnormal; // 异常报警使能 0-不报警 1-报警；
    public ushort wHideAlarmAreaTopLeftX; // [0, 实际宽－1]（遮挡报警时有效）
    public ushort wHideAlarmAreaTopLeftY; // [0, 实际高－1]（遮挡报警时有效）
    public ushort wHideAlarmAreaWidth; // [16, 实际宽] （遮挡报警时有效）
    public ushort wHideAlarmAreaHeight; // [16, 实际高] （遮挡报警时有效）

    // 联动报警输出
    public byte[] byAlarmOutLocal = new byte[16]; //报警输出端口(本地)
    public byte[,] byAlarmOutRemote = new byte[128, 16]; //报警输出端口(前端设备)

    public byte byWeekEnable; // 设置每天0-不使能 1-使能(若使能,只取struAlarmTime[0][0~7]对每天做设置)
    public byte[] byFullDayEnable = new byte[8]; // 完整天录像 0-不使能(缺省值) 1-使能,若此项使能,则对应的天为全天布防,不用判断时间段
    public HB_SDVR_SCHEDTIME_V2_ST[,] struAlarmTime = new HB_SDVR_SCHEDTIME_V2_ST[8, 8]; //布防时间段, 8个时间段
    public uint dwHandleType; // 处理方式 按位 2-声音报警5-监视器最大化 //6-邮件上传

    public uint[] dwReserve = new uint[32]; // 保留
}



//数文件导出
//#define NET_SDVR_PARAM_FILE_EXPORT      0xDA
public class HB_EXPT_REQ
{
    public uint dwFileSize; // 导出文件的大小
    public uint dwReserve; // 保留
                           //    BYTE *pFileData; 	    // 参数文件数据
}


//参数文件导入
//#define NET_SDVR_PARAM_FILE_IMPORT      0xDB 
public class HB_IMPT_REQ
{
    public uint dwFileSize; // 导出文件的大小
    public uint dwReserve; // 保留
}

public class HB_NVR_RESOLUTION
{
    public byte[] bySupportResolution = new byte[32]; // 该参数表示nvr主机支持的输出分辨率格式，
                                                      // 数组下标代表映射表序号(映射表如下)，1表示支持，0表示不支持
    public byte byCurResolution; // 主机端当前输出分辨率
    public byte[] reserve = new byte[7]; // 保留
}

public class HB_LOG_REQ_PARAM_ST
{
    public ushort wBeginYear;
    public byte byBeginMon;
    public byte byBeginDay;

    public ushort wEndYear;
    public byte byEndMon;
    public byte byEndDay;

    public byte byPriType;
    public byte bySecType;
    public uint[] dwReserve = new uint[4];
}

//#define HB_IMPORT_OK
//#define HB_TRANS_FILE_ERR
//#define HB_FILE_VERSION_ERR

public class HB_PARAMFILE_STATUS_ST
{
    public uint dwFileSize;
    public uint dwGotSize;
    public uint dwStatus; // 导入状态码，仅导入时有效
    public uint[] dwReserve = new uint[4];
}

//#define NET_SDVR_GET_ZERO_VENCCONF
//#define NET_SDVR_SET_ZERO_VENCCONF

// 复合通道编码参数配置
//#define MAX_CHANNUM 128
public enum NET_AUSTREAMADD_E : int
{
    NET_AUSTREAM_DISABLE, //视频流
    NET_AUSTREAM_ENABLE, //复合流
}

public enum NET_RESOLUTION_E : int
{
    NET_QCIF, //QCIF
    NET_QVGA, //QVGA
    NET_CIF, //CIF
    NET_DCIF, //DCIF
    NET_HD1, //HD1
    NET_VGA, //VGA
    NET_FD1, //FD1
    NET_SD, //SD
    NET_HD //HD
}

public enum NET_BITRATETYPE_E : int
{
    NET_BITRATE_CHANGE, //变码率
    NET_BITRATE_NOCHANGE, //定码率
}

public enum NET_VQUALITY_E : int
{
    NET_VQUALITY_BEST = 0, //最高
    NET_VQUALITY_BETTER, //较高
    NET_VQUALITY_GOOD, //高
    NET_VQUALITY_NORMAL, //中
    NET_VQUALITY_BAD, //低
    NETT_VQUALITY_WORSE //最低
}

public class HB_SDVR_VENC_CONFIG
{
    public NET_AUSTREAMADD_E byStreamType = new NET_AUSTREAMADD_E(); //视频流类型
    public NET_RESOLUTION_E byResolution = new NET_RESOLUTION_E(); //视频分辨率
    public NET_BITRATETYPE_E byBitrateType = new NET_BITRATETYPE_E(); //码率类型
    public NET_VQUALITY_E byPicQuality = new NET_VQUALITY_E(); //图像质量
    public uint dwVideoBitrate; //视频码率 实际码率
    public ushort dwVideoFrameRate; //帧率 PAL 2-30 N 2-30
    public ushort quant; //量化因子 1-31
}

public class HB_SDVR_ZERO_VENC_CONFIG
{
    public uint enable; // 零通道使能，1-启用，0-不启用
    public string chlist = new string(new char[HBConst.MAX_CHANNUM_EX]); // 零通道画面格式输出, 对应通道按数组下标从0开始表示, 0-不选中, 1-选中
    public HB_SDVR_VENC_CONFIG venc_conf = new HB_SDVR_VENC_CONFIG(); //编码参数
    public uint reserve; //保留
}


//public class TAG_HB_SDVR_VOD_PARAM
//{
//    public byte byChannel; // 通道号 [0, n-1],n:通道数
//    public byte byType; // 录像类型:1-手动，2-定时，4-移动，8-报警，0xFF-全部
//    public ushort wLoadMode; // 回放下载模式 1-按时间， 2-按名字
////C++ TO C# CONVERTER TODO TASK: Unions are not supported in C#.
////ORIGINAL LINE: union
////C++ TO C# CONVERTER NOTE: Structs must be named in C#, so the following struct has been named AnonymousStruct6:
//    public struct AnonymousStruct6
//    {
////C++ TO C# CONVERTER NOTE: Classes must be named in C#, so the following class has been named AnonymousClass6:
//        public class AnonymousClass6
//        {
//            public HB_SDVR_TIME struStartTime = new HB_SDVR_TIME(); // 最多一个自然天
//            public HB_SDVR_TIME struStopTime = new HB_SDVR_TIME(); // 结束时间最多到23:59:59,
//            // 即表示从开始时间开始一直播放
//            public string cReserve = new string(new char[16]);
//        }
//        public AnonymousClass6 byTime = new AnonymousClass6();

//        public byte[] byFile = new byte[64]; // 是否够长？
//    }
//    public AnonymousStruct6 mode = new AnonymousStruct6();

//    public uint[] dwReserve = new uint[4]; // 保留
//}

//////////////////////////////////////////
////////IPC新协议
/////////////////////////////////////////
//#define HB_IPCCFG_THERMAL_IMAGING
//#define HB_IPCCFG_IP_FILTER
//#define HB_IPCCFG_BLC
//#define HB_IPCCFG_PROTOCL

public class TAG_HB_SDVR_IPC_THERMAL_IMAGING
{
    public uint dwSize; // 结构体长度
    public sbyte shutter_correct; // 快门校正 0:not support 1:close 2:open
    public sbyte electronic_enlarge; // 电子放大 0:not support 1:normal_display 2:enlarge_display
    public sbyte pseudo_colormode; // 伪彩模式 0:not support 1:white hot 2:black hot 3:fusion 4:rainbow 5:globow
                                   // 6:ironbow1 7:ironbow2 8:sepia 9:color1 10:color2 11:icefire 12:rain
    public sbyte auto_correct_switch; // 自动校正开关 0:not support 1:close 2:open
    public sbyte auto_agcmode; // 自动增益模式 0:不支持 1:自动增益 2:线性直方图 3:一次亮度 4:自动亮度 5:手动
    public sbyte contrast; // 对比度 （0～255）
    public short light; // 亮度 -1:不支持
    public short light_bias; // 亮度偏置 -1:不支持
    public short[] reserve = new short[3]; // 保留
}

public class TAG_HB_SDVR_IPC_IP_FILTER
{
    public uint dwSize; // sizeof(HB_SDVR_IPC_IP_FILTER)
    public sbyte cIPFilter; // 局域网IP地址过滤 0-不支持 1-打开 2-关闭
    public sbyte cIPRule; // 规则 1-允许通过 2-不允许通过
    public sbyte cPortFilter; // 端口过滤 0-不支持 1-打开 2-关闭
    public sbyte cPortRule; // 规则 1-允许通过 2-不允许通过
    public uint dwIPBegin; // 局域网IP起始地址
    public uint dwIPEnd; // 局域网IP结束地址，结束地址的值要大于起始地址，
                         // 如果结束地址为空，则认为只过滤起始地址
    public uint dwPortBegin; // 过滤端口起始
    public uint dwPortEnd; // 过滤端口结束
    public uint[] dwReserve = new uint[3]; // 保留
}

public class TAG_HB_SDVR_IPC_BLC
{
    public uint dwSize; // sizeof(HB_SDVR_IPC_BLC)
    public byte byBLCEnable; // 背光补偿 0-不支持 1-打开 2-关闭
    public byte[] byReserve = new byte[3]; // 保留
}

public class TAG_HB_SDVR_IPC_PROTOCL
{
    public uint dwSize;
    public byte byProtocl; // 0不支持， 1-7000sdk, 2-ONVIF(码流不带汉邦帧头)
    public byte[] byReserve = new byte[3];
}

//public class TAG_HB_SDVR_IPC_CFG
//{
//    public uint dwIPCCfgType; // 1:STRUCT_IPC_THERMAL_IMAGING 2:...
//    public uint dwSize; // sizeof(STRUCT_IPC_THERMAL_IMAGING)...
////C++ TO C# CONVERTER TODO TASK: Unions are not supported in C#, but the following union can be simulated with the StructLayout and FieldOffset attributes.
////ORIGINAL LINE: union
////C++ TO C# CONVERTER NOTE: Structs must be named in C#, so the following struct has been named AnonymousStruct7:
//    [StructLayout(LayoutKind.Explicit)]
//    public struct AnonymousStruct7
//    {
//        [FieldOffset(0)]
//        public TAG_HB_SDVR_IPC_THERMAL_IMAGING thermal = new TAG_HB_SDVR_IPC_THERMAL_IMAGING();
//        [FieldOffset(0)]
//        public TAG_HB_SDVR_IPC_IP_FILTER IPFilter = new TAG_HB_SDVR_IPC_IP_FILTER();
//        [FieldOffset(0)]
//        public TAG_HB_SDVR_IPC_BLC blc = new TAG_HB_SDVR_IPC_BLC();
//        [FieldOffset(0)]
//        public TAG_HB_SDVR_IPC_PROTOCL protocl = new TAG_HB_SDVR_IPC_PROTOCL();
//    }
//    public AnonymousStruct7 cfg = new AnonymousStruct7();
//}

public class TAG_HB_SDVR_IPSERVER
{
    public uint dwSize;
    public uint dwIP; // IPServer IP地址
    public uint dwPort; // IPServer 端口
    public byte[] byRegID = new byte[64]; // 设备注册码
    public string reserve = new string(new char[4]);
}




public enum HB_SDVR_IPC_PRI_CMD_TYPE_E : int //IPC命令分类命令
{
    IPC_VIDEO_CMD = 0, //IPC音视频命令
    IPC_NET_CMD, //IPC网络命令
    IPC_STORE_CMD, //IPC存储命令
    IPC_ALARM_CMD, //IPC报警命令
    IPC_MANAGE_CMD, //IPC管理命令
    IPC_PRI_CMD_MAX
}

//////////////////////////////////////////////////////////////////////////////
//音视频命令
//////////////////////////////////////////////////////////////////////////////
public enum HB_SDVR_IPC_VIDEO_CMD_TYPE_E : int
{
    IPC_IMAGE_PARAM = 0, //IPC图像参数
    IPC_VIDEO_PARAM, //IPC视频参数
    IPC_VIDEO_ENCODE, //IPC视频编码参数
    IPC_PICTURE_SNAP, //IPC图像抓拍参数
    IPC_OSD_OVERLAY, //IPC字符叠加参数
    IPC_MASK, //IPC遮挡参数
    IPC_ADVANCE_PARAM, //IPC高级参数
    IPC_AUDIO_IN_PARAM, //IPC音频输入参数
    IPC_AUDIO_OUT_PARAM, //IPC音频输出参数
    IPC_VIDEO_CMD_MAX
}

//////////////////////////////////////////////////////////////////////////////
//网络命令
//////////////////////////////////////////////////////////////////////////////
public enum HB_SDVR_IPC_NET_CMD_TYPE_E : int
{
    IPC_IP_PARAM = 0, //IPC 有线网络参数
                      //     IPC_WLAN,                   //IPC 无线网络参数
                      //     IPC_DDNS,                   //IPC DDNS参数    
    IPC_PPPOE, //IPC PPPOE参数
    IPC_E_MAIL, //IPC E_MAIL参数
                //    IPC_UPNP,                   //IPC UPNP参数  
    IPC_FTP, //IPC FTP参数
             //    IPC_NAS,                    //IPC NAS参数  
             //    IPC_AUTO_REGIST,            //IPC 自动注册参数
    IPC_PLATFORM, //IPC 平台服务器信息
                  //    IPC_IP_FILTER,              //IPC IP地址过滤参数   
    IPC_NET_CMD_MAX
}

//////////////////////////////////////////////////////////////////////////////
//存储命令
//////////////////////////////////////////////////////////////////////////////
public enum IPC_STORE_CMD_TYPE_E : int
{
    IPC_TIME_RECORD = 0, //IPC定时录像参数
    IPC_RECORD_MODE, //存储方式
    IPC_DISK_CFG, //IPC存储设备状态
    IPC_STORE_CMD_MAX //存储类命令最大值
}

//////////////////////////////////////////////////////////////////////////////
//报警命令
//////////////////////////////////////////////////////////////////////////////
public enum IPC_ALARM_CMD_TYPE_E : int
{
    IPC_MOTION = 0, //IPC移动侦测参数
    IPC_ALARMIN, //IPC报警输入参数
    IPC_ALARMOUT, //IPC报警输出参数
    IPC_NET_BUG, //IPC网络故障报警参数
    IPC_STORE_BUG, //IPC存储故障报警参数
    IPC_ALARM_CMD_MAX //报警类命令最大值
}


//////////////////////////////////////////////////////////////////////////////
//管理类命令
//////////////////////////////////////////////////////////////////////////////
public enum IPC_MANAGE_CMD_TYPE_E : int
{
    IPC_DEVICE_INFO = 0, //IPC设备信息
    IPC_AUTO_MAINTAINING, //IPC自动维护参数
    IPC_MANAGE_CMD_MAX //设备管理类命令最大值
}




//时间段
public class HB_SDVR_IPC_SYS_TIME
{
    public ushort uYear; //年
    public byte uMonth; //月
    public byte uDay; //日
    public byte uWeek; //星期
    public byte uHour; //小时
    public byte uMin; //分钟
    public byte uSec; //秒
}

public class HB_SDVR_IPC_TIME_PERIOD
{
    public byte uStartHour; //开始
    public byte uStartMin;
    public byte uEndHour; //结束
    public byte uEndMin;
}

public class HB_SDVR_IPC_TIME_ITEM
{
    public byte[] uSelect = new byte[4]; //0-no 1-yes
    public HB_SDVR_IPC_TIME_PERIOD[] Period = new HB_SDVR_IPC_TIME_PERIOD[4];
}

public class HB_SDVR_IPC_TIME_SCHEDULE
{
    public int nStatus; //0-off, 1-on
    public HB_SDVR_IPC_TIME_ITEM[] Item = new HB_SDVR_IPC_TIME_ITEM[8]; // 0-7: everyday, monday...sunday
}
//视频、音频
public class HB_SDVR_IMAGE_PARAM_ITEM
{
    public HB_SDVR_IPC_TIME_PERIOD Time = new HB_SDVR_IPC_TIME_PERIOD();
    public short sBrightValue;
    public short sContrastValue;
    public short sSaturationValue;
    public short sHueValue;
    public short sSharpness;
    public short sReserve;
}

public class HB_SDVR_IPC_IMAGE_PARAM
{
    public int nLens;
    public int nChannelid;
    public HB_SDVR_IMAGE_PARAM_ITEM[] Item = new HB_SDVR_IMAGE_PARAM_ITEM[3];
}

public class HB_SDVR_IPC_LENS
{
    public int nLensIris;
    public int nLensIndex;
    public byte[,] uLensVal = new byte[16, 32];
}

public class HB_SDVR_IPC_AE
{
    public int nAeContrl;
    public int nShutterIndex;
    public int[] nShutterVal = new int[32];
    public int nAgcIndex;
    public int[] nAgcVal = new int[32];
}

public class HB_SDVR_IPC_ICR_ABILITY
{
    public int nMaxValue;
    public int nMinValue;
    public int nCurValue;
}

public class HB_SDVR_IPC_DAY_NIGHT
{
    public int nDaynightModel;
    public int nIcrEnable;
    public int nLightRange;
    public HB_SDVR_IPC_ICR_ABILITY BlackwhiteValue = new HB_SDVR_IPC_ICR_ABILITY();
    public HB_SDVR_IPC_ICR_ABILITY ColorValue = new HB_SDVR_IPC_ICR_ABILITY();
    public int nMinInterval;
    public HB_SDVR_IPC_TIME_PERIOD[] TimeRange = new HB_SDVR_IPC_TIME_PERIOD[2];
}

public class HB_SDVR_IPC_VIDEO_PARAM
{
    public int nLens;
    public int nChannelid;
    public HB_SDVR_IPC_AE AeModel = new HB_SDVR_IPC_AE();
    public HB_SDVR_IPC_DAY_NIGHT Daynight = new HB_SDVR_IPC_DAY_NIGHT();
    public HB_SDVR_IPC_LENS IrisModel = new HB_SDVR_IPC_LENS();
    public int nImageFlip;
    public int nImagSpin;
    public int nSceneModel;
}

public class HB_SDVR_IPC_RESOLUTION_PARAM
{
    public int nResolutionIndex;
    public int[,] nResolution = new int[32, 2];
}

public class HB_SDVR_IPC_FRAMERATE
{
    public short sMinFramerate;
    public short sMaxFramerate;
    public short sCurFramerate;
    public short sReserve;
}

public class HB_SDVR_IPC_STREAM_PARAM
{
    public int nEnable;
    public int nEncodeType;
    public short sStreamControl;
    public short sIFrame;
    public int nVideoBitrate;
    public HB_SDVR_IPC_RESOLUTION_PARAM Resolution = new HB_SDVR_IPC_RESOLUTION_PARAM();
    public HB_SDVR_IPC_FRAMERATE Framerate = new HB_SDVR_IPC_FRAMERATE();
}

public class HB_SDVR_IPC_VIDEO_ENCODE
{
    public int nLens;
    public int nChannelid;
    public int nEncodeModel;
    public int nStreamType;
    public HB_SDVR_IPC_STREAM_PARAM[] Stream = new HB_SDVR_IPC_STREAM_PARAM[4];
    public int nReserved;
}

public class HB_SDVR_IPC_OSD_POS
{
    public int nPosx;
    public int nPosy;
    public int nReserved;
}

public class HB_SDVR_IPC_OSD_USERDEF
{
    public int nEnable;
    public HB_SDVR_IPC_OSD_POS OsdPos = new HB_SDVR_IPC_OSD_POS();
    public string cOsdStrings = new string(new char[128]);
    public int nReserved;
}

public class HB_SDVR_IPC_OSD_OVERLAY
{
    public int nLens;
    public int nChannelid;
    public int nOsdEnable;
    public short sOsdAttribute;
    public short sOsdAddModel;
    public short sChNameEnable;
    public short sTimeInfoEnable;
    public short sWeekDisplay;
    public short sTimeFormat;
    public string cChName = new string(new char[32]);
    public HB_SDVR_IPC_OSD_POS ChNamePos = new HB_SDVR_IPC_OSD_POS();
    public HB_SDVR_IPC_OSD_POS TimePos = new HB_SDVR_IPC_OSD_POS();
    public HB_SDVR_IPC_OSD_USERDEF[] OsdUserdef = new HB_SDVR_IPC_OSD_USERDEF[8];
}

public class HB_SDVR_IPC_MASK_AREA
{
    public int nStartx;
    public int nStarty;
    public int nWidth;
    public int nHeight;
}

public class HB_SDVR_IPC_MASK
{
    public int nLens;
    public int nChannelid;
    public int nMaskEnable;
    public HB_SDVR_IPC_MASK_AREA[] Maskarea = new HB_SDVR_IPC_MASK_AREA[8];
}


//网络
public class HB_SDVR_IPC_IPV4
{
    public byte[] uIp = new byte[4];
    public byte[] uMask = new byte[4];
    public byte[] uGateway = new byte[4];
    public byte[] uDns = new byte[4];
    public byte[] uDnsBak = new byte[4];
}

public class HB_SDVR_IPC_IPV6
{
    public ushort[] uIp = new ushort[8];
    public byte[] uGateway = new byte[16];
    public byte[] uDns = new byte[16];
    public byte[] uDnsBak = new byte[16];
    public byte uSubnetPrefix;
}

public class HB_SDVR_IPC_IP_PARAM
{
    public int nLens;
    public byte uNetInterface;
    public byte uNetModel;
    public byte[] uMacAddr = new byte[6];
    public HB_SDVR_IPC_IPV4 Ipv4 = new HB_SDVR_IPC_IPV4();
    public HB_SDVR_IPC_IPV6 Ipv6 = new HB_SDVR_IPC_IPV6();
    public int nListenPort;
    public int nHttpPort;
    public int nMulticast;
}

public class HB_SDVR_IPC_TIME_RECORD
{
    public int nLens;
    public int nChannelid;
    public HB_SDVR_IPC_TIME_SCHEDULE Schedule = new HB_SDVR_IPC_TIME_SCHEDULE();
    public int nReserved;
}


public class HB_SDVR_IPC_RECORD_MODE
{
    public int nLens;
    public int nChannelid;
    public int nCoverDelete;
    public string cManualRcd = new string(new char[8]);
    public string cTimeRcd = new string(new char[8]);
    public string cLinkageRcd = new string(new char[8]);
}
public class HB_SDVR_IPC_DISK_PARAM
{
    public string cDevName = new string(new char[16]);
    public string cStatus = new string(new char[32]);
    public uint uDiskcapacity;
    public uint uDiskFree;
}
public class HB_SDVR_IPC_DISK_CFG
{
    public int nLens;
    public int nDisknum;
    public HB_SDVR_IPC_DISK_PARAM[] Diskinfo = new HB_SDVR_IPC_DISK_PARAM[12];
}





public class HB_SDVR_IPC_E_MAIL
{
    public int nLens;
    public int nEnable;
    public int nMd5Auth;
    public int nUseIpv6;
    public int nUseSsl;
    public int nUseStarttls;
    public short sAccessoryEnable;
    public ushort uPort;
    public string cSmtpServer = new string(new char[128]);
    public string cUserName = new string(new char[32]);
    public string cPwd = new string(new char[32]);
    public string cSendPerson = new string(new char[128]);
    public string cRecvPerson = new string(new char[256]);
    public int nSendPeriod;
    public int nSnapEnable;
    public string cMailTopic = new string(new char[32]);
}


public class HB_SDVR_IPC_PPPOE
{
    public int nLens;
    public int nPppoeEnable;
    public int nAutoCon;
    public int nState;
    public string cUserName = new string(new char[32]);
    public string cPwd = new string(new char[32]);
    public int nPpppoeSave;
    public byte[] uIpv4 = new byte[4];
    public byte[] uGatewayv4 = new byte[4];
    public ushort[] uIpv6 = new ushort[8];
    public byte[] uGatewayv6 = new byte[16];
}




public class HB_SDVR_IPC_LINKAGE_RECORD
{
    public int nRecord;
    public int nDelayTime;
    public int nRecordTime;
    public int[] nReserved = new int[2];
}

public class HB_SDVR_IPC_LINKAGE_PTZ
{
    public int nPtz;
    public int nPtzItem;
    public int nPtzAddr;
    public int nReserved;
}
public class HB_SDVR_IPC_LINKAGE_ALARM
{
    public int nAlarmOut;
    public int nAlarm;
    public int nDelayTime;
    public int nContinueTime;
}

public class HB_SDVR_IPC_LINKAGE_SNAP
{
    public int nSnap;
    public short sDelayTime;
    public short sPicNum;
    public int nReserved;
}

public class HB_SDVR_IPC_LINKAGE_MAIL
{
    public int nEmail;
    public int nDelayTime;
    public int nReserved;
}

public class HB_SDVR_IPC_LINKAGE
{
    public HB_SDVR_IPC_LINKAGE_MAIL Mail = new HB_SDVR_IPC_LINKAGE_MAIL();
    public HB_SDVR_IPC_LINKAGE_SNAP Snap = new HB_SDVR_IPC_LINKAGE_SNAP();
    public HB_SDVR_IPC_LINKAGE_ALARM Alarm = new HB_SDVR_IPC_LINKAGE_ALARM();
    public HB_SDVR_IPC_LINKAGE_PTZ Ptz = new HB_SDVR_IPC_LINKAGE_PTZ();
    public HB_SDVR_IPC_LINKAGE_RECORD Record = new HB_SDVR_IPC_LINKAGE_RECORD();
}
public class HB_SDVR_IPC_MOTION
{
    public int nLens;
    public int nChannelid;
    public int nMotionEnable;
    public int[] nMotionBlock = new int[18];
    public int nSensitivity;
    public int nReserved;
    public HB_SDVR_IPC_TIME_SCHEDULE MonitorTime = new HB_SDVR_IPC_TIME_SCHEDULE();
    public HB_SDVR_IPC_LINKAGE LinkageOut = new HB_SDVR_IPC_LINKAGE();
}

public class HB_SDVR_IPC_ALARMIN_PARAM
{
    public HB_SDVR_IPC_TIME_SCHEDULE MonitorTime = new HB_SDVR_IPC_TIME_SCHEDULE();
    public int nAlarmInType;
    public HB_SDVR_IPC_LINKAGE LinkageOut = new HB_SDVR_IPC_LINKAGE();
}
public class HB_SDVR_IPC_ALARMIN
{
    public int nLens;
    public HB_SDVR_IPC_ALARMIN_PARAM[] AlarmIn = new HB_SDVR_IPC_ALARMIN_PARAM[4];
}
public class HB_SDVR_IPC_ALARMOUT_PARAM
{
    public int nAlarmTime;
    public HB_SDVR_IPC_TIME_SCHEDULE Time = new HB_SDVR_IPC_TIME_SCHEDULE();
}
public class HB_SDVR_IPC_ALARMOUT
{
    public int nLens;
    public HB_SDVR_IPC_ALARMOUT_PARAM[] AlarmOut = new HB_SDVR_IPC_ALARMOUT_PARAM[4];
}
public class HB_SDVR_IPC_NET_BUG
{
    public int nLens;
    public int nEnable;
    public short sRecordEnable;
    public short sAlarmEnable;
    public short sRecordTime;
    public short sAlarmoutTime;
}
public class HB_SDVR_IPC_SAVE_BUG_PROCESS
{
    public short sEnable;
    public short sEmail;
    public short sAlarmoutEnable;
    public short sAlarmoutDelay;
}
public class HB_SDVR_IPC_STORE_BUG
{
    public int nLens;
    public HB_SDVR_IPC_SAVE_BUG_PROCESS NoSd = new HB_SDVR_IPC_SAVE_BUG_PROCESS();
    public HB_SDVR_IPC_SAVE_BUG_PROCESS NoFreeSd = new HB_SDVR_IPC_SAVE_BUG_PROCESS();
    public HB_SDVR_IPC_SAVE_BUG_PROCESS ErrorSd = new HB_SDVR_IPC_SAVE_BUG_PROCESS();
}


////C++ TO C# CONVERTER TODO TASK: Unions are not supported in C#, but the following union can be simulated with the StructLayout and FieldOffset attributes.
////ORIGINAL LINE: typedef union
//[StructLayout(LayoutKind.Explicit)]
//public struct HB_SDVR_IPC_ALARM_CFG_U
//{
//    [FieldOffset(0)]
//    public HB_SDVR_IPC_MOTION Motion = new HB_SDVR_IPC_MOTION();
//    [FieldOffset(0)]
//    public HB_SDVR_IPC_ALARMIN Alarmin = new HB_SDVR_IPC_ALARMIN();
//    [FieldOffset(0)]
//    public HB_SDVR_IPC_ALARMOUT Alarmout = new HB_SDVR_IPC_ALARMOUT();
//    [FieldOffset(0)]
//    public HB_SDVR_IPC_NET_BUG NetBug = new HB_SDVR_IPC_NET_BUG();
//    [FieldOffset(0)]
//    public HB_SDVR_IPC_STORE_BUG StoreBug = new HB_SDVR_IPC_STORE_BUG();
//}

public class HB_SDVR_IPC_DEVICE_INFO
{
    public int nLens;
    public string cDevName = new string(new char[32]);
    public string cdDevType = new string(new char[32]);
    public string cSoftVersion = new string(new char[32]);
    public string cImprint = new string(new char[32]);
    public byte[] uSn = new byte[4];
    public byte[] uMacAddr = new byte[6];
    public byte[] uReserved = new byte[2];
}
public class HB_SDVR_IPC_AUTO_MAINTAINING
{
    public int nLens;
    public short sAutoReboot;
    public short sStyle;
    public HB_SDVR_IPC_SYS_TIME RebootTime = new HB_SDVR_IPC_SYS_TIME();
}
////C++ TO C# CONVERTER TODO TASK: Unions are not supported in C#, but the following union can be simulated with the StructLayout and FieldOffset attributes.
////ORIGINAL LINE: typedef union
//[StructLayout(LayoutKind.Explicit)]
//public struct HB_SDVR_IPC_MANAGE_CFG_U
//{
//    [FieldOffset(0)]
//    public HB_SDVR_IPC_DEVICE_INFO DevInfo = new HB_SDVR_IPC_DEVICE_INFO();
//    //HB_SDVR_IPC_NTP_PARAM          NtpParam;
//    [FieldOffset(0)]
//    public HB_SDVR_IPC_AUTO_MAINTAINING AutoMaintaining = new HB_SDVR_IPC_AUTO_MAINTAINING();
//}

public class HB_SDVR_IPC_PICTURE_SNAP
{
    public int nLens;
    public int nChannelid;
    public int nSnapEnable;
    public int nSnapType;
    public int nSnapResIndex;
    public int[,] nSnapRes = new int[32, 2];
    public int nPictureFormat;
    public int nPictureQuality;
    public int nSnapSpeed;
    public HB_SDVR_IPC_TIME_SCHEDULE Schedule = new HB_SDVR_IPC_TIME_SCHEDULE();
}

public class HB_SDVR_IPC_ADVANCE_PARAM
{
    public int nLens;
    public int nChannelid;
    public sbyte cAutoDropframe;
    public sbyte cAntiFlicker;
    public sbyte cWdr;
    public sbyte cColorStyle;
    public sbyte cDenoise3d;
    public byte byDenoise3dVal;
    public sbyte cTimeDomain;
    public byte bySpaceDomain;
    public sbyte cDenoise2d;
    public sbyte cDeFog;
    public sbyte cDeformityAdjust;
    public sbyte cPseudoColor;
    public sbyte cFocusSpeed;
    public sbyte cDigitalFoucus;
    public sbyte cBlc;
    public sbyte cAutoFoucus;
    public sbyte cCurframeDenoise;
    public sbyte cVoutType;
}


public class HB_SDVR_IPC_AUDIO_IN_PARAM
{
    public int nLens;
    public int nChannelid;
    public sbyte cAudioInCh;
    public sbyte cAudioEnable;
    public sbyte cVolume;
    public sbyte cCompressType;
    public int nBitrateIndex;
    public uint[] dwBitrate = new uint[16];
    public int nSamplerateIndex;
    public uint[] dwSamplerate = new uint[16];
}

public class HB_SDVR_IPC_AUDIO_OUT_PARAM
{
    public int nLens;
    public int nChannelid;
    public sbyte cAudioOutCh;
    public sbyte cAudioEnable;
    public sbyte cVolume;
    public sbyte cCompressType;
    public int nBitrateIndex;
    public uint[] dwBitrate = new uint[16];
    public int nSamplerateIndex;
    public uint[] dwSamplerate = new uint[16];
}

public class HB_SDVR_IPC_FTP
{
    public int nLens;
    public short sEnable;
    public ushort uPort;
    public string cServerAddress = new string(new char[128]);
    public string cUsername = new string(new char[32]);
    public string cPwd = new string(new char[32]);
    public string cSavePath = new string(new char[128]);
}
public class HB_SDVR_IPC_GB28181_DEVICE_INFO
{
    public string cDeviceId = new string(new char[32]);
    public int nDevicePort;
    public string cDeviceDomainName = new string(new char[64]);
    public string cDevicePwd = new string(new char[32]);
    public int nDeviceExpires;
    public int nDeviceAlarminNum;
    public string[] cDeviceAlarminId = new string[8];
}
public class HB_SDVR_IPC_GB28181_SMS_SERVER_INFO
{
    public string cSmssvrIp = new string(new char[32]);
    public int nSmssvrPort;
}
public class HB_SDVR_IPC_GB28181_SIP_SERVER_INFO
{
    public string cSipsvrIp = new string(new char[32]);
    public string cSipsvrId = new string(new char[32]);
    public int nSipsvrPort;
    public string cSipsvrDomainName = new string(new char[64]);
}

public class HB_SDVR_IPC_PLATFORM_INFO
{
    public string cUsername = new string(new char[32]);
    public string cPassword = new string(new char[16]);
    public int nProtocolPort;
}
public class HB_SDVR_IPC_GB28181_INFO
{
    public HB_SDVR_IPC_GB28181_DEVICE_INFO DevInfo = new HB_SDVR_IPC_GB28181_DEVICE_INFO();
    public HB_SDVR_IPC_GB28181_SMS_SERVER_INFO SmsserverInfo = new HB_SDVR_IPC_GB28181_SMS_SERVER_INFO();
    public HB_SDVR_IPC_GB28181_SIP_SERVER_INFO SipserverInfo = new HB_SDVR_IPC_GB28181_SIP_SERVER_INFO();
}

//public class HB_SDVR_IPC_PLATFORM
//{
//    public int nLens;
//    public int nProtocolType;
////C++ TO C# CONVERTER TODO TASK: Unions are not supported in C#, but the following union can be simulated with the StructLayout and FieldOffset attributes.
////ORIGINAL LINE: union
////C++ TO C# CONVERTER NOTE: Structs must be named in C#, so the following struct has been named AnonymousStruct8:
//    [StructLayout(LayoutKind.Explicit)]
//    public struct AnonymousStruct8
//    {
//        [FieldOffset(0)]
//        public HB_SDVR_IPC_PLATFORM_INFO Onvif = new HB_SDVR_IPC_PLATFORM_INFO();
//        [FieldOffset(0)]
//        public HB_SDVR_IPC_PLATFORM_INFO Psia = new HB_SDVR_IPC_PLATFORM_INFO();
//        [FieldOffset(0)]
//        public HB_SDVR_IPC_GB28181_INFO Gb28181 = new HB_SDVR_IPC_GB28181_INFO();
//    }
//    public AnonymousStruct8 PlatformCfg = new AnonymousStruct8();
//}

//C++ TO C# CONVERTER TODO TASK: Unions are not supported in C#, but the following union can be simulated with the StructLayout and FieldOffset attributes.
//ORIGINAL LINE: typedef union
//[StructLayout(LayoutKind.Explicit)]
//public struct HB_SDVR_IPC_NET_CFG_U
//{
//    [FieldOffset(0)]
//    public HB_SDVR_IPC_IP_PARAM IpParam = new HB_SDVR_IPC_IP_PARAM();
//    //    IPC_WLAN               wlan;
//    //    IPC_DDNS               ddns;
//    [FieldOffset(0)]
//    public HB_SDVR_IPC_PPPOE Pppoe = new HB_SDVR_IPC_PPPOE();
//    [FieldOffset(0)]
//    public HB_SDVR_IPC_E_MAIL Email = new HB_SDVR_IPC_E_MAIL();
//    //    IPC_UPNP               upnp;
//    [FieldOffset(0)]
//    public HB_SDVR_IPC_FTP Ftp = new HB_SDVR_IPC_FTP();
//    //    IPC_NAS                nas;
//    //    IPC_AUTO_REGIST        auto_regist;
//    [FieldOffset(0)]
//    public HB_SDVR_IPC_PLATFORM Platform = new HB_SDVR_IPC_PLATFORM();
//    //    IPC_IP_FILTER          ip_filter;
//}
//C++ TO C# CONVERTER TODO TASK: Unions are not supported in C#, but the following union can be simulated with the StructLayout and FieldOffset attributes.
//ORIGINAL LINE: typedef union
//[StructLayout(LayoutKind.Explicit)]
//public struct HB_SDVR_IPC_STORE_CFG_U
//{
//    [FieldOffset(0)]
//    public HB_SDVR_IPC_TIME_RECORD TimeRecord = new HB_SDVR_IPC_TIME_RECORD();
//    [FieldOffset(0)]
//    public HB_SDVR_IPC_RECORD_MODE RecordMode = new HB_SDVR_IPC_RECORD_MODE();
//    [FieldOffset(0)]
//    public HB_SDVR_IPC_DISK_CFG DiskCfg = new HB_SDVR_IPC_DISK_CFG();
//}

//C++ TO C# CONVERTER TODO TASK: Unions are not supported in C#, but the following union can be simulated with the StructLayout and FieldOffset attributes.
//ORIGINAL LINE: typedef union
//[StructLayout(LayoutKind.Explicit)]
//public struct HB_SDVR_IPC_VIDEO_CFG_U
//{
//    [FieldOffset(0)]
//    public HB_SDVR_IPC_IMAGE_PARAM ImageParam = new HB_SDVR_IPC_IMAGE_PARAM();
//    [FieldOffset(0)]
//    public HB_SDVR_IPC_VIDEO_PARAM VideoParam = new HB_SDVR_IPC_VIDEO_PARAM();
//    [FieldOffset(0)]
//    public HB_SDVR_IPC_VIDEO_ENCODE VideoEncode = new HB_SDVR_IPC_VIDEO_ENCODE();
//    [FieldOffset(0)]
//    public HB_SDVR_IPC_PICTURE_SNAP PictureSnap = new HB_SDVR_IPC_PICTURE_SNAP();
//    [FieldOffset(0)]
//    public HB_SDVR_IPC_OSD_OVERLAY OsdOverlay = new HB_SDVR_IPC_OSD_OVERLAY();
//    [FieldOffset(0)]
//    public HB_SDVR_IPC_MASK Mask = new HB_SDVR_IPC_MASK();
//    [FieldOffset(0)]
//    public HB_SDVR_IPC_ADVANCE_PARAM AdvanceParam = new HB_SDVR_IPC_ADVANCE_PARAM();
//    [FieldOffset(0)]
//    public HB_SDVR_IPC_AUDIO_IN_PARAM AudioInParam = new HB_SDVR_IPC_AUDIO_IN_PARAM();
//    [FieldOffset(0)]
//    public HB_SDVR_IPC_AUDIO_OUT_PARAM AudioOutParam = new HB_SDVR_IPC_AUDIO_OUT_PARAM();
//}



//public class HB_SDVR_IPC_CMD
//{
//    public int nPriCmdType; //对应HB_SDVR_IPC_PRI_CMD_TYPE_E
//    public int nSecCmdType; //对应HB_SDVR_IPC_VIDEO_CMD_TYPE_E的子命令
//    public int nChannelid;
//}

//public class HB_SDVR_IPC_CONFIG
//{
//    public int nPriCmdType; //对应HB_SDVR_IPC_PRI_CMD_TYPE_E
//    public int nSecCmdType; //对应HB_SDVR_IPC_VIDEO_CMD_TYPE_E的子命令
//    public int nSecCmdParaSize;
////C++ TO C# CONVERTER TODO TASK: Unions are not supported in C#, but the following union can be simulated with the StructLayout and FieldOffset attributes.
////ORIGINAL LINE: union
////C++ TO C# CONVERTER NOTE: Structs must be named in C#, so the following struct has been named AnonymousStruct9:
//    [StructLayout(LayoutKind.Explicit)]
//    public struct AnonymousStruct9
//    {
//        [FieldOffset(0)]
//        public HB_SDVR_IPC_VIDEO_CFG_U VideoCfg = new HB_SDVR_IPC_VIDEO_CFG_U();
//        [FieldOffset(0)]
//        public HB_SDVR_IPC_NET_CFG_U NetCfg = new HB_SDVR_IPC_NET_CFG_U();
//        [FieldOffset(0)]
//        public HB_SDVR_IPC_STORE_CFG_U StoreCfg = new HB_SDVR_IPC_STORE_CFG_U();
//        [FieldOffset(0)]
//        public HB_SDVR_IPC_ALARM_CFG_U AlarmCfg = new HB_SDVR_IPC_ALARM_CFG_U();
//        [FieldOffset(0)]
//        public HB_SDVR_IPC_MANAGE_CFG_U ManageCfg = new HB_SDVR_IPC_MANAGE_CFG_U();
//    }
//    public AnonymousStruct9 IpcCfg = new AnonymousStruct9();
//    public int[] nReserved = new int[2];
//}

public class TAG_HB_SDVR_QUERY_MONTH
{
    public byte byChannel; // 通道号[0,n-1], n:通道数
    public byte byType; // 查询录像类型 0x01:手动 0x02:定时 0x04:移动 0x08:报警 0xFF所有
    public byte byYear; // 查询年份[0, 63], 实际年份-2000
    public byte byMonth; // 查询月份[1, 12]
    public byte[] byReserver = new byte[32];
}

public class TAG_HB_SDVR_RECFILE_MONTHINFO
{
    public byte[] byDate = new byte[31]; // 返回有录像数据的日期， 数据[n]代表某月的第n+1天, 0:无录像 1:有
    public byte[] byReserver = new byte[9]; // 保留
}


public class STRUCT_SDVR_RESOLUTION
{
    public uint dwResolution; //分辨率值，如：0x07800438，高两字节(0x0780=1920)、低两字节(0x0438=1080)
    public uint[] dwVideoBitrate_support = new uint[32]; //该分辨率下支持的码率范围，每一个数组代表一种码率，数组的值
                                                         //如果为0，表示该数组未用到，不为0，表示支持的码率值，单位为Kbit/s
    public ushort wVideoFrameRate_min; //该分辨率下的最小帧率
    public ushort wVideoFrameRate_max; //该分辨率下的最大帧率
    public byte[] byPicQuality_support = new byte[10]; //该分辨率下支持的图像质量等级,每个数组代表一种图像质量等级，
                                                       //0数组是最高， 1数组是较高， 2数组是高， 3数组是中，4数组是低， 
                                                       //5数组是最低，该数组为1，表示支持该种图像质量
    public byte[] reserve = new byte[2]; //保留
}


//发送的时候只需要填写byChannel,dwSize,byCompressionType这三个字段)
public class STRUCT_SDVR_COMPRESSINFO_SUPPORT
{
    public uint dwSize; //结构体大小
    public byte byChannel; //通道号
    public byte byCompressionType; //码流，0-主码流，1-子码流1，子码流2…
    public byte byCompression_support; //支持的码流，每位代表一种码流，该位为1表示支持该码流， 从低位开始， //0位代表主码流，第1位代表子码流1，第2位代表子码流2...
    public byte byBitrateTypeIndex; //当前码流类型索引值，表示基于dwBitrateType中的位置,
                                    //例如dwBitrateTypeIndex = 0，则当前码流类型为dwBitrateType的第0位
                                    //所指定的码流类型，即变码流。
    public byte byBitrateType_support; //支持的码流类型，每一位代表一种码流类型，该位为1表示支持该码流类型，
                                       //从低位开始，第0位是变码流，第1位是定码流
    public byte byRecordType_index; //当前录像类型索引值
    public byte byRecordType_support; //支持的录像类型，每位代表一种录像类型，该位为1表示支持该类型，
                                      //从低位开始，第0位手动录像，第1位定时录像，第2位移动录像，
                                      //第3位报警录像，……第15位所有录像
    public byte byAudioflag; //当前是否有音频，0-无音频，1-有音频
    public byte byAudio_support; //是否支持音频，0-不支持，1-支持，当不支持音频时，byAudioflag只能为0
    public byte byPicQuality; //当前图像质量， 0--最高， 1-较高， 2-高， 3-中，4-低， 5-最低
    public ushort wVideoFrameRate; //当前帧率值
    public uint dwVideoBitrate; //当前码率值，单位为Kbit/s
    public byte[] reserve = new byte[3]; //保留
    public byte byResoluIndex; //当前分辨率索引值，表示基于byResolution_support中的位置
    public STRUCT_SDVR_RESOLUTION[] Resolution_support = new STRUCT_SDVR_RESOLUTION[16]; //支持的分辨率，最大16种分辨率，每个结构
                                                                                         //体代表一种分辨率及该分辨率下支持的码率，帧率，图像质量范围，该结构
                                                                                         //体的dwResolution为0，表示该结构体未用到
}



public class STRUCT_USERINFO
{
    public byte[] sUserName = new byte[32]; //用户名
    public byte[] sPassword = new byte[32]; //密码
    public byte[] byLocalRight = new byte[32]; //本地权限 1.数组0未使用；2.取值：0-无权限，1-有权限
                                               //数组1-常规设置、数组2-录像设置、数组3-输出设置、数组4-报警设置、
                                               //数组5-串口设置、数组6-网络设置、数组7-录像回放、数组8-系统管理、
                                               //数组9-系统信息、数组10-报警清除、数组11-云台控制、数组12-关机重启、
                                               //数组-13-USB升级、数组14-备份
    public byte[] byLocalChannel = new byte[128]; //本地用户对通道的操作权限，最大128个通道，0-无权限，1-有权限
    public byte[] byRemoteRight = new byte[32]; //远程登录用户所具备的权限 1.数组0未使用；2.取值：0-无权限，1-有权限
                                                //数组1-显示设置、数组2-录像参数、数组3-定时录像、数组4-移动录像、
                                                //数组5-报警录像、数组6-网络参数、数组7-云台设置、数组8-存储管理
                                                //数组9-系统管理、数组10-信息查询、数组11-手动录像、数组12-回放、
                                                //数组-13-备份、数组14-视频参数、数组-15-报警清除、数组16-远程预览
    public byte[] byRemoteChannel = new byte[128]; //远程登录用户对通道的操作权限，最大128个通道，0-无权限，1-有权限
    public uint dwUserIP; //用户IP地址(为0时表示允许任何地址)
    public byte[] byMACAddr = new byte[8]; //物理地址
}

public class STRUCT_USERINFO_GUI
{
    public byte[] sUserName = new byte[32]; //用户名，以’\0’结束字符串
    public byte[] sPassword = new byte[32]; //密码，以’\0’结束字符串
    public byte[] byLocalRight = new byte[32]; //本地权限 1.数组0未使用；2.取值：0-无权限，1-有权限
                                               //数组1-手动录像、数组2-手动报警、数组3-录像回放、数组4-备份管理、
                                               //数组5-磁盘管理、数组6-系统关机、数组7-系统重启、数组8-云台控制、
                                               //数组9-报警清除、数组10-常规设置、数组11-输出设置、数组12-录像设置、
                                               //数组13-定时录像、数组14-报警设置、数组15-串口设置、数组16-云台设置、
                                               //数组17-网络设置、数组18-系统信息、数组19-录像状态、数组20-报警状态、
                                               //数组21-在线状态、数组22-日志查询、数组23-快速设置、数组24-用户管理、
                                               //数组25-恢复出厂设置、数组26-升级权限、数组27-定时重启、
                                               //数组28-卡号录像
    public byte[] byLocalChannel = new byte[128]; //本地用户对通道的操作权限，最大128个通道，0-无权限，1-有权限
    public byte[] byRemoteRight = new byte[32]; //远程登陆用户所具备的权限 1.数组0未使用；2.取值：0-无权限，1-有权限
                                                //数组 1-远程预览、数组 2-参数设置、数组 3-远程回放、数组 4-远程备份、
                                                //数组 5-查看日志、数组 6-语音对讲、数组 7-远程升级、数组 8-远程重启
    public byte[] byRemoteChannel = new byte[128]; //用户远程登陆时对通道所具备的权限，最大128个通道，0-无权限，1-有权限
    public uint dwUserIP; //用户登录时pc机的ip地址，为0表示任何PC机都可以使用该用户登陆到
                          //DVR上，不为0表示只有ip地址为设定值的pc机才可以使用该用户登录到
                          //DVR上
    public byte[] byMACAddr = new byte[8]; //用户登录时PC机的MAC地址，为0表示任何PC机都可以使用该用户登陆
                                           //到DVR上，不为0表示只有MAC地址为设定值的PC机才可以使用该用户
                                           //登陆到DVR上

}

public class STRUCT_USERINFO_9000
{
    public byte[] user = new byte[32]; //用户名，以’\0’结束字符串
    public byte[] pwd = new byte[32]; //密码，以’\0’结束字符串
    public byte[] grp_name = new byte[32]; //分组名
    public long[] local_authority = new long[64]; //本地用户使用权限，每位代表一个通道,bit0~bit63表示0~63通道，
                                                  //每个数组代表一种权限，数组0-实时预览、数组1-手动录像、
                                                  //数组2-录像查询回放、数组3-备份管理、数组4-录像参数、数组5-云台设置、
                                                  //数组6-截图设置、数组7-通道设置、数组8-定时录像、数组9-移动检测、
                                                  //数组10-报警管理、数组11-常规设置、数组12-串口设置、数组13-磁盘设置、
                                                  //数组14-网络设置、数组15-信息查看、数组16-升级管理、数组17-快速设置、
                                                  //数组18-出厂设置、数组19-系统关机、数组20-卡号录像
    public long[] remote_authority = new long[64]; //远程权限，每位代表一个通道，bit0~bit63表示0~63通道，
                                                   //每个数组代表一种权限，数组0-远程预览、数组1-参数设置、数组2-远程回									//放、数组3-远程备份、数组4-查看日志、数组5-语音对讲、数组6-远程升级
    public uint dwbind_ipaddr; //用户登录时pc机的ip地址，为0表示任何PC机都可以使用该用户登陆到
                               //DVR上，不为0表示只有ip地址为设定值的pc机才可以使用该用户登录到
                               //DVR上
    public byte[] bybind_macaddr = new byte[8]; //用户登录时PC机的MAC地址，为0表示任何PC机都可以使用该用户登陆
                                                //到DVR上，不为0表示只有MAC地址为设定值的PC机才可以使用该用户
                                                //登陆到DVR上
}

//public class STRUCT_SDVR_USER_INFO_EX1
//{
//    public uint dwSize; //结构体大小
//    public ushort wUserInfoMode; //用户权限模式，1-老的权限模式，2-新GUI权限模式，3-9000项目权限模式
//    public byte[] reserve = new byte[2]; //保留
////C++ TO C# CONVERTER TODO TASK: Unions are not supported in C#, but the following union can be simulated with the StructLayout and FieldOffset attributes.
////ORIGINAL LINE: union
////C++ TO C# CONVERTER NOTE: Structs must be named in C#, so the following struct has been named AnonymousStruct10:
//    [StructLayout(LayoutKind.Explicit)]
//    public struct AnonymousStruct10
//    {
//        [FieldOffset(0)]
//        public STRUCT_USERINFO[] userInfo = new STRUCT_USERINFO[16]; //当dwUserInfoMode=1时，使用该结构体
//        [FieldOffset(0)]
//        public STRUCT_USERINFO_GUI[] userInfoGui = new STRUCT_USERINFO_GUI[16]; //当dwUserInfoMode=2时，使用该结构体
//        [FieldOffset(0)]
//        public STRUCT_USERINFO_9000[] userInfo9000 = new STRUCT_USERINFO_9000[16]; //当dwUserInfoMode=3时，使用该结构体
//    }
//    public AnonymousStruct10 info = new AnonymousStruct10();
//}

// 夏令时按周设置时间
public class STRUCT_SDVR_DST_WEEK_TIME_S
{
    public byte month; //夏令时按月设置，月[1，12]
    public byte weeks; //夏令时按周设置，周[1，5]
    public byte week; //夏令时按周设置，星期[0，6]，
    public byte hour; //夏令时按周设置, 时[0，23]
    public byte min; //夏令时按周设置，分[0，59]
    public byte sec; //夏令时按周设置，秒[0，59]
}
//说明：按周设置的时间，表示第几月的第几个星期几的几时几分几秒，如month=5，weeks=2，week=1，hour=10，min=0，sec=0，表示5月份的第2个星期1的10：00：00


//系统时间定义
public class HSYSTIME
{
    //C++ TO C# CONVERTER TODO TASK: C# does not allow bit fields:
    public uint sec;
    //C++ TO C# CONVERTER TODO TASK: C# does not allow bit fields:
    public uint min;
    //C++ TO C# CONVERTER TODO TASK: C# does not allow bit fields:
    public uint hour;
    //C++ TO C# CONVERTER TODO TASK: C# does not allow bit fields:
    public uint day;
    //C++ TO C# CONVERTER TODO TASK: C# does not allow bit fields:
    public uint month;
    //C++ TO C# CONVERTER TODO TASK: C# does not allow bit fields:
    public uint year;
}
//夏令时时间设置
public class STRUCT_SDVR_DST_TIME_S
{
    public byte dst_en; //夏令时使能键，0-不使能，1-使能
    public byte dsttype_en; //按周设置为0, 按日期设置为1
    public HSYSTIME start_date = new HSYSTIME(); //按日期设置的开始时间
    public HSYSTIME end_date = new HSYSTIME(); //按日期设置的结束时间
    public STRUCT_SDVR_DST_WEEK_TIME_S start_time = new STRUCT_SDVR_DST_WEEK_TIME_S(); //按周设置的开始时间
    public STRUCT_SDVR_DST_WEEK_TIME_S end_time = new STRUCT_SDVR_DST_WEEK_TIME_S(); //按周设置的结束时间
    public byte[] reserve = new byte[4]; //保留
}

//#define OSDINFONUM_MAX
//#define OSDSTRSIZE_MAX

public class HB_SDVR_STRUCT_SDVR_OSDINFO
{
    public byte id; // 通道字符叠加信息号 [0, n－1] n: 叠加字符信息组数
    public byte byLinkMode; // 0-主码流 1-子码流
    public byte byChanOSDStrSize; // 叠加字符信息里字符串数据的长度，包含字符串结束符'\0',[1, 100]
    public byte byOSDAttrib; // 通道字符叠加信息 1-不透明 2-透明(只针对 PC 端显示)
    public byte byOSDType; // 格式及语言，最高位为 0表示解码后叠加，为 1表示前端叠加
                           // 设为 0x80 时表示将 osd 设为前端叠加
    public string reservedData = new string(new char[3]); // 保留
    public uint dwShowChanOSDInfo; // 是否显示通道字符叠加信息 0-显示 1-不显示
    public ushort wShowOSDInfoTopLeftX; // 通道字符叠加信息显示位置的 x 坐标
                                        //  [0,  实际宽－叠加字符数据长度]
    public ushort wShowOSDInfoTopLeftY; // 通道字符叠加信息显示位置的 y 坐标
                                        //  [0,  实际高－字体高度] 
    public string data = new string(new char[HBConst.OSDSTRSIZE_MAX]); // 叠加字符信息里的字符串数据，包含字符串结束符'\0',长度小于100。
}

public class HB_SDVR_STRUCT_SDVR_OSDCFG
{
    public byte byChannel; // 通道号 [0, n－1] n:通道数
    public byte byOSDInfoNum; // 包含的叠加字符信息组数[1,64]，每组结构为STRUCT_SDVR_OSDINFO
    public ushort byChanOSDInfoSize; // 叠加字符信息的数据包大小,保留
    public HB_SDVR_STRUCT_SDVR_OSDINFO[] OsdInfo = new HB_SDVR_STRUCT_SDVR_OSDINFO[HBConst.OSDINFONUM_MAX];
}


public class HB_SDVR_INITIATIVE_LOGIN
{
    public string sDVRID = new string(new char[HBConst.SERIALNO_LEN]);
    public string sSerialNumber = new string(new char[HBConst.SERIALNO_LEN]);
    public byte byAlarmInPortNum;
    public byte byAlarmOutPortNum;
    public byte byDiskNum;
    public byte byProtocol;
    public byte byChanNum;
    public byte byEncodeType;
    public byte[] reserve = new byte[26];
    public string sDvrName = new string(new char[HBConst.NAME_LEN]);
    public string[] sChanName = new string[HBConst.MAX_CHANNUM_EX];
}

//public class HB_SDVR_ALARM_REQ
//{
//    public byte byChannel;
//    public byte byAlarmStat;
//    public byte[] reserve1 = new byte[2];
//    public uint dwAlarmType;
//    public HB_SDVR_TIME dwAlarmTime = new HB_SDVR_TIME();
//    public byte[] reserve2 = new byte[4];
//}

public class ST_HB_SDVR_REALPLAYCON
{
    public uint dwSize;
    public uint dwChl;
    public IntPtr hWnd;
    public uint dwStreamType; // 0-主码流 1-子码流
    public uint dwLinkMode; // 0-TCP 1-UDP
    public uint dwMultiCast; // 是否多播
    public uint dwOSDScheme; // osd字符编码格式
    public uint dwMultiCastIP; // 多播IP地址
    public uint dwPort; // 多播端口
    public IntPtr pfnDataProc;
    public IntPtr pContext;
    public uint[] dwReserver = new uint[4];
}

public class ST_HB_SDVR_PLAYBACKCON
{
    public uint dwSize;
    public uint dwChannel; // 通道号
    public HB_SDVR_RECTYPE_E dwFileType = new HB_SDVR_RECTYPE_E(); // 文件类型
    public IntPtr hWnd;
    public HB_SDVR_TIME struStartTime = new HB_SDVR_TIME(); // 下载时间段开始时间
    public HB_SDVR_TIME struStopTime = new HB_SDVR_TIME(); // 结束时间
    public IntPtr pfnDataProc;
    public IntPtr pContext;
    public uint[] dwResever = new uint[4];
}


//登陆
//
//功  能：登陆主机
//参  数：
//sDVRIP： IP地址
//wDVRPort： 端口
//sUserName：用户名
//sPassword：密码
//lpDeviceInfo： 指向HB_SDVR_DEVICEINFO 结构的指针
//返回值：-1 表示失败，其他值表示返回用户的ID值，该ID值是由SDK分配，每个用户ID 值在客户端是唯一的
//山东中孚加解密
//#define MAX_IPC_CHANNUM
public class STRUCT_SDVR_DONGLEINFO
{
    public byte[] dongle_type = new byte[HBConst.MAX_IPC_CHANNUM]; //0: 未连接加密机模块, 1: 山东中孚SATA加密机
    public byte[] realtime_encrypt = new byte[HBConst.MAX_IPC_CHANNUM]; //0: 未启用加密, 1: 启用加密数据连接
    public byte[] record_encrypt = new byte[HBConst.MAX_IPC_CHANNUM]; //0: 未启用加密, 1: 启用加密数据连接
}

public class STRUCT_SDVR_DONGLE_CHANNEL_INFO
{
    public byte channel; // 通道号
    public byte stream_mode; // 0: 实时流, 1: 历史回放
    public byte stream_type; // 0: 主码流, 1: 子码流
}

public class STRUCT_SDVR_DONGLE_ENABLE
{
    public byte realtime_enc_enable;
    public byte reserve;

}
public struct HB_SDVR_VIDEOEFFECT
{
    public byte byChannel; //通道号
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public string reservedData;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public HB_SDVR_SCHEDULE_VIDEOPARAM[] Schedule_VideoParam; //一天包含2个时间段
    public HB_SDVR_VIDEOPARAM Default_VideoParam; //不在时间段内就使用默认
}
public struct HB_SDVR_VIDEOPARAM
{
    public uint dwBrightValue; //亮度 1-127
    public uint dwContrastValue; //对比度1-127
    public uint dwSaturationValue; //饱和度1-127
    public uint dwHueValue; //色度 1-127
}
public delegate void fVoiceDataCallBackDelegate(int lVoiceComHandle, ref string pRecvDataBuffer, uint dwBufSize, byte byAudioFlag, uint dwUser);



public sealed class HBConst
{

    public const int HB_SDVR_SYSHEAD = 64;
    public const int HB_SDVR_STREAMDATA = 2;
    public const int HB_SDVR_BACKUPEND = 3;
    public const int HB_SDVR_PLAYSTART = 1;
    public const int HB_SDVR_PLAYSTOP = 2;
    public const int HB_SDVR_PLAYPAUSE = 3;
    public const int HB_SDVR_PLAYRESTART = 4;
    public const int HB_SDVR_PLAYFAST = 5;
    public const int HB_SDVR_PLAYSLOW = 6;
    public const int HB_SDVR_PLAYBACK = 21;
    public const int HB_SDVR_PLAYNORMAL = 7;
    public const int HB_SDVR_PLAYFRAME = 8;
    public const int HB_SDVR_PLAYSTARTAUDIO = 9;
    public const int HB_SDVR_PLAYSTOPAUDIO = 10;
    public const int HB_SDVR_PLAYAUDIOVOLUME = 11;
    public const int HB_SDVR_PLAYGETPOS = 13;
    public const int HB_SDVR_PLAYBYSLIDER = 22;
    public const int HB_SDVR_SET_SERIALID = 0x67;
    public const int HB_SDVR_GET_SERIALID = 0x68;
    public const int HB_SDVR_GET_VLostStatus = 0x69;
    public const int HB_SDVR_GET_DEVICECFG = 100;
    public const int HB_SDVR_SET_DEVICECFG = 101;
    public const int HB_SDVR_GET_NETCFG = 102;
    public const int HB_SDVR_SET_NETCFG = 103;
    public const int HB_SDVR_GET_PICCFG = 104;
    public const int HB_SDVR_SET_PICCFG = 105;
    public const int HB_SDVR_GET_COMPRESSCFG = 106;
    public const int HB_SDVR_SET_COMPRESSCFG = 107;
    public const int HB_SDVR_GET_RECORDCFG = 108;
    public const int HB_SDVR_SET_RECORDCFG = 109;
    public const int HB_SDVR_GET_DECODERCFG = 110;
    public const int HB_SDVR_SET_DECODERCFG = 111;
    public const int HB_SDVR_GET_RS232CFG = 112;
    public const int HB_SDVR_SET_RS232CFG = 113;
    public const int HB_SDVR_GET_ALARMINCFG = 114;
    public const int HB_SDVR_SET_ALARMINCFG = 115;
    public const int HB_SDVR_GET_ALARMINCFG_NVS = 128;
    public const int HB_SDVR_SET_ALARMINCFG_NVS = 129;
    public const int HB_SDVR_GET_ALARMOUTCFG = 116;
    public const int HB_SDVR_SET_ALARMOUTCFG = 117;
    public const int HB_SDVR_GET_TIMECFG = 118;
    public const int HB_SDVR_SET_TIMECFG = 119;
    public const int HB_SDVR_GET_PREVIEWCFG = 120;
    public const int HB_SDVR_SET_PREVIEWCFG = 121;
    public const int HB_SDVR_GET_VIDEOOUTCFG = 122;
    public const int HB_SDVR_SET_VIDEOOUTCFG = 123;
    public const int HB_SDVR_GET_USERCFG = 124;
    public const int HB_SDVR_SET_USERCFG = 125;
    public const int HB_SDVR_GET_USERCFG_NVS = 206;
    public const int HB_SDVR_SET_USERCFG_NVS = 207;
    public const int HB_SDVR_GET_EXCEPTIONCFG = 126;
    public const int HB_SDVR_SET_EXCEPTIONCFG = 127;
    public const int HB_SDVR_GET_SHOWSTRING = 130;
    public const int HB_SDVR_SET_SHOWSTRING = 131;
    public const int HB_SDVR_GET_EVENTCOMPCFG = 132;
    public const int HB_SDVR_SET_EVENTCOMPCFG = 133;
    public const int HB_SDVR_GET_FTPCFG = 134;
    public const int HB_SDVR_SET_FTPCFG = 135;
    public const int HB_SDVR_GET_JPEGCFG = 136;
    public const int HB_SDVR_SET_JPEGCFG = 137;
    public const int HB_SDVR_GET_PPPOECFG = 138;
    public const int HB_SDVR_SET_PPPOECFG = 139;
    public const int HB_SDVR_GET_AUXOUTCFG = 140;
    public const int HB_SDVR_SET_AUXOUTCFG = 141;
    public const int HB_SDVR_GET_PICCFG_EX = 200;
    public const int HB_SDVR_SET_PICCFG_EX = 201;
    public const int HB_SDVR_GET_PICCFG_EX_NVS = 204;
    public const int HB_SDVR_SET_PICCFG_EX_NVS = 205;
    public const int HB_SDVR_GET_USERCFG_EX = 202;
    public const int HB_SDVR_SET_USERCFG_EX = 203;
    public const int HB_SDVR_GET_DNS = 301;
    public const int HB_SDVR_SET_DNS = 302;
    public const int HB_SDVR_GET_DNS_NVS = 305;
    public const int HB_SDVR_SET_DNS_NVS = 306;
    public const int HB_SDVR_GET_PPPoE = 303;
    public const int HB_SDVR_SET_PPPoE = 304;
    public const int HB_SDVR_SERVERCFG_GET = 400;
    public const int HB_SDVR_SERVERCFG_SET = 401;
    public const int EXCEPTION_EXCHANGE = 0x8000;
    public const int EXCEPTION_AUDIOEXCHANGE = 0x8001;
    public const int EXCEPTION_ALARM = 0x8002;
    public const int EXCEPTION_PREVIEW = 0x8003;
    public const int EXCEPTION_SERIAL = 0x8004;
    public const int EXCEPTION_RECONNECT = 0x8005;
    public const int NAME_LEN = 32;
    public const int SERIALNO_LEN = 48;
    public const int MACADDR_LEN = 6;
    public const int MAX_ETHERNET = 2;
    public const int PATHNAME_LEN = 128;
    public const int PASSWD_LEN = 16;
    public const int MAX_CHANNUM = 16;
    public const int MAX_CHANNUM_EX = 128;
    public const int MAX_ALARMOUT = 4;
    public const int MAX_ALARMOUT_EX = 128;
    public const int MAX_TIMESEGMENT = 2;
    public const int MAX_PRESET = 128;
    public const int MAX_DAYS = 8;
    public const int PHONENUMBER_LEN = 32;
    public const int MAX_DISKNUM = 16;
    public const int MAX_WINDOW = 16;
    public const int MAX_VGA = 1;
    public const int MAX_USERNUM = 16;
    public const int MAX_EXCEPTIONNUM = 16;
    public const int MAX_LINK = 32;
    public const int MAX_ALARMIN = 16;
    public const int MAX_ALARMIN_EX = 128;
    public const int MAX_VIDEOOUT = 2;
    public const int MAX_NAMELEN = 16;
    public const int MAX_RIGHT = 32;
    public const int CARDNUM_LEN = 20;
    public const int MAX_SHELTERNUM = 4;
    public const int MAX_DECPOOLNUM = 4;
    public const int MAX_DECNUM = 4;
    public const int MAX_TRANSPARENTNUM = 2;
    public const int MAX_STRINGNUM = 4;
    public const int MAX_AUXOUT = 4;
    public const int NET_IF_10M_HALF = 1;
    public const int NET_IF_10M_FULL = 2;
    public const int NET_IF_100M_HALF = 3;
    public const int NET_IF_100M_FULL = 4;
    public const int NET_IF_AUTO = 5;
    public const int DVR = 1;
    public const int ATMDVR = 2;
    public const int DVS = 3;
    public const int DEC = 4;
    public const int ENC_DEC = 5;
    public const int DVR_HC = 6;
    public const int DVR_HT = 7;
    public const int DVR_HF = 8;
    public const int DVR_HS = 9;
    public const int DVR_HTS = 10;
    public const int DVR_HB = 11;
    public const int DVR_HCS = 12;
    public const int DVS_A = 13;
    public const int MAX_LOG_NUM = 100;
    public const int MFS_REC_TYPE_MANUAL = 1;
    public const int MFS_REC_TYPE_SCHEDULE = 2;
    public const int MFS_REC_TYPE_MOTION = 4;
    public const int MFS_REC_TYPE_ALARM = 8;
    public const int MFS_REC_TYPE_ALL = 0xff;
    public const int MAX_REC_NUM = 100;
    public const int NOACTION = 0x0;
    public const int WARNONMONITOR = 0x1;
    public const int WARNONAUDIOOUT = 0x2;
    public const int UPTOCENTER = 0x4;
    public const int TRIGGERALARMOUT = 0x8;
    public const int YOULI = 0;
    public const int LILIN_1016 = 1;
    public const int LILIN_820 = 2;
    public const int PELCO_P = 3;
    public const int DM_QUICKBALL = 4;
    public const int HD600 = 5;
    public const int JC4116 = 6;
    public const int PELCO_DWX = 7;
    public const int PELCO_D = 8;
    public const int VCOM_VC_2000 = 9;
    public const int NETSTREAMER = 10;
    public const int SAE = 11;
    public const int SAMSUNG = 12;
    public const int KALATEL_KTD_312 = 13;
    public const int CELOTEX = 14;
    public const int TLPELCO_P = 15;
    public const int TL_HHX2000 = 16;
    public const int BBV = 17;
    public const int RM110 = 18;
    public const int KC3360S = 19;
    public const int ACES = 20;
    public const int ALSON = 21;
    public const int INV3609HD = 22;
    public const int HOWELL = 23;
    public const int TC_PELCO_P = 24;
    public const int TC_PELCO_D = 25;
    public const int AUTO_M = 26;
    public const int AUTO_H = 27;
    public const int ANTEN = 28;
    public const int CHANGLIN = 29;
    public const int DELTADOME = 30;
    public const int XYM_12 = 31;
    public const int ADR8060 = 32;
    public const int EVI = 33;
    public const int Demo_Speed = 34;
    public const int DM_PELCO_D = 35;
    public const int ST_832 = 36;
    public const int LC_D2104 = 37;
    public const int HUNTER = 38;
    public const int A01 = 39;
    public const int TECHWIN = 40;
    public const int WEIHAN = 41;
    public const int LG = 42;
    public const int D_MAX = 43;
    public const int PANASONIC = 44;
    public const int KTD_348 = 45;
    public const int INFINOVA = 46;
    public const int PIH717 = 47;
    public const int IDOME_IVIEW_LCU = 48;
    public const int DENNARD_DOME = 49;
    public const int PHLIPS = 50;
    public const int SAMPLE = 51;
    public const int PLD = 52;
    public const int PARCO = 53;
    public const int HY = 54;
    public const int NAIJIE = 55;
    public const int CAT_KING = 56;
    public const int YH_06 = 57;
    public const int SP9096X = 58;
    public const int M_PANEL = 59;
    public const int M_MV2050 = 60;
    public const int SAE_QUICKBALL = 61;
    public const int RED_APPLE = 62;
    public const int NKO8G = 63;
    public const int DH_CC440 = 64;
    public const int MAJOR_ALARM = 0x1;
    public const int MINOR_ALARM_IN = 0x1;
    public const int MINOR_ALARM_OUT = 0x2;
    public const int MINOR_MOTDET_START = 0x3;
    public const int MINOR_MOTDET_STOP = 0x4;
    public const int MINOR_HIDE_ALARM_START = 0x5;
    public const int MINOR_HIDE_ALARM_STOP = 0x6;
    public const int MAJOR_EXCEPTION = 0x2;
    public const int MINOR_VI_LOST = 0x21;
    public const int MINOR_ILLEGAL_ACCESS = 0x22;
    public const int MINOR_HD_FULL = 0x23;
    public const int MINOR_HD_ERROR = 0x24;
    public const int MINOR_DCD_LOST = 0x25;
    public const int MINOR_IP_CONFLICT = 0x26;
    public const int MINOR_NET_BROKEN = 0x27;
    public const int MAJOR_OPERATION = 0x3;
    public const int MINOR_START_DVR = 0x41;
    public const int MINOR_STOP_DVR = 0x42;
    public const int MINOR_STOP_ABNORMAL = 0x43;
    public const int MINOR_LOCAL_LOGIN = 0x50;
    public const int MINOR_LOCAL_LOGOUT = 0x51;
    public const int MINOR_LOCAL_CFG_PARM = 0x52;
    public const int MINOR_LOCAL_PLAYBYFILE = 0x53;
    public const int MINOR_LOCAL_PLAYBYTIME = 0x54;
    public const int MINOR_LOCAL_START_REC = 0x55;
    public const int MINOR_LOCAL_STOP_REC = 0x56;
    public const int MINOR_LOCAL_PTZCTRL = 0x57;
    public const int MINOR_LOCAL_PREVIEW = 0x58;
    public const int MINOR_LOCAL_MODIFY_TIME = 0x59;
    public const int MINOR_LOCAL_UPGRADE = 0x5a;
    public const int MINOR_LOCAL_COPYFILE = 0x5b;
    public const int MINOR_REMOTE_LOGIN = 0x70;
    public const int MINOR_REMOTE_LOGOUT = 0x71;
    public const int MINOR_REMOTE_START_REC = 0x72;
    public const int MINOR_REMOTE_STOP_REC = 0x73;
    public const int MINOR_START_TRANS_CHAN = 0x74;
    public const int MINOR_STOP_TRANS_CHAN = 0x75;
    public const int MINOR_REMOTE_GET_PARM = 0x76;
    public const int MINOR_REMOTE_CFG_PARM = 0x77;
    public const int MINOR_REMOTE_GET_STATUS = 0x78;
    public const int MINOR_REMOTE_ARM = 0x79;
    public const int MINOR_REMOTE_DISARM = 0x7a;
    public const int MINOR_REMOTE_REBOOT = 0x7b;
    public const int MINOR_START_VT = 0x7c;
    public const int MINOR_STOP_VT = 0x7d;
    public const int MINOR_REMOTE_UPGRADE = 0x7e;
    public const int MINOR_REMOTE_PLAYBYFILE = 0x7f;
    public const int MINOR_REMOTE_PLAYBYTIME = 0x80;
    public const int MINOR_REMOTE_PTZCTRL = 0x81;
    public const int INFO_LEN = 32;
    public const int INFO_SEQ = 4;
    public const int COMM_ALARM = 0x1100;
    public const int COMM_CONNECT = 0x1200;
    public const int PRESETNUM = 16;
    public const int NET_DEC_STARTDEC = 1;
    public const int NET_DEC_STOPDEC = 2;
    public const int NET_DEC_STOPCYCLE = 3;
    public const int NET_DEC_CONTINUECYCLE = 4;
    public const int MAX_KEYNUM = 19;
    public const int MAXPTZNUM = 100;
    public const int MAX_SMTP_HOST = 128;
    public const int MAX_SMTP_ADDR = 256;
    public const int MAX_STRING = 32;
    public const int PT_ATMI_MAX_ALARM_NUM = 10;
    public const int HB_SDVR_NOERROR = 0;
    public const int NO_PERMISSION = 0xf0;
    public const int HB_SDVR_FILE_SUCCESS = 1000;
    public const int HB_SDVR_FILE_NOFIND = 1001;
    public const int HB_SDVR_ISFINDING = 1002;
    public const int HB_SDVR_NOMOREFILE = 1003;
    public const int HB_SDVR_FILE_EXCEPTION = 1004;
    public const int MOTION_SCOPE_WIDTH = 22;
    public const int MOTION_SCOPE_HIGHT = 18;


    public const int HB_MAX_AM_COUNT = 16;
    public const int NET_SDVR_GET_VCOVER_DETECT = 0x98;
    public const int NET_SDVR_SET_VCOVER_DETECT = 0x99;
    public const int NET_SDVR_IPCWORKPARAM_GET = 0xB0;
    public const int NET_SDVR_IPCWORKPARAM_SET = 0xB1;
    public const int NET_SDVR_STREAM_TYPE_NVR = 0xC0;
    public const int NET_SDVR_WORK_STATE_EX = 0xC1;
    public const int NET_SDVR_GET_CH_CLIENT_IP = 0xC2;
    public const int NET_SDVR_LOG_QUERY_EX = 0xC3;
    public const int NET_SDVR_SERIAL_START_NVR = 0xC4;
    public const int NET_SDVR_SERIAL_STOP_NVR = 0xC5;
    public const int NET_SDVR_DEVICECFG_GET_EX = 0xC6;
    public const int NET_SDVR_DEVICECFG_SET_EX = 0xC7;
    public const int NET_SDVR_PTZLIST_GET_NVR = 0xC8;
    public const int NET_SDVR_ALRM_ATTR_NVR = 0xC9;
    public const int NET_SDVR_ALRMIN_GET_NVR = 0xCA;
    public const int NET_SDVR_ALRMIN_SET_NVR = 0xCB;
    public const int NET_SDVR_ALRMOUT_GET_NVR = 0xCC;
    public const int NET_SDVR_ALRMOUT_SET_NVR = 0xCD;
    public const int NET_SDVR_ALRMIN_STATUS_GET_NVR = 0xCE;
    public const int NET_SDVR_ALRMOUT_STATUS_GET_NVR = 0xCF;
    public const int NET_SDVR_ALRMOUT_STATUS_SET_NVR = 0xD1;
    public const int NET_SDVR_PICCFG_GET_EX_NVR = 0xD2;
    public const int NET_SDVR_PICCFG_SET_EX_NVR = 0xD3;
    public const int NET_SDVR_RECORD_GET_EX_NVR = 0xD4;
    public const int NET_SDVR_RECORD_SET_EX_NVR = 0xD5;
    public const int NET_SDVR_MOTION_DETECT_GET_NVR = 0xD6;
    public const int NET_SDVR_MOTION_DETECT_SET_NVR = 0xD7;
    public const int NET_SDVR_ABNORMAL_ALRM_GET_NVR = 0xD8;
    public const int NET_SDVR_ABNORMAL_ALRM_SET_NVR = 0xD9;
    public const int NET_SDVR_PARAM_FILE_EXPORT = 0xDA;
    public const int NET_SDVR_PARAM_FILE_IMPORT = 0xDB;
    public const int NET_SDVR_RESOLUTION_GET_NVR = 0xDC;
    public const int NET_SDVR_RESOLUTION_SET_NVR = 0xDD;
    public const int NET_SDVR_QUERY_MONTH_RECFILE = 0x47;
    public const int HB_IMPORT_OK = 0x01;
    public const int HB_TRANS_FILE_ERR = 0x02;
    public const int HB_FILE_VERSION_ERR = 0x03;
    public const int NET_SDVR_GET_ZERO_VENCCONF = 0xA8;
    public const int NET_SDVR_SET_ZERO_VENCCONF = 0xA9;
    public const int HB_IPCCFG_THERMAL_IMAGING = 1;
    public const int HB_IPCCFG_IP_FILTER = 2;
    public const int HB_IPCCFG_BLC = 3;
    public const int HB_IPCCFG_PROTOCL = 4;
    public const int OSDINFONUM_MAX = 64;
    public const int OSDSTRSIZE_MAX = 100;
    public const int MAX_IPC_CHANNUM = 4;
}
