using System.ComponentModel;
using System.Web.Mvc;
using NBlog.Web.Application;
using NBlog.Web.Controllers;

namespace NBlog.Web.Models
{
    public class Authentication
    {
        public class LoginModel : LayoutViewModel
        {
            public string OpenID_Identifier { get; set; }
            public string ReturnUrl { get; set; }
            public string Message { get; set; }
        }
    }
}