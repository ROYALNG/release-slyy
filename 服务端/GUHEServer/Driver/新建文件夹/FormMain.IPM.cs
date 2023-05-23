using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Text.RegularExpressions;
using GHIBMS.Server;
using IPModuleLib;
using System.IO;
using System.Threading;
using GHIBMS.Common;
using GHIBMS.IPM;

namespace GHIBMS.Server
{
    public partial class FormMain
    {
         #region Honeywell IPM事件处理
         /*public void StartIPMService(ChannelInfo cha)
         {
             try
             {
                 
                 UpdateTree(cha.Name, NodeStateEnum.OffLine);
                 foreach (ControllerInfo con in cha.ConList)
                 {
                     UpdateTree(con.Name, NodeStateEnum.OffLine);
                 }
                 FormIPMOCX IPM = new FormIPMOCX(cha.Name);
                 IPM.Text = cha.Name;
                 IPM.OnDeviceConnected += new FormIPMOCX.DeviceConnected(IPM_OnDeviceConnected);
                 IPM.OnDeviceDisConnected += new FormIPMOCX.DeviceDisConnected(IPM_OnDeviceDisConnected);
                 IPM.OnVistaCIDReport += new FormIPMOCX.VistaCIDReport(IPM_OnVistaCIDReport);
                 IPM.OnVistaPanelStatus += new FormIPMOCX.VistaPanelStatus(IPM_OnVistaPanelStatus);
                 IPM.OnVistaKeypadInfo += new FormIPMOCX.VistaKeypadInfo(IPM_OnVistaKeypadInfo);
               
                 ipmContrller.OnIPMctrl += new IPMController.OnIPMctrldelegate(ipmContrller_OnIPMctrl);

                 if ((IPM.Startup(cha.NetPort) == 1))
                 {
                     cha.Active = true;
                   
                     string msg=string.Format("HoneywellIPM通讯服务启动成功！通道名称：{0} 网络端口号：{1}",cha.Name,cha.NetPort);
                     AddOperationLog(StrConst.SERVERITY_MSG,StrConst.TITLE_OPER,"",msg);
                     UpdateTree(cha.Name,NodeStateEnum.OnLine);

                 }
                 else
                 {
                     cha.Active = false;
                    // Log("通讯通道" + cha.Name + ",网络端口号：" + cha.NetPort.ToString() + "启动成功！");
                     string msg = string.Format("HoneywellIPM通讯服务启动失败！通道名称：{0} 网络端口号：{1}", cha.Name, cha.NetPort);
                     AddOperationLog(StrConst.SERVERITY_MSG, StrConst.TITLE_OPER, "", msg);
                     UpdateTree(cha.Name,NodeStateEnum.OffLine);

                 }
                 cha.CommObject = IPM;
             }
             catch (Exception ex)
             {
                 
                 Logger.GetInstance().LogError(ex.ToString());
             }
         }
         public void StopIPMService(ChannelInfo cha)
         {
             try
             {
                 if (cha.CommObject == null) return;
                 FormIPMOCX IPM = cha.CommObject as FormIPMOCX;
                 IPM.OnDeviceConnected -= new FormIPMOCX.DeviceConnected(IPM_OnDeviceConnected);
                 IPM.OnDeviceDisConnected -= new FormIPMOCX.DeviceDisConnected(IPM_OnDeviceDisConnected);
                 IPM.OnVistaCIDReport += new FormIPMOCX.VistaCIDReport(IPM_OnVistaCIDReport);
                 IPM.OnVistaPanelStatus -= new FormIPMOCX.VistaPanelStatus(IPM_OnVistaPanelStatus);
                 IPM.OnVistaKeypadInfo -= new FormIPMOCX.VistaKeypadInfo(IPM_OnVistaKeypadInfo);

                 ipmContrller.OnIPMctrl -= new IPMController.OnIPMctrldelegate(ipmContrller_OnIPMctrl);

                 IPM.Shutdown();

                 cha.Active=false;
                 UpdateTree(cha.Name, NodeStateEnum.OffLine);
                 //Log("通讯通道" + cha.Name + ",网络端口号：" + cha.NetPort.ToString() + "已经关闭！");
                 string msg = string.Format("HoneywellIPM通讯服务关闭成功！通道名称：{0} 网络端口号：{1}", cha.Name, cha.NetPort);
                 AddOperationLog(StrConst.SERVERITY_MSG, StrConst.TITLE_OPER, "", msg);
                 Thread.Sleep(100);
                 IPM.Dispose();




             }
             catch (Exception ex)
             {
                 Logger.GetInstance().LogError(ex.ToString());
             }
            
         }
       
         public delegate void OnIpmCtrlDelegate(AlarmCtrlCommand Ctrlcmd);
         private void OnIPMctrlCallback(AlarmCtrlCommand Ctrlcmd)
         {
             try
             {
                 switch (Ctrlcmd.Command)
                 {
                     case PanelCtrlEnum.OFF:  //撤防
                     case PanelCtrlEnum.AWAY:  //外出布防
                     case PanelCtrlEnum.STAY:  //留守布防
                     case PanelCtrlEnum.MAXIMUM:  //最大布防
                     case PanelCtrlEnum.INSTANT:  //限时面布防
                         ((FormIPMOCX)Ctrlcmd.Conntroller.ChannelObject.CommObject).SendKeySequence(Ctrlcmd.Conntroller.MacAddress, Ctrlcmd.Conntroller.PassWord + ((int)Ctrlcmd.Command).ToString());
                         break;
                     case PanelCtrlEnum.BYPASS:  //防区旁路
                         ((FormIPMOCX)Ctrlcmd.Conntroller.ChannelObject.CommObject).SendKeySequence(Ctrlcmd.Conntroller.MacAddress, Ctrlcmd.Conntroller.PassWord + ((int)Ctrlcmd.Command).ToString() + pubFun.FormatInt(3, Ctrlcmd.Zone));
                         break;
                     case PanelCtrlEnum.SENTKEY:  //sentkey
                         ((FormIPMOCX)Ctrlcmd.Conntroller.ChannelObject.CommObject).Sendkey(Ctrlcmd.Conntroller.MacAddress, Ctrlcmd.Key);
                         break;
                     case PanelCtrlEnum.SENTKEYSEQ:  //SendKeySequence
                         ((FormIPMOCX)Ctrlcmd.Conntroller.ChannelObject.CommObject).SendKeySequence(Ctrlcmd.Conntroller.MacAddress, Ctrlcmd.Keyseq);
                         break;
                 }
             }
             catch (Exception ex)
             {
                 Logger.GetInstance().LogError(ex.ToString());
             }
         }
         private void ipmContrller_OnIPMctrl(AlarmCtrlCommand alarmcommand)
         {
             this.BeginInvoke(new OnIpmCtrlDelegate(OnIPMctrlCallback), new object[] { alarmcommand });

         }
        */
         public delegate void IPMOnVistaKeypadInfoCallback(object sender, AxIPModuleLib._ICooMonitorEvents_VistaKeypadInfoEvent e);
         private void IPMOnVistaKeypadInfo(object sender, AxIPModuleLib._ICooMonitorEvents_VistaKeypadInfoEvent e)
         {
             try
             {
                 string strMac = e.strMac;
                 string strInfo = e.strInfo;
                 int progState = e.lState;
                 int playback = e.lPlayback;
                 int zone = 0;
                 if (playback != 0) return;
                 //解析主机状态,更新实时数据库
               
                 if (strInfo.IndexOf("ARMED") > -1 && strInfo.IndexOf("ALL SECURE") > -1)  //布防状态无报警
                 {
                     Device2varListByMac(strMac, StrConst.ALARM_PANEL_ISREADY_ADDR, ((int)PanelReadyStateEnum.Ready).ToString());  
                     Device2varListByMac(strMac, StrConst.ALARM_PANEL_ISARMED_ADDR, ((int)PanelArmStateEnum.Armed).ToString());  
                 }
                 else if (strInfo.IndexOf("ARMED") > -1 && strInfo.IndexOf("ZONE BYPASSED") > -1)  //布防状态有旁路
                 {
                     Device2varListByMac(strMac, StrConst.ALARM_PANEL_ISREADY_ADDR, ((int)PanelReadyStateEnum.Ready).ToString());
                     Device2varListByMac(strMac, StrConst.ALARM_PANEL_ISARMED_ADDR, ((int)PanelArmStateEnum.Armed).ToString());            
                 }
                 else if (strInfo.IndexOf("ARMED") > -1 && strInfo.IndexOf("ZONES IN ALARM") > -1)  //布防状态有报警
                 {
                     Device2varListByMac(strMac, StrConst.ALARM_PANEL_ISREADY_ADDR, ((int)PanelReadyStateEnum.NotReady).ToString());
                     Device2varListByMac(strMac, StrConst.ALARM_PANEL_ISARMED_ADDR, ((int)PanelArmStateEnum.Alarm).ToString());                 
                 }
                 else if (strInfo.IndexOf("ARMED") > -1 && strInfo.IndexOf("You may exit now") > -1)  //布防状态退出
                 {
                     Device2varListByMac(strMac, StrConst.ALARM_PANEL_ISREADY_ADDR, ((int)PanelReadyStateEnum.Ready).ToString());
                     Device2varListByMac(strMac, StrConst.ALARM_PANEL_ISARMED_ADDR, ((int)PanelArmStateEnum.Armed).ToString());            
                 }
               
                 else if (strInfo.IndexOf("DISARMED") > -1 && strInfo.IndexOf("READY TO ARM") > -1)  //撤防状态 准备好
                 {
                     Device2varListByMac(strMac, StrConst.ALARM_PANEL_ISREADY_ADDR, ((int)PanelReadyStateEnum.Ready).ToString());
                     Device2varListByMac(strMac, StrConst.ALARM_PANEL_ISARMED_ADDR, ((int)PanelArmStateEnum.DisArm).ToString());  
                 } 
                 else if (strInfo.IndexOf("DISARMED") > -1 && strInfo.IndexOf("Key * for faults") > -1)  //撤防状态 没准备好
                 {
                     Device2varListByMac(strMac, StrConst.ALARM_PANEL_ISREADY_ADDR, ((int)PanelReadyStateEnum.NotReady).ToString());
                     Device2varListByMac(strMac, StrConst.ALARM_PANEL_ISARMED_ADDR, ((int)PanelArmStateEnum.DisArm).ToString());                 

                 }
                 else if (strInfo.IndexOf("SYSTEM LOBAT") > -1)  //低电压
                 {
                     Device2varListByMac(strMac, StrConst.ALARM_LOW_BATTERY_ADDR, ((int)StrConst.STATUS_ALARM).ToString());                 

                 }
                 else if (strInfo.IndexOf("AC LOSS") > -1)  //无交流
                 {
                     Device2varListByMac(strMac, StrConst.ALARM_AC_LOSS_ADDR, ((int)StrConst.STATUS_ALARM).ToString());                 

                 }
                 else if (strInfo.IndexOf("DISARMED") > -1 && strInfo.IndexOf("ZONES IN ALARM") > -1)  //消警状态
                 {
                     Device2varListByMac(strMac, StrConst.ALARM_PANEL_ISREADY_ADDR, ((int)PanelReadyStateEnum.Ready).ToString());
                     Device2varListByMac(strMac, StrConst.ALARM_PANEL_ISARMED_ADDR, ((int)PanelArmStateEnum.DisArm).ToString());  
                 }
                 else if (strInfo.IndexOf("ALARM") > -1 && strInfo.IndexOf("ZN") > -1) //防区状态
                 {
                    zone = int.Parse(strInfo.Substring(6, 3));
                    Device2varListByMac(strMac, zone, ((int)ZoneStateEnum.Alarm).ToString());
                 }
                 else if (strInfo.IndexOf("BYPAS") > -1 && strInfo.IndexOf("ZN") > -1)  //旁路防区
                 {
                     zone = int.Parse(strInfo.Substring(6, 3));
                     Device2varListByMac(strMac, zone, ((int)ZoneStateEnum.Bypass).ToString());
                 }
                 Device2varListByMac(strMac, StrConst.ALARM_KEYPAD_INFO_ADDR, e.strInfo);

                 //string msg = string.Format("HoneywellIPM上报信息[OnVistaKeypadInfo]！主机MAC：{0} 信息内容：{1}", e.strMac, e.strInfo);
                 //AddOperationLog(StrConst.SERVERITY_MSG, StrConst.TITLE_OPER, "", msg);
             
             }
             catch (Exception ex)
             {
                 Logger.GetInstance().LogError(ex.ToString());
             }

         }
         private void IPM_OnVistaKeypadInfo(object sender, AxIPModuleLib._ICooMonitorEvents_VistaKeypadInfoEvent e)
         {
             this.Invoke(new IPMOnVistaKeypadInfoCallback(IPMOnVistaKeypadInfo),new object[]{sender,e});


         }

