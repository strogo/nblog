namespace NBlog.Web.Application.Domain.Entity
{
    public class User
    {
        public string FriendlyName { get; set; }
        public bool IsAuthenticated { get; set; }
        public bool IsAdmin { get; set; }
    }
}