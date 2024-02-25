using Models.Responses;

namespace Web.Commons
{
    public interface IMessagesHub
    {
        Task GetMessagesClient(List<GetMessagesResponse> getMessagesResponses);
    }
}
