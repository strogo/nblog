namespace NBlog.Web.Controllers
{
    public class KeyTitleModel
    {
        public KeyTitleModel(string key, string title)
        {
            Key = key;
            Title = title;
        }

        public string Key { get; private set; }
        public string Title { get; private set; }
    }
}