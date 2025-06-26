using PixCharge.Domain.Account.Aggregates;

namespace PixCharge.Repository.DataSeeders;
public class DataSeederFlat : IDataSeeder
{
    private readonly RegisterContext _context;
    public DataSeederFlat(RegisterContext context)
    {
        _context = context;
    }
    public void SeedData()
    {
        try
        {
            _context?.Flat?.AddRange(
            new Flat
            {
                Id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa5"),
                Description = "Free Flat Desciption",
                Value = 0,
                Active = true,
                DtActivation = DateTime.Now
            },
            new Flat
            {
                Id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                Description = "Basic Flat Desciption",
                Value = 250m,
                Active = true,
                DtActivation = DateTime.Now
            },
            new Flat
            {
                Id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa7"),
                Description = "Standard  Flat Desciption",
                Value = 500m,
                Active = true,
                DtActivation = DateTime.Now
            },
            new Flat
            {
                Id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa8"),
                Description = "Premium  Flat Desciption",
                Value = 750m,
                Active = false,
                DtActivation = DateTime.Now
            });

            _context?.SaveChanges();
        }
        catch
        {
            throw;
        }
    }
}