using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NicoKingdomManager.DataAccessLayer.Configurations.Common;
using NicoKingdomManager.DataAccessLayer.Entities;

namespace NicoKingdomManager.DataAccessLayer.Configurations;

internal class UserConfiguration : BaseEntityConfiguration<User>
{
    protected override void Configuration(EntityTypeBuilder<User> builder)
    {
        base.Configuration(builder);

        builder.ToTable("Users");

        builder.Property(u => u.UserName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(u => u.NickName)
            .HasMaxLength(100)
            .IsRequired();
    }
}