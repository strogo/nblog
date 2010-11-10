using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using NBlog.Web.Application.Service.Entity;
using Newtonsoft.Json;

namespace NBlog.Web.Application.Storage.Json
{
    public class JsonRepository : IRepository
    {
        private readonly string _dataPath;
        private readonly Dictionary<Type, Func<object, string>> _keys = new Dictionary<Type, Func<object, string>>();

        public JsonRepository(string physicalFolderPath)
        {
            _dataPath = physicalFolderPath;

            // todo: make this an external configuration
            RegisterKey<Entry>(e => e.Slug);
            RegisterKey<Config>(e => e.Site);
            RegisterKey<User>(e => e.Username);
        }


        public TEntity Single<TEntity>(string key)
        {
            return Single<TEntity, string>(key);
        }

        public TEntity Single<TEntity, TKey>(TKey key)
        {
            var filename = key.ToString();
            var recordPath = Path.Combine(_dataPath, typeof(TEntity).Name, filename + ".json");
            var json = File.ReadAllText(recordPath);
            var item = JsonConvert.DeserializeObject<TEntity>(json);
            return item;            
        }


        public void Add<TEntity>(TEntity item) where TEntity : class, new()
        {
            var json = JsonConvert.SerializeObject(item, Formatting.Indented);
            var folderPath = Path.Combine(_dataPath, typeof(TEntity).Name);
            if (!Directory.Exists(folderPath)) { Directory.CreateDirectory(folderPath); }

            var filename = GetKeyValue(item);
            var recordPath = Path.Combine(folderPath, filename + ".json");

            File.WriteAllText(recordPath, json);
        }


        private void RegisterKey<T>(Func<T, string> key)
        {
            _keys.Add(typeof(T), f => key((T)f));
        }


        private string GetKeyValue<T>(T item)
        {
            return _keys[typeof(T)](item);
        }
    }
}
