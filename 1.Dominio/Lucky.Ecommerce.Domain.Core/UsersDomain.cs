using Lucky.Ecommerce.Domain.Entity;
using Lucky.Ecommerce.Domain.Interface;
using Lucky.Ecommerce.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucky.Ecommerce.Domain.Core
{
    public class UsersDomain : IUsersDomain
    {
        private readonly IUsersRepository _usersRepository;
        public UsersDomain(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        public Users Authenticate(string userName, string password)
        {
            return _usersRepository.Authenticate(userName, password);
        }
    }
}
