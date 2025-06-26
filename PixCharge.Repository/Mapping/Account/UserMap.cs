using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PixCharge.Domain.Account.Agreggates;
using PixCharge.Domain.Account.ValueObject;

namespace PixCharge.Repository.Account.Mapping;
public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User"); 
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).ValueGeneratedOnAdd();
        builder.Property(u => u.DtCreated).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();                

        builder.OwnsOne<Login>(u => u.Login, l =>
        {
            l.Property(p => p.Email).HasColumnName("Email").HasMaxLength(150).IsRequired();
            l.Property(p => p.Password).HasColumnName("Password").HasMaxLength(255).IsRequired();
            l.WithOwner();
        });
    }
}