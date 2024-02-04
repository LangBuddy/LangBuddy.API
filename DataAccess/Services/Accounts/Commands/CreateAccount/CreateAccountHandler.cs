using Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Services.Accounts.Commands.CreateAccount
{
    public class CreateAccountHandler : IRequestHandler<CreateAccountCommand>
    {
        private readonly DataDbContext _dbContext;

        public CreateAccountHandler(DataDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var findAccount = await _dbContext.Accounts
                .FirstOrDefaultAsync(
                    el => el.DeleteDate != null &&
                    (el.Nickname.Equals(request.Nickname) || el.Email.Equals(request.Email)),
                    cancellationToken
                );

            if (findAccount != null)
            {
                throw new ArgumentException($"{nameof(findAccount.Nickname)}");
            }

            var account = new Database.Entity.Accounts()
            {
                Nickname = request.Nickname,
                Email = request.Email,
                PasswordHash = request.PasswordHash
            };

            account.SetCreateTime();

            await _dbContext.Accounts.AddAsync(account, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
