namespace Models.Requests
{
    public record CreateAccountRequest(string Email, string Nickname, string PasswordHash);
}
