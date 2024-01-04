using MediatR;

namespace Services.Accounts.Queries.GetAccount
{
    public record GetAccountQuery(string Email): IRequest<Database.Entity.Accounts>;
}
