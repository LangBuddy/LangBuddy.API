using Database.Mocks;
using MediatR;

namespace Services.Users.Commands.CreateUser
{
    public record CreateUserCommand(
        long AccountId,
        string FirstName,
        string LastName,
        GenderList Gender,
        DateTime BirthDate
    ) : IRequest;
}
