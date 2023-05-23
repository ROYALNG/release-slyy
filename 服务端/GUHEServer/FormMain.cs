using GHIBMS.Common;
using GHIBMS.Common.BaseClass;
using GHIBMS.Common.Basic;
using GHIBMS.Interface;
using GHIBMS.Pub;
using GHIBMS.Server.Models;
using GHIBMS.Server.Pub;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SourceGrid;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace GHIBMS.Server
{

    public partial class FormMain : Form
    {

        #region 常量
        //public static System.Diagnostics.PerformanceCounter CpuPerformanceCounter = new System.Diagnostics.PerformanceCounter("Processor", "% Processor Time", "_Total");
        //private PerformanceCounter MemoPerformanceCounter = new PerformanceCounter("Memory", "Available MBytes");
        //设备驱动监控    毫秒
        private const int DRIVE_MONITOR_INTERVAL = 1000 * 30;
        private const int MAX_TRIAL_DAY = 90;
        private const int MAX_LOGLIST_COUNT = 1000;
        private const int MAX_ALARM_LIST_COUNT = 5000;
        #endregion

        #region 私有全局变量
        private const string SOFTNAME = "GHIBMS";

        private StringBuilder DevicePath;
        private bool LOG_MSG = false;//记录程序运行日志
                                     //private bool LOG_ERR = true;  //记录查询错误日志

        private string currentPrejectPath = "";


        // private static GUHETcpService netService = null;
       

        //private static IISWebServer.IISWebServer IISWebServer_;

        //private static bool RunningState = false;
        private static bool FormIsShow = true;


        //private static List<ListViewItem>  LogCacheList = new List<ListViewItem>();

        private static object syncLockChan = new object();
        private static object syncLockFrm = new object();
        private static object syncLockAlm = new object();

        private DateTime LastStartTime;
        private DateTime LastStopTime;

        private bool bSaveVariableEdit = false;
        private bool bExistProject = false;


        //自定义对象剪贴板
        private static List<ListViewItem> LisViewItemClipboard = new List<ListViewItem>();
        private static bool bCut = false;
        //private  SourceGrid.Cells.Editors.ComboBox comboStandard; //报警条件选择类
        //public SoftKey ytsoftkey;//引用由域天工具随机生成的加密类模块
        public static string MainArgs = "";
        MRUManager mruManager;
        //public static FormWindowState frmState = FormWindowState.Maximized;
        // private List<FBDForm> g_controlFormlist = new List<FBDForm>();


        public static int g_KeyCapacity = 16;

        #endregion

        #region SourceGrid控制器

        //Setup the controllers
        //private static CellClickEvent clickController = new CellClickEvent();
        //private static PopupMenu menuController = new PopupMenu();
        //private static CellCursor cursorController = new CellCursor();
        private ControllerValueChanged valueChangedController;
        private SourceGrid.Cells.Controllers.ToolTipText toolTipController;

        #endregion

        public WaitForm frmWait = null;
        public static bool g_bConnDbOK = false;
        public const int USER = 0x0400;//用户自定义消息的开始数值
                                       //[DllImport("User32.dll", EntryPoint = "SendMessage")]
                                       //private static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
                                       //[DllImport("user32.dll")]
                                       //public static extern void PostMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        /* 
        protected override void DefWndProc(ref System.Windows.Forms.Message m)
        {
            switch (m.Msg)
            {
                case USER+1:
                    //string message = string.Format("收到自己消息的参数:{0},{1}", m.WParam, m.LParam);
                   // MessageBox.Show(message);
                    PostMessage(m.WParam, m.Msg, (int)m.LParam, (int)m.LParam);
                    break;
                default:
                    base.DefWndProc(ref m);//一定要调用基类函数，以便系统处理其它消息。
                    break;
            }
        }
        */

        public static ToolStrip toolStrip = null;
        private DriverMng _DriverMng;
        protected override void WndProc(ref Message m)

        {

            if (m.Msg == 0x0014) // 禁掉清除背景消息WM_ERASEBKGND

                return;

            base.WndProc(ref m);

        }
        #region 构造函数
        static FormMain()
        {
            ServerConfig.loadFromFile();
        }
        public FormMain()
        {
            try
            {
                Logger.GetInstance().LogMsg("IOServer启动");
                SetStyle(
                        ControlStyles.OptimizedDoubleBuffer,
                        true);

                #region 只运行一个实例
                pubFun.HandleRunningInstance(false);

                #endregion


                // FormStart frmStart= new FormStart();
                //frmStart.Show();
                // Thread.Sleep(1000);
                try
                {
                    InitializeComponent();
                }
                catch { }
                //this.toolStrip1.Items["tspDowloadFromCloud"].Visible = ServerConfig.CompatibleWithV1;
                toolStrip = this.toolStrip1;

                this.ListViewDevice.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ListViewDeviceMouseDown);
                this.ListViewDevice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListViewDeviceKeyDown);
                this.ListViewDevice.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ListViewDeviceMouseClick);

                this.trvDevice.MouseDown += new System.Windows.Forms.MouseEventHandler(this.trvDeviceMngMouseDown);
                this.trvDevice.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvDeviceMngNodeMouseClick);

                this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);


                //lblProjectInfo.Text = string.Format("{0} {1}", Project.ProjectName, Project.ProjectEdition);

                //ServerConfig.APP_PATH = Application.StartupPath + "\\";
                ServerConfig.APP_PATH = AppDomain.CurrentDomain.BaseDirectory;
                ServerConfig.loadFromFile();
                GetLicence();
                LOG_MSG = ServerConfig.WriteLog;

                /*
                //分控服务
                netService = new GUHETcpService();
                netService.Port = ServerConfig.DataPort;
                netService.OnActive += new GUHETcpService.activeDelegate(dataServer_OnActive);
                netService.OnDeactive += new GUHETcpService.deactiveDelegate(dataServer_OnDeactive);
                netService.OnConnect += new GUHETcpService.conncetDelegate(dataServer_OnConnect);
                netService.OnDisconnect += new GUHETcpService.disconncetDelegate(dataServer_OnDisconnect);
                netService.OnCommMsg += new CommMsgDelegate(ShowCommMsg);

                IISWebServer_ = new IISWebServer.IISWebServer();
                //设定HTTP服务的端口号
                IISWebServer_.Port = ServerConfig.webDataPort;
                //设定HTTP服务的虚拟目录
                IISWebServer_.MyWebServerRoot = ServerConfig.WebPath;
                //注册HTTP请求事件
                IISWebServer_.IISAS += new GHIBMS.IISWebServer.IISAcceptSocket(IISWebServer_IISAS);
                //注册HTTP服务的启动事件
                IISWebServer_.OnStart += new GHIBMS.IISWebServer.IISWebServer.StartDelegate(IISWebServer__OnStart);
                //注册HTTP服务的停止事件
                IISWebServer_.OnStop += new GHIBMS.IISWebServer.IISWebServer.StopDelegate(IISWebServer__OnStop);
                */

                //SourceGrid控制器
                valueChangedController = new ControllerValueChanged();
                valueChangedController.OnValueChangedEvent += new ControllerValueChanged.OnValueChangedDelegate(valueChangedController_OnValueChangedEvent);
                toolTipController = new SourceGrid.Cells.Controllers.ToolTipText();

                LastStartTime = Convert.ToDateTime("2000-01-01");
                LastStopTime = Convert.ToDateTime("2000-01-01");



                //加载通讯插件
                PluginMng.PlugLoad();
                //frmStart.Close();
                //this.WindowState = FormWindowState.Normal;
                //this.DesktopBounds = Screen.AllScreens[0].Bounds;
                //this.WindowState = FormWindowState.Maximized;

                trvDevice.HideSelection = false;
                //自已绘制  
                //this.trvDevice.DrawMode = TreeViewDrawMode.OwnerDrawText;
                //this.trvDevice.DrawNode += new DrawTreeNodeEventHandler(trvDevice_DrawNode);  
                ListViewDevice.FullRowSelect = true;
                ListViewDevice.HideSelection = false;
                mruManager = new MRUManager(MruMenuItemStar, MruMenuItemEnd, Project.ProductName_Server);
                mruManager.ItemClicked += new MRUManager.ItemClickedDelegate(mruManager_ItemClicked);

                _DriverMng = new DriverMng();
                _DriverMng.OnAddOperationLog += AddOperationLog;
                _DriverMng.OnUpdateTree += UpdateTree;



            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }
        }

        //在绘制节点事件中，按自已想的绘制  
        private void trvDevice_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            e.DrawDefault = true; //我这里用默认颜色即可，只需要在TreeView失去焦点时选中节点仍然突显  
            return;

            if ((e.State & TreeNodeStates.Selected) != 0)
            {
                //演示为绿底白字  
                e.Graphics.FillRectangle(Brushes.DarkBlue, e.Node.Bounds);

                Font nodeFont = e.Node.NodeFont;
                if (nodeFont == null) nodeFont = ((TreeView)sender).Font;
                e.Graphics.DrawString(e.Node.Text, nodeFont, Brushes.White, Rectangle.Inflate(e.Bounds, 2, 0));
            }
            else
            {
                e.DrawDefault = true;
            }

            if ((e.State & TreeNodeStates.Focused) != 0)
            {
                using (Pen focusPen = new Pen(Color.Black))
                {
                    focusPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    Rectangle focusBounds = e.Node.Bounds;
                    focusBounds.Size = new Size(focusBounds.Width - 1,
                    focusBounds.Height - 1);
                    e.Graphics.DrawRectangle(focusPen, focusBounds);
                }
            }

        }

        private string waitingmsg = "正在测试数据库连接中，请耐心等待……";
        private delegate void ShowProgressDelegete();
        private void ShowProgress()
        {
            try
            {
                frmWait = new WaitForm();
                frmWait.SetText(waitingmsg);
                frmWait.ShowDialog();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        private delegate void CloseWaitingFormdelegate();
        private void CloseWaitingForm()
        {
            if (frmWait != null)
            {
                if (frmWait.InvokeRequired)
                {
                    frmWait.Invoke(new CloseWaitingFormdelegate(CloseWaitingForm));
                }
                else
                {
                    frmWait.Dispose();
                    frmWait = null;
                }
            }
        }
        #endregion

        #region 加密锁事件

        //void RegisterDeviceNotification()
        //{
        //    Win32.DEV_BROADCAST_DEVICEINTERFACE dbi = new
        //        Win32.DEV_BROADCAST_DEVICEINTERFACE();
        //    int size = Marshal.SizeOf(dbi);
        //    dbi.dbcc_size = size;
        //    dbi.dbcc_devicetype = Win32.DBT_DEVTYP_DEVICEINTERFACE;
        //    dbi.dbcc_reserved = 0;
        //    dbi.dbcc_classguid = Win32.GUID_DEVINTERFACE_USB_DEVICE;
        //    dbi.dbcc_name = 0;
        //    IntPtr buffer = Marshal.AllocHGlobal(size);
        //    Marshal.StructureToPtr(dbi, buffer, true);
        //    IntPtr r = Win32.RegisterDeviceNotification(Handle, buffer,
        //        Win32.DEVICE_NOTIFY_WINDOW_HANDLE);
        //    if (r == IntPtr.Zero)
        //        MessageBox.Show(Win32.GetLastError().ToString());
        //}
        //protected override void WndProc(ref Message m)
        //{
        //    switch (m.Msg)
        //    {
        //        case Win32.WM_DEVICECHANGE: OnDeviceChange(ref m); break;
        //    }
        //    base.WndProc(ref m);
        //}
        /*     void OnDeviceChange(ref Message msg)
             {
                 try
                 {
                     int wParam = (int)msg.WParam;
                     if (wParam == Win32.DBT_DEVICEARRIVAL)
                     {
                         int devType = Marshal.ReadInt32(msg.LParam, 4);
                         if (devType == Win32.DBT_DEVTYP_DEVICEINTERFACE)
                         {
                             Win32.DEV_BROADCAST_DEVICEINTERFACE1 DeviceInfo = (Win32.DEV_BROADCAST_DEVICEINTERFACE1)Marshal.PtrToStructure(msg.LParam, typeof(Win32.DEV_BROADCAST_DEVICEINTERFACE1));
                             // 如果需要知道是否我们的设备插入或拨出，可以查看这个结构中的dbcc_name
                             //MessageBox.Show(" 加密锁被插入。");
                             User.CheckKey = false;// ytsoftkey.YCheckKey_1();


                             if (User.CheckKey)
                             {
                                 notifyIcon1.BalloonTipText = "检测到软件锁，软件正常运行！";
                                 notifyIcon1.ShowBalloonTip(2);

                             }
                         }
                     }
                     if (wParam == Win32.DBT_DEVICEREMOVECOMPLETE)
                     {
                         Win32.DEV_BROADCAST_DEVICEINTERFACE1 DeviceInfo = (Win32.DEV_BROADCAST_DEVICEINTERFACE1)Marshal.PtrToStructure(msg.LParam, typeof(Win32.DEV_BROADCAST_DEVICEINTERFACE1));
                         // 如果需要知道是否我们的设备插入或拨出，可以查看这个结构中的dbcc_name
                         //MessageBox.Show(" 加密锁被拨出。");
                         User.CheckKey = false;// ytsoftkey.YCheckKey_1();
                           RSAHelper rsa = new RSAHelper();

                           if (!User.CheckKey && !User.SoftCode)
                         {
                             RunTimeSun = 0;
                             uint Expires = (uint)3600 * 4 - RunTimeSun;
                             notifyIcon1.BalloonTipText = "未注册服务端,软件将在" + pubFun.parseTimeSeconds((int)Expires, 1) + "后自动关闭！";
                             notifyIcon1.ShowBalloonTip(2);
                         }
                     }
                 }
                 catch (Exception ex)
                 {
                     Logger.GetInstance().LogError(ex.ToString());
                 }
             }
     */
        #endregion

        #region 设备树列表
        #region Edit TreeView

        //private void btnAddRoot_Click(object sender, EventArgs e)
        //{

        //    TreeNode node = CreateNewNode();

        //    this.treeView1.Nodes.Add(node);
        //}

        private TreeNode CreateNewNode(string nodeText)
        {
            TreeNode node = new TreeNode(nodeText.Trim());
            node.Tag = nodeText.Trim().Clone();
            return node;
        }

        //private void btnAddChild_Click(object sender, EventArgs e)
        //{
        //    if (this.treeView1.SelectedNode == null)
        //    {
        //        return;
        //    }
        //    TreeNode node = CreateNewNode();

        //    this.treeView1.SelectedNode.Nodes.Add(node);
        //    this.treeView1.SelectedNode.Expand();
        //}
        private void btnDelSel_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region TreeView 2 XML

        private void SavePrj()
        {

            try
            {

                //将TreeView保存到XML文件中
                if (currentPrejectPath == "" || !File.Exists(currentPrejectPath))
                {
                    //将TreeView保存到XML文件中
                    dlgSave.Filter = "数据文件 (*.svr)|*.svr";
                    dlgSave.FilterIndex = 0;
                    dlgSave.RestoreDirectory = true;
                    if (!Directory.Exists(Application.StartupPath + "\\工程项目"))
                        Directory.CreateDirectory(Application.StartupPath + "\\工程项目");

                    dlgSave.InitialDirectory = Application.StartupPath + "\\工程项目";

                    dlgSave.Title = "保存文件";
                    if (dlgSave.ShowDialog() == DialogResult.OK)
                    {
                        currentPrejectPath = dlgSave.FileName;
                        mruManager.Add(currentPrejectPath, Project.ProductName_Server);

                    }
                }
                if (currentPrejectPath.Trim() == "")
                {
                    return;
                }
                waitingmsg = "正在配置文件正在写入磁盘，请耐心等待……";
                Thread th = new Thread(new ThreadStart(this.ShowProgress));
                th.IsBackground = true;
                th.Name = "ShowProgressThread";
                th.Start();
                //确保frmWait显示
                while (frmWait == null)
                {
                    Thread.Sleep(1);
                }
                while (!frmWait.IsShowing)
                {
                    Thread.Sleep(1);
                }
                ProjectMng.SaveToXml(currentPrejectPath);


                this.Text = "智能建筑集成管理系统-IO采集服务 [" + currentPrejectPath + "]";
                ServerConfig.ProjectPath = currentPrejectPath;
                ServerConfig.saveToFile();

                CloseWaitingForm();

            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
                CloseWaitingForm();
            }

        }

        private void SaveAsPrj()
        {
            try
            {
                //将TreeView保存到XML文件中
                // if (currentFile == "")
                // {
                //将TreeView保存到XML文件中
                dlgSave.Filter = "数据文件(*.svr)|*.svr|All files (*.*)|*.*";
                dlgSave.FilterIndex = 0;
                dlgSave.RestoreDirectory = true;
                dlgSave.Title = "保存文件";
                if (dlgSave.ShowDialog() == DialogResult.OK)
                {
                    currentPrejectPath = dlgSave.FileName;

                }
                // }
                if (currentPrejectPath.Trim() == "")
                {
                    //MessageBox.Show("文件名输入不正确，文件不能正常保存！");
                    MessageBox.Show("文件名输入不正确，文件不能正常保存！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                    return;
                }
                /*
                XmlDocument doc = new XmlDocument();
                doc.LoadXml("<SOPCprj></SOPCprj>");
                XmlNode root = doc.DocumentElement;
                doc.InsertBefore(doc.CreateXmlDeclaration("1.0", "utf-8", "yes"), root);
                XmlNode Devices = doc.CreateNode("element", "Devices", "");
                root.AppendChild(Devices);
                TreeNode2Xml(this.trvDevice.Nodes, Devices);

                XmlNode Forms = doc.CreateNode("element", "Forms", "");
                root.AppendChild(Forms);
                TreeNode2Xml(this.trvForm.Nodes, Forms);
                doc.Save(currentFile);
                 * */
                /*ProjectMng.SaveToXml(currentPrejectPath);
                this.Text = "智能建筑集成管理系统-IO采集服务[" + currentPrejectPath + "]";
                ServerConfig.ProjectPath = currentPrejectPath;
                ServerConfig.saveToFile();*/
                SavePrj();
            }
            catch
            {
                //Log("保存数据文件出错！数据文件：" + currentFile);

                string msg = string.Format("保存数据文件出错！数据文件：{0}", currentPrejectPath);
                AddOperationLog(StrConst.SERVERITY_ERR, StrConst.TITLE_SYS, "", msg);


            }

        }

        #endregion

        #region XML 2 TreeView
        private void LoadPrj(string filename)
        {
            ServerConfig.RunningState = ServerStateEnum.BUSYING;
            if (!File.Exists(filename))
            {
                //MessageBox.Show("文件不存在！"+FileName);
                MessageBox.Show("文件不存在！" + filename, "文件", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                ServerConfig.RunningState = ServerStateEnum.STOPED;
                return;
            }
            waitingmsg = "正在从磁盘载入配置文件，请耐心等待……";
            Thread th = new Thread(new ThreadStart(this.ShowProgress));
            th.IsBackground = true;
            th.Name = "ShowProgressThread";
            th.Start();
            //确保frmWait显示
            while (frmWait == null)
            {
                Thread.Sleep(1);
            }
            while (!frmWait.IsShowing)
            {
                Thread.Sleep(1);
            }
            try
            {
                CurrentAllParmClear();
                if (ProjectMng.LoadFromXml(filename))
                {
                    currentPrejectPath = filename;
                    this.Text = "数据采集服务器-IOSERVER[" + currentPrejectPath + "]";

                    ChanList2TreeView();
                    SetOpenState();
                    //if (ServerConfig.DataBaseEnable&& g_bConnDbOK)
                    //{
                    //    ProjectMng.ResoreFormList(Rtdb.DesignFormList);
                    //}
                    ShowMainTabPage(MainTabPage.Log);
                    bExistProject = true;
                    mruManager.Add(filename, Project.ProductName_Server);
                    AddOperationLog(Severity.信息.ToString(), StrConst.TITLE_SYS, "", "载入文件成功:" + filename);
                    //Thread.Sleep(5000);
                }
                else
                {
                    MessageBox.Show("加载数据文件失败，请检查是否注册对应插件", "文件加载", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);

                    currentPrejectPath = "";
                    this.Text = "数据采集服务器-IOSERVER";
                    AddOperationLog(Severity.信息.ToString(), StrConst.TITLE_SYS, "", "载入文件失败：" + filename);
                }

            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
                string msg = string.Format("加载数据文件出错！数据文件：{0}", currentPrejectPath);
                AddOperationLog(StrConst.SERVERITY_ERR, StrConst.TITLE_SYS, "", msg);
            }

            CloseWaitingForm();
            ServerConfig.RunningState = ServerStateEnum.STOPED;
        }

        private void LoadPrj_V2(string filename, string url)
        {

            waitingmsg = "正在从磁盘载入配置文件，请耐心等待……";
            Thread th = new Thread(new ThreadStart(this.ShowProgress));
            th.IsBackground = true;
            th.Name = "ShowProgressThread";
            th.Start();
            //确保frmWait显示
            while (frmWait == null)
            {
                Thread.Sleep(1);
            }
            while (!frmWait.IsShowing)
            {
                Thread.Sleep(1);
            }
            try
            {
                CurrentAllParmClear();
                if (ProjectMng.LoadFromXml(url))
                {
                    currentPrejectPath = filename;
                    this.Text = "数据采集服务器-IOSERVER[" + currentPrejectPath + "]";

                    ChanList2TreeView();
                    SetOpenState();
                    //if (ServerConfig.DataBaseEnable&& g_bConnDbOK)
                    //{
                    //    ProjectMng.ResoreFormList(Rtdb.DesignFormList);
                    //}
                    ShowMainTabPage(MainTabPage.Log);
                    bExistProject = true;
                    mruManager.Add(filename, Project.ProductName_Server);
                    AddOperationLog(Severity.信息.ToString(), StrConst.TITLE_SYS, "", "载入文件成功:" + filename);
                    //Thread.Sleep(5000);
                }
                else
                {
                    MessageBox.Show("加载数据文件失败，请检查是否注册对应插件", "文件加载", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);

                    currentPrejectPath = "";
                    this.Text = "数据采集服务器-IOSERVER";
                    AddOperationLog(Severity.信息.ToString(), StrConst.TITLE_SYS, "", "载入文件失败：" + filename);
                }

            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
                string msg = string.Format("加载数据文件出错！数据文件：{0}", currentPrejectPath);
                AddOperationLog(StrConst.SERVERITY_ERR, StrConst.TITLE_SYS, "", msg);
            }

            CloseWaitingForm();

        }

        #endregion
        #endregion 设备列表

        #region 主窗体初始化
        private void RegisterFileType()
        {
            //注册文件关联
            try
            {

                if (!FileTypeRegister.FileTypeRegistered(".svr")) //如果文件类型没有注册，则进行注册
                {
                    FileTypeRegInfo fileTypeRegInfo = new FileTypeRegInfo(".svr"); //文件类型信息
                    fileTypeRegInfo.Description = "智能建筑集成管理系统服务端项目文件";
                    fileTypeRegInfo.ExePath = Application.ExecutablePath;
                    fileTypeRegInfo.ExtendName = ".svr";
                    fileTypeRegInfo.IcoPath = Application.ExecutablePath; //文件图标使用应用程序的
                    FileTypeRegister fileTypeRegister = new FileTypeRegister(); //注册
                    FileTypeRegister.RegisterFileType(fileTypeRegInfo);

                    Process[] process = Process.GetProcesses(); //重启Explorer进程，使更新立即生效
                    foreach (Process proc in process)
                    {
                        if (proc.ProcessName.Equals("explorer"))
                        {
                            proc.Kill();
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }
        }
        /// <summary>
        /// 是否过试用期检查
        /// </summary>
        /// <returns>
        /// True：已经过期了
        /// False:试用期内
        /// </returns>
        private bool ExpireCheck()
        {
            try
            {
                DateTime start = DateTime.Parse(licStartDate);
                if (DateTime.Now < start)
                {
                    //DeviceManagement.SingletonInstance.KeyAdd("-1");
                    return true;
                }
                if (DateTime.Now > start.AddDays(int.Parse(licMaxTime)))
                {
                    //DeviceManagement.SingletonInstance.KeyAdd("-1");
                    return true;
                }
                // DeviceManagement.SingletonInstance.KeyAdd("0");
                return false;
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
                //DeviceManagement.SingletonInstance.KeyAdd("-1");
                return true;
            }
            //DeviceManagement.SingletonInstance.KeyAdd("-1");
            return true;
        }
        private string licMachaneCode = "";
        private string licStartDate = "";
        private string licType = "";
        private string licRegCode = "";
        private string licMaxTime = "";
        private string licMaxPoint = "";
        private bool licOK = false;
        private bool GetLicence()
        {
            RSAHelper rsa = new RSAHelper();

            //机器码||当前时间||授权类型||注册码||试运行时间||最大点数

            string code = ServerConfig.Expire;
            string code1 = rsa.RSADecrypt(code);

            string[] arr = code1.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            if (arr.Length == 6)
            {
                licMachaneCode = arr[0];
                licStartDate = arr[1];
                licType = arr[2];
                licRegCode = arr[3];
                licMaxTime = arr[4];
                licMaxPoint = arr[5];
                //licOK = rsa.CheckSoftRegCodeByInput(StrConst.SOFTNAME, licRegCode);

                licOK=rsa.CheckSoftRegCodeByInput2(StrConst.SOFTNAME, licMachaneCode);

                string mCode = rsa.CreateGomputerbit(StrConst.SOFTNAME);

                if (licMachaneCode != mCode)
                {
                    licType = "0";
                    licMaxTime = "30";
                    licMaxPoint = "500";
                }
            }
            return licOK;
        }
        private void FormMain_Load(object sender, EventArgs e)
        {



            if (ServerConfig.AutoComm)
            {
                this.Hide();
            }
            DevicePath = new StringBuilder("", 260);
            //Stopwatch watch = new Stopwatch();
            //watch.Start();
            //V2取消数据库
            //if (ServerConfig.DataBaseEnable)
            //      g_bConnDbOK = TestDBconn();
            //watch.Stop();
            //Debug.WriteLine("数据库连接测试时间（毫秒）:" + watch.ElapsedMilliseconds);

            //RegisterFileType();
            //在窗体开发装载时，必须首先调用该函数
            //ytsoftkey = new SoftKey();

            //@def_net MessageBox.Show ("要测试网络版，请先运行通用服务程序，请确定通用服务程序已运行后，单击‘确定’。");
            //@def_net short ret = ytsoftkey.ConnectSvr("127.0.0.1", 3001);
            //@def_net if(ret!=0)MessageBox.Show( "不能连接服务器, 错误码为："+ytsoftkey.GetErrInfo(ret));
            //#if DEBUG

            //#else
            try
            {
                if (!licOK)
                {
                    //检查是否超时
                    if (ExpireCheck())
                    {
                        /*string id=  rsa2.CreateGomputerbit(StrConst.SOFTNAME);
                        MessageBox.Show("已经超过软件试用时间，软件将被关闭！\r\n"+
                                         "软件序列号："+id+"\r\n请注册后使用");
                        Process current = Process.GetCurrentProcess();
                        current.Kill();*/
                        if (!ServerConfig.AutoComm)
                            MessageBox.Show("试用版软件已经过期，部分功能停用，请注册后使用！");


                    }
                    else
                    {
                        if (!ServerConfig.AutoComm)
                            MessageBox.Show("试用版软件仅用于测试，请注册后使用！");


                    }
                }
                else
                {
                    //DeviceManagement.SingletonInstance.KeyAdd("1");
                }


            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }


            //#endif
            try
            {
                //tabMain.TabPages.Clear();
                //tabMain.TabPages.Add(tabPageLog);
                tabPageValueDesc.Parent = null;
                tabPageAlarm.Parent = null;
                tabPageAction.Parent = null;
                tabPageVideo.Parent = null;
                tabPageSMS.Parent = null;


                FormIsShow = true;
                //iniTagListView();
                inilsvLogHead();
                //V2取消数据库
                //if (ServerConfig.DataBaseEnable && g_bConnDbOK)
                //   dbAssistant.Start();

                //add grid controller

                gridAlarmAction.Controller.AddController(valueChangedController);
                gridEventTrigger.Controller.AddController(valueChangedController);
                gridStateDesc.Controller.AddController(valueChangedController);
                gridAlarmSMS.Controller.AddController(valueChangedController);
                gridAlarmVideo.Controller.AddController(valueChangedController);

                gridAlarmAction.Controller.AddController(toolTipController);
                gridEventTrigger.Controller.AddController(toolTipController);
                gridStateDesc.Controller.AddController(toolTipController);
                gridAlarmSMS.Controller.AddController(toolTipController);
                gridAlarmVideo.Controller.AddController(toolTipController);

                //add grid head
                CreateVariableGridHead();
                if (MainArgs != "")
                {

                    string s = MainArgs.Substring(MainArgs.Length - 4);
                    if (s.ToLower() == ".svr")
                    {
                        currentPrejectPath = ServerConfig.ProjectPath = MainArgs;
                        this.Text = "智能建筑集成管理系统 [" + currentPrejectPath + "]";
                        //MessageBox.Show(currentPrejectPath);
                        LoadPrj(currentPrejectPath);
                        btnSave.Enabled = true;
                        SetOpenState();
                    }
                }
                else
                {
                    //autoload
                    if (ServerConfig.FileAutoLoad && (ServerConfig.ProjectPath != ""))
                    {
                        if (File.Exists(ServerConfig.ProjectPath))
                        {
                            currentPrejectPath = ServerConfig.ProjectPath;
                            this.Text = "智能建筑集成管理系统 [" + currentPrejectPath + "]";
                            LoadPrj(currentPrejectPath);
                            btnSave.Enabled = true;

                            SetOpenState();
                        }
                        else
                        {
                            //MessageBox.Show("自动加载数据文件不存在！\r\n文件名" + ServerConfig.ProjectPath);
                            if (!ServerConfig.AutoComm)
                                MessageBox.Show("自动加载数据文件不存在！\r\n文件名" + ServerConfig.ProjectPath, "文件", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                            else
                                Logger.GetInstance().LogMsg("自动加载数据文件不存在！\r\n文件名" + ServerConfig.ProjectPath);
                        }
                    }
                    //auto communication
                    if (ServerConfig.AutoComm)
                    {
                        Thread.Sleep(1000);
                        btnStartComm.PerformClick();
                        Thread.Sleep(50);
                    }
                }

                /* if (ServerConfig.DataBaseEnable && g_bConnDbOK)
                 {
                     ProjectMng.ResoreFormList(Rtdb.DesignFormList);
                     Sqldb.LoadInfoFromDB();
                     //MapList2TreeView();
                 }
                 */


                timerSysTime.Enabled = true;

#if DEBUG

#else
                StartPresessMonitor();  
#endif

            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
                Console.WriteLine(ex.ToString());
            }


        }
        private void WriteProgromRunFlag()
        {
            try
            {
                File.WriteAllText(Application.StartupPath + "\\exit.txt", "RUNNING");
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError("写入exit.txt RUNNING出错");
            }
        }
        private void WriteProgramStopFlag()
        {
            try
            {
                File.WriteAllText(Application.StartupPath + "\\exit.txt", "OK");
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError("写入exit.txt OK出错");
            }
        }
        private void StartPresessMonitor()
        {
            Process[] w = Process.GetProcessesByName("IOServer.Monitor");
            if (w.Length == 0)
            {
                try
                {
                    var si = new ProcessStartInfo(Program.AppPath + "/IOServer.Monitor.exe", "IOServer" + " " + this.Handle.ToString());
                    Process.Start(si);
                }
                catch
                {
                }
            }
        }
        private void SleepDoEvents(int n)
        {
            for (int i = 0; i < n; i++)
            {
                Application.DoEvents();
                Thread.Sleep(1);
            }
        }
        /// <summary>
        /// 硬件加密锁读点数限制
        /// </summary>
        /// <returns></returns>
        private int ReadKey_MaxPoints()
        {
            int defaultPoints = 1000;
            int ret;
            string H_KEY, L_KEY;
            int outlen = 0;
            short addr = 0;//要从储存器中读取数据的起始地址
            StringBuilder Out_str = new StringBuilder("", 300000);


            //这个例子与上面的不同的地方，是先将字符串的长度写入到要储存的地址的首位置，然后再写入锁的长度，
            //在读字符串时，先取出原来写入的字符串的长度，然后再读出相应长度的字符串
            H_KEY = "ffffffff";
            L_KEY = "ffffffff";
            //先从addr读取原来字符串的长度
            ret = SoftKey.YtReadLong(ref outlen, addr, H_KEY, L_KEY, DevicePath);
            if (ret != 0)
            {
                // MessageBox.Show("未能将数据从加密锁EPROM中读出，原因请参看返回码：" + ret.ToString());
                return defaultPoints;
            }
            //再从addr+4读取指定义长度的字符串
            ret = SoftKey.YtReadString(Out_str, (short)(addr + 4), (short)outlen, H_KEY, L_KEY, DevicePath);
            if (ret == 0)
            {
                // MessageBox.Show("已成功将数据从加密锁EPROM中读出！");
                //textBoxExpTimeEn.Text += "\r\n*******以下字符为从加密锁中读出*********\r\n";
                //textBoxExpTimeEn.Text += Out_str.ToString();
                string keyStr = Out_str.ToString();
                RSAHelper rsa = new RSAHelper();
                int max = defaultPoints;
                try
                {
                    max = Int32.Parse(rsa.RSADecrypt(keyStr));
                }
                catch
                {
                    max = 100;
                }
                return max;
            }
            else
            {
                //MessageBox.Show("未能将数据从加密锁EPROM中读出，原因请参看返回码：" + ret.ToString());
                return defaultPoints;
            }
        }
        private void mruManager_ItemClicked(object sender, string file)
        {
            LoadPrj(file);
        }
        private void CreateVariableGridHead()
        {
            iniGridAction();
            iniGridEventTrigger();
            iniGridStateDesc();
            iniGridVideo();
            iniGridSMS();
        }
        private void ClearVariableGrid()
        {
            try
            {
                int c = gridAlarmAction.Rows.Count;
                if (c > 1)
                {
                    for (int i = c - 1; i > 0; i--)
                        gridAlarmAction.Rows.Remove(i);
                }
                c = gridEventTrigger.Rows.Count;
                if (c > 1)
                {
                    for (int i = c - 1; i > 0; i--)
                        gridEventTrigger.Rows.Remove(i);
                }
                c = gridStateDesc.Rows.Count;
                if (c > 1)
                {
                    for (int i = c - 1; i > 0; i--)
                        gridStateDesc.Rows.Remove(i);
                }
                c = gridAlarmVideo.Rows.Count;
                if (c > 1)
                {
                    for (int i = c - 1; i > 0; i--)
                        gridAlarmVideo.Rows.Remove(i);
                }
                c = gridAlarmSMS.Rows.Count;
                if (c > 1)
                {
                    for (int i = c - 1; i > 0; i--)
                        gridAlarmSMS.Rows.Remove(i);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (MessageBox.Show(this, StrConst.WARN_EXIT, StrConst.TITLE_WARN, MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
            //{
            //    ForceClose();
            //}
            //else e.Cancel = true;

            if ((e.CloseReason == CloseReason.ApplicationExitCall) || (e.CloseReason == CloseReason.WindowsShutDown))
            {

                e.Cancel = false;
                ForceClose();

                //Process current = Process.GetCurrentProcess();
                //current.Kill();
            }
            else
            {
                e.Cancel = true;
                FormIsShow = false;
                this.WindowState = FormWindowState.Minimized;
                this.Hide();
            }


        }
        //退出程序前关闭各个服务
        private void ForceClose()
        {
            try
            {

                Logger.GetInstance().LogMsg("程序正常关闭！");
                Logger.GetInstance().Stop();
                WriteProgramStopFlag();

            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }
;
            notifyIcon1.Visible = false;
            _DriverMng.StopMqtt();
            MqttController.Save();
            Process current = Process.GetCurrentProcess();
            current.Kill();
        }

        private void iniTreeView()
        {
            //初始化树
            trvDevice.Nodes.Clear();
            trvDevice.ImageList = imageList1;

            //for (int i = 1; i < 25; i++)
            //{
            //    if (Config.GetSelSystem(i))
            //        CreateTreeRootNode(((DeviceSortEnum)i).ToString(), i);
            //}

            CreateTreeRootNode("设备驱动", 1);
        }

        private void CreateTreeRootNode(string nodeText, int Sort)
        {
            TreeNode treeNode = new System.Windows.Forms.TreeNode(nodeText);
            this.trvDevice.SelectedImageIndex = 1;
            Project tag = new Project();
            tag.Sort = Sort;
            treeNode.Tag = tag;
            treeNode.Text = nodeText;
            treeNode.ImageIndex = 9;
            treeNode.SelectedImageIndex = 9;
            this.trvDevice.Nodes.Add(treeNode);

        }


        /*private void iniMapTree()
        {
            //画面树
            trvForm.Nodes.Clear();
            TreeNode treeNode5 = new System.Windows.Forms.TreeNode("画面变量");
            this.trvForm.ImageList = this.imageList1;
            this.trvForm.SelectedImageIndex = 10;
            this.trvForm.ImageIndex = 10;
            Project tag5 = new Project();
            tag5.Sort = 2;
            treeNode5.Name = "nodForm";
            treeNode5.Tag = tag5;
            treeNode5.Text = "画面变量";

            //组建树
            this.trvForm.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
               treeNode5
            });
          
        }*/
        private void iniListViewDeviceVarHead(TreeNode node)
        {
            try
            {
                SetShortcutsDisable();

                ListViewDevice.Clear();
                ListViewDevice.GridLines = true;     //显示各个记录的分隔线 
                ListViewDevice.FullRowSelect = true; //要选择就是一行 
                ListViewDevice.MultiSelect = true;
                ListViewDevice.View = View.Details;  //定义列表显示的方式 
                ListViewDevice.Scrollable = true;    //需要时候显示滚动条 
                ListViewDevice.MultiSelect = true;  // 不可以多行选择 
                ListViewDevice.HeaderStyle = ColumnHeaderStyle.Clickable;
                ListViewDevice.Tag = node;

                // 针对数据库的字段名称，建立与之适应显示表头 
                ListViewDevice.Columns.Add("序  号", 80, HorizontalAlignment.Center);
                ListViewDevice.Columns.Add("变量ID", 80, HorizontalAlignment.Center);
                ListViewDevice.Columns.Add("名  称", 250, HorizontalAlignment.Center);
                ListViewDevice.Columns.Add("地  址", 100, HorizontalAlignment.Center);
                ListViewDevice.Columns.Add("数值类型", 100, HorizontalAlignment.Center);
                ListViewDevice.Columns.Add("数  值", 100, HorizontalAlignment.Center);
                ListViewDevice.Columns.Add("单  位", 100, HorizontalAlignment.Center);
                ListViewDevice.Columns.Add("邮  戳", 150, HorizontalAlignment.Center);
                ListViewDevice.Columns.Add("品  质", 100, HorizontalAlignment.Center);
                ListViewDevice.Columns.Add("更  新", 100, HorizontalAlignment.Center);
                ListViewDevice.Columns.Add("描  述", 100, HorizontalAlignment.Center);
                //ListViewDevice.VirtualMode = true;
                //ListViewDevice.VirtualListSize = 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
        private void iniListViewDeviceChaHead(TreeNode node)
        {
            try
            {
                SetShortcutsDisable();

                ListViewDevice.Clear();
                ListViewDevice.GridLines = true;     //显示各个记录的分隔线 
                ListViewDevice.FullRowSelect = true; //要选择就是一行 
                ListViewDevice.MultiSelect = true;
                ListViewDevice.View = View.Details;  //定义列表显示的方式 
                ListViewDevice.Scrollable = true;    //需要时候显示滚动条 
                ListViewDevice.MultiSelect = true;  // 不可以多行选择 
                ListViewDevice.HeaderStyle = ColumnHeaderStyle.Clickable;
                ListViewDevice.Tag = node;
                ListViewDevice.VirtualMode = true;
                ListViewDevice.VirtualListSize = 0;

                ListViewDevice.Columns.Add("序    号", 80, HorizontalAlignment.Center);
                ListViewDevice.Columns.Add("通道ID", 80, HorizontalAlignment.Center);
                ListViewDevice.Columns.Add("通道名称", 250, HorizontalAlignment.Center);
                ListViewDevice.Columns.Add("通道描述", 100, HorizontalAlignment.Center);
                ListViewDevice.Columns.Add("协议名称", 100, HorizontalAlignment.Center);
                ListViewDevice.Columns.Add("通讯接口", 100, HorizontalAlignment.Center);
                ListViewDevice.Columns.Add("通道状态", 100, HorizontalAlignment.Center);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        private void iniListViewDeviceConHead(TreeNode node)
        {
            try
            {
                SetShortcutsDisable();
                ListViewDevice.Clear();
                ListViewDevice.GridLines = true;     //显示各个记录的分隔线 
                ListViewDevice.FullRowSelect = true; //要选择就是一行 
                ListViewDevice.MultiSelect = true;
                ListViewDevice.View = View.Details;  //定义列表显示的方式 
                ListViewDevice.Scrollable = true;    //需要时候显示滚动条 
                ListViewDevice.MultiSelect = true;  // 不可以多行选择 
                ListViewDevice.HeaderStyle = ColumnHeaderStyle.Clickable;

                ListViewDevice.VirtualMode = true;
                ListViewDevice.VirtualListSize = 0;
                ListViewDevice.Tag = node;


                // 针对数据库的字段名称，建立与之适应显示表头 
                ListViewDevice.Columns.Add("序    号", 80, HorizontalAlignment.Center);
                ListViewDevice.Columns.Add("控制器ID", 80, HorizontalAlignment.Center);
                ListViewDevice.Columns.Add("控制器名称", 250, HorizontalAlignment.Center);
                ListViewDevice.Columns.Add("描述", 150, HorizontalAlignment.Center);
                ListViewDevice.Columns.Add("控制器地址", 100, HorizontalAlignment.Center);
                ListViewDevice.Columns.Add("IP地址", 150, HorizontalAlignment.Center);
                ListViewDevice.Columns.Add("Mac地址", 150, HorizontalAlignment.Center);
                ListViewDevice.Columns.Add("通讯状态", 100, HorizontalAlignment.Center);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        private void iniGridAction()
        {
            gridAlarmAction.SelectionMode = GridSelectionMode.Row;
            gridAlarmAction.BorderStyle = BorderStyle.FixedSingle;

            gridAlarmAction.ColumnsCount = 4;
            gridAlarmAction.FixedRows = 1;
            gridAlarmAction.Rows.Insert(0);

            //ColumnHeader view

            SourceGrid.Cells.Views.ColumnHeader boldHeader = new SourceGrid.Cells.Views.ColumnHeader();
            boldHeader.Font = new Font("宋体", 9, FontStyle.Regular);
            boldHeader.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;

            gridAlarmAction[0, 0] = new SourceGrid.Cells.ColumnHeader("触发事件");
            gridAlarmAction[0, 1] = new SourceGrid.Cells.ColumnHeader("动作变量");
            gridAlarmAction[0, 2] = new SourceGrid.Cells.ColumnHeader("动作数值");
            gridAlarmAction[0, 3] = new SourceGrid.Cells.ColumnHeader("描    述");

            gridAlarmAction[0, 0].View = boldHeader;
            gridAlarmAction[0, 1].View = boldHeader;
            gridAlarmAction[0, 2].View = boldHeader;
            gridAlarmAction[0, 3].View = boldHeader;

            gridAlarmAction.AutoStretchColumnsToFitWidth = true;

        }
        private void iniGridVideo()
        {
            gridAlarmVideo.BorderStyle = BorderStyle.FixedSingle;
            gridAlarmVideo.SelectionMode = GridSelectionMode.Row;
            gridAlarmVideo.ColumnsCount = 5;
            gridAlarmVideo.FixedRows = 1;
            gridAlarmVideo.Rows.Insert(0);

            //ColumnHeader view

            SourceGrid.Cells.Views.ColumnHeader boldHeader = new SourceGrid.Cells.Views.ColumnHeader();
            boldHeader.Font = new Font("宋体", 9, FontStyle.Regular);
            boldHeader.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;


            gridAlarmVideo[0, 0] = new SourceGrid.Cells.ColumnHeader("触发事件");
            gridAlarmVideo[0, 1] = new SourceGrid.Cells.ColumnHeader("监视器");
            gridAlarmVideo[0, 2] = new SourceGrid.Cells.ColumnHeader("子画面");
            gridAlarmVideo[0, 3] = new SourceGrid.Cells.ColumnHeader("摄像机");
            gridAlarmVideo[0, 4] = new SourceGrid.Cells.ColumnHeader("预置位");

            gridAlarmVideo[0, 0].View = boldHeader;
            gridAlarmVideo[0, 1].View = boldHeader;
            gridAlarmVideo[0, 2].View = boldHeader;
            gridAlarmVideo[0, 3].View = boldHeader;
            gridAlarmVideo[0, 4].View = boldHeader;

            gridAlarmVideo.AutoStretchColumnsToFitWidth = true;




        }
        private void iniGridSMS()
        {
            gridAlarmSMS.SelectionMode = SourceGrid.GridSelectionMode.Row;
            gridAlarmSMS.BorderStyle = BorderStyle.FixedSingle;
            gridAlarmSMS.ColumnsCount = 4;
            gridAlarmSMS.FixedRows = 1;
            gridAlarmSMS.Rows.Insert(0);



            //ColumnHeader view
            SourceGrid.Cells.Views.ColumnHeader boldHeader = new SourceGrid.Cells.Views.ColumnHeader();
            boldHeader.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;
            boldHeader.Font = new Font("宋体", 9, FontStyle.Regular);

            gridAlarmSMS[0, 0] = new SourceGrid.Cells.ColumnHeader("触发事件");
            gridAlarmSMS[0, 1] = new SourceGrid.Cells.ColumnHeader("号码/邮箱");
            gridAlarmSMS[0, 2] = new SourceGrid.Cells.ColumnHeader("信息内容");
            gridAlarmSMS[0, 3] = new SourceGrid.Cells.ColumnHeader("消息类型");

            gridAlarmSMS[0, 0].View = boldHeader;
            gridAlarmSMS[0, 1].View = boldHeader;
            gridAlarmSMS[0, 2].View = boldHeader;
            gridAlarmSMS[0, 3].View = boldHeader;

            //Stretch only the last column
            gridAlarmSMS.AutoStretchColumnsToFitWidth = true;
            gridAlarmSMS.Columns[0].Width = 150;
            gridAlarmSMS.Columns[0].AutoSizeMode = SourceGrid.AutoSizeMode.None;
            gridAlarmSMS.Columns[1].Width = 150;
            gridAlarmSMS.Columns[1].AutoSizeMode = SourceGrid.AutoSizeMode.None;




        }
        private void iniGridStateDesc()
        {
            gridStateDesc.BorderStyle = BorderStyle.FixedSingle;
            gridStateDesc.SelectionMode = SourceGrid.GridSelectionMode.Row;
            gridStateDesc.ColumnsCount = 2;
            gridStateDesc.FixedRows = 1;
            gridStateDesc.Rows.Insert(0);

            //ColumnHeader view
            SourceGrid.Cells.Views.ColumnHeader boldHeader = new SourceGrid.Cells.Views.ColumnHeader();
            boldHeader.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;
            boldHeader.Font = new Font("宋体", 9, FontStyle.Regular);
            gridStateDesc[0, 0] = new SourceGrid.Cells.ColumnHeader("数  值");
            gridStateDesc[0, 1] = new SourceGrid.Cells.ColumnHeader("描  述");

            gridStateDesc[0, 0].View = boldHeader;
            gridStateDesc[0, 1].View = boldHeader;

            //Stretch only the last column
            gridStateDesc.AutoStretchColumnsToFitWidth = true;
            gridStateDesc.Columns[0].AutoSizeMode = SourceGrid.AutoSizeMode.None;
            gridStateDesc.Columns[0].Width = 100;





        }
        private void iniGridEventTrigger()
        {
            gridEventTrigger.BorderStyle = BorderStyle.FixedSingle;
            gridEventTrigger.SelectionMode = GridSelectionMode.Row;
            gridEventTrigger.ColumnsCount = 6;
            gridEventTrigger.FixedRows = 1;
            gridEventTrigger.Rows.Insert(0);

            //ColumnHeader view
            SourceGrid.Cells.Views.ColumnHeader boldHeader = new SourceGrid.Cells.Views.ColumnHeader();
            boldHeader.Font = new Font("宋体", 9, FontStyle.Regular);
            boldHeader.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;


            gridEventTrigger[0, 0] = new SourceGrid.Cells.ColumnHeader("触发条件");
            gridEventTrigger[0, 1] = new SourceGrid.Cells.ColumnHeader("事件类型");
            gridEventTrigger[0, 2] = new SourceGrid.Cells.ColumnHeader("事件描述");
            gridEventTrigger[0, 3] = new SourceGrid.Cells.ColumnHeader("优 先 级");
            gridEventTrigger[0, 4] = new SourceGrid.Cells.ColumnHeader("执行脚本");
            gridEventTrigger[0, 5] = new SourceGrid.Cells.ColumnHeader("触发时限");

            gridEventTrigger[0, 0].View = boldHeader;
            gridEventTrigger[0, 1].View = boldHeader;
            gridEventTrigger[0, 2].View = boldHeader;
            gridEventTrigger[0, 3].View = boldHeader;
            gridEventTrigger[0, 4].View = boldHeader;
            gridEventTrigger[0, 5].View = boldHeader;

            gridEventTrigger.AutoStretchColumnsToFitWidth = true;

        }
        #endregion

        #region 窗体控件事件
        /// <summary>
        /// 设备树事件MouseDown（设置右键快捷菜单）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void trvDeviceMngMouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                Point ClickPoint = new Point(e.X, e.Y);
                TreeNode nod = trvDevice.GetNodeAt(ClickPoint);

                if (nod != null)//判断你点的是不是一个节点
                {
                    trvDevice.SelectedNode = nod;
                    if (e.Button == MouseButtons.Right)//判断你点的是不是右键
                    {
                        nod.ContextMenuStrip = null;
                        object o = nod.Tag;

                        if (ServerConfig.RunningState == ServerStateEnum.STOPED)  //配置模式
                        {
                            if (o is Project)
                            {
                                nod.ContextMenuStrip = context_L1;

                            }
                            else if (o is IChannel)
                            {
                                nod.ContextMenuStrip = context_L2;
                                IChannel currentChan = nod.Tag as IChannel;
                                if (currentChan.Enable)
                                {
                                    tspItemChannelEnable.Text = "禁用通道";
                                    nod.ImageIndex = DevTreeImage.ChannelNormal;
                                    nod.SelectedImageIndex = DevTreeImage.ChannelNormal;
                                }
                                else
                                {
                                    tspItemChannelEnable.Text = "启用通道";
                                    nod.ImageIndex = DevTreeImage.ChannelDisable;
                                    nod.SelectedImageIndex = DevTreeImage.ChannelDisable;
                                }


                            }
                            else if (o is IController)
                            {
                                nod.ContextMenuStrip = context_L3;
                                IController currentCon = nod.Tag as IController;
                                if (currentCon.Enable)
                                {
                                    nod.ImageIndex = DevTreeImage.ControllNormal;
                                    nod.SelectedImageIndex = DevTreeImage.ControllNormal;
                                    toolsripControllIsEnable.Text = "禁用控制器";

                                }
                                else
                                {
                                    nod.ImageIndex = DevTreeImage.ControllDisable;
                                    nod.SelectedImageIndex = DevTreeImage.ControllDisable;
                                    toolsripControllIsEnable.Text = "启用控制器";

                                }

                            }
                        }
                        else if (ServerConfig.RunningState == ServerStateEnum.RUNING) //运行模式
                        {
                            if (o is IController)
                            {
                                IController currentCon = nod.Tag as IController;
                                if (currentCon != null && currentCon.Active)
                                {
                                    if (currentCon is IAlarmController)
                                        nod.ContextMenuStrip = context_ArmKeybord;
                                    else if (currentCon is ISMSController)
                                        nod.ContextMenuStrip = context_sms;

                                }
                            }
                            else
                            {
                                nod.ContextMenuStrip = null;

                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Logger.GetInstance().LogError(ex.ToString());
            }

        }
        /// <summary>
        /// 设备树节点选中事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trvDeviceMngNodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                //this.SuspendLayout();
                object o = e.Node.Tag;
                //设备驱动根节点
                if (o is Project)
                {
                    if (ServerConfig.RunningState == ServerStateEnum.STOPED)
                        ShowMainTabPage(MainTabPage.None);
                    labelDevieInfo.Text = ">>通讯通道";
                    ListViewChannel(e.Node, Rtdb.ChanList);


                }
                //第二级节点：通讯通道
                else if (o is IChannel)
                {
                    //当前通道
                    if (ServerConfig.RunningState == ServerStateEnum.STOPED)
                        ShowMainTabPage(MainTabPage.None);
                    IChannel currentChan = o as IChannel;
                    labelDevieInfo.Text = ">>通讯通道 >>控制器";

                    ListViewController(e.Node, currentChan.ConList);

                }
                //第三级节点：控制器
                else if (o is IController)
                {
                    //当前控制器
                    IController currentCon = o as IController;
                    if (ServerConfig.RunningState == ServerStateEnum.STOPED)
                        ShowMainTabPage(MainTabPage.None);
                    labelDevieInfo.Text = ">>通讯通道  >>控制器  >>设备控制点";
                    //虚模式显示点列表
                    ListViewVariable(e.Node, currentCon.VarList);

                }

                if (ServerConfig.RunningState == ServerStateEnum.STOPED)
                    propertyGrid1.SelectedObject = o;
                //this.ResumeLayout(false);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Logger.GetInstance().LogError(ex.ToString());
            }

        }

        //新增通道
        private void NewChanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                FormChannelWizard formChanWizard;
                formChanWizard = new FormChannelWizard();

                //formChanWizard.TrvDev = trvDevice;
                if (formChanWizard.ShowDialog() == DialogResult.OK)
                {
                    IChannel cha = formChanWizard.ChanInfo;
                    cha.ID = "";
                    // cha.ID = Rtdb.GetUniqueChannelCloneID(pubFun.checkUrl(pubFun.GetFirstPinYin(pubFun.ChineseTONumber(pubFun.ToDBC(cha.Name)))));
                    cha.ID = Rtdb.GetUniqueChannelID();
                    //cha.Name = Rtdb.GetPrefixName(cha.Name)+ cha.ID;
                    TreeNode node = new TreeNode();
                    node.Text = formChanWizard.ChanInfo.Name;
                    node.Tag = cha;
                    if (cha.Enable)
                    {
                        node.ImageIndex = DevTreeImage.ChannelNormal;
                        node.SelectedImageIndex = DevTreeImage.ChannelNormal;
                    }
                    else
                    {
                        node.ImageIndex = DevTreeImage.ChannelDisable;
                        node.SelectedImageIndex = DevTreeImage.ChannelDisable;
                    }
                    trvDevice.SelectedNode.Nodes.Add(node);

                    // trvDevice.ExpandAll();
                    if (cha != null)
                    {
                        Rtdb.ChanList.Add(cha);
                        AddNewItem2ListView(cha);
                    }
                    ListViewDevice.Invalidate();
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }

        }
        //新增控制器
        private void NewControllerTspMenuItem_Click(object sender, EventArgs e)
        {
            if (trvDevice.SelectedNode == null) return;
            try
            {
                IChannel currentChan = trvDevice.SelectedNode.Tag as IChannel;
                if (currentChan == null) return;

                FormControllerWizard formConWizard = new FormControllerWizard();
                formConWizard.SelChannel = currentChan;
                if (formConWizard.ShowDialog() == DialogResult.OK)
                {
                    foreach (IController con in formConWizard.NewControllerList)
                    {
                        con.ID = "";
                        //con.ID = Rtdb.GetUniqueControllCloneID(pubFun.checkUrl(pubFun.GetFirstPinYin(pubFun.ChineseTONumber(pubFun.ToDBC(con.Name)))));
                        con.ID = Rtdb.GetUniqueControllID(con.ChannelObject);
                        //con.Name = Rtdb.GetPrefixName(con.Name) + con.ID;
                        TreeNode node = new TreeNode();
                        node.Text = con.Name;
                        node.Tag = con;
                        if (con.Enable)
                        {
                            node.ImageIndex = DevTreeImage.ControllNormal;
                            node.SelectedImageIndex = DevTreeImage.ControllNormal;
                        }
                        else
                        {
                            node.ImageIndex = DevTreeImage.ControllDisable;
                            node.SelectedImageIndex = DevTreeImage.ControllDisable; ;
                        }

                        trvDevice.SelectedNode.Nodes.Add(node);
                        AddNewItem2ListView(con);
                    }
                    ListViewDevice.Invalidate();
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }
        }
        private void tspItemImportController_Click(object sender, EventArgs e)
        {
            if (trvDevice.SelectedNode == null) return;
            try
            {
                IChannel currentChan = trvDevice.SelectedNode.Tag as IChannel;
                if (currentChan == null) return;

                List<IController> NewControllerList = currentChan.Plugin.ImportController(currentChan);
                foreach (IController con in NewControllerList)
                {
                    //con.ID = "";
                    //con.ID = Rtdb.GetUniqueControllCloneID(pubFun.checkUrl(pubFun.GetFirstPinYin(pubFun.ChineseTONumber(pubFun.ToDBC(con.Name)))));
                    if (con.ID == "")
                        con.ID = Rtdb.GetUniqueControllID(currentChan);


                    foreach (IVariable var in con.VarList)
                    {
                        if(var.ID!="")
                          var.ID = Rtdb.GetUniqueVariableID(con);
                    }

                    TreeNode node = new TreeNode();
                    node.Text = con.Name;
                    node.Tag = con;
                    if (con.Enable)
                    {
                        node.ImageIndex = DevTreeImage.ControllNormal;
                        node.SelectedImageIndex = DevTreeImage.ControllNormal;
                    }
                    else
                    {
                        node.ImageIndex = DevTreeImage.ControllDisable;
                        node.SelectedImageIndex = DevTreeImage.ControllDisable; ;
                    }

                    trvDevice.SelectedNode.Nodes.Add(node);
                    AddNewItem2ListView(con);

                }
                ListViewDevice.Refresh();
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }

        }

        //新增控制点
        private void NewTagTspMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                if (trvDevice.SelectedNode == null) return;

                IController currentCon = trvDevice.SelectedNode.Tag as IController;
                FormVarWizard formTagWizard = new FormVarWizard(false, currentCon); //IsEdit 
                                                                                    //formTagWizard.selController = currentCon;

                if (formTagWizard.ShowDialog() == DialogResult.OK)
                {
                    foreach (IVariable var in formTagWizard.NewVarList)
                    {
                        //var.ID = "";
                        var.ID = Rtdb.GetUniqueVariableID(currentCon);
                        //var.Name = Rtdb.GetPrefixName(var.Name) + var.ID;

                    }
                    AddNewItem2ListView(formTagWizard.NewVarList);
                }
                this.propertyGrid1.Refresh();

            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }
        }
        private void AddNewItem2ListView(List<IVariable> VarList)
        {
            //Stopwatch watch = new Stopwatch();
            //watch.Start();
            foreach (IVariable var in VarList)
            {

                AddNewItem2ListView(var);

            }
            //watch.Stop();
            //Debug.WriteLine("AddNewItem2ListView  " + watch.ElapsedMilliseconds);
            ListViewDevice.Invalidate();
        }
        //新增显示项
        private void AddNewItem2ListView(object o)
        {
            TreeNode treenode = ListViewDevice.Tag as TreeNode;
            if (treenode == null) return;
            object obj = treenode.Tag;
            if (obj is Project)
            {
                IChannel ch = o as IChannel;
                ListViewItem lsvTagItem = new ListViewItem((ListViewDevice.GetItemsConut() + 1).ToString("D4"));
                lsvTagItem.Tag = ch;
                if (ch.Enable)
                {
                    //lsvTagItem.ImageIndex = DevTreeImage.ChannelNormal;
                    if(ch.Active)
                        lsvTagItem.ImageIndex = DevTreeImage.ChannelNormal;
                    else
                        lsvTagItem.ImageIndex = DevTreeImage.ChannelOffLine;
                }
                else
                {
                    lsvTagItem.ImageIndex = DevTreeImage.ChannelDisable;

                }

                lsvTagItem.SubItems.AddRange(new string[] {
                    ch.ID,
                    ch.Name,
                    ch.Description,
                    ch.ProtocolName,
                    ch.CommType.ToString(),
                    ch.Active.ToString()

                    });
                ListViewDevice.Add(lsvTagItem);

            }

            //控制器
            else if (obj is IChannel)
            {
                IController con = o as IController;
                ListViewItem lsvTagItem = new ListViewItem((ListViewDevice.GetItemsConut() + 1).ToString("D4"));
                lsvTagItem.Tag = con;

                if (con.Enable)
                    //lsvTagItem.ImageIndex = DevTreeImage.ControllNormal;
                    if(con.Active)
                        lsvTagItem.ImageIndex = DevTreeImage.ControllNormal;
                     else
                        lsvTagItem.ImageIndex = DevTreeImage.ControllOffLine;
                else
                    lsvTagItem.ImageIndex = DevTreeImage.ControllDisable;

                lsvTagItem.SubItems.AddRange(new string[] {
                    con.ID,
                    con.Name,
                    con.Description,
                    con.Address.ToString(),
                    con is ITcpController? (con as ITcpController).IpAddress:""/**/,
                    con is ITcpController?(con as ITcpController).MacAddress:"" /*con.MacAddress*/,
                    con.Active.ToString(),

                });
                ListViewDevice.Add(lsvTagItem);

            }

            else if (obj is IController)
            {
                IVariable var = o as IVariable;
                ListViewItem lsvTagItem = new ListViewItem((ListViewDevice.GetItemsConut() + 1).ToString("D4"));
                lsvTagItem.Tag = var;
                if (var.Enable)
                {
                    //lsvTagItem.ImageIndex = DevTreeImage.VariableNormal;
                    if(var.Active)
                        lsvTagItem.ImageIndex = DevTreeImage.VariableNormal;
                    else
                        lsvTagItem.ImageIndex = DevTreeImage.VariableOffLine;
                }
                else
                {
                    lsvTagItem.ImageIndex = DevTreeImage.VariableDisable;

                }


                lsvTagItem.SubItems.AddRange(new string[] {
                                    var.ID,
                                    var.Name,
                                    var.Address.ToString(),
                                    var.ValueType.ToString(),
                                    var.Value==null?"Null":var.Value.ToString(),
                                    var.Unit,
                                    var.DateStamp.ToString(),
                                    ((COMM_QUALITY_STATUS)var.Quality).ToString(),
                                    var.Counter.ToString(),
                                    var.Description
                                   });


                ListViewDevice.Add(lsvTagItem);


            }
            //ListViewDevice.Invalidate();

        }
        public void RefreshListViewDev()
        {
            try
            {

                TreeNode currentNode = ListViewDevice.Tag as TreeNode;
                if (currentNode == null) return;
                object o = currentNode.Tag;

                //设备驱动根节点
                if (o is Project)
                {
                    ListViewDevice.Clear();
                    labelDevieInfo.Text = ">>" + currentNode.Text.ToString() + " >>通讯通道";
                    ListViewChannel(currentNode, Rtdb.ChanList);

                }
                //第二级节点：通讯通道
                else if (o is IChannel)
                {
                    //当前通道
                    IChannel currentChan = currentNode.Tag as IChannel;
                    labelDevieInfo.Text = ">>通讯通道 >>控制器";
                    ListViewController(currentNode, currentChan.ConList);

                }
                //第三级节点：控制器
                else if (o is IController)
                {
                    //当前控制器
                    IController currentCon = currentNode.Tag as IController;
                    labelDevieInfo.Text = ">>通讯通道  >>控制器  >>设备控制点";
                    //虚模式显示点列表
                    ListViewVariable(currentNode, currentCon.VarList);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Logger.GetInstance().LogError(ex.ToString());
            }
            ListViewDevice.Invalidate();

        }

        //修改控制器
        private void toolStripEditDev_Click(object sender, EventArgs e)
        {

        }

        //删除控制器
        private void toolStripDelDev_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认要删除该控制器？\n删除控制器将同时删除该控制器的变量！", "操作确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (trvDevice.SelectedNode == null) return;
                try
                {
                    IController con = trvDevice.SelectedNode.Tag as IController;
                    IChannel currentChan = con.ChannelObject;
                    currentChan.ConList.Remove(con);
                    trvDevice.SelectedNode.Remove();
                    ListViewDevice.Clear();
                }
                catch (Exception ex)
                {
                    Logger.GetInstance().LogError(ex.ToString());
                }
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            SavePrj();

        }
        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            SaveAsPrj();
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            if (currentPrejectPath != "")
            {
                if (MessageBox.Show("当前项目已经存在是否继续新建项目?", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (MessageBox.Show("是否保存当前项目?", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        SavePrj();
                    }
                    currentPrejectPath = "";
                    CurrentAllParmClear();
                    SetOpenState();
                    this.Text = "智能建筑集成管理系统-IO采集服务 [新建数据文件]";
                    iniTreeView();
                    //iniMapTree();
                    bExistProject = true;
                }
            }
            else
            {
                currentPrejectPath = "";
                CurrentAllParmClear();
                SetOpenState();
                this.Text = "智能建筑集成管理系统-IO采集服务 [新建数据文件]";
                iniTreeView();
                //iniMapTree();
                bExistProject = true;

            }
        }
        private void btnOpen_Click(object sender, EventArgs e)
        {

            //将TreeView保存到XML文件中
            dlgOpen.Filter = "数据文件 (*.svr)|*.svr|All files (*.*)|*.*";
            dlgOpen.FilterIndex = 0;
            dlgOpen.RestoreDirectory = true;
            dlgOpen.InitialDirectory = Application.StartupPath + "\\Preject";
            dlgOpen.FileName = "";
            dlgOpen.CheckFileExists = true;
            dlgOpen.CheckPathExists = true;
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                if (dlgOpen.FileName != "")
                {

                    try
                    {
                        LoadPrj(dlgOpen.FileName);

                    }
                    catch
                    {

                    }




                    // mruManager.Add(dlgOpen.FileName, Project.ProductName_Server);
                }
            }

        }

        private void LisviewDeviceDelete()
        {
            try
            {
                //Stopwatch wath = new Stopwatch();
                //wath.Start();
                //删除CurrentCacheItemsSource的项
                List<ListViewItem> delItems = ListViewDevice.GetSelectItem();


                //wath.Stop();
                //Debug.WriteLine("3:" + wath.ElapsedMilliseconds.ToString());
                //wath.Restart();

                TreeNode treenode = ListViewDevice.Tag as TreeNode;
                object o = treenode.Tag;
                if (o is Project)
                {
                    foreach (ListViewItem item in delItems)
                    {
                        IChannel chan = item.Tag as IChannel;
                        Rtdb.ChanList.Remove(chan);
                        TreeNode node = TreeviewSearch.FindNodeByText(trvDevice, chan.Name);
                        if (node != null)
                            node.Remove();
                    }

                }
                else if (o is IChannel)
                {
                    IChannel chan = treenode.Tag as IChannel;
                    foreach (ListViewItem item in delItems)
                    {
                        IController con = item.Tag as IController;
                        chan.ConList.Remove(con);
                        TreeNode node = TreeviewSearch.FindNodeByText(trvDevice, con.Name);
                        if (node != null)
                            node.Remove();
                    }

                }
                else if (o is IController)
                {
                    IController con = treenode.Tag as IController;
                    foreach (ListViewItem item in delItems)
                    {
                        IVariable var = item.Tag as IVariable;
                        con.VarList.Remove(var);
                        //TreeNode node = TreeviewSearch.FindNodeByText(trvDevice, var.Name);
                        //if (node != null)
                        //    node.Remove();
                    }

                }
                else if (o is IVariable)
                {


                }
                //wath.Stop();
                //Debug.WriteLine("1:"+wath.ElapsedMilliseconds.ToString());
                //wath.Restart();
                ListViewDevice.BeginUpdate();
                if (delItems.Count == ListViewDevice.CurrentCacheItemsSource.Count)
                {
                    ListViewDevice.RemoveAll();
                }
                else
                {

                    foreach (ListViewItem item in delItems)
                    {
                        //item.Selected = false;
                        ListViewDevice.CurrentCacheItemsSource.Remove(item);
                    }
                }
                ListViewDevice.Invalidate();
                ListViewDevice.EndUpdate();

                //wath.Stop();
                //Debug.WriteLine("2:" + wath.ElapsedMilliseconds.ToString());
                //wath.Restart();
                //重新生成序号
                for (int i = 0; i < ListViewDevice.CurrentCacheItemsSource.Count; i++)
                {

                    ListViewItem item = ListViewDevice.CurrentCacheItemsSource[i];
                    item.Text = (i + 1).ToString("D4");
                    //item.EnsureVisible();
                    //item.Selected = false;

                }

                delItems.Clear();
                tspCopy.Enabled = false;
                tspCut.Enabled = false;
                //wath.Stop();
                //Debug.WriteLine("4:" + wath.ElapsedMilliseconds.ToString());
                // wath.Restart();
                RefreshListViewDev();

            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }
        }

        //private void tspItemEditVariable_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        ListViewItem lsvTagItem = ListViewDevice.GetFirstSelectItem();
        //        if (lsvTagItem != null)
        //        {
        //            IVariable var = lsvTagItem.Tag as IVariable;
        //            FormVarWizard formTagWizard;
        //            formTagWizard = new FormVarWizard(true);

        //            formTagWizard.VarInfo = var;
        //            formTagWizard.ConInfo =var.ControllerObject;
        //            formTagWizard.ChanInfo =var.ControllerObject.ChannelObject;

        //            formTagWizard.TrvDev = trvDevice;
        //            if (formTagWizard.ShowDialog() == DialogResult.OK)
        //            {
        //                    if (var.Enable)
        //                        lsvTagItem.ImageIndex = DevTreeImage.VariableNormal;
        //                    else
        //                        lsvTagItem.ImageIndex = DevTreeImage.VariableDisable;
        //                    lsvTagItem.SubItems[1].Text = var.Name;
        //                    lsvTagItem.SubItems[2].Text = var.Address;
        //                    lsvTagItem.SubItems[3].Text = var.ValueType.ToString();
        //                    lsvTagItem.SubItems[4].Text = var.Value.ToString();
        //                    lsvTagItem.SubItems[5].Text = var.Unit;
        //                    lsvTagItem.SubItems[6].Text = var.DateStamp.ToString();
        //                    lsvTagItem.SubItems[7].Text = ((OPC_QUALITY_STATUS)var.Quality).ToString();
        //                    lsvTagItem.SubItems[8].Text = var.Counter.ToString();
        //                    lsvTagItem.SubItems[9].Text = var.Active.ToString();
        //                    ListViewDevice.Refresh(); //局部更新
        //             }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.GetInstance().LogError(ex.ToString());
        //    }
        //}


        private void tspItemDelCha_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认要删除该通道？\n删除通道将同时删除该通道的控制器和变量！", "操作确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (trvDevice.SelectedNode != null)
                {

                    IChannel chan = trvDevice.SelectedNode.Tag as IChannel;
                    if (chan != null)
                    {
                        Rtdb.ChanList.Remove(chan);
                        trvDevice.SelectedNode.Remove();
                        ListViewDevice.Clear();
                    }
                }
            }

        }
        private void CurrentAllParmClear()
        {
            ListViewDevice.Clear();
            Rtdb.ChanList.Clear();
            //Rtdb.DesignFormList.Clear();
            trvDevice.Nodes.Clear();
            //trvForm.Nodes.Clear();

        }


        private delegate void formWaiting_OnFinishdel(object sender, EventArgs e);
        private void formWaiting_OnFinish(object sender, EventArgs e)
        {
            this.Invoke(new formWaiting_OnFinishdel(formWaitingOnFinish), new object[] { sender, e });

        }
        private void formWaitingOnFinish(object sender, EventArgs e)
        {
            this.Visible = true;

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }
        private void tsbtnStart_Click(object sender, EventArgs e)
        {
            if (IsAdministrator() && ServiceStatus())
            {
                MessageBox.Show("IO采集服务器已经以Windows服务的方式运行，请停止服务再运行！");
                return;
            }
            toolStrip1.Enabled = false;
            try
            {

                WriteProgromRunFlag();
                RunTimeSun = 1;
                lsvLog.VirtualListSize = 0;
                lsvLog.Invalidate();
                //key(K0):0演示，1:试用，2正式 -1：无授权
                //Rtdb.CreateVariableDict();

                //#if DEBUG

                //#else

                if (!licOK)
                {
                    //检查是否超时
                    if (ExpireCheck())
                    {

                        string msg = "已经超过软件试用时间，软件禁止启动！请注册后使用";
                        AddOperationLog(Severity.错误.ToString(), "注册", "", msg);

                        MessageBox.Show(msg, "注册", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                        toolStrip1.Enabled = true;
                        return;
                    }
                    else
                    {

                        string msg = "试用版软件仅用于测试，请注册后使用！";
                        AddOperationLog(Severity.错误.ToString(), "注册", "", msg);
                        //  MessageBox.Show(msg);
                        if (!ServerConfig.AutoComm)
                        {
                            MessageBox.Show(msg, "注册", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                        }
                    }
                }
                else
                {
                    // DeviceManagement.SingletonInstance.KeyAdd("1");
                }

                try
                {
                    uint count = Rtdb.GetVarCounts();
                    if (count > int.Parse(licMaxPoint))
                    {
                        MessageBox.Show("系统目前点数为：" + count + ",已经超过授权点数，软件将不能正常运行！", "注册", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                        toolStrip1.Enabled = true;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Logger.GetInstance().LogError(ex.ToString());
                    MessageBox.Show("软件进行系统授权点数失败！");
                    toolStrip1.Enabled = true;
                    return;
                }


                //#endif
                frmWait = null;
                waitingmsg = "正在启动设备驱动，请耐心等待……";
                Thread th = new Thread(new ThreadStart(this.ShowProgress));
                th.IsBackground = true;
                th.Name = "ShowProgressThread";
                th.Start();
                //确保frmWait显示
                while (frmWait == null)
                {
                    Thread.Sleep(1);
                }
                while (!frmWait.IsShowing)
                {
                    Thread.Sleep(1);
                }

                try
                {

                    //启动驱动

                    SetBusyingState();
                    ShowMainTabPage(MainTabPage.Log);

                    //inilsvLogHead();
                    string msg = string.Format("设备驱动启动...");
                    AddOperationLog(StrConst.SERVERITY_MSG, StrConst.TITLE_OPER, "", msg);

                    //订阅点的变化事件
                    //foreach (var item in Rtdb.VarDictID.Values)
                    //{
                    //    item.Counter = 0;
                    //    item.OnValueChange += new OnValueChangeDelegate(var_OnValueChange);
                    //    //item.OnCounterChange += new OnCounterChangeDelegate(var_OnCounterChange);
                    // }


                    //ListViewDevice.Invalidate();


                    _DriverMng.ConnectedDrive();
                    //LoadFBD();
                    //LoadVBS();


                    //启动历史记录线程
                   // if (ServerConfig.EnableNormalRecorder || ServerConfig.EnableSlowRecorder || ServerConfig.EnableFastRecorder)
                   // {
                 

                   // }

                    RefreshListViewDev();
                }
                catch (Exception ex)
                {

                    Logger.GetInstance().LogError(ex.ToString());

                }

                CloseWaitingForm();

                SetRunningState();

#if DEBUG

#else
                    StartPresessMonitor();
#endif
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }
            toolStrip1.Enabled = true;

        }




        private void tsbtnStop_Click(object sender, EventArgs e)
        {
            toolStrip1.Enabled = false;
            WriteProgramStopFlag();
            waitingmsg = "正在停止设备驱动，请耐心等待……";
            Thread th = new Thread(new ThreadStart(this.ShowProgress));
            th.IsBackground = true;
            th.Start();
            //确保frmWait显示
            while (frmWait == null)
            {
                Thread.Sleep(1);
            }
            while (!frmWait.IsShowing)
            {
                Thread.Sleep(1);
            }
            try
            {

                SetBusyingState();

                _DriverMng.DisConnectedDrive();
                //订阅点的变化事件
                //foreach (var item in Rtdb.VarDictID.Values)
                //{
                //    item.OnValueChange -= new OnValueChangeDelegate(var_OnValueChange);
                //    //item.OnCounterChange -= new OnCounterChangeDelegate(var_OnCounterChange);
                //    item.Counter = 0;
                //}

                try
                {
                    string msg = string.Format("设备驱动关闭...");
                    AddOperationLog(StrConst.SERVERITY_MSG, StrConst.TITLE_OPER, "", msg);


                    ////停止服务
                    //netService.Stop();
                    //IISWebServer_.Stop();
                   
                    Almdb.RemoveAllAlm();


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            catch (Exception ex)
            {

            }
            toolStrip1.Enabled = true;
            //SleepDoEvents(3000);
            Thread.Sleep(3000);
            SetNotRunningState();
            Debug.WriteLine("CloseWaitingForm start");
            CloseWaitingForm();
            Debug.WriteLine("CloseWaitingForm end");
            GC.Collect();


        }
        //导入系统预设的点
        private void TspLoadDefaultVar_Click(object sender, EventArgs e)
        {
            IController currentCon = trvDevice.SelectedNode.Tag as IController;

            if (currentCon != null && currentCon.ChannelObject != null)
            {
                IChannel currentChan = currentCon.ChannelObject;
                if (currentChan.Plugin.ImportVariable(currentCon))
                {
                    foreach (IVariable var in currentCon.VarList)
                    {
                        if (var.ID == "")
                            var.ID = Rtdb.GetUniqueVariableID(currentCon);
                    }
                    RefreshListViewDev();
                }
                if (trvDevice.SelectedNode.Text != currentCon.Name)
                {
                    trvDevice.SelectedNode.Text = currentCon.Name;
                }
            }


        }


        private void tspMenuServerSetup_Click(object sender, EventArgs e)
        {
            FormConfig frm = new FormConfig();
            frm.ShowDialog();

        }


        private void tspAutoLoad_Click(object sender, EventArgs e)
        {
            ServerConfig.FileAutoLoad = tspAutoLoad.Checked;
            ServerConfig.saveToFile();
        }

        private void tspAutoComm_Click(object sender, EventArgs e)
        {
            ServerConfig.AutoComm = tspAutoComm.Checked;
            ServerConfig.saveToFile();
        }

        private void 通讯CToolStripMenuItem_Click(object sender, EventArgs e)
        {



        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {



        }

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnNew.PerformClick();
        }

        private void ToolStripMenuItemClose_Click(object sender, EventArgs e)
        {
            if (ServerConfig.AutoComm)
            {
                if (ServerConfig.RunningState == ServerStateEnum.RUNING)
                    _DriverMng.DisConnectedDrive();
            }
            else
            {
                if (ServerConfig.RunningState == ServerStateEnum.RUNING)
                {
                    MessageBox.Show("请停止运行后，再关闭系统！");
                    return;
                }
                if (currentPrejectPath != "")
                {
                    if (MessageBox.Show("是否保存数据文件？", "保存提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
                    {
                        btnSave.PerformClick();
                    }
                }
            }
            this.ForceClose();
        }

        private void 关于软件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 about = new AboutBox1();
            about.ShowDialog();
        }

        private void 软件注册ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormReg frm = new FormReg();
            frm.ShowDialog();
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            if (IsAdministrator())
            {
                if (ServiceStatus())
                {
                    MessageBox.Show("IO采集服务器已经在Windows服务中启动，请关闭服务后再操作本窗体程序");
                    btnStartService.Enabled = false;
                    btnStopService.Enabled = true;
                }
                else
                {
                    btnStartService.Enabled = true;
                    btnStopService.Enabled = false;
                }
            }
            //Mngalgo rsa = new Mngalgo();
            //if ((rsa.CheckSoftRegCode("GHIBMS") == false) && (System.DateTime.Now > DateTime.Parse("2013-2-20 0:0:0")))
            //{
            //    MessageBox.Show("软件试用期满！");
            //    Process current = Process.GetCurrentProcess();
            //    current.Kill();
            //}
            FormIsShow = true;

            if (ServerConfig.AutoComm)
                this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Rtdb.ChanList.Count.ToString());
        }

        private void tspItemChangeVarValue_Click(object sender, EventArgs e)
        {
            ListViewItem item = ListViewDevice.GetFirstSelectItem();
            if (item != null)
            {
                IVariable currentVar = item.Tag as IVariable;
                FormWriteValue frm = new FormWriteValue();

                frm.SetText(currentVar.Name, currentVar.ValueType.ToString(), currentVar.Value.ToString());
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    // if (currentVar.Value.ToString() != frm.Newvalue.Trim()) //2016/4/20修改，可以重复写值
                    {

                        if (currentVar.ReadOnly == false)
                        {
                            currentVar.WriteValue(frm.Newvalue.Trim());
                            //if (Write2Device(currentVar.Name, frm.Newvalue.Trim()))
                            //{
                            //    //currentVar.Value = frm.Newvalue;
                            //    //UpdataLsvValueByName(currentVar.Name, currentVar.Value);
                            //}
                        }

                    }
                }
            }
        }

        //private void UpdataLsvDev(IVariable var)
        //{
        //    ListViewItem item = ListViewDevice.SearchListViewItemByText(var.Name);
        //        if (item != null)
        //        {
        //            item.SubItems[2].Text = var.Address;

        //            ListViewDevice.Invalidate(item.Bounds); //局部更新

        //            //ListViewDevice.Refresh();
        //        }
        //}

        //编辑状态下，propertyGrid1中改变属性后刷新设备列表
        private void UpdataLsvDev(object[] objects)
        {
            foreach (object o in objects)
            {
                if (o is IChannel)
                {
                    IChannel ch = o as IChannel;
                    ListViewItem lsvTagItem = ListViewDevice.SearchListViewItemByText(ch.ID);
                    if (lsvTagItem != null)
                    {
                        if (ch.Enable)
                        {
                            lsvTagItem.ImageIndex = DevTreeImage.ChannelNormal;
                        }
                        else
                        {
                            lsvTagItem.ImageIndex = DevTreeImage.ChannelDisable;

                        }
                        lsvTagItem.SubItems[1].Text = ch.ID;
                        lsvTagItem.SubItems[2].Text = ch.Name;
                        lsvTagItem.SubItems[3].Text = ch.Description;
                        lsvTagItem.SubItems[4].Text = ch.ProtocolName;
                        lsvTagItem.SubItems[5].Text = ch.CommType.ToString();
                        lsvTagItem.SubItems[6].Text = ch.Active.ToString();

                    }

                }
                else if (o is IController)
                {
                    IController con = o as IController;
                    ListViewItem lsvTagItem = ListViewDevice.SearchListViewItemByText(con.ID);
                    if (lsvTagItem != null)
                    {
                        if (con.Enable)
                            lsvTagItem.ImageIndex = DevTreeImage.ControllNormal;
                        else
                            lsvTagItem.ImageIndex = DevTreeImage.ControllDisable;
                        lsvTagItem.SubItems[1].Text = con.ID;
                        lsvTagItem.SubItems[2].Text = con.Name;
                        lsvTagItem.SubItems[3].Text = con.Description;
                        lsvTagItem.SubItems[4].Text = con.Address.ToString();
                        lsvTagItem.SubItems[5].Text = con is ITcpController ? (con as ITcpController).IpAddress : "";
                        lsvTagItem.SubItems[6].Text = con is ITcpController ? (con as ITcpController).MacAddress : "";
                        lsvTagItem.SubItems[7].Text = con.Active.ToString();

                    }
                }
                else if (o is IVariable)
                {
                    IVariable var = o as IVariable;
                    ListViewItem lsvTagItem = ListViewDevice.SearchListViewItemByText(var.ID);
                    if (lsvTagItem != null)
                    {
                        if (var.Enable)
                        {
                            lsvTagItem.ImageIndex = DevTreeImage.VariableNormal;
                        }
                        else
                        {
                            lsvTagItem.ImageIndex = DevTreeImage.VariableDisable;

                        }
                        lsvTagItem.SubItems[1].Text = var.ID;
                        lsvTagItem.SubItems[2].Text = var.Name;
                        lsvTagItem.SubItems[3].Text = var.Address.ToString();
                        lsvTagItem.SubItems[4].Text = var.ValueType.ToString();
                        lsvTagItem.SubItems[5].Text = var.Value.ToString();
                        lsvTagItem.SubItems[6].Text = var.Unit;
                        lsvTagItem.SubItems[7].Text = var.DateStamp.ToString();
                        lsvTagItem.SubItems[8].Text = ((COMM_QUALITY_STATUS)var.Quality).ToString();
                        lsvTagItem.SubItems[9].Text = var.Counter.ToString();
                        lsvTagItem.SubItems[10].Text = var.Description;
                        // lsvTagItem.SubItems[10].Text = var.Enable.ToString();


                        //ListViewDevice.Columns.Add("序  号", 80, HorizontalAlignment.Center);
                        //ListViewDevice.Columns.Add("名  称", 250, HorizontalAlignment.Center);
                        //ListViewDevice.Columns.Add("地  址", 100, HorizontalAlignment.Center);
                        //ListViewDevice.Columns.Add("类  型", 100, HorizontalAlignment.Center);
                        //ListViewDevice.Columns.Add("数  值", 100, HorizontalAlignment.Center);
                        //ListViewDevice.Columns.Add("单  位", 100, HorizontalAlignment.Center);
                        //ListViewDevice.Columns.Add("邮  戳", 150, HorizontalAlignment.Center);
                        //ListViewDevice.Columns.Add("品  质", 100, HorizontalAlignment.Center);
                        //ListViewDevice.Columns.Add("更  新", 100, HorizontalAlignment.Center);
                        //ListViewDevice.Columns.Add("描  述", 100, HorizontalAlignment.Center);

                    }
                }
                ListViewDevice.Invalidate();
            }
        }
        private void RefreshLsvVariable()
        {
            if (trvDevice.SelectedNode != null)
            {
                TreeNode o = trvDevice.SelectedNode;

                //当前控制器
                IController currentCon = o.Tag as IController;
                if (currentCon != null)
                {
                    if (ServerConfig.RunningState == ServerStateEnum.RUNING)
                    {
                        labelDevieInfo.Text = ">>通讯通道  >>控制器  >>设备控制点";
                        //虚模式显示点列表
                        ListViewVariable(o, currentCon.VarList);
                    }
                }
            }

        }
        //dong 2018/3/8修改为每秒刷新一次
        private void RefreshLsvVariable2()
        {
            if (trvDevice.SelectedNode != null)
            {
                TreeNode o = trvDevice.SelectedNode;

                TreeNode treenode = ListViewDevice.Tag as TreeNode;

                if (o != treenode)
                {
                    //当前控制器
                    IController currentCon = o.Tag as IController;
                    if (currentCon != null)
                    {
                        if (ServerConfig.RunningState == ServerStateEnum.RUNING)
                        {
                            labelDevieInfo.Text = ">>通讯通道  >>控制器  >>设备控制点";
                            //虚模式显示点列表
                            ListViewVariable(o, currentCon.VarList);
                        }
                    }
                }
                else
                {
                    //当前控制器
                    IController currentCon = o.Tag as IController;
                    if (currentCon != null)
                    {
                        if (ServerConfig.RunningState == ServerStateEnum.RUNING)
                        {
                            foreach (ListViewItem lsvTagItem in ListViewDevice.CurrentCacheItemsSource)
                            {
                                IVariable var = lsvTagItem.Tag as IVariable;
                                lsvTagItem.SubItems[5].Text = var.Value.ToString();
                                lsvTagItem.SubItems[7].Text = var.DateStamp.ToString();
                                lsvTagItem.SubItems[8].Text = ((COMM_QUALITY_STATUS)var.Quality).ToString();
                                lsvTagItem.SubItems[9].Text = var.Counter.ToString();

                            }
                            ListViewDevice.Invalidate();
                        }
                    }



                }
            }

        }

        private void iPM管理工具ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FormIPMFinder frm = new FormIPMFinder();
            //frm.ShowDialog();
        }



        private void honeyIPM调试工具ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //声明一个程序信息类   
            System.Diagnostics.ProcessStartInfo Info = new System.Diagnostics.ProcessStartInfo();
            //设置外部程序名   
            Info.FileName = "IPM调试工具.exe";

            //设置外部程序的启动参数（命令行参数）为test.txt   
            //Info.Arguments = "test.txt";
            //设置外部程序工作目录为    
            Info.WorkingDirectory = Application.StartupPath;

            //声明一个程序类   
            System.Diagnostics.Process Proc;
            try
            {
                //   
                //启动外部程序   
                //   
                Proc = System.Diagnostics.Process.Start(Info);
            }
            catch
            {
                // MessageBox.Show("系统找不到指定的程序文件");

                MessageBox.Show("系统找不到指定的程序文件", "注册", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

            }
        }


        /* private void tspLoadFormVar_Click(object sender, EventArgs e)
         {
             try
             {
                 LoadFormData();
                 if (Rtdb.DesignFormList == null) return;
                 iniMapTree();
                 trvForm.BeginUpdate();
                 foreach (TreeNode nod in trvForm.Nodes)
                 {
                     if (nod.Text == "画面变量")
                     {
                         nod.Nodes.Clear();
                         foreach (DesignForm frmGraphi in Rtdb.DesignFormList)
                         {
                             TreeNode child = new TreeNode();
                             child.Text = frmGraphi.FormName;
                             child.Tag = frmGraphi;
                             nod.Nodes.Add(child);
                             child.ImageIndex = 8;
                             child.SelectedImageIndex = 8;
                             foreach (GraphicAssociate gh in frmGraphi.GraphicList)
                             {
                                 TreeNode leaf = new TreeNode();
                                 leaf.ImageIndex = 4;
                                 leaf.SelectedImageIndex = 4;
                                 leaf.Text = gh.associateVar;
                                 leaf.Tag = gh;
                                 child.Nodes.Add(leaf);
                             }
                         }
                     }
                 }
                 trvForm.EndUpdate();
                 //设定变量的AlarmForm
                 AssociateFormVar();
             }
             catch (Exception ex)
             {
                 Logger.GetInstance().LogError(ex.ToString());
             }
         }
         //定位变位所在的Form
         private void AssociateFormVar()
         {
             foreach (DesignForm frmGraphi in Rtdb.DesignFormList)
             {
                 foreach (GraphicAssociate gh in frmGraphi.GraphicList)
                 {
                     IVariable var =Rtdb.GetVarByName(gh.associateVar);
                     if (var != null)
                         var.AssociateForm = frmGraphi.FormName;
                 }
             }
         }*/

        private void 导入变量ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //tspLoadFormVar_Click(sender,e);

        }

        private string GetVariableValueByName(string strVarName)
        {
            try
            {
                //读数据链表
                foreach (IChannel chan in Rtdb.ChanList)
                {
                    foreach (IController con in chan.ConList)
                    {
                        foreach (IVariable var in con.VarList)
                        {
                            if (var.Name == strVarName)
                            {
                                return var.Value.ToString();
                                // break;
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                //Logger.GetInstance().LogError(ex.ToString());
                Console.WriteLine(ex.ToString());
            }
            return "";
        }
        //public void AddOPCItem(string progName,string progID)
        //{

        //    //CreateNewTag(progID, progID, "OPC标签");

        //}
        //public void AddALLOPCItem(ListView list)
        //{

        //    int count = list.Items.Count;
        //    string selectedName;
        //    string selecteditemID;
        //    for (int i = 0; i < count; i++)
        //    {
        //        selectedName = list.Items[i].SubItems[0].Text;
        //        selecteditemID = list.Items[i].SubItems[1].Text;
        //        AddOPCItem(selectedName, selecteditemID);
        //    }

        //}

        private void tspItemShowKeybroad_Click(object sender, EventArgs e)
        {
            try
            {
                /*IController currentCon = trvDevice.SelectedNode.Tag as IController;
                ChannelInfo currentChan = currentCon.ChannelObject;
                if (currentChan == null && currentCon == null) return;
                if ((currentChan.ProtocolCode == string.BJ_HONEYWELL_IPM) && (currentCon != null))
                {
                    if ((currentChan.CommObject != null) && (currentChan.CommObject is FormIPMOCX))
                    {
                        FormIPMMonitor frm = new FormIPMMonitor();
                        frm.formIPMOCX = currentChan.CommObject as FormIPMOCX;
                        frm.CurrentMac = currentCon.MacAddress;
                        frm.CurrentHostName = currentCon.Name;
                        frm.formIPMOCX.EnableKeypad(currentCon.MacAddress, 1);
                        frm.ShowDialog();
                    }
                }*/
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }
        }

        private void MenuItemSentSms_Click(object sender, EventArgs e)
        {
            FormSentSMS frmSentSms = new FormSentSMS();
            frmSentSms.ShowDialog();

        }


        private void 项目管理PToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FormProjMng frm = new FormProjMng();
            //frm.ShowDialog();
        }
        private void cmbDeviceSort_TextChanged(object sender, EventArgs e)
        {
            //if (tabPageGenerally.Tag == null) return;
            //Variable var = tabPageGenerally.Tag as Variable;
            //if (cmbDeviceSort.Text.Trim().Length == 0 || (!cmbDeviceSort.Text.Contains(var.Controller.DeviceLabel.ToString())))
            //{
            //   MessageBox.Show("设备标签类型选择不正确，请重新选择！");
            //}
        }

        private void cmbDeviceSort_Leave(object sender, EventArgs e)
        {

        }

        private void menuSetRecorder_Click(object sender, EventArgs e)
        {
            if (bExistProject)
            {
                FormHistorySet frm = new FormHistorySet();
                frm.ShowDialog();
            }
        }
        private void menuHisRecorder_Click(object sender, EventArgs e)
        {
            if (bExistProject)
            {
                FormHistroyList frm = new FormHistroyList();
                frm.ShowDialog();
            }
        }
        uint RunTimeSun = 1;
        private void timerSysTime_Tick(object sender, EventArgs e)
        {

            //toolLabelTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (ServerConfig.RunningState == ServerStateEnum.RUNING)
            {
                if (FormIsShow)
                {
                    RefreshLsvVariable2();
                    //ListViewDevice.Invalidate();
                    lsvLog.Invalidate();
                    //trvDevice.Invalidate();
                    //运行时，最长显示窗体1小时
                    if (RunTimeSun % 3600 == 0)
                    {
                        this.Hide();
                        FormIsShow = false;

                    }
                }


            }
            //每4小时检查一次
            //if (RunTimeSun % 60 * 60 * 4 == 0)
            //{
            //    if (ServerConfig.RunningState == ServerStateEnum.RUNING)
            //    {
            //        foreach (IChannel chan in Rtdb.ChanList)
            //        {
            //            if (chan.Active)
            //            {
            //                if (DateTime.Now > new DateTime(2021, 4, 11))
            //                {
            //                    //非注册插件4小时停止运行
            //                    if (chan.RunTime > 4 * 60 * 60)
            //                    {
            //                        if (!PluginMng.IsActivePlug(chan.Plugin.PlugID))
            //                        {
            //                            chan.Stop();
            //                            chan.RunTime = 0;
            //                            string msg = string.Format("通讯通道因未激活被停止，通道名：{0}", chan.Name);
            //                            AddOperationLog(StrConst.SERVERITY_MSG, StrConst.TITLE_SYS, "", msg);
            //                            Logger.GetInstance().LogMsg(msg);
            //                        }
            //                    }
            //                }
            //                chan.RunTime++;
            //            }
            //        }
            //    }

            //    if (ServerConfig.WriteLog)
            //    {

            //        string usedCpu = Math.Round(CpuPerformanceCounter.NextValue(), 2, MidpointRounding.AwayFromZero).ToString() + "%";
            //        string usedMem = Math.Round(MemoPerformanceCounter.NextValue(), 2, MidpointRounding.AwayFromZero).ToString() + "G";

            //        Logger.GetInstance().LogMsg("IOServer运行情况报告，可用内存：" + usedMem + "    CPU占用:" + usedCpu);
            //    }

            //}


            if (!licOK) //试用版检查
            {

                if (RunTimeSun % 3600 * 4 == 0)
                {
                    //检查是否存在对应的加密锁
                    //检查是否超时
                    if (ExpireCheck())
                    {
                        if (licType == "0")
                        {

                            Logger.GetInstance().LogMsg("演示授权，试用时间到，软件强制关闭！");
                            Process current = Process.GetCurrentProcess();
                            current.Kill();
                        }
                    }
                }
                //每60秒检查一次
                if (RunTimeSun % 60 * 60 == 0)
                {
                    //注册提醒
                    uint Expires = (uint)3600 * 4 - RunTimeSun;
                    notifyIcon1.BalloonTipText = "试用版软件，请尽快注册！";
                    notifyIcon1.ShowBalloonTip(5000);

                }
            }
            if (MqttController.IsConnected())
            {
                if (tspMqttState.BackColor != Color.LightGreen)
                {
                    tspMqttState.BackColor = Color.LightGreen;
                    tspMqttState.Text = "在线";
                }
            }
            else
            {
                if (tspMqttState.BackColor != Color.Red)
                {
                    tspMqttState.BackColor = Color.Red;
                    tspMqttState.Text = "离线";
                }
            }

            if (ServerConfig.StandbyEnable)
            {
                tspStandbyActive.Text = "";
                if (tspStandbyState.BackColor != Color.LightGreen)
                {
                    tspStandbyState.BackColor = Color.LightGreen;
                    if (ServerConfig.StandbyMaster)
                    {
                        tspStandbyState.Text = "主机";

                    }
                    else
                    {
                        tspStandbyState.Text = "备机";

                    }
                }
                if (_DriverMng.StandbyActive)
                {
                    tspStandbyActive.Text = "运行";
                    tspStandbyActive.BackColor = Color.LightGreen;
                }
                else
                {
                    tspStandbyActive.Text = "待机";
                    tspStandbyActive.BackColor = Color.Red;
                }

            }
            else
            {
                if (tspStandbyState.BackColor != Color.Red)
                {
                    tspStandbyState.BackColor = Color.Red;
                    tspStandbyState.Text = "禁用";
                    tspStandbyActive.Text = "";
                }



            }


            if (ServerConfig.RunningState == ServerStateEnum.RUNING)
            {
                SetRunningState();
            }
            else
            {
                SetNotRunningState();
            }


            RunTimeSun++;
        }
        private bool Authentication()
        {
            bool bExpire = true;

            try
            {
                //读写注册信息

                string s = GetRegistData("EXP");
                string s0 = pubFun.Decode(s);
                if (s0 != "True")
                {

                    RSAHelper rsa = new RSAHelper();
                    string strDe = rsa.RSADecrypt(ServerConfig.Expire);
                    if (strDe != "")
                    {
                        DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();
                        dtFormat.ShortDatePattern = "yyyy-M-d";
                        try
                        {
                            DateTime exp = Convert.ToDateTime(strDe, dtFormat);


                            if (DateTime.Now > exp)
                            {

                                bExpire = true;  //试用期满
                                WTRegedit("EXP", pubFun.Encode("True"));
                            }
                            else
                                bExpire = false;
                        }
                        catch (Exception ex)
                        {
                            bExpire = true;  //试用期满
                            Logger.GetInstance().LogError("解密试用期出错！" + ex.ToString());
                        }
                    }
                    else
                    {
                        bExpire = true;
                        Logger.GetInstance().LogError("没正确解密试用期的开始日期！默认为试用期满");

                    }
                }

            }
            catch
            {
                bExpire = true;
            }


            //string s1 = "abc";
            //string s2 = "abc";
            User.CheckKey = false;// ytsoftkey.YCompareStringNoCase_2(s1, s2);

            RSAHelper rsa2 = new RSAHelper();

            if (!User.CheckKey && !User.SoftCode && bExpire)
                return false;
            else
                return true;
        }

        private void tspItemVariableIsEnable_Click(object sender, EventArgs e)
        {
            List<ListViewItem> selItems = ListViewDevice.GetSelectItem();
            if (selItems.Count > 0)
            {
                if (selItems[0].Tag is IVariable)
                {
                    IVariable var = selItems[0].Tag as IVariable;
                    if (var.Enable)
                    {
                        foreach (ListViewItem item in selItems)
                        {
                            (item.Tag as IVariable).Enable = false;
                            item.ImageIndex = DevTreeImage.VariableDisable;

                        }

                    }
                    else
                    {
                        foreach (ListViewItem item in selItems)
                        {
                            (item.Tag as IVariable).Enable = true;
                            item.ImageIndex = DevTreeImage.VariableNormal;

                        }

                    }
                }
            }
            ListViewDevice.Invalidate();

        }

        private void ListViewDeviceMouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (ServerConfig.RunningState == ServerStateEnum.STOPED)
                {
                    List<ListViewItem> items = ListViewDevice.GetSelectItem();
                    if (items.Count > 0)//选中 
                    {
                        tspCut.Enabled = true;
                        tspCopy.Enabled = true;
                        object[] tags = new object[items.Count];
                        for (int i = 0; i < items.Count; i++)
                        {
                            tags[i] = items[i].Tag;
                        }
                        propertyGrid1.SelectedObjects = tags;
                        object o = (ListViewDevice.Tag as TreeNode).Tag;

                        //定向快捷菜单
                        if (o is Project)
                        {
                            ShowMainTabPage(MainTabPage.None);

                        }
                        else if (o is IChannel)
                        {
                            ShowMainTabPage(MainTabPage.None);
                        }
                        else if (o is IController)
                        {
                            ShowMainTabPage(MainTabPage.Variable);
                            bSaveVariableEdit = false;
                            ShowVariableProperty(items[0].Tag as IVariable);
                            bSaveVariableEdit = true;

                        }
                    }
                    else
                    {
                        tspCut.Enabled = false;
                        tspCopy.Enabled = false;
                        propertyGrid1.SelectedObject = null;

                    }
                }
                else
                {
                    ShowMainTabPage(MainTabPage.Log);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }


        //复制
        private void MenuItemCopy_Click(object sender, EventArgs e)
        {
            ListViewDeviceCopy();

        }
        private void ListViewDeviceCopy()
        {
            bCut = false;
            if (ListViewDevice.Tag == null) return;
            List<ListViewItem> lst = ListViewDevice.GetSelectItem();
            if (lst.Count == 0) return;
            LisViewItemClipboard.Clear();
            foreach (ListViewItem item in lst)
                LisViewItemClipboard.Add(item);
            tspPaste.Enabled = true;

        }
        private void ListViewDeviceEnable()
        {
            if (ListViewDevice.Tag == null) return;
            List<ListViewItem> lst = ListViewDevice.GetSelectItem();
            if (lst.Count == 0) return;
            foreach (ListViewItem item in lst)
            {
                if (item.Tag is IChannel)
                {
                    ((IChannel)item.Tag).Enable = true;
                    item.ImageIndex = DevTreeImage.ChannelNormal;
                    TreeNode node = SearchTreeDev(item.SubItems[2].Text);
                    if (node != null)
                    {
                        node.ImageIndex = DevTreeImage.ChannelNormal;
                        node.SelectedImageIndex = DevTreeImage.ChannelNormal;
                    }
                }
                else if (item.Tag is IController)
                {
                    ((IController)item.Tag).Enable = true;
                    item.ImageIndex = DevTreeImage.ControllNormal;
                    TreeNode node = SearchTreeDev(item.SubItems[2].Text);
                    if (node != null)
                    {
                        node.ImageIndex = DevTreeImage.ControllNormal;
                        node.SelectedImageIndex = DevTreeImage.ControllNormal;
                    }

                }
                else if (item.Tag is IVariable)
                {
                    ((IVariable)item.Tag).Enable = true;
                    item.ImageIndex = DevTreeImage.VariableNormal;

                }

            }
            ListViewDevice.Invalidate();
            trvDevice.Invalidate();
        }
        private void ListViewDeviceDisable()
        {
            if (ListViewDevice.Tag == null) return;
            List<ListViewItem> lst = ListViewDevice.GetSelectItem();
            if (lst.Count == 0) return;
            foreach (ListViewItem item in lst)
            {
                if (item.Tag is IChannel)
                {
                    ((IChannel)item.Tag).Enable = false;
                    item.ImageIndex = DevTreeImage.ChannelDisable;
                    TreeNode node = SearchTreeDev(item.SubItems[2].Text);
                    if (node != null)
                    {
                        node.ImageIndex = DevTreeImage.ChannelDisable;
                        node.SelectedImageIndex = DevTreeImage.ChannelDisable;

                    }

                }
                else if (item.Tag is IController)
                {
                    ((IController)item.Tag).Enable = false;
                    item.ImageIndex = DevTreeImage.ControllDisable;
                    TreeNode node = SearchTreeDev(item.SubItems[2].Text);
                    if (node != null)
                    {
                        node.ImageIndex = DevTreeImage.ControllDisable;
                        node.SelectedImageIndex = DevTreeImage.ControllDisable;


                    }

                }
                else if (item.Tag is IVariable)
                {
                    ((IVariable)item.Tag).Enable = true;
                    item.ImageIndex = DevTreeImage.VariableDisable;

                }
            }
            ListViewDevice.Invalidate();
            trvDevice.Invalidate();

        }

        TreeNode SearchTreeDev(string nodetext)
        {
            TreeNode tnRet = null;
            foreach (TreeNode tn in trvDevice.Nodes)
            {
                tnRet = pubFun.FindNodeByText(tn, nodetext);
                if (tnRet != null) break;
            }
            return tnRet;

        }
        //剪切
        private void MenuItemCut_Click(object sender, EventArgs e)
        {
            ListViewDeviceCut();
        }
        private void ListViewDeviceCut()
        {
            if (ListViewDevice.Tag == null) return;
            List<ListViewItem> lst = ListViewDevice.GetSelectItem();
            if (lst.Count == 0)
            {
                //tspCut.Enabled = false;
                //tspCopy.Enabled = false;
                return;
            }
            LisViewItemClipboard.Clear();
            LisViewItemClipboard.AddRange(lst);
            LisviewDeviceDelete();
            tspCancel.Enabled = true;
            tspMenuPaste.Enabled = true;
            tspPaste.Enabled = true;
            bCut = true;

        }
        //ctrl+z 
        private void ListViewDeviceCutRecovery()
        {
            bCut = false;
            if (ListViewDevice.Tag == null || LisViewItemClipboard.Count == 0) return;
            TreeNode treenode = ListViewDevice.Tag as TreeNode;
            object flag = treenode.Tag;
            tspCancel.Enabled = false;


            //通道
            if (flag is Project)
            {
                if (LisViewItemClipboard[0].Tag is IChannel)
                {

                    foreach (ListViewItem item in LisViewItemClipboard)
                    {
                        IChannel chan = item.Tag as IChannel;
                        if (chan == null) return;
                        //检查通道ID是否重复
                        if (Rtdb.IsExistChannelID(chan.ID))
                            chan.ID = Rtdb.GetUniqueChannelID();
                        // IChannel newCh = (IChannel)ch.Clone();
                        //恢复通道节点
                        TreeNode nodeCH = new TreeNode(chan.Name);
                        nodeCH.Tag = chan;
                        if (chan.Enable)
                        {
                            nodeCH.ImageIndex = DevTreeImage.ChannelNormal;
                            nodeCH.SelectedImageIndex = DevTreeImage.ChannelNormal;
                        }
                        else
                        {
                            nodeCH.ImageIndex = DevTreeImage.ChannelDisable;
                            nodeCH.SelectedImageIndex = DevTreeImage.ChannelDisable;
                        }
                        treenode.Nodes.Add(nodeCH);
                        trvDevice.Invalidate();
                        //恢复控制器节点
                        foreach (IController con in chan.ConList)
                        {
                            //con.Name = Rtdb.CreateNameClone(con.Name);
                            //con.ChannelObject = newCh;
                            TreeNode nodeCon = new TreeNode(con.Name);
                            nodeCon.Tag = con;
                            if (con.Enable)
                            {
                                nodeCon.ImageIndex = DevTreeImage.ControllNormal;
                                nodeCon.SelectedImageIndex = DevTreeImage.ControllNormal;
                            }
                            else
                            {
                                nodeCon.ImageIndex = DevTreeImage.ControllDisable;
                                nodeCon.SelectedImageIndex = DevTreeImage.ControllDisable;
                            }
                            nodeCH.Nodes.Add(nodeCon);
                            //foreach (Variable var in con.VarList)
                            //{
                            //    var.Name = Rtdb.CreateNameClone(var.Name);
                            //    var.ControllerObject = con;
                            //}

                        }
                        //if (newCh != null)
                        //{
                        Rtdb.ChanList.Add(chan);
                        AddNewItem2ListView(chan);
                        // }

                    }
                }
            }

            else if (flag is IChannel)
            {
                if (LisViewItemClipboard[0].Tag is IController)
                {
                    IChannel chan = treenode.Tag as IChannel;
                    IController c = LisViewItemClipboard[0].Tag as IController;
                    if (c.ChannelObject.Name != chan.Name)
                    {
                        MessageBox.Show("不同通道内的控制器不能恢复！");
                        return;
                    }
                    foreach (ListViewItem item in LisViewItemClipboard)
                    {

                        IController newCon = item.Tag as IController;
                        //判断一下conID
                        if (Rtdb.IsExistControllID(chan, newCon.ID))
                            newCon.ID = Rtdb.GetUniqueControllID(chan);


                        chan.ConList.Add(newCon);

                        TreeNode node = new TreeNode(newCon.Name);
                        node.Tag = newCon;
                        if (newCon.Enable)
                        {
                            node.ImageIndex = DevTreeImage.ControllNormal;
                            node.SelectedImageIndex = DevTreeImage.ControllNormal;
                        }
                        else
                        {
                            node.ImageIndex = DevTreeImage.ControllDisable;
                            node.SelectedImageIndex = DevTreeImage.ControllNormal;
                        }
                        treenode.Nodes.Add(node);
                        trvDevice.Invalidate();
                        AddNewItem2ListView(newCon);
                    }
                }
            }

            else if (flag is IController)
            {
                if (LisViewItemClipboard[0].Tag is IVariable)
                {
                    IController con = treenode.Tag as IController;
                    IVariable v = LisViewItemClipboard[0].Tag as IVariable;
                    if (v.ControllerObject.ChannelObject.Name != con.ChannelObject.Name)
                    {
                        MessageBox.Show("不同通道内的变量不能恢复！");
                        return;
                    }

                    foreach (ListViewItem item in LisViewItemClipboard)
                    {
                        IVariable var = item.Tag as IVariable;

                        if (Rtdb.IsExistVariableID(con, var.ID))
                            var.ID = Rtdb.GetUniqueVariableID(con);

                        if (con.VarList.Contains(var))
                        {
                            IVariable newVar = var.Clone();
                            con.VarList.Add(var);
                            var.ControllerObject = con;
                            AddNewItem2ListView(var);
                        }
                        else
                        {
                            con.VarList.Add(var);
                            var.ControllerObject = con;
                            AddNewItem2ListView(var);
                        }
                    }

                }
            }
            LisViewItemClipboard.Clear();
            ListViewDevice.Invalidate();


        }
        //粘贴
        private void MenuItemPaste_Click(object sender, EventArgs e)
        {
            ListViewDevicePaste();
        }
        private void ListViewDevicePaste()
        {
            bCut = false;
            if (ListViewDevice.Tag == null || LisViewItemClipboard.Count == 0) return;
            TreeNode treenode = ListViewDevice.Tag as TreeNode;
            object flag = treenode.Tag;
            tspCancel.Enabled = false;


            //通道
            if (flag is Project)
            {
                if (LisViewItemClipboard[0].Tag is IChannel)
                {
                    //通道复制除了通道名称和ID不能相同，通道下的控制器和变量名称和ID不变
                    foreach (ListViewItem item in LisViewItemClipboard)
                    {
                        IChannel ch = item.Tag as IChannel;
                        IChannel newCh = (IChannel)ch.Clone();
                        newCh.ConList.Clear();
                        //newCh.ID = Rtdb.GetUniqueChannelCloneID(pubFun.checkUrl(pubFun.GetFirstPinYin(pubFun.ChineseTONumber(pubFun.ToDBC(newCh.Name)))));
                        newCh.ID = Rtdb.GetUniqueChannelID();
                        newCh.Name = Rtdb.GetUniqueCloneNameChan(ch.Name);

                        Rtdb.ChanList.Add(newCh);
                        AddNewItem2ListView(newCh);

                        TreeNode nodeCH = new TreeNode(newCh.Name);
                        nodeCH.Tag = newCh;
                        if (newCh.Enable)
                        {
                            nodeCH.ImageIndex = DevTreeImage.ChannelNormal;
                            nodeCH.SelectedImageIndex = DevTreeImage.ChannelNormal;
                        }
                        else
                        {
                            nodeCH.ImageIndex = DevTreeImage.ChannelDisable;
                            nodeCH.SelectedImageIndex = DevTreeImage.ChannelDisable;
                        }
                        treenode.Nodes.Add(nodeCH);
                        trvDevice.Invalidate();
                        //复制通道下面的对象
                        foreach (IController con in ch.ConList)
                        {

                            IController newCon = con.Clone();
                            newCon.Name = con.Name; //clone名称相同
                            newCon.ID = con.ID;
                            newCon.ChannelObject = newCh;
                            TreeNode nodeCon = new TreeNode(con.Name);
                            nodeCon.Tag = newCon;
                            if (con.Enable)
                            {
                                nodeCon.ImageIndex = DevTreeImage.ControllNormal;
                                nodeCon.SelectedImageIndex = DevTreeImage.ControllNormal;
                            }
                            else
                            {
                                nodeCon.ImageIndex = DevTreeImage.ControllDisable;
                                nodeCon.SelectedImageIndex = DevTreeImage.ControllDisable;
                            }
                            newCh.ConList.Add(newCon);
                            nodeCH.Nodes.Add(nodeCon);
                            foreach (IVariable var in con.VarList)
                            {
                                IVariable newVar = var.Clone();
                                newVar.Name = var.Name;
                                newVar.ID = var.ID;
                                newVar.Address = var.Address;
                                newVar.ControllerObject = newCon;
                                newCon.VarList.Add(newVar);
                            }

                        }
                    }
                }
            }

            else if (flag is IChannel)
            {
                //剪贴板内是控制器
                //控制器复制同一通道下控制器名称和ID不能相同
                if (LisViewItemClipboard[0].Tag is IController)
                {
                    IChannel chan = treenode.Tag as IChannel; //当前选中的

                    IController c = LisViewItemClipboard[0].Tag as IController;
                    if (c.ChannelObject.GetType().Name != chan.GetType().Name)
                    {
                        MessageBox.Show("不同类型通道内的控制器不能复制！");
                        return;
                    }


                    foreach (ListViewItem item in LisViewItemClipboard)
                    {

                        IController con = item.Tag as IController;
                        IController newCon = con.Clone();
                        newCon.ChannelObject = chan;


                        if (!Rtdb.IsExistControllID(chan, con.ID))
                            newCon.ID = con.ID;
                        else
                            newCon.ID = Rtdb.GetUniqueControllID(chan);

                        newCon.Name = Rtdb.GetUniqueCloneNameCtrl(chan, con.Name);

                        chan.ConList.Add(newCon);
                        if (!chan.ConList.Contains(con))
                        {
                            newCon.Address = con.Address;
                        }
                        TreeNode node = new TreeNode(newCon.Name);
                        node.Tag = newCon;
                        if (newCon.Enable)
                        {
                            node.ImageIndex = DevTreeImage.ControllNormal;
                            node.SelectedImageIndex = DevTreeImage.ControllNormal;
                        }
                        else
                        {
                            node.ImageIndex = DevTreeImage.ControllDisable;
                            node.SelectedImageIndex = DevTreeImage.ControllNormal;
                        }
                        treenode.Nodes.Add(node);
                        trvDevice.Invalidate();
                        AddNewItem2ListView(newCon);
                        //同步复制变量
                        foreach (IVariable var in con.VarList)
                        {
                            IVariable newVar = var.Clone();
                            newVar.Address = var.Address;
                            newVar.ID = var.ID;
                            newVar.Name = var.Name;
                            newVar.ControllerObject = newCon;
                            //if (!Rtdb.IsExistVariableID(newCon,var.ID))
                            // {
                            //       newVar.ID = var.ID;
                            //}else
                            //    newVar.ID = Rtdb.GetUniqueVariableID(newCon);
                            //newVar.Name = Rtdb.GetUniqueCloneNameVar(con, var.Name);
                            newCon.VarList.Add(newVar);

                        }

                    }
                }
            }

            else if (flag is IController)
            {
                //变量的复制 不同控制器下的ID可以相同
                if (LisViewItemClipboard[0].Tag is IVariable)
                {
                    IController con = treenode.Tag as IController;
                    IVariable v = LisViewItemClipboard[0].Tag as IVariable;
                    //string s = v.ControllerObject.GetType().Name;
                    //Console.Write(s);
                    if (v.ControllerObject.GetType().Name != con.GetType().Name)
                    {
                        //不同类型通道内的数据点不能复制
                        MessageBox.Show("不同类型控制器内的数据点不能复制！");
                        return;
                    }

                    foreach (ListViewItem item in LisViewItemClipboard)
                    {
                        IVariable var = item.Tag as IVariable;

                        IVariable newVar = var.Clone();
                        newVar.Address = var.Address;

                        newVar.ControllerObject = con;
                        if (!Rtdb.IsExistVariableID(con, var.ID))
                            newVar.ID = var.ID;
                        else
                            newVar.ID = Rtdb.GetUniqueVariableID(con);
                        // newVar.ID = Rtdb.GetUniqueVariableCloneID(pubFun.checkUrl(pubFun.GetFirstPinYin(pubFun.ChineseTONumber(pubFun.ToDBC(newVar.Name)))));
                        newVar.Name = Rtdb.GetUniqueCloneNameVar(con, var.Name);
                        con.VarList.Add(newVar);
                        AddNewItem2ListView(newVar);
                    }
                }
            }
            ListViewDevice.Invalidate();


        }
        //剪切
        private void tspCut_Click(object sender, EventArgs e)
        {
            if (ServerConfig.RunningState == ServerStateEnum.STOPED)
                MenuItemCut_Click(sender, e);
        }

        private void tspCopy_Click(object sender, EventArgs e)
        {
            if (ServerConfig.RunningState == ServerStateEnum.STOPED)
                MenuItemCopy_Click(sender, e);
        }

        private void tspPaste_Click(object sender, EventArgs e)
        {
            if (ServerConfig.RunningState == ServerStateEnum.STOPED)
                MenuItemPaste_Click(sender, e);
        }
        #endregion

        #region 信息记录

        void LogErrToFile(String s)
        {
            try
            {
                FileInfo fi = new FileInfo(Application.StartupPath + "\\err_" + DateTime.Now.ToString("yyyy_MM_dd") + ".log");
                StreamWriter sw = fi.AppendText();
                sw.WriteLine("");
                sw.WriteLine(DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss]"));
                sw.WriteLine(s);
                sw.Close();
            }
            catch { }
        }
        //记录程序异常
        void LogErrToFile(String source, String msg, String content)
        {
            try
            {
                FileInfo fi = new FileInfo(Application.StartupPath + "\\err_" + pubFun.DateStr + ".log");
                StreamWriter sw = fi.AppendText();
                sw.WriteLine("");
                sw.WriteLine(pubFun.DateTimeStr);
                sw.WriteLine(source);
                sw.WriteLine(msg);
                sw.WriteLine(content);
                sw.Close();
            }
            catch { }
        }

        void LogDedugToFile(String s)
        {
            try
            {
                FileInfo fi = new FileInfo(Application.StartupPath + "\\debug_" + DateTime.Now.ToString("yyyy_MM_dd") + ".log");
                StreamWriter sw = fi.AppendText();
                sw.WriteLine(DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss]") + s);
                sw.Close();
            }
            catch { }
        }


        #endregion

        #region 设备程序状态
        private void SetBusyingState()
        {
            //ServerConfig.RunningState = true;
            btnStartComm.Enabled = false;
            btnStopComm.Enabled = false;
            btnNew.Enabled = false;
            btnOpen.Enabled = false;
            btnSave.Enabled = false;
            //ServerConfig.RunningState = true;

            打开OToolStripMenuItem.Enabled = false;
            新建ToolStripMenuItem.Enabled = false;
            另存ToolStripMenuItem.Enabled = false;

            menuSetRecorder.Enabled = false;
            tspAutoComm.Enabled = false;
            tspAutoLoad.Enabled = false;
            tspMenuServerSetup.Enabled = false;
            保存ToolStripMenuItem.Enabled = false;
            menuHisRecorder.Enabled = false;

            tspItemChanExportExecl.Enabled = false;
            tspItemConExportExcel.Enabled = false;
            tspTtemVarExportExcel.Enabled = false;
            tspItemCamExportExcel.Enabled = false;

        }
        private void SetRunningState()
        {

            //ServerConfig.RunningState = true;
            btnStartComm.Enabled = false;
            btnStopComm.Enabled = true;
            btnNew.Enabled = false;
            btnOpen.Enabled = false;
            btnSave.Enabled = false;
            btnStartService.Enabled = false;
            打开OToolStripMenuItem.Enabled = false;
            新建ToolStripMenuItem.Enabled = false;
            另存ToolStripMenuItem.Enabled = false;

            menuSetRecorder.Enabled = false;
            tspAutoComm.Enabled = false;
            tspAutoLoad.Enabled = false;
            tspMenuServerSetup.Enabled = false;
            保存ToolStripMenuItem.Enabled = false;
            menuHisRecorder.Enabled = false;

            tspCut.Enabled = false;
            tspCopy.Enabled = false;
            tspPaste.Enabled = false;
            tspCancel.Enabled = false;
            tspItemChanExportExecl.Enabled = false;
            tspItemConExportExcel.Enabled = false;
            tspTtemVarExportExcel.Enabled = false;
            tspItemCamExportExcel.Enabled = false;
            expandableSplitter3.Visible = false;
            propertyGrid1.Visible = false;
            tspStarServer.Enabled = false;
            tspStopServer.Enabled = true;
            btnUploadToCloud.Enabled = false;
            tspDowloadFromCloud.Enabled = false;
            //btnUploadDB.Enabled = false;
            //tspTimeTask.Enabled = false;
            TspConfigTimeZone.Enabled = false;
            //ToolStripMenuItemCloud.Enabled = false;
            ToolStripMenuItemMqtt.Enabled = false;
            tspCommPlugin.Enabled = false;
            foreach (ToolStripItem item in FileToolStripMenuItem.DropDownItems)
            {
                if (!item.Text.Contains("退出"))
                {
                    item.Enabled = false;
                }
            }


        }
        private void SetNotRunningState()
        {
            //ServerConfig.RunningState = false;
            btnStartComm.Enabled = true;
            btnStopComm.Enabled = false;
            btnNew.Enabled = true;
            btnOpen.Enabled = true;
            btnSave.Enabled = true;

            打开OToolStripMenuItem.Enabled = true;
            新建ToolStripMenuItem.Enabled = true;
            另存ToolStripMenuItem.Enabled = true;

            menuSetRecorder.Enabled = true;
            tspAutoComm.Enabled = true;
            tspAutoLoad.Enabled = true;
            tspMenuServerSetup.Enabled = true;
            保存ToolStripMenuItem.Enabled = true;
            menuHisRecorder.Enabled = true;

            tspItemChanExportExecl.Enabled = true;
            tspItemConExportExcel.Enabled = true;
            tspTtemVarExportExcel.Enabled = true;
            tspItemCamExportExcel.Enabled = true;

            tspCut.Enabled = true;
            tspCopy.Enabled = true;
            tspPaste.Enabled = true;
            tspCancel.Enabled = true;
            expandableSplitter3.Visible = true;
            propertyGrid1.Visible = true;
            tspStarServer.Enabled = true;
            tspStopServer.Enabled = false;
            btnUploadToCloud.Enabled = true;
            tspDowloadFromCloud.Enabled = true;
            //btnUploadDB.Enabled = true;
            tspTimeTask.Enabled = true;
            TspConfigTimeZone.Enabled = true;
            //ToolStripMenuItemCloud.Enabled = true;
            tspCommPlugin.Enabled = true;
            ToolStripMenuItemMqtt.Enabled = true;
            btnStartService.Enabled = true;
            foreach (ToolStripItem item in FileToolStripMenuItem.DropDownItems)
            {
                if (!item.Text.Contains("退出"))
                {
                    item.Enabled = true;
                }
            }
            if(IsAdministrator()&&ServiceStatus())
            {
                btnStartService.Enabled = false;
                btnStopService.Enabled = true;
            }else
            {
                btnStartService.Enabled = true;
                btnStopService.Enabled = false;
            }

        }
        #endregion

        #region TabMain运行和配置状态切换

        /// <summary>
        /// 属性选项卡配置
        /// </summary>
        /// <param name="ObjectDisply">显示对象 </param>
        private void ShowMainTabPage(MainTabPage page)
        {

            switch (page)
            {

                case MainTabPage.Variable:
                    if (!tabMain.TabPages.Contains(tabPageValueDesc))
                    {

                        tabPageValueDesc.Parent = tabMain;
                        tabPageAlarm.Parent = tabMain;
                        tabPageAction.Parent = tabMain;
                        tabPageVideo.Parent = tabMain;
                        tabPageSMS.Parent = tabMain;
                        //tabMain.TabPages.Add(tabPageValueDesc);
                        //tabMain.TabPages.Add(tabPageAlarm);
                        //tabMain.TabPages.Add(tabPageAction);
                        //tabMain.TabPages.Add(tabPageVideo);
                        //tabMain.TabPages.Add(tabPageSMS);
                    }
                    break;
                default:
                    tabPageValueDesc.Parent = null;
                    tabPageAlarm.Parent = null;
                    tabPageAction.Parent = null;
                    tabPageVideo.Parent = null;
                    tabPageSMS.Parent = null;
                    break;
            }
        }
        #endregion

        #region 点的属性页

        /// <summary>
        /// 显示点的详细
        /// </summary>
        /// <param name="var"></param>
        private void ShowVariableProperty(IVariable var)
        {
            try
            {

                ClearVariableGrid();
                //事件列表
                List<string> strList = new List<string>();

                foreach (VariableTrigger v in var.WayList)
                {
                    strList.Add(v.Express);
                }
                ////矩阵列表
                //List<string> matList = new List<string>();
                //foreach (IChannel cha in Rtdb.ChanList)
                //{
                //    foreach (IController con in cha.ConList)
                //    {
                //        if (con is IMatrixController)
                //            matList.Add(con.Name);
                //    }
                //}
                //摄像机列表
                List<string> camList = new List<string>();
                camList.Add("");
                foreach (IChannel cha in Rtdb.ChanList)
                {
                    if (cha is ICamChannel)
                    {
                        foreach (IController con in cha.ConList)
                        {
                            foreach (IVariable c in con.VarList)
                                camList.Add(c.ID);
                        }
                    }
                }
                //监视器列表
                List<string> monList = new List<string>();
                monList.Add("");
                foreach (IChannel cha in Rtdb.ChanList)
                {
                    if (cha is IMonitorChannel)
                    {
                        foreach (IController con in cha.ConList)
                        {
                            foreach (IVariable c in con.VarList)
                                monList.Add(c.ID);
                        }
                    }
                }
                //时限列表
                List<string> timezoneList = new List<string>();
                timezoneList.Add("");
                foreach (TimeZoneClt z in TimeZoneDb.TimeZoneList)
                {
                    timezoneList.Add(z.ZoneName);
                }

                SourceGrid.Cells.Editors.ComboBox comboStandard;
                comboStandard = new SourceGrid.Cells.Editors.ComboBox(typeof(string), strList.ToArray(), false);
                comboStandard.KeyPress += pubFun.NoKey_KeyPress;

                //SourceGrid.Cells.Editors.ComboBox combMatrix;
                //combMatrix = new SourceGrid.Cells.Editors.ComboBox(typeof(string), matList.ToArray(), false);
                //combMatrix.KeyPress += pubFun.NoKey_KeyPress;

                SourceGrid.Cells.Editors.ComboBox combCam;
                combCam = new SourceGrid.Cells.Editors.ComboBox(typeof(string), camList.ToArray(), false);
                combCam.KeyPress += pubFun.NoKey_KeyPress;


                SourceGrid.Cells.Editors.ComboBox combMon;
                combMon = new SourceGrid.Cells.Editors.ComboBox(typeof(string), monList.ToArray(), false);
                combMon.KeyPress += pubFun.NoKey_KeyPress;

                SourceGrid.Cells.Editors.ComboBox comboExpressType;
                comboExpressType = new SourceGrid.Cells.Editors.ComboBox(typeof(string), Enum.GetNames(typeof(EventTypeEnum)), false);
                comboExpressType.KeyPress += pubFun.NoKey_KeyPress;


                SourceGrid.Cells.Editors.ComboBox combTimeZone;
                combTimeZone = new SourceGrid.Cells.Editors.ComboBox(typeof(string), timezoneList.ToArray(), false);
                combTimeZone.KeyPress += pubFun.NoKey_KeyPress;

                List<string> smsTypeList = new List<string>();
                smsTypeList.Add("短信");
                smsTypeList.Add("语音");
                smsTypeList.Add("通知");
                smsTypeList.Add("邮件");
                SourceGrid.Cells.Editors.ComboBox combTypesms;
                combTypesms = new SourceGrid.Cells.Editors.ComboBox(typeof(string), smsTypeList.ToArray(), false);
                combTypesms.KeyPress += pubFun.NoKey_KeyPress;


                foreach (VariableDesc desc in var.DescList)
                {
                    int n = gridStateDesc.RowsCount;
                    gridStateDesc.Rows.Insert(n);
                    gridStateDesc[n, 0] = new SourceGrid.Cells.Cell(desc.StateValue, typeof(string));
                    gridStateDesc[n, 1] = new SourceGrid.Cells.Cell(desc.StateDesc, typeof(string));

                }

                SourceGrid.Cells.Editors.ComboBox comboPriority;
                string[] enumPriorty = new string[] { _AlarmLevel.普通报警.ToString(), _AlarmLevel.严重报警.ToString(), _AlarmLevel.紧急报警.ToString() };
                comboPriority = new SourceGrid.Cells.Editors.ComboBox(typeof(string), enumPriorty, false);
                comboPriority.KeyPress += pubFun.NoKey_KeyPress;

                SourceGrid.Cells.Editors.ComboBox combSplit;
                uint[] enumSplit = new uint[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
                combSplit = new SourceGrid.Cells.Editors.ComboBox(typeof(uint), enumSplit, false);
                combSplit.KeyPress += pubFun.NoKey_KeyPress;

                //SourceGrid.Cells.Editors.TextBox txt = new SourceGrid.Cells.Editors.TextBox(typeof(string));
                // txt.KeyPress += pubFun.NubOnly_KeyPress;

                foreach (VariableTrigger way in var.WayList)
                {
                    int n = gridEventTrigger.RowsCount;
                    gridEventTrigger.Rows.Insert(n);
                    if (way.Priority > 2)
                        //兼容之前的Priority 0-7级，现在改为1,2,3
                        if (way.Priority > 3)
                        {

                        }

                    gridEventTrigger[n, 0] = new SourceGrid.Cells.Cell(way.Express);
                    gridEventTrigger[n, 1] = new SourceGrid.Cells.Cell(((EventTypeEnum)way.EventType).ToString(), comboExpressType);
                    gridEventTrigger[n, 2] = new SourceGrid.Cells.Cell(way.EventDesc, typeof(string));
                    gridEventTrigger[n, 3] = new SourceGrid.Cells.Cell(((_AlarmLevel)way.Priority).ToString(), comboPriority);
                    gridEventTrigger[n, 4] = new SourceGrid.Cells.Cell(way.ScriptText);
                    gridEventTrigger[n, 5] = new SourceGrid.Cells.Cell(way.TimZone, combTimeZone);

                }
                foreach (VariableAction act in var.ActionList)
                {
                    int n = gridAlarmAction.RowsCount;
                    gridAlarmAction.Rows.Insert(n);
                    gridAlarmAction[n, 0] = new SourceGrid.Cells.Cell(act.Way, comboStandard);
                    gridAlarmAction[n, 1] = new SourceGrid.Cells.Cell(act.VarTarget);
                    gridAlarmAction[n, 2] = new SourceGrid.Cells.Cell(act.VarValue, typeof(int));
                    gridAlarmAction[n, 3] = new SourceGrid.Cells.Cell(act.Description, typeof(string));
                }


                foreach (VariableVideo vdo in var.VideoList)
                {
                    int n = gridAlarmVideo.RowsCount;
                    gridAlarmVideo.Rows.Insert(n);

                    gridAlarmVideo[n, 0] = new SourceGrid.Cells.Cell(vdo.Way, comboStandard);
                    gridAlarmVideo[n, 1] = new SourceGrid.Cells.Cell(vdo.MonName, combMon);
                    gridAlarmVideo[n, 2] = new SourceGrid.Cells.Cell(vdo.SubVideoOut, combSplit);
                    gridAlarmVideo[n, 3] = new SourceGrid.Cells.Cell(vdo.CamID, combCam);
                    gridAlarmVideo[n, 4] = new SourceGrid.Cells.Cell(vdo.PreSetID, typeof(int));

                }


                foreach (VariableSMS sms in var.SmsList)
                {
                    int n = gridAlarmSMS.RowsCount;
                    gridAlarmSMS.Rows.Insert(n);

                    gridAlarmSMS[n, 0] = new SourceGrid.Cells.Cell(sms.Way, comboStandard);
                    gridAlarmSMS[n, 1] = new SourceGrid.Cells.Cell(sms.Phone, typeof(string));
                    gridAlarmSMS[n, 2] = new SourceGrid.Cells.Cell(sms.Msg, typeof(string));
                    string s = "";
                    if (sms.Type == 0)
                        s = "短信";
                    else if (sms.Type == 1)
                        s = "语音";
                    else if (sms.Type == 2)
                        s = "通知";
                    else
                        s = "邮件";
                    gridAlarmSMS[n, 3] = new SourceGrid.Cells.Cell(s, combTypesms);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Logger.GetInstance().LogError(ex.ToString());

            }
        }

        //点的描述属性编辑
        private void VariableEdit_SaveChanged(object sender, EventArgs e)
        {
            try
            {
                List<ListViewItem> CurrentEditVarList = ListViewDevice.GetSelectItem();
                if (((Control)sender).Tag.ToString() == "1")
                {
                    string name = (sender as Control).Name;
                    /*
                    //Console.WriteLine("OnChanged:"+name);
                    #region start switch
                    switch (name)
                    {
                        case "txtVariableDesc":
                            foreach (ListViewItem item in CurrentEditVarList)
                            {
                                IVariable var = item.Tag as IVariable;
                                var.Description = txtVariableDesc.Text;
                            }
                            break;
                        case "cmbVariableSort":
                            foreach (ListViewItem item in CurrentEditVarList)
                            {
                                IVariable var = item.Tag as IVariable;
                                var.DeviceLabel = cmbVariableSort.Text;
                          
                            }
                            break;
                        case "cmbVariableArea":
                            foreach (ListViewItem item in CurrentEditVarList)
                            {
                                IVariable var = item.Tag as IVariable;
                                var.Area = cmbVariableArea.Text;
                            }
                            break;
                        case "txtVariableAddress":
                            foreach (ListViewItem item in CurrentEditVarList)
                            {
                                IVariable var = item.Tag as IVariable;
                                var.Address = txtVariableAddress.Text;
                                 UpdataLsvDev(var);
                            }
                            break;
                      
                        case "txtVariableLevel":
                            foreach (ListViewItem item in CurrentEditVarList)
                            {
                                IVariable var = item.Tag as IVariable;
                                var.OperLevel =pubFun.IsInt(txtVariableLevel.Text,0);
                            }
                            break;
                        case "txtVariableUnit":
                            foreach (ListViewItem item in CurrentEditVarList)
                            {
                                IVariable var = item.Tag as IVariable;
                                var.Unit = txtVariableUnit.Text;
                            }
                            break;
                        case "chkVariableEnable":
                            foreach (ListViewItem item in CurrentEditVarList)
                            {
                                IVariable var = item.Tag as IVariable;
                                var.Enable = chkVariableEnable.Checked;
                            }
                            break;
                        case "chkReadOnly":
                            foreach (ListViewItem item in CurrentEditVarList)
                            {
                                IVariable var = item.Tag as IVariable;
                                var.ReadOnly = chkReadOnly.Checked;
                            }
                            break;
                        case "cmbVariableAlarmForm":
                            foreach (ListViewItem item in CurrentEditVarList)
                            {
                                IVariable var = item.Tag as IVariable;
                                var.AssociateForm = cmbVariableAlarmForm.Text;
                            }
                            break;
                        case "cmbVariableCam":
                            foreach (ListViewItem item in CurrentEditVarList)
                            {
                                IVariable var = item.Tag as IVariable;
                                var.AssociateVideo = cmbVariableCam.Text;
                            }
                            break;
                        case "chkVarOnAlarmSave":
                            foreach (ListViewItem item in CurrentEditVarList)
                            {
                                IVariable var = item.Tag as IVariable;
                                var.AlarmRecorderEnable = chkVarOnAlarmSave.Checked;
                            }
                            break;
                        case "chkVarOnChangeSave":
                            foreach (ListViewItem item in CurrentEditVarList)
                            {
                                IVariable var = item.Tag as IVariable;
                                var.DataChangedRecorderEnable = chkVarOnChangeSave.Checked;
                            }
                            break;
                        case "rdoVarNoRecord":
                            foreach (ListViewItem item in CurrentEditVarList)
                            {
                                IVariable var = item.Tag as IVariable;
                                if (rdoVarNoRecord.Checked==true)
                                    var.HistoryRecorder = HistoryTimerRecordEnum.NONE;
                           
                            }
                            break;
                        case "rdoVarFastRecord":
                            foreach (ListViewItem item in CurrentEditVarList)
                            {
                                IVariable var = item.Tag as IVariable;
                                if (rdoVarFastRecord.Checked == true)
                                    var.HistoryRecorder = HistoryTimerRecordEnum.FAST;

                            }
                            break;
                        case "rdoVarStdRecord":
                            foreach (ListViewItem item in CurrentEditVarList)
                            {
                                IVariable var = item.Tag as IVariable;
                                if (rdoVarStdRecord.Checked == true)
                                    var.HistoryRecorder = HistoryTimerRecordEnum.STANDARD;

                            }
                            break;
                        case "rdoVarSlowRecord":
                            foreach (ListViewItem item in CurrentEditVarList)
                            {
                                IVariable var = item.Tag as IVariable;
                                if (rdoVarSlowRecord.Checked == true)
                                    var.HistoryRecorder = HistoryTimerRecordEnum.SLOW;

                            }
                            break;
                        case "cmVarMatrixName":
                            foreach (ListViewItem item in CurrentEditVarList)
                            {
                                IVariable var = item.Tag as IVariable;
                                if (var.CamEx != null)
                                {
                                    var.CamEx.MatrixName = cmVarMatrixName.Text;
                                }
                            }
                            break;
                        case "cmbVarMatrixIn":
                            foreach (ListViewItem item in CurrentEditVarList)
                            {
                                IVariable var = item.Tag as IVariable;
                                if (var.CamEx != null)
                                {
                                    var.CamEx.MatrixInchannel = pubFun.IsInt(cmbVarMatrixIn.Text,1);
                                }
                            }
                            break;
                        case "cmbVarDvrName":
                            foreach (ListViewItem item in CurrentEditVarList)
                            {
                                IVariable var = item.Tag as IVariable;
                                if (var.CamEx != null)
                                {
                                    var.CamEx.DvrName = cmbVarDvrName.Text;
                                }
                            }
                            break;
                        case "cmbVarDvrIn":
                            foreach (ListViewItem item in CurrentEditVarList)
                            {
                                IVariable var = item.Tag as IVariable;
                                if (var.CamEx != null)
                                {
                                    var.CamEx.DvrInchannel = pubFun.IsInt(cmbVarDvrIn.Text,1);
                                }
                            }
                            break;
                        case "cmbVarVodName":
                            foreach (ListViewItem item in CurrentEditVarList)
                            {
                                IVariable var = item.Tag as IVariable;
                                if (var.CamEx != null)
                                {
                                    var.CamEx.VodName = cmbVarVodName.Text;
                                }
                            }
                            break;
                        case "chkUseMainCodeStream":
                            foreach (ListViewItem item in CurrentEditVarList)
                            {
                                IVariable var = item.Tag as IVariable;
                                var.CamEx.UseMainCodeStream = chkUseMainCodeStream.Checked;
                            }
                            break;
                        case "chkVarUseVod":
                            foreach (ListViewItem item in CurrentEditVarList)
                            {
                                IVariable var = item.Tag as IVariable;
                                var.CamEx.UseVodStream =chkVarUseVod.Checked;
                            }
                            break;

                    }
                    #endregion
                    */
                }
            }
            catch (System.Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());

            }
        }
        /// <summary>
        /// 1：进入编辑模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VariableEdit_Enter(object sender, EventArgs e)
        {
            ((Control)sender).Tag = "1";
        }
        /// <summary>
        /// 0：退出编辑模式 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VariableEdit_Leave(object sender, EventArgs e)
        {
            ((Control)sender).Tag = "0";
        }
        private void valueChangedController_OnValueChangedEvent(SourceGrid.CellContext sender, EventArgs e)
        {
            Debug.WriteLine("******OnValueChangedEven" + sender.Grid.Name);
            string name = sender.Grid.Name;
            SaveVariableRelation(name);

        }
        /// <summary>
        /// 点的关系表变化后的保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveVariableRelation(string gridName)
        {
            // Console.WriteLine(sender.ToString());
            //bSaveVariableEdit = false;
            if (bSaveVariableEdit)
            {
                List<ListViewItem> CurrentEditVarList = ListViewDevice.GetSelectItem();
                switch (gridName)
                {
                    case "gridStateDesc":
                        foreach (ListViewItem item in CurrentEditVarList)
                        {
                            IVariable var = item.Tag as IVariable;
                            var.DescList.Clear();
                            for (int r = 1; r < gridStateDesc.Rows.Count; r++)
                            {
                                CellContext[] context = new CellContext[gridStateDesc.Columns.Count];
                                for (int c = 0; c < gridStateDesc.Columns.Count; c++)
                                {
                                    SourceGrid.Cells.ICellVirtual cell = gridStateDesc.GetCell(r, c);
                                    Position pos = new Position(r, c);
                                    context[c] = new CellContext(gridStateDesc, pos, cell);

                                }
                                //add one 
                                //if (context[0].DisplayText.Trim() != "" && context[1].DisplayText.Trim() != "")
                                //{
                                VariableDesc desc = new VariableDesc();
                                desc.StateValue = context[0].DisplayText;
                                desc.StateDesc = context[1].DisplayText;
                                var.DescList.Add(desc);
                                //}
                            }
                        }
                        break;
                    case "gridEventTrigger":
                        foreach (ListViewItem item in CurrentEditVarList)
                        {
                            IVariable var = item.Tag as IVariable;
                            var.WayList.Clear();
                            for (int r = 1; r < gridEventTrigger.Rows.Count; r++)
                            {
                                CellContext[] context = new CellContext[gridEventTrigger.Columns.Count];
                                for (int c = 0; c < gridEventTrigger.Columns.Count; c++)
                                {
                                    SourceGrid.Cells.ICellVirtual cell = gridEventTrigger.GetCell(r, c);
                                    Position pos = new Position(r, c);

                                    context[c] = new CellContext(gridEventTrigger, pos, cell);

                                }
                                //add one 
                                //if (context[0].DisplayText.Trim() != "")
                                //{

                                string Express = context[0].DisplayText;
                                int EventType = (int)((EventTypeEnum)Enum.Parse(typeof(EventTypeEnum), context[1].DisplayText));
                                string EventDesc = context[2].DisplayText;
                                // uint Priority = (pubFun.IsNumeric(context[3].DisplayText) == -1) ? 0 : Convert.ToUInt32(pubFun.IsNumeric(context[3].DisplayText));
                                uint Priority = (uint)((_AlarmLevel)Enum.Parse(typeof(_AlarmLevel), context[3].DisplayText));
                                string ScriptText = context[4].DisplayText;
                                string time = context[5].DisplayText;


                                VariableTrigger way = new VariableTrigger(Express,
                                    EventType,
                                    EventDesc,
                                    Priority,
                                    ScriptText,
                                    time);

                                if (!var.WayList.Exists(delegate (VariableTrigger w) { return w.Express == way.Express; }))
                                {
                                    var.WayList.Add(way);
                                }
                                else
                                {
                                    MessageBox.Show("触发条件\"" + way.Express + "\"已经存在了,不能重复添加！");
                                }
                                //}
                            }
                        }
                        break;
                    case "gridAlarmAction":
                        foreach (ListViewItem item in CurrentEditVarList)
                        {
                            IVariable var = item.Tag as IVariable;
                            var.ActionList.Clear();
                            for (int r = 1; r < gridAlarmAction.Rows.Count; r++)
                            {
                                CellContext[] context = new CellContext[gridAlarmAction.Columns.Count];
                                for (int c = 0; c < gridAlarmAction.Columns.Count; c++)
                                {
                                    SourceGrid.Cells.ICellVirtual cell = gridAlarmAction.GetCell(r, c);
                                    Position pos = new Position(r, c);

                                    context[c] = new CellContext(gridAlarmAction, pos, cell);

                                }
                                //add one 
                                //if (context[0].DisplayText != "" && context[1].DisplayText != "" && context[2].DisplayText != "")
                                //{
                                VariableAction act = new VariableAction();
                                act.Way = context[0].DisplayText;
                                act.VarTarget = context[1].DisplayText;
                                act.VarValue = pubFun.IsInt(context[2].DisplayText, 0);
                                act.Description = context[3].DisplayText;
                                var.ActionList.Add(act);
                                //}
                            }
                        }
                        break;
                    case "gridAlarmVideo":
                        foreach (ListViewItem item in CurrentEditVarList)
                        {
                            IVariable var = item.Tag as IVariable;
                            var.VideoList.Clear();
                            for (int r = 1; r < gridAlarmVideo.Rows.Count; r++)
                            {
                                CellContext[] context = new CellContext[gridAlarmVideo.Columns.Count];
                                for (int c = 0; c < gridAlarmVideo.Columns.Count; c++)
                                {
                                    SourceGrid.Cells.ICellVirtual cell = gridAlarmVideo.GetCell(r, c);
                                    Position pos = new Position(r, c);

                                    context[c] = new CellContext(gridAlarmVideo, pos, cell);

                                }
                                //add one 
                                //if (context[0].DisplayText != "" && context[1].DisplayText != "" && context[2].DisplayText != "" && context[3].DisplayText != "")
                                //{

                                VariableVideo vdo = new VariableVideo();
                                vdo.Way = context[0].DisplayText;
                                vdo.MonName = context[1].DisplayText;
                                vdo.SubVideoOut = pubFun.IsInt(context[2].DisplayText, 1);
                                vdo.CamID = context[3].DisplayText;
                                vdo.PreSetID = pubFun.IsNumeric(context[4].DisplayText);
                                var.VideoList.Add(vdo);
                                //}
                            }
                        }
                        break;
                    case "gridAlarmSMS":
                        foreach (ListViewItem item in CurrentEditVarList)
                        {
                            IVariable var = item.Tag as IVariable;
                            var.SmsList.Clear();
                            for (int r = 1; r < gridAlarmSMS.Rows.Count; r++)
                            {
                                CellContext[] context = new CellContext[gridAlarmSMS.Columns.Count];
                                for (int c = 0; c < gridAlarmSMS.Columns.Count; c++)
                                {
                                    SourceGrid.Cells.ICellVirtual cell = gridAlarmSMS.GetCell(r, c);
                                    Position pos = new Position(r, c);

                                    context[c] = new CellContext(gridAlarmSMS, pos, cell);

                                }
                                //add one 
                                //if (context[0].DisplayText != "" && context[1].DisplayText != "" && context[2].DisplayText != "")
                                //{
                                VariableSMS sms = new VariableSMS();
                                sms.Way = context[0].DisplayText;
                                sms.Phone = context[1].DisplayText;
                                sms.Msg = context[2].DisplayText;
                                if (context[3].DisplayText == "短信")
                                    sms.Type = 0;
                                else if (context[3].DisplayText == "语音")
                                    sms.Type = 1;
                                else if (context[3].DisplayText == "通知")
                                    sms.Type = 2;
                                else if (context[3].DisplayText == "邮件")
                                    sms.Type = 3;
                                var.SmsList.Add(sms);
                                //}
                            }
                        }
                        break;

                }
            }

        }

        #endregion

        #region Grid快捷菜单事件
        private void tspAddAction_Click(object sender, EventArgs e)
        {

            try
            {
                List<ListViewItem> CurrentEditVarList = ListViewDevice.GetSelectItem();
                if (CurrentEditVarList.Count == 0) return;
                IVariable var = CurrentEditVarList[0].Tag as IVariable;
                int n = gridAlarmAction.RowsCount;
                gridAlarmAction.Rows.Insert(n);

                //报警条件列表
                List<string> strList = new List<string>();
                foreach (VariableTrigger v in var.WayList)
                {
                    strList.Add(v.Express);
                }
                SourceGrid.Cells.Editors.ComboBox comboStandard;
                comboStandard = new SourceGrid.Cells.Editors.ComboBox(typeof(string), strList.ToArray(), false);
                comboStandard.KeyPress += pubFun.NoKey_KeyPress;

                //SourceGrid.Cells.Editors.TextBox txt = new SourceGrid.Cells.Editors.TextBox(typeof(string));
                //txt.KeyPress += pubFun.No_KeyPress;

                gridAlarmAction[n, 0] = new SourceGrid.Cells.Cell("", comboStandard);
                gridAlarmAction[n, 1] = new SourceGrid.Cells.Cell("");
                gridAlarmAction[n, 2] = new SourceGrid.Cells.Cell(1, typeof(int));
                gridAlarmAction[n, 3] = new SourceGrid.Cells.Cell("", typeof(string));

                gridAlarmAction.Selection.ResetSelection(false);
                gridAlarmAction.Selection.SelectRow(n, true);

                gridAlarmAction[n, 0].ToolTipText = "请双击选择连锁动作条件。";
                gridAlarmAction[n, 1].ToolTipText = "请双击选择要连锁动作的变量。";
                gridAlarmAction[n, 2].ToolTipText = "请输入连锁动作的变量的动作数值";
                toolTipController.ToolTipTitle = "操作提示：";
                toolTipController.ToolTipIcon = ToolTipIcon.Info;
                toolTipController.IsBalloon = true;


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        //增加视频联动
        private void tspAddVideo_Click(object sender, EventArgs e)
        {
            List<ListViewItem> CurrentEditVarList = ListViewDevice.GetSelectItem();
            if (CurrentEditVarList.Count == 0) return;
            IVariable var = CurrentEditVarList[0].Tag as IVariable;

            int n = gridAlarmVideo.RowsCount;
            gridAlarmVideo.Rows.Insert(n);
            //报警条件列表
            List<string> strList = new List<string>();
            foreach (VariableTrigger v in var.WayList)
            {
                strList.Add(v.Express);
            }
            //子通道列表
            List<string> submonList = new List<string>();
            for (int i = 1; i < 17; i++)
            {
                submonList.Add(i.ToString());
            }

            //摄像机列表
            List<string> camList = new List<string>();
            camList.Add("");
            foreach (IChannel cha in Rtdb.ChanList)
            {
                if (cha is ICamChannel)
                {
                    foreach (IController con in cha.ConList)
                    {
                        foreach (IVariable c in con.VarList)
                            camList.Add(c.ID);
                    }
                }
            }
            //监视器列表
            List<string> monList = new List<string>();
            monList.Add("");
            foreach (IChannel cha in Rtdb.ChanList)
            {
                if (cha is IMonitorChannel)
                {
                    foreach (IController con in cha.ConList)
                    {
                        foreach (IVariable c in con.VarList)
                            monList.Add(c.ID);
                    }
                }
            }
            SourceGrid.Cells.Editors.ComboBox comboStandard;
            comboStandard = new SourceGrid.Cells.Editors.ComboBox(typeof(string), strList.ToArray(), false);
            comboStandard.KeyPress += pubFun.NoKey_KeyPress;

            SourceGrid.Cells.Editors.ComboBox combSubMon;
            combSubMon = new SourceGrid.Cells.Editors.ComboBox(typeof(string), submonList.ToArray(), false);
            combSubMon.KeyPress += pubFun.NoKey_KeyPress;

            SourceGrid.Cells.Editors.ComboBox combMon;
            combMon = new SourceGrid.Cells.Editors.ComboBox(typeof(string), monList.ToArray(), false);
            combMon.KeyPress += pubFun.NoKey_KeyPress;

            SourceGrid.Cells.Editors.ComboBox combCam;
            combCam = new SourceGrid.Cells.Editors.ComboBox(typeof(string), camList.ToArray(), false);
            combCam.KeyPress += pubFun.NoKey_KeyPress;




            gridAlarmVideo[n, 0] = new SourceGrid.Cells.Cell("", comboStandard);
            gridAlarmVideo[n, 1] = new SourceGrid.Cells.Cell("", combMon);
            gridAlarmVideo[n, 2] = new SourceGrid.Cells.Cell("1", combSubMon);
            gridAlarmVideo[n, 3] = new SourceGrid.Cells.Cell("", combCam);
            gridAlarmVideo[n, 4] = new SourceGrid.Cells.Cell(0, typeof(int));


            gridAlarmVideo[n, 0].ToolTipText = "请双击输入视频联动条件。";
            gridAlarmVideo[n, 1].ToolTipText = "请双击选择监视器。";
            gridAlarmVideo[n, 2].ToolTipText = "请双击选择监视器子画面。";
            gridAlarmVideo[n, 3].ToolTipText = "请双击选择摄像机。";
            gridAlarmVideo[n, 4].ToolTipText = "请输入摄像机预置位编号(只能输入数字)。";
            toolTipController.ToolTipTitle = "操作提示：";
            toolTipController.ToolTipIcon = ToolTipIcon.Info;
            toolTipController.IsBalloon = true;



        }
        //报警动作
        private void tspDelAction_Click(object sender, EventArgs e)
        {
            int[] rowsIndex = gridAlarmAction.Selection.GetSelectionRegion().GetRowsIndex();
            SourceGrid.RowInfo[] rows = new SourceGrid.RowInfo[rowsIndex.Length];

            for (int i = 0; i < rows.Length; i++)
            {
                rows[i] = gridAlarmAction.Rows[rowsIndex[i]];
            }

            foreach (SourceGrid.RowInfo r in rows)
            {

                gridAlarmAction.Rows.Remove(r.Index);
            }

            if (gridAlarmAction.RowsCount > 1)
                gridAlarmAction.Selection.FocusRow(1);
            SaveVariableRelation("gridAlarmAction");


        }

        private void tspDelVideo_Click(object sender, EventArgs e)
        {
            int[] rowsIndex = gridAlarmVideo.Selection.GetSelectionRegion().GetRowsIndex();
            SourceGrid.RowInfo[] rows = new SourceGrid.RowInfo[rowsIndex.Length];

            for (int i = 0; i < rows.Length; i++)
            {
                rows[i] = gridAlarmVideo.Rows[rowsIndex[i]];
            }

            foreach (SourceGrid.RowInfo r in rows)
            {

                gridAlarmVideo.Rows.Remove(r.Index);
            }

            if (gridAlarmVideo.RowsCount > 1)
                gridAlarmVideo.Selection.FocusRow(1);
            SaveVariableRelation("gridAlarmVideo");

        }

        private void tspAddTag_Click(object sender, EventArgs e)
        {
            NewTagTspMenuItem_Click(sender, e);
        }

        private void tspAddController_Click(object sender, EventArgs e)
        {
            NewControllerTspMenuItem_Click(sender, e);
        }
        //增加短信
        private void tspAddSMS_Click(object sender, EventArgs e)
        {
            try
            {
                List<ListViewItem> CurrentEditVarList = ListViewDevice.GetSelectItem();
                if (CurrentEditVarList.Count == 0) return;
                IVariable var = CurrentEditVarList[0].Tag as IVariable;
                int n = gridAlarmSMS.RowsCount;
                gridAlarmSMS.Rows.Insert(n);

                //报警条件列表
                List<string> strList = new List<string>();
                foreach (VariableTrigger v in var.WayList)
                {
                    strList.Add(v.Express);
                }
                SourceGrid.Cells.Editors.ComboBox comboStandard;
                comboStandard = new SourceGrid.Cells.Editors.ComboBox(typeof(string), strList.ToArray(), false);
                comboStandard.KeyPress += pubFun.NoKey_KeyPress;

                SourceGrid.Cells.Editors.TextBox txt = new SourceGrid.Cells.Editors.TextBox(typeof(string));
                //txt.KeyPress += pubFun.NubOnly_KeyPress;


                List<string> smsTypeList = new List<string>();
                smsTypeList.Add("短信");
                smsTypeList.Add("语音");
                smsTypeList.Add("通知");
                smsTypeList.Add("邮件");
                SourceGrid.Cells.Editors.ComboBox combTypesms;
                combTypesms = new SourceGrid.Cells.Editors.ComboBox(typeof(string), smsTypeList.ToArray(), false);
                combTypesms.KeyPress += pubFun.NoKey_KeyPress;

                gridAlarmSMS[n, 0] = new SourceGrid.Cells.Cell("", comboStandard);
                gridAlarmSMS[n, 1] = new SourceGrid.Cells.Cell("", txt);
                gridAlarmSMS[n, 2] = new SourceGrid.Cells.Cell("", typeof(string));
                gridAlarmSMS[n, 3] = new SourceGrid.Cells.Cell("短信", combTypesms);
                gridAlarmSMS[n, 0].ToolTipText = "请双击选择短信发送的触发事件。";
                gridAlarmSMS[n, 1].ToolTipText = "请输入信息的号码或邮箱。";
                gridAlarmSMS[n, 2].ToolTipText = "请输入信息内容。";
                gridAlarmSMS[n, 3].ToolTipText = "选择是手机短信还是语音电话，如果是语音电话，信息内容填文件名,如果是群发通知接收号码写0\r\n如果是邮件填邮箱";

                toolTipController.ToolTipTitle = "操作提示：";
                toolTipController.ToolTipIcon = ToolTipIcon.Info;
                toolTipController.IsBalloon = true;

            }

            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }

        }
        //删除短信
        private void tspDelSMS_Click(object sender, EventArgs e)
        {
            try
            {
                int[] rowsIndex = gridAlarmSMS.Selection.GetSelectionRegion().GetRowsIndex();
                SourceGrid.RowInfo[] rows = new SourceGrid.RowInfo[rowsIndex.Length];

                for (int i = 0; i < rows.Length; i++)
                {
                    rows[i] = gridAlarmSMS.Rows[rowsIndex[i]];
                }

                foreach (SourceGrid.RowInfo r in rows)
                {


                    gridAlarmSMS.Rows.Remove(r.Index);
                }

                if (gridAlarmSMS.RowsCount > 1)
                    gridAlarmSMS.Selection.FocusRow(1);
                SaveVariableRelation("gridAlarmSMS");
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }
        }
        //增加点的状态描述
        private void tspAddDesc_Click(object sender, EventArgs e)
        {
            try
            {
                List<ListViewItem> CurrentEditVarList = ListViewDevice.GetSelectItem();
                if (CurrentEditVarList.Count == 0) return;
                IVariable var = CurrentEditVarList[0].Tag as IVariable;
                int n = gridStateDesc.RowsCount;
                gridStateDesc.Rows.Insert(n);
                gridStateDesc[n, 0] = new SourceGrid.Cells.Cell("", typeof(string));
                gridStateDesc[n, 1] = new SourceGrid.Cells.Cell("", typeof(string));
                gridStateDesc[n, 0].ToolTipText = "请输入要描述的点的状态值。";
                gridStateDesc[n, 1].ToolTipText = "请输入该状态的文本描述。";
                toolTipController.ToolTipTitle = "操作提示：";
                toolTipController.ToolTipIcon = ToolTipIcon.Info;
                toolTipController.IsBalloon = true;
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }

        }
        //删除点的状态描述
        private void tspDelDesc_Click(object sender, EventArgs e)
        {
            try
            {
                int[] rowsIndex = gridStateDesc.Selection.GetSelectionRegion().GetRowsIndex();
                SourceGrid.RowInfo[] rows = new SourceGrid.RowInfo[rowsIndex.Length];

                for (int i = 0; i < rows.Length; i++)
                {
                    rows[i] = gridStateDesc.Rows[rowsIndex[i]];
                }

                foreach (SourceGrid.RowInfo r in rows)
                {

                    gridStateDesc.Rows.Remove(r.Index);
                }

                if (gridStateDesc.RowsCount > 1)
                    gridStateDesc.Selection.FocusRow(1);

                SaveVariableRelation("gridStateDesc");

            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }
        }

        //增加事件触发条件
        private void tspAddAlarmWay_Click(object sender, EventArgs e)
        {
            try
            {
                List<ListViewItem> CurrentEditVarList = ListViewDevice.GetSelectItem();
                if (CurrentEditVarList.Count == 0) return;
                int n = gridEventTrigger.RowsCount;
                gridEventTrigger.Rows.Insert(n);

                SourceGrid.Cells.Editors.ComboBox comboType;

                comboType = new SourceGrid.Cells.Editors.ComboBox(typeof(string), Enum.GetNames(typeof(EventTypeEnum)), false);
                comboType.KeyPress += pubFun.NoKey_KeyPress;

                SourceGrid.Cells.Editors.ComboBox comboPriority;
                // uint[] enumPriorty = new uint[] {0, 1, 2, 3, 4, 5, 6, 7 };
                //string[] enumPriorty = new string[] { "普通", "重要", "紧急" };
                string[] enumPriorty = new string[] { _AlarmLevel.普通报警.ToString(), _AlarmLevel.严重报警.ToString(), _AlarmLevel.紧急报警.ToString() };
                comboPriority = new SourceGrid.Cells.Editors.ComboBox(typeof(string), enumPriorty, false);

                comboPriority.KeyPress += pubFun.NoKey_KeyPress;

                List<string> timezoneList = new List<string>();
                timezoneList.Add("");
                foreach (TimeZoneClt z in TimeZoneDb.TimeZoneList)
                {
                    timezoneList.Add(z.ZoneName);
                }

                SourceGrid.Cells.Editors.ComboBox combTimeZone;
                combTimeZone = new SourceGrid.Cells.Editors.ComboBox(typeof(string), timezoneList.ToArray(), false);
                combTimeZone.KeyPress += pubFun.NoKey_KeyPress;



                gridEventTrigger[n, 0] = new SourceGrid.Cells.Cell("");
                gridEventTrigger[n, 1] = new SourceGrid.Cells.Cell(EventTypeEnum.普通报警.ToString(), comboType);
                gridEventTrigger[n, 2] = new SourceGrid.Cells.Cell("", typeof(string));
                gridEventTrigger[n, 3] = new SourceGrid.Cells.Cell(_AlarmLevel.普通报警.ToString(), comboPriority);
                gridEventTrigger[n, 4] = new SourceGrid.Cells.Cell("");
                gridEventTrigger[n, 5] = new SourceGrid.Cells.Cell("", combTimeZone);

                gridEventTrigger[n, 0].ToolTipText = "双击定义事件触发条件！";
                gridEventTrigger[n, 1].ToolTipText = "请选择该事件的类型！";
                gridEventTrigger[n, 2].ToolTipText = "请输入该事件的文本描述！";
                gridEventTrigger[n, 3].ToolTipText = "请选择该事件的优化级！1-3级为普通报警，4-6为重要报警，7级为紧急报警";
                gridEventTrigger[n, 4].ToolTipText = "双击输入脚本！";
                gridEventTrigger[n, 5].ToolTipText = "请选择该事件的时限";
                toolTipController.ToolTipTitle = "操作提示：";
                toolTipController.ToolTipIcon = ToolTipIcon.Info;
                toolTipController.IsBalloon = true;
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }
        }
        //删除报警条件
        private void tspDelAlarmWay_Click(object sender, EventArgs e)
        {
            int[] rowsIndex = gridEventTrigger.Selection.GetSelectionRegion().GetRowsIndex();
            SourceGrid.RowInfo[] rows = new SourceGrid.RowInfo[rowsIndex.Length];

            for (int i = 0; i < rows.Length; i++)
            {
                rows[i] = gridEventTrigger.Rows[rowsIndex[i]];
            }

            foreach (SourceGrid.RowInfo r in rows)
            {

                gridEventTrigger.Rows.Remove(r.Index);
            }

            if (gridEventTrigger.RowsCount > 1)
                gridEventTrigger.Selection.FocusRow(1);
            SaveVariableRelation("gridEventTrigger");
        }

        private void gridAlarmAction_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                SourceGrid.Grid grid = (Grid)sender;
                int r = grid.MouseDownPosition.Row;
                int c = grid.MouseDownPosition.Column;
                if (r < 0 || c < 0)
                {
                    return;
                }
                if (grid[0, c].DisplayText == "动作变量")
                {
                    SourceGrid.Cells.ICellVirtual cell = grid.GetCell(r, c);
                    Position pos = new Position(r, c);
                    CellContext context = new CellContext(grid, pos, cell);
                    FormVarList form = new FormVarList();
                    form.ShowDialog();
                    if (form.DialogResult == DialogResult.OK)
                    {
                        //grid[r, c] = new SourceGrid.Cells.Cell(form.VarName,typeof(string));
                        //grid[r, c].AddController(valueChangedController);
                        grid[r, c].Value = form.VarID;

                    }
                    form.Dispose();
                    grid.Invalidate();
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }

        }

        private void gridEventTrigger_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                SourceGrid.Grid grid = (Grid)sender;
                int r = grid.MouseDownPosition.Row;
                int c = grid.MouseDownPosition.Column;
                if (r < 1 || c < 0)
                {
                    return;
                }
                if (grid[0, c].DisplayText == "触发条件")
                {
                    SourceGrid.Cells.ICellVirtual cell = grid.GetCell(r, c);
                    Position pos = new Position(r, c);
                    CellContext context = new CellContext(grid, pos, cell);

                    FormTriggerSetting form = new FormTriggerSetting();

                    form.VarExpress = context.DisplayText;
                    //form.CurrentVar=tabPageGenerally.Tag as Variable;
                    form.ShowDialog();
                    bool bIsExsit = false;
                    if (form.DialogResult == DialogResult.OK)
                    {
                        for (int i = 1; i < r; i++)
                        {
                            if (grid[i, c].Value.ToString() == form.VarExpress)
                            {
                                bIsExsit = true;
                            }
                        }
                        if (!bIsExsit)
                            grid[r, c].Value = form.VarExpress;
                        else
                            MessageBox.Show("该触发条件已经存在！");
                        //grid[r, c].AddController(valueChangedController);
                    }
                    form.Dispose();
                }
                else if (grid[0, c].DisplayText == "执行脚本")
                {
                    SourceGrid.Cells.ICellVirtual cell = grid.GetCell(r, c);
                    Position pos = new Position(r, c);
                    CellContext context = new CellContext(grid, pos, cell);

                    FormEditVbs form = new FormEditVbs();
                    if (context.DisplayText.Trim() != "")
                        form.VbsText = context.DisplayText;
                    else
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("'读变量值：Server.GetValue(\"通道ID\",\"控制器ID\",\"变量ID\")\r\n");
                        sb.Append("'写变量值：Server.SetValue(\"通道ID\",\"控制器ID\",\"变量ID\",\"12\")\r\n");

                        form.VbsText = "sub PointOnEvent()\r\n" + sb.ToString() + " 'add your code here.   \r\nend sub";
                    }

                    //form.VBAEngine = _DriverMng.VarEventVBAEngine;
                    if (form.VBAEngine == null)
                    {
                        form.VBAEngine = new XVBAEngine();
                        form.VBAEngine.AddReferenceAssemblyByType(this.GetType());
                        form.VBAEngine.VBCompilerImports.Add("GHIBMS.Server.Global");

                    }
                    //form.CurrentVar=tabPageGenerally.Tag as Variable;
                    form.ShowDialog();
                    bool bIsExsit = false;
                    if (form.DialogResult == DialogResult.OK)
                    {
                        for (int i = 1; i < r; i++)
                        {
                            if (grid[i, c].Value.ToString() == form.VbsText)
                            {
                                bIsExsit = true;
                            }
                        }
                        if (!bIsExsit)
                            grid[r, c].Value = form.VbsText;
                        else
                        {
                            MessageBox.Show("该脚本已经存在！", "信息", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                            // MessageBox.Show("该脚本已经存在！");
                        }
                        //grid[r, c].AddController(valueChangedController);
                    }
                    form.Dispose();
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }
        }
        #endregion

        #region 数据内存链表到树表

        private void ChanList2TreeView()
        {

            trvDevice.BeginUpdate();
            iniTreeView();
            ListViewDevice.Clear();
            TreeNode root = trvDevice.Nodes[0];
            foreach (IChannel chan in Rtdb.ChanList)
            {
                TreeNode ChanNode = new TreeNode(chan.Name);
                ChanNode.Tag = chan;
                if (chan.Enable)
                {
                    ChanNode.ImageIndex = DevTreeImage.ChannelNormal;
                    ChanNode.SelectedImageIndex = DevTreeImage.ChannelNormal;
                }
                else
                {
                    ChanNode.ImageIndex = DevTreeImage.ChannelDisable;
                    ChanNode.SelectedImageIndex = DevTreeImage.ChannelDisable;
                }
                root.Nodes.Add(ChanNode);
                foreach (IController con in chan.ConList)
                {
                    TreeNode ConNode = new TreeNode(con.Name);

                    ConNode.Tag = con;
                    if (con.Enable)
                    {
                        ConNode.ImageIndex = DevTreeImage.ControllNormal;
                        ConNode.SelectedImageIndex = DevTreeImage.ControllNormal;
                    }
                    else
                    {
                        ConNode.ImageIndex = DevTreeImage.ControllDisable;
                        ConNode.SelectedImageIndex = DevTreeImage.ControllDisable;

                    }
                    ChanNode.Nodes.Add(ConNode);
                    /*
                    foreach (Variable var in con.VarList)
                    {
                        TreeNode VarNode = new TreeNode(var.Name);
                        VarNode.Tag = var;
                        if (var.Enable)
                        {
                            VarNode.ImageIndex = DevTreeImage.VariableNormal;
                            VarNode.SelectedImageIndex = DevTreeImage.VariableNormal;
                        }
                        else
                        {
                            VarNode.ImageIndex = DevTreeImage.VariableDisable;
                            VarNode.SelectedImageIndex = DevTreeImage.VariableDisable;
                        }
                        ConNode.Nodes.Add(VarNode);
                    }*/
                }
            }
            trvDevice.EndUpdate();

        }
        /*private void MapList2TreeView()
        {

            trvForm.BeginUpdate();
            trvForm.Nodes.Clear();
            iniMapTree();
            TreeNode root = trvForm.Nodes[0];
            foreach (DesignForm form in Rtdb.DesignFormList)
            {
                TreeNode formNode = new TreeNode(form.FormName);
                formNode.Tag = form;
                formNode.ImageIndex = 8;
                formNode.SelectedImageIndex = 8;
                root.Nodes.Add(formNode);
                foreach (GraphicAssociate g in form.GraphicList)
                {
                    TreeNode gNode = new TreeNode(g.associateVar);
                    gNode.Tag = g;
                    gNode.ImageIndex = 4;
                    gNode.SelectedImageIndex = 4;
                    formNode.Nodes.Add(gNode);
                }
            }
            trvForm.EndUpdate();
        }*/

        private bool TestDBconn()
        {
            //bool ret = false;
            waitingmsg = "正在测试数据库连接中，可能会等待1-2分钟……";
            Thread th = new Thread(new ThreadStart(this.ShowProgress));
            th.IsBackground = true;
            th.Name = "waitingtestdb";
            th.Start();
            //确保frmWait显示
            while (frmWait == null)
            {
                Thread.Sleep(1);
            }
            while (!frmWait.IsShowing)
            {
                Thread.Sleep(1);
            }
            DBConnectTest dbTest = new DBConnectTest(ServerConfig.DbHost, ServerConfig.DbName, ServerConfig.DbUser, ServerConfig.DbPw, 10000);
            if (ServerConfig.DataBaseEnable)
                g_bConnDbOK = dbTest.Test();

            CloseWaitingForm();



            if (!g_bConnDbOK)
            {
                //MessageBox.Show(this, "数据库" + StrConst.ERR_DB);
                MessageBox.Show("数据库" + StrConst.ERR_DB, "信息", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

            }

            return g_bConnDbOK;

        }
        private void tspSave2Db_Click(object sender, EventArgs e)
        {
            //if (ServerConfig.DataBaseEnable && bExistProject && g_bConnDbOK && !ServerConfig.RunningState)
            //   ProjectMng.SaveRtdb2DB();
        }



        #endregion

        #region 软件状态

        private void SetOpenState()
        {
            btnSave.Enabled = true;
            btnStartComm.Enabled = true;

            btnStartComm.Enabled = true;
            btnStopComm.Enabled = false;
            tspItemChanExportExecl.Enabled = true;
            tspItemConExportExcel.Enabled = true;
            tspTtemVarExportExcel.Enabled = true;
            tspItemCamExportExcel.Enabled = true;
            tspStarServer.Enabled = true;
            btnUploadToCloud.Enabled = true;
            tspDowloadFromCloud.Enabled = true;
            //btnUploadDB.Enabled = true;

        }

        #endregion

        #region Listview虚模式显示点的实时信息
        private void ListViewChannel(TreeNode node, List<IChannel> chList)
        {
            try
            {
                iniListViewDeviceChaHead(node);
                for (int i = 0; i < chList.Count; i++)
                {
                    IChannel chan = chList[i];
                    AddNewItem2ListView(chan);

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Logger.GetInstance().LogError(ex.ToString());

            }
            ListViewDevice.Invalidate();
        }
        private void ListViewController(TreeNode node, List<IController> cnList)
        {
            try
            {
                iniListViewDeviceConHead(node);
                for (int i = 0; i < cnList.Count; i++)
                {
                    IController con = cnList[i];
                    AddNewItem2ListView(con);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Logger.GetInstance().LogError(ex.ToString());

            }
            ListViewDevice.Invalidate();
        }
        private void ListViewVariable(TreeNode node, List<IVariable> varList)
        {
            iniListViewDeviceVarHead(node);
            try
            {
                for (int i = 0; i < varList.Count; i++)
                {
                    IVariable var = varList[i];
                    AddNewItem2ListView(var);

                }

            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());

            }
            ListViewDevice.Invalidate();
        }



        private void ListViewDeviceMouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                ListViewItem item = ListViewDevice.GetItemAt(e.X, e.Y);

                if (item != null)//选中 
                {

                    object o = item.Tag;
                    //定向快捷菜单
                    if (o is IChannel)
                    {
                        if (ServerConfig.RunningState == ServerStateEnum.STOPED)
                        {
                            ListViewDevice.ContextMenuStrip = context_ShortKey;
                            if (LisViewItemClipboard.Count > 0)
                            {
                                tspMenuPaste.Enabled = true;
                                tspPaste.Enabled = true;
                            }
                            else
                            {
                                tspMenuPaste.Enabled = false;
                                tspPaste.Enabled = false;

                            }

                        }
                        else
                        {
                            ListViewDevice.ContextMenuStrip = null;

                        }
                    }
                    else if (o is IController)
                    {
                        if (ServerConfig.RunningState == ServerStateEnum.STOPED)
                        {
                            ListViewDevice.ContextMenuStrip = context_ShortKey;
                            if (LisViewItemClipboard.Count > 0)
                            {
                                tspMenuPaste.Enabled = true;
                                tspPaste.Enabled = true;
                            }
                            else
                            {
                                tspMenuPaste.Enabled = false;
                                tspPaste.Enabled = false;
                            }


                        }
                        else
                        {
                            ListViewDevice.ContextMenuStrip = null;

                        }
                    }
                    else if (o is IVariable)
                    {

                        if (ServerConfig.RunningState == ServerStateEnum.STOPED)
                        {

                            ListViewDevice.ContextMenuStrip = context_L4;
                            if (item.Tag is IVariable)
                            {
                                IVariable v = item.Tag as IVariable;
                                if (v.Enable)
                                    tspItemVariableIsEnable.Text = "禁用变量";
                                else
                                    tspItemVariableIsEnable.Text = "启用变量";

                            }
                            if (LisViewItemClipboard.Count > 0)
                                MenuItemPaste.Enabled = true;
                            else
                                MenuItemPaste.Enabled = false;


                        }
                        else
                        {
                            ListViewDevice.ContextMenuStrip = context_Write;

                        }
                    }
                }
                else  //没选中行禁止快捷菜单
                {
                    ListViewDevice.ContextMenuStrip = null;

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Logger.GetInstance().LogError(ex.ToString());
            }

        }


        #endregion

        #region Log
        private void inilsvLogHead()
        {
            lsvLog.Clear();
            lsvLog.GridLines = true;     //显示各个记录的分隔线 
            lsvLog.FullRowSelect = true; //要选择就是一行 
            lsvLog.View = View.Details;  //定义列表显示的方式 
            lsvLog.Scrollable = true;    //需要时候显示滚动条 
            lsvLog.MultiSelect = false;  // 不可以多行选择 
            lsvLog.HeaderStyle = ColumnHeaderStyle.Clickable;
            lsvLog.VirtualMode = true;
            lsvLog.VirtualListSize = 0;
            //lsvLog.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(lsvLog_RetrieveVirtualItem);


            // 针对数据库的字段名称，建立与之适应显示表头 
            lsvLog.Columns.Add("时    间", 180, HorizontalAlignment.Center);
            lsvLog.Columns.Add("记录类型", 100, HorizontalAlignment.Center);
            lsvLog.Columns.Add("标    题", 100, HorizontalAlignment.Center);
            lsvLog.Columns.Add("操 作 员", 100, HorizontalAlignment.Center);
            lsvLog.Columns.Add("内    容", 800, HorizontalAlignment.Left);

        }

        private delegate void AddOperationLogdelgate(String Severity, String Title, String MachineName, String Message);
        public void AddOperationLogSafe(String Severity, String Title, String MachineName, String Message)
        {
            //Debug.WriteLine(" AddOperationLogSafe:" + Message);


            // if (/*FormIsShow || ServerConfig.WriteLog*/)
            {
                try
                {

                    string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    ListViewItem lsvLogItem = new ListViewItem(date);
                    lsvLogItem.SubItems.AddRange(new string[] {
                               Severity,
                               Title,
                               MachineName,
                               Message

                        });
                    lsvLogItem.ImageIndex = 14;
                    //lsvLog.BeginUpdate();
                    lsvLog.Insert(0, lsvLogItem);
                    int c = lsvLog.GetItemsConut();
                    if (c > MAX_LOGLIST_COUNT)
                        lsvLog.RemoveAt(c - 1);
                    //lsvLog.VirtualListSize = LogCacheList.Count;
                    //lsvLog.EndUpdate();
                    //lsvLog.Refresh();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    Logger.GetInstance().LogError(ex.ToString());
                }
            }
            if (LOG_MSG)
            {
                Logger.GetInstance().LogMsg("  " + Severity + "  " + Title + "  " + Message);
            }


        }
        /// <summary>
        /// 线程安全 LOG
        /// </summary>
        /// <param name="Severity"></param>
        /// <param name="Title"></param>
        /// <param name="MachineName"></param>
        /// <param name="Message"></param>
        public void AddOperationLog(String Severity, String Title, String MachineName, String Message)
        {
            //Debug.WriteLine("recevice log :" + Message);
            if (lsvLog.InvokeRequired)
            {
                lsvLog.BeginInvoke(new AddOperationLogdelgate(AddOperationLogSafe), new object[] { Severity, Title, MachineName, Message });
            }
            else
            {
                AddOperationLogSafe(Severity, Title, MachineName, Message);
            }
        }
        #endregion

        #region 系统热键

        /*private void RegisterHotKey()
        {
            //注册热键Ctrl+X，Id号为100。
            HotKey.RegisterHotKey(Handle, 100, HotKey.KeyModifiers.Ctrl, Keys.X);
            //注册热键Ctrl+C，Id号为101。
            HotKey.RegisterHotKey(Handle, 101, HotKey.KeyModifiers.Ctrl, Keys.C);
            //注册热键Alt+V，Id号为102。
            HotKey.RegisterHotKey(Handle, 102, HotKey.KeyModifiers.Ctrl, Keys.V);
        }
        private void UnregisterHotKey()
        {
            //注销Id号为100的热键设定
            HotKey.UnregisterHotKey(Handle, 100);
            //注销Id号为101的热键设定
            HotKey.UnregisterHotKey(Handle, 101);
            //注销Id号为102的热键设定
            HotKey.UnregisterHotKey(Handle, 102);
        }*/


        #endregion

        private void FormMain_Activated(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void FormMain_Leave(object sender, EventArgs e)
        {

        }

        private void tspMenuItemDelVarible_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认要删除该变量？", "操作确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                LisviewDeviceDelete();
            }
        }

        private void tspCancel_Click(object sender, EventArgs e)
        {
            if (bCut)
            {
                ListViewDeviceCutRecovery();
                bCut = false;
            }
            tspCancel.Enabled = false;
        }

        private void tspItemResetAlarm_Click(object sender, EventArgs e)
        {
            User user = new User();
            user.UserName = "admin";
            Almdb.ResetVarAlarm(user);
            RefreshListViewDev();

        }

        private void tspItemChannelEnable_Click(object sender, EventArgs e)
        {
            TreeNode currentNode = trvDevice.SelectedNode;
            if (currentNode != null && currentNode.Tag != null)
            {

                IChannel currentChan = currentNode.Tag as IChannel;
                if (currentChan != null)
                {
                    if (currentChan.Enable)
                    {
                        currentChan.Enable = false;
                        currentNode.ImageIndex = DevTreeImage.ChannelDisable;
                        currentNode.SelectedImageIndex = DevTreeImage.ChannelDisable;
                    }
                    else
                    {
                        currentChan.Enable = true;

                        currentNode.ImageIndex = DevTreeImage.ChannelNormal;
                        currentNode.SelectedImageIndex = DevTreeImage.ChannelNormal;
                    }
                }
            }

        }

        private void toolsripControllIsEnable_Click(object sender, EventArgs e)
        {
            TreeNode currentNode = trvDevice.SelectedNode;
            if (currentNode != null && currentNode.Tag != null)
            {

                IController con = currentNode.Tag as IController;
                if (con != null)
                {
                    if (con.Enable)
                    {
                        con.Enable = false;
                        currentNode.ImageIndex = DevTreeImage.ControllDisable;
                        currentNode.SelectedImageIndex = DevTreeImage.ControllDisable;
                    }
                    else
                    {
                        con.Enable = true;

                        currentNode.ImageIndex = DevTreeImage.ControllNormal;
                        currentNode.SelectedImageIndex = DevTreeImage.ControllNormal;
                    }
                }
            }
        }

        private void tspWriteLog_Click(object sender, EventArgs e)
        {
            ServerConfig.WriteLog = tspWriteLog.Checked;
            //不再保存状态值
            //ServerConfig.saveToFile();
            LOG_MSG = ServerConfig.WriteLog;
        }

        private void FormMain_SizeChanged(object sender, EventArgs e)
        {
            Debug.WriteLine(this.WindowState.ToString());

            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                this.notifyIcon1.Visible = true;
                FormIsShow = false;
            }
            else
            {
                FormIsShow = true;
            }
        }

        private void tspItemExportExecl_Click(object sender, EventArgs e)
        {
            //ExportExcel.Export("S_ChannelInfo");
        }

        private void tspItemConExportExcel_Click(object sender, EventArgs e)
        {
            //ExportExcel.Export("S_ControllerInfo");
        }
        private void tspItemVarExportExcel_Click(object sender, EventArgs e)
        {
            string saveFileName = "";
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "xls";
            saveDialog.Filter = "Excel文件|*.xls";
            saveDialog.FileName = "变量点表";
            saveDialog.ShowDialog();
            saveFileName = saveDialog.FileName;
            if (saveFileName.IndexOf(":") < 0)
            {
                //MessageBox.Show("文件路径有误，取消导出操作！", "提示信息：", MessageBoxButtons.OK, MessageBoxIcon.Asterisk,MessageBoxOptions.DefaultDesktopOnly);
                MessageBox.Show("文件路径有误，取消导出操作！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                return;
            }
            waitingmsg = "正在导出EXCEL文件，请耐心等待……";
            Thread th = new Thread(new ThreadStart(this.ShowProgress));
            th.IsBackground = true;
            th.Name = "ShowProgressThread";
            th.Start();
            //确保frmWait显示
            while (frmWait == null)
            {
                Thread.Sleep(1);
            }
            while (!frmWait.IsShowing)
            {
                Thread.Sleep(1);
            }
            ExportExcel.Export("S_Point", saveFileName);

            CloseWaitingForm();
        }

        private void tspItemCamExportExcel_Click(object sender, EventArgs e)
        {
            string saveFileName = "";
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "xls";
            saveDialog.Filter = "Excel文件|*.xls";
            //saveDialog.FileName = tableName;
            saveDialog.ShowDialog();
            saveFileName = saveDialog.FileName;
            if (saveFileName.IndexOf(":") < 0)
            {
                //MessageBox.Show("文件路径有误，取消导出操作！", "提示信息：", MessageBoxButtons.OK, MessageBoxIcon.Asterisk,MessageBoxOptions.DefaultDesktopOnly);
                MessageBox.Show("文件路径有误，取消导出操作！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                return;
            }
            waitingmsg = "正在导出EXCEL文件，请耐心等待……";
            Thread th = new Thread(new ThreadStart(this.ShowProgress));
            th.IsBackground = true;
            th.Name = "ShowProgressThread";
            th.Start();
            //确保frmWait显示
            while (frmWait == null)
            {
                Thread.Sleep(1);
            }
            while (!frmWait.IsShowing)
            {
                Thread.Sleep(1);
            }
            ExportExcel.Export("S_PointCamera", saveFileName);
            CloseWaitingForm();
        }

        private void ListViewDeviceKeyDown(object sender, KeyEventArgs e)
        {
            if (ServerConfig.RunningState != ServerStateEnum.STOPED) return;
            if (e.Control && e.KeyCode == Keys.C) //ctrl+c
            {
                ListViewDeviceCopy();

            }
            else if (e.Control && e.KeyCode == Keys.V)//ctrl+v
            {
                ListViewDevicePaste();
            }
            else if (e.Control && e.KeyCode == Keys.X) //ctrl+x
            {
                ListViewDeviceCut();
            }
            else if (e.Control && e.KeyCode == Keys.Z) //ctrl+z
            {
                if (bCut)
                {
                    ListViewDeviceCutRecovery();
                    //ListViewDevicePaste();
                    bCut = false;
                }
            }
            else if (e.KeyCode == Keys.Escape)  //esc
            {
                LisViewItemClipboard.Clear();
            }
            else if (e.Control && e.KeyCode == Keys.A)  //ctrl+a
            {
                tspMenuItemSelectAll.PerformClick();
            }
        }
        private void SetShortcutsDisable()
        {
            //tspCancel.Enabled = false;
            //tspCopy.Enabled = false;
            //tspCut.Enabled = false;
            //tspPaste.Enabled = false;
            //LisViewItemClipboard.Clear();

        }
        //快捷键操作 全选
        private void tspMenuItemSelectAll_Click(object sender, EventArgs e)
        {
            if (ServerConfig.RunningState == ServerStateEnum.STOPED)
            {
                //ListViewDevice.BeginUpdate();
                ListViewDevice.SelectedAll();
                List<ListViewItem> items = ListViewDevice.GetSelectItem();
                if (items.Count > 0)//选中 
                {
                    tspCut.Enabled = true;
                    tspCopy.Enabled = true;
                    object[] tags = new object[items.Count];
                    for (int i = 0; i < items.Count; i++)
                    {
                        tags[i] = items[i].Tag;
                    }
                    propertyGrid1.SelectedObjects = tags;
                }
                // ListViewDevice.EndUpdate();
            }
        }

        private void tspNetLink_Click(object sender, EventArgs e)
        {
            //FormNetLink frm = new FormNetLink();
            //frm.Show();
        }

        private void tspMenuDelete_Click(object sender, EventArgs e)
        {
            LisviewDeviceDelete();
        }

        private void tspTimeTask_Click(object sender, EventArgs e)
        {
            if (bExistProject)
            {
                //taskController.Stop();
                FormTimeTask frm = new FormTimeTask();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if (ServerConfig.RunningState == ServerStateEnum.STOPED)
                        if (MessageBox.Show("是否保存当前项目?", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            SavePrj();
                        }
                }
                //taskController.Start();
            }
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            //foreach (object o in propertyGrid1.SelectedObjects)
            //{
            //    Console.WriteLine(o.ToString());
            //}

            UpdataLsvDev(propertyGrid1.SelectedObjects);
        }

        private void tspRenameCH_Click(object sender, EventArgs e)
        {
            TreeNode nod = trvDevice.SelectedNode;
            if (nod != null)
            {
                FormRename frm = new FormRename(nod.Text);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if (Rtdb.IsExistChanName(frm.newName))
                    {
                        MessageBox.Show("该通道名称已经存在！");
                        return;
                    }
                    nod.Text = frm.newName;
                    (nod.Tag as IChannel).Name = frm.newName;
                }
            }
            trvDevice.Invalidate();
        }

        private void tspRenameCON_Click(object sender, EventArgs e)
        {
            TreeNode nod = trvDevice.SelectedNode;
            if (nod != null)
            {
                FormRename frm = new FormRename(nod.Text);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if (Rtdb.IsExistConName((IChannel)nod.Parent.Tag, frm.newName))
                    {
                        MessageBox.Show("该控制器名称已经存在！");
                        return;
                    }
                    nod.Text = frm.newName;
                    (nod.Tag as IController).Name = frm.newName;
                }
            }
            trvDevice.Invalidate();

        }

        private void tspRenameVar_Click(object sender, EventArgs e)
        {
            ListViewItem item = ListViewDevice.GetFirstSelectItem();
            if (item != null)
            {
                FormRename frm = new FormRename(item.SubItems[2].Text);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if (Rtdb.IsExistVarName(((IVariable)item.Tag).ControllerObject, frm.newName))
                    {
                        MessageBox.Show("该变量名称已经存在！");
                        return;
                    }
                    item.SubItems[2].Text = frm.newName;
                    (item.Tag as IVariable).Name = frm.newName;
                }

            }
            ListViewDevice.Invalidate();

        }

        private void TspOpenMain_Click(object sender, EventArgs e)
        {
            this.Visible = true;
            FormIsShow = true;
            this.WindowState = FormWindowState.Maximized;
            RefreshLsvVariable();

        }

        private void tspStarServer_Click(object sender, EventArgs e)
        {
            btnStartComm.PerformClick();
        }

        private void tspStopServer_Click(object sender, EventArgs e)
        {
            btnStopComm.PerformClick();
        }

        private void tspClose_Click(object sender, EventArgs e)
        {
            ToolStripMenuItemClose.PerformClick();
        }

        private void TspMenuItemRefresh_Click(object sender, EventArgs e)
        {
            RefreshLsvVariable();
        }


        private void tspMenuItemAutoRun_Click(object sender, EventArgs e)
        {
            /*if (ServerConfig.AutoStart == true)
            {
                RunWhenStart(false, Application.ProductName, Application.ExecutablePath);
                ServerConfig.AutoStart = false;
            }
            else
            {
                RunWhenStart(true, Application.ProductName, Application.ExecutablePath);
                ServerConfig.AutoStart = true;
            }

            tspMenuItemAutoRun.Checked = ServerConfig.AutoStart;*/
        }
        public static void RunWhenStart(bool Started, string name, string path)
        {
            try
            {
                RegistryKey HKLM = Registry.LocalMachine;
                RegistryKey Run = HKLM.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                if (Started == true)
                {
                    try
                    {
                        Run.SetValue(name, path);
                        HKLM.Close();
                    }
                    catch (Exception Err)
                    {
                        MessageBox.Show(Err.Message.ToString(), "提醒", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    try
                    {
                        Run.DeleteValue(name);
                        HKLM.Close();
                    }
                    catch (Exception Err)
                    {
                        MessageBox.Show(Err.Message.ToString(), "提醒", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception Err)
            {
                MessageBox.Show(Err.Message.ToString(), "提醒", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void 通讯CToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            tspAutoLoad.Checked = ServerConfig.FileAutoLoad;
            tspAutoComm.Checked = ServerConfig.AutoComm;
            tspWriteLog.Checked = ServerConfig.WriteLog;
            tspMenuItemAutoRun.Checked = ServerConfig.AutoStart;
        }
        private void WTRegedit(string name, string tovalue)
        {
            try
            {
                RegistryKey hklm = Registry.LocalMachine;
                RegistryKey software = hklm.OpenSubKey("SOFTWARE", true);
                RegistryKey aimdir = software.CreateSubKey("GUHE");
                aimdir.SetValue(name, tovalue);
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());

            }
        }
        private string GetRegistData(string name)
        {
            try
            {
                string registData;
                RegistryKey hkml = Registry.LocalMachine;
                RegistryKey software = hkml.OpenSubKey("SOFTWARE", true);
                RegistryKey aimdir = software.OpenSubKey("GUHE", true);
                if (aimdir != null && aimdir.GetValue(name, "") != null)
                {
                    registData = aimdir.GetValue(name).ToString();
                    return registData;
                }
                else
                {
                    return "";
                };
            }
            catch (Exception ex)
            {

                Logger.GetInstance().LogError(ex.ToString());
                return "";
            }
        }

        private void ToolStripMenuItemEnable_Click(object sender, EventArgs e)
        {
            ListViewDeviceEnable();
        }

        private void ToolStripMenuItemDisable_Click(object sender, EventArgs e)
        {
            ListViewDeviceDisable();
        }

        private void TspConfigTimeZone_Click(object sender, EventArgs e)
        {
            if (bExistProject)
            {
                FormTimeZone frm = new FormTimeZone();
                if (frm.ShowDialog() == DialogResult.OK)
                {

                }
                if (MessageBox.Show("是否保存当前项目?", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SavePrj();
                }
            }
        }



        private void ToolStripMenuItemCloud_Click(object sender, EventArgs e)
        {
            FormCloudSet frm = new FormCloudSet();
            frm.ShowDialog();
        }

        private void btnCloud_Click(object sender, EventArgs e)
        {
            if (!MqttController.IsConnected())
            {
                if (!ServerConfig.AutoComm)
                    MessageBox.Show("物联网平台没有连接不能进行该操作！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }
            toolStrip1.Enabled = false;
            btnUploadToCloud.Enabled = false;

            try
            {
                waitingmsg = "正在上传数据至云服务器，请耐心等待……";
                Thread th = new Thread(new ThreadStart(this.ShowProgress));
                th.IsBackground = true;
                th.Name = "ShowProgressCloudThread";
                th.Start();
                //确保frmWait显示
                while (frmWait == null)
                {
                    Thread.Sleep(100);
                }
                while (!frmWait.IsShowing)
                {
                    Thread.Sleep(100);
                }
                if (true)
                {

                    #region V2.0
                    if (!_DriverMng.UpLoadToCloud_V2())
                    {
                        CloseWaitingForm();
                        if (!ServerConfig.AutoComm)
                            MessageBox.Show("云同步失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                        btnUploadToCloud.Enabled = true;
                        toolStrip1.Enabled = true;
                        return;
                    }
                    #endregion

                }
                else
                {
                    CloseWaitingForm();
                    if (!ServerConfig.AutoComm)
                        MessageBox.Show("云同步失败！云端授权认证失败，请检查用户名、密码、授权服务地址！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    btnUploadToCloud.Enabled = true;
                    toolStrip1.Enabled = true;
                    return;
                }

            }
            catch (Exception ex)
            {
                CloseWaitingForm();
                if (!ServerConfig.AutoComm)
                    MessageBox.Show("云同步失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                Logger.GetInstance().LogError(ex.ToString());
                btnUploadToCloud.Enabled = true;
                toolStrip1.Enabled = true;
                return;
            }

            CloseWaitingForm();
            if (!ServerConfig.AutoComm)
                MessageBox.Show("云同步成功！", "云同步", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

            AddOperationLog(StrConst.SERVERITY_MSG, StrConst.TITLE_SYS, "", "云同步成功！");
            btnUploadToCloud.Enabled = true;
            toolStrip1.Enabled = true;
            return;
        }

        private void 查找ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //if (item != null)
            //{
            //    FormSearch frm = new FormSearch(item.SubItems[1].Text);
            //    if (frm.ShowDialog() == DialogResult.OK)
            //    {
            //        item.SubItems[1].Text = frm.newName;
            //        (item.Tag as IVariable).Name = frm.newName;
            //    }

            //}
            //ListViewDevice.Invalidate();

            TreeNode currentNode = ListViewDevice.Tag as TreeNode;
            if (currentNode == null) return;
            object o = currentNode.Tag;
            if (o is IController)
            {
                FormSearch frm = new FormSearch();
                frm.SetController((IController)o, ListViewDevice);
                frm.Show();

            }

            //try
            //{
            //    if (trvDevice.SelectedNode == null) return;

            //    IController currentCon = trvDevice.SelectedNode.Tag as IController;
            //    FormVarWizard formTagWizard = new FormVarWizard(false); //IsEdit 
            //    formTagWizard.selController = currentCon;

            //    if (formTagWizard.ShowDialog() == DialogResult.OK)
            //    {
            //        foreach (IVariable var in formTagWizard.NewVarList)
            //        {
            //            AddNewItem2ListView(var);
            //        }

            //    }
            //    ListViewDevice.Invalidate();
            //}
            //catch (Exception ex)
            //{
            //    Logger.GetInstance().LogError(ex.ToString());
            //}
        }

        private void 替换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode currentNode = ListViewDevice.Tag as TreeNode;
            if (currentNode == null) return;
            object o = currentNode.Tag;
            if (o is IController)
            {
                FormReplace frm = new FormReplace(this);
                frm.SetController((IController)o, ListViewDevice);
                frm.Show();

            }

        }

        private void btnUploadDB_Click(object sender, EventArgs e)
        {
            if (ServerConfig.DataBaseEnable && g_bConnDbOK)
            {
                waitingmsg = "正在上传数据至数据库，请耐心等待……";
                Thread th = new Thread(new ThreadStart(this.ShowProgress));
                th.IsBackground = true;
                th.Name = "ShowProgressCloudThread";
                th.Start();
                //确保frmWait显示
                while (frmWait == null)
                {
                    Thread.Sleep(1);
                }
                while (!frmWait.IsShowing)
                {
                    Thread.Sleep(1);
                }
                //ProjectMng.SaveRtdb2DB();
                CloseWaitingForm();
            }
            else
                MessageBox.Show("上传失败，数据库没有连接成功，\r\n请在设置>通讯参数设置！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

            //MessageBox.Show("上传失败，数据库没有连接成功，\r\n请在设置>通讯参数设置！");

        }

        private void tspExportPuginInfo_Click(object sender, EventArgs e)
        {
            try
            {



                string saveFileName = "";
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.DefaultExt = "xls";
                saveDialog.Filter = "Excel文件|*.xls";
                saveDialog.FileName = "设备驱动信息表";
                saveDialog.ShowDialog();
                saveFileName = saveDialog.FileName;
                if (saveFileName.IndexOf(":") < 0)
                {
                    MessageBox.Show("您取消了导出操作！", "提示信息：", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
                waitingmsg = "正在导出EXCEL文件，请耐心等待……";
                Thread th = new Thread(new ThreadStart(this.ShowProgress));
                th.IsBackground = true;
                th.Name = "ShowProgressThread";
                th.Start();
                //确保frmWait显示
                while (frmWait == null)
                {
                    Thread.Sleep(1);
                }
                while (!frmWait.IsShowing)
                {
                    Thread.Sleep(1);
                }

                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                object miss = System.Reflection.Missing.Value;

                if (xlApp == null)
                {
                    MessageBox.Show("无法创建Excel对象，可能您的机子未安装Excel！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                    return;
                }


                Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
                Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
                Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];//取得sheet1
                Microsoft.Office.Interop.Excel.Range range;


                //总行数和总列数
                long totalRowCount = PluginMng.CommPlugs.Count;
                long totalColCount = 6;

                //写入第一行表头    
                // for (int i = 0; i < totalColCount; i++)
                {
                    worksheet.Cells[1, 1] = "序号";
                    range = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[1, 1];
                    range.Interior.ColorIndex = 15;
                    range.Font.Bold = true;
                    range.ColumnWidth = 10;

                    worksheet.Cells[1, 2] = "驱动名称";
                    range = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[1, 2];
                    range.Interior.ColorIndex = 15;
                    range.Font.Bold = true;
                    range.ColumnWidth = 50;

                    worksheet.Cells[1, 3] = "驱动ID";
                    range = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[1, 3];
                    range.Interior.ColorIndex = 15;
                    range.Font.Bold = true;
                    range.ColumnWidth = 30;

                    worksheet.Cells[1, 4] = "协议编码";
                    range = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[1, 4];
                    range.Interior.ColorIndex = 15;
                    range.ColumnWidth = 30;
                    range.Font.Bold = true;

                    worksheet.Cells[1, 5] = "系统分类";
                    range = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[1, 5];
                    range.Interior.ColorIndex = 15;
                    range.Font.Bold = true;
                    range.ColumnWidth = 20;

                    worksheet.Cells[1, 6] = "项目名称";
                    range = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[1, 6];
                    range.Interior.ColorIndex = 15;
                    range.Font.Bold = true;
                    range.ColumnWidth = 20;
                }
                int r = 0;
                foreach (ICommunicationPlug plug in PluginMng.CommPlugs)
                {
                    //写入数值          
                    // for (int r = 0; r < totalRowCount; r++)
                    {

                        worksheet.Cells[r + 2, 1] = (r + 1).ToString();
                        worksheet.Cells[r + 2, 2] = PluginMng.CommPlugs[r].Name;
                        worksheet.Cells[r + 2, 3] = PluginMng.CommPlugs[r].PlugID;
                        worksheet.Cells[r + 2, 4] = PluginMng.CommPlugs[r].SelectedProtocol;
                        worksheet.Cells[r + 2, 5] = PluginMng.CommPlugs[r].SystemLabel;
                        worksheet.Cells[r + 2, 6] = PluginMng.GetPlugFileName(PluginMng.CommPlugs[r].PlugID);

                    }
                    r++;
                }

                range = worksheet.Range[worksheet.Cells[2, 1], worksheet.Cells[totalRowCount + 1, totalColCount]];

                if (totalRowCount > 0)
                {
                    range.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal].ColorIndex = Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic;
                    range.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    range.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;

                }

                if (totalColCount > 1)
                {
                    range.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical].ColorIndex = Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic;
                    range.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    range.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;

                }

                if (range != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(range);
                    range = null;
                }

                if (worksheet != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                    worksheet = null;
                }
                if (saveFileName != "")
                {
                    try
                    {
                        workbook.Saved = true;
                        System.Reflection.Missing missing = System.Reflection.Missing.Value;
                        workbook.SaveAs(saveFileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, miss, miss, miss, miss,
                            Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, miss, miss, miss, miss, miss);
                        MessageBox.Show("导出成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                    }
                    catch
                    {
                        MessageBox.Show("导出文件错误！请重试！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                    }

                }

                if (workbook != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                    workbook = null;

                }

                if (workbooks != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workbooks);
                    workbooks = null;

                }

                xlApp.Application.Workbooks.Close();

                xlApp.Quit();
                GC.Collect();
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
                MessageBox.Show("导出文件错误！请重试！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
            CloseWaitingForm();
        }

        private void tspCommPlugin_Click(object sender, EventArgs e)
        {
            FormAddPlugin frm = new FormAddPlugin();
            frm.ShowDialog();
        }

        //private bool UpLoadToCloud_V2()
        //{
        //    foreach (IChannel ch in Rtdb.ChanList)
        //    {
        //        ch.DateStamp = DateTime.Now;
        //        ch.Active = false;
        //        foreach (IController con in ch.ConList)
        //        {
        //            con.DateStamp = DateTime.Now;
        //            con.Active = false;
        //            foreach (IVariable v in con.VarList)
        //            {
        //                v.Active = false;
        //                v.Quality = (short)COMM_QUALITY_STATUS.NOT_CONNECTED;
        //                v.DateStamp = DateTime.Now;
        //            }
        //        }
        //    }
        //    bool ret = false;
        //    Dictionary<string, string> dic = new Dictionary<string, string>();
        //    BaseIOServer io = new BaseIOServer()
        //    {
        //        IPAddress = GetInternalIP(),
        //        Host = Dns.GetHostName(),
        //        Name = ServerConfig.CloudClientID,
        //        LastTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
        //        ID = ServerConfig.CloudClientID,
        //        KEY = ServerConfig.CloudClientKEY,
        //        AvailableMem = GetAvailableMem(),
        //        CpuUsage = GetCpuUsage(),
        //        LicenceState = GetLicenceState(),
        //        OnlineState = true,
        //        State = ServerConfig.RunningState,
        //        TotalTime = pubFun.DateDiff2(DateTime.Now, ServerConfig.TotalTime)

        //    };
        //    dic.Add("ID", io.ID);
        //    dic.Add("Name", io.Name);
        //    dic.Add("Host", io.Host);
        //    dic.Add("LastTime", io.LastTime);
        //    dic.Add("IPAddress", io.IPAddress);

        //    dic.Add("KEY", io.KEY);
        //    dic.Add("AvailableMem", io.AvailableMem);
        //    dic.Add("CpuUsage", io.CpuUsage);
        //    dic.Add("LicenceState", io.LicenceState.ToString());
        //    dic.Add("OnlineState", io.OnlineState.ToString());
        //    dic.Add("State", io.State.ToString());
        //    dic.Add("TotalTime", io.TotalTime);
        //    if (ServerConfig.MqttEnable)
        //    {
        //        string key = ServerConfig.CloudClientID;
        //        ret = _DriverMng.mqttController.PublishUploadConfig("ioserver_add", data);
                
        //    }
        //    Thread.Sleep(500);
        //    dic.Clear();
        //    //创建XmlDocument文档
        //    XmlDocument doc = new XmlDocument();
        //    //创建根元素
        //    doc.LoadXml("<Channels></Channels>");
        //    XmlNode root = doc.DocumentElement;
        //    doc.InsertBefore(doc.CreateXmlDeclaration("1.0", "utf-8", "yes"), root);

        //    foreach (IChannel chan in Rtdb.ChanList)
        //    {

        //        ICommunicationPlug plug = chan.Plugin;
        //        XmlElement xmlChan = plug.SaveChannel(doc, chan);
        //        root.AppendChild(xmlChan);
        //        dic.Add("PlugID", xmlChan.Name);
        //        foreach (XmlAttribute arrchan in xmlChan.Attributes)
        //        {
        //            dic.Add(arrchan.Name, arrchan.Value);

        //        }

        //        if (ServerConfig.MqttEnable)
        //        {
        //            var data =JsonConvert.SerializeObject(dic);
        //            string key = $"{ServerConfig.CloudClientID}:{chan.ID}";
        //            ret = _DriverMng.mqttController.PublishUploadConfig("channel_add",key, data);
                  
        //        }

        //        dic.Clear();
        //        //控制器
        //        foreach (XmlNode ctrlnode in xmlChan.ChildNodes)
        //        {
        //            foreach (XmlAttribute arrCtrl in ctrlnode.Attributes)
        //            {
        //                dic.Add(arrCtrl.Name, arrCtrl.Value);

        //            }
        //            if (ServerConfig.MqttEnable)
        //            {
        //                var data = JsonConvert.SerializeObject(dic);
        //                string key = $"{ServerConfig.CloudClientID}:{chan.ID}:{ctrlnode.Attributes["ID"].Value}";
        //                ret = _DriverMng.mqttController.PublishUploadConfig("controller_add",key,data);
                        
        //            }
        //            dic.Clear();
        //            //变量
        //            foreach (XmlNode varnode in ctrlnode.ChildNodes)
        //            {
        //                foreach (XmlAttribute arrVar in varnode.Attributes)
        //                {
        //                    dic.Add(arrVar.Name, arrVar.Value);

        //                }
        //                if (ServerConfig.MqttEnable)
        //                {
        //                    if (dic["Value"] != null)  //初始同步时不上传Value
        //                        dic.Remove("Value");
        //                    var data =JsonConvert.SerializeObject(dic);
        //                    string key = $"{ServerConfig.CloudClientID}:{chan.ID}:{ctrlnode.Attributes["ID"].Value}:{varnode.Attributes["ID"].Value}";
        //                    ret =  _DriverMng.mqttController.PublishUploadConfig("variable_add",key, data);
                          
        //                }
        //                dic.Clear();

        //            }



        //        }

        //    }


        //    root.AppendChild(TimeDb.SaveTimeTask(doc));
        //    root.AppendChild(TimeZoneDb.SaveTimeZone(doc));
        //    ProjectMng.BackupToZip(doc, "Upload");
        //    return ret;
        //}
        private string GetAvailableMem()
        {
            //try
            //{
            //    string usedMem = Math.Round(MemoPerformanceCounter.NextValue() / 1024, 2, MidpointRounding.AwayFromZero).ToString() + "G";
            //    return usedMem;
            //}
            //catch { }
            return "";
        }
        private string GetCpuUsage()
        {
            //try
            //{
            //    string usedCpu = Math.Round(CpuPerformanceCounter.NextValue(), 2, MidpointRounding.AwayFromZero).ToString() + "%";
            //    return usedCpu;
            //}
            //catch { }
            return "";
        }
        private LicenceStateEnum GetLicenceState()
        {
            LicenceStateEnum state = new LicenceStateEnum();
            if (licOK) state = LicenceStateEnum.AUTH;
            else
            {
                if (ExpireCheck())
                {
                    state = LicenceStateEnum.NONO;
                }
                else
                {
                    state = LicenceStateEnum.TRIAL;
                }

            }
            return state;

        }

        private void DownLoadFromCloud_V2()
        {
            string url = "http://" + ServerConfig.MqttServerIp + ":9005/api/loadConfig/" + ServerConfig.CloudClientID;

            if (!ProjectMng.LoadFromXml(url))
            {
                MessageBox.Show("自云端载入失败");
                return;
            }
            string newname = ServerConfig.APP_PATH + "dowload" + DateTime.Now.ToString("yyyymmddHHmmss") + ".xml";
            ProjectMng.SaveToXml(newname);
            ServerConfig.ProjectPath = newname;
            UpdateTree(null, ServerConfig.ProjectPath, NodeStateEnum.None);
            /*
            XmlDocument doc = new XmlDocument();

            doc.LoadXml("<Channels></Channels>");
            XmlNode root = doc.DocumentElement;
            doc.InsertBefore(doc.CreateXmlDeclaration("1.0", "utf-8", "yes"), root);
            //获取所有通道的键
            var chlkeys = GHNETBASE.RTDB.DeviceManagement.SingletonInstance.ChlGetAll(ServerConfig.CloudClientID).ToList();

            foreach (string chlkey in chlkeys)
            {

                Dictionary<string, string> chldic = DeviceManagement.SingletonInstance.ChlGetPropertyAll_V2(ServerConfig.CloudClientID, chlkey);
                if (chldic.Count != 0)
                {
                    XmlElement xmlchl = doc.CreateElement(chldic["PlugID"]);
                    foreach (var chlpro in chldic)
                    {
                        xmlchl.SetAttribute(chlpro.Key, chlpro.Value);
                    }
                    root.AppendChild(xmlchl);

                    //控制器
                    var ctrlkeys = GHNETBASE.RTDB.DeviceManagement.SingletonInstance.CtrlGetAll(ServerConfig.CloudClientID, chldic["ID"]).ToList();


                    foreach (string ctrlkey in ctrlkeys)
                    {

                        Dictionary<string, string> ctrldic = DeviceManagement.SingletonInstance.CtrlGetPropertyAll_V2(ServerConfig.CloudClientID, chldic["ID"], ctrlkey);

                        if (ctrldic.Count != 0)
                        {
                            XmlElement xmlctrl = doc.CreateElement("Controller");
                            foreach (var ctrlpro in ctrldic)
                            {
                                xmlctrl.SetAttribute(ctrlpro.Key, ctrlpro.Value);
                            }
                            xmlchl.AppendChild(xmlctrl);
                            //变量
                            var varkeys = GHNETBASE.RTDB.DeviceManagement.SingletonInstance.VarGetAll(ServerConfig.CloudClientID, chldic["ID"], ctrldic["ID"]).ToList();

                            foreach (string varkey in varkeys)
                            {

                                Dictionary<string, string> vardic = DeviceManagement.SingletonInstance.VarGetPropertyAll_V2(ServerConfig.CloudClientID, chldic["ID"], ctrldic["ID"], varkey);
                                if (vardic.Count != 0)
                                {
                                    XmlElement xmlvar = doc.CreateElement("Variable");
                                    foreach (var varpro in vardic)
                                    {
                                        xmlvar.SetAttribute(varpro.Key, varpro.Value);
                                    }
                                    xmlctrl.AppendChild(xmlvar);
                                }
                                else
                                {
                                    Logger.GetInstance().LogWarning("实时数据库变量索引和内容不一致,需要同步");
                                }
                            }
                        }
                        else
                        {
                            Logger.GetInstance().LogWarning("实时数据库控制器索引和内容不一致,需要同步");
                        }
                    }
                }
                else
                {
                    Logger.GetInstance().LogWarning("实时数据库通道索引和内容不一致,需要同步");
                }
            }

            Rtdb.ChanList.Clear();
            // Console.WriteLine("star:" + DateTime.Now.ToString());
            //从文件中载入实时内存链表
            foreach (ICommunicationPlug plug in PluginMng.CommPlugs)
            {
                #region Create channel
                XmlNodeList xmlChans = doc.SelectNodes("/Channels/" + plug.PlugID);
                foreach (XmlNode chNode in xmlChans)
                {
                    IChannel chan = (IChannel)plug.CreateChannel(chNode);
                    if (chan != null)
                    {
                        Rtdb.ChanList.Add(chan);

                    }
                }

                #endregion
            }
            ChanList2TreeView();
            SetOpenState();
            */

        }
        private string GetInternalIP()
        {
            IPHostEntry host;
            string localIP = "?";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }
        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.Show();
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            contextMenuStripMain.Show(MousePosition);
        }

        private void tspDowdoadFromcloud_Click(object sender, EventArgs e)
        {
            toolStrip1.Enabled = false;

            if (currentPrejectPath != "" && MessageBox.Show("当前项目将关闭，是否保存？", "云端下载", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly) == DialogResult.Yes)
            {
                SavePrj();
            }

            waitingmsg = "正在自云服务器下载数据，请耐心等待……";
            Thread th = new Thread(new ThreadStart(this.ShowProgress));
            th.IsBackground = true;
            th.Name = "ShowProgressCloudThread";
            th.Start();

            //确保frmWait显示
            while (frmWait == null)
            {
                Thread.Sleep(100);
            }
            while (!frmWait.IsShowing)
            {
                Thread.Sleep(100);
            }
            DownLoadFromCloud_V2();
            ShowMainTabPage(MainTabPage.None);
            bExistProject = true;
            CloseWaitingForm();

            toolStrip1.Enabled = true;
        }
        private delegate void UpdateVarViewDelegate(BaseVariable var);
        /// <summary>
        /// 线程安全更新点的显示
        /// </summary>
        /// <param name="var"></param>
        private void UpdateVarView(IVariable var)
        {
            if (FormIsShow)
            {
                if (ListViewDevice.InvokeRequired)
                {
                    ListViewDevice.BeginInvoke(new UpdateVarViewDelegate(UpdateVarView), new object[] { var });
                }
                else
                {

                    //Stopwatch watch = new Stopwatch();
                    //watch.Start();
                    if (ListViewDevice.CurrentCacheItemsSource.Count > var.VarListIndex)
                    {
                        //ListViewItem item = ListViewDevice.CurrentCacheItemsSource[var.VarListIndex];
                        ListViewItem item = null;
                        foreach (ListViewItem lv in ListViewDevice.CurrentCacheItemsSource)
                        {
                            //if (((IVariable)lv.Tag).Name == var.Name) dong 2017/6/13 --
                            if (((IVariable)lv.Tag).ID == var.ID) // dong 2017/7/3 ++
                            {
                                item = lv;
                                break;
                            }
                        }
                        if (item != null)
                        {

                            item.SubItems[4].Text = var.ValueType.ToString();
                            item.SubItems[5].Text = var.Value.ToString();
                            item.SubItems[7].Text = DateTime.Now.ToString();
                            item.SubItems[8].Text = ((COMM_QUALITY_STATUS)var.Quality).ToString();
                            item.SubItems[9].Text = var.Counter.ToString();

                            //已经改为定时器定时更新
                            //ListViewDevice.Invalidate(); //局部更新
                            //Debug.WriteLine(var.Name + "   " + var.Counter);
                        }
                        //watch.Stop();
                        //Debug.WriteLine("查找变量时间:" + watch.ElapsedMilliseconds / 1000f + "/" + ListViewDevice.CurrentCacheItemsSource.Count);
                    }
                }
            }
        }

        /// <summary>
        /// 更新设备树图标
        /// </summary>
        /// <param name="nodetext">设备名称</param>
        /// <param name="NodeStatus">状态1：离线 0：在线</param>
        /// 
        public delegate void UpdateTreedelegate(object sender, string nodetext, NodeStateEnum NodeStatus);
        public void UpdateTree(object sender, string nodetext, NodeStateEnum NodeStatus)
        {
            if (trvDevice.InvokeRequired)
            {
                trvDevice.BeginInvoke(new UpdateTreedelegate(UpdateTree), new object[] { sender, nodetext, NodeStatus });
            }
            else
            {
                try
                {

                    if (sender == null && NodeStatus == NodeStateEnum.None) //重载设备树
                    {
                        currentPrejectPath = nodetext;
                        this.Text = "数据采集服务器-IOSERVER[" + currentPrejectPath + "]";

                        ChanList2TreeView();
                        SetOpenState();
                        //if (ServerConfig.DataBaseEnable&& g_bConnDbOK)
                        //{
                        //    ProjectMng.ResoreFormList(Rtdb.DesignFormList);
                        //}
                        ShowMainTabPage(MainTabPage.Log);
                        bExistProject = true;
                        mruManager.Add(nodetext, Project.ProductName_Server);
                        AddOperationLog(Severity.信息.ToString(), StrConst.TITLE_SYS, "", "载入文件成功:" + nodetext);


                        return;
                    }

                    TreeNode chNode = GetNodeByChannelSender(sender);
                    if (NodeStatus == NodeStateEnum.ReName)
                    {
                        try
                        {

                            TreeNode node0 = TreeviewSearch.FindNode(chNode, nodetext.Split('|')[0]);
                            if (node0 != null)
                            {
                                node0.Text = nodetext.Split('|')[1];
                            }
                        }
                        catch (Exception)
                        {

                        }
                        return;
                    }


                    TreeNode node = TreeviewSearch.FindNode(chNode, nodetext);
                    if (node != null)
                    {
                        object o = node.Tag;
                        if (o is BaseChannel)
                        {
                            BaseChannel chan = node.Tag as BaseChannel;
                            if (chan.Enable)
                            {
                                if (NodeStatus == NodeStateEnum.OffLine)        //通道离线
                                {
                                    node.ImageIndex = DevTreeImage.ChannelOffLine;
                                    node.SelectedImageIndex = DevTreeImage.ChannelOffLine;
                                    chan.Active = false;
                                }
                                else if (NodeStatus == NodeStateEnum.OnLine)   //通道在线
                                {
                                    node.ImageIndex = DevTreeImage.ChannelNormal;
                                    node.SelectedImageIndex = DevTreeImage.ChannelNormal;
                                    chan.Active = true;
                                }
                                else if (NodeStatus == NodeStateEnum.Enable)   //通道启用
                                {
                                    node.ImageIndex = DevTreeImage.ChannelNormal;
                                    node.SelectedImageIndex = DevTreeImage.ChannelNormal;
                                    chan.Active = true;
                                }
                                else if (NodeStatus == NodeStateEnum.Disable)   //通道禁用
                                {
                                    node.ImageIndex = DevTreeImage.ChannelDisable;
                                    node.SelectedImageIndex = DevTreeImage.ChannelDisable;
                                    chan.Active = false;
                                }
                            }
                        }
                        else if (o is BaseController)
                        {

                            BaseController con = node.Tag as BaseController;
                            if (con.Enable)
                            {
                                if (NodeStatus == NodeStateEnum.OffLine)            //控制器离线
                                {
                                    node.ImageIndex = DevTreeImage.ControllOffLine;
                                    node.SelectedImageIndex = DevTreeImage.ControllOffLine;
                                    con.Active = false;


                                }
                                else if (NodeStatus == NodeStateEnum.OnLine)        //控制器上线
                                {
                                    node.ImageIndex = DevTreeImage.ControllNormal;
                                    node.SelectedImageIndex = DevTreeImage.ControllNormal;
                                    con.Active = true;

                                }
                                else if (NodeStatus == NodeStateEnum.Enable)   //控制器启用
                                {
                                    node.ImageIndex = DevTreeImage.ControllNormal;
                                    node.SelectedImageIndex = DevTreeImage.ControllNormal;
                                    con.Enable = true;
                                }
                                else if (NodeStatus == NodeStateEnum.Disable)   //控制器禁用
                                {
                                    node.ImageIndex = DevTreeImage.ControllDisable;
                                    node.SelectedImageIndex = DevTreeImage.ControllDisable;
                                    con.Enable = false;
                                }
                            }
                        }
                        else if (o is BaseVariable)
                        {
                            BaseVariable var = node.Tag as BaseVariable;
                            if (var.Enable)
                            {
                                if (NodeStatus == NodeStateEnum.OffLine)            //变量离线
                                {
                                    node.ImageIndex = DevTreeImage.VariableOffLine;
                                    node.SelectedImageIndex = DevTreeImage.VariableOffLine;
                                    var.Active = false;

                                }
                                else if (NodeStatus == NodeStateEnum.OnLine)        //变量上线
                                {
                                    node.ImageIndex = DevTreeImage.VariableNormal;
                                    node.SelectedImageIndex = DevTreeImage.VariableNormal;
                                    var.Active = true;

                                }
                                else if (NodeStatus == NodeStateEnum.Enable)   //变量启用
                                {
                                    node.ImageIndex = DevTreeImage.VariableNormal;
                                    node.SelectedImageIndex = DevTreeImage.VariableNormal;
                                    var.Active = true;
                                }
                                else if (NodeStatus == NodeStateEnum.Disable)   //变量禁用
                                {
                                    node.ImageIndex = DevTreeImage.VariableDisable;
                                    node.SelectedImageIndex = DevTreeImage.VariableDisable;
                                    var.Active = false;
                                }
                            }

                        }
                    }
                    //Application.DoEvents();
                    //trvDevice.Invalidate();
                }
                catch (Exception ex)
                {
                    Logger.GetInstance().LogError(ex.ToString());
                }

            }

        }

        private TreeNode GetNodeByChannelSender(object sender)
        {
            if (sender is IChannel)
            {
                foreach (TreeNode node in trvDevice.Nodes)
                {
                    foreach (TreeNode ch in node.Nodes)
                        if (ch.Tag == sender)
                            return ch;
                }
            }
            return null;
        }
        private void mQTT设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormMqttConfig config = new FormMqttConfig(_DriverMng);
            config.ShowDialog();
        }

        private void context_L4_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void btnStartService_Click(object sender, EventArgs e)
        {
            if (IsAdministrator())
            {
                if (MessageBox.Show("启动服务将自动关闭窗体，请保存好数据后再操作！", "操作确认", MessageBoxButtons.OKCancel) == DialogResult.OK)

                {
                    StartService("IOService", true);
                    this.ForceClose();

                }
            }
            else
            {
                MessageBox.Show("此项操作需要管理员权限，请以管理员重新运行本软件！");
            }
            if (IsAdministrator())
            {
                if (ServiceStatus())
                {
                    btnStartService.Enabled = false;
                    btnStopService.Enabled = true;
                }
                else
                {
                    btnStartService.Enabled = true;
                    btnStopService.Enabled = false;
                }
            }
        }
        public static bool IsAdministrator()
        {
            //获得当前登录的Windows用户标示
            System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
            //判断当前登录用户是否为管理员
            if (principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
            {
                //如果是管理员，则直接运行
                return true;
            }
            return false;
        }
        public bool ServiceStatus()
        {
            try
            {
                using (System.ServiceProcess.ServiceController sc = new System.ServiceProcess.ServiceController("IOService", "localhost"))
                {

                    TimeSpan timeout = new TimeSpan(0, 0, 15);

                    if (sc.Status == ServiceControllerStatus.Running)
                    {
                        return true;
                    }
                    else
                        return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }

        }
        public string StartService(string serviceName, bool serviceFlag)
        {
            try
            {
                using (System.ServiceProcess.ServiceController sc = new System.ServiceProcess.ServiceController(serviceName, "localhost"))
                {


                    TimeSpan timeout = new TimeSpan(0, 0, 15);
                    //开
                    if (serviceFlag)
                    {
                        if (sc.Status != ServiceControllerStatus.Running)
                        {
                            sc.Start();
                            sc.WaitForStatus(ServiceControllerStatus.Running, timeout);
                        }
                    }
                    else
                    {
                        if (sc.Status != ServiceControllerStatus.Stopped)
                        {
                            sc.Stop();
                            sc.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
            var msg = "";

            if (serviceFlag)
            { msg = "开启 " + serviceName + "  服务成功"; }
            else
            {
                msg = "关闭 " + serviceName + " 服务成功";
            }
            return msg;

        }

        private void btnStopService_Click(object sender, EventArgs e)
        {
            if (IsAdministrator())
            {
                MessageBox.Show(StartService("IOService", false));
               
           
            }
            else
            {
                MessageBox.Show("此项操作需要管理员权限，请以管理员重新运行本软件！");
            }
            if (IsAdministrator())
            {
                if (ServiceStatus())
                {

                    btnStartService.Enabled = false;
                    btnStopService.Enabled = true;
                }
                else
                {
                    btnStartService.Enabled = true;
                    btnStopService.Enabled = false;
                }
            }
        }

        private void tspStandby_Click(object sender, EventArgs e)
        {
            FormBackup ba = new FormBackup(_DriverMng);
            ba.ShowDialog();
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void tspPriority_Click(object sender, EventArgs e)
        {
            ListViewItem item = ListViewDevice.GetFirstSelectItem();
            if (item != null)
            {
                IVariable currentVar = item.Tag as IVariable;
                FormPriority frm = new FormPriority(currentVar);

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if (currentVar.ReadOnly == false)
                    {
                        // currentVar.ControllerObject.ChannelObject.ExecCommand(currentVar.ControllerObject,)
                    }

                }
            }
        }

        private void tspExportController_Click(object sender, EventArgs e)
        {
            if (trvDevice.SelectedNode == null) return;
            try
            {
                IChannel currentChan = trvDevice.SelectedNode.Tag as IChannel;
                if (currentChan == null) return;

                List<IController> list = currentChan.ConList;
                List<ExportController> conList = new List<ExportController>();
                foreach(IController con in list)
                {
                    ExportController ec = new ExportController
                    {
                        ID = con.ID,
                        ChID = con.ChannelObject.ID,
                        IOServerID = ServerConfig.CloudClientID,
                        Name = con.Name,

                    };
                    
                    conList.Add(ec);
                    foreach(IVariable va in con.VarList)
                    {
                        ec.VarList.Add(new ExportVariable
                        {
                            ID = va.ID,
                            Name=va.Name
                        });
                     }
                }

                string json = JsonConvert.SerializeObject(conList);

                try
                {
                    string expPath = "";
                  
                    dlgSave.Filter = "JSON文件(*.json)|*.json|All files (*.*)|*.*";
                    dlgSave.FilterIndex = 0;
                    dlgSave.RestoreDirectory = true;
                    dlgSave.Title = "导出文件";
                    dlgSave.FileName = currentChan.Name;
                    if (dlgSave.ShowDialog() == DialogResult.OK)
                    {
                        expPath = dlgSave.FileName;

                    }
                   
                    if (expPath.Trim() == "")
                    {
                        //MessageBox.Show("文件名输入不正确，文件不能正常保存！");
                        MessageBox.Show("文件名输入不正确，文件不能正常保存！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                        return;
                    }

                    File.WriteAllText(expPath, json, Encoding.UTF8); 
                }
                catch
                {
                    //Log("保存数据文件出错！数据文件：" + currentFile);

                    string msg = string.Format("导出数据文件出错!");
                    AddOperationLog(StrConst.SERVERITY_ERR, StrConst.TITLE_SYS, "", msg);


                }

            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }
        }

        private void tspExportJson_Click(object sender, EventArgs e)
        {
            try
            {
               
                List<ExportChannel> chlList = new List<ExportChannel>();
                foreach (IChannel currentChan in Rtdb.ChanList)
                {
                    if (currentChan.Enable)
                    {

                        ExportChannel eh = new ExportChannel
                        {
                            ChID = currentChan.ID,
                            IOServerID = ServerConfig.CloudClientID,
                            Name=currentChan.Name,
                        };

                        chlList.Add(eh);

                        List<IController> list = currentChan.ConList;
                        List<ExportController> conList = new List<ExportController>();
                        eh.ConList = conList;
                        foreach (IController con in list)
                        {
                            ExportController ec = new ExportController
                            {
                                ID = con.ID,
                                ChID = con.ChannelObject.ID,
                                IOServerID = ServerConfig.CloudClientID,
                                Name = con.Name,
                            };

                            conList.Add(ec);
                            foreach (IVariable va in con.VarList)
                            {
                                ec.VarList.Add(new ExportVariable
                                {
                                    ID = va.ID,
                                    Name = va.Name,
                                    DeviceType=va.DeviceType,
                                    DeviceName=va.DeviceName,
                                    DeviceIndex=va.DeviceIndex
                                });
                            }
                        }

                      
                    }
                }
                string json = JsonConvert.SerializeObject(chlList);

                try
                {
                    string expPath = "";

                    dlgSave.Filter = "JSON文件(*.json)|*.json|All files (*.*)|*.*";
                    dlgSave.FilterIndex = 0;
                    dlgSave.RestoreDirectory = true;
                    dlgSave.Title = "导出文件";
                    dlgSave.FileName = "IOSERVER.JSON";
                    if (dlgSave.ShowDialog() == DialogResult.OK)
                    {
                        expPath = dlgSave.FileName;

                    }

                    if (expPath.Trim() == "")
                    {
                        //MessageBox.Show("文件名输入不正确，文件不能正常保存！");
                        MessageBox.Show("文件名输入不正确，文件不能正常保存！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                        return;
                    }

                    File.WriteAllText(expPath, json, Encoding.UTF8);
                }
                catch
                {
                    //Log("保存数据文件出错！数据文件：" + currentFile);

                    string msg = string.Format("导出数据文件出错!");
                    AddOperationLog(StrConst.SERVERITY_ERR, StrConst.TITLE_SYS, "", msg);


                }

            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }

        }

        private void tspHistoryView_Click(object sender, EventArgs e)
        {
            FormHisDataView his = new FormHisDataView();
            his.ShowDialog();
        }
    }
    #region Sourcegrid控制器类
    public class CellCursor : SourceGrid.Cells.Controllers.MouseCursor
    {
        public CellCursor()
            : base(Cursors.Cross, true)
        {
        }
    }

    public class PopupMenu : SourceGrid.Cells.Controllers.ControllerBase
    {
        ContextMenu menu = new ContextMenu();
        public PopupMenu()
        {
            menu.MenuItems.Add("Menu 1", new EventHandler(Menu1_Click));
            menu.MenuItems.Add("Menu 2", new EventHandler(Menu2_Click));
        }

        public override void OnMouseUp(SourceGrid.CellContext sender, MouseEventArgs e)
        {
            base.OnMouseUp(sender, e);

            if (e.Button == MouseButtons.Right)
                menu.Show(sender.Grid, new Point(e.X, e.Y));
        }

        private void Menu1_Click(object sender, EventArgs e)
        {
            //TODO Your code here
        }
        private void Menu2_Click(object sender, EventArgs e)
        {
            //TODO Your code here
        }
    }

    public class CellClickEvent : SourceGrid.Cells.Controllers.ControllerBase
    {
        public override void OnClick(SourceGrid.CellContext sender, EventArgs e)
        {
            base.OnClick(sender, e);

            MessageBox.Show(sender.Grid, sender.DisplayText);
        }
    }
    /// <summary>
    /// SourceGrid 数值改变控制器
    /// </summary>
    public class ControllerValueChanged : SourceGrid.Cells.Controllers.ControllerBase
    {
        public ControllerValueChanged()
        {

        }
        public delegate void OnValueChangedDelegate(SourceGrid.CellContext sender, EventArgs e);
        public event OnValueChangedDelegate OnValueChangedEvent;

        public override void OnValueChanged(SourceGrid.CellContext sender, EventArgs e)
        {
            base.OnValueChanged(sender, e);
            if (OnValueChangedEvent != null)
                OnValueChangedEvent(sender, e);
        }
    }

    public class KeyEvent : SourceGrid.Cells.Controllers.ControllerBase
    {
        //private frmSample26 mFrm;
        //public KeyEvent(frmSample26 frm)
        //{
        //    mFrm = frm;
        //}
        //public override void OnKeyDown(SourceGrid.CellContext sender, KeyEventArgs e)
        //{
        //    base.OnKeyDown(sender, e);

        //    mFrm.SetKeyDownLabel(e.KeyCode.ToString());
        //}
    }
    public class ExportChannel
    {

        public string Name { get; set; }
        public string ChID { get; set; }
        public string IOServerID { get; set; }
       
        private List<ExportController> _ConList = new List<ExportController>();
        public List<ExportController> ConList
        {
            get { return _ConList; }
            set { _ConList = value; }
        }

    }
    public class ExportController
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string ChID { get; set; }
        public string IOServerID { get; set; }
     
        private List<ExportVariable> _VarList = new List<ExportVariable>();
        public List<ExportVariable> VarList{
            get { return _VarList; } 
        }

    }
    public class ExportVariable
    {
        public string DeviceType { get; set; }
        public string DeviceName { get; set; }
        public string DeviceIndex { get; set; }
        public string Name { get; set; }
        public string ID { get; set; }
       
    }
    #endregion




}

