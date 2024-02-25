using MediatR;
using Microsoft.Extensions.Options;
using Models.Requests;
using Models.Responses;
using Service.Http;
using Service.Options;

namespace Service.Chats.Commands.CreateChat
{
    public class CreateChatHandler : IRequestHandler<CreateChatCommand>
    {
        private readonly ApiOptions _options;
        private readonly IHttpService _httpService;

        public CreateChatHandler(IOptions<ApiOptions> options, IHttpService httpService)
        {
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
            _httpService = httpService;
        }

        public async Task Handle(CreateChatCommand request, CancellationToken cancellationToken)
        {
            await _httpService.Send<CreateChatCommand>(
                endpoint: $"{_options.Chat}chats",
                httpMethod: HttpMethod.Post,
                body: request
            );

        }
    }
}
