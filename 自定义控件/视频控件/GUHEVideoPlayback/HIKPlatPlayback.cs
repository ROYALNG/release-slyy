using GHIBMS.Common;
using GHIBMS.DvrSDK;
using GHIBMS.HIKPLATSDK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace GHIBMS.VideoPlayback
{
    public class HIKPlatPlayback : IVideoPlayback
    {
        private VideoPlaybackArgs m_playbackArgs = new VideoPlaybackArgs();
        private static bool bLogin = false;
        private bool m_bSearching = false;
        private bool bDowloading = false;
        private int playID = -1;
        private long downID = -1;
        private string protocolCode = "JK_PLAT_HIKPlat";
        private int dowloadPos = 0;
        pStreamCallback streamCallback;
        uint recType = 0xFF;
        public HIKPlatPlayback()
        {
            streamCallback = new pStreamCallback(OnDealDownloadStream);
        }
        public string ProtocolCode
        {
            get { return protocolCode; }
        }
        private static UInt16 videoWndNubs = 1;
        public ushort VideoWndNubs
        {
            get
            {
                return videoWndNubs; ;
            }
            set
            {
                videoWndNubs = value;
            }
        }

        #region 事件
        //log事件
        public delegate void OnMessagedelegate(object sender, string msg);
        public event OnMessagedelegate OnMessage;
        private void SentMessage(string message)
        {
            if (OnMessage != null)
                OnMessage(this, message);
        }
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
        public delegate void OnFileSearchdelegate(object sender, int result, List<DvrFindData> data);
        public event OnFileSearchdelegate OnFileSearch;
        #endregion
        public void SetPlayAgrs(VideoPlaybackArgs args)
        {
            m_playbackArgs.Clone(args);
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
        private void LoginPlat()
        {
            if (!bLogin)
            {
                HikLoginInfo info = new HikLoginInfo();
                info.szServerUrl = m_playbackArgs.PlatIpAddress;// "192.168.45.2";
                info.uServerPort = (uint)m_playbackArgs.PlatPort;// 80;
                info.szUserName = m_playbackArgs.PlatUsername;// "admin_gh";
                info.szPassword = m_playbackArgs.PlatPassword;// "Admin12345";
                if (0 == HikPlatSDK.HikPt_Login(ref info))
                {
                    bLogin = true;
                }
            }
        }
        public void DVR_Cleanup()
        {
            if (bLogin)
            {
                HikPlatSDK.HikPt_Logout();
                bLogin = false;
            }
            HikPlatSDK.Uninit();
            // throw new NotImplementedException();
        }
        string ResultXml = "";
        DateTime dtStart = DateTime.Now;
        DateTime dtEnd = DateTime.Now;
        public bool VIDEO_FindFile(uint dwFileType, byte isLock, DateTime startTime, DateTime endTime)
        {
            //dwFileType
            // 全部=0xff,
            //定时录像=0,
            //移动侦测=1,
            //报警触发=2,
            //报警触发或移动侦测=3,
            //报警触发和移动侦测=4,
            //命令触发=5,
            //手动录像=6,
            //智能录像=7

            //            enum HIK_RECORD_TYPE
            //{
            //    HIK_RECTYPE_UNKNOWN = 0,     // 未知
            //    HIK_RECTYPE_PLAN = 0x01,     // 计划录像
            //    HIK_RECTYPE_MOVE = 0x02,     // 移动录像
            //    HIK_RECTYPE_ALARM = 0x04,    // 告警录像
            //    HIK_RECTYPE_MANUAL = 0x10,   // 手动录像
            ////    HIK_RECTYPE_ALL = 0xFF,      // 全部类型
            //};

            switch (dwFileType)
            {
                case 0xff:// 全部
                    recType = 0xff;
                    break;
                case 0://定时录像=0
                    recType = 1;
                    break;
                case 1:// 全部
                    recType = 2;
                    break;
                case 2://报警录像
                case 3://报警录像
                case 4://报警录像
                case 5://报警录像
                    recType = 4;
                    break;
                case 6:
                    recType = 0x10;   // 手动录像
                    break;
                default:
                    recType = 0xff;
                    break;
            }


            ResultXml = "";
            m_bSearching = true;
            dtStart = startTime;
            dtEnd = endTime;
            //throw new NotImplementedException();
            if (searchThread == null)
            {
                searchThread = new Thread(FindThread);
                searchThread.IsBackground = true;
                searchThread.Name = "hikplatfound";
                searchThread.Start();
            }
            return true;
        }
        Thread searchThread = null;
        void FindThread()
        {
            try
            {
                VIDEO_Init();
                LoginPlat();
                ResultXml = "";
                int nXmlSize = 1024 * 200;
                IntPtr szResultXml = IntPtr.Zero;
                szResultXml = Marshal.AllocHGlobal(nXmlSize);
                sessionRec = HikPlatSDK.HikPt_QueryRecord(m_playbackArgs.SerialNumber, DateTimeToTime_t(dtStart), DateTimeToTime_t(dtEnd), szResultXml, nXmlSize, recType);
                string s4 = Marshal.PtrToStringAnsi(szResultXml);
                Marshal.FreeHGlobal(szResultXml);
                string HEAD = "<QueryResult>";
                string FOOT = "</QueryResult>";

                List<DvrFindData> dataList = new List<DvrFindData>();
                //使用xpath表达式选择文档中所有的student子节点
                try
                {
                    int a = s4.IndexOf(HEAD);
                    int b = s4.IndexOf(FOOT);
                    if (s4.IndexOf(HEAD) > -1 && s4.IndexOf(FOOT) > -1)
                    {
                        int n = s4.IndexOf(FOOT);
                        while (s4.IndexOf(HEAD) != 0)
                            s4 = s4.Substring(s4.IndexOf(HEAD));
                        s4 = s4.Substring(0, n + FOOT.Length);

                    }
                    ResultXml = s4;
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(s4);
                    XmlNodeList NodeList = doc.SelectNodes("//RecordSegment");
                    if (NodeList != null)
                    {
                        // <RecordSegment>
                        //<BeginTime>2016-10-16T22:13:12.000</BeginTime> 
                        //<EndTime>2016-10-17T00:13:15.000</EndTime> 
                        //<RecordType>1</RecordType> 
                        //<MediaDataLen>1817182208</MediaDataLen> 
                        //<IsLocked>0</IsLocked> 
                        string BeginTime = "";
                        string EndTime = "";
                        string RecordType = "";
                        string PlayURL = "";
                        string IsLocked = "";
                        foreach (XmlNode nod in NodeList)
                        {
                            foreach (XmlNode child in nod.ChildNodes)
                            {
                                //通过Attributes获得属性名字为name的属性
                                if (child.Name == "BeginTime")
                                {
                                    BeginTime = child.InnerXml;
                                }
                                else if (child.Name == "EndTime")
                                {
                                    EndTime = child.InnerXml;
                                }
                                else if (child.Name == "RecordType")
                                {
                                    RecordType = child.InnerXml;
                                }
                                else if (child.Name == "PlayURL")
                                {
                                    PlayURL = child.InnerXml;
                                }
                                else if (child.Name == "PlayURL")
                                {
                                    IsLocked = child.InnerXml;
                                }
                            }
                            //
                            DvrFindData data = new DvrFindData();

                            data.FileName = PlayURL;
                            data.Locked = pubFun.Isbtye(IsLocked, 0);
                            data.StartTime = Convert.ToDateTime(BeginTime.Replace('T', ' '));
                            data.StopTime = Convert.ToDateTime(EndTime.Replace('T', ' '));
                            data.PlayState = 0;
                            data.FileSize = "1";
                            dataList.Add(data);

                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.GetInstance().LogError(ex.ToString());
                }


                if (OnFileSearch != null)
                    OnFileSearch(this, strConst.NET_DVR_NOMOREFILE, dataList);
                m_bSearching = false;
                searchThread = null;
                if (ResultXml == "")
                {
                    if (OnMessage != null)
                        OnMessage(this, "下载完成！");
                }
                if (OnMessage != null)
                    OnMessage(this, "下载完成！");
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }
        }
        public static long DateTimeToTime_t(DateTime dateTime)
        {

            long time_t;
            DateTime dt1 = new DateTime(1970, 1, 1, 0, 0, 0);
            TimeSpan ts = dateTime - dt1;
            time_t = ts.Ticks / 10000000 - 28800;
            return time_t;
        }
        long sessionRec = -1;
        public bool VIDEO_FindClose()
        {
            if (sessionRec != -1)
            {
                HikPlatSDK.HikPt_CloseQueryRecord((Int32)sessionRec);
                sessionRec = -1;
            }
            //throw new NotImplementedException();
            return true;
        }

        public bool VIDEO_PlayBackByName(string sPlayBackFileName, DateTime dtStar)
        {
            //throw new NotImplementedException();
            return true;
        }

        public bool VIDEO_PlayBackByTime(DateTime startTime, DateTime endTime)
        {
            if (ResultXml == "")
            {
                if (OnMessage != null)
                {
                    OnMessage(this, "请查找后再播放");
                }
                return false;
            }
            dtStart = startTime;
            dtEnd = endTime;
            playID = HikPlatSDK.HikPt_StartPlayBack(m_playbackArgs.PlayWnd, ResultXml, DateTimeToTime_t(startTime), DateTimeToTime_t(endTime), null, IntPtr.Zero);
            //throw new NotImplementedException();
            return true;
        }

        public bool VIDEO_PlayBackSaveData()
        {
            //throw new NotImplementedException();
            return true;
        }

        public bool VIDEO_StopPlayBackSave()
        {
            //throw new NotImplementedException();
            return true;//
        }

        public bool VIDEO_PlayBackCaptureFile()
        {
            string time = System.DateTime.Now.ToString("yyyyMMdd_HHmmss_fff");
            string path;
            if (!VideoPlaybackControl.PicPath.EndsWith("\\"))
                path = VideoPlaybackControl.PicPath + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\";
            else
                path = VideoPlaybackControl.PicPath + DateTime.Now.ToString("yyyy-MM-dd") + "\\";
            if (!GetDriver(path.Substring(0, 1)))
            {
                MessageBox.Show("该磁盘不存在!");
                return false;
            }
            if (Directory.Exists(path) == false)
            {
                // MessageBox.Show("该目录不存在，自动创建‘" + textBox1.Text + "\\’" + "目录");
                Directory.CreateDirectory(path);
            }
            string sPicFileName = String.Format("{0}{1}_{2}_{3}.jpg", path, m_playbackArgs.CamName, m_playbackArgs.CamID, time);

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
            SnapParam.szSaveFolder = sPicFileName;
            //抓图模式(0~自动  1~手动)
            SnapParam.lOpenFlag = 0;
            //生成图片的名称格式(1~{监控点名称}_{时间}  2_{时间}  3_{时间}_{监控点名称})
            SnapParam.nFormatType = 1;

            int i = HikPlatSDK.HikPt_PlaybackSnapShot(playID, ref SnapParam, null, IntPtr.Zero);
            if (i != 0)
            {
                string msg = String.Format("图片抓拍失败！}", m_playbackArgs.CamName);
                Log(msg);
                return false;
            }
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
        public bool VIDEO_GetFileByName(string sDVRFileName)
        {
            dowloadPos = 100;
            return true;
        }
        FileStream fs1;
        public bool VIDEO_GetFileByTime(DateTime startTime, DateTime endTime)
        {
            if (ResultXml == "")
            {
                dowloadPos = 100;
                return false;
            }
            string time = System.DateTime.Now.ToString("yyyyMMdd_HHmmss_fff");
            string path;
            if (!VideoPlaybackControl.PicPath.EndsWith("\\"))
                path = VideoPlaybackControl.RecPath + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\";
            else
                path = VideoPlaybackControl.RecPath + DateTime.Now.ToString("yyyy-MM-dd") + "\\";
            if (!GetDriver(path.Substring(0, 1)))
            {
                MessageBox.Show("该磁盘不存在!");
                return false;
            }
            if (Directory.Exists(path) == false)
            {
                // MessageBox.Show("该目录不存在，自动创建‘" + textBox1.Text + "\\’" + "目录");
                Directory.CreateDirectory(path);
            }
            string sPicFileName = String.Format("{0}{1}_{2}_{3}.mp4", path, m_playbackArgs.CamName, m_playbackArgs.CamID, time);
            fs1 = new FileStream(sPicFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            downID = HikPlatSDK.HikPt_StartDownloadRecord(ResultXml, DateTimeToTime_t(dtStart), DateTimeToTime_t(dtEnd), streamCallback, IntPtr.Zero);
            if (downID != -1)
            {
                bDowloading = true;
                return true;
            }
            else
            {
                return false;
            }
        }
        //Int32 lSession, int iStreamType, byte[] data, int dataLen, IntPtr pUser);
        void OnDealDownloadStream(Int32 lSession, int iStreamType, IntPtr data, int dataLen, IntPtr pUser)
        {
            try
            {

                if (iStreamType == (int)PB_STREAM_DATATYPE.PBDT_FORWARD)
                {
                    // PLAT_INFO("Session = %d, Start recv download stream");
                }
                else if (iStreamType == (int)PB_STREAM_DATATYPE.PBDT_DATA) //码流数据
                {
                    byte[] arr = new byte[dataLen];
                    Marshal.Copy(data, arr, 0, dataLen);
                    fs1.Write(arr, 0, dataLen);
                    bDowloading = true;
                }
                else if (iStreamType == (int)PB_STREAM_DATATYPE.PBDT_END)//PBDT_END
                {
                    fs1.Close();
                    fs1 = null;
                    dowloadPos = 100;
                    bDowloading = false;
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }
        }

        public bool VIDEO_StopGetFile()
        {
            dowloadPos = 100;
            bDowloading = false;
            HikPlatSDK.HikPt_StopDownloadRecord((int)downID);
            if (fs1 != null)
            {
                fs1.Close();
                fs1 = null;
            }
            return true;
        }

        public int VIDEO_GetDownloadPos()
        {
            return dowloadPos;
            // throw new NotImplementedException();
        }

        public bool VIDEO_PlayBackControl(uint dwControlCode, uint dwInValue, out uint lpOutValue)
        {

            lpOutValue = 0;
            int cmd = 0;
            Int64 parm = 0;
            switch (dwControlCode)
            {
                case HCNetSDK.NET_DVR_PLAYSTART:
                    cmd = HikPlatSDK.PLAY_START;
                    parm = 0;
                    break;
                case HCNetSDK.NET_DVR_PLAYSTOP:
                    cmd = HikPlatSDK.PLAY_PAUSE;
                    parm = 0;
                    break;
                case HCNetSDK.NET_DVR_PLAYPAUSE:
                    cmd = HikPlatSDK.PLAY_PAUSE;
                    parm = 0;
                    break;
                case HCNetSDK.NET_DVR_PLAYRESTART:
                    cmd = HikPlatSDK.PLAY_START;
                    parm = 0;
                    break;
                case HCNetSDK.NET_DVR_PLAYFAST:
                    cmd = HikPlatSDK.PLAY_FAST;
                    if (dwInValue == 8)
                        parm = 2;
                    else if (dwInValue == 9)
                        parm = 4;
                    else if (dwInValue == 10)
                        parm = 6;
                    else if (dwInValue == 11)
                        parm = 8;
                    else if (dwInValue == 12)
                        parm = 10;
                    else
                        parm = 2;
                    break;
                case HCNetSDK.NET_DVR_PLAYSLOW:
                    cmd = HikPlatSDK.PLAY_SLOW;
                    parm = -2;
                    break;
                case HCNetSDK.NET_DVR_PLAYNORMAL:
                    cmd = HikPlatSDK.PLAY_START;
                    parm = 0;
                    break;
                case HCNetSDK.NET_DVR_PLAYFRAME:
                    cmd = HikPlatSDK.PLAY_ONEFRAMEFORWARD;
                    parm = 0;
                    break;
                case HCNetSDK.NET_DVR_PLAYSETPOS:
                    {
                        long l = pubFun.DateDiff(dtStart, dtEnd);
                        long pos = dwInValue / 100 * l;
                        DateTime dtPos = dtStart.AddSeconds(pos);
                        cmd = HikPlatSDK.PLAY_OFFSET;
                        parm = pos;
                    }
                    break;
                case HCNetSDK.NET_DVR_PLAYGETPOS:

                    return true;
                case HCNetSDK.NET_DVR_PLAYGETTIME:
                    {

                        return true;
                    }

                default:
                    return true;
            }
            if (cmd != 0)
            {
                return HikPlatSDK.HikPt_PlayBackControl(playID, cmd, dwInValue) == 0 ? true : false;
            }
            else
                return true;

        }

        public bool VIDEO_StopPlayBack()
        {
            try
            {
                if (searchThread != null)
                {
                    searchThread.Abort();
                    searchThread = null;
                }
            }
            catch { }
            HikPlatSDK.HikPt_StopPlayBack(playID);
            if (bDowloading)
            {
                VIDEO_StopGetFile();
            }
            if (sessionRec != -1)
            {
                VIDEO_FindClose();
            }
            return true;
        }



        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
