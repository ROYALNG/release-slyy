//using System.Web.Mvc;
using GHRESTFul.Server.Extensions;
using GHRESTFul.Server.Http;
using GHRESTFul.Server.MediaTypes;
using System.Web.Http.Filters;

namespace GHRESTFul.Server.ResultsApi.Chooser
{
    public class ResultChooser : IResultChooser
    {
        #region IResultChooser Members

        public System.Net.Http.HttpResponseMessage BasedOnMediaType(HttpActionExecutedContext context, IMediaType type, IRequestInfoFinder requestInfoFinder)
        {
            if (!(context.Response is RestfulieResult))
                return context.Response;

            //if (type is HTML && (context.Response is OK || context.Response is Created))
            //    return AspNetResult(context);

            return RestfulieResult(context, type, requestInfoFinder);
        }

        #endregion

        private RestfulieResult RestfulieResult(HttpActionExecutedContext context, IMediaType type, IRequestInfoFinder requestInfoFinder)
        {
            var result = (RestfulieResult)context.Response;
            result.MediaType = type;
            result.RequestInfo = requestInfoFinder;
            result.ExecuteResult();

            return result;
        }

        //private RestfulieResult AspNetResult(HttpActionExecutedContext context)
        //{
        //    var result = (RestfulieResult)context.Result;

        //    var viewResult = new ViewResult
        //    {
        //        TempData = context.Controller.TempData,
        //        ViewData = context.Controller.ViewData
        //    };

        //    viewResult.ViewData.Model = result.Model;

        //    return viewResult;
        //}
    }
}