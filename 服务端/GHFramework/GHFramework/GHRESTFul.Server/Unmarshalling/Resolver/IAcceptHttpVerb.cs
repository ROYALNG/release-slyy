using System.Web.Mvc;

namespace GHRESTFul.Server.Unmarshalling.Resolver
{
    public interface IAcceptHttpVerb
    {
        bool IsValid(ControllerContext context);
    }

    public interface IAcceptHttpVerbApi
    {
        bool IsValid(System.Web.Http.Controllers.HttpActionContext context);
    }
}