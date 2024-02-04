using MediatR;
using Models.Responses;

namespace Service.Authentication.Registration
{
    public record RegistrationCommand(string Email, string Nickname, string Password) : IRequest<RegistrationResponse>;
}
