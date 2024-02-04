using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Requests;
using Models.Responses;
using Services.Authentication.Commands.Login;
using Services.Authentication.Commands.Registration;
using Services.Exceptions;
using System.Security.Claims;

namespace Authentication.Controllers
{
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
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
                var res = await _mediator.Send(new LoginCommand(
                    Email: loginRequest.Email,
                    Password: loginRequest.Password
                ));

                return Ok(res);
            }
            catch (ValidationException ex)
            {
                return StatusCode(422, ex.FieldErrorValidation);
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
                await _mediator.Send(new RegistrationCommand(
                    Email: registrationRequest.Email,
                    Nickname: registrationRequest.Nickname,
                    Password: registrationRequest.Password
                ));

                return Ok();
            }
            catch(ValidationException ex)
            {
                return StatusCode(422, ex.FieldErrorValidation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("check-auth")]
        public async Task<IActionResult> CheckAuth()
        {
            return Ok();
        }

        [Authorize]
        [HttpGet("account-data")]
        public async Task<IActionResult> GetAccountData()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            var name = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var accountId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            return Ok(new AccountDataResponse(
                Id: long.Parse(accountId),
                UserId: userId.Length > 0? long.Parse(userId) : null,
                Email: email,
                Nickname: name
            ));
        }
    }
}
