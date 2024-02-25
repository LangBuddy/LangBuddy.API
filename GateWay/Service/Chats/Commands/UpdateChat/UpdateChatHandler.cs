using MediatR;
using Microsoft.Extensions.Options;
using Models.Requests;
using Service.Http;
using Service.Options;

namespace Service.Chats.Commands.UpdateChat
{
    public class UpdateChatHandler : IRequestHandler<UpdateChatCommand>
    {
        private readonly ApiOptions _options;
        private readonly IHttpService _httpService;

        public UpdateChatHandler(IOptions<ApiOptions> options, IHttpService httpService)
        {
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
            _httpService = httpService;
        }

        public async Task Handle(UpdateChatCommand request, CancellationToken cancellationToken)
        {
            await _httpService.Send<UpdateChatRequest>(
                endpoint: $"{_options.Chat}chats/{request.ChatId}",
                httpMethod: HttpMethod.Patch,
                body: new UpdateChatRequest(request.Title, request.UsersId)
            );

        }
    }
}
