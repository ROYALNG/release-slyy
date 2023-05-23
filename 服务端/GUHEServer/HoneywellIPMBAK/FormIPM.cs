using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IPModuleLib;

namespace HoneywellIPM
{
    public partial class FormIPM : Form
    {
        private IPModuleLib.IPMDevice device = new IPMDevice();
        
        private string currentMac = "";
        public FormIPM()
        {
            InitializeComponent();
        }
        public delegate void DeviceConnected(string strMac);
        public delegate void DeviceDisconnected(string strMac);

        #region 所有事件处理
        //VISTA编程状态上报
        private void cooMonitor_VistaProgramReport(object sender, AxIPModuleLib._ICooMonitorEvents_VistaProgramReportEvent e)
        {
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了VistaProgramReport事件");
            lboxMain.Items.Insert(0, "Mac地址:" + e.strMac + "进入编程状态:" + e.lState.ToString() + "实时上报:" + e.lPlayback.ToString());
        }
        //VISTA警情上报
        private void cooMonitor_VistaCIDReport(object sender, AxIPModuleLib._ICooMonitorEvents_VistaCIDReportEvent e)
        {
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了VistaCIDReport事件");
            lboxMain.Items.Insert(0, "Mac地址:" + e.strMac + "实时上报:" + e.lPlayback.ToString() + "账号:" + e.acct + "子系统编号:" + e.subSystemID + "防区号:" + e.strCode);
        }
        //VISTA键盘信息显示上报
        private void cooMonitor_VistaKeypadInfo(object sender, AxIPModuleLib._ICooMonitorEvents_VistaKeypadInfoEvent e)
        {
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了VistaKeypadInfo事件");
            lboxMain.Items.Insert(0, "键盘显示信息:" + e.strInfo + "Mac地址:" + e.strMac + "实时上报:" + e.lPlayback.ToString() + "主机是否处于编程状态:" + e.lState.ToString() + "是否具有光标:" + e.showCursor.ToString() + "光标位置:" + e.cursorPos.ToString());
        }
        //VISTA键盘使能结果
        private void cooMonitor_EnableKeypadResult(object sender, AxIPModuleLib._ICooMonitorEvents_EnableKeypadResultEvent e)
        {
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了EnableKeypadResult事件");
            lboxMain.Items.Insert(0, "Mac地址:" + e.strMac + "VISTA键盘使能与否:" + e.lResult.ToString());
        }
        //VISTA LRR 地址查询结果
        private void cooMonitor_LRRQueryResult(object sender, AxIPModuleLib._ICooMonitorEvents_LRRQueryResultEvent e)
        {
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了LRRQueryResult事件");
            lboxMain.Items.Insert(0, "Mac地址:" + e.strMac + "LRR地址为:" + e.lRR.ToString());
        }
        //修改主机子类型
        private void cooMonitor_ModifyPTResult(object sender, AxIPModuleLib._ICooMonitorEvents_ModifyPTResultEvent e)
        {
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了ModifyPTResult事件");
            lboxMain.Items.Insert(0, "Mac地址：" + e.strMac + "修改主机子类型结果是否成功:" + e.lSuccess.ToString());
        }

        //无法进入编程模式报告
        private void cooMonitor_ProgramReport(object sender, AxIPModuleLib._ICooMonitorEvents_ProgramReportEvent e)
        {
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了ProgramReport事件");
            lboxMain.Items.Insert(0, "Mac地址:" + e.strMac + "在" + e.strTime + "无法进入编程模式");
        }
        //挟持报告
        private void cooMonitor_DuressReport(object sender, AxIPModuleLib._ICooMonitorEvents_DuressReportEvent e)
        {
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了DuressReport事件");
            lboxMain.Items.Insert(0, "Mac地址:" + e.strMac + "时间:" + e.strTime + "是否回放:" + e.lPlayback.ToString() + "是否布防:" + e.lArmed.ToString() + "布防用户号:" + e.lUser.ToString());
        }

