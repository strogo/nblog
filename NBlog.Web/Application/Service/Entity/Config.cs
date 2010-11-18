using System.Collections.Generic;
using Newtonsoft.Json;

namespace NBlog.Web.Application.Service.Entity
{
    public class Config
    {
        public string Site { get; set; }
        public string Theme { get; set; }
        public string Title { get; set; }
        public string Tagline { get; set; }
        public string Crossbar { get; set; }
        public List<string> Admins { get; set; }
    }
}