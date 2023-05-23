using System.Linq;
using System.Net;
using GHRESTFul.Server.ResultsApi.Decorators;

namespace GHRESTFul.Server.ResultsApi
{
    public class OK : RestfulieResult
    {
        public OK() {}
        public OK(object model) : base(model) {}

        public override ResultDecorator GetDecorators()
        {
            return new StatusCode(HttpStatusCode.OK,
                                    new Content(BuildContent(), 
                                        new ContentType(MediaType.Synonyms.First())));
        }
    }
}