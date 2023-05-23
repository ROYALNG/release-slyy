using System;
using System.IO;
using System.Windows.Forms;

namespace GHIBMS.Server
{

    static class Program
    {
        private static string _apppath = "";
        public static string AppPath
        {
            get
            {
                if (_apppath != "")
                    return _apppath;
                _apppath = (Application.StartupPath.ToLower());
                _apppath = _apppath.Replace(@"\bin\debug", @"\").Replace(@"\bin\release", @"\");
                _apppath = _apppath.Replace(@"\bin\x86\debug", @"\").Replace(@"\bin\x86\release", @"\");
                _apppath = _apppath.Replace(@"\bin\x64\debug", @"\").Replace(@"\bin\x64\release", @"\");

                _apppath = _apppath.Replace(@"\\", @"\");

                if (!_apppath.EndsWith(@"\"))
                    _apppath += @"\";
                Directory.SetCurrentDirectory(_apppath);
                return _apppath;
            }
        }
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)

        {
            if ((args != null) && (args.Length > 0))
            {
                string filePath = "";
                for (int i = 0; i < args.Length; i++)
                {
                    filePath += args[i];
                }
                FormMain.MainArgs = filePath.Trim();
                //FormMain.MainArgs = args[0];
            }

            try
            {

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormMain());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
