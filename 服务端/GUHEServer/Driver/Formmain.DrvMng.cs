using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.IO.Ports;
using GHIBMS.Common;
using System.Collections;
using System.Threading;
using GHIBMS.Interface;
using System.Reflection;


namespace GHIBMS.Server
{
    public partial class FormMain
    {
        public CloudController _CloudController = new CloudController();
       

      
        #region 驱动启停

        public void ConnectedDrive()
        {
            foreach (BaseChannel chan in Rtdb.ChanList)
            {
                try { 
                //订阅通讯事件
                chan.OnCommMsg+=new CommMsgDelegate(ShowCommMsg);
                //try
                //{
                    if (chan.Enable)
                    {
                        try
                        {
                            frmWait.SetText("正在启动通讯通道"+chan.Name+".....");
                            chan.Start();
                        }
                        catch(Exception ex)
                        {
                            AddOperationLog(Severity.错误.ToString(), "通讯", chan.Name, "通讯通道启动错误！");
                            Logger.GetInstance().LogError(ex.ToString());
                        }
                    }
                }
                catch
                {
                    AddOperationLog(Severity.错误.ToString(), "通讯", chan.Name, "通讯通道启动错误！");

                }
            }
             try
             {
                    frmWait.SetText("正在登录云服务器......");
                   _CloudController.OnCommMsg += new CommMsgDelegate(ShowCommMsg);
                   _CloudController.Start();
              }
             catch
             {
                   AddOperationLog(Severity.错误.ToString(), "通讯", "", "云通讯启动错误！");

             }
       
        }

        public void DisConnectedDrive()
        {
            _CloudController.OnCommMsg -= new CommMsgDelegate(ShowCommMsg);
            _CloudController.Stop();
            foreach (BaseChannel chan in Rtdb.ChanList)
            {
                try
                {
                    chan.RunTime = 0;
                    if (chan.Enable)
                    {
                        Logger.GetInstance().LogMsg("开始停止驱动：" + chan.Name);
                        chan.Stop();
                        Logger.GetInstance().LogMsg("完成停止驱动：" + chan.Name);
                    }
                }
                catch { }
                chan.OnCommMsg -= new CommMsgDelegate(ShowCommMsg);
            }

        }
        #endregion

        #region 通信信息处理




