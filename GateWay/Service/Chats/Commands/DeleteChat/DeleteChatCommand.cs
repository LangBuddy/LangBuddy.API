using MediatR;

namespace Service.Chats.Commands.DeleteChat
{
    public record DeleteChatCommand(long chatId): IRequest;
}
