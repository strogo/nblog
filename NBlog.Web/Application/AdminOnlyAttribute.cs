using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace NBlog.Web.Application
{
    public class AdminOnlyAttribute : AuthorizeAttribute
    {
        public Settings Settings { get; set; }

        private readonly bool _authorize;

        public AdminOnlyAttribute()
        {
            _authorize = true;
        }

        public AdminOnlyAttribute(bool authorize)
        {
            _authorize = authorize;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            if (!_authorize) { return true; }

            var identity = httpContext.User.Identity;
            if (!identity.IsAuthenticated)
            {
                return false;
            }

            if (Settings.Admins == null || !Settings.Admins.Contains(identity.Name, StringComparer.InvariantCultureIgnoreCase))
            {
                return false;
            }

            return true;
        }
    }
}