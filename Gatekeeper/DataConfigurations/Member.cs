using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gatekeeper.Data.DataConfigurations
{
    internal class Member : IEntityTypeConfiguration<Models.Member>
    {
        public void Configure(EntityTypeBuilder<Models.Member> builder) =>
            builder.HasKey(x => x.DiscordID);
    }
}
