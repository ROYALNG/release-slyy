//��ʾģʽ
#if ! DISPLAY_MODE_defined
#define DISPLAY_MODE_defined
//����ģʽ
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
    // �ӿں�������
    //3. ��½�Լ������豸��Ϣ
    //SDK��ʼ�� 

    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_Init();

    //SDK�ͷ�

    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_Cleanup();

    //������Ϣ���մ���
    //
    //��  �ܣ����ý���������Ϣ�ľ���ͷ�ʽ ���ص��������̷߳���Ϣ��  
    //��  ����
    //nMessage��COMM_ALARM(������Ϣ)��COMM_CONNECT(����״̬) 
    //		hWnd�����(�����Ǵ��ڣ��������߳̾��������ʹ���߳��Է�ֹ����) 
    //		����ֵ��TRUE-�ɹ�	FASLE-ʧ��	

    // ע��ʹ��HB_SDVR_SetDVRMessage��HB_SDVR_SetDVRMessage_Nvsʱ��ò�Ҫͬʱʹ�ã�ͬʱʹ��ʱע��ʹ�ò�ͬ�Ĵ��ھ���������޷�����
    // ʹ��HB_SDVR_SetDVRMessageʱ����HB_SDVR_ALARMINFO��LPARAM���н���
    // ʹ��HB_SDVR_SetDVRMessage_Nvsʱ����HB_SDVR_ALARMINFO_EX��LPARAM���н���
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetDVRMessage(uint nMessage, IntPtr hWnd);
    //��չ�ӿڣ�Ϊ�˼������128·������by cui 10.05.20
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetDVRMessage_Nvs(uint nMessage, IntPtr hWnd);
    public delegate int fMessCallBackDelegate(int lCommand, ref string sDVRIP, ref string pBuf, uint dwBufLen);

    //���ñ�����Ϣ�ص�-���ڴ�����Ϣ
    //
    //��  �ܣ����ý�����Ϣ�Ļص����� ��IP���֣� 
    //��  ����
    //#define COMM_ALARM				0x1100	//������Ϣ
    //#define COMM_CONNECT				0x1200	//��������Ͽ�
    //fMessCallBack�� ��Ϣ�ص�����
    //lCommand�� ��Ϣ������ ��COMM_ALARM
    //sDVRIP�� ip��ַ
    //pBuf�������Ϣ�Ļ�����
    //dwBufLen���������Ĵ�С
    //����ֵ��TRUE-�ɹ�	FASLE-ʧ��
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetDVRMessCallBack(fMessCallBackDelegate fMessCallBack);
    //public delegate int fMessCallBackDelegate(int lCommand, ref string sDVRIP, ref string pBuf, uint dwBufLen);
    //����128·�ӿ�
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetDVRMessCallBack_Nvs(fMessCallBackDelegate fMessCallBack);
    //public delegate int fMessCallBack_EXDelegate(int lCommand, int lUserID, ref string pBuf, uint dwBufLen);


    //���ñ�����Ϣ�ص�-�����߳���Ϣ
    //
    //��  �ܣ����ý�����Ϣ�Ļص����� ��ID���֣� 
    //��  ����
    //#define COMM_ALARM				0x1100	//������Ϣ
    //#define COMM_CONNECT				0x1200	//��������Ͽ�
    //fMessCallBack�� ��Ϣ�ص�����
    //lCommand�� ��Ϣ������ ��COMM_ALARM
    //lUserID ����HB_SDVR _Login ����
    //pBuf�������Ϣ�Ļ�����
    //dwBufLen���������Ĵ�С
    //����ֵ��TRUE-�ɹ�	FASLE-ʧ��
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetDVRMessCallBack_EX(fMessCallBack_EXDelegate fMessCallBack_EX);
    public delegate int fMessCallBack_EXDelegate(int lCommand, int lUserID, ref string pBuf, uint dwBufLen);
    //����128·�ӿ�
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetDVRMessCallBack_EX_Nvs(fMessCallBack_EXDelegate fMessCallBack_EX);
    //public delegate int fMessCallBackDelegate(int lCommand, ref string sDVRIP, int lUserID, ref string pBuf, uint dwBufLen, uint dwUser);

    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetDEVMessCallBack(int lUserID, fMessCallBackDelegate fMessCallBack, uint dwUser);


    //�������ӳ�ʱʱ��
    //DWORD dwTryTimes����
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetConnectTime(uint dwWaitTime, uint dwTryTimes);

    //SDK�汾
    [DllImport(SDK_HB)]
    public static extern uint HB_SDVR_GetSDKVersion();

    //ϵͳ�Ƿ�֧��
    //���أ��ɹ�-0xFF   ʧ��-0
    //[DllImport(SDK_HB)]
    //public static extern int HB_SDVR_IsSupport();
    //[DllImport(SDK_HB)]
    //public static extern int HB_SDVR_Login(ref string sDVRIP, ushort wDVRPort, ref string sUserName, ref string sPassword, LPHB_SDVR_DEVICEINFO lpDeviceInfo);
    //[DllImport(SDK_HB)]
    //public static extern int HB_SDVR_LoginA(ref string sDVRIP, ushort wDVRPort, ref string sUserName, ref string sPassword, LPHB_SDVR_DEVICEINFO lpDeviceInfo);
    //[DllImport(SDK_HB)]
    //public static extern int HB_SDVR_LoginW(ref string sDVRIP, ushort wDVRPort, ref string sUserName, ref string sPassword, LPHB_SDVR_DEVICEINFO lpDeviceInfo);
    //    //Ϊ�������128·�������͵���չ�ӿ� 10.05.25
    //[DllImport(SDK_HB)]
    //public static extern int HB_SDVR_Login_Nvs(ref string sDVRIP, ushort wDVRPort, ref string sUserName, ref string sPassword, HB_SDVR_DEVICEINFO_EX lpDeviceInfo);
    //    //*********************************************************************************
    //    //��������HB_SDVR_Login_Ex
    //    //��  ��:��¼��չ����������ʹ�û���ε�¼ͬһ��dvr��
    //    //��  �����÷�ͬHB_SDVR_Login
    //    //**********************************************************************************
    //[DllImport(SDK_HB)]
    //public static extern int HB_SDVR_Login_Ex(ref string sDVRIP, ushort wDVRPort, ref string sUserName, ref string sPassword, LPHB_SDVR_DEVICEINFO lpDeviceInfo);
    //����ʹ�û���ε�¼���Ҽ������128·�������͵���չ�ӿ� 10.05.25
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_Login_Ex_Nvs(string sDVRIP, ushort wDVRPort, string sUserName, string sPassword, out HB_SDVR_DEVICEINFO_EX lpDeviceInfo);

    //4. ע��
    //
    //��  �ܣ�ע��
    //��  ����
    //sDVRIP�� IP��ַ�û�ID ֵ����HB_SDVR _Login ����
    //����ֵ��TRUE-�ɹ�	FASLE-ʧ��
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_Logout(int lUserID);


    //5. ʵʱ��Ƶ
    //
    //��  �ܣ�����Զ����Ƶ
    //��  ����
    //lDVRIP�� IP��ַ�û�ID ֵ����NET_DVR_Login ����
    //lWindows��ͨ����
    //lpClientInfo��ָ��HB_SDVR_CLIENTINFO�ṹ��ָ��
    //����ֵ��FASLE-ʧ�� ������ֵ��ʾ������Ƶ����IDֵ����IDֵ����SDK���䣬��ΪHB_SDVR _StopRealPlay�Ⱥ����Ĳ���
    //

    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_RealPlay(int lUserID, int lWindows, ref HB_SDVR_CLIENTINFO lpClientInfo);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_UpdataRealPlayWnd(int lRealHandle, IntPtr hPlayWnd);
    //6. �ر���Ƶ
    //
    //��  �ܣ��ر�Զ����Ƶ
    //��  ����
    //lRealHandle�� ��Ƶ����IDֵ����HB_SDVR_RealPlay����
    //����ֵ��TRUE-�ɹ�	FASLE-ʧ��

    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_StopRealPlay(int lRealHandle);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_StopRealPlayEx(int lRealHandle);

    //7. ��Ƶ����
    //
    //��  �ܣ�������Ƶ����
    //��  ����
    //lRealHandle�� ��Ƶ����IDֵ����HB_SDVR_RealPlay����
    //videoeff��ָ��HB_SDVR_VIDEOEFFECT�ṹ��ָ��
    //����ֵ��TRUE-�ɹ�	FASLE-ʧ��

    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_ClientSetVideoEffect(int lRealHandle, HB_SDVR_VIDEOEFFECT videoeff);

    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_ClientGetVideoEffect(int lRealHandle, HB_SDVR_VIDEOEFFECT videoeff);
    //��  ����
    //lUserID�� sDVRIP�� IP��ַ�û�ID ֵ����HB_SDVR _Login ����
    //lChannel��ͨ����
    //����ֵ��TRUE-�ɹ�	FASLE-ʧ��
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_ClientSetVideoEffect_Ex(int lUserID, int lChannel, HB_SDVR_VIDEOEFFECT videoeff);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_ClientGetVideoEffect_Ex(int lUserID, int lChannel, HB_SDVR_VIDEOEFFECT videoeff);


    //8. ����ؼ�֡
    //
    //��  �ܣ�ʵʱ��Ƶ�в����ؼ�֡
    //��  ����
    //lUserID�� sDVRIP�� IP��ַ�û�ID ֵ����HB_SDVR _Login ����
    //lChannel��ͨ����
    //����ֵ��TRUE-�ɹ�	FASLE-ʧ��
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_MakeKeyFrame(int lUserID, int lChannel);

    //9. ��̨����(͸����̨) 
    //��  �ܣ���̨�������Ĭ���ٶȣ�������Ƶ��ID������
    //��  ����
    //lRealHandle�� ��Ƶ����IDֵ����HB_SDVR_RealPlay����
    //dwPTZCommand����������
    //#define TM_COM_GUI_BRUSH   0x0001002e   //��ˢ
    //#define TILT_UP			0x0001000c	/* ��̨��SS���ٶ����� */
    //#define TILT_DOWN		0x0001000d	/* ��̨��SS���ٶ��¸� */
    //#define PAN_LEFT		0x0001000e 	/* ��̨��SS���ٶ���ת */
    //#define PAN_RIGHT		0x0001000f	/* ��̨��SS���ٶ���ת */
    //#define ZOOM_IN			0x00010016	/* �������ٶ�SS���(���ʱ��) */
    //#define ZOOM_OUT		0x00010017 	/* �������ٶ�SS��С(���ʱ�С) */
    //#define IRIS_OPEN		0x00010018 	/* ��Ȧ���ٶ�SS���� */
    //#define IRIS_CLOSE		0x00010019	/* ��Ȧ���ٶ�SS��С */
    //#define FOCUS_FAR		0x00010015 	/* �������ٶ�SS��� */
    //#define FOCUS_NEAR		0x00010014  /* �������ٶ�SSǰ�� */
    //#define LIGHT_PWRON		0x00010024	/* ��ͨ�ƹ��Դ */
    //#define WIPER_PWRON		0x00010025	/* ��ͨ��ˢ���� */
    //#define PAN_AUTO		0x0001001c 	/* ��̨��SS���ٶ������Զ�ɨ�� */
    //dwStop������ֹ̨ͣ���ǿ�ʼ
    //����ֵ��TRUE-�ɹ�	FASLE-ʧ��
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_PTZControl(int lRealHandle, uint dwPTZCommand, uint dwStop);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_PTZControl_Other(int lUserID, int lChannel, uint dwPTZCommand, uint dwStop);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_PTZControlWithSpeed(int lRealHandle, uint dwPTZCommand, uint dwStop, uint dwSpeed);
    //͸����̨
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_TransPTZ(int lRealHandle, ref string pPTZCodeBuf, uint dwBufSize);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_TransPTZ_Other(int lUserID, int lChannel, ref string pPTZCodeBuf, uint dwBufSize);

    //10.��������
    //����
    //
    //��  �ܣ�Զ���������� 
    //��  ����
    //lUserID��  IP��ַ�û�ID ֵ����HB_SDVR _Login ����
    //����ֵ�� FASLE-ʧ�� TRUE-�ɹ�
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_RebootDVR(int lUserID);

    //�ر�DVR
    //
    //��  �ܣ�Զ�̹ر����� 
    //��  ����
    //lUserID��  IP��ַ�û�ID ֵ����HB_SDVR _Login ����
    //����ֵ�� FASLE-ʧ�� TRUE-�ɹ�
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_ShutDownDVR(int lUserID);


    //11.��������
    //
    //��  �ܣ�Զ���������� 
    //��  ����
    //lUserID��  IP��ַ�û�ID ֵ����HB_SDVR _Login ����
    //sFileName���������ļ�����·��
    //����ֵ�� FASLE-ʧ��  ��������������ID
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_Upgrade(int lUserID, ref string sFileName);

    //
    //��  �ܣ�Զ����������״̬ 
    //��  ����
    //lUpgradeHandle�� ������ID����HB_SDVR_Upgrade����
    //����ֵ�� FASLE-ʧ�� 
    //����ֵ��
    //-1 �汾���ԣ�����ʧ��
    //100 ���ݴ�����ɣ��ȴ���������  
    //101 �������³ɹ�
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_GetUpgradeState(int lUpgradeHandle);

    //�����������ж� �˽ӿڱ���
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_CloseUpgradeHandle(int lUpgradeHandle);
    //12.�Խ����� 
    //�����Խ�
    //
    //��  �ܣ���ʼ�����Խ�
    //��  ����
    //lUserID��NET_DVR_Login ()�ķ���ֵ
    //fVoiceDataCallBack���ص��������ص���Ƶ����
    //dwUser���û�����
    //�ص�����˵����
    //lVoiceComHandle��HB_SDVR_StartVoiceCom ()�ķ���ֵ
    //pRecvDataBuffer��������ݵĻ�����ָ��
    //dwBufSize���������Ĵ�С
    //byAudioFlag����������
    //0- �ͻ��˲ɼ�����Ƶ����
    //1- �ͻ����յ��豸�˵���Ƶ����
    //dwUser���û����ݣ���������������û�����
    //����ֵ��-1��ʾʧ�ܣ�����ֵ��ΪHB_SDVR _SetVoiceComClientVolume ()�Ⱥ����Ĳ��� 
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_StartVoiceCom(int lUserID, fVoiceDataCallBackDelegate fVoiceDataCallBack, uint dwUser);


    //
    //��  �ܣ����������Խ�PC �˵�����
    //��  ����
    //lVoiceComHandle��HB_SDVR _StartVoiceCom �ķ���ֵ
    //wVolume�����ú����������0-0xffff
    //����ֵ��TRUE ��ʾ�ɹ���FALSE ��ʾʧ�ܡ�
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetVoiceComClientVolume(int lVoiceComHandle, ushort wVolume);

    //
    //��  �ܣ������Խ�
    //��  ����
    //lVoiceComHandle��HB_SDVR _StartVoiceCom �ķ���ֵ
    //����ֵ��TRUE ��ʾ�ɹ���FALSE ��ʾʧ��
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_StopVoiceCom(int lVoiceComHandle);

    //���������㲥
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_ClientAudioStart();
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_ClientAudioStop();
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_AddDVR(int lUserID);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_DelDVR(int lUserID);
    public delegate void fSerialDataCallBackDelegate(int lSerialHandle, ref string pRecvDataBuffer, uint dwBufSize, uint dwUser);

    //13.͸��ͨ��
    ////*�ر�ע�� ����485ͨ����ʱ��һ��Ҫ��232��channel=2����һ�²ſ���
    //��  �ܣ�����͸��ͨ��
    //��  ���� 
    //lUserID��NET_DVR_Login ()�ķ���ֵ
    //lSerialPort�����ںţ�1-232 ���ڣ�2-485 ����
    //dwUser���û�����
    //fSerialDataCallBack���ص�����
    //�ص�����˵����
    //lSerialHandle��NET_DVR_SerialStart()�ķ���ֵ
    //pRecvDataBuffer����Ž��յ����ݵĻ�����ָ��
    //dwBufSize���������Ĵ�С
    //dwUser��������û�����
    //����ֵ��-1��ʾʧ�ܣ�����ֵ��ΪHB_SDVR_SerialSend()�Ⱥ����Ĳ���
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SerialStart(int lUserID, int lSerialPort, fSerialDataCallBackDelegate fSerialDataCallBack, uint dwUser);

    //
    //��  �ܣ�ͨ��͸��ͨ����DVR ���ڷ�������
    //��  ����lSerialHandle��NET_DVR_SerialStart �ķ���ֵ
    //lChannel��Ӳ��¼�����ͨ����, ��485 ����͸��ͨ��ʱ��Ч,ָ�����ĸ�ͨ��������,��232 ����͸��ͨ��ʱ���ó�0;
    //pSendBuf��Ҫ���͵Ļ�������ָ��
    //dwBufSize���������Ĵ�С
    //����ֵ��TRUE ��ʾ�ɹ���FALSE ��ʾʧ��
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SerialSend(int lSerialHandle, int lChannel, ref string pSendBuf, uint dwBufSize);

    //
    //���ܣ��Ͽ�͸��ͨ��
    //����˵����
    //lSerialHandle��NET_DVR_SerialStart �ķ���ֵ
    //����ֵ��TRUE ��ʾ�ɹ���FALSE ��ʾʧ�ܡ�
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SerialStop(int lSerialHandle);

    //14.�������
    //
    //��  �ܣ��������
    //��  ����
    //lUserID��  �û�ID ֵ����HB_SDVR _Login ����
    //lKeyIndex����ֵ
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
    //����ֵ����FALSE ��ʾʧ�ܣ�TRUE��ʾ�ɹ�
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_ClickKey(int lUserID, int lKeyIndex);

    //15.�ֶ�¼��
    //
    //��  �ܣ���ȡԶ���ֶ�¼��
    //��  ����
    //lUserID��   �û�ID ֵ����HB_SDVR _Login ����
    //lChannel�����յ�λ����λ��˳���ʾͨ����
    //lRecordType������
    //����ֵ����FALSE ��ʾʧ�ܣ�TRUE��ʾ�ɹ�
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_GetDVRRecord(int lUserID, ref ushort lChannel, int lRecordType);

    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_GetDVRRecord_Nvs(int lUserID, HB_SDVR_REMOTERECORDCHAN lChannel, int lRecordType);

    //
    //��  �ܣ�����Զ���ֶ�¼��
    //��  ����
    //lUserID��  IP��ַ�û�ID ֵ����HB_SDVR _Login ����
    //lChannel�����յ�λ����λ��˳���ʾͨ����
    //����ֵ����FALSE ��ʾʧ�ܣ�TRUE��ʾ�ɹ�
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetDVRRecord(int lUserID, ushort lChannel);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetDVRRecord_Nvs(int lUserID, HB_SDVR_REMOTERECORDCHAN lChannel);

    //16.�豸����
    //���أ�7004  8004  2004�����ͺ�

    //
    // typedef struct {
    // 	ULONG  dvrtype;    //7004  8004  2004�����ͺ�
    // 	ULONG  nNULL1;         
    // 	ULONG  nNULL2;
    //  }HB_SDVR_INFO,*LPHB_SDVR_INFO; 


    //����汾

    //[DllImport(SDK_HB)]
    //public static extern int HB_SDVR_GetDeviceType(int lUserID, LPHB_SDVR_INFO device);
    //[DllImport(SDK_HB)]
    //public static extern int HB_SDVR_SetDeviceType(int lUserID, LPHB_SDVR_INFO device);
    //17.�豸Ӳ����Ϣ
    //18.�豸������Ϣ
    //19.ͨ������(��������¼��,�ƶ�¼���)
    //20.����ѹ������
    //21.¼�����
    //22.����������(��̨����)
    //23.��������
    //24.�������
    //25.�û�Ȩ��
    //26.DNS
    //27.PPPoE
    //28.ƽ̨��Ϣ
    //DVR�豸����
    //32.�������״̬




    /////////////////////////////////////////////////////////////////////////////////////
    //
    //��  �ܣ���ȡԶ����������
    //��  ����
    //lUserID��  IP��ַ�û�ID ֵ����HB_SDVR _Login ����
    //dwCommand������
    //HB_SDVR_SET_DEVICECFG:   �豸Ӳ����Ϣ
    //HB_SDVR_SET_NETCFG:		 �豸������Ϣ
    //HB_SDVR_SET_PICCFG_EX:	 ͨ������(��������¼��,�ƶ�¼���)	 
    //HB_SDVR_SET_COMPRESSCFG: ����ѹ������
    //HB_SDVR_SET_RECORDCFG:	 ¼�����
    //HB_SDVR_SET_DECODERCFG:  ����������(��̨����)
    //HB_SDVR_SET_RS232CFG:    RS232
    //HB_SDVR_SET_ALARMINCFG:	 ��������
    //HB_SDVR_SET_ALARMOUTCFG: �������	
    //HB_SDVR_SET_TIMECFG:     ʱ��
    //HB_SDVR_SET_USERCFG:	 �û�Ȩ��
    //HB_SDVR_SET_DNS:         DNS
    //HB_SDVR_SET_PPPoE:		 PPPoE	
    //HB_SDVR_SERVERCFG_SET:   ƽ̨��Ϣ
    //lChannel��ͨ��
    //lpOutBuffer������������
    //dwInBufferSize�����������ݴ�С
    //lpBytesReturned������
    //type������
    //����ֵ����FALSE ��ʾʧ�ܣ�TRUE��ʾ�ɹ�
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_GetDVRConfig(int lUserID, uint dwCommand, int lChannel, IntPtr lpOutBuffer, uint dwOutBufferSize, ref uint lpBytesReturned, uint type);

    //
    //��  �ܣ�Զ����������
    //��  ����
    //lUserID��  IP��ַ�û�ID ֵ����HB_SDVR _Login ����
    //dwCommand������
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
    //lChannel��ͨ��
    //lpInBuffer������������
    //dwInBufferSize�����������ݴ�С
    //����ֵ����FALSE ��ʾʧ�ܣ�TRUE��ʾ�ɹ�
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetDVRConfig(int lUserID, uint dwCommand, int lChannel, IntPtr lpInBuffer, uint dwInBufferSize);

    //29.������Ϣ
    //30.Զ��¼���ļ���ѯ�͵㲥  ����
    //
    //��  �ܣ�Զ���ļ��ط�
    //��  ����
    //lUserID��  IP��ַ�û�ID ֵ����HB_SDVR _Login ����
    //lChannel��ͨ����
    //dwPTZCommand����������
    //dwStop������ֹ̨ͣ���ǿ�ʼ
    //dwSpeed���ٶ�
    //����ֵ����FALSE ��ʾʧ�ܣ�����ֵ��ΪHB_SDVR _FindClose �Ⱥ����Ĳ���
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_FindFile(int lUserID, int lChannel, uint dwFileType, HB_SDVR_TIME lpStartTime, HB_SDVR_TIME lpStopTime);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_FindFile_ByCard(int lUserID, ref string lpcard, uint dwFileType, HB_SDVR_TIME lpStartTime, HB_SDVR_TIME lpStopTime);

    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_FindFileWithChl(int lUserID, int lChannel, uint dwFileType, ref HB_SDVR_TIME lpStartTime, ref HB_SDVR_TIME lpStopTime);
    //
    //��  �ܣ���ȡԶ���ļ�����Ϣ
    //��  ����
    //lRealHandle�� ��Ƶ����IDֵ����HB_SDVR_RealPlay���� 
    //lpFindData�� ָ��HB_SDVR_FIND_DATA�Ľṹ���ָ��
    //����ֵ����������ֵ
    //#define HB_SDVR_FILE_SUCCESS				1000	//����ļ���Ϣ
    //#define HB_SDVR_FILE_NOFIND					1001	//û���ļ�
    //#define HB_SDVR_ISFINDING					1002	//���ڲ����ļ�
    //#define	HB_SDVR_NOMOREFILE	 1003	//�����ļ�ʱû�и�����ļ�
    //#define	HB_SDVR_FILE_EXCEPTION				1004	//�����ļ�ʱ�쳣
    //¼������

    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_FindNextFile(int lFindHandle, out HB_SDVR_FIND_DATA lpFindData);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_FindNextFile_ByCard(int lFindHandle, HB_SDVR_FIND_DATA lpFindData);

    //
    //��  �ܣ�������ȡԶ���ļ�����Ϣ
    //��  ����
    //lFindHandle�� �ļ�����IDֵ����HB_SDVR_FindFile���� 
    //����ֵ��TRUE-�ɹ�	FASLE-ʧ��
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_FindClose(int lFindHandle);

    //
    //��  �ܣ������ļ����ط�
    //��  ����
    //lUserID��  IP��ַ�û�ID ֵ����HB_SDVR _Login ����
    //Channel��ͨ����
    //sPlayBackFileName�� Ҫ������ļ�����·��
    //hWnd����ʾ�Ĵ��ھ��
    //����ֵ��FASLE-ʧ�� ������ֵ��ʾ������Ƶ����IDֵ����IDֵ����SDK���� 
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_PlayBackByName(int lUserID, string sPlayBackFileName, IntPtr hWnd);

    //
    //��  �ܣ�����ʱ��λط�
    //��  ����
    //lUserID��  IP��ַ�û�ID ֵ����HB_SDVR _Login ����
    //Channel��ͨ����
    //lpStartTime�� ��ʼʱ��
    //lpStopTime������ʱ��
    //hWnd����ʾ�Ĵ��ھ��
    //����ֵ��FASLE-ʧ�� ������ֵ��ʾ������Ƶ����IDֵ����IDֵ����SDK���� 

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
    //��  �ܣ��طſ���
    //��  ����
    //lRealHandle�� ��Ƶ����IDֵ����HB_SDVR_RealPlay���� 
    //dwControlCode����������
    //#define HB_SDVR_PLAYSTART		1//��ʼ����
    //#define HB_SDVR_PLAYSTOP		2//ֹͣ����
    //#define HB_SDVR_PLAYPAUSE		3//��ͣ����
    //#define HB_SDVR_PLAYRESTART		4//�ָ�����
    //#define HB_SDVR_PLAYFAST		5//���
    //#define HB_SDVR_PLAYSLOW		6//����
    //#define HB_SDVR_PLAYNORMAL		7//�����ٶ�
    //#define HB_SDVR_PLAYFRAME		8//��֡��
    //#define HB_SDVR_PLAYSTARTAUDIO		9//������
    //#define HB_SDVR_PLAYSTOPAUDIO		10//�ر�����
    //dwInValue��HB_SDVR_PLAYFAST��HB_SDVR_PLAYSLOW��Ҫ�ٶȱ���  ��Χ0-3
    //lpOutValue������
    //����ֵ��TRUE-�ɹ�	FASLE-ʧ��
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_PlayBackControl(int lPlayHandle, uint dwControlCode, uint dwInValue, out uint lpOutValue);



    //
    //��  �ܣ�ֹͣ�ط� 
    //��  ����
    //lRealHandle�� ��Ƶ����IDֵ����HB_SDVR_RealPlay���� 
    //����ֵ��TRUE-�ɹ�	FASLE-ʧ��
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_StopPlayBack(int lPlayHandle);

    //
    //��  �ܣ������ļ������� 
    //��  ����
    //lUserID��  IP��ַ�û�ID ֵ����HB_SDVR _Login ����
    //sDVRFileName��Զ���ļ����ļ���
    //sSavedFileName��Ҫ���汾�ص��ļ�����·��
    //����ֵ��FALSE ��ʾʧ�ܣ�����ֵ��ʾ���ر�������IDֵ����IDֵ����SDK����
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_GetFileByName(int lUserID, string sDVRFileName, string sSavedFileName);

    //
    //��  �ܣ������ļ������� 
    //��  ����
    //lUserID��  IP��ַ�û�ID ֵ����HB_SDVR _Login ����
    //sDVRFileName��Զ���ļ����ļ���
    //sSavedFileName��Ҫ���汾�ص��ļ�����·��,���Ϊ�յĻ�,��ʾ�����ļ�������
    //����ֵ��FALSE ��ʾʧ�ܣ�����ֵ��ʾ���ر�������IDֵ����IDֵ����SDK����
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_GetFileByName_EX(int lUserID, ref string sDVRFileName, ref string sSavedFileName);

    //
    //��  �ܣ�����ʱ��α���
    //��  ����
    //lUserID��  IP��ַ�û�ID ֵ����HB_SDVR _Login ����
    //lChannel��ͨ����
    //lpStartTime����ʼʱ��
    //lpStopTime������ʱ��
    //sSavedFileName��Ҫ���汾�ص��ļ���
    //����ֵ��FALSE ��ʾʧ�ܣ�����ֵ��ʾ���ر�������IDֵ����IDֵ����SDK����
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_GetFileByTime(int lUserID, int lChannel, HB_SDVR_TIME lpStartTime, HB_SDVR_TIME lpStopTime, string sSavedFileName);

    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_GetFileByTimeEx(int lUserID, int lChannel, byte byType, HB_SDVR_TIME lpStartTime, HB_SDVR_TIME lpStopTime, ref string sSavedFileName);

    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_GetFileByTimeWithChl(int lUserID, int lChannel, HB_SDVR_TIME lpStartTime, HB_SDVR_TIME lpStopTime, ref string sSavedFileName);


    // ʹ�ýṹ�����
    //C++ TO C# CONVERTER TODO TASK: The original C++ function pointer contained an unconverted modifier:
    //ORIGINAL LINE: typedef void (CALLBACK* PHB_SDVR_STREAMDATA_PROC)(int lRealHandle,uint dwDataType, byte *pBuffer,uint dwBufSize,uint dwUser);
    public delegate void PHB_SDVR_STREAMDATA_PROC(int lRealHandle, uint dwDataType, ref byte pBuffer, uint dwBufSize, uint dwUser);

    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_GetFile(int lUserID, HB_SDVR_FILEGETCOND pGetFile);

    //
    //��  �ܣ�ֹͣ���� 
    //��  ����
    //lFileHandle�� ��������IDֵ����HB_SDVR_GetFileByTime��HB_SDVR_GetFileByName���� 
    //����ֵ��TRUE-�ɹ�	FASLE-ʧ��
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_StopGetFile(int lFileHandle);

    //
    //��  �ܣ������ܵ����ݴ�С  
    //��  ����
    //lFileHandle�� ��������IDֵ����HB_SDVR_GetFileByTime��HB_SDVR_GetFileByName���� 
    //����ֵ�� FASLE-ʧ�� ����ֵΪ������ ��λΪ K
    [DllImport(SDK_HB)]
    public static extern uint HB_SDVR_GetDownloadTotalSize(int lFileHandle);

    //
    //��  �ܣ������Ѿ����ܵ�������  
    //��  ����
    //lFileHandle�� ��������IDֵ����HB_SDVR_GetFileByTime��HB_SDVR_GetFileByName���� 
    //����ֵ�� FASLE-ʧ�� ����ֵΪ������ ��λΪ K
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_GetDownloadPos(int lFileHandle);

    //
    //��  �ܣ������Ѿ����ܵ�������  
    //��  ����
    //lFileHandle�� ��������IDֵ����HB_SDVR_GetFileByTime��HB_SDVR_GetFileByName���� 
    //����ֵ�� FASLE-ʧ�� ����ֵΪ������ ��λΪ B
    [DllImport(SDK_HB)]
    public static extern double HB_SDVR_GetDownloadBytesSize(int lFileHandle); //cwh 20080806


    //31.Զ�̷���������״̬
    //
    //��  �ܣ�������״̬
    //��  ����
    //lUserID��  IP��ַ�û�ID ֵ����HB_SDVR _Login ����
    //lpWorkState��ָ��HB_SDVR_WORKSTATE�Ľṹ��ָ��
    //����ֵ����FALSE ��ʾʧ�ܣ�TRUE��ʾ�ɹ�

    //
    //��  �ܣ�������״̬
    //��  ����
    //lUserID��  IP��ַ�û�ID ֵ����HB_SDVR _Login ����
    //lpWorkState��ָ��HB_SDVR_WORKSTATE�Ľṹ��ָ��
    //����ֵ����FALSE ��ʾʧ�ܣ�TRUE��ʾ�ɹ�
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_GetDVRWorkState(int lUserID, ref HB_SDVR_WORKSTATE lpWorkState);
    //�������128·����չ�ӿ�
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_GetDVRWorkState_Nvs(int lUserID, ref HB_SDVR_WORKSTATE_EX lpWorkState);


    //33.��־
    //
    //��  �ܣ���ѯ��־
    //��  ����
    //lUserID��  IP��ַ�û�ID ֵ����HB_SDVR _Login ����
    //lpStartTime����ʼʱ��
    //lpStopTime������ʱ��
    //����ֵ����FALSE ��ʾʧ�ܣ�����������־ID��
    //
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_FindDVRLog(int lUserID, HB_SDVR_TIME lpStartTime, HB_SDVR_TIME lpStopTime);

    //
    //��  �ܣ���һ����־
    //��  ����
    //lLogHandle��   ��־ID ֵ����HB_SDVR_FindDVRLog����
    //lpLogData��һ����־��Ϣ
    //nlanguage:���� 0�����ģ�1��Ӣ�ġ�
    //����ֵ����FALSE ��ʾʧ�ܣ�TRUE��ʾ�ɹ�
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_FindNextLog(int lLogHandle, ref string lpLogData, int nlanguage);

    //
    //��  �ܣ�������������
    //��  ����
    //lLogHandle��   ��־ID ֵ����HB_SDVR_FindDVRLog����
    //����ֵ����FALSE ��ʾʧ�ܣ�TRUE��ʾ�ɹ�
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_FindLogClose(int lLogHandle);



    //34.N/P���л�
    //#define   NET_SDVR_SET_PRESETPOLL   0x73
    //#define   NET_SDVR_GET_PRESETPOLL   0x74
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_GET_PRESETPOLL(int lUserID, HB_SDVR_PRESETPOLL presetpoll);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SET_PRESETPOLL(int lUserID, HB_SDVR_PRESETPOLL presetpoll);


    //35.��Ԥ�õ���Ѳ
    //#define   NET_SDVR_SET_VIDEOSYS       0x75
    //#define   NET_SDVR_GET_VIDEOSYS       0x76
    //ֻ��һ���ֽڴ�����Ƶ��ʽֵ��1---PAL��2---NTSC4.43  3--NTSC3.58
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_GET_VIDEOSYS(int lUserID, ref byte videosys);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SET_VIDEOSYS(int lUserID, byte videosys);


    //����

    //ˢ��������������
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_REFRESH_FLASH(int lUserID);

    //��������
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_OpenSound(int lRealHandle);

    //�ر�����
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_CloseSound();

    //��������
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_OpenSoundShare(int lRealHandle);

    //�ر�����
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_CloseSoundShare(int lRealHandle);

    //�������� 
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_Volume(int lRealHandle, ushort wVolume);

    //����¼��
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SaveRealData(int lRealHandle, string sFileName);

    //ֹͣ����¼��
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_StopSaveRealData(int lRealHandle);
    public delegate void fRealDataCallBackDelegate(int lRealHandle, uint dwDataType, ref byte pBuffer, uint dwBufSize, uint dwUser);

    //����ʵʱ�����ݻص�
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetRealDataCallBack(int lRealHandle, fRealDataCallBackDelegate fRealDataCallBack, uint dwUser);
    //public delegate void fRealDataCallBackDelegate(int lRealHandle, PFRAMEINFO pFarmeInfo, ref byte pBuffer, uint dwBufSize, uint dwUser);

    //����ʵʱԭʼ���ݴ�֡��Ϣ�ص�
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetRealDataCallBack_Ex(int lRealHandle, fRealDataCallBackDelegate fRealDataCallBack, uint dwUser);
    //ץͼ
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_CapturePicture(int lRealHandle, string sPicFileName);

    //������
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetDecMode(int bSDKDec); //ȫ�֣���7000SDK�У���ʹ���ڲ����뺯��

    //�������
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_ClearAlarm(int lUserID);

    //���û�ȡ���к�
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_GetSEQ(int lUserID, ref string buff, uint dwBufSize);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetSEQ(int lUserID, ref string buff, uint dwBufSize);

    //��Ƶ��ʧ״̬  ��λ
    [DllImport(SDK_HB)]
    public static extern ushort HB_SDVR_GET_VideoLostStatus(int lUserID);
    //�������128·���ͻ��� add by cui for 7024 or nvs 100325
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

    //�ص�ʱָ�������ʽ
    //#define SDVR_OUT_FMT_YUV_420
    //#define SDVR_OUT_FMT_YUV_422
    public const int SDVR_OUT_FMT_YUV_420 = 0x00000601;
    public const int SDVR_OUT_FMT_YUV_422 = 0x00000102;
    //�����ص� ��ȡ��������ݺ�ʱ�䣬֡
    //ʵʱ���ݻص�  ���������� ֡��Ϣ����ʱ��
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetRealDecCallBack(int lRealHandle, DecCBFunDelegate DecCBFun, int nOutFormat, int dwUser);
    //public delegate void DecCBFunDelegate(int nChl, ref string pBuf, int nSize, ref HB_FRAME_INFO pFrameInfo, int nReserved1, int nReserved2, int dwUser);

    //�㲥���ݻص�
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetPlayDecCallBack(int lRealHandle, DecCBFunDelegate DecCBFun, int nOutFormat, int dwUser);
    public delegate void SrcDataParseCBFunDelegate(int nChl, ref string SrcDataBuf, int nSize, int nFrameType, HB_VIDEO_TIME ets, int user);
    //ԭʼ������ ��֡ �Ұ���֡��Ϣ
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetParseCallBack(int lRealHandle, SrcDataParseCBFunDelegate SrcDataParseCBFun, int nRseserved);
    //public delegate void  SrcDataParseCBFunDelegate(int nChl, HB_FRAME pFrame, IntPtr  pContext);

    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetParseCallBack_Ex(int lRealHandle, SrcDataParseCBFunDelegate SrcDataParseCBFun, int dwUser);
    //public delegate void  SrcDataParseCBFunDelegate(int nChl, ref string SrcDataBuf, int nSize, int nFrameType, HB_VIDEO_TIME ets, int user);
    //���ػص�
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetDownloadCallBack(int lFileHandle, SrcDataParseCBFunDelegate SrcDataParseCBFun, int nReserved1);
    //public delegate void  SrcDataParseCBFunDelegate(int nChl, HB_FRAME pFrame, IntPtr  pContext);
    [DllImport(SDK_HB)]
    public static extern int HB_SDVR_SetDownloadCallBackEx(int lFileHandle, SrcDataParseCBFunDelegate SrcDataParseCBFun, int dwUser);
    public delegate void fDrawFunDelegate(int lRealHandle, IntPtr hDc, int dwUser);


    ////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //
    //*����ӿڱ���   20080702
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
    public static extern uint HB_SDVR_GetPlayBackDTS(int lPlayHandle); //���ص�ǰ֡��DTSʱ��
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

    // ��������û��ʵ�֣���HB_SDVR_ClientSetVideoEffect/HB_SDVR_ClientGetVideoEffect�к�����
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
    //	���ܣ�����ľֲ��Ŵ�
    //	���룺lPlayHandle ���ž����nRegionNum ��ʾ������ţ�����1��3����pSrcRect Ҫ�Ŵ������
    //			hdestWnd ��ʾ��Ƶ�Ĵ��ھ����bEnable 1��ʾ 0����ʾ��
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
    //�ָ�DVRȱʡ��������
    //nType: 0:����ȱʡ;1:����ȱʡ.
    //cwh 20090318
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_RecoverDefault(int lUserID,byte nType);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_RecoverDefault(int lUserID, byte nType);



    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_GetPTZType(int lUserID, LPSTRUCT_SDVR_PTZTYPE lpStructPtzType, int lSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_GetPTZType(int lUserID, LPSTRUCT_SDVR_PTZTYPE lpStructPtzType, int lSize);



    //��ʱ���� //add by cwh 20090803
    //*********************************************************
    //������
    //	lUserID����½ID
    //	struParam��STRUCT_ALARMIN_WAIT�ṹ��
    //����ֵ��
    //	TRUE���ɹ���
    //	FALSE��ʧ�ܡ�
    //**********************************************************
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_SetDelayDefence(int lUserID,STRUCT_ALARMIN_WAIT struParam);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_SetDelayDefence(int lUserID, STRUCT_ALARMIN_WAIT struParam);

    //��ȡ����״̬ //add by cwh 20090810
    //*********************************************************
    //������
    //	lUserID����½ID
    //	struParam��STRUCT_ALARMIN_WAIT�ṹ��
    //����ֵ��
    //	TRUE���ɹ���
    //	FALSE��ʧ�ܡ�
    //**********************************************************
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_GetDelayDefence(int lUserID,LPSTRUCT_ALARMIN_WAIT pStruParam);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_GetDelayDefence(int lUserID, LPSTRUCT_ALARMIN_WAIT pStruParam);

    //
    //���ܣ���ȡIPC����
    //�ṹ�壺STRUCT_SDVR_IPCCONFIG
    //��������¼id lUserID 
    //����ֵ���ɹ�ture ʧ��false
    //

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_GetIpcConfig(int lUserID,LPSTRUCT_SDVR_IPCCONFIG pStruIpcParam);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_GetIpcConfig(int lUserID, LPSTRUCT_SDVR_IPCCONFIG pStruIpcParam);
    //
    //���ܣ�����IPC����
    //�ṹ�壺STRUCT_SDVR_IPCCONFIG
    //��������¼id lUserID 
    //����ֵ���ɹ�ture ʧ��false
    //

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_SetIpcConfig(int lUserID,LPSTRUCT_SDVR_IPCCONFIG pStruIpcParam);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_SetIpcConfig(int lUserID, LPSTRUCT_SDVR_IPCCONFIG pStruIpcParam);
    //
    //���ܣ�����IPC����
    //��������¼id lUserID 
    //����ֵ���ɹ�ture ʧ��false
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
    //���ܣ�����IPCAGC
    //��������¼id lUserID 
    //����ֵ���ɹ�ture ʧ��false
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
    //���ܣ�����IPC���߲�������
    //STRUCT_SDVR_IPCWIRELESS
    //������ 
    //����ֵ���ɹ�ture ʧ��false
    //

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_SetIPCWirelessSet(int lUserID, LPSTRUCT_SDVR_IPCWIRELESS pStruIpcWireless);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_SetIPCWirelessSet(int lUserID, LPSTRUCT_SDVR_IPCWIRELESS pStruIpcWireless);

    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_GetIPCWirelessSet(int lUserID,LPSTRUCT_SDVR_IPCWIRELESS pStruIpcWireless);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_GetIPCWirelessSet(int lUserID, LPSTRUCT_SDVR_IPCWIRELESS pStruIpcWireless);
    // ��7000sdk�ӿڣ���������
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_SetIPCWirelessGet(int lUserID,LPSTRUCT_SDVR_IPCWIRELESS pStruIpcWireless);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_SetIPCWirelessGet(int lUserID, LPSTRUCT_SDVR_IPCWIRELESS pStruIpcWireless);
    //********************************************************************************
    //add by cui 09.10.09
    //���ܣ�����ouerlayģʽ��
    //����: bOverlayMode[In] : ΪTRUE ��ʾ����Overlay ģʽ, �������Overlay
    //		ƽ��ʧ��,���Զ�����������ʾģʽ.
    //		colorKey [In] : Ҫ���õ�͸��ɫ. ͸��ɫ�൱��һ��͸��Ĥ����ʾ��
    //		����ֻ�ܴ���������ɫ������������ɫ����ס��ʾ�Ļ���.�û�Ӧ������ʾ����
    //		��Ϳ��������ɫ���������ܿ�����ʾ����.һ��Ӧ��ʹ��һ�ֲ����õ���ɫ��Ϊ
    //		͸��ɫ.����һ��DWORD ֵ:0x00rrggbb,����ֽ�Ϊ0���������ֽڷֱ��ʾ
    //		r,g,b ��ֵ.
    //
    //********************************************************************************
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_SetOverlayMode(int lPlayHandle, int bOverlayMode, uint colorKey);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_SetOverlayMode(int lPlayHandle, int bOverlayMode, uint colorKey);
    //add by njt for ̩��
    //
    //���ܣ���ȡ�ײ�UserID
    //������
    //   lUserID ����HB_SDVR_Login����
    //����ֵ��
    //    �ɹ�:���صײ�UserID
    //	ʧ��:����false
    //
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_GetUserID(int lUserID);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_GetUserID(int lUserID);
    //ADD by njt
    // ���ܣ� ����7000T��֡���
    // ������ lUserID ����¼id 
    //       iFrameRateΪSTRUCT_SDVR_IFRAMERATE�ṹ��
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
    /////////////////////////////////������8000TH����ӵ�Э��ӿ�////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////


    //********************************************************************************
    ////add by cuigc for 8000TH 100401
    //���ܣ�8000TH  NTP���ò��������úͲ�ѯ��
    //����: lUserID �û�ID���û���¼ʱ���صġ�pStruNtpPara NTP���ò�����
    //���أ�true �ɹ�   false ʧ�ܡ�
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
    //���ܣ�8000TH �ʼ�������������úͲ�ѯ��
    //����: lUserID �û�ID���û���¼ʱ���صġ�pStruSmtpPara �ʼ��������ò�����
    //���أ�true �ɹ�   false ʧ�ܡ�
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
    //���ܣ�8000TH ��Ѳ���ò��������úͲ�ѯ��
    //����: lUserID �û�ID���û���¼ʱ���صġ�pStruPollPara ��Ѳ���ò�����
    //���أ�true �ɹ�   false ʧ�ܡ�
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
    //���ܣ�8000TH ��Ƶ������������úͲ�ѯ��
    //����: lUserID �û�ID���û���¼ʱ���صġ�pStruVideoMatrixPara ��Ƶ�������ò�����
    //���أ�true �ɹ�   false ʧ�ܡ�
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
    //���ܣ�ƽ̨��Ϣ�����úͲ�ѯ��
    //����: lUserID �û�ID���û���¼ʱ���صġ�
    //���أ�true �ɹ�   false ʧ�ܡ�
    //********************************************************************************


    // typedef struct
    // {
    // 	BYTE plat_type[MAX_PLATNUM];//ƽ̨����
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

    //����ͨ��
    //C++ TO C# CONVERTER NOTE: CALLBACK is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_AlarmStart(int lUserID,void(CALLBACK *fAlarmDataCallBack)(int lAlarmHandle,sbyte *pRecvDataBuffer,uint dwBufSize,uint dwUser),uint dwUser);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_AlarmStart(int lUserID, fAlarmDataCallBackDelegate fAlarmDataCallBack, uint dwUser);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_AlarmStop(int lUserID);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_AlarmStop(int lUserID);


    // ����ATM
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

    // ��ȡʵʱ����״̬
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_GetRealDefenceCfg(int lUserID, LPHB_SDVR_REAL_DEFENCE lpCfg, uint* pDataSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_GetRealDefenceCfg(int lUserID, LPHB_SDVR_REAL_DEFENCE lpCfg, ref uint pDataSize);
    // ʵʱ����������
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_SetRealDefenceCfg(int lUserID, LPHB_SDVR_REAL_DEFENCE lpCfg, uint dwDataSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_SetRealDefenceCfg(int lUserID, LPHB_SDVR_REAL_DEFENCE lpCfg, uint dwDataSize);
    // ��ȡ���������ڼ�ı�����Ϣ
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_GetDisconAlarmInfo(int lUserID, LPHB_SDVR_DISCONN_ALMSTAT lpInfo, uint* pDataSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_GetDisconAlarmInfo(int lUserID, LPHB_SDVR_DISCONN_ALMSTAT lpInfo, ref uint pDataSize);

    // �ӿں���
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_GetVcoverAlarmCfg(int lUserID, LPHB_SDVR_VCOVER_ALM lpCfg, uint* pDataSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_GetVcoverAlarmCfg(int lUserID, LPHB_SDVR_VCOVER_ALM lpCfg, ref uint pDataSize);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_SetVcoverAlarmCfg(int lUserID, LPHB_SDVR_VCOVER_ALM lpCfg, uint dwDataSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_SetVcoverAlarmCfg(int lUserID, LPHB_SDVR_VCOVER_ALM lpCfg, uint dwDataSize);

    // �ӿں���
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_IpcGetWorkParam(int lUserID, LPHB_SDVR_IPCWORKMODE lpCfg, uint* pDataSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_IpcGetWorkParam(int lUserID, LPHB_SDVR_IPCWORKMODE lpCfg, ref uint pDataSize);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_IpcSetWorkParam(int lUserID, LPHB_SDVR_IPCWORKMODE lpCfg, uint dwDataSize);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_IpcSetWorkParam(int lUserID, LPHB_SDVR_IPCWORKMODE lpCfg, uint dwDataSize);
    // ӳ���б�
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

    // //�����ļ�����־��������ֵ
    // #define HB_SDVR_FILE_SUCCESS				1000	//����ļ���Ϣ
    // #define HB_SDVR_FILE_NOFIND				    1001	//û���ļ�
    // #define HB_SDVR_ISFINDING				    1002	//���ڲ����ļ�
    // #define	HB_SDVR_NOMOREFILE				    1003	//�����ļ�ʱû�и�����ļ�
    // #define	HB_SDVR_FILE_EXCEPTION				1004	//�����ļ�ʱ�쳣
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

    //����������¼�ص�����
    //pLoginCallBack == NULL�ر�������¼��
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_SetInitiativeLoginCallBack(ushort wDVRPort, PInitiativeLoginCallBack pLoginCallBack, IntPtr  pContext);

    //����Ƶ���� ����ģʽ
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_InitiativeRealPlay(int lUserID, uint dwMsgID, LPHB_SDVR_REALPLAYCON pRealPlay);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_InitiativeRealPlay(int lUserID, uint dwMsgID, LPHB_SDVR_REALPLAYCON pRealPlay);

    //Զ��¼��㲥��չ ����ģʽ
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_InitiativePlayBack(int lUserID, uint dwMsgid, LPHB_SDVR_PLAYBACKCON pPlayBack);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_InitiativePlayBack(int lUserID, uint dwMsgid, LPHB_SDVR_PLAYBACKCON pPlayBack);

    //Զ��¼�񱸷���չ ����ģʽ
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_InitiativeGetFile(int lUserID, uint dwMsgid, LPHB_SDVR_FILEGETCOND pGetFile);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_InitiativeGetFile(int lUserID, uint dwMsgid, LPHB_SDVR_FILEGETCOND pGetFile);
    //public delegate void fVoiceDataCallBackDelegate(int lVoiceComHandle, ref string pRecvDataBuffer, uint dwBufSize, byte byAudioFlag, uint dwUser);

    //�����Խ�Э����չ ����ģʽ
    //C++ TO C# CONVERTER NOTE: CALLBACK is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_InitiativeStartVoiceCom(int lUserID, uint dwMsgid, void(CALLBACK *fVoiceDataCallBack)(int lVoiceComHandle, sbyte *pRecvDataBuffer,uint dwBufSize,byte byAudioFlag,uint dwUser), uint dwUser);
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_InitiativeStartVoiceCom(int lUserID, uint dwMsgid, fVoiceDataCallBackDelegate fVoiceDataCallBack, uint dwUser);

    //������ץͼ��չ ����ģʽ
    //C++ TO C# CONVERTER NOTE: __stdcall is not available in C#:
    //ORIGINAL LINE: extern "C" int __stdcall HB_SDVR_InitiativeGetPicFromDev(int lUserID, uint dwMsgid, ushort channel,sbyte *sPicFileName);
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_InitiativeGetPicFromDev(int lUserID, uint dwMsgid, ushort channel, ref string sPicFileName);

    //DDNS����
    //C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
    //int HB_SDVR_TestDDNS(int lUserID);

    //�ʼ�����
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

// ԭ����ӿ�
//#if ! HB_NETSDK_H
//#define HB_NETSDK_H

////#include "../include/HBPlaySDK.h"

//#define HB_SDVR_API extern "C"

// #ifdef __cplusplus
// extern "C" {
// #endif

// �궨��
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

//���ſ�������궨�� HB_SDVR_PlayBackControl,HB_SDVR_PlayControlLocDisplay,HB_SDVR_DecPlayBackCtrl�ĺ궨��
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


//HB_SDVR_GetDVRConfig,HB_SDVR_GetDVRConfig�������
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
//�������128·���� 
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
//�������128·�������͵���չ����
#define HB_SDVR_GET_USERCFG_NVS
#define HB_SDVR_SET_USERCFG_NVS

#define HB_SDVR_GET_EXCEPTIONCFG
#define HB_SDVR_SET_EXCEPTIONCFG
//�����ַ�
#define HB_SDVR_GET_SHOWSTRING
#define HB_SDVR_SET_SHOWSTRING
//GE ODM
#define HB_SDVR_GET_EVENTCOMPCFG
#define HB_SDVR_SET_EVENTCOMPCFG

#define HB_SDVR_GET_FTPCFG
#define HB_SDVR_SET_FTPCFG
#define HB_SDVR_GET_JPEGCFG
#define HB_SDVR_SET_JPEGCFG
//������γѶ
#define HB_SDVR_GET_PPPOECFG
#define HB_SDVR_SET_PPPOECFG
//HS�豸�������
#define HB_SDVR_GET_AUXOUTCFG
#define HB_SDVR_SET_AUXOUTCFG

#define HB_SDVR_GET_PICCFG_EX
#define HB_SDVR_SET_PICCFG_EX

#define HB_SDVR_GET_PICCFG_EX_NVS
#define HB_SDVR_SET_PICCFG_EX_NVS

//SDK_V15 ��չ����
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


//��Ϣ��ʽ
//�쳣����
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

// ����ӿڶ��� 
#define NET_IF_10M_HALF
#define NET_IF_10M_FULL
#define NET_IF_100M_HALF
#define NET_IF_100M_FULL
#define NET_IF_AUTO

//�豸����		
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
//����ʽ
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
//DVR��־
// ���� 
//������
#define MAJOR_ALARM
//������
#define MINOR_ALARM_IN
#define MINOR_ALARM_OUT
#define MINOR_MOTDET_START
#define MINOR_MOTDET_STOP
#define MINOR_HIDE_ALARM_START
#define MINOR_HIDE_ALARM_STOP

// �쳣 
//������
#define MAJOR_EXCEPTION
//������
#define MINOR_VI_LOST
#define MINOR_ILLEGAL_ACCESS
#define MINOR_HD_FULL
#define MINOR_HD_ERROR
#define MINOR_DCD_LOST
#define MINOR_IP_CONFLICT
#define MINOR_NET_BROKEN
// ���� 
//������
#define MAJOR_OPERATION
//������
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
//�����豸�����붨��
//#define NET_DEC_STARTDEC
//#define NET_DEC_STOPDEC
//#define NET_DEC_STOPCYCLE
//#define NET_DEC_CONTINUECYCLE
////////////////////////////////////////////
////
//#define MAX_KEYNUM
///////////////////////////////////
////ptzЭ��
//#define MAXPTZNUM
/////////////////////////////////////////////////////

//////////////////////////////////////////////
//#define MAX_SMTP_HOST
//#define MAX_SMTP_ADDR
//#define MAX_STRING
/////////////////////////////////////////////

//#define PT_ATMI_MAX_ALARM_NUM
//// ������
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

////�����ļ�����־��������ֵ
//#define HB_SDVR_FILE_SUCCESS
//#define HB_SDVR_FILE_NOFIND
//#define HB_SDVR_ISFINDING
//#define HB_SDVR_NOMOREFILE
//#define HB_SDVR_FILE_EXCEPTION
// ��̨��������

// �طſ�������

// ������������

//C++ TO C# CONVERTER TODO TASK: There is no equivalent to most C++ 'pragma' directives in C#:
//#pragma pack(1)
//////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////
// �������ýṹ�嶨��
//Ϊ���������Ҫ��ϸ֡��Ϣ���
public class FRAMEINFO
{
    public int frame_type; //֡���� 1��I֡ 2��P֡ 8����Ƶ֡
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
        public short res1; //����
    } //ʱ��
    public AnonymousClass ets = new AnonymousClass();
    public uint frame_num; //֡��
    public uint width; //ͼ����
    public uint height; //ͼ��߶�
    public short frame_rate; //֡��
    public short reserve1; //����
    public int reserve2; //����
}
//ͨ������
public class HB_SDVR_HANDLEEXCEPTION
{
    public uint dwHandleType; //��λ 2-�������� 5-���������
    public ushort wAlarmOut; //�����������ͨ�� ��λ��Ӧͨ��
}
public class HB_SDVR_HANDLEEXCEPTION_EX
{
    public uint dwHandleType; //��λ 2-�������� 5-��������� 6-�ʼ��ϴ� 7-��Ƶ����
    public byte[] wAlarmOut = new byte[HBConst.MAX_CHANNUM_EX]; //�����������ͨ�� ��λ��Ӧͨ��
}

public class HB_SDVR_VODLOST
{
    public byte[] wVodLost = new byte[HBConst.MAX_CHANNUM_EX]; //��Ӧͨ�� 0����Ƶ��ʧ 1������Ƶ
    public uint dwres; //����
}

//�ϴ�������Ϣ
public class HB_SDVR_ALARMINFO
{
    public byte[] byAlarm = new byte[HBConst.MAX_CHANNUM]; //̽ͷ����
    public byte[] byVlost = new byte[HBConst.MAX_CHANNUM]; //�źŶ�ʧ
    public byte[] byMotion = new byte[HBConst.MAX_CHANNUM]; //�ƶ�����
    public byte[] byHide = new byte[HBConst.MAX_CHANNUM]; //�ڵ�����
    public byte[] byDisk = new byte[HBConst.MAX_DISKNUM]; //Ӳ��״̬
}

public class HB_SDVR_ALARMINFO_EX
{
    public byte[] byAlarm = new byte[HBConst.MAX_CHANNUM_EX]; //̽ͷ���� 0-�ޱ��� 1-�б���
    public byte[] byVlost = new byte[HBConst.MAX_CHANNUM_EX]; //��Ƶ��ʧ ...
    public byte[] byMotion = new byte[HBConst.MAX_CHANNUM_EX]; //�ƶ����� ...
    public byte[] byHide = new byte[HBConst.MAX_CHANNUM_EX]; //�ڵ����� ...
    public byte[] byDisk = new byte[HBConst.MAX_DISKNUM]; //Ӳ��״̬
}

public struct HB_SDVR_TIME
{
    public uint dwYear; //��
    public uint dwMonth; //��
    public uint dwDay; //��
    public uint dwHour; //ʱ
    public uint dwMinute; //��
    public uint dwSecond; //��
}

public struct HB_SDVR_SCHEDTIME
{
    public byte byEnable; //����
                          //��ʼʱ��
    public byte byStartHour;
    public byte byStartMin;
    //����ʱ��
    public byte byStopHour;
    public byte byStopMin;
}

//  ����
public class HB_SDVR_ALARMOUTSTATUS
{
    public byte[] Output = new byte[HBConst.MAX_ALARMOUT];
}

//ͼƬ���� ����
public class HB_SDVR_JPEGPARA
{
    public ushort wPicSize; // 0=CIF, 1=QCIF, 2=D1 
    public ushort wPicQuality; // ͼƬ����ϵ�� 0-��� 1-�Ϻ� 2-һ�� 
}

public struct HB_SDVR_DEVICEINFO
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = HBConst.SERIALNO_LEN)]
    public byte[] sSerialNumber; //����
    public byte byAlarmInPortNum; //DVR�����������
    public byte byAlarmOutPortNum; //DVR�����������
    public byte byDiskNum; //DVR Ӳ�̸���
    public byte byProtocol; //�����Ͳ�Ʒ��ֵ��Ϊ0x20����Э�������
    public byte byChanNum; //DVR ͨ������
    public byte byStartChan; //����
    [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = HBConst.NAME_LEN)]
    public byte[] sDvrName; //������
    [MarshalAs(UnmanagedType.AnsiBStr, SizeConst = HBConst.MAX_CHANNUM * HBConst.NAME_LEN)]
    public byte[,] sChanName; //ͨ������
}


//add by cui for 7024 or nvs 100325
public struct HB_SDVR_DEVICEINFO_EX
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = HBConst.SERIALNO_LEN)]
    public byte[] sSerialNumber; //���кţ������˱��뷵�أ���ǰ���������ಹ��
    public byte byAlarmInPortNum; //DVR�����������
    public byte byAlarmOutPortNum; //DVR�����������
    public byte byDiskNum; //�洢�豸������Ӳ��/SD������
    public byte byProtocol; //Э��汾 0x20
    public byte byChanNum; //DVR ͨ������
    public byte byEncodeType; //���������ʽ��1ΪANSI�ַ��������Ĳ���GB2312���룻2ΪUTF8
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = HBConst.NAME_LEN)]
    public byte[] sDvrName; //�����������ԡ�\0������,�����������ʽ�й�
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = HBConst.MAX_CHANNUM_EX * HBConst.NAME_LEN)]
    public byte[,] sChanName; //ͨ�����ƣ����ԡ�\0������,�����������ʽ�й�
}
//end add

