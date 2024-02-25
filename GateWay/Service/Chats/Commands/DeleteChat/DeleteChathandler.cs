using MediatR;
using Microsoft.Extensions.Options;
using Service.Http;
using Service.Options;

namespace Service.Chats.Commands.DeleteChat
{
    public class DeleteChathandler : IRequestHandler<DeleteChatCommand>
    {
        private readonly ApiOptions _options;
        private readonly IHttpService _httpService;

        public DeleteChathandler(IOptions<ApiOptions> options, IHttpService httpService)
        {
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
            _httpService = httpService;
        }   

        public async Task Handle(DeleteChatCommand request, CancellationToken cancellationToken)
        {
            await _httpService.Send(
                endpoint: $"{_options.Chat}chats/{request.chatId}",
                httpMethod: HttpMethod.Delete
            );

        }
    }
}
