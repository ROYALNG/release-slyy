using GHIBMS.Common;

using System;
using System.Windows.Forms;

namespace GHIBMS.Server
{
    public partial class FormCloudSet : Form
    {
        public FormCloudSet()
        {
            InitializeComponent();
        }

        private void FormCloudSet_Load(object sender, EventArgs e)
        {
            chkCompatibleV1.Checked = FormMain.toolStrip.Items["tspDowloadFromCloud"].Visible;
        }

        private void btxOK_Click(object sender, EventArgs e)
        {

            //ServerConfig.CloudRtdbUrl =txtRTDB.Text;
            ServerConfig.CloudMNSUrl = txtMNS.Text;
            //ServerConfig.CloudAlmUrl =txtAlarm.Text;
            ServerConfig.CloudUsername = txtName.Text;
            ServerConfig.CloudPassword = txtPass.Text;
            ServerConfig.CloudClientID = pubFun.checkUrl(txtDeviceID.Text);
            ServerConfig.CloudClientKEY = txtDeviceKey.Text;
            ServerConfig.CloudRtdbUrl = txtRedis.Text;
            ServerConfig.CloudLogin = txtLogin.Text;

            ServerConfig.CompatibleWithV1 = chkCompatibleV1.Checked;
            ServerConfig.EnableMNS = chkMns.Checked;
            ServerConfig.RedisPassword = txtRedisPw.Text;
            ServerConfig.EnableRedis = chkUseRedis.Checked;
            //if (chkCompatibleV1.Checked)
            //{
            //    FormMain.toolStrip.Items["tspDowloadFromCloud"].Visible = false;
            //}
            //else
            //{
            //    FormMain.toolStrip.Items["tspDowloadFromCloud"].Visible = true;
            //}

            ServerConfig.saveToFile();

            //try
            //{
            //     DeviceManagement.SingletonInstance.ReSet();

            //}
            //catch
            //{
            //    MessageBox.Show("参数有误！");
            //}


            Close();
        }

        private void FormCloudSet_Shown(object sender, EventArgs e)
        {
            //txtRTDB.Text=  ServerConfig.CloudRtdbUrl ;
            txtMNS.Text = ServerConfig.CloudMNSUrl;
            //txtAlarm.Text=ServerConfig.CloudAlmUrl ;
            txtName.Text = ServerConfig.CloudUsername;
            txtPass.Text = ServerConfig.CloudPassword;
            txtDeviceID.Text = pubFun.checkUrl(ServerConfig.CloudClientID);
            txtDeviceKey.Text = ServerConfig.CloudClientKEY;
            txtRedis.Text = ServerConfig.CloudRtdbUrl;
            txtLogin.Text = ServerConfig.CloudLogin;
            //chkCloud.Checked= ServerConfig.CloudEnable;
            chkMns.Checked = ServerConfig.EnableMNS;
            txtRedisPw.Text = ServerConfig.RedisPassword;
            chkCompatibleV1.Checked = ServerConfig.CompatibleWithV1;
            chkUseRedis.Checked = ServerConfig.EnableRedis;
        }

        private void btxCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkCompatibleV1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkMns_CheckedChanged(object sender, EventArgs e)
        {
            ServerConfig.EnableMNS = chkMns.Checked;
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void txtIOName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }
            if (e.KeyChar <= '9' && e.KeyChar >= '0' || e.KeyChar <= 'Z' && e.KeyChar >= 'A' || e.KeyChar <= 'z' && e.KeyChar >= 'a')
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void Label9_Click(object sender, EventArgs e)
        {

        }

        private void txtDeviceID_TextChanged(object sender, EventArgs e)
        {
            txtDeviceID.Text = pubFun.checkUrl(txtDeviceID.Text);
        }

        private void txtDeviceID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && e.KeyChar != '_' && e.KeyChar != '|')//这是允许输入 
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsLetter(e.KeyChar))//如果不是字符 也不是数字
                {
                    e.Handled = true; //当前输入处理置为已处理。即文本框不再显示当前按键信息
                }
            }
        }
    }
}
