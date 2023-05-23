namespace GHIBMS.Server
{
    partial class FormIPMFinderOCX
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormIPMFinderOCX));
            this.NetFinder = new AxIPModuleLib.AxNetFinder();
            ((System.ComponentModel.ISupportInitialize)(this.NetFinder)).BeginInit();
            this.SuspendLayout();
            // 
            // NetFinder
            // 
            this.NetFinder.Enabled = true;
            this.NetFinder.Location = new System.Drawing.Point(37, 36);
            this.NetFinder.Name = "NetFinder";
            this.NetFinder.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("NetFinder.OcxState")));
            this.NetFinder.Size = new System.Drawing.Size(192, 192);
            this.NetFinder.TabIndex = 0;
            this.NetFinder.ConfigResult += new AxIPModuleLib._INetFinderEvents_ConfigResultEventHandler(this.NetFinder_ConfigResult);
            this.NetFinder.DeviceReport += new AxIPModuleLib._INetFinderEvents_DeviceReportEventHandler(this.NetFinder_DeviceReport);
            // 
            // FormIPMFinderOCX
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.NetFinder);
            this.Name = "FormIPMFinderOCX";
            this.Text = "FormIPMFinderOCX";
            ((System.ComponentModel.ISupportInitialize)(this.NetFinder)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxIPModuleLib.AxNetFinder NetFinder;
    }
}