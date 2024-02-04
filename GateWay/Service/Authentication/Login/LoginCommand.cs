using MediatR;
using Models.Responses;

namespace Service.Authentication.Login
{
    public record LoginCommand(string Email, string Password) : IRequest<LoginResponse>;
}
