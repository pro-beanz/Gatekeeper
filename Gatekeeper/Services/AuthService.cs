using Gatekeeper.Models;
using Gatekeeper.Utility;

namespace Gatekeeper.Services
{
    public class AuthService
    {
        private readonly RepositoryService _repo;
        private readonly MemberService _member;
        private readonly GuildAffiliationService _guild;

        public static readonly TimeSpan RESEND_TIME = TimeSpan.FromMinutes(1);
        public static readonly TimeSpan VALID_TIME = TimeSpan.FromMinutes(5);

        public AuthService(RepositoryService repositoryService, MemberService memberService, GuildAffiliationService guildAffiliationService)
        {
            _repo = repositoryService;
            _member = memberService;
            _guild = guildAffiliationService;
        }

        public async Task<bool> GenerateAuthCodeAsync(long discordId, string email)
        {
            var old = await _repo.GetAuthCodeAsync(discordId);
            if (old != null)
            {
                if (Helper.YoungerThan(old.Created, RESEND_TIME))
                    return false;
                await _repo.DeleteAuthCodeRangeAsync(new List<AuthCode>(new AuthCode[] { old }));
            }
            
            var code = new AuthCode(discordId, email);
            await _repo.SaveAuthCodeAsync(code);

            //TODO: Send a templated email with the auth code.
            Console.WriteLine($"Send email to ${email} with code ${code.Code}");

            return true;
        }

        public async Task<bool> VerifyAuthCodeAsync(long discordId, long guildId, int code)
        {
            if (await _member.ExistsAsync(discordId))
            {
                await _guild.AddGuildAffiliation(discordId, guildId);
                return true;
            }

            var authCode = await _repo.GetAuthCodeAsync(discordId);
            var verified = authCode != null && Helper.YoungerThan(authCode.Created, VALID_TIME) && authCode!.Code == code;

            if (verified)
                await Task.WhenAll(
                    _member.AddMemberAsync(discordId, authCode!.Email),
                    _guild.AddGuildAffiliation(discordId, guildId));

            return verified;
        }
    }
}
