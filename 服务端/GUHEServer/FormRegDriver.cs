using GHIBMS.Common;
using GHIBMS.Pub;
using System;
using System.Windows.Forms;



namespace GHIBMS.Server
{
    public partial class FormDriverReg : Form
    {
        private PluginConfig pluginConfig;
        public FormDriverReg(PluginConfig cfg)
        {
            pluginConfig = cfg;
            InitializeComponent();
        }

        private void FormReg_Load(object sender, EventArgs e)
        {
            RSAHelper rsa = new RSAHelper();
            if (rsa.CheckPluginRegCodeByInput(StrConst.SOFTNAME, pluginConfig.PlugID, pluginConfig.ActiveCode))
            {
                lblMsg.Visible = true;
                lblMsg.Text = "已激活";
                btnReg.Enabled = false;
                textBox2.Enabled = false;
            }
            else
            {
                lblMsg.Visible = true;
                lblMsg.Text = "未激活";
                btnReg.Enabled = true;
                textBox2.Enabled = true;
            }

            textBox1.Text = rsa.CreateGomputerbit(StrConst.SOFTNAME);
            textBoxDriveName.Text = pluginConfig.PlugName;
            textBoxID.Text = pluginConfig.PlugID;
        }



        private void btnReg_Click(object sender, EventArgs e)
        {
            RSAHelper rsa = new RSAHelper();

            if (textBox2.Text == "") return;

            string regtxt = textBox2.Text.Trim();

            if (rsa.CheckPluginRegCodeByInput(StrConst.SOFTNAME, pluginConfig.PlugID, regtxt))
            {
                pluginConfig.ActiveCode = regtxt;
                pluginConfig.ActiveState = true;
                MessageBox.Show("通讯驱动插件激活成功！");
                this.Close();

            }
            else
            {
                pluginConfig.ActiveState = false;
                MessageBox.Show("通讯驱动插件激活失败！");
            }
        }
    }
}
