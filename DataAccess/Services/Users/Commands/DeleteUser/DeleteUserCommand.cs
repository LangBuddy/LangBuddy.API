using MediatR;

namespace Services.Users.Commands.DeleteUser
{
    public record DeleteUserCommand(long UserId): IRequest;
}
