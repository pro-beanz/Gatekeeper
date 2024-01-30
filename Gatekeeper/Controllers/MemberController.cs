using Gatekeeper.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gatekeeper.Data.Controllers
{
    [Route("api/v1/member")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly MemberService _member;

        public MemberController(MemberService memberService) =>
            _member = memberService;

        // GET api/v1/member/{Guild ID}/{Discord ID}
        [HttpGet("{guildId}/{discordId}")]
        public async Task<ActionResult<string>> GetEmail(long guildId, long discordId)
        {
            var result = await _member.GetMemberAsync(discordId, guildId);
            return result == null ? NotFound() : Ok(result.Email);
        }
    }
}
