using PixCharge.Application.CommonInjectDependence;
using Microsoft.Extensions.DependencyInjection;
using PixCharge.Application.Account.Profile;
using Application.Transactions.Profile;

namespace Application.DependencyInjection;
public class AutoMapperInjectDependenceTest
{
    [Fact]
    public void AddAutoMapper_Should_Register_Profiles()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddAutoMapper();

        // Assert
        Assert.NotNull(services.Any(descriptor => descriptor.ServiceType == typeof(CustomerProfile)));
        Assert.NotNull(services.Any(descriptor => descriptor.ServiceType == typeof(ChargeProfile)));
        Assert.NotNull(services.Any(descriptor => descriptor.ServiceType == typeof(AddressProfile)));
    }
}
