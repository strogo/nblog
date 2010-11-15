using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NBlog.Web.Application;

namespace NBlog.Web.Controllers
{
    public partial class HomeController
    {
        public class ListModel : LayoutModel
        {
            public IEnumerable<KeyTitleModel> Entries { get; set; }
        }

        public class KeyTitleModel
        {
            public KeyTitleModel(string key, string title)
            {
                Key = key;
                Title = title;
            }

            public string Key { get; private set; }
            public string Title { get; private set; }
        }
    }
}