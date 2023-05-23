using System.Net.Http;
//using System.Web.Mvc;

namespace GHRESTFul.Server.ResultsApi.Decorators
{
    public class ContentType : ResultDecorator
    {
        private readonly string contentType;

        public ContentType(string contentType)
        {
            this.contentType = contentType;
        }

        public ContentType(string contentType, ResultDecorator nextDecorator) : base(nextDecorator)
        {
            this.contentType = contentType;
        }

        public override void Execute(HttpResponseMessage responseMsg)
        {
            if (!string.IsNullOrEmpty(contentType) && responseMsg.Content != null)
                //responseMsg.Headers.Add("Content-Type", contentType);
                responseMsg.Content.Headers.ContentType.MediaType = contentType;

            Next(responseMsg);
        }
    }
}