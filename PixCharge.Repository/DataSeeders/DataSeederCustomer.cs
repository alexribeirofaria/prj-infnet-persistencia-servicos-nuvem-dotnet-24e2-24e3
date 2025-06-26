using PixCharge.Domain.Account.Aggregates;
using PixCharge.Domain.Account.Agreggates;
using PixCharge.Domain.Account.ValueObject;

namespace PixCharge.Repository.DataSeeders;

public class DataSeederCustomer : IDataSeeder
{
    private readonly RegisterContext _context;

    public DataSeederCustomer(RegisterContext context)
    {
        _context = context;
    }

    public void SeedData()
    {
        try
        {

            var customer = new Customer
            {
                Id = new Guid("A83FA3A6-2EC2-4FDA-354E-08DC4F659A39"),
                Name = "Free User Test",
                Birth = new DateTime(1990, 1, 1),
                CPF = "123.456.789-01",
                Phone = new Phone { Number = "+5521993879312" },
                User = new User()
                {
                    Login = new Login { Email = "free@user.com", Password = "12345T!" },
                },
                Address = new Address()
                {
                    Zipcode = "12345-678",
                    Street = "Free Street",
                    Number = "123",
                    Neighborhood = "Free",
                    City = "FreeCityville",
                    State = "ST",
                    Complement = "Apt 456",
                    Country = "FreeCountryland"
                }
            };           

            customer.AddFlat(_context?.Flat?.Where(f => f.Id == new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa5")).First() ?? new());
            _context?.Add(customer);

            customer = new Customer
            {
                Id = new Guid("E78AF386-E0E5-4271-354F-08DC4F659A39"),
                Name = "Basic User Test",
                Birth = new DateTime(1992, 1, 1),
                CPF = "123.456.789-02",
                Phone = new Phone { Number = "+5521993879312" },
                User = new User()
                {
                    Login = new Login { Email = "basic@user.com", Password = "12345T!" },

                },
                Address = new Address()
                {
                    Zipcode = "12345-678",
                    Street = "Basic Street",
                    Number = "123",
                    Neighborhood = "Basic",
                    City = "BasicCityville",
                    State = "ST",
                    Complement = "Apt 456",
                    Country = "BasicCountryland"
                }
            };

            customer.AddFlat(_context?.Flat?.Where(f => f.Id == new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6")).FirstOrDefault() ?? new());
            _context?.Add(customer);
            
            customer = new Customer
            {
                Id = new Guid("71029CB8-48F3-40AD-3550-08DC4F659A39"),
                Name = "Standard User Test",
                Birth = new DateTime(1993, 1, 1),
                CPF = "123.456.789-03",
                Phone = new Phone { Number = "+5521993879312" },
                User = new User()
                {
                    Login = new Login { Email = "standard@user.com", Password = "12345T!" },
                },
                Address = new Address()
                {
                    Zipcode = "12345-678",
                    Street = "Standard Street",
                    Number = "123",
                    Neighborhood = "Standard",
                    City = "StandardCityville",
                    State = "ST",
                    Complement = "Apt 456",
                    Country = "StandardCountryland"
                }
            };

            customer.AddFlat(_context?.Flat?.Where(f => f.Id == new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa7")).FirstOrDefault() ?? new());
            _context?.Add(customer);

            customer = new Customer
            {
                Id = new Guid("473E3556-3219-4159-3551-08DC4F659A39"),
                Name = "Usuário Teste Padrão",
                Birth = new DateTime(1993, 1, 1),
                CPF = "999.999.999-99",
                Phone = new Phone { Number = "+5521993879312" },
                User = new User()
                {
                    Login = new Login { Email = "user@customer.com", Password = "12345T!" },
                },
                Address = new Address()
                {
                    Zipcode = "12345-678",
                    Street = "Main Street",
                    Number = "123",
                    Neighborhood = "Downtown",
                    City = "Cityville",
                    State = "ST",
                    Complement = "Apt 456",
                    Country = "Countryland"
                }
            };
            
            customer.AddFlat(_context?.Flat?.Where(f => f.Id == new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa7")).FirstOrDefault() ?? new());
            _context?.Add(customer);
            _context?.SaveChanges();
        }
        catch
        {
            throw;
        }
    }
}