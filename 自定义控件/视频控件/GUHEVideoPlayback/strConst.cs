namespace GHIBMS.VideoPlayback
{
    public class strConst
    {
        #region 文件查找
        /// <summary>
        /// 获得文件信息
        /// </summary>
        public const int NET_DVR_FILE_SUCCESS = 1000;
        /// <summary>
        /// 没有文件
        /// </summary>
        public const int NET_DVR_FILE_NOFIND = 1001;
        /// <summary>
        /// 正在查找文件
        /// </summary>
        public const int NET_DVR_ISFINDING = 1002;
        /// <summary>
        /// 查找文件时没有更多的文件
        /// </summary>
        public const int NET_DVR_NOMOREFILE = 1003;
        /// <summary>
        /// 查找文件时异常
        /// </summary>
        public const int NET_DVR_FILE_EXCEPTION = 1004;

        #endregion
        #region 回放状态命令/文件播放命令(1-12)
        /// <summary>
        /// 开始播放
        /// </summary>
        public const int NET_DVR_PLAYSTART = 1;
        /// <summary>
        /// 停止播放
        /// </summary>
        public const int NET_DVR_PLAYSTOP = 2;
        /// <summary>
        /// 暂停播放
        /// </summary>
        public const int NET_DVR_PLAYPAUSE = 3;
        /// <summary>
        /// 恢复播放
        /// </summary>
        public const int NET_DVR_PLAYRESTART = 4;
        /// <summary>
        /// 快放
        /// </summary>
        public const int NET_DVR_PLAYFAST = 5;
        /// <summary>
        /// 慢放
        /// </summary>
        public const int NET_DVR_PLAYSLOW = 6;
        /// <summary>
        /// 正常速度
        /// </summary>
        public const int NET_DVR_PLAYNORMAL = 7;
        /// <summary>
        /// 单帧放
        /// </summary>
        public const int NET_DVR_PLAYFRAME = 8;
        /// <summary>
        /// 打开声音
        /// </summary>
        public const int NET_DVR_PLAYSTARTAUDIO = 9;
        /// <summary>
        /// 关闭声音
        /// </summary>
        public const int NET_DVR_PLAYSTOPAUDIO = 10;
        /// <summary>
        /// 调节音量
        /// </summary>
        public const int NET_DVR_PLAYAUDIOVOLUME = 11;
        /// <summary>
        /// 改变文件回放的进度
        /// </summary>
        public const int NET_DVR_PLAYSETPOS = 12;
        /// <summary>
        /// 获取文件回放的进度
        /// </summary>
        public const int NET_DVR_PLAYGETPOS = 13;
        /// <summary>
        /// 获取当前已经播放的时间(按文件回放的时候有效)
        /// </summary>
        public const int NET_DVR_PLAYGETTIME = 14;
        /// <summary>
        /// 获取当前已经播放的帧数(按文件回放的时候有效)
        /// </summary>
        public const int NET_DVR_PLAYGETFRAME = 15;
        /// <summary>
        /// 获取当前播放文件总的帧数(按文件回放的时候有效)
        /// </summary>
        public const int NET_DVR_GETTOTALFRAMES = 16;
        /// <summary>
        /// 获取当前播放文件总的时间(按文件回放的时候有效)
        /// </summary>
        public const int NET_DVR_GETTOTALTIME = 17;
        /// <summary>
        /// 丢B帧
        /// </summary>
        public const int NET_DVR_THROWBFRAME = 20;
        #endregion
    }
}
