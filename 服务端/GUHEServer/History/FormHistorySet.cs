using GHIBMS.Common;
using System;
using System.Windows.Forms;

namespace GHIBMS.Server
{
    public partial class FormHistorySet : Form
    {
        public FormHistorySet()
        {
            InitializeComponent();
        }

        private void FormHistorySet_Shown(object sender, EventArgs e)
        {
            chkFastRecorder.Checked = ServerConfig.EnableFastRecorder;
            chkNormalRecorder.Checked = ServerConfig.EnableNormalRecorder;
            chkSlowRecorder.Checked = ServerConfig.EnableSlowRecorder;
            txtFastRecorder.Text = ServerConfig.TimeFastRecorder.ToString();
            txtNormalRecorder.Text = ServerConfig.TimeNormalRecorder.ToString();
            txtSlowRecorder.Text = ServerConfig.TimeSlowRecorder.ToString();
            txtFastRecorder.KeyPress += pubFun.NubOnly_KeyPress;
            txtNormalRecorder.KeyPress += pubFun.NubOnly_KeyPress;
            txtSlowRecorder.KeyPress += pubFun.NubOnly_KeyPress;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Int32.Parse(txtFastRecorder.Text) < 5)
            {
                MessageBox.Show("快速记录最小间隔为5秒！");
                txtFastRecorder.Text = "5";
            }
            if (Int32.Parse(txtNormalRecorder.Text) < 1)
            {
                MessageBox.Show("标准记录最小间隔为1分钟！");
                txtNormalRecorder.Text = "1";
            }
            if (Int32.Parse(txtNormalRecorder.Text) < 1)
            {
                MessageBox.Show("慢速记录最小间隔为1小时！");
                txtSlowRecorder.Text = "1";
            }

            ServerConfig.EnableFastRecorder = chkFastRecorder.Checked;
            ServerConfig.EnableNormalRecorder = chkNormalRecorder.Checked;
            ServerConfig.EnableSlowRecorder = chkSlowRecorder.Checked;
            ServerConfig.TimeFastRecorder = Int32.Parse(txtFastRecorder.Text);
            ServerConfig.TimeNormalRecorder = Int32.Parse(txtNormalRecorder.Text);
            ServerConfig.TimeSlowRecorder = Int32.Parse(txtSlowRecorder.Text);


            ServerConfig.saveToFile();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
