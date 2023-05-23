using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GHIBMS.Server
{
    public partial class FormModifyPassword : DevComponents.DotNetBar.Office2007Form
    {
        private string _OldPass;
        private string _NewPass;
        public string OldPass
        {
            get { return _OldPass; }

        }
        public string NewPass
        {
            get { return _NewPass; }
        }

        public FormModifyPassword()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtOldPass.Text.Trim().Length > 0)
            {
                if (txtNewPass1.Text.Trim().Length > 0 && txtNewPass2.Text.Trim().Length > 0
                                 && txtNewPass1.Text.Trim() == txtNewPass2.Text.Trim())
                {
                    _OldPass = txtOldPass.Text.Trim();
                    _NewPass = txtNewPass1.Text.Trim();
                    this.DialogResult = DialogResult.OK;
                    this.Close();


                }
                else
                {
                    MessageBox.Show("新密码输入有误！");
                }


            }
            else
            {
                MessageBox.Show("旧密码不能为空！");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
