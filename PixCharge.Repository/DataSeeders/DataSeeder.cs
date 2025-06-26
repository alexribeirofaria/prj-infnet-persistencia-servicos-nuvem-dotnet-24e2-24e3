namespace PixCharge.Repository.DataSeeders;
public class DataSeeder : IDataSeeder
{
    private readonly RegisterContext _context;
    public DataSeeder(RegisterContext context)
    {
        _context = context;
    }
    public void SeedData()
    {
        try
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            new DataSeederFlat(_context).SeedData();
            new DataSeederCustomer(_context).SeedData();
        }
        catch
        {
            throw;
        }
    }
}
