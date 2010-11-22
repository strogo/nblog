namespace NBlog.Web.Application
{
    public class LayoutModel
    {
        [NoBinding]
        public LayoutBaseModel Base { get; set; }

        public class LayoutBaseModel
        {
            public bool IsAuthenticated { get; set; }
            public bool IsAdmin { get; set; }
            public string FriendlyUsername { get; set; }
            public string Theme { get; set; }
            public string SiteTitle { get; set; }
            public string SiteMetaDescription { get; set; }
            public string SiteHeading { get; set; }
            public string SiteTagline { get; set; }
            public string Crossbar { get; set; }
            public string GoogleAnalyticsId { get; set; }
            public string TwitterUsername { get; set; }
        }
    }
}