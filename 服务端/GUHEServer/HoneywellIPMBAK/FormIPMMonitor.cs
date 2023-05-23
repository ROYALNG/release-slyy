﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using IPModuleLib;

namespace HoneywellIPM
{
    public partial class FormIPMMonitor : Form
    {
        private string currentMac = "";
        private string currentHostName = "";
        private FormIPMOCX _formIPMOCX;
        public FormIPMOCX formIPMOCX
        {
            set 
            {
                _formIPMOCX = value;
                SubscribeEvents();
            }
            get { return _formIPMOCX; }
        }
        public string CurrentMac
        {
            set 
            { 
                 currentMac = value;
                 this.txtHostMac.Text=currentMac;
            }
        }
        public string CurrentHostName
        {
            set
            {
                currentHostName=value;
                this.txtHostName.Text=currentHostName;
            }
        }
        public FormIPMMonitor()
        {
            InitializeComponent();
        }
        public void SubscribeEvents()
        {
         
            this.formIPMOCX.OnArmReport+=new FormIPMOCX.ArmReport(cooMonitor_ArmReport);
            this.formIPMOCX.OnBLVerQueryResult+=new FormIPMOCX.BLVerQueryResult(cooMonitor_BLVerQueryResult);
            this.formIPMOCX.OnDeviceConnected+=new FormIPMOCX.DeviceConnected(cooMonitor_DeviceConnected);
            this.formIPMOCX.OnDeviceDisConnected+=new FormIPMOCX.DeviceDisConnected(cooMonitor_DeviceDisConnected);
            this.formIPMOCX.OnDuressReport+=new FormIPMOCX.DuressReport(cooMonitor_DuressReport);
            this.formIPMOCX.OnEnableKeypadResult+=new FormIPMOCX.EnableKeypadResult(cooMonitor_EnableKeypadResult);
            this.formIPMOCX.OnFaultRestore+=new FormIPMOCX.FaultRestore(cooMonitor_FaultRestore);
            this.formIPMOCX.OnLRRQueryResult+=new FormIPMOCX.LRRQueryResult(cooMonitor_LRRQueryResult);
            this.formIPMOCX.OnModifyModInfoResult+=new FormIPMOCX.ModifyModInfoResult(cooMonitor_ModifyModInfoResult);
            this.formIPMOCX.OnModifyPasswdResult+=new FormIPMOCX.ModifyPasswdResult(cooMonitor_ModifyPasswdResult);
            this.formIPMOCX.OnModifyPTResult+=new FormIPMOCX.ModifyPTResult(cooMonitor_ModifyPTResult);
            this.formIPMOCX.OnModInfoQueryResult+=new FormIPMOCX.ModInfoQueryResult(cooMonitor_ModInfoQueryResult);
            this.formIPMOCX.OnNewAlarm+=new FormIPMOCX.NewAlarm(cooMonitor_NewAlarm);
            this.formIPMOCX.OnNewDisplayMsg+=new FormIPMOCX.NewDisplayMsg(cooMonitor_NewDisplayMsg);
            this.formIPMOCX.OnNewTrouble +=new FormIPMOCX.NewTrouble(cooMonitor_NewTrouble);
            this.formIPMOCX.OnPanelConnected+=new FormIPMOCX.PanelConnected(cooMonitor_PanelConnected);
            this.formIPMOCX.OnPanelDisConnected+=new FormIPMOCX.PanelDisConnected(cooMonitor_PanelDisConnected);
            this.formIPMOCX.OnPanelStatus+=new FormIPMOCX.PanelStatus(cooMonitor_PanelStatus);
            this.formIPMOCX.OnPanelStatusEx+=new FormIPMOCX.PanelStatusEx(cooMonitor_PanelStatusEx);
            this.formIPMOCX.OnProgramReport+=new FormIPMOCX.ProgramReport(cooMonitor_ProgramReport);
            this.formIPMOCX.OnResetResult+=new FormIPMOCX.ResetResult(cooMonitor_ResetResult);
            this.formIPMOCX.OnRPSReport+=new FormIPMOCX.RPSReport(cooMonitor_RPSReport);
            this.formIPMOCX.OnSoftZoneAlarm+=new FormIPMOCX.SoftZoneAlarm(cooMonitor_SoftZoneAlarm);
            this.formIPMOCX.OnVistaCIDReport+=new FormIPMOCX.VistaCIDReport(cooMonitor_VistaCIDReport);
            this.formIPMOCX.OnVistaKeypadInfo+=new FormIPMOCX.VistaKeypadInfo(cooMonitor_VistaKeypadInfo);
            this.formIPMOCX.OnVistaKeyResponse+=new FormIPMOCX.VistaKeyResponse(cooMonitor_VistaKeyResponse);
            this.formIPMOCX.OnVistaKeyResponse+=new FormIPMOCX.VistaKeyResponse(cooMonitor_VistaKeyResponse);
            this.formIPMOCX.OnVistaPanelStatus+=new FormIPMOCX.VistaPanelStatus(cooMonitor_VistaPanelStatus);
            this.formIPMOCX.OnVistaProgramReport+=new FormIPMOCX.VistaProgramReport(cooMonitor_VistaProgramReport);

        }
    