        /// <summary>
        /// 通讯插件上报的通讯信息
        /// </summary>
        /// <param name="sender">信息发生的对象</param>
        /// <param name="severity">紧急程序</param>
        /// <param name="CommunicationEvent">信息类型</param>
        /// <param name="wParamm">参数1</param>
        /// <param name="lParamm">参数2</param>
        void ShowCommMsg(object sender, Severity severity, CommunicationEvent commMsgType, string wParamm, string lParamm)
        {
            try
            {
                //通讯
                if (commMsgType == CommunicationEvent.COMM_ALARM)
                {
                    //2017年2月24日新增，设备驱动中可直接发送报警到云端
                    if (wParamm == StrConst.ALARM_EVENT_DEV)
                    {
                        AlarmMessage newAlarm = GHCore.Serialization.JSONFormatter.Deserialize<AlarmMessage>(lParamm);
                        Almdb.InsertAlarm(newAlarm);
                    }
                    else
                    {
                        AlarmMessage newAlarm = new AlarmMessage();
                        newAlarm.AlarmVariable = "";
                        newAlarm.DateStamp = DateTime.Now;
                        newAlarm.AlarmWay = wParamm;
                        newAlarm.AlarmDesc = lParamm;
                        newAlarm.AlarmValue = "1";

                        //S_OnAlarm(newAlarm);
                        AddOperationLog(severity.ToString(), "通讯报警", wParamm, lParamm);
                    }

                }
                //发送信息到客户端
                else if (commMsgType == CommunicationEvent.COMM_CLIENT)
                {
                    //netService.SendEventMsg(wParamm, "通讯事件", lParamm);
                    //AddOperationLog(severity.ToString(), "通讯", wParamm, lParamm);
                }
                else if (commMsgType == CommunicationEvent.COMM_INFO)
                {
                    if (lParamm != "")
                        AddOperationLog(severity.ToString(), "通讯信息", wParamm, lParamm);
                }
                else if (sender is IChannel)
                {
                    IChannel ch = sender as IChannel;
                    switch (commMsgType)
                    {
                        case CommunicationEvent.COMM_ONLINE:
                            UpdateTree(wParamm, NodeStateEnum.OnLine);
                            if (lParamm != "")
                                AddOperationLog(severity.ToString(), "通讯事件", "系统用户", lParamm);
                            break;
                        case CommunicationEvent.COMM_OFFLINE:
                            UpdateTree(wParamm, NodeStateEnum.OffLine);
                            if (lParamm != "")
                                AddOperationLog(severity.ToString(), "通讯事件", "系统用户", lParamm);
                            break;
                        case CommunicationEvent.COMM_ENABLE:
                            UpdateTree(wParamm, NodeStateEnum.Enable);
                            if (lParamm != "")
                                AddOperationLog(severity.ToString(), "通讯事件", "系统用户", lParamm);
                            break;
                        case CommunicationEvent.COMM_DISABLE:
                            UpdateTree(wParamm, NodeStateEnum.Disable);
                            if (lParamm != "")
                                AddOperationLog(severity.ToString(), "通讯事件", "系统用户", lParamm);
                            break;
                        default:
                            if (lParamm != "")
                                AddOperationLog(severity.ToString(), "通讯事件", "系统用户", lParamm);
                            break;
                    }
                }
                else
                {
                    if (lParamm != "")
                        AddOperationLog(severity.ToString(), "通讯", wParamm, lParamm);
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }
          
        }

        #endregion


        #region 通用方法
 
        /// <summary>
        /// 更新设备树图标
        /// </summary>
        /// <param name="nodetext">设备名称</param>
        /// <param name="NodeStatus">状态1：离线 0：在线</param>
        /// 
        public delegate void UpdateTreedelegate(string nodetext, NodeStateEnum NodeStatus);
        public void UpdateTree(string nodetext, NodeStateEnum NodeStatus)
        {
            if (trvDevice.InvokeRequired)
            {
                trvDevice.BeginInvoke(new UpdateTreedelegate(UpdateTree), new object[] { nodetext, NodeStatus });
            }
            else
            {
                try
                {
                    TreeNode node = TreeviewSearch.FindNodeByText(trvDevice, nodetext);
                    if (node != null)
                    {
                       object o= node.Tag;
                        if (o is BaseChannel)
                        {
                                BaseChannel chan=node.Tag as BaseChannel;
                                if (chan.Enable)
                                {
                                    if (NodeStatus == NodeStateEnum.OffLine)        //通道离线
                                    {
                                        node.ImageIndex = DevTreeImage.ChannelOffLine;
                                        node.SelectedImageIndex = DevTreeImage.ChannelOffLine;
                                        chan.Active = false;
                                    }
                                    else if (NodeStatus == NodeStateEnum.OnLine)   //通道在线
                                    {
                                        node.ImageIndex = DevTreeImage.ChannelNormal;
                                        node.SelectedImageIndex = DevTreeImage.ChannelNormal;
                                        chan.Active=true;
                                    }
                                    else if (NodeStatus == NodeStateEnum.Enable)   //通道启用
                                    {
                                        node.ImageIndex = DevTreeImage.ChannelNormal;
                                        node.SelectedImageIndex = DevTreeImage.ChannelNormal;
                                        chan.Active = true;
                                    }
                                    else if (NodeStatus == NodeStateEnum.Disable)   //通道禁用
                                    {
                                        node.ImageIndex = DevTreeImage.ChannelDisable;
                                        node.SelectedImageIndex = DevTreeImage.ChannelDisable;
                                        chan.Active = false;
                                    }
                                }
                        }else if (o is BaseController)
                        {
      
                                BaseController con = node.Tag as BaseController;
                                if (con.Enable)
                                {
                                    if (NodeStatus == NodeStateEnum.OffLine)            //控制器离线
                                    {
                                        node.ImageIndex = DevTreeImage.ControllOffLine;
                                        node.SelectedImageIndex = DevTreeImage.ControllOffLine;
                                        con.Active = false;
                                       
                                    }
                                    else if (NodeStatus == NodeStateEnum.OnLine)        //控制器上线
                                    {
                                        node.ImageIndex = DevTreeImage.ControllNormal;
                                        node.SelectedImageIndex = DevTreeImage.ControllNormal;
                                        con.Active = true;

                                    }
                                    else if (NodeStatus == NodeStateEnum.Enable)   //控制器启用
                                    {
                                        node.ImageIndex = DevTreeImage.ControllNormal;
                                        node.SelectedImageIndex = DevTreeImage.ControllNormal;
                                        con.Active = true;
                                    }
                                    else if (NodeStatus == NodeStateEnum.Disable)   //控制器禁用
                                    {
                                        node.ImageIndex = DevTreeImage.ControllDisable;
                                        node.SelectedImageIndex = DevTreeImage.ControllDisable;
                                        con.Active = false;
                                    }
                                }
                        }else if (o is BaseVariable)
                        {
                                BaseVariable var = node.Tag as BaseVariable;
                                if (var.Enable)
                                {
                                    if (NodeStatus == NodeStateEnum.OffLine)            //变量离线
                                    {
                                        node.ImageIndex = DevTreeImage.VariableOffLine;
                                        node.SelectedImageIndex = DevTreeImage.VariableOffLine;
                                        var.Active = false;

                                    }
                                    else if (NodeStatus == NodeStateEnum.OnLine)        //变量上线
                                    {
                                        node.ImageIndex = DevTreeImage.VariableNormal;
                                        node.SelectedImageIndex = DevTreeImage.VariableNormal;
                                        var.Active = true;

                                    }
                                    else if (NodeStatus == NodeStateEnum.Enable)   //变量启用
                                    {
                                        node.ImageIndex = DevTreeImage.VariableNormal;
                                        node.SelectedImageIndex = DevTreeImage.VariableNormal;
                                        var.Active = true;
                                    }
                                    else if (NodeStatus == NodeStateEnum.Disable)   //变量禁用
                                    {
                                        node.ImageIndex = DevTreeImage.VariableDisable;
                                        node.SelectedImageIndex = DevTreeImage.VariableDisable;
                                        var.Active = false;
                                    }
                                }
                             
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.GetInstance().LogError(ex.ToString());
                }

            }
            
        }
        #endregion

      
    }
}
