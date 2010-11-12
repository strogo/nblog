using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using NBlog.Web.Application.Service.Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NBlog.Web.Application.Storage.Json
{
    public class JsonRepository : IRepository
    {
        private readonly string _dataPath;
        private readonly Dictionary<Type, Func<object, string>> _keys = new Dictionary<Type, Func<object, string>>();

        public JsonRepository(string dataPath)
        {
            _dataPath = dataPath;

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


        public IQueryable<TEntity> All<TEntity>()
        {
            var folderPath = Path.Combine(_dataPath, typeof(TEntity).Name);
            var filePaths = Directory.GetFiles(folderPath, "*.json", SearchOption.TopDirectoryOnly);

            var list = new List<TEntity>();
            foreach (var path in filePaths)
            {
                var jsonString = File.ReadAllText(path);
                var entity = JsonConvert.DeserializeObject<TEntity>(jsonString);
                list.Add(entity);
            }

            return list.AsQueryable();
        }


        public void Add<TEntity>(TEntity item)
        {
            var json = JsonConvert.SerializeObject(item, Formatting.Indented);
            var folderPath = Path.Combine(_dataPath, typeof(TEntity).Name);
            if (!Directory.Exists(folderPath)) { Directory.CreateDirectory(folderPath); }

            var filename = GetKeyValue(item);
            var recordPath = Path.Combine(folderPath, filename + ".json");

            File.WriteAllText(recordPath, json);
        }

        public void DeleteAll<TEntity>()
        {
            var folderPath = GetEntityPath<TEntity>();
            if (Directory.Exists(folderPath))
            {
                Directory.Delete(folderPath, true);
            }
        }

        private void RegisterKey<T>(Func<T, string> key)
        {
            _keys.Add(typeof(T), f => key((T)f));
        }


        private string GetKeyValue<T>(T item)
        {
            return _keys[typeof(T)](item);
        }

        private string GetEntityPath<TEntity>()
        {
            return Path.Combine(_dataPath, typeof(TEntity).Name);
        }
    }
}
