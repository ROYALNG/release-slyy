using GHIBMS.Common;
using System;
using System.Windows.Forms;


namespace GHIBMS.Server
{
    public partial class FormRename : Form
    {
        public FormRename(string oldName)
        {
            InitializeComponent();
            this.txtOldName.Text = oldName;
            this.txtOldName.KeyPress += pubFun.No_KeyPress;
            newName = "";
        }
        public string newName = "";
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtNewName.Text.Trim() == "")
            {
                MessageBox.Show("新名称不能为空！");
                return;
            }
            if (txtNewName.Text.Trim() == txtOldName.Text)
            {
                MessageBox.Show("新名称不能和老名称相同！");
                return;
            }
            string name = txtNewName.Text.Trim();
            //if (Rtdb.IsExistName(name))
            //{
            //    MessageBox.Show("新名称已经存在！");
            //    return;
            //}
            if (!pubFun.NameSyntaxCheck(name))
                return;

            newName = name;
            this.DialogResult = DialogResult.OK;


        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

        }
    }

}
