using System;
using NBlog.Web.Application.Domain.Entity;
using NBlog.Web.Application.Storage.Json;

namespace NBlog.Web.Application.Service
{
    public class EntryService
    {
        private readonly UserService _userService;
        private readonly JsonRepository _repository;

        public EntryService(UserService userService, JsonRepository repository)
        {
            _userService = userService;
            _repository = repository;
        }

        public void Add(Entry entry)
        {
            entry.DateCreated = DateTime.Now;
            entry.Author = _userService.Current.FriendlyName;
            _repository.Add(entry, entry.Slug);
        }
    }
}