using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        public ActionResult Show([Bind(Prefix = "id")] string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
                throw new ArgumentNullException("slug");

            Entry entry;
            try
            {
                entry = Services.Entry.GetBySlug(slug);
            }
            catch (Exception ex)
            {
                throw new HttpException(404, "Entry not found", ex);
            }

            var markdown = new MarkdownSharp.Markdown();
            var html = markdown.Transform(entry.Markdown);

            var model = new ShowModel
            {
                Date = entry.DateCreated.ToString("dddd, dd MMMM yyyy"),
                Slug = entry.Slug,
                Title = entry.Title,
                Html = html
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
                return View(new EditModel());
            }

            var entry = Services.Entry.GetBySlug(slug);

            var model = new EditModel
            {
                Title = entry.Title,
                Markdown = entry.Markdown,
                Slug = slug,
                NewSlug = slug
            };

            return View(model);
        }

        [AdminOnly]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(EditModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var isCreatingNew = string.IsNullOrWhiteSpace(model.Slug);
            Entry entry = null;

            if (isCreatingNew)
            {
                entry = new Entry
                {
                    Title = model.Title,
                    Markdown = model.Markdown,
                    Slug = model.Title.ToUrlSlug(),
                    Author = Services.User.Current.FriendlyName,
                    DateCreated = DateTime.Now
                };
            }
            else
            {
                entry = Services.Entry.GetBySlug(model.Slug);
                entry.Title = model.Title;
                entry.Markdown = model.Markdown;

                var slugChanged = !string.Equals(model.Slug, model.NewSlug, StringComparison.InvariantCultureIgnoreCase);
                if (slugChanged)
                {
                    Services.Entry.Delete(model.Slug);
                    entry.Slug = model.NewSlug;
                }
            }

            Services.Entry.Save(entry);

            return RedirectToAction("Show", "Entry", new { id = entry.Slug });
        }
    }
}