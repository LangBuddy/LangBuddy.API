using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Service.Http;
using Service.Options;
using System.Net.Http.Headers;

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
            // Здесь можно добавить логику аутентификации, например, отправить запрос на сервер аутентификации и проверить результат
            // В данном примере просто проверяем, что пользователь авторизирован

            string token = context.Request.Headers["Authorization"];

            var res = await _httpService.Send(
                endpoint: $"{_options.Authentication}/check-auth",
                httpMethod: HttpMethod.Get,
                token
            );

            if (!res.Status)
            {
                context.Response.StatusCode = (int)res.Code; // Unauthorized
                await context.Response.WriteAsync("User is not authenticated");
                return;
            }

            await _next(context);
        }
    }
}
