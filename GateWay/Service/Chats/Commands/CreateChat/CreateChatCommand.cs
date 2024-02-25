using MediatR;

namespace Service.Chats.Commands.CreateChat
{
    public record CreateChatCommand(string Title, List<long> UsersId): IRequest;
}
