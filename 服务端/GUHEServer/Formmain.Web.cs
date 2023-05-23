using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using GHIBMS.Common;
using System.Xml;
using System.IO;
using System.Collections.Specialized;

using System.Diagnostics;
using GHIBMS.Interface;



namespace GHIBMS.Server
{

    public partial class FormMain 
    {

         private void IISWebServer__OnStart(object sender, EventArgs e)
         {
             AddOperationLog(StrConst.SERVERITY_MSG, StrConst.TITLE_SYS, "", "Web服务启动，端口号：" + ServerConfig.webDataPort.ToString());
         }
         private void IISWebServer__OnStop(object sender, EventArgs e)
         {
             AddOperationLog(StrConst.SERVERITY_MSG, StrConst.TITLE_SYS, "", "Web服务关闭，端口号：" + ServerConfig.webDataPort.ToString());
         }
       //WebService服务
       //以下代码为多线程调用

         private void IISWebServer_IISAS(System.Net.Sockets.Socket Socket_, Method Method_, IISWebSend IISWebSend_, string Header_, string Body_)
         {
             //Msgs.Add("Header:" + Header_ + "\r\n body:" + Body_);

             //可在此做返回给客户访问或提交相关信息,如:
             //IISWebSend_.SendMessage_ = "<H2>服务器返回的信息</H2><Br>" + DateTime.Now.ToString();
             ////Version_ = "HTTP/1.1";//此项不用设置,在对像IISWebSend_中已自动设置,当然人为设置亦可.

             //IISWebSend_.MIMEHeader_ = "text/html";

             //IISWebSend_.StatusCode_ = " 202 OK";

             //IISWebSend_.SendHeader();//发送头信息

             //IISWebSend_.SendToBrowser();//发送主体信息

             //Socket_.Close();//关闭连接(若此处不关闭,会在对像IISWebSend_处理默认返回给远程端数据,关闭对像IISWebSend_不会再做返回信息的操作)

             int iStartPos = 0;
             String sRequest;  //WEB请求头 0--HTTP/1.1 url
             String sRequestedFile; //WEB请求文件


             if (!Socket_.Connected)
             {
                 return;
             }

             // Debug.WriteLine("Receive buffer:" + sBuffer);
             // 查找 "HTTP" 的位置 
             iStartPos = Header_.IndexOf("HTTP", 1);
             // 得到请求类型（GET/POST）和文件目录文件名 
             sRequest = Header_.Substring(0, iStartPos - 1);

             //得带请求文件名(命令码+参数)
             iStartPos = sRequest.LastIndexOf("/") + 1;
             sRequestedFile = sRequest.Substring(iStartPos).Trim();
             //AddOperationLog(StrConst.SERVERITY_MSG, StrConst.TITLE_SYS, "", "收到Web请求：" + sRequestedFile);


             //刷新画面数据请求 改为POST方法
             if (sRequestedFile.Contains(CommandEnum.GetFormData.ToString()))
             {
                 try
                 {
                     string ret = "";
                     if (Method_ == Method.POST)//sRequestedFile == "" || 
                     {
                         ret = ResponeGetFormData(Body_);
                     }


                     IISWebSend_.SendMessage_ = StrConst.XML_HEAD + ret;

                     //Version_ = "HTTP/1.1";//此项不用设置,在对像IISWebSend_中已自动设置,当然人为设置亦可.

                     IISWebSend_.MIMEHeader_ = "text/xml";

                     IISWebSend_.StatusCode_ = " 200 OK";
                     try
                     {
                         IISWebSend_.SendHeader();//发送头信息

                         IISWebSend_.SendToBrowser();//发送主体信息
                     }
                     catch (Exception ex)
                     {
                         Console.WriteLine(ex.ToString());
                     }
                     try
                     {
                         if (Socket_ != null)
                         {
                             Socket_.Close();
                             Socket_ = null;
                         }
                         return;
                     }
                     catch
                     {
                         Socket_ = null;
                     }
                 }
                 catch (Exception e)
                 {
                     Console.WriteLine("发生错误 : {0} ", e);
                     return;
                 }
             }
             //数据点设定值
             else if (sRequestedFile.Contains(CommandEnum.SetPointData.ToString()))
             {
                 try
                 {
                     string baseUrl;
                     NameValueCollection nvc;

                     pubFun.ParseUrl(sRequestedFile, out baseUrl, out nvc);
                     Debug.WriteLine(baseUrl);
                     string pointName = "";
                     string pointValue = "";
                     string pointType = "";
                     foreach (string key in nvc.Keys)
                     {
                         if (key == "name")  //Debug.WriteLine("{0}:{1}", key, nvc[key]);
                             pointName = nvc[key];
                         if (key == "type")  //Debug.WriteLine("{0}:{1}", key, nvc[key]);
                             pointType = nvc[key];
                         if (key == "value")  //Debug.WriteLine("{0}:{1}", key, nvc[key]);
                             pointValue = nvc[key];
                     }
                     if (pointName != "")
                     {
                         //更新内存链表并写入设备
                         //Write2Device(pointName, pointValue);
                         IVariable var = Rtdb.GetVariableByNameRun(pointName);
                         if (var != null)
                         {
                             string value="0";
                             if  (var.Value != null)
                                 value = var.Value.ToString();
                             if (pointType != "固定数值")
                             {
                                 if (StringHelper.IsFloat(value))
                                 {
                                     double db = Convert.ToDouble(value);
                                     if (pointType == "自动递减")
                                     {
                                         db++;
                                     }
                                     else if (pointType == "自动递增")
                                     {
                                         db--;
                                     }
                                     else if (pointType == "自动取反")
                                     {
                                         if (db != 0)
                                         {
                                             db = 0;
                                         }
                                         else
                                         {
                                             db = 1;
                                         }
                                     }
                                     var.WriteValue(db); ;
                                 }
                                 else
                                 {
                                     if (pointType == "自动取反")
                                     {
                                         if (value.ToLower() != "true")
                                             var.WriteValue("True");
                                         else
                                             var.WriteValue("False");

                                     }

                                 }
                             }
                             else
                             {
                                 var.WriteValue(pointValue);

                             }



                            
                         }
                         IISWebSend_.SendMessage_ = "<H2>Write2Device OK " + pointName + "=" + pointValue + "</H2>";
                         IISWebSend_.MIMEHeader_ = "text/html";

                         IISWebSend_.StatusCode_ = " 200 OK";
                         IISWebSend_.SendHeader();
                         IISWebSend_.SendToBrowser();

                     }
                     try
                     {
                         if (Socket_ != null)
                         {
                             Socket_.Close();
                             Socket_ = null;
                         }
                         return;
                     }
                     catch
                     {
                         Socket_ = null;
                     }
                 }
                 catch (Exception e)
                 {
                     Console.WriteLine("发生错误 : {0} ", e);
                     return;
                 }
             }
             else if (sRequestedFile.Contains(CommandEnum.GetAlarmCount.ToString()))//读当前报警的数量
             {
                 try
                 {
                     IISWebSend_.SendMessage_ = Almdb.GetAlarmCount().ToString();
                     //Version_ = "HTTP/1.1";
                     IISWebSend_.MIMEHeader_ = "text/html";
                     IISWebSend_.StatusCode_ = " 200 OK";
                     IISWebSend_.SendHeader();
                     IISWebSend_.SendToBrowser();
                     try
                     {
                         if (Socket_ != null)
                         {
                             Socket_.Close();
                             Socket_ = null;
                         }
                         return;
                     }
                     catch
                     {
                         Socket_ = null;
                     }

                 }

                 catch (Exception e)
                 {
                     Console.WriteLine("发生错误 : {0} ", e);
                     return;
                 }
             }
             else if (sRequestedFile.Contains(CommandEnum.GetAlarmList.ToString()))//读当前报警的列表
             {
                 try
                 {
                     IISWebSend_.SendMessage_ = Almdb.AlarmList2XML();
                     //Version_ = "HTTP/1.1";
                     IISWebSend_.MIMEHeader_ = "text/html";
                     IISWebSend_.StatusCode_ = " 200 OK";
                     IISWebSend_.SendHeader();
                     IISWebSend_.SendToBrowser();
                     try
                     {
                         if (Socket_ != null)
                         {
                             Socket_.Close();
                             Socket_ = null;
                         }
                         return;
                     }
                     catch
                     {
                         Socket_ = null;
                     }


                 }
                 catch (Exception e)
                 {
                     Console.WriteLine("发生错误 : {0} ", e);
                     return;
                 }
             }
             else if (sRequestedFile.Contains(CommandEnum.GetOneAlarm.ToString()))//读报警详细信息
             {
                 try
                 {
                     string baseUrl;
                     NameValueCollection nvc;

                     pubFun.ParseUrl(sRequestedFile, out baseUrl, out nvc);
                     Debug.WriteLine(baseUrl);
                     string guid = "";

                     foreach (string key in nvc.Keys)
                     {
                         if (key == "guid")  //Debug.WriteLine("{0}:{1}", key, nvc[key]);
                             guid = nvc[key];
                     }
                     IISWebSend_.SendMessage_ = Almdb.S_OnGetOneAlarm(guid);

                     //Version_ = "HTTP/1.1";

                     IISWebSend_.MIMEHeader_ = "text/html";
                     IISWebSend_.StatusCode_ = " 200 OK";
                     IISWebSend_.SendHeader();
                     IISWebSend_.SendToBrowser();
                     try
                     {
                         if (Socket_ != null)
                         {
                             Socket_.Close();
                             Socket_ = null;
                         }
                         return;
                     }
                     catch
                     {
                         Socket_ = null;
                     }


                 }
                 catch (Exception e)
                 {
                     Console.WriteLine("发生错误 : {0} ", e);
                     return;
                 }
             }
             else if (sRequestedFile.Contains(CommandEnum.GetNewAlarm.ToString()))//读最新报警
             {
                 try
                 {
                     IISWebSend_.SendMessage_ = Almdb.S_OnGetNewAlarm();
                     //Version_ = "HTTP/1.1";
                     IISWebSend_.MIMEHeader_ = "text/html";
                     IISWebSend_.StatusCode_ = " 200 OK";
                     IISWebSend_.SendHeader();
                     IISWebSend_.SendToBrowser();
                     try
                     {
                         if (Socket_ != null)
                         {
                             Socket_.Close();
                              Socket_ = null;
                         }
                         return;
                     }
                     catch
                     {
                         Socket_ = null;
                     }
                 }
                 catch (Exception e)
                 {
                     Console.WriteLine("发生错误 : {0} ", e);
                     return;
                 }

             }
             //消除一个报警
             else if (sRequestedFile.Contains(CommandEnum.ClearAlarm.ToString()))
             {
                 try
                 {
                     string baseUrl;
                     NameValueCollection nvc;

                     pubFun.ParseUrl(sRequestedFile, out baseUrl, out nvc);

                     string userName = "";
                     string alarmGuid = "";
                     string result = "";
                     foreach (string key in nvc.Keys)
                     {
                         if (key == "userName")  //Debug.WriteLine("{0}:{1}", key, nvc[key]);
                             userName = nvc[key];
                         if (key == "alarmGuid")  //Debug.WriteLine("{0}:{1}", key, nvc[key]);
                             alarmGuid = nvc[key];

                         if (key == "result")  //Debug.WriteLine("{0}:{1}", key, nvc[key]);
                             result = nvc[key];
                     }
                     if (alarmGuid != "")
                     {
                         netService.dataAnalysis.C_AlarmClear(userName, alarmGuid, result);
                         IISWebSend_.SendMessage_ = "<H2>AlarmClear OK </H2><Br>" + DateTime.Now.ToString();

                         //Version_ = "HTTP/1.1";

                         IISWebSend_.MIMEHeader_ = "text/html";
                         IISWebSend_.StatusCode_ = " 200 OK";
                         IISWebSend_.SendHeader();
                         IISWebSend_.SendToBrowser();
                         try
                         {
                             if (Socket_ != null)
                             {
                                 Socket_.Close();
                             }
                             return;
                         }
                         catch
                         {
                             Socket_ = null;
                         }

                     }
                 }
                 catch (Exception e)
                 {
                     Console.WriteLine("发生错误 : {0} ", e);
                     return;
                 }
             }
             //矩阵云台控制
             else if (sRequestedFile.Contains(CommandEnum.VideoControl.ToString()))
             {
                 try
                 {
                     string baseUrl;
                     NameValueCollection nvc;
                     pubFun.ParseUrl(sRequestedFile, out baseUrl, out nvc);
                     //Debug.WriteLine(baseUrl);
                     string parm = "";
                     foreach (string key in nvc.Keys)
                     {
                         if (key == "parm")  //Debug.WriteLine("{0}:{1}", key, nvc[key]);
                             parm = nvc[key];
                     }
                     string[] strArray = parm.Split(new char[] { '|' });
                     if (strArray.Length == 8)
                     {
                         string cmd = strArray[0];
                         string camName = strArray[1];
                         string monName = strArray[2];
                         string videoIn = strArray[3];
                         string videoOut = strArray[4];
                         string vparam1 = strArray[5];
                         string vparam2 = strArray[6];
                         string vparam3 = strArray[7];

                         VideoCommandArgs args = new VideoCommandArgs();
                         args.CamName = camName;
                         args.MonName = monName;
                         args.VideoIn = pubFun.IsNumeric(videoIn);
                         args.VideoOut = pubFun.IsNumeric(videoOut);
                         args.VideoCommand = (PTZCmdCodeEnum)pubFun.IsInt(cmd, 0);

                         #region 处理最后的3个可变含义参数
                         switch (args.VideoCommand)
                         {
                             //云台镜头 
                             case PTZCmdCodeEnum.AUX_PWRON1: //"6"
                             case PTZCmdCodeEnum.AUX_PWRON2:  //"7"
                             case PTZCmdCodeEnum.DOWN_LEFT: //"27"
                             case PTZCmdCodeEnum.DOWN_RIGHT:// "28"
                             case PTZCmdCodeEnum.FAN_PWRON: // "4":
                             case PTZCmdCodeEnum.FOCUS_FAR: // "14":
                             case PTZCmdCodeEnum.FOCUS_NEAR:// "13"
                             case PTZCmdCodeEnum.HEATER_PWRON:// "5"
                             case PTZCmdCodeEnum.IRIS_CLOSE:// "16"
                             case PTZCmdCodeEnum.IRIS_OPEN:// "15"
                             case PTZCmdCodeEnum.PAN_AUTO:// "2"
                             case PTZCmdCodeEnum.PAN_LEFT:// "23"
                             case PTZCmdCodeEnum.PAN_RIGHT:// "24"
                             case PTZCmdCodeEnum.TILT_DOWN:// "22"
                             case PTZCmdCodeEnum.TILT_UP:// "21"
                             case PTZCmdCodeEnum.UP_LEFT:// "25"
                             case PTZCmdCodeEnum.UP_RIGHT:// "26"
                             case PTZCmdCodeEnum.WIPER_PWROFF:// "3"
                             case PTZCmdCodeEnum.ZOOM_IN:// "11"
                             case PTZCmdCodeEnum.ZOOM_OUT:// "12"
                                 args.Stop = uint.Parse(vparam1);
                                 args.Speed = uint.Parse(vparam2);
                                 break;
                             //预置位
                             case PTZCmdCodeEnum.CLE_PRESET: // "9"
                             case PTZCmdCodeEnum.GOTO_PRESET:// "39"
                             case PTZCmdCodeEnum.SET_PRESET: // "8"
                                 args.PresetIndex = uint.Parse(vparam1);
                                 break;
                             //巡航
                             case PTZCmdCodeEnum.FILL_PRE_SEQ:// "30"
                             case PTZCmdCodeEnum.SET_SEQ_DWELL:// "31"
                             case PTZCmdCodeEnum.SET_SEQ_SPEED:// "32"
                             case PTZCmdCodeEnum.CLE_PRE_SEQ:// "33"
                             case PTZCmdCodeEnum.RUN_SEQ:// "37"
                             case PTZCmdCodeEnum.STOP_SEQ:// "38"
                                 args.CruiseRoute = Convert.ToByte(vparam1);
                                 args.CruisePoint = Convert.ToByte(vparam2);
                                 args.Input = Convert.ToUInt16(vparam3);
                                 break;
                             //轨迹
                             case PTZCmdCodeEnum.STA_MEM_CRUISE:// "34"
                             case PTZCmdCodeEnum.STO_MEM_CRUISE:// "35"
                             case PTZCmdCodeEnum.RUN_CRUISE:// "36"
                                 break;
                             case PTZCmdCodeEnum.MAT_MON_SW:// "100":
                             case PTZCmdCodeEnum.MAT_CAM_SW:// "101":
                             case PTZCmdCodeEnum.MAT_VIDEO_SW:// "102":
                             case PTZCmdCodeEnum.MAT_RUN:// "103"
                                 args.AutoRunIndex = uint.Parse(vparam1);
                                 break;
                             case PTZCmdCodeEnum.MAT_HOLD: //"104":
                                 break;
                             case PTZCmdCodeEnum.MAT_GROUP:// "105"
                                 args.GroupIndex = uint.Parse(vparam1);
                                 break;
                         }
                         #endregion

                         netService.dataAnalysis.C_VideoControl(args);

                         string msg = string.Format("WEB客户端{0}矩阵云台控制，命令码：{1}，摄像机{2},监视器{3}}", Socket_.RemoteEndPoint, args.VideoCommand.ToString(), args.VideoIn,args.MonName,args);
                         AddOperationLog(StrConst.SERVERITY_MSG, StrConst.TITLE_OPER, "", msg);
                         IISWebSend_.SendMessage_ = "<H2>VideoControl OK </H2><Br>" + DateTime.Now.ToString();
                      
                         //Version_ = "HTTP/1.1";

                         IISWebSend_.MIMEHeader_ = "text/html";
                         IISWebSend_.StatusCode_ = " 200 OK";
                         IISWebSend_.SendHeader();
                         IISWebSend_.SendToBrowser();
                         try
                         {
                             if (Socket_ != null)
                             {
                                 Socket_.Close();
                             }
                             return;
                         }
                         catch
                         {
                             Socket_ = null;
                         }

                     }

                 }
                 catch (Exception e)
                 {
                     Console.WriteLine("发生错误 : {0} ", e);
                     return;
                 }

             }
         }
         private string ResponeGetFormData(string points)
         {
             try
             {
                 string xml = "";
                 using (StringWriter sw = new StringWriter())
                 {
                     XmlTextWriter writer = new XmlTextWriter(sw);
                     writer.WriteStartElement("objectlist");

                     string[] pointAarray = points.Split(new char[]{'|'}, StringSplitOptions.RemoveEmptyEntries);
                         foreach (string s in pointAarray)
                         {
                             string[] lines = s.Split(new char[] {':'}, StringSplitOptions.RemoveEmptyEntries);
                             if (lines.Length == 2)
                             {
                                 string id = lines[0];
                                 string point = lines[1];
                                 IVariable var = Rtdb.GetVariableByNameRun(point);
                                 if (var != null)
                                 {
                                     string value = var.Value.ToString();
                                     writer.WriteStartElement("object");
                                     writer.WriteAttributeString("id", id);
                                     writer.WriteAttributeString("value", value);
                                     writer.WriteAttributeString("desc", var.GetValueStateDesc());
                                     writer.WriteEndElement();
                                 }
                             }

                         }
                     writer.WriteEndElement();
                     xml = sw.ToString();
                     writer.Close();
                   //  sw.Close();
                     return xml;
                     
                    }
             } catch { }
             return "";
         }
        
    }

}
