using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac.Integration.Web;
using NBlog.Web.Application;
using Newtonsoft.Json;

namespace NBlog.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(Settings settings)
        {
            
        }

        [AdminOnly]
        public ActionResult Index()
        {
            return View();
        }
    }
}
