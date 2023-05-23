using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GHIBMS.Server
{
    public partial class FormIPMFinderOCX : DevComponents.DotNetBar.Office2007Form
    {
        public FormIPMFinderOCX()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 启动服务器；服务器端口需要在Port 属性中指定
        /// </summary>
        /// <param name="Port"></param>
        /// <returns></returns>

        public void Startup(int Port)
        {
            NetFinder.Port = Port;
            NetFinder.Startup();
        }
        /// <summary>
        /// 关闭服务器；
        /// </summary>
        /// <returns></returns>

        public void Shutdown()
        {
            NetFinder.Shutdown();
        }
        public void Stop()
        {
            NetFinder.Stop();
        }
        public int FindAllPanel()
        {
            return NetFinder.FindAllPanel();
        }
        public int FindPanel(IPModuleLib.FindWay matchAttr, string addr)
        {
            return NetFinder.FindPanel(matchAttr, addr);
            
        }
        public int ConfigPanel(IPModuleLib.IPMDevice device)
        {
            return NetFinder.ConfigPanel(device);
        }
        public delegate void DeviceReportDelegate(object sender, _INetFinderEvents_DeviceReportEvent e);
        public event DeviceReportDelegate OnDeviceReport;


        public delegate void ConfigResultDelegate(object sender, _INetFinderEvents_ConfigResultEvent e);
        public event ConfigResultDelegate OnConfigResul;

      

        private void NetFinder_ConfigResult(object sender, _INetFinderEvents_ConfigResultEvent e)
        {
            if (OnConfigResul != null)
                OnConfigResul(sender,e);
            

        }

        private void NetFinder_DeviceReport(object sender, _INetFinderEvents_DeviceReportEvent e)
        {
            if (OnDeviceReport != null)
                OnDeviceReport(sender,e);
      

        }
      
    }
}
