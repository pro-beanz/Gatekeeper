using Gatekeeper.Data;
using Gatekeeper.Models;
using Gatekeeper.Utility;
using Microsoft.EntityFrameworkCore;

namespace Gatekeeper.Services
{
    public class RepositoryService
    {
        private readonly IDbContextFactory<MemberDbContext> _dbContextFactory;

        public RepositoryService(IDbContextFactory<MemberDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<AuthCode?> GetAuthCodeAsync(long discordId)
        {
            using (var db = _dbContextFactory.CreateDbContext())
            {
                return await db.AuthCodes.FindAsync(discordId);
            }
        }

        public async Task<List<AuthCode>> GetExpiredAuthCodesAsync()
        {
            using (var db = _dbContextFactory.CreateDbContext())
            {
                return await db.AuthCodes
                        .Where(x => DateTime.UtcNow - x.Created >= AuthService.VALID_TIME)
                        .ToListAsync();
            }
        }
        
        public async Task SaveAuthCodeAsync(AuthCode authCode)
        {
            using (var db = _dbContextFactory.CreateDbContext())
            {
                db.AuthCodes.Add(authCode);
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteAuthCodeRangeAsync(List<AuthCode> authCodes)
        {
            using (var db = _dbContextFactory.CreateDbContext())
            {
                db.AuthCodes.RemoveRange(authCodes);
                await db.SaveChangesAsync();
            }
        }

        public async Task<Member?> GetMemberAsync(long discordId)
        {
            using (var db = _dbContextFactory.CreateDbContext())
            {
                return await db.Members.FindAsync(discordId);
            }
        }

        public async Task AddMemberAsync(Member member)
        {
            using (var db = _dbContextFactory.CreateDbContext())
            {
                db.Members.Add(member);
                await db.SaveChangesAsync();
            }
        }

        public async Task<GuildAffiliation?> GetGuildAffiliationAsync(long discordId, long guildId)
        {
            using (var db = _dbContextFactory.CreateDbContext())
            {
                return await db.GuildAffiliations.FindAsync(discordId, guildId);
            }
        }

        public async Task AddGuildAffiliation(GuildAffiliation guildAffiliation)
        {
            using (var db = _dbContextFactory.CreateDbContext())
            {
                db.GuildAffiliations.Add(guildAffiliation);
                await db.SaveChangesAsync();
            }
        }

        public async Task UpdateGuildAffiliation(GuildAffiliation updated)
        {
            using (var db = _dbContextFactory.CreateDbContext())
            {
                db.GuildAffiliations.Update(updated);
                await db.SaveChangesAsync();
            }
        }
    }
}
