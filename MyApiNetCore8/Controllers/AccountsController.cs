using Microsoft.AspNetCore.Mvc;
using MyApiNetCore8.DTO.Request;
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
            return BadRequest(result.Errors);
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(SignInModel model)
        {
            var (token, refreshToken) = await accountRepository.SignInAsync(model);
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }
            return Ok(new { Token = token, RefreshToken = refreshToken });
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(string token, string refreshToken)
        {
            var (newToken, newRefreshToken) = await accountRepository.RefreshTokenAsync(token, refreshToken);
            if (string.IsNullOrEmpty(newToken))
            {
                return Unauthorized();
            }
            return Ok(new { Token = newToken, RefreshToken = newRefreshToken });
        }
    }
}
