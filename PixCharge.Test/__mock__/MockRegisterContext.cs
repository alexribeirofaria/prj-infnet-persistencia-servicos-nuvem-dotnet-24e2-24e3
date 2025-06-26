using PixCharge.Domain.Account.Agreggates;

namespace __mock__;
public class MockRegisterContext : DbContext
{
    public MockRegisterContext(DbContextOptions<MockRegisterContext> options) : base(options) {}
    public DbSet<Customer>? Custumer{ get; set; }
    public DbSet<Customer>? Customer { get; set; }
    public DbSet<Flat>? Flat { get; set; }
    public DbSet<User>? User { get; set; }
    public DbSet<Charge>? Charge { get; set; }
    public DbSet<PIX>? PIX { get; set; }
    public DbSet<Transaction>? Transaction { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}