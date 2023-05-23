using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Net.Sockets;
using System.Xml;
using System.Text.RegularExpressions;
using System.Net;
using GHIBMS.Common;
using System.IO;
using System.Threading;


using System.Diagnostics;

using GHIBMS.Interface;
using GHCore;
using GHNETBASE.RTDB;
using Newtonsoft.Json.Linq;

namespace GHIBMS.Server
{
    public  class DrvSvr
    {
      
        #region 报警事件判定
        /// <summary>
        /// 判断变量是否触发报警事件
        /// </summary>
        /// <param name="var"></param>
        private void VarOnEventTrigger(IVariable var)
        {
            //Console.WriteLine(Thread.CurrentThread);
            if (var.WayList.Count > 0)
            {
                try
                {
                    //自定义事件条件判断，同时判断自定义条件是不包括了数值：1的默认报警
                    foreach (VariableTrigger way in var.WayList)
                    {
                        //时限检查 为空时不检查时限
                        if (!string.IsNullOrEmpty(way.TimZone))
                        {
                            //根据配置的名称，查找时限对象
                            TimeZoneClt timeZone = TimeZoneDb.GetTimeZonebyName(way.TimZone);
                            if (timeZone != null)
                            {
                                //如果不在时限内，直接跳过事件联动
                                if (!timeZone.IsActive())
                                {
                                    continue;
                                }
                            }
                        }

                        //数值改变事件检查
                        if (way.Express == "数值改变")
                        {
                            //报警联动
                            VarEventActionHandle(new AlarmMessage(var, way), var);
                            //执行脚本
                            if (!string.IsNullOrEmpty(way.ScriptText.Trim()))
                                Var_ActionScript(var, way.ScriptText);
                        }
                        else
                        {
                            #region 超限检查
                            string[] arr = way.Express.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string s in arr)
                            {
                                string[] s1 = s.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                                if (s1.Length == 2)
                                {
                                    //超过上限检查
                                    if (s1[0] == "上限")
                                    {
                                        if (pubFun.IsDouble(var.Value.ToString(), -1) >= pubFun.IsDouble(s1[1], 0))
                                        {

                                            if (string.IsNullOrEmpty(way.EventDesc))
                                                way.EventDesc = "超上限报警";
                                            //报警联动
                                            VarEventActionHandle(new AlarmMessage(var, way), var);
                                            //执行脚本
                                            if (!string.IsNullOrEmpty(way.ScriptText.Trim()))
                                                Var_ActionScript(var, way.ScriptText);
                                        }
                                    }
                                    //超过下限检查
                                    else if (s1[0] == "下限")
                                    {
                                        if (pubFun.IsDouble(var.Value.ToString(), 0) <= pubFun.IsDouble(s1[1], -1))
                                        {
                                            if (string.IsNullOrEmpty(way.EventDesc))
                                                way.EventDesc = "超下限报警";
                                            //报警联动
                                            VarEventActionHandle(new AlarmMessage(var, way), var);
                                            //执行脚本
                                            if (!string.IsNullOrEmpty(way.ScriptText.Trim()))
                                                Var_ActionScript(var, way.ScriptText);

                                        }
                                    }
                                    //数值相等检查
                                    else if (s1[0] == "数值")
                                    {
                                        if (pubFun.IsInt(var.Value.ToString(), 0) == pubFun.IsInt(s1[1], -1))
                                        {
                                            if (string.IsNullOrEmpty(way.EventDesc))
                                                way.EventDesc = "自定义报警警";
                                            //报警联动
                                            VarEventActionHandle(new AlarmMessage(var, way), var);
                                            //执行脚本
                                            if (!string.IsNullOrEmpty(way.ScriptText.Trim()))
                                                Var_ActionScript(var, way.ScriptText);

                                        }
                                    }
                                    //数值不等检查
                                    else if (s1[0] == "不等")
                                    {
                                        //披克门禁OPC 去除没有监控的误报
                                        if (var.Value.ToString().Trim() != "没有监控")
                                        {
                                            if (s1[1] == "空")
                                            {
                                                if (!string.IsNullOrEmpty(var.Value.ToString()))
                                                {
                                                    if (string.IsNullOrEmpty(way.EventDesc))
                                                        way.EventDesc = "自定义报警";
                                                    //报警联动
                                                    VarEventActionHandle(new AlarmMessage(var, way), var);
                                                    //执行脚本
                                                    if (!string.IsNullOrEmpty(way.ScriptText.Trim()))
                                                        Var_ActionScript(var, way.ScriptText);


                                                }

                                            }
                                            else if (pubFun.IsInt(var.Value.ToString(), -1) != pubFun.IsInt(s1[1], 0))
                                            {
                                                if (string.IsNullOrEmpty(way.EventDesc))
                                                    way.EventDesc = "自定义报警";
                                                //报警联动
                                                VarEventActionHandle(new AlarmMessage(var, way), var);
                                                //执行脚本
                                                if (!string.IsNullOrEmpty(way.ScriptText.Trim()))
                                                    Var_ActionScript(var, way.ScriptText);
                                            }
                                        }

                                    }

                                }

                            }
                            #endregion
                        }

                    } //end foreach
                }
                catch (Exception ex)
                {
                    Logger.GetInstance().LogError(ex.ToString());
                }
            }
            //所有报警需要通过自定义报警条件
            //自定义报警中定义了状态1报警则不再处理缺省报警
            //缺省报警仅用于变量的DeviceLabel为报警输入变量和通讯报警变量
            /*else
            {
                if (var.DeviceLabel == DeviceLabelEnum.通讯报警.ToString())
                {
                    if (var.Value != null && var.Value.ToString() != "" && var.Value.ToString() == "1")
                    {
                        //创建一个通讯报警的触发器
                        string Express = "数值:1";
                        string EventDesc = "通讯报警";
                        int EventType = (int)EventTypeEnum.故障报警;
                        uint Priority = 2;
                        string ScriptText = "";
                        string timezone = "";
                        VariableTrigger way = new VariableTrigger(Express,
                                   EventType,
                                   EventDesc,
                                   Priority,
                                   ScriptText,
                                   timezone);

                        VarEventActionHandle(new AlarmMessage(var, way), var);
                    }
                }
                else if (var is IAlarmVariable && var.DeviceLabel == (DeviceLabelEnum.报警输入.ToString()))
                {
                    //处理报警 1
                    if (var.Value != null && var.Value.ToString() != "" && var.Value.ToString() == "1")
                    {

                        string Express = "数值:1";
                        string EventDesc = "普通报警";
                        int EventType = (int)EventTypeEnum.普通报警;
                        uint Priority = 3;
                        string ScriptText = "";
                        string timezone = "";
                        VariableTrigger way = new VariableTrigger(Express,
                                   EventType,
                                   EventDesc,
                                   Priority,
                                   ScriptText,
                                   timezone);


                        VarEventActionHandle(new AlarmMessage(var, way), var);

                    }

                }
            }*/
        }