//�豸Ӳ����Ϣ
public class HB_SDVR_DEVICECFG
{
    public uint dwSize;
    public byte[] sDVRName = new byte[HBConst.NAME_LEN]; //DVR����
    public uint dwDVRID; //����
    public uint dwRecycleRecord; //����
                                 //���²��ܸ���
    public byte[] sSerialNumber = new byte[HBConst.SERIALNO_LEN]; //����
    public byte[] sSoftwareVersion = new byte[16]; //����汾��
    public byte[] sSoftwareBuildDate = new byte[16]; //�����������
    public uint dwDSPSoftwareVersion; //DSP����汾
                                      //	BYTE sDSPSoftwareBuildDate[16];		// DSP�����������
    public byte[] sPanelVersion = new byte[16]; // ǰ���汾
    public byte[] sHardwareVersion = new byte[16]; //����
    public byte byAlarmInPortNum; //DVR�����������
    public byte byAlarmOutPortNum; //DVR�����������
    public byte byRS232Num; //����
    public byte byRS485Num; //����
    public byte byNetworkPortNum; //����
    public byte byDiskCtrlNum; //����
    public byte byDiskNum; //DVR Ӳ�̸���
    public byte byDVRType; //DVR����, 1:DVR 2:ATM DVR 3:DVS ����ʹ��HB_SDVR_GetDeviceType
    public byte byChanNum; //DVR ͨ������
    public byte byStartChan; //����
    public byte byDecordChans; //����
    public byte byVGANum; //����
    public byte byUSBNum; //����
    public string reservedData = new string(new char[3]); //����
}

