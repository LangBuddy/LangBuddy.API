using MediatR;

namespace Service.Chats.Commands.UpdateMessage
{
    public record UpdateMessageCommand(
        long MessageId,
        string Value
    ) : IRequest;
}
