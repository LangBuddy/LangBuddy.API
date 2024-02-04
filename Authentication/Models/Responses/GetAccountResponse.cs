namespace Models.Responses
{
    public record GetAccountResponse(long Id, long? UserId, string Nickname, string Email, string PasswordHash);
}