        //布撤防报告
        private void cooMonitor_ArmReport(object sender, AxIPModuleLib._ICooMonitorEvents_ArmReportEvent e)
        {
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了ArmReport事件");
            lboxMain.Items.Insert(0, "Mac地址:" + e.strMac + "是否布防:" + e.lArmed.ToString() + "布防用户号:" + e.lUser.ToString() + "时间:" + e.strTime);
        }
        //主机和IPM断开连接
        private void cooMonitor_PanelDisConnected(object sender, AxIPModuleLib._ICooMonitorEvents_PanelDisConnectedEvent e)
        {
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了PanelDisConnected事件");
            lboxMain.Items.Insert(0, "Mac地址：" + e.strMac + "断开连接,时间:" + e.strTime);
        }
        //主机和IPM连接上
        private void cooMonitor_PanelConnected(object sender, AxIPModuleLib._ICooMonitorEvents_PanelConnectedEvent e)
        {
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了PanelConnected事件");
            lboxMain.Items.Insert(0, "Mac地址：" + e.strMac + "已经连接,时间:" + e.strTime);
        }
        //确认用户按的特殊键已经被主机接收
        private void cooMonitor_VistaKeyResponse(object sender, AxIPModuleLib._ICooMonitorEvents_VistaKeyResponseEvent e)
        {
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了VistaKeyResponse事件");
            lboxMain.Items.Insert(0, "Mac地址:" + e.strMac + "主机已经接收特殊键:" + e.key.ToString());
        }
        //上报修改VISTA主机状态
        private void cooMonitor_VistaPanelStatus(object sender, AxIPModuleLib._ICooMonitorEvents_VistaPanelStatusEvent e)
        {
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了VistaPanelStatus事件");
            lboxMain.Items.Insert(0, "Mac地址:" + e.strMac + "键盘屏幕显示:" + e.strInfo + "光标显示:" + e.cursorPos.ToString() + "主机当前状态:" + e.lState.ToString() + "对应防区:" + e.zone.ToString());
        }

        //上报修改IPM设备信息结果
        private void cooMonitor_ModifyModInfoResult(object sender, AxIPModuleLib._ICooMonitorEvents_ModifyModInfoResultEvent e)
        {
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了ModifyModInfoResult事件");
            lboxMain.Items.Insert(0, "Mac地址为:" + e.strMac + "的设备信息修改成功:" + e.lSuccess.ToString());
        }
        //复位命令执行结果
        private void cooMonitor_ResetResult(object sender, AxIPModuleLib._ICooMonitorEvents_ResetResultEvent e)
        {
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了ResetResult事件");
            lboxMain.Items.Insert(0, "Mac地址：" + e.strMac + "复位成功:" + e.lSuccess.ToString());
        }
        //上报版本号查询结果
        private void cooMonitor_AppVerQueryResult(object sender, AxIPModuleLib._ICooMonitorEvents_AppVerQueryResultEvent e)
        {
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了AppVerQueryResult事件");
            lboxMain.Items.Insert(0, "Mac地址:" + e.strMac + "查询成功:" + e.lSuccess.ToString() + "版本号为:" + e.strAppVer);
        }
        //上报编程数据
        private void cooMonitor_ProgramData(object sender, AxIPModuleLib._ICooMonitorEvents_ProgramDataEvent e)
        {
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了ProgramData事件");
            lboxMain.Items.Insert(0, "Mac 地址:" + e.strMac + "上报编程数据:" + e.strData + "数据地址:" + e.strCmdNum);
        }
        //上报在键盘LCD上显示信息时触发此事件
        private void cooMonitor_PanelStatusEx(object sender, AxIPModuleLib._ICooMonitorEvents_PanelStatusExEvent e)
        {
            lboxMain.Items.Insert(0, "----------------------------------");
            //lboxMain.Items.Insert(0,"在 "+System .DateTime.Now.ToString()+" "+"触发了PanelStatusEx事件");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了PanelStatusEx事件");
        }
        //上报在键盘LCD上显示信息时触发此事件
        private void cooMonitor_PanelStatus(object sender, AxIPModuleLib._ICooMonitorEvents_PanelStatusEvent e)
        {
            lboxMain.Items.Insert(0, "----------------------------------");
            //lboxMain.Items.Insert(0,"在 "+System .DateTime.Now.ToString()+" "+"触发了PanelStatus事件");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了PanelStatus事件");
        }
        //设备在键盘上显示信息
        private void cooMonitor_NewDisplayMsg(object sender, AxIPModuleLib._ICooMonitorEvents_NewDisplayMsgEvent e)
        {
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了NewDisplayMsg事件");
            lboxMain.Items.Insert(0, "Mac地址:" + e.strMac + "显示信息" + e.strMsg);
        }
        //更改设备密码结果
        private void cooMonitor_ModifyPasswdResult(object sender, AxIPModuleLib._ICooMonitorEvents_ModifyPasswdResultEvent e)
        {
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了ModifyPasswdResult事件");
            lboxMain.Items.Insert(0, "Mac地址:" + e.strMac + "修改密码成功:" + e.bSuccess.ToString());
        }

