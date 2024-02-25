using MediatR;
using Models.Responses;

namespace Service.Chats.Queries.GetChatsByUserId
{
    public record GetChatsByUserIdQuery(long UserId): IRequest<List<ChatResponse>>;
}
