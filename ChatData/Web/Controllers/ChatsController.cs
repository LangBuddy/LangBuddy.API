using Database.Entity;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Service.Chats.Commands.CreateChat;
using Service.Chats.Queries.GetChatsByUserId;
using Web.Hubs;

namespace Web.Controllers
{
    [Route("[controller]")]
    public class ChatsController: ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHubContext<ChatsHub, IChatsHub> _chatHubContext;
        public ChatsController(IMediator mediator, IHubContext<ChatsHub, IChatsHub> chatHubContext)
        {
            _mediator = mediator;
            _chatHubContext = chatHubContext;
        }

        [HttpPost]
        public async Task<IActionResult> CreateChat([FromBody] CreateChatCommand chat)
        {
            try
            {
                await _mediator.Send(chat);

                return Ok();
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<List<ChatRoom>>> GetChat([FromRoute] long userId)
        {
            //User.Identity.Name

            try
            {
                var res = await _mediator.Send(new GetChatsByUserIdQuery(UserId: userId));
                await _chatHubContext.Clients.All.GetChats(res);
                return Ok(res);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
    }
}
