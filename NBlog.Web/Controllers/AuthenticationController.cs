using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.RelyingParty;
using NBlog.Web.Application;

namespace NBlog.Web.Controllers
{
    public partial class AuthenticationController : Controller
    {
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            var model = new LoginModel { ReturnUrl = returnUrl.AsNullIfEmpty() ?? Url.Action("Index", "Home") };
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult OpenId(string identifier, string returnUrl)
        {
            var openId = new OpenIdRelyingParty();

            // if authentication fails and we need to go back to the login page then maintain the original returnUrl
            var loginModel = new LoginModel { ReturnUrl = returnUrl };

            var openIdResponse = openId.GetResponse();
            if (openIdResponse != null)
            {
                // receive OpenID provider's response assertion
                switch (openIdResponse.Status)
                {
                    case AuthenticationStatus.Authenticated:
                        // set default friendly name to OpenID friendly identifier
                        var friendlyName = openIdResponse.FriendlyIdentifierForDisplay;

                        // if sreg supported, use real name or email as friendly name, whichever is available
                        var sregResponse = openIdResponse.GetExtension<ClaimsResponse>();
                        if (sregResponse != null)
                            friendlyName = sregResponse.FullName ?? sregResponse.Email;

                        FormsAuthentication.SetAuthCookie(openIdResponse.ClaimedIdentifier, false);
                        return Redirect(returnUrl.AsNullIfEmpty() ?? Url.Action("Index", "Home"));

                    case AuthenticationStatus.Canceled:
                        loginModel.Message = "Canceled at provider";
                        return View("Login", loginModel);

                    case AuthenticationStatus.Failed:
                        loginModel.Message = openIdResponse.Exception.Message;
                        return View("Login", loginModel);
                }
            }
            else
            {
                // submit user's OpenID identifier
                Identifier id;
                if (Identifier.TryParse(identifier, out id))
                {
                    try
                    {
                        // include request for name and email using sreg (OpenID Simple Registration Extension)
                        var request = openId.CreateRequest(id);
                        request.AddExtension(new ClaimsRequest { Email = DemandLevel.Request, FullName = DemandLevel.Request });
                        return request.RedirectingResponse.AsActionResult();
                    }
                    catch (ProtocolException ex)
                    {
                        loginModel.Message = ex.Message;
                        return View("Login", loginModel);
                    }
                }
                else
                {
                    loginModel.Message = "Invalid identifier";
                    return View("Login", loginModel);
                }
            }
            return null;
        }
    }
}
