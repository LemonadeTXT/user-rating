using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRating.Data;
using UserRating.Infrastructure.Connection;
using UserRating.Infrastructure.RepositoryInterfaces;
using UserRating.Models;

namespace UserRating.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ConnectionSettings _connectionSettings { get; set; }

        public UserRepository(ConnectionSettings connectionSettings)
        {
            _connectionSettings = connectionSettings;
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Edit(User user)
        {
            throw new NotImplementedException();
        }

        public void Create(User user)
        {
            throw new NotImplementedException();
        }
    }
}