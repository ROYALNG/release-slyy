using GHIBMS.Common;
using GHIBMS.HIKPLATSDK;
using System;
using System.IO;
using System.Windows.Forms;

namespace GHIBMS.NetVideo
{
    public class HIKPlatRealPlayer : IVideoRealPlayer, IDisposable
    {

        #region 内部变量与属性

        public static bool bLoginPlat = false;
        private static UInt16 videoWndNubs = 1;

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
        public HIKPlatRealPlayer()
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
        ~HIKPlatRealPlayer()
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
            if (!HikPlatSDK.Init())
            {
                Log("海康平台SDK初始化失败！");
                return false;
            }
            return true;
        }
        //清理资源
        public void DVR_Cleanup()
        {



        }


        #endregion

        #region 设备注册
        private bool DVR_Login()
        {
            VIDEO_Init();
            VideoRealPlayArgs e = playArgs;
            if (e.Ip == "") return false;
            string key = e.Ip + ":" + e.Port.ToString();
            if (bLoginPlat == false)
            {
                HikLoginInfo info = new HikLoginInfo();
                info.szServerUrl = e.Ip;
                info.uServerPort = e.Port;
                info.szUserName = e.UserName;
                info.szPassword = e.Password;
                if (HikPlatSDK.HikPt_Login(ref info) == 0)
                {
                    bLoginPlat = true;
                }
                else
                    bLoginPlat = false;
            }
            return bLoginPlat;
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

            else return "登录失败";
        }

