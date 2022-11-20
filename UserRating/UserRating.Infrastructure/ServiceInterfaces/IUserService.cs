using Microsoft.AspNetCore.Http;
using UserRating.Models;

namespace UserRating.Infrastructure.ServiceInterfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();

        User Get(int id);

        void Delete(int id);

        void Edit(User user);

        byte[] ConvertAvatarToByteArray(HttpRequest files);

        void RemoveAvatar(int id);

        void Create(User user);

        int GetLastId();
    }
}