        #region 所有事件处理
        //VISTA编程状态上报
        private void cooMonitor_VistaProgramReport(object sender, AxIPModuleLib._ICooMonitorEvents_VistaProgramReportEvent e)
        {
            if (e.strMac != currentMac) return;
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了VistaProgramReport事件");
            lboxMain.Items.Insert(0, "Mac地址:" + e.strMac + "进入编程状态:" + e.lState.ToString() + "实时上报:" + e.lPlayback.ToString());
        }
        //VISTA警情上报
        private void cooMonitor_VistaCIDReport(object sender, AxIPModuleLib._ICooMonitorEvents_VistaCIDReportEvent e)
        {
            if (e.strMac != currentMac) return;
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了VistaCIDReport事件");
            lboxMain.Items.Insert(0, "Mac地址:" + e.strMac +"CID码："+e.cID+ " 实时上报:" + e.lPlayback.ToString() + "账号:" + e.acct + "子系统编号:" + e.subSystemID + "防区号:" + e.strCode +"新事件："+e.isNewEvent.ToString());
        }
        //VISTA键盘信息显示上报
        private void cooMonitor_VistaKeypadInfo(object sender, AxIPModuleLib._ICooMonitorEvents_VistaKeypadInfoEvent e)
        {
            if (e.strMac != currentMac) return;
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了VistaKeypadInfo事件");
            lboxMain.Items.Insert(0, "键盘显示信息:" + e.strInfo + "Mac地址:" + e.strMac + "实时上报:" + e.lPlayback.ToString() + "主机是否处于编程状态:" + e.lState.ToString() + "是否具有光标:" + e.showCursor.ToString() + "光标位置:" + e.cursorPos.ToString());
        
        }
        //VISTA键盘使能结果
        private void cooMonitor_EnableKeypadResult(object sender, AxIPModuleLib._ICooMonitorEvents_EnableKeypadResultEvent e)
        {
            if (e.strMac != currentMac) return;
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了EnableKeypadResult事件");
            lboxMain.Items.Insert(0, "Mac地址:" + e.strMac + "VISTA键盘使能与否:" + e.lResult.ToString());
        }
        //VISTA LRR 地址查询结果
        private void cooMonitor_LRRQueryResult(object sender, AxIPModuleLib._ICooMonitorEvents_LRRQueryResultEvent e)
        {
            if (e.strMac != currentMac) return;
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了LRRQueryResult事件");
            lboxMain.Items.Insert(0, "Mac地址:" + e.strMac + "LRR地址为:" + e.lRR.ToString());
        }
        //修改主机子类型
        private void cooMonitor_ModifyPTResult(object sender, AxIPModuleLib._ICooMonitorEvents_ModifyPTResultEvent e)
        {
            if (e.strMac != currentMac) return;
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了ModifyPTResult事件");
            lboxMain.Items.Insert(0, "Mac地址：" + e.strMac + "修改主机子类型结果是否成功:" + e.lSuccess.ToString());
        }

        //无法进入编程模式报告
        private void cooMonitor_ProgramReport(object sender, AxIPModuleLib._ICooMonitorEvents_ProgramReportEvent e)
        {
            if (e.strMac != currentMac) return;
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了ProgramReport事件");
            lboxMain.Items.Insert(0, "Mac地址:" + e.strMac + "在" + e.strTime + "无法进入编程模式");
        }
        //挟持报告
        private void cooMonitor_DuressReport(object sender, AxIPModuleLib._ICooMonitorEvents_DuressReportEvent e)
        {
            if (e.strMac != currentMac) return;
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了DuressReport事件");
            lboxMain.Items.Insert(0, "Mac地址:" + e.strMac + "时间:" + e.strTime + "是否回放:" + e.lPlayback.ToString() + "是否布防:" + e.lArmed.ToString() + "布防用户号:" + e.lUser.ToString());
        }