        /// <summary>
        /// 2.0
        /// </summary>
        /// <param name="var"></param>
        private void VarOnEventTriggerV2(IVariable var)
        {
            double value = pubFun.IsDouble(var.Value.ToString(), -1);

            if (var.AlarmHHEnable)
            {
                BaseVariable baseVar = var as BaseVariable;
                if (value >= var.AlarmHH)
                {
                    String AlarmDesc = var.Name + "超高限报警";
                    uint alarmLevel = (uint)var.AlarmLevelHH; //报警级别
                    VariableTrigger trigger = new VariableTrigger("HH", (int)var.AlarmSource, AlarmDesc, alarmLevel, "", "");

                    AlarmMessage msg = new AlarmMessage(var, trigger);
                    if(var.AlarmHHdelay>0)
                       VarEventActionHandle_V2(msg, var,true);
                    else
                        VarEventActionHandle_V2(msg, var, false);
                }
            }

            if (var.AlarmHEnable)
            {
                if(value >= var.AlarmH )
                {
                    String AlarmDesc = var.Name + "高限报警";
                    uint alarmLevel = (uint)var.AlarmLevelH; //报警级别
                    VariableTrigger trigger = new VariableTrigger("H", (int)var.AlarmSource, AlarmDesc, alarmLevel, "", "");

                    AlarmMessage msg = new AlarmMessage(var, trigger);
                    if (var.AlarmHdelay > 0)
                        VarEventActionHandle_V2(msg, var, true);
                    else
                        VarEventActionHandle_V2(msg, var, false);

                }
                
            }

            if (var.AlarmLEnable)
            {
                if(value <= var.AlarmL)
                {
                    String AlarmDesc = var.Name + "低限报警";
                    uint alarmLevel = (uint)var.AlarmLevelL; //报警级别
                    VariableTrigger trigger = new VariableTrigger("L", (int)var.AlarmSource, AlarmDesc, alarmLevel, "", "");

                    AlarmMessage msg = new AlarmMessage(var, trigger);
                    if (var.AlarmLdelay > 0)
                        VarEventActionHandle_V2(msg, var, true);
                    else
                        VarEventActionHandle_V2(msg, var, false);
                }
            }

            if (var.AlarmLLEnable)
            {
                if(value <= var.AlarmLL)
                {
                    String AlarmDesc = var.Name + "超低限报警";
                    uint alarmLevel = (uint)var.AlarmLevelLL; //报警级别
                    VariableTrigger trigger = new VariableTrigger("LL", (int)var.AlarmSource, AlarmDesc, alarmLevel,"" , "");
                    AlarmMessage msg = new AlarmMessage(var, trigger);
                    if (var.AlarmLLdelay > 0)
                        VarEventActionHandle_V2(msg, var, true);
                    else
                        VarEventActionHandle_V2(msg, var, false);
                }
                
            }
            if (var.AlarmST0)
            {
                if (value ==0)
                {
                    String AlarmDesc = var.Name + "状态0报警";
                    uint alarmLevel = (uint)var.AlarmLevelST; //报警级别
                    VariableTrigger trigger = new VariableTrigger("ST0", (int)var.AlarmSource, AlarmDesc, alarmLevel, "", "");
                    AlarmMessage msg = new AlarmMessage(var, trigger);
                    VarEventActionHandle_V2(msg, var, false);
                }
            }
            if (var.AlarmST1)
            {
                if (value == 1)
                {
                    String AlarmDesc = var.Name + "状态1报警";
                    uint alarmLevel = (uint)var.AlarmLevelST; //报警级别
                    VariableTrigger trigger = new VariableTrigger("ST1", (int)var.AlarmSource, AlarmDesc, alarmLevel, "", "");
                    AlarmMessage msg = new AlarmMessage(var, trigger);
                    VarEventActionHandle_V2(msg, var, false);
                }
            }
            if (var.AlarmST2)
            {
                if (value == 2)
                {
                    String AlarmDesc = var.Name + "状态2报警";
                    uint alarmLevel = (uint)var.AlarmLevelST; //报警级别
                    VariableTrigger trigger = new VariableTrigger("ST2", (int)var.AlarmSource, AlarmDesc, alarmLevel, "", "");
                    AlarmMessage msg = new AlarmMessage(var, trigger);
                    VarEventActionHandle_V2(msg, var, false);
                }
            }
            if (var.AlarmST3)
            {
                if (value == 3)
                {
                    String AlarmDesc = var.Name + "状态3报警";
                    uint alarmLevel = (uint)var.AlarmLevelST; //报警级别
                    VariableTrigger trigger = new VariableTrigger("ST3", (int)var.AlarmSource, AlarmDesc, alarmLevel, "", "");
                    AlarmMessage msg = new AlarmMessage(var, trigger);
                    VarEventActionHandle_V2(msg, var, false);
                }
            }
        }

