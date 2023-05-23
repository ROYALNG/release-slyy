using System.Net;
using GHRESTFul.Server.Results.Decorators;

namespace GHRESTFul.Server.Results
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
            return new StatusCode((int) HttpStatusCode.SeeOther,
                                  new Location(location));
        }
    }
}