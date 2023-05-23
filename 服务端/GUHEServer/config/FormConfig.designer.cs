namespace GHIBMS.Server
{
    partial class FormConfig
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtDbPort = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkDB = new System.Windows.Forms.CheckBox();
            this.btnTestLogDB = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxDBPw = new System.Windows.Forms.TextBox();
            this.Label21 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxDBUser = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxDBName = new System.Windows.Forms.TextBox();
            this.textBoxDBHost = new System.Windows.Forms.TextBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.fldDlg = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtInfluxName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkInflux = new System.Windows.Forms.CheckBox();
            this.txtInfluxPort = new System.Windows.Forms.TextBox();
            this.btnTestInflux = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtInfluxPw = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtInfluxUser = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtInfluxIP = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.chkModbusEnable = new System.Windows.Forms.CheckBox();
            this.txtModbusPort = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtModbsuIp = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.txtDbPort);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.chkDB);
            this.groupBox2.Controls.Add(this.btnTestLogDB);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.textBoxDBPw);
            this.groupBox2.Controls.Add(this.Label21);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.textBoxDBUser);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.textBoxDBName);
            this.groupBox2.Controls.Add(this.textBoxDBHost);
            this.groupBox2.Location = new System.Drawing.Point(22, 208);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(445, 170);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "边缘存储历史数据(MySql)";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(39, 95);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 12);
            this.label11.TabIndex = 27;
            this.label11.Text = "端  口";
            // 
            // txtDbPort
            // 
            this.txtDbPort.Location = new System.Drawing.Point(95, 92);
            this.txtDbPort.Name = "txtDbPort";
            this.txtDbPort.Size = new System.Drawing.Size(100, 21);
            this.txtDbPort.TabIndex = 26;
            this.txtDbPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtInfluxPort_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 25;
            this.label1.Text = "数据库";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // chkDB
            // 
            this.chkDB.Checked = true;
            this.chkDB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDB.Location = new System.Drawing.Point(118, 30);
            this.chkDB.Name = "chkDB";
            this.chkDB.Size = new System.Drawing.Size(100, 23);
            this.chkDB.TabIndex = 23;
            this.chkDB.CheckedChanged += new System.EventHandler(this.chkDB_CheckedChanged);
            // 
            // btnTestLogDB
            // 
            this.btnTestLogDB.Location = new System.Drawing.Point(356, 131);
            this.btnTestLogDB.Name = "btnTestLogDB";
            this.btnTestLogDB.Size = new System.Drawing.Size(50, 23);
            this.btnTestLogDB.TabIndex = 12;
            this.btnTestLogDB.Text = "测试";
            this.btnTestLogDB.UseVisualStyleBackColor = true;
            this.btnTestLogDB.Click += new System.EventHandler(this.btnTestLogDB_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(41, 62);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 9;
            this.label8.Text = "IP地址";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // textBoxDBPw
            // 
            this.textBoxDBPw.Location = new System.Drawing.Point(308, 94);
            this.textBoxDBPw.Name = "textBoxDBPw";
            this.textBoxDBPw.PasswordChar = '*';
            this.textBoxDBPw.Size = new System.Drawing.Size(100, 21);
            this.textBoxDBPw.TabIndex = 3;
            // 
            // Label21
            // 
            this.Label21.AutoSize = true;
            this.Label21.Location = new System.Drawing.Point(41, 34);
            this.Label21.Name = "Label21";
            this.Label21.Size = new System.Drawing.Size(41, 12);
            this.Label21.TabIndex = 24;
            this.Label21.Text = "启用：";
            this.Label21.Click += new System.EventHandler(this.Label21_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(237, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "密  码";
            // 
            // textBoxDBUser
            // 
            this.textBoxDBUser.Location = new System.Drawing.Point(308, 58);
            this.textBoxDBUser.Name = "textBoxDBUser";
            this.textBoxDBUser.Size = new System.Drawing.Size(100, 21);
            this.textBoxDBUser.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(237, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "用户名";
            // 
            // textBoxDBName
            // 
            this.textBoxDBName.Location = new System.Drawing.Point(95, 128);
            this.textBoxDBName.Name = "textBoxDBName";
            this.textBoxDBName.Size = new System.Drawing.Size(100, 21);
            this.textBoxDBName.TabIndex = 1;
            // 
            // textBoxDBHost
            // 
            this.textBoxDBHost.Location = new System.Drawing.Point(96, 60);
            this.textBoxDBHost.Name = "textBoxDBHost";
            this.textBoxDBHost.Size = new System.Drawing.Size(100, 21);
            this.textBoxDBHost.TabIndex = 0;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonOk.Location = new System.Drawing.Point(490, 429);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(91, 28);
            this.buttonOk.TabIndex = 8;
            this.buttonOk.Text = "保存";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(490, 473);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(91, 27);
            this.buttonCancel.TabIndex = 9;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtInfluxName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.chkInflux);
            this.groupBox1.Controls.Add(this.txtInfluxPort);
            this.groupBox1.Controls.Add(this.btnTestInflux);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtInfluxPw);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtInfluxUser);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtInfluxIP);
            this.groupBox1.Location = new System.Drawing.Point(22, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(445, 172);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "边缘存储历史数据（InfluxDB）";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(39, 122);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 28;
            this.label10.Text = "数据库";
            // 
            // txtInfluxName
            // 
            this.txtInfluxName.Location = new System.Drawing.Point(95, 122);
            this.txtInfluxName.Name = "txtInfluxName";
            this.txtInfluxName.Size = new System.Drawing.Size(101, 21);
            this.txtInfluxName.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 26;
            this.label2.Text = "端  口";
            // 
            // chkInflux
            // 
            this.chkInflux.Checked = true;
            this.chkInflux.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkInflux.Location = new System.Drawing.Point(128, 27);
            this.chkInflux.Name = "chkInflux";
            this.chkInflux.Size = new System.Drawing.Size(82, 23);
            this.chkInflux.TabIndex = 23;
            this.chkInflux.CheckedChanged += new System.EventHandler(this.chkInflux_CheckedChanged);
            // 
            // txtInfluxPort
            // 
            this.txtInfluxPort.Location = new System.Drawing.Point(96, 91);
            this.txtInfluxPort.Name = "txtInfluxPort";
            this.txtInfluxPort.Size = new System.Drawing.Size(101, 21);
            this.txtInfluxPort.TabIndex = 25;
            this.txtInfluxPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtInfluxPort_KeyPress);
            // 
            // btnTestInflux
            // 
            this.btnTestInflux.Location = new System.Drawing.Point(356, 131);
            this.btnTestInflux.Name = "btnTestInflux";
            this.btnTestInflux.Size = new System.Drawing.Size(50, 23);
            this.btnTestInflux.TabIndex = 12;
            this.btnTestInflux.Text = "测试";
            this.btnTestInflux.UseVisualStyleBackColor = true;
            this.btnTestInflux.Click += new System.EventHandler(this.btnTestInflux_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(39, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 24;
            this.label5.Text = "启用：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(39, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "IP地址";
            // 
            // txtInfluxPw
            // 
            this.txtInfluxPw.Location = new System.Drawing.Point(308, 95);
            this.txtInfluxPw.Name = "txtInfluxPw";
            this.txtInfluxPw.PasswordChar = '*';
            this.txtInfluxPw.Size = new System.Drawing.Size(100, 21);
            this.txtInfluxPw.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(237, 96);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "密  码";
            // 
            // txtInfluxUser
            // 
            this.txtInfluxUser.Location = new System.Drawing.Point(309, 62);
            this.txtInfluxUser.Name = "txtInfluxUser";
            this.txtInfluxUser.Size = new System.Drawing.Size(100, 21);
            this.txtInfluxUser.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(237, 63);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 4;
            this.label9.Text = "用户名";
            // 
            // txtInfluxIP
            // 
            this.txtInfluxIP.Location = new System.Drawing.Point(95, 62);
            this.txtInfluxIP.Name = "txtInfluxIP";
            this.txtInfluxIP.Size = new System.Drawing.Size(101, 21);
            this.txtInfluxIP.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.chkModbusEnable);
            this.groupBox3.Controls.Add(this.txtModbusPort);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.txtModbsuIp);
            this.groupBox3.Location = new System.Drawing.Point(22, 384);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(442, 129);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "边缘转发实时数据（Modbus-TCP）";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(40, 89);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 12);
            this.label13.TabIndex = 26;
            this.label13.Text = "端  口";
            // 
            // chkModbusEnable
            // 
            this.chkModbusEnable.Checked = true;
            this.chkModbusEnable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkModbusEnable.Location = new System.Drawing.Point(128, 27);
            this.chkModbusEnable.Name = "chkModbusEnable";
            this.chkModbusEnable.Size = new System.Drawing.Size(82, 23);
            this.chkModbusEnable.TabIndex = 23;
            // 
            // txtModbusPort
            // 
            this.txtModbusPort.Location = new System.Drawing.Point(96, 89);
            this.txtModbusPort.Name = "txtModbusPort";
            this.txtModbusPort.Size = new System.Drawing.Size(101, 21);
            this.txtModbusPort.TabIndex = 25;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(356, 86);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "测试";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(39, 32);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(41, 12);
            this.label14.TabIndex = 24;
            this.label14.Text = "启用：";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(39, 60);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(41, 12);
            this.label15.TabIndex = 9;
            this.label15.Text = "IP地址";
            // 
            // txtModbsuIp
            // 
            this.txtModbsuIp.Location = new System.Drawing.Point(95, 60);
            this.txtModbsuIp.Name = "txtModbsuIp";
            this.txtModbsuIp.Size = new System.Drawing.Size(101, 21);
            this.txtModbsuIp.TabIndex = 0;
            // 
            // FormConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 528);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.groupBox2);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormConfig";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "边缘存储配置";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxDBPw;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxDBUser;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxDBName;
        private System.Windows.Forms.TextBox textBoxDBHost;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.FolderBrowserDialog fldDlg;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnTestLogDB;
        private System.Windows.Forms.Label Label21;
        private System.Windows.Forms.CheckBox chkDB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkInflux;
        private System.Windows.Forms.Button btnTestInflux;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtInfluxPw;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtInfluxUser;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtInfluxIP;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtDbPort;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtInfluxName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtInfluxPort;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox chkModbusEnable;
        private System.Windows.Forms.TextBox txtModbusPort;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtModbsuIp;
    }
}