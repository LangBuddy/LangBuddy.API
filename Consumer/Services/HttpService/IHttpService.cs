using Consumer.Models;

namespace Consumer.Services.HttpService
{
    public interface IHttpService
    {
        Task SendEmail(Email email);
    }
}
