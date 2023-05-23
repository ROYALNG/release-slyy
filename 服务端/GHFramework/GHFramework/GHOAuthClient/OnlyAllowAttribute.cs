using GHOAuthClient.Controllers;
using GHOAuthClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GHOAuthClient
{
    /// <summary>
    /// Constrain routes to certain <see cref="Roles"/>.  Can be placed at the class or method level.
    /// </summary>
    /// <remarks>
    /// When constrainting an entire controller/class, per-route additions can be made using the <see cref="AlsoAllowAttribute"/>.
    /// </remarks>
    public class OnlyAllowAttribute : AuthorizeAttribute
    {
        private const string ITEMS_KEY = "AlsoAllow.Roles";

        public new IEnumerable<string> Roles { get; set; }

        public OnlyAllowAttribute(string roles)
        {
            if (string.IsNullOrEmpty(roles))
                throw new ArgumentOutOfRangeException("roles");

            var rls = roles.Trim().Split(',');
            rls.Union(roles.Trim().Split(' '));
            Roles = rls;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            // method attribute allows additions to a policy set at the class level
            var alsoAllow = filterContext.ActionDescriptor.GetCustomAttributes(typeof(AlsoAllowAttribute), inherit: false).SingleOrDefault() as AlsoAllowAttribute;
            if (alsoAllow != null)
            {
                filterContext.HttpContext.Items[ITEMS_KEY] = alsoAllow.Roles;
            }

            // this will then call AuthorizeCore - one should view MS' source for OnAuthorization
            base.OnAuthorization(filterContext);
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var alsoAllow = httpContext.Items.Contains(ITEMS_KEY) ? (IEnumerable<string>)httpContext.Items[ITEMS_KEY] : Enumerable.Empty<string>();
            var allAllow = Roles.Union(alsoAllow);

            var u = httpContext.User as IPrincipalUser;
            return u != null && u.IsInRole(allAllow); // when false, HandleUnauthorizedRequest executes
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = ((StatusController)filterContext.Controller).AccessDenied();
        }
    }
    /// <summary>
    /// Specifies that an action method constrained by a class-level <see cref="OnlyAllowAttribute"/> can authorize additional <see cref="Roles"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AlsoAllowAttribute : Attribute
    {
        public IEnumerable<string> Roles { get; set; }

        public AlsoAllowAttribute(string roles)
        {
            if (string.IsNullOrEmpty(roles))
                throw new ArgumentOutOfRangeException("roles");

            var rls = roles.Trim().Split(',');
            rls.Union(roles.Trim().Split(' '));
            Roles = rls;
        }
    }

    /// <summary>
    /// Shortcut for [Allow("Admin")]
    /// </summary>
    public class AdminOnlyAttribute : OnlyAllowAttribute
    {
        public AdminOnlyAttribute() : base(Models.Roles.GlobalAdmin) { }
    }
}