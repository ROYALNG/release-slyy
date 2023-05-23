namespace GHIBMS.Server
{
    partial class FormControllerWizard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormControllerWizard));
            this.wizardControl1 = new WizardBase.WizardControl();
            this.startStep1 = new WizardBase.StartStep();
            this.label1 = new System.Windows.Forms.Label();
            this.intermediateStep2 = new WizardBase.IntermediateStep();
            this.txtCount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.chkEnable = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.intermediateStep3 = new WizardBase.IntermediateStep();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbLevel = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbLogID = new System.Windows.Forms.ComboBox();
            this.finishStep1 = new WizardBase.FinishStep();
            this.label6 = new System.Windows.Forms.Label();
            this.intermediateStep1 = new WizardBase.IntermediateStep();
            this.startStep1.SuspendLayout();
            this.intermediateStep2.SuspendLayout();
            this.intermediateStep3.SuspendLayout();
            this.finishStep1.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizardControl1
            // 
            this.wizardControl1.BackButtonEnabled = false;
            this.wizardControl1.BackButtonText = "< 上一步";
            this.wizardControl1.BackButtonVisible = true;
            this.wizardControl1.CancelButtonEnabled = true;
            this.wizardControl1.CancelButtonText = "取消";
            this.wizardControl1.CancelButtonVisible = true;
            this.wizardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardControl1.FinishButtonText = "完成";
            this.wizardControl1.HelpButtonEnabled = true;
            this.wizardControl1.HelpButtonText = "帮助";
            this.wizardControl1.HelpButtonVisible = true;
            this.wizardControl1.Location = new System.Drawing.Point(0, 0);
            this.wizardControl1.Name = "wizardControl1";
            this.wizardControl1.NextButtonEnabled = true;
            this.wizardControl1.NextButtonText = "下一步 >";
            this.wizardControl1.NextButtonVisible = true;
            this.wizardControl1.Size = new System.Drawing.Size(492, 386);
            this.wizardControl1.WizardSteps.Add(this.startStep1);
            this.wizardControl1.WizardSteps.Add(this.intermediateStep2);
            this.wizardControl1.WizardSteps.Add(this.intermediateStep3);
            this.wizardControl1.WizardSteps.Add(this.finishStep1);
            this.wizardControl1.BackButtonClick += new WizardBase.WizardClickEventHandler(this.wizardControl1_BackButtonClick);
            this.wizardControl1.CancelButtonClick += new System.EventHandler(this.wizardControl1_CancelButtonClick);
            this.wizardControl1.FinishButtonClick += new System.EventHandler(this.wizardControl1_FinishButtonClick);
            this.wizardControl1.NextButtonClick += new WizardBase.WizardNextButtonClickEventHandler(this.wizardControl1_NextButtonClick);
            // 
            // startStep1
            // 
            this.startStep1.BindingImage = ((System.Drawing.Image)(resources.GetObject("startStep1.BindingImage")));
            this.startStep1.Controls.Add(this.label1);
            this.startStep1.Icon = ((System.Drawing.Image)(resources.GetObject("startStep1.Icon")));
            this.startStep1.Name = "startStep1";
            this.startStep1.Subtitle = "";
            this.startStep1.SubtitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.startStep1.Title = "";
            this.startStep1.TitleFont = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(194, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "欢迎使用控制器向导!";
            // 
            // intermediateStep2
            // 
            this.intermediateStep2.BackColor = System.Drawing.Color.White;
            this.intermediateStep2.BindingImage = ((System.Drawing.Image)(resources.GetObject("intermediateStep2.BindingImage")));
            this.intermediateStep2.Controls.Add(this.txtCount);
            this.intermediateStep2.Controls.Add(this.label3);
            this.intermediateStep2.Controls.Add(this.label16);
            this.intermediateStep2.Controls.Add(this.chkEnable);
            this.intermediateStep2.Controls.Add(this.label9);
            this.intermediateStep2.Controls.Add(this.label23);
            this.intermediateStep2.Controls.Add(this.label20);
            this.intermediateStep2.Controls.Add(this.txtName);
            this.intermediateStep2.Controls.Add(this.label2);
            this.intermediateStep2.ForeColor = System.Drawing.Color.White;
            this.intermediateStep2.Name = "intermediateStep2";
            this.intermediateStep2.Subtitle = "";
            this.intermediateStep2.SubtitleFont = new System.Drawing.Font("宋体", 8.25F);
            this.intermediateStep2.Title = "第一步：基本信息";
            this.intermediateStep2.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            // 
            // txtCount
            // 
            this.txtCount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtCount.Location = new System.Drawing.Point(170, 131);
            this.txtCount.MaxLength = 4;
            this.txtCount.Name = "txtCount";
            this.txtCount.Size = new System.Drawing.Size(69, 21);
            this.txtCount.TabIndex = 25;
            this.txtCount.Text = "1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(97, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 24;
            this.label3.Text = "数    量：";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label16.Location = new System.Drawing.Point(196, 240);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(65, 12);
            this.label16.TabIndex = 23;
            this.label16.Text = "启用[选中]";
            // 
            // chkEnable
            // 
            this.chkEnable.AutoSize = true;
            this.chkEnable.Checked = true;
            this.chkEnable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnable.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkEnable.Location = new System.Drawing.Point(174, 242);
            this.chkEnable.Name = "chkEnable";
            this.chkEnable.Size = new System.Drawing.Size(15, 14);
            this.chkEnable.TabIndex = 22;
            this.chkEnable.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label9.Location = new System.Drawing.Point(97, 242);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 21;
            this.label9.Text = "是否启用：";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.BackColor = System.Drawing.Color.Transparent;
            this.label23.Location = new System.Drawing.Point(26, 36);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(203, 12);
            this.label23.TabIndex = 9;
            this.label23.Text = "请输入控制器的基本信息，*为必选项";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label20.Location = new System.Drawing.Point(379, 186);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(11, 12);
            this.label20.TabIndex = 6;
            this.label20.Text = "*";
            // 
            // txtName
            // 
            this.txtName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtName.Location = new System.Drawing.Point(171, 181);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(202, 21);
            this.txtName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(97, 186);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "名    称：";
            // 
            // intermediateStep3
            // 
            this.intermediateStep3.BackColor = System.Drawing.Color.White;
            this.intermediateStep3.BindingImage = ((System.Drawing.Image)(resources.GetObject("intermediateStep3.BindingImage")));
            this.intermediateStep3.Controls.Add(this.label5);
            this.intermediateStep3.Controls.Add(this.cmbLevel);
            this.intermediateStep3.Controls.Add(this.label17);
            this.intermediateStep3.Controls.Add(this.label21);
            this.intermediateStep3.Controls.Add(this.label10);
            this.intermediateStep3.Controls.Add(this.label4);
            this.intermediateStep3.Controls.Add(this.cmbLogID);
            this.intermediateStep3.ForeColor = System.Drawing.Color.White;
            this.intermediateStep3.Name = "intermediateStep3";
            this.intermediateStep3.Subtitle = "";
            this.intermediateStep3.SubtitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.intermediateStep3.Title = "第二步：通讯参数";
            this.intermediateStep3.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(351, 217);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 12);
            this.label5.TabIndex = 16;
            this.label5.Text = "*";
            // 
            // cmbLevel
            // 
            this.cmbLevel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmbLevel.FormattingEnabled = true;
            this.cmbLevel.Location = new System.Drawing.Point(179, 215);
            this.cmbLevel.Name = "cmbLevel";
            this.cmbLevel.Size = new System.Drawing.Size(151, 20);
            this.cmbLevel.TabIndex = 15;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label17.Location = new System.Drawing.Point(108, 218);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(65, 12);
            this.label17.TabIndex = 14;
            this.label17.Text = "操作级别：";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label21.Location = new System.Drawing.Point(351, 164);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(11, 12);
            this.label21.TabIndex = 13;
            this.label21.Text = "*";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Location = new System.Drawing.Point(32, 33);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(227, 12);
            this.label10.TabIndex = 8;
            this.label10.Text = "请设置控制器通讯所需的参数，*为必选项";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(108, 164);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "设备编号：";
            // 
            // cmbLogID
            // 
            this.cmbLogID.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmbLogID.FormattingEnabled = true;
            this.cmbLogID.Location = new System.Drawing.Point(179, 161);
            this.cmbLogID.Name = "cmbLogID";
            this.cmbLogID.Size = new System.Drawing.Size(151, 20);
            this.cmbLogID.TabIndex = 1;
            this.cmbLogID.SelectedIndexChanged += new System.EventHandler(this.cmbLogID_SelectedIndexChanged);
            // 
            // finishStep1
            // 
            this.finishStep1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("finishStep1.BackgroundImage")));
            this.finishStep1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.finishStep1.Controls.Add(this.label6);
            this.finishStep1.Name = "finishStep1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(48, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 14);
            this.label6.TabIndex = 2;
            this.label6.Text = "新增控制器成功！";
            // 
            // intermediateStep1
            // 
            this.intermediateStep1.BindingImage = ((System.Drawing.Image)(resources.GetObject("intermediateStep1.BindingImage")));
            this.intermediateStep1.Name = "intermediateStep1";
            this.intermediateStep1.SubtitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.intermediateStep1.Title = "New WizardControl step.";
            this.intermediateStep1.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            // 
            // FormControllerWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 386);
            this.Controls.Add(this.wizardControl1);
            this.DoubleBuffered = true;
            this.Name = "FormControllerWizard";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "控制器向导";
            this.Load += new System.EventHandler(this.FormControllerWizard_Load);
            this.startStep1.ResumeLayout(false);
            this.startStep1.PerformLayout();
            this.intermediateStep2.ResumeLayout(false);
            this.intermediateStep2.PerformLayout();
            this.intermediateStep3.ResumeLayout(false);
            this.intermediateStep3.PerformLayout();
            this.finishStep1.ResumeLayout(false);
            this.finishStep1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private WizardBase.WizardControl wizardControl1;
        private WizardBase.IntermediateStep intermediateStep1;
        private WizardBase.StartStep startStep1;
        private WizardBase.IntermediateStep intermediateStep2;
        private WizardBase.FinishStep finishStep1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label2;
        private WizardBase.IntermediateStep intermediateStep3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbLogID;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.CheckBox chkEnable;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbLevel;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label6;
    }
}