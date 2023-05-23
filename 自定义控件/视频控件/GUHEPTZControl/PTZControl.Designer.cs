namespace GHIBMS.PTZControl
{
    partial class PTZControl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PTZControl));
            this.btnZoomIn = new DevComponents.DotNetBar.ButtonX();
            this.VideoCommand = new DevComponents.DotNetBar.Command(this.components);
            this.btnZoomOut = new DevComponents.DotNetBar.ButtonX();
            this.btnFocus = new DevComponents.DotNetBar.ButtonX();
            this.btnFocusNear = new DevComponents.DotNetBar.ButtonX();
            this.btnIrisClose = new DevComponents.DotNetBar.ButtonX();
            this.btnIrisOpen = new DevComponents.DotNetBar.ButtonX();
            this.btnWiperOff = new DevComponents.DotNetBar.ButtonX();
            this.btnWiperOn = new DevComponents.DotNetBar.ButtonX();
            this.btnLightOff = new DevComponents.DotNetBar.ButtonX();
            this.btnLightOn = new DevComponents.DotNetBar.ButtonX();
            this.sliderSpeed = new DevComponents.DotNetBar.Controls.Slider();
            this.panControl = new GHIBMS.PTZControl.PanControl();
            this.SuspendLayout();
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnZoomIn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnZoomIn.Command = this.VideoCommand;
            this.btnZoomIn.CommandParameter = "ZOOM_IN";
            this.btnZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("btnZoomIn.Image")));
            this.btnZoomIn.Location = new System.Drawing.Point(158, 13);
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(49, 23);
            this.btnZoomIn.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnZoomIn.TabIndex = 1;
            this.btnZoomIn.Tooltip = "变倍[放大]";
            this.btnZoomIn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PTZButtonMouseDown);
            this.btnZoomIn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PTZButtonMouseUp);
            // 
            // VideoCommand
            // 
            this.VideoCommand.Name = "VideoCommand";
            this.VideoCommand.Executed += new System.EventHandler(this.PTZCommand_Executed);
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnZoomOut.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnZoomOut.Command = this.VideoCommand;
            this.btnZoomOut.CommandParameter = "ZOOM_OUT";
            this.btnZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("btnZoomOut.Image")));
            this.btnZoomOut.Location = new System.Drawing.Point(218, 13);
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(49, 23);
            this.btnZoomOut.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnZoomOut.TabIndex = 2;
            this.btnZoomOut.Tooltip = "变倍[缩小]";
            this.btnZoomOut.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PTZButtonMouseDown);
            this.btnZoomOut.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PTZButtonMouseUp);
            // 
            // btnFocus
            // 
            this.btnFocus.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnFocus.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnFocus.Command = this.VideoCommand;
            this.btnFocus.CommandParameter = "FOCUS_FAR";
            this.btnFocus.Image = ((System.Drawing.Image)(resources.GetObject("btnFocus.Image")));
            this.btnFocus.Location = new System.Drawing.Point(218, 47);
            this.btnFocus.Name = "btnFocus";
            this.btnFocus.Size = new System.Drawing.Size(49, 23);
            this.btnFocus.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnFocus.TabIndex = 4;
            this.btnFocus.Tooltip = "变焦[远景]";
            this.btnFocus.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PTZButtonMouseDown);
            this.btnFocus.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PTZButtonMouseUp);
            // 
            // btnFocusNear
            // 
            this.btnFocusNear.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnFocusNear.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnFocusNear.Command = this.VideoCommand;
            this.btnFocusNear.CommandParameter = "FOCUS_NEAR";
            this.btnFocusNear.Image = ((System.Drawing.Image)(resources.GetObject("btnFocusNear.Image")));
            this.btnFocusNear.Location = new System.Drawing.Point(158, 47);
            this.btnFocusNear.Name = "btnFocusNear";
            this.btnFocusNear.Size = new System.Drawing.Size(49, 23);
            this.btnFocusNear.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnFocusNear.TabIndex = 3;
            this.btnFocusNear.Tooltip = "变焦[近景]";
            this.btnFocusNear.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PTZButtonMouseDown);
            this.btnFocusNear.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PTZButtonMouseUp);
            // 
            // btnIrisClose
            // 
            this.btnIrisClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnIrisClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnIrisClose.Command = this.VideoCommand;
            this.btnIrisClose.CommandParameter = "IRIS_CLOSE";
            this.btnIrisClose.Image = ((System.Drawing.Image)(resources.GetObject("btnIrisClose.Image")));
            this.btnIrisClose.Location = new System.Drawing.Point(218, 81);
            this.btnIrisClose.Name = "btnIrisClose";
            this.btnIrisClose.Size = new System.Drawing.Size(49, 23);
            this.btnIrisClose.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnIrisClose.TabIndex = 6;
            this.btnIrisClose.Tooltip = "光圈[变暗]";
            this.btnIrisClose.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PTZButtonMouseDown);
            this.btnIrisClose.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PTZButtonMouseUp);
            // 
            // btnIrisOpen
            // 
            this.btnIrisOpen.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnIrisOpen.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnIrisOpen.Command = this.VideoCommand;
            this.btnIrisOpen.CommandParameter = "IRIS_OPEN";
            this.btnIrisOpen.Image = ((System.Drawing.Image)(resources.GetObject("btnIrisOpen.Image")));
            this.btnIrisOpen.Location = new System.Drawing.Point(158, 81);
            this.btnIrisOpen.Name = "btnIrisOpen";
            this.btnIrisOpen.Size = new System.Drawing.Size(49, 23);
            this.btnIrisOpen.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnIrisOpen.TabIndex = 5;
            this.btnIrisOpen.Tooltip = "光圈[变亮]";
            this.btnIrisOpen.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PTZButtonMouseDown);
            this.btnIrisOpen.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PTZButtonMouseUp);
            // 
            // btnWiperOff
            // 
            this.btnWiperOff.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnWiperOff.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnWiperOff.Command = this.VideoCommand;
            this.btnWiperOff.CommandParameter = "WIPER_PWROFF";
            this.btnWiperOff.Image = ((System.Drawing.Image)(resources.GetObject("btnWiperOff.Image")));
            this.btnWiperOff.Location = new System.Drawing.Point(218, 150);
            this.btnWiperOff.Name = "btnWiperOff";
            this.btnWiperOff.Size = new System.Drawing.Size(49, 23);
            this.btnWiperOff.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnWiperOff.TabIndex = 10;
            this.btnWiperOff.Tooltip = "雨刷[关]";
            this.btnWiperOff.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PTZButtonMouseDown);
            this.btnWiperOff.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PTZButtonMouseUp);
            // 
            // btnWiperOn
            // 
            this.btnWiperOn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnWiperOn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnWiperOn.Command = this.VideoCommand;
            this.btnWiperOn.CommandParameter = "WIPER_PWRON";
            this.btnWiperOn.Image = ((System.Drawing.Image)(resources.GetObject("btnWiperOn.Image")));
            this.btnWiperOn.Location = new System.Drawing.Point(158, 149);
            this.btnWiperOn.Name = "btnWiperOn";
            this.btnWiperOn.Size = new System.Drawing.Size(49, 23);
            this.btnWiperOn.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnWiperOn.TabIndex = 9;
            this.btnWiperOn.Tooltip = "雨刷[开]";
            this.btnWiperOn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PTZButtonMouseDown);
            this.btnWiperOn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PTZButtonMouseUp);
            // 
            // btnLightOff
            // 
            this.btnLightOff.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnLightOff.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnLightOff.Command = this.VideoCommand;
            this.btnLightOff.CommandParameter = "LIGHT_PWROFF";
            this.btnLightOff.Image = ((System.Drawing.Image)(resources.GetObject("btnLightOff.Image")));
            this.btnLightOff.Location = new System.Drawing.Point(218, 115);
            this.btnLightOff.Name = "btnLightOff";
            this.btnLightOff.Size = new System.Drawing.Size(49, 23);
            this.btnLightOff.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnLightOff.TabIndex = 8;
            this.btnLightOff.Tooltip = "灯光[关闭]";
            this.btnLightOff.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PTZButtonMouseDown);
            this.btnLightOff.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PTZButtonMouseUp);
            // 
            // btnLightOn
            // 
            this.btnLightOn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnLightOn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnLightOn.Command = this.VideoCommand;
            this.btnLightOn.CommandParameter = "LIGHT_PWRON";
            this.btnLightOn.Image = ((System.Drawing.Image)(resources.GetObject("btnLightOn.Image")));
            this.btnLightOn.Location = new System.Drawing.Point(158, 115);
            this.btnLightOn.Name = "btnLightOn";
            this.btnLightOn.Size = new System.Drawing.Size(49, 23);
            this.btnLightOn.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnLightOn.TabIndex = 7;
            this.btnLightOn.Tooltip = "灯光[打开]";
            this.btnLightOn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PTZButtonMouseDown);
            this.btnLightOn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PTZButtonMouseUp);
            // 
            // sliderSpeed
            // 
            // 
            // 
            // 
            this.sliderSpeed.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.sliderSpeed.Command = this.VideoCommand;
            this.sliderSpeed.CommandParameter = "PTZSPEED";
            this.sliderSpeed.LabelVisible = false;
            this.sliderSpeed.Location = new System.Drawing.Point(12, 152);
            this.sliderSpeed.Maximum = 7;
            this.sliderSpeed.Minimum = 1;
            this.sliderSpeed.Name = "sliderSpeed";
            this.sliderSpeed.Size = new System.Drawing.Size(132, 23);
            this.sliderSpeed.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.sliderSpeed.TabIndex = 11;
            this.sliderSpeed.Value = 4;
            // 
            // panControl
            // 
            this.panControl.Location = new System.Drawing.Point(12, 13);
            this.panControl.MajorTickAmount = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.panControl.MaxValue = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.panControl.MaxZonePercentage = 1;
            this.panControl.MinorTickAmount = new decimal(new int[] {
            45,
            0,
            0,
            0});
            this.panControl.MinValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.panControl.MinZonePercentage = 1;
            this.panControl.Name = "panControl";
            this.panControl.ShowTickLabels = false;
            this.panControl.Size = new System.Drawing.Size(132, 133);
            this.panControl.StartAngle = 0;
            this.panControl.SweepAngle = 360;
            this.panControl.TabIndex = 0;
            this.panControl.Text = "panControl1";
            // 
            // PTZControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.sliderSpeed);
            this.Controls.Add(this.btnWiperOff);
            this.Controls.Add(this.btnWiperOn);
            this.Controls.Add(this.btnLightOff);
            this.Controls.Add(this.btnLightOn);
            this.Controls.Add(this.btnIrisClose);
            this.Controls.Add(this.btnIrisOpen);
            this.Controls.Add(this.btnFocus);
            this.Controls.Add(this.btnFocusNear);
            this.Controls.Add(this.btnZoomOut);
            this.Controls.Add(this.btnZoomIn);
            this.Controls.Add(this.panControl);
            this.Name = "PTZControl";
            this.Size = new System.Drawing.Size(277, 186);
            this.ResumeLayout(false);

        }

        #endregion

        private GHIBMS.PTZControl.PanControl panControl;
        private DevComponents.DotNetBar.ButtonX btnZoomIn;
        private DevComponents.DotNetBar.ButtonX btnZoomOut;
        private DevComponents.DotNetBar.ButtonX btnFocus;
        private DevComponents.DotNetBar.ButtonX btnFocusNear;
        private DevComponents.DotNetBar.ButtonX btnIrisClose;
        private DevComponents.DotNetBar.ButtonX btnIrisOpen;
        private DevComponents.DotNetBar.ButtonX btnWiperOff;
        private DevComponents.DotNetBar.ButtonX btnWiperOn;
        private DevComponents.DotNetBar.ButtonX btnLightOff;
        private DevComponents.DotNetBar.ButtonX btnLightOn;
        private DevComponents.DotNetBar.Controls.Slider sliderSpeed;
        private DevComponents.DotNetBar.Command VideoCommand;
    }
}