         public delegate void IPM_OnDeviceConnectedCallback(object sender, AxIPModuleLib._ICooMonitorEvents_DeviceConnectedEvent e);
         private void IPM_OnDeviceConnected(object sender, AxIPModuleLib._ICooMonitorEvents_DeviceConnectedEvent e)
         {
             try
             {
                 this.BeginInvoke(new IPM_OnDeviceConnectedCallback(IpmDeviceConnected), new object[] { sender,e });
             }
             catch { }

         }
         private void IpmDeviceConnected(object sender, AxIPModuleLib._ICooMonitorEvents_DeviceConnectedEvent e)
         {

             string text = GetHostNameByMac(e.strMac);
             ((AxIPModuleLib.AxCooMonitor)sender).EnableKeypad(e.strMac, 1);
             //PushDeviceConnectState(GetHostAddrByMac(e.strMac), 1);
             ////将控制器图标切换为上线状态
             UpdateTree(text,NodeStateEnum.OnLine);
             int controllerCommStateAddr = StrConst.CONTROL_COMM_STATUS_ADDR;
             Device2varListByMac(e.strMac, controllerCommStateAddr, StrConst.COM_ONLINE.ToString());

             //Log("收到IPM控制器:" + text + "上报的网络连接事件，主机MAC:" + e.strMac);
             string msg = string.Format("Honeywell报警主机通讯连接成功！主机MAC：{0},名称:{1}",e.strMac,text);
             AddOperationLog(StrConst.SERVERITY_MSG, StrConst.TITLE_OPER, "", msg);
             
            
         }

