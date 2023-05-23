using GHIBMS.Common;
using GHIBMS.Pub;
using System;
using System.Reflection;
using System.Windows.Forms;


namespace GHIBMS.Server
{
    partial class AboutBox1 : Form
    {
        public AboutBox1()
        {
            InitializeComponent();
            GetLicence();
            this.Text = String.Format("关于 {0}", AssemblyTitle);
            this.labelProductName.Text = AssemblyProduct;
            this.labelVersion.Text = String.Format("版本 ：{0}", AssemblyVersion);
            this.labelCopyright.Text = AssemblyCopyright;
            this.labelCompanyName.Text = AssemblyCompany;
            //this.textBoxDescription.Text = AssemblyDescription;
            //Rtdb.CreateVariableDict();
            string type = "";
            if (licType == "0")
                type = "演示授权";
            else if (licType == "1")
                type = "试运行授权";
            else if (licType == "2")
                type = "永久授权";

            this.textBoxDescription.Text = string.Format("授权类型：{0}\r\n试运行时间{1}\r\n系统当前容量：{2}\r\n系统授权容量：{3}",
                type, licMaxTime, Rtdb.GetVarCounts(), licMaxPoint);
        }
        private string licMachaneCode = "";
        private string licStartDate = "";
        private string licType = "";
        private string licRegCode = "";
        private string licMaxTime = "";
        private string licMaxPoint = "";
        private bool licOK = false;
        private void GetLicence()
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
        }

        #region 程序集属性访问器

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion
    }
}
