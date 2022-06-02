using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NicoKingdomManager.DataAccessLayer.Configurations.Common;
using NicoKingdomManager.DataAccessLayer.Entities;

namespace NicoKingdomManager.DataAccessLayer.Configurations;

internal class ServerLogConfiguration : BaseEntityConfiguration<ServerLog>
{
    protected override void Configuration(EntityTypeBuilder<ServerLog> builder)
    {
        base.Configuration(builder);

        builder.ToTable("ServerLogs");

        builder.Property(log => log.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(log => log.Action)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(log => log.Description)
            .HasMaxLength(512)
            .IsRequired();

        builder.Property(log => log.Reason)
            .HasMaxLength(512)
            .IsRequired(false);

        builder.Property(log => log.LogDate)
            .IsRequired();

        builder.Property(log => log.LogType)
            .HasConversion<string>()
            .HasMaxLength(25)
            .IsRequired();

        builder.HasOne(log => log.User)
            .WithMany(user => user.ServerLogs)
            .HasForeignKey(log => log.UserId)
            .IsRequired();
    }
}