         public delegate void IPM_OnDeviceDisConnectedCallback(object sender, AxIPModuleLib._ICooMonitorEvents_DeviceDisConnectedEvent e);
         private void IPM_OnDeviceDisConnected(object sender, AxIPModuleLib._ICooMonitorEvents_DeviceDisConnectedEvent e)
         {
             try
             {
                 this.BeginInvoke(new IPM_OnDeviceDisConnectedCallback(IpmOnDeviceDisConnected), new object[] { sender, e });
             }
             catch { }            
         }
         private void IpmOnDeviceDisConnected(object sender, AxIPModuleLib._ICooMonitorEvents_DeviceDisConnectedEvent e)
         {
             string text = GetHostNameByMac(e.strMac);
             //PushDeviceConnectState(GetHostAddrByMac(e.strMac), 1);
             ////将控制器图标切换为断线状态
             UpdateTree(text,NodeStateEnum.OffLine);

             int controllerCommStateAddr = StrConst.CONTROL_COMM_STATUS_ADDR; 
             Device2varListByMac(e.strMac, controllerCommStateAddr, StrConst.COM_OFFLINE.ToString());

             string msg = string.Format("Honeywell报警主机通讯断线！主机MAC：{0},名称{1}",e.strMac,text);
             AddOperationLog(StrConst.SERVERITY_MSG, StrConst.TITLE_OPER, "", msg);
         }
        /// <summary>
         /// Vista 警情ContactID 上报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
         private void IPM_OnVistaCIDReport(object sender, AxIPModuleLib._ICooMonitorEvents_VistaCIDReportEvent e)
         {
             try
             {
                 this.BeginInvoke(new IPM_OnVistaCIDReportCallback(IpmOnVistaCIDReport), new object[] { sender, e });
             }
             catch (Exception ex)
             {
                 Logger.GetInstance().LogError(ex.ToString());
             }

         }
         public delegate void IPM_OnVistaCIDReportCallback(object sender, AxIPModuleLib._ICooMonitorEvents_VistaCIDReportEvent e);
         private void IpmOnVistaCIDReport(object sender, AxIPModuleLib._ICooMonitorEvents_VistaCIDReportEvent e)
         {
             try
             {
                 
                 int cid = int.Parse(e.cID);

                 switch (cid)
                 {
                     case 110://火警
                         break;
                     case 120://键盘报警
                         break;
                     case 130://盗警
                         int zone = int.Parse(e.strCode);
                         if (e.isNewEvent == 0)
                             Device2varListByMac(e.strMac, zone, ((int)ZoneStateEnum.Normal).ToString());
                         else
                             Device2varListByMac(e.strMac, zone, ((int)ZoneStateEnum.Alarm).ToString());
                         break;
                     case 301: //AC loss
                         if (e.isNewEvent != 0) e.isNewEvent = 1;
                         Device2varListByMac(e.strMac, StrConst.ALARM_AC_LOSS_ADDR, e.isNewEvent.ToString());
                         break;
                     case 302: //Low batt
                         if (e.isNewEvent != 0) e.isNewEvent = 1;
                          Device2varListByMac(e.strMac, StrConst.ALARM_LOW_BATTERY_ADDR, e.isNewEvent.ToString());
                         break;
                     case 306: //Enter Prog
                         if (e.isNewEvent != 0) e.isNewEvent = 1;
                         Device2varListByMac(e.strMac, StrConst.ALARM_ENTRY_PROGRAM_ADDR, e.isNewEvent.ToString());
                         break;
                     
                     case 380: //防区故障
                          zone = int.Parse(e.strCode);
                         if (e.isNewEvent == 0)
                             Device2varListByMac(e.strMac, zone, ((int)ZoneStateEnum.Normal).ToString());
                         else
                             Device2varListByMac(e.strMac, zone, ((int)ZoneStateEnum.Trouble).ToString());
                         break;

                     case 401://主机状态 ARM/DISARM
                          if (e.isNewEvent == 0) 
                             Device2varListByMac(e.strMac, StrConst.ALARM_PANEL_ISARMED_ADDR, ((int)PanelArmStateEnum.Armed).ToString());
                         else
                             Device2varListByMac(e.strMac, StrConst.ALARM_PANEL_ISARMED_ADDR, ((int)PanelArmStateEnum.DisArm).ToString());
                         break;
                     case 570://防区旁路
                          zone = int.Parse(e.strCode);
                         if (e.isNewEvent == 0)
                             Device2varListByMac(e.strMac, zone, ((int)ZoneStateEnum.Normal).ToString());
                         else
                             Device2varListByMac(e.strMac, zone, ((int)ZoneStateEnum.Bypass).ToString());
                         break;
                 }
                 //string msg = string.Format("HoneywellIPMCID报告[OnVistaCIDReport]！主机MAC：{0} CID码：{1} 是否新事件：{2}", e.strMac, e.strCode, e.isNewEvent);
                 //AddOperationLog(StrConst.SERVERITY_MSG, StrConst.TITLE_OPER, "", msg);
             
             }
             catch (Exception ex)
             {
                 Logger.GetInstance().LogError(ex.ToString());
             }
         }
      
