using GHIBMS.Common;
using GHIBMS.DvrSDK;
using System;
using System.IO;
using System.Windows.Forms;

namespace GHIBMS.NetVideo
{
    public class HikDvrRealPlayer : IVideoRealPlayer, IDisposable
    {

        #region 内部变量与属性

        //public  static Hashtable logidTable = new Hashtable();
        private static UInt16 videoWndNubs = 1;
        private HCNetSDK.NET_DVR_SHOWSTRING_V30 PICCFG = new HCNetSDK.NET_DVR_SHOWSTRING_V30();
        private HCNetSDK.NET_DVR_SHOWSTRINGINFO[] m_ShowStringInfo = new HCNetSDK.NET_DVR_SHOWSTRINGINFO[8];
        private HCNetSDK.VOICEDATACALLBACKV30 voiceDataCallBack = null;

        private VideoRealPlayArgs playArgs = new VideoRealPlayArgs();
        private int playID = -1;
        private int loginID = -1;
        private bool bRecording = false;
        private int VoiceComHandle = -1;

        private string protocolCode = "JK_DVR_HK";
        public string ProtocolCode
        {
            get { return protocolCode; }

        }


        public UInt16 VideoWndNubs
        {
            set { videoWndNubs = value; }
            get { return videoWndNubs; }
        }
        public int PlayID
        {
            get { return playID; }
            set { playID = value; }
        }
        #endregion

        #region 事件
        //log事件
        public event OnMessagedelegate OnMessage;
        private void Log(string message)
        {
            if (OnMessage != null)
                OnMessage(this, message);
        }
        public void OnWinMessage(ref System.Windows.Forms.Message m)
        {
        }
        public void SetWinMessage(IntPtr lWinHandle)
        {
        }
        #endregion

        #region 构造与晰构
        //private static IntPtr lptr;
        public HikDvrRealPlayer()
        {
            // voiceDataCallBack = new HCNetSDK.VoiceDataCallBack(fVoiceDataCallBack);
        }
        private bool disposed = false;
        ///
        /// 实现IDisposable中的Dispose方法
        ///

        public void Dispose()
        {
            //必须为true
            Dispose(true);
            //通知垃圾回收机制不再调用终结器(析构器)
            GC.SuppressFinalize(this);

        }
        ///
        　     　/// 必须，以备程序员忘记了显式调用Dispose方法
        　     　///
        ~HikDvrRealPlayer()
        {
            //必须为false
            Dispose(false);

        }
        ///
        　　/// 非密封类修饰用protected virtual
        　　/// 密封类修饰用private
        　　///
        　　///
        private void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            if (disposing)
            {
                // 此处添加清理托管资源
            }

            //------- 此处添加清理非托管资源------------------//

            //if (PlayID > 01)  //停止播放
            //{
            //      DVR_StopRealPlay();
            //}

            //------------------------------------------------//

            //让类型知道自己已经被释放
            disposed = true;
        }

        #endregion

        #region 设备初始化
        /// <summary>
        /// 设置播放参数
        /// </summary>
        /// <param name="args"></param>
        public void SetPlayAgrs(VideoRealPlayArgs args)
        {
            playArgs.Clone(args);
        }
        public bool VIDEO_Init()
        {
            //MessageBox.Show("海康初始化成功");
            if (!HCNetSDK.bInit)
            {

                if (!HCNetSDK.NET_DVR_Init2())
                {
                    Log("海康DVR SDK初始化失败！");
                    return false;
                }
                HCNetSDK.bInit = true;
                HCNetSDK.NET_DVR_SetConnectTime(2000, 1);
                //1:重连
                HCNetSDK.NET_DVR_SetReconnect(5000, 1);
            }
            return true;
        }
        //清理资源
        public void DVR_Cleanup()
        {
            if (HCNetSDK.bInit)
            {
                HCNetSDK.bInit = false;
                HCNetSDK.NET_DVR_Cleanup2();
            }
        }


        #endregion

