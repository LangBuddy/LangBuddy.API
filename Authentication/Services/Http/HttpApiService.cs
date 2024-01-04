using Microsoft.Extensions.Options;
using Models.Requests;
using Models.Responses;
using Services.Options;

namespace Services.Http
{
    public interface IHttpApiService
    {
        Task<GetAccountResponse> GetAccountByEmail(string email);
        Task CreateAccountSend(string email, string nickname, string passwordHash);
    }
    public class HttpApiService: IHttpApiService
    {
        private readonly ApiOptions _options;
        private readonly IHttpService _httpService;

        public HttpApiService(IOptions<ApiOptions> options, IHttpService httpService)
        {
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
            _httpService = httpService;
        }

        public async Task<GetAccountResponse> GetAccountByEmail(string email)
        {
            var res = await _httpService.Send<GetAccountResponse>(
                endpoint: $"{_options.DataAccess}/accounts/{email}",
                httpMethod: HttpMethod.Get
            );

            if (res.Status)
            {
                return ((HttpResponse<GetAccountResponse>)res).Result;
            }

            return null;
        }

        public async Task CreateAccountSend(string email, string nickname, string passwordHash)
        {
            await _httpService.Send<CreateAccountRequest>(
                endpoint: $"{_options.DataAccess}/accounts",
                httpMethod: HttpMethod.Post,
                body: new CreateAccountRequest(email, nickname, passwordHash)
            );
        }
    }
}