//�豸������Ϣ
public class HB_SDVR_ETHERNET
{
    public string sDVRIP = new string(new char[16]); //DVR IP��ַ
    public string sDVRIPMask = new string(new char[16]); //DVR IP��ַ����
    public uint dwNetInterface; //����ӿ� 1-10MBase-T 2-10MBase-Tȫ˫�� 3-100MBase-TX 4-100Mȫ˫�� 5-10M/100M����Ӧ 6-100M��˫�� 7-1000M��˫��
                                // 8-1000Mȫ˫�� 9-100M/1000M����Ӧ 10-10000M��˫�� 11-10000Mȫ˫�� 12-100M/1000M/10000M����Ӧ(��չ)
    public ushort wDVRPort; //�˿ں�
    public byte[] byMACAddr = new byte[HBConst.MACADDR_LEN]; //�������������ַ
}

//�������ýṹ
public class HB_SDVR_NETCFG
{
    public uint dwSize;
    public HB_SDVR_ETHERNET[] struEtherNet = new HB_SDVR_ETHERNET[HBConst.MAX_ETHERNET]; // ��̫���� 
    public string sManageHostIP = new string(new char[16]); //Զ�̹���������ַ
    public ushort wManageHostPort; //����
    public string sDNSIP = new string(new char[16]); //DNS��������ַ
    public string sMultiCastIP = new string(new char[16]); //�ಥ���ַ
    public string sGatewayIP = new string(new char[16]); //���ص�ַ
    public string sNFSIP = new string(new char[16]); //����
    public byte[] sNFSDirectory = new byte[HBConst.PATHNAME_LEN]; //����
    public uint dwPPPOE; //0-������,1-����
    public byte[] sPPPoEUser = new byte[HBConst.NAME_LEN]; //PPPoE�û���
    public string sPPPoEPassword = new string(new char[HBConst.PASSWD_LEN]); // PPPoE����
    public string sPPPoEIP = new string(new char[16]); //PPPoE IP��ַ
    public ushort wHttpPort; //HTTP�˿ں�
}

public struct HB_SDVR_CLIENTINFO
{
    public int lChannel; //ͨ����
    public int lLinkMode; //���λ(31)Ϊ0��ʾ��������Ϊ1��ʾ��������0��30λ��ʾ�������ӷ�ʽ: 0��TCP��ʽ,1��UDP��ʽ,2���ಥ��ʽ
    public IntPtr hPlayWnd; //���Ŵ��ڵľ��,ΪNULL��ʾ������ͼ��
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
    public char[] sMultiCastIP; //����
}


public class HB_SDVR_CLIENTINFO_EX
{
    public int lChannel; //ͨ����
    public int lLinkMode; //���λ(31)Ϊ0��ʾ��������Ϊ1��ʾ��������0��30λ��ʾ�������ӷ�ʽ: 0��TCP��ʽ,1��UDP��ʽ,2���ಥ��ʽ
    public IntPtr hPlayWnd; //���Ŵ��ڵľ��,ΪNULL��ʾ������ͼ��
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
    public char[] sMultiCastIP; //����
    public uint msgid;
}




public class HB_SDVR_SCHEDULE_VIDEOPARAM
{
    public ushort wStartTime; //��8λ��ʾСʱ ��8λ��ʾ����
    public ushort wEndTime; //��8λ��ʾСʱ ��8λ��ʾ����
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
    NET_DEVTYPE_2600TB, //����ͳ�����ܷ�����
    NET_DEVTYPE_2600TC, //����ʶ�����ܷ�����
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
    public uint dvrtype; // 7004 8004 2004�����ͺ�
    public ushort dwDevice_type; // HB_SDVR_TYPE_E
    public ushort memory_size; // HB_SDVR_MEMSIZE_E
    public uint dwReserve;
}

//// �ƶ�������򣬷�22*18��С���
//#define MOTION_SCOPE_WIDTH
//#define MOTION_SCOPE_HIGHT
//ͨ��ͼ��ṹ
//�ƶ����
public class HB_SDVR_MOTION
{
    public byte[,] byMotionScope = new byte[18, 22]; //�������,����22*18��С���,Ϊ1��ʾ�ú�����ƶ��������,0-��ʾ����
    public byte byMotionSensitive; //�ƶ����������, 0 - 5,Խ��Խ����,0xff�ر�*/
    public byte byEnableHandleMotion; // �Ƿ����ƶ���� */
    public HB_SDVR_HANDLEEXCEPTION struMotionHandleType = new HB_SDVR_HANDLEEXCEPTION(); // ����ʽ */
    public HB_SDVR_SCHEDTIME[,] struAlarmTime = new HB_SDVR_SCHEDTIME[HBConst.MAX_DAYS, HBConst.MAX_TIMESEGMENT]; //����ʱ��
    public byte[] byRelRecordChan = new byte[HBConst.MAX_CHANNUM]; //����������¼��ͨ��,Ϊ1��ʾ������ͨ��
}

//add by cui for 7024 or nvs 100329
public class HB_SDVR_MOTION_EX
{
    public byte[,] byMotionScope = new byte[18, 22]; //�������,����22*18��С���,Ϊ1��ʾ�ú�����ƶ��������,0-��ʾ����
    public byte byMotionSensitive; //�ƶ����������, 0 - 5,Խ��Խ����,0xff�ر�*/
    public byte byEnableHandleMotion; // �Ƿ����ƶ���� */
    public HB_SDVR_HANDLEEXCEPTION_EX struMotionHandleType = new HB_SDVR_HANDLEEXCEPTION_EX(); // ����ʽ */
    public HB_SDVR_SCHEDTIME[,] struAlarmTime = new HB_SDVR_SCHEDTIME[HBConst.MAX_DAYS, HBConst.MAX_TIMESEGMENT]; //����ʱ��
    public byte[] byRelRecordChan = new byte[HBConst.MAX_CHANNUM_EX]; //����������¼��ͨ��,Ϊ1��ʾ������ͨ��
}
//end add

//�ڵ���������Ϊ 
public class HB_SDVR_HIDEALARM
{
    public uint dwEnableHideAlarm; //����
    public ushort wHideAlarmAreaTopLeftX; //����
    public ushort wHideAlarmAreaTopLeftY; //����
    public ushort wHideAlarmAreaWidth; //����
    public ushort wHideAlarmAreaHeight; //����
    public HB_SDVR_HANDLEEXCEPTION struHideAlarmHandleType = new HB_SDVR_HANDLEEXCEPTION(); //����
    public HB_SDVR_SCHEDTIME[,] struAlarmTime = new HB_SDVR_SCHEDTIME[HBConst.MAX_DAYS, HBConst.MAX_TIMESEGMENT]; //����
}

//�ڵ���������Ϊ 
public class HB_SDVR_HIDEALARM_EX
{
    public uint dwEnableHideAlarm; //����
    public ushort wHideAlarmAreaTopLeftX; //����
    public ushort wHideAlarmAreaTopLeftY; //����
    public ushort wHideAlarmAreaWidth; //����
    public ushort wHideAlarmAreaHeight; //����
    public HB_SDVR_HANDLEEXCEPTION_EX struHideAlarmHandleType = new HB_SDVR_HANDLEEXCEPTION_EX(); //����
    public HB_SDVR_SCHEDTIME[,] struAlarmTime = new HB_SDVR_SCHEDTIME[HBConst.MAX_DAYS, HBConst.MAX_TIMESEGMENT]; //����
}

//�źŶ�ʧ����
public class HB_SDVR_VILOST
{
    public byte byEnableHandleVILost; // �Ƿ����źŶ�ʧ����
    public HB_SDVR_HANDLEEXCEPTION strVILostHandleType = new HB_SDVR_HANDLEEXCEPTION(); //����ʽ
    public HB_SDVR_SCHEDTIME[,] struAlarmTime = new HB_SDVR_SCHEDTIME[HBConst.MAX_DAYS, HBConst.MAX_TIMESEGMENT]; //����
}

public class HB_SDVR_VILOST_EX
{
    public byte byEnableHandleVILost; // �Ƿ����źŶ�ʧ����
    public HB_SDVR_HANDLEEXCEPTION_EX strVILostHandleType = new HB_SDVR_HANDLEEXCEPTION_EX(); //����ʽ
    public HB_SDVR_SCHEDTIME[,] struAlarmTime = new HB_SDVR_SCHEDTIME[HBConst.MAX_DAYS, HBConst.MAX_TIMESEGMENT]; //����
}

public class HB_SDVR_SHELTER
{
    public ushort wHideAreaTopLeftX; // �ڸ������x����
    public ushort wHideAreaTopLeftY; // �ڸ������y����
    public ushort wHideAreaWidth; // �ڸ�����Ŀ�
    public ushort wHideAreaHeight; //�ڸ�����ĸ�
}


public class HB_SDVR_PICCFG_EX
{
    public uint dwSize;
    public byte[] sChanName = new byte[HBConst.NAME_LEN]; // ͨ����
    public uint dwVideoFormat; // ����
    public byte byBrightness; // ����
    public byte byContrast; // ����
    public byte bySaturation; // ����
    public byte byHue; // ����
                       //��ʾͨ����
    public uint dwShowChanName; // ����
    public ushort wShowNameTopLeftX; // ͨ��������ʾλ�õ�x����
    public ushort wShowNameTopLeftY; // ͨ��������ʾλ�õ�y����
    public HB_SDVR_VILOST struVILost = new HB_SDVR_VILOST(); // �źŶ�ʧ����
    public HB_SDVR_MOTION struMotion = new HB_SDVR_MOTION(); // �ƶ����
    public HB_SDVR_HIDEALARM struHideAlarm = new HB_SDVR_HIDEALARM(); // ����
    public uint dwEnableHide; // �Ƿ������ڸ� ,0-��,1-��
    public HB_SDVR_SHELTER[] struShelter = new HB_SDVR_SHELTER[HBConst.MAX_SHELTERNUM];
    public uint dwShowOsd; // ����
    public ushort wOSDTopLeftX; // ����
    public ushort wOSDTopLeftY; // ����
    public byte byOSDType; // ����
    public byte byDispWeek; // �Ƿ���ʾ���� 
    public byte byOSDAttrib; //ͨ���� 1-��͸�� 2-͸��

}

//add by cui for 7024 or nvs 100329
public class HB_SDVR_PICCFG_EX_EX
{
    public uint dwSize;
    public byte[] sChanName = new byte[HBConst.NAME_LEN]; // ͨ����
    public uint dwVideoFormat; // ����
    public byte byBrightness; // ����
    public byte byContrast; // ����
    public byte bySaturation; // ����
    public byte byHue; // ����
                       //��ʾͨ����
    public uint dwShowChanName; // ����
    public ushort wShowNameTopLeftX; // ͨ��������ʾλ�õ�x����
    public ushort wShowNameTopLeftY; // ͨ��������ʾλ�õ�y����
    public HB_SDVR_VILOST_EX struVILost = new HB_SDVR_VILOST_EX(); // �źŶ�ʧ����
    public HB_SDVR_MOTION_EX struMotion = new HB_SDVR_MOTION_EX(); // �ƶ����
    public HB_SDVR_HIDEALARM_EX struHideAlarm = new HB_SDVR_HIDEALARM_EX(); // ����
    public uint dwEnableHide; // �Ƿ������ڸ� ,0-��,1-��
    public HB_SDVR_SHELTER[] struShelter = new HB_SDVR_SHELTER[HBConst.MAX_SHELTERNUM];
    public uint dwShowOsd; // ����
    public ushort wOSDTopLeftX; // ����
    public ushort wOSDTopLeftY; // ����
    public byte byOSDType; // ��ʽ�����ԣ����λΪ0��ʾ�������ӣ�Ϊ1��ʾǰ�˵��ӣ�Ĭ��Ϊ0����Ϊ0x80ʱ��ʾ��osd��Ϊǰ�˵���
    public byte byDispWeek; // �Ƿ���ʾ���� 
    public byte byOSDAttrib; //ͨ���� 1-��͸�� 2-͸��

}
//end add

//����ֻ��һ���ڵ�����ͻ��˰汾 20100504
public class HB_SDVR_PICCFG_EX_TMP
{
    public uint dwSize;
    public byte[] sChanName = new byte[HBConst.NAME_LEN]; // ͨ����
    public uint dwVideoFormat; // ����
    public byte byBrightness; // ����
    public byte byContrast; // ����
    public byte bySaturation; // ����
    public byte byHue; // ����
                       //��ʾͨ����
    public uint dwShowChanName; // ����
    public ushort wShowNameTopLeftX; // ͨ��������ʾλ�õ�x����
    public ushort wShowNameTopLeftY; // ͨ��������ʾλ�õ�y����
    public HB_SDVR_VILOST struVILost = new HB_SDVR_VILOST(); // �źŶ�ʧ����
    public HB_SDVR_MOTION struMotion = new HB_SDVR_MOTION(); // �ƶ����
    public HB_SDVR_HIDEALARM struHideAlarm = new HB_SDVR_HIDEALARM(); // ����
    public uint dwEnableHide; // �Ƿ������ڸ� ,0-��,1-��
    public HB_SDVR_SHELTER[] struShelter = new HB_SDVR_SHELTER[1];
    public uint dwShowOsd; // ����
    public ushort wOSDTopLeftX; // ����
    public ushort wOSDTopLeftY; // ����
    public byte byOSDType; // ����
    public byte byDispWeek; // �Ƿ���ʾ���� 
    public byte byOSDAttrib; //ͨ���� 1-��͸�� 2-͸��

}

//����ѹ������
//ѹ������
public class HB_SDVR_COMPRESSION_INFO
{
    public byte byStreamType; //��������0-����Ƶ,1-����Ƶ
    public byte byResolution; //�ֱ��� 0-CIF 1-HD1, 2-D1��Э���:���� 3-QCIF�� 4-720p��5-1080p��6-960H��7-Q960H��8-QQ960H
    public byte byBitrateType; //�������� 0:�����ʣ�1:������ 2��������
    public byte byPicQuality; //ͼ������ 1-��� 2-�κ� 3-�Ϻ� 4-һ��5-�ϲ� 6-��
    public uint dwVideoBitrate; //Э��һ:��Ƶ���� 0-100K 1-128K��2-256K��3-512K��4-1M��5-2M��6-3M��7-4M��8-6M��9-8M��10-12M ,11-�Զ���
                                //Э���:��Ƶ���� 0-100K��1-128K��2-256K��3-512K��4-1M��5-1.5M��6-2M��7-3M��8-4M ����:����ֵ(kbps)��Ч��Χ 30~2^32�����ڵ���32����KΪ��λ
    public uint dwVideoFrameRate; //֡�� 2 �� 25
}

