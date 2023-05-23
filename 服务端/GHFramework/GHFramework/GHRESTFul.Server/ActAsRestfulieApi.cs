
using GHRESTFul.Server.Configuration;
using GHRESTFul.Server.Http;
using GHRESTFul.Server.MediaTypes;
using GHRESTFul.Server.Negotiation;
using GHRESTFul.Server.ResultsApi;
using GHRESTFul.Server.ResultsApi.Chooser;
using GHRESTFul.Server.Unmarshalling;
using GHRESTFul.Server.Unmarshalling.Resolver;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

using System.Linq;

namespace GHRESTFul.Server
{
    public class ActAsRestfulieApi :  ActionFilterAttribute
    {
        private IMediaType mediaType;
        private readonly IContentTypeToMediaType contentType;
        private readonly IRequestInfoFinderFactoryApi requestInfoFactory;
        private readonly IUnmarshallerResolverApi unmarshallerResolver;
        private readonly IResultChooser choose;
        private IRequestInfoFinder requestInfo;
        private readonly IAcceptHeaderToMediaType acceptHeader;

        public ActAsRestfulieApi()
        {
            var mediaTypesList = ConfigurationStore.Get().MediaTypeList;
            acceptHeader = new AcceptHeaderToMediaType(mediaTypesList);
            contentType = new ContentTypeToMediaType(mediaTypesList);
            requestInfoFactory = new RequestInfoFinderFactoryApi();
            unmarshallerResolver = new UnmarshallerResolverApi(new AcceptPostPutAndPatchVerbsApi());
            choose = new ResultChooser();
        }

        public ActAsRestfulieApi(IAcceptHeaderToMediaType acceptHeader, IContentTypeToMediaType contentType,
            IRequestInfoFinderFactoryApi requestInfoFactory, IResultChooser choose, IUnmarshallerResolverApi unmarshallerResolver)
        {
            this.acceptHeader = acceptHeader;
            this.contentType = contentType;
            this.requestInfoFactory = requestInfoFactory;
            this.choose = choose;
            this.unmarshallerResolver = unmarshallerResolver;
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            //actionExecutedContext.Result = choose.BasedOnMediaType(actionExecutedContext, mediaType, requestInfo);
            actionExecutedContext.Response = choose.BasedOnMediaType(actionExecutedContext, mediaType, requestInfo);

            base.OnActionExecuted(actionExecutedContext);
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            try
            {
                GetRequestInfo(actionContext);
                GetMediaType();
                DoUnmarshalling(actionContext);
            }
            catch(AcceptHeaderNotSupportedException)
            {
                //actionContext.Result = new NotAcceptable();
                var not = new NotAcceptable();
                not.MediaType = mediaType;
                not.RequestInfo = requestInfo;
                not.ExecuteResult();
                actionContext.Response = not;
                return;
            }
            catch(ContentTypeNotSupportedException)
            {
                // let asp.net mvc try to unmarshall!
            }
            catch(UnmarshallingException)
            {
                //actionContext.Result = new BadRequest();
                var bad = new BadRequest();
                bad.MediaType = mediaType;
                bad.RequestInfo = requestInfo;
                bad.ExecuteResult();
                actionContext.Response = bad;
                return;
            }

            base.OnActionExecuting(actionContext);
        }

        private void GetMediaType()
        {
            mediaType = acceptHeader.GetMediaType(requestInfo.GetAcceptHeader());
        }

        public void GetRequestInfo(HttpActionContext filterContext)
        {
            requestInfo = requestInfoFactory.BasedOn(filterContext.Request);
        }

        private void DoUnmarshalling(HttpActionContext filterContext)
        {
            unmarshallerResolver.DetectIn(filterContext);

            if (unmarshallerResolver.HasResource)
            {
                var cType = requestInfo.GetContentType();
                if (string.IsNullOrEmpty(cType))
                    return;
                //此处mvc4底层会自动把请求数据转换为对象实体
                var requestMediaType = contentType.GetMediaType(cType);

                var resource = requestMediaType.BuildUnmarshaller().Build(requestInfo.GetContent(), unmarshallerResolver.ParameterType);
                if (resource != null)
                {
                    //filterContext.ActionParameters[unmarshallerResolver.ParameterName] = resource;
                    filterContext.ActionArguments[unmarshallerResolver.ParameterName] = resource;
                }

                
            }
        }
    }

}
