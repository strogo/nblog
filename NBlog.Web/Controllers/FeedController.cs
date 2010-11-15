using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web;
using System.Web.Mvc;
using NBlog.Web.Application;
using NBlog.Web.Application.Service;

namespace NBlog.Web.Controllers
{
    public class FeedController : Controller
    {
        private readonly IServices _services;

        public FeedController(IServices services)
        {
            _services = services;
        }

        public ActionResult Index()
        {
            // todo: build the action link to entry/show
            var url = Request.Url;

            var markdown = new MarkdownSharp.Markdown();
            var baseUri = new Uri(url.GetLeftPart(UriPartial.Authority));
            
            var entries =
                _services.Entry.GetList()
                .Select(e => new SyndicationItem(
                    e.Title,
                    markdown.Transform(e.Markdown),
                    new Uri(baseUri, Url.Action("Show", "Entry", new { id = e.Slug }, null))));

            // todo: get title & description from ConfigService
            var feed = new SyndicationFeed(
                title: "Chris Fulstow",
                description: "ASP.NET Tech Lead and Web Developer",
                feedAlternateLink: new Uri(baseUri, Url.Action("Index", "Feed")),
                items: entries);

            return new RssResult(feed);
        }
    }
}
