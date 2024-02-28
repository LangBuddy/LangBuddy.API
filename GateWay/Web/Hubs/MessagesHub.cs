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
        public async Task<List<GetMessagesResponse>> GetMessages(long chatId)
        {
            var res = await _mediator.Send(new GetMessagesQuery(chatId));

            await Clients.All.GetMessagesClient(res);

            return res;
        }
    }
}
