using MediatR;

namespace Services.Authentication.Commands.Registration
{
    public record RegistrationCommand(string Email, string Nickname, string Password) : IRequest;
}
