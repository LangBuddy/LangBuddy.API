using MediatR;
using Microsoft.AspNetCore.SignalR;
using Models.Responses;
using Service.Chats.Queries.GetChats;
using System.Web.Http;
using Web.Commons;

namespace Web.Hubs
{
    public class ChatHub: Hub<IChatHub>
    {
        private readonly IMediator _mediator;

        public ChatHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<GetChatsResponse> GetChats()
        {
            var httpContext = Context.GetHttpContext();

            if (httpContext != null && httpContext.Items.TryGetValue("UserId", out object userId))
            {
                // Используем данные пользователя
                var userIdValue = long.Parse(userId.ToString());

                var chats = await _mediator.Send(new GetChatsQuery(userIdValue));

                await Clients.All.GetChatsClient(chats);

                return chats;
            }

            throw new ArgumentNullException("UserId");
        }

    }
}
