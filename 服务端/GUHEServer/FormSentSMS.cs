using GHIBMS.Common;
using System;
using System.Windows.Forms;

namespace GHIBMS.Server
{
    public partial class FormSentSMS : Form
    {
        public FormSentSMS()
        {
            InitializeComponent();
        }


        private void txtMsg_TextChanged(object sender, EventArgs e)
        {
            lblCount.Text = txtMsg.Text.Length.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SmsMsg sms = new SmsMsg();
            sms.Phone = txtPhone.Text.Trim();
            sms.Msg = txtMsg.Text.Trim();
            Smsdb.IntoSmsQueue(sms);

        }
    }
}
