using System;
using System.Linq;
using System.Web;
using System.Web.Security;
using NBlog.Web.Application.Domain.Entity;

namespace NBlog.Web.Application.Service
{
    public class UserService
    {
        private readonly ConfigService _configService;

        public UserService(ConfigService configService)
        {
            _configService = configService;
            var identity = HttpContext.Current.User.Identity;
            var formsIdentity = identity as FormsIdentity;
            var friendlyName = formsIdentity != null ? formsIdentity.Ticket.UserData : identity.Name;
            if (string.IsNullOrEmpty(friendlyName)) { friendlyName = identity.Name; }

            var isAdmin =
                identity.IsAuthenticated
                && _configService.Admins != null
                && _configService.Admins.Contains(identity.Name, StringComparer.InvariantCultureIgnoreCase);

            var user = new User
            {
                FriendlyName = friendlyName, 
                IsAuthenticated = identity.IsAuthenticated,
                IsAdmin = isAdmin
            };

            Current = user;
        }

        public User Current { get; private set; }
    }
}