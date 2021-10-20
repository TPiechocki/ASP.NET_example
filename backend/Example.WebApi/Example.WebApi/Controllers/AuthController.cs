using Example.WebApi.Contract;
using Example.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
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

        public AuthController(IAuthService authService)
        {
            _authService = authService;
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
