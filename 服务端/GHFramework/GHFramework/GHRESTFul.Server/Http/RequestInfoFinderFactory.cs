using System.Web;

namespace GHRESTFul.Server.Http
{
    public class RequestInfoFinderFactory : IRequestInfoFinderFactory
    {
        #region IRequestInfoFinderFactory Members

        public IRequestInfoFinder BasedOn(HttpContextBase httpContext)
        {
            return new RequestInfoFinder(httpContext);
        }

        #endregion
    }

    public class RequestInfoFinderFactoryApi : IRequestInfoFinderFactoryApi
    {
        #region IRequestInfoFinderFactory Members

        public IRequestInfoFinder BasedOn(System.Net.Http.HttpRequestMessage httpContext)
        {
            return new RequestInfoFinderApi(httpContext);
        }

        #endregion
    }
}