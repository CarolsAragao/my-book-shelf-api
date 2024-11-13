using System.Data;
using System.Data.SqlClient;

namespace my_book_shelf_api.Core.Data
{
    public class ConnectionManager
    {
        private static string _connectionString = "DefaultConnection";
        private static SqlConnection _connection = null;

        public ConnectionManager(IConfiguration configurationManager)
        {
            if (_connectionString.Contains("DefaultConnection"))
            {
                _connectionString = configurationManager.GetConnectionString(_connectionString);
            }

            if(_connection is null || _connection.State == ConnectionState.Closed)
            {
                _connection = new SqlConnection(_connectionString); 
            }
        }

        public SqlConnection GetConnection()
        {          
            return _connection;
        } 
    }
}