        #endregion

        #region 报警事件处理
        /*
         *   报警处理机制
         *   1、每次设备收到数据，更新内存变量内都做报警检查，无论数据是否变化；
         *   2、定义一个报警内存链表，报警检查结果如果存在报警则和报警内存链表比较
         *      比较变量名称、报警条件，时间，如果三者相同则不再加入报警链表，以及发送给客户端，
         *      如果变量名称、报警条件相同则为同一个报警，如果时间差ALARM_REPEAT_TIME内不再重发
         */
        //存在线程内执行 
        /// <summary>
        /// 报警事件处理
        /// </summary>
        /// <param name="newAlarm">报警事件消息</param>
        /// <param name="var">报警变量</param>
        private void VarEventActionHandle(AlarmMessage newAlarm, IVariable var)
        {
            try
            {
                //if (newAlarm.ExpressType == (int)EventTypeEnum.报警事件)
                if (newAlarm.EventType > 0) //2016,612 修改，除了普通事件外，其他事件都作为报警处理
                {

                            Almdb.InsertAlarm(newAlarm);
                       
                            string msg = "变量:" + newAlarm.AlarmVariable + newAlarm.AlarmDesc + "  " + newAlarm.AlarmWay;
                            AddOperationLog(Severity.警告.ToString(), StrConst.TITLE_SYS, "变量报警", msg);

                }
                //已经采用异步处理了
                Var_SMSLink(var, newAlarm);
                //处理事件联动 新事件后处理，在后面的设备驱动中需要采用异步处理
                //Var_VideoLink(var, newAlarm);
                //每次事件发生处理，在后面的设备驱动中需要采用异步处理
                Var_ActionLink(var, newAlarm);
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }
        }

