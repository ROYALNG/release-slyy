namespace GHIBMS.Server
{
    partial class FormMqttConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMqttConfig));
            this.check_enable_mqtt = new System.Windows.Forms.CheckBox();
            this.lb_server_ip = new System.Windows.Forms.Label();
            this.text_server_ip = new System.Windows.Forms.TextBox();
            this.lb_server_port = new System.Windows.Forms.Label();
            this.text_server_port = new System.Windows.Forms.TextBox();
            this.lb_username = new System.Windows.Forms.Label();
            this.text_username = new System.Windows.Forms.TextBox();
            this.lb_password = new System.Windows.Forms.Label();
            this.text_password = new System.Windows.Forms.TextBox();
            this.text_deviceID = new System.Windows.Forms.TextBox();
            this.lb_devicename = new System.Windows.Forms.Label();
            this.group_mqtt = new System.Windows.Forms.GroupBox();
            this.group_server = new System.Windows.Forms.GroupBox();
            this.group_user = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDeviceKey = new System.Windows.Forms.TextBox();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_confirm = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkEnableMqttRecord = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.txtMaxRecordHis = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDelay = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkEncrypt = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.group_mqtt.SuspendLayout();
            this.group_server.SuspendLayout();
            this.group_user.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // check_enable_mqtt
            // 
            this.check_enable_mqtt.AutoSize = true;
            this.check_enable_mqtt.Location = new System.Drawing.Point(77, 20);
            this.check_enable_mqtt.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.check_enable_mqtt.Name = "check_enable_mqtt";
            this.check_enable_mqtt.Size = new System.Drawing.Size(48, 16);
            this.check_enable_mqtt.TabIndex = 1;
            this.check_enable_mqtt.Text = "启用";
            this.check_enable_mqtt.UseVisualStyleBackColor = true;
            // 
            // lb_server_ip
            // 
            this.lb_server_ip.AutoSize = true;
            this.lb_server_ip.Location = new System.Drawing.Point(22, 70);
            this.lb_server_ip.Name = "lb_server_ip";
            this.lb_server_ip.Size = new System.Drawing.Size(53, 12);
            this.lb_server_ip.TabIndex = 2;
            this.lb_server_ip.Text = "IP地址：";
            // 
            // text_server_ip
            // 
            this.text_server_ip.Location = new System.Drawing.Point(82, 66);
            this.text_server_ip.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.text_server_ip.Name = "text_server_ip";
            this.text_server_ip.Size = new System.Drawing.Size(121, 21);
            this.text_server_ip.TabIndex = 3;
            // 
            // lb_server_port
            // 
            this.lb_server_port.AutoSize = true;
            this.lb_server_port.Location = new System.Drawing.Point(22, 106);
            this.lb_server_port.Name = "lb_server_port";
            this.lb_server_port.Size = new System.Drawing.Size(53, 12);
            this.lb_server_port.TabIndex = 4;
            this.lb_server_port.Text = "端口号：";
            // 
            // text_server_port
            // 
            this.text_server_port.Location = new System.Drawing.Point(82, 102);
            this.text_server_port.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.text_server_port.Name = "text_server_port";
            this.text_server_port.Size = new System.Drawing.Size(121, 21);
            this.text_server_port.TabIndex = 3;
            // 
            // lb_username
            // 
            this.lb_username.AutoSize = true;
            this.lb_username.Location = new System.Drawing.Point(26, 106);
            this.lb_username.Name = "lb_username";
            this.lb_username.Size = new System.Drawing.Size(53, 12);
            this.lb_username.TabIndex = 4;
            this.lb_username.Text = "用户名：";
            // 
            // text_username
            // 
            this.text_username.Location = new System.Drawing.Point(92, 102);
            this.text_username.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.text_username.Name = "text_username";
            this.text_username.Size = new System.Drawing.Size(151, 21);
            this.text_username.TabIndex = 3;
            // 
            // lb_password
            // 
            this.lb_password.AutoSize = true;
            this.lb_password.Location = new System.Drawing.Point(38, 142);
            this.lb_password.Name = "lb_password";
            this.lb_password.Size = new System.Drawing.Size(41, 12);
            this.lb_password.TabIndex = 4;
            this.lb_password.Text = "密码：";
            // 
            // text_password
            // 
            this.text_password.Location = new System.Drawing.Point(92, 140);
            this.text_password.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.text_password.Name = "text_password";
            this.text_password.PasswordChar = '*';
            this.text_password.Size = new System.Drawing.Size(151, 21);
            this.text_password.TabIndex = 3;
            // 
            // text_deviceID
            // 
            this.text_deviceID.Location = new System.Drawing.Point(92, 30);
            this.text_deviceID.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.text_deviceID.Name = "text_deviceID";
            this.text_deviceID.Size = new System.Drawing.Size(151, 21);
            this.text_deviceID.TabIndex = 3;
            this.text_deviceID.TextChanged += new System.EventHandler(this.text_deviceID_TextChanged);
            this.text_deviceID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.text_deviceID_KeyPress);
            // 
            // lb_devicename
            // 
            this.lb_devicename.AutoSize = true;
            this.lb_devicename.Location = new System.Drawing.Point(14, 32);
            this.lb_devicename.Name = "lb_devicename";
            this.lb_devicename.Size = new System.Drawing.Size(65, 12);
            this.lb_devicename.TabIndex = 4;
            this.lb_devicename.Text = "采集器ID：";
            // 
            // group_mqtt
            // 
            this.group_mqtt.Controls.Add(this.check_enable_mqtt);
            this.group_mqtt.Location = new System.Drawing.Point(14, 18);
            this.group_mqtt.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.group_mqtt.Name = "group_mqtt";
            this.group_mqtt.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.group_mqtt.Size = new System.Drawing.Size(222, 50);
            this.group_mqtt.TabIndex = 5;
            this.group_mqtt.TabStop = false;
            this.group_mqtt.Text = "启用/禁用";
            // 
            // group_server
            // 
            this.group_server.Controls.Add(this.text_server_ip);
            this.group_server.Controls.Add(this.lb_server_ip);
            this.group_server.Controls.Add(this.text_server_port);
            this.group_server.Controls.Add(this.lb_server_port);
            this.group_server.Location = new System.Drawing.Point(8, 82);
            this.group_server.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.group_server.Name = "group_server";
            this.group_server.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.group_server.Size = new System.Drawing.Size(228, 178);
            this.group_server.TabIndex = 6;
            this.group_server.TabStop = false;
            this.group_server.Text = "服务器";
            // 
            // group_user
            // 
            this.group_user.Controls.Add(this.pictureBox1);
            this.group_user.Controls.Add(this.label2);
            this.group_user.Controls.Add(this.txtDeviceKey);
            this.group_user.Controls.Add(this.text_username);
            this.group_user.Controls.Add(this.text_password);
            this.group_user.Controls.Add(this.lb_devicename);
            this.group_user.Controls.Add(this.text_deviceID);
            this.group_user.Controls.Add(this.lb_username);
            this.group_user.Controls.Add(this.lb_password);
            this.group_user.Location = new System.Drawing.Point(242, 82);
            this.group_user.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.group_user.Name = "group_user";
            this.group_user.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.group_user.Size = new System.Drawing.Size(284, 178);
            this.group_user.TabIndex = 7;
            this.group_user.TabStop = false;
            this.group_user.Text = "认证信息";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(249, 140);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(29, 22);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "采集器KEY：";
            // 
            // txtDeviceKey
            // 
            this.txtDeviceKey.Location = new System.Drawing.Point(92, 66);
            this.txtDeviceKey.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDeviceKey.Name = "txtDeviceKey";
            this.txtDeviceKey.Size = new System.Drawing.Size(151, 21);
            this.txtDeviceKey.TabIndex = 5;
            this.txtDeviceKey.TextChanged += new System.EventHandler(this.txtDeviceKey_TextChanged);
            this.txtDeviceKey.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDeviceKey_KeyPress);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(300, 430);
            this.btn_cancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 22);
            this.btn_cancel.TabIndex = 8;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_confirm
            // 
            this.btn_confirm.Location = new System.Drawing.Point(434, 428);
            this.btn_confirm.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_confirm.Name = "btn_confirm";
            this.btn_confirm.Size = new System.Drawing.Size(75, 22);
            this.btn_confirm.TabIndex = 8;
            this.btn_confirm.Text = "确定";
            this.btn_confirm.UseVisualStyleBackColor = true;
            this.btn_confirm.Click += new System.EventHandler(this.btn_confirm_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Location = new System.Drawing.Point(242, 18);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(284, 50);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "协议版本";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(166, 20);
            this.radioButton2.Margin = new System.Windows.Forms.Padding(2);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(41, 16);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "2.0";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(92, 20);
            this.radioButton1.Margin = new System.Windows.Forms.Padding(2);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(41, 16);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "1.0";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkEncrypt);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.chkEnableMqttRecord);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.txtMaxRecordHis);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtDelay);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(8, 264);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(518, 148);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "通讯参数";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(287, 12);
            this.label4.TabIndex = 29;
            this.label4.Text = "(断线续传仅上传变量的变化云存储属性开启的变量）";
            // 
            // chkEnableMqttRecord
            // 
            this.chkEnableMqttRecord.Checked = true;
            this.chkEnableMqttRecord.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnableMqttRecord.Location = new System.Drawing.Point(105, 86);
            this.chkEnableMqttRecord.Name = "chkEnableMqttRecord";
            this.chkEnableMqttRecord.Size = new System.Drawing.Size(82, 23);
            this.chkEnableMqttRecord.TabIndex = 27;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(10, 90);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(89, 12);
            this.label16.TabIndex = 28;
            this.label16.Text = "是否断线续传：";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(230, 90);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(137, 12);
            this.label17.TabIndex = 26;
            this.label17.Text = "断线缓存最大记录条数：";
            // 
            // txtMaxRecordHis
            // 
            this.txtMaxRecordHis.Location = new System.Drawing.Point(373, 86);
            this.txtMaxRecordHis.Name = "txtMaxRecordHis";
            this.txtMaxRecordHis.Size = new System.Drawing.Size(128, 21);
            this.txtMaxRecordHis.TabIndex = 25;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(189, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "毫秒（>50）";
            // 
            // txtDelay
            // 
            this.txtDelay.Location = new System.Drawing.Point(105, 54);
            this.txtDelay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDelay.Name = "txtDelay";
            this.txtDelay.Size = new System.Drawing.Size(78, 21);
            this.txtDelay.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "传输延时：";
            // 
            // chkEncrypt
            // 
            this.chkEncrypt.Checked = true;
            this.chkEncrypt.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEncrypt.Location = new System.Drawing.Point(105, 19);
            this.chkEncrypt.Name = "chkEncrypt";
            this.chkEncrypt.Size = new System.Drawing.Size(82, 23);
            this.chkEncrypt.TabIndex = 30;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 12);
            this.label5.TabIndex = 31;
            this.label5.Text = "是否加密传输：";
            // 
            // FormMqttConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 463);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_confirm);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.group_user);
            this.Controls.Add(this.group_server);
            this.Controls.Add(this.group_mqtt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormMqttConfig";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "物联网平台配置";
            this.Shown += new System.EventHandler(this.FormMqttConfig_Shown);
            this.group_mqtt.ResumeLayout(false);
            this.group_mqtt.PerformLayout();
            this.group_server.ResumeLayout(false);
            this.group_server.PerformLayout();
            this.group_user.ResumeLayout(false);
            this.group_user.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.CheckBox check_enable_mqtt;
        private System.Windows.Forms.Label lb_server_ip;
        private System.Windows.Forms.TextBox text_server_ip;
        private System.Windows.Forms.Label lb_server_port;
        private System.Windows.Forms.TextBox text_server_port;
        private System.Windows.Forms.Label lb_username;
        private System.Windows.Forms.TextBox text_username;
        private System.Windows.Forms.Label lb_password;
        private System.Windows.Forms.TextBox text_password;
        private System.Windows.Forms.TextBox text_deviceID;
        private System.Windows.Forms.Label lb_devicename;
        private System.Windows.Forms.GroupBox group_mqtt;
        private System.Windows.Forms.GroupBox group_server;
        private System.Windows.Forms.GroupBox group_user;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_confirm;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDeviceKey;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDelay;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkEnableMqttRecord;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtMaxRecordHis;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkEncrypt;
        private System.Windows.Forms.Label label5;
    }
}