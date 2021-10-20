using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Example.WebApi.Config;
using Example.WebApi.Contract;
using Microsoft.IdentityModel.Tokens;

namespace Example.WebApi.Services
{
    public interface IAuthService
    {
        /// <returns>Token string on successful login or null on invalid attempt.</returns>
        Task<string> Login(LoginContract user, CancellationToken cancellationToken);
    }

    internal class AuthService : IAuthService
    {
        private readonly IExampleConfig _exampleConfig;
        private readonly IUserService _userService;

        public AuthService(IUserService userService, IExampleConfig exampleConfig)
        {
            _userService = userService;
            _exampleConfig = exampleConfig;
        }

        public async Task<string> Login(LoginContract user, CancellationToken cancellationToken)
        {
            if (await _userService.VerifyLogin(user, cancellationToken))
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_exampleConfig.JwtSigningKey));
                var signInCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


                var tokenOptions = new JwtSecurityToken(
                    issuer: "https://localhost:5001",
                    audience: "https://localhost:5001",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: signInCredentials);

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return tokenString;
            }
            else
            {
                return null;
            }
        }
    }
}