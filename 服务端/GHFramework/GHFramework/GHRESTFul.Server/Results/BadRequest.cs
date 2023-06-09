﻿using System.Linq;
using System.Net;
using GHRESTFul.Server.Results.Decorators;

namespace GHRESTFul.Server.Results
{
    public class BadRequest : RestfulieResult
    {
        public BadRequest() {}
        public BadRequest(object model) : base(model) {}

        public override ResultDecorator GetDecorators()
        {
            return new StatusCode((int) HttpStatusCode.BadRequest,
                                  new ContentType(MediaType.Synonyms.First(),
                                                  new Content(BuildContent())));
        }
    }
}