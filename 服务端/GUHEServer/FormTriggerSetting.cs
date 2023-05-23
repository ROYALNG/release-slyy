using System;
using System.Windows.Forms;

namespace GHIBMS.Server
{
    public partial class FormTriggerSetting : Form
    {

        public FormTriggerSetting()
        {
            InitializeComponent();
            cmbEventType.Items.Add("数值相等");
            cmbEventType.Items.Add("数值超限");
            cmbEventType.Items.Add("数值改变");
            cmbEventType.Items.Add("数值不等");
            cmbEventType.SelectedIndex = 0;
            groupBox1.Visible = true;
            groupBox2.Visible = false;

        }
        private string _VarExpress = "";
        public string VarExpress
        {
            get
            {
                return _VarExpress;
            }
            set
            {
                _VarExpress = value;
                SetExpress(_VarExpress);

            }
        }
        private void SetExpress(string exp)
        {

            if (exp == "数值改变")
            {
                cmbEventType.SelectedIndex = 2;
            }
            else
            {
                foreach (Control c in groupBox1.Controls)
                {
                    if (c is CheckBox)
                    {
                        CheckBox b = c as CheckBox;
                        b.Checked = false;
                    }
                }
                foreach (Control c in groupBox3.Controls)
                {
                    if (c is CheckBox)
                    {
                        CheckBox b = c as CheckBox;
                        b.Checked = false;
                    }
                }

                string[] arr = exp.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string s in arr)
                {
                    string[] s1 = s.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                    if (s1.Length == 2)
                    {
                        if (s1[0] == "上限")
                        {
                            cmbEventType.SelectedIndex = 1;
                            txtUp.Text = s1[1];
                            chkUp.Checked = true;
                        }
                        else if (s1[0] == "下限")
                        {
                            cmbEventType.SelectedIndex = 1;
                            txtDown.Text = s1[1];
                            chkDown.Checked = true;
                        }
                        else if (s1[0] == "数值")
                        {
                            cmbEventType.SelectedIndex = 0;
                            foreach (Control c in groupBox1.Controls)
                            {
                                if (c is CheckBox)
                                {
                                    CheckBox b = c as CheckBox;
                                    if (b.Text == s1[1].ToString())
                                    {
                                        b.Checked = true;
                                    }

                                }
                            }
                        }
                        else if (s1[0] == "不等")
                        {
                            cmbEventType.SelectedIndex = 3;
                            foreach (Control c in groupBox3.Controls)
                            {
                                if (c is CheckBox)
                                {
                                    CheckBox b = c as CheckBox;
                                    if (b.Text == s1[1].ToString())
                                    {
                                        b.Checked = true;
                                    }

                                }
                            }

                        }

                    }
                }
            }
            this.Invalidate();
        }

        private void cmbEventType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = cmbEventType.SelectedIndex;
            switch (i)
            {
                case 0:
                    groupBox1.Visible = true;
                    groupBox2.Visible = false;
                    groupBox3.Visible = false;
                    break;
                case 1:
                    groupBox1.Visible = false;
                    groupBox2.Visible = true;
                    groupBox3.Visible = false;
                    break;
                case 2:
                    groupBox1.Visible = false;
                    groupBox2.Visible = false;
                    groupBox3.Visible = false;
                    break;
                case 3:
                    groupBox1.Visible = false;
                    groupBox2.Visible = false;
                    groupBox3.Visible = true;
                    break;

            }
        }

        private void chkUp_CheckedChanged(object sender, EventArgs e)
        {
            txtUp.Enabled = chkUp.Checked;
        }

        private void chkDown_CheckedChanged(object sender, EventArgs e)
        {
            txtDown.Enabled = chkDown.Checked;
        }

        private void FormTriggerSetting_Load(object sender, EventArgs e)
        {

        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            int i = cmbEventType.SelectedIndex;
            switch (i)
            {
                case 0: //状态
                    {
                        string s = "";
                        foreach (Control c in groupBox1.Controls)
                        {
                            if (c is CheckBox)
                            {
                                CheckBox b = c as CheckBox;
                                if (b.Checked)
                                {
                                    s += "数值:" + b.Text;
                                    s += "||";
                                }
                            }
                        }
                        _VarExpress = s;
                    }
                    break;
                case 1: //限值
                    {
                        string s = "";
                        if (chkUp.Checked)
                        {
                            s += "上限:" + txtUp.Text;
                            s += "||";
                        }

                        if (chkDown.Checked)
                        {
                            s += "下限:" + txtDown.Text;
                            s += "||";
                        }
                        _VarExpress = s;
                    }
                    break;
                case 2: //变化
                    _VarExpress = "数值改变";
                    break;

                case 3: //不等
                    {
                        string s = "";
                        foreach (Control c in groupBox3.Controls)
                        {
                            if (c is CheckBox)
                            {
                                CheckBox b = c as CheckBox;
                                if (b.Checked)
                                {
                                    s += "不等:" + b.Text;
                                    s += "||";
                                }
                            }
                        }
                        _VarExpress = s;
                    }
                    break;

            }
            if (_VarExpress.EndsWith("||"))
            {
                _VarExpress = _VarExpress.Remove(VarExpress.Length - 2);
            }
            this.DialogResult = DialogResult.OK;
        }

        private void btnQuxiao_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

    }
}
