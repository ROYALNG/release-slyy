namespace GHIBMS.NetVideo
{
    partial class NetVideoControl
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

            //-------------------清理DVR资源--------------------//

            //if (RealPlayer != null)
            //{
            //    RealPlayer.Dispose();
            //}


            //--------------------------------------------------//

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NetVideoControl));
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.lblCamTitle = new DevComponents.DotNetBar.LabelItem();
            this.btnOpen = new DevComponents.DotNetBar.ButtonItem();
            this.PlayerCommand = new DevComponents.DotNetBar.Command(this.components);
            this.btnPlay = new DevComponents.DotNetBar.ButtonItem();
            this.btnStop = new DevComponents.DotNetBar.ButtonItem();
            this.btnStartRecord = new DevComponents.DotNetBar.ButtonItem();
            this.btnStopRecord = new DevComponents.DotNetBar.ButtonItem();
            this.btnCapture = new DevComponents.DotNetBar.ButtonItem();
            this.btnSound = new DevComponents.DotNetBar.ButtonItem();
            this.btnOpenSound = new DevComponents.DotNetBar.ButtonItem();
            this.btnMuteSound = new DevComponents.DotNetBar.ButtonItem();
            this.sldVoice = new DevComponents.DotNetBar.SliderItem();
            this.btnPhone = new DevComponents.DotNetBar.ButtonItem();
            this.btnStartPhone = new DevComponents.DotNetBar.ButtonItem();
            this.btnStopPhone = new DevComponents.DotNetBar.ButtonItem();
            this.slildPhoneVoice = new DevComponents.DotNetBar.SliderItem();
            this.btnPTZ = new DevComponents.DotNetBar.ButtonItem();
            this.btnConfig = new DevComponents.DotNetBar.ButtonItem();
            this.btnFullScreen = new DevComponents.DotNetBar.ButtonItem();
            this.panelPlay = new DevComponents.DotNetBar.PanelEx();
            this.picRecordShow = new System.Windows.Forms.PictureBox();
            this.sliderSpeed = new DevComponents.DotNetBar.Controls.Slider();
            this.exPanelConfig = new DevComponents.DotNetBar.ExpandablePanel();
            this.btnConfigCancel = new DevComponents.DotNetBar.ButtonX();
            this.btnSaveConfig = new DevComponents.DotNetBar.ButtonX();
            this.sliderHue = new DevComponents.DotNetBar.Controls.Slider();
            this.sliderSaturation = new DevComponents.DotNetBar.Controls.Slider();
            this.sliderContrast = new DevComponents.DotNetBar.Controls.Slider();
            this.sliderBright = new DevComponents.DotNetBar.Controls.Slider();
            this.btnZOOMOUT = new DevComponents.DotNetBar.ButtonX();
            this.btnZOOMIN = new DevComponents.DotNetBar.ButtonX();
            this.btnPTZRIGHT = new DevComponents.DotNetBar.ButtonX();
            this.btnPTZUP = new DevComponents.DotNetBar.ButtonX();
            this.btnPTZLEFT = new DevComponents.DotNetBar.ButtonX();
            this.btnPTZDOWN = new DevComponents.DotNetBar.ButtonX();
            this.exPanelVideoSource = new DevComponents.DotNetBar.ExpandablePanel();
            this.ipInputVod = new DevComponents.Editors.IpAddressInput();
            this.ipInputDvr = new DevComponents.Editors.IpAddressInput();
            this.txtDvrCH = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.btnConnect = new DevComponents.DotNetBar.ButtonX();
            this.txtVodPort = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.chkCodeType = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.txtPassword = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.txtUserName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.txtDvrPort = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.cmbProtocol = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.panelPlay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRecordShow)).BeginInit();
            this.exPanelConfig.SuspendLayout();
            this.exPanelVideoSource.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ipInputVod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ipInputDvr)).BeginInit();
            this.SuspendLayout();
            // 
            // bar1
            // 
            this.bar1.AntiAlias = true;
            this.bar1.AutoHide = true;
            this.bar1.AutoHideTabTextAlwaysVisible = true;
            this.bar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bar1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblCamTitle,
            this.btnOpen,
            this.btnPlay,
            this.btnStop,
            this.btnStartRecord,
            this.btnStopRecord,
            this.btnCapture,
            this.btnSound,
            this.btnPhone,
            this.btnPTZ,
            this.btnConfig,
            this.btnFullScreen});
            this.bar1.Location = new System.Drawing.Point(0, 685);
            this.bar1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(852, 33);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar1.TabIndex = 0;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            this.bar1.Visible = false;
            this.bar1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelPlay_MouseDown);
            // 
            // lblCamTitle
            // 
            this.lblCamTitle.Image = ((System.Drawing.Image)(resources.GetObject("lblCamTitle.Image")));
            this.lblCamTitle.Name = "lblCamTitle";
            this.lblCamTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelPlay_MouseDown);
            // 
            // btnOpen
            // 
            this.btnOpen.Command = this.PlayerCommand;
            this.btnOpen.CommandParameter = "OPENVIDEO";
            this.btnOpen.Image = ((System.Drawing.Image)(resources.GetObject("btnOpen.Image")));
            this.btnOpen.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Text = "buttonItem1";
            this.btnOpen.Tooltip = "打开";
            // 
            // PlayerCommand
            // 
            this.PlayerCommand.Name = "PlayerCommand";
            this.PlayerCommand.Executed += new System.EventHandler(this.PlayerCommand_Executed);
            // 
            // btnPlay
            // 
            this.btnPlay.Command = this.PlayerCommand;
            this.btnPlay.CommandParameter = "PLAY";
            this.btnPlay.Enabled = false;
            this.btnPlay.Image = ((System.Drawing.Image)(resources.GetObject("btnPlay.Image")));
            this.btnPlay.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Tooltip = "播放";
            // 
            // btnStop
            // 
            this.btnStop.Command = this.PlayerCommand;
            this.btnStop.CommandParameter = "STOP";
            this.btnStop.Enabled = false;
            this.btnStop.Image = ((System.Drawing.Image)(resources.GetObject("btnStop.Image")));
            this.btnStop.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.btnStop.Name = "btnStop";
            this.btnStop.Tooltip = "停止";
            // 
            // btnStartRecord
            // 
            this.btnStartRecord.Command = this.PlayerCommand;
            this.btnStartRecord.CommandParameter = "STARTREC";
            this.btnStartRecord.Enabled = false;
            this.btnStartRecord.Image = ((System.Drawing.Image)(resources.GetObject("btnStartRecord.Image")));
            this.btnStartRecord.Name = "btnStartRecord";
            this.btnStartRecord.Tooltip = "录像";
            // 
            // btnStopRecord
            // 
            this.btnStopRecord.Command = this.PlayerCommand;
            this.btnStopRecord.CommandParameter = "STOPREC";
            this.btnStopRecord.Enabled = false;
            this.btnStopRecord.Image = ((System.Drawing.Image)(resources.GetObject("btnStopRecord.Image")));
            this.btnStopRecord.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.btnStopRecord.Name = "btnStopRecord";
            this.btnStopRecord.Tooltip = "停止录像";
            // 
            // btnCapture
            // 
            this.btnCapture.Command = this.PlayerCommand;
            this.btnCapture.CommandParameter = "PICTURE";
            this.btnCapture.Enabled = false;
            this.btnCapture.Image = ((System.Drawing.Image)(resources.GetObject("btnCapture.Image")));
            this.btnCapture.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Tooltip = "抓拍";
            // 
            // btnSound
            // 
            this.btnSound.Image = ((System.Drawing.Image)(resources.GetObject("btnSound.Image")));
            this.btnSound.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.btnSound.Name = "btnSound";
            this.btnSound.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnOpenSound,
            this.btnMuteSound,
            this.sldVoice});
            this.btnSound.Tooltip = "声音";
            // 
            // btnOpenSound
            // 
            this.btnOpenSound.Command = this.PlayerCommand;
            this.btnOpenSound.CommandParameter = "OPENSOUND";
            this.btnOpenSound.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenSound.Image")));
            this.btnOpenSound.Name = "btnOpenSound";
            this.btnOpenSound.Text = "打开声音";
            // 
            // btnMuteSound
            // 
            this.btnMuteSound.Command = this.PlayerCommand;
            this.btnMuteSound.CommandParameter = "CLOSESOUND";
            this.btnMuteSound.Image = ((System.Drawing.Image)(resources.GetObject("btnMuteSound.Image")));
            this.btnMuteSound.Name = "btnMuteSound";
            this.btnMuteSound.Text = "关闭声音";
            // 
            // sldVoice
            // 
            this.sldVoice.Command = this.PlayerCommand;
            this.sldVoice.CommandParameter = "VOICE";
            this.sldVoice.Maximum = 65535;
            this.sldVoice.Name = "sldVoice";
            this.sldVoice.Text = "音量";
            this.sldVoice.Value = 0;
            // 
            // btnPhone
            // 
            this.btnPhone.Image = ((System.Drawing.Image)(resources.GetObject("btnPhone.Image")));
            this.btnPhone.Name = "btnPhone";
            this.btnPhone.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnStartPhone,
            this.btnStopPhone,
            this.slildPhoneVoice});
            this.btnPhone.Tooltip = "对讲";
            // 
            // btnStartPhone
            // 
            this.btnStartPhone.Command = this.PlayerCommand;
            this.btnStartPhone.CommandParameter = "OPENPHONE";
            this.btnStartPhone.Image = ((System.Drawing.Image)(resources.GetObject("btnStartPhone.Image")));
            this.btnStartPhone.Name = "btnStartPhone";
            this.btnStartPhone.Text = "打开对讲";
            // 
            // btnStopPhone
            // 
            this.btnStopPhone.Command = this.PlayerCommand;
            this.btnStopPhone.CommandParameter = "CLOSEPHONE";
            this.btnStopPhone.Image = ((System.Drawing.Image)(resources.GetObject("btnStopPhone.Image")));
            this.btnStopPhone.Name = "btnStopPhone";
            this.btnStopPhone.Text = "关闭对讲";
            // 
            // slildPhoneVoice
            // 
            this.slildPhoneVoice.Command = this.PlayerCommand;
            this.slildPhoneVoice.CommandParameter = "PHONE_VOICE";
            this.slildPhoneVoice.Maximum = 65535;
            this.slildPhoneVoice.Name = "slildPhoneVoice";
            this.slildPhoneVoice.Text = "音量";
            this.slildPhoneVoice.Value = 0;
            // 
            // btnPTZ
            // 
            this.btnPTZ.Command = this.PlayerCommand;
            this.btnPTZ.CommandParameter = "PTZ";
            this.btnPTZ.Enabled = false;
            this.btnPTZ.Image = ((System.Drawing.Image)(resources.GetObject("btnPTZ.Image")));
            this.btnPTZ.Name = "btnPTZ";
            this.btnPTZ.Tooltip = "云台";
            // 
            // btnConfig
            // 
            this.btnConfig.Command = this.PlayerCommand;
            this.btnConfig.CommandParameter = "CONFIG";
            this.btnConfig.Image = ((System.Drawing.Image)(resources.GetObject("btnConfig.Image")));
            this.btnConfig.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Tooltip = "设置";
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // btnFullScreen
            // 
            this.btnFullScreen.Command = this.PlayerCommand;
            this.btnFullScreen.CommandParameter = "FULLSCREEN";
            this.btnFullScreen.Image = ((System.Drawing.Image)(resources.GetObject("btnFullScreen.Image")));
            this.btnFullScreen.Name = "btnFullScreen";
            this.btnFullScreen.Text = "buttonItem1";
            this.btnFullScreen.Tooltip = "全屏";
            // 
            // panelPlay
            // 
            this.panelPlay.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelPlay.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelPlay.Controls.Add(this.picRecordShow);
            this.panelPlay.Controls.Add(this.sliderSpeed);
            this.panelPlay.Controls.Add(this.exPanelConfig);
            this.panelPlay.Controls.Add(this.btnZOOMOUT);
            this.panelPlay.Controls.Add(this.btnZOOMIN);
            this.panelPlay.Controls.Add(this.btnPTZRIGHT);
            this.panelPlay.Controls.Add(this.btnPTZUP);
            this.panelPlay.Controls.Add(this.btnPTZLEFT);
            this.panelPlay.Controls.Add(this.btnPTZDOWN);
            this.panelPlay.Controls.Add(this.exPanelVideoSource);
            this.panelPlay.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelPlay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPlay.Location = new System.Drawing.Point(0, 0);
            this.panelPlay.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelPlay.Name = "panelPlay";
            this.panelPlay.Size = new System.Drawing.Size(852, 685);
            this.panelPlay.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelPlay.Style.BackColor1.Color = System.Drawing.Color.Navy;
            this.panelPlay.Style.BackColor2.Color = System.Drawing.Color.Navy;
            this.panelPlay.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelPlay.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelPlay.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelPlay.Style.GradientAngle = 90;
            this.panelPlay.TabIndex = 1;
            this.panelPlay.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelPlay_MouseDown);
            // 
            // picRecordShow
            // 
            this.picRecordShow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picRecordShow.Image = ((System.Drawing.Image)(resources.GetObject("picRecordShow.Image")));
            this.picRecordShow.Location = new System.Drawing.Point(774, 42);
            this.picRecordShow.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.picRecordShow.Name = "picRecordShow";
            this.picRecordShow.Size = new System.Drawing.Size(16, 16);
            this.picRecordShow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picRecordShow.TabIndex = 9;
            this.picRecordShow.TabStop = false;
            this.picRecordShow.Visible = false;
            // 
            // sliderSpeed
            // 
            this.sliderSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.sliderSpeed.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.sliderSpeed.CausesValidation = false;
            this.sliderSpeed.LabelVisible = false;
            this.sliderSpeed.Location = new System.Drawing.Point(712, 517);
            this.sliderSpeed.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.sliderSpeed.Maximum = 7;
            this.sliderSpeed.Minimum = 1;
            this.sliderSpeed.Name = "sliderSpeed";
            this.sliderSpeed.Size = new System.Drawing.Size(50, 160);
            this.sliderSpeed.SliderOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            this.sliderSpeed.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.sliderSpeed.TabIndex = 8;
            this.sliderSpeed.Text = "slider5";
            this.sliderSpeed.Value = 4;
            this.sliderSpeed.Visible = false;
            // 
            // exPanelConfig
            // 
            this.exPanelConfig.CanvasColor = System.Drawing.SystemColors.Control;
            this.exPanelConfig.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.exPanelConfig.Controls.Add(this.btnConfigCancel);
            this.exPanelConfig.Controls.Add(this.btnSaveConfig);
            this.exPanelConfig.Controls.Add(this.sliderHue);
            this.exPanelConfig.Controls.Add(this.sliderSaturation);
            this.exPanelConfig.Controls.Add(this.sliderContrast);
            this.exPanelConfig.Controls.Add(this.sliderBright);
            this.exPanelConfig.DisabledBackColor = System.Drawing.Color.Empty;
            this.exPanelConfig.ExpandButtonVisible = false;
            this.exPanelConfig.Location = new System.Drawing.Point(190, 40);
            this.exPanelConfig.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.exPanelConfig.Name = "exPanelConfig";
            this.exPanelConfig.Size = new System.Drawing.Size(508, 404);
            this.exPanelConfig.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.exPanelConfig.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.exPanelConfig.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.exPanelConfig.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.exPanelConfig.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.exPanelConfig.Style.GradientAngle = 90;
            this.exPanelConfig.TabIndex = 7;
            this.exPanelConfig.TitleHeight = 52;
            this.exPanelConfig.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
            this.exPanelConfig.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.exPanelConfig.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.exPanelConfig.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.exPanelConfig.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.exPanelConfig.TitleStyle.GradientAngle = 90;
            this.exPanelConfig.TitleText = "视频参数设置";
            this.exPanelConfig.Visible = false;
            // 
            // btnConfigCancel
            // 
            this.btnConfigCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnConfigCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnConfigCancel.Command = this.PlayerCommand;
            this.btnConfigCancel.CommandParameter = "CONFIG_CANCEL";
            this.btnConfigCancel.Location = new System.Drawing.Point(350, 338);
            this.btnConfigCancel.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnConfigCancel.Name = "btnConfigCancel";
            this.btnConfigCancel.Size = new System.Drawing.Size(106, 46);
            this.btnConfigCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnConfigCancel.TabIndex = 10;
            this.btnConfigCancel.Text = "取消";
            this.btnConfigCancel.Click += new System.EventHandler(this.btnConfigCancel_Click);
            // 
            // btnSaveConfig
            // 
            this.btnSaveConfig.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSaveConfig.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSaveConfig.Command = this.PlayerCommand;
            this.btnSaveConfig.CommandParameter = "SET_VIDEO_EFFECT";
            this.btnSaveConfig.Location = new System.Drawing.Point(214, 338);
            this.btnSaveConfig.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnSaveConfig.Name = "btnSaveConfig";
            this.btnSaveConfig.Size = new System.Drawing.Size(108, 46);
            this.btnSaveConfig.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSaveConfig.TabIndex = 9;
            this.btnSaveConfig.Text = "设置";
            this.btnSaveConfig.Click += new System.EventHandler(this.btnSaveConfig_Click);
            // 
            // sliderHue
            // 
            this.sliderHue.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.sliderHue.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.sliderHue.Location = new System.Drawing.Point(80, 260);
            this.sliderHue.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.sliderHue.Maximum = 10;
            this.sliderHue.Name = "sliderHue";
            this.sliderHue.Size = new System.Drawing.Size(352, 46);
            this.sliderHue.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.sliderHue.TabIndex = 8;
            this.sliderHue.Text = "色度";
            this.sliderHue.Value = 1;
            // 
            // sliderSaturation
            // 
            this.sliderSaturation.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.sliderSaturation.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.sliderSaturation.Location = new System.Drawing.Point(80, 202);
            this.sliderSaturation.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.sliderSaturation.Maximum = 10;
            this.sliderSaturation.Name = "sliderSaturation";
            this.sliderSaturation.Size = new System.Drawing.Size(352, 46);
            this.sliderSaturation.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.sliderSaturation.TabIndex = 7;
            this.sliderSaturation.Text = "饱和";
            this.sliderSaturation.Value = 1;
            // 
            // sliderContrast
            // 
            this.sliderContrast.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.sliderContrast.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.sliderContrast.Location = new System.Drawing.Point(80, 144);
            this.sliderContrast.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.sliderContrast.Maximum = 10;
            this.sliderContrast.Name = "sliderContrast";
            this.sliderContrast.Size = new System.Drawing.Size(352, 46);
            this.sliderContrast.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.sliderContrast.TabIndex = 6;
            this.sliderContrast.Text = "对比";
            this.sliderContrast.Value = 1;
            // 
            // sliderBright
            // 
            this.sliderBright.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.sliderBright.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.sliderBright.Location = new System.Drawing.Point(80, 86);
            this.sliderBright.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.sliderBright.Maximum = 10;
            this.sliderBright.Name = "sliderBright";
            this.sliderBright.Size = new System.Drawing.Size(352, 46);
            this.sliderBright.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.sliderBright.TabIndex = 5;
            this.sliderBright.Text = "亮度";
            this.sliderBright.Value = 1;
            // 
            // btnZOOMOUT
            // 
            this.btnZOOMOUT.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnZOOMOUT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnZOOMOUT.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnZOOMOUT.CommandParameter = "ZOOM_OUT";
            this.btnZOOMOUT.Image = ((System.Drawing.Image)(resources.GetObject("btnZOOMOUT.Image")));
            this.btnZOOMOUT.Location = new System.Drawing.Point(774, 619);
            this.btnZOOMOUT.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnZOOMOUT.Name = "btnZOOMOUT";
            this.btnZOOMOUT.Size = new System.Drawing.Size(50, 50);
            this.btnZOOMOUT.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnZOOMOUT.TabIndex = 6;
            this.btnZOOMOUT.Visible = false;
            this.btnZOOMOUT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PTZButtonMouseDown);
            this.btnZOOMOUT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PTZButtonMouseUp);
            // 
            // btnZOOMIN
            // 
            this.btnZOOMIN.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnZOOMIN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnZOOMIN.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnZOOMIN.CommandParameter = "ZOOM_IN";
            this.btnZOOMIN.Image = ((System.Drawing.Image)(resources.GetObject("btnZOOMIN.Image")));
            this.btnZOOMIN.Location = new System.Drawing.Point(774, 517);
            this.btnZOOMIN.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnZOOMIN.Name = "btnZOOMIN";
            this.btnZOOMIN.Size = new System.Drawing.Size(50, 50);
            this.btnZOOMIN.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnZOOMIN.TabIndex = 5;
            this.btnZOOMIN.Visible = false;
            this.btnZOOMIN.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PTZButtonMouseDown);
            this.btnZOOMIN.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PTZButtonMouseUp);
            // 
            // btnPTZRIGHT
            // 
            this.btnPTZRIGHT.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPTZRIGHT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPTZRIGHT.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnPTZRIGHT.CommandParameter = "PAN_RIGHT";
            this.btnPTZRIGHT.Image = ((System.Drawing.Image)(resources.GetObject("btnPTZRIGHT.Image")));
            this.btnPTZRIGHT.Location = new System.Drawing.Point(654, 569);
            this.btnPTZRIGHT.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnPTZRIGHT.Name = "btnPTZRIGHT";
            this.btnPTZRIGHT.Size = new System.Drawing.Size(50, 50);
            this.btnPTZRIGHT.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPTZRIGHT.TabIndex = 4;
            this.btnPTZRIGHT.Visible = false;
            this.btnPTZRIGHT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PTZButtonMouseDown);
            this.btnPTZRIGHT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PTZButtonMouseUp);
            // 
            // btnPTZUP
            // 
            this.btnPTZUP.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPTZUP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPTZUP.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnPTZUP.CommandParameter = "TILT_UP";
            this.btnPTZUP.Image = ((System.Drawing.Image)(resources.GetObject("btnPTZUP.Image")));
            this.btnPTZUP.Location = new System.Drawing.Point(590, 517);
            this.btnPTZUP.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnPTZUP.Name = "btnPTZUP";
            this.btnPTZUP.Size = new System.Drawing.Size(50, 50);
            this.btnPTZUP.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPTZUP.TabIndex = 3;
            this.btnPTZUP.Visible = false;
            this.btnPTZUP.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PTZButtonMouseDown);
            this.btnPTZUP.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PTZButtonMouseUp);
            // 
            // btnPTZLEFT
            // 
            this.btnPTZLEFT.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPTZLEFT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPTZLEFT.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnPTZLEFT.CommandParameter = "PAN_LEFT";
            this.btnPTZLEFT.Image = ((System.Drawing.Image)(resources.GetObject("btnPTZLEFT.Image")));
            this.btnPTZLEFT.Location = new System.Drawing.Point(528, 569);
            this.btnPTZLEFT.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnPTZLEFT.Name = "btnPTZLEFT";
            this.btnPTZLEFT.Size = new System.Drawing.Size(50, 50);
            this.btnPTZLEFT.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPTZLEFT.TabIndex = 2;
            this.btnPTZLEFT.Visible = false;
            this.btnPTZLEFT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PTZButtonMouseDown);
            this.btnPTZLEFT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PTZButtonMouseUp);
            // 
            // btnPTZDOWN
            // 
            this.btnPTZDOWN.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPTZDOWN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPTZDOWN.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnPTZDOWN.CommandParameter = "TILT_DOWN";
            this.btnPTZDOWN.Image = ((System.Drawing.Image)(resources.GetObject("btnPTZDOWN.Image")));
            this.btnPTZDOWN.Location = new System.Drawing.Point(588, 619);
            this.btnPTZDOWN.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnPTZDOWN.Name = "btnPTZDOWN";
            this.btnPTZDOWN.Size = new System.Drawing.Size(50, 50);
            this.btnPTZDOWN.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPTZDOWN.TabIndex = 1;
            this.btnPTZDOWN.Visible = false;
            this.btnPTZDOWN.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PTZButtonMouseDown);
            this.btnPTZDOWN.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PTZButtonMouseUp);
            // 
            // exPanelVideoSource
            // 
            this.exPanelVideoSource.CanvasColor = System.Drawing.SystemColors.Control;
            this.exPanelVideoSource.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.exPanelVideoSource.Controls.Add(this.ipInputVod);
            this.exPanelVideoSource.Controls.Add(this.ipInputDvr);
            this.exPanelVideoSource.Controls.Add(this.txtDvrCH);
            this.exPanelVideoSource.Controls.Add(this.labelX8);
            this.exPanelVideoSource.Controls.Add(this.btnCancel);
            this.exPanelVideoSource.Controls.Add(this.btnConnect);
            this.exPanelVideoSource.Controls.Add(this.txtVodPort);
            this.exPanelVideoSource.Controls.Add(this.labelX7);
            this.exPanelVideoSource.Controls.Add(this.chkCodeType);
            this.exPanelVideoSource.Controls.Add(this.txtPassword);
            this.exPanelVideoSource.Controls.Add(this.labelX5);
            this.exPanelVideoSource.Controls.Add(this.txtUserName);
            this.exPanelVideoSource.Controls.Add(this.labelX4);
            this.exPanelVideoSource.Controls.Add(this.txtDvrPort);
            this.exPanelVideoSource.Controls.Add(this.labelX3);
            this.exPanelVideoSource.Controls.Add(this.labelX2);
            this.exPanelVideoSource.Controls.Add(this.cmbProtocol);
            this.exPanelVideoSource.Controls.Add(this.labelX1);
            this.exPanelVideoSource.DisabledBackColor = System.Drawing.Color.Empty;
            this.exPanelVideoSource.ExpandButtonVisible = false;
            this.exPanelVideoSource.Location = new System.Drawing.Point(120, 42);
            this.exPanelVideoSource.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.exPanelVideoSource.Name = "exPanelVideoSource";
            this.exPanelVideoSource.Size = new System.Drawing.Size(638, 494);
            this.exPanelVideoSource.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.exPanelVideoSource.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.exPanelVideoSource.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.exPanelVideoSource.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.exPanelVideoSource.Style.GradientAngle = 90;
            this.exPanelVideoSource.TabIndex = 0;
            this.exPanelVideoSource.TitleHeight = 52;
            this.exPanelVideoSource.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
            this.exPanelVideoSource.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.exPanelVideoSource.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.exPanelVideoSource.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.exPanelVideoSource.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.exPanelVideoSource.TitleStyle.GradientAngle = 90;
            this.exPanelVideoSource.TitleText = "连接视频源";
            this.exPanelVideoSource.Visible = false;
            // 
            // ipInputVod
            // 
            this.ipInputVod.AutoOverwrite = true;
            // 
            // 
            // 
            this.ipInputVod.BackgroundStyle.Class = "DateTimeInputBackground";
            this.ipInputVod.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ipInputVod.ButtonClear.Tooltip = "";
            this.ipInputVod.ButtonCustom.Tooltip = "";
            this.ipInputVod.ButtonCustom2.Tooltip = "";
            this.ipInputVod.ButtonDropDown.Tooltip = "";
            this.ipInputVod.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.ipInputVod.ButtonFreeText.Tooltip = "";
            this.ipInputVod.ButtonFreeText.Visible = true;
            this.ipInputVod.Location = new System.Drawing.Point(146, 358);
            this.ipInputVod.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.ipInputVod.Name = "ipInputVod";
            this.ipInputVod.Size = new System.Drawing.Size(240, 35);
            this.ipInputVod.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ipInputVod.TabIndex = 23;
            // 
            // ipInputDvr
            // 
            this.ipInputDvr.AutoOverwrite = true;
            // 
            // 
            // 
            this.ipInputDvr.BackgroundStyle.Class = "DateTimeInputBackground";
            this.ipInputDvr.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ipInputDvr.ButtonClear.Tooltip = "";
            this.ipInputDvr.ButtonCustom.Tooltip = "";
            this.ipInputDvr.ButtonCustom2.Tooltip = "";
            this.ipInputDvr.ButtonDropDown.Tooltip = "";
            this.ipInputDvr.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.ipInputDvr.ButtonFreeText.Tooltip = "";
            this.ipInputDvr.ButtonFreeText.Visible = true;
            this.ipInputDvr.Location = new System.Drawing.Point(146, 158);
            this.ipInputDvr.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.ipInputDvr.Name = "ipInputDvr";
            this.ipInputDvr.Size = new System.Drawing.Size(240, 35);
            this.ipInputDvr.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ipInputDvr.TabIndex = 22;
            // 
            // txtDvrCH
            // 
            this.txtDvrCH.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtDvrCH.Border.Class = "TextBoxBorder";
            this.txtDvrCH.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtDvrCH.ButtonCustom.Tooltip = "";
            this.txtDvrCH.ButtonCustom2.Tooltip = "";
            this.txtDvrCH.DisabledBackColor = System.Drawing.Color.White;
            this.txtDvrCH.ForeColor = System.Drawing.Color.Black;
            this.txtDvrCH.Location = new System.Drawing.Point(146, 294);
            this.txtDvrCH.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtDvrCH.Name = "txtDvrCH";
            this.txtDvrCH.Size = new System.Drawing.Size(108, 35);
            this.txtDvrCH.TabIndex = 19;
            this.txtDvrCH.Text = "0";
            // 
            // labelX8
            // 
            this.labelX8.AutoSize = true;
            this.labelX8.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX8.Location = new System.Drawing.Point(22, 300);
            this.labelX8.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(109, 33);
            this.labelX8.TabIndex = 18;
            this.labelX8.Text = "视频通道";
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.Location = new System.Drawing.Point(472, 422);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(110, 46);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnConnect.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnConnect.Location = new System.Drawing.Point(322, 422);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(110, 46);
            this.btnConnect.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnConnect.TabIndex = 16;
            this.btnConnect.Text = "连接";
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtVodPort
            // 
            this.txtVodPort.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtVodPort.Border.Class = "TextBoxBorder";
            this.txtVodPort.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtVodPort.ButtonCustom.Tooltip = "";
            this.txtVodPort.ButtonCustom2.Tooltip = "";
            this.txtVodPort.DisabledBackColor = System.Drawing.Color.White;
            this.txtVodPort.ForeColor = System.Drawing.Color.Black;
            this.txtVodPort.Location = new System.Drawing.Point(498, 360);
            this.txtVodPort.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtVodPort.Name = "txtVodPort";
            this.txtVodPort.Size = new System.Drawing.Size(108, 35);
            this.txtVodPort.TabIndex = 15;
            this.txtVodPort.Text = "0";
            // 
            // labelX7
            // 
            this.labelX7.AutoSize = true;
            this.labelX7.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Location = new System.Drawing.Point(22, 366);
            this.labelX7.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(109, 33);
            this.labelX7.TabIndex = 12;
            this.labelX7.Text = "流媒体IP";
            // 
            // chkCodeType
            // 
            this.chkCodeType.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.chkCodeType.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkCodeType.Location = new System.Drawing.Point(418, 86);
            this.chkCodeType.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.chkCodeType.Name = "chkCodeType";
            this.chkCodeType.Size = new System.Drawing.Size(200, 46);
            this.chkCodeType.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkCodeType.TabIndex = 11;
            this.chkCodeType.Text = "主码流";
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtPassword.Border.Class = "TextBoxBorder";
            this.txtPassword.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPassword.ButtonCustom.Tooltip = "";
            this.txtPassword.ButtonCustom2.Tooltip = "";
            this.txtPassword.DisabledBackColor = System.Drawing.Color.White;
            this.txtPassword.ForeColor = System.Drawing.Color.Black;
            this.txtPassword.Location = new System.Drawing.Point(498, 226);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtPassword.MaxLength = 6;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(108, 35);
            this.txtPassword.TabIndex = 10;
            // 
            // labelX5
            // 
            this.labelX5.AutoSize = true;
            this.labelX5.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(418, 232);
            this.labelX5.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(85, 33);
            this.labelX5.TabIndex = 9;
            this.labelX5.Text = "密  码";
            // 
            // txtUserName
            // 
            this.txtUserName.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtUserName.Border.Class = "TextBoxBorder";
            this.txtUserName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtUserName.ButtonCustom.Tooltip = "";
            this.txtUserName.ButtonCustom2.Tooltip = "";
            this.txtUserName.DisabledBackColor = System.Drawing.Color.White;
            this.txtUserName.ForeColor = System.Drawing.Color.Black;
            this.txtUserName.Location = new System.Drawing.Point(146, 228);
            this.txtUserName.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(240, 35);
            this.txtUserName.TabIndex = 8;
            // 
            // labelX4
            // 
            this.labelX4.AutoSize = true;
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(22, 234);
            this.labelX4.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(109, 33);
            this.labelX4.TabIndex = 7;
            this.labelX4.Text = "用 户 名";
            // 
            // txtDvrPort
            // 
            this.txtDvrPort.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtDvrPort.Border.Class = "TextBoxBorder";
            this.txtDvrPort.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtDvrPort.ButtonCustom.Tooltip = "";
            this.txtDvrPort.ButtonCustom2.Tooltip = "";
            this.txtDvrPort.DisabledBackColor = System.Drawing.Color.White;
            this.txtDvrPort.ForeColor = System.Drawing.Color.Black;
            this.txtDvrPort.Location = new System.Drawing.Point(498, 154);
            this.txtDvrPort.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtDvrPort.Name = "txtDvrPort";
            this.txtDvrPort.Size = new System.Drawing.Size(108, 35);
            this.txtDvrPort.TabIndex = 6;
            this.txtDvrPort.Text = "0";
            // 
            // labelX3
            // 
            this.labelX3.AutoSize = true;
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(418, 164);
            this.labelX3.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(85, 33);
            this.labelX3.TabIndex = 5;
            this.labelX3.Text = "端口号";
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(22, 166);
            this.labelX2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(109, 33);
            this.labelX2.TabIndex = 3;
            this.labelX2.Text = "编码器IP";
            // 
            // cmbProtocol
            // 
            this.cmbProtocol.DisplayMember = "Text";
            this.cmbProtocol.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbProtocol.FormattingEnabled = true;
            this.cmbProtocol.ItemHeight = 15;
            this.cmbProtocol.Location = new System.Drawing.Point(146, 92);
            this.cmbProtocol.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cmbProtocol.Name = "cmbProtocol";
            this.cmbProtocol.Size = new System.Drawing.Size(236, 21);
            this.cmbProtocol.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbProtocol.TabIndex = 2;
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(22, 98);
            this.labelX1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(109, 33);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "通讯协议";
            // 
            // NetVideoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.panelPlay);
            this.Controls.Add(this.bar1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "NetVideoControl";
            this.Size = new System.Drawing.Size(852, 718);
            this.Load += new System.EventHandler(this.NetVideoControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.panelPlay.ResumeLayout(false);
            this.panelPlay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRecordShow)).EndInit();
            this.exPanelConfig.ResumeLayout(false);
            this.exPanelVideoSource.ResumeLayout(false);
            this.exPanelVideoSource.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ipInputVod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ipInputDvr)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.LabelItem lblCamTitle;
        private DevComponents.DotNetBar.ButtonItem btnPlay;
        private DevComponents.DotNetBar.ButtonItem btnStop;
        private DevComponents.DotNetBar.ButtonItem btnStopRecord;
        private DevComponents.DotNetBar.ButtonItem btnCapture;
        private DevComponents.DotNetBar.ButtonItem btnSound;
        private DevComponents.DotNetBar.ButtonItem btnConfig;
        private DevComponents.DotNetBar.SliderItem sldVoice;
        private DevComponents.DotNetBar.ButtonItem btnOpenSound;
        private DevComponents.DotNetBar.ButtonItem btnMuteSound;
        private DevComponents.DotNetBar.ButtonItem btnStartPhone;
        private DevComponents.DotNetBar.ButtonItem btnStopPhone;
        private DevComponents.DotNetBar.ButtonItem btnPTZ;
        private DevComponents.DotNetBar.Command PlayerCommand;
        private DevComponents.DotNetBar.ButtonItem btnStartRecord;
        private DevComponents.DotNetBar.ButtonItem btnOpen;
        private DevComponents.DotNetBar.ButtonItem btnFullScreen;
        private DevComponents.DotNetBar.ButtonItem btnPhone;
        private DevComponents.DotNetBar.SliderItem slildPhoneVoice;
        private DevComponents.DotNetBar.PanelEx panelPlay;
        private System.Windows.Forms.PictureBox picRecordShow;
        private DevComponents.DotNetBar.Controls.Slider sliderSpeed;
        private DevComponents.DotNetBar.ExpandablePanel exPanelConfig;
        private DevComponents.DotNetBar.ButtonX btnConfigCancel;
        private DevComponents.DotNetBar.ButtonX btnSaveConfig;
        private DevComponents.DotNetBar.Controls.Slider sliderHue;
        private DevComponents.DotNetBar.Controls.Slider sliderSaturation;
        private DevComponents.DotNetBar.Controls.Slider sliderContrast;
        private DevComponents.DotNetBar.Controls.Slider sliderBright;
        private DevComponents.DotNetBar.ButtonX btnZOOMOUT;
        private DevComponents.DotNetBar.ButtonX btnZOOMIN;
        private DevComponents.DotNetBar.ButtonX btnPTZRIGHT;
        private DevComponents.DotNetBar.ButtonX btnPTZUP;
        private DevComponents.DotNetBar.ButtonX btnPTZLEFT;
        private DevComponents.DotNetBar.ButtonX btnPTZDOWN;
        private DevComponents.DotNetBar.ExpandablePanel exPanelVideoSource;
        private DevComponents.DotNetBar.Controls.TextBoxX txtDvrCH;
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.ButtonX btnConnect;
        private DevComponents.DotNetBar.Controls.TextBoxX txtVodPort;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkCodeType;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPassword;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.Controls.TextBoxX txtUserName;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.TextBoxX txtDvrPort;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbProtocol;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.Editors.IpAddressInput ipInputDvr;
        private DevComponents.Editors.IpAddressInput ipInputVod;
    }
}
