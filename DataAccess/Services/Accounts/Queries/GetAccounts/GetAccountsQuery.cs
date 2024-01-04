using MediatR;

namespace Services.Accounts.Queries.GetAccounts
{
    public record GetAccountsQuery(): IRequest<List<Database.Entity.Accounts>>;
}
