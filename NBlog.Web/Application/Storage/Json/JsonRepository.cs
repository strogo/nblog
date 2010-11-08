using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Newtonsoft.Json;

namespace NBlog.Web.Application.Storage.Json
{
    public class JsonRepository
    {
        private readonly string _dataPath;

        public JsonRepository()
        {
            _dataPath = HttpContext.Current.Server.MapPath("~/App_Data/");
        }

        public void Add<T>(T item, string slug) where T : class, new()
        {
            var json = JsonConvert.SerializeObject(item, Formatting.Indented);
            var folderPath = Path.Combine(_dataPath, typeof(T).Name);
            if (!Directory.Exists(folderPath)) { Directory.CreateDirectory(folderPath); }

            var recordPath = Path.Combine(folderPath, slug + ".json");

            File.WriteAllText(recordPath, json);
        }

        public T Single<T>(string slug)
        {
            var recordPath = Path.Combine(_dataPath, typeof(T).Name, slug + ".json");
            var json = File.ReadAllText(recordPath);
            var item = JsonConvert.DeserializeObject<T>(json);
            return item;
        }
    }
}
