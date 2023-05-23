using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HoneywellIPM
{
    public enum FindWay
    {
        FindByMac = 0 ,
        FindByIP = 1
    }
    public enum IPMConfigResult
    {
        ConfigRefused = 0, 
        ConfigAccepted = 1 ,
        ConfigError = 2
    }
    public class IPMDevice
    {
        private string _IP;
        public string IP
        {
            set { _IP = value; }
            get { return _IP; }
        }
        private string _Gateway;
        public string Gateway
        {
            set { _Gateway = value; }
            get { return _Gateway; }
        }
        private string _SubnetMask;
        public string SubnetMask
        {
            set { _SubnetMask = value; }
            get { return _SubnetMask; }
        }
        private string _AppVersion;
        public string AppVersion
        {
            set { _AppVersion = value; }
            get { return _AppVersion; }
        }
        private string _BootloaderVersion;
        public string BootloaderVersion
        {
            set { _BootloaderVersion = value; }
            get { return _BootloaderVersion; }
        }
        private int    _ModuleID;
        public int ModuleID
        {
            set { _ModuleID = value; }
            get { return _ModuleID; }
        }
        private string _MacAddr;
        public string MacAddr
        {
            set { _MacAddr = value; }
            get { return _MacAddr; }
        }
        private string _Password;
        public string Password
        {
            set { _Password = value; }
            get { return _Password; }
        }
        private int _KeypadAddr;
        public int KeypadAddr
        {
            set { _KeypadAddr = value; }
            get { return _KeypadAddr; }
        }
        private string _ServerIP;
        public string ServerIP
        {
            set { _ServerIP = value; }
            get { return _ServerIP; }
        }
        private int  _ServerPort;
        public int ServerPort
        {
            set { _ServerPort = value; }
            get { return _ServerPort; }
        }
        private string _OldPassword;
        public string OldPassword
        {
            set { _OldPassword = value; }
            get { return _OldPassword; }
        }
        private int    _ControlType;
        public int ControlType
        {
            set { _ControlType = value; }
            get { return _ControlType; }
        }
        private int    _SubControlType;
        public int SubControlType
        {
            set { _SubControlType = value; }
            get { return _SubControlType; }
        }
        private string _InstallerCode;
        public string InstallerCode
        {
            set { _InstallerCode = value; }
            get { return _InstallerCode; }
        }
        private int    _LRRAddr;
        public int LRRAddr
        {
            set { _LRRAddr = value; }
            get { return _LRRAddr; }
        }

      
    }
}
