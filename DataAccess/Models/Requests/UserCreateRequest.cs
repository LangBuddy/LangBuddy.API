using Database.Mocks;

namespace Models.Requests
{
    public record UserCreateRequest(
        long AccountId,
        string FirstName,
        string LastName,
        GenderList Gender,
        DateTime BirthDate
    );
}
