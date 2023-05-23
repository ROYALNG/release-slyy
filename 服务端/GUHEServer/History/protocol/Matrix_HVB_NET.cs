using System;
using System.Collections.Generic;
using System.Text;
using GHIBMS.Common;

namespace GHIBMS.Server
{
    public class Matrix_HVB_NET
    {
       private static byte seriation=0; 
       /// <summary>
       /// 组装HoneywellHVB 网络协议命令
       /// </summary>
        /// <param name="cmd"></param>
       /// <returns></returns>
       public static void CreateCommand(ref VideoCommandArgs cmd)
       {
            seriation++;
            if (cmd.VideoIn > 0) cmd.VideoIn--;
            if (cmd.VideoOut > 0) cmd.VideoOut--;
            string strCmd=string.Empty;
            switch (cmd.VideoCommand)
            {
                case PTZCmdCodeEnum.MAT_VIDEO_SW:
                    strCmd = string.Format("FA 55 AA 5F 40 01 00 06 00 00 00 {0} 00 15 FF FF 00 00 {1} {2} 00  DD",
                        seriation.ToString("X2"),//命令序列码
                        cmd.VideoIn.ToString("X4"),//CAM
                        cmd.VideoOut.ToString("X4")//MON
                    );
                    break;
                case PTZCmdCodeEnum.GOTO_PRESET:
                    strCmd = string.Format("FA 55 AA 5F 40 01 00 06 00 00 00 {0} 00 30 00 00 00 00 {1} 07 00 00 {2}",
                        seriation.ToString("X2"),//命令序列码
                        cmd.VideoIn.ToString("X4"),
                        cmd.PresetIndex.ToString("X2")
                        );
                    break;
                case PTZCmdCodeEnum.SET_PRESET:
                    strCmd = string.Format("FA 55 AA 5F 40 01 00 06 00 00 00 {0} 00 30 00 00 00 00 {1} 06 00 00 {2}",
                       seriation.ToString("X2"),//命令序列码
                       cmd.VideoIn.ToString("X4"),
                       cmd.PresetIndex.ToString("X2")
                       );
                    break;
                case PTZCmdCodeEnum.MAT_RUN:
                    strCmd = string.Format("FA 55 AA 5F 40 01 00 0A 00 00 00 {0} 00 50 FF FF 00 00 00 {1} 02 00 00 {2} FF FF 00 37",
                       seriation.ToString("X2"),//命令序列码
                       cmd.AutoRunIndex.ToString("X2"),
                       cmd.VideoOut.ToString("X4")
                       );
                    break;
                case PTZCmdCodeEnum.MAT_HOLD:
                    strCmd = string.Format("FA 55 AA 5F 40 01 00 0A 00 00 00 {0} 00 50 FF FF 00 00 00 {1} 01 00 00 {2} FF FF 00 37",
                       seriation.ToString("X2"),//命令序列码
                       cmd.AutoRunIndex.ToString("X2"),
                       cmd.VideoOut.ToString("X2")
                       );
                    break;
                case PTZCmdCodeEnum.PAN_LEFT:
                    if (cmd.Stop==0)
                    {
                        //开始
                        strCmd = string.Format("FA 55 AA 5F 40 01 00 06 00 00 00 {0} 00 30 00 00 00 00{1}04{2}0001",
                                              seriation.ToString("X2"),//命令序列码
                                              cmd.VideoIn.ToString("X4"),
                                              (256-cmd.Speed*17).ToString("X2")
                                              );
                        break;
                    }
                    else
                    {
                        //停止
                        strCmd = string.Format("FA 55 AA 5F 40 01 00 06 00 00 00 {0} 00 30 00 00 00 00{1}04{2}0001",
                                           seriation.ToString("X2"),//命令序列码
                                           cmd.VideoIn.ToString("X4"),
                                           "00"
                                           );
                        break;
                    }
                    break;
                case PTZCmdCodeEnum.PAN_RIGHT:
                    if (cmd.Stop == 0)
                    {
                        //开始
                        strCmd = string.Format("FA 55 AA 5F 40 01 00 06 00 00 00 {0} 00 30 00 00 00 00{1}04{2}0001",
                                              seriation.ToString("X2"),//命令序列码
                                              cmd.VideoIn.ToString("X4"),
                                              (cmd.Speed * 17).ToString("X2")
                                              );
                        break;
                    }
                    else
                    {
                        //停止
                        strCmd = string.Format("FA 55 AA 5F 40 01 00 06 00 00 00 {0} 00 30 00 00 00 00{1}04{2}0001",
                                           seriation.ToString("X2"),//命令序列码
                                           cmd.VideoIn.ToString("X4"),
                                           "00"
                                           );
                        break;
                    }
                    break;
                case PTZCmdCodeEnum.TILT_UP:
                    if (cmd.Stop == 0)
                    {
                        //开始
                        strCmd = string.Format("FA 55 AA 5F 40 01 00 06 00 00 00 {0} 00 30 00 00 00 00{1}04{2}0002",
                                              seriation.ToString("X2"),//命令序列码
                                              cmd.VideoIn.ToString("X4"),
                                              (256 - cmd.Speed * 17).ToString("X2")
                                              );
                        break;
                    }
                    else
                    {
                        //停止
                        strCmd = string.Format("FA 55 AA 5F 40 01 00 06 00 00 00 {0} 00 30 00 00 00 00{1}04{2}0002",
                                           seriation.ToString("X2"),//命令序列码
                                           cmd.VideoIn.ToString("X4"),
                                           "00"
                                           );
                        break;
                    }
                    break;
                case PTZCmdCodeEnum.TILT_DOWN:
                    if (cmd.Stop == 0)
                    {
                        //开始
                        strCmd = string.Format("FA 55 AA 5F 40 01 00 06 00 00 00 {0} 00 30 00 00 00 00{1}04{2}00 02",
                                              seriation.ToString("X2"),//命令序列码
                                              cmd.VideoIn.ToString("X4"),
                                              (cmd.Speed * 17).ToString("X2")
                                              );
                        break;
                    }
                    else
                    {
                        //停止
                        strCmd = string.Format("FA 55 AA 5F 40 01 00 06 00 00 00 {0} 00 30 00 00 00 00{1}04{2}00 02",
                                           seriation.ToString("X2"),//命令序列码
                                           cmd.VideoIn.ToString("X4"),
                                           "00"
                                           );
                        break;
                    }
                    break;
                case PTZCmdCodeEnum.DOWN_LEFT:
                    if (cmd.Stop == 0)
                    {
                        //开始
                        strCmd = string.Format("FA 55 AA 5F 40 01 00 06 00 00 00 {0} 00 30 00 00 00 00{1}04{2}00 02",
                                              seriation.ToString("X2"),//命令序列码
                                              cmd.VideoIn.ToString("X4"),
                                              (cmd.Speed * 17).ToString("X2")
                                              );
                        strCmd +=string.Format("FA 55 AA 5F 40 01 00 06 00 00 00 {0} 00 30 00 00 00 00{1}04{2}00 01",
                                              seriation.ToString("X2"),//命令序列码
                                              cmd.VideoIn.ToString("X4"),
                                              (256-cmd.Speed * 17).ToString("X2")
                                              );
                        break;
                    }
                    else
                    {
                        //停止
                        strCmd = string.Format("FA 55 AA 5F 40 01 00 06 00 00 00 {0} 00 30 00 00 00 00{1}04{2}00 02",
                                           seriation.ToString("X2"),//命令序列码
                                           cmd.VideoIn.ToString("X4"),
                                           "00"
                                           );
                        strCmd+= string.Format("FA 55 AA 5F 40 01 00 06 00 00 00 {0} 00 30 00 00 00 00{1}04{2}00 01",
                                           seriation.ToString("X2"),//命令序列码
                                           cmd.VideoIn.ToString("X4"),
                                           "00"
                                           );
                        break;
                    }
                    break;
                case PTZCmdCodeEnum.DOWN_RIGHT:
                    if (cmd.Stop == 0)
                    {
                        //开始
                        strCmd = string.Format("FA 55 AA 5F 40 01 00 06 00 00 00 {0} 00 30 00 00 00 00{1}04{2}00 02",
                                              seriation.ToString("X2"),//命令序列码
                                              cmd.VideoIn.ToString("X4"),
                                              (cmd.Speed * 17).ToString("X2")
                                              );
                        strCmd += string.Format("FA 55 AA 5F 40 01 00 06 00 00 00 {0} 00 30 00 00 00 00{1}04{2}00 01",
                                              seriation.ToString("X2"),//命令序列码
                                              cmd.VideoIn.ToString("X4"),
                                              (cmd.Speed * 17).ToString("X2")
                                              );
                        break;
                    }
                    else
                    {
                        //停止
                        strCmd = string.Format("FA 55 AA 5F 40 01 00 06 00 00 00 {0} 00 30 00 00 00 00{1}04{2}00 02",
                                           seriation.ToString("X2"),//命令序列码
                                           cmd.VideoIn.ToString("X4"),
                                           "00"
                                           );
                        strCmd += string.Format("FA 55 AA 5F 40 01 00 06 00 00 00 {0} 00 30 00 00 00 00{1}04{2}00 01",
                                           seriation.ToString("X2"),//命令序列码
                                           cmd.VideoIn.ToString("X4"),
                                           "00"
                                           );
                        break;
                    }
                    break;
                case PTZCmdCodeEnum.UP_LEFT:
                    if (cmd.Stop == 0)
                    {
                        //开始
                        strCmd = string.Format("FA 55 AA 5F 40 01 00 06 00 00 00 {0} 00 30 00 00 00 00{1}04{2}00 02",
                                              seriation.ToString("X2"),//命令序列码
                                              cmd.VideoIn.ToString("X4"),
                                              (256-cmd.Speed * 17).ToString("X2")
                                              );
                        strCmd += string.Format("FA 55 AA 5F 40 01 00 06 00 00 00 {0} 00 30 00 00 00 00{1}04{2}00 01",
                                              seriation.ToString("X2"),//命令序列码
                                              cmd.VideoIn.ToString("X4"),
                                              (256 - cmd.Speed * 17).ToString("X2")
                                              );
                        break;
                    }
                    else
                    {
                        //停止
                        strCmd = string.Format("FA 55 AA 5F 40 01 00 06 00 00 00 {0} 00 30 00 00 00 00{1}04{2}00 02",
                                           seriation.ToString("X2"),//命令序列码
                                           cmd.VideoIn.ToString("X4"),
                                           "00"
                                           );
                        strCmd += string.Format("FA 55 AA 5F 40 01 00 06 00 00 00 {0} 00 30 00 00 00 00{1}04{2}00 01",
                                           seriation.ToString("X2"),//命令序列码
                                           cmd.VideoIn.ToString("X4"),
                                           "00"
                                           );
                        break;
                    }
                    break;
                case PTZCmdCodeEnum.UP_RIGHT:
                    if (cmd.Stop == 0)
                    {
                        //开始
                        strCmd = string.Format("FA 55 AA 5F 40 01 00 06 00 00 00 {0} 00 30 00 00 00 00{1}04{2}00 02",
                                              seriation.ToString("X2"),//命令序列码
                                              cmd.VideoIn.ToString("X4"),
                                              (256-cmd.Speed * 17).ToString("X2")
                                              );
                        strCmd += string.Format("FA 55 AA 5F 40 01 00 06 00 00 00 {0} 00 30 00 00 00 00{1}04{2}00 01",
                                              seriation.ToString("X2"),//命令序列码
                                              cmd.VideoIn.ToString("X4"),
                                              (cmd.Speed * 17).ToString("X2")
                                              );
                        break;
                    }
                    else
                    {
                        //停止
                        strCmd = string.Format("FA 55 AA 5F 40 01 00 06 00 00 00 {0} 00 30 00 00 00 00{1}04{2}00 02",
                                           seriation.ToString("X2"),//命令序列码
                                           cmd.VideoIn.ToString("X4"),
                                           "00"
                                           );
                        strCmd += string.Format("FA 55 AA 5F 40 01 00 06 00 00 00 {0} 00 30 00 00 00 00{1}04{2}00 01",
                                           seriation.ToString("X2"),//命令序列码
                                           cmd.VideoIn.ToString("X4"),
                                           "00"
                                           );
                        break;
                    }
                    break;


                case PTZCmdCodeEnum.ZOOM_IN:
                    if (cmd.Stop == 0)
                    {
                        //开始
                        strCmd = string.Format("FA 55 AA 5F 40 01 00 06 00 00 00 {0} 00 30 00 00 00 00 {1} 04 10 00 03",
                                              seriation.ToString("X2"),//命令序列码
                                              cmd.VideoIn.ToString("X4")
                                              );
                        break;
                    }
                    else
                    {
                        //停止
                        strCmd = string.Format("FA 55 AA 5F 40 01 00 06 00 00 00 {0} 00 30 00 00 00 00 {1} 04 00 00 03",
                                             seriation.ToString("X2"),//命令序列码
                                             cmd.VideoIn.ToString("X4")
                                             );
                    }
                    break;
                case PTZCmdCodeEnum.ZOOM_OUT:
                    if (cmd.Stop == 0)
                    {
                        //开始
                        strCmd = string.Format("FA 55 AA 5F 40 01 00 06 00 00 00 {0} 00 30 00 00 00 00 {1} 04 F0 00 03",
                                              seriation.ToString("X2"),//命令序列码
                                              cmd.VideoIn.ToString("X4")
                                              );
                        break;
                    }
                    else
                    {
                        //停止
                        strCmd = string.Format("FA 55 AA 5F 40 01 00 06 00 00 00 {0} 00 30 00 00 00 00 {1} 04 F0 00 03",
                                             seriation.ToString("X2"),//命令序列码
                                             cmd.VideoIn.ToString("X4")
                                             );
                    }
                    break;
                case PTZCmdCodeEnum.FOCUS_NEAR:
                    if (cmd.Stop == 0)
                    {
                        //开始
                        strCmd = string.Format("FA 55 AA 5F 40 01 00 06 00 00 00 {0} 00 30 00 00 00 00 {1} 03 10 00 05",
                                              seriation.ToString("X2"),//命令序列码
                                              cmd.VideoIn.ToString("X4")
                                              );
                        break;
                    }
                    else
                    {
                        //停止
                        strCmd = string.Format("FA 55 AA 5F 40 01 00 06 00 00 00 {0} 00 30 00 00 00 00 {1} 03 00 00 05",
                                             seriation.ToString("X2"),//命令序列码
                                             cmd.VideoIn.ToString("X4")
                                             );
                    }
                    break;
                case PTZCmdCodeEnum.FOCUS_FAR:
                    if (cmd.Stop == 0)
                    {
                        //开始
                        strCmd = string.Format("FA 55 AA 5F 40 01 00 06 00 00 00 {0} 00 30 00 00 00 00 {1} 03 F0 00 05",
                                              seriation.ToString("X2"),//命令序列码
                                              cmd.VideoIn.ToString("X4")
                                              );
                        break;
                    }
                    else
                    {
                        //停止
                        strCmd = string.Format("FA 55 AA 5F 40 01 00 06 00 00 00 {0} 00 30 00 00 00 00 {1} 03 00 00 05",
                                             seriation.ToString("X2"),//命令序列码
                                             cmd.VideoIn.ToString("X4")
                                             );
                    }
                    break;
                case PTZCmdCodeEnum.IRIS_CLOSE:
                    if (cmd.Stop == 0)
                    {
                        //开始
                        strCmd = string.Format("FA 55 AA 5F 40 01 00 06 00 00 00 {0} 00 30 00 00 00 00 {1} 03 F0 00 04",
                                              seriation.ToString("X2"),//命令序列码
                                              cmd.VideoIn.ToString("X4")
                                              );
                        break;
                    }
                    else
                    {
                        //停止
                        strCmd = string.Format("FA 55 AA 5F 40 01 00 06 00 00 00 {0} 00 30 00 00 00 00 {1} 03 00 00 04",
                                             seriation.ToString("X2"),//命令序列码
                                             cmd.VideoIn.ToString("X4")
                                             );
                    }
                    break;
                case PTZCmdCodeEnum.IRIS_OPEN:
                    if (cmd.Stop == 0)
                    {
                        //开始
                        strCmd = string.Format("FA 55 AA 5F 40 01 00 06 00 00 00 {0} 00 30 00 00 00 00 {1} 03 10 00 04",
                                              seriation.ToString("X2"),//命令序列码
                                              cmd.VideoIn.ToString("X4")
                                              );
                        break;
                    }
                    else
                    {
                        //停止
                        strCmd = string.Format("FA 55 AA 5F 40 01 00 06 00 00 00 {0} 00 30 00 00 00 00 {1} 03 00 00 04",
                                             seriation.ToString("X2"),//命令序列码
                                             cmd.VideoIn.ToString("X4")
                                             );
                    }
                    break;
            }
            int cOut;
            cmd.OutBuffer = pubFun.GetBytes(strCmd, out cOut);
       }
    }
}
