using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NBlog.Web.Application;
using NBlog.Web.Application.Service;

namespace NBlog.Web.Controllers
{
    public partial class HomeController : LayoutController
    {
        public HomeController(IServices services) : base(services) { }

        [HttpGet]
        public ViewResult Index()
        {
            var entries = Services.Entry.GetList();

            var model = new IndexModel
            {
                Entries = entries
                    .OrderByDescending(e => e.DateCreated)
                    .Select(e => new EntrySummaryModel
                    {
                        Key = e.Slug,
                        Title = e.Title,
                        Date = e.DateCreated.ToString("dddd, dd MMMM yyyy"),
                        PrettyDate = e.DateCreated.ToPrettyDate()
                    })
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
