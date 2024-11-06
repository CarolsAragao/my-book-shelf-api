using my_book_shelf_api.Models;
using my_book_shelf_api.Repositories;

namespace my_book_shelf_api.Services
{
    public class AuthService
    {
        private AuthRepository _authRepository;

        public AuthService(AuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public List<AuthModel> getAuth()
        {
            var auth = new List<AuthModel>();

            auth = _authRepository.GetAuthLogin();

            return auth;
        }
    }
}
