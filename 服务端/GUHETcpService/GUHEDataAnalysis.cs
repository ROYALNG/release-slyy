using System;
using System.Collections.Generic;
using System.Text;
using GHIBMS.Common;
using System.Net.Sockets;
using System.Xml;
using System.IO;
using System.Net;
using GHIBMS.Interface;
using GHIBMS.LED;
using System.Diagnostics;
using System.Windows.Forms;
using GHIBMS.Command;


namespace GHIBMS.Server
{
    public class GUHEDataAnalysis
    {
        public event CommMsgDelegate OnCommMsg;
        #region 数据订阅

        private GUHETcpService netService;
        public GUHEDataAnalysis(GUHETcpService netSvr)
        {
            this.netService = netSvr;
        }
       
        private void SubscribeFormData(DataClient dc, string PointName)
        {
            string[] pointArray = PointName.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            dc.AddSubscribePoint(pointArray);
            //同步发送一次窗体所有点的数据
            S_OnSubscribeVariable(dc, pointArray);

        }

        private void UnSubscribeFormData(DataClient dc, string PointName)
        {
            string[] pointArray = PointName.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            dc.UnSubscribePoint(pointArray);
        }
        private void C_RefreshVariable(DataClient dc, string PointName)
        {
            string[] pointArray = PointName.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            S_OnRefreshVariable(dc, pointArray);

        }
        private void C_GetFunDetail(DataClient dc, string conId)
        {
            try
            {
                //生成xml字符串
                using (StringWriter sw = new StringWriter())
                {
                    XmlTextWriter xtw = new XmlTextWriter(sw);
                    xtw.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"UTF-8\"");
                    xtw.WriteStartElement("GHISMS");
                    xtw.WriteStartElement("command");

                    xtw.WriteStartElement("code");
                    xtw.WriteString("133");
                    xtw.WriteEndElement();

                    xtw.WriteStartElement("function");
                    xtw.WriteString("GetFunDetail");
                    xtw.WriteEndElement();

                    IController con = Rtdb.GetControllerByAddr(conId);
                    if (con != null)
                    {
                        xtw.WriteStartElement("no");
                        xtw.WriteString((con.Description=="")?con.Name:con.Description);
                        xtw.WriteEndElement();
                        foreach(IVariable var in con.VarList)
                        {
                            if (var.Address==con.Address+".1")
                            {
                                xtw.WriteStartElement("comm");
                                xtw.WriteString((var.Value.ToString()=="0")?"在线":"离线");
                                xtw.WriteEndElement();
                            }
                            else if (var.Address==con.Address+".2")
                            {
                                xtw.WriteStartElement("speed");
                                xtw.WriteString(var.Value.ToString());
                                xtw.WriteEndElement();
                            }
                            else if (var.Address==con.Address+".3")
                            {
                                xtw.WriteStartElement("state");
                                xtw.WriteString(var.Value.ToString());
                                xtw.WriteEndElement();
                            }
                            else if (var.Address==con.Address+".4")
                            {
                                xtw.WriteStartElement("power");
                                xtw.WriteString(var.Value.ToString());
                                xtw.WriteEndElement();
                            }
                            else if (var.Address==con.Address+".5")
                            {
                                xtw.WriteStartElement("type");
                                xtw.WriteString(var.Description);
                                xtw.WriteEndElement();
                            }
                        }
                    }

                    xtw.WriteEndElement();  //end command
                    xtw.WriteEndElement();   //end ghisms
                    //xtw.WriteEndDocument();;

                    string st = sw.ToString();
                    try
                    {
                           dc.Send(Encoding.UTF8.GetBytes(st.ToCharArray()));
                    }
                    catch (SocketException ex)
                    {
                        dc.CloseConnection();
                        Logger.GetInstance().LogError(ex.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                //if (FormMain.LOG_ERR) logger.AddErr(e, "");
                Logger.GetInstance().LogError(e.ToString());
                Console.WriteLine(e.ToString());
            }

        }

        private void C_GetAlarmPreCase(DataClient dc,string VariableName)
        {
            try
            {
                dc.Send(Encoding.UTF8.GetBytes(Sqldb.GetAlarmPreCase(VariableName)));
            }
            catch (SocketException ex)
            {
                dc.CloseConnection();
                Logger.GetInstance().LogError(ex.ToString());
            }
        }
        #endregion

        #region 客户端--->服务器端
        /// <summary>
        /// 报警消警
        /// </summary>
        /// <param name="dc">客户端对象</param>
        /// <param name="AlarmGuid">报警ID</param>
        /// <param name="AlarmResult">处理结果</param>
        public void C_AlarmClear(DataClient dc, string AlarmGuid, string AlarmResult)
        {
            try
            {
                
               // Almdb.RemoveAlm();
                string msg = string.Format("用户消警成功!用户名：{0}", dc.LoginUser.UserName);
                SendOperationLog(Severity.信息, StrConst.TITLE_OPER, dc.LoginUser.UserName, msg);
                DBAssistant.GetInstance().UpdateHisRecord(AlarmGuid, AlarmResult, dc.LoginUser.UserName);
                //转发消警指令到所有客户端
                S_OnAlarmClear(AlarmGuid, dc.LoginUser.UserName, AlarmResult);
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }

        }
        /// <summary>
        /// 消警 WEB客户端
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="AlarmGuid"></param>
        /// <param name="AlarmResult"></param>
        public void C_AlarmClear(string UserName, string AlarmGuid, string AlarmResult)
        {
            try
            {
                //Almdb.RemoveAllAlm();

                string msg = string.Format("用户消警成功!用户名：{0}", UserName);
                SendOperationLog(Severity.信息, StrConst.TITLE_OPER, UserName, msg);
                DBAssistant.GetInstance().UpdateHisRecord(AlarmGuid, AlarmResult, UserName);
               
                //转发消警指令到所有客户端
                S_OnAlarmClear(AlarmGuid, UserName, AlarmResult);
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }

        }

        /// <summary>
        /// 消警 CS
        /// </summary>
        /// <param name="AlarmGuid">报警ID</param>
        private void C_AlarmReset(DataClient dc)
        {
            Almdb.ResetVarAlarm(dc.LoginUser);
            Almdb.ResetConAlarm(dc.LoginUser);

        }
        private void C_LedShowString(DataClient dc, XmlDocument xmldoc)
        {
            try
            {
                List<LedOutText> listText = new List<LedOutText>();
                XmlNodeList txtNodes = xmldoc.SelectNodes("/GHISMS/command/outtextinfo");
                foreach (XmlNode infNod in txtNodes)
                {
                    if (infNod.NodeType != XmlNodeType.Element)
                    {
                        continue;
                    }
                    LedOutText textInfo = new LedOutText();
                    textInfo.Left = (ushort)pubFun.IsInt(infNod.Attributes["Left"].Value, 0);
                    textInfo.Top = (ushort)pubFun.IsInt(infNod.Attributes["Top"].Value, 0);
                    textInfo.Width = (ushort)pubFun.IsInt(infNod.Attributes["Width"].Value, 0);
                    textInfo.Height = (ushort)pubFun.IsInt(infNod.Attributes["Height"].Value, 0);
                    textInfo.ASCFont = (ushort)pubFun.IsInt(infNod.Attributes["ASCFont"].Value, 0);
                    textInfo.HZFont = (ushort)pubFun.IsInt(infNod.Attributes["HZFont"].Value, 0);
                    textInfo.Color = infNod.Attributes["Color"].Value;
                    textInfo.Content = infNod.Attributes["Content"].Value;
                    textInfo.EnterMode = (ushort)pubFun.IsInt(infNod.Attributes["EnterMode"].Value, 0);
                    textInfo.Speed = (ushort)pubFun.IsInt(infNod.Attributes["Speed"].Value, 0);
                    textInfo.StayTime = pubFun.IsInt(infNod.Attributes["StayTime"].Value, 0);
                    listText.Add(textInfo);

                }
              

                XmlNodeList xmlNodes = xmldoc.SelectNodes("/GHISMS/command/ledlist");
                foreach (XmlNode xml in xmlNodes)
                {
                    if (xml.NodeType != XmlNodeType.Element)
                    {
                        continue;
                    }
                    string name = xml.InnerText;

                    IController con = Rtdb.GetControllerByName(name);
                    if (con != null)
                    {
                        ILEDController ledcon = con as ILEDController;
                        LedCommand args = new LedCommand();
                        args.dataClient = dc;
                        args.Channel = ledcon.ChannelObject;
                        args.Controller = con;
                        args.Command = LedCommandEnum.ShowString;
                        args.DevID =pubFun.IsInt(ledcon.Address,0);
                        args.IpAddress = ledcon.IpAddress;
                        args.OutTextList = listText;
                        object[] cmd = new object[] { args, dc };
                        con.ExecCommand(CommandCode.C_LED_SHOW_STRING, cmd);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }
        }
        private void C_LedReplay(DataClient dc, XmlDocument xmldoc)
        {
            try
            {
                int drv=0, index=0;
                XmlNode drvNod = xmldoc.SelectSingleNode("/GHISMS/command/drv");
                if (drvNod != null)
                {
                    drv = pubFun.IsInt(drvNod.InnerText, 0);
                }
                XmlNode indexNod = xmldoc.SelectSingleNode("/GHISMS/command/index");
                if (indexNod != null)
                {
                    index = pubFun.IsInt(indexNod.InnerText, 0);
                }
                XmlNodeList xmlNodes = xmldoc.SelectNodes("/GHISMS/command/ledlist");
                foreach (XmlNode xml in xmlNodes)
                {
                    if (xml.NodeType != XmlNodeType.Element)
                    {
                        continue;
                    }
                    string name = xml.InnerText;

                    IController con = Rtdb.GetControllerByName(name);
                    if (con != null)
                    {
                        ILEDController ledcon = con as ILEDController;
                        LedCommand args = new LedCommand();
                        args.dataClient = dc;
                        args.Channel = ledcon.ChannelObject;
                        args.Controller = con;
                        args.Command = LedCommandEnum.Replay;
                        args.DevID = pubFun.IsInt(ledcon.Address,0);
                        args.IpAddress = ledcon.IpAddress;
                        args.Drv = drv;
                        args.Index = index;
                        object[] cmd = new object[] { args, dc };
                        con.ExecCommand(CommandCode.C_LED_REPLAY, cmd);
                    }
                }





             }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }

        }
        private void C_LedReset(DataClient dc, XmlDocument xmldoc)
        {
            try
            {
               
                XmlNodeList xmlNodes = xmldoc.SelectNodes("/GHISMS/command/ledlist");
                foreach (XmlNode xml in xmlNodes)
                {
                    if (xml.NodeType != XmlNodeType.Element)
                    {
                        continue;
                    }
                    string name = xml.InnerText;

                    IController con = Rtdb.GetControllerByName(name);
                    if (con != null)
                    {
                        ILEDController ledcon = con as ILEDController;
                        LedCommand args = new LedCommand();
                        args.dataClient = dc;
                        args.Channel = ledcon.ChannelObject;
                        args.Controller = con;
                        args.Command = LedCommandEnum.Reset;
                        args.DevID = pubFun.IsInt(ledcon.Address,0);
                        args.IpAddress = ledcon.IpAddress;
                    
                        object[] cmd = new object[] { args, dc };
                        con.ExecCommand(CommandCode.C_LED_RESET, cmd);
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }

        }

   
        /// <summary>
        /// 处理客户端的登录返馈
        /// </summary>
        /// <param name="dc"></param>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        private void C_Login(DataClient dc, string UserName, string Password,bool bRelogin)
        {
            try
            {
                if (dc != null)
                {
                    //dc.ClientID = ClientID;
                    User user = null;
                    string msg3 = ("客户端操作指令,指令类型：用户登录请求[LoginRequest]，用户名" + UserName + "  IP:" + dc.ClientIp+"  端口号："+dc.ClientPort.ToString());
                    SendOperationLog(Severity.信息, StrConst.TITLE_OPER, UserName, msg3);
            
                    user = Sqldb.GetAuthorizationUser(UserName, Password);
                    if (user == null)
                    {
                        user = new User();
                        user.HasLogin = false;
                    }
                    else
                    {
                        user.HasLogin = true;
                        user.IsDisConnLogin = bRelogin;
                    }
                    string s = UserLogin2XML(user);
                    try
                    {
                           dc.Send(Encoding.UTF8.GetBytes(s.ToCharArray()));
                    }
                    catch (SocketException ex)
                    {
                        dc.CloseConnection();
                        Logger.GetInstance().LogError(ex.ToString());
                        return;
                    }
                    if (user != null)  //通过授权检查
                    {

                        dc.LoginUser = user;
                      
                        if (bRelogin)
                        {
                            dc.HasLogin = true;
                            SendOperationLog(Severity.信息, StrConst.TITLE_OPER, UserName, "通讯断线重连成功！");
                            return;
                        }
                      
                        //如果用户为NULL UserLogin2XML将不包括用户登录信息 
                      
                        //用户登录后发送区域列表
                          S_SendRegionTree(dc);
                        //用户登录后发送报警列表
                          S_SendAlarmList(dc);
                        //用户登录后发送设备列表
                          S_SendDeviceList(dc, user);
                        //用户登录后发送所有点的列表
                        //2012-11-18 取消向客户端发送所有点列表，防止点过多，发送时间长
                        //2013-4-10 恢复 仅发送IDataChannel通道内的点，且只发送点的名称
                          S_SendPointList(dc);

                          DBAssistant.GetInstance().AddOperation("Information", "操作日志", dc.ClientIp, "用户：" + UserName + " 登录系统成功！");
                          msg3 = ("客户端用户登录成功，用户名：" + UserName + "  IP:" + dc.ClientIp);
                          dc.HasLogin = true;
                    }
                    //登录日志
                    else
                    {
                        dc.HasLogin=false;
                        DBAssistant.GetInstance().AddOperation("Information", "操作日志", dc.ClientIp, "用户：" + UserName + " 登录系统失败！");
                        msg3 = ("客户端用户登录失败，用户名：" + UserName + "  IP:" + dc.ClientIp + "  端口号：" + dc.ClientPort.ToString());
                    }

                    S_SentUserDataReady(dc);
                    SendOperationLog(Severity.信息, StrConst.TITLE_OPER, UserName, msg3);
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }

        }
        private void C_Logout(DataClient dc, string UserName)
        {
            if (dc != null)
            {
                dc.LoginUser = null;
                dc.HasLogin = false;
                string s = UserLogout2XML(UserName);
                try
                {
                       dc.Send(Encoding.UTF8.GetBytes(s.ToCharArray()));
                }
                catch (SocketException ex)
                {
                    dc.CloseConnection();
                    Logger.GetInstance().LogError(ex.ToString());
                }


                Logger.GetInstance().LogMsg("客户端请求注销用户成功！用户名：" + UserName);
                //写数据库
               
                DBAssistant.GetInstance().AddOperation("Information", "操作日志", dc.ClientIp, "用户登出系统：" + UserName);
            }
        }

        private void C_GetEventHistory(DataClient dc, string time, string type)
        {

            if (dc != null)
            {

                string s = Sqldb.GetEventHistory(time, type);
                try
                {
                        dc.Send(Encoding.UTF8.GetBytes(s.ToCharArray()));
                }
                catch (Exception ex)
                {
                    Logger.GetInstance().LogError(ex.ToString());
                }
            }


        }
        private void C_GetAlarmHistory(DataClient dc, string time, string name)
        {
            if (dc != null)
            {

                string s = Sqldb.GetAlarmHistory(time, name);
                try
                {
                       dc.Send(Encoding.UTF8.GetBytes(s.ToCharArray()));
                }
                catch (Exception ex)
                {
                    Logger.GetInstance().LogError(ex.ToString());
                }
            }
        }
        private void C_GetDataHistory(DataClient dc, string time, string name, string type)
        {
            if (dc != null)
            {

                string s = Sqldb.GetDataHistory(time, name, type);
                try
                {
                        dc.Send(Encoding.UTF8.GetBytes(s.ToCharArray()));
                }
                catch (Exception ex)
                {
                    Logger.GetInstance().LogError(ex.ToString());
                }
            }
        }
        private void C_GetDataHistoryCurve(DataClient dc, string time, string name)
        {
            if (dc != null)
            {

                string s = Sqldb.GetDataHistoryCurve(time, name);
                try
                {
                       dc.Send(Encoding.UTF8.GetBytes(s.ToCharArray()));
                }
                catch (Exception ex)
                {
                    Logger.GetInstance().LogError(ex.ToString());
                }
            }
        }
        /// <summary>
        /// 通道的启用和停用外部控制
        /// </summary>
        /// <param name="name"></param>
        /// <param name="iEnable"></param>
        private void C_SetChannelState(string name, int iEnable)
        {
            foreach (IChannel chan in Rtdb.ChanList)
            {
                if (chan.Name == name)
                {
                    if (iEnable == 1)
                    {
                        if (!chan.Enable)
                        {
                            chan.Enable = true;
                            chan.Start();
                            string msg = string.Format("客户端启用通讯通道！通道名：{0}",chan.Name);
                            chan.SendCommEvent(Severity.事件, CommunicationEvent.COMM_ENABLE, chan.Name, msg);
                        }

                    }
                    else
                    {
                        if (chan.Enable)
                        {
                            chan.Enable = false;
                            if (chan.Active)
                                chan.Stop();
                            string msg = string.Format("客户端禁用通讯通道！通道名：{0}", chan.Name);
                            chan.SendCommEvent(Severity.事件, CommunicationEvent.COMM_ENABLE, chan.Name, msg);

                        }

                    }
                }
                  
            }
        }
        /// <summary>
        /// 控制器的启用和停用外部控制
        /// </summary>
        /// <param name="name"></param>
        /// <param name="iEnable"></param>
        private void C_SetControllerState(string name, int iEnable)
        {

            foreach (IChannel chan in Rtdb.ChanList)
            {
                foreach (IController con in chan.ConList)
                {
                    if (con.Name == name)
                    {
                        if (iEnable == 1)
                        {
                            if (!con.Enable)
                            {
                                con.Enable = true;
                                con.Active = true;
                                string msg = string.Format("客户端启用控制器！控制器名：{0}", con.Name);
                                chan.SendCommEvent( Severity.事件, CommunicationEvent.COMM_ENABLE, con.Name, msg);
                            }

                        }
                        else
                        {
                            if (con.Enable)
                            {
                                con.Enable = false;
                                con.Active = false;
                                string msg = string.Format("客户端禁用控制器！控制器名：{0}", con.Name);
                                chan.SendCommEvent( Severity.事件, CommunicationEvent.COMM_ENABLE, con.Name, msg);

                            }

                        }
                    }
                }

            }

        }
        /// <summary>
        /// 变量的启用和禁用外部控制
        /// </summary>
        /// <param name="name"></param>
        /// <param name="iEnable"></param>
        private void C_SetVariableState(string name, int iEnable)
        {
            IVariable var = Rtdb.GetVariableByNameRun(name);
            if (var != null)
            {
                if (iEnable == 1)
                {
                    var.Enable = true;
                    var.Active = true;
                }
                else
                {
                    var.Enable = false;
                    var.Active = false;
                }
                
            }

        }
        private void C_SetDeviceState(string name, int iEnable,int type)
        {
            if (type == 1) //通讯通道启用和禁用
            {
                C_SetChannelState(name, iEnable);
            }
            else if (type == 2) //通讯控制器启用和禁用
            {
                C_SetControllerState(name,iEnable);
            }
            else if (type == 3)
            {
                C_SetVariableState(null, iEnable);
            }
           
        }
        private void C_ChangePassword(DataClient dc, string userid, string password)
        {
            if (Sqldb.ChangePassword(userid, password))
            {
                S_OnChangePassword(dc, true, password);
                DBAssistant.GetInstance().AddOperation("Information", "操作日志", userid, "用户修改密码成功！用户ID:" + userid);
                SendOperationLog(Severity.事件, "密码修改", "", "用户修改密码成功！用户ID:" + userid);
            }
            else
            {
                S_OnChangePassword(dc, false, password);
                DBAssistant.GetInstance().AddOperation("Information", "操作日志", userid, "用户修改密码失败！用户ID:" + userid);
                SendOperationLog(Severity.事件, "密码修改", "", "用户修改密码失败！用户ID:" + userid);
            }
        }
        private void C_GisSetPos(DataClient dc, string name, string ln, string la, string bshow, string iconname)
        {
            try
            {
                IVariable var = Rtdb.GetVariableByNameRun(name);
                if ((var != null) && (var is IGisCoordinate))
                {
                    IGisCoordinate gis = var as IGisCoordinate;

                    gis.Latitudes = Convert.ToDouble(la);
                    gis.Longitude = Convert.ToDouble(ln);
                    gis.Marker = Convert.ToBoolean(bshow);
                    gis.IconName = iconname;
                    if (Sqldb.DBUpdateGisPos(name, Convert.ToDouble(ln), Convert.ToDouble(la), Convert.ToBoolean(bshow), iconname))
                    {
                        S_OnUpdateGisPos(dc, true);
                        DBAssistant.GetInstance().AddOperation("Information", "操作日志", dc.LoginUser.UserName, "用户更新GIS定位成功！点名称:" + name);
                        SendOperationLog(Severity.事件, "GIS定位", "", "用户更新GIS定位成功！点名称:" + name);
                    }
                    else
                    {
                        S_OnUpdateGisPos(dc, false);
                        DBAssistant.GetInstance().AddOperation("Information", "操作日志", dc.LoginUser.UserName, "用户更新GIS定位失败！点名称:" + name);
                        SendOperationLog(Severity.事件, "GIS定位", "", "用户更新GIS定位失败！点名称:" + name);
                    }
                }
                //ProjectMng.SaveToXml();
               

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }



        }
        #region 矩阵控制客户端操作响应

        public void C_VideoControl(VideoCommandArgs e)
        {
            IController con = Rtdb.GetMatrixByMonNameRun(e.MonName);
            if (con != null)
            {
                object[] cmd = new object[] { e };
                e.oCon = con;
                con.ExecCommand(CommandCode.C_VIDEO_CONTROL, cmd);
              
            }
            
            
        }
/*
        /// <summary>
        /// 预置位
        /// </summary>
        /// <param name="MatrixAddr"></param>
        /// <param name="MonID"></param>
        /// <param name="CamID"></param>
        //public void PTZPreset(int MatrixAddr, int CamID, int PresetID, int PresetCMD)
        //{
        //    try
        //    {
        //        foreach (ChannelInfo chan in Rtdb.Rtdb.ChanList)
        //        {
        //            if (chan.Sort == DeviceLabelEnum.视频矩阵.ToString())
        //            {
        //                foreach (IController con in chan.ConList)
        //                {
        //                    if (con.Address == MatrixAddr)
        //                    {
        //                        MatrixCtrlCommand cmd = new MatrixCtrlCommand();
        //                        cmd.Channel = chan;
        //                        cmd.Conntroller = con;
        //                        if (PresetCMD == (int)PtzPresetEnum.调用)
        //                        {
        //                            cmd.Command = MatrixCommandEnum.GOTO_PRESET;
        //                            cmd.Param1 = CamID;
        //                            cmd.Param2 = PresetID;
        //                        }
        //                        else if (PresetCMD == (int)PtzPresetEnum.设置)
        //                        {
        //                            cmd.Command = MatrixCommandEnum.SET_PRESET;
        //                            cmd.Param1 = CamID;
        //                            cmd.Param2 = PresetID;
        //                        }
        //                        matrixController.SendCmd(cmd);

        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.GetInstance().LogError(ex.ToString());
        //    }
        //}
        //public void PTZControl(int MatrixAddr, int CamID, MatrixCommandEnum PtzCmd, int Stop, int Speed)
        //{
        //    try
        //    {
        //        foreach (ChannelInfo chan in Rtdb.Rtdb.ChanList)
        //        {
        //            if (chan.Sort == DeviceLabelEnum.视频矩阵.ToString())
        //            {
        //                foreach (IController con in chan.ConList)
        //                {
        //                    if (con.Address == MatrixAddr)
        //                    {
        //                        MatrixCtrlCommand cmd = new MatrixCtrlCommand();
        //                        cmd.Channel = chan;
        //                        cmd.Conntroller = con;
        //                        cmd.Command = PtzCmd;
        //                        cmd.Param1 = CamID;
        //                        cmd.Param2 = Stop;
        //                        cmd.Param3 = Speed;
        //                        matrixController.SendCmd(cmd);

        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.GetInstance().LogError(ex.ToString());
        //    }
        //}
        */

        #endregion

        #endregion

        #region 服务器端--->客户端
        private void S_SentUserDataReady(DataClient dc)
        {
            try
            {
                //生成xml字符串
                using (StringWriter sw = new StringWriter())
                {
                    XmlTextWriter xtw = new XmlTextWriter(sw);

                    xtw.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"UTF-8\"");

                    xtw.WriteStartElement("GHISMS");
                    xtw.WriteStartElement("command");

                    xtw.WriteStartElement("code");
                    xtw.WriteString("132");
                    xtw.WriteEndElement();

                    xtw.WriteStartElement("function");
                    xtw.WriteString("SentUserDataReady");
                    xtw.WriteEndElement();

                    xtw.WriteEndElement();  //end command
                    xtw.WriteEndElement();   //end ghisms
                    ////xtw.WriteEndDocument();;
                    string ret = sw.ToString();
                    dc.Send(Encoding.UTF8.GetBytes(ret));
                }
            }
            catch (SocketException ex)
            {
                dc.CloseConnection();
                Logger.GetInstance().LogError(ex.ToString());
            }

        }
        //通讯驱动状态发送
        private void S_SentChannelState(DataClient dc)
        {
            try
            {
                //生成xml字符串
                using (StringWriter sw = new StringWriter())
                {
                    XmlTextWriter xtw = new XmlTextWriter(sw);

                    xtw.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"UTF-8\"");

                    xtw.WriteStartElement("GHISMS");
                    xtw.WriteStartElement("command");

                    xtw.WriteStartElement("code");
                    xtw.WriteString("118");
                    xtw.WriteEndElement();

                    xtw.WriteStartElement("function");
                    xtw.WriteString("GetChannelState");
                    xtw.WriteEndElement();

                    foreach (IChannel chan in Rtdb.ChanList)
                    {
                        xtw.WriteStartElement("channel");
                        xtw.WriteAttributeString("Name", chan.Name);
                        xtw.WriteAttributeString("Description", chan.Description);
                        xtw.WriteAttributeString("Active", chan.Active ? "1" : "0");
                        xtw.WriteAttributeString("Enable", chan.Enable ? "1" : "0");
                        xtw.WriteEndElement();
                    }
                    xtw.WriteEndElement();  //end command
                    xtw.WriteEndElement();   //end ghisms
                    ////xtw.WriteEndDocument();;
                    string ret = sw.ToString();
                    dc.Send(Encoding.UTF8.GetBytes(ret));
                }
            }
            catch (SocketException ex)
            {
                dc.CloseConnection();
                Logger.GetInstance().LogError(ex.ToString());
            }

        }
        private void S_SentAlarmHostState(DataClient dc, string hostlist)
        {
            string[] lines = hostlist.Split(',');

            try
            {
                //生成xml字符串
                using (StringWriter sw = new StringWriter())
                {
                    XmlTextWriter xtw = new XmlTextWriter(sw);

                    xtw.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"UTF-8\"");

                    xtw.WriteStartElement("GHISMS");
                    xtw.WriteStartElement("command");

                    xtw.WriteStartElement("code");
                    xtw.WriteString("117");
                    xtw.WriteEndElement();

                    xtw.WriteStartElement("function");
                    xtw.WriteString("SentAlarmHostState");
                    xtw.WriteEndElement();
                    foreach (string line in lines)
                    {
                        if (line == "") continue;
                        foreach (IChannel chan in Rtdb.ChanList)
                        {
                            foreach (IController con in chan.ConList)
                            {
                                if (con.Name == line)
                                {
                                    xtw.WriteStartElement("host");
                                    xtw.WriteAttributeString("hostname", con.Name.ToString());
                                    string ready = "0";
                                    string arm = "0";
                                    string info = "";
                                    string battery = "0";
                                    string ac = "0";
                                    string prom = "0";
                                    string com = "1";
                                    foreach (IVariable var in con.VarList)
                                    {
                                        if (var.Address == StrConst.ALARM_PANEL_ISREADY_ADDR.ToString())
                                        {
                                            ready = var.Value.ToString();
                                        }
                                        else if (var.Address == StrConst.ALARM_PANEL_ISARMED_ADDR.ToString())
                                        {
                                            arm = var.Value.ToString();
                                        }
                                        else if (var.Address == StrConst.ALARM_KEYPAD_INFO_ADDR.ToString())
                                        {
                                            info = var.Value.ToString();
                                        }
                                        else if (var.Address == StrConst.ALARM_LOW_BATTERY_ADDR.ToString())
                                        {
                                            battery = var.Value.ToString();
                                        }
                                        else if (var.Address == StrConst.ALARM_AC_LOSS_ADDR.ToString())
                                        {
                                            ac = var.Value.ToString();
                                        }
                                        else if (var.Address == StrConst.ALARM_ENTRY_PROGRAM_ADDR.ToString())
                                        {
                                            prom = var.Value.ToString();
                                        }
                                        else if (var.Address == StrConst.ALARM_COMM_STATUE_ADDR.ToString())
                                        {
                                            com = var.Value.ToString();
                                        }
                                    }
                                    xtw.WriteAttributeString("PanelReady", ready);
                                    xtw.WriteAttributeString("PanelArm", arm);
                                    xtw.WriteAttributeString("KeypadInfo", info);
                                    xtw.WriteAttributeString("LowBattery", battery);
                                    xtw.WriteAttributeString("ACLoss", ac);
                                    xtw.WriteAttributeString("EntryProgram", prom);
                                    xtw.WriteAttributeString("CommStatus", com);

                                    xtw.WriteEndElement();
                                }
                            }
                        }
                    }
                    xtw.WriteEndElement();  //end command
                    xtw.WriteEndElement();   //end ghisms
                    ////xtw.WriteEndDocument();;
                    string ret = sw.ToString();
                    dc.Send(Encoding.UTF8.GetBytes(ret));

                }
            }
            catch (SocketException ex)
            {
                dc.CloseConnection();
                Console.WriteLine(ex.ToString());
            }

        }

      
        /// <summary>
        /// 消警
        /// </summary>
        /// <param name="AlarmGuid">报警ID</param>
        private void S_OnAlarmClear(string AlarmGuid, string UserName, string Result)
        {
            try
            {
                //生成xml字符串
                using (StringWriter sw = new StringWriter())
                {
                    XmlTextWriter xtw = new XmlTextWriter(sw);
                    xtw.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"UTF-8\"");
                    xtw.WriteStartElement("GHISMS");
                    xtw.WriteStartElement("command");

                    xtw.WriteStartElement("code");
                    xtw.WriteString("101");
                    xtw.WriteEndElement();

                    xtw.WriteStartElement("function");
                    xtw.WriteString("ClearAlarm");
                    xtw.WriteEndElement();

                    xtw.WriteStartElement("Guid");
                    xtw.WriteString(AlarmGuid);
                    xtw.WriteEndElement();

                    xtw.WriteStartElement("user");
                    xtw.WriteString(UserName);
                    xtw.WriteEndElement();

                    xtw.WriteStartElement("result");
                    xtw.WriteString(UserName);
                    xtw.WriteEndElement();

                    xtw.WriteEndElement();  //end command
                    xtw.WriteEndElement();   //end ghisms
                    //xtw.WriteEndDocument();;
                    
                    netService.Send2AllClient(sw.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
        /// <summary>
        /// 发送报警列表到指定的客户端
        /// </summary>
        /// <param name="dc"></param>
        private void S_SendAlarmList(DataClient dc)
        {
            try
            {
                string s = Almdb.AlarmList2XML();
                dc.Send(Encoding.UTF8.GetBytes(s.ToCharArray()));
           }
            catch ( SocketException ex)
            {
                dc.CloseConnection();
                Logger.GetInstance().LogError(ex.ToString());
            }
        }
        /// <summary>
        /// 发送变量列表到客户端
        /// 用户登录成功后自动发送
        /// </summary>
        /// <param name="dc"></param>
        private void S_SendPointList(DataClient dc)
        {
            try
            {
                string s = PointList2XML();
                dc.Send(Encoding.UTF8.GetBytes(s.ToCharArray()));
            }
            catch ( SocketException ex)
            {
                dc.CloseConnection();
                Logger.GetInstance().LogError(ex.ToString());
            }

        }
        private void S_OnGetAlarmCount(DataClient dc)
        {

            try
            {
                using (StringWriter sw = new StringWriter())
                {
                    XmlTextWriter xtw = new XmlTextWriter(sw);
                    xtw.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"UTF-8\"");
                    xtw.WriteStartElement("GHISMS");
                    xtw.WriteStartElement("command");

                    xtw.WriteStartElement("code");
                    xtw.WriteString("103");
                    xtw.WriteEndElement();

                    xtw.WriteStartElement("function");
                    xtw.WriteString("SendAlarmCount");
                    xtw.WriteEndElement();

                    xtw.WriteStartElement("count");
                    xtw.WriteString(Almdb.GetAlarmCount().ToString());
                    xtw.WriteEndElement();


                    xtw.WriteEndElement();  //end command
                    xtw.WriteEndElement();   //end ghisms
                    //xtw.WriteEndDocument();;

                    string s = sw.ToString();
                    try
                    {
                       dc.Send(Encoding.UTF8.GetBytes(s.ToCharArray()));
                     }
                    catch ( SocketException ex)
                    {
                        dc.CloseConnection();
                        Logger.GetInstance().LogError(ex.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }

        }
        private void S_OnPointSetValue(DataClient dc, string name, string Result)
        {
            try
            {
                //生成xml字符串
                using (StringWriter sw = new StringWriter())
                {
                    XmlTextWriter xtw = new XmlTextWriter(sw);
                    xtw.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"UTF-8\"");
                    xtw.WriteStartElement("GHISMS");
                    xtw.WriteStartElement("command");

                    xtw.WriteStartElement("code");
                    xtw.WriteString("104");
                    xtw.WriteEndElement();

                    xtw.WriteStartElement("function");
                    xtw.WriteString("PointSetValueResult");
                    xtw.WriteEndElement();

                    xtw.WriteStartElement("Name");
                    xtw.WriteString(name);
                    xtw.WriteEndElement();

                    xtw.WriteStartElement("Result");
                    xtw.WriteString(Result);
                    xtw.WriteEndElement();

                    xtw.WriteEndElement();  //end command
                    xtw.WriteEndElement();   //end ghisms
                    //xtw.WriteEndDocument();;

                    string s = sw.ToString();
                    try
                    {
                         dc.Send(Encoding.UTF8.GetBytes(s.ToCharArray()));
                    }
                    catch (SocketException ex)
                    {
                        dc.CloseConnection();
                        Logger.GetInstance().LogError(ex.ToString());
                    }
                }
            }
            catch
            {

            }

        }
        /*
        private void S_OnSubcribeFormData(Socket socket, string FormName)
        {
            try
            {
                string s = FormData2XML(FormName);
                socket.Send(Encoding.UTF8.GetBytes(s.ToCharArray()));
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }


        }*/

        private void S_OnSubscribeVariable(DataClient dc, string[] pointArray)
        {
            try
            {
                //生成xml字符串
                using (StringWriter sw = new StringWriter())
                {
                    XmlTextWriter xtw = new XmlTextWriter(sw);
                    xtw.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"UTF-8\"");
                    xtw.WriteStartElement("GHISMS");
                    xtw.WriteStartElement("command");

                    xtw.WriteStartElement("code");
                    xtw.WriteString("105");
                    xtw.WriteEndElement();

                    xtw.WriteStartElement("function");
                    xtw.WriteString("SendSubscribeFormData");
                    xtw.WriteEndElement();
                    foreach (string s in pointArray)
                    {
                        IVariable  var =Rtdb.GetVariableByNameRun(s);
                        if (var != null)
                        {
                            xtw.WriteStartElement("VarList");
                            xtw.WriteAttributeString("Name", var.Name);
                            xtw.WriteAttributeString("Value", var.Value.ToString());
                            xtw.WriteAttributeString("Description", var.GetValueStateDesc());
                            xtw.WriteEndElement();
                        }
                    }

                    xtw.WriteEndElement();  //end command
                    xtw.WriteEndElement();   //end ghisms
                    //xtw.WriteEndDocument();;

                    string st = sw.ToString();
                    try
                    {
                           dc.Send(Encoding.UTF8.GetBytes(st.ToCharArray()));
                    }
                    catch (SocketException ex)
                    {
                        dc.CloseConnection();
                        Logger.GetInstance().LogError(ex.ToString());
                    }

                }
            }
            catch (Exception e)
            {
                //if (FormMain.LOG_ERR) logger.AddErr(e, "");
                Logger.GetInstance().LogError(e.ToString());
                Console.WriteLine(e.ToString());
            }
        }

        private void S_OnRefreshVariable(DataClient dc, string[] pointArray)
        {
            try
            {
                //生成xml字符串
                using (StringWriter sw = new StringWriter())
                {
                    XmlTextWriter xtw = new XmlTextWriter(sw);
                    xtw.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"UTF-8\"");
                    xtw.WriteStartElement("GHISMS");
                    xtw.WriteStartElement("command");

                    xtw.WriteStartElement("code");
                    xtw.WriteString("130");
                    xtw.WriteEndElement();

                    xtw.WriteStartElement("function");
                    xtw.WriteString("RefreshVariable");
                    xtw.WriteEndElement();
                    foreach (string s in pointArray)
                    {
                        IVariable var = Rtdb.GetVariableByNameRun(s);
                        if (var != null)
                        {
                            xtw.WriteStartElement("VarList");
                            xtw.WriteAttributeString("Name", var.Name);
                            xtw.WriteAttributeString("Value", var.Value.ToString());
                            xtw.WriteAttributeString("Description", var.GetValueStateDesc());
                            xtw.WriteEndElement();
                        }
                    }

                    xtw.WriteEndElement();  //end command
                    xtw.WriteEndElement();   //end ghisms
                    //xtw.WriteEndDocument();;

                    string st = sw.ToString();
                    try
                    {
                           dc.Send(Encoding.UTF8.GetBytes(st.ToCharArray()));
                    }
                    catch (SocketException ex)
                    {
                        dc.CloseConnection();
                        Logger.GetInstance().LogError(ex.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                //if (FormMain.LOG_ERR) logger.AddErr(e, "");
                Logger.GetInstance().LogError(e.ToString());
                Console.WriteLine(e.ToString());
            }
        }

        private void S_SendRegionTree(DataClient dc)
        {
            try
            {
                string s = Sqldb.g_RegionTree.OuterXml;
                try
                {
                       dc.Send(Encoding.UTF8.GetBytes(s.ToCharArray()));
                }
                catch (SocketException ex)
                {
                    dc.CloseConnection();
                    Logger.GetInstance().LogError(ex.ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }
        }
        private bool RegionCheck(IVariable var, User user)
        {
            bool inRegion = false;
            if (var.Area.Trim() != "")
            {
                foreach (Area area in user.RegionList)
                {
                    if (area.Name == var.Area)
                    {
                        inRegion = true;
                        break;
                    }

                }
            }
            else
            {
                inRegion = true;
            }
            return inRegion;
        }
        /// <summary>
        /// 向客户端发送登录用户可以使用的设备，按区域授权
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="user"></param>
        private void S_SendDeviceList(DataClient dc, User user)
        {
            string ret = "";
            try
            {
                //LoadCCTVInfoFromDB();
                //生成xml字符串
                using (StringWriter sw = new StringWriter())
                {
                    XmlTextWriter xtw = new XmlTextWriter(sw);
                    xtw.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"UTF-8\"");
                    xtw.WriteStartElement("GHISMS");
                    xtw.WriteStartElement("command");

                    xtw.WriteStartElement("code");
                    xtw.WriteString("113");
                    xtw.WriteEndElement();

                    xtw.WriteStartElement("function");
                    xtw.WriteString("SendDeviceList");
                    xtw.WriteEndElement();

                    #region 通讯通道信息
                    try
                    {
                        
                        foreach (IChannel chan in Rtdb.ChanList)
                        {
                            xtw.WriteStartElement("chanlist");
                            //xtw.WriteAttributeString("Guid", cam.Guid);
                            xtw.WriteAttributeString("Name", chan.Name);
                            xtw.WriteAttributeString("Description", chan.Description);
                            xtw.WriteAttributeString("Active", chan.Active ? "1" : "0");
                            xtw.WriteEndElement();
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.GetInstance().LogError("发送通道信息"+ex.ToString());
                    }
                    #endregion

                    #region 通讯控制器信息
                    try
                    {

                        foreach (IChannel chan in Rtdb.ChanList)
                        {
                            foreach (IController con in chan.ConList)
                            {
                                xtw.WriteStartElement("conlist");
                                //xtw.WriteAttributeString("Guid", cam.Guid);
                                xtw.WriteAttributeString("Name", con.Name);
                                xtw.WriteAttributeString("Description", con.Description);
                                xtw.WriteAttributeString("Active", con.Active ? "1" : "0");
                                xtw.WriteEndElement();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.GetInstance().LogError("发送通道信息" + ex.ToString());
                    }
                    #endregion

                    #region 摄像机信息
                    try
                    {
                        foreach(IChannel cha in Rtdb.ChanList)
                        {
                            if (cha is ICamChannel)
                            {
                                foreach (IController con in cha.ConList)
                                {
                                    foreach (ICamVariable cam in con.VarList)
                                    {
                                        bool inRegionCam = RegionCheck(cam, user);
                                        if (inRegionCam || user.UserName.ToLower() == "admin")  //区域授权检查
                                        {
                                            xtw.WriteStartElement("camlist");
                                            //xtw.WriteAttributeString("Guid", cam.Guid);
                                            xtw.WriteAttributeString("Name", cam.Name);
                                            xtw.WriteAttributeString("Area", cam.Area);
                                            xtw.WriteAttributeString("Address", cam.Address);
                                            xtw.WriteAttributeString("Description", cam.Description);
                                            xtw.WriteAttributeString("DvrName", cam.DvrName);
                                            xtw.WriteAttributeString("DvrInchannel", cam.DvrInchannel.ToString());
                                            xtw.WriteAttributeString("MatrixName", cam.MatrixName);
                                            xtw.WriteAttributeString("MatrixInchannel", cam.MatrixInchannel.ToString());
                                            xtw.WriteAttributeString("VodName", cam.VodName);
                                            xtw.WriteAttributeString("UseMainCodeStream", cam.UseMainCodeStream.ToString());
                                            xtw.WriteAttributeString("UseVodStream", cam.UseVodStream.ToString());
                                            xtw.WriteAttributeString("Latitudes", cam.Latitudes.ToString());
                                            xtw.WriteAttributeString("Longitude", cam.Longitude.ToString());
                                            xtw.WriteAttributeString("Marker", cam.Marker.ToString());
                                            xtw.WriteAttributeString("IconName", cam.IconName);
                                            xtw.WriteAttributeString("OperLevel", cam.OperLevel.ToString());
                                            xtw.WriteAttributeString("ProtocolCode", cam.ControllerObject.ChannelObject.ProtocolCode.ToString());
                                            xtw.WriteAttributeString("IpAddress", cam.IpAddress);
                                            xtw.WriteAttributeString("Port", cam.Port.ToString());
                                            xtw.WriteAttributeString("UserName", cam.UserName);
                                            xtw.WriteAttributeString("PassWord", cam.PassWord);
                                            xtw.WriteAttributeString("PlatName", cam.PlatName);
                                            xtw.WriteAttributeString("SerialNumber", cam.ID);

                                            xtw.WriteEndElement();
                                        }

                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.GetInstance().LogError("发送摄像机信息" + ex.ToString());
                    }

                    #endregion

                    #region 报警变量和监视器变量信息


                    foreach (var item in Rtdb.VarDict.Values)
                    {
                        if (item is IAlarmVariable)
                        {
                            bool inRegionAlm = RegionCheck(item, user);
                            if (inRegionAlm || user.UserName.ToLower() == "admin")  //区域授权检查
                            {
                                IAlarmVariable alm = item as IAlarmVariable;
                                xtw.WriteStartElement("alarmzone");
                                //xtw.WriteAttributeString("Guid", cam.Guid);
                                xtw.WriteAttributeString("Name", alm.Name);
                                xtw.WriteAttributeString("Area", alm.Area);
                                xtw.WriteAttributeString("Address", alm.Address);
                                xtw.WriteAttributeString("Description", alm.Description);
                                xtw.WriteAttributeString("ConName", alm.ControllerObject.Name);
                                xtw.WriteAttributeString("Latitudes", alm.Latitudes.ToString());
                                xtw.WriteAttributeString("Longitude", alm.Longitude.ToString());
                                xtw.WriteAttributeString("Marker", alm.Marker.ToString());
                                xtw.WriteAttributeString("IconName", alm.IconName);
                                xtw.WriteAttributeString("OperLevel", alm.OperLevel.ToString());
                                xtw.WriteEndElement();
                            }
                        }
                        if (item is IMonitorVariable)
                        {
                            bool inRegionMon = RegionCheck(item, user);
                            IMonitorVariable mon = item as IMonitorVariable;
                            if (inRegionMon || user.UserName.ToLower() == "admin")  //区域授权检查
                            {
                                xtw.WriteStartElement("monlist");
                                //xtw.WriteAttributeString("Guid", cam.Guid);
                                xtw.WriteAttributeString("Name", item.Name);
                                xtw.WriteAttributeString("Area", item.Area);
                                xtw.WriteAttributeString("Address", item.Address);
                                xtw.WriteAttributeString("Description", item.Description);
                                xtw.WriteAttributeString("MatrixName", mon.MatrixName);
                                xtw.WriteEndElement();
                            }
                        }

                    }

                    #endregion

                    #region 特定控制器信息
                    bool inRegion = false;
                    foreach (IChannel chan in Rtdb.ChanList)
                    {
                        foreach (IController con in chan.ConList)
                        {
                            inRegion = false;
                            if (con.Area.Trim() == "")
                            {
                                inRegion = true;
                            }
                            else
                            {
                                foreach (Area area in user.RegionList)
                                {
                                    if (area.Name == con.Area)
                                    {
                                        inRegion = true;
                                        break;
                                    }

                                }
                            }
                            if (inRegion || user.UserName.ToLower() == "admin")  //区域授权检查
                            {
                                //DVR
                                if (con is IDvrController)
                                {
                                    IDvrController condvr = con as IDvrController;

                                    xtw.WriteStartElement("dvrlist");
                                    //xtw.WriteAttributeString("Guid", con.Guid);
                                    xtw.WriteAttributeString("Name", condvr.Name);
                                    xtw.WriteAttributeString("Area", condvr.Area);
                                    xtw.WriteAttributeString("Address", condvr.Address.ToString());
                                    xtw.WriteAttributeString("Description", condvr.Description);
                                    xtw.WriteAttributeString("IpAddress", condvr.IpAddress);
                                    xtw.WriteAttributeString("Port", condvr.Port.ToString());
                                    xtw.WriteAttributeString("UserName", condvr.UserName);
                                    xtw.WriteAttributeString("PassWord", condvr.PassWord);
                                    xtw.WriteAttributeString("Inchannel", condvr.InChannel.ToString());
                                    xtw.WriteAttributeString("ProtocolCode", condvr.ChannelObject.ProtocolCode.ToString());

                                    //xtw.WriteAttributeString("Domain", con.Domain);
                                    //xtw.WriteAttributeString("MuiticastIp", con.MuiticastIp);
                                    xtw.WriteEndElement();
                                }
                                //矩阵
                                if (con is IMatrixController)
                                {
                                    IMatrixController conMat = con as IMatrixController;
                                    xtw.WriteStartElement("matrixlist");
                                    //tw.WriteAttributeString("Guid", conMat.Guid);
                                    xtw.WriteAttributeString("Name", conMat.Name);
                                    xtw.WriteAttributeString("Area", conMat.Area);
                                    xtw.WriteAttributeString("Address", conMat.Address.ToString());
                                    xtw.WriteAttributeString("Description", conMat.Description);
                                    //xtw.WriteAttributeString("InChannel", conMat.InChannel.ToString());
                                    //xtw.WriteAttributeString("OutChannel", conMat.OutChannel.ToString());
                                    //xtw.WriteAttributeString("UserName", conMat.UserName);
                                    //xtw.WriteAttributeString("PassWord", conMat.PassWord);
                                    xtw.WriteEndElement();
                                }
                                //流媒体
                                if (con is IVodController)
                                {
                                    IVodController conVod = con as IVodController;
                                    xtw.WriteStartElement("vodlist");
                                    //xtw.WriteAttributeString("Guid", con.Guid);
                                    xtw.WriteAttributeString("Name", conVod.Name);
                                    xtw.WriteAttributeString("Area", conVod.Area);
                                    xtw.WriteAttributeString("Address", conVod.Address.ToString());
                                    xtw.WriteAttributeString("Description", conVod.Description);
                                    xtw.WriteAttributeString("IpAddress", conVod.IpAddress);
                                    xtw.WriteAttributeString("Port", conVod.Port.ToString());
                                    
                                    xtw.WriteEndElement();
                                }
                                //安防平台
                                if (con is IPlatController)
                                {
                                    IPlatController conPlat = con as IPlatController;

                                    xtw.WriteStartElement("platlist");
                                    //xtw.WriteAttributeString("Guid", con.Guid);
                                    xtw.WriteAttributeString("Name", conPlat.Name);
                                    xtw.WriteAttributeString("Area", conPlat.Area);
                                    xtw.WriteAttributeString("Description", conPlat.Description);
                                    xtw.WriteAttributeString("IpAddress", conPlat.IpAddress);
                                    //2016/5/5修改 Port原来传的是控制器端口号，
                                    xtw.WriteAttributeString("Port", conPlat.Port.ToString());
                                    xtw.WriteAttributeString("UserName", conPlat.UserName);
                                    xtw.WriteAttributeString("PassWord", conPlat.PassWord);
                                    xtw.WriteAttributeString("ProtocolCode", conPlat.ChannelObject.ProtocolCode.ToString());

                                    xtw.WriteEndElement();
                                }
                                //报警主机
                                if (con is IAlarmController)
                                {
                                    IAlarmController conAlm = con as IAlarmController;
                                    xtw.WriteStartElement("alarmhost");
                                    //xtw.WriteAttributeString("Guid", con.Guid);
                                    xtw.WriteAttributeString("Name", conAlm.Name);
                                    xtw.WriteAttributeString("Area", conAlm.Area);
                                    xtw.WriteAttributeString("Address", conAlm.Address.ToString());
                                    xtw.WriteAttributeString("Description", conAlm.Description);
                                    xtw.WriteAttributeString("IpAddress", conAlm.IpAddress);
                                    xtw.WriteAttributeString("Port", conAlm.Port.ToString());
                                    xtw.WriteAttributeString("MacAddress", conAlm.MacAddress);
                                    xtw.WriteEndElement();
                                }
                                //LED
                                if (con is ILEDController)
                                {
                                    ILEDController conLed = con as ILEDController;
                                    xtw.WriteStartElement("ledscreen");
                                    //xtw.WriteAttributeString("Guid", con.Guid);
                                    xtw.WriteAttributeString("Name", conLed.Name);
                                    xtw.WriteAttributeString("Area", conLed.Area);
                                    xtw.WriteAttributeString("Address", conLed.Address.ToString());
                                    xtw.WriteAttributeString("Description", conLed.Description);
                                    xtw.WriteAttributeString("IpAddress", conLed.IpAddress);
                                    xtw.WriteAttributeString("Port", conLed.Port.ToString());
                                    xtw.WriteAttributeString("Width", conLed.Width.ToString());
                                    xtw.WriteAttributeString("Height", conLed.Height.ToString());


                                    xtw.WriteEndElement();

                                }
                                //公共广播
                                if (con is IIPCastController)
                                {
                                    IIPCastController ipcast= con as IIPCastController;
                                    xtw.WriteStartElement("ipcast");
                                    //xtw.WriteAttributeString("Guid", con.Guid);
                                    xtw.WriteAttributeString("Name", ipcast.Name);
                                    xtw.WriteAttributeString("Area", ipcast.Area);
                                    xtw.WriteAttributeString("Address", ipcast.Address.ToString());
                                    xtw.WriteAttributeString("Description", ipcast.Description);
                                    xtw.WriteAttributeString("IpAddress", ipcast.IpAddress);
                                    xtw.WriteAttributeString("Port", ipcast.Port.ToString());
                                    xtw.WriteEndElement();

                                }
                            }
                        }
                    }
                    #endregion

                    xtw.WriteEndElement();  //end command
                    xtw.WriteEndElement();   //end ghisms
                    ret = sw.ToString();
                    try
                    {
                          dc.Send(Encoding.UTF8.GetBytes(ret));
                    }
                    catch ( Exception ex)
                    {

                        //MessageBox.Show(ex.ToString());
                        dc.CloseConnection();
                        Logger.GetInstance().LogError(ex.ToString());
                    }

                }
            }
            catch (Exception ex)
            {
               //MessageBox.Show(ex.ToString());
                Logger.GetInstance().LogError(ex.ToString());

            }

        }
        private void S_OnChangePassword(DataClient dc, bool bSuccess, string psw)
        {
            try
            {
                //生成xml字符串
                using (StringWriter sw = new StringWriter())
                {
                    XmlTextWriter xtw = new XmlTextWriter(sw);

                    xtw.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"UTF-8\"");

                    xtw.WriteStartElement("GHISMS");
                    xtw.WriteStartElement("command");

                    xtw.WriteStartElement("code");
                    xtw.WriteString("120");
                    xtw.WriteEndElement();

                    xtw.WriteStartElement("function");
                    xtw.WriteString("ChangePassword");
                    xtw.WriteEndElement();

                    xtw.WriteStartElement("state");
                    xtw.WriteString(bSuccess ? "1" : "0");
                    xtw.WriteEndElement();


                    xtw.WriteStartElement("psw");
                    xtw.WriteString(psw);
                    xtw.WriteEndElement();

                    xtw.WriteEndElement();  //end command
                    xtw.WriteEndElement();   //end ghisms
                    ////xtw.WriteEndDocument();;
                    string ret = sw.ToString();
                    try
                    {
                        dc.Send(Encoding.UTF8.GetBytes(ret));
                    }
                    catch (SocketException ex)
                    {
                        dc.CloseConnection();
                        Logger.GetInstance().LogError(ex.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }

        }
        private void S_OnUpdateGisPos(DataClient dc, bool bSuccess)
        {
            try
            {
                //生成xml字符串
                using (StringWriter sw = new StringWriter())
                {
                    XmlTextWriter xtw = new XmlTextWriter(sw);

                    xtw.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"UTF-8\"");

                    xtw.WriteStartElement("GHISMS");
                    xtw.WriteStartElement("command");

                    xtw.WriteStartElement("code");
                    xtw.WriteString("121");
                    xtw.WriteEndElement();

                    xtw.WriteStartElement("function");
                    xtw.WriteString("OnUpdateGisPos");
                    xtw.WriteEndElement();

                    xtw.WriteStartElement("state");
                    xtw.WriteString(bSuccess ? "1" : "0");
                    xtw.WriteEndElement();


                    xtw.WriteEndElement();  //end command
                    xtw.WriteEndElement();   //end ghisms
                    ////xtw.WriteEndDocument();;
                    string ret = sw.ToString();
                    try
                    {
                        dc.Send(Encoding.UTF8.GetBytes(ret));
                    }
                    catch (SocketException ex)
                    {
                        dc.CloseConnection();
                        Logger.GetInstance().LogError(ex.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }

        }


        #region xml编码
        private string  PointList2XML()
        {
            string ret = "";
            try
            {

                //生成xml字符串
                using (StringWriter sw = new StringWriter())
                {
                    XmlTextWriter xtw = new XmlTextWriter(sw);
                    xtw.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"UTF-8\"");

 //                   xtw.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"UTF-8\"");
                    xtw.WriteStartElement("GHISMS");
                    xtw.WriteStartElement("command");

                    xtw.WriteStartElement("code");
                    xtw.WriteString("107");
                    xtw.WriteEndElement();

                    xtw.WriteStartElement("function");
                    xtw.WriteString("SendPointList");
                    xtw.WriteEndElement();



                    foreach (IChannel chan in Rtdb.ChanList)
                    {
                        //if (chan is IDataChannel)
                        {
                            foreach (IController con in chan.ConList)
                                foreach (IVariable var in con.VarList)
                                {
                                    xtw.WriteStartElement("VarList");
                                    xtw.WriteAttributeString("Name", var.Name);
                                    //xtw.WriteAttributeString("Description", var.Description);
                                    //xtw.WriteAttributeString("conName", var.ControllerObject.Name);

                                    //xtw.WriteAttributeString("addr", var.Address);
                                    //xtw.WriteAttributeString("area", var.Area);
                                    //xtw.WriteAttributeString("deviceLabel", var.DeviceLabel.ToString());
                                    //xtw.WriteAttributeString("OperLevel", var.OperLevel.ToString());
                                    xtw.WriteEndElement();
                                }
                        }
                    }

                    xtw.WriteEndElement();  //end command
                    xtw.WriteEndElement();   //end ghisms
                    //xtw.WriteEndDocument();;
                    ret = sw.ToString();

                }
            }
            catch (Exception e)
            {
                //if (FormMain.LOG_ERR) logger.AddErr(e, "");
              Logger.GetInstance().LogError(e.ToString());
            }
            return ret;

        }
       
        private string UserLogin2XML(User us)
        {
            string ret = "";
            try
            {

                //生成xml字符串
                using (StringWriter sw = new StringWriter())
                {
                    XmlTextWriter xtw = new XmlTextWriter(sw);

                    xtw.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"UTF-8\"");
                    xtw.WriteStartElement("GHISMS");
                    xtw.WriteStartElement("command");

                    xtw.WriteStartElement("code");
                    xtw.WriteString("110");
                    xtw.WriteEndElement();

                    xtw.WriteStartElement("function");
                    xtw.WriteString("SendLoginUser");
                    xtw.WriteEndElement();
                    if (us != null)
                    {
                        xtw.WriteStartElement("user");
                        xtw.WriteAttributeString("Guid", us.Guid);
                        xtw.WriteAttributeString("UserName", us.UserName);
                        xtw.WriteAttributeString("Password", us.Password);
                        xtw.WriteAttributeString("DisplayName", us.DisplayName);
                        xtw.WriteAttributeString("Email", us.Email);
                        xtw.WriteAttributeString("Phone", us.Phone);
                        xtw.WriteAttributeString("MobilePhone", us.MobilePhone);
                        xtw.WriteAttributeString("Status", us.Status.ToString());
                        xtw.WriteAttributeString("Level", us.Level.ToString());
                        xtw.WriteAttributeString("CheckKey", (User.CheckKey ||User.SoftCode).ToString());
                        xtw.WriteAttributeString("IsDisConnLogin", us.IsDisConnLogin.ToString());
                        xtw.WriteAttributeString("HasLogin", us.HasLogin.ToString());
                        xtw.WriteEndElement();

                        foreach (Area area in us.RegionList)
                        {

                            xtw.WriteStartElement("AreaList");
                            xtw.WriteAttributeString("Guid", area.Guid);
                            xtw.WriteAttributeString("Name", area.Name);
                            xtw.WriteAttributeString("Code", area.Code);
                            xtw.WriteAttributeString("Description", area.Description);
                            xtw.WriteEndElement();
                        }
                        foreach (Function fun in us.FunList)
                        {

                            xtw.WriteStartElement("FunList");
                            xtw.WriteAttributeString("Guid", fun.Guid);
                            xtw.WriteAttributeString("Name", fun.Name);
                            xtw.WriteAttributeString("Code", fun.Code);
                            xtw.WriteAttributeString("Description", fun.Description);
                            xtw.WriteAttributeString("Category", fun.Category);
                            xtw.WriteEndElement();
                        }
                    }

                    xtw.WriteEndElement();  //end command
                    xtw.WriteEndElement();   //end ghisms
                    //xtw.WriteEndDocument();;
                    ret = sw.ToString();

                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());

            }
            return ret;

        }
        private void DisArm(string ConName)
        {
            foreach (IChannel chan in Rtdb.ChanList)
                if (chan is IAlarmChannel)
                {
                    foreach (IController con in chan.ConList)
                    {
                        if (con.Name == ConName)
                        {
                            foreach (IVariable var in con.VarList)
                            {
                                // if (var.DeviceLabel == DeviceLabelEnum.报警探测器)
                                // {
                                // var.Value = 0;
                                var.UpdateValue(0);
                                // }

                            }
                        }
                    }
                }
        }
        private string UserLogout2XML(string userName)
        {
            string ret = "";
            try
            {
                //生成xml字符串
                using (StringWriter sw = new StringWriter())
                {
                    XmlTextWriter xtw = new XmlTextWriter(sw);
                    xtw.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"UTF-8\"");
                    xtw.WriteStartElement("GHISMS");
                    xtw.WriteStartElement("command");

                    xtw.WriteStartElement("code");
                    xtw.WriteString("111");
                    xtw.WriteEndElement();

                    xtw.WriteStartElement("function");
                    xtw.WriteString("SendLogoutUser");
                    xtw.WriteEndElement();

                    xtw.WriteStartElement("Name");
                    xtw.WriteString(userName);
                    xtw.WriteEndElement();

                    xtw.WriteEndElement();  //end command
                    xtw.WriteEndElement();   //end ghisms
                    //xtw.WriteEndDocument();;
                    ret = sw.ToString();

                }
            }
            catch (Exception e)
            {
                //if (FormMain.LOG_ERR) logger.AddErr(e, "");
                Console.WriteLine(e.ToString());
                Logger.GetInstance().LogError(e.ToString());
            }
            return ret;

        }
        #endregion
        #endregion

        #region  设备--->内存链表
        /*-------------------------------------------------------------------------------------------------------
         * 设备采集的数据更新链表
         * 所有报警联动在此处理
         *  //报警联动处理
            OnDeviceWrite2varList(var);
         *------------------------------------------------------------------------------------------------------*/

        
   

     

        #endregion

        #region 客户端控制指令解码
        /// <summary>
        /// 解析客户端指令
        /// </summary>
        /// <param name="st"></param>
        /// <param name="xmldoc"></param>
        public void AnalysisReceive(Socket st, XmlDocument xmldoc)
        {
            try
            {
                //Debug.WriteLine(xmldoc.InnerText);
                XmlNode code = xmldoc.SelectSingleNode("/GHISMS/command/code");
              
                if (code != null)
                {
                    int iCode = pubFun.IsInt(code.InnerText,0);
                    if ((iCode) == 0)
                    {
                        SendOperationLog(Severity.错误, StrConst.TITLE_SYS, "",  "收到错误的操作码。");
                        return;
                    }

                    DataClient dc = netService.GetDataClient(st);
                    if (dc == null)
                    {
                        SendOperationLog(Severity.错误, StrConst.TITLE_SYS, "","SOCKET连接查找失败，无法请求其它操作！操作码：" + iCode.ToString());
                        return;
                    }

                    dc.DateStamp = DateTime.Now;
                    if (iCode == (int)CommandCode.C_TEST)
                    {
                        try
                        {
                            string s = xmldoc.OuterXml.ToString();
                            dc.Send(Encoding.UTF8.GetBytes(s));
                        }
                        catch (SocketException ex)
                        {
                            dc.CloseConnection();
                            Logger.GetInstance().LogError(ex.ToString());
                        }
                        return;
                    }

                    if (iCode != (int)CommandCode.C_REQUEST_LOGIN)
                    {
                        if (dc.LoginUser == null)
                        {
                            SendOperationLog(Severity.错误, StrConst.TITLE_SYS,  dc.ClientIp,"用户未登录，无法请求其它操作！操作码："+iCode.ToString());

                            return; //非登录用户，仅能执行用户登录
                        }
                    }
                   

                    ///////////////////////////报警主机控制指令解码/////////////////////////////////////
                    switch (iCode)
                    {
                        case CommandCode.C_SET_ALARM_PANEL_STATE:     // 3: //SetAlarmPanelState
                            {
                                #region 报警主机布撤防操作
                                XmlNode nodeHostname = xmldoc.SelectSingleNode("/GHISMS/command/hostname");
                                XmlNode nodePart = xmldoc.SelectSingleNode("/GHISMS/command/part");
                                XmlNode nodeStatus = xmldoc.SelectSingleNode("/GHISMS/command/panelstatus");
                                if ((nodeHostname != null) && (nodePart != null) && (nodeStatus != null))
                                {   //上传报警信息
                                   
                                    string sHostname=nodeHostname.InnerText;
                                    int iPart = pubFun.IsNumeric(nodePart.InnerText);
                                    int iStatus = pubFun.IsNumeric(nodeStatus.InnerText);

                                    //C_AlarmHostCtrl(Hostname, iPart, iStatus);

                                    IController con = Rtdb.GetControllerByName(sHostname);
                                    if (con == null) return;

                                    object[] cmd = new object[] {iPart,iStatus,dc };
                                    con.ExecCommand(iCode,cmd);

                                    string msg = ("客户端操作指令，指令类型：报警主机布撤防，主机名称：" + nodeHostname.InnerText + "指令编码：" + nodeStatus.InnerText);
                                    SendOperationLog(Severity.信息, StrConst.TITLE_OPER, dc.LoginUser.UserName, msg);

                                }
                                #endregion
                            }
                            break;
                       
                        case CommandCode.C_SET_ALARM_ZONE_BYPASS: //4: //SetAlarmZoneBypass 旁路防区
                            {
                                #region 报警主机防区旁路
                                XmlNode nodeHostname = xmldoc.SelectSingleNode("/GHISMS/command/hostname");
                                XmlNode nodeZone = xmldoc.SelectSingleNode("/GHISMS/command/zone");
                                if ((nodeHostname != null) && (nodeZone != null))
                                {  
                            
                                    string sHostname=nodeHostname.InnerText;
                                    int iZone = pubFun.IsNumeric(nodeZone.InnerText);
                                    // C_AlarmZoneCtrl(sHoostname, PanelCtrlEnum.BYPASS, iZone);
                                    IController con = Rtdb.GetControllerByName(sHostname);
                                    if (con == null) return;
                                    object[] cmd = new object[] {PanelCtrlEnum.BYPASS, iZone,dc };
                                    con.ExecCommand(iCode,cmd);
                                    
                                    string msg = ("客户端操作指令，指令类型：报警主机防区旁路，主机名称：" + nodeHostname.InnerText + "防区：" + nodeZone.InnerText);
                                    SendOperationLog(Severity.信息, StrConst.TITLE_OPER, dc.LoginUser.UserName, msg);
                                  
                                }
                                #endregion
                            }
                           break;
                        case CommandCode.C_ALALRM_PANEL_SEND_KEY://5: //Sendkey 报警键盘控制
                            {
                                #region 报警主机键盘操作
                                XmlNode nodeHostname = xmldoc.SelectSingleNode("/GHISMS/command/hostname");
                                XmlNode nodeKey = xmldoc.SelectSingleNode("/GHISMS/command/key");
                                if ((nodeHostname != null) && (nodeKey != null))
                                {  
                                        string sHostname=nodeHostname.InnerText;
                                        int iKey = pubFun.IsNumeric(nodeKey.InnerText);
                                        //C_AlarmPanelCtrl_SendKey(nodeHostname.InnerText, PanelCtrlEnum.SENTKEY, iKey);
                                        IController con = Rtdb.GetControllerByName(sHostname);
                                        if (con == null) return;
                                        object[] cmd = new object[] {iKey,dc };
                                        con.ExecCommand(iCode,cmd);
                                    
                                        string msg = ("客户端操作指令；指令类型：报警主机键盘控制，主机名称：" + nodeHostname.InnerText + "按键编码：" + nodeKey.InnerText);
                                        SendOperationLog(Severity.信息, StrConst.TITLE_OPER, dc.LoginUser.UserName, msg);
                                   
                                }
                                  #endregion
                            }
                            break;
                          
                        case CommandCode.C_ALALRM_PANEL_SEND_KEYSEQ://6: //keysequence
                            {
                                #region 报警主机键盘组操作
                                XmlNode nodeHostname = xmldoc.SelectSingleNode("/GHISMS/command/hostname");
                                XmlNode nodeKeyseq = xmldoc.SelectSingleNode("/GHISMS/command/keysequence");
                                if ((nodeHostname != null) && (nodeKeyseq != null))
                                {  
                                    string sHostname=nodeHostname.InnerText;
                                    string sKeyseq=nodeKeyseq.InnerText;

                                    IController con = Rtdb.GetControllerByName(sHostname);
                                    if (con == null) return;
                                    object[] cmd = new object[] { sKeyseq,dc };
                                    con.ExecCommand(iCode,cmd);
                                    
                                   // C_AlarmPanelCtrl_SendKeySeq(nodeHostname.InnerText, PanelCtrlEnum.SENTKEYSEQ, nodeKeyseq.InnerText);
                                   
                                    string msg = ("客户端操作指令,指令类型：报警主机键盘控制序列，主机名称：" + nodeHostname.InnerText + "按键序列：" + nodeKeyseq.InnerText);
                                    SendOperationLog(Severity.信息, StrConst.TITLE_OPER, dc.LoginUser.UserName, msg);
                                   
                                }
                                #endregion
                            }
                            break;
                        case CommandCode.C_ALARM_PANEL_ENABLE_LCD://7: //EnableKeypad
                            {
                                #region 报警主机 信息发送使能
                                XmlNode nodeHostname = xmldoc.SelectSingleNode("/GHISMS/command/hostname");
                                if ((nodeHostname != null))
                                {
                                    string sHostname=nodeHostname.InnerText;
                                    //SetAlarmEnableKeypad(pram17.InnerText);
                                    IController con = Rtdb.GetControllerByName(sHostname);
                                    if (con == null) return;
                                    object[] cmd = new object[] {dc };
                                    con.ExecCommand(iCode,cmd);

                                    string msg = ("客户端操作指令；指令类型：报警主机键盘使能， 主机名称：" + nodeHostname.InnerText);
                                    SendOperationLog(Severity.信息, StrConst.TITLE_OPER, dc.LoginUser.UserName, msg);
                                }
                                #endregion
                            }
                            break;
                        ///////////////////////////////矩阵控制指令/////////////////////////////
                        case CommandCode.C_VIDEO_CONTROL://10: //VideoControl
                            {
                                #region 视频操作
                                Debug.WriteLine("视频操作C_VIDEO_CONTROL");
                                XmlNode nodeCmd = xmldoc.SelectSingleNode("/GHISMS/command/cmd"); 
                                XmlNode nodeCamName = xmldoc.SelectSingleNode("/GHISMS/command/camname");
                                XmlNode nodeMatrixName = xmldoc.SelectSingleNode("/GHISMS/command/matrixname");
                                XmlNode nodeMonName = xmldoc.SelectSingleNode("/GHISMS/command/monname");
                                XmlNode nodeVideoIn = xmldoc.SelectSingleNode("/GHISMS/command/videoin");
                                XmlNode nodeVideoOut = xmldoc.SelectSingleNode("/GHISMS/command/videoout");
                                XmlNode nodeSubVideoOut = xmldoc.SelectSingleNode("/GHISMS/command/subvideoout");
                                XmlNode vparam1 = xmldoc.SelectSingleNode("/GHISMS/command/vparam1");
                                XmlNode vparam2 = xmldoc.SelectSingleNode("/GHISMS/command/vparam2");
                                XmlNode vparam3 = xmldoc.SelectSingleNode("/GHISMS/command/vparam3");
                                if ((nodeCmd != null) && (nodeCamName != null) && (nodeMatrixName != null) && (nodeVideoIn != null)
                                    && (nodeVideoOut != null) && (nodeSubVideoOut!=null) && (vparam1 != null) && (vparam2 != null) && (vparam3 != null))
                                {
                                    VideoCommandArgs args = new VideoCommandArgs();
                               
                                    PTZCmdCodeEnum ptzCode= (PTZCmdCodeEnum)pubFun.IsNumeric(nodeCmd.InnerText);
                                    args.VideoCommand = ptzCode;
                                    args.dataClient = dc;
                                    args.CamName = nodeCamName.InnerText;
                                    args.SubVideoOut = pubFun.IsNumeric(nodeSubVideoOut.InnerText);
                                    if (nodeMonName != null)
                                        args.MonName = nodeMonName.InnerText;
                                  
                                    #region 处理最后的3个可变含义参数
                                    switch (ptzCode)
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
                                            args.Stop = uint.Parse(vparam1.InnerText);
                                            args.Speed = uint.Parse(vparam2.InnerText);
                                            break;
                                        //预置位
                                        case PTZCmdCodeEnum.CLE_PRESET: // "9"
                                        case PTZCmdCodeEnum.GOTO_PRESET:// "39"
                                        case PTZCmdCodeEnum.SET_PRESET: // "8"
                                            args.PresetIndex = uint.Parse(vparam1.InnerText);
                                            break;
                                        //巡航
                                        case PTZCmdCodeEnum.FILL_PRE_SEQ:// "30"
                                        case PTZCmdCodeEnum.SET_SEQ_DWELL:// "31"
                                        case PTZCmdCodeEnum.SET_SEQ_SPEED:// "32"
                                        case PTZCmdCodeEnum.CLE_PRE_SEQ:// "33"
                                        case PTZCmdCodeEnum.RUN_SEQ:// "37"
                                        case PTZCmdCodeEnum.STOP_SEQ:// "38"
                                            args.CruiseRoute = Convert.ToByte(vparam1.InnerText);
                                            args.CruisePoint = Convert.ToByte(vparam2.InnerText);
                                            args.Input = Convert.ToUInt16(vparam3.InnerText);
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
                                            args.AutoRunIndex = uint.Parse(vparam1.InnerText);
                                            break;
                                        case PTZCmdCodeEnum.MAT_HOLD: //"104":
                                            break;
                                        case PTZCmdCodeEnum.MAT_GROUP:// "105"
                                            args.GroupIndex = uint.Parse(vparam1.InnerText);
                                            break;
                                    }
                                    #endregion
                                
                                    string msg = string.Format("客户端操作指令,指令类型：视频控制，指令编码：{0},摄像机：{1},监视器：{2}", args.VideoCommand, args.CamName,args.MonName);
                                    SendOperationLog(Severity.信息, StrConst.TITLE_OPER, dc.LoginUser.UserName, msg);
                                    //C_VideoControl(args);
                                     
                                    IController  con = Rtdb.GetMatrixByMonNameRun(args.MonName);
                                    if (con != null)
                                    {
                                        object[] cmd = new object[] { args };
                                        args.oCon = con;
                                        con.ExecCommand(iCode, cmd);
                                      
                                    }
                                    else
                                    {
                                        dc.SendMessageToClient(StrConst.CALLBACK_INFO,"矩阵控制失败！找不到关联的控制器，监视器："+args.MonName);
                                        msg = string.Format("客户端视频控制指令没有执行，可能是监视器没有设置关联矩阵名称，监视器：" + args.MonName);
                                        SendOperationLog(Severity.信息, StrConst.TITLE_OPER, dc.LoginUser.UserName, msg);
           
                                    }

                                }
                                #endregion
                            }
                            break;
                        #region 禁用代码，功能并入VIDEO CLONTRL
                        /*
                        //case 11: //PTZPreset
                        //    {
                        //        XmlNode bpram12 = xmldoc.SelectSingleNode("/GHISMS/command/matrixid"); //主机号
                        //        XmlNode bpram22 = xmldoc.SelectSingleNode("/GHISMS/command/camid");
                        //        XmlNode bpram32 = xmldoc.SelectSingleNode("/GHISMS/command/presetid");
                        //        XmlNode bpram42 = xmldoc.SelectSingleNode("/GHISMS/command/presetcmd");

                        //        if ((bpram12 != null) && (bpram22 != null) & (bpram32 != null) & (bpram42 != null))
                        //        {
                        //            int ibpram12 = pubFun.IsNumeric(bpram12.InnerText);
                        //            int ibpram22 = pubFun.IsNumeric(bpram22.InnerText);
                        //            int ibpram32 = pubFun.IsNumeric(bpram32.InnerText);
                        //            int ibpram42 = pubFun.IsNumeric(bpram42.InnerText);


                        //            if ((ibpram12 > -1) && (ibpram22 > -1) && (ibpram32 > -1) && (ibpram42 > -1))
                        //            {
                        //                //处理
                        //                PTZPreset(ibpram12, ibpram22, ibpram32, ibpram42);
                        //                Log("收到客户端的摄像机预置位操作指令；指令类型：PTZPreset，矩阵地址！" + bpram12.InnerText + "摄像机：" + bpram22.InnerText + "预置位：" + bpram32.InnerText + "命令码：" + bpram42.InnerText);

                        //            }
                        //        }
                        //    }
                        //    break;
                        //case 12: //PTZControl
                        //    {
                        //        XmlNode bpram11 = xmldoc.SelectSingleNode("/GHISMS/command/matrixid"); //主机号
                        //        XmlNode bpram21 = xmldoc.SelectSingleNode("/GHISMS/command/camid");
                        //        XmlNode bpram31 = xmldoc.SelectSingleNode("/GHISMS/command/VideoCommand");
                        //        XmlNode bpram41 = xmldoc.SelectSingleNode("/GHISMS/command/stop");
                        //        XmlNode bpram51 = xmldoc.SelectSingleNode("/GHISMS/command/speed");

                        //        if ((bpram11 != null) && (bpram21 != null) & (bpram31 != null) & (bpram41 != null) & (bpram51 != null))
                        //        {
                        //            int ibpram11 = pubFun.IsNumeric(bpram11.InnerText);
                        //            int ibpram21 = pubFun.IsNumeric(bpram21.InnerText);
                        //            int ibpram31 = pubFun.IsNumeric(bpram31.InnerText);
                        //            int ibpram41 = pubFun.IsNumeric(bpram41.InnerText);
                        //            int ibpram51 = pubFun.IsNumeric(bpram51.InnerText);


                        //            if ((ibpram11 > -1) && (ibpram21 > -1) && (ibpram31 > -1) && (ibpram41 > -1) && (ibpram51 > -1))
                        //            {
                        //                //处理
                        //                PTZControl(ibpram11, ibpram21, (MatrixCommandEnum)ibpram31, ibpram41, ibpram51);
                        //                Log("收到客户端的云台控制操作指令；指令类型：PTZControl，矩阵地址！" + bpram11.InnerText + "摄像机：" + bpram21.InnerText + "云台命令：" + bpram31.InnerText + "启停：" + bpram41.InnerText + "速度：" + bpram51.InnerText);

                        //            }
                        //        }
                        //    }
                        //    break;*/
                        #endregion
                        case CommandCode.C_CLEAR_ALARM:  //201: //2011-08-09 add ClearAlarm  消警
                            {
                                #region 消警
                                XmlNode nodeGuid = xmldoc.SelectSingleNode("/GHISMS/command/Guid");
                                XmlNode nodeResult = xmldoc.SelectSingleNode("/GHISMS/command/Result");
                                if (nodeGuid != null && nodeResult != null)
                                {
                                    //处理
                                    C_AlarmClear(dc, nodeGuid.InnerText, nodeResult.InnerText);
                                    string msg = ("客户端操作指令；指令类型：消警，报警ID：" + nodeGuid.InnerText + "  报警原因：" + nodeResult.InnerText);
                                    SendOperationLog(Severity.信息, StrConst.TITLE_OPER, dc.LoginUser.UserName, msg);
                      
                                }
                                #endregion
                            }
                            break;
                        case CommandCode.C_GET_ALARM_LIST: // 202: //2011-08-09 add GetAlarmList  发送报警列表
                            {
                                #region 获取报警列表
                                    S_SendAlarmList(dc);
                                    string msg = ("客户端操作指令,指令类型：请求发送报警列表");
                                    SendOperationLog(Severity.信息, StrConst.TITLE_OPER, dc.LoginUser.UserName, msg);
                                #endregion
                            }
                            break;
                        case CommandCode.C_GET_ALARM_COUNT://203: //2011-08-09 add GetAlarmCount  报警数据
                            {
                                #region 获取报警总数
                                    S_OnGetAlarmCount(dc);
                                    string msg = ("客户端操作指令,指令类型：请求发送报警数量[GetAlarmList]");
                                    SendOperationLog(Severity.信息, StrConst.TITLE_OPER, dc.LoginUser.UserName, msg);
                                    #endregion

                            }
                            break;
                        case CommandCode.C_SET_VAR_VALUE:// 204: //2011-08-09 add PointSetValue  点设定值
                            {
                                #region 设定变量数值
                                XmlNode nodeName = xmldoc.SelectSingleNode("/GHISMS/command/Name");
                                XmlNode nodeValue = xmldoc.SelectSingleNode("/GHISMS/command/Value");
                                if (nodeName != null && nodeValue != null)
                                {
                                        IVariable var = Rtdb.GetVariableByNameRun(nodeName.InnerText);
                                        if (var != null)
                                        {

                                            if (var.WriteValue(nodeValue.InnerText))
                                            {
                                                S_OnPointSetValue(dc, nodeName.InnerText, "True");
                                            }
                                            else
                                            {
                                                S_OnPointSetValue(dc, nodeName.InnerText, "False");
                                            }
                                        }
                                        string msg = ("客户端操作指令,；指令类型：变量设定值[PointSetValue],Name:" + nodeName.InnerText + ",Value:" + nodeValue.InnerText);
                                        SendOperationLog(Severity.信息, StrConst.TITLE_OPER, dc.LoginUser.UserName, msg);
                                }
                                #endregion
                            }
                            break;
                        case CommandCode.C_SUBSCRIBE_FORM_VAR:// 205: //2011-08-09 add SubscribeFormData  订阅FormData
                            {
                                #region 订阅画面数据
                                XmlNode nodeName = xmldoc.SelectSingleNode("/GHISMS/command/Name");
                                if (nodeName != null)
                                {
                                    if (nodeName.InnerText != "")
                                    {
                                        SubscribeFormData(dc, nodeName.InnerText);
                                    }
                                    string msg = ("客户端操作指令,指令类型：订阅画面数据[SubscribeFormData],Name:" + nodeName.InnerText);
                                    SendOperationLog(Severity.信息, StrConst.TITLE_OPER, dc.LoginUser.UserName, msg);
                                   
                                }
                                #endregion
                            }
                            break;
                        case CommandCode.C_UNSUBSCRIBE_FORM_VAR:// 206: //2011-08-09 add UnSubscribeFormData  取消订阅FormData
                            {
                                #region 取消订阅
                                XmlNode nodeName = xmldoc.SelectSingleNode("/GHISMS/command/Name");
                                if (nodeName != null)
                                {
                                     if (nodeName.InnerText != "")
                                     {
                                        UnSubscribeFormData(dc, nodeName.InnerText);
                                     }
                                    string msg = ("客户端操作指令,指令类型：取消订阅画面数据[UnSubscribeFormData],Name:" + nodeName.InnerText);
                                    SendOperationLog(Severity.信息, StrConst.TITLE_OPER, dc.LoginUser.UserName, msg);
                                }
                                #endregion
                                break;
                            }
                        case CommandCode.C_GET_VAR_LIST:// 207: //GetPointList  发送变量列表
                            {
                                #region 发送变量列表
                                    S_SendPointList(dc);
                                    string msg = ("客户端操作指令,指令类型：请求发送点列表[PointGetList]");
                                    SendOperationLog(Severity.信息, StrConst.TITLE_OPER, dc.LoginUser.UserName, msg);
                                #endregion
                            }
                            break;
                        case CommandCode.C_GET_REGION:// 208: //GetRegion  获取区域列表
                            {
                                 #region 发送区域列表
                                    string msg2 = ("客户端操作指令,指令类型：请求发送区域列表[GetRegion]");
                                    SendOperationLog(Severity.信息, StrConst.TITLE_OPER, dc.LoginUser.UserName, msg2);
                                    S_SendRegionTree(dc);
                                 #endregion
                            }
                            break;

                        case CommandCode.C_REQUEST_LOGIN:// 210: //LoginRequest  用户登录
                            {
                                #region 用户登录
                                XmlNode nodeName = xmldoc.SelectSingleNode("/GHISMS/command/Name");
                                XmlNode nodePsw = xmldoc.SelectSingleNode("/GHISMS/command/Psw");
                                XmlNode nodeRelogin = xmldoc.SelectSingleNode("/GHISMS/command/Relogin");
                                if (nodeName != null && nodePsw != null && nodeRelogin != null)
                                {
                                    if (dc != null)
                                        C_Login(dc, nodeName.InnerText, nodePsw.InnerText,pubFun.IsBool(nodeRelogin.InnerText,false));
                                    else
                                    {
                                        //Console.WriteLine("login DC:" + dc.Socket.Handle.ToString()+"user:"+dc.LoginUser.UserName);
                                        string msg3 = ("客户端登录错误，用户名" + nodeName.InnerText);
                                        SendOperationLog(Severity.信息, StrConst.TITLE_OPER, nodeName.InnerText, msg3);
                                    }

                                }
                                #endregion
                            }
                            break;
                        case CommandCode.C_REQUEST_LOGOUT:// 211: //LogoutRequest  用户注销
                            {
                                #region 用户注销
                                XmlNode nodeName = xmldoc.SelectSingleNode("/GHISMS/command/Name");
                                if (nodeName != null)
                                {
                                    if (dc.LoginUser != null)
                                    {
                                        string msg4 = ("客户端操作指令,指令类型：用户注销请求[LoginoutRequest]，用户名" + nodeName.InnerText);
                                        SendOperationLog(Severity.信息, StrConst.TITLE_OPER, nodeName.InnerText, msg4);
                                        C_Logout(dc, nodeName.InnerText);
                                    }
                                }
                                #endregion
                            }
                            break;
                        case  CommandCode.C_GET_EVENT_HIS_LIST:////214: //请求事件记录
                            {
                                #region 事件记录
                                XmlNode nodeTime = xmldoc.SelectSingleNode("/GHISMS/command/time");
                                XmlNode nodeType = xmldoc.SelectSingleNode("/GHISMS/command/type");
                                if (nodeTime != null && nodeType != null)
                                {
                                    C_GetEventHistory(dc, nodeTime.InnerText, nodeType.InnerText);
                                    string msg5 = ("客户端操作指令,指令类型：请求查询事件记录，查询时间：" + nodeTime.InnerText);
                                    SendOperationLog(Severity.信息, StrConst.TITLE_OPER, dc.LoginUser.UserName, msg5);
                                }
                                #endregion
                            }
                            break;

                        case CommandCode.C_GET_ALARM_HIS_LIST://215: //请求报警记录
                            {
                                #region 报警记录
                                XmlNode nodeTime = xmldoc.SelectSingleNode("/GHISMS/command/time");
                                XmlNode nodeName = xmldoc.SelectSingleNode("/GHISMS/command/name");
                                if (nodeTime != null && nodeName != null)
                                {
                                    C_GetAlarmHistory(dc, nodeTime.InnerText, nodeName.InnerText);
                                    string msg6 = ("客户端操作指令,指令类型：请求查询报警记录，查询时间：" + nodeTime.InnerText);
                                    SendOperationLog(Severity.信息, StrConst.TITLE_OPER, dc.LoginUser.UserName, msg6);
                                }
                                #endregion
                            }
                            break;
                        case CommandCode.C_GET_DATA_HIS_LIST:// 216: //请求数据历史记录
                            {
                                #region 历史记录
                                XmlNode nodeTime = xmldoc.SelectSingleNode("/GHISMS/command/time");
                                XmlNode nodeName = xmldoc.SelectSingleNode("/GHISMS/command/name");
                                XmlNode nodeTpye = xmldoc.SelectSingleNode("/GHISMS/command/type");
                                if (nodeTime != null && nodeName != null && nodeTpye != null)
                                {
                                    C_GetDataHistory(dc, nodeTime.InnerText, nodeName.InnerText, nodeTpye.InnerText);
                                    string msg7 = ("客户端操作指令,指令类型：请求查询数据历史记录，查询时间：" + nodeTime.InnerText);
                                    SendOperationLog(Severity.信息, StrConst.TITLE_OPER, dc.LoginUser.UserName, msg7);
                                }
                                #endregion
                            }
                            break;
                        case CommandCode.C_GET_ALARM_HOST_STATE:// 217: //请求 报警主机状态
                            {
                                #region 查询报警主机状态 
                                XmlNode nodeHostList = xmldoc.SelectSingleNode("/GHISMS/command/hostlist");
                                if (nodeHostList != null)
                                {
                                    S_SentAlarmHostState(dc, nodeHostList.InnerText);
                                    //string msg8 = ("客户端操作指令,指令类型：请求查询数报警主机状态，主机名称：" + pram1.InnerText);
                                    //SendOperationLog(Severity.信息, StrConst.TITLE_OPER, dc.LoginUser.UserName, msg8);
                                }
                                #endregion
                            }
                            break;
                        case CommandCode.C_GET_CHANNEL_STATE:// 218: //请求 驱动状态
                            {
                                S_SentChannelState(dc);
                                string msg9 = ("客户端操作指令,指令类型：请求查通讯状态");
                                SendOperationLog(Severity.信息, StrConst.TITLE_OPER, dc.LoginUser.UserName, msg9);
                            }

                            break;
                        case CommandCode.C_SET_CHANNEL_STATE:// 219: //设置通道状态
                            {
                                XmlNode nodeName = xmldoc.SelectSingleNode("/GHISMS/command/name");
                                XmlNode nodeActive = xmldoc.SelectSingleNode("/GHISMS/command/active");
                                XmlNode nodetype = xmldoc.SelectSingleNode("/GHISMS/command/type");
                                if (nodeName != null && nodeActive != null && nodetype != null)
                                {
                                    C_SetDeviceState(nodeName.InnerText, pubFun.IsNumeric(nodeActive.InnerText), pubFun.IsNumeric(nodetype.InnerText));
                                    string msg10 = ("客户端操作指令,指令类型：设置通讯状态，名称：" + nodeName.InnerText + " 设定值：" + nodeActive.InnerText) + "类型：" + nodetype.InnerText;
                                    SendOperationLog(Severity.信息, StrConst.TITLE_OPER, dc.LoginUser.UserName, msg10);
                                    DBAssistant.GetInstance().AddOperation("Information", "操作日志", dc.ClientIp, "用户：" + dc.LoginUser.UserName + " 进行设备启用禁用操作！+，名称：" + nodeName.InnerText + " 设定值：" + nodeActive.InnerText + "类型：" + nodetype.InnerText);

                                }
                            }
                            break;
                        case CommandCode.C_CHANGE_PASSWORD:// 220: //修改密码
                            {
                                XmlNode nodePsw = xmldoc.SelectSingleNode("/GHISMS/command/psw");
                                if (nodePsw != null)
                                {
                                    C_ChangePassword(dc, dc.LoginUser.UserName, nodePsw.InnerText);
                                    string msg10 = ("客户端操作指令,指令类型：修改密码。");
                                    SendOperationLog(Severity.信息, StrConst.TITLE_OPER, dc.LoginUser.UserName, msg10);
                                }
                            }
                            break;
                        case CommandCode.C_UPDATE_GIS_POS:// 221: //gis marker set pos
                            {
                                #region GIS图标
                                XmlNode nodePoint = xmldoc.SelectSingleNode("/GHISMS/command/point");
                                XmlNode nodeLn = xmldoc.SelectSingleNode("/GHISMS/command/ln");
                                XmlNode nodeLa = xmldoc.SelectSingleNode("/GHISMS/command/la");
                                XmlNode nodeShow = xmldoc.SelectSingleNode("/GHISMS/command/bshow");
                                XmlNode nodeIcon = xmldoc.SelectSingleNode("/GHISMS/command/iconname");

                                if (nodePoint != null && nodeLn != null && nodeLa != null && nodeShow != null && nodeIcon != null)
                                {
                                    C_GisSetPos(dc, nodePoint.InnerText, nodeLn.InnerText, nodeLa.InnerText, nodeShow.InnerText, nodeIcon.InnerText);
                                    string msg10 = ("客户端操作指令,更新点的GIS定位：点名称：" + nodePoint.InnerText);
                                    SendOperationLog(Severity.信息, StrConst.TITLE_OPER, dc.LoginUser.UserName, msg10);
                                }
                                #endregion
                            }
                            break;
                        case CommandCode.C_RESET_ALARM:// 222: //2012-10-010 AlarmReset  报警复位
                            {
                                C_AlarmReset(dc);
                                string msg = ("客户端操作指令：报警复位");
                                SendOperationLog(Severity.信息, StrConst.TITLE_OPER, dc.LoginUser.UserName, msg);
                            } 
                            break;
                        case CommandCode.C_LED_SHOW_STRING://223 2012-11-25
                            {
                                C_LedShowString(dc, xmldoc);
                            }
                            break;
                        case CommandCode.C_LED_REPLAY:
                            {
                                C_LedReplay(dc, xmldoc);
                            }
                            break;
                        case CommandCode.C_LED_RESET:
                            {
                                C_LedReset(dc, xmldoc);
                            }
                            break;
                        case CommandCode.C_REFRESH_VAR:
                            {
                                XmlNode node = xmldoc.SelectSingleNode("/GHISMS/command/Name");
                                if (node != null)
                                {
                                    if (node.InnerText != "")
                                    { 
                                        C_RefreshVariable(dc, node.InnerText);
                                    }
                                }
                            }
                            break;
                        case CommandCode.C_GET_DATA_HIS_LIST_CURVE:// 231: //请求数据历史记录
                            {
                                #region 历史记录
                                XmlNode nodeTime = xmldoc.SelectSingleNode("/GHISMS/command/time");
                                XmlNode nodeName = xmldoc.SelectSingleNode("/GHISMS/command/name");
                                if (nodeTime != null && nodeName != null)
                                {
                                    C_GetDataHistoryCurve(dc, nodeTime.InnerText, nodeName.InnerText);
                                }
                                #endregion
                            }
                            break;
                        case CommandCode.C_GET_FUN_DETAIL: //233:请求风电风机明细
                            {
                                XmlNode node = xmldoc.SelectSingleNode("/GHISMS/command/id");
                                if (node != null)
                                {
                                    if (node.InnerText != "")
                                    {
                                        C_GetFunDetail(dc, node.InnerText);
                                    }
                                }

                            }
                            break;
                        case CommandCode.C_GET_ALARM_PRECASE: //234:请求报警预案
                            {
                                XmlNode node = xmldoc.SelectSingleNode("/GHISMS/command/variable");
                                if (node != null)
                                {
                                    if (node.InnerText != "")
                                    {
                                        C_GetAlarmPreCase(dc, node.InnerText);
                                    }
                                }

                            }
                            break;
                        case CommandCode.C_LCD_SCREEN_CTRL:
                            {
                                #region LCD屏操作
                                XmlNode nodeName = xmldoc.SelectSingleNode("/GHISMS/command/name");
                                XmlNode nodeCode = xmldoc.SelectSingleNode("/GHISMS/command/ctrlcode");
                                XmlNode nodeRC = xmldoc.SelectSingleNode("/GHISMS/command/screenrc");
                                XmlNode nodeArgs = xmldoc.SelectSingleNode("/GHISMS/command/args");

                                if ((nodeName != null) && (nodeCode != null) && (nodeRC != null) && (nodeArgs != null))
                                {
                                    string strName = nodeName.InnerText;
                                    string strCode = nodeCode.InnerText;
                                    string strRc = nodeRC.InnerText;
                                    string strArgs = nodeArgs.InnerText;

                                    LCDCommandArgs args = new LCDCommandArgs();
                                    args.CmdCode =strCode;
                                    args.ScreenRC = strRc;
                                    args.VariabilityArgs = strArgs;


                                    string msg = string.Format("客户端操作指令,指令类型：LCD控制，指令编码：{0},控制器名称：{1})", strCode, strName);
                                    SendOperationLog(Severity.信息, StrConst.TITLE_OPER, dc.LoginUser.UserName, msg);

                                    IController con = Rtdb.GetLCDControllByNameRun(strName);
                                    if (con != null)
                                    {
                                        object[] cmd = new object[] { args };
                                        args.oCon = con;
                                        con.ExecCommand(iCode, cmd);

                                    }
                                    else
                                    {
                                        dc.SendMessageToClient(StrConst.CALLBACK_INFO, "LCD控制失败！找不到关联的控制器：" + strName);
                                        msg = string.Format("客户端LCD控制指令没有执行，找不到关联的控制器：" + strName);
                                        SendOperationLog(Severity.信息, StrConst.TITLE_OPER, dc.LoginUser.UserName, msg);

                                    }
                                

                                }
                                #endregion
                                break;

                            }
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
                Console.WriteLine(ex.ToString());
            }
        }

     
        //检测客户端连接是否正常
        private void timerTest_Tick(object sender, EventArgs e)
        {
            //try
            //{
            //    for (int i = 0; i < dataClientList.Count; i++)
            //    {
            //        if (!dataClientList[i].TestOk && dataClientList[i].HasLogin)
            //            continue;
            //        dataClientList[i].ConnCount++;
            //        if (dataClientList[i].ConnCount > DataClient.MAX_CONN_COUNT)
            //            dataClientList[i].CloseConnection();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    if (FormMain.LOG_ERR)
            //        LogErrToFile(ex.TargetSite.Name, ex.Message, ex.StackTrace);
            //    //}
            //}
        }
        //发送信息
        private bool ClientSend(Socket client, String s)
        {
            if (client != null)
            {
                try
                {
                    //Console.WriteLine("send:" + s);
                    client.Send(Encoding.UTF8.GetBytes(s.ToCharArray()));
                    return true;
                }
                catch (Exception ex)
                {
                    Logger.GetInstance().LogError(ex.ToString());
                }
            }
            return false;
        }


        #endregion


        void SendOperationLog(Severity severity, String Title, String MachineName, String Message)
        {
            if (OnCommMsg != null)
                OnCommMsg(this,severity, CommunicationEvent.COMM_INFO, MachineName, Message);

        }


      

    }
}
  