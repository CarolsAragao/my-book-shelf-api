using Microsoft.EntityFrameworkCore;
using my_book_shelf_api.Core.Data;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace my_book_shelf_api.Core.Extensions
{
    public static class DataBaseConnectionExtension
    {
        public static IServiceCollection AddDataBaseConnection(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IDbConnection>(sp =>
                new SqlConnection(config.GetConnectionString("DefaultConnection")));

            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });

            return services;

        }
    }
}
