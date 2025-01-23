using my_book_shelf_api.Core.Data;
using my_book_shelf_api.Core.Profiles;
using my_book_shelf_api.Repositories;
using my_book_shelf_api.Services;

namespace my_book_shelf_api.Core.Extensions;

public static class DependencyRegistrationExtension
{
    public static IServiceCollection RegistersDependencies(this IServiceCollection service)
    {
        service.AddScoped<ConnectionManager>();          
        service.AddScoped<AuthService>();
        service.AddScoped<UserService>();
        service.AddScoped<UserRepository>();

        service.AddAutoMapper(typeof(UserProfile).Assembly);

        return service;
    }
}