        //布撤防报告
        private void cooMonitor_ArmReport(object sender, AxIPModuleLib._ICooMonitorEvents_ArmReportEvent e)
        {
            if (e.strMac != currentMac) return;
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了ArmReport事件");
            lboxMain.Items.Insert(0, "Mac地址:" + e.strMac + "是否布防:" + e.lArmed.ToString() + "布防用户号:" + e.lUser.ToString() + "时间:" + e.strTime);
        }
        //主机和IPM断开连接
        private void cooMonitor_PanelDisConnected(object sender, AxIPModuleLib._ICooMonitorEvents_PanelDisConnectedEvent e)
        {
            if (e.strMac != currentMac) return;
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了PanelDisConnected事件");
            lboxMain.Items.Insert(0, "Mac地址：" + e.strMac + "断开连接,时间:" + e.strTime);
        }
        //主机和IPM连接上
        private void cooMonitor_PanelConnected(object sender, AxIPModuleLib._ICooMonitorEvents_PanelConnectedEvent e)
        {
            if (e.strMac != currentMac) return;
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了PanelConnected事件");
            lboxMain.Items.Insert(0, "Mac地址：" + e.strMac + "已经连接,时间:" + e.strTime);
        }
        //确认用户按的特殊键已经被主机接收
        private void cooMonitor_VistaKeyResponse(object sender, AxIPModuleLib._ICooMonitorEvents_VistaKeyResponseEvent e)
        {
            if (e.strMac != currentMac) return;
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了VistaKeyResponse事件");
            lboxMain.Items.Insert(0, "Mac地址:" + e.strMac + "主机已经接收特殊键:" + e.key.ToString());
        }
        //上报修改VISTA主机状态
        private void cooMonitor_VistaPanelStatus(object sender, AxIPModuleLib._ICooMonitorEvents_VistaPanelStatusEvent e)
        {
            if (e.strMac != currentMac) return;
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了VistaPanelStatus事件");
            lboxMain.Items.Insert(0, "Mac地址:" + e.strMac + "键盘屏幕显示:" + e.strInfo + "光标显示:" + e.cursorPos.ToString() + "主机当前状态:" + e.lState.ToString() + "对应防区:" + e.zone.ToString());
            
        }

        //上报修改IPM设备信息结果
        private void cooMonitor_ModifyModInfoResult(object sender, AxIPModuleLib._ICooMonitorEvents_ModifyModInfoResultEvent e)
        {
            if (e.strMac != currentMac) return;
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了ModifyModInfoResult事件");
            lboxMain.Items.Insert(0, "Mac地址为:" + e.strMac + "的设备信息修改成功:" + e.lSuccess.ToString());
        }
        //复位命令执行结果
        private void cooMonitor_ResetResult(object sender, AxIPModuleLib._ICooMonitorEvents_ResetResultEvent e)
        {
            if (e.strMac != currentMac) return;
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了ResetResult事件");
            lboxMain.Items.Insert(0, "Mac地址：" + e.strMac + "复位成功:" + e.lSuccess.ToString());
        }
        //上报版本号查询结果
        private void cooMonitor_AppVerQueryResult(object sender, AxIPModuleLib._ICooMonitorEvents_AppVerQueryResultEvent e)
        {
            if (e.strMac != currentMac) return;
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了AppVerQueryResult事件");
            lboxMain.Items.Insert(0, "Mac地址:" + e.strMac + "查询成功:" + e.lSuccess.ToString() + "版本号为:" + e.strAppVer);
        }
        //上报编程数据
        private void cooMonitor_ProgramData(object sender, AxIPModuleLib._ICooMonitorEvents_ProgramDataEvent e)
        {
            if (e.strMac != currentMac) return;
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了ProgramData事件");
            lboxMain.Items.Insert(0, "Mac 地址:" + e.strMac + "上报编程数据:" + e.strData + "数据地址:" + e.strCmdNum);
        }
        //上报在键盘LCD上显示信息时触发此事件
        private void cooMonitor_PanelStatusEx(object sender, AxIPModuleLib._ICooMonitorEvents_PanelStatusExEvent e)
        {

            if (e.strMac != currentMac) return; 
            lboxMain.Items.Insert(0, "----------------------------------");
            //lboxMain.Items.Insert(0,"在 "+System .DateTime.Now.ToString()+" "+"触发了PanelStatusEx事件");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了PanelStatusEx事件");
        }
        //上报在键盘LCD上显示信息时触发此事件
        private void cooMonitor_PanelStatus(object sender, AxIPModuleLib._ICooMonitorEvents_PanelStatusEvent e)
        {
            if (e.strMac != currentMac) return;
            lboxMain.Items.Insert(0, "----------------------------------");
            //lboxMain.Items.Insert(0,"在 "+System .DateTime.Now.ToString()+" "+"触发了PanelStatus事件");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了PanelStatus事件");
        }
        //设备在键盘上显示信息
        private void cooMonitor_NewDisplayMsg(object sender, AxIPModuleLib._ICooMonitorEvents_NewDisplayMsgEvent e)
        {
            if (e.strMac != currentMac) return;
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了NewDisplayMsg事件");
            lboxMain.Items.Insert(0, "Mac地址:" + e.strMac + "显示信息" + e.strMsg);
        }
        //更改设备密码结果
        private void cooMonitor_ModifyPasswdResult(object sender, AxIPModuleLib._ICooMonitorEvents_ModifyPasswdResultEvent e)
        {
            if (e.strMac != currentMac) return;
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了ModifyPasswdResult事件");
            lboxMain.Items.Insert(0, "Mac地址:" + e.strMac + "修改密码成功:" + e.bSuccess.ToString());
        }

