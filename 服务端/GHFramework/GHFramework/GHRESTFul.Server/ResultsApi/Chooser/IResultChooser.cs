//using System.Web.Mvc;
using GHRESTFul.Server.Http;
using GHRESTFul.Server.MediaTypes;
using System.Web.Http.Filters;

namespace GHRESTFul.Server.ResultsApi.Chooser
{
    public interface IResultChooser
    {
        System.Net.Http.HttpResponseMessage BasedOnMediaType(HttpActionExecutedContext context, IMediaType type,
                                      IRequestInfoFinder requestInfoFinder);
    }
}