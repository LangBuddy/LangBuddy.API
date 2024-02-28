using Consumer.Models;
using Consumer.Options;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace Consumer.Services.HttpService
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiOptions _options;

        public HttpService(IOptions<ApiOptions> options)
        {
            _httpClient = new HttpClient();
            _options = options.Value;
        }

        public async Task SendEmail(Email email)
        {
            await Console.Out.WriteLineAsync("Start Send");
            await Console.Out.WriteLineAsync($"to: {email.To} \nsubject: {email.Subject} \ntext: {email.Text}");

            var request = new HttpRequestMessage(HttpMethod.Post, $"{_options.EmailSender}email-sender");

            request.Content = new StringContent(
                JsonSerializer.Serialize(email), Encoding.UTF8, "application/json"
            );

            try
            {
                var response = await _httpClient.SendAsync(request);

                await Console.Out.WriteLineAsync(
                         $"Status Sending is {response.IsSuccessStatusCode}"
                    );

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
