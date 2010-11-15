using System;

namespace NBlog.Web.Application.Service.Entity
{
    public class Entry
    {
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime DateCreated { get; set; }
        public string Markdown { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}