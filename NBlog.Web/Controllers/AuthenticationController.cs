using System;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.RelyingParty;
using NBlog.Web.Application;
using NBlog.Web.Models;

namespace NBlog.Web.Controllers
{
    public partial class AuthenticationController : BaseController
    {
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            var model = new Authentication.LoginModel { ReturnUrl = returnUrl.AsNullIfEmpty() ?? Url.Action("Index", "Home") };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OpenId(Authentication.LoginModel model)
        {
            Identifier id;
            if (Identifier.TryParse(model.OpenID_Identifier, out id))
            {
                try
                {
                    var openId = new OpenIdRelyingParty();
                    var returnToUrl = new Uri(Url.Action("OpenIdCallback", "Authentication", new { ReturnUrl = model.ReturnUrl }, Request.Url.Scheme), UriKind.Absolute);
                    var request = openId.CreateRequest(id, Realm.AutoDetect, returnToUrl);

                    // add request for name and email using sreg (OpenID Simple Registration Extension)
                    request.AddExtension(new ClaimsRequest
                    {
                        Email = DemandLevel.Request, FullName = DemandLevel.Request, Nickname = DemandLevel.Require
                    });

                    // also add AX request
                    var axRequest = new FetchRequest();
                    axRequest.Attributes.AddRequired(WellKnownAttributes.Name.FullName);
                    axRequest.Attributes.AddRequired(WellKnownAttributes.Name.First);
                    axRequest.Attributes.AddRequired(WellKnownAttributes.Name.Last);
                    request.AddExtension(axRequest);

                    return request.RedirectingResponse.AsActionResult();
                }
                catch (ProtocolException ex)
                {
                    model.Message = ex.Message;
                    return View("Login", model);
                }
            }
            else
            {
                model.Message = "Invalid identifier";
                return View("Login", model);
            }
        }


        [HttpGet]
        [ValidateInput(false)]
        public ActionResult OpenIdCallback(string returnUrl)
        {
            var model = new Authentication.LoginModel { ReturnUrl = returnUrl };
            var openId = new OpenIdRelyingParty();
            var openIdResponse = openId.GetResponse();

            if (openIdResponse.Status == AuthenticationStatus.Authenticated)
            {
                // todo: don't think we should ever use this for friendly!
                var friendlyName = openIdResponse.FriendlyIdentifierForDisplay;

                // if sreg supported, use real name or email as friendly name, whichever is available
                var sregResponse = openIdResponse.GetExtension<ClaimsResponse>();
                var axResponse = openIdResponse.GetExtension<FetchResponse>();

                // todo: build the friendlyname

                //if (sregResponse != null)
                //{
                //    friendlyName = sregResponse.FullName ?? sregResponse.Email;
                //}

                FormsAuthentication.SetAuthCookie(openIdResponse.ClaimedIdentifier, false);
                return Redirect(returnUrl.AsNullIfEmpty() ?? Url.Action("Index", "Home"));
            }

            model.Message = "Sorry, login failed.";
            return View("Login", model);
        }
    }
}
