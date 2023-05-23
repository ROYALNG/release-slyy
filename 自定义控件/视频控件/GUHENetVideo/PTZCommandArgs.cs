using System;
using System.Collections.Generic;
using System.Text;

namespace GHIBMS.NetVideo
{
    public enum PTZCommandEnum
    {
        /// <summary>
        /// 接通灯光电源
        /// </summary>
        LIGHT_PWRON = 2,
        /// <summary>
        /// 关闭灯光电源
        /// </summary>
        LIGHT_PWROFF = 32,
        /// <summary>
        /// 接通雨刷开关
        /// </summary>
        WIPER_PWRON = 3,
        /// <summary>
        /// 关闭雨刷开关
        /// </summary>
        WIPER_PWROFF = 3,
        /// <summary>
        /// 接通风扇开关
        /// </summary>
        FAN_PWRON = 4,
        /// <summary>
        /// 接通加热器开关
        /// </summary>
        HEATER_PWRON = 5,
        /// <summary>
        /// 接通辅助设备开关
        /// </summary>
        AUX_PWRON1 = 6,
        /// <summary>
        /// 接通辅助设备开关
        /// </summary>
        AUX_PWRON2 = 7,
        #region 云台预置位命令
        /// <summary>
        /// 设置预置点
        /// </summary>
        SET_PRESET = 8,
        /// <summary>
        /// 清除预置点
        /// </summary>
        CLE_PRESET = 9,
        /// <summary>
        /// 快球转到预置点
        /// </summary>
        GOTO_PRESET = 39,
        #endregion
        /// <summary>
        /// 焦距以速度SS变大(倍率变大) 
        /// </summary>
        ZOOM_IN = 11,
        /// <summary>
        /// 焦距以速度SS变小(倍率变小)
        /// </summary>
        ZOOM_OUT = 12,
        /// <summary>
        /// 焦点以速度SS前调 
        /// </summary>
        FOCUS_NEAR = 13,
        /// <summary>
        /// 焦点以速度SS后调
        /// </summary>
        FOCUS_FAR = 14,
        /// <summary>
        /// 光圈以速度SS扩大
        /// </summary>
        IRIS_OPEN = 15,
        /// <summary>
        /// 光圈以速度SS缩小
        /// </summary>
        IRIS_CLOSE = 16,
        /// <summary>
        /// 云台以SS的速度上仰
        /// </summary>
        TILT_UP = 21,
        /// <summary>
        /// 云台以SS的速度下俯
        /// </summary>
        TILT_DOWN = 22,
        /// <summary>
        /// 云台以SS的速度左转
        /// </summary>
        PAN_LEFT = 23,
        /// <summary>
        /// 云台以SS的速度右转
        /// </summary>
        PAN_RIGHT = 24,
        /// <summary>
        /// 云台以SS的速度上仰和左转
        /// </summary>
        UP_LEFT = 25,
        /// <summary>
        /// 云台以SS的速度上仰和右转
        /// </summary>
        UP_RIGHT = 26,
        /// <summary>
        /// 云台以SS的速度下俯和左转
        /// </summary>
        DOWN_LEFT = 27,
        /// <summary>
        /// 云台以SS的速度下俯和右转
        /// </summary>
        DOWN_RIGHT = 28,
        /// <summary>
        /// 云台以SS的速度左右自动扫描
        /// </summary>
        PAN_AUTO = 29
    }
    public class PTZCommandArgs
    {
        private int matrixID = 1;
        public int MatrixID
        {
            get { return matrixID; }
            set { matrixID = value; }
        }
        private int camID = 1;
        public int CamID
        {
            get { return camID; }
            set { camID = value; }
        }
        private int userID = -1;
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }
        private string dvrIp = "";
        public string DvrIp
        {
            get { return dvrIp; }
            set { dvrIp = value; }
        }

        private int channel = 0;
        public int Channel
        {
            get { return channel; }
            set { channel = value; }
        }
        private uint ptzCommand = 0;
        public uint PTZCommand
        {
            get { return ptzCommand; }
            set { ptzCommand = value; }
        }
        private uint stop = 0;
        public uint Stop
        {
            get { return stop; }
            set { stop = value; }
        }
        private uint speed = 5;
        public uint Speed
        {
            get { return speed; }
            set { speed = value; }
        }
    }
}
