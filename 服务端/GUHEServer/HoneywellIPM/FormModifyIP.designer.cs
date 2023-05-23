namespace GHIBMS.Server
{
    partial class FormModifyIP
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
            this.lblIP = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ipAddr = new IPAddressControlLib.IPAddressControl();
            this.ipSubAddr = new IPAddressControlLib.IPAddressControl();
            this.ipGateway = new IPAddressControlLib.IPAddressControl();
            this.ipServer = new IPAddressControlLib.IPAddressControl();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblIP
            // 
            this.lblIP.AutoSize = true;
            this.lblIP.Location = new System.Drawing.Point(101, 56);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(65, 12);
            this.lblIP.TabIndex = 0;
            this.lblIP.Text = "IP  地址：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(101, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "子网掩码：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(101, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "默认网关：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(77, 164);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "服务器IP地址：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(77, 200);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "服务器端口号：";
            // 
            // ipAddr
            // 
            this.ipAddr.AllowInternalTab = false;
            this.ipAddr.AutoHeight = true;
            this.ipAddr.BackColor = System.Drawing.SystemColors.Window;
            this.ipAddr.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipAddr.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipAddr.Location = new System.Drawing.Point(191, 56);
            this.ipAddr.Name = "ipAddr";
            this.ipAddr.ReadOnly = false;
            this.ipAddr.Size = new System.Drawing.Size(120, 19);
            this.ipAddr.TabIndex = 15;
            this.ipAddr.Text = "...";
            // 
            // ipSubAddr
            // 
            this.ipSubAddr.AllowInternalTab = false;
            this.ipSubAddr.AutoHeight = true;
            this.ipSubAddr.BackColor = System.Drawing.SystemColors.Window;
            this.ipSubAddr.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipSubAddr.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipSubAddr.Location = new System.Drawing.Point(191, 85);
            this.ipSubAddr.Name = "ipSubAddr";
            this.ipSubAddr.ReadOnly = false;
            this.ipSubAddr.Size = new System.Drawing.Size(120, 19);
            this.ipSubAddr.TabIndex = 14;
            this.ipSubAddr.Text = "...";
            // 
            // ipGateway
            // 
            this.ipGateway.AllowInternalTab = false;
            this.ipGateway.AutoHeight = true;
            this.ipGateway.BackColor = System.Drawing.SystemColors.Window;
            this.ipGateway.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipGateway.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipGateway.Location = new System.Drawing.Point(191, 128);
            this.ipGateway.Name = "ipGateway";
            this.ipGateway.ReadOnly = false;
            this.ipGateway.Size = new System.Drawing.Size(120, 19);
            this.ipGateway.TabIndex = 13;
            this.ipGateway.Text = "...";
            // 
            // ipServer
            // 
            this.ipServer.AllowInternalTab = false;
            this.ipServer.AutoHeight = true;
            this.ipServer.BackColor = System.Drawing.SystemColors.Window;
            this.ipServer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipServer.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipServer.Location = new System.Drawing.Point(191, 164);
            this.ipServer.Name = "ipServer";
            this.ipServer.ReadOnly = false;
            this.ipServer.Size = new System.Drawing.Size(120, 19);
            this.ipServer.TabIndex = 12;
            this.ipServer.Text = "...";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(191, 197);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(120, 21);
            this.txtPort.TabIndex = 9;
            this.txtPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPort_KeyPress);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(163, 276);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "确认";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(271, 276);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "取消";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormModifyIP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 326);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.ipServer);
            this.Controls.Add(this.ipGateway);
            this.Controls.Add(this.ipSubAddr);
            this.Controls.Add(this.ipAddr);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblIP);
            this.Name = "FormModifyIP";
            this.Text = "修改IP";
            this.Load += new System.EventHandler(this.FormModifyIP_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private IPAddressControlLib.IPAddressControl ipAddr;
        private IPAddressControlLib.IPAddressControl ipSubAddr;
        private IPAddressControlLib.IPAddressControl ipGateway;
        private IPAddressControlLib.IPAddressControl ipServer;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button button1;
    }
}