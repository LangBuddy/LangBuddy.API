using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Services.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Services.Authentication.Commands.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand>
    {
        private readonly IHttpApiService _apiService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginHandler(IHttpApiService apiService,
            IHttpContextAccessor httpContextAccessor)
        {
            _apiService = apiService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var account = await _apiService.GetAccountByEmail(request.Email);

            if (account == null)
            {
                throw new ArgumentNullException(request.Email);
            }

            if(!BCrypt.Net.BCrypt.Verify(request.Password, account.PasswordHash)) 
            {
                throw new ArgumentNullException(request.Password);
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, account.Nickname),
                new Claim(ClaimTypes.Email,account.Email)
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
