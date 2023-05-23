using System;
namespace GHIBMS.NetVideo
{
    //实时视频播放的接口
    public delegate void OnMessagedelegate(object sender, string msg);
    public interface IVideoRealPlayer : IDisposable
    {

        event OnMessagedelegate OnMessage;
        string ProtocolCode
        {
            get;
        }

        UInt16 VideoWndNubs
        {
            set;
            get;
        }
        void SetPlayAgrs(VideoRealPlayArgs args);
        bool VIDEO_Init();
        void DVR_Cleanup();
        bool VIDEO_StartRealPlay();
        bool VIDEO_SaveRealData(string fileName);
        bool VIDEO_CapturePicture(string fileName);
        bool VIDEO_CaptureJPEGPicture(string fileName);
        bool VIDEO_StopRealPlay();
        bool VIDEO_StopSaveRealData();
        /// <summary>
        /// 云台控制 采用海康命令码
        /// </summary>
        /// <param name="dwPTZCommand">云台控制命令码</param>
        //LIGHT_PWRON = 2;// 接通灯光电源
        //WIPER_PWRON = 3;// 接通雨刷开关 
        //FAN_PWRON = 4;// 接通风扇开关
        //HEATER_PWRON = 5;// 接通加热器开关
        //AUX_PWRON1 = 6;// 接通辅助设备开关
        //AUX_PWRON2 = 7;// 接通辅助设备开关 
        //SET_PRESET = 8;// 设置预置点 
        //CLE_PRESET = 9;// 清除预置点 

        //ZOOM_IN = 11;// 焦距以速度SS变大(倍率变大)
        //ZOOM_OUT = 12;// 焦距以速度SS变小(倍率变小)
        //FOCUS_NEAR = 13;// 焦点以速度SS前调 
        //FOCUS_FAR = 14;// 焦点以速度SS后调
        //IRIS_OPEN = 15;// 光圈以速度SS扩大
        //IRIS_CLOSE = 16;// 光圈以速度SS缩小 

        //TILT_UP = 21;/* 云台以SS的速度上仰 */
        //TILT_DOWN = 22;/* 云台以SS的速度下俯 */
        //PAN_LEFT = 23;/* 云台以SS的速度左转 */
        //PAN_RIGHT = 24;/* 云台以SS的速度右转 */
        //UP_LEFT = 25;/* 云台以SS的速度上仰和左转 */
        //UP_RIGHT = 26;/* 云台以SS的速度上仰和右转 */
        //DOWN_LEFT = 27;/* 云台以SS的速度下俯和左转 */
        //DOWN_RIGHT = 28;/* 云台以SS的速度下俯和右转 */
        //PAN_AUTO = 29;/* 云台以SS的速度左右自动扫描 */

        //FILL_PRE_SEQ = 30;/* 将预置点加入巡航序列 */
        //SET_SEQ_DWELL = 31;/* 设置巡航点停顿时间 */
        //SET_SEQ_SPEED = 32;/* 设置巡航速度 */
        //CLE_PRE_SEQ = 33;/* 将预置点从巡航序列中删除 */
        //STA_MEM_CRUISE = 34;/* 开始记录轨迹 */
        //STO_MEM_CRUISE = 35;/* 停止记录轨迹 */
        //RUN_CRUISE = 36;/* 开始轨迹 */
        //RUN_SEQ = 37;/* 开始巡航 */
        //STOP_SEQ = 38;/* 停止巡航 */
        //GOTO_PRESET = 39;/* 快球转到预置点 */
        ///
        /// <param name="dwStop">云台停止动作或开始动作：0－开始；1－停止</param>
        /// <param name="dwSpeed">云台控制的速度，用户按不同解码器的速度控制值设置。取值范围[1,7]</param>
        /// <returns></returns>
        bool VIDEO_PTZControlWithSpeed(uint dwPTZCommand, uint dwStop, uint dwSpeed);
        bool VIDEO_PTZPreset(uint dwPTZCommand, uint PresetIndex);
        bool VIDEO_PTZCruise(uint dwPTZCommand, byte CruiseRoute, byte CruisePoint, ushort Input);
        bool VIDEO_PTZTrack(uint dwPTZCommand, uint dwTrackIndex);
        VideoEffect VIDEO_GetVideoEffect();

        bool VIDEO_SetVideoEffect(VideoEffect effect);
        bool VIDEO_OpenSound();
        bool VIDEO_CloseSound();
        bool VIDEO_Volume(ushort wVolume);
        bool VIDEO_StartVoiceCom();
        bool VIDEO_StopVoiceCom();
        bool VIDEO_SetVoiceComClientVolume(ushort wVolume);
        void OnWinMessage(ref System.Windows.Forms.Message m);
        void SetWinMessage(IntPtr lWinHandle);
        bool VIDEO_PlayControl(RealPlayControlEnum State);
    }
    public enum RealPlayControlEnum
    {
        PLAY = 0,
        STOP = 1
    }
}
