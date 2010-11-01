using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NBlog.Web.Application;

namespace NBlog.Web.Controllers
{
    public class AdminController : BaseController
    {
        [HttpGet]
        [AdminOnly]
        public ActionResult Index()
        {
            return View();
        }

    }
}
