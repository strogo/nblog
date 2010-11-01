using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace NBlog.Web.Application
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            var viewResult = filterContext.Result as ViewResultBase;
            if (viewResult == null) return;

            if (viewResult.ViewData.Model == null)
                viewResult.ViewData.Model = new BaseViewModel();
            
            var model = (BaseViewModel)viewResult.ViewData.Model;
            InitialiseBaseViewModel(model);
        }

        private void InitialiseBaseViewModel(BaseViewModel model)
        {
            var formsIdentity = User.Identity as FormsIdentity;
            var friendlyName = formsIdentity != null ? formsIdentity.Ticket.UserData : User.Identity.Name;
            if (string.IsNullOrEmpty(friendlyName)) { friendlyName = User.Identity.Name; }

            model.FriendlyUsername = friendlyName;
            model.IsAuthenticated = User.Identity.IsAuthenticated;
        }
    }
}