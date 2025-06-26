using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PixCharge.Domain.Transactions.Aggregates;

namespace PixCharge.Repository.Mapping;
public class ChargeMap : IEntityTypeConfiguration<Charge>
{
    public void Configure(EntityTypeBuilder<Charge> builder)
    {
        builder.ToTable(nameof(Charge));
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        
        builder.OwnsOne(d => d.Value, c =>
        {
            c.Property(x => x.Value)
            .HasColumnName("Monetary")
            .IsRequired()
            .HasColumnType("decimal(18,2)");
        });
    }
}