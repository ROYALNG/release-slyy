using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HoneywellIPM
{
    public partial class FormSetSearch : Form
    {
        private int findway = 0;
        public int Findway
        {
            set { findway = value; }
            get { return findway; }
        }
        private string _MacOrIp;
        public string MacOrIp
        {
            set { _MacOrIp = value; }
            get { return _MacOrIp; }

        }

        public FormSetSearch()
        {
            InitializeComponent();
        }

        private void rdoMac_Click(object sender, EventArgs e)
        {
            if (rdoMac.Checked == true)
            {
                mskMac.Enabled = true;
                ipAddr.Enabled = false;
                findway=0;
            }
        }

        private void rdoIP_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoIP.Checked == true)
            {
                mskMac.Enabled = false;
                ipAddr.Enabled = true;
                findway=1;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (rdoMac.Checked == true)
            {
                _MacOrIp= mskMac.Text;
                if (_MacOrIp == "   -  -  -  -  -")
                {
                    MessageBox.Show("请正确输入Mac地址！");
                    return;
                }
                
            }
            if (rdoIP.Checked == true)
            {
                _MacOrIp=ipAddr.Text;
                if (_MacOrIp == "...")
                {
                    MessageBox.Show("请正确输入IP地址！");
                    return;
                }
            }
            this.DialogResult= DialogResult.OK;
            this.Close();
        }

       
        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            this.DialogResult= DialogResult.Cancel;
            this.Close();
        }
    }
}
