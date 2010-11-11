using System;

namespace NBlog.Web.Application
{
    public class SummaryModel<T>
    {
        public SummaryModel(T item, Func<T,string> keySelector, Func<T,string> titleSelector)
        {
            Key = keySelector(item);
            Title = titleSelector(item);
        }

        public string Key { get; private set; }
        public string Title { get; private set; }
    }
}