using GHIBMS.Common;
using GHIBMS.Common.BaseClass;
using GHIBMS.Interface;
using GHIBMS.Pub;
using GHIBMS.Server;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.ServiceProcess;
using System.Threading;
using System.Xml;

namespace GUHEIOService
{
    public partial class IOService : ServiceBase
    {
        //public static System.Diagnostics.PerformanceCounter CpuPerformanceCounter = new System.Diagnostics.PerformanceCounter("Processor", "% Processor Time", "_Total");
        //private PerformanceCounter MemoPerformanceCounter = new PerformanceCounter("Memory", "Available MBytes");

        DriverMng _DriverMng;
        public IOService()
        {
            InitializeComponent();

        }

        protected override void OnStart(string[] args)
        {
            StartProject();
        }
        public void StartProject()
        {
            try
            {
                string assemblyFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                string assemblyDirPath = Path.GetDirectoryName(assemblyFilePath);
                //ServerConfig.APP_PATH = assemblyDirPath + "\\";
                ServerConfig.APP_PATH = AppDomain.CurrentDomain.BaseDirectory;
                ServerConfig.loadFromFile();
                //加载通讯插件
                PluginMng.PlugLoad();
                _DriverMng = new DriverMng();
                _DriverMng.OnAddOperationLog += AddOperationLog;

                Logger.GetInstance().LogMsg("IOServer开始启动");
                LoadProject();
                _DriverMng.ConnectedDrive();
                Thread.Sleep(1000);
                //UpLoadToCloud_V2();
                //Logger.GetInstance().LogMsg("IOServer云同步完成");
                //StartVBS();
                //StartChannel();
                Logger.GetInstance().LogMsg("IOServer启动完成");
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }
        }
        protected override void OnStop()
        {
            try
            {
                // DisConnectedDrive();
                Logger.GetInstance().LogMsg("IOServer停止");
                Thread.Sleep(1000);
                ////停止服务
                //netService.Stop();
                //IISWebServer_.Stop();
                Almdb.RemoveAllAlm();
                //string currentPrejectPath = ServerConfig.ProjectPath;
                // ProjectMng.SaveToXml(currentPrejectPath);
                //Logger.GetInstance().LogMsg("保存配置成功！");
                _DriverMng.DisConnectedDrive();
                _DriverMng.StopMqtt();
                _DriverMng = null;
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }
            //服务结束执行代码
        }
        protected override void OnPause()
        {
            //服务暂停执行代码
            base.OnPause();
        }
        protected override void OnContinue()
        {
            //服务恢复执行代码
            base.OnContinue();
        }
        protected override void OnShutdown()
        {
            //系统即将关闭执行代码
            base.OnShutdown();
        }
        private void LoadProject()
        {


            if (File.Exists(ServerConfig.ProjectPath))
            {
                string currentPrejectPath = ServerConfig.ProjectPath;

                Rtdb.ChanList.Clear();
                if (!ProjectMng.LoadFromXml(currentPrejectPath))
                {
                    Logger.GetInstance().LogMsg("加载数据文件失败，请检查是否注册对应插件！\r\n文件名" + ServerConfig.ProjectPath);
                }
                Logger.GetInstance().LogMsg("加载数据文件成功！\r\n文件名" + ServerConfig.ProjectPath);
                Logger.GetInstance().LogMsg("通讯通道数量：" + Rtdb.ChanList.Count.ToString());
            }
            else
            {
                Logger.GetInstance().LogMsg("自动加载数据文件不存在！\r\n文件名" + ServerConfig.ProjectPath);
            }
        }


        public void AddOperationLog(String Severity, String Title, String MachineName, String Message)
        {
            Logger.GetInstance().LogMsg("操作记录：" + Title + "   " + MachineName + "   " + Message);
        }


        private string GetInternalIP()
        {
            //var ipAddr = NetworkInterface.GetAllNetworkInterfaces()
            //  .Select(n => n.GetIPProperties())
            //  .SelectMany(n => n.UnicastAddresses)
            //  .Where(n => AddressFamily.InterNetwork == n.Address.AddressFamily && !IPAddress.IsLoopback(n.Address))
            //  .FirstOrDefault();
            //return ipAddr?.Address.ToString() ?? "?";
            List<string> ret = new List<string>();
            ///获取本地的IP地址
            string AddressIP = string.Empty;
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    ret.Add(_IPAddress.ToString());
                }
            }
            return ret.Count > 0 ? ret[0] : "";

        }
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
                licOK = rsa.CheckSoftRegCodeByInput(StrConst.SOFTNAME, licRegCode);

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
    }
}
