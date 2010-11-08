using System.Web.Mvc;
using System.Web;
namespace NBlog.Web.Application
{
    public class LayoutViewModel
    {
        public bool IsAuthenticated { get; set; }
        public string FriendlyUsername { get; set; }
    }
}