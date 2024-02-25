using MediatR;
using Microsoft.AspNetCore.SignalR;
using Models.Responses;
using Service.Chats.Queries.GetMessages;
using Web.Commons;

namespace Web.Hubs
{
    public class MessagesHub: Hub<IMessagesHub>
    {
        private readonly IMediator _mediator;

        public MessagesHub(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<List<GetMessagesResponse>> GetMessages()
        {
            var res = await _mediator.Send(new GetMessagesQuery(1));

            await Clients.All.GetMessagesClient(res);

            return res;
        }
    }
}