        private void VarEventActionHandle_V2(AlarmMessage newAlarm, IVariable var,bool bDelay)
        {
            try
            {
                //if (newAlarm.ExpressType == (int)EventTypeEnum.报警事件)
                if (newAlarm.EventType > 0) //2016,612 修改，除了普通事件外，其他事件都作为报警处理
                {
                        bool bExist = DeviceManagement.SingletonInstance.AmsExist_V2(newAlarm.AlarmIOServer, newAlarm.AlarmChannel, newAlarm.AlarmController, newAlarm.AlarmVariableID, newAlarm.AlarmWay);
                        if (!bExist)
                        {
                            if (bDelay)
                                Almdb.InsertDelayAms(newAlarm);
                            else
                                Almdb.InsertAlarm(newAlarm);
                        }


                    // GHLogger.Log("当前位置:插入延时报警，时间：" + DateTime.Now.ToString() + "报警值：" + newAlarm.AlarmValue, LogCategory.Debug);
                    //DebugLogger.WriteLog("当前位置:插入延时报警，时间：" + DateTime.Now.ToString() + "报警值：" + newAlarm.AlarmValue);
                      string msg = "";
                     if (bDelay)
                       msg = "变量延时报警:" + newAlarm.AlarmVariable + newAlarm.AlarmDesc + "  " + newAlarm.AlarmWay;
                     else
                        msg = "变量报警:" + newAlarm.AlarmVariable + newAlarm.AlarmDesc + "  " + newAlarm.AlarmWay;

                    AddOperationLog(Severity.警告.ToString(), StrConst.TITLE_SYS, "变量报警", msg);
        

                }
                //已经采用异步处理了
                Var_SMSLink(var, newAlarm);
                //处理事件联动 新事件后处理，在后面的设备驱动中需要采用异步处理
                //Var_VideoLink(var, newAlarm);
                //每次事件发生处理，在后面的设备驱动中需要采用异步处理
                Var_ActionLink_V2(var, newAlarm);
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }
        }
        private void Var_ActionScript(IVariable var, string scriptText)
        {
            VarEventVBAEngine.ScriptText = scriptText;
            string[] scriptMethodNames = VarEventVBAEngine.ScriptMethodNames;
            if ((scriptMethodNames != null) && (scriptMethodNames.Length > 0))
            {
                try
                {
                    VarEventVBAEngine.ExecuteSub(scriptMethodNames[0]);
                }
                catch (Exception exception)
                {
                    string msg = "执行脚本 " + scriptMethodNames[0] + " 变量：" + var.Name + "错误:" + exception.Message;
                    AddOperationLog(StrConst.SERVERITY_MSG, StrConst.TITLE_SYS, "", msg);
                }
            }
        }
        private void Var_ActionLink_V2(IVariable var, AlarmMessage newAlarm)
        {
            try
            {
                if (var.Action1!="")
                {
                    string id = var.Action1.Split('|')[0];
                    string value = var.Action1.Split('|')[1];
                    string io=id.Split(':')[0];
                    string ch = id.Split(':')[1];
                    string con = id.Split(':')[2];
                    string varID = id.Split(':')[3];

                    if(ServerConfig.CloudClientID==io)
                    {
                        IChannel cha = Rtdb.GetChannelByID(io);
                        if (cha != null)
                        {
                            foreach (IController ctrl in cha.ConList)
                            {
                                if (ctrl.ID == con)
                                foreach (IVariable v in ctrl.VarList)
                                {
                                    if(v.ID==varID)
                                        {
                                            v.WriteValue(value);
                                        }
                                }
                            }

                        }

                    }
                    else
                    {
                        string upAction = $"down_write_{io.ToLower()}";

                        JObject js = new JObject();
                        js.Add(new JProperty("IOSvrKey",io));
                        js.Add(new JProperty("ChlKey", ch));
                        js.Add(new JProperty("CtrlKey",con ));
                        js.Add(new JProperty("VarKey", varID));
                        js.Add(new JProperty("Value", value));
                        js.Add(new JProperty("Level", 0));
                        js.Add(new JProperty("Area", ""));
                        long l = DeviceManagement.SingletonInstance.Publish(upAction, js.ToString());

                    }
                }

                if (var.Action2 != "")
                {
                    string id = var.Action2.Split('|')[0];
                    string value = var.Action2.Split('|')[1];
                    string io = id.Split(':')[0];
                    string ch = id.Split(':')[1];
                    string con = id.Split(':')[2];
                    string varID = id.Split(':')[3];

                    if (ServerConfig.CloudClientID == io)
                    {
                        IChannel cha = Rtdb.GetChannelByID(io);
                        if (cha != null)
                        {
                            foreach (IController ctrl in cha.ConList)
                            {
                                if (ctrl.ID == con)
                                    foreach (IVariable v in ctrl.VarList)
                                    {
                                        if (v.ID == varID)
                                        {
                                            v.WriteValue(value);
                                        }
                                    }
                            }

                        }

                    }else
                    {
                            string upAction = $"down_write_{io.ToLower()}";

                            JObject js = new JObject();
                            js.Add(new JProperty("IOSvrKey", io));
                            js.Add(new JProperty("ChlKey", ch));
                            js.Add(new JProperty("CtrlKey", con));
                            js.Add(new JProperty("VarKey", varID));
                            js.Add(new JProperty("Value", value));
                            js.Add(new JProperty("Level", 0));
                            js.Add(new JProperty("Area", ""));
                            long l = DeviceManagement.SingletonInstance.Publish(upAction, js.ToString());


                    }
                }
                // Write2Device(alarmAction.VarName, alarmAction.VarValue.ToString());
                //IVariable var = Rtdb.GetVariableByID(alarmAction.VarTarget.Split(':')[0], alarmAction.VarTarget.Split(':')[1], alarmAction.VarTarget.Split(':')[2]);
                //if (var != null)
                //    var.WriteValue(alarmAction.VarValue);
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }

        }
        private void Var_ActionLink(IVariable var, AlarmMessage newAlarm)
        {
            string way = newAlarm.AlarmWay;
            foreach (VariableAction act in var.ActionList)
            {
                if (act.Way == way)
                    var_OnAlarmAction(var, act);
            }
        }
        //private void Var_VideoLink(IVariable var, AlarmMessage newAlarm)
        //{
        //    string way = newAlarm.AlarmWay;
        //    foreach (VariableVideo vdo in var.VideoList)
        //    {

