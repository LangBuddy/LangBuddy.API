using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Models.Requests;
using Service.Chats.Commands.CreateChat;
using Service.Chats.Commands.DeleteChat;
using Service.Chats.Commands.UpdateChat;
using Service.Chats.Queries.GetChats;
using Web.Commons;
using Web.Hubs;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/private/[controller]")]
    public class ChatController: ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHubContext<ChatHub, IChatHub> _chatHubContext;
        public ChatController(IMediator mediator, IHubContext<ChatHub, IChatHub> chatHubContext)
        {
            _mediator = mediator;
            _chatHubContext = chatHubContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetChats()
        {
            try
            {
                var chats = await _mediator.Send(new GetChatsQuery(1));

                return Ok(chats);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> CreateChat([FromBody] CreateChatCommand createChatCommand)
        {
            try
            {
                await _mediator.Send(createChatCommand);
                var chats = await _mediator.Send(new GetChatsQuery(1));

                _chatHubContext.Clients.All.GetChatsClient(chats);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPatch("{chatId}")]
        public async Task<IActionResult> UpdateChat([FromRoute] long chatId, [FromBody] UpdateChatRequest updateChatRequest)
        {
            try
            {
                await _mediator.Send(new UpdateChatCommand(chatId, updateChatRequest.Title, updateChatRequest.UsersId));
                var chats = await _mediator.Send(new GetChatsQuery(1));

                _chatHubContext.Clients.All.GetChatsClient(chats);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{chatId}")]
        public async Task<IActionResult> DeleteChat([FromRoute] long chatId)
        {
            try
            {
                await _mediator.Send(new DeleteChatCommand(chatId));
                var chats = await _mediator.Send(new GetChatsQuery(1));

                _chatHubContext.Clients.All.GetChatsClient(chats);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
