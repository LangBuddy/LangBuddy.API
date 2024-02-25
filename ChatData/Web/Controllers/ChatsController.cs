using MediatR;
using Microsoft.AspNetCore.Mvc;
using Models.Requests;
using Service.Chats.Commands.CreateChat;
using Service.Chats.Commands.DeleteChat;
using Service.Chats.Commands.UpdateChat;
using Service.Chats.Queries.GetChatsByUserId;

namespace Web.Controllers
{
    [Route("[controller]")]
    public class ChatsController: ControllerBase
    {
        private readonly IMediator _mediator;
        public ChatsController(IMediator mediator)
        {
            _mediator = mediator;
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

        [HttpPatch("{chatId}")]
        public async Task<IActionResult> UpdateChat([FromRoute] long chatId, [FromBody] UpdateChatRequest updateChatRequest)
        {
            try
            {
                await _mediator.Send(new UpdateChatCommand(chatId, updateChatRequest.Title, updateChatRequest.UsersId));

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

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetChats([FromRoute] long userId)
        {

            try
            {
                var res = await _mediator.Send(new GetChatsByUserIdQuery(UserId: userId));
                return Ok(res);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
    }
}
