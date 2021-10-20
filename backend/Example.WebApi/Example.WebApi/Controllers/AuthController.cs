using Example.WebApi.Contract;
using Example.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Example.WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AuthController(IUserService userService, IAuthService authService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost, Route("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginContract user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                return BadRequest("Missing user credentials");
            }

            var token = await _authService.Login(user, cancellationToken);

            return token != null
                ? Ok(token)
                : Unauthorized();
        }
    }
}
