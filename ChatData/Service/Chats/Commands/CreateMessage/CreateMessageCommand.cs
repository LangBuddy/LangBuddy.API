using MediatR;

namespace Service.Chats.Commands.CreateMessage
{
    public record CreateMessageCommand(string Value, long ChatRoomId, long UseId) : IRequest;
}
