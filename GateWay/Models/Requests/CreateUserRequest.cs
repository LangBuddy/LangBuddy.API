using Models.Commons;

namespace Models.Requests
{
    public record CreateUserRequest(string FirstName,
        string LastName,
        GenderList Gender,
        DateTime BirthDate);
}
