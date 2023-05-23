using System.IO;
using System.Web;

namespace GHRESTFul.Server.Http
{
    public class RequestInfoFinder : IRequestInfoFinder
    {
        private readonly HttpContextBase httpContext;

        public RequestInfoFinder(HttpContextBase httpContext)
        {
            this.httpContext = httpContext;
        }

        #region IRequestInfoFinder Members

        public string GetAcceptHeader()
        {
            return httpContext.Request.Headers["accept"];
        }

        public string GetContentType()
        {
            return httpContext.Request.ContentType;
        }

        public string GetUrl()
        {
            return httpContext.Request.Url.AbsoluteUri;
        }

        public string GetContent()
        {
            return new StreamReader(httpContext.Request.InputStream).ReadToEnd();
        }

        #endregion
    }

    public class RequestInfoFinderApi : IRequestInfoFinder
    {
        private readonly System.Net.Http.HttpRequestMessage httpContext;

        public RequestInfoFinderApi(System.Net.Http.HttpRequestMessage httpContext)
        {
            this.httpContext = httpContext;
        }

        #region IRequestInfoFinder Members

        public string GetAcceptHeader()
        {
            //return httpContext.Request.Headers["accept"];
            return httpContext.Headers.Accept.ToString();
        }

        public string GetContentType()
        {
            if (httpContext.Content.Headers.ContentType == null)
                return "";
            //return httpContext.Request.ContentType;
            return httpContext.Content.Headers.ContentType.MediaType;
        }

        public string GetUrl()
        {
            //return httpContext.Request.Url.AbsoluteUri;
            return httpContext.RequestUri.AbsoluteUri;
        }

        public string GetContent()
        {
            //return new StreamReader(httpContext.Request.InputStream).ReadToEnd();
            var task = httpContext.Content.ReadAsStreamAsync();
            task.Result.Position = 0;
            StreamReader sr = new StreamReader(task.Result);
            var ret = sr.ReadToEnd();
            sr.Close();
            task.Result.Close();
            return ret;
        }

        #endregion
    }
}