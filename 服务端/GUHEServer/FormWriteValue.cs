using System;
using System.Windows.Forms;

namespace GHIBMS.Server
{
    public partial class FormWriteValue : Form
    {
        public FormWriteValue()
        {
            InitializeComponent();
        }
        public void SetText(string name, string type, string oldvalue)
        {
            txtName.Text = name;
            txtDataType.Text = type;
            txtCurrentValue.Text = oldvalue;
        }
        public string Newvalue
        {
            get { return txtNewValue.Text; }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