        #region 设备注册
        private bool DVR_Login()
        {
            VIDEO_Init();
            VideoRealPlayArgs e = playArgs;
            if (e.Ip == "0.0.0.0" || e.Ip == "")
            {
                Log("实时视频播放失败，没有正确设置播放参数！");
                return false;

            }
            //string key=e.Ip + ":" + e.Port.ToString();
            ////已经登录，但登录ID不正确，直接删除重新登录
            //if ( logidTable.ContainsKey(key) && ((HikDvrLoginInfo)logidTable[key]).loginId == -1)
            //{
            //    logidTable.Remove(key);
            //}

            //if (!logidTable.ContainsKey(key))
            //{

            GHIBMS.DvrSDK.HCNetSDK.NET_DVR_DEVICEINFO_V30 lpDeviceInfo = new GHIBMS.DvrSDK.HCNetSDK.NET_DVR_DEVICEINFO_V30();
            //NET_DVR_DEVICEINFO lpDeviceInfo = new NET_DVR_DEVICEINFO();
            if (loginID > -1)
                HCNetSDK.NET_DVR_Logout(loginID);
            loginID = HCNetSDK.NET_DVR_Login_V30(e.Ip, e.Port, e.UserName, e.Password, ref lpDeviceInfo);
            if (loginID > -1)
            {

                //Debug.WriteLine("  HCNetSDK.NET_DVR_Login_V30:" + userId + "  ip:" + e.Ip);
                //  loginID = userId;
                //HikDvrLoginInfo info = new HikDvrLoginInfo();
                //info.loginId = userId;
                //info.DeviceInfo = lpDeviceInfo;
                //logidTable.Add(key, info);
                string msg = String.Format("DVR登录成功！IP：{0} 返回的用户ID:{1}", e.Ip, loginID);
                Log(msg);
                return true;
            }
            else
            {
                Log("DVR登录失败！ IP:" + e.Ip + "  " + e.Port);
                int error = (int)HCNetSDK.NET_DVR_GetLastError();
                string msg = string.Format("海康HCNetSDK.NET_DVR_Login_V30失败!名称：{0},IP:{1},端口号：{2},原因：{3},错误码：{4}", e.CamName, e.Ip, e.Port, GetLoginErrorReason(error), error);
                Logger.GetInstance().LogMsg(msg);
                return false;
            }
            //}
            //else //已经登录过了，直接返回
            //{
            //    return true;
            //}
        }
        string GetLoginErrorReason(int nError)
        {
            if (1 == nError) return "密码不正确";
            else if (2 == nError) return "用户名不存在";
            else if (3 == nError) return "登录超时";
            else if (4 == nError) return "帐号已登录";
            else if (5 == nError) return "帐号已被锁定 ";
            else if (6 == nError) return "帐号被列为黑名单 ";
            else if (7 == nError) return "资源不足，系统忙";
            else if (8 == nError) return "子连接失败";
            else if (9 == nError) return "主连接失败";
            else if (10 == nError) return "超过最大用户连接数";

            else return "错误码：" + nError.ToString();
        }
        //private int Login()
        //{

        //    VideoRealPlayArgs e = playArgs;
        //    string key = e.Ip + ":" + e.Port.ToString();
        //    if (e.Ip == "") return -1;
        //    if (logidTable.ContainsKey(key))
        //    {
        //        //存在正确的登录ID，返回
        //        int id = ((HikDvrLoginInfo)logidTable[key]).loginId;
        //        if (id > -1)
        //            return id;
        //        else
        //        {
        //            //登录ID不正确，移出HT,再次调用自我
        //            logidTable.Remove(key);
        //            DVR_Login();
        //        }
        //    }
        //    else //没有登录
        //    {
        //        DVR_Login();
        //    }
        //    //登录后再次查找logID
        //    if (logidTable.ContainsKey(key))
        //    {
        //        //存在正确的登录ID，返回
        //        int id = ((HikDvrLoginInfo)logidTable[key]).loginId;
        //        if (id > -1)
        //            return id;
        //    }
        //    return -1;
        //}
        //private HikDvrLoginInfo GetLoginDvrInfo()
        //{
        //    VideoRealPlayArgs e = playArgs;
        //    string key = e.Ip + ":" + e.Port.ToString();
        //    if (logidTable.ContainsKey(key))
        //    {
        //        return (HikDvrLoginInfo)logidTable[key];
        //    }
        //    return new HikDvrLoginInfo();
        //}
        public bool DVR_Logout()
        {
            //foreach(DictionaryEntry de in logidTable )
            //{
            //    int id = ((HikDvrLoginInfo)de.Value).loginId;

            if (loginID > -1)
            {
                HCNetSDK.NET_DVR_Logout_V30(loginID);
                loginID = -1;
            }
            //    Debug.WriteLine("  HCNetSDK.NET_DVR_Logout_V30:" + id);

            //}
            //logidTable.Clear();
            return true;
        }
        #endregion

