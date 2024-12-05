using Microsoft.AspNetCore.Cors.Infrastructure;
using my_book_shelf_api.Core.Data;
using my_book_shelf_api.Core.Helpers;
using my_book_shelf_api.Repositories;
using my_book_shelf_api.Services;
using System.Data;
using System.Data.SqlClient;

namespace my_book_shelf_api.Core.Extensions
{
    public static class DependencyRegistrationExtension
    {
        public static IServiceCollection RegistersDependencies(this IServiceCollection service)
        {
            service.AddScoped<ConnectionManager>();          
            service.AddScoped<AuthService>();
            service.AddScoped<UserService>();
            service.AddScoped<UserRepository>();

            service.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

            return service;
        }
    }
}
