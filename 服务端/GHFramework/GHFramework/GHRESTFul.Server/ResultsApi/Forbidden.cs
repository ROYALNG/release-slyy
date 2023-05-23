using System.Net;
using GHRESTFul.Server.ResultsApi.Decorators;

namespace GHRESTFul.Server.ResultsApi
{
    public class Forbidden : RestfulieResult
    {
        public override ResultDecorator GetDecorators()
        {
            return new StatusCode(HttpStatusCode.Forbidden);
        }
    }
}