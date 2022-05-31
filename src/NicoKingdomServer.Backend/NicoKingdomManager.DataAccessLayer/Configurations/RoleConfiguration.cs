using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NicoKingdomManager.DataAccessLayer.Configurations.Common;
using NicoKingdomManager.DataAccessLayer.Entities;

namespace NicoKingdomManager.DataAccessLayer.Configurations;

internal class RoleConfiguration : BaseEntityConfiguration<Role>
{
    protected override void Configuration(EntityTypeBuilder<Role> builder)
    {
        base.Configuration(builder);

        builder.ToTable("Roles");

        builder.Property(r => r.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(r => r.Color)
            .HasMaxLength(10)
            .HasDefaultValue("Gray")
            .IsRequired();
    }
}