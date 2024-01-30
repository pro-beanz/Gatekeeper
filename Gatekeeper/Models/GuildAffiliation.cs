using System.ComponentModel.DataAnnotations;

namespace Gatekeeper.Models
{
    public class GuildAffiliation
    {
        [Required]
        public long DiscordID { get; }

        [Required]
        public long GuildID { get; }

        [Required]
        public DateTimeOffset JoinTime { get; set; }
        public DateTimeOffset? LeaveTime { get; set; }

        public GuildAffiliation(long discordID, long guildID)
        {
            DiscordID = discordID;
            GuildID = guildID;
            JoinTime = DateTime.Now;
        }
    }
}
