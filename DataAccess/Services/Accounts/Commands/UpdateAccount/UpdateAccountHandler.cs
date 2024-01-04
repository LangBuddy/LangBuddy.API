using Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Services.Accounts.Commands.UpdateAccount
{
    public class UpdateAccountHandler : IRequestHandler<UpdateAccountCommand>
    {
        private readonly DataDbContext _dbContext;

        public UpdateAccountHandler(DataDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            var findAccount = await _dbContext.Accounts
                .FirstOrDefaultAsync(el => el.DeleteDate != null && el.Email.Equals(request.CurrentEmail));

            if (findAccount == null)
            {
                throw new ArgumentNullException($"{nameof(request.CurrentEmail)}");
            }

            if (request.Email is not null)
            {
                findAccount.Email = request.Email;
            }

            if (request.Nickname is not null)
            {
                findAccount.Nickname = request.Nickname;
            }

            if (request.PasswordHash is not null)
            {
                findAccount.PasswordHash = request.PasswordHash;
            }

            findAccount.SetUpdateTime();

            _dbContext.SaveChanges();
        }
    }
}
