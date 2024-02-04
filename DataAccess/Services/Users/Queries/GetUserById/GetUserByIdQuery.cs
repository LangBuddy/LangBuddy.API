using MediatR;

namespace Services.Users.Queries.GetUserById
{
    public record GetUserByIdQuery(long UserId):IRequest<Database.Entity.Users>;
}
