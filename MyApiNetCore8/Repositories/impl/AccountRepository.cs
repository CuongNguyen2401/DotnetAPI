using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MyApiNetCore8.DTO.Request;
using MyApiNetCore8.Helper;
using MyApiNetCore8.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MyApiNetCore8.Repositories.impl
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IConfiguration configuration;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountRepository(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IConfiguration configuration,
            RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.roleManager = roleManager;
        }

        public async Task<(string, string)> SignInAsync(SignInModel model)
        {
            var user = await userManager.FindByNameAsync(model.userName);
            if (user == null || !await userManager.CheckPasswordAsync(user, model.password))
            {
                return (string.Empty, string.Empty);
            }

            var result = await signInManager.PasswordSignInAsync(model.userName, model.password, false, false);
            if (!result.Succeeded)
            {
                return (string.Empty, string.Empty);
            }

            var token = await GenerateTokenAsync(user, model.userName);
            var refreshToken = GenerateRefreshToken();
            await StoreRefreshTokenAsync(user, refreshToken);

            return (token, refreshToken);
        }

        public async Task<IdentityResult> SignUpAsync(SignUpModel model)
        {
            var user = new User
            {
                first_name = model.firstName,
                last_name = model.lastName,
                Email = model.email,
                UserName = model.userName,
                date_of_birth = model.Dob.Date
            };

            var result = await userManager.CreateAsync(user, model.password);
            if (result.Succeeded)
            {
                if (!await roleManager.RoleExistsAsync(AppRole.User))
                {
                    await roleManager.CreateAsync(new IdentityRole(AppRole.User));
                }
                await userManager.AddToRoleAsync(user, AppRole.User);
            }
            return result;
        }

        public async Task<(string, string)> RefreshTokenAsync(string token, string refreshToken)
        {
            var principal = GetPrincipalFromExpiredToken(token);
            if (principal == null)
            {
                return (string.Empty, string.Empty);
            }

            var userName = principal.Identity.Name;
            var user = await userManager.FindByNameAsync(userName);

            if (user == null || user.RefreshToken != refreshToken || user.TokenExpires <= DateTime.Now)
            {
                return (string.Empty, string.Empty);
            }

            var newToken = await GenerateTokenAsync(user, userName);
            var newRefreshToken = GenerateRefreshToken();
            await StoreRefreshTokenAsync(user, newRefreshToken);

            return (newToken, newRefreshToken);
        }

        private async Task<string> GenerateTokenAsync(User user, string userName)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var userRoles = await userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole.ToString()));
            }

            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddSeconds(10),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha512Signature)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private async Task StoreRefreshTokenAsync(User user, string refreshToken)
        {
            user.RefreshToken = refreshToken;
            user.TokenCreated = DateTime.Now;
            user.TokenExpires = DateTime.Now.AddDays(7);
            await userManager.UpdateAsync(user);
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"])),
                ValidateLifetime = true // Here we are saying that we don't care about the token's expiration date
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);


            if (!(securityToken is JwtSecurityToken jwtSecurityToken) || jwtSecurityToken.ValidTo <= DateTime.UtcNow)
            {
                throw new SecurityTokenExpiredException("Token has expired");
            }

            // Validate token signature
            if (!jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512Signature, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }
    }
}
