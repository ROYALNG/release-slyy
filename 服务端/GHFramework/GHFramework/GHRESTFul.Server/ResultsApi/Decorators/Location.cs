﻿using System.Net.Http;
//using System.Web.Mvc;

namespace GHRESTFul.Server.ResultsApi.Decorators
{
    public class Location : ResultDecorator
    {
        private readonly string location;

        public Location(string location)
        {
            this.location = location;
        }

        public Location(string location, ResultDecorator nextDecorator) : base(nextDecorator)
        {
            this.location = location;
        }

        public override void Execute(HttpResponseMessage responseMsg)
        {
            if (!string.IsNullOrEmpty(location))
                responseMsg.Headers.Location = new System.Uri(location, System.UriKind.RelativeOrAbsolute);

            Next(responseMsg);
        }
    }
}