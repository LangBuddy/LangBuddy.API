using Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Services.Users.Commands.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly DataDbContext _dataDbContext;

        public CreateUserHandler(DataDbContext dataDbContext)
        {
            _dataDbContext = dataDbContext;
        }

        public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var account = await _dataDbContext.Accounts
                .FirstOrDefaultAsync(el => el.Id == request.AccountId, cancellationToken);

            if (account == null)
            {
                throw new ArgumentNullException(nameof(request.AccountId));   
            }

            var user = new Database.Entity.Users()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Gender = request.Gender,
                BirthDate = request.BirthDate,
            };

            user.SetCreateTime();

            account.User = user;

            await _dataDbContext.Users.AddAsync(user, cancellationToken);

            await _dataDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