        //设备断开连接
        private void cooMonitor_DeviceDisConnected(object sender, AxIPModuleLib._ICooMonitorEvents_DeviceDisConnectedEvent e)
        {
            if (e.strMac != currentMac) return;
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了DeviceDisConnected事件");
            lboxMain.Items.Insert(0, "Mac地址:" + e.strMac + "断开连接");
        }
        //设备连接到服务器
        private void cooMonitor_DeviceConnected(object sender, AxIPModuleLib._ICooMonitorEvents_DeviceConnectedEvent e)
        {
            if (e.strMac != currentMac) return;
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了DeviceConnected事件");
            currentMac = e.strMac;//为了保险，这里也将赋值给MAC地址
            lboxMain.Items.Insert(0, "Mac地址:" + e.strMac + "已经连接上服务器");
        }

        private void cooMonitor_ModInfoQueryResult(object sender, AxIPModuleLib._ICooMonitorEvents_ModInfoQueryResultEvent e)
        {
            if (e.strMac != currentMac) return;
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了ModInfoQueryResult事件");
            lboxMain.Items.Insert(0, "Mac地址为:" + e.strMac + "的设备地址:" + e.lKeypad.ToString() + "设备ID：" + e.lModID.ToString());
        }

    
        //查询设备Boot Loader 版本号
        private void cooMonitor_BLVerQueryResult(object sender, AxIPModuleLib._ICooMonitorEvents_BLVerQueryResultEvent e)
        {
            if (e.strMac != currentMac) return;
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了BLVerQueryResult事件");
            lboxMain.Items.Insert(0, "Mac地址：" + e.strMac + "Boot Loader版本号:" + e.strBLVer);
        }
        //上报新的报警事件
        private void cooMonitor_NewAlarm(object sender, AxIPModuleLib._ICooMonitorEvents_NewAlarmEvent e)
        {

            if (e.strMac != currentMac) return; 
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了NewAlarm事件");
            lboxMain.Items.Insert(0, "Mac地址:" + e.strMac + "时间:" + e.strTime + "防区:" + e.lZone.ToString() + "是否报警:" + e.lState.ToString());
        }
        //上报新的Trouble事件
        private void cooMonitor_NewTrouble(object sender, AxIPModuleLib._ICooMonitorEvents_NewTroubleEvent e)
        {
            if (e.strMac != currentMac) return;
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了NewTrouble事件");
            lboxMain.Items.Insert(0, "Mac地址:" + e.strMac + "时间:" + e.strTime + "防区:" + e.lZone.ToString() + "有事件:" + e.lState.ToString());
        }
        //上报新的RPS布撤防事件
        private void cooMonitor_RPSReport(object sender, AxIPModuleLib._ICooMonitorEvents_RPSReportEvent e)
        {
            if (e.strMac != currentMac) return;
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了RPSReport事件");
            lboxMain.Items.Insert(0, "Mac地址:" + e.strMac + "时间:" + e.strTime + "用户编号:" + e.lUser.ToString() + "是否布防:" + e.lArmed.ToString());
        }
        //上报新的软防区报警事件
        private void cooMonitor_SoftZoneAlarm(object sender, AxIPModuleLib._ICooMonitorEvents_SoftZoneAlarmEvent e)
        {
            if (e.strMac != currentMac) return;
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了SoftZoneAlarm事件");
            lboxMain.Items.Insert(0, "Mac地址:" + e.strMac + "时间:" + e.strTime + "防区类型:" + e.zone.ToString() + "是否报警:" + e.lState.ToString());
        }
        //防区恢复
        private void cooMonitor_FaultRestore(object sender, AxIPModuleLib._ICooMonitorEvents_FaultRestoreEvent e)
        {
            if (e.strMac != currentMac) return;
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了FaultRestore事件");
            lboxMain.Items.Insert(0, "Mac地址:" + e.strMac + "时间:" + e.strTime + "防区号:" + e.lZone.ToString() + "恢复");
        }

