using System.Linq;
using System.Net;
using GHRESTFul.Server.ResultsApi.Decorators;

namespace GHRESTFul.Server.ResultsApi
{
    public class PreconditionFailed : RestfulieResult
    {
        public PreconditionFailed() {}
        
        public PreconditionFailed(object model) 
            : base(model) {}
        
        public override ResultDecorator GetDecorators()
        {
            return new StatusCode(HttpStatusCode.PreconditionFailed,
                                   new Content(BuildContent(),
                                       new ContentType(MediaType.Synonyms.First())));
        }
    }
}