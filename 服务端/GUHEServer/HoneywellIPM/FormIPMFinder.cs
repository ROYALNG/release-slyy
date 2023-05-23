using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using IPModuleLib;
using AxIPModuleLib;
using System.Diagnostics;

namespace GHIBMS.Server
{

    public partial class FormIPMFinder : DevComponents.DotNetBar.Office2007Form
    {
        public IPMDevice currentDevice = null;
        private FormIPMFinderOCX netFinder = new FormIPMFinderOCX();
        //private string currentMac = "";
        public FormIPMFinder()
        {
            InitializeComponent();
           
        }
        #region 按钮事件
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (txtPort.Text.Trim().Length > 0)
            {

                int Port = int.Parse(txtPort.Text.Trim());
                netFinder.Startup(Port);
                netFinder.OnConfigResul+=new FormIPMFinderOCX.ConfigResultDelegate(netFinder_OnConfigResul);
                netFinder.OnDeviceReport+=new FormIPMFinderOCX.DeviceReportDelegate(netFinder_OnDeviceReport);
                btnStart.Enabled = false;
                btnStop.Enabled = true;
                btnBroadSearch.Enabled = true;
                btnRangeSearch.Enabled = true;
                btnSetIP.Enabled = true;

               
            }
        }
        public delegate void netFinderOnConfigResulCallback(object sender, _INetFinderEvents_ConfigResultEvent e);
        private void netFinderOnConfig(object sender, _INetFinderEvents_ConfigResultEvent e)
        {
            //MessageBox.Show("netFinderOnConfig   " );

        }

        private void netFinder_OnConfigResul(object sender, _INetFinderEvents_ConfigResultEvent e)
        {
            this.Invoke(new netFinderOnConfigResulCallback(netFinderOnConfig), new object[] { sender,e });

        }

        public delegate void netFinder_OnDeviceReportCallback(object sender, _INetFinderEvents_DeviceReportEvent e);
        private void netFindeOnDeviceReport(object sender, _INetFinderEvents_DeviceReportEvent e)
        {
            try
            {

             
                timer1.Enabled = false;
                ShowDeviceInfo(e.device);
                progressBar1.Value = 0;
                btnRangeSearch.Enabled = true;
                btnStopSearch.Enabled = false;
                currentDevice = e.device;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
        }
        private void netFinder_OnDeviceReport(object sender, _INetFinderEvents_DeviceReportEvent e)
        {
            this.Invoke(new netFinder_OnDeviceReportCallback(netFindeOnDeviceReport),new object[]{sender,e});
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
            //netFinder.FindPanel(IPModuleLib.FindWay.FindByIP, "192.168.0.3");
            FormSetSearch frm = new FormSetSearch();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    netFinder.FindPanel((IPModuleLib.FindWay)frm.Findway, frm.MacOrIp);
                }
                catch { }
                timer1.Enabled = true;
                btnRangeSearch.Enabled = false;
                btnStopSearch.Enabled = true;
            }
        }
        private void btnSetIP_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentDevice.MacAddr != "")
                {
                    FormModifyIP frm = new FormModifyIP();
                    frm.Dev = currentDevice;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        netFinder.ConfigPanel(currentDevice);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        #endregion
        #region IPM事件处理
        private void ShowDeviceInfo(IPMDevice device)
        {
             richTextBox1.Clear();
             richTextBox1.AppendText( "设备的IP地址：" + device.IP+"\r\n");
             richTextBox1.AppendText("设备的网关地址：" + device.Gateway + "\r\n");
             richTextBox1.AppendText("设备的子网掩码：" + device.SubnetMask + "\r\n");
             richTextBox1.AppendText("设备的Firmware版本号：" + device.AppVersion + "\r\n");
             richTextBox1.AppendText("设备的Boot Loader版本号：" + device.BootloaderVersion + "\r\n");
             richTextBox1.AppendText("设备模块号：" + device.ModuleID.ToString() + "\r\n");
             richTextBox1.AppendText("设备MAC地址：" + device.MacAddr + "\r\n");
             richTextBox1.AppendText("设备键盘地址：" + device.KeypadAddr.ToString() + "\r\n");
             richTextBox1.AppendText("服务器IP地址：" + device.ServerIP + "\r\n");
             richTextBox1.AppendText("服务器端口号：" + device.ServerPort.ToString() + "\r\n");
             richTextBox1.AppendText("设备控制的主机类型：" + device.ControlType.ToString() + "\r\n");
             richTextBox1.AppendText("主机的子类型：" + device.SubControlType.ToString() + "\r\n");
             richTextBox1.AppendText("Vista主机LRR地址：" + device.LRRAddr.ToString() + "\r\n");
            // lstDev.Items.Insert(0, "Vista主机新密码：" + device.Password);
            //lstDev.Items.Insert(0, "Vista主机旧密码：" + device.OldPassword);
            //lstDev.Items.Insert(0, "Vista主机安装密码：" + device.InstallerCode);
            
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
