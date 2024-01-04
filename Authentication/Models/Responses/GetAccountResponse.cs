namespace Models.Responses
{
    public record GetAccountResponse(string Nickname, string Email, string PasswordHash);
}
