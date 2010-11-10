using System.Web.Mvc;
using NBlog.Web.Application;
using NBlog.Web.Application.Service;

namespace NBlog.Web.Controllers
{
    public partial class EntryController : LayoutController
    {
        public EntryController(Services services) : base(services) { }

        [HttpGet]
        public ActionResult Show(string slug)
        {
            return View();
        }

        [HttpGet]
        public ActionResult List()
        {
            return new EmptyResult();
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