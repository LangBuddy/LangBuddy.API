using Database.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Database
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var redisConnectionString = configuration.GetConnectionString("Redis");

            services.AddDbContext<DataDbContext>(options =>
            {
                options.UseNpgsql(connectionString,
                    options => options.UseNodaTime());
            });

            services.AddDistributedMemoryCache();
            services.AddSingleton<ICacheService, CacheService>();

            services.AddStackExchangeRedisCache(redisOptions =>
            {
                redisOptions.Configuration = redisConnectionString;
            });

            return services;
        }
    }
}