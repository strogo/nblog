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
    public partial class HomeController : LayoutController
    {
        public HomeController(IServices services) : base(services) { }

        [HttpGet]
        public ViewResult Index()
        {
            var entries = Services.Entry.GetList();

            var model = new ListModel
            {
                Entries = entries.Select(e => new KeyTitleModel(e.Slug, e.Title))
            };

            return View(model);
        }

        [HttpGet]
        public ViewResult Throw()
        {
            throw new NotImplementedException("Example exception for testing error handling.");
        }
    }
}
