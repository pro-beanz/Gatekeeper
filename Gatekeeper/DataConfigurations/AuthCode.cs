﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gatekeeper.DataConfigurations
{
    internal class AuthCode : IEntityTypeConfiguration<Models.AuthCode>
    {
        public void Configure(EntityTypeBuilder<Models.AuthCode> builder) =>
            builder.HasKey(x => x.DiscordID);
    }
}