        //        if (vdo.Way == way)
        //            var_OnAlarmVideoLink(var, vdo);
        //    }

        //}
        private void Var_SMSLink(IVariable var, AlarmMessage newAlarm)
        {
            string way = newAlarm.AlarmWay;
            foreach (VariableSMS sms in var.SmsList)
            {
                if (sms.Way == way)
                    var_OnAlarmSms(var, sms);
            }
        }

        private void var_OnAlarmAction(IVariable sender, VariableAction alarmAction)
        {
            try
            {
                // Write2Device(alarmAction.VarName, alarmAction.VarValue.ToString());
                IVariable var = Rtdb.GetVariableByID(alarmAction.VarTarget.Split(':')[0], alarmAction.VarTarget.Split(':')[1], alarmAction.VarTarget.Split(':')[2]);
                if (var != null)
                    var.WriteValue(alarmAction.VarValue);
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }

        }
        ////报警视频联动
        //private void var_OnAlarmVideoLink(IVariable sender, VariableVideo videoLink)
        //{
        //    //切换视频
        //    if (videoLink.MonName.Trim() != "" && videoLink.CamID.Trim() != "")
        //    {
        //        ICamVariable cam = Rtdb.GetCamVariableByIDRun(videoLink.CamID);
        //        if (cam != null)
        //        {
        //            VideoCommandArgs args = new VideoCommandArgs();
        //            args.CamName = videoLink.CamID;
        //            args.MonName = videoLink.MonName;
        //            args.SubVideoOut = videoLink.SubVideoOut;
        //            if (cam is IDvrCamVariable)
        //                args.VideoIn = ((IDvrCamVariable)cam).MatrixInchannel;
        //            args.VideoCommand = PTZCmdCodeEnum.MAT_VIDEO_SW;
        //            //netService.dataAnalysis.C_VideoControl(args);
        //            string msg = ("报警联动视频： 矩阵：" + videoLink.MatrixName + "  监视器：" + videoLink.MonName
        //      + "摄像机：" + videoLink.CamID + " 预置位：" + videoLink.PreSetID.ToString());

