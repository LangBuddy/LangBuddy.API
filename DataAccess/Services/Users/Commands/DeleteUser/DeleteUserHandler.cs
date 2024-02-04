using Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Services.Users.Commands.DeleteUser
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly DataDbContext _dbContext;

        public DeleteUserHandler(DataDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(el => el.Id == request.UserId, cancellationToken);

            user.SetDeleteTime();

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
