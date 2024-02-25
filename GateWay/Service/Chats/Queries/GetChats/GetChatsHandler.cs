using MediatR;
using Microsoft.Extensions.Options;
using Models.Responses;
using Service.Chats.Commands.CreateChat;
using Service.Http;
using Service.Options;

namespace Service.Chats.Queries.GetChats
{
    public class GetChatsHandler : IRequestHandler<GetChatsQuery, GetChatsResponse>
    {
        private readonly ApiOptions _options;
        private readonly IHttpService _httpService;

        public GetChatsHandler(IOptions<ApiOptions> options, IHttpService httpService)
        {
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
            _httpService = httpService;
        }
        public async Task<GetChatsResponse?> Handle(GetChatsQuery request, CancellationToken cancellationToken)
        {
            var res = await _httpService.Send<List<ChatsResponse>>(
                endpoint: $"{_options.Chat}chats/{request.UserId}",
                httpMethod: HttpMethod.Get
            );

            if (res.Status)
            {
                return new GetChatsResponse
                {
                    chats = ((HttpResponse<List<ChatsResponse>>)res).Result,
                };
            }

            return null;
        }
    }
}
