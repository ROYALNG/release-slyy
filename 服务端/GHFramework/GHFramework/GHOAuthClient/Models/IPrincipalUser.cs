using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace GHOAuthClient.Models
{
    public class IPrincipalUser : GenericPrincipal//IPrincipal
    {

        //public IIdentity Identity { get; private set; }

        public string AccountName { get; private set; }
        public bool IsAnonymous { get; private set; }

        public IPrincipalUser(IIdentity identity)
            : base(identity, new string[0])
        {
            //Identity = identity;
            var i = identity as ClaimsIdentity;
            if (i == null)
            {
                IsAnonymous = true;
                return;
            }

            IsAnonymous = !i.IsAuthenticated;
            if (i.IsAuthenticated)
                AccountName = i.Name;
        }

        /// <summary>
        /// 角色名 （单个或 用","和" "分隔的多个角色名）
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public override bool IsInRole(string role)
        {
            var rls = role.Trim().Split(',');
            rls.Union(role.Trim().Split(' '));
            return IsInRole(rls);
            //Roles r;
            //return Enum.TryParse(role, out r) && IsInRole(r);
        }

        public bool IsInRole(IEnumerable<string> roles)
        {
            return roles.Any(t => Roles.Count(c => c.ToUpper() == t.ToUpper()) > 0)
                || roles.Count(c => c.ToUpper() == Models.Roles.Anonymous.ToUpper()) > 0    //匿名
                || Roles.Count(c => c.ToUpper() == Models.Roles.GlobalAdmin.ToUpper()) > 0; //管理员
        }

        //public bool IsInRole(Roles roles)
        //{
        //    return (Role & roles) != Roles.None || Role.HasFlag(Roles.GlobalAdmin);
        //}

        //private Roles? _role;
        //public Roles? RawRoles { get { return _role; } }

        /// <summary>
        /// Returns this user's role on the current site.
        /// </summary>
        public IEnumerable<string> Roles
        {
            get
            {
                //if (_role == null)
                //{
                //    if (IsAnonymous)
                //    {
                //        _role = Roles.Anonymous;
                //    }
                //    else
                //    {
                //        var result = Roles.Authenticated;

                //        if (AccountName == "admin") 
                //            result |= Roles.GlobalAdmin;


                //        _role = result;
                //    }
                //}
                //return _role.Value;
                if (IsAnonymous)
                {
                    return Enumerable.Empty<string>();//匿名
                }
                else
                {

                    var roles = (Identity as ClaimsIdentity).FindAll(ClaimTypes.Role);
                    if (roles != null && roles.Count() > 0)
                        return roles.Select(t => t.Value).Concat(new[] { Models.Roles.Authenticated });
                    else
                        return new[] { Models.Roles.Authenticated };//基本验证 无角色信息
                }
            }
        }

        public bool IsGlobalAdmin
        {
            get
            {
                return IsInRole(new[] { Models.Roles.GlobalAdmin });
            }
        }
    }
}