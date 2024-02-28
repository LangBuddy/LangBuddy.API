using MediatR;
using Microsoft.Extensions.Options;
using Models.Requests;
using Models.Responses;
using Service.Http;
using Service.Options;

namespace Service.Authentication.Registration
{
    public class RegistrationHandler : IRequestHandler<RegistrationCommand>
    {
        private readonly ApiOptions _options;
        private readonly IHttpService _httpService;

        public RegistrationHandler(IOptions<ApiOptions> options, IHttpService httpService)
        {
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
            _httpService = httpService;
        }
        public async Task Handle(RegistrationCommand request, CancellationToken cancellationToken)
        {
            await _httpService.Send<RegistrationRequest>(
                endpoint: $"{_options.Authentication}/registration",
                httpMethod: HttpMethod.Post,
                body: new RegistrationRequest(request.Email, request.Nickname, request.Password)
            );
        }
    }
}
