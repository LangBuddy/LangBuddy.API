using MediatR;

namespace Services.Accounts.Commands.DeleteAccount
{
    public record DeleteAccountCommand(string Email): IRequest;
}
