using MediatR;
using Microsoft.Extensions.Options;
using Models.Responses;
using Service.Http;
using Service.Options;

namespace Service.Chats.Queries.GetMessages
{
    public class GetMessagesHandler : IRequestHandler<GetMessagesQuery, List<GetMessagesResponse>>
    {
        private readonly ApiOptions _options;
        private readonly IHttpService _httpService;

        public GetMessagesHandler(IOptions<ApiOptions> options, IHttpService httpService)
        {
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
            _httpService = httpService;
        }
        public async Task<List<GetMessagesResponse>?> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
        {
            var res = await _httpService.Send<List<GetMessagesResponse>> (
                endpoint: $"{_options.Chat}messages/{request.ChatId}",
                httpMethod: HttpMethod.Get
            );

            if (res.Status)
            {
                return ((HttpResponse<List<GetMessagesResponse>>)res).Result;
            }

            return null;
        }
    }
}
