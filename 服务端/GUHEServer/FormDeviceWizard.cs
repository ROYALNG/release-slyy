using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SOPC
{
    public partial class FormDeviceWizard : Form
    {
        private DeviceInfo deviceInfo =null;
        private TreeView trvDev;
        private List<DeviceInfo> devList = new List<DeviceInfo>();

        public DeviceInfo DevInfo
        {
            get { return deviceInfo; }
            set { deviceInfo = value; }
        }
        public TreeView TrvDev
        {
            set { trvDev = value; }
            get { return trvDev;}
        }
        public  List<DeviceInfo> DevList 
        {
            set{devList = value;}
            get{return devList;}
        }
        public FormDeviceWizard()
        {
            InitializeComponent();
        }

        private void wizardDev_NextButtonClick(WizardBase.WizardControl sender, WizardBase.WizardNextButtonClickEventArgs args)
        {
          
           if (wizardDev.CurrentStepIndex ==0) return;
           switch (wizardDev.CurrentStepIndex)
           {
               case 1:
                   if (pubFun.NodeTextCheck(TrvDev,txtDeviceName.Text.Trim())==false)
                   {
                       wizardDev.CurrentStepIndex--;

                   }
                   foreach (DeviceInfo dev in devList)
                   {
                       if (dev == deviceInfo) continue;
                       if (dev.Name == txtDeviceName.Text.Trim())
                       {
                           MessageBox.Show("设备名称重复，请重新输入设备名称！");
                           wizardDev.CurrentStepIndex--;
                       }
                   }

                   break;
               case 2:
                   if (txtDeviceType.Text.Trim().Length == 0)
                   {
                       MessageBox.Show("设备类型不能为空，请选择设备类型！");
                       wizardDev.CurrentStepIndex--;
                   }
                   break;
               case 3:
                   if (cmbCommType.Text.Trim().Length == 0)
                   {
                       MessageBox.Show("通讯类型不能为空，请选择通讯类型！");
                       wizardDev.CurrentStepIndex--;
                    

                   }
                   break;
               case 4:
                   if (cmbCommType.SelectedIndex == 0)  //TCP
                   {
                       if ((cmbProtocol.Text.Trim().Length==0)
                           || (ipAddressDevice.Text.Trim().Length==0)
                           || (txtNetPort.Text.Trim().Length==0))
                       {
                    
                       MessageBox.Show("参数可能为空，请输入完整所有参数！");
                       wizardDev.CurrentStepIndex--;
                       }
                   }
                   else
                   {
                       if ((cmbProtocol.Text.Trim().Length == 0)
                           || (ipAddressDevice.Text.Trim().Length == 0)
                           || (txtNetPort.Text.Trim().Length == 0))
                       {

                           MessageBox.Show("参数可能为空，请输入完整所有参数！");
                           wizardDev.CurrentStepIndex--;
                       }


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

        private void cmbCommType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCommType.SelectedIndex == 0)
            {

                pnlSerial.Visible = false;
                pnlNET.Visible = true;
              
            }
            else
            {
                pnlNET.Visible = false;
                pnlSerial.Visible = true;
            }
        }

        private void wizardDev_FinishButtonClick(object sender, EventArgs e)
        {
           
            deviceInfo.Name = this.txtDeviceName.Text;
            deviceInfo.DeviceType = this.txtDeviceType.Text;
            deviceInfo.DeviceCode = Convert.ToInt32(this.txtDeviceType.Tag);
            deviceInfo.CommType = this.cmbCommType.SelectedIndex;
            deviceInfo.NetProtocol = this.cmbProtocol.SelectedIndex;
            deviceInfo.NetPort = Convert.ToInt32(this.txtNetPort.Text);
            deviceInfo.Ipaddress = this.ipAddressDevice.Text;
            deviceInfo.SerialPort = this.cmbPort.SelectedIndex;
            deviceInfo.Baud = this.cmdBaud.SelectedIndex;
            deviceInfo.DataBit = this.cmbDataBit.SelectedIndex;
            deviceInfo.StopBit = this.cmdStopBit.SelectedIndex;
            deviceInfo.DataCheck = this.cmdCheck.SelectedIndex;
            deviceInfo.FlowCtrl = this.cmbFlowCtrl.SelectedIndex;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnDevType_Click(object sender, EventArgs e)
        {
            FormDeviceType frm = new FormDeviceType(deviceInfo.Sort);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.txtDeviceType.Text = frm.SelectDevText;
                this.txtDeviceType.Tag = frm.SelectDevID;
            }
        }

        private void FormDeviceWizard_Load(object sender, EventArgs e)
        {
            if (deviceInfo != null)
            {
                this.txtDeviceName.Text = deviceInfo.Name;
                this.txtDeviceType.Text = deviceInfo.DeviceType;
                this.txtDeviceType.Tag = deviceInfo.DeviceCode;
                this.cmbCommType.SelectedIndex = deviceInfo.CommType;
                this.cmbProtocol.SelectedIndex = deviceInfo.NetProtocol;
                this.txtNetPort.Text = deviceInfo.NetPort.ToString();
                this.ipAddressDevice.Text = deviceInfo.Ipaddress;
                this.cmbPort.SelectedIndex = deviceInfo.SerialPort;
                this.cmdBaud.SelectedIndex = deviceInfo.Baud;
                this.cmbDataBit.SelectedIndex = deviceInfo.DataBit;
                this.cmdStopBit.SelectedIndex = deviceInfo.StopBit;
                this.cmdCheck.SelectedIndex = deviceInfo.DataCheck;
                this.cmbFlowCtrl.SelectedIndex = deviceInfo.FlowCtrl;

                //生成默认名称
                if (deviceInfo.Name == "")
                {
                    int i = 0;
                    foreach (DeviceInfo dev in DevList)
                    {
                        if (dev.Name.IndexOf("Device_") >= 0)
                        {
                            try
                            {
                                string str = dev.Name.Substring(7);

                                if (int.Parse(str) > i)
                                    i = int.Parse(str);
                            }
                            catch
                            { }
                        }

                    }
                    this.txtDeviceName.Text = "Device_" + (i+1).ToString();
                }
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
