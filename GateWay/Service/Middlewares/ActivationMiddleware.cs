using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Models.Responses;
using Service.Http;
using Service.Options;
using System.IdentityModel.Tokens.Jwt;

namespace Service.Middlewares
{
    public class ActivationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ApiOptions _options;
        private readonly IHttpService _httpService;

        public ActivationMiddleware(RequestDelegate next,
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

            if (token is not null)
            {
                token = token.Split(' ')[1];

                var res = await _httpService.Send<AccountDataResponse>(
                    endpoint: $"{_options.Authentication}/check-auth",
                    httpMethod: HttpMethod.Get,
                    token
                );

                if (!res.Status)
                {
                    context.Response.StatusCode = (int)res.Code; // Unauthorized

                    if((int)res.Code != 403)
                    {
                        await context.Response.WriteAsync("User is not authenticated");
                        return;
                    }
                }

                if(((HttpResponse<AccountDataResponse>)res).Result is not null)
                {
                    context.Items["Id"] = ((HttpResponse<AccountDataResponse>)res).Result.Id;
                    context.Items["Nickname"] = ((HttpResponse<AccountDataResponse>)res).Result.Nickname;
                    context.Items["UserId"] = ((HttpResponse<AccountDataResponse>)res).Result.UserId;
                    context.Items["Email"] = ((HttpResponse<AccountDataResponse>)res).Result.Email;
                }

                await _next(context);
                return;
            }

            context.Response.StatusCode = 401; // Unauthorized
            await context.Response.WriteAsync("User is not authenticated");
            return;
        }
    }
}
