using MediatR;

namespace Services.Accounts.Commands.UpdateAccount
{
    public record UpdateAccountCommand(long AccountId, string? Email, string? Nickname, string? PasswordHash) : IRequest;
}
