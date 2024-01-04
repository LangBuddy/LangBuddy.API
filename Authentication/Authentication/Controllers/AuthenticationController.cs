using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Requests;
using Services.Authentication.Commands.Login;
using Services.Authentication.Commands.Registration;
using System.Security.Claims;
using System.Security.Principal;

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
                await _mediator.Send(new LoginCommand(
                    Email: loginRequest.Email,
                    Password: loginRequest.Password
                ));

                return Ok();
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
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }
    }
}
