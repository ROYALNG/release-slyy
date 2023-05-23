namespace GHIBMS.VideoPlayback
{
    partial class VideoPlaybackControl 
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VideoPlaybackControl));
            this.panelBack = new DevComponents.DotNetBar.PanelEx();
            this.panelPlayer = new DevComponents.DotNetBar.PanelEx();
            this.exPanelVideoSource = new DevComponents.DotNetBar.ExpandablePanel();
            this.ipInputVod = new DevComponents.Editors.IpAddressInput();
            this.ipInputDvr = new DevComponents.Editors.IpAddressInput();
            this.txtDvrCH = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.btnConnect = new DevComponents.DotNetBar.ButtonX();
            this.txtVodPort = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.txtPassword = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.txtUserName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.txtDvrPort = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.cmbProtocol = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.panelTile = new DevComponents.DotNetBar.PanelEx();
            this.lblPlaySpeed = new DevComponents.DotNetBar.LabelX();
            this.lblDownLoadPos = new DevComponents.DotNetBar.LabelX();
            this.picDownloading = new System.Windows.Forms.PictureBox();
            this.picFinding = new System.Windows.Forms.PictureBox();
            this.picRecordShow = new System.Windows.Forms.PictureBox();
            this.lblPlayPos = new DevComponents.DotNetBar.LabelX();
            this.lblCamInfo = new DevComponents.DotNetBar.LabelX();
            this.panelControls = new DevComponents.DotNetBar.PanelEx();
            this.btnForwardFrame = new DevComponents.DotNetBar.ButtonX();
            this.AppcommandPlayControl = new DevComponents.DotNetBar.Command(this.components);
            this.btnBackwardSlow = new DevComponents.DotNetBar.ButtonX();
            this.timeline1 = new justin.time.axis.Timeline();
            this.btnFullScreen = new DevComponents.DotNetBar.ButtonX();
            this.btnPlayNormal = new DevComponents.DotNetBar.ButtonX();
            this.sliderVolume = new DevComponents.DotNetBar.Controls.Slider();
            this.btnMute = new DevComponents.DotNetBar.ButtonX();
            this.btnDownload = new DevComponents.DotNetBar.ButtonX();
            this.btnRec = new DevComponents.DotNetBar.ButtonX();
            this.btnCapture = new DevComponents.DotNetBar.ButtonX();
            this.btnForwardSlow = new DevComponents.DotNetBar.ButtonX();
            this.btnForwardFast = new DevComponents.DotNetBar.ButtonX();
            this.btnBackwordFast = new DevComponents.DotNetBar.ButtonX();
            this.btnStop = new DevComponents.DotNetBar.ButtonX();
            this.btnPlay = new DevComponents.DotNetBar.ButtonX();
            this.btnEject = new DevComponents.DotNetBar.ButtonX();
            this.timerGetPlayPos = new System.Windows.Forms.Timer(this.components);
            this.timerGetDownloadPos = new System.Windows.Forms.Timer(this.components);
            this.colorSliderPos = new GHIBMS.VideoPlayback.ColorSlider();
            this.panelBack.SuspendLayout();
            this.panelPlayer.SuspendLayout();
            this.exPanelVideoSource.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ipInputVod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ipInputDvr)).BeginInit();
            this.panelTile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDownloading)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFinding)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRecordShow)).BeginInit();
            this.panelControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBack
            // 
            this.panelBack.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelBack.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelBack.Controls.Add(this.panelPlayer);
            this.panelBack.Controls.Add(this.panelTile);
            this.panelBack.Controls.Add(this.panelControls);
            this.panelBack.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelBack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBack.Location = new System.Drawing.Point(0, 0);
            this.panelBack.Margin = new System.Windows.Forms.Padding(6);
            this.panelBack.Name = "panelBack";
            this.panelBack.Size = new System.Drawing.Size(1350, 1002);
            this.panelBack.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelBack.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelBack.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelBack.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelBack.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelBack.Style.GradientAngle = 90;
            this.panelBack.TabIndex = 2;
            this.panelBack.Text = "panelEx1";
            // 
            // panelPlayer
            // 
            this.panelPlayer.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelPlayer.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelPlayer.Controls.Add(this.exPanelVideoSource);
            this.panelPlayer.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelPlayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPlayer.Location = new System.Drawing.Point(0, 62);
            this.panelPlayer.Margin = new System.Windows.Forms.Padding(6);
            this.panelPlayer.Name = "panelPlayer";
            this.panelPlayer.Size = new System.Drawing.Size(1350, 752);
            this.panelPlayer.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelPlayer.Style.BackColor1.Color = System.Drawing.Color.Navy;
            this.panelPlayer.Style.BackColor2.Color = System.Drawing.Color.Navy;
            this.panelPlayer.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelPlayer.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelPlayer.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelPlayer.Style.GradientAngle = 90;
            this.panelPlayer.TabIndex = 4;
            this.panelPlayer.Click += new System.EventHandler(this.panelPlayer_Click);
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
            this.exPanelVideoSource.Location = new System.Drawing.Point(6, 12);
            this.exPanelVideoSource.Margin = new System.Windows.Forms.Padding(6);
            this.exPanelVideoSource.Name = "exPanelVideoSource";
            this.exPanelVideoSource.Size = new System.Drawing.Size(642, 476);
            this.exPanelVideoSource.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.exPanelVideoSource.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.exPanelVideoSource.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.exPanelVideoSource.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.exPanelVideoSource.Style.GradientAngle = 90;
            this.exPanelVideoSource.TabIndex = 1;
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
            this.ipInputVod.ButtonFreeText.Tooltip = "";
            this.ipInputVod.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipInputVod.Location = new System.Drawing.Point(146, 366);
            this.ipInputVod.Margin = new System.Windows.Forms.Padding(6);
            this.ipInputVod.Name = "ipInputVod";
            this.ipInputVod.Size = new System.Drawing.Size(240, 35);
            this.ipInputVod.TabIndex = 21;
            // 
            // ipInputDvr
            // 
            this.ipInputDvr.AutoOverwrite = true;
            this.ipInputDvr.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.ipInputDvr.BackgroundStyle.Class = "DateTimeInputBackground";
            this.ipInputDvr.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ipInputDvr.ButtonClear.Tooltip = "";
            this.ipInputDvr.ButtonCustom.Tooltip = "";
            this.ipInputDvr.ButtonCustom2.Tooltip = "";
            this.ipInputDvr.ButtonDropDown.Tooltip = "";
            this.ipInputDvr.ButtonFreeText.Tooltip = "";
            this.ipInputDvr.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipInputDvr.Location = new System.Drawing.Point(146, 166);
            this.ipInputDvr.Margin = new System.Windows.Forms.Padding(6);
            this.ipInputDvr.Name = "ipInputDvr";
            this.ipInputDvr.Size = new System.Drawing.Size(240, 35);
            this.ipInputDvr.TabIndex = 20;
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
            this.txtDvrCH.Margin = new System.Windows.Forms.Padding(6);
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
            this.labelX8.Margin = new System.Windows.Forms.Padding(6);
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
            this.btnCancel.Margin = new System.Windows.Forms.Padding(6);
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
            this.btnConnect.Margin = new System.Windows.Forms.Padding(6);
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
            this.txtVodPort.Margin = new System.Windows.Forms.Padding(6);
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
            this.labelX7.Margin = new System.Windows.Forms.Padding(6);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(109, 33);
            this.labelX7.TabIndex = 12;
            this.labelX7.Text = "流媒体IP";
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
            this.txtPassword.Margin = new System.Windows.Forms.Padding(6);
            this.txtPassword.MaxLength = 6;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(108, 35);
            this.txtPassword.TabIndex = 10;
            // 
            // labelX5
            // 
            this.labelX5.AutoSize = true;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(418, 232);
            this.labelX5.Margin = new System.Windows.Forms.Padding(6);
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
            this.txtUserName.Margin = new System.Windows.Forms.Padding(6);
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
            this.labelX4.Margin = new System.Windows.Forms.Padding(6);
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
            this.txtDvrPort.Margin = new System.Windows.Forms.Padding(6);
            this.txtDvrPort.Name = "txtDvrPort";
            this.txtDvrPort.Size = new System.Drawing.Size(108, 35);
            this.txtDvrPort.TabIndex = 6;
            this.txtDvrPort.Text = "0";
            // 
            // labelX3
            // 
            this.labelX3.AutoSize = true;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(418, 164);
            this.labelX3.Margin = new System.Windows.Forms.Padding(6);
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
            this.labelX2.Margin = new System.Windows.Forms.Padding(6);
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
            this.cmbProtocol.Margin = new System.Windows.Forms.Padding(6);
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
            this.labelX1.Margin = new System.Windows.Forms.Padding(6);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(109, 33);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "通讯协议";
            // 
            // panelTile
            // 
            this.panelTile.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelTile.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelTile.Controls.Add(this.lblPlaySpeed);
            this.panelTile.Controls.Add(this.lblDownLoadPos);
            this.panelTile.Controls.Add(this.picDownloading);
            this.panelTile.Controls.Add(this.picFinding);
            this.panelTile.Controls.Add(this.picRecordShow);
            this.panelTile.Controls.Add(this.lblPlayPos);
            this.panelTile.Controls.Add(this.lblCamInfo);
            this.panelTile.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelTile.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTile.Location = new System.Drawing.Point(0, 0);
            this.panelTile.Margin = new System.Windows.Forms.Padding(6);
            this.panelTile.Name = "panelTile";
            this.panelTile.Size = new System.Drawing.Size(1350, 62);
            this.panelTile.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelTile.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelTile.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelTile.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelTile.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelTile.Style.GradientAngle = 90;
            this.panelTile.TabIndex = 3;
            // 
            // lblPlaySpeed
            // 
            this.lblPlaySpeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPlaySpeed.AutoSize = true;
            // 
            // 
            // 
            this.lblPlaySpeed.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblPlaySpeed.Location = new System.Drawing.Point(660, 14);
            this.lblPlaySpeed.Margin = new System.Windows.Forms.Padding(6);
            this.lblPlaySpeed.Name = "lblPlaySpeed";
            this.lblPlaySpeed.Size = new System.Drawing.Size(146, 33);
            this.lblPlaySpeed.TabIndex = 14;
            this.lblPlaySpeed.Text = "播放速度：1";
            // 
            // lblDownLoadPos
            // 
            this.lblDownLoadPos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDownLoadPos.AutoSize = true;
            // 
            // 
            // 
            this.lblDownLoadPos.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblDownLoadPos.Location = new System.Drawing.Point(384, 14);
            this.lblDownLoadPos.Margin = new System.Windows.Forms.Padding(6);
            this.lblDownLoadPos.Name = "lblDownLoadPos";
            this.lblDownLoadPos.Size = new System.Drawing.Size(134, 33);
            this.lblDownLoadPos.TabIndex = 13;
            this.lblDownLoadPos.Text = "下载进度：";
            this.lblDownLoadPos.Visible = false;
            // 
            // picDownloading
            // 
            this.picDownloading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picDownloading.Image = ((System.Drawing.Image)(resources.GetObject("picDownloading.Image")));
            this.picDownloading.InitialImage = null;
            this.picDownloading.Location = new System.Drawing.Point(1264, 16);
            this.picDownloading.Margin = new System.Windows.Forms.Padding(6);
            this.picDownloading.Name = "picDownloading";
            this.picDownloading.Size = new System.Drawing.Size(16, 16);
            this.picDownloading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picDownloading.TabIndex = 12;
            this.picDownloading.TabStop = false;
            this.picDownloading.Visible = false;
            // 
            // picFinding
            // 
            this.picFinding.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picFinding.Image = ((System.Drawing.Image)(resources.GetObject("picFinding.Image")));
            this.picFinding.InitialImage = null;
            this.picFinding.Location = new System.Drawing.Point(1178, 16);
            this.picFinding.Margin = new System.Windows.Forms.Padding(6);
            this.picFinding.Name = "picFinding";
            this.picFinding.Size = new System.Drawing.Size(16, 16);
            this.picFinding.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picFinding.TabIndex = 11;
            this.picFinding.TabStop = false;
            this.picFinding.Visible = false;
            // 
            // picRecordShow
            // 
            this.picRecordShow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picRecordShow.Image = ((System.Drawing.Image)(resources.GetObject("picRecordShow.Image")));
            this.picRecordShow.Location = new System.Drawing.Point(1220, 14);
            this.picRecordShow.Margin = new System.Windows.Forms.Padding(6);
            this.picRecordShow.Name = "picRecordShow";
            this.picRecordShow.Size = new System.Drawing.Size(16, 16);
            this.picRecordShow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picRecordShow.TabIndex = 10;
            this.picRecordShow.TabStop = false;
            this.picRecordShow.Visible = false;
            // 
            // lblPlayPos
            // 
            this.lblPlayPos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPlayPos.AutoSize = true;
            // 
            // 
            // 
            this.lblPlayPos.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblPlayPos.Location = new System.Drawing.Point(968, 16);
            this.lblPlayPos.Margin = new System.Windows.Forms.Padding(6);
            this.lblPlayPos.Name = "lblPlayPos";
            this.lblPlayPos.Size = new System.Drawing.Size(171, 33);
            this.lblPlayPos.TabIndex = 2;
            this.lblPlayPos.Text = "播放进度：0/0";
            // 
            // lblCamInfo
            // 
            this.lblCamInfo.AutoSize = true;
            // 
            // 
            // 
            this.lblCamInfo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblCamInfo.Location = new System.Drawing.Point(18, 20);
            this.lblCamInfo.Margin = new System.Windows.Forms.Padding(6);
            this.lblCamInfo.Name = "lblCamInfo";
            this.lblCamInfo.Size = new System.Drawing.Size(0, 0);
            this.lblCamInfo.TabIndex = 0;
            // 
            // panelControls
            // 
            this.panelControls.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelControls.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelControls.Controls.Add(this.btnForwardFrame);
            this.panelControls.Controls.Add(this.btnBackwardSlow);
            this.panelControls.Controls.Add(this.timeline1);
            this.panelControls.Controls.Add(this.btnFullScreen);
            this.panelControls.Controls.Add(this.btnPlayNormal);
            this.panelControls.Controls.Add(this.sliderVolume);
            this.panelControls.Controls.Add(this.btnMute);
            this.panelControls.Controls.Add(this.btnDownload);
            this.panelControls.Controls.Add(this.btnRec);
            this.panelControls.Controls.Add(this.btnCapture);
            this.panelControls.Controls.Add(this.btnForwardSlow);
            this.panelControls.Controls.Add(this.btnForwardFast);
            this.panelControls.Controls.Add(this.btnBackwordFast);
            this.panelControls.Controls.Add(this.btnStop);
            this.panelControls.Controls.Add(this.btnPlay);
            this.panelControls.Controls.Add(this.btnEject);
            this.panelControls.Controls.Add(this.colorSliderPos);
            this.panelControls.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelControls.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControls.Location = new System.Drawing.Point(0, 814);
            this.panelControls.Margin = new System.Windows.Forms.Padding(6);
            this.panelControls.Name = "panelControls";
            this.panelControls.Size = new System.Drawing.Size(1350, 188);
            this.panelControls.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelControls.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelControls.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelControls.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelControls.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelControls.Style.GradientAngle = 90;
            this.panelControls.TabIndex = 2;
            // 
            // btnForwardFrame
            // 
            this.btnForwardFrame.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnForwardFrame.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnForwardFrame.Command = this.AppcommandPlayControl;
            this.btnForwardFrame.CommandParameter = "FORWARDFRAME";
            this.btnForwardFrame.Enabled = false;
            this.btnForwardFrame.Image = ((System.Drawing.Image)(resources.GetObject("btnForwardFrame.Image")));
            this.btnForwardFrame.Location = new System.Drawing.Point(436, 50);
            this.btnForwardFrame.Margin = new System.Windows.Forms.Padding(6);
            this.btnForwardFrame.Name = "btnForwardFrame";
            this.btnForwardFrame.Size = new System.Drawing.Size(50, 46);
            this.btnForwardFrame.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnForwardFrame.TabIndex = 18;
            this.btnForwardFrame.Tooltip = "单帧播放";
            // 
            // AppcommandPlayControl
            // 
            this.AppcommandPlayControl.Name = "AppcommandPlayControl";
            this.AppcommandPlayControl.Executed += new System.EventHandler(this.AppcommandPlayControl_Executed);
            // 
            // btnBackwardSlow
            // 
            this.btnBackwardSlow.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnBackwardSlow.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnBackwardSlow.Command = this.AppcommandPlayControl;
            this.btnBackwardSlow.CommandParameter = "BACKWORDSLOW";
            this.btnBackwardSlow.Enabled = false;
            this.btnBackwardSlow.Image = ((System.Drawing.Image)(resources.GetObject("btnBackwardSlow.Image")));
            this.btnBackwardSlow.Location = new System.Drawing.Point(168, 50);
            this.btnBackwardSlow.Margin = new System.Windows.Forms.Padding(6);
            this.btnBackwardSlow.Name = "btnBackwardSlow";
            this.btnBackwardSlow.Size = new System.Drawing.Size(50, 46);
            this.btnBackwardSlow.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnBackwardSlow.TabIndex = 17;
            this.btnBackwardSlow.Tooltip = "慢退";
            // 
            // timeline1
            // 
            this.timeline1.BackColor = System.Drawing.SystemColors.Control;
            this.timeline1.CurrentX = 0;
            this.timeline1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.timeline1.Location = new System.Drawing.Point(0, 121);
            this.timeline1.Margin = new System.Windows.Forms.Padding(6);
            this.timeline1.Name = "timeline1";
            this.timeline1.Position = new System.DateTime(2014, 4, 8, 12, 0, 0, 0);
            this.timeline1.ShowMenu = false;
            this.timeline1.Size = new System.Drawing.Size(1350, 67);
            this.timeline1.TabIndex = 16;
            this.timeline1.Text = "timeline1";
            // 
            // btnFullScreen
            // 
            this.btnFullScreen.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnFullScreen.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnFullScreen.Command = this.AppcommandPlayControl;
            this.btnFullScreen.CommandParameter = "FULLSCREEN";
            this.btnFullScreen.Image = ((System.Drawing.Image)(resources.GetObject("btnFullScreen.Image")));
            this.btnFullScreen.Location = new System.Drawing.Point(660, 50);
            this.btnFullScreen.Margin = new System.Windows.Forms.Padding(6);
            this.btnFullScreen.Name = "btnFullScreen";
            this.btnFullScreen.Size = new System.Drawing.Size(50, 46);
            this.btnFullScreen.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnFullScreen.TabIndex = 15;
            this.btnFullScreen.Tooltip = "窗口";
            // 
            // btnPlayNormal
            // 
            this.btnPlayNormal.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPlayNormal.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnPlayNormal.Command = this.AppcommandPlayControl;
            this.btnPlayNormal.CommandParameter = "NORMAL";
            this.btnPlayNormal.Enabled = false;
            this.btnPlayNormal.Image = ((System.Drawing.Image)(resources.GetObject("btnPlayNormal.Image")));
            this.btnPlayNormal.Location = new System.Drawing.Point(276, 50);
            this.btnPlayNormal.Margin = new System.Windows.Forms.Padding(6);
            this.btnPlayNormal.Name = "btnPlayNormal";
            this.btnPlayNormal.Size = new System.Drawing.Size(50, 46);
            this.btnPlayNormal.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPlayNormal.TabIndex = 14;
            this.btnPlayNormal.Tooltip = "正常播放";
            // 
            // sliderVolume
            // 
            // 
            // 
            // 
            this.sliderVolume.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.sliderVolume.Enabled = false;
            this.sliderVolume.LabelVisible = false;
            this.sliderVolume.Location = new System.Drawing.Point(832, 50);
            this.sliderVolume.Margin = new System.Windows.Forms.Padding(6);
            this.sliderVolume.Name = "sliderVolume";
            this.sliderVolume.Size = new System.Drawing.Size(162, 46);
            this.sliderVolume.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.sliderVolume.TabIndex = 13;
            this.sliderVolume.Text = "slider1";
            this.sliderVolume.Value = 0;
            this.sliderVolume.ValueChanging += new DevComponents.DotNetBar.CancelIntValueEventHandler(this.sliderVolume_ValueChanging);
            // 
            // btnMute
            // 
            this.btnMute.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnMute.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnMute.Command = this.AppcommandPlayControl;
            this.btnMute.CommandParameter = "SOUND";
            this.btnMute.Enabled = false;
            this.btnMute.Image = ((System.Drawing.Image)(resources.GetObject("btnMute.Image")));
            this.btnMute.Location = new System.Drawing.Point(714, 50);
            this.btnMute.Margin = new System.Windows.Forms.Padding(6);
            this.btnMute.Name = "btnMute";
            this.btnMute.Size = new System.Drawing.Size(50, 46);
            this.btnMute.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnMute.TabIndex = 12;
            this.btnMute.Tooltip = "声音";
            // 
            // btnDownload
            // 
            this.btnDownload.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDownload.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnDownload.Command = this.AppcommandPlayControl;
            this.btnDownload.CommandParameter = "DOWNLOAD";
            this.btnDownload.Image = ((System.Drawing.Image)(resources.GetObject("btnDownload.Image")));
            this.btnDownload.Location = new System.Drawing.Point(606, 50);
            this.btnDownload.Margin = new System.Windows.Forms.Padding(6);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(50, 46);
            this.btnDownload.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnDownload.TabIndex = 11;
            this.btnDownload.Tooltip = "下载";
            // 
            // btnRec
            // 
            this.btnRec.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRec.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnRec.Command = this.AppcommandPlayControl;
            this.btnRec.CommandParameter = "REC";
            this.btnRec.Enabled = false;
            this.btnRec.Image = ((System.Drawing.Image)(resources.GetObject("btnRec.Image")));
            this.btnRec.Location = new System.Drawing.Point(552, 50);
            this.btnRec.Margin = new System.Windows.Forms.Padding(6);
            this.btnRec.Name = "btnRec";
            this.btnRec.Size = new System.Drawing.Size(50, 46);
            this.btnRec.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnRec.TabIndex = 10;
            this.btnRec.Tooltip = "录像";
            // 
            // btnCapture
            // 
            this.btnCapture.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCapture.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCapture.Command = this.AppcommandPlayControl;
            this.btnCapture.CommandParameter = "PICTURE";
            this.btnCapture.Enabled = false;
            this.btnCapture.Image = ((System.Drawing.Image)(resources.GetObject("btnCapture.Image")));
            this.btnCapture.Location = new System.Drawing.Point(498, 50);
            this.btnCapture.Margin = new System.Windows.Forms.Padding(6);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(50, 46);
            this.btnCapture.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCapture.TabIndex = 9;
            this.btnCapture.Tooltip = "拍照";
            // 
            // btnForwardSlow
            // 
            this.btnForwardSlow.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnForwardSlow.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnForwardSlow.Command = this.AppcommandPlayControl;
            this.btnForwardSlow.CommandParameter = "FORWARDSLOW";
            this.btnForwardSlow.Enabled = false;
            this.btnForwardSlow.Image = ((System.Drawing.Image)(resources.GetObject("btnForwardSlow.Image")));
            this.btnForwardSlow.Location = new System.Drawing.Point(384, 50);
            this.btnForwardSlow.Margin = new System.Windows.Forms.Padding(6);
            this.btnForwardSlow.Name = "btnForwardSlow";
            this.btnForwardSlow.Size = new System.Drawing.Size(50, 46);
            this.btnForwardSlow.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnForwardSlow.TabIndex = 8;
            this.btnForwardSlow.Tooltip = "慢进";
            // 
            // btnForwardFast
            // 
            this.btnForwardFast.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnForwardFast.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnForwardFast.Command = this.AppcommandPlayControl;
            this.btnForwardFast.CommandParameter = "FORWARDFAST";
            this.btnForwardFast.Enabled = false;
            this.btnForwardFast.Image = ((System.Drawing.Image)(resources.GetObject("btnForwardFast.Image")));
            this.btnForwardFast.Location = new System.Drawing.Point(330, 50);
            this.btnForwardFast.Margin = new System.Windows.Forms.Padding(6);
            this.btnForwardFast.Name = "btnForwardFast";
            this.btnForwardFast.Size = new System.Drawing.Size(50, 46);
            this.btnForwardFast.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnForwardFast.TabIndex = 7;
            this.btnForwardFast.Tooltip = "快进";
            // 
            // btnBackwordFast
            // 
            this.btnBackwordFast.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnBackwordFast.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnBackwordFast.Command = this.AppcommandPlayControl;
            this.btnBackwordFast.CommandParameter = "BACKWORDFAST";
            this.btnBackwordFast.Enabled = false;
            this.btnBackwordFast.Image = ((System.Drawing.Image)(resources.GetObject("btnBackwordFast.Image")));
            this.btnBackwordFast.Location = new System.Drawing.Point(222, 50);
            this.btnBackwordFast.Margin = new System.Windows.Forms.Padding(6);
            this.btnBackwordFast.Name = "btnBackwordFast";
            this.btnBackwordFast.Size = new System.Drawing.Size(50, 46);
            this.btnBackwordFast.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnBackwordFast.TabIndex = 6;
            this.btnBackwordFast.Tooltip = "快退";
            this.btnBackwordFast.Click += new System.EventHandler(this.btnSlow_Click);
            // 
            // btnStop
            // 
            this.btnStop.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnStop.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnStop.Command = this.AppcommandPlayControl;
            this.btnStop.CommandParameter = "STOP";
            this.btnStop.Enabled = false;
            this.btnStop.Image = ((System.Drawing.Image)(resources.GetObject("btnStop.Image")));
            this.btnStop.Location = new System.Drawing.Point(114, 50);
            this.btnStop.Margin = new System.Windows.Forms.Padding(6);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(50, 46);
            this.btnStop.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnStop.TabIndex = 4;
            this.btnStop.Tooltip = "停止";
            // 
            // btnPlay
            // 
            this.btnPlay.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPlay.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnPlay.Command = this.AppcommandPlayControl;
            this.btnPlay.CommandParameter = "PLAY";
            this.btnPlay.Enabled = false;
            this.btnPlay.Image = ((System.Drawing.Image)(resources.GetObject("btnPlay.Image")));
            this.btnPlay.Location = new System.Drawing.Point(60, 50);
            this.btnPlay.Margin = new System.Windows.Forms.Padding(6);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(50, 46);
            this.btnPlay.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPlay.TabIndex = 3;
            this.btnPlay.Tooltip = "播放";
            // 
            // btnEject
            // 
            this.btnEject.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnEject.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnEject.Command = this.AppcommandPlayControl;
            this.btnEject.CommandParameter = "OPEN";
            this.btnEject.Image = ((System.Drawing.Image)(resources.GetObject("btnEject.Image")));
            this.btnEject.Location = new System.Drawing.Point(6, 50);
            this.btnEject.Margin = new System.Windows.Forms.Padding(6);
            this.btnEject.Name = "btnEject";
            this.btnEject.Size = new System.Drawing.Size(50, 46);
            this.btnEject.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnEject.TabIndex = 2;
            this.btnEject.Tooltip = "打开";
            // 
            // timerGetPlayPos
            // 
            this.timerGetPlayPos.Interval = 1000;
            this.timerGetPlayPos.Tick += new System.EventHandler(this.timerGetPlayPos_Tick);
            // 
            // timerGetDownloadPos
            // 
            this.timerGetDownloadPos.Interval = 1000;
            this.timerGetDownloadPos.Tick += new System.EventHandler(this.timerGetDownloadPos_Tick);
            // 
            // colorSliderPos
            // 
            this.colorSliderPos.BackColor = System.Drawing.Color.Transparent;
            this.colorSliderPos.BarInnerColor = System.Drawing.Color.CornflowerBlue;
            this.colorSliderPos.BorderRoundRectSize = new System.Drawing.Size(8, 8);
            this.colorSliderPos.Dock = System.Windows.Forms.DockStyle.Top;
            this.colorSliderPos.ElapsedInnerColor = System.Drawing.Color.Red;
            this.colorSliderPos.ElapsedOuterColor = System.Drawing.Color.Salmon;
            this.colorSliderPos.Enabled = false;
            this.colorSliderPos.LargeChange = ((uint)(5u));
            this.colorSliderPos.Location = new System.Drawing.Point(0, 0);
            this.colorSliderPos.Margin = new System.Windows.Forms.Padding(6);
            this.colorSliderPos.Name = "colorSliderPos";
            this.colorSliderPos.Size = new System.Drawing.Size(1350, 34);
            this.colorSliderPos.SmallChange = ((uint)(1u));
            this.colorSliderPos.TabIndex = 1;
            this.colorSliderPos.Text = "colorSlider1";
            this.colorSliderPos.ThumbRoundRectSize = new System.Drawing.Size(8, 8);
            this.colorSliderPos.Value = 0;
            this.colorSliderPos.MouseDown += new System.Windows.Forms.MouseEventHandler(this.colorSliderPos_MouseDown);
            this.colorSliderPos.MouseUp += new System.Windows.Forms.MouseEventHandler(this.colorSliderPos_MouseUp);
            // 
            // VideoPlaybackControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelBack);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "VideoPlaybackControl";
            this.Size = new System.Drawing.Size(1350, 1002);
            this.panelBack.ResumeLayout(false);
            this.panelPlayer.ResumeLayout(false);
            this.exPanelVideoSource.ResumeLayout(false);
            this.exPanelVideoSource.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ipInputVod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ipInputDvr)).EndInit();
            this.panelTile.ResumeLayout(false);
            this.panelTile.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDownloading)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFinding)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRecordShow)).EndInit();
            this.panelControls.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ColorSlider colorSliderPos;
        private DevComponents.DotNetBar.PanelEx panelBack;
        private DevComponents.DotNetBar.PanelEx panelControls;
        private DevComponents.DotNetBar.ButtonX btnEject;
        private DevComponents.DotNetBar.ButtonX btnStop;
        private DevComponents.DotNetBar.ButtonX btnPlay;
        private DevComponents.DotNetBar.ButtonX btnCapture;
        private DevComponents.DotNetBar.ButtonX btnForwardSlow;
        private DevComponents.DotNetBar.ButtonX btnForwardFast;
        private DevComponents.DotNetBar.ButtonX btnBackwordFast;
        private DevComponents.DotNetBar.ButtonX btnMute;
        private DevComponents.DotNetBar.ButtonX btnDownload;
        private DevComponents.DotNetBar.ButtonX btnRec;
        private DevComponents.DotNetBar.Controls.Slider sliderVolume;
        private DevComponents.DotNetBar.PanelEx panelPlayer;
        private DevComponents.DotNetBar.PanelEx panelTile;
        private DevComponents.DotNetBar.LabelX lblCamInfo;
        private DevComponents.DotNetBar.LabelX lblPlayPos;
        private System.Windows.Forms.PictureBox picRecordShow;
        private DevComponents.DotNetBar.ExpandablePanel exPanelVideoSource;
        private DevComponents.Editors.IpAddressInput ipInputVod;
        private DevComponents.Editors.IpAddressInput ipInputDvr;
        private DevComponents.DotNetBar.Controls.TextBoxX txtDvrCH;
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.ButtonX btnConnect;
        private DevComponents.DotNetBar.Controls.TextBoxX txtVodPort;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPassword;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.Controls.TextBoxX txtUserName;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.TextBoxX txtDvrPort;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbProtocol;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Command AppcommandPlayControl;
        private System.Windows.Forms.Timer timerGetPlayPos;
        private System.Windows.Forms.PictureBox picFinding;
        private System.Windows.Forms.PictureBox picDownloading;
        private DevComponents.DotNetBar.ButtonX btnPlayNormal;
        private DevComponents.DotNetBar.LabelX lblDownLoadPos;
        private System.Windows.Forms.Timer timerGetDownloadPos;
        private DevComponents.DotNetBar.ButtonX btnFullScreen;
        private justin.time.axis.Timeline timeline1;
        private DevComponents.DotNetBar.LabelX lblPlaySpeed;
        private DevComponents.DotNetBar.ButtonX btnBackwardSlow;
        private DevComponents.DotNetBar.ButtonX btnForwardFrame;
    }
}
