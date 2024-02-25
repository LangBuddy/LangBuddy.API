using MediatR;

namespace Service.Chats.Commands.DeleteMessage
{
    public record DeleteMessageCommand(long MessageId): IRequest;
}
