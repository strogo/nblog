namespace NBlog.Web.Application.Service
{
    public class Services
    {
        public Services(EntryService entryService, UserService userService, ConfigService configService)
        {
            Entry = entryService;
            User = userService;
            Config = configService;
        }
        
        public EntryService Entry { get; private set; }
        public UserService User { get; private set; }
        public ConfigService Config { get; private set; }
    }
}