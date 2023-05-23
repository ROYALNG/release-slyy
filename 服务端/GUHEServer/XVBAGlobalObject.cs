using Microsoft.VisualBasic.CompilerServices;
namespace GHIBMS.Server
{
    [StandardModule]
    public class XVBAGlobalObject
    {
        //internal static XVBAWindowObject myWindow = null;
        internal static XVBAServerObject myServer = new XVBAServerObject();

        //public static XVBAWindowObject Window
        //{
        //    get
        //    {
        //        return myWindow;
        //    }
        //}
        public static XVBAServerObject Server
        {
            get
            {
                return myServer;
            }
        }
    }
}

