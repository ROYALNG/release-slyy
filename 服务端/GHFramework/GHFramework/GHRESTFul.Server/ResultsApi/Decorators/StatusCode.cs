using System.Net;
using System.Net.Http;
//using System.Web.Mvc;

namespace GHRESTFul.Server.ResultsApi.Decorators
{
    public class StatusCode : ResultDecorator
    {
        private readonly HttpStatusCode statusCode;

        public StatusCode(HttpStatusCode statusCode)
        {
            this.statusCode = statusCode;
        }

        public StatusCode(HttpStatusCode statusCode, ResultDecorator nextDecorator)
            : base(nextDecorator)
        {
            this.statusCode = statusCode;
        }

        public override void Execute(HttpResponseMessage responseMsg)
        {
            responseMsg.StatusCode = statusCode;
            Next(responseMsg);
        }
    }
}