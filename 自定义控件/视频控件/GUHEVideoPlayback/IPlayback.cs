using System;


namespace GHIBMS.VideoPlayback
{
    //实时视频播放的接口
    public interface IVideoPlayback : IDisposable
    {

        string ProtocolCode
        {
            get;
        }

        UInt16 VideoWndNubs
        {
            set;
            get;
        }
        /// <summary>
        /// 设置视频回放参数
        /// </summary>
        /// <param name="args">参数</param>
        void SetPlayAgrs(VideoPlaybackArgs args);
        /// <summary>
        /// 初始化SDK
        /// </summary>
        /// <returns></returns>
        bool VIDEO_Init();
        void DVR_Cleanup();
        /// <summary>
        /// 查找文件
        /// </summary>
        /// <param name="dwFileType">文件类型
        /// 录象文件类型 
        /// 0xff-全部，0-定时录像，1-移动侦测，2-报警触发，3-报警触发或移动侦测，4-报警触发和移动侦测，5-命令触发，6-手动录像，7-智能录像，10-PIR报警，11-无线报警，12-呼救报警，13-移动侦测、PIR、无线、呼救等所有报警类型的"或"，14-智能交通事件，15-越界侦测，16-区域入侵，17-声音异常，18-场景变更侦测，19-智能侦测（越界侦测|区域入侵|进入区域|离开区域|人脸识别） 
        /// </param>
        /// <param name="isLock">是否锁定</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        bool VIDEO_FindFile(uint dwFileType, byte isLock, DateTime startTime, DateTime endTime);
        /// <summary>
        /// 结束文件查找
        /// </summary>
        /// <returns></returns>
        bool VIDEO_FindClose();
        /// <summary>
        /// 按文件名回放
        /// </summary>
        /// <param name="sPlayBackFileName">文件名</param>
        /// <param name="dtStar">开始的时间</param>
        /// <returns></returns>
        bool VIDEO_PlayBackByName(string sPlayBackFileName, DateTime dtStar);
        /// <summary>
        /// 按时间回放
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        bool VIDEO_PlayBackByTime(DateTime startTime, DateTime endTime);
        /// <summary>
        /// 开始本地录像
        /// </summary>
        /// <returns></returns>
        bool VIDEO_PlayBackSaveData();
        /// <summary>
        /// 停止本地录像
        /// </summary>
        /// <returns></returns>
        bool VIDEO_StopPlayBackSave();
        /// <summary>
        /// 抓拍
        /// </summary>
        /// <returns></returns>
        bool VIDEO_PlayBackCaptureFile();
        /// <summary>
        /// 按文件名下载录像
        /// </summary>
        /// <param name="sDVRFileName">文件名</param>
        /// <returns></returns>
        bool VIDEO_GetFileByName(string sDVRFileName);
        /// <summary>
        /// 按时间下载录像
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        bool VIDEO_GetFileByTime(DateTime startTime, DateTime endTime);
        /// <summary>
        /// 停止下载
        /// </summary>
        /// <returns></returns>
        bool VIDEO_StopGetFile();
        /// <summary>
        /// 获取下载进行
        /// </summary>
        /// <returns>进度百分比</returns>
        int VIDEO_GetDownloadPos();
        /// <summary>
        /// 回放控制
        /// </summary>
        /// <param name="dwControlCode"> 命令码
        /// 采用海康的播放控制命令码，具体如下：
        ///NET_DVR_PLAYSTART = 1;//开始播放
        ///NET_DVR_PLAYSTOP = 2;//停止播放
        ///NET_DVR_PLAYPAUSE = 3;//暂停播放
        ///NET_DVR_PLAYRESTART = 4;//恢复播放
        ///NET_DVR_PLAYFAST = 5;//快放
        ///NET_DVR_PLAYSLOW = 6;//慢放
        ///NET_DVR_PLAYNORMAL = 7;//正常速度
        ///NET_DVR_PLAYFRAME = 8;//单帧放
        ///NET_DVR_PLAYSTARTAUDIO = 9;//打开声音
        ///NET_DVR_PLAYSTOPAUDIO = 10;//关闭声音
        ///NET_DVR_PLAYAUDIOVOLUME = 11;//调节音量
        ///NET_DVR_PLAYSETPOS = 12;//改变文件回放的进度
        ///NET_DVR_PLAYGETPOS = 13;//获取文件回放的进度
        ///NET_DVR_PLAYGETTIME = 14;//获取当前已经播放的时间(按文件回放的时候有效)
        ///NET_DVR_PLAYGETFRAME = 15;//获取当前已经播放的帧数(按文件回放的时候有效)
        ///NET_DVR_GETTOTALFRAMES = 16;//获取当前播放文件总的帧数(按文件回放的时候有效)
        ///NET_DVR_GETTOTALTIME = 17;//获取当前播放文件总的时间(按文件回放的时候有效)
        ///NET_DVR_THROWBFRAME = 20;//丢B帧
        ///NET_DVR_SETSPEED = 24;//设置码流速度
        ///NET_DVR_KEEPALIVE = 25;//保持与设备的心跳(如果回调阻塞，建议2秒发送一次)
        ///NET_DVR_PLAYSETTIME = 26;//按绝对时间定位
        ///NET_DVR_PLAYGETTOTALLEN = 27;//获取按时间回放对应时间段内的所有文件的总长度
        ///NET_DVR_PLAY_FORWARD = 29;//倒放切换为正放
        ///NET_DVR_PLAY_REVERSE = 30;//正放切换为倒放
        ///NET_DVR_SET_TRANS_TYPE = 32;//设置转封装类型
        // NET_DVR_PLAY_CONVERT = 33;//正放切换为倒放
        /// </param>
        /// <param name="dwInValue">输入参数</param>
        /// <param name="lpOutValue">输出参数</param>
        /// <returns></returns>
        bool VIDEO_PlayBackControl(uint dwControlCode, uint dwInValue, out uint lpOutValue);
        /// <summary>
        /// 停止回放
        /// </summary>
        /// <returns></returns>
        bool VIDEO_StopPlayBack();
        /// <summary>
        /// 处理WINDOWS窗口消息
        /// </summary>
        /// <param name="m"></param>
        void OnWinMessage(ref System.Windows.Forms.Message m);
        /// <summary>
        /// 设置处理WINDOWS消息窗口句柄
        /// </summary>
        /// <param name="lWinHandle"></param>
        void SetWinMessage(IntPtr lWinHandle);
    }
}
