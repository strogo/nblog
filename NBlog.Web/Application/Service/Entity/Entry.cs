using System;

namespace NBlog.Web.Application.Domain.Entity
{
    public class Entry
    {
        public Entry() : this(true)
        {
        }

        public Entry(bool isNew)
        {
            _isNew = isNew;
        }

        private readonly bool _isNew = true;
        private string _slug;
        private string _title;

        public string Slug { get { return _slug; } }

        public string Author { get; set; }
        public DateTime DateCreated { get; set; }
        public string Markdown { get; set; }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                if (_isNew) _slug = value.ToUrlSlug();
            } 
        }

        public override string ToString()
        {
            return Title;
        }
    }
}