        //设备断开连接
        private void cooMonitor_DeviceDisConnected(object sender, AxIPModuleLib._ICooMonitorEvents_DeviceDisConnectedEvent e)
        {
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了DeviceDisConnected事件");
            lboxMain.Items.Insert(0, "Mac地址:" + e.strMac + "断开连接");
        }
        //设备连接到服务器
        private void cooMonitor_DeviceConnected(object sender, AxIPModuleLib._ICooMonitorEvents_DeviceConnectedEvent e)
        {
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了DeviceConnected事件");
            currentMac = e.strMac;//为了保险，这里也将赋值给MAC地址
            lboxMain.Items.Insert(0, "Mac地址:" + e.strMac + "已经连接上服务器");
        }

        private void cooMonitor_ModInfoQueryResult(object sender, AxIPModuleLib._ICooMonitorEvents_ModInfoQueryResultEvent e)
        {
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了ModInfoQueryResult事件");
            lboxMain.Items.Insert(0, "Mac地址为:" + e.strMac + "的设备地址:" + e.lKeypad.ToString() + "设备ID：" + e.lModID.ToString());
        }

        //上报查找到的设备
        private void netFinder_DeviceReport(object sender, AxIPModuleLib._INetFinderEvents_DeviceReportEvent e)
        {
            lboxMain.Items.Insert(0, "----------------------------------");
            currentMac = e.device.MacAddr;//赋值会执行，但是使其他控制作出反应有的时候不会执行
            //device = new IPMDevice();
            device = e.device; //赋值给自定义结构体，用来存储设备的完整信息
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了DeviceReport事件");

            //lboxMain.Items.Insert(0,"设备的IP地址：" + e.device.IP);
            //lboxMain.Items.Insert(0,"设备的网关地址：" + e.device.Gateway);
            //lboxMain.Items.Insert(0,"设备的子网掩码：" + e.device.SubnetMask);
            //lboxMain.Items.Insert(0,"设备的Firmware版本号：" + e.device.AppVersion);
            //lboxMain.Items.Insert(0,"设备的Boot Loader版本号：" + e.device.BootloaderVersion);
            //lboxMain.Items.Insert(0,"设备模块号：" + e.device.ModuleID.ToString());
            //lboxMain.Items.Insert(0,"设备MAC地址：" + e.device.MacAddr);
            //lboxMain.Items.Insert(0,"设备键盘地址：" + e.device.KeypadAddr.ToString());
            //lboxMain.Items.Insert(0,"服务器IP地址：" + e.device.ServerIP);
            //lboxMain.Items.Insert(0,"服务器端口号：" + e.device.ServerPort.ToString());
            //lboxMain.Items.Insert(0,"设备控制的主机类型：" + e.device.ControlType.ToString());
            //lboxMain.Items.Insert(0,"主机的子类型：" + e.device.SubControlType.ToString());
            //lboxMain.Items.Insert(0,"Vista主机LRR地址：" + e.device.LRRAddr.ToString());
            this.ShowDeviceInfo();//显示设备信息
            lboxMain.Items.Insert(0, "设备的IP地址：" + device.IP);
            lboxMain.Items.Insert(0, "设备的网关地址：" + device.Gateway);
            lboxMain.Items.Insert(0, "设备的子网掩码：" + device.SubnetMask);
            lboxMain.Items.Insert(0, "设备的Firmware版本号：" + device.AppVersion);
            lboxMain.Items.Insert(0, "设备的Boot Loader版本号：" + device.BootloaderVersion);
            lboxMain.Items.Insert(0, "设备模块号：" + device.ModuleID.ToString());
            lboxMain.Items.Insert(0, "设备MAC地址：" + device.MacAddr);
            lboxMain.Items.Insert(0, "设备键盘地址：" + device.KeypadAddr.ToString());
            lboxMain.Items.Insert(0, "服务器IP地址：" + device.ServerIP);
            lboxMain.Items.Insert(0, "服务器端口号：" + device.ServerPort.ToString());
            lboxMain.Items.Insert(0, "设备控制的主机类型：" + device.ControlType.ToString());
            lboxMain.Items.Insert(0, "主机的子类型：" + device.SubControlType.ToString());
            lboxMain.Items.Insert(0, "Vista主机LRR地址：" + device.LRRAddr.ToString());

            lboxMain.Items.Insert(0, "查询设备信息完成");
        }

