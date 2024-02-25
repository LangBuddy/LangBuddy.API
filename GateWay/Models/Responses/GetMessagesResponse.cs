namespace Models.Responses
{
    public record GetMessagesResponse(long Id, string Value, long ChatRoomId, long UserId);
}
