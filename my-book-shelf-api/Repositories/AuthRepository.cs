using my_book_shelf_api.Core.Data;
using my_book_shelf_api.Models;
using System.Data.SqlClient;

namespace my_book_shelf_api.Repositories
{
    public class AuthRepository
    {
        private readonly ConnectionManager _connectionManager;

        public AuthRepository(ConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public List<AuthModel> GetAuthLogin()
        {

            var authList = new List<AuthModel>();

            using (var connection = _connectionManager.GetConnection())
            {
                var command = new SqlCommand("SELECT * FROM [usuario_teste].[dbo].[User]", connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var auth = new AuthModel();
                        auth.Email = reader.GetString(1);
                        auth.Password = reader.GetString(2);

                        authList.Add(auth);
                    }
                }
            }
            _connectionManager.CloseConnection();
            return authList;
        }
    }
}
