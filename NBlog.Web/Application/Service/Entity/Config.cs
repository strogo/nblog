using System.Collections.Generic;
using Newtonsoft.Json;

namespace NBlog.Web.Application.Service.Entity
{
    public class Config
    {
        public string Site { get; set; }

        [JsonProperty]
        public List<string> Admins { get; private set; }
    }
}