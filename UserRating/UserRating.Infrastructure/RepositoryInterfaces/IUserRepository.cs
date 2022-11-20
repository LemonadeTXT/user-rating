using UserRating.Models;

namespace UserRating.Infrastructure.RepositoryInterfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();

        User Get(int id);

        void Delete(int id);

        void Edit(User user);

        void RemoveAvatar(int id);

        void Create(User user);

        int GetLastId();
    }
}