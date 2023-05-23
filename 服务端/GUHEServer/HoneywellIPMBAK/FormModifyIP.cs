using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace HoneywellIPM
{
    public partial class FormModifyIP : Form
    {
        public FormModifyIP()
        {
            InitializeComponent();
        }
        private IPModuleLib.IPMDevice dev;
        public IPModuleLib.IPMDevice Dev
        {
            set { dev = value; }
            get { return dev; }
        }

        private void txtPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if   ((e.KeyChar   >= '0')   &&   (e.KeyChar   <=  '9'))   
            { 
                e.Handled   =   false; 
                return; 
            } 
            e.Handled   =   true; 
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (dev != null)
            {
                dev.IP = ipAddr.Text;
                dev.SubnetMask=ipSubAddr.Text ;
                dev.Gateway=ipGateway.Text ;
                dev.ServerIP=ipServer.Text  ;
                dev.ServerPort=int.Parse(txtPort.Text);
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void FormModifyIP_Load(object sender, EventArgs e)
        {
            if (dev != null)
            {
                ipAddr.Text = dev.IP;
                ipSubAddr.Text = dev.SubnetMask;
                ipGateway.Text = dev.Gateway;
                ipServer.Text = dev.ServerIP;
                txtPort.Text = dev.ServerPort.ToString();
            }
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
