using System.Web.Mvc;
using GHRESTFul.Server.Http;
using GHRESTFul.Server.MediaTypes;

namespace GHRESTFul.Server.Results.Chooser
{
    public interface IResultChooser
    {
        ActionResult BasedOnMediaType(ActionExecutedContext context, IMediaType type,
                                      IRequestInfoFinder requestInfoFinder);
    }
}