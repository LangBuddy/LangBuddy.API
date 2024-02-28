using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace Service.Http
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        public HttpService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<HttpResponseDefault> Send<TRequest, TResponse>(string endpoint,
            HttpMethod httpMethod,
            TRequest body,
            string? token = null)
        {
            var request = new HttpRequestMessage(httpMethod, endpoint);

            if (token is not null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            if (body != null)
            {
                request.Content = new StringContent(
                    JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"
                );
            }

            var response = await _httpClient.SendAsync(request);
            var content = await response.Content.ReadFromJsonAsync<TResponse>();

            if (content is not null)
            {
                return new HttpResponse<TResponse>
                {
                    Status = response.IsSuccessStatusCode,
                    Code = response.StatusCode,
                    Result = content,
                };
            }

            return new HttpResponseDefault
            {
                Status = response.IsSuccessStatusCode,
                Code = response.StatusCode,
            };

        }

        public async Task<HttpResponseDefault> Send<TRequest>(string endpoint,
            HttpMethod httpMethod,
            TRequest body,
            string? token = null)
        {
            var request = new HttpRequestMessage(httpMethod, endpoint);

            if (token is not null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            if (body != null)
            {
                request.Content = new StringContent(
                    JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"
                );
            }

            var response = await _httpClient.SendAsync(request);

            return new HttpResponseDefault
            {
                Status = response.IsSuccessStatusCode,
                Code = response.StatusCode,
            };
        }

        public async Task<HttpResponseDefault> Send<TResponse>(string endpoint,
            HttpMethod httpMethod,
            string? token = null)
        {
            var request = new HttpRequestMessage(httpMethod, endpoint);

            if (token is not null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.SendAsync(request);
            var content = await response.Content.ReadFromJsonAsync<TResponse>();

            if(content is not null)
            {
                return new HttpResponse<TResponse>
                {
                    Status = response.IsSuccessStatusCode,
                    Code = response.StatusCode,
                    Result = content,
                };
            }

            return new HttpResponseDefault
            {
                Status = response.IsSuccessStatusCode,
                Code = response.StatusCode,
            };
        }

        public async Task<HttpResponseDefault> Send(string endpoint,
            HttpMethod httpMethod,
            string? token = null)
        {
            var request = new HttpRequestMessage(httpMethod, endpoint);

            if (token is not null) 
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.SendAsync(request);

            return new HttpResponseDefault
            {
                Status = response.IsSuccessStatusCode,
                Code = response.StatusCode,
            };
        }
    }
}
