using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gatekeeper.DataConfigurations
{
    internal class GuildAffiliation : IEntityTypeConfiguration<Models.GuildAffiliation>
    {
        public void Configure(EntityTypeBuilder<Models.GuildAffiliation> builder) =>
            builder.HasKey(x => new { x.DiscordID, x.GuildID });
    }
}
