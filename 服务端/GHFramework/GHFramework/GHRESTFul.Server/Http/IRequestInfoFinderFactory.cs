using System.Web;

namespace GHRESTFul.Server.Http
{
    public interface IRequestInfoFinderFactory
    {
        IRequestInfoFinder BasedOn(HttpContextBase httpContext);
    }

    public interface IRequestInfoFinderFactoryApi
    {
        IRequestInfoFinder BasedOn(System.Net.Http.HttpRequestMessage httpContext);
    }
}