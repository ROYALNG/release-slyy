using System.ServiceProcess;

namespace GUHEIOService
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new IOService()
            };
            ServiceBase.Run(ServicesToRun);
            IOService s = new IOService();
            //s.StartProject() ;
        }
    }
}
