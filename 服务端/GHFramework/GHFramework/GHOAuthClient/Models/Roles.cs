using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GHOAuthClient.Models
{
    public class Roles
    {
        public const string None = "";
        public const string Anonymous = "Anonymous";
        public const string Authenticated = "Authenticated";
        public const string GlobalAdmin = "Admin";
    }
}