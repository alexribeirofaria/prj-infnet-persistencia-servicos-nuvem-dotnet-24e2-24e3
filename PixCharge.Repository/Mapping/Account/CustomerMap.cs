using PixCharge.Domain.Account.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PixCharge.Repository.Account.Mapping;
public class CustomerMap : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable(nameof(Customer));

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Birth).IsRequired();
        builder.Property(x => x.CPF).IsRequired().HasMaxLength(14);

        builder.OwnsOne(e => e.Phone, c =>
        {
            c.Property(x => x.Number).HasColumnName("Phone").HasMaxLength(50).IsRequired();

        });
        
        builder.HasOne(x => x.Address);
        builder.HasMany(x => x.Charges).WithOne(c => c.Customer).OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(x => x.Transactions).WithOne(x => x.Customer).OnDelete(DeleteBehavior.NoAction);
    }
}