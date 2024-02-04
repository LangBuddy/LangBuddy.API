using Microsoft.AspNetCore.Builder;

namespace Service.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthenticationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenticationMiddleware>();
        }
    }
}
