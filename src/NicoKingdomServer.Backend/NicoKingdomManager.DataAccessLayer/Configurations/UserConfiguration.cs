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

        builder.Property(user => user.UserName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(user => user.NickName)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasOne(user => user.Role)
            .WithMany(role => role.Users)
            .HasForeignKey(user => user.RoleId)
            .IsRequired();
    }
}