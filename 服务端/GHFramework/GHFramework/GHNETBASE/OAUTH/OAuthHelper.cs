using DotNetOpenAuth.OAuth2;
using GHIBMS.Common;
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
        private string ServerCfgPath = "/Resource/IOServer/Config/{0}";

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
        /// 登录 采用OAth2.0 密码模式（resource owner password credentials）认证授权
        /// </summary>
        /// <param name="authUrl">授权基地址</param>
        /// <param name="rbacUrl">用户角色资源基地址</param>
        /// <param name="clientIdentifier">应用ID</param>
        /// <param name="clientSecret">应用KEY</param>
        /// <param name="userName">登录用户名</param>
        /// <param name="password">登录密码</param>
        public bool Login(string authUrl, string rbacUrl, 
            string clientIdentifier, string clientSecret, 
            string userName, string password,out string  errormsg)
        {
            try
            {
                if (!authUrl.ToLower().Contains("http://"))
                    authUrl = "http://"+authUrl;
                errormsg = "";
                AuthorizationServerBaseAddress = authUrl;
                //ResourceServerBaseAddress = rbacUrl;
                //ServerCfgPath = string.Format(ServerCfgPath, clientIdentifier);

                InitializeWebServerClient(clientIdentifier, clientSecret);

                //Console.WriteLine("Requesting Token...");

                try
                {
                    RequestToken(userName, password);
                }
                catch(Exception ex)
                {
                    string msg = "用户名和密码认证失败！认证服务器" + authUrl + "用户名：" + userName + "密码：" + password;
                    errormsg = msg;
                    return false;
                }

                //try
                //{
                //    string err;
                //   if (AccessProtectedResource(out err ))
                //    {
                //        errormsg = err;
                //        return true;
                //    }else
                //    {
                //        errormsg = err;
                //        return false;
                //    }
                 

                //}
                //catch
                //{
                //    string msg = "获取授权的资源失败！认证服务器" + authUrl + "用户名：" + userName + "密码：" + password;
                //    errormsg = msg;
                //    return false;
                //}
                
            }catch(Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
                Logger.GetInstance().LogMsg("登录认证失败！");
                errormsg = "登录认证失败!";
                return false;
            }
            return true;
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

      
    }
}
