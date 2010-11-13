using System;
using System.Collections.Generic;
using System.Linq;
using NBlog.Web.Application.Service.Entity;
using NBlog.Web.Application.Storage.Json;
using NBlog.Web.Application.Storage;

namespace NBlog.Web.Application.Service
{
    public class EntryService : IEntryService
    {
        private readonly IUserService _userService;
        private readonly IRepository _repository;

        public EntryService(IUserService userService, IRepository repository)
        {
            _userService = userService;
            _repository = repository;
        }

        public void Save(Entry entry)
        {
            entry.DateCreated = DateTime.Now;
            entry.Author = _userService.Current.FriendlyName;
            _repository.Save(entry);
        }

        public Entry GetBySlug(string slug)
        {
            return _repository.Single<Entry,string>(slug);
        }

        public List<Entry> GetList()
        {
            return _repository.All<Entry>().ToList();
        }
    }
}