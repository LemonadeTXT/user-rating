namespace UserRating.Infrastructure.ServiceInterfaces
{
    public interface IAuthService
    {
        public bool IsLogin(string login, string password, out int id);

        public bool IsRegistration(string login, string email);
    }
}