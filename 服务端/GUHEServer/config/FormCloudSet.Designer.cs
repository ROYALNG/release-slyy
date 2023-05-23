namespace GHIBMS.Server
{
    partial class FormCloudSet
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
            this.Label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.btxCancel = new System.Windows.Forms.Button();
            this.btxOK = new System.Windows.Forms.Button();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.txtDeviceID = new System.Windows.Forms.TextBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.txtDeviceKey = new System.Windows.Forms.TextBox();
            this.Label9 = new System.Windows.Forms.Label();
            this.chkCloud = new System.Windows.Forms.CheckBox();
            this.Label10 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chkCompatibleV1 = new System.Windows.Forms.CheckBox();
            this.txtRedis = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMNS = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.chkMns = new System.Windows.Forms.CheckBox();
            this.txtRedisPw = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.chkUseRedis = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(15, 56);
            this.Label1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(134, 18);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "云服务登录名：";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(170, 54);
            this.txtName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(151, 28);
            this.txtName.TabIndex = 1;
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(170, 117);
            this.txtPass.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPass.Name = "txtPass";
            this.txtPass.PasswordChar = '*';
            this.txtPass.Size = new System.Drawing.Size(151, 28);
            this.txtPass.TabIndex = 3;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(15, 117);
            this.Label2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(116, 18);
            this.Label2.TabIndex = 2;
            this.Label2.Text = "云服务密码：";
            // 
            // btxCancel
            // 
            this.btxCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btxCancel.Location = new System.Drawing.Point(646, 822);
            this.btxCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btxCancel.Name = "btxCancel";
            this.btxCancel.Size = new System.Drawing.Size(112, 34);
            this.btxCancel.TabIndex = 12;
            this.btxCancel.Text = "取消";
            this.btxCancel.Click += new System.EventHandler(this.btxCancel_Click);
            // 
            // btxOK
            // 
            this.btxOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btxOK.Location = new System.Drawing.Point(456, 822);
            this.btxOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btxOK.Name = "btxOK";
            this.btxOK.Size = new System.Drawing.Size(112, 34);
            this.btxOK.TabIndex = 11;
            this.btxOK.Text = "确定";
            this.btxOK.Click += new System.EventHandler(this.btxOK_Click);
            // 
            // txtLogin
            // 
            this.txtLogin.Location = new System.Drawing.Point(170, 165);
            this.txtLogin.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(482, 28);
            this.txtLogin.TabIndex = 14;
            this.txtLogin.Text = "127.0.0.1:9011";
            this.txtLogin.Visible = false;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(15, 174);
            this.Label6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(116, 18);
            this.Label6.TabIndex = 13;
            this.Label6.Text = "登录认证器：";
            this.Label6.Visible = false;
            // 
            // txtDeviceID
            // 
            this.txtDeviceID.Location = new System.Drawing.Point(474, 54);
            this.txtDeviceID.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDeviceID.Name = "txtDeviceID";
            this.txtDeviceID.Size = new System.Drawing.Size(178, 28);
            this.txtDeviceID.TabIndex = 18;
            this.txtDeviceID.TextChanged += new System.EventHandler(this.txtDeviceID_TextChanged);
            this.txtDeviceID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDeviceID_KeyPress);
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(363, 122);
            this.Label8.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(107, 18);
            this.Label8.TabIndex = 17;
            this.Label8.Text = "采集器KEY：";
            // 
            // txtDeviceKey
            // 
            this.txtDeviceKey.Location = new System.Drawing.Point(474, 112);
            this.txtDeviceKey.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDeviceKey.Name = "txtDeviceKey";
            this.txtDeviceKey.Size = new System.Drawing.Size(178, 28);
            this.txtDeviceKey.TabIndex = 20;
            this.txtDeviceKey.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIOName_KeyPress);
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(368, 58);
            this.Label9.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(98, 18);
            this.Label9.TabIndex = 19;
            this.Label9.Text = "采集器ID：";
            this.Label9.Click += new System.EventHandler(this.Label9_Click);
            // 
            // chkCloud
            // 
            this.chkCloud.Location = new System.Drawing.Point(105, 48);
            this.chkCloud.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkCloud.Name = "chkCloud";
            this.chkCloud.Size = new System.Drawing.Size(27, 34);
            this.chkCloud.TabIndex = 21;
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(39, 57);
            this.Label10.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(62, 18);
            this.Label10.TabIndex = 22;
            this.Label10.Text = "启用：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(333, 57);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 18);
            this.label3.TabIndex = 24;
            this.label3.Text = "兼容V1.0：";
            // 
            // chkCompatibleV1
            // 
            this.chkCompatibleV1.Location = new System.Drawing.Point(438, 48);
            this.chkCompatibleV1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkCompatibleV1.Name = "chkCompatibleV1";
            this.chkCompatibleV1.Size = new System.Drawing.Size(27, 34);
            this.chkCompatibleV1.TabIndex = 23;
            // 
            // txtRedis
            // 
            this.txtRedis.Location = new System.Drawing.Point(159, 78);
            this.txtRedis.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtRedis.Name = "txtRedis";
            this.txtRedis.Size = new System.Drawing.Size(482, 28);
            this.txtRedis.TabIndex = 26;
            this.txtRedis.Text = "127.0.0.1:32770";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 81);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 18);
            this.label4.TabIndex = 25;
            this.label4.Text = "IP和端口：";
            // 
            // txtMNS
            // 
            this.txtMNS.Location = new System.Drawing.Point(160, 78);
            this.txtMNS.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtMNS.Name = "txtMNS";
            this.txtMNS.Size = new System.Drawing.Size(482, 28);
            this.txtMNS.TabIndex = 28;
            this.txtMNS.Text = "127.0.0.1:9092";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 81);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 18);
            this.label5.TabIndex = 27;
            this.label5.Text = "Kafka服务器：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 33);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(143, 18);
            this.label7.TabIndex = 30;
            this.label7.Text = "是否采用KAFKA：";
            // 
            // chkMns
            // 
            this.chkMns.Location = new System.Drawing.Point(160, 26);
            this.chkMns.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkMns.Name = "chkMns";
            this.chkMns.Size = new System.Drawing.Size(27, 34);
            this.chkMns.TabIndex = 29;
            this.chkMns.CheckedChanged += new System.EventHandler(this.chkMns_CheckedChanged);
            // 
            // txtRedisPw
            // 
            this.txtRedisPw.Location = new System.Drawing.Point(159, 129);
            this.txtRedisPw.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtRedisPw.Name = "txtRedisPw";
            this.txtRedisPw.PasswordChar = '*';
            this.txtRedisPw.Size = new System.Drawing.Size(482, 28);
            this.txtRedisPw.TabIndex = 32;
            this.txtRedisPw.Text = "Njgh8888";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(57, 129);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(62, 18);
            this.label11.TabIndex = 31;
            this.label11.Text = "密码：";
            this.label11.Click += new System.EventHandler(this.label11_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.chkUseRedis);
            this.groupBox1.Controls.Add(this.txtRedis);
            this.groupBox1.Controls.Add(this.txtRedisPw);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Location = new System.Drawing.Point(86, 429);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(682, 184);
            this.groupBox1.TabIndex = 33;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "实时数据库";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 38);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(143, 18);
            this.label12.TabIndex = 34;
            this.label12.Text = "是否采用REDIS：";
            // 
            // chkUseRedis
            // 
            this.chkUseRedis.Location = new System.Drawing.Point(160, 30);
            this.chkUseRedis.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkUseRedis.Name = "chkUseRedis";
            this.chkUseRedis.Size = new System.Drawing.Size(27, 34);
            this.chkUseRedis.TabIndex = 33;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtMNS);
            this.groupBox2.Controls.Add(this.chkMns);
            this.groupBox2.Location = new System.Drawing.Point(86, 660);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(682, 118);
            this.groupBox2.TabIndex = 34;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "消息队列";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.Label1);
            this.groupBox3.Controls.Add(this.txtName);
            this.groupBox3.Controls.Add(this.Label2);
            this.groupBox3.Controls.Add(this.txtPass);
            this.groupBox3.Controls.Add(this.Label6);
            this.groupBox3.Controls.Add(this.txtLogin);
            this.groupBox3.Controls.Add(this.Label8);
            this.groupBox3.Controls.Add(this.txtDeviceKey);
            this.groupBox3.Controls.Add(this.txtDeviceID);
            this.groupBox3.Controls.Add(this.Label9);
            this.groupBox3.Location = new System.Drawing.Point(86, 168);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(682, 232);
            this.groupBox3.TabIndex = 35;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "登录认证";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.Label10);
            this.groupBox4.Controls.Add(this.chkCloud);
            this.groupBox4.Controls.Add(this.chkCompatibleV1);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Location = new System.Drawing.Point(86, 30);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(674, 117);
            this.groupBox4.TabIndex = 36;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "启用/禁用";
            // 
            // FormCloudSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 891);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btxCancel);
            this.Controls.Add(this.btxOK);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCloudSet";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "直连云平台参数配置";
            this.Load += new System.EventHandler(this.FormCloudSet_Load);
            this.Shown += new System.EventHandler(this.FormCloudSet_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Label Label2;
        private System.Windows.Forms.Button btxCancel;
        private System.Windows.Forms.Button btxOK;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.Label Label6;
        private System.Windows.Forms.TextBox txtDeviceID;
        private System.Windows.Forms.Label Label8;
        private System.Windows.Forms.TextBox txtDeviceKey;
        private System.Windows.Forms.Label Label9;
        private System.Windows.Forms.CheckBox chkCloud;
        private System.Windows.Forms.Label Label10;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkCompatibleV1;
        private System.Windows.Forms.TextBox txtRedis;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMNS;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkMns;
        private System.Windows.Forms.TextBox txtRedisPw;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox chkUseRedis;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
    }
}