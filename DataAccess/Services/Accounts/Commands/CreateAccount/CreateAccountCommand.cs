using MediatR;

namespace Services.Accounts.Commands.CreateAccount
{
    public record CreateAccountCommand(string Email, string Nickname, string PasswordHash) : IRequest;
}
