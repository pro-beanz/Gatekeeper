using System.ComponentModel.DataAnnotations;

namespace Gatekeeper.Models
{
    public class Member
    {
        [Required]
        public long DiscordID { get; set; }

        [Required]
        public string Email { get; set; }

        public Member(long discordId, string email)
        {
            DiscordID = discordId;
            Email = email;
        }
    }
}
