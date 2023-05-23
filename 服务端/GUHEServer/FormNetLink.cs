using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GHIBMS.Common;

namespace GHIBMS.Server
{
    public partial class FormNetLink : Form
    {
        public FormNetLink()
        {
            InitializeComponent();
        }

        private void FormNetLink_Shown(object sender, EventArgs e)
        {
            txtNetServerIP.Text = ServerConfig.NetServerIP;
            txtNetPort.Text = ServerConfig.NetServerPort.ToString();
            txtNetID.Text = ServerConfig.NetStationId.ToString();
            txtNetInterval.Text = ServerConfig.NetSendInterval.ToString();
        }

        private void btxOK_Click(object sender, EventArgs e)
        {
            if (pubFun.IsNumeric( txtNetInterval.Text) < 30)
            {
                MessageBox.Show("发送间隔不能小于30秒，请重新设定数值。");
                return;

            }
            ServerConfig.NetServerIP = txtNetServerIP.Text;
            ServerConfig.NetServerPort =pubFun.IsNumeric(txtNetPort.Text);
            ServerConfig.NetStationId = pubFun.IsNumeric(txtNetID.Text);
            ServerConfig.NetSendInterval = pubFun.IsNumeric(txtNetInterval.Text);
            ServerConfig.saveToFile();
            Close();
        }

        private void btxCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
