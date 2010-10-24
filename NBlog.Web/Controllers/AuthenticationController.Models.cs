using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NBlog.Web.Controllers
{
    public partial class AuthenticationController
    {
        public class LoginModel
        {
            public string ReturnUrl { get; set; }
            public string Message { get; set; }
        }
    }
}