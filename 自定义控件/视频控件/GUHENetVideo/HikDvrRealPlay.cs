using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading ;

namespace GHIBMS.NetVideo
{
    public class HikDvrRealPlayer : BaseRealPlayer 
    {
        private int lUserID;
        private ListView lsv  ;
        private Label _lblSearch;
        private Button _btnSearch;
        private Thread Thread_SearchFile;
        private bool m_bSearching = false;
        private int m_lFileHandle = -1;
        private HCNetSDK.NET_DVR_FILECOND m_struFileCond = new HCNetSDK.NET_DVR_FILECOND();
        private static HCNetSDK.NET_DVR_SHOWSTRINGINFO[] m_ShowStringInfo = new HCNetSDK.NET_DVR_SHOWSTRINGINFO[8];
        private static IntPtr lptr;
        private static HCNetSDK.NET_DVR_SHOWSTRING_V30 PICCFG = new HCNetSDK.NET_DVR_SHOWSTRING_V30();

        public HikDvrRealPlayer()
        {
            DvrSDKType = 0;
            //初始化
            PICCFG.struStringInfo = new HCNetSDK.NET_DVR_SHOWSTRINGINFO[8];
     
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

            //MessageBox.Show(ll);
        }
      
        #region 设备初始化
        public override bool DVR_Init()
        {
            MessageBox.Show("海康初始化成功");
            return HCNetSDK.NET_DVR_Init();

        }
        //public override bool DVR_SetConnectTime(uint dwWaitTime, uint dwTryTimes)
        //{
        //    return HCNetSDK.NET_DVR_SetConnectTime(dwWaitTime, dwTryTimes);
        //}
        public override bool DVR_Cleanup()
        {
            return HCNetSDK.NET_DVR_Cleanup();
        }
        #endregion
        #region 设备注册
        //public override int DVR_Login(string sDVRIP, ushort wDVRPort, string sUserName, string sPassword)
        //{
        //   NET_DVR_DEVICEINFO_V30 lpDeviceInfo = new  NET_DVR_DEVICEINFO_V30();
        //   return  HCNetSDK.NET_DVR_Login_V30(sDVRIP, wDVRPort, sUserName, sPassword, out lpDeviceInfo);
        //}
        //public override bool DVR_Logout(int lUserID)
        //{
        //   return HCNetSDK.NET_DVR_Logout_V30(lUserID);
        //}
        #endregion
        #region 视频预览
        public override int DVR_RealPlay(int lUserID, int lChannel,int lLinkMode,IntPtr hPlayWnd,string sMultiCastIP)
        {
            NET_DVR_CLIENTINFO lpClientInfo = new NET_DVR_CLIENTINFO();
            lpClientInfo.lChannel = lChannel;
            lpClientInfo.lLinkMode = lLinkMode;
            lpClientInfo.hPlayWnd = hPlayWnd;
            lpClientInfo.sMultiCastIP = sMultiCastIP;
            HCNetSDK.RealDataCallBack_V30 fRealDataCallBack_V30 = null;
            int pUser=0 ;
            bool bBlocked=false ;
            return HCNetSDK.NET_DVR_RealPlay_V30(lUserID, ref lpClientInfo,  fRealDataCallBack_V30, pUser, bBlocked);
        }
        //public override bool DVR_ThrowBFrame(int lRealHandle, uint dwNum)
        //{
        //    return HCNetSDK.NET_DVR_ThrowBFrame(lRealHandle, dwNum);
        // }
        public override bool DVR_SaveRealData(int lRealHandle, uint dwTransType, string sFileName)
        {
            return HCNetSDK.NET_DVR_SaveRealData_V30(lRealHandle, dwTransType, sFileName);
        }
        public override bool DVR_StopSaveRealData(int lRealHandle)
        {
            return HCNetSDK.NET_DVR_StopSaveRealData(lRealHandle);
        }
        public override bool DVR_CapturePicture(int lRealHandle, string sPicFileName)
        {
            return HCNetSDK.NET_DVR_CapturePicture(lRealHandle, sPicFileName);
        }
        public override bool DVR_CaptureJPEGPicture(int lUserID, int lChannel, string sPicFileName)
        {
            HCNetSDK.NET_DVR_JPEGPARA lpJpegPara=new HCNetSDK.NET_DVR_JPEGPARA();
            return HCNetSDK.NET_DVR_CaptureJPEGPicture(lUserID,lChannel,ref lpJpegPara,sPicFileName);
        }
        public override bool DVR_StopRealPlay(int lRealHandle)
        {
            return HCNetSDK.NET_DVR_StopRealPlay(lRealHandle);

        }
        #endregion
        #region 录像文件回放、下载

        public override int DVR_SearchFile(int UerID, int lChannel, uint m_iFileType, DateTime dtStar, DateTime dtEnd, ListView list,Label lblSearchState,Button btnSearch)
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
        
        */
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

        }
        #endregion
        #region 云台控制
        public override bool DVR_PTZControlWithSpeed_Other(int lUserID, int lChannel, uint dwPTZCommand, uint dwStop, uint dwSpeed)
        {
            return HCNetSDK.NET_DVR_PTZControlWithSpeed_Other(lUserID,lChannel,dwPTZCommand,dwStop,dwSpeed);
        }
        public override bool DVR_PTZPreset_Other(int lUserID, int lChannel, uint dwPTZPresetCmd, uint dwPresetIndex)
        {
            return HCNetSDK.NET_DVR_PTZPreset_Other(lUserID, lChannel, dwPTZPresetCmd, dwPresetIndex);
        }
        #endregion



    }
}