        public bool DVR_Logout()
        {

            HIKPLATSDK.HikPlatSDK.HikPt_Logout();
            bLoginPlat = false;
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

                PlayID = HikPlatSDK.HikPt_StartPlayView(e.CamID, e.PlayWnd, null, IntPtr.Zero);

                if (PlayID > -1)
                {

                    return true;
                }
                else
                {
                    string msg = String.Format("实时视频播放失败！监控点编号：" + e.CamID);
                    Log(msg);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
                Console.WriteLine(ex.ToString());
            }
            return false;
        }
        public bool VIDEO_StopRealPlay()
        {
            try
            {
                //如果正在录像，先停止
                if (bRecording)
                    VIDEO_StopSaveRealData();
                VideoRealPlayArgs e = playArgs;
                HikPlatSDK.HikPt_StopPlayView(PlayID);
                PlayID = -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return true;
        }


        #endregion

        #region 抓拍与录像
        public bool VIDEO_SaveRealData(string fileName)
        {
            VideoRealPlayArgs e = playArgs;

            if (bLoginPlat)
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
                path = NetVideoControl.RecPath + "\\" + pubFun.DateStr;
            else
                path = NetVideoControl.RecPath + pubFun.DateStr;
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
            string sPicFileName = String.Format("{0}{1}_{2}_{3}_{4}.MP4", path, fileName, playArgs.CamID, playArgs.CamName, time);

            HikRecordCfgParam parm = new HikRecordCfgParam();
            parm.dwMaxRecordTimes = 60;
            parm.dwPackSize = 0;
            parm.dwTimes = 5 * 60;
            parm.szFolde = sPicFileName;

            //(Int32 lSession, ref HikRecordCfgParam pRecordParam, pPreviewRecordEndCallback pfunrecord,IntPtr pUserData);
            int i = HikPlatSDK.HikPt_StartRecord(PlayID, ref parm, null, IntPtr.Zero);




            if (i != 0)
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
                if (HikPlatSDK.HikPt_StopRecord(PlayID) != 0)
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
            /*VideoRealPlayArgs e = playArgs;
            if (e == null)
            {
                Log("实时视频抓拍失败，没有正确设置播放参数！");
                return false;
            }
            if (bLoginPlat==false) return false;
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

            string sPicFileName = String.Format("{0}{1}_{2}_{3}_{4}.bmp", path,fileName,playArgs.CamID, playArgs.CamName, time);
         
            if (!HCNetSDK.NET_DVR_CapturePicture(PlayID, sPicFileName))
            {
                string msg = String.Format("图片抓拍失败！IP：{0} 通道号:{1}", playArgs.Ip, playArgs.DvrCh);
                Log(msg);
                return false;
            }

            string sPicFileName = String.Format("{0}{1}_{2}_{3}_{4}.mp4", path, fileName, playArgs.Ip, playArgs.DvrCh, time);
            */

            //pubFun.ShowFileDirectory(sPicFileName);
            VIDEO_CaptureJPEGPicture(fileName);
            return false;
        }
        public bool VIDEO_CaptureJPEGPicture(string fileName)
        {
            VideoRealPlayArgs e = playArgs;

            if (bLoginPlat) return false;
            string time = System.DateTime.Now.ToString("yyyyMMdd_HHmmss_fff");
            string path;
            if (!NetVideoControl.PicPath.EndsWith("\\"))
                path = NetVideoControl.PicPath + "\\" + pubFun.DateStr;
            else
                path = NetVideoControl.PicPath + pubFun.DateStr;
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

            string sPicFileName = String.Format("{0}{1}_{2}_{3}_{4}.jpg", path, fileName, playArgs.CamName, playArgs.CamID, time);
            HikSnapParam SnapParam = new HikSnapParam();

            //图片类型(0~JPG  1~BMP)
            SnapParam.nPicType = 0;
            //抓图类型(0~单张  1~多张)
            SnapParam.nSnapType = 0;
            //连续抓图模式(0~按时间  1~按帧)
            SnapParam.nMultiType = 0;
            //抓图时间间隔(1~1毫秒  2~2毫秒  3~3毫秒  4~4毫秒  5~5毫秒)
            SnapParam.nSpanTime = 1;
            //连续抓图张数(3~3张  4~4张  5~5张  6~6张)
            SnapParam.nSnapCount = 0;
            //图片质量
            SnapParam.Quality = 80;
            SnapParam.szSaveFolder = path;
            //抓图模式(0~自动  1~手动)
            SnapParam.lOpenFlag = 0;
            //生成图片的名称格式(1~{监控点名称}_{时间}  2_{时间}  3_{时间}_{监控点名称})
            SnapParam.nFormatType = 1;

            int i = HikPlatSDK.HikPt_PreviewSnapShot(PlayID, ref SnapParam, null, IntPtr.Zero);
            if (i != 0)
            {
                string msg = String.Format("图片抓拍失败！}", playArgs.CamName);
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
            //if ( HikPlatSDK.HikPt_PtzControl( PlayID, dwPTZCommand, dwStop, dwSpeed))
            //{
            //    string msg = String.Format("云台控制失败！IP：{0} 通道号:{1}", playArgs.Ip, playArgs.DvrCh);
            //    Log(msg);
            //    return false;
            //}
            //else
            return true;
        }
        public bool VIDEO_PTZPreset(uint dwPTZCommand, uint PresetIndex)
        {
            //if (!HCNetSDK.NET_DVR_PTZPreset_EX(playID, dwPTZCommand, PresetIndex))
            //{
            //    string msg = String.Format("云台预置位控制失败！IP：{0} 通道号:{1}", playArgs.Ip, playArgs.DvrCh);
            //    Log(msg);
            //    return false;
            //}
            //else

            return true;

        }
        public bool VIDEO_PTZCruise(uint dwPTZCommand, byte CruiseRoute, byte CruisePoint, ushort Input)
        {
            //if (!HCNetSDK.NET_DVR_PTZCruise(playID, dwPTZCommand, CruiseRoute, CruisePoint, Input))
            //{
            //      string msg = String.Format("云台巡航控制失败！IP：{0} 通道号:{1}", playArgs.Ip, playArgs.DvrCh);
            //      Log(msg);
            //     return false;

            //}
            //else
            return true;
        }
        public bool VIDEO_PTZTrack(uint dwPTZCommand, uint dwTrackIndex)
        {
            //if (!HCNetSDK.NET_DVR_PTZTrack(playID, dwPTZCommand))
            //{
            //      string msg = String.Format("云台轨迹控制失败！IP：{0} 通道号:{1}", playArgs.Ip, playArgs.DvrCh);
            //      Log(msg);
            //     return false;

            //}
            //else
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



        public VideoEffect VIDEO_GetVideoEffect()
        {
            return null;

        }
        public bool VIDEO_SetVideoEffect(VideoEffect effect)
        {

            return false;
        }


        #region 声音、对讲

        public bool VIDEO_OpenSound()
        {
            if (PlayID > -1)
            {
            }
            return false;

        }
        public bool VIDEO_CloseSound()
        {

            return false;

        }
        public bool VIDEO_Volume(ushort wVolume)
        {

            return false;
        }
        public bool VIDEO_StartVoiceCom()
        {


            return false;

        }
        public bool VIDEO_StopVoiceCom()
        {

            return false;

        }
        public bool VIDEO_SetVoiceComClientVolume(ushort wVolume)
        {

            return false;

        }
        public void fVoiceDataCallBack(int lVoiceComHandle, string pRecvDataBuffer, uint dwBufSize, byte byAudioFlag, uint dwUser)
        {

        }


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
