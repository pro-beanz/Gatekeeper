using System.ComponentModel.DataAnnotations;

namespace Gatekeeper.Models
{
    public class Member
    {
        [Required]
        public long DiscordID { get; private set; }

        [Required]
        public string Email { get; private set; }

        public Member(long discordId, string email)
        {
            DiscordID = discordId;
            Email = email;
        }

#pragma warning disable CS8618
        private Member() { }
#pragma warning restore CS8618
    }
}
