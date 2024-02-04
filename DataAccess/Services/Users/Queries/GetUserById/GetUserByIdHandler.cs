using Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Services.Users.Queries.GetUserById
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, Database.Entity.Users>
    {
        private readonly DataDbContext _dbContext;

        public GetUserByIdHandler(DataDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Database.Entity.Users> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(el => el.Id == request.UserId, cancellationToken);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(request.UserId));
            }

            return user;
        }
    }
}
