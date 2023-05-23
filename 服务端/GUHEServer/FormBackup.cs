using GHIBMS.Common;
using System;
using System.Windows.Forms;

namespace GHIBMS.Server
{
    public partial class FormBackup : Form
    {
        DriverMng mng;
        public FormBackup(DriverMng _mng)
        {
            InitializeComponent();
            mng = _mng;
        }

        private void btn_confirm_Click(object sender, EventArgs e)
        {
            if (text_server_ip.Text == ""
              || text_server_port.Text == "")
            {

                MessageBox.Show("IP和端口不能为空！");
                return;
            }

            ServerConfig.StandbyMasterIp = text_server_ip.Text;
            ServerConfig.StandbyMasterPort = pubFun.IsInt(text_server_port.Text, 32771);
            ServerConfig.StandbyMaster = rdoMaster.Checked;
            ServerConfig.StandbyEnable = chkEnable.Checked;
            ServerConfig.saveToFile();
            mng.standbyReset();
            this.Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormBackup_Shown(object sender, EventArgs e)
        {
            try
            {

                text_server_ip.Text = ServerConfig.StandbyMasterIp;
                text_server_port.Text = ServerConfig.StandbyMasterPort.ToString();
                rdoMaster.Checked = ServerConfig.StandbyMaster;
                rdoSlave.Checked = !ServerConfig.StandbyMaster;
                chkEnable.Checked = ServerConfig.StandbyEnable;

            }
            catch (Exception ex)
            {

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtDeviceKey_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
