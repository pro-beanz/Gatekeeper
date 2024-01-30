using System.ComponentModel.DataAnnotations;

namespace Gatekeeper.Models
{
    public class AuthCode
    {
        [Required]
        public long DiscordID { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public int Code { get; set; }

        [Required]
        public DateTimeOffset Created { get; set; }

        public AuthCode(long discordID, string email)
        {
            DiscordID = discordID;
            Email = email;
            Code = new Random().Next(10^6 - 1);
            Created = DateTimeOffset.Now;
        }
    }
}
