using MediatR;
using Microsoft.Extensions.Options;
using Models.Requests;
using Service.Http;
using Service.Options;

namespace Service.Chats.Commands.UpdateMessage
{
    public class UpdateMessageHandler : IRequestHandler<UpdateMessageCommand>
    {
        private readonly ApiOptions _options;
        private readonly IHttpService _httpService;

        public UpdateMessageHandler(IOptions<ApiOptions> options, IHttpService httpService)
        {
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
            _httpService = httpService;
        }

        public async Task Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
        {
            await _httpService.Send<UpdateMessageRequest>(
                endpoint: $"{_options.Chat}messages/{request.MessageId}",
                httpMethod: HttpMethod.Patch,
                body: new UpdateMessageRequest(request.Value)
            );

        }
    }
}
