using System.Linq;
using System.Net;
using GHRESTFul.Server.Results.Decorators;

namespace GHRESTFul.Server.Results
{
    public class PreconditionFailed : RestfulieResult
    {
        public PreconditionFailed() {}
        
        public PreconditionFailed(object model) 
            : base(model) {}
        
        public override ResultDecorator GetDecorators()
        {
            return new StatusCode((int)HttpStatusCode.PreconditionFailed,
                                  new ContentType(MediaType.Synonyms.First(),
                                                  new Content(BuildContent())));
        }
    }
}