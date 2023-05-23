using DotNetOpenAuth.OAuth2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace GHNETBASE.OAUTH
{
    public class OAuthHelper
    {
        #region Instance
        private static readonly OAuthHelper _instance = new OAuthHelper();
        private OAuthHelper()
        {
        }
        public static OAuthHelper SingletonInstance
        {
            get
            {
                return _instance;
            }
        }
        #endregion

        private string AuthorizePath = "/OAuth/Authorize";
        private string TokenPath = "/OAuth/Token";
        private string MePath = "/RBAC/UserRoles";

        private WebServerClient _webServerClient;
        private string _accessToken;

        private string AuthorizationServerBaseAddress;
        private string ResourceServerBaseAddress;

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get;private set; }
        /// <summary>
        /// 授权ID
        /// </summary>
        public string UUID { get;private set; }

        private List<string> roles = new List<string>();
        /// <summary>
        /// 角色集
        /// </summary>
        public List<string> Roles
        {
            get
            {
                return roles;
            }
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="authUrl">授权基地址</param>
        /// <param name="rbacUrl">用户角色资源基地址</param>
        /// <param name="clientIdentifier">应用ID</param>
        /// <param name="clientSecret">应用KEY</param>
        /// <param name="userName">登录用户名</param>
        /// <param name="password">登录密码</param>
        public void Login(string authUrl, string rbacUrl, 
            string clientIdentifier, string clientSecret, 
            string userName, string password)
        {
            AuthorizationServerBaseAddress = authUrl;
            ResourceServerBaseAddress = rbacUrl;

            InitializeWebServerClient(clientIdentifier, clientSecret);

            //Console.WriteLine("Requesting Token...");
            RequestToken(userName, password);
            //Console.WriteLine("Access Token: {0}", _accessToken);
            //Console.WriteLine("Access Protected Resource");
            AccessProtectedResource();
        }
        private void InitializeWebServerClient(string clientIdentifier, string clientSecret)
        {
            var authorizationServerUri = new Uri(AuthorizationServerBaseAddress);
            var authorizationServer = new AuthorizationServerDescription
            {
                AuthorizationEndpoint = new Uri(authorizationServerUri, AuthorizePath),
                TokenEndpoint = new Uri(authorizationServerUri, TokenPath)
            };
            _webServerClient = new WebServerClient(authorizationServer, clientIdentifier, clientSecret);
        }

        private void RequestToken(string userName, string password)
        {
            var state = _webServerClient.ExchangeUserCredentialForToken(userName, password);//scopes=null
            _accessToken = state.AccessToken;
        }

        private void AccessProtectedResource()
        {
            var resourceServerUri = new Uri(ResourceServerBaseAddress);
            var client = new HttpClient(_webServerClient.CreateAuthorizingHandler(_accessToken));

            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var responseMsg = client.GetAsync(new Uri(resourceServerUri, MePath)).Result;
            if (responseMsg.IsSuccessStatusCode)
            {
                var ur = responseMsg.Content.ReadAsStringAsync().Result;
                Console.WriteLine(ur);

                if (string.IsNullOrEmpty(ur))
                    Console.WriteLine("null value");
                //var jarr = Newtonsoft.Json.Linq.JArray.Parse(ur);
                Newtonsoft.Json.Linq.JObject jo1 = (Newtonsoft.Json.Linq.JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(ur);

                UserName = (string)jo1["Name"];
                UUID = (string)jo1["UUID"];
                //(Newtonsoft.Json.Linq.JArray)Newtonsoft.Json.JsonConvert.DeserializeObject(
                roles.Clear();
                foreach (string ro in jo1["Roles"])
                {
                    roles.Add(ro);
                }
            }
            //var body = client.GetStringAsync(new Uri(resourceServerUri, MePath)).Result;
            //Console.Read();
        }
    }
}
