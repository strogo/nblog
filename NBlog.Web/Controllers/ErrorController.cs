using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NBlog.Web.Application;
using NBlog.Web.Application.Service;

namespace NBlog.Web.Controllers
{
    public partial class ErrorController : LayoutController
    {
        public ErrorController(IServices services) : base(services) { }

        public ActionResult Index()
        {
            var model = new ErrorModel();
            var httpException = RouteData.Values["httpException"] as HttpException;

            var isNotFound = httpException != null && httpException.GetHttpCode() == 404;
            if (isNotFound)
            {
                Response.StatusCode = 404;
                model.Heading = "Page not found";
                model.Message = "We couldn't find the page you requested.";
            }
            else
            {
                Response.StatusCode = 500;
                model.Heading = "Boom!";
                model.Message = "Sorry, something went wrong.  It's been logged.";
            }

            return View(model);
        }
    }
}
