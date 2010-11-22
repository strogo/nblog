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
        public ActionResult Add()
        {
            return View("Edit", new EditModel());
        }


        [AdminOnly]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Add(EditModel model)
        {
            var slug = model.Title.ToUrlSlug();

            // (these fields are hidden when creating a new entry, so don't validate them)
            ModelState["NewSlug"].Errors.Clear();
            ModelState["Date"].Errors.Clear();

            if (Services.Entry.Exists(slug))
                ModelState.AddModelError("Title", "Sorry, a post already exists with the slug '" + slug + "', please change the title.");

            if (!ModelState.IsValid)
                return View("Edit", model);

            var entry = new Entry
            {
                Title = model.Title,
                Markdown = model.Markdown,
                Slug = slug,
                Author = Services.User.Current.FriendlyName,
                DateCreated = DateTime.Now
            };

            Services.Entry.Save(entry);

            return RedirectToAction("Show", "Entry", new { id = entry.Slug });
        }


        [AdminOnly]
        [HttpGet]
        public ActionResult Edit([Bind(Prefix = "id")] string slug)
        {
            var entry = Services.Entry.GetBySlug(slug);

            var model = new EditModel
            {
                Title = entry.Title,
                Date = entry.DateCreated.ToString("dd MMM yyyy"),
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
                return View(model);

            var entry = Services.Entry.GetBySlug(model.Slug);
            entry.Title = model.Title;
            entry.DateCreated = DateTime.Parse(model.Date);
            entry.Markdown = model.Markdown;
            
            var slugChanged =
                !string.Equals(model.Slug, model.NewSlug, StringComparison.InvariantCultureIgnoreCase)
                && !string.IsNullOrWhiteSpace(model.NewSlug);

            if (slugChanged)
            {
                if (Services.Entry.Exists(model.NewSlug))
                {
                    ModelState.AddModelError("NewSlug", "Sorry, a post with that slug already exists.");
                    return View(model);
                }
                Services.Entry.Delete(model.Slug);
                entry.Slug = model.NewSlug;
            }

            Services.Entry.Save(entry);

            return RedirectToAction("Show", "Entry", new { id = entry.Slug });
        }
    }
}