public class HB_SDVR_COMPRESSIONCFG
{
    public uint dwSize;
    public byte byRecordType; //0x0:�ֶ�¼��0x1:��ʱ¼��0x2:�ƶ���⣬0x3:������0x0f:��������
    public HB_SDVR_COMPRESSION_INFO struRecordPara = new HB_SDVR_COMPRESSION_INFO(); //¼��������������
    public HB_SDVR_COMPRESSION_INFO struNetPara = new HB_SDVR_COMPRESSION_INFO(); //����������������
}


//¼�����
public class HB_SDVR_RECORDSCHED
{
    public HB_SDVR_SCHEDTIME struRecordTime = new HB_SDVR_SCHEDTIME();
    public byte byRecordType; //����
    public string reservedData = new string(new char[3]); //����
}

public class HB_SDVR_RECORDDAY
{
    public ushort wAllDayRecord; //����
    public byte byRecordType; //����
    public sbyte reservedData; //����
}

public class HB_SDVR_RECORD
{
    public uint dwSize;
    public uint dwRecord; //�Ƿ�¼�� 0-�� 1-��
    public HB_SDVR_RECORDDAY[] struRecAllDay = new HB_SDVR_RECORDDAY[HBConst.MAX_DAYS]; //����
    public HB_SDVR_RECORDSCHED[,] struRecordSched = new HB_SDVR_RECORDSCHED[HBConst.MAX_DAYS, HBConst.MAX_TIMESEGMENT]; //ʱ���
    public uint dwPreRecordTime; //����
}


//������
public class HB_SDVR_DECODERCFG
{
    public uint dwSize;
    public uint dwBaudRate; // ������(bps)
                            // Э��һ��50 75 110 150 300 600 1200 2400 4800 9600 19200 38400 57600 76800 115.2k 
                            // Э�����0-default,1-2400,2-4800,3-9600,4-19200,5-38400�� �Զ���ȡֵ��Χ[300��115200]
    public byte byDataBit; // ����λ 5 6 7 8
    public byte byStopBit; // ֹͣλ 1 2
    public byte byParity; // У��λ (0-NONE,1-ODD,2-EVEN,3-SPACE)
    public byte byFlowcontrol; // ����(0-��,1-Xon/Xoff,2-Ӳ��)
    public ushort wDecoderType; // ��̨Э��ֵ�������ͨ��HB_SDVR_GetPTZType��ȡ���б�
                                //	 0-unknow 1-RV800  2-TOTA120 3-S1601 4-CLT-168 5-TD-500  6-V1200 7-ZION 8-ANT 9-CBC 10-CS850A 
                                //	 11-CONCORD 12-HD600 13-SAMSUNG 14-YAAN 15-PIH 16-MG-CS160 17-WISDOM 18-PELCOD1 19-PELCOD2 20-PELCOD3 
                                //	 21-PELCOD4 22-PELCOP1 23-PELCOP2 24-PELCOP3 25-Philips 26-NEOCAM  27-ZHCD 28-DongTian 29-PELCOD5 30-PELCOD6
                                //	 31-Emerson 32-TOTA160 33-PELCOP5
    public ushort wDecoderAddress; // ��������ַ:0 - 255
    public byte[] bySetPreset = new byte[HBConst.MAX_PRESET]; // ����
    public byte[] bySetCruise = new byte[HBConst.MAX_PRESET]; // ����
    public byte[] bySetTrack = new byte[HBConst.MAX_PRESET]; // ����
}


//RS232 
public class HB_SDVR_PPPCFG
{
    public string sRemoteIP = new string(new char[16]); //Զ��IP��ַ
    public string sLocalIP = new string(new char[16]); //����IP��ַ
    public string sLocalIPMask = new string(new char[16]); //����IP��ַ����
    public byte[] sUsername = new byte[HBConst.NAME_LEN]; // �û��� 
    public byte[] sPassword = new byte[HBConst.PASSWD_LEN]; // ���� 
    public byte byPPPMode; //PPPģʽ, 0��������1������
    public byte byRedial; //�Ƿ�ز� ��0-��,1-��
    public byte byRedialMode; //�ز�ģʽ,0-�ɲ�����ָ��,1-Ԥ�ûز�����
    public byte byDataEncrypt; //���ݼ���,0-��,1-��
    public uint dwMTU; //MTU
    public string sTelephoneNumber = new string(new char[HBConst.PHONENUMBER_LEN]); //�绰����
}

public class HB_SDVR_RS232CFG
{
    public uint dwSize;
    public uint dwBaudRate; // ������(bps)
    public byte byDataBit; // �����м�λ 5��8
    public byte byStopBit; // ֹͣλ 1-2
    public byte byParity; // У�� 0����У�飬1����У�飬2��żУ��;
    public byte byFlowcontrol; // 0���ޣ�1��������,2-Ӳ����
    public uint dwWorkMode; // ����
    public HB_SDVR_PPPCFG struPPPConfig = new HB_SDVR_PPPCFG(); // ����
}

//��������
public class HB_SDVR_ALARMINCFG
{
    public uint dwSize;
    public byte[] sAlarmInName = new byte[HBConst.NAME_LEN]; // ����ͨ����
    public byte byAlarmType; // ����
    public byte byAlarmInHandle; // �Ƿ��� 0-1
    public HB_SDVR_HANDLEEXCEPTION struAlarmHandleType = new HB_SDVR_HANDLEEXCEPTION(); //����ʽ
    public HB_SDVR_SCHEDTIME[,] struAlarmTime = new HB_SDVR_SCHEDTIME[HBConst.MAX_DAYS, HBConst.MAX_TIMESEGMENT]; //����ʱ��
    public byte[] byRelRecordChan = new byte[HBConst.MAX_CHANNUM]; //����������¼��ͨ��,Ϊ1��ʾ������ͨ��
    public byte[] byEnablePreset = new byte[HBConst.MAX_CHANNUM]; // �Ƿ����Ԥ�õ� ����byEnablePreset[0]���ж�;
    public byte[] byPresetNo = new byte[HBConst.MAX_CHANNUM]; // ���õ���̨Ԥ�õ����,һ������������Ե��ö��ͨ������̨Ԥ�õ�, 0xff��ʾ������Ԥ�õ�
    public byte[] byEnableCruise = new byte[HBConst.MAX_CHANNUM]; // ����
    public byte[] byCruiseNo = new byte[HBConst.MAX_CHANNUM]; // ����
    public byte[] byEnablePtzTrack = new byte[HBConst.MAX_CHANNUM]; // ����
    public byte[] byPTZTrack = new byte[HBConst.MAX_CHANNUM]; // ����
    public byte byRecordTm; // ����¼��ʱ�� 1-99��
}

//add by cui for 7024 or nvs 100325
public class HB_SDVR_ALARMINCFG_EX
{
    public uint dwSize;
    public byte[] sAlarmInName = new byte[HBConst.NAME_LEN]; // ����ͨ����
    public byte byAlarmType; // ����
    public byte byAlarmInHandle; // �Ƿ��� 0-1
    public HB_SDVR_HANDLEEXCEPTION_EX struAlarmHandleType = new HB_SDVR_HANDLEEXCEPTION_EX(); //����ʽ
    public HB_SDVR_SCHEDTIME[,] struAlarmTime = new HB_SDVR_SCHEDTIME[HBConst.MAX_DAYS, HBConst.MAX_TIMESEGMENT]; //����ʱ��
    public byte[] byRelRecordChan = new byte[HBConst.MAX_CHANNUM_EX]; //����������¼��ͨ��,Ϊ1��ʾ������ͨ��
    public byte[] byEnablePreset = new byte[HBConst.MAX_CHANNUM_EX]; // �Ƿ����Ԥ�õ� ����byEnablePreset[0]���ж�;
    public byte[] byPresetNo = new byte[HBConst.MAX_CHANNUM_EX]; // ���õ���̨Ԥ�õ����,һ������������Ե��ö��ͨ������̨Ԥ�õ�, 0xff��ʾ������Ԥ�õ�
    public byte[] byEnableCruise = new byte[HBConst.MAX_CHANNUM_EX]; // ����
    public byte[] byCruiseNo = new byte[HBConst.MAX_CHANNUM_EX]; // ����
    public byte[] byEnablePtzTrack = new byte[HBConst.MAX_CHANNUM_EX]; // ����
    public byte[] byPTZTrack = new byte[HBConst.MAX_CHANNUM_EX]; // ����
    public byte byRecordTm; // ����¼��ʱ�� 1-99��
}
//end add

//DVR�������
public class HB_SDVR_ALARMOUTCFG
{
    public uint dwSize;
    public byte[] sAlarmOutName = new byte[HBConst.NAME_LEN]; // ����
    public uint dwAlarmOutDelay; // �������ʱ�� ��λ��
    public byte byEnSchedule; // �����������ʱ�伤�� 0-���� 1-����
    public HB_SDVR_SCHEDTIME[,] struAlarmOutTime = new HB_SDVR_SCHEDTIME[HBConst.MAX_DAYS, HBConst.MAX_TIMESEGMENT]; // �����������ʱ���
}


//�û�Ȩ��
public class HB_SDVR_USER_INFO
{
    public byte[] sUserName = new byte[HBConst.NAME_LEN]; // �û��� 
    public byte[] sPassword = new byte[HBConst.PASSWD_LEN]; // ���� 
    public uint[] dwLocalRight = new uint[HBConst.MAX_RIGHT]; // ����Ȩ�� 
    public uint[] dwRemoteRight = new uint[HBConst.MAX_RIGHT]; // Զ��Ȩ�� 
                                                               //���� 0: ͨ��Ȩ��
                                                               //���� 1: ��ʾ����
                                                               //���� 2: ¼�����
                                                               //���� 3: ��ʱ¼��
                                                               //���� 4: �ƶ�¼��
                                                               //���� 5: ����¼��
                                                               //���� 6: �������
                                                               //���� 7: ��̨����
                                                               //���� 8: �洢����
                                                               //���� 9: ϵͳ����
                                                               //���� 10: ��Ϣ��ѯ
                                                               //���� 11: �ֶ�¼��
                                                               //���� 12: �ط�
                                                               //���� 13: ����
                                                               //���� 14: ��Ƶ����
                                                               //���� 15: �������
                                                               //���� 16: Զ��Ԥ��
    public string sUserIP = new string(new char[16]); // �û�IP��ַ(Ϊ0ʱ��ʾ�����κε�ַ) 
    public byte[] byMACAddr = new byte[HBConst.MACADDR_LEN]; // �����ַ 
}

public class HB_SDVR_USER_INFO_EX
{
    public byte[] sUserName = new byte[HBConst.NAME_LEN]; // �û��� 
    public byte[] sPassword = new byte[HBConst.PASSWD_LEN]; // ���� 
    public byte[] dwLocalRight = new byte[HBConst.MAX_RIGHT]; //����Ȩ�� 1.����0δʹ�ã�2.ȡֵ��0-��Ȩ�ޣ�1-��Ȩ��
                                                              //���� 1: ��������
                                                              //���� 2: ¼������
                                                              //���� 3: �������
                                                              //���� 4: ��������
                                                              //���� 5: ��������
                                                              //���� 6: ��������
                                                              //���� 7: ¼��ط�
                                                              //���� 8: ϵͳ����
                                                              //���� 9: ϵͳ��Ϣ
                                                              //���� 10: �������
                                                              //���� 11: ��̨����
                                                              //���� 12: �ػ�����
                                                              //���� 13: USB����
                                                              //���� 14������
    public byte[] LocalChannel = new byte[HBConst.MAX_CHANNUM_EX]; //�����û���ͨ���Ĳ���Ȩ�ޣ����128��ͨ����0-��Ȩ�ޣ�1-��Ȩ��
    public byte[] dwRemoteRight = new byte[HBConst.MAX_RIGHT]; //Զ�̵�½�û����߱���Ȩ�� 1.����0δʹ�ã�2.ȡֵ��0-��Ȩ�ޣ�1-��Ȩ��
                                                               //���� 1: Զ��Ԥ��
                                                               //���� 2: ��������
                                                               //���� 3: Զ�̻ط�
                                                               //���� 4: Զ�̱���
                                                               //���� 5: �鿴��־
                                                               //���� 6: �����Խ�
                                                               //���� 7: Զ������
                                                               //���� 8��Զ������
    public byte[] RemoteChannel = new byte[HBConst.MAX_CHANNUM_EX]; // Զ��ͨ��Ȩ��
    public string sUserIP = new string(new char[16]); // �û�IP��ַ(Ϊ0ʱ��ʾ�����κε�ַ) 
    public byte[] byMACAddr = new byte[HBConst.MACADDR_LEN]; // �����ַ 
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
    public string sDNSUser = new string(new char[HBConst.INFO_LEN]); // DNS�˺�
    public string sDNSPassword = new string(new char[HBConst.INFO_LEN]); // DNS����
    public string[] sDNSAddress = new string[HBConst.INFO_SEQ]; // DNS������ַ
    public byte sDNSALoginddress; //DNS������ַ��sDNSAddress�����е�ָ��������ַ������
    public byte sDNSAutoCon; //DNS�Զ�����
    public byte sDNSState; //DNS��½ 0-ע�� 1-��½
    public byte sDNSSave; //DNS��Ϣ����
    public ushort sDNServer; // 0-- hanbang.org.cn 1--oray.net 2--dyndns.com
    public ushort reserve; //1--��������,0--������
}
public class HB_SDVR_DNS_EX
{
    public uint dwSize;
    public string sDNSUser = new string(new char[HBConst.INFO_LEN]); // DNS�˺�
    public string sDNSPassword = new string(new char[HBConst.INFO_LEN]); // DNS����
    public string[] sDNSAddress = new string[HBConst.INFO_SEQ]; // DNS������ַ
    public byte sDNSALoginddress; //DNS������ַ��sDNSAddress�����е�ָ��������ַ������
    public byte sDNSAutoCon; //DNS�Զ�����
    public byte sDNSState; //DNS��½ 0-ע�� 1-��½
    public byte sDNSSave; //DNS��Ϣ����
    public ushort sDNServer; // 0-- hanbang.org.cn 1--oray.net 2--dyndns.com
    public ushort reserve; //1--��������,0--������
    public byte[] sDNSname = new byte[128]; //����������
}

//PPPoE
public class HB_SDVR_PPPoE
{
    public uint dwSize;
    public byte[] sPPPoEUser = new byte[HBConst.INFO_LEN]; //PPPoE�û���
    public string sPPPoEPassword = new string(new char[HBConst.INFO_LEN]); // PPPoE����
    public byte sPPPoEAutoCon; //PPPoE�Զ�����
    public byte sPPPoEState; //PPPoE��½ 0-ע�� 1-��½
    public byte sPPPoESave; //DNS��Ϣ����
    public sbyte reservedData;
}

//ƽ̨��Ϣ
public class HB_SDVR_SERVERCFG
{
    public string sServerIP = new string(new char[16]); //���������IP��ַ
    public uint nPort; //����������˿ں�
    public string puId = new string(new char[HBConst.NAME_LEN]); //�豸ע��ID��
    public uint nInternetIp; // ��������IP
    public uint nVideoPort; //��Ƶ�˿�
    public uint nTalkPort; //�Խ��˿�
    public uint nCmdPort; //����˿�
    public uint nVodPort; //�㲥�˿�
    public uint tran_mode; // 1 ������ 0 ������
                           // ��������
    public uint ftp_mode; // ��FTP��ʽ�������Ĵ洢 1 ���� 0 �ر�
    public uint max_link; // ��������� 0 - 32
}

public struct HB_SDVR_FIND_DATA
{
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
    public string sFileName; //�ļ���
    public HB_SDVR_TIME struStartTime; //�ļ��Ŀ�ʼʱ��
    public HB_SDVR_TIME struStopTime; //�ļ��Ľ���ʱ��
    public uint dwFileSize; //�ļ��Ĵ�С
    public byte nCh; //ͨ����
    public byte nType; //¼������
} //cwh 20080730


public struct HB_SDVR_CHANNELSTATE
{
    public byte byRecordStatic; //ͨ���Ƿ���¼��,0-��¼��,1-¼��
    public byte bySignalStatic; //���ӵ��ź�״̬,0-����,1-�źŶ�ʧ
    public byte byHardwareStatic; //����
    public sbyte reservedData;
    public uint dwBitRate; //ʵ������
    public uint dwLinkNum; //�ͻ������ӵĸ���
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = HBConst.MAX_LINK)]
    public uint[] dwClientIP; //����
}

public struct HB_SDVR_DISKSTATE_ST
{
    public uint dwVolume; //Ӳ�̵�������MB��
    public uint dwFreeSpace; //Ӳ�̵�ʣ��ռ䣨MB��
    public uint dwHardDiskStatic; //Ӳ��״̬��dwVolume��ֵʱ��Ч�� 0-���� 1-���̴��� 2-�ļ�ϵͳ����
}

public struct HB_SDVR_WORKSTATE
{
    public uint dwDeviceStatic; //����
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = HBConst.MAX_DISKNUM)]
    public HB_SDVR_DISKSTATE_ST[] struHardDiskStatic; //Ӳ��״̬
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = HBConst.MAX_CHANNUM)]
    public HB_SDVR_CHANNELSTATE[] struChanStatic; //ͨ����״̬	
    public uint byAlarmInStatic; //�����˿ڵ�״̬ ��λ��ʾ
    public uint byAlarmOutStatic; //��������˿ڵ�״̬ ��λ��ʾ
    public uint dwLocalDisplay; //����
}

//add by cui for 7024 or nvs 100325
public struct HB_SDVR_WORKSTATE_EX
{
    public uint dwDeviceStatic; //����
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = HBConst.MAX_DISKNUM)]
    public HB_SDVR_DISKSTATE_ST[] struHardDiskStatic; //Ӳ��״̬
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = HBConst.MAX_CHANNUM_EX)]
    public HB_SDVR_CHANNELSTATE[] struChanStatic; //ͨ����״̬
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = HBConst.MAX_ALARMIN_EX)]
    public uint[] byAlarmInStatic; //�����˿ڵ�״̬ ��λ��ʾ
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = HBConst.MAX_ALARMOUT_EX)]
    public uint[] byAlarmOutStatic; //��������˿ڵ�״̬ ��λ��ʾ
    public uint dwLocalDisplay; //����
}
//end add


public class HB_SDVR_PRESETPOLL
{
    public uint byChannel; //����ͨ��
    public ushort[] Preset = new ushort[HBConst.PRESETNUM];
    public ushort PresetPoll; //��Ԥ�õ���Ѳ������رձ�ʾ
    public ushort presettime; //��Ԥ�õ���Ѳʱ��
}

//Ԥ������
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
    public int lChannel; //ͨ����
    public int lLinkMode; //���λ(31)Ϊ0��ʾ��������Ϊ1��ʾ��������0��30λ��ʾ�������ӷ�ʽ:0��TCP��ʽ,1��UDP��ʽ,2���ಥ��ʽ,3 - RTP��ʽ��4-�绰�ߣ�5��128k�����6��256k�����7��384k�����8��512k�����
    public string sMultiCastIP;
    public HB_SDVR_DISPLAY_PARA struDisplayPara = new HB_SDVR_DISPLAY_PARA();
}
//2005-08-01
// �����豸͸��ͨ������ 
public class HB_SDVR_PORTINFO
{
    public uint dwEnableTransPort; // �Ƿ�����͸��ͨ�� 0�������� 1������
    public string sDecoderIP = new string(new char[16]); // DVR IP��ַ 
    public ushort wDecoderPort; // �˿ں� 
    public ushort wDVRTransPort; // ����ǰ��DVR�Ǵ�485/232�����1��ʾ485����,2��ʾ232���� 
    public string cReserve = new string(new char[4]);
}

//���ӵ�ͨ������
public class HB_SDVR_DECCHANINFO
{
    public string sDVRIP = new string(new char[16]); // DVR IP��ַ 
    public ushort wDVRPort; // �˿ں� 
    public byte[] sUserName = new byte[HBConst.NAME_LEN]; // �û��� 
    public byte[] sPassword = new byte[HBConst.PASSWD_LEN]; // ���� 
    public byte byChannel; // ͨ���� 
    public byte byLinkMode; // ����ģʽ 
    public byte byLinkType; // �������� 0�������� 1�������� 
}

//ÿ������ͨ��������
public class HB_SDVR_DECINFO
{
    public byte byPoolChans; //ÿ·����ͨ���ϵ�ѭ��ͨ������, ���4ͨ��
    public HB_SDVR_DECCHANINFO[] struchanConInfo = new HB_SDVR_DECCHANINFO[HBConst.MAX_DECPOOLNUM];
    public byte byEnablePoll; //�Ƿ���Ѳ 0-�� 1-��
    public byte byPoolTime; //��Ѳʱ�� 0-���� 1-10�� 2-15�� 3-20�� 4-30�� 5-45�� 6-1���� 7-2���� 8-5���� 
}

//�����豸��������
public class HB_SDVR_DECCFG
{
    public uint dwSize;
    public uint dwDecChanNum; //����ͨ��������
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
    public byte[] DeviceName = new byte[24]; //�豸����
    public uint dwChannelNumer; //ͨ����
    public byte[] CardNumber = new byte[32]; //����
    public string cTradeType = new string(new char[12]); //��������
    public uint dwCash; //���׽��
}

//����ΪATMר��
//֡��ʽ
public class HB_SDVR_FRAMETYPECODE
{
    public byte[] code = new byte[12]; // ���� 
}

public class HB_SDVR_FRAMEFORMAT
{
    public uint dwSize;
    public string sATMIP = new string(new char[16]); // ATM IP��ַ 
    public uint dwATMType; // ATM���� 
    public uint dwInputMode; // ���뷽ʽ 
    public uint dwFrameSignBeginPos; // ���ı�־λ����ʼλ��
    public uint dwFrameSignLength; // ���ı�־λ�ĳ��� 
    public byte[] byFrameSignContent = new byte[12]; // ���ı�־λ������ 
    public uint dwCardLengthInfoBeginPos; // ���ų�����Ϣ����ʼλ�� 
    public uint dwCardLengthInfoLength; // ���ų�����Ϣ�ĳ��� 
    public uint dwCardNumberInfoBeginPos; // ������Ϣ����ʼλ�� 
    public uint dwCardNumberInfoLength; // ������Ϣ�ĳ��� 
    public uint dwBusinessTypeBeginPos; // �������͵���ʼλ�� 
    public uint dwBusinessTypeLength; // �������͵ĳ��� 
    public HB_SDVR_FRAMETYPECODE[] frameTypeCode = new HB_SDVR_FRAMETYPECODE[10]; // ���� 
}

//�人���� 2005-06-29
public class HB_SDVR_FIND_PICTURE
{
    public string sFileName = new string(new char[100]); //ͼƬ��
    public HB_SDVR_TIME struTime = new HB_SDVR_TIME(); //ͼƬ��ʱ��
    public uint dwFileSize; //ͼƬ�Ĵ�С
    public string sCardNum = new string(new char[32]); //����
}



// ���������ļ��ط� 
public class HB_SDVR_PLAYREMOTEFILE
{
    public uint dwSize;
    public string sDecoderIP = new string(new char[16]); // DVR IP��ַ 
    public ushort wDecoderPort; // �˿ں� 
    public ushort wLoadMode; // �ط�����ģʽ 1�������� 2����ʱ�� 
                             //C++ TO C# CONVERTER TODO TASK: Unions are not supported in C#.
                             //ORIGINAL LINE: union
                             //C++ TO C# CONVERTER NOTE: Structs must be named in C#, so the following struct has been named AnonymousStruct:
    public struct AnonymousStruct
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
        public byte[] byFile; // �طŵ��ļ��� 
                              //C++ TO C# CONVERTER NOTE: Classes must be named in C#, so the following class has been named AnonymousClass2:
        public class AnonymousClass2
        {
            public uint dwChannel;
            public byte[] sUserName = new byte[HBConst.NAME_LEN]; //������Ƶ�û���
            public byte[] sPassword = new byte[HBConst.PASSWD_LEN]; // ���� 
            public HB_SDVR_TIME struStartTime = new HB_SDVR_TIME(); // ��ʱ��طŵĿ�ʼʱ�� 
            public HB_SDVR_TIME struStopTime = new HB_SDVR_TIME(); // ��ʱ��طŵĽ���ʱ�� 
        }
    }
    public AnonymousStruct mode_size = new AnonymousStruct();
}