        #region 视频预览
        public bool VIDEO_StartRealPlay()
        {
            try
            {
                if (PlayID > -1)
                {
                    VIDEO_StopRealPlay();
                }

                VideoRealPlayArgs e = playArgs;
                if (e.Ip == "0.0.0.0" || e.Ip == "")
                {
                    Log("实时视频播放失败，没有正确设置播放参数！");
                    return false;
                }
                DVR_Login();

                if (loginID == -1)
                {
                    Log("实时视频播放失败，没有正确登录DVR！");
                    return false;
                }



                HCNetSDK.NET_DVR_PREVIEWINFO lpPreviewInfo = new HCNetSDK.NET_DVR_PREVIEWINFO();
                lpPreviewInfo.hPlayWnd = e.PlayWnd;//预览窗口
                lpPreviewInfo.lChannel = e.DvrCh;//预te览的设备通道
                lpPreviewInfo.dwStreamType = 0;//码流类型：0-主码流，1-子码流，2-码流3，3-码流4，以此类推
                lpPreviewInfo.dwLinkMode = 0;//连接方式：0- TCP方式，1- UDP方式，2- 多播方式，3- RTP方式，4-RTP/RTSP，5-RSTP/HTTP 
                lpPreviewInfo.bBlocked = true; //0- 非阻塞取流，1- 阻塞取流
                lpPreviewInfo.dwDisplayBufNum = 15; //播放库播放缓冲区最大缓冲帧数

                HCNetSDK.REALDATACALLBACK RealData = new HCNetSDK.REALDATACALLBACK(RealDataCallBack);//预览实时流回调函数
                IntPtr pUser = new IntPtr();//用户数据

                //打开预览 Start live view 
                PlayID = HCNetSDK.NET_DVR_RealPlay_V40(loginID, ref lpPreviewInfo, null/*RealData*/, pUser);
                if (PlayID < 0)
                {
                    string msg = String.Format("实时视频播放失败！IP：{0} 通道号:{1},    错误码：{2}", e.Ip, e.DvrCh, GetLoginErrorReason((int)(HCNetSDK.NET_DVR_GetLastError())));
                    Log(msg);
                    return false;
                }
                else
                {
                    HCNetSDK.NET_DVR_ThrowBFrame(PlayID, 2);
                    //}
                    string msg = String.Format("实时视频播放成功！IP：{0} 通道号:{1}", e.Ip, e.DvrCh);
                    Log(msg);
                    return true;
                }


            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
                Console.WriteLine(ex.ToString());
            }
            return false;
        }
        public void RealDataCallBack(Int32 lRealHandle, UInt32 dwDataType, ref byte pBuffer, UInt32 dwBufSize, IntPtr pUser)
        {
        }
        public bool VIDEO_StopRealPlay()
        {
            bool ret = false;
            try
            {
                //如果正在录像，先停止
                if (bRecording)
                    VIDEO_StopSaveRealData();

                VideoRealPlayArgs e = playArgs;


                if (PlayID > -1)
                {
                    ret = HCNetSDK.NET_DVR_StopRealPlay(PlayID);
                }
                if (ret)
                {
                    PlayID = -1;

                }
                else
                {
                    string msg = String.Format("实时视频停止失败！IP：{0} 通道号:{1}", e.Ip, e.DvrCh);
                    Log(msg);
                    PlayID = -1;

                }
                DVR_Logout();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return ret;
        }
        public void RealDataCallBack_V30(int lRealHandle, uint dwDataType, byte[] pBuffer, uint dwBufSize, IntPtr pUser)
        {
        }
        private bool DVR_ThrowBFrame(int lRealHandle, uint dwNum)
        {
            return HCNetSDK.NET_DVR_ThrowBFrame(lRealHandle, dwNum);
        }


        #endregion

        #region 抓拍与录像
        public bool VIDEO_SaveRealData(string fileName)
        {
            VideoRealPlayArgs e = playArgs;

            if (loginID == -1)
            {
                Log("视频录像失败，没有正确登录设备！");
                return false;
            }
            if (PlayID == -1)
            {
                Log("视频录像失败，视频没有正常播放！");
                return false;
            }

            string time = System.DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string path;
            if (!NetVideoControl.RecPath.EndsWith("\\"))
                path = NetVideoControl.RecPath + "\\" + pubFun.DateStr + "\\";
            else
                path = NetVideoControl.RecPath + pubFun.DateStr + "\\";
            if (!GetDriver(path.Substring(0, 1)))
            {
                MessageBox.Show("该磁盘不存在!");
                return false;
            }
            if (Directory.Exists(path) == false)
            {
                // MessageBox.Show("该目录不存在，自动创建‘" + textBox1.Text + "\\’" + "目录");
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch { MessageBox.Show("创建录像文件失败!"); }
            }




            string sPicFileName = String.Format("{0}{1}_{2}_{3}_{4}.mp4", path, fileName, playArgs.Ip, playArgs.DvrCh, time);
            //  uint STREAM_HIK = 0;
            //  if (!HCNetSDK.NET_DVR_SaveRealData_V30(PlayID, STREAM_HIK, sPicFileName))
            if (!HCNetSDK.NET_DVR_SaveRealData(PlayID, sPicFileName))
            {
                string msg = String.Format("视频录像失败！IP：{0} 通道号:{1}", playArgs.Ip, playArgs.DvrCh);
                Log(msg);
                bRecording = false;
                return false;
            }
            else
            {
                bRecording = true;
                string msg = String.Format("视频录像开始！IP：{0} 通道号:{1}", playArgs.Ip, playArgs.DvrCh);
                Log(msg);
                return true;
            }
            // HCNetSDK.NET_DVR_SaveRealData_V30(lRealHandle, dwTransType, sFileName);
        }
        public bool VIDEO_StopSaveRealData()
        {
            if (PlayID > -1 && bRecording)
            {
                if (!HCNetSDK.NET_DVR_StopSaveRealData(PlayID))
                {
                    string msg = String.Format("视频录像停止失败！IP：{0} 通道号:{1}", playArgs.Ip, playArgs.DvrCh);
                    Log(msg);
                    bRecording = false;
                    return false;
                }
                else
                {
                    string msg = String.Format("视频录像停止成功！IP：{0} 通道号:{1}", playArgs.Ip, playArgs.DvrCh);
                    Log(msg);
                    bRecording = false;
                    return true;
                }
            }
            return true;
        }
        public bool VIDEO_CapturePicture(string fileName)
        {
            VideoRealPlayArgs e = playArgs;
            if (e == null)
            {
                Log("实时视频抓拍失败，没有正确设置播放参数！");
                return false;
            }

            if (loginID == -1) return false;
            string time = System.DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string path;
            if (!NetVideoControl.PicPath.EndsWith("\\"))
                path = NetVideoControl.PicPath + "\\" + pubFun.DateStr + "\\";
            else
                path = NetVideoControl.PicPath + pubFun.DateStr + "\\";
            string date = pubFun.DateStr;
            if (Directory.Exists(path) == false)
            {
                // MessageBox.Show("该目录不存在，自动创建‘" + textBox1.Text + "\\’" + "目录");
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch { }
            }

            string sPicFileName = String.Format("{0}{1}_{2}_{3}_{4}.bmp", path, fileName, playArgs.Ip, playArgs.DvrCh, time);

            if (!HCNetSDK.NET_DVR_CapturePicture(PlayID, sPicFileName))
            {
                string msg = String.Format("图片抓拍失败！IP：{0} 通道号:{1}", playArgs.Ip, playArgs.DvrCh);
                Log(msg);
                return false;
            }
            //pubFun.ShowFileDirectory(sPicFileName);
            return false;
        }
        public bool VIDEO_CaptureJPEGPicture(string fileName)
        {
            VideoRealPlayArgs e = playArgs;

            if (loginID == -1) return false;
            string time = System.DateTime.Now.ToString("yyyyMMdd_HHmmss_fff");
            string path;
            if (!NetVideoControl.PicPath.EndsWith("\\"))
                path = NetVideoControl.PicPath + "\\" + pubFun.DateStr + "\\";
            else
                path = NetVideoControl.PicPath + pubFun.DateStr + "\\";
            if (!GetDriver(path.Substring(0, 1)))
            {
                MessageBox.Show("该磁盘不存在!");
                return false;
            }
            string date = pubFun.DateStr;
            if (Directory.Exists(path) == false)
            {
                // MessageBox.Show("该目录不存在，自动创建‘" + textBox1.Text + "\\’" + "目录");
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch { }
            }

            string sPicFileName = String.Format("{0}{1}_{2}_{3}_{4}.jpg", path, fileName, playArgs.Ip, playArgs.DvrCh, time);
            //Console.WriteLine("___"+sPicFileName);
            HCNetSDK.NET_DVR_JPEGPARA lpJpegPara = new HCNetSDK.NET_DVR_JPEGPARA();

            lpJpegPara.wPicQuality = 0; //图像质量 Image quality
            lpJpegPara.wPicSize = 0xff; //抓图分辨率 Picture size: 0xff-Auto(使用当前码流分辨率) 
            //抓图分辨率需要设备支持，更多取值请参考SDK文档



            if (!HCNetSDK.NET_DVR_CaptureJPEGPicture(loginID, e.DvrCh, ref lpJpegPara, sPicFileName))
            {
                string msg = String.Format("图片抓拍失败！IP：{0} 通道号:{1},错误码：{2}", playArgs.Ip, playArgs.DvrCh, HCNetSDK.NET_DVR_GetLastError());
                Log(msg);
                return false;
            }
            //pubFun.ShowFileDirectory(sPicFileName);
            return false;
        }

        #endregion

        #region 录像文件回放、下载

        /*public  int DVR_SearchFile(int UerID, int lChannel, uint m_iFileType, DateTime dtStar, DateTime dtEnd, ListView list,Label lblSearchState,Button btnSearch)
        {
          
             lUserID = UerID;
             lsv = list;
             _lblSearch = lblSearchState;
             _btnSearch = btnSearch;
             if (!m_bSearching)
             {

                 HCNetSDK.NET_DVR_TIME StartTime = new HCNetSDK.NET_DVR_TIME();
                 HCNetSDK.NET_DVR_TIME StopTime = new HCNetSDK.NET_DVR_TIME();

                 StartTime.dwYear = (uint)dtStar.Year;
                 StartTime.dwMonth = (uint)dtStar.Month;
                 StartTime.dwDay = (uint)dtStar.Day;
                 StartTime.dwHour = (uint)dtStar.Hour;
                 StartTime.dwMinute = (uint)dtStar.Minute;
                 StartTime.dwSecond = (uint)dtStar.Second;
                 StopTime.dwYear = (uint)dtEnd.Year;
                 StopTime.dwMonth = (uint)dtEnd.Month;
                 StopTime.dwDay = (uint)dtEnd.Day;
                 StopTime.dwHour = (uint)dtEnd.Hour;
                 StopTime.dwMinute = (uint)dtEnd.Minute;
                 StopTime.dwSecond = (uint)dtEnd.Second;
                 uint dwFileType = 0xFF;
                 switch (m_iFileType)
                 {
                     case 0:
                         dwFileType = 0xFF;
                         break;
                     case 1:
                         dwFileType = 0;
                         break;

                     case 2:
                         dwFileType = 1;
                         break;
                     case 3:
                         dwFileType = 2;
                         break;
                 }

                 byte[] sCardNumber = new byte[32];
                 m_struFileCond.lChannel = lChannel;
                 m_struFileCond.dwFileType = dwFileType;
                 m_struFileCond.dwIsLocked = 0xFF;
                 m_struFileCond.sCardNumber = sCardNumber;
                 m_struFileCond.dwUseCardNo = 0;
                 m_struFileCond.struStartTime = StartTime;
                 m_struFileCond.struStopTime = StopTime;
                 m_lFileHandle = HCNetSDK.NET_DVR_FindFile_V30(lUserID, ref m_struFileCond);
                 if (m_lFileHandle != -1)
                 {
                     Thread_SearchFile = new Thread(new ThreadStart(ThreadSearchFile));
                     Thread_SearchFile.Start();
                 }

             }
             else
             {
                 if (Thread_SearchFile != null)
                     Thread_SearchFile.Abort();
             
             }
             return m_lFileHandle;
        }
        public override bool DVR_FindClose(int lFindHandle)
        {
            return HCNetSDK.NET_DVR_FindClose(lFindHandle);
        }
        private void ThreadSearchFile()
        {
            /*
         
            //  int m_lFileHandle = HCNetSDK.NET_DVR_FindFile(lUserID, 1, 0xFF, ref StartTime,ref  StopTime);

            if (m_lFileHandle != -1)
            {
                HCNetSDK.NET_DVR_FINDDATA_V30 struFileInfo = new HCNetSDK.NET_DVR_FINDDATA_V30();
                while (true)
                {
                    int lRet = HCNetSDK.NET_DVR_FindNextFile_V30(m_lFileHandle, out struFileInfo);
                    if (lRet == HCNetSDK.NET_DVR_FILE_SUCCESS)
                    {
                        //显示文件列表
                        UpdateListView updateListView = new UpdateListView();
                        updateListView.lsv = lsv;
                        string starttime = struFileInfo.struStartTime.dwYear + "-" + struFileInfo.struStartTime.dwMonth + "-" + struFileInfo.struStartTime.dwDay
                                           + " " + struFileInfo.struStartTime.dwHour + ":" + struFileInfo.struStartTime.dwMinute + ":" + struFileInfo.struStartTime.dwSecond;
                        string endtime = struFileInfo.struStopTime.dwYear + "-" + struFileInfo.struStopTime.dwMonth + "-" + struFileInfo.struStopTime.dwDay
                                           + " " + struFileInfo.struStopTime.dwHour + ":" + struFileInfo.struStopTime.dwMinute + ":" + struFileInfo.struStopTime.dwSecond;

                        string filesize;
                        if (struFileInfo.dwFileSize / 1024 == 0)
                        {
                            filesize = struFileInfo.dwFileSize.ToString();
                        }
                        else if (struFileInfo.dwFileSize / 1024 > 0 && struFileInfo.dwFileSize / (1024 * 1024) == 0)
                        {
                            filesize = (struFileInfo.dwFileSize / 1024).ToString() + "K";
                        }
                        else// if ()
                        {
                            filesize = (struFileInfo.dwFileSize / 1024 / 1024).ToString() + "M";
                        }

                        string filename = struFileInfo.sFileName;
                        updateListView.AddItem(Convert.ToDateTime(starttime).ToString(), Convert.ToDateTime(endtime).ToString(), filesize, filename);

                    }
                    else
                    {
                        if (lRet == HCNetSDK.NET_DVR_ISFINDING)
                        {
                            Thread.Sleep(5);
                            continue;
                        }
                        if ((lRet == HCNetSDK.NET_DVR_NOMOREFILE) || (lRet == HCNetSDK.NET_DVR_FILE_NOFIND))
                        {
                            UpdateListView updateListView = new UpdateListView();
                            updateListView.lbl = _lblSearch;
                            updateListView.btn = _btnSearch;
                            updateListView.SetLabelText("获取文件列表结束");
                            updateListView.SetButtonText("查找");
                            m_bSearching = false;
                            break;

                        }
                        else
                        {
                            UpdateListView updateListView = new UpdateListView();
                            updateListView.lbl = _lblSearch;
                            updateListView.btn = _btnSearch;
                            updateListView.SetLabelText("获取文件列表异常终止");
                            updateListView.SetButtonText("查找");
                            m_bSearching = false;
                            break;

                        }

                    }
                }
            }
            else 
            { 
                   UpdateListView updateListView = new UpdateListView();
                   updateListView.lbl = _lblSearch;
                   updateListView.SetLabelText(" 获取文件列表失败!");
                   updateListView.btn = _btnSearch;
                   updateListView.SetButtonText("查找");
                   m_bSearching = false;
            
            }
        
        
        }
        public override int  DVR_PlayBackByName(int lUserID, string sPlayBackFileName, IntPtr hWnd)
        {
           return HCNetSDK.NET_DVR_PlayBackByName(lUserID, sPlayBackFileName, hWnd);
        
        }
        public override bool DVR_StopPlayBack(int lPlayHandle)
        {
            return HCNetSDK.NET_DVR_StopPlayBack(lPlayHandle);
        }
        public override bool DVR_PlayBackControl(int lPlayHandle, uint dwControlCode, uint dwInValue, out uint LPOutValue)
        {
            return HCNetSDK.NET_DVR_PlayBackControl(lPlayHandle, dwControlCode, dwInValue, out  LPOutValue);
        }
        public override bool DVR_RefreshPlay(int lPlayHandle)
        { 
            return HCNetSDK.NET_DVR_RefreshPlay(lPlayHandle);
        }
        public override bool DVR_PlayBackCaptureFile(int lPlayHandle, string sFileName)
        {
            return HCNetSDK.NET_DVR_PlayBackCaptureFile(lPlayHandle, sFileName);
        
        }
        public override int DVR_GetFileByName(int lUserID, string sDVRFileName, string sSavedFileName)
        {
            return HCNetSDK.NET_DVR_GetFileByName(lUserID, sDVRFileName, sSavedFileName);
        }
        public override bool DVR_StopGetFile(int lFileHandle)
        { 
            return HCNetSDK.NET_DVR_StopGetFile( lFileHandle);
        }
        public override int DVR_GetDownloadPos(int lFileHandle)
        { 
            return HCNetSDK.NET_DVR_GetDownloadPos(lFileHandle);
          
        }
        public void DVR_SetShow(int lUserID, int lChannel, string strShow, int iLine)
        {
            byte[] source = new byte[44];
            byte[] destin = new byte[44];
            for (int i = 0; i < 8; i++)
            {
                if (i == iLine)
                {
                    source = System.Text.Encoding.Default.GetBytes(strShow);
                    int len;
                    if (source.Length > 44)
                        len = 44;
                    else
                        len = source.Length;
                    System.Array.Copy(source, destin, len);
                    m_ShowStringInfo[i].wStringSize = Convert.ToUInt16(destin.Length);
                    m_ShowStringInfo[i].sString = destin;
                    PICCFG.struStringInfo[i] = m_ShowStringInfo[i];
                    break;
                }
            }
           
            PICCFG.dwSize = (uint)Marshal.SizeOf(PICCFG);

            int size = Marshal.SizeOf(PICCFG);//返回对象的大小
            lptr = Marshal.AllocHGlobal(size);//根据大小分配内存
            Marshal.StructureToPtr(PICCFG, lptr, false);   // 将数据送到内存块
            if (HCNetSDK.NET_DVR_SetDVRConfig(lUserID, 1031, 1, lptr, (uint)size)) ;
            {
                // MessageBox.Show("OK");
                //log.Debug("叠加字符：:" + var.Name + "value:" + var.Value);

            }
           // Marshal.FreeHGlobal(lptr);

        }*/
        #endregion

        #region 云台控制
        public bool VIDEO_PTZControlWithSpeed(uint dwPTZCommand, uint dwStop, uint dwSpeed)
        {
            if (!HCNetSDK.NET_DVR_PTZControlWithSpeed(playID, dwPTZCommand, dwStop, dwSpeed))
            {
                string msg = String.Format("云台控制失败！IP：{0} 通道号:{1}", playArgs.Ip, playArgs.DvrCh);
                Log(msg);
                return false;
            }
            else
                return true;
        }
        public bool VIDEO_PTZPreset(uint dwPTZCommand, uint PresetIndex)
        {
            if (!HCNetSDK.NET_DVR_PTZPreset_EX(playID, dwPTZCommand, PresetIndex))
            {
                string msg = String.Format("云台预置位控制失败！IP：{0} 通道号:{1}", playArgs.Ip, playArgs.DvrCh);
                Log(msg);
                return false;
            }
            else

                return true;

        }
        public bool VIDEO_PTZCruise(uint dwPTZCommand, byte CruiseRoute, byte CruisePoint, ushort Input)
        {
            if (!HCNetSDK.NET_DVR_PTZCruise(playID, dwPTZCommand, CruiseRoute, CruisePoint, Input))
            {
                string msg = String.Format("云台巡航控制失败！IP：{0} 通道号:{1}", playArgs.Ip, playArgs.DvrCh);
                Log(msg);
                return false;

            }
            else
                return true;
        }
        public bool VIDEO_PTZTrack(uint dwPTZCommand, uint dwTrackIndex)
        {
            if (!HCNetSDK.NET_DVR_PTZTrack(playID, dwPTZCommand))
            {
                string msg = String.Format("云台轨迹控制失败！IP：{0} 通道号:{1}", playArgs.Ip, playArgs.DvrCh);
                Log(msg);
                return false;

            }
            else
                return true;

        }
        public static bool GetDriver(string disk)
        {
            string[] s = Directory.GetLogicalDrives();

            foreach (string str in s)
            {
                if (str.ToLower().StartsWith(disk.ToLower()))
                    return true;
            }
            return false;
        }
        #endregion

        #region 字符叠加

        private void iniShowStringPos()
        {
            //初始化
            PICCFG.struStringInfo = new HCNetSDK.NET_DVR_SHOWSTRINGINFO[8];

            for (int i = 0; i < 4; i++)
            {
                //string s = "";

                /*byte[] source = new byte[44];
                byte[] destin = new byte[44];

                source = System.Text.Encoding.Default.GetBytes(s);
                int len;
                if (source.Length > 44)
                    len = 44;
                else
                    len = source.Length;
                System.Array.Copy(source, destin, len);
                */

                //m_ShowStringInfo[i].wShowString = 1;
                //m_ShowStringInfo[i].wStringSize = Convert.ToUInt16(destin.Length);
                //m_ShowStringInfo[i].wShowStringTopLeftX = 0;
                //m_ShowStringInfo[i].wShowStringTopLeftY = Convert.ToUInt16(350 + i * 50);
                //m_ShowStringInfo[i].sString = "test";

                //PICCFG.struStringInfo[i] = m_ShowStringInfo[i];


            }
            //string ll = Encoding.Default.GetString(PICCFG.struStringInfo[0].sString);

            //MessageBox.Show(ll);

        }
        #region 视频参数

        public VideoEffect VIDEO_GetVideoEffect()
        {
            if (PlayID == -1) return null;
            uint pBrightValue = 0;
            uint pContrastValue = 0;
            uint pSaturationValue = 0;
            uint pHueValue = 0;
            VideoEffect ef = new VideoEffect();
            if (!HCNetSDK.NET_DVR_ClientGetVideoEffect(PlayID, ref pBrightValue, ref pContrastValue, ref pSaturationValue, ref pHueValue))
            {
                int error = (int)HCNetSDK.NET_DVR_GetLastError();
                string msg = string.Format("NET_DVR_ClientGetVideoEffect失败!error:{0}", error);
                Logger.GetInstance().LogMsg(msg);
                return null;
            }
            else
            {
                Logger.GetInstance().LogMsg("NET_DVR_ClientGetVideoEffect成功");
            }
            ef.BrightValue = pBrightValue;
            ef.ContrastValue = pContrastValue;
            ef.SaturationValue = pSaturationValue;
            ef.HueValue = pHueValue;
            return ef;
        }
        public bool VIDEO_SetVideoEffect(VideoEffect effect)
        {
            if (PlayID == -1) return false;
            if (HCNetSDK.NET_DVR_ClientSetVideoEffect(PlayID, effect.BrightValue, effect.ContrastValue, effect.SaturationValue, effect.HueValue))
            {
                Logger.GetInstance().LogMsg("NET_DVR_ClientSetVideoEffect成功！");
                return true;
            }
            else
            {
                int error = (int)HCNetSDK.NET_DVR_GetLastError();
                string msg = string.Format("NET_DVR_ClientSetVideoEffect失败!error:{0}", error);
                Logger.GetInstance().LogMsg(msg);
                return false;
            }
        }

        #endregion

        #region 声音、对讲

        public bool VIDEO_OpenSound()
        {
            if (PlayID > -1)
            {
                if (HCNetSDK.NET_DVR_OpenSound(PlayID))
                {
                    return true;
                }
            }
            return false;

        }
        public bool VIDEO_CloseSound()
        {
            if (HCNetSDK.NET_DVR_CloseSound())
            {
                return true;
            }
            else
                return false;

        }
        public bool VIDEO_Volume(ushort wVolume)
        {
            if (PlayID > -1)
            {
                if (HCNetSDK.NET_DVR_Volume(PlayID, wVolume))
                    return true;
            }
            return false;
        }
        public bool VIDEO_StartVoiceCom()
        {

            if (loginID == -1)
            {
                Log("语音对讲失败，没有正确登录DVR！");
                return false;
            }
            IntPtr pUser = new IntPtr();
            VoiceComHandle = HCNetSDK.NET_DVR_StartVoiceCom_V30(loginID, 1, false, voiceDataCallBack, pUser);
            // VoiceComHandle = HCNetSDK.NET_DVR_StartVoiceCom(logID,voiceDataCallBack, 0);
            if (VoiceComHandle > -1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool VIDEO_StopVoiceCom()
        {
            if (VoiceComHandle > -1)
            {
                if (HCNetSDK.NET_DVR_StopVoiceCom(VoiceComHandle))
                    return true;
            }
            return false;

        }
        public bool VIDEO_SetVoiceComClientVolume(ushort wVolume)
        {
            if (VoiceComHandle > -1)
            {
                if (HCNetSDK.NET_DVR_SetVoiceComClientVolume(VoiceComHandle, wVolume))
                    return true;
            }
            return false;

        }
        public void fVoiceDataCallBack(int lVoiceComHandle, string pRecvDataBuffer, uint dwBufSize, byte byAudioFlag, uint dwUser)
        {

        }


        #endregion
        #endregion

        public bool VIDEO_PlayControl(RealPlayControlEnum State)
        {
            if (State == RealPlayControlEnum.PLAY)
                return VIDEO_StartRealPlay();
            else
                return VIDEO_StopRealPlay();
        }
    }

}
