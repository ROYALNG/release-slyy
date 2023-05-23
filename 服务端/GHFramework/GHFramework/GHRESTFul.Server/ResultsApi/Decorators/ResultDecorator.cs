using System.Net.Http;
//using System.Web.Mvc;

namespace GHRESTFul.Server.ResultsApi.Decorators
{
    public abstract class ResultDecorator
    {
        protected ResultDecorator(ResultDecorator nextDecorator)
        {
            NextDecorator = nextDecorator;
        }

        protected ResultDecorator() {}
        public ResultDecorator NextDecorator { get; private set; }

        public abstract void Execute(HttpResponseMessage responseMsg);

        protected void Next(HttpResponseMessage responseMsg)
        {
            if (NextDecorator != null)
                NextDecorator.Execute(responseMsg);
        }
    }
}