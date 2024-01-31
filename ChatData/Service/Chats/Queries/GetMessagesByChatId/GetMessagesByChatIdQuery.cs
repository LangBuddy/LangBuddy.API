using Database.Entity;
using MediatR;

namespace Service.Chats.Queries.GetMessagesByChatId
{
    public record GetMessagesByChatIdQuery(long ChatRoomId): IRequest<List<Message>>;
}
