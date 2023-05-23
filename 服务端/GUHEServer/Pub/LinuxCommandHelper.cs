
using System.Runtime.InteropServices;


namespace GHIBMS.Server.Pub
{

    /// <summary>
    /// Linux命令工具类
    /// </summary>
    public class LinuxCommandHelper
    {
        private static string FileName { get; set; }
        private static string Arguments { get; set; }
        private static string WorkingDirectory { get; set; }

        /// <summary>
        /// 运行命令
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="arguments"></param>
        /// <param name="workingDirectory"></param>
        /// <returns></returns>
        public static string RunCommand(string fileName, string arguments, string workingDirectory = "")
        {
            FileName = fileName;
            Arguments = arguments;
            WorkingDirectory = workingDirectory;
            return Execute();
        }


        private static string Execute(int milliseconds = 10000)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                var rValue = "";
                using (System.Diagnostics.Process p = new System.Diagnostics.Process())
                {
                    p.StartInfo.FileName = FileName;
                    p.StartInfo.Arguments = Arguments;
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.WorkingDirectory = WorkingDirectory;
                    p.Start();
                    p.WaitForExit(milliseconds);
                    rValue = p.StandardOutput.ReadToEnd();
                }
                return rValue;
            }
            else
            {
                return "当前系统不是Linux系统，不能通过此类执行Linux命令!";
            }
        }
    }
   

}
