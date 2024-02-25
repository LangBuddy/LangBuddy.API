using MediatR;
using Models.Responses;

namespace Service.Chats.Queries.GetMessagesByChatId
{
    public record GetMessagesByChatIdQuery(long ChatRoomId): IRequest<List<MessageResponse>>;
}
