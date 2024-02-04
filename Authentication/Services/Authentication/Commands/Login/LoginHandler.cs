using MediatR;
using Services.Http;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Services.Options;
using Models.Responses;
using Models.Requests;
using System.Text;

namespace Services.Authentication.Commands.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IHttpApiService _apiService;
        private readonly JwtConfiguration _jwtConfiguration;

        public LoginHandler(IHttpApiService apiService,
            IOptions<JwtConfiguration> jwtConfiguration)
        {
            _apiService = apiService;
            _jwtConfiguration = jwtConfiguration.Value;
        }

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var account = await _apiService.GetAccountById(request.Email);

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
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new Claim(ClaimTypes.Name, account.Nickname),
                new Claim(ClaimTypes.Email,account.Email),
                new Claim("UserId", account.UserId.ToString()),
            };


            var jwt = new JwtSecurityToken(
                issuer: _jwtConfiguration.ISSUER,
                audience: _jwtConfiguration.AUDIENCE,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromDays(7)), // время действия 2 минуты
                signingCredentials: new SigningCredentials(_jwtConfiguration.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
            );

            string token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new LoginResponse(token);
        }
    }
}
