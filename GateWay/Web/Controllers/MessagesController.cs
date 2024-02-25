using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Models.Requests;
using Service.Chats.Commands.CreateChat;
using Service.Chats.Commands.CreateMessage;
using Service.Chats.Commands.DeleteChat;
using Service.Chats.Commands.DeleteMessage;
using Service.Chats.Commands.UpdateChat;
using Service.Chats.Commands.UpdateMessage;
using Service.Chats.Queries.GetChats;
using Service.Chats.Queries.GetMessages;
using System;
using Web.Commons;
using Web.Hubs;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/private/[controller]")]
    public class MessagesController: ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHubContext<MessagesHub, IMessagesHub> _messageHubContext;
        public MessagesController(IMediator mediator, IHubContext<MessagesHub, IMessagesHub> messageHubContext)
        {
            _mediator = mediator;
            _messageHubContext = messageHubContext;
        }

        [HttpGet("{chatId}")]
        public async Task<IActionResult> GetMessages([FromRoute] long chatId)
        {
            try
            {
                var chats = await _mediator.Send(new GetMessagesQuery(chatId));

                return Ok(chats);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(CreateMessageRequest createMessageRequest)
        {
            try
            {
                await _mediator.Send(new CreateMessageCommand(createMessageRequest.Value, createMessageRequest.ChatRoomId, 1));

                var chats = await _mediator.Send(new GetMessagesQuery(createMessageRequest.ChatRoomId));
                _messageHubContext.Clients.All.GetMessagesClient(chats);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{messageId}")]
        public async Task<IActionResult> UpdateMessage([FromRoute] long messageId, [FromBody] UpdateMessageRequest updateMessageRequest)
        {
            try
            {
                await _mediator.Send(new UpdateMessageCommand(messageId, updateMessageRequest.Value));

                //if(updateMessageRequest.ChatRoomId is not  null)
                //{
                //    var chats = await _mediator.Send(new GetMessagesQuery(updateMessageRequest.ChatRoomId));
                //    _messageHubContext.Clients.All.GetMessagesClient(chats);
                //}

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{messageId}")]
        public async Task<IActionResult> DeleteChat([FromRoute] long messageId)
        {
            try
            {
                await _mediator.Send(new DeleteMessageCommand(messageId));

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
