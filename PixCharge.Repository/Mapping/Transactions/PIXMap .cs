using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PixCharge.Domain.Transactions.Aggregates;

namespace PixCharge.Repository.Mapping;
public class PIXMap : IEntityTypeConfiguration<PIX>
{
    public void Configure(EntityTypeBuilder<PIX> builder)
    {
        builder.ToTable(nameof(PIX));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Date).IsRequired().ValueGeneratedOnAdd();

        builder.OwnsOne(x => x.QrCode, qr =>
        {
            qr.Property(x => x.Url).HasColumnName("Url").IsRequired();
            qr.Property(x => x.BrCode).HasColumnName("BrCode").IsRequired();
        });

        builder.OwnsOne(d => d.Value, c =>
        {
            c.Property(x => x.Value)
            .HasColumnName("Monetary")
            .IsRequired()
            .HasColumnType("decimal(18,2)");
        });


        builder.HasMany(p => p.Transactions);
    }
}