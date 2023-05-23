using System.Web.Mvc;

namespace GHRESTFul.Server.Unmarshalling.Resolver
{
    public class AcceptPostPutAndPatchVerbs : IAcceptHttpVerb
    {
        #region IAcceptHttpVerb Members

        public bool IsValid(ControllerContext context)
        {
            return context.RequestContext.HttpContext.Request.HttpMethod.Equals("POST") ||
                   context.RequestContext.HttpContext.Request.HttpMethod.Equals("PUT") ||
                   context.RequestContext.HttpContext.Request.HttpMethod.Equals("PATCH");
        }

        #endregion
    }

    public class AcceptPostPutAndPatchVerbsApi : IAcceptHttpVerbApi
    {
        #region IAcceptHttpVerbApi Members

        public bool IsValid(System.Web.Http.Controllers.HttpActionContext context)
        {
            return context.Request.Method.Method.Equals("POST") ||
                   context.Request.Method.Method.Equals("PUT") ||
                   context.Request.Method.Method.Equals("PATCH");
        }

        #endregion
    }
}