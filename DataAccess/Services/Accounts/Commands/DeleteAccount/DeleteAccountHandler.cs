using Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Services.Accounts.Commands.DeleteAccount
{
    public class DeleteAccountHandler : IRequestHandler<DeleteAccountCommand>
    {
        private readonly DataDbContext _dbContext;

        public DeleteAccountHandler(DataDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            var findAccount = await _dbContext.Accounts
                .FirstOrDefaultAsync(el => el.DeleteDate != null && el.Email.Equals(request.Email));

            if (findAccount == null)
            {
                throw new ArgumentNullException($"{nameof(request.Email)}");
            }

            findAccount.SeDeleteTime();

            _dbContext.SaveChanges();
        }
    }
}
