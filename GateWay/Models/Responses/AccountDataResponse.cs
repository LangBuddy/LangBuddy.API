namespace Models.Responses
{
    public record AccountDataResponse(long Id, long? UserId, string Email, string Nickname);
}
