using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UserRating.Infrastructure.RepositoryInterfaces;
using UserRating.Infrastructure.ServiceInterfaces;
using UserRating.Models;

namespace UserRating.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        private readonly ILogger _logger;

        public AuthService(IUserRepository userRepository, ILogger logger)
        {
            _userRepository = userRepository;
            _logger = logger;
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
                    _logger.Warning($"Trying to registry new account with old login or email: {user.Login}, {user.Email}!");

                    return true;
                }
            }

            return false;
        }
    }
}