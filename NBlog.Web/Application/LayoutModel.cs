using System.Web.Mvc;
using System.Web;
namespace NBlog.Web.Application
{
    public class LayoutModel
    {
        public LayoutBaseModel Base { get; private set; }

        public LayoutModel()
        {
            Base = new LayoutBaseModel();
        }

        public class LayoutBaseModel
        {
            public bool IsAuthenticated { get; set; }
            public bool IsAdmin { get; set; }
            public string FriendlyUsername { get; set; }
            public string Theme { get; set; }
            public string SiteTitle { get; set; }
            public string SiteTagline { get; set; }
            public string Crossbar { get; set; }
            public string GoogleAnalyticsId { get; set; }
        }
    }
}