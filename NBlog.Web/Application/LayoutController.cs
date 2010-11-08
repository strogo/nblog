using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using NBlog.Web.Application.Service;

namespace NBlog.Web.Application
{
    public class LayoutController : Controller
    {
        protected readonly Services Services;
        
        public LayoutController(Services services)
        {
            Services = services;
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            var viewResult = filterContext.Result as ViewResultBase;
            if (viewResult == null) return;

            if (viewResult.ViewData.Model == null)
                viewResult.ViewData.Model = new LayoutViewModel();
            
            var model = (LayoutViewModel)viewResult.ViewData.Model;
            InitialiseBaseViewModel(model);
        }

        private void InitialiseBaseViewModel(LayoutViewModel model)
        {
            var formsIdentity = User.Identity as FormsIdentity;
            var friendlyName = formsIdentity != null ? formsIdentity.Ticket.UserData : User.Identity.Name;
            if (string.IsNullOrEmpty(friendlyName)) { friendlyName = User.Identity.Name; }

            // todo: use UserService
            // _services.User.Current.FriendlyName;

            model.FriendlyUsername = friendlyName;
            model.IsAuthenticated = User.Identity.IsAuthenticated;
        }
    }
}