using Lucky.Ecommerce.Transversal.Common;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Lucky.Ecommerce.Infrastructure.Data
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly string _connectionString;
        public ConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("NortwindConnection");
        }

        public IDbConnection GetConnection
        {
            get
            {
                var connection = new SqlConnection(_connectionString);
                connection.Open();
                return connection;
            }
        }
    }
}
