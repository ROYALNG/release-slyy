namespace GHIBMS.Server
{
    partial class FormBackup
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
            this.lb_server_ip = new System.Windows.Forms.Label();
            this.text_server_ip = new System.Windows.Forms.TextBox();
            this.lb_server_port = new System.Windows.Forms.Label();
            this.text_server_port = new System.Windows.Forms.TextBox();
            this.group_mqtt = new System.Windows.Forms.GroupBox();
            this.rdoSlave = new System.Windows.Forms.RadioButton();
            this.rdoMaster = new System.Windows.Forms.RadioButton();
            this.group_server = new System.Windows.Forms.GroupBox();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_confirm = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkEnable = new System.Windows.Forms.CheckBox();
            this.group_mqtt.SuspendLayout();
            this.group_server.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lb_server_ip
            // 
            this.lb_server_ip.AutoSize = true;
            this.lb_server_ip.Location = new System.Drawing.Point(127, 40);
            this.lb_server_ip.Name = "lb_server_ip";
            this.lb_server_ip.Size = new System.Drawing.Size(53, 12);
            this.lb_server_ip.TabIndex = 2;
            this.lb_server_ip.Text = "IP地址：";
            // 
            // text_server_ip
            // 
            this.text_server_ip.Location = new System.Drawing.Point(204, 37);
            this.text_server_ip.Name = "text_server_ip";
            this.text_server_ip.Size = new System.Drawing.Size(121, 21);
            this.text_server_ip.TabIndex = 3;
            // 
            // lb_server_port
            // 
            this.lb_server_port.AutoSize = true;
            this.lb_server_port.Location = new System.Drawing.Point(127, 76);
            this.lb_server_port.Name = "lb_server_port";
            this.lb_server_port.Size = new System.Drawing.Size(53, 12);
            this.lb_server_port.TabIndex = 4;
            this.lb_server_port.Text = "端口号：";
            // 
            // text_server_port
            // 
            this.text_server_port.Location = new System.Drawing.Point(204, 74);
            this.text_server_port.Name = "text_server_port";
            this.text_server_port.Size = new System.Drawing.Size(121, 21);
            this.text_server_port.TabIndex = 3;
            // 
            // group_mqtt
            // 
            this.group_mqtt.Controls.Add(this.rdoSlave);
            this.group_mqtt.Controls.Add(this.rdoMaster);
            this.group_mqtt.Location = new System.Drawing.Point(8, 78);
            this.group_mqtt.Name = "group_mqtt";
            this.group_mqtt.Size = new System.Drawing.Size(509, 50);
            this.group_mqtt.TabIndex = 5;
            this.group_mqtt.TabStop = false;
            this.group_mqtt.Text = "主/备设置";
            // 
            // rdoSlave
            // 
            this.rdoSlave.AutoSize = true;
            this.rdoSlave.Location = new System.Drawing.Point(262, 20);
            this.rdoSlave.Name = "rdoSlave";
            this.rdoSlave.Size = new System.Drawing.Size(47, 16);
            this.rdoSlave.TabIndex = 1;
            this.rdoSlave.TabStop = true;
            this.rdoSlave.Text = "备机";
            this.rdoSlave.UseVisualStyleBackColor = true;
            // 
            // rdoMaster
            // 
            this.rdoMaster.AutoSize = true;
            this.rdoMaster.Location = new System.Drawing.Point(124, 20);
            this.rdoMaster.Name = "rdoMaster";
            this.rdoMaster.Size = new System.Drawing.Size(47, 16);
            this.rdoMaster.TabIndex = 0;
            this.rdoMaster.TabStop = true;
            this.rdoMaster.Text = "主机";
            this.rdoMaster.UseVisualStyleBackColor = true;
            // 
            // group_server
            // 
            this.group_server.Controls.Add(this.text_server_ip);
            this.group_server.Controls.Add(this.lb_server_ip);
            this.group_server.Controls.Add(this.text_server_port);
            this.group_server.Controls.Add(this.lb_server_port);
            this.group_server.Location = new System.Drawing.Point(8, 147);
            this.group_server.Name = "group_server";
            this.group_server.Size = new System.Drawing.Size(514, 112);
            this.group_server.TabIndex = 6;
            this.group_server.TabStop = false;
            this.group_server.Text = "主机参数";
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(199, 296);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 8;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_confirm
            // 
            this.btn_confirm.Location = new System.Drawing.Point(333, 295);
            this.btn_confirm.Name = "btn_confirm";
            this.btn_confirm.Size = new System.Drawing.Size(75, 23);
            this.btn_confirm.TabIndex = 8;
            this.btn_confirm.Text = "确定";
            this.btn_confirm.UseVisualStyleBackColor = true;
            this.btn_confirm.Click += new System.EventHandler(this.btn_confirm_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkEnable);
            this.groupBox1.Location = new System.Drawing.Point(8, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(509, 50);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "启用禁用";
            // 
            // chkEnable
            // 
            this.chkEnable.AutoSize = true;
            this.chkEnable.Location = new System.Drawing.Point(124, 23);
            this.chkEnable.Name = "chkEnable";
            this.chkEnable.Size = new System.Drawing.Size(48, 16);
            this.chkEnable.TabIndex = 0;
            this.chkEnable.Text = "启用";
            this.chkEnable.UseVisualStyleBackColor = true;
            // 
            // FormBackup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 350);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_confirm);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.group_server);
            this.Controls.Add(this.group_mqtt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormBackup";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "双机热备";
            this.Shown += new System.EventHandler(this.FormBackup_Shown);
            this.group_mqtt.ResumeLayout(false);
            this.group_mqtt.PerformLayout();
            this.group_server.ResumeLayout(false);
            this.group_server.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lb_server_ip;
        private System.Windows.Forms.TextBox text_server_ip;
        private System.Windows.Forms.Label lb_server_port;
        private System.Windows.Forms.TextBox text_server_port;
        private System.Windows.Forms.GroupBox group_mqtt;
        private System.Windows.Forms.GroupBox group_server;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_confirm;
        private System.Windows.Forms.RadioButton rdoSlave;
        private System.Windows.Forms.RadioButton rdoMaster;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkEnable;
    }
}