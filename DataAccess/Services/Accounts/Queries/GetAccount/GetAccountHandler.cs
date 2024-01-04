using Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Services.Accounts.Queries.GetAccount
{
    public class GetAccountHandler : IRequestHandler<GetAccountQuery, Database.Entity.Accounts>
    {
        private readonly DataDbContext _dbContext;

        public GetAccountHandler(DataDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Database.Entity.Accounts> Handle(GetAccountQuery request, CancellationToken cancellationToken)
        {
            var account = await _dbContext.Accounts
                .FirstOrDefaultAsync(el => el.Email.Equals(request.Email));

            if(account == null)
            {
                throw new ArgumentNullException(nameof(request.Email));
            }

            return account;
        }
    }
}
