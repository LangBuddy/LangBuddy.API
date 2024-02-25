using Database.Entity;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Models.Requests;
using Service.Chats.Commands.CreateMessage;
using Service.Chats.Commands.DeleteMessage;
using Service.Chats.Commands.UpdateMessage;
using Service.Chats.Queries.GetMessagesByChatId;

namespace Web.Controllers
{
    [Route("[controller]")]
    public class MessagesController: ControllerBase
    {
        private readonly IMediator _mediator;
        public MessagesController(IMediator mediator)
        {
            _mediator = mediator;
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

        [HttpPatch("{mesageId}")]
        public async Task<IActionResult> UpdateMessage([FromRoute] long mesageId,
            [FromBody] UpdateMessageRequest updateMessageRequest)
        {
            try
            {
                await _mediator.Send(new UpdateMessageCommand(mesageId, updateMessageRequest.Value));

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{mesageId}")]
        public async Task<IActionResult> DeleteMessage([FromRoute] long mesageId)
        {
            try
            {
                await _mediator.Send(new DeleteMessageCommand(mesageId));

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

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
