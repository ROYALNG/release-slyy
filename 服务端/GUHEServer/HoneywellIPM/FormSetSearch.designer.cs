namespace GHIBMS.Server
{
    partial class FormSetSearch
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
            this.rdoMac = new System.Windows.Forms.RadioButton();
            this.rdoIP = new System.Windows.Forms.RadioButton();
            this.ipAddr = new IPAddressControlLib.IPAddressControl();
            this.mskMac = new System.Windows.Forms.MaskedTextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rdoMac
            // 
            this.rdoMac.AutoSize = true;
            this.rdoMac.Location = new System.Drawing.Point(76, 142);
            this.rdoMac.Name = "rdoMac";
            this.rdoMac.Size = new System.Drawing.Size(113, 16);
            this.rdoMac.TabIndex = 0;
            this.rdoMac.Text = "通过Mac地址查询";
            this.rdoMac.UseVisualStyleBackColor = true;
            this.rdoMac.Click += new System.EventHandler(this.rdoMac_Click);
            // 
            // rdoIP
            // 
            this.rdoIP.AutoSize = true;
            this.rdoIP.Checked = true;
            this.rdoIP.Location = new System.Drawing.Point(76, 54);
            this.rdoIP.Name = "rdoIP";
            this.rdoIP.Size = new System.Drawing.Size(107, 16);
            this.rdoIP.TabIndex = 1;
            this.rdoIP.TabStop = true;
            this.rdoIP.Text = "通过IP地址查询";
            this.rdoIP.UseVisualStyleBackColor = true;
            this.rdoIP.CheckedChanged += new System.EventHandler(this.rdoIP_CheckedChanged);
            // 
            // ipAddr
            // 
            this.ipAddr.AllowInternalTab = false;
            this.ipAddr.AutoHeight = true;
            this.ipAddr.BackColor = System.Drawing.SystemColors.Window;
            this.ipAddr.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipAddr.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipAddr.Location = new System.Drawing.Point(89, 91);
            this.ipAddr.Name = "ipAddr";
            this.ipAddr.ReadOnly = false;
            this.ipAddr.Size = new System.Drawing.Size(120, 19);
            this.ipAddr.TabIndex = 7;
            this.ipAddr.Text = "...";
            // 
            // mskMac
            // 
            this.mskMac.BackColor = System.Drawing.SystemColors.Window;
            this.mskMac.Enabled = false;
            this.mskMac.Location = new System.Drawing.Point(89, 182);
            this.mskMac.Mask = "> CC-CC-CC-CC-CC-CC";
            this.mskMac.Name = "mskMac";
            this.mskMac.Size = new System.Drawing.Size(120, 21);
            this.mskMac.TabIndex = 4;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(283, 128);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 30);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(283, 180);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click_1);
            // 
            // FormSetSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 266);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.mskMac);
            this.Controls.Add(this.ipAddr);
            this.Controls.Add(this.rdoIP);
            this.Controls.Add(this.rdoMac);
            this.Name = "FormSetSearch";
            this.Text = "指定搜索";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rdoMac;
        private System.Windows.Forms.RadioButton rdoIP;
        private IPAddressControlLib.IPAddressControl ipAddr;
        private System.Windows.Forms.MaskedTextBox mskMac;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}