namespace Models.Requests
{
    public record AccountCreateRequest(string Email, string Nickname, string PasswordHash);
}
