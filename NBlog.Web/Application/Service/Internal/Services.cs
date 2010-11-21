namespace NBlog.Web.Application.Service.Internal
{
    public class Services : IServices
    {
        public Services(IEntryService entryService, IUserService userService, IConfigService configService, IMessageService messageService)
        {
            Entry = entryService;
            User = userService;
            Config = configService;
            Message = messageService;
        }
        
        public IEntryService Entry { get; private set; }
        public IUserService User { get; private set; }
        public IConfigService Config { get; private set; }
        public IMessageService Message { get; private set; }
    }
}