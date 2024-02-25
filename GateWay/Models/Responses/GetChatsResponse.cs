
namespace Models.Responses
{
    public record ChatsResponse(long Id,
        string Title);
    public class GetChatsResponse
    {
        public List<ChatsResponse> chats { get; set; }
    }
}
