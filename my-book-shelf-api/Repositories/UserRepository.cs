using Dapper;
using my_book_shelf_api.Core.Data;
using my_book_shelf_api.Models;
using System.Data;
using System.Data.SqlClient;

namespace my_book_shelf_api.Repositories
{
    public class UserRepository
    {
        private readonly ConnectionManager _connectionManager;
        private readonly SqlConnection _connection;
        private readonly IDbConnection _dbConnection;

        public UserRepository(ConnectionManager connectionManager, IDbConnection dbConnection)
        {
             _connection =  connectionManager.GetConnection();
            _dbConnection = dbConnection;
        }

        public User GetUserADO(AuthModel auth)
        {
            var user = new User();

            using (var connection = _connection)
            {
                _connection.Open();

                var command = new SqlCommand("SELECT * FROM [usuario_teste].[dbo].[TbUser]", connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user.Name = reader.GetString(1);
                        user.Email = reader.GetString(2);
                        user.Password = reader.GetString(3);
                        user.CPF = reader.GetString(4);
                        //user.UserType = reader.GetString(5);
                    }
                }
            }
            _connection.Close();

            return user;
        }

        public User GetUser(AuthModel auth)
        {
            var user = new User();

            using (var connection = _connection)
            {
                var query = "SELECT * FROM [usuario_teste].[dbo].[TbUser] WHERE Email = @Email";

                user = _dbConnection.QueryFirstOrDefault<User>(query, auth);                
            }
            return user;
        }     
    }
}
