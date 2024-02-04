using MediatR;
using Microsoft.Extensions.Options;
using Models.Requests;
using Models.Responses;
using Service.Http;
using Service.Options;

namespace Service.Authentication.Registration
{
    public class RegistrationHandler : IRequestHandler<RegistrationCommand, RegistrationResponse>
    {
        private readonly ApiOptions _options;
        private readonly IHttpService _httpService;

        public RegistrationHandler(IOptions<ApiOptions> options, IHttpService httpService)
        {
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
            _httpService = httpService;
        }
        public async Task<RegistrationResponse?> Handle(RegistrationCommand request, CancellationToken cancellationToken)
        {
            var res = await _httpService.Send<LoginRequest, LoginResponse>(
                endpoint: $"{_options.Authentication}/registration",
                httpMethod: HttpMethod.Post,
                body: new LoginRequest(request.Email, request.Password)
            );

            if (res.Status)
            {

                return ((HttpResponse<RegistrationResponse>)res).Result;
            }

            return null;
        }
    }
}
