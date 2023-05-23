using System.Net;
using GHRESTFul.Server.ResultsApi.Decorators;

namespace GHRESTFul.Server.ResultsApi
{
    public class SeeOther : RestfulieResult
    {
        private readonly string location;

        public SeeOther(string location)
        {
            this.location = location;
        }

        public override ResultDecorator GetDecorators()
        {
            return new StatusCode(HttpStatusCode.SeeOther,
                                  new Location(location));
        }
    }
}