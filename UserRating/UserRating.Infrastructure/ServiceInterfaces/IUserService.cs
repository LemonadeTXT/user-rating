using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRating.Models;

namespace UserRating.Infrastructure.ServiceInterfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();

        User Get(int id);

        void Delete(int id);

        void Edit(User user);

        void Create(User user);
    }
}
