using System.Collections.Generic;
using System.Web;
using NBlog.Web.Application.Service.Entity;
using NBlog.Web.Application.Storage.Json;
using Newtonsoft.Json;

namespace NBlog.Web.Application.Service
{
    public class ConfigService
    {
        private readonly JsonRepository _repository;

        public ConfigService(JsonRepository repository)
        {
            _repository = repository;
            Current = _repository.Single<Config>("site");
        }

        public Config Current { get; private set; }

        [JsonProperty]
        public List<string> Admins { get; private set; }
    }
}