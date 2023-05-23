namespace GHIBMS.Matrix
{
    partial class Monitor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Monitor));
            this.panelExButton = new DevComponents.DotNetBar.PanelEx();
            this.btnSplit = new DevComponents.DotNetBar.ButtonX();
            this.btnSplit1 = new DevComponents.DotNetBar.ButtonItem();
            this.btnSplit4 = new DevComponents.DotNetBar.ButtonItem();
            this.btnSplit9 = new DevComponents.DotNetBar.ButtonItem();
            this.btnSplit16 = new DevComponents.DotNetBar.ButtonItem();
            this.lblMon = new DevComponents.DotNetBar.LabelX();
            this.panelExTop = new DevComponents.DotNetBar.PanelEx();
            this.panelExButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelExButton
            // 
            this.panelExButton.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelExButton.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelExButton.Controls.Add(this.btnSplit);
            this.panelExButton.Controls.Add(this.lblMon);
            this.panelExButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelExButton.Location = new System.Drawing.Point(0, 135);
            this.panelExButton.Name = "panelExButton";
            this.panelExButton.Size = new System.Drawing.Size(182, 22);
            this.panelExButton.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelExButton.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelExButton.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelExButton.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelExButton.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelExButton.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelExButton.Style.GradientAngle = 90;
            this.panelExButton.TabIndex = 0;
            // 
            // btnSplit
            // 
            this.btnSplit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSplit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSplit.AutoSize = true;
            this.btnSplit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSplit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSplit.FocusCuesEnabled = false;
            this.btnSplit.Image = ((System.Drawing.Image)(resources.GetObject("btnSplit.Image")));
            this.btnSplit.Location = new System.Drawing.Point(144, 0);
            this.btnSplit.Margin = new System.Windows.Forms.Padding(0);
            this.btnSplit.Name = "btnSplit";
            this.btnSplit.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btnSplit.Size = new System.Drawing.Size(38, 22);
            this.btnSplit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSplit.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnSplit1,
            this.btnSplit4,
            this.btnSplit9,
            this.btnSplit16});
            this.btnSplit.TabIndex = 1;
            // 
            // btnSplit1
            // 
            this.btnSplit1.GlobalItem = false;
            this.btnSplit1.Image = ((System.Drawing.Image)(resources.GetObject("btnSplit1.Image")));
            this.btnSplit1.Name = "btnSplit1";
            this.btnSplit1.Text = "1分割";
            this.btnSplit1.Click += new System.EventHandler(this.btnSplit1_Click);
            // 
            // btnSplit4
            // 
            this.btnSplit4.GlobalItem = false;
            this.btnSplit4.Image = ((System.Drawing.Image)(resources.GetObject("btnSplit4.Image")));
            this.btnSplit4.Name = "btnSplit4";
            this.btnSplit4.Text = "4分割";
            this.btnSplit4.Click += new System.EventHandler(this.btnSplit4_Click);
            // 
            // btnSplit9
            // 
            this.btnSplit9.GlobalItem = false;
            this.btnSplit9.Image = ((System.Drawing.Image)(resources.GetObject("btnSplit9.Image")));
            this.btnSplit9.Name = "btnSplit9";
            this.btnSplit9.Text = "9分割";
            this.btnSplit9.Click += new System.EventHandler(this.btnSplit9_Click);
            // 
            // btnSplit16
            // 
            this.btnSplit16.GlobalItem = false;
            this.btnSplit16.Image = ((System.Drawing.Image)(resources.GetObject("btnSplit16.Image")));
            this.btnSplit16.Name = "btnSplit16";
            this.btnSplit16.Text = "16分割";
            this.btnSplit16.Click += new System.EventHandler(this.btnSplit16_Click);
            // 
            // lblMon
            // 
            this.lblMon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMon.AutoSize = true;
            // 
            // 
            // 
            this.lblMon.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblMon.Location = new System.Drawing.Point(3, 3);
            this.lblMon.Name = "lblMon";
            this.lblMon.Size = new System.Drawing.Size(37, 16);
            this.lblMon.TabIndex = 0;
            this.lblMon.Text = "MON 1";
            // 
            // panelExTop
            // 
            this.panelExTop.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelExTop.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelExTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExTop.Location = new System.Drawing.Point(0, 0);
            this.panelExTop.Name = "panelExTop";
            this.panelExTop.Size = new System.Drawing.Size(182, 135);
            this.panelExTop.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelExTop.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelExTop.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelExTop.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelExTop.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelExTop.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelExTop.Style.GradientAngle = 90;
            this.panelExTop.StyleMouseDown.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panelExTop.StyleMouseDown.BackColor2.Color = System.Drawing.Color.Teal;
            this.panelExTop.TabIndex = 1;
            this.panelExTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelExTop_MouseDown);
            this.panelExTop.SizeChanged += new System.EventHandler(this.panelExTop_SizeChanged);
            // 
            // Monitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelExTop);
            this.Controls.Add(this.panelExButton);
            this.Name = "Monitor";
            this.Size = new System.Drawing.Size(182, 157);
            this.panelExButton.ResumeLayout(false);
            this.panelExButton.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelExButton;
        private DevComponents.DotNetBar.PanelEx panelExTop;
        private DevComponents.DotNetBar.LabelX lblMon;
        private DevComponents.DotNetBar.ButtonX btnSplit;
        private DevComponents.DotNetBar.ButtonItem btnSplit1;
        private DevComponents.DotNetBar.ButtonItem btnSplit4;
        private DevComponents.DotNetBar.ButtonItem btnSplit9;
        private DevComponents.DotNetBar.ButtonItem btnSplit16;
    }
}
