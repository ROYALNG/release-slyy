using GHIBMS.Common;
using System;
using System.Windows.Forms;

namespace GHIBMS.Server
{
    public partial class FormMqttConfig : Form
    {
        DriverMng mng;
        public FormMqttConfig(DriverMng _mng)
        {
            InitializeComponent();
            mng = _mng;
            txtDelay.KeyPress += pubFun.NubOnly_KeyPress;
            text_server_port.KeyPress += pubFun.NubOnly_KeyPress;
            txtMaxRecordHis.KeyPress += pubFun.NubOnly_KeyPress;
        }

        private void btn_confirm_Click(object sender, EventArgs e)
        {
            if (text_server_ip.Text == ""
              || text_server_port.Text == "")
            {

                MessageBox.Show("IP和端口不能为空！");
                return;
            }
            if (text_deviceID.Text == ""
            || txtDeviceKey.Text == "")
            {

                MessageBox.Show("设备ID和KEY不能为空！");
                return;
            }
            if (text_username.Text == ""
            || text_password.Text == "")
            {

                MessageBox.Show("用户名和密码不能为空！");
                return;
            }


            ServerConfig.MqttEnable = this.check_enable_mqtt.Checked;
            ServerConfig.MqttServerIp = text_server_ip.Text;
            ServerConfig.MqttServerPort = pubFun.IsInt(text_server_port.Text, 0);
            ServerConfig.MqttUserName = text_username.Text;
            ServerConfig.MqttPassword = text_password.Text;
            ServerConfig.CloudClientID = text_deviceID.Text;
            ServerConfig.CloudClientKEY = txtDeviceKey.Text;
            ServerConfig.EnableMqttHisRecord = chkEnableMqttRecord.Checked;
            ServerConfig.MaxHisCount = pubFun.IsInt(txtMaxRecordHis.Text, 10000);
            if (radioButton1.Checked)
                ServerConfig.MqttVer = 1;
            else
                ServerConfig.MqttVer = 2;
            int d = pubFun.IsInt(txtDelay.Text, 50);
            if (d < 50)
                d = 50;
            ServerConfig.MqttDelay = d;
            ServerConfig.MqttEncrypt = chkEncrypt.Checked;

            ServerConfig.saveToFile();

            mng.mqttReset();

            this.Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormMqttConfig_Shown(object sender, EventArgs e)
        {
            try
            {
                check_enable_mqtt.Checked = ServerConfig.MqttEnable;
                text_server_ip.Text = ServerConfig.MqttServerIp;
                text_server_port.Text = ServerConfig.MqttServerPort.ToString();
                text_username.Text = ServerConfig.MqttUserName;
                text_password.Text = ServerConfig.MqttPassword;
                text_deviceID.Text = ServerConfig.CloudClientID;
                txtDeviceKey.Text = ServerConfig.CloudClientKEY;
                radioButton1.Checked = ServerConfig.MqttVer == 1;
                radioButton2.Checked = ServerConfig.MqttVer == 2;
                txtDelay.Text = ServerConfig.MqttDelay.ToString();
                txtMaxRecordHis.Text = ServerConfig.MaxHisCount.ToString();
                chkEnableMqttRecord.Checked = ServerConfig.EnableMqttHisRecord;
                chkEncrypt.Checked = ServerConfig.MqttEncrypt;
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            text_password.PasswordChar = new char();

        }

        private void text_deviceID_TextChanged(object sender, EventArgs e)
        {
            text_deviceID.Text = pubFun.checkUrl(text_deviceID.Text);
        }

        private void text_deviceID_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar != '\b' && e.KeyChar != '_' && e.KeyChar != '|')//这是允许输入 
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsLetter(e.KeyChar))//如果不是字符 也不是数字
                {
                    e.Handled = true; //当前输入处理置为已处理。即文本框不再显示当前按键信息
                }
            }
        }

        private void txtDeviceKey_KeyPress(object sender, KeyPressEventArgs e)
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
