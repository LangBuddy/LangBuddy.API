using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Requests;
using Service.Account.Commands.CreateUser;
using Service.Authentication.Login;
using Service.Authentication.Registration;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController: ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                var res = await _mediator.Send(new LoginCommand(loginRequest.Email, loginRequest.Password));
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("registration")]
        public async Task<IActionResult> Registration([FromBody] RegistrationRequest registrationRequest)
        {
            try
            {
                await _mediator.Send(new RegistrationCommand(registrationRequest.Email, registrationRequest.Nickname, registrationRequest.Password));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("check-auth")]
        public async Task<IActionResult> CheckAuth()
        {
            return Ok();
        }

        [HttpPost("personal-information")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest createUserRequest)
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
