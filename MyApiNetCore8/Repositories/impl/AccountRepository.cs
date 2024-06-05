using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyApiNetCore8.Data;
using MyApiNetCore8.DTO.Request;
using MyApiNetCore8.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MyApiNetCore8.Repositories.impl
{
    public class AccountRepository : IAccountRepository
    {
        private readonly MyContext context;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IConfiguration configuration;


        public AccountRepository(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IConfiguration configuration,
            MyContext context)
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
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
                // Add user to roles, if any
            }
            return result;
        }

        public async Task<(string, string)> RefreshTokenAsync(string token, string refreshToken)
        {
            // Validate the principal extracted from the token
            var principal = GetPrincipalFromExpiredToken(token);
            if (principal == null)
            {
                return (string.Empty, string.Empty);
            }

            // Extract the username from the principal
            var userName = principal.Identity.Name;
            if (string.IsNullOrEmpty(userName))
            {
                return (string.Empty, string.Empty);
            }

            // Find the user by username
            var user = await userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return (string.Empty, string.Empty);
            }
            var storedRefreshToken = context.RefreshTokens.FirstOrDefault(rt => rt.Token.Equals(refreshToken));
            // Retrieve the stored refresh token
            //var storedRefreshToken = user.RefreshTokens.FirstOrDefault(rt => rt.Token.Equals(refreshToken) );
            if (storedRefreshToken == null || storedRefreshToken.Expires <= DateTime.Now)
            {
                return (string.Empty, string.Empty);
            }

            // Generate a new access token
            var newToken = await GenerateTokenAsync(user, userName);
            if (string.IsNullOrEmpty(newToken))
            {
                return (string.Empty, string.Empty);
            }

            // Generate a new refresh token
            var newRefreshToken = GenerateRefreshToken();
            if (string.IsNullOrEmpty(newRefreshToken))
            {
                return (string.Empty, string.Empty);
            }

            // Store the new refresh token for the user
            await StoreRefreshTokenAsync(user, newRefreshToken);

            // Return the new access token and refresh token
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
            var refreshTokenEntity = new RefreshToken
            {
                Token = refreshToken,
                Expires = DateTime.Now.AddDays(7)
            };

            user.RefreshTokens.Add(refreshTokenEntity);
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
                ValidateLifetime = true
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

            if (securityToken is JwtSecurityToken jwtSecurityToken &&
                jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512Signature, StringComparison.InvariantCultureIgnoreCase))
            {
                return principal;
            }

            throw new SecurityTokenException("Invalid token");
        }
    }
}
