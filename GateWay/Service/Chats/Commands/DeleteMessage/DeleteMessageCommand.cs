using MediatR;

namespace Service.Chats.Commands.DeleteMessage
{
    public record DeleteMessageCommand(long messageId): IRequest;
}
