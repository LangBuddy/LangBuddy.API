using Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Services.Users.Queries.GetUsers
{
    public class GetUsersHandler : IRequestHandler<GetUsersQuery, List<Database.Entity.Users>>
    {
        private readonly DataDbContext _dbContext;

        public GetUsersHandler(DataDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Database.Entity.Users>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _dbContext.Users.ToListAsync(cancellationToken);
            return users;
        }
    }
}
