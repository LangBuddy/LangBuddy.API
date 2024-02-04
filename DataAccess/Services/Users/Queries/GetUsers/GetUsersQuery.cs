using MediatR;

namespace Services.Users.Queries.GetUsers
{
    public record GetUsersQuery(): IRequest<List<Database.Entity.Users>>;
}
