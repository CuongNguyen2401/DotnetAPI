using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApiNetCore8.Model;
using MyApiNetCore8.Repositories;

namespace MyApiNetCore8.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {

            private readonly IAccountRepository accountRepository;

            public AccountsController(IAccountRepository accountRepository)
            {
                this.accountRepository = accountRepository;
            }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(SignUpModel model)
        {
            var result = await accountRepository.SignUpAsync(model);
            if (result.Succeeded)
            {
                return Ok();
            }
            // Log the error or return it in the response
            return BadRequest(result.Errors);
        }

        [HttpPost("signin")]
            public async Task<IActionResult> SignIn(SignInModel model)
            {
                var token = await accountRepository.SignInAsync(model);
                if (string.IsNullOrEmpty(token))
                {
                    return Unauthorized();
                }
                return Ok( token );
            }
        }
    
}
