using Lucky.Ecommerce.Transversal.Common;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;
using System.Data.SqlClient;

namespace Lucky.Ecommerce.Infrastructure.Data
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly string _connectionString;
        public ConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("LuckyEcommerceConnection");
        }

        public IDbConnection GetConnection
        {
            get
            {
                var connection = new NpgsqlConnection(_connectionString);
                connection.Open();
                return connection;
            }
        }
    }
}
