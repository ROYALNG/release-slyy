using GHIBMS.Common;
using GHIBMS.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace GHIBMS.Server
{
    public partial class FormVarWizard : Form
    {
        private IController selController = null;
        public List<IVariable> NewVarList = new List<IVariable>();
        public FormVarWizard(bool bEdit, IController con)
        {
            InitializeComponent();
            selController = con;
            //cmbVariableAddress.KeyPress += pubFun.NubOnly_KeyPress;
            cmbVariableLevel.KeyPress += pubFun.NubOnly_KeyPress;
            txtCount.KeyPress += pubFun.NubOnly_KeyPress;
            if (con.VarList.Count > 0)
            {
                txtVariableName.Text = Rtdb.GetUniqueCloneName(con.VarList[con.VarList.Count - 1].Name);
                cmbVariableAddress.Text = Rtdb.GetUniqueVariableAddr(selController, con.VarList[con.VarList.Count - 1].Address).ToString();
            }
            else
            {
                txtVariableName.Text = selController.ChannelObject.Plugin.GetUniqueName(UniqueNameType.Variable);
                cmbVariableAddress.Text = Rtdb.GetUniqueVariableAddr(selController, "0").ToString();
            }

        }

        private void wizardDev_NextButtonClick(WizardBase.WizardControl sender, WizardBase.WizardNextButtonClickEventArgs args)
        {

            if (wizardDev.CurrentStepIndex == 0) return;
            switch (wizardDev.CurrentStepIndex)
            {
                case 1:
                    if (txtVariableName.Text.Trim().Length == 0)
                    {
                        MessageBox.Show("名称为空，请输入名称！");
                        wizardDev.CurrentStepIndex--;
                        return;
                    }
                    if (Rtdb.IsExistName(txtVariableName.Text.Trim()))
                    {
                        MessageBox.Show("名称重复，请重新输入名称！");
                        wizardDev.CurrentStepIndex--;
                        return;
                    }
                    if (txtCount.Text.Trim().Length == 0)
                    {
                        MessageBox.Show("新增变量数量为空，请输入数量！");
                        wizardDev.CurrentStepIndex--;
                        return;
                    }
                    if (!pubFun.NameSyntaxCheck(txtVariableName.Text))
                    {
                        wizardDev.CurrentStepIndex--;
                        return;
                    }

                    break;
                case 2:
                    if (cmbVariableAddress.Text.Trim().Length == 0)
                    {
                        MessageBox.Show("地址不能为空！");
                        wizardDev.CurrentStepIndex--;
                        return;
                    }
                    if (cmbVariableLevel.Text.Trim().Length == 0)
                    {
                        MessageBox.Show("级别不能为空，设定级别！");
                        wizardDev.CurrentStepIndex--;
                        return;
                    }
                    break;
            }

        }

        private void wizardDev_CurrentStepIndexChanged(object sender, EventArgs e)
        {
            //wizardDev.CurrentStepIndex = 0;
        }

        private void pnlNet3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void wizardDev_FinishButtonClick(object sender, EventArgs e)
        {
            try
            {
                // Console.WriteLine(DateTime.Now.ToString());
                Stopwatch watch = new Stopwatch();
                watch.Start();
                NewVarList.Clear();
                int iCount = pubFun.IsInt(txtCount.Text, 0);
                ICommunicationPlug plugin = selController.ChannelObject.Plugin;
                string varName = txtVariableName.Text.Trim();
                string addr = cmbVariableAddress.Text;

                for (int i = 1; i < (iCount + 1); i++)
                {
                    //watch.Restart();
                    IVariable var = plugin.CreateVariable(selController);
                    if (i == 1)
                    {
                        var.Name = varName;
                        //var.ID = pubFun.checkUrl(varName);
                        var.Address = addr;
                    }
                    else
                    {
                        //watch.Stop();
                        //Debug.WriteLine("plugin.CreateVariable" + watch.ElapsedMilliseconds);
                        //watch.Restart();
                        var.Name = varName = Rtdb.GetUniqueCloneName(varName);
                        // var.ID = Rtdb.GetUniqueCloneName(pubFun.checkUrl(var.Name));
                        //watch.Stop();
                        //Debug.WriteLine("GetUniqueCloneName" + watch.ElapsedMilliseconds);
                        //watch.Restart();
                        var.Address = addr = Rtdb.GetUniqueVariableAddr(selController, addr).ToString();
                        //watch.Stop();
                        //Debug.WriteLine("GetUniqueVariableAddr" + watch.ElapsedMilliseconds);

                    }
                    var.Enable = this.chkVariableEnable.Checked;
                    var.ReadOnly = this.chkVariableReadOnly.Checked;
                    var.OperLevel = int.Parse(this.cmbVariableLevel.Text);
                    //var.ControllerObject = selController;
                    selController.VarList.Add(var);
                    NewVarList.Add(var);

                }
                watch.Stop();
                Debug.WriteLine("wizardDev_FinishButtonClick" + watch.ElapsedMilliseconds);
                //Console.WriteLine(DateTime.Now.ToString());
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        private void FormVarWizard_Load(object sender, EventArgs e)
        {
            try
            {
                //cmbVariableAddress.Items.Clear();
                cmbVariableLevel.Items.Clear();
                for (int i = 1; i < 256; i++)
                {
                    //cmbVariableAddress.Items.Add(i.ToString());
                    cmbVariableLevel.Items.Add(i.ToString());
                }
                //txtVariableName.Text = selController.ChannelObject.Plugin.GetUniqueName(UniqueNameType.Variable);
                //cmbVariableAddress.Text = Rtdb.GetUniqueVariableAddr(selController, "0").ToString();
                cmbVariableLevel.Text = "1";

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

            }


        }

        private void txtNetPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            pubFun.NubOnly_KeyPress(sender, e);
        }

        private void wizardDev_CancelButtonClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