        //            AddOperationLog(StrConst.SERVERITY_MSG, StrConst.TITLE_OPER, "", msg);
        //        }
        //    }
        //    else
        //    {
        //        this.AddOperationLog(Severity.信息.ToString(), "", "", "联动关系缺少摄像机名称或监控器名称视频联动没有被执行！");
        //    }
        //    //切换预置位
        //    if (videoLink.CamID.Trim() != "" && videoLink.PreSetID != 0)
        //    {
        //        // PTZPreset(videoLink.MatrixID, videoLink.CamID, videoLink.CamID,(int)PtzPresetEnum.调用);
        //        ICamVariable cam = Rtdb.GetCamVariableByIDRun(videoLink.CamID);
        //        if (cam != null)
        //        {
        //            VideoCommandArgs args = new VideoCommandArgs();
        //            args.CamName = videoLink.CamID;
        //            args.MonName = videoLink.MonName;
        //            args.SubVideoOut = videoLink.SubVideoOut;
        //            if (cam is IDvrCamVariable)
        //                args.VideoIn = ((IDvrCamVariable)cam).MatrixInchannel;
        //            args.PresetIndex = (uint)videoLink.PreSetID;
        //            args.VideoCommand = PTZCmdCodeEnum.GOTO_PRESET;
        //            //netService.dataAnalysis.C_VideoControl(args);
        //            string msg = ("报警联动视频： 矩阵：" + videoLink.MatrixName + "  监视器：" + videoLink.MonName
        //      + "摄像机：" + videoLink.CamID + " 预置位：" + videoLink.PreSetID.ToString());

        //            AddOperationLog(StrConst.SERVERITY_MSG, StrConst.TITLE_OPER, "", msg);
        //        }

        //    }
        //    else
        //    {
        //        //this.AddOperationLog(Severity.信息.ToString(), "", "", "缺少矩阵名称、摄像机名称、监控器名称视频联动没有被执行！");
        //    }
        //}

        private void var_OnAlarmSms(IVariable sender, VariableSMS msg)
        {
            if (msg.Type == 0) //短信
            {
                SmsMsg sms = new SmsMsg();
                sms.Phone = msg.Phone;
                sms.Msg = msg.Msg;
                Smsdb.IntoSmsQueue(sms);
                string s = ("报警联动短信：号码：" + msg.Phone + "  内容：" + msg.Msg);
                AddOperationLog(StrConst.SERVERITY_MSG, StrConst.TITLE_SYS, "", s);
            }
            else if (msg.Type == 1) //固话
            {    
                PSTNMsg p = new PSTNMsg();
                p.Phone = msg.Phone;
                p.Msg = msg.Msg;
                PSTNdb.IntoPSTNQueue(p);
                string s = ("报警电话拨号：号码：" + msg.Phone + "  语音文件名：" + msg.Msg);
                AddOperationLog(StrConst.SERVERITY_MSG, StrConst.TITLE_SYS, "", s);

            }
            else if (msg.Type == 2)//通知
            {
                pushMsgController.AddMessage(new PushMessage(msg.Phone, msg.Msg));
            }
            else if (msg.Type == 3)//邮件
            {
                mailController.AddMessage(new mailContent(msg.Phone, msg.Msg));
            }
        }

        #endregion

        #region 公式运算
        private object ExpressValue(string sExpress)
        {
            bool bok = express.Parse(sExpress);
            if (bok)
            {
                bool ok = false;
                object d = express.Evaluate(ref ok);
                if (ok)
                {
                    return d;
                }
            }
            return null;
        }
        #endregion

        #region 公式解析回调
        //变量传到这个回调函数里
        public bool express_OnAccidenceAnalysis(ref Operand opd)
        {
            foreach (BaseChannel chan in Rtdb.ChanList)
            {
                foreach (BaseController con in chan.ConList)
                {
                    foreach (BaseVariable var in con.VarList)
                    {  //变量名是value值
                        if (var.Name.Equals(opd.Value.ToString()))
                        {
                            opd.Type = OperandType.NUMBER;
                            opd.Value = var.Value.ToString();
                            return true;
                        }
                    }
                }
            }
            opd.Type = OperandType.NUMBER;
            opd.Value = 0;
            return false;
        }
        //函数传到这个回调函数里
        public static object express_OnCallBack(string funcName, object[] param, ref bool isOk)
        {
            isOk = true;
            string fun = funcName.ToUpper();
            if (fun.Equals("SIN"))
            {
                return Math.Sin(Convert.ToDouble(param[0]));
            }
            else if (fun.Equals("COS"))
            {
                return Math.Cos(Convert.ToDouble(param[0]));
            }
            else if (fun.Equals("LOG10"))
            {
                return Math.Log10(Convert.ToDouble(param[0]));
            }
            else if (fun.Equals("EXP"))
            {
                return Math.Exp(Convert.ToDouble(param[0]));
            }
            else if (fun.Equals("ASIN"))
            {
                return Math.Asin(Convert.ToDouble(param[0]));
            }
            else if (fun.Equals("ACOS"))
            {
                return Math.Acos(Convert.ToDouble(param[0]));
            }
            else if (fun.Equals("ABS"))
            {
                return Math.Abs(Convert.ToDecimal(param[0]));
            }
            else if (fun.Equals("ATAN"))
            {
                return Math.Atan(Convert.ToDouble(param[0]));
            }
            else if (fun.Equals("CEILING"))
            {
                return Math.Ceiling(Convert.ToDouble(param[0]));
            }
            else if (fun.Equals("COSH"))
            {
                return Math.Cosh(Convert.ToDouble(param[0]));
            }
            else if (fun.Equals("FLOOR"))
            {
                return Math.Floor(Convert.ToDouble(param[0]));
            }
            else if (fun.Equals("SINH"))
            {
                return Math.Sinh(Convert.ToDouble(param[0]));
            }
            else if (fun.Equals("SQRT"))
            {
                return Math.Sqrt(Convert.ToDouble(param[0]));
            }
            else if (fun.Equals("TAN"))
            {
                return Math.Tan(Convert.ToDouble(param[0]));
            }
            else if (fun.Equals("TANH"))
            {
                return Math.Tanh(Convert.ToDouble(param[0]));
            }
            else if (fun.Equals("TRUNCATE"))
            {
                return Math.Truncate(Convert.ToDouble(param[0]));
            }
            isOk = false;
            return 0;
        }
        #endregion