//��ǰ�豸��������״̬
public class HB_SDVR_DECCHANSTATUS
{
    public uint dwWorkType; //������ʽ��1����Ѳ��2����̬���ӽ��롢3�����ļ��ط� 4����ʱ��ط�
    public string sDVRIP = new string(new char[16]); //���ӵ��豸ip
    public ushort wDVRPort; //���Ӷ˿ں�
    public byte byChannel; // ͨ���� 
    public byte byLinkMode; // ����ģʽ 
    public uint dwLinkType; //�������� 0�������� 1��������
                            //C++ TO C# CONVERTER TODO TASK: Unions are not supported in C#.
                            //ORIGINAL LINE: union
                            //C++ TO C# CONVERTER NOTE: Structs must be named in C#, so the following struct has been named AnonymousStruct2:
    public struct AnonymousStruct2
    {
        //C++ TO C# CONVERTER NOTE: Classes must be named in C#, so the following class has been named AnonymousClass3:
        public class AnonymousClass3
        {
            public byte[] sUserName = new byte[HBConst.NAME_LEN]; //������Ƶ�û���
            public byte[] sPassword = new byte[HBConst.PASSWD_LEN]; // ���� 
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
            public byte[] sUserName = new byte[HBConst.NAME_LEN]; //������Ƶ�û���
            public byte[] sPassword = new byte[HBConst.PASSWD_LEN]; // ���� 
            public HB_SDVR_TIME struStartTime = new HB_SDVR_TIME(); // ��ʱ��طŵĿ�ʼʱ�� 
            public HB_SDVR_TIME struStopTime = new HB_SDVR_TIME(); // ��ʱ��طŵĽ���ʱ�� 
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
    public HB_SDVR_PORTINFO[] struTransPortInfo = new HB_SDVR_PORTINFO[HBConst.MAX_TRANSPARENTNUM]; // ����0��ʾ485 ����1��ʾ232 
}

//�Զ�����̨Э��
public class STRUCT_SDVR_DECODERCUSTOMIZE
{
    public byte CheckSum; //Ч����λ��
    public byte PortNum; //��ַ��λ��
    public byte PreSet; //Ԥ�Ƶ�λ��
    public byte CheckSumMode; //Ч�������ģʽ
    public byte KeyLen; //�볤��
    public byte KeyNum; //������
    public byte[,] CommandKey = new byte[HBConst.MAX_KEYNUM, 24];
}


public class STRUCT_SDVR_PTZTYPE
{
    public int ptznum;
    public string[] ptztype = new string[HBConst.MAXPTZNUM];
}

public class STRUCT_ALARMIN_WAIT
{
    public ushort enable; //ʹ��
    public ushort time; //�ӳ�ʱ��
}


//add by cui for ipc 
public class STRUCT_SDVR_IPCCONFIG
{
    public byte bVideoOut; //��Ƶ���
    public byte bTemperature; //�¶�̽��
    public byte bVoltage; //��ѹ̽��
    public byte bSubStream; //������
    public uint Rserve1; //����
    public uint Rserve2; //����

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
    public byte safeoption; // ��ȫѡ�����ã�ȡֵ��Χ[0,2] 0:�Զ�ѡ�� 1������ϵͳ 2��������Կ
    public byte pswformat; // ��Կ��ʽ���ã�ȡֵ��Χ[0,1] 0��16���� 1��ASCII��
    public byte pswtype; // �� Կ �� �����ã�ȡֵ��Χ[0,3] 0������ 1��64λ 2:128λ 3:152λ
    public byte[] pswword = new byte[62]; // ���룬�ԡ�\0����β������62byte��Ϊ����STRUCT_SDVR_IPCWPAPSK�ȴ�С��
                                          //����ע�����볤��˵����ѡ��64λ��Կ������16�������ַ�10��������ASCII���ַ�5����ѡ��128λ��Կ��
                                          // ����16�������ַ�26��������ASCII���ַ�13����ѡ��152λ��Կ������16�������ַ�32��������ASCII
                                          // ���ַ�16������
    public byte[] reserve = new byte[3]; //����
}

public class STRUCT_SDVR_IPCWPAPSK
{
    public byte safeoption; // ��ȫѡ�����ã�ȡֵ��Χ[0,2] 0���Զ�ѡ�� 1��WPA-PSK 2:WPA2-PSK
    public byte pswmod; // ���ܷ�������,ȡֵ��Χ[0,2] 0���Զ�ѡ�� 1��TKIP 2:AES
    public byte[] pawword = new byte[64]; // psk���룬8��63���ַ����ԡ�\0����β
    public byte[] reserve = new byte[2]; // ����
}

public class STRUCT_SDVR_IPCWIRELESS
{
    public uint nSecCmdParaSize; // ������ӣ��ṹ�峤�ȡ�
    public byte[] ssid = new byte[50]; // SSID���ԡ�\0����β
    public byte[] wirelessIP = new byte[16]; // ����ip�ԡ�\0����β
    public byte safetype; // ��ȫ�������ã�ȡֵ��Χ[0,2] 0��WEB��1��WPA-PSK/WPA2-PSK��2���޼���
    public byte[] reserve = new byte[3]; // ����
                                         //C++ TO C# CONVERTER TODO TASK: Unions are not supported in C#, but the following union can be simulated with the StructLayout and FieldOffset attributes.
                                         //ORIGINAL LINE: union
                                         //C++ TO C# CONVERTER NOTE: Structs must be named in C#, so the following struct has been named AnonymousStruct3:
                                         //[StructLayout(LayoutKind.Explicit)]
                                         //public struct AnonymousStruct3
                                         //{ // ��Ϊ���������ṹ�岻����ͬʱʹ�ã������������塣
                                         //    [FieldOffset(0)]
                                         //    public STRUCT_SDVR_IPCWEP ipcwep = new STRUCT_SDVR_IPCWEP(); // ��ȫ����ΪWEPʱ�����ṹ��
                                         //    [FieldOffset(0)]
                                         //    public STRUCT_SDVR_IPCWPAPSK ipcwpapsk = new STRUCT_SDVR_IPCWPAPSK(); // ��ȫ����ΪWPA-PSK/WPA2-PSKʱ�����ṹ��
                                         //}
                                         //public AnonymousStruct3 u = new AnonymousStruct3();
}


public class STRUCT_SDVR_IFRAMERATE
{
    public byte channel; //ͨ����
    public short iframerate; //֡���
    public byte Reserve; //����

}
public delegate void PHB_SDVR_STREAMDATA_PROC(int lRealHandle, uint dwDataType, ref byte pBuffer, uint dwBufSize, uint dwUser);
public struct HB_SDVR_FILEGETCOND
{
    public uint dwSize;
    public uint dwChannel; // ͨ����
    public HB_SDVR_RECTYPE_E dwFileType; // �ļ�����
    public HB_SDVR_TIME struStartTime; // ����ʱ��ο�ʼʱ��
    public HB_SDVR_TIME struStopTime; // ����ʱ��
    public PHB_SDVR_STREAMDATA_PROC pfnDataProc;
    public IntPtr pContext;
    public string pSaveFilePath;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public uint[] dwResever;
}



public class HB_SDVR_NTPCONFIG
{
    public string server = new string(new char[32]); // ������
    public uint port; // �˿�
    public uint auto_enbale; // ����ntp����,0-��ʾ�ֶ�,1-��ʾ�Զ�
    public uint server_index; // ������������
    public uint sync_period; // ͬ�����ڣ�
    public uint sync_unit; // ͬ�����ڣ�0-���� 1-Сʱ 2-�� 3-���� 4-��
    public uint sync_time; //����
    public uint time_zone; //ʱ��
    public uint reserve; // ����
}

public class HB_SDVR_SMTPCONFIG
{
    public string host = new string(new char[HBConst.MAX_SMTP_HOST]); //�����ʼ���SMTP�����������磺126�������smtp.126.com
    public uint port; // �������˿ڣ������ʼ�(SMTP)�˿ڣ�Ĭ��ֵ25
    public string user = new string(new char[32]); // �ʼ��û���
    public string pwd = new string(new char[32]); // �ʼ��û�����
    public string send_addr = new string(new char[HBConst.MAX_SMTP_HOST]); // FROM���ʼ���ַ
    public string recv_addr = new string(new char[HBConst.MAX_SMTP_ADDR]); // TO���ʼ���ַ������Ƕ���ʼ���ַ����';'����
    public uint send_period; // �ϴ�����,��λ(����)
    public uint snap_enable; // �Ƿ�ץ���ϴ�
    public string reserve = new string(new char[HBConst.MAX_STRING]);
}

public class HB_SDVR_POLLCONFIG
{
    public uint poll_type; //��ѵ���ͣ�0����ѵ��1��spot��Ѳ
    public uint enable; // ���ã�0-���ã�1-����
    public uint period; // ��ѵ�������λ��
    public uint format; // �����ʽ��0-0ff, 1-1, 2-2, 4-2x2, 9-3x3, 16-4x4
    public byte[] ch_list = new byte[HBConst.MAX_CHANNUM];
}

public class HB_SDVR_POLLCONFIG128
{
    public uint poll_type; //��ѵ���ͣ�0����ѵ��1��spot��Ѳ
    public uint enable; // ���ã�0-���ã�1-����
    public uint period; // ��ѵ�������λ��
    public uint format; // �����ʽ��0-0ff, 1-1, 2-2, 4-2x2, 9-3x3, 16-4x4
    public byte[] ch_list = new byte[HBConst.MAX_CHANNUM_EX];
}

public class HB_SDVR_VIDEOMATRIX
{
    public byte[] matrix_channel = new byte[HBConst.MAX_CHANNUM]; // ��Ƶ�����Ӧͨ�� ��1��ʼ��0xff��ʾ�ر�
    public byte[] reserve = new byte[32]; // ����λ
}

public class HB_SDVR_VIDEOMATRIX128
{
    public byte[] matrix_channel = new byte[HBConst.MAX_CHANNUM_EX]; // ��Ƶ�����Ӧͨ�� ��1��ʼ��0xff��ʾ�ر�
    public byte[] reserve = new byte[32]; // ����λ
}


// ϵͳʱ�䶨��
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
//// ����
////
//*******************************************************************
//һ���������
public class PT_ATMI_POINT_S
{
    public ushort x; //������
    public ushort y; //������
}

// ����ͳ�Ʋ���
public class PT_HDC_CTRLPARAM_ST
{
    public uint dwWidth; // ������Ƶ�Ŀ�ȣ�Ĭ��ֵ352
    public uint dwHeight; // ������Ƶ�߶ȣ�Ĭ��ֵ288
    public uint objWidth; // ����Ŀ��Ŀ�ȣ���λΪ���أ�Ĭ��ֵ55
    public PT_ATMI_POINT_S ptStart = new PT_ATMI_POINT_S(); // �������㣬Ĭ��ֵ��5,216)
    public PT_ATMI_POINT_S ptEnd = new PT_ATMI_POINT_S(); // ������յ㣬Ĭ��ֵ(347,216)
    public PT_ATMI_POINT_S ptDirection = new PT_ATMI_POINT_S(); // ����ߵķ���Ĭ��ֵ(290, 205)
    public uint dwPassFrames; // ��ʼ���ĵ�Ŀ���ںϳ�ͼ�еĸ߶ȣ���Ŀ��ͨ������ߵ�֡����Ĭ��ֵ15
    public uint dwMutiObjWidth; // ��������Ŀ�겢��ʱ���ο�Ŀ�ȣ�Ĭ��ֵ110
    public uint dwMutiObjWidthEdge; // ��dwMutiObjWidth�йأ�dwMutiObjWidthEdge = ��dwMutiObjWidth / 2 - 5��/ 2��Ĭ��ֵ25
    public uint dwThreshBackDiff; // �����ֵ��Ĭ��ֵ50���Ƚ�����
    public uint dwThreshFrameDiff; // ֡��ֵ��Ĭ��ֵ20
    public byte byStartPtLabel; // ������ǣ�0��ʾ�κ�Ŀ���������1��ʾС�ڷ�ֵ��Ŀ�겻������Ĭ��ֵΪ0
    public byte byEndPtLable; // �յ����ǣ�0��ʾ�κ�Ŀ���������1��ʾС�ڷ�ֵ��Ŀ�겻������Ĭ��ֵΪ0
    public byte[] byReserve = new byte[2]; // �����ֶ�
    public uint dwHalfObjW; // ��ֵ����ǰ������أ����С�ڸ÷�ֵ��������Ĭ��ֵΪ20
}

//****************************************************************
//
//����Ϊ��·���ܵı���ͨ�Žṹ
//
//****************************************************************
//��������
public class PT_ATMI_RECT
{
    public int left; //����������
    public int top; //����������
    public int right; //����������
    public int bottom; //����������
}

//�������ͼ�λ����Ϣ
public class PT_ATMI_ALARM_POSITION_S
{
    public int alarm_type; //����,GUI_ATMI_ALARM_TYPE_E
    public PT_ATMI_RECT position = new PT_ATMI_RECT(); //����λ��
}

// 1.����ͨ�������ṹ��
public class PT_ATMI_FACE_ALARM_S
{
    public int alarm_num; //��������
    public PT_ATMI_ALARM_POSITION_S[] alarm_area = new PT_ATMI_ALARM_POSITION_S[10]; //��������ֵ,һ����alarm_num���������ȫΪ0
}

// 2.���ͨ�������ṹ��
public class PT_ATMI_PANEL_ALARM_S
{
    public int alarm_num; //��������
    public PT_ATMI_ALARM_POSITION_S[] alarm_area = new PT_ATMI_ALARM_POSITION_S[10]; //��������ֵ,һ����alarm_num���������ȫΪ0
}

// 3.�ӳ����������Ϣ
public class PT_ATMI_MONEY_ALARM_S
{
    public int type; //�Ƿ����˴��룬0��ʾ�ޣ�1��ʾ��
}

// 4.���������ṹ��,alarm_num����Ӧ��������ǰ��track_num����Ӧ�����������alarm_num�����
public class PT_ATMI_ENVI_ALARM_S
{
    public int alarm_num; //����Ŀ������
    public int track_num; //����Ŀ������
    public PT_ATMI_ALARM_POSITION_S[] envi_alarm_region = new PT_ATMI_ALARM_POSITION_S[25];
}

public enum PT_ATMI_ALARM_TYPE_E : int
{
    PT_ATMI_FACE_BLOCK = 0, //�����ڵ�
    PT_ATMI_FACE_NOSIGNAL, //����ͨ����Ƶ��ʧ
    PT_ATMI_FACE_UNUSUAL, //�����쳣
    PT_ATMI_FACE_NORMAL, //��������
    PT_ATMI_PANEL_BLOCK = 40, //����ڵ�
    PT_ATMI_PANEL_NOSIGNAL, //���ͨ����Ƶ��ʧ
    PT_ATMI_PANEL_PASTE, //����
    PT_ATMI_PANEL_KEYBOARD, //װ�ټ���
    PT_ATMI_PANEL_KEYMASK, //�ƻ����������
    PT_ATMI_PANEL_CARDREADER, //�ƻ�������
    PT_ATMI_PANEL_TMIEOUT, //��ʱ����
    PT_ATMI_ENTER, //���˽���
    PT_ATMI_EXIT, //�˳���
    PT_ATMI_MONEY_BLOCK = 80, //�ӳ�����Ƶ�ڵ�
    PT_ATMI_MONEY_NOSIGNAL, //�ӳ���ͨ����Ƶ��ʧ
    PT_ATMI_MONEY_UNUSUAL, //�ӳ����쳣,�����˴���ӳ���
    PT_ATMI_ENVI_BLOCK = 120, //����ͨ����Ƶ�ڵ�
    PT_ATMI_ENVI_NOSIGNAL, //����ͨ����Ƶ��ʧ
    PT_ATMI_ENVI_GATHER, //���˾ۼ�
    PT_ATMI_ENVI_MANYPEOPLEINREGION, //Υ��ȡ��
    PT_ATMI_ENVI_LINGERING, //��Ա�ǻ�
    PT_ATMI_ENVI_LIEDOWN, //��Ա����
    PT_ATMI_ENVI_FORBIDREGION, //�Ƿ����뾯����
    PT_ATMI_ENVI_LEAVEOBJECT, //��Ʒ����
}

//����ͼƬ�ṹ��
public class PT_ATMI_ALARM_PICINFO_S
{
    public uint pic_alarm_type; // PT_ATMI_ALARM_TYPE_E;
    public uint pic_format; // ͼƬ��ʽCIF:0 D1:1
    public uint pic_size;
}

public enum PT_HDCCOUNT_DIRECTION_E : int
{
    HBGK_HDCCOUNT_DIR1 = 0, //���Ƿ�����ͬ
    HBGK_HDCCOUNT_DIR2 //���Ƿ����෴
}

public class PT_HDC_RESULT_ST
{
    public uint dwResultType; // ������������
    public uint dwSubType; // �����������ͣ���ʾ��Ա����ͳ�Ƶķ����PT_HDCCOUNT_DIRECTION_E
    public uint dwTrackNum; // ��ǰ���ͳ�Ƶ�ID���(��ͳ������)
    public PT_ATMI_RECT rcPos = new PT_ATMI_RECT(); // ��ǰ�����ŵ���Ӿ��ο�
}

//����ʱ���������ͻ��˵��ܽṹ��
public class PT_ATMI_ALARM_INFO_S
{
    // int chn;    //ͨ����,ÿ�α����󣬸���ͨ���ţ�ȥ��ȡ�����ĸ��ṹ���ж�Ӧ����һ��
    public byte byChn;
    public byte byReserve1;
    public byte byInfoType; // �ϴ���Ϣ����
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
    //    public PT_ATMI_FACE_ALARM_S atmi_face_alarm = new PT_ATMI_FACE_ALARM_S(); // 1.����ͨ�������ṹ��
    //    [FieldOffset(0)]
    //    public PT_ATMI_PANEL_ALARM_S atmi_panel_alarm = new PT_ATMI_PANEL_ALARM_S(); // 2.���ͨ�������ṹ��
    //    [FieldOffset(0)]
    //    public PT_ATMI_MONEY_ALARM_S atmi_money_alarm = new PT_ATMI_MONEY_ALARM_S(); // 3.�ӳ���ͨ�������ṹ��
    //    [FieldOffset(0)]
    //    public PT_ATMI_ENVI_ALARM_S atmi_envi_alarm = new PT_ATMI_ENVI_ALARM_S(); // 4.����ͨ�������ṹ��
    //    [FieldOffset(0)]
    //    public PT_HDC_RESULT_ST hdc = new PT_HDC_RESULT_ST();
    //}
    //public AnonymousStruct4 info = new AnonymousStruct4();

    public PT_ATMI_ALARM_PICINFO_S alarm_picinfo = new PT_ATMI_ALARM_PICINFO_S();
    public ASYSTIME alarmtime = new ASYSTIME(); //����ʱ��
}
//****************************************************************
//
//����Ϊ��·���ܵ����û��ȡ�Ľ���ṹ
//
//****************************************************************
//����α�ʾ�ṹ�壬����������
public class PT_ATMI_POLYGON_S
{
    public PT_ATMI_POINT_S[] point = new PT_ATMI_POINT_S[10]; //����ζ�������
    public int point_num; //��ĸ���
    public int region_type; //��������
}

//�������򣬴���������
public class PT_ATMI_RECT_S
{
    public PT_ATMI_RECT region = new PT_ATMI_RECT(); //������������
    public int region_type; //��������
}

//��������Ȥ�����Լ��������������Ĵ�С
public class PT_ATMI_FACE_ROI_S
{
    public PT_ATMI_RECT_S roi = new PT_ATMI_RECT_S(); //����
    public int min_face; //��С�ߴ�
    public int max_face; //���ߴ�
}

// 1.����ͨ���������õ�����
public class PT_ATMI_FACEROI_ALL_S
{
    public int num;
    public PT_ATMI_FACE_ROI_S[] face_roi = new PT_ATMI_FACE_ROI_S[10];
}

// 2.���ͨ���������õ�����
public class PT_ATMI_PANEL_REGION_S
{
    public int num;
    public PT_ATMI_POLYGON_S[] atmi_panel_region = new PT_ATMI_POLYGON_S[20];
}

// 3.�ӳ���ͨ���������õ����򼰲���
public class PT_ATMI_DISTRICTPARA_S
{
    public PT_ATMI_POLYGON_S pol_proc_region = new PT_ATMI_POLYGON_S(); // ��������Ĭ��4���㣬����ȫͼ
    public PT_ATMI_RECT_S[] no_process = new PT_ATMI_RECT_S[10]; // ����������
    public int no_process_num; // ������������� (0)
    public int warn_interval; // ���α���ʱ������(100 ��)
}

// 4.����ͨ���������õ�����
public class PT_ATMI_SCENE_COMMONPARA_S
{
    public PT_ATMI_POLYGON_S pol_proc_region = new PT_ATMI_POLYGON_S(); // ͼ���еĴ������򣬶���α�ʾ

    //����ATM��ǰβ��ȡ����Ĳ�������ʶATMǰ��վ��������
    public PT_ATMI_POLYGON_S[] tail_region = new PT_ATMI_POLYGON_S[10]; // Region in polygon.
    public int tail_num; // Region number. default: 0

    //���ڽ�ֹ������뱨������ʶѡ���Ľ�ֹ���������
    public PT_ATMI_POLYGON_S[] forbid_region = new PT_ATMI_POLYGON_S[10]; // Region in polygon.
    public int forbid_num; // Region number. default: 0

    public PT_ATMI_POLYGON_S obj_height = new PT_ATMI_POLYGON_S(); // Ŀ�꣨�ˣ���ͼ���еĸ߶ȣ�Ĭ��85
}

// 5.����ͨ�����õĲ���,������֡Ϊ��λ�ģ������ڽ�������Ϊ�룬Ȼ�����ڲ���ת��Ϊ֡��
public class PT_ATMI_SCENE_WARN_PARAM_S
{
    //��Ʒ�����㷨��ز���
    public int objlv_frames_th; // ��Ʒ����ʱ����ֵ(֡) (30)

    //��Ա�ǻ��㷨��ز���
    public int mv_block_cnt; // �ƶ�����(20��0��ʾ�˹�����Ч)
    public short mv_stay_frames; // �����г���ʱ����ֵ(֡),������ʱ��(0��ʾ�˹�����Ч)
    public short mv_stay_valid_frames; // ATM����ͣ��ʱ����ֵ(֡),ATM����ǰͣ��ʱ��(200, 0��ʾ�˹�����Ч)

    //���˾ۼ��㷨��ز���
    public short gather_cnt; // ���ۼ�����(4)
    public short gather_interval_frames; // �������ʱ��(֡)(1000 frames,Լ100��)
    public int gather_frame_cnt; // ���˾ۼ�ʱ����ֵ(֡) (100)

    //��Ա�����㷨��ز���
    public int liedown_frame_cnt; // ����ʱ����ֵ(֡).(20 frames)

    //β��ȡ���㷨��ز���
    public short after_frame_cnt; // ������Ϊʱ����ֵ(֡)(20 frames)
    public short after_interval_frames; // �������ʱ��(֡)(1000 frames,Լ100��)

    //��ֹ�����㷨��ز���
    public short forbid_frame_cnt; // ��ֹվ��ʱ����ֵ(֡)(20 frames)
    public short reserve; // ����
}

// 1.����ͨ�����ýṹ��
public class PT_ATMI_SET_FACE_S
{
    public short face_unusual; //�Ƿ���쳣�����������֡�����ȣ���⹦�ܣ�1 Ϊ�򿪣�0 Ϊ�رա�Ĭ��Ϊ 0
    public short output_oneface; //��������ֻ���һ�����0Ϊ��1Ϊ�ǣ�Ĭ��Ϊ1
    public int fd_rate; //�������������ټ��
    public PT_ATMI_FACEROI_ALL_S face_roi = new PT_ATMI_FACEROI_ALL_S(); //����ͨ����������������
    public int abnormalface_alarmtime; //��������ʱ�䷧ֵ���룩
}

//���澯�����ṹ��
public class STRUCT_SDVR_PANELALARMCFG
{
    public int AlphaVal; //����alphaֵ(5)
    public int BetaVal; //����Betaֵ(3)
    public int MetinThVal; //ǰ���ڱ�����ֵ(4500)
    public int LBTWTriggerVal; //ȡ������������ֵ(75)

    public int AppearCntThdVal; //�������ֱ�������(40)
    public int AppearCntTriggerVal; //�������ֱ�����ֵ(40)
    public int LBTWCntThdVal; //ȡ��������������(75)
    public int LBTWCntTriggerVal; //ȡ������������ֵ(75)

    public int PanelTimeOutTriggerVal; //��ʱ������ֵ(1500)

    public int OpenLightTriggerVal; //���仯������ֵ(45)
    public int CloseLightTriggerVal; //���仯������ֵ(55)

    public uint AppearMinWidth; //����������СĿ����(10)
    public uint AppearMinHeight; //����������СĿ��߶�(10)
    public uint AppearMaxWidth; //�����������Ŀ����(200)
    public uint AppearMaxHeight; //�����������Ŀ��߶�(200)

    public uint LBTWMinWidth; //ȡ��������СĿ����(10)
    public uint LBTWMinHeight; //ȡ��������СĿ��߶�(10)
    public uint LBTWMaxWidth; //ȡ���������Ŀ����(200)
    public uint LBTWMaxHeight; //ȡ���������Ŀ��߶�(200)
}


// 2.���ͨ�����ýṹ��
public class PT_ATMI_SET_PANEL_S
{
    public int timeout_enable; //��ʱʱ��
    public PT_ATMI_PANEL_REGION_S panel_region = new PT_ATMI_PANEL_REGION_S(); //���ͨ��������������
    public STRUCT_SDVR_PANELALARMCFG panel_alarm_param = new STRUCT_SDVR_PANELALARMCFG(); //���ͨ���������ò���
}

// 3.�ӳ���ͨ�����ýṹ��
public class PT_ATMI_SET_MONEY_S
{
    public PT_ATMI_DISTRICTPARA_S money_para = new PT_ATMI_DISTRICTPARA_S(); //�ӳ���ͨ��������������
}

// 4.����ͨ�����ýṹ��
public class PT_ATMI_SET_ENVI_S
{
    public PT_ATMI_SCENE_WARN_PARAM_S envi_para = new PT_ATMI_SCENE_WARN_PARAM_S(); //����ͨ������
    public PT_ATMI_SCENE_COMMONPARA_S envi_region = new PT_ATMI_SCENE_COMMONPARA_S(); //����ͨ������
}

//�ͻ������û��ȡ��������·�����ܵĽṹ��
public class STRUCT_SDVR_INTELLECTCFG
{
    // int chn;                                      //ͨ����
    public byte byChn; // ͨ����
    public byte byReserve1; // ����
    public byte bySetInfoType; // ���ò������ͣ�
                               // 0, PT_ATMI_SET_FACE_S;
                               // 1, PT_ATMI_SET_PANEL_S;
                               // 2, PT_ATMI_SET_MONEY_S;
                               // 3, PT_ATMI_SET_ENVI_S;
                               // 4, PT_HDC_CTRLPARAM;
    public byte byReserve2; // ����

    public int chn_attri; // ͨ������(��������塢�ӳ�������)��Ŀǰδ�ã���ֹ�Ժ���
    public short channel_enable; // ͨ�����أ�0�رգ�1��
    public short if_pic; // �Ƿ���ҪץȡͼƬ
    public short enc_type; // ץȡͼƬ�ĸ�ʽ
    public short email_linkage; // ����email
    public uint sensor_num; // ̽ͷ���,λ��ʾ
    public uint rec_linkage; // ����¼��λ��ʾ

