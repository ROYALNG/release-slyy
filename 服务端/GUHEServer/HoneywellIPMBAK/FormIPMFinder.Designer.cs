namespace HoneywellIPM
{
    partial class FormIPMFinder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormIPMFinder));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnStopSearch = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.netFinder = new AxIPModuleLib.AxNetFinder();
            this.lstDev = new System.Windows.Forms.ListBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnSetIP = new System.Windows.Forms.Button();
            this.btnRangeSearch = new System.Windows.Forms.Button();
            this.btnBroadSearch = new System.Windows.Forms.Button();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lstLog = new System.Windows.Forms.ListBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.netFinder)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnStopSearch);
            this.splitContainer1.Panel1.Controls.Add(this.progressBar1);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.btnStop);
            this.splitContainer1.Panel1.Controls.Add(this.btnStart);
            this.splitContainer1.Panel1.Controls.Add(this.btnSetIP);
            this.splitContainer1.Panel1.Controls.Add(this.btnRangeSearch);
            this.splitContainer1.Panel1.Controls.Add(this.btnBroadSearch);
            this.splitContainer1.Panel1.Controls.Add(this.txtPort);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lstLog);
            this.splitContainer1.Size = new System.Drawing.Size(726, 558);
            this.splitContainer1.SplitterDistance = 397;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnStopSearch
            // 
            this.btnStopSearch.Enabled = false;
            this.btnStopSearch.Location = new System.Drawing.Point(46, 248);
            this.btnStopSearch.Name = "btnStopSearch";
            this.btnStopSearch.Size = new System.Drawing.Size(100, 28);
            this.btnStopSearch.TabIndex = 27;
            this.btnStopSearch.Text = "停止搜索";
            this.btnStopSearch.UseVisualStyleBackColor = true;
            this.btnStopSearch.Click += new System.EventHandler(this.btnStopSearch_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(46, 336);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(629, 28);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 25;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.netFinder);
            this.groupBox1.Controls.Add(this.lstDev);
            this.groupBox1.Location = new System.Drawing.Point(196, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(479, 274);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设备信息";
            // 
            // netFinder
            // 
            this.netFinder.Enabled = true;
            this.netFinder.Location = new System.Drawing.Point(174, 42);
            this.netFinder.Name = "netFinder";
            this.netFinder.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("netFinder.OcxState")));
            this.netFinder.Size = new System.Drawing.Size(192, 192);
            this.netFinder.TabIndex = 27;
            // 
            // lstDev
            // 
            this.lstDev.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.lstDev.FormattingEnabled = true;
            this.lstDev.ItemHeight = 12;
            this.lstDev.Location = new System.Drawing.Point(46, 30);
            this.lstDev.Name = "lstDev";
            this.lstDev.Size = new System.Drawing.Size(408, 232);
            this.lstDev.TabIndex = 0;
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(46, 116);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(100, 28);
            this.btnStop.TabIndex = 23;
            this.btnStop.Text = "停止搜索器";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(46, 72);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(100, 28);
            this.btnStart.TabIndex = 22;
            this.btnStart.Text = "启动搜索器";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnSetIP
            // 
            this.btnSetIP.Enabled = false;
            this.btnSetIP.Location = new System.Drawing.Point(46, 292);
            this.btnSetIP.Name = "btnSetIP";
            this.btnSetIP.Size = new System.Drawing.Size(100, 28);
            this.btnSetIP.TabIndex = 21;
            this.btnSetIP.Text = "配置IP";
            this.btnSetIP.UseVisualStyleBackColor = true;
            this.btnSetIP.Click += new System.EventHandler(this.btnSetIP_Click);
            // 
            // btnRangeSearch
            // 
            this.btnRangeSearch.Enabled = false;
            this.btnRangeSearch.Location = new System.Drawing.Point(46, 204);
            this.btnRangeSearch.Name = "btnRangeSearch";
            this.btnRangeSearch.Size = new System.Drawing.Size(100, 28);
            this.btnRangeSearch.TabIndex = 20;
            this.btnRangeSearch.Text = "指定搜索";
            this.btnRangeSearch.UseVisualStyleBackColor = true;
            this.btnRangeSearch.Click += new System.EventHandler(this.btnRangeSearch_Click);
            // 
            // btnBroadSearch
            // 
            this.btnBroadSearch.Enabled = false;
            this.btnBroadSearch.Location = new System.Drawing.Point(46, 160);
            this.btnBroadSearch.Name = "btnBroadSearch";
            this.btnBroadSearch.Size = new System.Drawing.Size(100, 28);
            this.btnBroadSearch.TabIndex = 19;
            this.btnBroadSearch.Text = "广播搜索";
            this.btnBroadSearch.UseVisualStyleBackColor = true;
            this.btnBroadSearch.Click += new System.EventHandler(this.btnBroadSearch_Click);
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(94, 42);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(55, 21);
            this.txtPort.TabIndex = 18;
            this.txtPort.Text = "3040";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 17;
            this.label1.Text = "端口号：";
            // 
            // lstLog
            // 
            this.lstLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstLog.FormattingEnabled = true;
            this.lstLog.ItemHeight = 12;
            this.lstLog.Location = new System.Drawing.Point(0, 0);
            this.lstLog.Name = "lstLog";
            this.lstLog.Size = new System.Drawing.Size(722, 148);
            this.lstLog.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FormIPMFinder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(726, 558);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FormIPMFinder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Honeywell IPM 远程管理";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.netFinder)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox lstLog;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lstDev;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnSetIP;
        private System.Windows.Forms.Button btnRangeSearch;
        private System.Windows.Forms.Button btnBroadSearch;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnStopSearch;
        private AxIPModuleLib.AxNetFinder netFinder;

    }
}