using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac.Integration.Web;
using NBlog.Web.Application;
using NBlog.Web.Application.Service;
using Newtonsoft.Json;

namespace NBlog.Web.Controllers
{
    public class HomeController : LayoutController
    {
        public HomeController(IServices services) : base(services) { }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}
