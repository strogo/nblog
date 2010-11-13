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


        // [AdminOnly]
        [HttpGet]
        public ActionResult Edit([Bind(Prefix = "id")] string slug)
        {
            var isCreatingNew = string.IsNullOrWhiteSpace(slug);

            if (isCreatingNew)
            {
                return View(new EditModel());
            }

            var entry = Services.Entry.GetBySlug(slug);
            var model = new EditModel { Title = entry.Title, Markdown = entry.Markdown };
            return View(model);
        }

        // [AdminOnly]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(EditModel model)
        {
            // todo: validation, try new MVC3 unobtrusive? client side too

            var isCreatingNew = string.IsNullOrWhiteSpace(model.Slug);

            if (isCreatingNew)
            {
                var entry = new Entry { Title = model.Title, Markdown = model.Markdown };
                Services.Entry.Save(entry);
            }
            else
            {
                var entry = Services.Entry.GetBySlug(model.Slug);
                entry.Markdown = model.Markdown;
                entry.Title = model.Title;
                Services.Entry.Save(entry);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}