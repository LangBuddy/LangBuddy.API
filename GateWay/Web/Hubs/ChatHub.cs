using MediatR;
using Microsoft.AspNetCore.SignalR;
using Models.Responses;
using Service.Chats.Queries.GetChats;
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
            var res = await _mediator.Send(new GetChatsQuery(1));

            await Clients.All.GetChatsClient(res);

            return res;
        }
    }
}
