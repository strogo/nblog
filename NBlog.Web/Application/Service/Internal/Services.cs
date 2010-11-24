namespace NBlog.Web.Application.Service.Internal
{
    public class Services : IServices
    {
        public Services(
            IEntryService entryService,
            IUserService userService,
            IConfigService configService,
            IMessageService messageService,
            ICloudService cloudService)
        {
            Entry = entryService;
            User = userService;
            Config = configService;
            Message = messageService;
            Cloud = cloudService;
        }
        
        public IEntryService Entry { get; private set; }
        public IUserService User { get; private set; }
        public IConfigService Config { get; private set; }
        public IMessageService Message { get; private set; }
        public ICloudService Cloud { get; private set; }
    }
}