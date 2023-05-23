namespace GHIBMS.Server
{
    partial class FormVarWizard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormVarWizard));
            this.wizardDev = new WizardBase.WizardControl();
            this.startStep1 = new WizardBase.StartStep();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.intermediateStep1 = new WizardBase.IntermediateStep();
            this.label2 = new System.Windows.Forms.Label();
            this.chkVariableEnable = new System.Windows.Forms.CheckBox();
            this.txtCount = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtVariableName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.intermediateStep2 = new WizardBase.IntermediateStep();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbVariableAddress = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cmbVariableLevel = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.chkVariableReadOnly = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.finishStep1 = new WizardBase.FinishStep();
            this.label3 = new System.Windows.Forms.Label();
            this.startStep1.SuspendLayout();
            this.intermediateStep1.SuspendLayout();
            this.intermediateStep2.SuspendLayout();
            this.finishStep1.SuspendLayout();
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
            this.wizardDev.CancelButtonClick += new System.EventHandler(this.wizardDev_CancelButtonClick);
            // 
            // startStep1
            // 
            this.startStep1.BindingImage = ((System.Drawing.Image)(resources.GetObject("startStep1.BindingImage")));
            this.startStep1.Controls.Add(this.label5);
            this.startStep1.Controls.Add(this.label7);
            this.startStep1.Icon = ((System.Drawing.Image)(resources.GetObject("startStep1.Icon")));
            this.startStep1.Name = "startStep1";
            this.startStep1.Subtitle = "";
            this.startStep1.SubtitleFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.startStep1.Title = "";
            this.startStep1.TitleFont = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(198, 99);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(217, 14);
            this.label5.TabIndex = 1;
            this.label5.Text = "请按照向导的步骤输入相关参数。";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(198, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(133, 14);
            this.label7.TabIndex = 0;
            this.label7.Text = "欢迎使用变量向导！";
            // 
            // intermediateStep1
            // 
            this.intermediateStep1.BackColor = System.Drawing.Color.White;
            this.intermediateStep1.BindingImage = ((System.Drawing.Image)(resources.GetObject("intermediateStep1.BindingImage")));
            this.intermediateStep1.Controls.Add(this.label2);
            this.intermediateStep1.Controls.Add(this.chkVariableEnable);
            this.intermediateStep1.Controls.Add(this.txtCount);
            this.intermediateStep1.Controls.Add(this.label10);
            this.intermediateStep1.Controls.Add(this.label6);
            this.intermediateStep1.Controls.Add(this.label4);
            this.intermediateStep1.Controls.Add(this.txtVariableName);
            this.intermediateStep1.Controls.Add(this.label1);
            this.intermediateStep1.ForeColor = System.Drawing.Color.White;
            this.intermediateStep1.Name = "intermediateStep1";
            this.intermediateStep1.Subtitle = "";
            this.intermediateStep1.SubtitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.intermediateStep1.Title = "第一步：基本信息";
            this.intermediateStep1.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(106, 239);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 28;
            this.label2.Text = "是否启用：";
            // 
            // chkVariableEnable
            // 
            this.chkVariableEnable.AutoSize = true;
            this.chkVariableEnable.Checked = true;
            this.chkVariableEnable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkVariableEnable.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkVariableEnable.Location = new System.Drawing.Point(177, 237);
            this.chkVariableEnable.Name = "chkVariableEnable";
            this.chkVariableEnable.Size = new System.Drawing.Size(84, 16);
            this.chkVariableEnable.TabIndex = 27;
            this.chkVariableEnable.Text = "启用[选中]";
            this.chkVariableEnable.UseVisualStyleBackColor = true;
            // 
            // txtCount
            // 
            this.txtCount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtCount.Location = new System.Drawing.Point(176, 132);
            this.txtCount.MaxLength = 4;
            this.txtCount.Name = "txtCount";
            this.txtCount.Size = new System.Drawing.Size(64, 21);
            this.txtCount.TabIndex = 14;
            this.txtCount.Text = "1";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label10.Location = new System.Drawing.Point(106, 135);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 13;
            this.label10.Text = "数    量：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(377, 187);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(11, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(34, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(227, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "请输入设备控制点的基本信息，*为必选项";
            // 
            // txtVariableName
            // 
            this.txtVariableName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtVariableName.Location = new System.Drawing.Point(177, 184);
            this.txtVariableName.Name = "txtVariableName";
            this.txtVariableName.Size = new System.Drawing.Size(194, 21);
            this.txtVariableName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(106, 187);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "名    称：";
            // 
            // intermediateStep2
            // 
            this.intermediateStep2.BackColor = System.Drawing.Color.White;
            this.intermediateStep2.BindingImage = ((System.Drawing.Image)(resources.GetObject("intermediateStep2.BindingImage")));
            this.intermediateStep2.Controls.Add(this.label8);
            this.intermediateStep2.Controls.Add(this.cmbVariableAddress);
            this.intermediateStep2.Controls.Add(this.label14);
            this.intermediateStep2.Controls.Add(this.cmbVariableLevel);
            this.intermediateStep2.Controls.Add(this.label17);
            this.intermediateStep2.Controls.Add(this.chkVariableReadOnly);
            this.intermediateStep2.Controls.Add(this.label16);
            this.intermediateStep2.ForeColor = System.Drawing.Color.White;
            this.intermediateStep2.Name = "intermediateStep2";
            this.intermediateStep2.Subtitle = "请设置参数";
            this.intermediateStep2.SubtitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.intermediateStep2.Title = "第二步：参数设置";
            this.intermediateStep2.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label8.Location = new System.Drawing.Point(322, 142);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 12);
            this.label8.TabIndex = 12;
            this.label8.Text = "*";
            // 
            // cmbVariableAddress
            // 
            this.cmbVariableAddress.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmbVariableAddress.FormattingEnabled = true;
            this.cmbVariableAddress.Location = new System.Drawing.Point(218, 138);
            this.cmbVariableAddress.Name = "cmbVariableAddress";
            this.cmbVariableAddress.Size = new System.Drawing.Size(88, 20);
            this.cmbVariableAddress.TabIndex = 8;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label14.Location = new System.Drawing.Point(147, 142);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 12);
            this.label14.TabIndex = 7;
            this.label14.Text = "地    址：";
            // 
            // cmbVariableLevel
            // 
            this.cmbVariableLevel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmbVariableLevel.FormattingEnabled = true;
            this.cmbVariableLevel.Location = new System.Drawing.Point(218, 186);
            this.cmbVariableLevel.Name = "cmbVariableLevel";
            this.cmbVariableLevel.Size = new System.Drawing.Size(88, 20);
            this.cmbVariableLevel.TabIndex = 6;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label17.Location = new System.Drawing.Point(147, 189);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(65, 12);
            this.label17.TabIndex = 5;
            this.label17.Text = "操作级别：";
            // 
            // chkVariableReadOnly
            // 
            this.chkVariableReadOnly.AutoSize = true;
            this.chkVariableReadOnly.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkVariableReadOnly.Location = new System.Drawing.Point(218, 234);
            this.chkVariableReadOnly.Name = "chkVariableReadOnly";
            this.chkVariableReadOnly.Size = new System.Drawing.Size(48, 16);
            this.chkVariableReadOnly.TabIndex = 4;
            this.chkVariableReadOnly.Text = "只读";
            this.chkVariableReadOnly.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label16.Location = new System.Drawing.Point(147, 237);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(65, 12);
            this.label16.TabIndex = 3;
            this.label16.Text = "操作权限：";
            // 
            // finishStep1
            // 
            this.finishStep1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("finishStep1.BackgroundImage")));
            this.finishStep1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.finishStep1.Controls.Add(this.label3);
            this.finishStep1.Name = "finishStep1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(62, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 14);
            this.label3.TabIndex = 1;
            this.label3.Text = "新增变量成功！";
            // 
            // FormVarWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 386);
            this.Controls.Add(this.wizardDev);
            this.DoubleBuffered = true;
            this.Name = "FormVarWizard";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "新增变量向导";
            this.Load += new System.EventHandler(this.FormVarWizard_Load);
            this.startStep1.ResumeLayout(false);
            this.startStep1.PerformLayout();
            this.intermediateStep1.ResumeLayout(false);
            this.intermediateStep1.PerformLayout();
            this.intermediateStep2.ResumeLayout(false);
            this.intermediateStep2.PerformLayout();
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
        private System.Windows.Forms.TextBox txtVariableName;
        private WizardBase.IntermediateStep intermediateStep2;
        private System.Windows.Forms.ComboBox cmbVariableLevel;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.CheckBox chkVariableReadOnly;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbVariableAddress;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtCount;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkVariableEnable;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
    }
}