using System.Collections.Generic;
using Newtonsoft.Json;

namespace NBlog.Web.Application.Service.Entity
{
    public class Config
    {
        public string Site { get; set; }
        public string Theme { get; set; }
        public string Title { get; set; }
        public string MetaDescription { get; set; }
        public string Heading { get; set; }
        public string Tagline { get; set; }
        public string Crossbar { get; set; }
        public List<string> Admins { get; set; }
        public string GoogleAnalyticsId { get; set; }
        public string TwitterUsername { get; set; }
        public ContactFormConfig ContactForm { get; set; }
        
        public class ContactFormConfig
        {
            public string RecipientName { get; set; }
            public string RecipientEmail { get; set; }
            public string Subject { get; set; }
        }
    }
}