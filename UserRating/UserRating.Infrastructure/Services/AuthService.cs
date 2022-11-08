using System.Security.Cryptography;
using System.Text;
using UserRating.Infrastructure.RepositoryInterfaces;
using UserRating.Infrastructure.ServiceInterfaces;
using UserRating.Models;

namespace UserRating.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool IsLogin(string login, string password, out int id)
        {
            id = -1;

            foreach (User user in _userRepository.GetAll())
            {
                if (user.Login == login &&
                    user.Password == Convert.ToBase64String(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(password))))
                {
                    id = user.Id;

                    return true;
                }
            }

            return false;
        }

        public bool IsRegistration(string login, string email)
        {
            foreach (User user in _userRepository.GetAll())
            {
                if (user.Email == email || user.Login == login)
                {
                    return true;
                }
            }

            return false;
        }
    }
}