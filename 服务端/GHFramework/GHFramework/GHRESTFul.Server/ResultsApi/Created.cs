using System.Linq;
using System.Net;
using GHRESTFul.Server.ResultsApi.Decorators;

namespace GHRESTFul.Server.ResultsApi
{
    public class Created : RestfulieResult
    {
        private readonly string location;

        public Created() {}

        public Created(object model, string location) : base(model)
        {
            this.location = location;
        }

        public Created(object model) : base(model) {}

        public Created(string location)
        {
            this.location = location;
        }

        public override ResultDecorator GetDecorators()
        {
            var contentType = new ContentType(MediaType.Synonyms.First());
            var content = new Content(BuildContent(), contentType);

            return new StatusCode(HttpStatusCode.Created, new Location(location, content));
        }
    }
}