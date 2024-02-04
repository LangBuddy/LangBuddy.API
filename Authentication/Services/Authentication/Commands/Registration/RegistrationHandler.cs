using MediatR;
using Services.Http;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Services.Options;
using System.IdentityModel.Tokens.Jwt;
using Models.Responses;
using Microsoft.Extensions.Options;

namespace Services.Authentication.Commands.Registration
{
    public class RegistrationHandler : IRequestHandler<RegistrationCommand>
    {
        private readonly IHttpApiService _apiService;
        private readonly JwtConfiguration _jwtConfiguration;


        public RegistrationHandler(IHttpApiService apiService,
           IOptions<JwtConfiguration> jwtConfiguration) 
        {
            _apiService = apiService;
            _jwtConfiguration = jwtConfiguration.Value;
        }
        public async Task Handle(RegistrationCommand request, CancellationToken cancellationToken)
        {
            var res = await _apiService.GetAccountById(request.Email);

            if(res != null)
            {
                throw new ArgumentException("Pizdec");
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            await _apiService.CreateAccountSend(email: request.Email, nickname: request.Nickname, passwordHash: passwordHash);
        }
    }
}
