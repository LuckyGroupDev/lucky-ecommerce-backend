using Dapper;
using Lucky.Ecommerce.Domain.Entity;
using Lucky.Ecommerce.Infrastructure.Interface;
using Lucky.Ecommerce.Transversal.Common;
using System.Data;

namespace Lucky.Ecommerce.Infrastructure.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public UsersRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public Users Authenticate(string userName, string password)
        {
            using var connection = _connectionFactory.GetConnection;

            // Forma correcta de llamar funciones en PostgreSQL
            var sql = "SELECT * FROM public.usersgetbyuserandpassword(@p_username, @p_password)";

            var user = connection.QueryFirstOrDefault<Users>(sql, new
            {
                p_username = userName,
                p_password = password
            });

            return user;
        }

    }

}
