using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NBlog.Web.Application;

namespace NBlog.Web.Controllers
{
    public partial class EntryController
    {
        public class EditModel : LayoutModel
        {
            public string Slug { get; set; }
            public string NewSlug { get; set; }
            
            [Required]
            public string Title { get; set; }

            [SkipRequestValidation]
            public string Markdown { get; set; }
        }

        public class ShowModel : LayoutModel
        {
            public string Slug { get; set; }
            public string Title { get; set; }
            public string Html { get; set; }
        }
    }
}