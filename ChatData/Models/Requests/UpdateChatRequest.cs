namespace Models.Requests
{
    public record UpdateChatRequest(
        string? Title,
        List<long>? UsersId
    );
}