        #endregion

        private void iP设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormIPMFinder frm = new FormIPMFinder();
            frm.ShowDialog();
        }

      
      
        #region 模拟按键处理

        private void btn1_Click(object sender, EventArgs e)
        {
            this.formIPMOCX.Sendkey(currentMac, KeyCode.KeyCode_1);
            //txtShow.Text += "1";
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            this.formIPMOCX.Sendkey(currentMac, KeyCode.KeyCode_2);
            //txtShow.Text += "2";
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            this.formIPMOCX.Sendkey(currentMac, KeyCode.KeyCode_3);
            //txtShow.Text += "3";
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            this.formIPMOCX.Sendkey(currentMac, KeyCode.KeyCode_4);
            //txtShow.Text += "4";
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            this.formIPMOCX.Sendkey(currentMac, KeyCode.KeyCode_5);
            //txtShow.Text += "5";
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            this.formIPMOCX.Sendkey(currentMac, KeyCode.KeyCode_6);
            //txtShow.Text += "6";
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            this.formIPMOCX.Sendkey(currentMac, KeyCode.KeyCode_7);
            //txtShow.Text += "7";
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            this.formIPMOCX.Sendkey(currentMac, KeyCode.KeyCode_8);
            //txtShow.Text += "8";
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            this.formIPMOCX.Sendkey(currentMac, KeyCode.KeyCode_9);
            //txtShow.Text += "9";
        }

        private void btnxing_Click(object sender, EventArgs e)
        {
            this.formIPMOCX.Sendkey(currentMac, KeyCode.KeyCode_X);
            ////txtShow.Text += "*";
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            this.formIPMOCX.Sendkey(currentMac, KeyCode.KeyCode_0);
            //txtShow.Text += "0";
        }

        private void btnjin_Click(object sender, EventArgs e)
        {
            this.formIPMOCX.Sendkey(currentMac, KeyCode.KeyCode_H);
            //txtShow.Text += "#";
        }
        

        #endregion

        private void btnProg_Click(object sender, EventArgs e)
        {
            if (txtProgAdd1.Text.Trim().Length > 0 && txtProgAdd2.Text.Trim().Length > 0)
            {
                this.formIPMOCX.SendProgramData(currentMac, txtProgAdd1.Text.Trim(), txtProgAdd2.Text.Trim());
            }
            else
            {
                MessageBox.Show("编程地址输入不正确！");
            }
        }

        private void btnKeySequence_Click(object sender, EventArgs e)
        {
            if (txtKeySequence.Text.Trim().Length > 0 && txtKeySequence.Text.Trim().Length <= 10)
            {
                this.formIPMOCX.SendKeySequence(currentMac, txtKeySequence.Text.Trim());
            }
            else
            {
                MessageBox.Show("指令输入不正确或超长！");

            }
        }

        private void btnModiKeyPanel_Click(object sender, EventArgs e)
        {
            if (txtModuleID.Text.Trim().Length > 0 && txtKeyPanelID.Text.Trim().Length > 0)
            {
                this.formIPMOCX.ModifyModInfo(currentMac, int.Parse(txtModuleID.Text.Trim()), int.Parse(txtKeyPanelID.Text.Trim()));
            }
            else
            {
                MessageBox.Show("指令输入不正确！");
            }
        }

        private void btnModiPass_Click(object sender, EventArgs e)
        {
            FormModifyPassword frm = new FormModifyPassword();
            if (frm.ShowDialog()== DialogResult.OK)
            {
                this.formIPMOCX.ModifyPasswd(currentMac, frm.OldPass, frm.NewPass);

                
            }
        }

        private void btnEnableKeyOpen_Click(object sender, EventArgs e)
        {
            this.formIPMOCX.EnableKeypad(currentMac, 1);
        }

        private void btnEnableKeyClose_Click(object sender, EventArgs e)
        {
            this.formIPMOCX.EnableKeypad(currentMac, 0);
        }

        private void btnExitProg_Click(object sender, EventArgs e)
        {
            this.formIPMOCX.ExitProgram(currentMac);
        }

    

    



     
    }
}
