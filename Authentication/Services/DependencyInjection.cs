using Microsoft.AspNetCore.Http;
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
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            services.Configure<ApiOptions>(u => configuration.GetSection("ApiOptions").Bind(u));

            services.AddTransient<IHttpService, HttpService>();
            services.AddTransient<IHttpApiService, HttpApiService>();

            services.AddAuthenticationConfiguration();

            return services;
        }
    }
}