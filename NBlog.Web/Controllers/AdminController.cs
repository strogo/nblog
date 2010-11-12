using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NBlog.Web.Application;
using NBlog.Web.Application.Service;

namespace NBlog.Web.Controllers
{
    public class AdminController : LayoutController
    {
        public AdminController(IServices services) : base(services) {}

        [HttpGet]
        [AdminOnly]
        public ActionResult Index()
        {
            return View();
        }

    }
}
