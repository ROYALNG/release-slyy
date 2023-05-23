namespace GHIBMS.Server
{
    partial class FormHistorySet
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
            this.label2 = new System.Windows.Forms.Label();
            this.chkFastRecorder = new System.Windows.Forms.CheckBox();
            this.txtFastRecorder = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNormalRecorder = new System.Windows.Forms.TextBox();
            this.chkNormalRecorder = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSlowRecorder = new System.Windows.Forms.TextBox();
            this.chkSlowRecorder = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(204, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "记录时间间隔";
            // 
            // chkFastRecorder
            // 
            this.chkFastRecorder.AutoSize = true;
            this.chkFastRecorder.Location = new System.Drawing.Point(93, 99);
            this.chkFastRecorder.Name = "chkFastRecorder";
            this.chkFastRecorder.Size = new System.Drawing.Size(84, 16);
            this.chkFastRecorder.TabIndex = 2;
            this.chkFastRecorder.Text = "快速记录器";
            this.chkFastRecorder.UseVisualStyleBackColor = true;
            // 
            // txtFastRecorder
            // 
            this.txtFastRecorder.Location = new System.Drawing.Point(206, 94);
            this.txtFastRecorder.Name = "txtFastRecorder";
            this.txtFastRecorder.Size = new System.Drawing.Size(75, 21);
            this.txtFastRecorder.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(300, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "秒(s)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(300, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "分(min)";
            // 
            // txtNormalRecorder
            // 
            this.txtNormalRecorder.Location = new System.Drawing.Point(206, 134);
            this.txtNormalRecorder.Name = "txtNormalRecorder";
            this.txtNormalRecorder.Size = new System.Drawing.Size(75, 21);
            this.txtNormalRecorder.TabIndex = 6;
            // 
            // chkNormalRecorder
            // 
            this.chkNormalRecorder.AutoSize = true;
            this.chkNormalRecorder.Location = new System.Drawing.Point(93, 137);
            this.chkNormalRecorder.Name = "chkNormalRecorder";
            this.chkNormalRecorder.Size = new System.Drawing.Size(84, 16);
            this.chkNormalRecorder.TabIndex = 5;
            this.chkNormalRecorder.Text = "标准记录器";
            this.chkNormalRecorder.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(300, 179);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "时(h)";
            // 
            // txtSlowRecorder
            // 
            this.txtSlowRecorder.Location = new System.Drawing.Point(206, 170);
            this.txtSlowRecorder.Name = "txtSlowRecorder";
            this.txtSlowRecorder.Size = new System.Drawing.Size(75, 21);
            this.txtSlowRecorder.TabIndex = 9;
            // 
            // chkSlowRecorder
            // 
            this.chkSlowRecorder.AutoSize = true;
            this.chkSlowRecorder.Location = new System.Drawing.Point(93, 175);
            this.chkSlowRecorder.Name = "chkSlowRecorder";
            this.chkSlowRecorder.Size = new System.Drawing.Size(84, 16);
            this.chkSlowRecorder.TabIndex = 8;
            this.chkSlowRecorder.Text = "慢速记录器";
            this.chkSlowRecorder.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(206, 239);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(302, 239);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(110, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "是否启用";
            // 
            // FormHistorySet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 292);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtSlowRecorder);
            this.Controls.Add(this.chkSlowRecorder);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtNormalRecorder);
            this.Controls.Add(this.chkNormalRecorder);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtFastRecorder);
            this.Controls.Add(this.chkFastRecorder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormHistorySet";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "记录器设置";
            this.Shown += new System.EventHandler(this.FormHistorySet_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkFastRecorder;
        private System.Windows.Forms.TextBox txtFastRecorder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNormalRecorder;
        private System.Windows.Forms.CheckBox chkNormalRecorder;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSlowRecorder;
        private System.Windows.Forms.CheckBox chkSlowRecorder;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
    }
}