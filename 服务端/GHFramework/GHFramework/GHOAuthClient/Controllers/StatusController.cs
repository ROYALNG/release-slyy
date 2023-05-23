using GHOAuthClient.Models;
using DotNetOpenAuth.OAuth2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GHOAuthClient.Controllers
{
    [OnlyAllow(Models.Roles.Authenticated)]
    public abstract class StatusController : Controller
    {
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {
                OAuthClient.SingletonInstance.GetAccessToken(Request);//获取Token
            }
            catch (Exception ex)
            {
                //GHCore.GHLogger.Log(ex.Message, GHCore.LogCategory.Exception);
            }

            if (OAuthClient.SingletonInstance.Authorization != null)
            {
                string sUrl = System.Configuration.ConfigurationManager.AppSettings.Get("RbacServer");
                try
                {
                    var userRoles = OAuthClient.SingletonInstance.GetResourceServer("GET", new[] { sUrl + "/RBAC/UserRoles" });

                    System.Security.Claims.ClaimsIdentity ident = new System.Security.Claims.ClaimsIdentity("Bearer");
                    try
                    {
                        foreach (var ur in userRoles)
                        {
                            if (string.IsNullOrEmpty(ur)) continue;
                            //var jarr = Newtonsoft.Json.Linq.JArray.Parse(ur);
                            Newtonsoft.Json.Linq.JObject jo1 = (Newtonsoft.Json.Linq.JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(ur);

                            ident.AddClaim(new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, (string)jo1["Name"]));
                            ident.AddClaim(new System.Security.Claims.Claim("GH:UUID", (string)jo1["UUID"]));
                            //(Newtonsoft.Json.Linq.JArray)Newtonsoft.Json.JsonConvert.DeserializeObject(
                            foreach (string ro in jo1["Roles"])
                            {
                                ident.AddClaim(new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, ro));
                            }
                        }
                    }
                    catch
                    {
                    }
                    filterContext.HttpContext.User = new IPrincipalUser(ident);
                    //授权通过后,供客户端调用
                    OnAuthorizationed(ident);
                }
                catch (Exception ex)
                {
                    //GHCore.GHLogger.Log(ex.Message, GHCore.LogCategory.Exception);
                }
            }

            if (filterContext.HttpContext.User != null)
            {
                filterContext.HttpContext.User = new IPrincipalUser(filterContext.HttpContext.User.Identity);
            }
        }

        /// <summary>
        /// 授权通过后调用，开放给接入客户端作初始化参数设置
        /// </summary>
        public virtual void OnAuthorizationed(System.Security.Claims.ClaimsIdentity ident)
        { }

        //[System.Web.Http.Route("denied")]
        public ActionResult AccessDenied()
        {
            //if ((HttpContext.User as IPrincipalUser).IsAnonymous)
            //{
            //    return Redirect("/login/Index?returnUrl=" + (string.IsNullOrEmpty(Request.Url.PathAndQuery) ? "" : HttpUtility.UrlEncode(Request.Url.PathAndQuery)));
            //}

            if ((HttpContext.User as IPrincipalUser).IsAnonymous)
            {
                try
                {
                    OAuthClient.SingletonInstance.GetAuthrizationCode(returnUrl: Request.Url.ToString());
                }
                catch (Exception ex)
                {
                    GHCore.GHLogger.Log(ex.Message, GHCore.LogCategory.Exception);
                }
            }

            Response.StatusCode = (int)HttpStatusCode.Forbidden;
            return View("~/Views/Shared/AccessDenied.cshtml");
        }

    }
}