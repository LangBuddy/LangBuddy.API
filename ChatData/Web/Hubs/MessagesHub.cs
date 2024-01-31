using Database.Entity;
using Microsoft.AspNetCore.SignalR;

namespace Web.Hubs
{
    public interface IMessagesHub
    {
        Task GetMessages(List<Message> messages);
    }
    public class MessagesHub: Hub<IMessagesHub>
    {
        public async Task GetMessages(List<Message> messages)
        {
            await Clients.Caller.GetMessages(messages);
        }
    }
}
