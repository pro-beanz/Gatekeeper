using Gatekeeper.Models;

namespace Gatekeeper.Services
{
    public class MemberService
    {
        private readonly RepositoryService _repo;
        private readonly GuildAffiliationService _guild;

        public MemberService(RepositoryService repositoryService, GuildAffiliationService guildAffiliationService)
        {
            _repo = repositoryService;
            _guild = guildAffiliationService;
        }

        public async Task<bool> ExistsAsync(long discordId) =>
            await _repo.GetMemberAsync(discordId) != null;

        public async Task<Member?> GetMemberAsync(long discordId, long guildId) =>
            (await _guild.IsMemberInGuildAsync(discordId, guildId)) ? await _repo.GetMemberAsync(discordId) : null;

        public async Task AddMemberAsync(long discordId, string email) =>
            await _repo.AddMemberAsync(new Member(discordId, email));
    }
}
