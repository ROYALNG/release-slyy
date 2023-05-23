using System.Web.Mvc;
using GHRESTFul.Server.Http;
using GHRESTFul.Server.MediaTypes;
using GHRESTFul.Server.ResultsApi.Decorators;
using System.Net.Http;

namespace GHRESTFul.Server.ResultsApi
{
    public abstract class RestfulieResult : HttpResponseMessage //: ActionResult
    {
        protected RestfulieResult() {}

        protected RestfulieResult(object model)
        {
            Model = model;
        }

        public object Model { get; private set; }
        public IMediaType MediaType { get; set; }
        public IRequestInfoFinder RequestInfo { get; set; }

        protected string BuildContent()
        { 
            return Model != null ? MediaType.BuildMarshaller().Build(Model, RequestInfo) : string.Empty;
        }

        public abstract ResultDecorator GetDecorators();

        public virtual void ExecuteResult()//HttpResponseMessage responseMsg
        {
            GetDecorators().Execute(this);
        }
    }
}