         public delegate void IPM_VistaPanelStatusCallback(object sender, AxIPModuleLib._ICooMonitorEvents_VistaPanelStatusEvent e);
         private void IPM_OnVistaPanelStatus(object sender, AxIPModuleLib._ICooMonitorEvents_VistaPanelStatusEvent e)
         {
             try
             {
                 this.BeginInvoke(new IPM_VistaPanelStatusCallback(IpmOnVistaPanelStatus), new object[] { sender, e });
             }
             catch (Exception ex)
             {
                  Logger.GetInstance().LogError(ex.ToString());
             }

         }
         private void IpmOnVistaPanelStatus(object sender, AxIPModuleLib._ICooMonitorEvents_VistaPanelStatusEvent e)
         {

             //PushPanelInfo(GetHostAddrByMac(e.strMac), e.strInfo, (VistaPanelStateEnum)e.lState, e.zone, e.lPlayback);
             //if (FormMain.LOG_MSG)
             //  Log(  "收到主机信息报告:" +GetHostNameByMac(e.strMac) + "键盘屏幕显示:" + e.strInfo + "光标显示:" + e.cursorPos.ToString() + "主机当前状态:" + e.lState.ToString() + "对应防区:" + e.zone.ToString());

         }
        #endregion
       
         #region XML信息发送



