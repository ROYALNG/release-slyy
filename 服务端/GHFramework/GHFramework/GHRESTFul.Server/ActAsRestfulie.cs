using System.Web.Mvc;
using GHRESTFul.Server.Configuration;
using GHRESTFul.Server.Http;
using GHRESTFul.Server.MediaTypes;
using GHRESTFul.Server.Negotiation;
using GHRESTFul.Server.Results;
using GHRESTFul.Server.Results.Chooser;
using GHRESTFul.Server.Unmarshalling;
using GHRESTFul.Server.Unmarshalling.Resolver;

namespace GHRESTFul.Server
{
    public class ActAsRestfulie : ActionFilterAttribute
    {
        private IMediaType mediaType;
        private readonly IContentTypeToMediaType contentType;
        private readonly IRequestInfoFinderFactory requestInfoFactory;
        private readonly IUnmarshallerResolver unmarshallerResolver;
        private readonly IResultChooser choose;
        private IRequestInfoFinder requestInfo;
        private readonly IAcceptHeaderToMediaType acceptHeader;

        public ActAsRestfulie()
        {
            var mediaTypesList = ConfigurationStore.Get().MediaTypeList;
            acceptHeader = new AcceptHeaderToMediaType(mediaTypesList);
            contentType = new ContentTypeToMediaType(mediaTypesList);
            requestInfoFactory = new RequestInfoFinderFactory();
            unmarshallerResolver = new UnmarshallerResolver(new AcceptPostPutAndPatchVerbs());
            choose = new ResultChooser();
        }

        public ActAsRestfulie(IAcceptHeaderToMediaType acceptHeader, IContentTypeToMediaType contentType,
            IRequestInfoFinderFactory requestInfoFactory, IResultChooser choose, IUnmarshallerResolver unmarshallerResolver)
        {
            this.acceptHeader = acceptHeader;
            this.contentType = contentType;
            this.requestInfoFactory = requestInfoFactory;
            this.choose = choose;
            this.unmarshallerResolver = unmarshallerResolver;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.Result = choose.BasedOnMediaType(filterContext, mediaType, requestInfo);

            base.OnActionExecuted(filterContext);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                GetRequestInfo(filterContext);
                GetMediaType();
                DoUnmarshalling(filterContext);
            }
            catch(AcceptHeaderNotSupportedException)
            {
                filterContext.Result = new NotAcceptable();
                return;
            }
            catch(ContentTypeNotSupportedException)
            {
                // let asp.net mvc try to unmarshall!
            }
            catch(UnmarshallingException)
            {
                filterContext.Result = new BadRequest();
                return;
            }

            base.OnActionExecuting(filterContext);
        }

        private void GetMediaType()
        {
            mediaType = acceptHeader.GetMediaType(requestInfo.GetAcceptHeader());
        }

        public void GetRequestInfo(ControllerContext filterContext)
        {
            requestInfo = requestInfoFactory.BasedOn(filterContext.HttpContext);
        }

        private void DoUnmarshalling(ActionExecutingContext filterContext)
        {
            unmarshallerResolver.DetectIn(filterContext);

            if (unmarshallerResolver.HasResource)
            {
                var requestMediaType = contentType.GetMediaType(requestInfo.GetContentType());

                var resource = requestMediaType.BuildUnmarshaller().Build(requestInfo.GetContent(), unmarshallerResolver.ParameterType);
                if (resource != null) filterContext.ActionParameters[unmarshallerResolver.ParameterName] = resource;
            }
        }
    }
}
