using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using NBlog.Web.Application;

namespace NBlog.Web.Application.Domain
{
    public class Entry
    {
        private bool _isNew = true;
        private string _slug;
        private string _title;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                if (_isNew) _slug = value.ToUrlSlug();
            } 
        }

        public DateTime DateCreated { get; set; }
        public string Markdown { get; set; }

        public Entry() { }

        public Entry(string slug)
        {
            throw new NotImplementedException();
        }

        public virtual void Save()
        {

            // todo: set global JSON Formatting.Indented?
            var json = JsonConvert.SerializeObject(this, Formatting.Indented);

            var physicalFolder = HttpContext.Current.Server.MapPath("~/App_Data/Entries");
            if (!Directory.Exists(physicalFolder)) { Directory.CreateDirectory(physicalFolder); }
            var entryPhysicalPath = Path.Combine(physicalFolder, _slug + ".json");
            File.WriteAllText(entryPhysicalPath, json);
        }
    }
}