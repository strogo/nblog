﻿using NBlog.Web.Application.Service.Entity;
using NBlog.Web.Application.Storage;

namespace NBlog.Web.Application.Service.Internal
{
    public class ConfigService : IConfigService
    {
        private readonly IRepository _repository;

        public ConfigService(IRepository repository)
        {
            _repository = repository;
            Current = _repository.Single<Config, string>("site");
        }

        public Config Current { get; private set; }
    }
}