  /*

        /// <summary>
        /// 接收防区状态，实时更新数据
        /// </summary>
        /// <param name="strChanName">通讯通道</param>
        /// <param name="strMac">MAC地址</param>
         /// <param name="strAddr">控制点编号、防区号</param>
        /// <param name="CommStatus">防区状区 0：正常 1：报警 2：旁路 3:故障</param>
 
         private void VistaInfoProcess(string strChanName,string strMac, string strAddr, string value)
         {
             try
             {
                 //更新数据链表
                 foreach (ChannelInfo chan in Rtdb.ChanList)
                 {

                     foreach (ControllerInfo con in chan.ConList)
                     {
                         if (con.MacAddress == strMac)
                         {
                             foreach (Variable var in con.VarList)
                             {
                                 if (var.Addr == pubFun.IsNumeric(strAddr))
                                 {
                                     var.Value = value;
                                     Write2Form(var);
                                     //更新显示
                                     if ((currentChan != null) && (currentChan.ProtocolCode == 202))
                                     {
                                         if (currentCon != null)
                                         {
                                             foreach (ListViewItem item in lsvTag.Items)
                                             {
                                                 if (item.Text == var.Name)
                                                 {
                                                     item.SubItems[3].Text = var.Value.ToString();
                                                     item.SubItems[5].Text = DateTime.Now.ToString();
                                                     if ((pubFun.IsNumeric(item.SubItems[7].Text)) > -1)
                                                     {
                                                         item.SubItems[7].Text = ((pubFun.IsNumeric(item.SubItems[7].Text)) + 1).ToString();
                                                     }
                                                     break;
                                                 }
                                             }
                                         }

                                     }
                                     break;
                                 }
                             }
                             break;
                         }

                     }
                 }
             }
             catch (Exception ex)
             {
                 Logger.GetInstance().LogError(ex.ToString());
             }

         }
        /// <summary>
        /// 撤防复位
        /// </summary>
        /// <param name="strChanName"></param>
        /// <param name="strMac"></param>
         private void DisAlarmProcess(string strMac)
         {
             
             //更新数据链表
             try
             {
                 foreach (ChannelInfo chan in Rtdb.ChanList)
                 {

                     foreach (ControllerInfo con in chan.ConList)
                     {
                         if (con.MacAddress == strMac)
                         {
                             foreach (Variable var in con.VarList)
                             {
                                 var.Value = 0;
                                 if (currentCon != null && currentCon == con)
                                 {
                                     foreach (ListViewItem item in lsvTag.Items)
                                     {
                                         item.SubItems[3].Text = var.Value.ToString();
                                         item.SubItems[5].Text = DateTime.Now.ToString();
                                         if ((pubFun.IsNumeric(item.SubItems[7].Text)) > -1)
                                         {
                                             item.SubItems[7].Text = ((pubFun.IsNumeric(item.SubItems[7].Text)) + 1).ToString();
                                         }
                                     }
                                 }

                             }
                         }
                         break;
                     }

                 }
             }
             catch (Exception ex)
             {
                 Logger.GetInstance().LogError(ex.ToString());
             }

         }
      
        /// <summary>
        /// 向客户端发送防区状态
        /// </summary>
        /// <param name="HostAddr"></param>
        /// <param name="ZoneCode"></param>
        /// <param name="IsNewEvent"></param>
         private void PushPanelAlarm(int HostAddr, int ZoneCode, ZoneStateEnum ZoneState)
         {
             try
             {
                 //生成xml字符串
                 using (StringWriter sw = new StringWriter())
                 {
                     XmlTextWriter xtw = new XmlTextWriter(sw);
                     xtw.WriteStartDocument();
                     xtw.WriteStartElement("GHISMS");
                     xtw.WriteStartElement("command");

                     xtw.WriteStartElement("code");
                     xtw.WriteString("1");
                     xtw.WriteEndElement();

                     xtw.WriteStartElement("function");
                     xtw.WriteString("PushPanelAlarm");
                     xtw.WriteEndElement();

                     xtw.WriteStartElement("hostaddr");
                     xtw.WriteString(HostAddr.ToString());
                     xtw.WriteEndElement();

                     xtw.WriteStartElement("zone");
                     xtw.WriteString(((int)ZoneCode).ToString());
                     xtw.WriteEndElement();

                     xtw.WriteStartElement("zonestatus");
                     xtw.WriteString(((int)ZoneState).ToString());
                     xtw.WriteEndElement();

                     xtw.WriteEndElement();  //end command
                     xtw.WriteEndElement();   //end ghisms
                     xtw.WriteEndDocument();
                     DataClient[] tempList = dataClientList.ToArray();
                     foreach (DataClient dc in tempList)
                         dc.Send(sw.ToString());
                     //result = sw.ToString();

                 }
             }
             catch (Exception e)
             {
                 if (FormMain.LOG_ERR) logger.AddErr(e, "");
             }
         }
         private void PushPanelInfo(int HostAddr, string strInfo, VistaPanelStateEnum PanelState, int ZoneCode, int IsPlayBack)
         {
             try
             {
                 //生成xml字符串
                 using (StringWriter sw = new StringWriter())
                 {
                     XmlTextWriter xtw = new XmlTextWriter(sw);
                     xtw.WriteStartDocument();
                     xtw.WriteStartElement("GHISMS");
                     xtw.WriteStartElement("command");

                     xtw.WriteStartElement("code");
                     xtw.WriteString("2");
                     xtw.WriteEndElement();

                     xtw.WriteStartElement("function");
                     xtw.WriteString("PushPanelInfo");
                     xtw.WriteEndElement();

                     xtw.WriteStartElement("hostaddr");
                     xtw.WriteString(HostAddr.ToString());
                     xtw.WriteEndElement();

                     xtw.WriteStartElement("dispinfo");
                     xtw.WriteString(strInfo);
                     xtw.WriteEndElement();

                     xtw.WriteStartElement("panelstatus");
                     xtw.WriteString(((int)PanelState).ToString());
                     xtw.WriteEndElement();

                     xtw.WriteStartElement("zone");
                     xtw.WriteString(ZoneCode.ToString());
                     xtw.WriteEndElement();

                     xtw.WriteStartElement("isplayback");
                     xtw.WriteString(IsPlayBack.ToString());
                     xtw.WriteEndElement();

                     xtw.WriteEndElement();  //end command
                     xtw.WriteEndElement();   //end ghisms
                     xtw.WriteEndDocument();
                     DataClient[] tempList = dataClientList.ToArray();
                     foreach (DataClient dc in tempList)
                         dc.Send(sw.ToString());
                 }
             }
             catch (Exception e)
             {
                 if (FormMain.LOG_ERR) logger.AddErr(e, "");
             }


         }
         private void PushDeviceConnectState(int HostAddr, int State)
         {
             try
             {
                 //生成xml字符串
                 using (StringWriter sw = new StringWriter())
                 {
                     XmlTextWriter xtw = new XmlTextWriter(sw);
                     xtw.WriteStartDocument();
                     xtw.WriteStartElement("GHISMS");
                     xtw.WriteStartElement("command");

                     xtw.WriteStartElement("code");
                     xtw.WriteString("7");
                     xtw.WriteEndElement();

                     xtw.WriteStartElement("function");
                     xtw.WriteString("PushDeviceConnectState");
                     xtw.WriteEndElement();

                     xtw.WriteStartElement("hostaddr");
                     xtw.WriteString(HostAddr.ToString());
                     xtw.WriteEndElement();

                     xtw.WriteStartElement("state");
                     xtw.WriteString(State.ToString());
                     xtw.WriteEndElement();

                   

                     xtw.WriteEndElement();  //end command
                     xtw.WriteEndElement();   //end ghisms
                     xtw.WriteEndDocument();
                     DataClient[] tempList = dataClientList.ToArray();
                     foreach (DataClient dc in tempList)
                         dc.Send(sw.ToString());
                 }
             }
             catch (Exception e)
             {
                 if (FormMain.LOG_ERR) logger.AddErr(e, "");
             }


         }*/
#endregion
      

    }
}
