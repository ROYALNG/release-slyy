using System.Web.Mvc;
using GHRESTFul.Server.Results;

namespace GHRESTFul.Server.Extensions
{
    public static class ActionResultExtensions
    {
        public static bool IsRestfulieResult(this ActionResult actionResult)
        {
            return actionResult is RestfulieResult;
        }
    }
}