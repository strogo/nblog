using System;
using NBlog.Web.Application.Service.Entity;
using NBlog.Web.Application.Storage.Json;
using NBlog.Web.Application.Storage;

namespace NBlog.Web.Application.Service
{
    public class EntryService
    {
        private readonly UserService _userService;
        private readonly IRepository _repository;

        public EntryService(UserService userService, IRepository repository)
        {
            _userService = userService;
            _repository = repository;
        }

        public void Add(Entry entry)
        {
            entry.DateCreated = DateTime.Now;
            entry.Author = _userService.Current.FriendlyName;
            _repository.Add(entry);
        }

        public Entry GetBySlug(string slug)
        {
            return _repository.Single<Entry,string>(slug);
        }
    }
}