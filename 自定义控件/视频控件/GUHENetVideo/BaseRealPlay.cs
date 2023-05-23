using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


namespace GHIBMS.NetVideo
{
    //实时视频播放的基类
    public  abstract class BaseRealPlayer
    {
         protected ushort _DvrSDKType;
         public ushort DvrSDKType
         {
             get { return _DvrSDKType; }
             set { _DvrSDKType = value; }
         }
         /// <summary>
         ///启动时初始化资源
         /// </summary>
         /// <returns></returns>
         public abstract bool DVR_Init();
        /// <summary>
        /// 退出时清理资源
        /// </summary>
        /// <returns></returns>
         public abstract bool DVR_Cleanup();
      
         public abstract int  DVR_RealPlay(string sDVRIP, ushort wDVRPort, string sUserName, string sPassword, int lChannel,int lLinkMode,IntPtr hPlayWnd);
         public abstract int  DVR_RealPlay(int lUserID, int lChannel, int lLinkMode, IntPtr hPlayWnd, string sMultiCastIP);
         public abstract bool DVR_SaveRealData(int lRealHandle, uint dwTransType, string sFileName);
         public abstract bool DVR_CapturePicture(int lRealHandle, string sPicFileName);
         public abstract bool DVR_CaptureJPEGPicture(int lUserID, int lChannel,string sPicFileName);
         public abstract bool DVR_StopRealPlay(int lRealHandle);
         public abstract bool DVR_StopSaveRealData(int lRealHandle);

         public abstract int  DVR_SearchFile(int lUserID, int lChannel, uint m_iFileType, DateTime dtStar, DateTime dtEnd, ListView lsv,Label lblSearchState,Button btnSearch);
         public abstract bool DVR_FindClose(int lFindHandle);
         public abstract int  DVR_PlayBackByName(int lUserID, string sPlayBackFileName, IntPtr hWnd);
         public abstract bool DVR_StopPlayBack(int lPlayHandle);
         public abstract bool DVR_PlayBackControl(int lPlayHandle, uint dwControlCode, uint dwInValue, out uint LPOutValue);
         public abstract bool DVR_RefreshPlay(int lPlayHandle);
         public abstract bool DVR_PlayBackCaptureFile( int lPlayHandle, string  sFileName);
         public abstract int  DVR_GetFileByName(int lUserID,string sDVRFileName,string  sSavedFileName);
         public abstract bool DVR_StopGetFile( int lFileHandle);
         public abstract int  DVR_GetDownloadPos(int lFileHandle);
         public abstract bool DVR_PTZControlWithSpeed_Other(int lUserID, int lChannel, uint dwPTZCommand, uint dwStop, uint dwSpeed);
         public abstract bool DVR_PTZPreset_Other(int lUserID, int lChannel, uint dwPTZPresetCmd, uint dwPresetIndex);


    }
}
