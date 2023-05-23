using DHNetSDK;
using GHIBMS.Common;
using GHIBMS.DvrSDK;
using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace GHIBMS.NetVideo
{
    public class DHDvrRealPlayer : IVideoRealPlayer, IDisposable
    {

        #region 内部变量与属性
        private fDisConnect disConnect;
        private fHaveReConnect haveReConnect;
        private NET_DEVICEINFO deviceInfo;
        private static bool hasInit = false;
        private static Hashtable logidTable = new Hashtable();
        private static UInt16 videoWndNubs = 1;

        private VideoRealPlayArgs playArgs = new VideoRealPlayArgs();
        private int playID = -1;
        private int loginID = -1;
        private bool bRecording = false;
        private int VoiceComHandle = -1;

        private string protocolCode = "JK_DVR_DH";
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

        /// <summary>
        /// 设备断开连接处理
        /// </summary>
        /// <param name="lLoginID"></param>
        /// <param name="pchDVRIP"></param>
        /// <param name="nDVRPort"></param>
        /// <param name="dwUser"></param>
        private void DisConnectEvent(int lLoginID, StringBuilder pchDVRIP, int nDVRPort, IntPtr dwUser)
        {
            //设备断开连接处理            
            // MessageBox.Show("设备用户断开连接", pMsgTitle);
            if (OnMessage != null)
                OnMessage(this, "设备用户断开连接!IP:" + pchDVRIP + " Port:" + nDVRPort.ToString());
        }
        //设备断开重连
        private void HaveReConnect(int lLoginID, StringBuilder pchDVRIP, int nDVRPort, IntPtr dwUser)
        {

        }
        private void HaveReConnectEvent(int lLoginID, StringBuilder pchDVRIP, int nDVRPort, IntPtr dwUser)
        {
            //设备断开连接处理            
            // MessageBox.Show("设备用户断开连接", pMsgTitle);
            if (OnMessage != null)
                OnMessage(this, "设备用户重新连接!IP:" + pchDVRIP + " Port:" + nDVRPort.ToString());
        }
        #endregion

        #region 构造与晰构
        //private static IntPtr lptr;
        public DHDvrRealPlayer()
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
        ~DHDvrRealPlayer()
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
            if (!hasInit)
            {
                hasInit = true;
                disConnect = new fDisConnect(DisConnectEvent);
                if (!DHClient.DHInit(null, IntPtr.Zero))
                {
                    Log("大华DVR SDK初始化失败！");
                    return false;
                }
                haveReConnect = new fHaveReConnect(HaveReConnect);
                DHClient.DHSetAutoReconnect(haveReConnect, IntPtr.Zero);
                DHClient.DHSetEncoding(LANGUAGE_ENCODING.gb2312);//字符编码格式设置，默认为gb2312字符编码，如果为其他字符编码请设置            
                DHClient.DHSetConnectTime(3000, 1);
            }
            return true;
        }
        //清理资源
        public void DVR_Cleanup()
        {
            DVR_Logout();
            if (hasInit)
            {
                hasInit = false;
                DHClient.DHCleanup();
            }
        }


        #endregion

        #region 设备注册
        private bool DVR_Login()
        {
            VIDEO_Init();
            VideoRealPlayArgs e = playArgs;
            if (e.Ip == "0.0.0.0")
            {
                Log("实时视频播放失败，没有正确设置播放参数！");
                return false;

            }
            if (logidTable.ContainsKey(e.Ip) && int.Parse(logidTable[e.Ip].ToString()) == 0)
            {
                logidTable.Remove(e.Ip);
            }
            if (!logidTable.ContainsKey(e.Ip))
            {

                deviceInfo = new NET_DEVICEINFO();
                int error = 0;

                int userId = DHClient.DHLogin(e.Ip, e.Port, e.UserName, e.Password, out deviceInfo, out error);
                if (userId > 0)
                {
                    loginID = userId;
                    logidTable.Add(e.Ip, userId);
                    string msg = String.Format("DVR登录成功！IP：{0} 返回的用户ID:{1}", e.Ip, userId);
                    Log(msg);
                    return true;
                }
                else
                {
                    Log("DVR登录失败！ IP:" + e.Ip);
                    return false;
                }
            }
            return false;
        }
        private int GetLogID()
        {

            VideoRealPlayArgs e = playArgs;

            if (logidTable.ContainsKey(e.Ip))
            {
                //存在正确的登录ID，返回
                int id = int.Parse(logidTable[e.Ip].ToString());
                if (id > 0)
                    return id;
                else
                {
                    //登录ID不正确，移出HT,再次调用自我
                    logidTable.Remove(e.Ip);
                    DVR_Login();
                }
            }
            else //没有登录
            {
                DVR_Login();
            }
            //登录后再次查找logID
            if (logidTable.ContainsKey(e.Ip))
            {
                //存在正确的登录ID，返回
                int id = int.Parse(logidTable[e.Ip].ToString());
                if (id > 0)
                    return id;
            }
            return 0;
        }
        public static bool DVR_Logout()
        {
            foreach (DictionaryEntry de in logidTable)
            {
                int logid = int.Parse(de.Value.ToString());
                string ip = de.Key.ToString();
                if (logid > 0)
                {
                    if (!DHClient.DHLogout(logid))
                    {

                    }
                }

            }
            logidTable.Clear();
            return true;
        }
        #endregion

        #region 视频预览
        public bool VIDEO_StartRealPlay()
        {
            try
            {
                if (PlayID > 0)
                {
                    VIDEO_StopRealPlay();
                }

                VideoRealPlayArgs e = playArgs;
                if (e.Ip == "0.0.0.0")
                {
                    Log("实时视频播放失败，没有正确设置播放参数！");
                    return false;
                }
                loginID = GetLogID();
                if (loginID == 0)
                {
                    Log("实时视频播放失败，没有正确登录DVR！");
                    return false;
                }

                Int32 m_playID = -1;

                REALPLAY_TYPE realPlayType;
                if (e.EncodeMode == EncodeTypeEnum.子码流)
                    realPlayType = REALPLAY_TYPE.DH_RType_RealPlay_1;
                else
                    realPlayType = REALPLAY_TYPE.DH_RType_RealPlay;

                m_playID = DHClient.DHRealPlayEx(loginID, e.DvrCh - 1, realPlayType, e.PlayWnd);
                Debug.WriteLine(string.Format("logid:{0},dvrch:{1},wnd:{2},ret:{3}", loginID, e.DvrCh - 1, e.PlayWnd, m_playID));
                if (m_playID > 0)
                {
                    PlayID = m_playID;
                    return true;
                }
                else
                {

                    string err = DHClient.DHGetLastError();
                    string msg = String.Format("实时视频播放失败！IP：{0} 通道号:{1}，错误:{2}", e.Ip, e.DvrCh, err);

                    Log(msg);
                    Debug.WriteLine(err);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return false;
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


                if (PlayID > 0)
                {
                    ret = DHClient.DHStopRealPlayEx(PlayID);

                }
                if (ret)
                {
                    PlayID = 0;

                }
                else
                {
                    string msg = String.Format("实时视频停止失败！IP：{0} 通道号:{1}", e.Ip, e.DvrCh);
                    Log(msg);
                    PlayID = 0;

                }
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
            // return HCNetSDK.NET_DVR_ThrowBFrame(lRealHandle, dwNum);
            return false;
        }


        #endregion

        #region 抓拍与录像
        public bool VIDEO_SaveRealData(string fileName)
        {
            VideoRealPlayArgs e = playArgs;

            int logid = GetLogID();
            if (logid == 0)
            {
                Log("视频录像失败，没有正确登录设备！");
                return false;
            }
            if (PlayID == 0)
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
            string date = pubFun.DateStr;
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

            string sPicFileName = String.Format("{0}{1}_{2}_{3}_{4}.MPG", path, fileName, playArgs.Ip, playArgs.DvrCh, time);

            if (!DHClient.DHStartSaveRealData(PlayID, sPicFileName))
            {
                string msg = String.Format("视频录像失败！IP：{0} 通道号:{1}", playArgs.Ip, playArgs.DvrCh);
                Log(msg);
                bRecording = false;
                return false;
            }
            else
            {
                string msg = String.Format("视频录像开始！IP：{0} 通道号:{1}", playArgs.Ip, playArgs.DvrCh);
                Log(msg);
                bRecording = true;
                return true;
            }
            // HCNetSDK.NET_DVR_SaveRealData_V30(lRealHandle, dwTransType, sFileName);
        }
        public bool VIDEO_StopSaveRealData()
        {
            if (PlayID > -1 && bRecording)
            {
                if (!DHClient.DHStopSaveRealData(PlayID))
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
            int logid = GetLogID();
            if (logid == -1) return false;
            string time = System.DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string path;
            if (!NetVideoControl.PicPath.EndsWith("\\"))
                path = NetVideoControl.PicPath + "\\" + pubFun.DateStr + "\\";
            else
                path = NetVideoControl.PicPath + pubFun.DateStr + "\\";
            string date = pubFun.DateStr;
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

            string sPicFileName = String.Format("{0}{1}_{2}_{3}_{4}.bmp", path, fileName, playArgs.Ip, playArgs.DvrCh, time);

            if (!DHClient.DHCapturePicture(PlayID, sPicFileName))
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
            Debug.WriteLine("图片抓拍开始：" + fileName);
            VideoRealPlayArgs e = playArgs;
            if (e == null)
            {
                Log("实时视频抓拍失败，没有正确设置播放参数！");
                return false;
            }
            int logid = GetLogID();
            if (logid == -1) return false;
            Debug.WriteLine("图片抓拍logid：" + logid);
            string time = System.DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string path;
            if (!NetVideoControl.PicPath.EndsWith("\\"))
                path = NetVideoControl.PicPath + "\\" + pubFun.DateStr + "\\";
            else
                path = NetVideoControl.PicPath + pubFun.DateStr + "\\";
            string date = pubFun.DateStr;
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

            string sPicFileName = String.Format("{0}{1}_{2}_{3}_{4}.bmp", path, fileName, playArgs.Ip, playArgs.DvrCh, time);
            Debug.WriteLine("图片抓拍sPicFileName：" + sPicFileName);
            //Thread.Sleep(100);
            if (!DHClient.DHCapturePicture(PlayID, sPicFileName))
            {
                string msg = String.Format("图片抓拍失败！IP：{0} 通道号:{1}", playArgs.Ip, playArgs.DvrCh);
                Log(msg);
                return false;
            }
            Debug.WriteLine("图片抓拍成功：" + sPicFileName);
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
            Debug.WriteLine("VIDEO_PTZControlWithSpeed loginid:" + loginID + "   ch:" + (playArgs.DvrCh - 1).ToString());
            if (!DHClient.DHPTZControl(loginID, playArgs.DvrCh - 1, ConverPTZCommand(dwPTZCommand), dwStop == 1 ? (ushort)0 : (ushort)dwSpeed, dwStop == 1 ? true : false))
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
            PTZ_CONTROL cmd = ConverPTZCommand(dwPTZCommand);
            if (!DHClient.DHPTZControl(loginID, playArgs.DvrCh - 1, cmd, 0, (ushort)PresetIndex, 0, false))
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
            if (!DHClient.DHPTZControl(loginID, playArgs.DvrCh - 1, ConverPTZCommand(dwPTZCommand), CruiseRoute, CruisePoint, 0, false))
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
            if (!DHClient.DHPTZControl(loginID, playArgs.DvrCh - 1, ConverPTZCommand(dwPTZCommand), (ushort)dwTrackIndex, 0, 0, false))
            {
                string msg = String.Format("云台轨迹控制失败！IP：{0} 通道号:{1}", playArgs.Ip, playArgs.DvrCh);
                Log(msg);
                return false;

            }
            else
                return true;

        }
        private PTZ_CONTROL ConverPTZCommand(uint hikPTZCommand)
        {
            PTZ_CONTROL cmd = PTZ_CONTROL.EXTPTZ_TOTAL;
            switch (hikPTZCommand)
            {
                case (uint)PTZCmdCodeEnum.PAN_LEFT:
                    cmd = PTZ_CONTROL.PTZ_LEFT_CONTROL;
                    break;
                case (uint)PTZCmdCodeEnum.PAN_RIGHT:
                    cmd = PTZ_CONTROL.PTZ_RIGHT_CONTROL;
                    break;
                case (uint)PTZCmdCodeEnum.TILT_UP:
                    cmd = PTZ_CONTROL.PTZ_UP_CONTROL;
                    break;
                case (uint)PTZCmdCodeEnum.TILT_DOWN:
                    cmd = PTZ_CONTROL.PTZ_DOWN_CONTROL;
                    break;
                case (uint)PTZCmdCodeEnum.UP_LEFT:
                    cmd = PTZ_CONTROL.EXTPTZ_LEFTTOP;
                    break;
                case (uint)PTZCmdCodeEnum.UP_RIGHT:
                    cmd = PTZ_CONTROL.EXTPTZ_RIGHTTOP;
                    break;
                case (uint)PTZCmdCodeEnum.DOWN_LEFT:
                    cmd = PTZ_CONTROL.EXTPTZ_LEFTDOWN;
                    break;
                case (uint)PTZCmdCodeEnum.DOWN_RIGHT:
                    cmd = PTZ_CONTROL.EXTPTZ_RIGHTDOWN;
                    break;
                case (uint)PTZCmdCodeEnum.ZOOM_IN:
                    cmd = PTZ_CONTROL.PTZ_ZOOM_ADD_CONTROL;
                    break;
                case (uint)PTZCmdCodeEnum.ZOOM_OUT:
                    cmd = PTZ_CONTROL.PTZ_ZOOM_DEC_CONTROL;
                    break;
                case (uint)PTZCmdCodeEnum.GOTO_PRESET:
                    cmd = PTZ_CONTROL.PTZ_POINT_MOVE_CONTROL;
                    break;
                case (uint)PTZCmdCodeEnum.SET_PRESET:
                    cmd = PTZ_CONTROL.PTZ_POINT_SET_CONTROL;
                    break;
                case (uint)PTZCmdCodeEnum.CLE_PRESET:
                    cmd = PTZ_CONTROL.PTZ_POINT_DEL_CONTROL;
                    break;
                case (uint)PTZCmdCodeEnum.FILL_PRE_SEQ:
                    cmd = PTZ_CONTROL.EXTPTZ_ADDTOLOOP;
                    break;
                case (uint)PTZCmdCodeEnum.CLE_PRE_SEQ:
                    cmd = PTZ_CONTROL.EXTPTZ_DELFROMLOOP;
                    break;
                case (uint)PTZCmdCodeEnum.RUN_SEQ:
                    cmd = PTZ_CONTROL.EXTPTZ_STARTLINESCAN;
                    break;
                case (uint)PTZCmdCodeEnum.STOP_SEQ:
                    cmd = PTZ_CONTROL.EXTPTZ_CLOSELINESCAN;
                    break;
                default:
                    cmd = PTZ_CONTROL.EXTPTZ_TOTAL;
                    break;

            }
            return cmd;
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
            /* PICCFG.struStringInfo = new HCNetSDK.NET_DVR_SHOWSTRINGINFO[8];

             for (int i = 0; i < 4; i++)
             {
                 string s = "";

                 byte[] source = new byte[44];
                 byte[] destin = new byte[44];

                 source = System.Text.Encoding.Default.GetBytes(s);
                 int len;
                 if (source.Length > 44)
                     len = 44;
                 else
                     len = source.Length;
                 System.Array.Copy(source, destin, len);

                 m_ShowStringInfo[i].wShowString = 1;
                 m_ShowStringInfo[i].wStringSize = Convert.ToUInt16(destin.Length);
                 m_ShowStringInfo[i].wShowStringTopLeftX = 0;
                 m_ShowStringInfo[i].wShowStringTopLeftY = Convert.ToUInt16(350 + i * 50);
                 m_ShowStringInfo[i].sString = destin;

                 PICCFG.struStringInfo[i] = m_ShowStringInfo[i];


             }
             //string ll = Encoding.Default.GetString(PICCFG.struStringInfo[0].sString);

             //MessageBox.Show(ll);*/

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
                return null;
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
                return true;
            else
                return false;
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

            /* int logID = GetLogID();
             if (logID == -1)
             {
                 Log("语音对讲失败，没有正确登录DVR！");
                 return false;
             }
            IntPtr pUser = new IntPtr();
            VoiceComHandle=HCNetSDK.NET_DVR_StartVoiceCom_V30(logID,1,false,voiceDataCallBack,pUser);
           // VoiceComHandle = HCNetSDK.NET_DVR_StartVoiceCom(logID,voiceDataCallBack, 0);
            if (VoiceComHandle>-1)
            {
                return true;
            }else
            {
                return false;
            }
             */
            return false;

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
