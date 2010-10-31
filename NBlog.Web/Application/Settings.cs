using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.IO;

namespace NBlog.Web.Application
{
    public class Settings
    {
        const string SettingsVirtualPath = "~/App_Data/Settings.json";

        public Settings()
        {
            var physicalPath = HttpContext.Current.Server.MapPath(SettingsVirtualPath);
            var json = File.ReadAllText(physicalPath);
            JsonConvert.PopulateObject(json, this);
        }

        [JsonProperty]
        public List<string> Admins { get; private set; }
    }
}