using Application.Transactions.Profile;
using Microsoft.Extensions.DependencyInjection;
using PixCharge.Application.Account.Profile;

namespace PixCharge.Application.CommonInjectDependence;
public static class AutoMapperInjectDependence
{
    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(CustomerProfile).Assembly);
        services.AddAutoMapper(typeof(ChargeProfile).Assembly);
        return services;
    }
}
