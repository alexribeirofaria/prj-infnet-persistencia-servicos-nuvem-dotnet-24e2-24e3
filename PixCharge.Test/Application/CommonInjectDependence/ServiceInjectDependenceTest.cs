using Application.Account;
using Microsoft.Extensions.DependencyInjection;
using PixCharge.Application.Account;
using PixCharge.Application.CommonInjectDependence;
using PixCharge.Application.Transactions;

namespace Application.DependencyInjection;
public class ServiceInjectDependenceTest
{
    [Fact]
    public void AddServices_Should_Register_Services()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddServices();

        // Assert
        Assert.NotNull(services.Any(descriptor => descriptor.ServiceType == typeof(UserService)));
        Assert.NotNull(services.Any(descriptor => descriptor.ServiceType == typeof(CustomerService)));
        Assert.NotNull(services.Any(descriptor => descriptor.ServiceType == typeof(ChargeService)));
    }
}