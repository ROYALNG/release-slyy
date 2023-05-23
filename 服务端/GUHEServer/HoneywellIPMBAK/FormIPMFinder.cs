using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HoneywellIPM
{
    public partial class FormIPMFinder : Form
    {
        private IPModuleLib.IPMDevice device = new IPModuleLib.IPMDevice();

        private string currentMac = "";
        public FormIPMFinder()
        {
            InitializeComponent();
        }
        #region 按钮事件
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
            timer1.Enabled = true;
            btnBroadSearch.Enabled = false;
            btnStopSearch.Enabled = true;

        }

        private void btnRangeSearch_Click(object sender, EventArgs e)
        {
            FormSetSearch frm = new FormSetSearch();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                netFinder.FindPanel((IPModuleLib.FindWay)frm.Findway, frm.MacOrIp);
                timer1.Enabled = true;
                btnRangeSearch.Enabled = false;
                btnStopSearch.Enabled = true;
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
        #endregion
        #region IPM事件处理
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
            //lstDev.Items.Insert(0, "Vista主机新密码：" + device.Password);
            //lstDev.Items.Insert(0, "Vista主机旧密码：" + device.OldPassword);
            //lstDev.Items.Insert(0, "Vista主机安装密码：" + device.InstallerCode);


        }
        //上报查找到的设备
        private void netFinder_DeviceReport(object sender, AxIPModuleLib._INetFinderEvents_DeviceReportEvent e)
        {
            timer1.Enabled = false;
            progressBar1.Value = 0;

            lstLog.Items.Insert(0, "----------------------------------");
            currentMac = e.device.MacAddr;//赋值会执行，但是使其他控制作出反应有的时候不会执行
            device = e.device; //赋值给自定义结构体，用来存储设备的完整信息
            lstLog.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了DeviceReport事件");

            this.ShowDeviceInfo();//显示设备信息
            lstLog.Items.Insert(0, "设备的IP地址：" + device.IP);
            lstLog.Items.Insert(0, "设备的网关地址：" + device.Gateway);
            lstLog.Items.Insert(0, "设备的子网掩码：" + device.SubnetMask);
            lstLog.Items.Insert(0, "设备的Firmware版本号：" + device.AppVersion);
            lstLog.Items.Insert(0, "设备的Boot Loader版本号：" + device.BootloaderVersion);
            lstLog.Items.Insert(0, "设备模块号：" + device.ModuleID.ToString());
            lstLog.Items.Insert(0, "设备MAC地址：" + device.MacAddr);
            lstLog.Items.Insert(0, "设备键盘地址：" + device.KeypadAddr.ToString());
            lstLog.Items.Insert(0, "服务器IP地址：" + device.ServerIP);
            lstLog.Items.Insert(0, "服务器端口号：" + device.ServerPort.ToString());
            lstLog.Items.Insert(0, "设备控制的主机类型：" + device.ControlType.ToString());
            lstLog.Items.Insert(0, "主机的子类型：" + device.SubControlType.ToString());
            lstLog.Items.Insert(0, "Vista主机LRR地址：" + device.LRRAddr.ToString());

            lstLog.Items.Insert(0, "查询设备信息完成");
        }


        //上报配置结果
        private void netFinder_ConfigResult(object sender, AxIPModuleLib._INetFinderEvents_ConfigResultEvent e)
        {
            lstLog.Items.Insert(0, "----------------------------------");
            lstLog.Items.Insert(0, "在 " + System.DateTime.Now.ToString() + " " + "触发了ConfigResult事件");
            lstLog.Items.Insert(0, "配置结果:" + e.result.ToString());
        }
        #endregion

        private void txtPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
            {
                e.Handled = false;
                return;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value == 100)
                progressBar1.Value = 0;

            progressBar1.Value++;
        }

     
        private void btnStopSearch_Click(object sender, EventArgs e)
        {
            netFinder.Stop();
            timer1.Enabled = false;
            progressBar1.Value = 0;
            btnBroadSearch.Enabled = true;
            btnRangeSearch.Enabled = true;
            btnStopSearch.Enabled = false; ;

        }

       
    }
}
