namespace GHIBMS.Server
{
    partial class FormNetLink
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
            this.txtNetServerIP = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.txtNetPort = new  System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.txtNetID = new  System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.txtNetInterval = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.btxOK = new System.Windows.Forms.Button();
            this.btxCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.txtNetServerIP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNetPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNetID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNetInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // Label1
            // 
            // 
            // 
            // 
            this.Label1.Location = new System.Drawing.Point(103, 57);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(75, 23);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "IP地址：";
            // 
            // txtNetServerIP
            // 
            // 
            // 
            // 
            this.txtNetServerIP.Location = new System.Drawing.Point(171, 57);
            this.txtNetServerIP.Name = "txtNetServerIP";
            this.txtNetServerIP.Size = new System.Drawing.Size(137, 21);
            this.txtNetServerIP.TabIndex = 1;
            // 
            // Label2
            // 
            // 
            // 
            // 
            this.Label2.Location = new System.Drawing.Point(103, 100);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(75, 23);
            this.Label2.TabIndex = 2;
            this.Label2.Text = "端口号：";
            // 
            // txtNetPort
            // 
            // 
            // 
            // 
            this.txtNetPort.Location = new System.Drawing.Point(171, 101);
            this.txtNetPort.Name = "txtNetPort";
            this.txtNetPort.Size = new System.Drawing.Size(80, 21);
            this.txtNetPort.TabIndex = 3;
            // 
            // Label3
            // 
            // 
            // 
            // 
            this.Label3.Location = new System.Drawing.Point(103, 150);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(75, 23);
            this.Label3.TabIndex = 4;
            this.Label3.Text = "子站编号：";
            // 
            // txtNetID
            // 
            // 
            // 
            // 
            this.txtNetID.Location = new System.Drawing.Point(171, 150);
            this.txtNetID.Name = "txtNetID";
            this.txtNetID.Size = new System.Drawing.Size(80, 21);
            this.txtNetID.TabIndex = 5;
            // 
            // Label4
            // 
            // 
            // 
            // 
            this.Label4.Location = new System.Drawing.Point(103, 191);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(75, 23);
            this.Label4.TabIndex = 6;
            this.Label4.Text = "通讯间隔：";
            // 
            // txtNetInterval
            // 
            // 
            // 
            // 
            this.txtNetInterval.Location = new System.Drawing.Point(171, 191);
            this.txtNetInterval.Name = "txtNetInterval";
            this.txtNetInterval.Size = new System.Drawing.Size(80, 21);
            this.txtNetInterval.TabIndex = 7;
            // 
            // Label5
            // 
            // 
            // 
            // 
            this.Label5.Location = new System.Drawing.Point(268, 191);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(75, 23);
            this.Label5.TabIndex = 8;
            this.Label5.Text = "秒";
            // 
            // btxOK
            // 
            this.btxOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btxOK.Location = new System.Drawing.Point(175, 258);
            this.btxOK.Name = "btxOK";
            this.btxOK.Size = new System.Drawing.Size(75, 23);
            this.btxOK.TabIndex = 9;
            this.btxOK.Text = "确定";
            this.btxOK.Click += new System.EventHandler(this.btxOK_Click);
            // 
            // btxCancel
            // 
            this.btxCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btxCancel.Location = new System.Drawing.Point(292, 258);
            this.btxCancel.Name = "btxCancel";
            this.btxCancel.Size = new System.Drawing.Size(75, 23);
            this.btxCancel.TabIndex = 10;
            this.btxCancel.Text = "取消";
            this.btxCancel.Click += new System.EventHandler(this.btxCancel_Click);
            // 
            // FormNetLink
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 316);
            this.Controls.Add(this.btxCancel);
            this.Controls.Add(this.btxOK);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.txtNetInterval);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.txtNetID);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.txtNetPort);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.txtNetServerIP);
            this.Controls.Add(this.Label1);
            this.Name = "FormNetLink";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "连网上传参数";
            this.Shown += new System.EventHandler(this.FormNetLink_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.txtNetServerIP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNetPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNetID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNetInterval)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.TextBox txtNetServerIP;
        private System.Windows.Forms.Label Label2;
        private System.Windows.Forms.TextBox txtNetPort;
        private System.Windows.Forms.Label Label3;
        private System.Windows.Forms.TextBox txtNetID;
        private System.Windows.Forms.Label Label4;
        private System.Windows.Forms.TextBox txtNetInterval;
        private System.Windows.Forms.Label Label5;
        private System.Windows.Forms.Button btxOK;
        private System.Windows.Forms.Button btxCancel;
    }
}