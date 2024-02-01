using Gatekeeper.Models;
using Gatekeeper.Utility;

namespace Gatekeeper.Services
{
    public class GuildAffiliationService
    {
        private readonly RepositoryService _repo;

        public static readonly TimeSpan VALID_LEAVE_TIMEOUT = TimeSpan.FromDays(2);

        public GuildAffiliationService(RepositoryService repositoryService)
        {
            _repo = repositoryService;
        }

        public async Task AddGuildAffiliation(long discordId, long guildId) =>
            await _repo.AddGuildAffiliation(new GuildAffiliation(discordId, guildId));

        public async Task<bool> IsMemberInGuildAsync(long discordId, long guildId)
        {
            var affiliation = await _repo.GetGuildAffiliationAsync(discordId, guildId);
            return affiliation != null && (affiliation.LeaveTime == null
                || Helper.YoungerThan((DateTimeOffset) affiliation.LeaveTime, VALID_LEAVE_TIMEOUT));

        }

        public async Task JoinGuild(long discordId, long guildId)
        {
            var affiliation = await _repo.GetGuildAffiliationAsync(discordId, guildId);
            if (affiliation == null)
                await AddGuildAffiliation(discordId, guildId);
            else
            {
                affiliation.JoinTime = DateTimeOffset.Now;
                affiliation.LeaveTime = null;
                await _repo.UpdateGuildAffiliation(affiliation);
            }
        }

        public async Task LeaveGuild(long discordId, long guildId)
        {
            var affiliation = await _repo.GetGuildAffiliationAsync(discordId, guildId);
            affiliation!.LeaveTime = DateTimeOffset.Now;
            await _repo.UpdateGuildAffiliation(affiliation);
        }
    }
}
