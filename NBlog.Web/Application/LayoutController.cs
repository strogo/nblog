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
        protected readonly IServices Services;
        
        public LayoutController(IServices services)
        {
            Services = services;
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            var viewResult = filterContext.Result as ViewResultBase;
            if (viewResult == null) return;

            if (viewResult.ViewData.Model == null)
                viewResult.ViewData.Model = new LayoutModel();

            if (!(viewResult.ViewData.Model is LayoutModel))
                throw new InvalidCastException("View model must derive from LayoutModel in action " + filterContext.ActionDescriptor.ActionName);

            var model = (LayoutModel)viewResult.ViewData.Model;
            InitialiseBaseViewModel(model);
        }

        private void InitialiseBaseViewModel(LayoutModel model)
        {
            var currentUser = Services.User.Current;

            model.FriendlyUsername = currentUser.FriendlyName;
            model.IsAuthenticated = currentUser.IsAuthenticated;
            model.IsAdmin = currentUser.IsAdmin;
        }
    }
}