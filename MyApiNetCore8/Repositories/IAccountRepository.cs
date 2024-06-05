using Microsoft.AspNetCore.Identity;
using MyApiNetCore8.DTO.Request;

namespace MyApiNetCore8.Repositories
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUpAsync(SignUpModel model);
        Task<(string, string)> SignInAsync(SignInModel model);
        Task<(string, string)> RefreshTokenAsync(string token, string refreshToken); 

    }
}