        #region 数值改变事件处理
        //private delegate void DelegateOnVariableChange(IVariable var);
        /// <summary>
        /// 变量值变化事件响应
        /// </summary>
        public void var_OnValueChange(IVariable sender)
        {
            variable_OnValueChange(sender); //线程内执行
            _CloudController.AddVarChangedList(sender);
        }
        /// <summary>
        /// 变量更新事件响应
        /// </summary>
        private void var_OnCounterChange(IVariable sender)
        {
            //variable_OnCounterChange(sender); //线程内执行

        }

        private void variable_OnCounterChange(IVariable var)
        {
            //服务器端显示更新
            if (FormIsShow && (ListViewDevice.Tag != null))
            {
                // object selectDevice = ((TreeNode)(ListViewDevice.Tag)).Tag;
                //if (selectDevice is BaseController)
                //{
                BaseController con = ((TreeNode)(ListViewDevice.Tag)).Tag as BaseController;
                if (con != null && con.Name == var.ControllerObject.Name)
                {
                    // UpdateVarView(var);

                }
                //}
            }
        }
        /// <summary>
        /// 变量值变化事件响应
        /// </summary>
        /// <param name="sender"></param>
        private void variable_OnValueChange(IVariable sender)
        {
            try
            {
                //Debug.WriteLine("server variable_OnValueChange " + sender.Name);
                IVariable var = sender;

                //报警条件是否满足检查与事件联动

                //兼容判断
                if (ServerConfig.CompatibleWithV1)
                {
                    VarOnEventTrigger(var);
                }
          
                 VarOnEventTriggerV2(var);
            


                //画面数据订阅分发 改为另起一个发送线程
                //netService.JugdgeIsClientSubscrible(var);

                //数值变化事件历史记录
                if (var.DataChangedRecorderEnable)
                {
                    //V2取消数据库
                    //if (ServerConfig.DataBaseEnable)
                    //{
                    //    dbAssistant.AddHisRecord(Guid.NewGuid().ToString("N"),
                    //                         var.Name, var.Value.ToString(),
                    //                         var.Description,
                    //                         var.DeviceLabel.ToString(),
                    //                         HistoryRecordTypeEnum.变化记录.ToString(),
                    //                         "", "1");
                    //}
                }
                //自动复位变量
                if (var.AutoReset && var.Value.ToString() != var.DefaultValue)
                {
                    autoResetController.AddVariable(var);
                }

            }
            catch (Exception e)
            {
                Logger.GetInstance().LogError(e.ToString());
               // Console.WriteLine(e.ToString());
            }

        }
        private delegate void UpdateVarViewDelegate(BaseVariable var);
        /// <summary>
        /// 线程安全更新点的显示
        /// </summary>
        /// <param name="var"></param>
        private void UpdateVarView(IVariable var)
        {
            if (FormIsShow)
            {
                if (ListViewDevice.InvokeRequired)
                {
                    ListViewDevice.BeginInvoke(new UpdateVarViewDelegate(UpdateVarView), new object[] { var });
                }
                else
                {

                    //Stopwatch watch = new Stopwatch();
                    //watch.Start();
                    if (ListViewDevice.CurrentCacheItemsSource.Count > var.VarListIndex)
                    {
                        //ListViewItem item = ListViewDevice.CurrentCacheItemsSource[var.VarListIndex];
                        ListViewItem item = null;
                        foreach (ListViewItem lv in ListViewDevice.CurrentCacheItemsSource)
                        {
                            //if (((IVariable)lv.Tag).Name == var.Name) dong 2017/6/13 --
                            if (((IVariable)lv.Tag).ID == var.ID) // dong 2017/7/3 ++
                            {
                                item = lv;
                                break;
                            }
                        }
                        if (item != null)
                        {

                            item.SubItems[3].Text = var.ValueType.ToString();
                            item.SubItems[4].Text = var.Value.ToString();
                            item.SubItems[6].Text = DateTime.Now.ToString();
                            item.SubItems[7].Text = ((COMM_QUALITY_STATUS)var.Quality).ToString();
                            item.SubItems[8].Text = var.Counter.ToString();

                            //已经改为定时器定时更新
                            //ListViewDevice.Invalidate(); //局部更新
                            //Debug.WriteLine(var.Name + "   " + var.Counter);
                        }
                        //watch.Stop();
                        //Debug.WriteLine("查找变量时间:" + watch.ElapsedMilliseconds / 1000f + "/" + ListViewDevice.CurrentCacheItemsSource.Count);
                    }
                }
            }
        }

