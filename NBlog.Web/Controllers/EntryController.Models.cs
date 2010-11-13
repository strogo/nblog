﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NBlog.Web.Application;

namespace NBlog.Web.Controllers
{
    public partial class EntryController
    {
        public class EditModel : LayoutModel
        {
            public string Slug { get; set; }
            public string Title { get; set; }
            public string Markdown { get; set; }
        }
    }
}