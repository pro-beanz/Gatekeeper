using Gatekeeper.Data;
using Gatekeeper.Models;
using Gatekeeper.Utility;
using Microsoft.EntityFrameworkCore;

namespace Gatekeeper.Services
{
    public class RepositoryService
    {
        private readonly MemberDbContext _db;
        public RepositoryService(MemberDbContext db)
        {
            _db = db;
        }

        public async Task<AuthCode?> GetAuthCodeAsync(long discordId) =>
            await _db.AuthCodes.FindAsync(discordId);

        public async Task<List<AuthCode>> GetExpiredAuthCodesAsync() =>
            (await _db.AuthCodes.ToListAsync())
                .Where(x => !Helper.YoungerThan(x.Created, AuthService.VALID_TIME))
                .ToList();
        
        public async Task SaveAuthCodeAsync(AuthCode authCode)
        {
            _db.AuthCodes.Add(authCode);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAuthCodeRangeAsync(List<AuthCode> authCodes)
        {
            _db.AuthCodes.RemoveRange(authCodes);
            await _db.SaveChangesAsync();
        }

        public async Task<Member?> GetMemberAsync(long discordId) =>
            await _db.Members.FindAsync(discordId);

        public async Task AddMemberAsync(Member member)
        {
            _db.Members.Add(member);
            await _db.SaveChangesAsync();
        }

        public async Task<GuildAffiliation?> GetGuildAffiliationAsync(long discordId, long guildId) =>
            await _db.GuildAffiliations.FindAsync(discordId, guildId);

        public async Task AddGuildAffiliation(GuildAffiliation guildAffiliation)
        {
            _db.GuildAffiliations.Add(guildAffiliation);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateGuildAffiliation(GuildAffiliation updated)
        {
            _db.GuildAffiliations.Update(updated);
            await _db.SaveChangesAsync();
        }
    }
}
