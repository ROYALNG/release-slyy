namespace HoneywellIPM
{
    partial class FormIPMOCX
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormIPMOCX));
            this.cooMonitor = new AxIPModuleLib.AxCooMonitor();
            ((System.ComponentModel.ISupportInitialize)(this.cooMonitor)).BeginInit();
            this.SuspendLayout();
            // 
            // cooMonitor
            // 
            this.cooMonitor.Enabled = true;
            this.cooMonitor.Location = new System.Drawing.Point(72, 36);
            this.cooMonitor.Name = "cooMonitor";
            this.cooMonitor.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("cooMonitor.OcxState")));
            this.cooMonitor.Size = new System.Drawing.Size(192, 192);
            this.cooMonitor.TabIndex = 27;
            this.cooMonitor.VistaProgramReport += new AxIPModuleLib._ICooMonitorEvents_VistaProgramReportEventHandler(this.cooMonitor_VistaProgramReport);
            this.cooMonitor.RPSReport += new AxIPModuleLib._ICooMonitorEvents_RPSReportEventHandler(this.cooMonitor_RPSReport);
            this.cooMonitor.BLVerQueryResult += new AxIPModuleLib._ICooMonitorEvents_BLVerQueryResultEventHandler(this.cooMonitor_BLVerQueryResult);
            this.cooMonitor.ModifyIPResult += new AxIPModuleLib._ICooMonitorEvents_ModifyIPResultEventHandler(this.cooMonitor_ModifyIPResult);
            this.cooMonitor.PanelDisConnected += new AxIPModuleLib._ICooMonitorEvents_PanelDisConnectedEventHandler(this.cooMonitor_PanelDisConnected);
            this.cooMonitor.AppVerQueryResult += new AxIPModuleLib._ICooMonitorEvents_AppVerQueryResultEventHandler(this.cooMonitor_AppVerQueryResult);
            this.cooMonitor.ModifyModInfoResult += new AxIPModuleLib._ICooMonitorEvents_ModifyModInfoResultEventHandler(this.cooMonitor_ModifyModInfoResult);
            this.cooMonitor.ArmReport += new AxIPModuleLib._ICooMonitorEvents_ArmReportEventHandler(this.cooMonitor_ArmReport);
            this.cooMonitor.SoftZoneAlarm += new AxIPModuleLib._ICooMonitorEvents_SoftZoneAlarmEventHandler(this.cooMonitor_SoftZoneAlarm);
            this.cooMonitor.PanelStatus += new AxIPModuleLib._ICooMonitorEvents_PanelStatusEventHandler(this.cooMonitor_PanelStatus);
            this.cooMonitor.VistaDummy += new AxIPModuleLib._ICooMonitorEvents_VistaDummyEventHandler(this.cooMonitor_VistaDummy);
            this.cooMonitor.NewAlarm += new AxIPModuleLib._ICooMonitorEvents_NewAlarmEventHandler(this.cooMonitor_NewAlarm);
            this.cooMonitor.LRRQueryResult += new AxIPModuleLib._ICooMonitorEvents_LRRQueryResultEventHandler(this.cooMonitor_LRRQueryResult);
            this.cooMonitor.VistaCIDReport += new AxIPModuleLib._ICooMonitorEvents_VistaCIDReportEventHandler(this.cooMonitor_VistaCIDReport);
            this.cooMonitor.ProgramReport += new AxIPModuleLib._ICooMonitorEvents_ProgramReportEventHandler(this.cooMonitor_ProgramReport);
            this.cooMonitor.NewTrouble += new AxIPModuleLib._ICooMonitorEvents_NewTroubleEventHandler(this.cooMonitor_NewTrouble);
            this.cooMonitor.DuressReport += new AxIPModuleLib._ICooMonitorEvents_DuressReportEventHandler(this.cooMonitor_DuressReport);
            this.cooMonitor.ModifyPasswdResult += new AxIPModuleLib._ICooMonitorEvents_ModifyPasswdResultEventHandler(this.cooMonitor_ModifyPasswdResult);
            this.cooMonitor.VistaPanelStatus += new AxIPModuleLib._ICooMonitorEvents_VistaPanelStatusEventHandler(this.cooMonitor_VistaPanelStatus);
            this.cooMonitor.DeviceDisConnected += new AxIPModuleLib._ICooMonitorEvents_DeviceDisConnectedEventHandler(this.cooMonitor_DeviceDisConnected);
            this.cooMonitor.DeviceConnected += new AxIPModuleLib._ICooMonitorEvents_DeviceConnectedEventHandler(this.cooMonitor_DeviceConnected);
            this.cooMonitor.ModInfoQueryResult += new AxIPModuleLib._ICooMonitorEvents_ModInfoQueryResultEventHandler(this.cooMonitor_ModInfoQueryResult);
            this.cooMonitor.ProgramData += new AxIPModuleLib._ICooMonitorEvents_ProgramDataEventHandler(this.cooMonitor_ProgramData);
            this.cooMonitor.EnableKeypadResult += new AxIPModuleLib._ICooMonitorEvents_EnableKeypadResultEventHandler(this.cooMonitor_EnableKeypadResult);
            this.cooMonitor.NewDisplayMsg += new AxIPModuleLib._ICooMonitorEvents_NewDisplayMsgEventHandler(this.cooMonitor_NewDisplayMsg);
            this.cooMonitor.VistaKeyResponse += new AxIPModuleLib._ICooMonitorEvents_VistaKeyResponseEventHandler(this.cooMonitor_VistaKeyResponse);
            this.cooMonitor.VistaKeypadInfo += new AxIPModuleLib._ICooMonitorEvents_VistaKeypadInfoEventHandler(this.cooMonitor_VistaKeypadInfo);
            this.cooMonitor.PanelConnected += new AxIPModuleLib._ICooMonitorEvents_PanelConnectedEventHandler(this.cooMonitor_PanelConnected);
            this.cooMonitor.PanelStatusEx += new AxIPModuleLib._ICooMonitorEvents_PanelStatusExEventHandler(this.cooMonitor_PanelStatusEx);
            this.cooMonitor.ModifyPTResult += new AxIPModuleLib._ICooMonitorEvents_ModifyPTResultEventHandler(this.cooMonitor_ModifyPTResult);
            // 
            // FormIPMOCX
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 250);
            this.Controls.Add(this.cooMonitor);
            this.Name = "FormIPMOCX";
            this.Text = "FormIPMOCX";
            ((System.ComponentModel.ISupportInitialize)(this.cooMonitor)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxIPModuleLib.AxCooMonitor cooMonitor;
    }
}