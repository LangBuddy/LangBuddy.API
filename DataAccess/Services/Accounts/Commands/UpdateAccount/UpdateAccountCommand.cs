using MediatR;

namespace Services.Accounts.Commands.UpdateAccount
{
    public record UpdateAccountCommand(string CurrentEmail, string? Email, string? Nickname, string? PasswordHash) : IRequest;
}
