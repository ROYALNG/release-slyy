using System.Linq;
using System.Net;
using GHRESTFul.Server.ResultsApi.Decorators;

namespace GHRESTFul.Server.ResultsApi
{
    public class BadRequest : RestfulieResult
    {
        public BadRequest() {}
        public BadRequest(object model) : base(model) {}

        public override ResultDecorator GetDecorators()
        {
            return new StatusCode(HttpStatusCode.BadRequest,
                                                  new Content(BuildContent(),
                                                      new ContentType(MediaType.Synonyms.First())));
        }
    }
}