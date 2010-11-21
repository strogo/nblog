using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NBlog.Web.Application;
using NBlog.Web.Application.Service;

namespace NBlog.Web.Controllers
{
    public partial class ContactController : LayoutController
    {
        public ContactController(IServices services) : base(services) { }

        [HttpGet]
        public ActionResult Index()
        {
            return View(new IndexModel());
        }

        [HttpPost]
        public ActionResult Index(IndexModel model)
        {
            if (!ModelState.IsValid)
                return View();

            // todo: send the message in an email

            return RedirectToAction("Confirm");
        }

        [HttpGet]
        public ActionResult Confirm()
        {
            return View();
        }
    }
}
