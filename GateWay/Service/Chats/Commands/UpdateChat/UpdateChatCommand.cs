using MediatR;

namespace Service.Chats.Commands.UpdateChat
{
    public record UpdateChatCommand(long ChatId, string? Title,
        List<long>? UsersId) : IRequest;
}
