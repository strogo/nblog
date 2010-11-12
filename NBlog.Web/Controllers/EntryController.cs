using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NBlog.Web.Application;
using NBlog.Web.Application.Service;
using NBlog.Web.Application.Service.Entity;

namespace NBlog.Web.Controllers
{
    public partial class EntryController : LayoutController
    {
        public EntryController(IServices services) : base(services) { }

        [HttpGet]
        public ActionResult Show(string slug)
        {
            return View();
        }


        [HttpGet]
        public ViewResult List()
        {
            var entries = Services.Entry.GetList();

            var model = new ListModel
            {
                Entries = entries.Select(e => new KeyTitleModel(e.Slug, e.Title))
            };

            return View(model);
        }

        [AdminOnly]
        [HttpGet]
        public ActionResult Edit([Bind(Prefix = "id")] string slug)
        {
            var isCreatingNew = string.IsNullOrWhiteSpace(slug);

            if (isCreatingNew)
            {
                return View();
            }
            else
            {
                var entry = Services.Entry.GetBySlug(slug);
                // todo: now map to the entry EditModel, maybe use AutoMapper
                return View();
            }

            return View();
        }

        [AdminOnly]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(EditModel model)
        {
            // todo: this could be a different InputModel
            // todo: input validation

            return View();
        }
    }
}