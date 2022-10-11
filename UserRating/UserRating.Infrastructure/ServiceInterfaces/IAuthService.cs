using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserRating.Infrastructure.ServiceInterfaces
{
    public interface IAuthService
    {
        public bool IsLogin(string login, string password, out int id);

        public bool IsRegistration(string login, string email);
    }
}