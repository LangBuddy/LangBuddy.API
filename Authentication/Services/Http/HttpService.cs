using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace Services.Http
{
    public interface IHttpService
    {
        Task<HttpResponseDefault> Send<TRequest, TResponse>(string endpoint, HttpMethod httpMethod, TRequest body);
        Task<HttpResponseDefault> Send<TRequest>(string endpoint, HttpMethod httpMethod, TRequest body);
        Task<HttpResponseDefault> Send<TResponse>(string endpoint, HttpMethod httpMethod);
        Task<HttpResponseDefault> Send(string endpoint, HttpMethod httpMethod);
    }
    public class HttpService: IHttpService
    {
        private readonly HttpClient _httpClient;
        public HttpService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<HttpResponseDefault> Send<TRequest, TResponse>(string endpoint, HttpMethod httpMethod, TRequest body)
        {
            var request = new HttpRequestMessage(httpMethod, endpoint);

            if(body != null )
            {
                request.Content = new StringContent(
                    JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"
                );
            }

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<TResponse>();

                //var accountCreatedResponse = JsonConvert.DeserializeObject<TResponse>(content.ToString());
                return new HttpResponse<TResponse>
                {
                    Status = true,
                    Result = content
                };
            }

            return new HttpResponseDefault { Status = false };

        }

        public async Task<HttpResponseDefault> Send<TRequest>(string endpoint, HttpMethod httpMethod, TRequest body)
        {
            var request = new HttpRequestMessage(httpMethod, endpoint);

            if (body != null)
            {
                request.Content = new StringContent(
                    JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"
                );
            }

            var response = await _httpClient.SendAsync(request);

            return new HttpResponseDefault
            {
                Status = response.IsSuccessStatusCode
            };
        }

        public async Task<HttpResponseDefault> Send<TResponse>(string endpoint, HttpMethod httpMethod)
        {
            var request = new HttpRequestMessage(httpMethod, endpoint);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<TResponse>();

                //var accountCreatedResponse = JsonConvert.DeserializeObject<TResponse>(content.ToString());
                return new HttpResponse<TResponse> 
                {
                    Status = true,
                    Result = content,
                };
            }

            return new HttpResponseDefault
            {
                Status = false
            };
        }

        public async Task<HttpResponseDefault> Send(string endpoint, HttpMethod httpMethod)
        {
            var request = new HttpRequestMessage(httpMethod, endpoint);

            var response = await _httpClient.SendAsync(request);

            return new HttpResponseDefault
            {
                Status = response.IsSuccessStatusCode
            };
        }
    }
}
