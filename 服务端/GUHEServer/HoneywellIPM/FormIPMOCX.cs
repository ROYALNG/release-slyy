using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using IPModuleLib;


namespace GHIBMS.Server
{
    public partial class FormIPMOCX : DevComponents.DotNetBar.Office2007Form
    {
        public FormIPMOCX(string chaName)
        {
            try
            {
                InitializeComponent();
                this.cooMonitor.Tag = chaName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #region 方法
        /// <summary>
        /// 启动服务器；服务器端口需要在Port 属性中指定
        /// </summary>
        /// <param name="Port"></param>
        /// <returns></returns>

        public int Startup(int Port)
        {
            cooMonitor.Port = Port;
            return cooMonitor.Startup();
        }
        /// <summary>
        /// 关闭服务器；
        /// </summary>
        /// <returns></returns>

        public void Shutdown()
        {
           cooMonitor.Shutdown();
        }
        /// <summary>
        ///向指定的设备发送键盘命令
        /// </summary>
        /// <param name="strMac"></param>
        /// <param name="key"></param>
        /// <returns></returns>

        public delegate void SendkeyDelegate(string strMac, KeyCode key);
        public void Sendkey(string strMac, KeyCode key)
        {
            try
            {
                this.BeginInvoke(new SendkeyDelegate(SendkeyCallback), new object[] { strMac, key });
            }
            catch
            {
              
            }
        }
        private void SendkeyCallback(string strMac, KeyCode key)
        {
             cooMonitor.Sendkey(strMac,key);
        }


        /// <summary>
        ///向指定的设备发送键盘命令序列； 
        /// </summary>
        /// <param name="strMac"></param>
        /// <param name="strKeys"></param>
        /// <returns></returns>
        public delegate void SendKeySequenceDelegate(string strMac, string strKeys);
        public void SendKeySequence(string strMac, string strKeys)
        {
            try
            {
                this.BeginInvoke(new SendKeySequenceDelegate(SendKeySequenceCallBack), new object[] { strMac, strKeys });
            }
            catch
            {

            }
        }
        public void SendKeySequenceCallBack(string strMac, string strKeys)
        {
             cooMonitor.SendKeySequence(strMac, strKeys);
        }
        /// <summary>
        /// 向指定的设备发送编程命令；
        /// </summary>
        /// <param name="strMac"></param>
        /// <param name="strCmdNum"></param>
        /// <param name="strData"></param>
        /// <returns></returns>

        public void SendProgramData(string strMac, string strCmdNum, string strData)
        {
             cooMonitor.SendProgramData(strMac, strCmdNum, strData);
        }
        /// <summary>
        /// 查询指定设备的应用程序版本号；
        /// </summary>
        /// <param name="strMac"></param>
        /// <returns></returns>

        public void QueryAppVersion(string strMac)
        {
             cooMonitor.QueryAppVersion(strMac);
        }
        /// <summary>
        /// 复位指定的设备
        /// </summary>
        /// <param name="strMac"></param>
        /// <param name="strPasswd"></param>
        /// <returns></returns>

        public void Reset(string strMac, string strPasswd)
        {
             cooMonitor.Reset(strMac, strPasswd);
        }
        /// <summary>
        /// 退出编程模式
        /// </summary>
        /// <param name="strMac"></param>
        /// <returns></returns>

        public void ExitProgram(string strMac)
        {
             cooMonitor.ExitProgram(strMac);
        }
        /// <summary>
        /// 修改指定设备的编号和键盘地址
        /// </summary>
        /// <param name="strMac"></param>
        /// <param name="lModID"></param>
        /// <param name="lKeyPad"></param>
        /// <returns></returns>

        public void ModifyModInfo(string strMac, int lModID, int lKeyPad)
        {
             cooMonitor.ModifyModInfo(strMac, lModID, lKeyPad);
        }

        /// <summary>
        /// 修改指定设备的IP
        /// </summary>
        /// <param name="strMac"></param>
        /// <param name="strPasswd"></param>
        /// <param name="strIP"></param>
        /// <param name="strSubnet"></param>
        /// <param name="strGateway"></param>
        /// <param name="strServerIP"></param>
        /// <param name="lPort"></param>
        /// <returns></returns>

        public void ModifyIP(string strMac, string strPasswd, string strIP, string strSubnet, string strGateway,
                              string strServerIP, int lPort)
        {
             cooMonitor.ModifyIP(strMac, strPasswd, strIP, strSubnet, strGateway, strServerIP, lPort);
        }
        public void ModifyPasswd(string strMac, string strOldPasswd, string strNewPasswd)
        {
             cooMonitor.ModifyPasswd(strMac, strOldPasswd, strNewPasswd);
        }
        /// <summary>
        /// 激发指定设备的状态；
        /// </summary>
        /// <param name="strMac"></param>
        /// <param name="zt"></param>
        /// <returns></returns>
        public int RaiseZoneStatus(string strMac,ZoneType zt)
        {
            return cooMonitor.RaiseZoneStatus(strMac, zt);
        }
        /// <summary>
        /// 使能Vista 主机键盘信息显示；
        /// </summary>
        /// <param name="strMac"></param>
        /// <param name="lEnable"></param>
        /// <returns></returns>

       public int  EnableKeypad(string strMac,int lEnable)
       {
           return cooMonitor.EnableKeypad(strMac, lEnable);
       }

        #endregion

        #region 所有事件处理
        //设备连接到服务器
        public delegate void DeviceConnected(object sender, AxIPModuleLib._ICooMonitorEvents_DeviceConnectedEvent e);
        public event DeviceConnected OnDeviceConnected;
        private void cooMonitor_DeviceConnected(object sender, AxIPModuleLib._ICooMonitorEvents_DeviceConnectedEvent e)
        {
            if (OnDeviceConnected!=null)
                OnDeviceConnected(sender, e);
        }
        //设备断开连接
        public delegate void DeviceDisConnected(object sender, AxIPModuleLib._ICooMonitorEvents_DeviceDisConnectedEvent e);
        public event DeviceDisConnected OnDeviceDisConnected;
        private void cooMonitor_DeviceDisConnected(object sender, AxIPModuleLib._ICooMonitorEvents_DeviceDisConnectedEvent e)
        {
            if (OnDeviceDisConnected!=null)
                OnDeviceDisConnected(sender, e);
        }
        //VISTA编程状态上报
        public delegate void VistaProgramReport(object sender, AxIPModuleLib._ICooMonitorEvents_VistaProgramReportEvent e);
        public event VistaProgramReport OnVistaProgramReport;
        private void cooMonitor_VistaProgramReport(object sender, AxIPModuleLib._ICooMonitorEvents_VistaProgramReportEvent e)
        {
            if  (OnVistaProgramReport!=null)
                 OnVistaProgramReport(sender, e);
        }
        //VISTA警情上报
        public delegate void VistaCIDReport(object sender, AxIPModuleLib._ICooMonitorEvents_VistaCIDReportEvent e);
        public event VistaCIDReport OnVistaCIDReport;
        private void cooMonitor_VistaCIDReport(object sender, AxIPModuleLib._ICooMonitorEvents_VistaCIDReportEvent e)
        {
            if (OnVistaCIDReport!=null)
                OnVistaCIDReport(sender, e);
        }
        //VISTA键盘信息显示上报
        public delegate void VistaKeypadInfo(object sender, AxIPModuleLib._ICooMonitorEvents_VistaKeypadInfoEvent e);
        public event VistaKeypadInfo OnVistaKeypadInfo; 
        private void cooMonitor_VistaKeypadInfo(object sender, AxIPModuleLib._ICooMonitorEvents_VistaKeypadInfoEvent e)
        {
            if (OnVistaKeypadInfo!=null)
               OnVistaKeypadInfo(sender, e);
        }
        //VISTA键盘使能结果
        public delegate void EnableKeypadResult(object sender, AxIPModuleLib._ICooMonitorEvents_EnableKeypadResultEvent e);
        public event EnableKeypadResult OnEnableKeypadResult;
        private void cooMonitor_EnableKeypadResult(object sender, AxIPModuleLib._ICooMonitorEvents_EnableKeypadResultEvent e)
        {
            if (OnEnableKeypadResult!=null)
                OnEnableKeypadResult(sender, e);
        }
        //VISTA LRR 地址查询结果
        public delegate void LRRQueryResult(object sender, AxIPModuleLib._ICooMonitorEvents_LRRQueryResultEvent e);
        public event LRRQueryResult OnLRRQueryResult;
        private void cooMonitor_LRRQueryResult(object sender, AxIPModuleLib._ICooMonitorEvents_LRRQueryResultEvent e)
        {
            if (OnLRRQueryResult != null)
               OnLRRQueryResult(sender, e);
        }
        //修改主机子类型
        public delegate void ModifyPTResult(object sender, AxIPModuleLib._ICooMonitorEvents_ModifyPTResultEvent e);
        public event ModifyPTResult OnModifyPTResult;
        private void cooMonitor_ModifyPTResult(object sender, AxIPModuleLib._ICooMonitorEvents_ModifyPTResultEvent e)
        {
            if (OnModifyPTResult!=null)
              OnModifyPTResult(sender, e);
        }

        //无法进入编程模式报告
        public delegate void ProgramReport(object sender, AxIPModuleLib._ICooMonitorEvents_ProgramReportEvent e);
        public event ProgramReport OnProgramReport;
        private void cooMonitor_ProgramReport(object sender, AxIPModuleLib._ICooMonitorEvents_ProgramReportEvent e)
        {
            if (OnProgramReport!=null)
                OnProgramReport(sender, e);
        }
        //挟持报告
        public delegate void DuressReport(object sender, AxIPModuleLib._ICooMonitorEvents_DuressReportEvent e);
        public event DuressReport OnDuressReport;
        private void cooMonitor_DuressReport(object sender, AxIPModuleLib._ICooMonitorEvents_DuressReportEvent e)
        {
            if (OnDuressReport!=null)
               OnDuressReport(sender, e);
        }

        //布撤防报告
        public delegate void ArmReport(object sender, AxIPModuleLib._ICooMonitorEvents_ArmReportEvent e);
        public event ArmReport OnArmReport;
        private void cooMonitor_ArmReport(object sender, AxIPModuleLib._ICooMonitorEvents_ArmReportEvent e)
        {
            if (OnArmReport!=null)
               OnArmReport(sender, e);   
        }
        //主机和IPM断开连接
        public delegate void PanelDisConnected(object sender, AxIPModuleLib._ICooMonitorEvents_PanelDisConnectedEvent e);
        public event PanelDisConnected OnPanelDisConnected;
        private void cooMonitor_PanelDisConnected(object sender, AxIPModuleLib._ICooMonitorEvents_PanelDisConnectedEvent e)
        {
            if (OnPanelDisConnected!=null)
               OnPanelDisConnected(sender, e);
        }
        //主机和IPM连接上
        public delegate void PanelConnected(object sender, AxIPModuleLib._ICooMonitorEvents_PanelConnectedEvent e);
        public event PanelConnected OnPanelConnected;
        private void cooMonitor_PanelConnected(object sender, AxIPModuleLib._ICooMonitorEvents_PanelConnectedEvent e)
        {
            if (OnPanelConnected!=null)
               OnPanelConnected(sender, e);
        }
        //确认用户按的特殊键已经被主机接收
        public delegate void VistaKeyResponse(object sender, AxIPModuleLib._ICooMonitorEvents_VistaKeyResponseEvent e);
        public event VistaKeyResponse OnVistaKeyResponse;
        private void cooMonitor_VistaKeyResponse(object sender, AxIPModuleLib._ICooMonitorEvents_VistaKeyResponseEvent e)
        {
            if (OnVistaKeyResponse!=null)
               OnVistaKeyResponse(sender, e);
        }
        //上报修改VISTA主机状态
        public delegate void VistaPanelStatus(object sender, AxIPModuleLib._ICooMonitorEvents_VistaPanelStatusEvent e);
        public event VistaPanelStatus OnVistaPanelStatus;
        private void cooMonitor_VistaPanelStatus(object sender, AxIPModuleLib._ICooMonitorEvents_VistaPanelStatusEvent e)
        {
            if (OnVistaPanelStatus!=null)
               OnVistaPanelStatus(sender, e);
            
        }

        //上报修改IPM设备信息结果
        public delegate void ModifyModInfoResult(object sender, AxIPModuleLib._ICooMonitorEvents_ModifyModInfoResultEvent e);
        public event ModifyModInfoResult OnModifyModInfoResult;
        private void cooMonitor_ModifyModInfoResult(object sender, AxIPModuleLib._ICooMonitorEvents_ModifyModInfoResultEvent e)
        {
            if (OnModifyModInfoResult!=null)
               OnModifyModInfoResult(sender, e);
        }
        //复位命令执行结果
        public delegate void ResetResult(object sender, AxIPModuleLib._ICooMonitorEvents_ResetResultEvent e);
        public event ResetResult OnResetResult;
        private void cooMonitor_ResetResult(object sender, AxIPModuleLib._ICooMonitorEvents_ResetResultEvent e)
        {
            if (OnResetResult != null) 
               OnResetResult(sender,e);
        }
        //上报版本号查询结果
        public delegate void AppVerQueryResult(object sender, AxIPModuleLib._ICooMonitorEvents_AppVerQueryResultEvent e);
        public event AppVerQueryResult OnAppVerQueryResultl;
        private void cooMonitor_AppVerQueryResult(object sender, AxIPModuleLib._ICooMonitorEvents_AppVerQueryResultEvent e)
        {
            if (OnAppVerQueryResultl!=null)
               OnAppVerQueryResultl(sender,e);
        }
        //上报编程数据
        public delegate void ProgramData(object sender, AxIPModuleLib._ICooMonitorEvents_ProgramDataEvent e);
        public event ProgramData programData;
        private void cooMonitor_ProgramData(object sender, AxIPModuleLib._ICooMonitorEvents_ProgramDataEvent e)
        {
            if (programData!=null)
               programData(sender,e);
        }
        //上报在键盘LCD上显示信息时触发此事件
        public delegate void PanelStatusEx(object sender, AxIPModuleLib._ICooMonitorEvents_PanelStatusExEvent e);
        public event PanelStatusEx OnPanelStatusEx;
        private void cooMonitor_PanelStatusEx(object sender, AxIPModuleLib._ICooMonitorEvents_PanelStatusExEvent e)
        {
            if (OnPanelStatusEx!=null)
                OnPanelStatusEx(sender,e);
        }
        //上报在键盘LCD上显示信息时触发此事件
        public delegate void PanelStatus(object sender, AxIPModuleLib._ICooMonitorEvents_PanelStatusEvent e);
        public event PanelStatus OnPanelStatus;
        private void cooMonitor_PanelStatus(object sender, AxIPModuleLib._ICooMonitorEvents_PanelStatusEvent e)
        {
            if (OnPanelStatus!=null)
                OnPanelStatus(sender,e);
        }
        //设备在键盘上显示信息
        public delegate void NewDisplayMsg(object sender, AxIPModuleLib._ICooMonitorEvents_NewDisplayMsgEvent e);
        public event NewDisplayMsg OnNewDisplayMsg;
        private void cooMonitor_NewDisplayMsg(object sender, AxIPModuleLib._ICooMonitorEvents_NewDisplayMsgEvent e)
        {
            if  ( OnNewDisplayMsg!=null)
              OnNewDisplayMsg(sender,e);
        }
        //更改设备密码结果
        public delegate void ModifyPasswdResult(object sender, AxIPModuleLib._ICooMonitorEvents_ModifyPasswdResultEvent e);
        public event ModifyPasswdResult OnModifyPasswdResult;
        private void cooMonitor_ModifyPasswdResult(object sender, AxIPModuleLib._ICooMonitorEvents_ModifyPasswdResultEvent e)
        {
            if (OnModifyPasswdResult!=null)
               OnModifyPasswdResult(sender,e);
        }
        //上报修改IPM 设备信息的结果
        public delegate void ModInfoQueryResult(object sender, AxIPModuleLib._ICooMonitorEvents_ModInfoQueryResultEvent e);
        public event ModInfoQueryResult OnModInfoQueryResult;
        private void cooMonitor_ModInfoQueryResult(object sender, AxIPModuleLib._ICooMonitorEvents_ModInfoQueryResultEvent e)
        {
            if (OnModInfoQueryResult!=null)
                OnModInfoQueryResult(sender,e);
        }
        //查询设备Boot Loader 版本号
        public delegate void BLVerQueryResult(object sender, AxIPModuleLib._ICooMonitorEvents_BLVerQueryResultEvent e);
        public event BLVerQueryResult OnBLVerQueryResult;
        private void cooMonitor_BLVerQueryResult(object sender, AxIPModuleLib._ICooMonitorEvents_BLVerQueryResultEvent e)
        {
            if (OnBLVerQueryResult!=null)
                OnBLVerQueryResult(sender,e);
        }
        //上报新的报警事件
        public delegate void NewAlarm(object sender, AxIPModuleLib._ICooMonitorEvents_NewAlarmEvent e);
        public event NewAlarm OnNewAlarm;
        private void cooMonitor_NewAlarm(object sender, AxIPModuleLib._ICooMonitorEvents_NewAlarmEvent e)
        {
            if (OnNewAlarm!=null)
                OnNewAlarm(sender,e);
        }
        //上报新的Trouble事件
        public delegate void NewTrouble(object sender, AxIPModuleLib._ICooMonitorEvents_NewTroubleEvent e);
        public event NewTrouble OnNewTrouble;
        private void cooMonitor_NewTrouble(object sender, AxIPModuleLib._ICooMonitorEvents_NewTroubleEvent e)
        {
            if (OnNewTrouble!=null)
                OnNewTrouble(sender,e);
        }
        //上报新的RPS布撤防事件
        public delegate void RPSReport(object sender, AxIPModuleLib._ICooMonitorEvents_RPSReportEvent e);
        public event RPSReport OnRPSReport;
        private void cooMonitor_RPSReport(object sender, AxIPModuleLib._ICooMonitorEvents_RPSReportEvent e)
        {
            if (OnRPSReport!=null)
               OnRPSReport(sender,e);
        }
        //上报新的软防区报警事件
        public delegate void  SoftZoneAlarm(object sender, AxIPModuleLib._ICooMonitorEvents_SoftZoneAlarmEvent e);
        public event SoftZoneAlarm OnSoftZoneAlarm;
        private void cooMonitor_SoftZoneAlarm(object sender, AxIPModuleLib._ICooMonitorEvents_SoftZoneAlarmEvent e)
        {
            if (OnSoftZoneAlarm!=null)
               OnSoftZoneAlarm(sender,e);
        }
        //防区恢复
        public delegate void FaultRestore(object sender, AxIPModuleLib._ICooMonitorEvents_FaultRestoreEvent e);
        public event FaultRestore OnFaultRestore;
        private void cooMonitor_FaultRestore(object sender, AxIPModuleLib._ICooMonitorEvents_FaultRestoreEvent e)
        {
            if (OnFaultRestore!=null)
               OnFaultRestore(sender,e);
        }
        #endregion

        private void cooMonitor_VistaDummy(object sender, AxIPModuleLib._ICooMonitorEvents_VistaDummyEvent e)
        {

        }

        private void cooMonitor_ModifyIPResult(object sender, AxIPModuleLib._ICooMonitorEvents_ModifyIPResultEvent e)
        {

        }
    }
}
