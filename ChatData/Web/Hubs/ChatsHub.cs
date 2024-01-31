using Database.Entity;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Service.Chats.Queries.GetChatsByUserId;
using System;

namespace Web.Hubs
{
    public interface IChatsHub
    {
        Task GetChats(List<ChatRoom> chats);
    }
    public class ChatsHub : Hub<IChatsHub>
    {
        public async Task GetChats(List<ChatRoom> chats)
        {
            await Clients.Caller.GetChats(chats);
        }
    }
}
