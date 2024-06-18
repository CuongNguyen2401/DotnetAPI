using CloudinaryDotNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using MyApiNetCore8.DTO.Request;
using MyApiNetCore8.DTO.Response;
using MyApiNetCore8.Repositories;

namespace MyApiNetCore8.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            this._accountService = accountService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(SignUpModel model)
        {
            var result = await _accountService.SignUpAsync(model);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("token")]
        public async Task<IActionResult> SignIn(SignInModel model)
        {
            var (token, refreshToken) = await _accountService.SignInAsync(model);
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }
            return Ok(new { Token = token, RefreshToken = refreshToken });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken(string token, string refreshToken)
        {
            var (newToken, newRefreshToken) = await _accountService.RefreshTokenAsync(token, refreshToken);
            if (string.IsNullOrEmpty(newToken))
            {
                return Unauthorized();
            }
            return Ok(new { Token = newToken, RefreshToken = newRefreshToken });
        }
        
        
        [HttpGet("me")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<AccountResponse>>> GetMyInfo()
        {
            var account = await _accountService.GetMyInfoAsync();
            if (account == null)
            {
                return NotFound();
            }
            return Ok(new ApiResponse<AccountResponse>(1000, "Success", account));
        }
    }
}
