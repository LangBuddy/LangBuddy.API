namespace Services.RabbiSendMessageService
{
    public interface IRabbiSendMessageService
    {
        void SendMessage<T>(T message);
    }
}
