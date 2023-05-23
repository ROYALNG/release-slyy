using System;
using System.Collections.Generic;
using System.Text;

namespace GHIBMS.DvrSDK.CS_SDK
{
    public enum EN_DVR_ALARMTYPE_EX
    {
	    DH_ALARM_ALARMEX,//External alarm 
	    DH_MOTIONDETECT_ALARMEX,//Motion detection alarm 
	    DH_VEDIOSHELTER_ALARMEX,//Video loss alarm 
	    DH_SHELTER_ALARMEX,//Camera masking alarm 
	    DH_SOUNDDETECT_ALARMEX,//Audio detectino alarm
	    DH_DISKFULL_ALARMEX,//HDD full alarm 
	    DH_DISKERROR_ALARMEX,//HDD malfunction alarm 
	    DH_ENCODER_ALARMEX,//Encoder alarm 
	    DH_URGENCY_ALARMEX,//Emergency alarm 
	    DH_WIRELESS_ALARMEX,//Wireless alarm 
	    DH_ALARM_DECODER_ALARMEX,//Alarm decoder alarm 
	    DH_STATIC_ALARMEX, // static alarm
	    DH_ALARM_ARM_DISARMSTATE,//Alarm arm disarm state 
	    DH_ALARM_NONE//No alarm. To initialize.
    }
    public struct NET_NEW_SOUND_ALARM_STATE
    {
	    public  int		channel;				// 报警通道号
	    public  int		alarmType;				// 报警类型；0：音频值过低，1：音频值过高
	    public  uint	volume;					// 音量值
	    public byte     byState;                // 音频报警状态, 0: 音频报警出现, 1: 音频报警消失
	    char				reserved[255];
    } 

    public struct  DH_NEW_SOUND_ALARM_STATE
    {
	    public   int  hannelcount;			// 报警的通道个数
	    public  NET_NEW_SOUND_ALARM_STATE SoundAlarmInfo[DH_MAX_ALARM_IN_NUM];
    } 

    public class DHAlarmInfoEx
    {
        	//Alarm type 
	    public EN_DVR_ALARMTYPE_EX	m_AlarmCommand;
	    public  byte[]          	m_dwAlarm =new byte[16];
	    DH_NEW_SOUND_ALARM_STATE	m_stuNewSound;
	    ALARM_DECODER_ALARM		m_stuAlarmDecoderAlarm;
	    //DVR alarm input channel amount 
	    int m_nAlarmInputCount;
	    //DVR video input channel amount 
	    int m_nChannelCount;
	    ALARM_ARM_DISARM_STATE_INFO m_stuAlarmArmDisarmstate;
    }
}