        #endregion

        #region 数据服务
        /*
        //服务启动 事件
        void dataServer_OnActive(bool actived)
        {
            try
            {
                this.Invoke(new data_OnActiveCallback(data_OnActive), new object[] { actived });
            }
            catch { }
        }
        public delegate void data_OnActiveCallback(bool actived);
        public void data_OnActive(bool actived)
        {
            if (actived)
            {
                //btnStartServer.Enabled = false;
                //btnStopServer.Enabled = true;
                //timerTest.Enabled = false;
                //string msg = ("网络数据服务已经启动！端口号：" + netService.Port.ToString());
                //AddOperationLog(StrConst.SERVERITY_MSG, StrConst.TITLE_OPER, "", msg);

                SetRunningState();
            }
            else
            {
                //btnStartServer.Enabled  = true;
                //btnStopServer.Enabled = false;

                //string msg = ("网络数据服务已经启动出错！端口号：" + netService.Port.ToString());
                //AddOperationLog(StrConst.SERVERITY_MSG, StrConst.TITLE_OPER, "", msg);


            }
        }
        public delegate void WebServer_OnActiveCallback(bool active);
        void WebServer_OnActive(bool active)
        {
            if (active)
            {
                string msg = ("WEB数据服务已经启动！端口号：" + ServerConfig.webDataPort.ToString());
                AddOperationLog(StrConst.SERVERITY_MSG, StrConst.TITLE_SYS, "", msg);
            }
            else
            {
                string msg = ("WEB数据服务已经关闭！端口号：" + ServerConfig.webDataPort.ToString());
                AddOperationLog(StrConst.SERVERITY_MSG, StrConst.TITLE_SYS, "", msg);
            }
        }
        void myWebServer_OnActive(bool active)
        {
            this.Invoke(new WebServer_OnActiveCallback(WebServer_OnActive), new object[] { active });
        }
        //服务关闭 事件

        public void dataServer_OnDeactive()
        {
            try
            {
                string msg = ("网络数据服务已经停止！端口号：" + netService.Port.ToString());
                AddOperationLog(StrConst.SERVERITY_MSG, StrConst.TITLE_SYS, "", msg);
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }

        }
        //客户端连接 事件

        public void dataServer_OnConnect(DataClient dc)
        {
            try
            {
                IPAddress ip = ((System.Net.IPEndPoint)dc.Socket.RemoteEndPoint).Address;
                int port = ((System.Net.IPEndPoint)dc.Socket.RemoteEndPoint).Port;
                string msg = ("客户端连接成功！IP:" + ip + " 端口号：" + port.ToString());
                AddOperationLog(StrConst.SERVERITY_MSG, StrConst.TITLE_OPER, "", msg);
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }

        }
        //客户端断开连接 事件

        public void dataServer_OnDisconnect(DataClient dc)
        {
              
            try
            {
                string msg = "客户端连接断开！IP:" + dc.ClientIp+ " 端口号：" + dc.ClientPort.ToString();
                AddOperationLog(StrConst.SERVERITY_MSG, StrConst.TITLE_OPER, "", msg);
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }
     
        }
        //接收信息 事件

        public void S_OnAlarm(AlarmMessage alarm)
        {
            netService.Send2AllClient(alarm);

        }
        */
        #endregion

    }
}
