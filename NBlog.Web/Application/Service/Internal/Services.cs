namespace NBlog.Web.Application.Service
{
    public class Services : IServices
    {
        public Services(IEntryService entryService, IUserService userService, IConfigService configService)
        {
            Entry = entryService;
            User = userService;
            Config = configService;
        }
        
        public IEntryService Entry { get; private set; }
        public IUserService User { get; private set; }
        public IConfigService Config { get; private set; }
    }
}