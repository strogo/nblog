using System.ComponentModel;
using NBlog.Web.Controllers;

namespace NBlog.Web.Models
{
    public class Authentication
    {
        public class LoginModel : BaseViewModel
        {
            [DisplayName("OpenID")]
            public string OpenIdIdentifier { get; set; }
            public string ReturnUrl { get; set; }
            public string Message { get; set; }
        }
    }
}