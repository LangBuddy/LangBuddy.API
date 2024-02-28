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
    public class ChatController : ControllerBase
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
                if (HttpContext.Items.TryGetValue("UserId", out object userId))
                {
                   var userIdValue = long.Parse(userId.ToString());

                    var chats = await _mediator.Send(new GetChatsQuery(userIdValue));

                    return Ok(chats);
                }

                return NotFound();
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

                if (HttpContext.Items.TryGetValue("UserId", out object userId))
                {
                   var userIdValue = long.Parse(userId.ToString());

                    var chats = await _mediator.Send(new GetChatsQuery(userIdValue));

                    _chatHubContext.Clients.All.GetChatsClient(chats);
                }

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

                if (HttpContext.Items.TryGetValue("UserId", out object userId))
                {
                    var userIdValue = long.Parse(userId.ToString());

                    var chats = await _mediator.Send(new GetChatsQuery(userIdValue));

                    _chatHubContext.Clients.All.GetChatsClient(chats);
                }

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

                if (HttpContext.Items.TryGetValue("UserId", out object userId))
                {
                    var userIdValue = long.Parse(userId.ToString());

                    var chats = await _mediator.Send(new GetChatsQuery(userIdValue));

                    _chatHubContext.Clients.All.GetChatsClient(chats);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
