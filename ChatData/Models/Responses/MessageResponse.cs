namespace Models.Responses
{
    public record MessageResponse(
        long Id,
        string Value,
        long ChatRoomId,
        long UserId
    );
}
