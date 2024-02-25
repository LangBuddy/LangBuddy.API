using Models.Responses;

namespace Web.Commons
{
    public interface IChatHub
    {
        Task GetChatsClient(GetChatsResponse getChatsResponse);
    }
}