        //上报配置结果
        private void netFinder_ConfigResult(object sender, AxIPModuleLib._INetFinderEvents_ConfigResultEvent e)
        {
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了ConfigResult事件");
            lboxMain.Items.Insert(0, "配置结果:" + e.result.ToString());
        }
        //查询设备Boot Loader 版本号
        private void cooMonitor_BLVerQueryResult(object sender, AxIPModuleLib._ICooMonitorEvents_BLVerQueryResultEvent e)
        {
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了BLVerQueryResult事件");
            lboxMain.Items.Insert(0, "Mac地址：" + e.strMac + "Boot Loader版本号:" + e.strBLVer);
        }
        //上报新的报警事件
        private void cooMonitor_NewAlarm(object sender, AxIPModuleLib._ICooMonitorEvents_NewAlarmEvent e)
        {
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了NewAlarm事件");
            lboxMain.Items.Insert(0, "Mac地址:" + e.strMac + "时间:" + e.strTime + "防区:" + e.lZone.ToString() + "是否报警:" + e.lState.ToString());
        }
        //上报新的Trouble事件
        private void cooMonitor_NewTrouble(object sender, AxIPModuleLib._ICooMonitorEvents_NewTroubleEvent e)
        {
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了NewTrouble事件");
            lboxMain.Items.Insert(0, "Mac地址:" + e.strMac + "时间:" + e.strTime + "防区:" + e.lZone.ToString() + "有事件:" + e.lState.ToString());
        }
        //上报新的RPS布撤防事件
        private void cooMonitor_RPSReport(object sender, AxIPModuleLib._ICooMonitorEvents_RPSReportEvent e)
        {
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了RPSReport事件");
            lboxMain.Items.Insert(0, "Mac地址:" + e.strMac + "时间:" + e.strTime + "用户编号:" + e.lUser.ToString() + "是否布防:" + e.lArmed.ToString());
        }
        //上报新的软防区报警事件
        private void cooMonitor_SoftZoneAlarm(object sender, AxIPModuleLib._ICooMonitorEvents_SoftZoneAlarmEvent e)
        {
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了SoftZoneAlarm事件");
            lboxMain.Items.Insert(0, "Mac地址:" + e.strMac + "时间:" + e.strTime + "防区类型:" + e.zone.ToString() + "是否报警:" + e.lState.ToString());
        }
        //防区恢复
        private void cooMonitor_FaultRestore(object sender, AxIPModuleLib._ICooMonitorEvents_FaultRestoreEvent e)
        {
            lboxMain.Items.Insert(0, "----------------------------------");
            lboxMain.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了FaultRestore事件");
            lboxMain.Items.Insert(0, "Mac地址:" + e.strMac + "时间:" + e.strTime + "防区号:" + e.lZone.ToString() + "恢复");
        }

        #endregion

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (txtPort.Text.Trim().Length > 0)
            {

                netFinder.Port = int.Parse(txtPort.Text.Trim());
                netFinder.Startup();
                btnStart.Enabled = false;
                btnStop.Enabled = true;
                btnBroadSearch.Enabled = true;
                btnRangeSearch.Enabled = true;
                btnSetIP.Enabled = true;
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            netFinder.Shutdown();
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            btnBroadSearch.Enabled = false;
            btnRangeSearch.Enabled = false;
            btnSetIP.Enabled = false;
        }

