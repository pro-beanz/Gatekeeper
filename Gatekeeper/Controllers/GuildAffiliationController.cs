using Gatekeeper.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gatekeeper.Data.Controllers
{
    [Route("api/v1/guild")]
    [ApiController]
    public class GuildAffiliationController : ControllerBase
    {
        private readonly GuildAffiliationService _guild;

        public GuildAffiliationController(GuildAffiliationService guildAffiliationService) =>
            _guild = guildAffiliationService;

        // POST api/v1/guild/{Guild ID}/join
        [HttpPost("{guildId}/join")]
        public async Task<ActionResult<bool>> PostJoinGuild(long guildId, [FromBody] long discordId) =>
            Ok(await _guild.JoinGuild(discordId, guildId));

        // POST api/v1/guild/{Guild ID}/leave
        [HttpPost("{guildId}/leave")]
        public async Task<ActionResult> PostLeaveGuild(long guildId, [FromBody] long discordId)
        {
            await PostLeaveGuild(discordId, guildId);
            return Ok();
        }
    }
}
