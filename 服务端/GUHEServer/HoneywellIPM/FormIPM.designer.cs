namespace GHIBMS.Server
{
    partial class FormIPM
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormIPM));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstDev = new System.Windows.Forms.ListBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnSetIP = new System.Windows.Forms.Button();
            this.btnRangeSearch = new System.Windows.Forms.Button();
            this.btnBroadSearch = new System.Windows.Forms.Button();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lboxMain = new System.Windows.Forms.ListBox();
            this.netFinder = new AxIPModuleLib.AxNetFinder();
            this.cooMonitor = new AxIPModuleLib.AxCooMonitor();
            this.menuStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.netFinder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cooMonitor)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(762, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(762, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 531);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(762, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 49);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lboxMain);
            this.splitContainer1.Size = new System.Drawing.Size(762, 482);
            this.splitContainer1.SplitterDistance = 356;
            this.splitContainer1.TabIndex = 6;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(758, 352);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.btnStop);
            this.tabPage1.Controls.Add(this.btnStart);
            this.tabPage1.Controls.Add(this.btnSetIP);
            this.tabPage1.Controls.Add(this.btnRangeSearch);
            this.tabPage1.Controls.Add(this.btnBroadSearch);
            this.tabPage1.Controls.Add(this.txtPort);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(750, 327);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "搜索主机";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstDev);
            this.groupBox1.Location = new System.Drawing.Point(240, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(479, 260);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设备信息";
            // 
            // lstDev
            // 
            this.lstDev.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.lstDev.FormattingEnabled = true;
            this.lstDev.ItemHeight = 12;
            this.lstDev.Location = new System.Drawing.Point(46, 30);
            this.lstDev.Name = "lstDev";
            this.lstDev.Size = new System.Drawing.Size(408, 220);
            this.lstDev.TabIndex = 0;
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(90, 126);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(105, 31);
            this.btnStop.TabIndex = 15;
            this.btnStop.Text = "停止搜索器";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(90, 79);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(105, 31);
            this.btnStart.TabIndex = 14;
            this.btnStart.Text = "启动搜索器";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnSetIP
            // 
            this.btnSetIP.Enabled = false;
            this.btnSetIP.Location = new System.Drawing.Point(90, 267);
            this.btnSetIP.Name = "btnSetIP";
            this.btnSetIP.Size = new System.Drawing.Size(105, 31);
            this.btnSetIP.TabIndex = 12;
            this.btnSetIP.Text = "配置IP";
            this.btnSetIP.UseVisualStyleBackColor = true;
            this.btnSetIP.Click += new System.EventHandler(this.btnSetIP_Click);
            // 
            // btnRangeSearch
            // 
            this.btnRangeSearch.Enabled = false;
            this.btnRangeSearch.Location = new System.Drawing.Point(90, 220);
            this.btnRangeSearch.Name = "btnRangeSearch";
            this.btnRangeSearch.Size = new System.Drawing.Size(105, 31);
            this.btnRangeSearch.TabIndex = 11;
            this.btnRangeSearch.Text = "指定搜索";
            this.btnRangeSearch.UseVisualStyleBackColor = true;
            this.btnRangeSearch.Click += new System.EventHandler(this.btnRangeSearch_Click);
            // 
            // btnBroadSearch
            // 
            this.btnBroadSearch.Enabled = false;
            this.btnBroadSearch.Location = new System.Drawing.Point(90, 173);
            this.btnBroadSearch.Name = "btnBroadSearch";
            this.btnBroadSearch.Size = new System.Drawing.Size(105, 31);
            this.btnBroadSearch.TabIndex = 10;
            this.btnBroadSearch.Text = "广播搜索";
            this.btnBroadSearch.UseVisualStyleBackColor = true;
            this.btnBroadSearch.Click += new System.EventHandler(this.btnBroadSearch_Click);
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(138, 49);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(55, 21);
            this.txtPort.TabIndex = 9;
            this.txtPort.Text = "3040";
            this.txtPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPort_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(88, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "端口号：";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(750, 327);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "监控主机";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lboxMain
            // 
            this.lboxMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lboxMain.FormattingEnabled = true;
            this.lboxMain.ItemHeight = 12;
            this.lboxMain.Location = new System.Drawing.Point(0, 0);
            this.lboxMain.Name = "lboxMain";
            this.lboxMain.Size = new System.Drawing.Size(758, 112);
            this.lboxMain.TabIndex = 3;
            // 
            // netFinder
            // 
            this.netFinder.Enabled = true;
            this.netFinder.Location = new System.Drawing.Point(508, 54);
            this.netFinder.Name = "netFinder";
            this.netFinder.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("netFinder.OcxState")));
            this.netFinder.Size = new System.Drawing.Size(192, 192);
            this.netFinder.TabIndex = 7;
            this.netFinder.DeviceReport += new AxIPModuleLib._INetFinderEvents_DeviceReportEventHandler(this.netFinder_DeviceReport);
            // 
            // cooMonitor
            // 
            this.cooMonitor.Enabled = true;
            this.cooMonitor.Location = new System.Drawing.Point(310, 52);
            this.cooMonitor.Name = "cooMonitor";
            this.cooMonitor.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("cooMonitor.OcxState")));
            this.cooMonitor.Size = new System.Drawing.Size(192, 192);
            this.cooMonitor.TabIndex = 9;
            // 
            // FormIPM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 553);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.cooMonitor);
            this.Controls.Add(this.netFinder);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormIPM";
            this.Text = "Honeywell IPM";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.netFinder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cooMonitor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox lboxMain;
        private AxIPModuleLib.AxNetFinder netFinder;
        private AxIPModuleLib.AxCooMonitor cooMonitor;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnSetIP;
        private System.Windows.Forms.Button btnRangeSearch;
        private System.Windows.Forms.Button btnBroadSearch;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lstDev;
    }
}

