using Microsoft.Extensions.DependencyInjection;
using PixCharge.Domain.Account.Agreggates;
using PixCharge.Repository;
using PixCharge.Repository.DependencyInjection;
using PixCharge.Repository.Persistency;

namespace Repository.DependencyInjection;
public class RepositoryInjectDependenceTest
{
    [Fact]
    public void AddRepositories_Should_Register_Repositories()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddRepositories();

        // Assert
        Assert.NotNull(services.Any(descriptor => descriptor.ServiceType == typeof(IRepository<User>) && descriptor.ImplementationType == typeof(UserRepository)));
        Assert.NotNull(services.Any(descriptor => descriptor.ServiceType == typeof(IRepository<Customer>) && descriptor.ImplementationType == typeof(CustomerRepository)));
        Assert.NotNull(services.Any(descriptor => descriptor.ServiceType == typeof(IRepository<Charge>) && descriptor.ImplementationType == typeof(ChargeRepository)));
        Assert.NotNull(services.Any(descriptor => descriptor.ServiceType == typeof(IRepository<Flat>) && descriptor.ImplementationType == typeof(FlatRepository)));
        Assert.NotNull(services.Any(descriptor => descriptor.ServiceType == typeof(IRepository<PIX>) && descriptor.ImplementationType == typeof(PIXRepository)));
        Assert.NotNull(services.Any(descriptor => descriptor.ServiceType == typeof(IRepository<Transaction>) && descriptor.ImplementationType == typeof(TransactionRepository)));
    }
}