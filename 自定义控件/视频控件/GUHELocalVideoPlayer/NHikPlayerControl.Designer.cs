namespace GHIBMS.NHikPlayer
{
    partial class NHikPlayerControl
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
                if (bOpen)
                {
                    Close();
                 
                }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NHikPlayerControl));
            this.expandableSplitter1 = new DevComponents.DotNetBar.ExpandableSplitter();
            this.expPanelList = new DevComponents.DotNetBar.ExpandablePanel();
            this.combType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txtRecDir = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.labelItem1 = new DevComponents.DotNetBar.LabelItem();
            this.btnFileListOpen = new DevComponents.DotNetBar.ButtonItem();
            this.btnRemoveFolder = new DevComponents.DotNetBar.ButtonItem();
            this.btnFileListClear = new DevComponents.DotNetBar.ButtonItem();
            this.btnFileListSearch = new DevComponents.DotNetBar.ButtonItem();
            this.panelControls = new DevComponents.DotNetBar.PanelEx();
            this.cSliderTime = new GHIBMS.NHikPlayer.ColorSlider();
            this.btnPlayNormal = new DevComponents.DotNetBar.ButtonX();
            this.cSliderVolume = new DevComponents.DotNetBar.Controls.Slider();
            this.btnMute = new DevComponents.DotNetBar.ButtonX();
            this.btnCapture = new DevComponents.DotNetBar.ButtonX();
            this.btnFrame = new DevComponents.DotNetBar.ButtonX();
            this.btnFast = new DevComponents.DotNetBar.ButtonX();
            this.btnSlow = new DevComponents.DotNetBar.ButtonX();
            this.btnStop = new DevComponents.DotNetBar.ButtonX();
            this.btnPlay = new DevComponents.DotNetBar.ButtonX();
            this.btnEject = new DevComponents.DotNetBar.ButtonX();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblFileName = new DevComponents.DotNetBar.LabelX();
            this.tsslblFrame = new DevComponents.DotNetBar.LabelX();
            this.tsslblTime = new DevComponents.DotNetBar.LabelX();
            this.tsslblSpeed = new DevComponents.DotNetBar.LabelX();
            this.panelExTop = new DevComponents.DotNetBar.PanelEx();
            this.pVideo = new DevComponents.DotNetBar.PanelEx();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.expPanelList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.panelControls.SuspendLayout();
            this.panelExTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // expandableSplitter1
            // 
            this.expandableSplitter1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter1.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandableSplitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.expandableSplitter1.ExpandableControl = this.expPanelList;
            this.expandableSplitter1.ExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter1.ExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.ExpandLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.expandableSplitter1.ExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitter1.GripDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.expandableSplitter1.GripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitter1.GripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.expandableSplitter1.GripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.expandableSplitter1.HotBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(151)))), ((int)(((byte)(61)))));
            this.expandableSplitter1.HotBackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(184)))), ((int)(((byte)(94)))));
            this.expandableSplitter1.HotBackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground2;
            this.expandableSplitter1.HotBackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground;
            this.expandableSplitter1.HotExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter1.HotExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.HotExpandLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.expandableSplitter1.HotExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitter1.HotGripDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter1.HotGripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.HotGripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.expandableSplitter1.HotGripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.expandableSplitter1.Location = new System.Drawing.Point(550, 0);
            this.expandableSplitter1.Name = "expandableSplitter1";
            this.expandableSplitter1.Size = new System.Drawing.Size(4, 472);
            this.expandableSplitter1.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007;
            this.expandableSplitter1.TabIndex = 1;
            this.expandableSplitter1.TabStop = false;
            // 
            // expPanelList
            // 
            this.expPanelList.CanvasColor = System.Drawing.SystemColors.Control;
            this.expPanelList.CollapseDirection = DevComponents.DotNetBar.eCollapseDirection.LeftToRight;
            this.expPanelList.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.expPanelList.Controls.Add(this.combType);
            this.expPanelList.Controls.Add(this.labelX2);
            this.expPanelList.Controls.Add(this.txtRecDir);
            this.expPanelList.Controls.Add(this.labelX1);
            this.expPanelList.Controls.Add(this.treeView1);
            this.expPanelList.Controls.Add(this.bar1);
            this.expPanelList.Dock = System.Windows.Forms.DockStyle.Right;
            this.expPanelList.Location = new System.Drawing.Point(554, 0);
            this.expPanelList.Name = "expPanelList";
            this.expPanelList.Size = new System.Drawing.Size(296, 472);
            this.expPanelList.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.expPanelList.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expPanelList.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expPanelList.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.expPanelList.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.expPanelList.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expPanelList.Style.GradientAngle = 90;
            this.expPanelList.TabIndex = 0;
            this.expPanelList.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
            this.expPanelList.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expPanelList.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expPanelList.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.expPanelList.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expPanelList.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.expPanelList.TitleStyle.GradientAngle = 90;
            this.expPanelList.TitleText = "播放列表";
            // 
            // combType
            // 
            this.combType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.combType.DisplayMember = "Text";
            this.combType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.combType.FormattingEnabled = true;
            this.combType.ItemHeight = 15;
            this.combType.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2});
            this.combType.Location = new System.Drawing.Point(71, 378);
            this.combType.Name = "combType";
            this.combType.Size = new System.Drawing.Size(206, 21);
            this.combType.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.combType.TabIndex = 8;
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "海康";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "大华";
            // 
            // labelX2
            // 
            this.labelX2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelX2.AutoSize = true;
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(6, 382);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(68, 18);
            this.labelX2.TabIndex = 7;
            this.labelX2.Text = "录像类型：";
            // 
            // txtRecDir
            // 
            this.txtRecDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtRecDir.Border.Class = "TextBoxBorder";
            this.txtRecDir.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtRecDir.Location = new System.Drawing.Point(71, 415);
            this.txtRecDir.Name = "txtRecDir";
            this.txtRecDir.Size = new System.Drawing.Size(206, 21);
            this.txtRecDir.TabIndex = 6;
            // 
            // labelX1
            // 
            this.labelX1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelX1.AutoSize = true;
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(7, 418);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(68, 18);
            this.labelX1.TabIndex = 5;
            this.labelX1.Text = "录像目录：";
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(3, 29);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 1;
            this.treeView1.Size = new System.Drawing.Size(290, 327);
            this.treeView1.TabIndex = 4;
            this.treeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "(02,31).png");
            this.imageList1.Images.SetKeyName(1, "openfolderHS.png");
            this.imageList1.Images.SetKeyName(2, "ico_video.png");
            // 
            // bar1
            // 
            this.bar1.AntiAlias = true;
            this.bar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bar1.DockSide = DevComponents.DotNetBar.eDockSide.Right;
            this.bar1.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItem1,
            this.btnFileListOpen,
            this.btnRemoveFolder,
            this.btnFileListClear,
            this.btnFileListSearch});
            this.bar1.Location = new System.Drawing.Point(0, 447);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(296, 25);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar1.TabIndex = 1;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // labelItem1
            // 
            this.labelItem1.Name = "labelItem1";
            this.labelItem1.Text = " ";
            // 
            // btnFileListOpen
            // 
            this.btnFileListOpen.Image = ((System.Drawing.Image)(resources.GetObject("btnFileListOpen.Image")));
            this.btnFileListOpen.Name = "btnFileListOpen";
            this.btnFileListOpen.Text = "buttonItem1";
            this.btnFileListOpen.Tooltip = "打开文件夹";
            this.btnFileListOpen.Click += new System.EventHandler(this.btnFileListAdd_Click);
            // 
            // btnRemoveFolder
            // 
            this.btnRemoveFolder.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveFolder.Image")));
            this.btnRemoveFolder.Name = "btnRemoveFolder";
            this.btnRemoveFolder.Text = "buttonItem1";
            this.btnRemoveFolder.Tooltip = "移除文件夹";
            this.btnRemoveFolder.Click += new System.EventHandler(this.btnRemoveFolder_Click);
            // 
            // btnFileListClear
            // 
            this.btnFileListClear.Image = ((System.Drawing.Image)(resources.GetObject("btnFileListClear.Image")));
            this.btnFileListClear.Name = "btnFileListClear";
            this.btnFileListClear.Text = "buttonItem3";
            this.btnFileListClear.Tooltip = "清除文件";
            this.btnFileListClear.Click += new System.EventHandler(this.btnFileListClear_Click);
            // 
            // btnFileListSearch
            // 
            this.btnFileListSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnFileListSearch.Image")));
            this.btnFileListSearch.Name = "btnFileListSearch";
            this.btnFileListSearch.Text = "buttonItem1";
            this.btnFileListSearch.Tooltip = "查的文件";
            this.btnFileListSearch.Click += new System.EventHandler(this.btnFileListSearch_Click);
            // 
            // panelControls
            // 
            this.panelControls.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelControls.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelControls.Controls.Add(this.cSliderTime);
            this.panelControls.Controls.Add(this.btnPlayNormal);
            this.panelControls.Controls.Add(this.cSliderVolume);
            this.panelControls.Controls.Add(this.btnMute);
            this.panelControls.Controls.Add(this.btnCapture);
            this.panelControls.Controls.Add(this.btnFrame);
            this.panelControls.Controls.Add(this.btnFast);
            this.panelControls.Controls.Add(this.btnSlow);
            this.panelControls.Controls.Add(this.btnStop);
            this.panelControls.Controls.Add(this.btnPlay);
            this.panelControls.Controls.Add(this.btnEject);
            this.panelControls.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControls.Location = new System.Drawing.Point(0, 413);
            this.panelControls.Name = "panelControls";
            this.panelControls.Size = new System.Drawing.Size(550, 59);
            this.panelControls.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelControls.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelControls.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelControls.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelControls.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelControls.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelControls.Style.GradientAngle = 90;
            this.panelControls.TabIndex = 3;
            // 
            // cSliderTime
            // 
            this.cSliderTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cSliderTime.BackColor = System.Drawing.Color.Transparent;
            this.cSliderTime.BorderRoundRectSize = new System.Drawing.Size(8, 8);
            this.cSliderTime.ElapsedInnerColor = System.Drawing.Color.Crimson;
            this.cSliderTime.ElapsedOuterColor = System.Drawing.Color.Plum;
            this.cSliderTime.LargeChange = ((uint)(5u));
            this.cSliderTime.Location = new System.Drawing.Point(3, 2);
            this.cSliderTime.Name = "cSliderTime";
            this.cSliderTime.Size = new System.Drawing.Size(541, 19);
            this.cSliderTime.SmallChange = ((uint)(1u));
            this.cSliderTime.TabIndex = 15;
            this.cSliderTime.Text = "colorSlider2";
            this.cSliderTime.ThumbRoundRectSize = new System.Drawing.Size(8, 8);
            this.cSliderTime.Value = 0;
            this.cSliderTime.MouseDown += new System.Windows.Forms.MouseEventHandler(this.cSliderTime_MouseDown);
            this.cSliderTime.MouseUp += new System.Windows.Forms.MouseEventHandler(this.cSliderTime_MouseUp);
            // 
            // btnPlayNormal
            // 
            this.btnPlayNormal.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPlayNormal.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnPlayNormal.CommandParameter = "NORMAL";
            this.btnPlayNormal.Enabled = false;
            this.btnPlayNormal.Image = ((System.Drawing.Image)(resources.GetObject("btnPlayNormal.Image")));
            this.btnPlayNormal.Location = new System.Drawing.Point(127, 25);
            this.btnPlayNormal.Name = "btnPlayNormal";
            this.btnPlayNormal.Size = new System.Drawing.Size(28, 23);
            this.btnPlayNormal.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPlayNormal.TabIndex = 14;
            this.btnPlayNormal.Tooltip = "正常播放";
            this.btnPlayNormal.Click += new System.EventHandler(this.btnPlayNormal_Click);
            // 
            // cSliderVolume
            // 
            // 
            // 
            // 
            this.cSliderVolume.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.cSliderVolume.Enabled = false;
            this.cSliderVolume.LabelVisible = false;
            this.cSliderVolume.Location = new System.Drawing.Point(301, 25);
            this.cSliderVolume.Name = "cSliderVolume";
            this.cSliderVolume.Size = new System.Drawing.Size(145, 23);
            this.cSliderVolume.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cSliderVolume.TabIndex = 13;
            this.cSliderVolume.Text = "slider1";
            this.cSliderVolume.Value = 0;
            // 
            // btnMute
            // 
            this.btnMute.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnMute.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnMute.CommandParameter = "SOUND";
            this.btnMute.Enabled = false;
            this.btnMute.Image = ((System.Drawing.Image)(resources.GetObject("btnMute.Image")));
            this.btnMute.Location = new System.Drawing.Point(251, 25);
            this.btnMute.Name = "btnMute";
            this.btnMute.Size = new System.Drawing.Size(28, 23);
            this.btnMute.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnMute.TabIndex = 12;
            this.btnMute.Tooltip = "声音";
            this.btnMute.Click += new System.EventHandler(this.btnMute_Click);
            // 
            // btnCapture
            // 
            this.btnCapture.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCapture.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCapture.CommandParameter = "PICTURE";
            this.btnCapture.Enabled = false;
            this.btnCapture.Image = ((System.Drawing.Image)(resources.GetObject("btnCapture.Image")));
            this.btnCapture.Location = new System.Drawing.Point(220, 25);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(28, 23);
            this.btnCapture.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCapture.TabIndex = 9;
            this.btnCapture.Tooltip = "拍照";
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
            // 
            // btnFrame
            // 
            this.btnFrame.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnFrame.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnFrame.CommandParameter = "FRAME";
            this.btnFrame.Enabled = false;
            this.btnFrame.Image = ((System.Drawing.Image)(resources.GetObject("btnFrame.Image")));
            this.btnFrame.Location = new System.Drawing.Point(189, 25);
            this.btnFrame.Name = "btnFrame";
            this.btnFrame.Size = new System.Drawing.Size(28, 23);
            this.btnFrame.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnFrame.TabIndex = 8;
            this.btnFrame.Tooltip = "单帧播放";
            this.btnFrame.Click += new System.EventHandler(this.btnFrame_Click);
            // 
            // btnFast
            // 
            this.btnFast.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnFast.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnFast.CommandParameter = "FAST";
            this.btnFast.Enabled = false;
            this.btnFast.Image = ((System.Drawing.Image)(resources.GetObject("btnFast.Image")));
            this.btnFast.Location = new System.Drawing.Point(158, 25);
            this.btnFast.Name = "btnFast";
            this.btnFast.Size = new System.Drawing.Size(28, 23);
            this.btnFast.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnFast.TabIndex = 7;
            this.btnFast.Tooltip = "快放";
            this.btnFast.Click += new System.EventHandler(this.btnFast_Click);
            // 
            // btnSlow
            // 
            this.btnSlow.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSlow.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSlow.CommandParameter = "SLOW";
            this.btnSlow.Enabled = false;
            this.btnSlow.Image = ((System.Drawing.Image)(resources.GetObject("btnSlow.Image")));
            this.btnSlow.Location = new System.Drawing.Point(96, 25);
            this.btnSlow.Name = "btnSlow";
            this.btnSlow.Size = new System.Drawing.Size(28, 23);
            this.btnSlow.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSlow.TabIndex = 6;
            this.btnSlow.Tooltip = "慢放";
            this.btnSlow.Click += new System.EventHandler(this.btnSlow_Click);
            // 
            // btnStop
            // 
            this.btnStop.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnStop.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnStop.CommandParameter = "STOP";
            this.btnStop.Enabled = false;
            this.btnStop.Image = ((System.Drawing.Image)(resources.GetObject("btnStop.Image")));
            this.btnStop.Location = new System.Drawing.Point(65, 25);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(28, 23);
            this.btnStop.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnStop.TabIndex = 4;
            this.btnStop.Tooltip = "停止";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPlay.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnPlay.CommandParameter = "PLAY";
            this.btnPlay.Enabled = false;
            this.btnPlay.Image = ((System.Drawing.Image)(resources.GetObject("btnPlay.Image")));
            this.btnPlay.Location = new System.Drawing.Point(34, 25);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(28, 23);
            this.btnPlay.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPlay.TabIndex = 3;
            this.btnPlay.Tooltip = "播放";
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnEject
            // 
            this.btnEject.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnEject.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnEject.CommandParameter = "OPEN";
            this.btnEject.Image = ((System.Drawing.Image)(resources.GetObject("btnEject.Image")));
            this.btnEject.Location = new System.Drawing.Point(3, 25);
            this.btnEject.Name = "btnEject";
            this.btnEject.Size = new System.Drawing.Size(28, 23);
            this.btnEject.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnEject.TabIndex = 2;
            this.btnEject.Tooltip = "打开";
            this.btnEject.Click += new System.EventHandler(this.btnEject_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            // 
            // 
            // 
            this.lblFileName.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblFileName.Location = new System.Drawing.Point(3, 6);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(56, 18);
            this.lblFileName.TabIndex = 0;
            this.lblFileName.Text = "文件名：";
            // 
            // tsslblFrame
            // 
            this.tsslblFrame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tsslblFrame.AutoSize = true;
            // 
            // 
            // 
            this.tsslblFrame.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tsslblFrame.Location = new System.Drawing.Point(189, 6);
            this.tsslblFrame.Name = "tsslblFrame";
            this.tsslblFrame.Size = new System.Drawing.Size(44, 18);
            this.tsslblFrame.TabIndex = 19;
            this.tsslblFrame.Text = "帧数：";
            // 
            // tsslblTime
            // 
            this.tsslblTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tsslblTime.AutoSize = true;
            // 
            // 
            // 
            this.tsslblTime.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tsslblTime.Location = new System.Drawing.Point(327, 6);
            this.tsslblTime.Name = "tsslblTime";
            this.tsslblTime.Size = new System.Drawing.Size(44, 18);
            this.tsslblTime.TabIndex = 20;
            this.tsslblTime.Text = "时间：";
            // 
            // tsslblSpeed
            // 
            this.tsslblSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tsslblSpeed.AutoSize = true;
            // 
            // 
            // 
            this.tsslblSpeed.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tsslblSpeed.Location = new System.Drawing.Point(491, 6);
            this.tsslblSpeed.Name = "tsslblSpeed";
            this.tsslblSpeed.Size = new System.Drawing.Size(44, 18);
            this.tsslblSpeed.TabIndex = 21;
            this.tsslblSpeed.Text = "速度：";
            // 
            // panelExTop
            // 
            this.panelExTop.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelExTop.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelExTop.Controls.Add(this.tsslblSpeed);
            this.panelExTop.Controls.Add(this.tsslblTime);
            this.panelExTop.Controls.Add(this.tsslblFrame);
            this.panelExTop.Controls.Add(this.lblFileName);
            this.panelExTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelExTop.Location = new System.Drawing.Point(0, 0);
            this.panelExTop.Name = "panelExTop";
            this.panelExTop.Size = new System.Drawing.Size(550, 29);
            this.panelExTop.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelExTop.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelExTop.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelExTop.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelExTop.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelExTop.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelExTop.Style.GradientAngle = 90;
            this.panelExTop.TabIndex = 5;
            // 
            // pVideo
            // 
            this.pVideo.CanvasColor = System.Drawing.SystemColors.Control;
            this.pVideo.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.pVideo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pVideo.Location = new System.Drawing.Point(0, 29);
            this.pVideo.Name = "pVideo";
            this.pVideo.Size = new System.Drawing.Size(550, 384);
            this.pVideo.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pVideo.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.pVideo.Style.BackColor2.Color = System.Drawing.Color.Gray;
            this.pVideo.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pVideo.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.pVideo.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pVideo.Style.GradientAngle = 90;
            this.pVideo.TabIndex = 6;
            // 
            // NHikPlayerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pVideo);
            this.Controls.Add(this.panelExTop);
            this.Controls.Add(this.panelControls);
            this.Controls.Add(this.expandableSplitter1);
            this.Controls.Add(this.expPanelList);
            this.Name = "NHikPlayerControl";
            this.Size = new System.Drawing.Size(850, 472);
            this.Load += new System.EventHandler(this.NHikPlayerControl_Load);
            this.expPanelList.ResumeLayout(false);
            this.expPanelList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.panelControls.ResumeLayout(false);
            this.panelExTop.ResumeLayout(false);
            this.panelExTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ExpandableSplitter expandableSplitter1;
        private DevComponents.DotNetBar.PanelEx panelControls;
        private DevComponents.DotNetBar.ButtonX btnPlayNormal;
        private DevComponents.DotNetBar.Controls.Slider cSliderVolume;
        private DevComponents.DotNetBar.ButtonX btnMute;
        private DevComponents.DotNetBar.ButtonX btnCapture;
        private DevComponents.DotNetBar.ButtonX btnFrame;
        private DevComponents.DotNetBar.ButtonX btnFast;
        private DevComponents.DotNetBar.ButtonX btnSlow;
        private DevComponents.DotNetBar.ButtonX btnStop;
        private DevComponents.DotNetBar.ButtonX btnPlay;
        private DevComponents.DotNetBar.ButtonX btnEject;
       // private ColorSlider colorSlider1;
        private System.Windows.Forms.Timer timer1;
        private ColorSlider cSliderTime;
        private DevComponents.DotNetBar.ExpandablePanel expPanelList;
        private DevComponents.DotNetBar.LabelX lblFileName;
        private DevComponents.DotNetBar.LabelX tsslblFrame;
        private DevComponents.DotNetBar.LabelX tsslblTime;
        private DevComponents.DotNetBar.LabelX tsslblSpeed;
        private DevComponents.DotNetBar.PanelEx panelExTop;
        private DevComponents.DotNetBar.PanelEx pVideo;
        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.ButtonItem btnFileListOpen;
        private DevComponents.DotNetBar.ButtonItem btnFileListClear;
        private DevComponents.DotNetBar.LabelItem labelItem1;
        private System.Windows.Forms.ImageList imageList1;
        private DevComponents.DotNetBar.ButtonItem btnFileListSearch;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtRecDir;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonItem btnRemoveFolder;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx combType;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
    }
}
