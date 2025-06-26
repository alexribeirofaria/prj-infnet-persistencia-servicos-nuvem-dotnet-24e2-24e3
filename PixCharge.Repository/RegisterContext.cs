using Microsoft.EntityFrameworkCore;
using PixCharge.Domain.Account.Aggregates;
using PixCharge.Domain.Account.Agreggates;
using PixCharge.Domain.Transactions.Aggregates;
using PixCharge.Repository.Account.Mapping;
using PixCharge.Repository.Mapping;
using PixCharge.Repository.Mapping.Account;

namespace PixCharge.Repository;
public class RegisterContext: DbContext
{
    public RegisterContext(DbContextOptions<RegisterContext> options) : base(options) { }
    public DbSet<Customer> Customer { get; set; }
    public DbSet<Flat> Flat { get; set; }    
    public DbSet<User> User { get; set; }
    public DbSet<Charge> Charge{ get; set; }
    public DbSet<PIX> PIX { get; set; }
    public DbSet<Transaction> Transaction { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new CustomerMap());
        modelBuilder.ApplyConfiguration(new FlatMap());
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new ChargeMap());
        modelBuilder.ApplyConfiguration(new PIXMap());
        modelBuilder.ApplyConfiguration(new TransactionMap());        
    }
}
