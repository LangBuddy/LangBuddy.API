using MediatR;
using Microsoft.Extensions.Options;
using Service.Http;
using Service.Options;

namespace Service.Chats.Commands.DeleteMessage
{
    public class DeleteMessageHandler : IRequestHandler<DeleteMessageCommand>
    {
        private readonly ApiOptions _options;
        private readonly IHttpService _httpService;

        public DeleteMessageHandler(IOptions<ApiOptions> options, IHttpService httpService)
        {
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
            _httpService = httpService;
        }

        public async Task Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
            await _httpService.Send(
                endpoint: $"{_options.Chat}messages/{request.messageId}",
                httpMethod: HttpMethod.Delete
            );

        }
    }
}
