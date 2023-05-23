namespace GHIBMS.NetVideo
{
    partial class NetVideo
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NetVideo));
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.lblCamTitle = new DevComponents.DotNetBar.LabelItem();
            this.btnPlay = new DevComponents.DotNetBar.ButtonItem();
            this.btnStop = new DevComponents.DotNetBar.ButtonItem();
            this.btnRecord = new DevComponents.DotNetBar.ButtonItem();
            this.btnCapture = new DevComponents.DotNetBar.ButtonItem();
            this.btnSound = new DevComponents.DotNetBar.ButtonItem();
            this.btnOpenSound = new DevComponents.DotNetBar.ButtonItem();
            this.btnMuteSound = new DevComponents.DotNetBar.ButtonItem();
            this.btnStartPhone = new DevComponents.DotNetBar.ButtonItem();
            this.btnStopPhone = new DevComponents.DotNetBar.ButtonItem();
            this.sldVoice = new DevComponents.DotNetBar.SliderItem();
            this.btnConfig = new DevComponents.DotNetBar.ButtonItem();
            this.panelPlay = new DevComponents.DotNetBar.PanelEx();
            this.btnPTZ = new DevComponents.DotNetBar.ButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.SuspendLayout();
            // 
            // bar1
            // 
            this.bar1.AntiAlias = true;
            this.bar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblCamTitle,
            this.btnPlay,
            this.btnStop,
            this.btnRecord,
            this.btnCapture,
            this.btnSound,
            this.btnPTZ,
            this.btnConfig});
            this.bar1.Location = new System.Drawing.Point(0, 288);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(352, 25);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar1.TabIndex = 0;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // lblCamTitle
            // 
            this.lblCamTitle.Image = ((System.Drawing.Image)(resources.GetObject("lblCamTitle.Image")));
            this.lblCamTitle.Name = "lblCamTitle";
            this.lblCamTitle.Text = "摄像机标题";
            // 
            // btnPlay
            // 
            this.btnPlay.Image = ((System.Drawing.Image)(resources.GetObject("btnPlay.Image")));
            this.btnPlay.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Text = "buttonItem1";
            // 
            // btnStop
            // 
            this.btnStop.Image = ((System.Drawing.Image)(resources.GetObject("btnStop.Image")));
            this.btnStop.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.btnStop.Name = "btnStop";
            this.btnStop.Text = "buttonItem2";
            // 
            // btnRecord
            // 
            this.btnRecord.Image = ((System.Drawing.Image)(resources.GetObject("btnRecord.Image")));
            this.btnRecord.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Text = "buttonItem3";
            // 
            // btnCapture
            // 
            this.btnCapture.Image = ((System.Drawing.Image)(resources.GetObject("btnCapture.Image")));
            this.btnCapture.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Text = "buttonItem4";
            // 
            // btnSound
            // 
            this.btnSound.Image = ((System.Drawing.Image)(resources.GetObject("btnSound.Image")));
            this.btnSound.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.btnSound.Name = "btnSound";
            this.btnSound.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnOpenSound,
            this.btnMuteSound,
            this.btnStartPhone,
            this.btnStopPhone,
            this.sldVoice});
            this.btnSound.Text = "buttonItem5";
            // 
            // btnOpenSound
            // 
            this.btnOpenSound.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenSound.Image")));
            this.btnOpenSound.Name = "btnOpenSound";
            this.btnOpenSound.Text = "打开声音";
            // 
            // btnMuteSound
            // 
            this.btnMuteSound.Image = ((System.Drawing.Image)(resources.GetObject("btnMuteSound.Image")));
            this.btnMuteSound.Name = "btnMuteSound";
            this.btnMuteSound.Text = "关闭声音";
            // 
            // btnStartPhone
            // 
            this.btnStartPhone.Image = ((System.Drawing.Image)(resources.GetObject("btnStartPhone.Image")));
            this.btnStartPhone.Name = "btnStartPhone";
            this.btnStartPhone.Text = "打开对讲";
            // 
            // btnStopPhone
            // 
            this.btnStopPhone.Image = ((System.Drawing.Image)(resources.GetObject("btnStopPhone.Image")));
            this.btnStopPhone.Name = "btnStopPhone";
            this.btnStopPhone.Text = "关闭对讲";
            // 
            // sldVoice
            // 
            this.sldVoice.Name = "sldVoice";
            this.sldVoice.Text = "音量";
            this.sldVoice.Value = 0;
            // 
            // btnConfig
            // 
            this.btnConfig.Image = ((System.Drawing.Image)(resources.GetObject("btnConfig.Image")));
            this.btnConfig.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Text = "buttonItem7";
            // 
            // panelPlay
            // 
            this.panelPlay.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelPlay.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelPlay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPlay.Location = new System.Drawing.Point(0, 0);
            this.panelPlay.Margin = new System.Windows.Forms.Padding(1);
            this.panelPlay.Name = "panelPlay";
            this.panelPlay.Size = new System.Drawing.Size(352, 288);
            this.panelPlay.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelPlay.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelPlay.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelPlay.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelPlay.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelPlay.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelPlay.Style.GradientAngle = 90;
            this.panelPlay.TabIndex = 1;
            // 
            // btnPTZ
            // 
            this.btnPTZ.Image = ((System.Drawing.Image)(resources.GetObject("btnPTZ.Image")));
            this.btnPTZ.Name = "btnPTZ";
            // 
            // NetVideo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.RoyalBlue;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.panelPlay);
            this.Controls.Add(this.bar1);
            this.ForeColor = System.Drawing.Color.MidnightBlue;
            this.Name = "NetVideo";
            this.Size = new System.Drawing.Size(352, 313);
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.LabelItem lblCamTitle;
        private DevComponents.DotNetBar.ButtonItem btnPlay;
        private DevComponents.DotNetBar.ButtonItem btnStop;
        private DevComponents.DotNetBar.ButtonItem btnRecord;
        private DevComponents.DotNetBar.ButtonItem btnCapture;
        private DevComponents.DotNetBar.PanelEx panelPlay;
        private DevComponents.DotNetBar.ButtonItem btnSound;
        private DevComponents.DotNetBar.ButtonItem btnConfig;
        private DevComponents.DotNetBar.SliderItem sldVoice;
        private DevComponents.DotNetBar.ButtonItem btnOpenSound;
        private DevComponents.DotNetBar.ButtonItem btnMuteSound;
        private DevComponents.DotNetBar.ButtonItem btnStartPhone;
        private DevComponents.DotNetBar.ButtonItem btnStopPhone;
        private DevComponents.DotNetBar.ButtonItem btnPTZ;
    }
}
