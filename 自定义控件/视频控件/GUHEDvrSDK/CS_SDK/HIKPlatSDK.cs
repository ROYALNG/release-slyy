using System;
using System.Runtime.InteropServices;

namespace GHIBMS.HIKPLATSDK
{
    public delegate void pStreamCallback(Int32 lSession, int iStreamType, IntPtr data, int dataLen, IntPtr pUser);
    public delegate void pPreviewRecordEndCallback(Int32 lSession, int nErr, string data, IntPtr pUser);
    /** 
*  @brief 抓图拍照回调(包括预览和回放)
*  @param long lSession：取流实例
*  @param int nErr：错误码，一般为0，表示成功；其他暂不处理
*  @param const char* picPath：抓图成功后保存的图片全路径
*  @param void* pUser：用户数据
*/
    public delegate void pSnapShotCallback(Int32 lSession, int nErr, string picPath, IntPtr pUser);
    public struct HikLoginInfo
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string szUserName;         // 登录名称
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string szPassword;         // 密码
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string szServerUrl;        // CMS服务地址
        public uint uServerPort;       // 服务器端口
        public int nErrCode;               // 登入失败错误码
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
        public string szErrInfo;         // 登入失败错误详情
    }
    //抓图参数
    public struct HikSnapParam
    {
        public int nPicType;           // 0~JPG 1~BMP
        public int nSnapType;          // 0~单张 1~多张
        public int nMultiType;         // 连续抓图方式 按时间 0 ，按帧 1
        public int nSpanTime;          // 多张时 时间间隔 ms
        public int nSnapCount;         // 连抓张数  
        public int Quality;            // 图像质量
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string szSaveFolder; // 保存路径
        public Int32 lOpenFlag;         // 打开方式
        public int nFormatType;        // 格式化方式
    }
    //录像参数配置
    public struct HikRecordCfgParam
    {
        public int nType;              // 0按时间分包 1，其他按 大小
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string szFolde;     // 录像存储文件夹
        public uint dwMaxRecordTimes; // 最长录像时长 秒 0 不限制
        public uint dwTimes;          // 分包大小 按秒
        public UInt64 dwPackSize;   // 分包大小，0则不分包,单位 字节
        public int nFormatType;        // 格式化方式
    }
    public enum HIK_RECORD_TYPE
    {
        HIK_RECTYPE_UNKNOWN = 0,     // 未知
        HIK_RECTYPE_PLAN = 0x01,     // 计划录像
        HIK_RECTYPE_MOVE = 0x02,     // 移动录像
        HIK_RECTYPE_ALARM = 0x04,    // 告警录像
        HIK_RECTYPE_MANUAL = 0x10,   // 手动录像
        HIK_RECTYPE_ALL = 0xFF,      // 全部类型
    }

    /** @enum PB_STREAM_MODE
    * @brief 取流模式
    */
    public enum PB_STREAM_MODE
    {
        PBSTRM_NONE = 0,         // 没有任何控制
        // bit16~bit31，扩展模式，可相互组合
        PBSTRM_BUFFER = 0x10000, // bit16 过本地缓冲(1)不过缓冲(0)
        PBSTRM_DOWNLOAD = 0x20000, // bit17 下载(1)回放(0)
        // 默认：取流路径自动检测&本地缓冲
        PBSTRM_DEFAULT = PBSTRM_BUFFER,
    }

    /** @enum PB_STREAM_DATATYPE
    * @brief 回放码流的数据类型
    */
    public enum PB_STREAM_DATATYPE
    {
        // 底层库会直接回调上来的标识
        PBDT_FORWARD = 1, // 正放头标记
        PBDT_DATA = 2, // 码流数据
        PBDT_END = 100, // 结束标记(回放、下载或倒放结束)

        // 客户端自己加的额外标记
        PBDT_BACKWARD, // 倒放头标记(正放切换倒放标识)
        PBDT_SEEK_FINISHED, // 定位完成标记

        // 注：切换片段时回调中的长度参数转义为切换片段的开始时间，便于上层定位片段
        PBDT_SWITCH_TO_NEXT, // 切换到下个片段
        PBDT_SWITCH_TO_NEXT_FAIL, // 切换到下个片段失败

        PBDT_DRY_UP, // 码流断绝的标记
        PBDT_FILTER_NULL, // 过滤类型后无可用数据
        PBDT_CLOUDDOWNLOAD_FILESIZE, //云存储录像文件大小
    }

    /**@enum PB_STREAM_MSG_ID
    * @brief 回放取流的消息定义
    */
    public enum PB_STREAM_MSG_ID
    {
        MSG_SEEK_FAILED, // 定位失败
        MSG_SEEK_RETURN_FAILED, // 定位失败后返回原位也失败
        MSG_SEEK_RETURN_FINISHED, // 定位失败后返回了原位
        MSG_SEEK_FINISHED, // 定位成功完成
    }


    public class HikPlatSDK
    {
        //播放控制
        public const int PLAY_START = 10001; //开始播放 
        public const int PLAY_PAUSE = 10002; //暂停播放
        public const int PLAY_FAST = 10003; //加快播放速度
        public const int PLAY_SLOW = 10004; //减慢播放速度
        public const int PLAY_OFFSET = 10005; //播放定位
        public const int PLAY_ONEFRAMEFORWARD = 10006; //单帧进
        public const int PLAY_OneFRAMEBACKWARD = 10007; //单帧退

        public const string SDK_PATH = @".\driver\hikplat\HikPlatformSDK.dll";
        public static bool bInit = false;
        public static bool Init()
        {
            if (HikPt_Init() == 0)
            {
                bInit = true;
                return true;
            }
            else
                return false;
        }
        public static void Uninit()
        {
            if (bInit)
            {
                HikPt_Uninit();
            }
            bInit = false;
        }
        //用户登录信息


        /** @fn HikPt_Init()
        *  @brief 初始化SDK
        *  @param void
        *  @return int 0表示成功， 其他表示失败
        */
        [DllImport(SDK_PATH, CallingConvention = CallingConvention.StdCall)]
        public static extern int HikPt_Init();
        /** @fn HikPt_Uninit()
        *  @brief 反初始化SDK
        *  @param void
        *  @return int 0表示成功， 其他表示失败
        */
        [DllImport(SDK_PATH, CallingConvention = CallingConvention.StdCall)]
        public static extern int HikPt_Uninit();
        /** @fn      HikPt_Login()
        *   @brief   登录中心平台
        *   @param   HikLoginInfo* pLoginInfo：登录信息结构体，包括平台IP，端口，用户名，密码
        *   @return  int 0表示成功， 其他表示失败
        */
        [DllImport(SDK_PATH, CallingConvention = CallingConvention.StdCall)]
        public static extern int HikPt_Login(ref HikLoginInfo pLoginInfo);

        /** @fn    HikPt_Logout()
        *  @brief  退出登录平台
        *  @param  void
        *  @return int 0表示成功， 其他表示失败
        */
        [DllImport(SDK_PATH, CallingConvention = CallingConvention.StdCall)]
        public static extern int HikPt_Logout();

        /** @fn   HikPt_StartPlayView()
         *  @brief 开始实时预览
         *  @param const char*  szCameraIndexCode：监控点编号
         *  @param void* hWnd：播放窗口句柄，窗口句柄为NULL只回调不解码
         *  @param pStreamCallback pFun：数据回调
         *  @param void* pUserData：用户数据
         *  @return long 预览实例会话ID
         */
        [DllImport(SDK_PATH, CallingConvention = CallingConvention.StdCall)]
        public static extern int HikPt_StartPlayView(string szCameraIndexCode, IntPtr hWnd, pStreamCallback pFun, IntPtr pUserData);

        /** @fn   HikPt_StopPlayView()
        *  @brief 停止实时预览
        *  @param long lSession 预览实例session
        *  @return int 0 表示成功， 其他表示失败
        */
        [DllImport(SDK_PATH, CallingConvention = CallingConvention.StdCall)]
        public static extern int HikPt_StopPlayView(Int32 lSession);

        /** @fn    HikPt_StartRecord()
        *  @brief  开始预览本地录像
        *  @param long lSession：预览实例session
        *  @param HikRecordCfgParam pRecordParam：录像配置参数
        *  @param pPreviewRecordEndCallback pfunrecord：录像结束回调函数，调用HikPt_StopRecord时触发该回调
        *  @param pUserData：用户数据
        *  @return int 0 表示成功， 其他表示失败
        */
        [DllImport(SDK_PATH, CallingConvention = CallingConvention.StdCall)]
        public static extern int HikPt_StartRecord(Int32 lSession, ref HikRecordCfgParam pRecordParam, pPreviewRecordEndCallback pfunrecord, IntPtr pUserData);

        /** @fn    HikPt_StopRecord()
        *  @brief  停止预览本地录像
        *  @param lSession：预览实例
        *  @return int 0 表示成功， 其他表示失败
        */
        [DllImport(SDK_PATH, CallingConvention = CallingConvention.StdCall)]
        public static extern int HikPt_StopRecord(Int32 lSession);

        /** @fn   HikPt_PreviewSnapShot()
        *  @brief 预览视频抓图
        *  @param long lSession：预览实例session
        *  @param pSnapParam：抓图配置参数(参数const char* data：抓图图片保存全路径)
        *  @param pfunsnap：抓图回调函数
        *  @param pUserData：用户数据
        *  @return int 0 表示成功， 其他表示失败
        */
        [DllImport(SDK_PATH, CallingConvention = CallingConvention.StdCall)]
        public static extern int HikPt_PreviewSnapShot(Int32 lSession, ref HikSnapParam pSnapParam, pSnapShotCallback pfunsnap, IntPtr pUserData);

        /** @fn   HikPt_PtzControl()
        *  @brief 云台控制
        *  @param const char* szCameraIndexCode：监控点编号
        *  @param int nPtzCommand：云台控制指令
        *  @param int nAction：开始/停止
        *  @param int nParam：云台控制参数
        *  @param int nSpeed：云台速度（1~7）
        *  @param void* pCruiseInfo 巡航信息
        *  @return int 0表示成功， 其他表示失败
        */
        [DllImport(SDK_PATH, CallingConvention = CallingConvention.StdCall)]
        public static extern int HikPt_PtzControl(string szCameraIndexCode, int nPtzCommand, int nAction, int nParam, int nSpeed, IntPtr pCruiseInfo);

        /** @fn   HikPt_PtzLock()
        *  @brief 云台锁定
        *  @param const char* szCameraIndexCode：监控点编号
        *  @param int nLockSec：锁定时间,单位为：分
        *  @return int 0表示成功， 其他表示失败
        */
        [DllImport(SDK_PATH, CallingConvention = CallingConvention.StdCall)]
        public static extern int HikPt_PtzLock(string szCameraIndexCode, int nLockSec);

        /** @fn   HikPt_PtzUnLock()
        *  @brief 云台解锁
        *  @param const char* szCameraIndexCode：监控点编号
        *  @return int 0表示成功， 其他表示失败
        */
        [DllImport(SDK_PATH, CallingConvention = CallingConvention.StdCall)]
        public static extern int HikPt_PtzUnLock(string szCameraIndexCode);

        /** @fn   HikPt_QueryRecord()
        *  @brief 录像查询(查询到的结果szResultXml通过HikPt_StartPlayBack进行回放)
        *  @param const char* szCameraIndexCode：监控点编号
        *  @param const __int64 lStartTime：录像开始时间
        *  @param const __int64 lEndtime：录像结束时间
        *  @param char* szResultXml：录像查询结果
        *  @param unsigned int recType：录像类型，默认为HIK_RECTYPE_ALL(全部)，其他参照结构体HIK_RECORD_TYPE定义
        *  @return long 录像查询实例session*/
        [DllImport(SDK_PATH, CallingConvention = CallingConvention.StdCall)]
        public static extern long HikPt_QueryRecord(string szCameraIndexCode, Int64 lStartTime, Int64 lEndtime, IntPtr szResultXml, int nXmlSize, uint recType);

        /** @fn    HikPt_CloseQueryRecord()
        *  @brief  关闭录像查询句柄，释放资源
        *  @param long lSession 录像查询实例session
        *  @return int 0 表示成功， 其他表示失败
        */
        [DllImport(SDK_PATH, CallingConvention = CallingConvention.StdCall)]
        public static extern int HikPt_CloseQueryRecord(Int32 lSession);

        /** @fn   HikPt_StartPlayBack()
        *  @brief 开始录像回放
        *  @param void* hWnd：播放窗口句柄
        *  @param const char*szResultXml：回放取流结果集
        *  @param const __int64 lStartTime：开始时间
        *  @param const __int64 lEndtime：停止时间
        *  @param pStreamCallback pFun：码流数据回调
        *  @param void* pUserData：用户数据
        *  @return long 录像回放实例session
        */
        [DllImport(SDK_PATH, CallingConvention = CallingConvention.StdCall)]
        public static extern int HikPt_StartPlayBack(IntPtr hWnd, string szResultXml, Int64 lStartTime, Int64 lEndtime, pStreamCallback pFun, IntPtr pUserData);

        /** @fn    HikPt_StopPlayBack()
        *  @brief  停止录像回放
        *  @param long lSession：录像回放实例session
        *  @return int 0 表示成功， 其他表示失败
        */
        [DllImport(SDK_PATH, CallingConvention = CallingConvention.StdCall)]
        public static extern int HikPt_StopPlayBack(Int32 lSession);

        /** @fn    HikPt_PlayBackControl()
        *  @brief  播放控制,具体命令参照:播放控制宏
        *  @param long lSession：回放实例session
        *  @param int iCommand：播放控制命令,参照:播放控制宏
        *  @param const __int64 iParam：播放控制参数,当播放命令为定位时，iParam传递定位时间
        *  @return int 0 表示成功， 其他表示失败
        */
        [DllImport(SDK_PATH, CallingConvention = CallingConvention.StdCall)]
        public static extern int HikPt_PlayBackControl(Int32 lSession, int iCommand, Int64 iParam);

        /** @fn   HikPt_PlaybackSnapShot()
        *  @brief 录像回放抓图
        *  @param long lSession：预览实例session
        *  @param pSnapParam：抓图配置参数
        *  @param pSnapCallback pfunsnap：抓图回调函数（参数const char* data：抓图图片保存全路径）
        *  @param void* pUserData：用户数据
        *  @return int 0 表示成功， 其他表示失败
        */
        [DllImport(SDK_PATH, CallingConvention = CallingConvention.StdCall)]
        public static extern int HikPt_PlaybackSnapShot(Int32 lSession, ref HikSnapParam pSnapParam, pSnapShotCallback pfunsnap, IntPtr pUserData);

        /** @fn   HikPt_StartDownloadRecord()
        *  @brief 开始录像下载
        *  @param const char*szResultXml：回放取流结果集
        *  @param const __int64 lStartTime：开始时间
        *  @param const __int64 lEndtime：停止时间
        *  @param pStreamCallback pFun：码流数据回调
        *  @param void* pUserData：用户数据
        *  @return long 录像下载实例session
        */
        [DllImport(SDK_PATH, CallingConvention = CallingConvention.StdCall)]
        public static extern int HikPt_StartDownloadRecord(string szResultXml, Int64 lStartTime, Int64 lEndtime, pStreamCallback pFun, IntPtr pUserData);

        /** @fn    HikPt_StopDownloadRecord()
        *  @brief  停止录像下载
        *  @param long lSession：录像回放实例session
        *  @return int 0 表示成功， 其他表示失败
        */
        [DllImport(SDK_PATH, CallingConvention = CallingConvention.StdCall)]
        public static extern int HikPt_StopDownloadRecord(Int32 lSession);


    }
}
