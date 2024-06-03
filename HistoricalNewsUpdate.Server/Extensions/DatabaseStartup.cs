using HistoricalNewsUpdate.Entities;
using Microsoft.EntityFrameworkCore;

namespace HistoricalNewsUpdate.Extensions
{
    public static class DatabaseStartup
    {
        public static IServiceCollection AddDatabaseModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<HistoricalNewsUpdateDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("OpifexDb")));
            return services;
        }
    }
}
