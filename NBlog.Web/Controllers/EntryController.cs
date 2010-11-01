using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NBlog.Web.Application;
using NBlog.Web.Application.Domain;

namespace NBlog.Web.Controllers
{
    public class EntryController : BaseController
    {
        public ActionResult Index()
        {
            var entry = new Entry { Title = "Test entry", Markdown = "Some markdown" };
            entry.Save();

            return View();
        }
    }
}
