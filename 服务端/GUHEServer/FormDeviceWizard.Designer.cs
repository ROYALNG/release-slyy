namespace SOPC
{
    partial class FormDeviceWizard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDeviceWizard));
            this.wizardDev = new WizardBase.WizardControl();
            this.startStep1 = new WizardBase.StartStep();
            this.intermediateStep1 = new WizardBase.IntermediateStep();
            this.txtDeviceName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.intermediateStep2 = new WizardBase.IntermediateStep();
            this.btnDevType = new System.Windows.Forms.Button();
            this.txtDeviceType = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.intermediateStep3 = new WizardBase.IntermediateStep();
            this.cmbCommType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.intermediateStep4 = new WizardBase.IntermediateStep();
            this.pnlNET = new System.Windows.Forms.Panel();
            this.txtNetPort = new System.Windows.Forms.TextBox();
            this.ipAddressDevice = new IPAddressControlLib.IPAddressControl();
            this.cmbProtocol = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.pnlSerial = new System.Windows.Forms.Panel();
            this.cmbFlowCtrl = new System.Windows.Forms.ComboBox();
            this.cmdStopBit = new System.Windows.Forms.ComboBox();
            this.cmbDataBit = new System.Windows.Forms.ComboBox();
            this.cmdCheck = new System.Windows.Forms.ComboBox();
            this.cmdBaud = new System.Windows.Forms.ComboBox();
            this.cmbPort = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.finishStep1 = new WizardBase.FinishStep();
            this.label13 = new System.Windows.Forms.Label();
            this.intermediateStep1.SuspendLayout();
            this.intermediateStep2.SuspendLayout();
            this.intermediateStep3.SuspendLayout();
            this.intermediateStep4.SuspendLayout();
            this.pnlNET.SuspendLayout();
            this.pnlSerial.SuspendLayout();
            this.finishStep1.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizardDev
            // 
            this.wizardDev.BackButtonEnabled = true;
            this.wizardDev.BackButtonText = "< 上一步";
            this.wizardDev.BackButtonVisible = true;
            this.wizardDev.CancelButtonEnabled = true;
            this.wizardDev.CancelButtonText = "取消";
            this.wizardDev.CancelButtonVisible = true;
            this.wizardDev.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardDev.FinishButtonText = "完成";
            this.wizardDev.HelpButtonEnabled = true;
            this.wizardDev.HelpButtonText = "帮助";
            this.wizardDev.HelpButtonVisible = true;
            this.wizardDev.Location = new System.Drawing.Point(0, 0);
            this.wizardDev.Name = "wizardDev";
            this.wizardDev.NextButtonEnabled = true;
            this.wizardDev.NextButtonText = "下一步 >";
            this.wizardDev.NextButtonVisible = true;
            this.wizardDev.Size = new System.Drawing.Size(402, 332);
            this.wizardDev.WizardSteps.Add(this.startStep1);
            this.wizardDev.WizardSteps.Add(this.intermediateStep1);
            this.wizardDev.WizardSteps.Add(this.intermediateStep2);
            this.wizardDev.WizardSteps.Add(this.intermediateStep3);
            this.wizardDev.WizardSteps.Add(this.intermediateStep4);
            this.wizardDev.WizardSteps.Add(this.finishStep1);
            this.wizardDev.CurrentStepIndexChanged += new System.EventHandler(this.wizardDev_CurrentStepIndexChanged);
            this.wizardDev.FinishButtonClick += new System.EventHandler(this.wizardDev_FinishButtonClick);
            this.wizardDev.NextButtonClick += new WizardBase.WizardNextButtonClickEventHandler(this.wizardDev_NextButtonClick);
            this.wizardDev.CancelButtonClick += new System.EventHandler(this.wizardDev_CancelButtonClick);
            // 
            // startStep1
            // 
            this.startStep1.BindingImage = ((System.Drawing.Image)(resources.GetObject("startStep1.BindingImage")));
            this.startStep1.Icon = ((System.Drawing.Image)(resources.GetObject("startStep1.Icon")));
            this.startStep1.Name = "startStep1";
            this.startStep1.Subtitle = "欢迎使用设备添加向导！";
            this.startStep1.SubtitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startStep1.Title = "Welcome to the Device Wizard.";
            this.startStep1.TitleFont = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
            // 
            // intermediateStep1
            // 
            this.intermediateStep1.BackColor = System.Drawing.Color.White;
            this.intermediateStep1.BindingImage = ((System.Drawing.Image)(resources.GetObject("intermediateStep1.BindingImage")));
            this.intermediateStep1.Controls.Add(this.txtDeviceName);
            this.intermediateStep1.Controls.Add(this.label1);
            this.intermediateStep1.Name = "intermediateStep1";
            this.intermediateStep1.Subtitle = "请输入设备名称";
            this.intermediateStep1.SubtitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.intermediateStep1.Title = "第一步：设备名称";
            this.intermediateStep1.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            // 
            // txtDeviceName
            // 
            this.txtDeviceName.Location = new System.Drawing.Point(139, 142);
            this.txtDeviceName.Name = "txtDeviceName";
            this.txtDeviceName.Size = new System.Drawing.Size(194, 21);
            this.txtDeviceName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(92, 145);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称：";
            // 
            // intermediateStep2
            // 
            this.intermediateStep2.BackColor = System.Drawing.Color.White;
            this.intermediateStep2.BindingImage = ((System.Drawing.Image)(resources.GetObject("intermediateStep2.BindingImage")));
            this.intermediateStep2.Controls.Add(this.btnDevType);
            this.intermediateStep2.Controls.Add(this.txtDeviceType);
            this.intermediateStep2.Controls.Add(this.label2);
            this.intermediateStep2.Name = "intermediateStep2";
            this.intermediateStep2.Subtitle = "请选择设备类型";
            this.intermediateStep2.SubtitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.intermediateStep2.Title = "第二步：设备类型";
            this.intermediateStep2.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            // 
            // btnDevType
            // 
            this.btnDevType.Location = new System.Drawing.Point(299, 146);
            this.btnDevType.Name = "btnDevType";
            this.btnDevType.Size = new System.Drawing.Size(36, 23);
            this.btnDevType.TabIndex = 6;
            this.btnDevType.Text = "...";
            this.btnDevType.UseVisualStyleBackColor = true;
            this.btnDevType.Click += new System.EventHandler(this.btnDevType_Click);
            // 
            // txtDeviceType
            // 
            this.txtDeviceType.Location = new System.Drawing.Point(109, 147);
            this.txtDeviceType.Name = "txtDeviceType";
            this.txtDeviceType.ReadOnly = true;
            this.txtDeviceType.Size = new System.Drawing.Size(184, 21);
            this.txtDeviceType.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(62, 151);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "类型：";
            // 
            // intermediateStep3
            // 
            this.intermediateStep3.BackColor = System.Drawing.Color.White;
            this.intermediateStep3.BindingImage = ((System.Drawing.Image)(resources.GetObject("intermediateStep3.BindingImage")));
            this.intermediateStep3.Controls.Add(this.cmbCommType);
            this.intermediateStep3.Controls.Add(this.label3);
            this.intermediateStep3.Name = "intermediateStep3";
            this.intermediateStep3.Subtitle = "请选择通讯方式";
            this.intermediateStep3.SubtitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.intermediateStep3.Title = "第三步：通讯方式：";
            this.intermediateStep3.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            // 
            // cmbCommType
            // 
            this.cmbCommType.FormattingEnabled = true;
            this.cmbCommType.Items.AddRange(new object[] {
            "网络",
            "串口"});
            this.cmbCommType.Location = new System.Drawing.Point(139, 142);
            this.cmbCommType.Name = "cmbCommType";
            this.cmbCommType.Size = new System.Drawing.Size(194, 20);
            this.cmbCommType.TabIndex = 9;
            this.cmbCommType.SelectedIndexChanged += new System.EventHandler(this.cmbCommType_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(92, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "通讯：";
            // 
            // intermediateStep4
            // 
            this.intermediateStep4.BindingImage = ((System.Drawing.Image)(resources.GetObject("intermediateStep4.BindingImage")));
            this.intermediateStep4.Controls.Add(this.pnlNET);
            this.intermediateStep4.Controls.Add(this.pnlSerial);
            this.intermediateStep4.Name = "intermediateStep4";
            this.intermediateStep4.Subtitle = "请输入通讯参数";
            this.intermediateStep4.SubtitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.intermediateStep4.Title = "第四步：通讯参数";
            this.intermediateStep4.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            // 
            // pnlNET
            // 
            this.pnlNET.BackColor = System.Drawing.Color.White;
            this.pnlNET.Controls.Add(this.txtNetPort);
            this.pnlNET.Controls.Add(this.ipAddressDevice);
            this.pnlNET.Controls.Add(this.cmbProtocol);
            this.pnlNET.Controls.Add(this.label10);
            this.pnlNET.Controls.Add(this.label11);
            this.pnlNET.Controls.Add(this.label12);
            this.pnlNET.Location = new System.Drawing.Point(3, 64);
            this.pnlNET.Name = "pnlNET";
            this.pnlNET.Size = new System.Drawing.Size(396, 225);
            this.pnlNET.TabIndex = 4;
            // 
            // txtNetPort
            // 
            this.txtNetPort.Location = new System.Drawing.Point(156, 134);
            this.txtNetPort.Name = "txtNetPort";
            this.txtNetPort.Size = new System.Drawing.Size(163, 21);
            this.txtNetPort.TabIndex = 17;
            this.txtNetPort.Text = "10000";
            this.txtNetPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNetPort_KeyPress);
            // 
            // ipAddressDevice
            // 
            this.ipAddressDevice.AllowInternalTab = false;
            this.ipAddressDevice.AutoHeight = true;
            this.ipAddressDevice.BackColor = System.Drawing.SystemColors.Window;
            this.ipAddressDevice.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipAddressDevice.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipAddressDevice.Location = new System.Drawing.Point(156, 103);
            this.ipAddressDevice.Name = "ipAddressDevice";
            this.ipAddressDevice.ReadOnly = false;
            this.ipAddressDevice.Size = new System.Drawing.Size(163, 19);
            this.ipAddressDevice.TabIndex = 16;
            this.ipAddressDevice.Text = "...";
            // 
            // cmbProtocol
            // 
            this.cmbProtocol.FormattingEnabled = true;
            this.cmbProtocol.Items.AddRange(new object[] {
            "TCP",
            "UDP"});
            this.cmbProtocol.Location = new System.Drawing.Point(156, 73);
            this.cmbProtocol.Name = "cmbProtocol";
            this.cmbProtocol.Size = new System.Drawing.Size(163, 20);
            this.cmbProtocol.TabIndex = 12;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(77, 137);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 11;
            this.label10.Text = "端口号：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(77, 105);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 10;
            this.label11.Text = "IP地址：";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(77, 73);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 9;
            this.label12.Text = "协  议：";
            // 
            // pnlSerial
            // 
            this.pnlSerial.BackColor = System.Drawing.Color.White;
            this.pnlSerial.Controls.Add(this.cmbFlowCtrl);
            this.pnlSerial.Controls.Add(this.cmdStopBit);
            this.pnlSerial.Controls.Add(this.cmbDataBit);
            this.pnlSerial.Controls.Add(this.cmdCheck);
            this.pnlSerial.Controls.Add(this.cmdBaud);
            this.pnlSerial.Controls.Add(this.cmbPort);
            this.pnlSerial.Controls.Add(this.label9);
            this.pnlSerial.Controls.Add(this.label8);
            this.pnlSerial.Controls.Add(this.label7);
            this.pnlSerial.Controls.Add(this.label6);
            this.pnlSerial.Controls.Add(this.label5);
            this.pnlSerial.Controls.Add(this.label4);
            this.pnlSerial.Location = new System.Drawing.Point(3, 62);
            this.pnlSerial.Name = "pnlSerial";
            this.pnlSerial.Size = new System.Drawing.Size(393, 227);
            this.pnlSerial.TabIndex = 3;
            this.pnlSerial.Visible = false;
            // 
            // cmbFlowCtrl
            // 
            this.cmbFlowCtrl.FormattingEnabled = true;
            this.cmbFlowCtrl.Items.AddRange(new object[] {
            "无",
            "软件",
            "硬件"});
            this.cmbFlowCtrl.Location = new System.Drawing.Point(157, 180);
            this.cmbFlowCtrl.Name = "cmbFlowCtrl";
            this.cmbFlowCtrl.Size = new System.Drawing.Size(163, 20);
            this.cmbFlowCtrl.TabIndex = 11;
            // 
            // cmdStopBit
            // 
            this.cmdStopBit.FormattingEnabled = true;
            this.cmdStopBit.Items.AddRange(new object[] {
            "1",
            "1.5",
            "2"});
            this.cmdStopBit.Location = new System.Drawing.Point(157, 147);
            this.cmdStopBit.Name = "cmdStopBit";
            this.cmdStopBit.Size = new System.Drawing.Size(163, 20);
            this.cmdStopBit.TabIndex = 10;
            // 
            // cmbDataBit
            // 
            this.cmbDataBit.FormattingEnabled = true;
            this.cmbDataBit.Items.AddRange(new object[] {
            "8",
            "7",
            "6",
            "4"});
            this.cmbDataBit.Location = new System.Drawing.Point(157, 121);
            this.cmbDataBit.Name = "cmbDataBit";
            this.cmbDataBit.Size = new System.Drawing.Size(163, 20);
            this.cmbDataBit.TabIndex = 9;
            // 
            // cmdCheck
            // 
            this.cmdCheck.FormattingEnabled = true;
            this.cmdCheck.Items.AddRange(new object[] {
            "NONE(无)",
            "ODD (奇)",
            "EVEN(偶)"});
            this.cmdCheck.Location = new System.Drawing.Point(157, 89);
            this.cmdCheck.Name = "cmdCheck";
            this.cmdCheck.Size = new System.Drawing.Size(163, 20);
            this.cmdCheck.TabIndex = 8;
            // 
            // cmdBaud
            // 
            this.cmdBaud.FormattingEnabled = true;
            this.cmdBaud.Items.AddRange(new object[] {
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.cmdBaud.Location = new System.Drawing.Point(157, 60);
            this.cmdBaud.Name = "cmdBaud";
            this.cmdBaud.Size = new System.Drawing.Size(163, 20);
            this.cmdBaud.TabIndex = 7;
            // 
            // cmbPort
            // 
            this.cmbPort.FormattingEnabled = true;
            this.cmbPort.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16"});
            this.cmbPort.Location = new System.Drawing.Point(157, 28);
            this.cmbPort.Name = "cmbPort";
            this.cmbPort.Size = new System.Drawing.Size(163, 20);
            this.cmbPort.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(78, 188);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 5;
            this.label9.Text = "流控制：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(78, 156);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 4;
            this.label8.Text = "停止位：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(78, 124);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 3;
            this.label7.Text = "数据位：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(78, 92);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "校  验：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(78, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "波特率：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(78, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "串口号：";
            // 
            // finishStep1
            // 
            this.finishStep1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("finishStep1.BackgroundImage")));
            this.finishStep1.Controls.Add(this.label13);
            this.finishStep1.Name = "finishStep1";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(114, 161);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(147, 14);
            this.label13.TabIndex = 0;
            this.label13.Text = "设备添加或修改完成！";
            // 
            // FormDeviceWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 332);
            this.Controls.Add(this.wizardDev);
            this.Name = "FormDeviceWizard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设备向导";
            this.Load += new System.EventHandler(this.FormDeviceWizard_Load);
            this.intermediateStep1.ResumeLayout(false);
            this.intermediateStep1.PerformLayout();
            this.intermediateStep2.ResumeLayout(false);
            this.intermediateStep2.PerformLayout();
            this.intermediateStep3.ResumeLayout(false);
            this.intermediateStep3.PerformLayout();
            this.intermediateStep4.ResumeLayout(false);
            this.pnlNET.ResumeLayout(false);
            this.pnlNET.PerformLayout();
            this.pnlSerial.ResumeLayout(false);
            this.pnlSerial.PerformLayout();
            this.finishStep1.ResumeLayout(false);
            this.finishStep1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private WizardBase.WizardControl wizardDev;
        private WizardBase.StartStep startStep1;
        private WizardBase.IntermediateStep intermediateStep1;
        private WizardBase.FinishStep finishStep1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDeviceName;
        private WizardBase.IntermediateStep intermediateStep2;
        private WizardBase.IntermediateStep intermediateStep3;
        private WizardBase.IntermediateStep intermediateStep4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbCommType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnlSerial;
        private System.Windows.Forms.ComboBox cmbFlowCtrl;
        private System.Windows.Forms.ComboBox cmdStopBit;
        private System.Windows.Forms.ComboBox cmbDataBit;
        private System.Windows.Forms.ComboBox cmdCheck;
        private System.Windows.Forms.ComboBox cmdBaud;
        private System.Windows.Forms.ComboBox cmbPort;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel pnlNET;
        private System.Windows.Forms.TextBox txtNetPort;
        private IPAddressControlLib.IPAddressControl ipAddressDevice;
        private System.Windows.Forms.ComboBox cmbProtocol;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnDevType;
        private System.Windows.Forms.TextBox txtDeviceType;
    }
}