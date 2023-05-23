﻿using System.Linq;
using System.Net;
using GHRESTFul.Server.Results.Decorators;

namespace GHRESTFul.Server.Results
{
    public class OK : RestfulieResult
    {
        public OK() {}
        public OK(object model) : base(model) {}

        public override ResultDecorator GetDecorators()
        {
            return new StatusCode((int) HttpStatusCode.OK,
                                  new ContentType(MediaType.Synonyms.First(),
                                                  new Content(BuildContent())));
        }
    }
}