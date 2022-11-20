using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Text;
using UserRating.Infrastructure.RepositoryInterfaces;
using UserRating.Infrastructure.ServiceInterfaces;
using UserRating.Models;

namespace UserRating.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User Get(int id)
        {
            return _userRepository.Get(id);
        }

        public void Delete(int id)
        {
            _userRepository.Delete(id);
        }

        public void Edit(User user)
        {
            _userRepository.Edit(user);
        }

        public byte[] ConvertAvatarToByteArray(HttpRequest files)
        {
            return _userRepository.ConvertAvatarToByteArray(files);
        }

        public void RemoveAvatar(int id)
        {
            _userRepository.RemoveAvatar(id);
        }

        public void Create(User user)
        {
            _userRepository.Create(user);
        }

        public int GetLastId()
        {
            return _userRepository.GetLastId();
        }
    }
}