using MediatR;
using Models.Commons;

namespace Service.Account.Commands.CreateUser
{
    public record CreateUserCommand(long AccountId,
        string FirstName,
        string LastName,
        GenderList Gender,
        DateTime BirthDate) : IRequest;
}
