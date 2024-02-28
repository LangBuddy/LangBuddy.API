﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Models.Responses;
using Service.Http;
using Service.Options;
using System.IdentityModel.Tokens.Jwt;

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
            string authToken = context.Request.Headers["Authorization"];

            if(authToken != null)
            {
                authToken = authToken.Split(' ')[1];
            }

            var accessToken = context.Request.Query["access_token"];

            var token = "";

            var path = context.Request.Path;


            if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/api/private/chat-hub"))
            {
                // получаем токен из строки запроса
                token = accessToken;
            }
            else
            {
                token = authToken;
            }


            var handler = new JwtSecurityTokenHandler();

            if(token is not null)
            {
                var res = await _httpService.Send<AccountDataResponse>(
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

                context.Items["Id"] = ((HttpResponse<AccountDataResponse>)res).Result.Id;
                context.Items["Nickname"] = ((HttpResponse<AccountDataResponse>)res).Result.Nickname;
                context.Items["UserId"] = ((HttpResponse<AccountDataResponse>)res).Result.UserId;
                context.Items["Email"] = ((HttpResponse<AccountDataResponse>)res).Result.Email;

                await _next(context);
                return;
            }

            context.Response.StatusCode = 401; // Unauthorized
            await context.Response.WriteAsync("User is not authenticated");
            return;
        }
    }
}
