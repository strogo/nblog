using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            model.FriendlyUsername = User.Identity.Name;
            model.IsAuthenticated = User.Identity.IsAuthenticated;
        }
    }
}