using Database.Entity;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Service.Chats.Commands.CreateMessage;
using Service.Chats.Queries.GetChatsByUserId;
using Service.Chats.Queries.GetMessagesByChatId;
using System;
using Web.Hubs;

namespace Web.Controllers
{
    [Route("[controller]")]
    public class MessagesController: ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHubContext<MessagesHub, IMessagesHub> _messagesHubContext;

        public MessagesController(IMediator mediator, IHubContext<MessagesHub, IMessagesHub> messagesHubContext)
        {
            _mediator = mediator;
            _messagesHubContext = messagesHubContext;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage([FromBody] CreateMessageCommand createMessageCommand)
        {
            try
            {
                await _mediator.Send(createMessageCommand);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{chatId}")]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages([FromRoute] long chatId)
        {

            try
            {
                var res = await _mediator.Send(new GetMessagesByChatIdQuery(chatId));

                await _messagesHubContext.Clients.All.GetMessages(res);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
