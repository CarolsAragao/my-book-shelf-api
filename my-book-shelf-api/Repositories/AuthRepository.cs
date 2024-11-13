using my_book_shelf_api.Core.Data;
using my_book_shelf_api.Models;
using System.Data.SqlClient;

namespace my_book_shelf_api.Repositories
{
    public class AuthRepository
    {
        private readonly ConnectionManager _connectionManager;
        private readonly SqlConnection _connection;

        public AuthRepository(ConnectionManager connectionManager)
        {
             _connection =  connectionManager.GetConnection();
        }

        public UserModel GetValidLogin(AuthModel auth)
        {
            var user = new UserModel();

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
                        user.UserType = reader.GetString(5);
                    }
                }
            }
            //_connectionManager.CloseConnection();
            _connection.Close();

            return user;
        }
    }
}
