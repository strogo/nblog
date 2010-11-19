﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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

            [DisplayName("Slug")]
            [Required(ErrorMessage = "Please supply a slug for this post")]
            [RegularExpression("^[a-zA-Z-]+$", ErrorMessage = "That's not a valid slug. Only letters, numbers and hypens are allowed.")]
            public string NewSlug { get; set; }
            
            [Required(ErrorMessage = "Please enter the title of this post.")]
            public string Title { get; set; }

            [SkipRequestValidation]
            [Required(ErrorMessage = "Please enter some content.")]
            public string Markdown { get; set; }
        }

        public class ShowModel : LayoutModel
        {
            public string Slug { get; set; }
            public string Date { get; set; }
            public string Title { get; set; }
            public string Html { get; set; }
        }
    }
}