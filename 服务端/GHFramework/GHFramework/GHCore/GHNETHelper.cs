using System;

namespace GHCore
{
    public class GHNETHelper
    {
        /// <summary>
        /// 获取 Schema + IP
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static string GetRequestSchemaIP(Uri uri)
        {
            return uri.Scheme + "://" + uri.Host;// uri.Port
        }

    }
}
