using Gatekeeper.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gatekeeper.Data.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _auth;

        public AuthController(AuthService authService) =>
            _auth = authService;

        // GET api/v1/auth/{Discord ID}
        [HttpGet("{discordId}")]
        public async Task<ActionResult> GetNewAuthCode(long discordId, [FromBody] string email) =>
            await _auth.GenerateAuthCodeAsync(discordId, email) ? Ok() : BadRequest();

        // POST api/v1/auth/{Discord ID}
        [HttpPost("{discordId}")]
        public async Task<ActionResult<bool>> PostAuthCodeVerify(long discordId, [FromBody] long guild, [FromBody] int code) =>
            Ok(await _auth.VerifyAuthCodeAsync(discordId, guild, code));
    }
}
