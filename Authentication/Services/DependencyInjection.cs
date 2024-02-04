using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Authentication;
using Services.Http;
using Services.Options;

namespace Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            services.Configure<ApiOptions>(u => configuration.GetSection("ApiOptions").Bind(u));
            services.Configure<JwtConfiguration>(u => configuration.GetSection("JwtConfiguration").Bind(u));

            services.AddTransient<IHttpService, HttpService>();
            services.AddTransient<IHttpApiService, HttpApiService>();

            services.AddAuthenticationConfiguration(configuration);

            return services;
        }
    }
}