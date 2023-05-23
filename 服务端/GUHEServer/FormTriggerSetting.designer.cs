namespace GHIBMS.Server
{
    partial class FormTriggerSetting
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTriggerSetting));
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnQuxiao = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.cmbEventType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxState5 = new System.Windows.Forms.CheckBox();
            this.checkBoxState6 = new System.Windows.Forms.CheckBox();
            this.checkBoxState7 = new System.Windows.Forms.CheckBox();
            this.checkBoxState8 = new System.Windows.Forms.CheckBox();
            this.checkBoxState4 = new System.Windows.Forms.CheckBox();
            this.checkBoxState3 = new System.Windows.Forms.CheckBox();
            this.checkBoxState2 = new System.Windows.Forms.CheckBox();
            this.checkBoxState1 = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtDown = new System.Windows.Forms.TextBox();
            this.chkDown = new System.Windows.Forms.CheckBox();
            this.txtUp = new System.Windows.Forms.TextBox();
            this.chkUp = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkNoStateNull = new System.Windows.Forms.CheckBox();
            this.chkNoState5 = new System.Windows.Forms.CheckBox();
            this.chkNoState6 = new System.Windows.Forms.CheckBox();
            this.chkNoState0 = new System.Windows.Forms.CheckBox();
            this.chkNoState4 = new System.Windows.Forms.CheckBox();
            this.chkNoState3 = new System.Windows.Forms.CheckBox();
            this.chkNoState2 = new System.Windows.Forms.CheckBox();
            this.chkNoState1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "FormulaEvaluator.bmp");
            this.imageList1.Images.SetKeyName(1, "fun.ico");
            this.imageList1.Images.SetKeyName(2, "tray_on.ico");
            // 
            // btnQuxiao
            // 
            this.btnQuxiao.Location = new System.Drawing.Point(267, 163);
            this.btnQuxiao.Name = "btnQuxiao";
            this.btnQuxiao.Size = new System.Drawing.Size(75, 26);
            this.btnQuxiao.TabIndex = 16;
            this.btnQuxiao.Text = "取消";
            this.btnQuxiao.UseVisualStyleBackColor = true;
            this.btnQuxiao.Click += new System.EventHandler(this.btnQuxiao_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(267, 113);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 26);
            this.btnOK.TabIndex = 15;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // cmbEventType
            // 
            this.cmbEventType.FormattingEnabled = true;
            this.cmbEventType.Location = new System.Drawing.Point(81, 29);
            this.cmbEventType.Name = "cmbEventType";
            this.cmbEventType.Size = new System.Drawing.Size(165, 20);
            this.cmbEventType.TabIndex = 20;
            this.cmbEventType.SelectedIndexChanged += new System.EventHandler(this.cmbEventType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 21;
            this.label1.Text = "事件类型";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxState5);
            this.groupBox1.Controls.Add(this.checkBoxState6);
            this.groupBox1.Controls.Add(this.checkBoxState7);
            this.groupBox1.Controls.Add(this.checkBoxState8);
            this.groupBox1.Controls.Add(this.checkBoxState4);
            this.groupBox1.Controls.Add(this.checkBoxState3);
            this.groupBox1.Controls.Add(this.checkBoxState2);
            this.groupBox1.Controls.Add(this.checkBoxState1);
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(13, 73);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(231, 115);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数值相等";
            // 
            // checkBoxState5
            // 
            this.checkBoxState5.AutoSize = true;
            this.checkBoxState5.Location = new System.Drawing.Point(72, 77);
            this.checkBoxState5.Name = "checkBoxState5";
            this.checkBoxState5.Size = new System.Drawing.Size(30, 16);
            this.checkBoxState5.TabIndex = 7;
            this.checkBoxState5.Text = "5";
            this.checkBoxState5.UseVisualStyleBackColor = true;
            // 
            // checkBoxState6
            // 
            this.checkBoxState6.AutoSize = true;
            this.checkBoxState6.Location = new System.Drawing.Point(123, 77);
            this.checkBoxState6.Name = "checkBoxState6";
            this.checkBoxState6.Size = new System.Drawing.Size(30, 16);
            this.checkBoxState6.TabIndex = 6;
            this.checkBoxState6.Text = "6";
            this.checkBoxState6.UseVisualStyleBackColor = true;
            // 
            // checkBoxState7
            // 
            this.checkBoxState7.AutoSize = true;
            this.checkBoxState7.Location = new System.Drawing.Point(175, 77);
            this.checkBoxState7.Name = "checkBoxState7";
            this.checkBoxState7.Size = new System.Drawing.Size(30, 16);
            this.checkBoxState7.TabIndex = 5;
            this.checkBoxState7.Text = "7";
            this.checkBoxState7.UseVisualStyleBackColor = true;
            // 
            // checkBoxState8
            // 
            this.checkBoxState8.AutoSize = true;
            this.checkBoxState8.Location = new System.Drawing.Point(21, 39);
            this.checkBoxState8.Name = "checkBoxState8";
            this.checkBoxState8.Size = new System.Drawing.Size(30, 16);
            this.checkBoxState8.TabIndex = 4;
            this.checkBoxState8.Text = "0";
            this.checkBoxState8.UseVisualStyleBackColor = true;
            // 
            // checkBoxState4
            // 
            this.checkBoxState4.AutoSize = true;
            this.checkBoxState4.Location = new System.Drawing.Point(21, 77);
            this.checkBoxState4.Name = "checkBoxState4";
            this.checkBoxState4.Size = new System.Drawing.Size(30, 16);
            this.checkBoxState4.TabIndex = 3;
            this.checkBoxState4.Text = "4";
            this.checkBoxState4.UseVisualStyleBackColor = true;
            // 
            // checkBoxState3
            // 
            this.checkBoxState3.AutoSize = true;
            this.checkBoxState3.Location = new System.Drawing.Point(175, 39);
            this.checkBoxState3.Name = "checkBoxState3";
            this.checkBoxState3.Size = new System.Drawing.Size(30, 16);
            this.checkBoxState3.TabIndex = 2;
            this.checkBoxState3.Text = "3";
            this.checkBoxState3.UseVisualStyleBackColor = true;
            // 
            // checkBoxState2
            // 
            this.checkBoxState2.AutoSize = true;
            this.checkBoxState2.Location = new System.Drawing.Point(123, 39);
            this.checkBoxState2.Name = "checkBoxState2";
            this.checkBoxState2.Size = new System.Drawing.Size(30, 16);
            this.checkBoxState2.TabIndex = 1;
            this.checkBoxState2.Text = "2";
            this.checkBoxState2.UseVisualStyleBackColor = true;
            // 
            // checkBoxState1
            // 
            this.checkBoxState1.AutoSize = true;
            this.checkBoxState1.Checked = true;
            this.checkBoxState1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxState1.Location = new System.Drawing.Point(72, 39);
            this.checkBoxState1.Name = "checkBoxState1";
            this.checkBoxState1.Size = new System.Drawing.Size(30, 16);
            this.checkBoxState1.TabIndex = 0;
            this.checkBoxState1.Text = "1";
            this.checkBoxState1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtDown);
            this.groupBox2.Controls.Add(this.chkDown);
            this.groupBox2.Controls.Add(this.txtUp);
            this.groupBox2.Controls.Add(this.chkUp);
            this.groupBox2.ForeColor = System.Drawing.Color.Black;
            this.groupBox2.Location = new System.Drawing.Point(13, 73);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(231, 115);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数值比较";
            // 
            // txtDown
            // 
            this.txtDown.Enabled = false;
            this.txtDown.Location = new System.Drawing.Point(75, 61);
            this.txtDown.Name = "txtDown";
            this.txtDown.Size = new System.Drawing.Size(128, 21);
            this.txtDown.TabIndex = 3;
            // 
            // chkDown
            // 
            this.chkDown.AutoSize = true;
            this.chkDown.Location = new System.Drawing.Point(21, 63);
            this.chkDown.Name = "chkDown";
            this.chkDown.Size = new System.Drawing.Size(48, 16);
            this.chkDown.TabIndex = 2;
            this.chkDown.Text = "下限";
            this.chkDown.UseVisualStyleBackColor = true;
            this.chkDown.CheckedChanged += new System.EventHandler(this.chkDown_CheckedChanged);
            // 
            // txtUp
            // 
            this.txtUp.Enabled = false;
            this.txtUp.Location = new System.Drawing.Point(75, 29);
            this.txtUp.Name = "txtUp";
            this.txtUp.Size = new System.Drawing.Size(128, 21);
            this.txtUp.TabIndex = 1;
            // 
            // chkUp
            // 
            this.chkUp.AutoSize = true;
            this.chkUp.Location = new System.Drawing.Point(21, 31);
            this.chkUp.Name = "chkUp";
            this.chkUp.Size = new System.Drawing.Size(48, 16);
            this.chkUp.TabIndex = 0;
            this.chkUp.Text = "上限";
            this.chkUp.UseVisualStyleBackColor = true;
            this.chkUp.CheckedChanged += new System.EventHandler(this.chkUp_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkNoStateNull);
            this.groupBox3.Controls.Add(this.chkNoState5);
            this.groupBox3.Controls.Add(this.chkNoState6);
            this.groupBox3.Controls.Add(this.chkNoState0);
            this.groupBox3.Controls.Add(this.chkNoState4);
            this.groupBox3.Controls.Add(this.chkNoState3);
            this.groupBox3.Controls.Add(this.chkNoState2);
            this.groupBox3.Controls.Add(this.chkNoState1);
            this.groupBox3.ForeColor = System.Drawing.Color.Black;
            this.groupBox3.Location = new System.Drawing.Point(13, 73);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(231, 115);
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "数值不等";
            // 
            // chkNoStateNull
            // 
            this.chkNoStateNull.AutoSize = true;
            this.chkNoStateNull.Location = new System.Drawing.Point(21, 39);
            this.chkNoStateNull.Name = "chkNoStateNull";
            this.chkNoStateNull.Size = new System.Drawing.Size(36, 16);
            this.chkNoStateNull.TabIndex = 8;
            this.chkNoStateNull.Text = "空";
            this.chkNoStateNull.UseVisualStyleBackColor = true;
            // 
            // chkNoState5
            // 
            this.chkNoState5.AutoSize = true;
            this.chkNoState5.Location = new System.Drawing.Point(120, 76);
            this.chkNoState5.Name = "chkNoState5";
            this.chkNoState5.Size = new System.Drawing.Size(30, 16);
            this.chkNoState5.TabIndex = 7;
            this.chkNoState5.Text = "5";
            this.chkNoState5.UseVisualStyleBackColor = true;
            // 
            // chkNoState6
            // 
            this.chkNoState6.AutoSize = true;
            this.chkNoState6.Location = new System.Drawing.Point(171, 76);
            this.chkNoState6.Name = "chkNoState6";
            this.chkNoState6.Size = new System.Drawing.Size(30, 16);
            this.chkNoState6.TabIndex = 6;
            this.chkNoState6.Text = "6";
            this.chkNoState6.UseVisualStyleBackColor = true;
            // 
            // chkNoState0
            // 
            this.chkNoState0.AutoSize = true;
            this.chkNoState0.Checked = true;
            this.chkNoState0.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNoState0.Location = new System.Drawing.Point(69, 39);
            this.chkNoState0.Name = "chkNoState0";
            this.chkNoState0.Size = new System.Drawing.Size(30, 16);
            this.chkNoState0.TabIndex = 4;
            this.chkNoState0.Text = "0";
            this.chkNoState0.UseVisualStyleBackColor = true;
            // 
            // chkNoState4
            // 
            this.chkNoState4.AutoSize = true;
            this.chkNoState4.Location = new System.Drawing.Point(69, 76);
            this.chkNoState4.Name = "chkNoState4";
            this.chkNoState4.Size = new System.Drawing.Size(30, 16);
            this.chkNoState4.TabIndex = 3;
            this.chkNoState4.Text = "4";
            this.chkNoState4.UseVisualStyleBackColor = true;
            // 
            // chkNoState3
            // 
            this.chkNoState3.AutoSize = true;
            this.chkNoState3.Location = new System.Drawing.Point(21, 76);
            this.chkNoState3.Name = "chkNoState3";
            this.chkNoState3.Size = new System.Drawing.Size(30, 16);
            this.chkNoState3.TabIndex = 2;
            this.chkNoState3.Text = "3";
            this.chkNoState3.UseVisualStyleBackColor = true;
            // 
            // chkNoState2
            // 
            this.chkNoState2.AutoSize = true;
            this.chkNoState2.Location = new System.Drawing.Point(174, 39);
            this.chkNoState2.Name = "chkNoState2";
            this.chkNoState2.Size = new System.Drawing.Size(30, 16);
            this.chkNoState2.TabIndex = 1;
            this.chkNoState2.Text = "2";
            this.chkNoState2.UseVisualStyleBackColor = true;
            // 
            // chkNoState1
            // 
            this.chkNoState1.AutoSize = true;
            this.chkNoState1.Location = new System.Drawing.Point(120, 39);
            this.chkNoState1.Name = "chkNoState1";
            this.chkNoState1.Size = new System.Drawing.Size(30, 16);
            this.chkNoState1.TabIndex = 0;
            this.chkNoState1.Text = "1";
            this.chkNoState1.UseVisualStyleBackColor = true;
            // 
            // FormTriggerSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 223);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbEventType);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnQuxiao);
            this.Controls.Add(this.btnOK);
            this.Name = "FormTriggerSetting";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "变量事件触发器";
            this.Load += new System.EventHandler(this.FormTriggerSetting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnQuxiao;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ComboBox cmbEventType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxState5;
        private System.Windows.Forms.CheckBox checkBoxState6;
        private System.Windows.Forms.CheckBox checkBoxState7;
        private System.Windows.Forms.CheckBox checkBoxState8;
        private System.Windows.Forms.CheckBox checkBoxState4;
        private System.Windows.Forms.CheckBox checkBoxState3;
        private System.Windows.Forms.CheckBox checkBoxState2;
        private System.Windows.Forms.CheckBox checkBoxState1;
        private System.Windows.Forms.TextBox txtDown;
        private System.Windows.Forms.CheckBox chkDown;
        private System.Windows.Forms.TextBox txtUp;
        private System.Windows.Forms.CheckBox chkUp;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkNoState5;
        private System.Windows.Forms.CheckBox chkNoState6;
        private System.Windows.Forms.CheckBox chkNoState0;
        private System.Windows.Forms.CheckBox chkNoState4;
        private System.Windows.Forms.CheckBox chkNoState3;
        private System.Windows.Forms.CheckBox chkNoState2;
        private System.Windows.Forms.CheckBox chkNoState1;
        private System.Windows.Forms.CheckBox chkNoStateNull;
    }
}