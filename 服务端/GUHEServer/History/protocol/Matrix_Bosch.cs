using System;
using System.Collections.Generic;
using System.Text;
using GHIBMS.Common;

namespace GHIBMS.Server
{
   public class Matrix_Bosch
    {
       /// <summary>
       /// 组装BOSCH协议命令
       /// </summary>
        /// <param name="cmd"></param>
       /// <returns></returns>
       
       public static void CreateCommand(ref VideoCommandArgs cmd)
       {
            string strCmd=string.Empty;
            switch (cmd.VideoCommand)
            {
                case PTZCmdCodeEnum.MAT_VIDEO_SW:
                    strCmd = "LCM " + cmd.VideoOut.ToString() + " " + cmd.VideoIn.ToString() + "\r";
                    break;
                case PTZCmdCodeEnum.GOTO_PRESET:
                    strCmd = "PREPOS " + cmd.VideoIn.ToString() + " " + cmd.PresetIndex.ToString() + "\r"; 
                    break;
                case PTZCmdCodeEnum.SET_PRESET:
                    strCmd = "PREPOS-SET  " + cmd.VideoIn.ToString() + " " + cmd.PresetIndex.ToString() + "\r";
                    strCmd+= "REMOTE-ACTION "+cmd.VideoIn.ToString() +" 2"+  " 0"+"\r";
                    strCmd+= "REMOTE-ACTION "+cmd.VideoIn.ToString() +" 0" + " 0"+"\r";
                    break;
                case PTZCmdCodeEnum.MAT_RUN:
                    strCmd = "MON-RUN " + cmd.AutoRunIndex.ToString() + "\r";
                    break;
                case PTZCmdCodeEnum.MAT_HOLD:
                    strCmd = "MON-HOLD " + cmd.VideoOut.ToString() + "\r";
                    break;
                case PTZCmdCodeEnum.PAN_LEFT:
                    if (cmd.Stop==0)
                    {
                        //strCmd = "REMOTE-TGL " + cmd.Param2.ToString() + " 16 0" + "\r";
                        strCmd = "VARSPEED_PTZ " + cmd.VideoIn.ToString() + " " + 2 * cmd.Speed + " " + 2 * cmd.Speed + " " + cmd.Speed + " 2" + "\r";
                    }else
                    {
                        strCmd="REMOTE-ACTION "+ cmd.VideoIn.ToString()+" 0"+" 0"+"\r"; 
                    }
                    break;
                case PTZCmdCodeEnum.PAN_RIGHT:
                    if (cmd.Stop == 0)
                    {
                       // strCmd = "REMOTE-TGL " + cmd.Param2.ToString() + " 0 16" + "\r";
                        strCmd = "VARSPEED_PTZ " + cmd.VideoIn.ToString() + " " + 2 * cmd.Speed + " " + 2 * cmd.Speed + " " + cmd.Speed + " 1" + "\r";
                    }else
                    {
                        strCmd="REMOTE-ACTION "+ cmd.VideoIn.ToString()+" 0"+" 0"+"\r"; 
                    }
                    break;
                case PTZCmdCodeEnum.TILT_UP:
                     if (cmd.Stop==0)
                    {
                        //strCmd = "REMOTE-TGL " + cmd.Param2.ToString() + " 8 8" + "\r";
                        strCmd = "VARSPEED_PTZ " + cmd.VideoIn.ToString() + " " + 2 * cmd.Speed + " " + 2 * cmd.Speed + " " + cmd.Speed + " 8" + "\r";
                    }else
                    {
                        strCmd="REMOTE-ACTION "+ cmd.VideoIn.ToString()+" 0"+" 0"+"\r"; 
                    }

                    break;
                case PTZCmdCodeEnum.TILT_DOWN:
                    if (cmd.Stop==0)
                    {
                        // strCmd = "REMOTE-TGL " + cmd.VideoIn.ToString() + " 0 8" + "\r";
                         strCmd = "VARSPEED_PTZ " + cmd.VideoIn.ToString() + " " + 2 * cmd.Speed + " " + 2 * cmd.Speed + " " + cmd.Speed + " 4" + "\r";

                    }else
                    {
                        strCmd = "REMOTE-ACTION " + cmd.VideoIn.ToString() + " 0" + " 0" + "\r"; 
                    }
                    break;

                case PTZCmdCodeEnum.ZOOM_IN:
                    if (cmd.Stop == 0)
                    {
                        strCmd = "VARSPEED_PTZ " + cmd.VideoIn.ToString() + " " + 2 * cmd.Speed + " " + 2 * cmd.Speed + " " + cmd.Speed + " 32" + "\r";
                    }
                    else
                    {
                        strCmd = "REMOTE-ACTION " + cmd.VideoIn.ToString() + " 0" + " 0" + "\r";
                    }

                    break;
                case PTZCmdCodeEnum.ZOOM_OUT:
                    if (cmd.Stop == 0)
                    {
                        strCmd = "VARSPEED_PTZ " + cmd.VideoIn.ToString() + " " + 2 * cmd.Speed + " " + 2 * cmd.Speed + " " + cmd.Speed + " 16" + "\r";
                    }
                    else
                    {
                        strCmd = "REMOTE-ACTION " + cmd.VideoIn.ToString() + " 0" + " 0" + "\r";
                    }

                    break;
            }
            cmd.OutMsg = strCmd;
       }
    }
}
