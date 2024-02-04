using Database.Mocks;

namespace Models.Requests
{
    public record UserUpdateRequest(
        string? FirstName,
        string? LastName,
        GenderList? Gender,
        DateTime? BirthDate
    );
}
