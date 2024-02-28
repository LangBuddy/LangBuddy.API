namespace Models.Requests
{
    public record SendConfirmRegistrationRequest(string To, string Subject, string Text);
}
