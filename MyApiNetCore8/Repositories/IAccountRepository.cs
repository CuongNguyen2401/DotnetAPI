using Microsoft.AspNetCore.Identity;
using MyApiNetCore8.Model;

namespace MyApiNetCore8.Repositories
{
    public interface IAccountRepository
    {
        public Task<IdentityResult> SignUpAsync(SignUpModel model);
        public Task<string> SignInAsync(SignInModel model);
    }
}
