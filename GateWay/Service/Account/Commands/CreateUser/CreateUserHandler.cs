using MediatR;
using Microsoft.Extensions.Options;
using Service.Http;
using Service.Options;

namespace Service.Account.Commands.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly ApiOptions _options;
        private readonly IHttpService _httpService;

        public CreateUserHandler(IOptions<ApiOptions> options, IHttpService httpService)
        {
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
            _httpService = httpService;
        }

        public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            await _httpService.Send<CreateUserCommand>(
                endpoint: $"{_options.Accounts}users",
                httpMethod: HttpMethod.Post,
                body: request
            );

        }
    }
}
