using Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Services.Accounts.Queries.GetAccounts
{
    public class GetAccountsHandler : IRequestHandler<GetAccountsQuery, List<Database.Entity.Accounts>>
    {
        private readonly DataDbContext _dbContext;

        public GetAccountsHandler(DataDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Database.Entity.Accounts>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Accounts.ToListAsync(cancellationToken);
        }
    }
}
