using GHIBMS.Common;
using GHIBMS.Interface;
using System;
using System.Windows.Forms;

namespace GHIBMS.Server
{
    public partial class FormChannelWizard : Form
    {
        private IChannel chanInfo = null;
        private string chanCode = "";
        public IChannel ChanInfo
        {
            get { return chanInfo; }
        }

        public FormChannelWizard()
        {
            InitializeComponent();
            cmbCommType.KeyPress += pubFun.No_KeyPress;
            lblCommType.Visible = false;
            cmbCommType.Visible = false;

        }

        private void wizardDev_NextButtonClick(WizardBase.WizardControl sender, WizardBase.WizardNextButtonClickEventArgs args)
        {

            if (wizardDev.CurrentStepIndex == 0) return;
            switch (wizardDev.CurrentStepIndex)
            {
                case 1:
                    if (txtProtocolName.Text.Trim().Length == 0)
                    {
                        MessageBox.Show("通讯协议为空，请选择通讯协议！");
                        wizardDev.CurrentStepIndex--;
                    }
                    break;
                case 2:
                    string name = txtChannelName.Text.Trim();
                    if (pubFun.NameSyntaxCheck(name) == false)
                    {
                        wizardDev.CurrentStepIndex--;

                    }
                    if (Rtdb.IsExistChanName(name))
                    {
                        MessageBox.Show("该名称已经存在，请重新输入名称！");
                        wizardDev.CurrentStepIndex--;
                    }
                    break;
            }

        }
        private void wizardDev_BackButtonClick(WizardBase.WizardControl sender, WizardBase.WizardClickEventArgs args)
        {
            switch (wizardDev.CurrentStepIndex)
            {
                case 1:
                    break;
                case 2:
                    break;
            }
        }


        private void wizardDev_CurrentStepIndexChanged(object sender, EventArgs e)
        {
            //wizardDev.CurrentStepIndex = 0;
        }

        private void cmbCommType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void wizardDev_FinishButtonClick(object sender, EventArgs e)
        {
            try
            {
                //CommInterface type = CommInterface.API;
                //try
                //{
                //    if (cmbCommType.Items.Count > 0)
                //        type =Enum.Parse(typeof(CommInterface), cmbCommType.Text);
                //}
                //catch { }

                chanInfo = PluginMng.CreateChannel(chanCode, cmbCommType.Text);
                chanInfo.Name = this.txtChannelName.Text;
                chanInfo.Enable = this.chkEnable.Checked;
            }
            catch { }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnDevType_Click(object sender, EventArgs e)
        {
            FormDevPluginBrowse frm = new FormDevPluginBrowse();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.txtProtocolName.Text = frm.selectedProtocol.ProtocolName;
                this.chanCode = frm.selectedProtocol.ProtocolCode;
                this.txtChannelName.Text = frm.selectedProtocol.Plugin.GetUniqueName(UniqueNameType.Channel);
                string[] type = frm.selectedProtocol.Plugin.GetCommInterface();
                foreach (string s in type)
                {
                    cmbCommType.Items.Add(s);
                }
                if (type.Length < 2)
                {
                    lblCommType.Visible = false;
                    cmbCommType.Visible = false;
                }
                else
                {
                    lblCommType.Visible = true;
                    cmbCommType.Visible = true;
                }
                if (type.Length > 0)
                    cmbCommType.SelectedIndex = 0;
            }
        }

        private void FormChannelWizard_Load(object sender, EventArgs e)
        {

        }
        private void wizardDev_CancelButtonClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }


    }
}
