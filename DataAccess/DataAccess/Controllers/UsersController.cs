using MediatR;
using Microsoft.AspNetCore.Mvc;
using Models.Requests;
using Services.Users.Commands.CreateUser;
using Services.Users.Commands.DeleteUser;
using Services.Users.Commands.UpdateUser;
using Services.Users.Queries.GetUserById;
using Services.Users.Queries.GetUsers;

namespace Web.Controllers
{
    [Route("[controller]")]
    public class UsersController: ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var res = await _mediator.Send(new GetUsersQuery());
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] long id)
        {
            try
            {
                var res = await _mediator.Send(new GetUserByIdQuery(id));
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateRequest userCreateRequest)
        {
            try
            {
                await _mediator.Send(new CreateUserCommand(
                    AccountId: userCreateRequest.AccountId,
                    FirstName: userCreateRequest.FirstName,
                    LastName: userCreateRequest.LastName,
                    Gender: userCreateRequest.Gender,
                    BirthDate: userCreateRequest.BirthDate
                ));

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] long id, [FromBody] UserUpdateRequest userUpdateRequest)
        {
            try
            {
                await _mediator.Send(new UpdateUserCommand(
                    UserId: id,
                    FirstName: userUpdateRequest.FirstName,
                    LastName: userUpdateRequest.LastName,
                    Gender: userUpdateRequest.Gender,
                    BirthDate: userUpdateRequest.BirthDate
                ));

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] long id)
        {
            try
            {
                await _mediator.Send(new DeleteUserCommand(
                    UserId: id
                ));

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
