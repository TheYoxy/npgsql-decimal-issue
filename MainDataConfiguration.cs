using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Issue;

public sealed class MainDataConfiguration : IEntityTypeConfiguration<MainData>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<MainData> builder)
    {
        builder.HasKey(x => x.Id);
        builder.OwnsMany(x => x.DependentData, b =>
        {
            b.WithOwner(d => d.MainData);
            b.Property(d => d.Amount).HasPrecision(22, 2);
        });
    }
}