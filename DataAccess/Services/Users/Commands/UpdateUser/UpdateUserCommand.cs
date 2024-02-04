using Database.Mocks;
using MediatR;

namespace Services.Users.Commands.UpdateUser
{
    public record UpdateUserCommand(
        long UserId,
        string? FirstName,
        string? LastName,
        GenderList? Gender,
        DateTime? BirthDate
    ) : IRequest;
}
