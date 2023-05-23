using System.Net;
using GHRESTFul.Server.Results.Decorators;

namespace GHRESTFul.Server.Results
{
    public class ServiceUnavailable : RestfulieResult
    {
        public override ResultDecorator GetDecorators()
        {
            return new StatusCode((int) HttpStatusCode.ServiceUnavailable);
        }
    }
}