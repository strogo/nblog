using System.Web.Mvc;
using System.Web;
namespace NBlog.Web.Application
{
    public class LayoutModel
    {
        public bool IsAuthenticated { get; set; }
        public bool IsAdmin { get; set; }
        public string FriendlyUsername { get; set; }
        public string SiteTitle { get; set; }
        public string SiteTagline { get; set; }
    }
}