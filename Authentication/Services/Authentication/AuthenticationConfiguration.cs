using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;

namespace Services.Authentication
{
    public static class AuthenticationConfiguration
    {
        public static IServiceCollection AddAuthenticationConfiguration(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie();

            return services;
        }
    }
}
