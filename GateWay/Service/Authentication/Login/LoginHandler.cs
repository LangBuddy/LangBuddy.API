using MediatR;
using Service.Http;
using Service.Options;
using Microsoft.Extensions.Options;
using Models.Requests;
using Models.Responses;

namespace Service.Authentication.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, LoginResponse?>
    {
        private readonly ApiOptions _options;
        private readonly IHttpService _httpService;

        public LoginHandler(IOptions<ApiOptions> options, IHttpService httpService)
        {
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
            _httpService = httpService;
        }
        public async Task<LoginResponse?> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var res = await _httpService.Send<LoginRequest, LoginResponse>(
                endpoint: $"{_options.Authentication}/login",
                httpMethod: HttpMethod.Post,
                body: new LoginRequest(request.Email, request.Password)
            );

            if (res.Status)
            {

                return ((HttpResponse<LoginResponse>)res).Result;
            }

            return null;
        }
    }
}
