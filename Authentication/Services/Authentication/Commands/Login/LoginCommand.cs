using MediatR;
using Models.Responses;

namespace Services.Authentication.Commands.Login
{
    public record LoginCommand(string Email, string Password): IRequest<LoginResponse>;
}
