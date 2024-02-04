using Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Services.Users.Commands.UpdateUser
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly DataDbContext _dbContext;

        public UpdateUserHandler(DataDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(el => el.Id == request.UserId, cancellationToken);

            if(request.FirstName is not null)
            {
                user.FirstName = request.FirstName;
            }

            if (request.LastName is not null)
            {
                user.LastName = request.LastName;
            }

            if (request.Gender is not null)
            {
                user.Gender = (Database.Mocks.GenderList)request.Gender;
            }

            if (request.BirthDate is not null)
            {
                user.BirthDate = (DateTime)request.BirthDate;
            }

            user.SetUpdateTime();

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
