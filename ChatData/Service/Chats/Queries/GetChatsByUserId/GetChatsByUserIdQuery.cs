using Database.Entity;
using MediatR;

namespace Service.Chats.Queries.GetChatsByUserId
{
    public record GetChatsByUserIdQuery(long UserId): IRequest<List<ChatRoom>>;
}
