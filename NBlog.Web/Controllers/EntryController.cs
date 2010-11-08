using System.Web.Mvc;
using NBlog.Web.Application;
using NBlog.Web.Application.Domain;
using NBlog.Web.Application.Domain.Entity;
using NBlog.Web.Application.Service;

namespace NBlog.Web.Controllers
{
    public class EntryController : LayoutController
    {
        public EntryController(Services services) : base(services) { }

        public ActionResult Show(string slug)
        {
            return View();
        }

        public ActionResult Add()
        {
            var entry = new Entry
            {
                Title = "Chris Testing! 123", Markdown = "Some markdown"
            };

            Services.Entry.Add(entry);

            return View();
        }

        public ActionResult List()
        {
            return new EmptyResult();
        }
    }
}