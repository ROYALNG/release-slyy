using System;
using System.Runtime.InteropServices;

namespace GHIBMS.INFIPCSDK
{
    public class INFIPCCS
    {
        #region 数据类型
        /**
        * @struct tagAlarmerInfo
        * @brief 报警源设备信息
        * @attention 无
        */
        public struct S_ALARMER_INFO
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string sDeviceIP;      /**< 报警源设备的IP地址 */
            public ushort wLinkPort; /**< 报警源设备的通讯端口 */
        }
        public struct S_ALARM_INFO
        {
            public E_ALARM_TYPE eAlarmType;    /**< 报警类型 */
            public uint nAlarmID;      /**< 报警通道号，从1开始，通常表示设备通道号，继电器输入报警时表示继电器输入号；设备异常类型时表示：1-硬盘满，2-硬盘出错，3-网络断开，4-非法访问，5-网络冲突 */
            public byte cAlarmStatus; /**< 报警状态，0-无报警，1-有报警。继电器输入报警时， 0-报警取消，1-报警触发，2-报警持续 */
            public byte cAlarmArea;   /**< 报警区域号，移动侦测和视频遮挡有效，区域号代表哪个区域发生报警 */
        }

        /**
        * @enum tagRealDataType
        * @brief 报警类型
        * @attention 无
        */
        public enum E_ALARM_TYPE
        {
            ALARM_UNKNOWN = 0,/**< 未知类型报警 */
            ALARM_INPUT,      /**< 继电器输入报警 */
            ALARM_MOTION,     /**< 移动侦测报警 */
            ALARM_SHELTER,    /**< 视频遮挡报警 */
            ALARM_VIDEOLOST,  /**< 视频丢失报警 */
            ALARM_DEVICEERR,  /**< 预留，设备异常报警 */
        }

        #endregion
        #region 委托
        public delegate void CBAlarmMsg(IntPtr pAlarmer,
                                        IntPtr pAlarmInfo,
                                    IntPtr pUserData);
        #endregion
        #region 方法
        public const string SDK_INF = @".\Driver\INFIPC\INFSDK_Net.dll";
        static bool bInit = false;
        static bool bCleaup = false;
        public static bool Cleanup2()
        {
            if (!bCleaup)
            {
                INFNET_Cleanup();
                bCleaup = true;
                bInit = false;
            }
            return true;
        }
        public static bool Init2()
        {
            if (!bInit)
            {
                if (INFNET_Init())
                {
                    bInit = true;
                    bCleaup = false;
                    return true;
                }
                else
                    return false;
            }
            return true;
        }
        [DllImport(SDK_INF)]
        private static extern bool INFNET_Init();
        [DllImport(SDK_INF)]
        private static extern bool INFNET_Cleanup();
        [DllImport(SDK_INF)]
        public static extern int INFNET_GetLastError();
        /******************************************************************************
        普通摄像机用户登陆接口
        *******************************************************************************/
        /**
        * 用户登陆
        * @param [IN]   sDevIP    设备IP地址
        * @param [IN]   nDevPort  设备端口号
        * @param [IN]   sUserName 登录的用户名，最大长度为32字节
        * @param [IN]   sPassword 用户密码，最大长度为32字节
        * @return 返回如下结果：
        * - 失败：-1
        * - 其他值：表示返回的用户ID值。该用户ID具有唯一性，后续对设备的操作都需要通过此ID实现
        * - 获取错误码调用INFNET_GetLastError
        * @note 无
        */
        [DllImport(SDK_INF)]
        public static extern int INFNET_Login(string sDevIP,
                                              uint nDevPort,
                                              string sUserName,
                                              string sPassword);

        /******************************************************************************
        普通摄像机用户注销接口
        *******************************************************************************/
        /**
        * 用户注销
        * @param [IN]   lLoginID 用户ID号,INFNET_Login的返回值
        * @return 返回如下结果：
        * - 成功:true
        * - 失败:false
        * - 获取错误码调用INFNET_GetLastError
        * @note 只用于普通摄像机(不包括解码器,抓拍机),与INFNET_Login配合使用
        */
        [DllImport(SDK_INF)]
        public static extern bool INFNET_Logout(int lLoginID);
        [DllImport(SDK_INF)]
        public static extern bool INFNET_StartAlarmListen(CBAlarmMsg fDevUploadMsg,
                                                IntPtr pUserData);
        [DllImport(SDK_INF)]
        public static extern bool INFNET_StopAlarmListen();
        #endregion

    }
}
