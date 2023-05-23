namespace GHIBMS.Server
{
    partial class FormChannelWizard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormChannelWizard));
            this.wizardDev = new WizardBase.WizardControl();
            this.startStep1 = new WizardBase.StartStep();
            this.label4 = new System.Windows.Forms.Label();
            this.intermediateStep1 = new WizardBase.IntermediateStep();
            this.cmbCommType = new System.Windows.Forms.ComboBox();
            this.lblCommType = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.btnDevType = new System.Windows.Forms.Button();
            this.txtProtocolName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.intermediateStep2 = new WizardBase.IntermediateStep();
            this.chkEnable = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtChannelName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.finishStep1 = new WizardBase.FinishStep();
            this.label5 = new System.Windows.Forms.Label();
            this.intermediateStep4 = new WizardBase.IntermediateStep();
            this.tabControlParam = new System.Windows.Forms.TabControl();
            this.startStep1.SuspendLayout();
            this.intermediateStep1.SuspendLayout();
            this.intermediateStep2.SuspendLayout();
            this.finishStep1.SuspendLayout();
            this.intermediateStep4.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizardDev
            // 
            this.wizardDev.BackButtonEnabled = false;
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
            this.wizardDev.Size = new System.Drawing.Size(492, 386);
            this.wizardDev.WizardSteps.Add(this.startStep1);
            this.wizardDev.WizardSteps.Add(this.intermediateStep1);
            this.wizardDev.WizardSteps.Add(this.intermediateStep2);
            this.wizardDev.WizardSteps.Add(this.finishStep1);
            this.wizardDev.CurrentStepIndexChanged += new System.EventHandler(this.wizardDev_CurrentStepIndexChanged);
            this.wizardDev.FinishButtonClick += new System.EventHandler(this.wizardDev_FinishButtonClick);
            this.wizardDev.NextButtonClick += new WizardBase.WizardNextButtonClickEventHandler(this.wizardDev_NextButtonClick);
            this.wizardDev.BackButtonClick += new WizardBase.WizardClickEventHandler(this.wizardDev_BackButtonClick);
            this.wizardDev.CancelButtonClick += new System.EventHandler(this.wizardDev_CancelButtonClick);
            // 
            // startStep1
            // 
            this.startStep1.BindingImage = ((System.Drawing.Image)(resources.GetObject("startStep1.BindingImage")));
            this.startStep1.Controls.Add(this.label4);
            this.startStep1.Icon = ((System.Drawing.Image)(resources.GetObject("startStep1.Icon")));
            this.startStep1.Name = "startStep1";
            this.startStep1.Subtitle = "";
            this.startStep1.SubtitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startStep1.Title = "";
            this.startStep1.TitleFont = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(193, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 14);
            this.label4.TabIndex = 0;
            this.label4.Text = "欢迎使用通道向导！";
            // 
            // intermediateStep1
            // 
            this.intermediateStep1.AutoScroll = true;
            this.intermediateStep1.BackColor = System.Drawing.Color.White;
            this.intermediateStep1.BindingImage = ((System.Drawing.Image)(resources.GetObject("intermediateStep1.BindingImage")));
            this.intermediateStep1.Controls.Add(this.cmbCommType);
            this.intermediateStep1.Controls.Add(this.lblCommType);
            this.intermediateStep1.Controls.Add(this.label17);
            this.intermediateStep1.Controls.Add(this.btnDevType);
            this.intermediateStep1.Controls.Add(this.txtProtocolName);
            this.intermediateStep1.Controls.Add(this.label2);
            this.intermediateStep1.ForeColor = System.Drawing.Color.White;
            this.intermediateStep1.Name = "intermediateStep1";
            this.intermediateStep1.Subtitle = "";
            this.intermediateStep1.SubtitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.intermediateStep1.Title = "第一步：通讯参数";
            this.intermediateStep1.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            // 
            // cmbCommType
            // 
            this.cmbCommType.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmbCommType.FormattingEnabled = true;
            this.cmbCommType.Location = new System.Drawing.Point(113, 246);
            this.cmbCommType.Name = "cmbCommType";
            this.cmbCommType.Size = new System.Drawing.Size(228, 20);
            this.cmbCommType.TabIndex = 12;
            // 
            // lblCommType
            // 
            this.lblCommType.AutoSize = true;
            this.lblCommType.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCommType.Location = new System.Drawing.Point(111, 216);
            this.lblCommType.Name = "lblCommType";
            this.lblCommType.Size = new System.Drawing.Size(101, 12);
            this.lblCommType.TabIndex = 11;
            this.lblCommType.Text = "请选择通讯接口：";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label17.Location = new System.Drawing.Point(389, 167);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(11, 12);
            this.label17.TabIndex = 7;
            this.label17.Text = "*";
            // 
            // btnDevType
            // 
            this.btnDevType.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnDevType.Location = new System.Drawing.Point(347, 162);
            this.btnDevType.Name = "btnDevType";
            this.btnDevType.Size = new System.Drawing.Size(36, 23);
            this.btnDevType.TabIndex = 6;
            this.btnDevType.Text = "...";
            this.btnDevType.UseVisualStyleBackColor = true;
            this.btnDevType.Click += new System.EventHandler(this.btnDevType_Click);
            // 
            // txtProtocolName
            // 
            this.txtProtocolName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtProtocolName.Location = new System.Drawing.Point(113, 163);
            this.txtProtocolName.Name = "txtProtocolName";
            this.txtProtocolName.ReadOnly = true;
            this.txtProtocolName.Size = new System.Drawing.Size(228, 21);
            this.txtProtocolName.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(111, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "请选择通讯协议：";
            // 
            // intermediateStep2
            // 
            this.intermediateStep2.BackColor = System.Drawing.Color.White;
            this.intermediateStep2.BindingImage = ((System.Drawing.Image)(resources.GetObject("intermediateStep2.BindingImage")));
            this.intermediateStep2.Controls.Add(this.chkEnable);
            this.intermediateStep2.Controls.Add(this.label13);
            this.intermediateStep2.Controls.Add(this.label16);
            this.intermediateStep2.Controls.Add(this.txtChannelName);
            this.intermediateStep2.Controls.Add(this.label1);
            this.intermediateStep2.ForeColor = System.Drawing.Color.White;
            this.intermediateStep2.Name = "intermediateStep2";
            this.intermediateStep2.Subtitle = "";
            this.intermediateStep2.SubtitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.intermediateStep2.Title = "第二步：基本参数";
            this.intermediateStep2.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            // 
            // chkEnable
            // 
            this.chkEnable.AutoSize = true;
            this.chkEnable.Checked = true;
            this.chkEnable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnable.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkEnable.Location = new System.Drawing.Point(164, 203);
            this.chkEnable.Name = "chkEnable";
            this.chkEnable.Size = new System.Drawing.Size(84, 16);
            this.chkEnable.TabIndex = 14;
            this.chkEnable.Text = "启用[选中]";
            this.chkEnable.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label13.Location = new System.Drawing.Point(92, 204);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(65, 12);
            this.label13.TabIndex = 13;
            this.label13.Text = "是否启用：";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label16.Location = new System.Drawing.Point(382, 143);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(11, 12);
            this.label16.TabIndex = 2;
            this.label16.Text = "*";
            // 
            // txtChannelName
            // 
            this.txtChannelName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtChannelName.Location = new System.Drawing.Point(162, 141);
            this.txtChannelName.Name = "txtChannelName";
            this.txtChannelName.Size = new System.Drawing.Size(214, 21);
            this.txtChannelName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(92, 145);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "通道名称：";
            // 
            // finishStep1
            // 
            this.finishStep1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("finishStep1.BackgroundImage")));
            this.finishStep1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.finishStep1.Controls.Add(this.label5);
            this.finishStep1.Name = "finishStep1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(66, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 14);
            this.label5.TabIndex = 1;
            this.label5.Text = "新增通道成功！";
            // 
            // intermediateStep4
            // 
            this.intermediateStep4.BackColor = System.Drawing.Color.White;
            this.intermediateStep4.BindingImage = ((System.Drawing.Image)(resources.GetObject("intermediateStep4.BindingImage")));
            this.intermediateStep4.Controls.Add(this.tabControlParam);
            this.intermediateStep4.Name = "intermediateStep4";
            this.intermediateStep4.Subtitle = "请输入通讯参数";
            this.intermediateStep4.SubtitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.intermediateStep4.Title = "第四步：通讯参数";
            this.intermediateStep4.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            // 
            // tabControlParam
            // 
            this.tabControlParam.Location = new System.Drawing.Point(3, 66);
            this.tabControlParam.Name = "tabControlParam";
            this.tabControlParam.SelectedIndex = 0;
            this.tabControlParam.Size = new System.Drawing.Size(492, 280);
            this.tabControlParam.TabIndex = 5;
            // 
            // FormChannelWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 386);
            this.Controls.Add(this.wizardDev);
            this.DoubleBuffered = true;
            this.Name = "FormChannelWizard";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "通讯通道向导";
            this.Load += new System.EventHandler(this.FormChannelWizard_Load);
            this.startStep1.ResumeLayout(false);
            this.startStep1.PerformLayout();
            this.intermediateStep1.ResumeLayout(false);
            this.intermediateStep1.PerformLayout();
            this.intermediateStep2.ResumeLayout(false);
            this.intermediateStep2.PerformLayout();
            this.finishStep1.ResumeLayout(false);
            this.finishStep1.PerformLayout();
            this.intermediateStep4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private WizardBase.WizardControl wizardDev;
        private WizardBase.StartStep startStep1;
        private WizardBase.IntermediateStep intermediateStep2;
        private WizardBase.FinishStep finishStep1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtChannelName;
        private WizardBase.IntermediateStep intermediateStep1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDevType;
        private System.Windows.Forms.TextBox txtProtocolName;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.CheckBox chkEnable;
        private System.Windows.Forms.Label label13;
        private WizardBase.IntermediateStep intermediateStep4;
        private System.Windows.Forms.TabControl tabControlParam;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbCommType;
        private System.Windows.Forms.Label lblCommType;
        private System.Windows.Forms.Label label5;
    }
}