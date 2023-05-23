using System.Net.Http;
//using System.Web.Mvc;

namespace GHRESTFul.Server.ResultsApi.Decorators
{
    public class Content : ResultDecorator
    {
        private readonly string content;

        public Content(string content)
        {
            this.content = content;
        }

        public Content(string content, ResultDecorator nextDecorator) : base(nextDecorator)
        {
            this.content = content;
        }

        public override void Execute(HttpResponseMessage responseMsg)
        {
            if (!string.IsNullOrEmpty(content))
            {
                responseMsg.Content = new StringContent(content, System.Text.Encoding.UTF8);//"text/html"
            }

            Next(responseMsg);
        }
    }
}