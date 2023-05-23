using GHIBMS.Common;
using GHIBMS.Interface;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GHIBMS.Server
{
    public partial class FormControllerWizard : Form
    {

        public List<IController> NewControllerList = new List<IController>();
        public IChannel SelChannel;

        public FormControllerWizard()
        {
            InitializeComponent();
            //this.cmbLogID.KeyPress += pubFun.NubOnly_KeyPress;
            txtCount.KeyPress += pubFun.NubOnly_KeyPress;
        }


        private void wizardControl1_NextButtonClick(WizardBase.WizardControl sender, WizardBase.WizardNextButtonClickEventArgs args)
        {
            if (wizardControl1.CurrentStepIndex == 0) return;
            switch (wizardControl1.CurrentStepIndex)
            {
                case 1:
                    if (txtName.Text.Trim().Length == 0)
                    {
                        MessageBox.Show("名称为空，请输入名称！");
                        wizardControl1.CurrentStepIndex--;
                        return;
                    }
                    if (Rtdb.IsExistName(txtName.Text.Trim()))
                    {
                        MessageBox.Show("控制器名称重复，请重新输入名称！");
                        wizardControl1.CurrentStepIndex--;
                        return;
                    }
                    if (txtCount.Text.Trim().Length == 0)
                    {
                        MessageBox.Show("新增控制器数量为空，请输入数量！");
                        wizardControl1.CurrentStepIndex--;
                        return;
                    }
                    if (!pubFun.NameSyntaxCheck(txtName.Text))
                    {
                        wizardControl1.CurrentStepIndex--;
                        return;
                    }

                    break;
                case 2:
                    if (cmbLogID.Text.Trim().Length == 0)
                    {
                        MessageBox.Show("地址不能为空，设定地址！");
                        wizardControl1.CurrentStepIndex--;
                        return;
                    }
                    //同一通道控制器地址唯一性检查
                    if (Rtdb.IsExistControllerAddr(SelChannel, cmbLogID.Text))
                    {
                        MessageBox.Show("地址重复，请重新输入地址！");
                        return;
                    }
                    if (cmbLevel.Text.Trim().Length == 0)
                    {
                        MessageBox.Show("级别不能为空，设定级别！");
                        wizardControl1.CurrentStepIndex--;
                        return;
                    }

                    break;

            }
        }

        private void FormControllerWizard_Load(object sender, EventArgs e)
        {

            cmbLevel.Items.Clear();
            cmbLogID.Items.Clear();
            for (int i = 0; i < 512; i++)
            {
                cmbLogID.Items.Add(i.ToString());
                cmbLevel.Items.Add(i.ToString());
            }

            txtName.Text = SelChannel.Plugin.GetUniqueName(UniqueNameType.Controller);
            cmbLogID.Text = Rtdb.GetUniqueControllerAddr(SelChannel, "0");
            cmbLevel.Text = "1";


        }

        private void wizardControl1_FinishButtonClick(object sender, EventArgs e)
        {
            try
            {
                NewControllerList.Clear();
                //Console.WriteLine(DateTime.Now.ToString());
                int iCount = pubFun.IsInt(txtCount.Text, 0);
                ICommunicationPlug plugin = SelChannel.Plugin;
                string name = txtName.Text.Trim();
                string addr = cmbLogID.Text.Trim();
                for (int i = 1; i < (iCount + 1); i++)
                {

                    IController con = plugin.CreateController(SelChannel);

                    if (con != null)
                    {
                        con.ChannelObject = SelChannel;
                        if (i == 1)
                        {
                            con.Name = name;
                            con.Address = addr;
                            //con.ID = pubFun.checkUrl(name);
                        }
                        else
                        {
                            name = con.Name = Rtdb.GetUniqueCloneName(name);
                            //con.ID = Rtdb.GetUniqueCloneName(pubFun.checkUrl(name));
                            addr = con.Address = Rtdb.GetUniqueControllerAddr(SelChannel, addr);

                        }
                        con.Enable = this.chkEnable.Checked;
                        con.OperLevel = pubFun.IsInt(this.cmbLevel.Text, 1);

                        SelChannel.ConList.Add(con);
                        NewControllerList.Add(con);
                    }

                }
                // Console.WriteLine(DateTime.Now.ToString());
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void wizardControl1_CancelButtonClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void wizardControl1_BackButtonClick(WizardBase.WizardControl sender, WizardBase.WizardClickEventArgs args)
        {

        }

        private void cmbLogID_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