    //C++ TO C# CONVERTER TODO TASK: Unions are not supported in C#, but the following union can be simulated with the StructLayout and FieldOffset attributes.
    //ORIGINAL LINE: union
    //C++ TO C# CONVERTER NOTE: Structs must be named in C#, so the following struct has been named AnonymousStruct5:
    //    [StructLayout(LayoutKind.Explicit)]
    //    public struct AnonymousStruct5
    //    {
    //        [FieldOffset(0)]
    //        public PT_ATMI_SET_FACE_S face_set_para = new PT_ATMI_SET_FACE_S(); // ����ͨ�����ýṹ��
    //        [FieldOffset(0)]
    //        public PT_ATMI_SET_PANEL_S panel_set_para = new PT_ATMI_SET_PANEL_S(); // ���ͨ�����ýṹ��
    //        [FieldOffset(0)]
    //        public PT_ATMI_SET_MONEY_S money_set_para = new PT_ATMI_SET_MONEY_S(); // �ӳ���ͨ�����ýṹ��
    //        [FieldOffset(0)]
    //        public PT_ATMI_SET_ENVI_S envi_set_para = new PT_ATMI_SET_ENVI_S(); // ����ͨ�����ýṹ��
    //        [FieldOffset(0)]
    //        public PT_HDC_CTRLPARAM_ST hdc = new PT_HDC_CTRLPARAM_ST(); // ����ͳ�Ʋ�������
    //    }
    //    public AnonymousStruct5 setInfo = new AnonymousStruct5();
}

public enum HB_SDVR_RECTYPE_E
{
    HB_SDVR_RECMANUAL = 1, // �ֶ�¼��
    HB_SDVR_RECSCHEDULE = 2, // ��ʱ¼��
    HB_SDVR_RECMOTION = 4, // �ƶ����¼��
    HB_SDVR_RECALARM = 8, // ����¼��
    HB_SDVR_REC_ALL = 0xff, // ����¼��
}

public struct ST_HB_SDVR_FILEGETCOND
{
    public uint dwSize;
    public uint dwChannel; // ͨ����
    public HB_SDVR_RECTYPE_E dwFileType; // �ļ�����
    public HB_SDVR_TIME struStartTime; // ����ʱ��ο�ʼʱ��
    public HB_SDVR_TIME struStopTime; // ����ʱ��
    public IntPtr pfnDataProc;
    public IntPtr pContext;
    public string pSaveFilePath;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public uint dwResever;
}


// add 2010/08/04
//
//���ܣ����÷�����״̬
//    ������lUserID����HB_SDVR_Login����
//����ֵ��
//    �ɹ�������TRUE
//    ʧ�ܣ�����FALSE
//
// 2010/08/04 ���÷���������
public class _STRUCT_BUZZER_CTRL
{
    public int ctrl; //1.�� 0.��
}

public class MFS_Field_Time
{
    //C++ TO C# CONVERTER TODO TASK: C# does not allow bit fields:
    public uint second; // ��: 0~59
                        //C++ TO C# CONVERTER TODO TASK: C# does not allow bit fields:
    public uint minute; // ��: 0~59
                        //C++ TO C# CONVERTER TODO TASK: C# does not allow bit fields:
    public uint hour; // ʱ: 0~23
                      //C++ TO C# CONVERTER TODO TASK: C# does not allow bit fields:
    public uint day; // ��: 1~31
                     //C++ TO C# CONVERTER TODO TASK: C# does not allow bit fields:
    public uint month; // ��: 1~12
                       //C++ TO C# CONVERTER TODO TASK: C# does not allow bit fields:
    public uint year; // ��: 2000~2063
}

// ��������
//#define HB_MAX_AM_COUNT

public class HB_SDVR_REAL_DEFENCE
{
    public ushort enable; //0-������1-����
    public ushort time; //������ʱʱ�䣨�˱�����ʱ���ã�
    public uint[] reserver = new uint[4]; // ����
}

public class HB_SDVR_ALMINFO
{
    public ushort alm_ch; //����ͨ��
    public ushort alm_type; //��������0-�ޱ���1-̽ͷ����2-�ƶ����3-��Ƶ��ʧ
    public MFS_Field_Time time = new MFS_Field_Time(); //��������ʱ���
}

public class HB_SDVR_DISCONN_ALMSTAT
{
    public uint alm_count; //�����ڼ䱨������
    public HB_SDVR_ALMINFO[] alminfo = new HB_SDVR_ALMINFO[HBConst.HB_MAX_AM_COUNT]; //ÿ�α����������Ϣ��ֻ�洢���µ�16�α���
    public uint[] reserver = new uint[4]; // ����
}

// 2011/09/21 Tϵ�л��������ڵ��������õ�����/��ȡ
// �����:
//#define NET_SDVR_GET_VCOVER_DETECT
//#define NET_SDVR_SET_VCOVER_DETECT

// ������ṹ
public class HB_SDVR_VCOVER_ALM_ST
{
    public byte byChannel; // ͨ���� [0,n]
    public uint dwVcover_Enable; // �ڵ�����ʹ�ܣ� 1-���ã�0-�����ã�
    public uint dwSensor_Out; // ������������� ��λ��ʾ�� 1-������0-��������
}

// 2011/09/21 IPC��ؽӿڱ䶯
// 1.���߲������� ������ṹ�䶯
// 2.����ģʽ����ȥ����ʹ�ù��������滻������ű��Ϊ0xB0/0xB1
// 3.ԭ0xB0-0xB6����

// �����
//#define NET_SDVR_IPCWORKPARAM_GET
//#define NET_SDVR_IPCWORKPARAM_SET

public class HB_SDVR_REQIPCWORKPARAM_ST
{
    public byte cbStreamType; // �������� 0-������ 1-������ 2-��������
    public byte[] cbReserve = new byte[3];
}

// ������ṹ
// typedef struct	{
//     BYTE	byEnable;  		// ����ʱ��ʹ��  0-�رգ�1-����	
//     BYTE	byStartHour;  	//��ʼСʱ 0-23
//     BYTE	byStartMin;  		//��ʼ���� 0-59
//     BYTE	byStopHour; 		//����Сʱ  0-23
//     BYTE	byStopMin;  		//��������  0-59
// }STRUCT_SDVR_SCHEDTIME; 


public class HB_SDVR_SCHEDTIME_V2_ST
{
    public byte byStartHour; //��ʼСʱ 0-23
    public byte byStartMin; //��ʼ���� 0-59
    public byte byStopHour; //����Сʱ 0-23
    public byte byStopMin; //�������� 0-59
}

public class HB_SDVR_ICRTIME_ST
{
    public ushort wLightRange; //ICR�����л��ٽ�ֵ��ȡֵ��Χ0 < wLightRange< 2^16;
    public ushort wEnable; // 0--����ֵ��Ч 1--ʱ�����Ч
    public HB_SDVR_SCHEDTIME_V2_ST[] stSchedTime = new HB_SDVR_SCHEDTIME_V2_ST[2];
}

public class HB_SDVR_SHUTTERVAL_ST
{
    public uint dwShutterIndex; //��ǰ����ʱ������ֵ,��ʾ����dwShutterVal�е�λ�ã�����dwShutterIndex = 2��
                                //��ǰ����ʱ��ΪdwShutterVal[2];
    public uint[] dwShutterVal = new uint[32]; //��ȡ����ʱ���֧�ֲ����б�,��dwShutterVal[1]=0,��ʾ��һ��ȡֵ��Χ����
                                               //dwShutterVal[0]= 4096,���ʾȡֵ��ΧΪ��1/[1,4096*2],��dwShutterVal[1] != 0ʱ��
                                               //��ʾ�ж�����������ֵ�����磺dwShutterVal[0]= 2048��dwShutterVal[1]=4096����dwShutterVal[x]=0���ʾ���ݹ���x����
}

public class HB_SDVR_SCENEVAL_ST
{
    public uint dwSceneIndex; //��ǰ��ͷ����ֵ����ʾ����cbSceneVal�е�λ�ã�����dwSceneIndex = 2��
                              //��ǰ��ͷΪ��cbSceneVal[2] = ��JCD661 lens������cbSceneVal[x] =��\0����ʾ�ܹ���
                              //x�������
    public byte[,] cbSceneVal = new byte[8, 32]; //�û���֧�ֵľ�ͷ����,//0 - Full Maual lens,1 - DC Iris lens, //2 - JCD661 lens,
                                                 //3 - Ricom NL3XZD lens,4 - Tamron 18X lens��
}

public class HB_SDVR_RESOLUTION_ST
{
    public uint dwResoluIndex; //��ǰ�ֱ�������ֵ����ʾ����dwResolution�е�λ�ã�����dwResoluIndex= 2��
                               //��ǰ�ֱ���ΪdwResolution[2]����ָ���ķֱ���
    public uint[] dwResolution = new uint[16]; //�û���֧�ֵķֱ��ʣ���DWORD����ʾ֧�ֵķֱ��ʣ����磺
                                               //dwResolution[0]=0x07800438�������ֽڣ�0x0780=1920���������ֽڣ�0x0438=1080����
}

public class HB_SDVR_AGCVAL_ST
{
    public uint dwAgcIndex; //��ǰAGC������ֵ����ʾ����cbAgcVal�е�λ�ã�����cbAgcVal =2�����ʾAGC
                            //ֵΪcbAgcVal[2]�е�ֵ��
    public byte[] cbAgcVal = new byte[32]; //AGC���Զ����棩��֧�ֲ����б�,��cbAgcVal[1]= 0ʱ��ʾcbAgcVal[0]�д洢����
                                           //һ��ȡֵ��Χ����cbAgcVal[0]= 128,���ʾȡֵ��ΧΪ��[1,128],��cbAgcVal[1]!=0
                                           //ʱ�����ʾcbAgcVal�����д洢���Ǿ����ֵ������ cbAgcVal[0]= 32��//cbAgcVal[1]=64�ȣ���cbAgcVal [x] =0��ʾ�ܹ���x�������
}

public class HB_SDVR_FRAMERATE_ST
{
    public byte cbMinFrameRate; //�û���֧�ֵ���С����֡��ֵ;��ȡֵ��ΧΪ��1��2^8����ͬ��
    public byte cbMaxFrameRate; //�û���֧�ֵ�������֡��ֵ;
    public byte cbCurFrameRate; //�û������õĵ�ǰ����֡��ֵ;
    public byte cbreserve; //����
}

public class HB_SDVR_IPCWORKMODE_ST
{
    public uint dwLength; //�ṹ�峤��
    public byte cbStreamEnable; //�Ƿ�����ǰ����
    public byte cbStreamType; //�������� 0-����1-���� 2-��������
    public byte cbAudioEnable; //��Ƶʹ�� 0-����Ƶ ,1-����Ƶ
    public byte cbAntiFlicker; //����˸���� 0-60HZ 1-50HZ
    public HB_SDVR_FRAMERATE_ST stFrameRate = new HB_SDVR_FRAMERATE_ST(); //����֡������;
    public HB_SDVR_SHUTTERVAL_ST stShutterVal = new HB_SDVR_SHUTTERVAL_ST(); //������ز�����ȡ
    public HB_SDVR_SCENEVAL_ST stSceneVal = new HB_SDVR_SCENEVAL_ST(); //��ͷ��ز�����ȡ
    public HB_SDVR_RESOLUTION_ST stResolution = new HB_SDVR_RESOLUTION_ST(); //���������
    public HB_SDVR_AGCVAL_ST stAgcVal = new HB_SDVR_AGCVAL_ST(); //Agc���
    public uint dwBitRateVal; //��Ƶ���� 0-100K 1-128K��2-256K��3-512K��4-1M��5-1.5M��6-2M��7-3M, 8-4M
                              //9-5M��10-6M��11-7M��12-8M, 13-9M��14-10M��15-11 M��16-12M
                              //����������ֵ��kbps����Ч��Χ 32~2^32,���ڵ���32����KΪ��λ��
    public byte cbFoucusSpeedVal; //��ѧ�佹�ٶ�ֵ��0����֧��
    public byte cbDigitalFoucusVal; //���ֱ佹ֵ��0����֧��
    public byte cbImageTurnVal; //��ǰͼ��ת���� //1-����ת,2-ˮƽ��ת 3-��ֱ��ת, 4-ˮƽ&��ֱ,0-��֧��
    public byte cbBlackWhiteCtrlVal; //��ǰ�ڰ�ģʽ���� //1- Off, 2- On, 3�CAuto, 0-��֧��
    public byte cbIRISCtrl; //Iris control mode ��Ȧ����ģʽ���ã�1-Off,2-Basic, 3-Advanced,0-��֧��
    public byte cbAutoFoucusVal; //�Ƿ�֧���Զ��Խ���//0-��֧��,1-֧��
    public byte cbAWBVal; //��ƽ�ⳡ��ģʽ���ã�1-auto_wide, 2-auto_normal, 3-sunny, 4-shadow, 5-indoor,
                          //6-lamp, 7-FL1, 8-FL2,0-��֧��
    public byte cbA3Ctrl; //3A����0-off; 1-Auto Exposure; 2-Auto White Balance; 3-both, (Auto Focus no support)
    public HB_SDVR_ICRTIME_ST stICRTime = new HB_SDVR_ICRTIME_ST(); //ircut(�˹�Ƭ�л�ģʽ����)����ģʽ4ʱ�����û�ȡ��ֵ
    public byte cbFNRSuppVal; //��ǰ֡�������ã�1-��,2-��,0-��֧��
    public byte cbStreamKindVal; //��ǰ�������ͣ�1-������,2-������
    public byte cbVideoOutKindVal; //vout��Ƶ������ã�0-disable, 1-CVBS, 2-HDMI,3-YPbPr�ȵ�
    public byte cbWDRVal; //�û����Ƿ�֧�ֿ�̬����,0-��֧��,1-֧��
    public byte cbColorMode; //ɫ�ʷ������ 0-TV 1--PC
    public byte cbSharpNess; //������ã�ȡֵ��ΧΪ��[1,255]
    public byte cbPlatformType; //ƽ̨���
    public byte[] cbReserve = new byte[17]; //����
}

// 2011/09/22 NVR����Э��
// �����
//nvr����28��Э��
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


//��������������(ʵʱ��Ƶ���طš�����)
//#define NET_SDVR_STREAM_TYPE_NVR       0xC0  
public class HB_STREAM_TYPE
{
    public byte[] sBuff = new byte[64]; // �ļ�ͷ����
}


//��ȡ�����豸����״̬
//#define NET_SDVR_WORK_STATE_EX          0xC1 
// typedef struct	
// {
//     DWORD dwVolume;				//Ӳ�̵�������MB��
//     DWORD dwFreeSpace;			//Ӳ�̵�ʣ��ռ䣨MB��
//     DWORD dwHardDiskStatic;		//Ӳ��״̬��dwVolume��ֵʱ��Ч�� 0-���� 1-���̴��� 2-�ļ�ϵͳ����
// } STRUCT_SDVR_DISKSTATE;

public class HB_SDVR_CHANNELSTATE_EX
{
    public byte byRecordStatic; //ͨ���Ƿ���¼��,0-��¼��,1-¼��
    public byte bySignalStatic; //���ӵ��ź�״̬,0-����,1-�źŶ�ʧ
    public byte byHardwareStatic; //����
    public byte byLinkNum; //�ͻ������ӵĸ�����ͬһͨ����ǰʱ���ʵʱ��������������������������ͬһIP���������������
    public uint dwBitRate; //ʵ������
}

public class HB_SDVR_WORKSTATE_V2
{
    public uint dwSize; //�ṹ���С
    public HB_SDVR_DISKSTATE_ST[] struHardDiskStatic = new HB_SDVR_DISKSTATE_ST[16]; //Ӳ��״̬
    public HB_SDVR_CHANNELSTATE_EX[] struChanStatic = new HB_SDVR_CHANNELSTATE_EX[128]; //ͨ����״̬
    public byte[] byAlarminStatusLocal = new byte[128]; //���ر�������˿ڵ�״̬
    public byte[] byAlarmoutStatusLocal = new byte[128]; //���ر�������˿ڵ�״̬
    public uint[] dwReserve = new uint[4]; //����
}


//��ȡ��ͨ��ʵʱ��Ƶ�����ӵĿͻ���ip�б�
//#define NET_SDVR_GET_CH_CLIENT_IP       0xC2
public class HB_CLIENT_IP_INFO
{
    public uint dwSize; //�ṹ���С
    public byte byChannel; //ͨ����[0, n-1],n:ͨ����
    public byte byClientIpNum; //��ͨ�����ӿͻ���IP����
    public byte[] byReserve1 = new byte[2]; //����
    public uint[] dwDwClientIP = new uint[64]; //�ͻ���IP�б�
    public uint[] dwReserve2 = new uint[4]; //����
}


//��־����ṹ��
//#define NET_SDVR_LOG_QUERY_EX           0xC3
public class HB_REQLOG_EX
{
    public ushort wYear; //��: 2000~2063
    public byte byMonth; //��: 1~12
    public byte byDay; //��: 1~31
    public ushort wStart; //��ѯ�ӵڼ�����ʼ��һ��Ϊ0��
    public ushort wnum; //һ�β�ѯ���������Ϊ100��
    public byte byPriType; //������ ������չ���У�
    public byte bySecType; //������
    public byte[] byReserve = new byte[6]; //����
}

public class HB_SDVR_LOGINFO_EX
{
    public uint dwSize; //�ṹ���С
    public uint dwTotalLogNum; //��־������
    public uint dwCurLogNum; //���β鵽������
    public uint dwStart; //���β�ѯ������־����ʼ���
    public byte byEncType; //�����ʽ 1- UTF-8 2-gb2312
    public byte[] byReserve = new byte[3]; //����
    public byte[,] sSigalLogData = new byte[100, 128]; // ��־��Ϣ (ÿ�β�ѯ���֧��100����־����־����100�� // ��Ҫ��ε��ã�ÿ��128�ֽڣ�ÿ���ԡ�\0������)
}

public enum LOG_PRI_TYPE : int //������
{
    LOG_PRI_ALL = -1, //ȫ��
    LOG_PRI_ERROR, // �쳣
    LOG_PRI_ALARM, // ����
    LOG_PRI_OPERATE, // ����
    LOG_PRI_MAX
}

public enum LOG_OPERATE_TYPE : int //����������
{
    LOG_OP_ALL = -1,
    LOG_OP_POWERON, // ����
    LOG_OP_SHUTDOWN, // �ػ�
    LOG_OP_EXC_SHUTDOWN, //�쳣�ػ�
    LOG_OP_REBOOT, // ����
    LOG_OP_LOGIN, // ��½
    LOG_OP_LOGOUT, // ע��
    LOG_OP_SETCONFIG, // ����
    LOG_OP_FRONTEND_SETCONFIG, //ǰ���豸����
    LOG_OP_UPGRADE, // ����
    LOG_OP_FRONTEND_UPGRADE, //ǰ���豸����
    LOG_OP_RECORD_START, // ���������ֶ�¼��
    LOG_OP_RECORD_STOP, // ����ֹͣ�ֶ�¼��
    LOG_OP_PTZ, // ��̨����
    LOG_OP_MANUAL_ALARM, //�����ֶ�����
    LOG_OP_FORMAT_DISK, // ��ʽ��Ӳ��
    LOG_OP_FILE_PLAYBACK, // ���ػط��ļ�
    LOG_OP_EXPORT_CONFIGFILE, //�������������ļ�
    LOG_OP_LMPORT_CONFIGFILE, //���뱾�������ļ�
    LOG_OP_FREXPORT_CONFIGFILE, //����ǰ���豸�����ļ�
    LOG_OP_FRLMPORT_CONFIGFILE, //����ǰ���豸�����ļ�
    LOG_OP_BACKUP_REC, //���ر���¼���ļ�
    LOG_OP_DEFAULT, //���ػָ�ȱʡ
    LOG_OP_SETTIME, // ��������ϵͳʱ��
    LOG_OP_TRANSCOM_OPEN, // ����͸��ͨ��
    LOG_OP_TRANSCOM_CLOSE, // �Ͽ�͸��ͨ��
    LOG_OP_TALKBACK_START, // �Խ���ʼ
    LOG_OP_TALKBACK_STOP, // �Խ�����

    LOG_OP_TYPE_NONE, // ��¼��
    LOG_OP_TYPE_MANUAL, // �ֶ�¼��
    LOG_OP_TYPE_TIME, // ��ʱ¼��
    LOG_OP_TYPE_MOTION, // �ƶ�¼��
    LOG_OP_TYPE_SENSOR, // ̽ͷ����
    LOG_OP_TYPE_MOTION_OR_SENSOR, // �ƶ��򱨾�¼��
    LOG_OP_TYPE_MOTION_AND_SENSOR, // �ƶ���̽ͷ����
    LOG_OP_REMOTE_LOGIN, // ǰ���豸��½
    LOG_OP_REMOTE_LOGOUT, // ǰ���豸ע��

    LOG_OP_TYPE_MAX
}

public enum LOG_ALARM_TYPE : int //����������
{
    LOG_ALM_ALL = -1,
    LOG_ALM_LOCAL_SENSORIN, // ���ر�������
    LOG_ALM_LOCAL_SENSOROUT, // ���ر������
    LOG_ALM_FRONTEND_SENSORIN, //ǰ���豸��������
    LOG_ALM_FRONTEND_SENSOROUT, //ǰ���豸�������
    LOG_ALM_MOTION_START, // �ƶ���⿪ʼ
    LOG_ALM_MOTION_STOP, // �ƶ�������
    LOG_ALM_MAIL_UPLOAD, // �ʼ��ϴ�
    LOG_ALM_TYPE_MAX
}

public enum LOG_ERROR_TYPE : int //���������
{
    LOG_ERR_ALL = -1,
    LOG_ERR_VIDEOLOST, // ��Ƶ��ʧ
    LOG_ERR_HD_ERROR, // ���̴���
    LOG_ERR_HD_FULL, // ������
    LOG_ERR_LOGIN_FAIL, // ��½ʧ��
    LOG_ERR_TEMP_HI, // �¶ȹ���
    LOG_ERR_HD_PFILE_INDEX, // ��������������
    LOG_ERR_HD_DEV_FATAL, // �����豸��������
    LOG_ERR_IP_CONFLICT, //ip��ͻ
    LOG_ERR_NET_EXCEPTION, //�����쳣
    LOG_ERR_REC_EXCEPTION, //¼���쳣
    LOG_ERR_FRONTEND_EXCEPTION, //ǰ���豸�쳣
    LOG_ERR_TIME_EXCEPTION, //ʱ���쳣
    LOG_ERR_FRONTBOARD_EXCEPTION, //ǰ����쳣
    LOG_ERR_TYPE_MAX
}

//����nvr͸��ͨ��
//#define NET_SDVR_SERIAL_START_NVR       0xC4
//�ر�NVR͸��ͨ��
//#define NET_SDVR_SERIAL_STOP_NVR        0xC5  
public class HB_NVR_SERIAL_START
{
    public byte byOpType; //0-�����DVR,���ñ��أ���1-ǰ���豸������ (Ϊ1ʱbyChannel��Ч)
    public byte bySeriaType; //һ���ֽڵĴ������ͣ�1��232 2��485��
    public byte byChannel; //[0, n-1],n:ͨ����
    public byte[] byReserve = new byte[5]; //����
}


//��ȡ�豸��Ϣ��չ
//#define NET_SDVR_DEVICECFG_GET_EX       0xC6
//�����豸��Ϣ(��չ)
//#define NET_SDVR_DEVICECFG_SET_EX       0xC7  
public class HB_DEVICEINFO_V2
{
    public uint dwSize; //�ṹ���С
    public byte[] sDVRName = new byte[32]; //�豸, �ԡ�\0�������ַ���
    public uint dwDVRID; //����
    public uint dwRecycleRecord; //Э���: //¼�񸲸ǲ��� 0-ѭ������ 1-��ʾ����
    public byte[] sSerialNumber = new byte[48]; //���к�
    public byte[] sSoftwareVersion = new byte[64]; //����汾���ԡ�\0�������ַ���Э���: �������ͺ� ����汾�ţ�
    public byte[] sSoftwareBuildDate = new byte[32]; //������������ԡ�\0�������ַ���Э���:��Build 100112��
    public uint dwDSPSoftwareVersion; //DSP����汾
    public byte[] sPanelVersion = new byte[32]; //ǰ���汾���ԡ�\0�������ַ�����IPC��
    public byte[] sHardWareVersion = new byte[32]; //(����)Э���: ������汾�ų���16�ֽ�ʱ��ʹ����Ϊ�����ͺ���ʾ
    public byte byAlarmInPortNum; //�����������, NVRֻȡ���ر�������
    public byte byAlarmOutPortNum; //�����������, NVRֻȡ���ر������
    public byte byRS232Num; //����
    public byte byRS485Num; //����
    public byte byNetworkPortNum; //����
    public byte byDiskCtrlNum; //����
    public byte byDiskNum; //Ӳ�̸���
    public byte byDVRType; //DVR����, 1:NVR 2:ATM NVR 3:DVS 4:IPC 5:NVR ������ʹ��//NET_SDVR_GET_DVRTYPE���
    public byte byChanNum; //ͨ������[0, 128]
    public byte byStartChan; //����
    public byte byDecordChans; //����
    public byte byVGANum; //����
    public byte byUSBNum; //����
    public byte[] byReserve = new byte[3]; //����
}


//��ȡnvr��̨Э���б�
//#define NET_SDVR_PTZLIST_GET_NVR        0xC8
public class HB_NVR_PTZLIST
{
    public byte byType; //0-NVR������̨��1-ǰ���豸��̨ (Ϊ1ʱbyChannel��Ч)
    public byte byChannel; //[0, n-1],n:ͨ����
    public byte[] byReserve = new byte[2]; //����
}

public class HB_NVR_PTZLIST_INFO
{
    public uint dwPtznum; //Э�����������Ϊ���100����
    public byte[] byReserve = new byte[4]; //����
    public byte[,] sPtzType = new byte[100, 10]; //Э�����б�D�D�D0��unknow;
}

//��ȡNVR������������˿�����
//#define NET_SDVR_ALRM_ATTR_NVR                  0xC9 
public class HB_NVR_ALRM_PORT_ATTR
{
    public uint dwSize; // �ṹ���С
    public byte byOpType; // 0-���� 1-ǰ�� (Ϊ1ʱ,byChannel��Ч)
    public byte byChannel; // ����ǰ��ĳͨ���豸 [0, n-1], n:ͨ������
    public byte byAlarmInNum; // ���豸ӵ�б����������
    public byte byAlarmOutNum; // ���豸ӵ�б����������
    public byte[,] sAlarmInPortName = new byte[128, 32]; // ��������˿����� �ԡ�\0�������ַ���
    public byte[,] sAlarmOutPortName = new byte[16, 32]; // ��������˿����� �ԡ�\0�������ַ���
    public byte[] sDevName = new byte[32]; // ͨ����Ӧ�豸����
    public uint dwIP; // �豸IP
    public byte[] byReserve = new byte[4]; // ����
}


//��ȡnvr�����������
//#define NET_SDVR_ALRMIN_GET_NVR         0xCA
//����nvr�����������
//#define NET_SDVR_ALRMIN_SET_NVR         0xCB	

public class HB_NVR_ALRMININFO
{
    public uint dwSize; //�ṹ���С
    public byte byOpType; //0-���� 1-ǰ�� (Ϊ1ʱ,byChannel��Ч)
    public byte byChannel; //����ǰ��ĳͨ���豸 [0, n-1], n:ͨ������
    public byte byAlarmInPort; //��������˿ں�[0, n-1], n:�����������
    public byte[] sAlarmInName = new byte[32]; //��������˿����� �ԡ�\0�������ַ���
    public byte byAlarmType; //̽ͷ���� 0-����1-����
    public byte byEnSchedule; //�������벼��ʱ�伤�� 0-���� 1-����
    public byte byWeekEnable; //ÿ��ʹ��λ0-��ʹ�� 1-ʹ��(��ʹ��,ֻȡstruAlarmTime[0][0~7]������ÿһ��)
    public byte[] byAllDayEnable = new byte[8]; //ȫ��ʹ�� ,0-��ʹ�� 1-ʹ��������ʹ��,���Ӧ����Ϊȫ�첼��,�����ж�ʱ���
    public HB_SDVR_SCHEDTIME_V2_ST[,] struAlarmTime = new HB_SDVR_SCHEDTIME_V2_ST[8, 8]; //����ʱ���
    public uint dwHandleType; //��λ 2-�������� 5-��������� //6-�ʼ��ϴ�

    // �����������
    public byte[] byAlarmOutLocal = new byte[16]; //��������˿�(����)
    public byte[,] byAlarmOutRemote = new byte[128, 16]; //��������˿�(ǰ���豸)

    // ����¼��
    public byte[] byRelRecordChan = new byte[128]; //����������¼��ͨ��,Ϊ1��ʾ������ͨ�� 0Ϊ������

    // ��������
    public byte[] byEnablePreset = new byte[128]; //�Ƿ����Ԥ�õ� ����byEnablePreset[0]���ж�
    public byte[] byPresetNo = new byte[128]; //���õ���̨Ԥ�õ����,һ������������Ե��ö��ͨ������̨Ԥ�õ�, 0xff��ʾ������Ԥ�õ� [1, 254]
    public byte[] byReserve = new byte[32]; //����
}


//��ȡnvr�����������
//#define NET_SDVR_ALRMOUT_GET_NVR        0xCC
//����nvr�����������
//#define NET_SDVR_ALRMOUT_SET_NVR        0xCD
public class HB_NVR_ALARMOUTINFO
{
    public uint dwSize; // �ṹ���С
    public byte byOpType; // 0-���� 1-ǰ�� (Ϊ1ʱ,byChannel��Ч)
    public byte byChannel; // ����ǰ��ĳͨ���豸 [0, n-1], n:ͨ������
    public byte byALarmOutPort; // �������ͨ���� [0, n-1], n:��������˿���
    public byte[] sAlarmOutName = new byte[32]; // ���� �ԡ�\0�������ַ���
    public uint dwAlarmOutDelay; // �������ʱ�� ��λ�� [2, 300]
    public byte byAlarmType; // ̽ͷ���� 0-����1-���� (����)
    public byte byEnSchedule; // �����������ʱ�伤�� 0-���� 1-����
    public byte byWeekEnable; // ÿ��ʹ��λ0-��ʹ�� 1-ʹ��(��ʹ��,ֻȡstruAlarmTime[0][0~7]��ÿ��������)
    public byte[] byFullDayEnable = new byte[8]; // ������¼�� 0-��ʹ��(ȱʡֵ) 1-ʹ��
    public HB_SDVR_SCHEDTIME_V2_ST[,] struAlarmTime = new HB_SDVR_SCHEDTIME_V2_ST[8, 8]; //����ʱ���, 8��ʱ���

    public byte[] byReserve = new byte[32]; // ����
}


//��ȡnvr��������״̬
//#define NET_SDVR_ALRMIN_STATUS_GET_NVR  0xCE
public class HB_NVR_ALRMIN_STATUS
{
    public byte byOpType; // 0-���� 1-ǰ�� (Ϊ1ʱ,byChannel��Ч)
    public byte byChannel; // ����ǰ��ĳͨ���豸 [0, n-1], n:ͨ������
    public byte byAlarm; // ����
    public byte byReserve1; // ����
    public byte[] byAlarmIn = new byte[128]; // ��������״̬ 128����������, 0-������ 1-������
    public byte[] byRreserve2 = new byte[32]; // ����
}

//��ȡnvr�������״̬
//#define NET_SDVR_ALRMOUT_STATUS_GET_NVR 0xCF
//����nvr�������״̬
//#define NET_SDVR_ALRMOUT_STATUS_SET_NVR 0xD1
public class HB_NVR_ALRMOUT_STATUS
{
    public byte byOpType; // 0-���� 1-ǰ�� (Ϊ1ʱ,byChannel��Ч)
    public byte byChannel; // ����ǰ��ĳͨ���豸 [0, n-1], n:ͨ������
    public byte byAlarm; // �������״̬ 0-������ 1-����
    public byte byReserve1; // ����
    public byte[] byAlarmOut = new byte[16]; // �������״̬ 16���������, ��ȡ����ʱ,byAlarm��Ч,byAlarmOut[16]��Ϊ1���������,0Ϊ���������������ʱbyAlarm��Ч��byAlarmOut[16]��0-״̬���� 1-ִ��byAlarm����
    public byte[] byReserve2 = new byte[32];
}


//��ȡnvrͨ������
//#define NET_SDVR_PICCFG_GET_EX_NVR      0xD2
//����nvrͨ������
//#define NET_SDVR_PICCFG_SET_EX_NVR      0xD3 
// typedef struct	
// {
//     WORD wHideAreaTopLeftX;				    // �ڸ������x����  0~704
//     WORD wHideAreaTopLeftY;				    // �ڸ������y����  0~576
//     WORD wHideAreaWidth;					// �ڸ�����Ŀ� 0~704
//     WORD wHideAreaHeight;					// �ڸ�����ĸ� 0~576
// } HB_SDVR_SHELTER;

public class VIDEO_INFO
{
    public byte byBrightness; // ���� ȡֵ��Χ[0��255] ȱʡֵ128
    public byte byConstrast; // �Աȶ� ȡֵ��Χ[0��255] ȱʡֵ128
    public byte bySaturation; // ���Ͷ� ȡֵ��Χ[0��255] ȱʡֵ128
    public byte byHue; // ɫ�� ȡֵ��Χ[0��255] ȱʡֵ128
    public byte bySharp; // ��� 0��ȱʡֵ����1
    public uint dwReserved; // Ԥ��
}

public class HB_NVR_CHN_ATTR_INFO
{
    public uint dwSize; // ����(�ṹ���С)

    // ͨ�������
    public byte[] sChanName = new byte[32]; // ͨ���� �ԡ�\0�������ַ���
    public byte byChannel; // ͨ���� [0, n��1] n:ͨ����
    public uint dwShowChanName; // �Ƿ���ʾͨ���� 0-��ʾ 1-����ʾ
    public byte byOSDAttrib; // ͨ���� 1-��͸�� 2-͸����ֻ���PC����ʾ��
    public ushort wShowNameTopLeftX; // ͨ��������ʾλ�õ�x���� ��->�� 0~��Ƶʵ�ʿ�ȣ�ͨ��������
    public ushort wShowNameTopLeftY; // ͨ��������ʾλ�õ�y���� ��->�� 0~��Ƶʵ�ʸ߶ȣ�����߶�

    // �������
    public uint dwShowTime; // �Ƿ���ʾʱ�� 0-��ʾ 1-����ʾ
    public ushort wOSDTopLeftX; // ʱ��osd����X [0, ʵ�ʿ�ʱ�볤��]
    public ushort wOSDTopLeftY; // ʱ��osd����Y[0, ʵ�ʸߣ�����߶�]
    public byte byDateFormat; // ���ڸ�ʽ
                              //  0 - YYYY-MM-DD    ��ȱʡֵ��
                              //  1 - MM-DD-YYYY
                              //  2 - YYYY��MM��DD��
                              //  3 - MM��DD��YYYY��

    // �������
    public byte byDispWeek; // �Ƿ���ʾ���� 0-��ʾ 1-����ʾ
    public byte byOSDLanguage; // �������� 0-���� 1-Ӣ�� (����չ)

    // ����ɫ�����
    public VIDEO_INFO struVideoInfo = new VIDEO_INFO(); // ��Ƶ��Ϣ

    // �ڵ��������
    public uint dwEnableHide; // ��Ƶ�ڵ�ʹ�� ,0-���ڵ�,1-�ڵ�(�ڵ�����ȫ��) 2-�ڵ�(�ڵ�����������)
    public HB_SDVR_SHELTER[] struShelter = new HB_SDVR_SHELTER[16]; // ��Ƶ�ڵ�����
    public uint dwOsdOverType; // osd�������� 0-������ 1-ǰ�˵��� 2-��˵���
    public uint[] dwReserve = new uint[32]; // ����
}



//��ȡnvr¼�����
//#define NET_SDVR_RECORD_GET_EX_NVR      0xD4
//����nvr¼�����
//#define NET_SDVR_RECORD_SET_EX_NVR      0xD5
public class HB_RECORD_PARAM
{
    public byte byStreamtype; // ������ 0-��������ȱʡֵ�� 1-������
    public byte byQuality; // ��Ƶ���� 1-��� 2-�ϸ� 3-�ߣ�ȱʡֵ�� 4-�� 5-�� 6-���
    public byte byResolution; // ������ 0-DIF 1-D1��ȱʡֵ�� 2-720P 3-1080P
                              // ������     0-CIF 1-D1(ȱʡֵ)
    public byte byFramerate; // ֡�� ȡֵ��Χ[2,25] ȱʡֵ25
    public byte byMaxbitrate; // ����(��) 0-100K 1-128K 2-256K 3-512K 4-1M 5-1.5M 6-2M��ȱʡֵ�� 7-3M 8-4M 9-6M 10-8M
                              // �������ӣ� 0-30K 1-45K 2-60K 3-75K 4-90K 5-100K 6-128K 7-256K 8-512K(ȱʡֵ) 9-1M 10-2M
    public byte byAudio; // ��Ƶ��ʶ 0-����Ƶ 1-����Ƶ��ȱʡֵ��
    public uint dwReserved; // Ԥ��
}

public class HB_REC_TIME_PERIOD
{
    public byte byStarth; // ��ʼʱ��-ʱ
    public byte byStartm; // ��ʼʱ��-��
    public byte byStoph; // ����ʱ��-ʱ
    public byte byStopm; // ����ʱ��-��
    public byte byRecType; // ¼������ 0 - �� 1-�ֶ�(��Ч) 2-��ʱ 3-�ƶ� 4-���� 5-�ƶ� | ���� 6 -�ƶ� & ����
    public byte[] byReserve = new byte[3]; // ����
}

public class HB_FULL_DAY_S
{
    public byte byEnable; // ������ʹ�� 0-��ʹ��(ȱʡֵ) 1-ʹ��
    public byte byRecType; // �������Ӧ��¼������ 0 - �� 1-�ֶ�(��Ч) 2-��ʱ 3-�ƶ� 4-���� 5-�ƶ� | ���� 6- �ƶ� & ����
    public byte[] byReserve = new byte[2]; // ����
}

public class HB_REC_TIME_SCHEDULE
{
    public byte byEnable; // ʹ��ʱ��� 0-��ʹ��(ȱʡֵ) 1-ʹ��
    public byte byWeekEnable; // ÿ��ʹ��λ 0-��ʹ�� 1-ʹ��(��ʹ��,ֻȡstruAlarmTime[0][0~7]��ÿ��������)
    public HB_FULL_DAY_S[] struFullDayEnable = new HB_FULL_DAY_S[8]; // ������¼��
    public HB_REC_TIME_PERIOD[,] struAlarmTime = new HB_REC_TIME_PERIOD[8, 8]; // [0-7][0]����ȫ��ʹ�ܵ�������

    public uint dwReserved; // Ԥ��
}

public class HB_RECORD_SET
{
    public uint dwSize; // �ṹ���С
    public byte byChannel; // ͨ����
    public ushort wPreRecTime; // Ԥ¼ʱ�� ȡֵ��Χ[5��30]�� ȱʡֵ10
    public uint dwDelayRecTime; // ¼�����ʱ�� ȡֵ��Χ[0,180]�� ȱʡֵ30 (��3-�ƶ�¼�� 4-����¼�� 5-�ƶ� | ���� 6-�ƶ� & ���� ��Ч)
    public HB_REC_TIME_SCHEDULE struTimeSchedule = new HB_REC_TIME_SCHEDULE(); // ¼��ʱ�����¼����������

    public HB_RECORD_PARAM struTimeRecord = new HB_RECORD_PARAM(); // ��ʱ ¼�����
    public HB_RECORD_PARAM struMoveRecord = new HB_RECORD_PARAM(); // �ƶ� ¼�����
    public HB_RECORD_PARAM struAlarmRecord = new HB_RECORD_PARAM(); // ���� ¼�����
    public HB_RECORD_PARAM struMoveOrAlarmRecord = new HB_RECORD_PARAM(); // �ƶ� | ���� ¼�����
    public HB_RECORD_PARAM struMoveAndAlarmRecord = new HB_RECORD_PARAM(); // �ƶ� & ���� ¼�����
    public HB_RECORD_PARAM[] struNetRecParam = new HB_RECORD_PARAM[4]; // ������ ¼����� (����)

    public uint byLinkMode; // ��������(0-�������� 1-��һ��������2-�ڶ�������....)
    public uint[] dwReserved = new uint[31]; // Ԥ��
}


//��ȡnvr�ƶ�������
//#define NET_SDVR_MOTION_DETECT_GET_NVR  0xD6
//����nvr�ƶ�������
//#define NET_SDVR_MOTION_DETECT_SET_NVR  0xD7
public class HB_NVR_MOTION
{
    public uint dwSize; // ���ȣ��ṹ���С��
    public byte byChannel; // ͨ���� [0, n��1] n:ͨ����

    public byte[,] byMotionScope = new byte[18, 22]; // �������,����32*32��С���,Ϊ1��ʾ�ú�����ƶ��������,0-��ʾ����
    public byte byMotionSensitive; // �ƶ����������, 0 - 5,Խ��Խ����

    // ʱ������
    public byte byEnableHandleMotion; // �ƶ���Ⲽ��ʹ�� 0-���� 1-����
    public byte byWeekEnable; // ����ÿ��0-��ʹ�� 1-ʹ��(��ʹ��,ֻȡstruAlarmTime[0][0~7]��ÿ��������)
    public byte[] byFullDayEnable = new byte[8]; // ������¼�� 0-��ʹ��(ȱʡֵ) 1-ʹ��,������ʹ��,���Ӧ����Ϊȫ�첼��,�����ж�ʱ���
    public HB_SDVR_SCHEDTIME_V2_ST[,] struAlarmTime = new HB_SDVR_SCHEDTIME_V2_ST[8, 8]; // ����ʱ���, 8��ʱ���
    public uint dwHandleType; // ��λ 2-��������5-��������� //6-�ʼ��ϴ�

    // �����������
    public byte[] byAlarmOutLocal = new byte[16]; // ��������˿�(����)
    public byte[,] byAlarmOutRemote = new byte[128, 16]; // ��������˿�(ǰ���豸)

    // ����¼��    
    public byte[] byRecordChannel = new byte[128]; // ������¼��ͨ����Ϊ0-������ 1-����

    // ��������  
    public byte[] byEnablePreset = new byte[128]; // �Ƿ����Ԥ�õ� ����byEnablePreset[0]���ж�;
    public byte[] byPresetNo = new byte[128]; // ���õ���̨Ԥ�õ����,һ������������Ե��ö��ͨ������̨Ԥ�õ�, 0xff��ʾ������Ԥ�õ� [1, 254]
    public uint[] dwReserve = new uint[32]; // ����
}




//��ȡnvr�쳣��������
//#define NET_SDVR_ABNORMAL_ALRM_GET_NVR  0xD8
//����nvr�쳣��������
//#define NET_SDVR_ABNORMAL_ALRM_SET_NVR  0xD9
public class HB_NVR_ABNORMAL
{
    public uint dwSize; // ���ȣ��ṹ���С��
    public byte byAbnormalAlarmType; // 1-��Ƶ��ʧ 2-����Ͽ� 3-�¶ȹ��� 4-�ڵ�����...
    public byte byChannel; // ͨ���� (����Ƶ��ʧ, �ڵ�����, ����Ͽ�����Ч)

    public byte byEnableAbnormal; // �쳣����ʹ�� 0-������ 1-������
    public ushort wHideAlarmAreaTopLeftX; // [0, ʵ�ʿ�1]���ڵ�����ʱ��Ч��
    public ushort wHideAlarmAreaTopLeftY; // [0, ʵ�ʸߣ�1]���ڵ�����ʱ��Ч��
    public ushort wHideAlarmAreaWidth; // [16, ʵ�ʿ�] ���ڵ�����ʱ��Ч��
    public ushort wHideAlarmAreaHeight; // [16, ʵ�ʸ�] ���ڵ�����ʱ��Ч��

    // �����������
    public byte[] byAlarmOutLocal = new byte[16]; //��������˿�(����)
    public byte[,] byAlarmOutRemote = new byte[128, 16]; //��������˿�(ǰ���豸)

    public byte byWeekEnable; // ����ÿ��0-��ʹ�� 1-ʹ��(��ʹ��,ֻȡstruAlarmTime[0][0~7]��ÿ��������)
    public byte[] byFullDayEnable = new byte[8]; // ������¼�� 0-��ʹ��(ȱʡֵ) 1-ʹ��,������ʹ��,���Ӧ����Ϊȫ�첼��,�����ж�ʱ���
    public HB_SDVR_SCHEDTIME_V2_ST[,] struAlarmTime = new HB_SDVR_SCHEDTIME_V2_ST[8, 8]; //����ʱ���, 8��ʱ���
    public uint dwHandleType; // ����ʽ ��λ 2-��������5-��������� //6-�ʼ��ϴ�

    public uint[] dwReserve = new uint[32]; // ����
}



//���ļ�����
//#define NET_SDVR_PARAM_FILE_EXPORT      0xDA
public class HB_EXPT_REQ
{
    public uint dwFileSize; // �����ļ��Ĵ�С
    public uint dwReserve; // ����
                           //    BYTE *pFileData; 	    // �����ļ�����
}


//�����ļ�����
//#define NET_SDVR_PARAM_FILE_IMPORT      0xDB 
public class HB_IMPT_REQ
{
    public uint dwFileSize; // �����ļ��Ĵ�С
    public uint dwReserve; // ����
}

public class HB_NVR_RESOLUTION
{
    public byte[] bySupportResolution = new byte[32]; // �ò�����ʾnvr����֧�ֵ�����ֱ��ʸ�ʽ��
                                                      // �����±����ӳ������(ӳ�������)��1��ʾ֧�֣�0��ʾ��֧��
    public byte byCurResolution; // �����˵�ǰ����ֱ���
    public byte[] reserve = new byte[7]; // ����
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
    public uint dwStatus; // ����״̬�룬������ʱ��Ч
    public uint[] dwReserve = new uint[4];
}

//#define NET_SDVR_GET_ZERO_VENCCONF
//#define NET_SDVR_SET_ZERO_VENCCONF

// ����ͨ�������������
//#define MAX_CHANNUM 128
public enum NET_AUSTREAMADD_E : int
{
    NET_AUSTREAM_DISABLE, //��Ƶ��
    NET_AUSTREAM_ENABLE, //������
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
    NET_BITRATE_CHANGE, //������
    NET_BITRATE_NOCHANGE, //������
}

public enum NET_VQUALITY_E : int
{
    NET_VQUALITY_BEST = 0, //���
    NET_VQUALITY_BETTER, //�ϸ�
    NET_VQUALITY_GOOD, //��
    NET_VQUALITY_NORMAL, //��
    NET_VQUALITY_BAD, //��
    NETT_VQUALITY_WORSE //���
}

public class HB_SDVR_VENC_CONFIG
{
    public NET_AUSTREAMADD_E byStreamType = new NET_AUSTREAMADD_E(); //��Ƶ������
    public NET_RESOLUTION_E byResolution = new NET_RESOLUTION_E(); //��Ƶ�ֱ���
    public NET_BITRATETYPE_E byBitrateType = new NET_BITRATETYPE_E(); //��������
    public NET_VQUALITY_E byPicQuality = new NET_VQUALITY_E(); //ͼ������
    public uint dwVideoBitrate; //��Ƶ���� ʵ������
    public ushort dwVideoFrameRate; //֡�� PAL 2-30 N 2-30
    public ushort quant; //�������� 1-31
}

public class HB_SDVR_ZERO_VENC_CONFIG
{
    public uint enable; // ��ͨ��ʹ�ܣ�1-���ã�0-������
    public string chlist = new string(new char[HBConst.MAX_CHANNUM_EX]); // ��ͨ�������ʽ���, ��Ӧͨ���������±��0��ʼ��ʾ, 0-��ѡ��, 1-ѡ��
    public HB_SDVR_VENC_CONFIG venc_conf = new HB_SDVR_VENC_CONFIG(); //�������
    public uint reserve; //����
}


//public class TAG_HB_SDVR_VOD_PARAM
//{
//    public byte byChannel; // ͨ���� [0, n-1],n:ͨ����
//    public byte byType; // ¼������:1-�ֶ���2-��ʱ��4-�ƶ���8-������0xFF-ȫ��
//    public ushort wLoadMode; // �ط�����ģʽ 1-��ʱ�䣬 2-������
////C++ TO C# CONVERTER TODO TASK: Unions are not supported in C#.
////ORIGINAL LINE: union
////C++ TO C# CONVERTER NOTE: Structs must be named in C#, so the following struct has been named AnonymousStruct6:
//    public struct AnonymousStruct6
//    {
////C++ TO C# CONVERTER NOTE: Classes must be named in C#, so the following class has been named AnonymousClass6:
//        public class AnonymousClass6
//        {
//            public HB_SDVR_TIME struStartTime = new HB_SDVR_TIME(); // ���һ����Ȼ��
//            public HB_SDVR_TIME struStopTime = new HB_SDVR_TIME(); // ����ʱ����ൽ23:59:59,
//            // ����ʾ�ӿ�ʼʱ�俪ʼһֱ����
//            public string cReserve = new string(new char[16]);
//        }
//        public AnonymousClass6 byTime = new AnonymousClass6();

//        public byte[] byFile = new byte[64]; // �Ƿ񹻳���
//    }
//    public AnonymousStruct6 mode = new AnonymousStruct6();

//    public uint[] dwReserve = new uint[4]; // ����
//}

//////////////////////////////////////////
////////IPC��Э��
/////////////////////////////////////////
//#define HB_IPCCFG_THERMAL_IMAGING
//#define HB_IPCCFG_IP_FILTER
//#define HB_IPCCFG_BLC
//#define HB_IPCCFG_PROTOCL

public class TAG_HB_SDVR_IPC_THERMAL_IMAGING
{
    public uint dwSize; // �ṹ�峤��
    public sbyte shutter_correct; // ����У�� 0:not support 1:close 2:open
    public sbyte electronic_enlarge; // ���ӷŴ� 0:not support 1:normal_display 2:enlarge_display
    public sbyte pseudo_colormode; // α��ģʽ 0:not support 1:white hot 2:black hot 3:fusion 4:rainbow 5:globow
                                   // 6:ironbow1 7:ironbow2 8:sepia 9:color1 10:color2 11:icefire 12:rain
    public sbyte auto_correct_switch; // �Զ�У������ 0:not support 1:close 2:open
    public sbyte auto_agcmode; // �Զ�����ģʽ 0:��֧�� 1:�Զ����� 2:����ֱ��ͼ 3:һ������ 4:�Զ����� 5:�ֶ�
    public sbyte contrast; // �Աȶ� ��0��255��
    public short light; // ���� -1:��֧��
    public short light_bias; // ����ƫ�� -1:��֧��
    public short[] reserve = new short[3]; // ����
}

public class TAG_HB_SDVR_IPC_IP_FILTER
{
    public uint dwSize; // sizeof(HB_SDVR_IPC_IP_FILTER)
    public sbyte cIPFilter; // ������IP��ַ���� 0-��֧�� 1-�� 2-�ر�
    public sbyte cIPRule; // ���� 1-����ͨ�� 2-������ͨ��
    public sbyte cPortFilter; // �˿ڹ��� 0-��֧�� 1-�� 2-�ر�
    public sbyte cPortRule; // ���� 1-����ͨ�� 2-������ͨ��
    public uint dwIPBegin; // ������IP��ʼ��ַ
    public uint dwIPEnd; // ������IP������ַ��������ַ��ֵҪ������ʼ��ַ��
                         // ���������ַΪ�գ�����Ϊֻ������ʼ��ַ
    public uint dwPortBegin; // ���˶˿���ʼ
    public uint dwPortEnd; // ���˶˿ڽ���
    public uint[] dwReserve = new uint[3]; // ����
}

public class TAG_HB_SDVR_IPC_BLC
{
    public uint dwSize; // sizeof(HB_SDVR_IPC_BLC)
    public byte byBLCEnable; // ���ⲹ�� 0-��֧�� 1-�� 2-�ر�
    public byte[] byReserve = new byte[3]; // ����
}

public class TAG_HB_SDVR_IPC_PROTOCL
{
    public uint dwSize;
    public byte byProtocl; // 0��֧�֣� 1-7000sdk, 2-ONVIF(������������֡ͷ)
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
    public uint dwIP; // IPServer IP��ַ
    public uint dwPort; // IPServer �˿�
    public byte[] byRegID = new byte[64]; // �豸ע����
    public string reserve = new string(new char[4]);
}




public enum HB_SDVR_IPC_PRI_CMD_TYPE_E : int //IPC�����������
{
    IPC_VIDEO_CMD = 0, //IPC����Ƶ����
    IPC_NET_CMD, //IPC��������
    IPC_STORE_CMD, //IPC�洢����
    IPC_ALARM_CMD, //IPC��������
    IPC_MANAGE_CMD, //IPC��������
    IPC_PRI_CMD_MAX
}

//////////////////////////////////////////////////////////////////////////////
//����Ƶ����
//////////////////////////////////////////////////////////////////////////////
public enum HB_SDVR_IPC_VIDEO_CMD_TYPE_E : int
{
    IPC_IMAGE_PARAM = 0, //IPCͼ�����
    IPC_VIDEO_PARAM, //IPC��Ƶ����
    IPC_VIDEO_ENCODE, //IPC��Ƶ�������
    IPC_PICTURE_SNAP, //IPCͼ��ץ�Ĳ���
    IPC_OSD_OVERLAY, //IPC�ַ����Ӳ���
    IPC_MASK, //IPC�ڵ�����
    IPC_ADVANCE_PARAM, //IPC�߼�����
    IPC_AUDIO_IN_PARAM, //IPC��Ƶ�������
    IPC_AUDIO_OUT_PARAM, //IPC��Ƶ�������
    IPC_VIDEO_CMD_MAX
}

//////////////////////////////////////////////////////////////////////////////
//��������
//////////////////////////////////////////////////////////////////////////////
public enum HB_SDVR_IPC_NET_CMD_TYPE_E : int
{
    IPC_IP_PARAM = 0, //IPC �����������
                      //     IPC_WLAN,                   //IPC �����������
                      //     IPC_DDNS,                   //IPC DDNS����    
    IPC_PPPOE, //IPC PPPOE����
    IPC_E_MAIL, //IPC E_MAIL����
                //    IPC_UPNP,                   //IPC UPNP����  
    IPC_FTP, //IPC FTP����
             //    IPC_NAS,                    //IPC NAS����  
             //    IPC_AUTO_REGIST,            //IPC �Զ�ע�����
    IPC_PLATFORM, //IPC ƽ̨��������Ϣ
                  //    IPC_IP_FILTER,              //IPC IP��ַ���˲���   
    IPC_NET_CMD_MAX
}

//////////////////////////////////////////////////////////////////////////////
//�洢����
//////////////////////////////////////////////////////////////////////////////
public enum IPC_STORE_CMD_TYPE_E : int
{
    IPC_TIME_RECORD = 0, //IPC��ʱ¼�����
    IPC_RECORD_MODE, //�洢��ʽ
    IPC_DISK_CFG, //IPC�洢�豸״̬
    IPC_STORE_CMD_MAX //�洢���������ֵ
}

//////////////////////////////////////////////////////////////////////////////
//��������
//////////////////////////////////////////////////////////////////////////////
public enum IPC_ALARM_CMD_TYPE_E : int
{
    IPC_MOTION = 0, //IPC�ƶ�������
    IPC_ALARMIN, //IPC�����������
    IPC_ALARMOUT, //IPC�����������
    IPC_NET_BUG, //IPC������ϱ�������
    IPC_STORE_BUG, //IPC�洢���ϱ�������
    IPC_ALARM_CMD_MAX //�������������ֵ
}


//////////////////////////////////////////////////////////////////////////////
//����������
//////////////////////////////////////////////////////////////////////////////
public enum IPC_MANAGE_CMD_TYPE_E : int
{
    IPC_DEVICE_INFO = 0, //IPC�豸��Ϣ
    IPC_AUTO_MAINTAINING, //IPC�Զ�ά������
    IPC_MANAGE_CMD_MAX //�豸�������������ֵ
}




//ʱ���
public class HB_SDVR_IPC_SYS_TIME
{
    public ushort uYear; //��
    public byte uMonth; //��
    public byte uDay; //��
    public byte uWeek; //����
    public byte uHour; //Сʱ
    public byte uMin; //����
    public byte uSec; //��
}

public class HB_SDVR_IPC_TIME_PERIOD
{
    public byte uStartHour; //��ʼ
    public byte uStartMin;
    public byte uEndHour; //����
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
//��Ƶ����Ƶ
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


//����
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
//    public int nPriCmdType; //��ӦHB_SDVR_IPC_PRI_CMD_TYPE_E
//    public int nSecCmdType; //��ӦHB_SDVR_IPC_VIDEO_CMD_TYPE_E��������
//    public int nChannelid;
//}

//public class HB_SDVR_IPC_CONFIG
//{
//    public int nPriCmdType; //��ӦHB_SDVR_IPC_PRI_CMD_TYPE_E
//    public int nSecCmdType; //��ӦHB_SDVR_IPC_VIDEO_CMD_TYPE_E��������
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
    public byte byChannel; // ͨ����[0,n-1], n:ͨ����
    public byte byType; // ��ѯ¼������ 0x01:�ֶ� 0x02:��ʱ 0x04:�ƶ� 0x08:���� 0xFF����
    public byte byYear; // ��ѯ���[0, 63], ʵ�����-2000
    public byte byMonth; // ��ѯ�·�[1, 12]
    public byte[] byReserver = new byte[32];
}

public class TAG_HB_SDVR_RECFILE_MONTHINFO
{
    public byte[] byDate = new byte[31]; // ������¼�����ݵ����ڣ� ����[n]����ĳ�µĵ�n+1��, 0:��¼�� 1:��
    public byte[] byReserver = new byte[9]; // ����
}


public class STRUCT_SDVR_RESOLUTION
{
    public uint dwResolution; //�ֱ���ֵ���磺0x07800438�������ֽ�(0x0780=1920)�������ֽ�(0x0438=1080)
    public uint[] dwVideoBitrate_support = new uint[32]; //�÷ֱ�����֧�ֵ����ʷ�Χ��ÿһ���������һ�����ʣ������ֵ
                                                         //���Ϊ0����ʾ������δ�õ�����Ϊ0����ʾ֧�ֵ�����ֵ����λΪKbit/s
    public ushort wVideoFrameRate_min; //�÷ֱ����µ���С֡��
    public ushort wVideoFrameRate_max; //�÷ֱ����µ����֡��
    public byte[] byPicQuality_support = new byte[10]; //�÷ֱ�����֧�ֵ�ͼ�������ȼ�,ÿ���������һ��ͼ�������ȼ���
                                                       //0��������ߣ� 1�����ǽϸߣ� 2�����Ǹߣ� 3�������У�4�����ǵͣ� 
                                                       //5��������ͣ�������Ϊ1����ʾ֧�ָ���ͼ������
    public byte[] reserve = new byte[2]; //����
}


//���͵�ʱ��ֻ��Ҫ��дbyChannel,dwSize,byCompressionType�������ֶ�)
public class STRUCT_SDVR_COMPRESSINFO_SUPPORT
{
    public uint dwSize; //�ṹ���С
    public byte byChannel; //ͨ����
    public byte byCompressionType; //������0-��������1-������1��������2��
    public byte byCompression_support; //֧�ֵ�������ÿλ����һ����������λΪ1��ʾ֧�ָ������� �ӵ�λ��ʼ�� //0λ��������������1λ����������1����2λ����������2...
    public byte byBitrateTypeIndex; //��ǰ������������ֵ����ʾ����dwBitrateType�е�λ��,
                                    //����dwBitrateTypeIndex = 0����ǰ��������ΪdwBitrateType�ĵ�0λ
                                    //��ָ�����������ͣ�����������
    public byte byBitrateType_support; //֧�ֵ��������ͣ�ÿһλ����һ���������ͣ���λΪ1��ʾ֧�ָ��������ͣ�
                                       //�ӵ�λ��ʼ����0λ�Ǳ���������1λ�Ƕ�����
    public byte byRecordType_index; //��ǰ¼����������ֵ
    public byte byRecordType_support; //֧�ֵ�¼�����ͣ�ÿλ����һ��¼�����ͣ���λΪ1��ʾ֧�ָ����ͣ�
                                      //�ӵ�λ��ʼ����0λ�ֶ�¼�񣬵�1λ��ʱ¼�񣬵�2λ�ƶ�¼��
                                      //��3λ����¼�񣬡�����15λ����¼��
    public byte byAudioflag; //��ǰ�Ƿ�����Ƶ��0-����Ƶ��1-����Ƶ
    public byte byAudio_support; //�Ƿ�֧����Ƶ��0-��֧�֣�1-֧�֣�����֧����Ƶʱ��byAudioflagֻ��Ϊ0
    public byte byPicQuality; //��ǰͼ�������� 0--��ߣ� 1-�ϸߣ� 2-�ߣ� 3-�У�4-�ͣ� 5-���
    public ushort wVideoFrameRate; //��ǰ֡��ֵ
    public uint dwVideoBitrate; //��ǰ����ֵ����λΪKbit/s
    public byte[] reserve = new byte[3]; //����
    public byte byResoluIndex; //��ǰ�ֱ�������ֵ����ʾ����byResolution_support�е�λ��
    public STRUCT_SDVR_RESOLUTION[] Resolution_support = new STRUCT_SDVR_RESOLUTION[16]; //֧�ֵķֱ��ʣ����16�ֱַ��ʣ�ÿ���ṹ
                                                                                         //�����һ�ֱַ��ʼ��÷ֱ�����֧�ֵ����ʣ�֡�ʣ�ͼ��������Χ���ýṹ
                                                                                         //���dwResolutionΪ0����ʾ�ýṹ��δ�õ�
}



public class STRUCT_USERINFO
{
    public byte[] sUserName = new byte[32]; //�û���
    public byte[] sPassword = new byte[32]; //����
    public byte[] byLocalRight = new byte[32]; //����Ȩ�� 1.����0δʹ�ã�2.ȡֵ��0-��Ȩ�ޣ�1-��Ȩ��
                                               //����1-�������á�����2-¼�����á�����3-������á�����4-�������á�
                                               //����5-�������á�����6-�������á�����7-¼��طš�����8-ϵͳ����
                                               //����9-ϵͳ��Ϣ������10-�������������11-��̨���ơ�����12-�ػ�������
                                               //����-13-USB����������14-����
    public byte[] byLocalChannel = new byte[128]; //�����û���ͨ���Ĳ���Ȩ�ޣ����128��ͨ����0-��Ȩ�ޣ�1-��Ȩ��
    public byte[] byRemoteRight = new byte[32]; //Զ�̵�¼�û����߱���Ȩ�� 1.����0δʹ�ã�2.ȡֵ��0-��Ȩ�ޣ�1-��Ȩ��
                                                //����1-��ʾ���á�����2-¼�����������3-��ʱ¼������4-�ƶ�¼��
                                                //����5-����¼������6-�������������7-��̨���á�����8-�洢����
                                                //����9-ϵͳ��������10-��Ϣ��ѯ������11-�ֶ�¼������12-�طš�
                                                //����-13-���ݡ�����14-��Ƶ����������-15-�������������16-Զ��Ԥ��
    public byte[] byRemoteChannel = new byte[128]; //Զ�̵�¼�û���ͨ���Ĳ���Ȩ�ޣ����128��ͨ����0-��Ȩ�ޣ�1-��Ȩ��
    public uint dwUserIP; //�û�IP��ַ(Ϊ0ʱ��ʾ�����κε�ַ)
    public byte[] byMACAddr = new byte[8]; //�����ַ
}

public class STRUCT_USERINFO_GUI
{
    public byte[] sUserName = new byte[32]; //�û������ԡ�\0�������ַ���
    public byte[] sPassword = new byte[32]; //���룬�ԡ�\0�������ַ���
    public byte[] byLocalRight = new byte[32]; //����Ȩ�� 1.����0δʹ�ã�2.ȡֵ��0-��Ȩ�ޣ�1-��Ȩ��
                                               //����1-�ֶ�¼������2-�ֶ�����������3-¼��طš�����4-���ݹ���
                                               //����5-���̹�������6-ϵͳ�ػ�������7-ϵͳ����������8-��̨���ơ�
                                               //����9-�������������10-�������á�����11-������á�����12-¼�����á�
                                               //����13-��ʱ¼������14-�������á�����15-�������á�����16-��̨���á�
                                               //����17-�������á�����18-ϵͳ��Ϣ������19-¼��״̬������20-����״̬��
                                               //����21-����״̬������22-��־��ѯ������23-�������á�����24-�û�����
                                               //����25-�ָ��������á�����26-����Ȩ�ޡ�����27-��ʱ������
                                               //����28-����¼��
    public byte[] byLocalChannel = new byte[128]; //�����û���ͨ���Ĳ���Ȩ�ޣ����128��ͨ����0-��Ȩ�ޣ�1-��Ȩ��
    public byte[] byRemoteRight = new byte[32]; //Զ�̵�½�û����߱���Ȩ�� 1.����0δʹ�ã�2.ȡֵ��0-��Ȩ�ޣ�1-��Ȩ��
                                                //���� 1-Զ��Ԥ�������� 2-�������á����� 3-Զ�̻طš����� 4-Զ�̱��ݡ�
                                                //���� 5-�鿴��־������ 6-�����Խ������� 7-Զ������������ 8-Զ������
    public byte[] byRemoteChannel = new byte[128]; //�û�Զ�̵�½ʱ��ͨ�����߱���Ȩ�ޣ����128��ͨ����0-��Ȩ�ޣ�1-��Ȩ��
    public uint dwUserIP; //�û���¼ʱpc����ip��ַ��Ϊ0��ʾ�κ�PC��������ʹ�ø��û���½��
                          //DVR�ϣ���Ϊ0��ʾֻ��ip��ַΪ�趨ֵ��pc���ſ���ʹ�ø��û���¼��
                          //DVR��
    public byte[] byMACAddr = new byte[8]; //�û���¼ʱPC����MAC��ַ��Ϊ0��ʾ�κ�PC��������ʹ�ø��û���½
                                           //��DVR�ϣ���Ϊ0��ʾֻ��MAC��ַΪ�趨ֵ��PC���ſ���ʹ�ø��û�
                                           //��½��DVR��

}

public class STRUCT_USERINFO_9000
{
    public byte[] user = new byte[32]; //�û������ԡ�\0�������ַ���
    public byte[] pwd = new byte[32]; //���룬�ԡ�\0�������ַ���
    public byte[] grp_name = new byte[32]; //������
    public long[] local_authority = new long[64]; //�����û�ʹ��Ȩ�ޣ�ÿλ����һ��ͨ��,bit0~bit63��ʾ0~63ͨ����
                                                  //ÿ���������һ��Ȩ�ޣ�����0-ʵʱԤ��������1-�ֶ�¼��
                                                  //����2-¼���ѯ�طš�����3-���ݹ�������4-¼�����������5-��̨���á�
                                                  //����6-��ͼ���á�����7-ͨ�����á�����8-��ʱ¼������9-�ƶ���⡢
                                                  //����10-������������11-�������á�����12-�������á�����13-�������á�
                                                  //����14-�������á�����15-��Ϣ�鿴������16-������������17-�������á�
                                                  //����18-�������á�����19-ϵͳ�ػ�������20-����¼��
    public long[] remote_authority = new long[64]; //Զ��Ȩ�ޣ�ÿλ����һ��ͨ����bit0~bit63��ʾ0~63ͨ����
                                                   //ÿ���������һ��Ȩ�ޣ�����0-Զ��Ԥ��������1-�������á�����2-Զ�̻�									//�š�����3-Զ�̱��ݡ�����4-�鿴��־������5-�����Խ�������6-Զ������
    public uint dwbind_ipaddr; //�û���¼ʱpc����ip��ַ��Ϊ0��ʾ�κ�PC��������ʹ�ø��û���½��
                               //DVR�ϣ���Ϊ0��ʾֻ��ip��ַΪ�趨ֵ��pc���ſ���ʹ�ø��û���¼��
                               //DVR��
    public byte[] bybind_macaddr = new byte[8]; //�û���¼ʱPC����MAC��ַ��Ϊ0��ʾ�κ�PC��������ʹ�ø��û���½
                                                //��DVR�ϣ���Ϊ0��ʾֻ��MAC��ַΪ�趨ֵ��PC���ſ���ʹ�ø��û�
                                                //��½��DVR��
}

//public class STRUCT_SDVR_USER_INFO_EX1
//{
//    public uint dwSize; //�ṹ���С
//    public ushort wUserInfoMode; //�û�Ȩ��ģʽ��1-�ϵ�Ȩ��ģʽ��2-��GUIȨ��ģʽ��3-9000��ĿȨ��ģʽ
//    public byte[] reserve = new byte[2]; //����
////C++ TO C# CONVERTER TODO TASK: Unions are not supported in C#, but the following union can be simulated with the StructLayout and FieldOffset attributes.
////ORIGINAL LINE: union
////C++ TO C# CONVERTER NOTE: Structs must be named in C#, so the following struct has been named AnonymousStruct10:
//    [StructLayout(LayoutKind.Explicit)]
//    public struct AnonymousStruct10
//    {
//        [FieldOffset(0)]
//        public STRUCT_USERINFO[] userInfo = new STRUCT_USERINFO[16]; //��dwUserInfoMode=1ʱ��ʹ�øýṹ��
//        [FieldOffset(0)]
//        public STRUCT_USERINFO_GUI[] userInfoGui = new STRUCT_USERINFO_GUI[16]; //��dwUserInfoMode=2ʱ��ʹ�øýṹ��
//        [FieldOffset(0)]
//        public STRUCT_USERINFO_9000[] userInfo9000 = new STRUCT_USERINFO_9000[16]; //��dwUserInfoMode=3ʱ��ʹ�øýṹ��
//    }
//    public AnonymousStruct10 info = new AnonymousStruct10();
//}

// ����ʱ��������ʱ��
public class STRUCT_SDVR_DST_WEEK_TIME_S
{
    public byte month; //����ʱ�������ã���[1��12]
    public byte weeks; //����ʱ�������ã���[1��5]
    public byte week; //����ʱ�������ã�����[0��6]��
    public byte hour; //����ʱ��������, ʱ[0��23]
    public byte min; //����ʱ�������ã���[0��59]
    public byte sec; //����ʱ�������ã���[0��59]
}
//˵�����������õ�ʱ�䣬��ʾ�ڼ��µĵڼ������ڼ��ļ�ʱ���ּ��룬��month=5��weeks=2��week=1��hour=10��min=0��sec=0����ʾ5�·ݵĵ�2������1��10��00��00


//ϵͳʱ�䶨��
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
//����ʱʱ������
public class STRUCT_SDVR_DST_TIME_S
{
    public byte dst_en; //����ʱʹ�ܼ���0-��ʹ�ܣ�1-ʹ��
    public byte dsttype_en; //��������Ϊ0, ����������Ϊ1
    public HSYSTIME start_date = new HSYSTIME(); //���������õĿ�ʼʱ��
    public HSYSTIME end_date = new HSYSTIME(); //���������õĽ���ʱ��
    public STRUCT_SDVR_DST_WEEK_TIME_S start_time = new STRUCT_SDVR_DST_WEEK_TIME_S(); //�������õĿ�ʼʱ��
    public STRUCT_SDVR_DST_WEEK_TIME_S end_time = new STRUCT_SDVR_DST_WEEK_TIME_S(); //�������õĽ���ʱ��
    public byte[] reserve = new byte[4]; //����
}

//#define OSDINFONUM_MAX
//#define OSDSTRSIZE_MAX

public class HB_SDVR_STRUCT_SDVR_OSDINFO
{
    public byte id; // ͨ���ַ�������Ϣ�� [0, n��1] n: �����ַ���Ϣ����
    public byte byLinkMode; // 0-������ 1-������
    public byte byChanOSDStrSize; // �����ַ���Ϣ���ַ������ݵĳ��ȣ������ַ���������'\0',[1, 100]
    public byte byOSDAttrib; // ͨ���ַ�������Ϣ 1-��͸�� 2-͸��(ֻ��� PC ����ʾ)
    public byte byOSDType; // ��ʽ�����ԣ����λΪ 0��ʾ�������ӣ�Ϊ 1��ʾǰ�˵���
                           // ��Ϊ 0x80 ʱ��ʾ�� osd ��Ϊǰ�˵���
    public string reservedData = new string(new char[3]); // ����
    public uint dwShowChanOSDInfo; // �Ƿ���ʾͨ���ַ�������Ϣ 0-��ʾ 1-����ʾ
    public ushort wShowOSDInfoTopLeftX; // ͨ���ַ�������Ϣ��ʾλ�õ� x ����
                                        //  [0,  ʵ�ʿ������ַ����ݳ���]
    public ushort wShowOSDInfoTopLeftY; // ͨ���ַ�������Ϣ��ʾλ�õ� y ����
                                        //  [0,  ʵ�ʸߣ�����߶�] 
    public string data = new string(new char[HBConst.OSDSTRSIZE_MAX]); // �����ַ���Ϣ����ַ������ݣ������ַ���������'\0',����С��100��
}

public class HB_SDVR_STRUCT_SDVR_OSDCFG
{
    public byte byChannel; // ͨ���� [0, n��1] n:ͨ����
    public byte byOSDInfoNum; // �����ĵ����ַ���Ϣ����[1,64]��ÿ��ṹΪSTRUCT_SDVR_OSDINFO
    public ushort byChanOSDInfoSize; // �����ַ���Ϣ�����ݰ���С,����
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
    public uint dwStreamType; // 0-������ 1-������
    public uint dwLinkMode; // 0-TCP 1-UDP
    public uint dwMultiCast; // �Ƿ�ಥ
    public uint dwOSDScheme; // osd�ַ������ʽ
    public uint dwMultiCastIP; // �ಥIP��ַ
    public uint dwPort; // �ಥ�˿�
    public IntPtr pfnDataProc;
    public IntPtr pContext;
    public uint[] dwReserver = new uint[4];
}

public class ST_HB_SDVR_PLAYBACKCON
{
    public uint dwSize;
    public uint dwChannel; // ͨ����
    public HB_SDVR_RECTYPE_E dwFileType = new HB_SDVR_RECTYPE_E(); // �ļ�����
    public IntPtr hWnd;
    public HB_SDVR_TIME struStartTime = new HB_SDVR_TIME(); // ����ʱ��ο�ʼʱ��
    public HB_SDVR_TIME struStopTime = new HB_SDVR_TIME(); // ����ʱ��
    public IntPtr pfnDataProc;
    public IntPtr pContext;
    public uint[] dwResever = new uint[4];
}


//��½
//
//��  �ܣ���½����
//��  ����
//sDVRIP�� IP��ַ
//wDVRPort�� �˿�
//sUserName���û���
//sPassword������
//lpDeviceInfo�� ָ��HB_SDVR_DEVICEINFO �ṹ��ָ��
//����ֵ��-1 ��ʾʧ�ܣ�����ֵ��ʾ�����û���IDֵ����IDֵ����SDK���䣬ÿ���û�ID ֵ�ڿͻ�����Ψһ��
//ɽ�����ڼӽ���
//#define MAX_IPC_CHANNUM
public class STRUCT_SDVR_DONGLEINFO
{
    public byte[] dongle_type = new byte[HBConst.MAX_IPC_CHANNUM]; //0: δ���Ӽ��ܻ�ģ��, 1: ɽ������SATA���ܻ�
    public byte[] realtime_encrypt = new byte[HBConst.MAX_IPC_CHANNUM]; //0: δ���ü���, 1: ���ü�����������
    public byte[] record_encrypt = new byte[HBConst.MAX_IPC_CHANNUM]; //0: δ���ü���, 1: ���ü�����������
}

public class STRUCT_SDVR_DONGLE_CHANNEL_INFO
{
    public byte channel; // ͨ����
    public byte stream_mode; // 0: ʵʱ��, 1: ��ʷ�ط�
    public byte stream_type; // 0: ������, 1: ������
}

public class STRUCT_SDVR_DONGLE_ENABLE
{
    public byte realtime_enc_enable;
    public byte reserve;

}
public struct HB_SDVR_VIDEOEFFECT
{
    public byte byChannel; //ͨ����
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    public string reservedData;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public HB_SDVR_SCHEDULE_VIDEOPARAM[] Schedule_VideoParam; //һ�����2��ʱ���
    public HB_SDVR_VIDEOPARAM Default_VideoParam; //����ʱ����ھ�ʹ��Ĭ��
}
public struct HB_SDVR_VIDEOPARAM
{
    public uint dwBrightValue; //���� 1-127
    public uint dwContrastValue; //�Աȶ�1-127
    public uint dwSaturationValue; //���Ͷ�1-127
    public uint dwHueValue; //ɫ�� 1-127
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
