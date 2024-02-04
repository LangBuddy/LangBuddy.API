using MediatR;

namespace Services.Accounts.Commands.DeleteAccount
{
    public record DeleteAccountCommand(long AccountId): IRequest;
}
