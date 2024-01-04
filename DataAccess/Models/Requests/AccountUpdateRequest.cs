namespace Models.Requests
{
    public record AccountUpdateRequest(string? Email, string? Nickname, string? PasswordHash);
}
