using MediatR;
using Microsoft.AspNetCore.Mvc;
using Models.Requests;
using Service.Account.Commands.CreateUser;
using Service.Chats.Commands.CreateChat;
using Service.Chats.Queries.GetChats;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/private/[controller]")]
    public class PersonalInformationController: ControllerBase
    {
        private readonly IMediator _mediator;

        public PersonalInformationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task <IActionResult> CreateUser([FromBody] CreateUserRequest createUserRequest)
        {
            try
            {
                if (HttpContext.Items.TryGetValue("Id", out object id))
                {
                    // Используем данные пользователя
                    var idValue = long.Parse(id.ToString());

                    await _mediator.Send(new CreateUserCommand(
                        AccountId: idValue,
                        FirstName: createUserRequest.FirstName,
                        LastName: createUserRequest.LastName,
                        Gender: createUserRequest.Gender,
                        BirthDate: createUserRequest.BirthDate
                    ));

                    return Ok();
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
