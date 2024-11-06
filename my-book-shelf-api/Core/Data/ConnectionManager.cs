using System.Data;
using System.Data.SqlClient;

namespace my_book_shelf_api.Core.Data
{
    public class ConnectionManager
    {
        private static string _connectionString = "DefaultConnection";
        private static SqlConnection _connection;

        public ConnectionManager(IConfiguration configurationManager)
        {
            _connectionString = configurationManager.GetConnectionString(_connectionString);

            if(_connection is null)
            {
                _connection = new SqlConnection(_connectionString); 
            }
        }

        public SqlConnection GetConnection()
        {
            if (_connection == null || _connection.State == ConnectionState.Closed)
            {
                _connection = new SqlConnection(_connectionString);
                _connection.Open();
            }
            return _connection;
        }

        public void CloseConnection()
        {
            if (_connection != null && _connection.State != ConnectionState.Closed)
            {
                _connection.Close();
            }
        }

    }
}
