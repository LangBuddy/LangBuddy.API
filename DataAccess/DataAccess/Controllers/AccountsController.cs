using MediatR;
using Microsoft.AspNetCore.Mvc;
using Models.Requests;
using Services.Accounts.Commands.CreateAccount;
using Services.Accounts.Commands.DeleteAccount;
using Services.Accounts.Commands.UpdateAccount;
using Services.Accounts.Queries.GetAccount;
using Services.Accounts.Queries.GetAccounts;

namespace Web.Controllers
{
    [Route("[controller]")]
    public class AccountsController: ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAccount()
        {
            try
            {
                var res = await _mediator.Send(new GetAccountsQuery());
                return Ok(res);
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetOneAccount([FromRoute] string email)
        {
            try
            {
                var res = await _mediator.Send(new GetAccountQuery(email));
                return Ok(res);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] AccountCreateRequest accountCreateRequest)
        {
            try
            {
                await _mediator.Send(new CreateAccountCommand(
                    Email: accountCreateRequest.Email,
                    Nickname: accountCreateRequest.Nickname,
                    PasswordHash: accountCreateRequest.PasswordHash)
                );
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{email}")]
        public async Task<IActionResult> UpdateAccount([FromRoute] string email, 
            [FromBody] AccountUpdateRequest accountUpdateRequest)
        {
            try
            {
                await _mediator.Send(new UpdateAccountCommand(
                    CurrentEmail: email,
                    Email: accountUpdateRequest.Email,
                    Nickname: accountUpdateRequest.Nickname,
                    PasswordHash: accountUpdateRequest.PasswordHash)
                );

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{email}")]
        public async Task<IActionResult> DeleteAccount([FromRoute] string email)
        {
            try
            {
                await _mediator.Send(new DeleteAccountCommand(email));
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
