using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucky.Ecommerce.Domain.Entity
{
    public class Users
    {
        public int UserId { get; set; }
        public string FirsName { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }

    }
}
