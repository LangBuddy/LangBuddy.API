using MediatR;
using Microsoft.Extensions.Options;
using Service.Chats.Commands.CreateChat;
using Service.Http;
using Service.Options;

namespace Service.Chats.Commands.CreateMessage
{
    public class CreateMessageHandler : IRequestHandler<CreateMessageCommand>
    {
        private readonly ApiOptions _options;
        private readonly IHttpService _httpService;

        public CreateMessageHandler(IOptions<ApiOptions> options, IHttpService httpService)
        {
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
            _httpService = httpService;
        }

        public async Task Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            await _httpService.Send<CreateMessageCommand>(
                endpoint: $"{_options.Chat}messages",
                httpMethod: HttpMethod.Post,
                body: request
            );

        }
    }
}
