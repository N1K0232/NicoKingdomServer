using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NicoKingdomManager.DataAccessLayer.Entities.Common;

namespace NicoKingdomManager.DataAccessLayer.Configurations.Common;

internal abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
{
    public void Configure(EntityTypeBuilder<T> builder) => Configuration(builder);
    protected virtual void Configuration(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(x => x.CreatedDate)
            .IsRequired();

        builder.Property(x => x.LastModifiedDate)
            .IsRequired(false);
    }
}