namespace Models.Requests
{
    public record UpdateMessageRequest(string Value, long? ChatRoomId = null);
}
