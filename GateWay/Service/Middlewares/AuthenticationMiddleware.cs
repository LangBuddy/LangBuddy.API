using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Service.Http;
using Service.Options;
using System.IdentityModel.Tokens.Jwt;
using System.IO;

namespace Service.Middlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ApiOptions _options;
        private readonly IHttpService _httpService;

        public AuthenticationMiddleware(RequestDelegate next,
            IOptions<ApiOptions> options,
            IHttpService httpService)
        {
            _next = next;
            _options = options.Value;
            _httpService = httpService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string token = context.Request.Headers["Authorization"];

            var handler = new JwtSecurityTokenHandler();

            if(token is not null)
            {
                token = token.Split(' ')[1];

                var res = await _httpService.Send(
                    endpoint: $"{_options.Authentication}/check-auth",
                    httpMethod: HttpMethod.Get,
                    token
                );

                if (res.Status)
                {
                    await _next(context);
                    return;
                }

                await _next(context);
            }

            context.Response.StatusCode = 401; // Unauthorized
            await context.Response.WriteAsync("User is not authenticated");
            return;
        }
    }
}
