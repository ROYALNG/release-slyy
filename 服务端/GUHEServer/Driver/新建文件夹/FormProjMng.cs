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
    public partial class FormProjMng : DevComponents.DotNetBar.Office2007Form
    {
        public FormProjMng()
        {
            InitializeComponent();
        }

        private void FormProjMng_Load(object sender, EventArgs e)
        {
            for (int i = 1; i < 25; i++)
            {
                CheckBox chk = this.Controls["chkSys" + i] as CheckBox;
                chk.Checked = Config.GetSelSystem(i);
            }

        }

        private void FormProjMng_FormClosing(object sender, FormClosingEventArgs e)
        {
            for (int i = 1; i < 25; i++)
            {
                CheckBox chk = this.Controls["chkSys" + i] as CheckBox;
                Config.SetSelSystem(i, chk.Checked);
            }
            Config.saveToFile();
        }
    }
}
