using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Services.Http;
using System.Security.Claims;

namespace Services.Authentication.Commands.Registration
{
    public class RegistrationHandler : IRequestHandler<RegistrationCommand>
    {
        private readonly IHttpApiService _apiService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RegistrationHandler(IHttpApiService apiService,
            IHttpContextAccessor httpContextAccessor) 
        {
            _apiService = apiService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task Handle(RegistrationCommand request, CancellationToken cancellationToken)
        {
            var res = await _apiService.GetAccountByEmail(request.Email);

            if(res != null)
            {
                throw new ArgumentException("Pizdec");
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            await _apiService.CreateAccountSend(email: request.Email, nickname: request.Nickname, passwordHash: passwordHash);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, request.Nickname),
                new Claim(ClaimTypes.Email,request.Email)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await _httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                new AuthenticationProperties
                {
                    IsPersistent = false   //remember me
                }
            );

        }
    }
}
