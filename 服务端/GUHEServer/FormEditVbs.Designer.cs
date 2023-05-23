namespace GHIBMS.Server
{
    partial class FormEditVbs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEditVbs));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tspScriptMethod = new System.Windows.Forms.ToolStripSplitButton();
            this.tspSelVar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSeparator();
            this.tspCompile = new System.Windows.Forms.ToolStripButton();
            this.tspOK = new System.Windows.Forms.ToolStripButton();
            this.tspCancel = new System.Windows.Forms.ToolStripButton();
            this.textEditor1 = new ICSharpCode.TextEditor.TextEditorControl();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspScriptMethod,
            this.tspSelVar,
            this.toolStripSplitButton1,
            this.tspCompile,
            this.tspOK,
            this.tspCancel});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.toolStrip1.Size = new System.Drawing.Size(2736, 100);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tspScriptMethod
            // 
            this.tspScriptMethod.Image = ((System.Drawing.Image)(resources.GetObject("tspScriptMethod.Image")));
            this.tspScriptMethod.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspScriptMethod.Name = "tspScriptMethod";
            this.tspScriptMethod.Size = new System.Drawing.Size(121, 94);
            this.tspScriptMethod.Text = "方法";
            // 
            // tspSelVar
            // 
            this.tspSelVar.Image = ((System.Drawing.Image)(resources.GetObject("tspSelVar.Image")));
            this.tspSelVar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspSelVar.Name = "tspSelVar";
            this.tspSelVar.Size = new System.Drawing.Size(98, 94);
            this.tspSelVar.Text = "变量";
            this.tspSelVar.Click += new System.EventHandler(this.tspSelVar_Click);
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(6, 100);
            // 
            // tspCompile
            // 
            this.tspCompile.Image = global::GHIBMS.Server.Properties.Resources.build;
            this.tspCompile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspCompile.Name = "tspCompile";
            this.tspCompile.Size = new System.Drawing.Size(98, 94);
            this.tspCompile.Text = "编译";
            this.tspCompile.Click += new System.EventHandler(this.tspCompile_Click);
            // 
            // tspOK
            // 
            this.tspOK.Image = global::GHIBMS.Server.Properties.Resources.SAVE;
            this.tspOK.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspOK.Name = "tspOK";
            this.tspOK.Size = new System.Drawing.Size(98, 94);
            this.tspOK.Text = "保存";
            this.tspOK.Click += new System.EventHandler(this.tspOK_Click);
            // 
            // tspCancel
            // 
            this.tspCancel.Image = ((System.Drawing.Image)(resources.GetObject("tspCancel.Image")));
            this.tspCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspCancel.Name = "tspCancel";
            this.tspCancel.Size = new System.Drawing.Size(98, 94);
            this.tspCancel.Text = "取消";
            this.tspCancel.Click += new System.EventHandler(this.tspCancel_Click);
            // 
            // textEditor1
            // 
            this.textEditor1.AutoScroll = true;
            this.textEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textEditor1.Encoding = ((System.Text.Encoding)(resources.GetObject("textEditor1.Encoding")));
            this.textEditor1.Location = new System.Drawing.Point(0, 200);
            this.textEditor1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.textEditor1.Name = "textEditor1";
            this.textEditor1.ShowEOLMarkers = true;
            this.textEditor1.ShowSpaces = true;
            this.textEditor1.ShowTabs = true;
            this.textEditor1.ShowVRuler = true;
            this.textEditor1.Size = new System.Drawing.Size(2736, 1700);
            this.textEditor1.TabIndex = 2;
            // 
            // FormEditVbs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1368, 950);
            this.Controls.Add(this.textEditor1);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormEditVbs";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "脚本编辑";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormEditVbs_FormClosing);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tspCompile;
        private System.Windows.Forms.ToolStripButton tspOK;
        private System.Windows.Forms.ToolStripButton tspCancel;
        private ICSharpCode.TextEditor.TextEditorControl textEditor1;
        private System.Windows.Forms.ToolStripButton tspSelVar;
        private System.Windows.Forms.ToolStripSplitButton tspScriptMethod;
        private System.Windows.Forms.ToolStripSeparator toolStripSplitButton1;
    }
}