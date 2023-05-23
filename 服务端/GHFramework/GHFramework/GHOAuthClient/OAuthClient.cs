using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetOpenAuth.OAuth2;

namespace GHOAuthClient
{
    public class OAuthClient
    {
        #region Instance
        private static readonly OAuthClient _instance = new OAuthClient();
        private OAuthClient()
        {
            AuthorizationEndpoint = System.Configuration.ConfigurationManager.AppSettings.Get("AuthorizeEndpoint");
            TokenEndpoint = System.Configuration.ConfigurationManager.AppSettings.Get("TokenEndpoint");
            ClientKey = System.Configuration.ConfigurationManager.AppSettings.Get("ClientKey");
            ClientId = System.Configuration.ConfigurationManager.AppSettings.Get("ClientID");
            authServer = new AuthorizationServerDescription
            {
                AuthorizationEndpoint = new Uri(AuthorizationEndpoint),
                TokenEndpoint = new Uri(TokenEndpoint)
            };

            oauthWebClient = new WebServerClient(authServer, clientIdentifier: ClientId, clientSecret: ClientKey);
            Authorization = null;// new AuthorizationState();

            scopes = null;// new[] { "bio", "notes" };//授权范围 要配合 资源服务器 验证
            redirectUri = System.Configuration.ConfigurationManager.AppSettings.Get("SelfServer"); //接收验证code，处理的地址
        }
        public static OAuthClient SingletonInstance
        {
            get
            {
                return _instance;
            }
        }
        #endregion
        public string[] scopes { get; set; }
        public string redirectUri { get; set; }
        public IAuthorizationState Authorization
        {
            get
            {
                return (AuthorizationState)HttpContext.Current.Session["Authorization"];
            }
            set
            {
                HttpContext.Current.Session["Authorization"] = value;
            }
        }
        private string AuthorizationEndpoint, TokenEndpoint, ClientId, ClientKey;
        private AuthorizationServerDescription authServer = null;
        private WebServerClient oauthWebClient = null;

        private bool CheckCodeExist(HttpRequestBase request)
        {
            if (request.Url.AbsoluteUri.Contains(redirectUri))
            {
                if (!string.IsNullOrEmpty(request.Params["code"]))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 获取授权服务器返回的Token,如果没有则发送请求
        /// </summary>
        /// <param name="Requst"></param>
        public void GetAccessToken(HttpRequestBase Requst)
        {
            if (!RefreshToken() && CheckCodeExist(Requst))
            {
                var authorizationState = oauthWebClient.ProcessUserAuthorization();//
                if (authorizationState != null)
                {
                    Authorization = authorizationState;
                    return;

                    //if (!string.IsNullOrEmpty(authorizationState.AccessToken))
                    //{
                    //    token = authorizationState.AccessToken;
                    //    refreshToken = authorizationState.RefreshToken;
                    //    //scopes = authorizationState.Scope.ToArray();//授权服务器返回的 scopes, 
                    //    return;
                    //}
                }
            }
            //GetAuthrizationCode();
        }
        /// <summary>
        /// 刷新Token
        /// </summary>
        /// <returns>bool</returns>
        public bool RefreshToken()
        {
            try
            {
                if (Authorization != null && Authorization.AccessTokenExpirationUtc.HasValue)
                    return oauthWebClient.RefreshAuthorization(Authorization);
            }
            catch
            { }
            return false;
        }

        public void Logout()
        {
            HttpContext.Current.Session["Authorization"] = null;
            HttpContext.Current.Session.Remove("Authorization");
            HttpContext.Current.Response.Redirect(System.Configuration.ConfigurationManager.AppSettings.Get("Logout"));
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 向授权服务器发送验证请求
        /// </summary>
        /// <param name="sps">scopes</param>
        /// <param name="returnUrl">returnUrl</param>
        public void GetAuthrizationCode(string[] sps = null, string returnUrl = "")
        {
            var userAuthorization = oauthWebClient.PrepareRequestUserAuthorization(sps == null ? scopes : sps, returnTo: new Uri(string.IsNullOrEmpty(returnUrl) ? redirectUri : returnUrl));
            userAuthorization.Send(HttpContext.Current);
        }

        public IEnumerable<string> GetResourceServer(string method, IEnumerable<string> resourceAddress, string requestInput = "")
        {
            //判断是否已授权
            if (Authorization == null)
            {
                //throw new InvalidOperationException("No access token!");
                return Enumerable.Empty<string>();
            }

            // 刷新AccessToken如果它已经过期 ,并且可以设置请求超时时间(可选参数,请求超时将不会刷新)
            if (Authorization.AccessTokenExpirationUtc.HasValue)
            {
                if (oauthWebClient.RefreshAuthorization(Authorization))//, TimeSpan.FromSeconds(30)//请求超时
                {
                    //TimeSpan timeLeft = Authorization.AccessTokenExpirationUtc.Value - DateTime.UtcNow;
                }
            }

            List<string> msg = new List<string>();
            //using (System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient())
            using (System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient(oauthWebClient.CreateAuthorizingHandler(Authorization.AccessToken)))
            {
                //httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                if (method.ToUpper() == "POST")
                {
                    System.Net.Http.StringContent content = new System.Net.Http.StringContent(requestInput);
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    foreach (var address in resourceAddress)
                    {
                        var responseMsg = httpClient.PostAsync(address, content).Result;
                        if (responseMsg.IsSuccessStatusCode)
                        {
                            msg.Add(responseMsg.Content.ReadAsStringAsync().Result);
                        }
                    }

                }
                else
                {
                    foreach (var address in resourceAddress)
                    {
                        var responseMsg = httpClient.GetAsync(address).Result;
                        if (responseMsg.IsSuccessStatusCode)
                        {
                            msg.Add(responseMsg.Content.ReadAsStringAsync().Result);
                        }
                    }
                }
            }

            return msg;
        }


    }
}