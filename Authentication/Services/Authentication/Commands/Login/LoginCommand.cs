using MediatR;

namespace Services.Authentication.Commands.Login
{
    public record LoginCommand(string Email, string Password): IRequest;
}