        private void btnBroadSearch_Click(object sender, EventArgs e)
        {
            netFinder.FindAllPanel();
        }

        private void btnRangeSearch_Click(object sender, EventArgs e)
        {
            FormSetSearch frm = new FormSetSearch();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                netFinder.FindPanel((FindWay)frm.Findway, frm.MacOrIp);
            }
        }
        private void ShowDeviceInfo()
        {
            lstDev.Items.Clear();
            lstDev.Items.Insert(0, "设备的IP地址：" + device.IP);
            lstDev.Items.Insert(0, "设备的网关地址：" + device.Gateway);
            lstDev.Items.Insert(0, "设备的子网掩码：" + device.SubnetMask);
            lstDev.Items.Insert(0, "设备的Firmware版本号：" + device.AppVersion);
            lstDev.Items.Insert(0, "设备的Boot Loader版本号：" + device.BootloaderVersion);
            lstDev.Items.Insert(0, "设备模块号：" + device.ModuleID.ToString());
            lstDev.Items.Insert(0, "设备MAC地址：" + device.MacAddr);
            lstDev.Items.Insert(0, "设备键盘地址：" + device.KeypadAddr.ToString());
            lstDev.Items.Insert(0, "服务器IP地址：" + device.ServerIP);
            lstDev.Items.Insert(0, "服务器端口号：" + device.ServerPort.ToString());
            lstDev.Items.Insert(0, "设备控制的主机类型：" + device.ControlType.ToString());
            lstDev.Items.Insert(0, "主机的子类型：" + device.SubControlType.ToString());
            lstDev.Items.Insert(0, "Vista主机LRR地址：" + device.LRRAddr.ToString());
            lstDev.Items.Insert(0, "Vista主机新密码：" + device.Password);
            lstDev.Items.Insert(0, "Vista主机旧密码：" + device.OldPassword);
            lstDev.Items.Insert(0, "Vista主机安装密码：" + device.InstallerCode);


        }

        private void txtPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
            {
                e.Handled = false;
                return;
            } 
        }

        private void btnSetIP_Click(object sender, EventArgs e)
        {
            FormModifyIP frm = new FormModifyIP();
            frm.Dev = device;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                netFinder.ConfigPanel(device);
            }
        }
        #region 模拟按键处理
/*
        private void btn1_Click(object sender, EventArgs e)
        {
            cooMonitor.Sendkey(MAC, KeyCode.KeyCode_1);
            txtShow.Text += "1";
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            cooMonitor.Sendkey(MAC, KeyCode.KeyCode_2);
            txtShow.Text += "2";
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            cooMonitor.Sendkey(MAC, KeyCode.KeyCode_3);
            txtShow.Text += "3";
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            cooMonitor.Sendkey(MAC, KeyCode.KeyCode_4);
            txtShow.Text += "4";
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            cooMonitor.Sendkey(MAC, KeyCode.KeyCode_5);
            txtShow.Text += "5";
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            cooMonitor.Sendkey(currentMac, KeyCode.KeyCode_6);
            txtShow.Text += "6";
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            cooMonitor.Sendkey(currentMac, KeyCode.KeyCode_7);
            txtShow.Text += "7";
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            cooMonitor.Sendkey(currentMac, KeyCode.KeyCode_8);
            txtShow.Text += "8";
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            cooMonitor.Sendkey(currentMac, KeyCode.KeyCode_9);
            txtShow.Text += "9";
        }

        private void btnxing_Click(object sender, EventArgs e)
        {
            cooMonitor.Sendkey(currentMac, KeyCode.KeyCode_X);
            txtShow.Text += "*";
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            cooMonitor.Sendkey(currentMac, KeyCode.KeyCode_0);
            txtShow.Text += "0";
        }

        private void btnjin_Click(object sender, EventArgs e)
        {
            cooMonitor.Sendkey(currentMac, KeyCode.KeyCode_H);
            txtShow.Text += "#";
        }
        */

        #endregion



     
    }
}
