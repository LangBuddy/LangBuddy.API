using MediatR;
using Models.Responses;

namespace Service.Chats.Queries.GetChats
{
    public record GetChatsQuery(long UserId): IRequest<GetChatsResponse>;
}
