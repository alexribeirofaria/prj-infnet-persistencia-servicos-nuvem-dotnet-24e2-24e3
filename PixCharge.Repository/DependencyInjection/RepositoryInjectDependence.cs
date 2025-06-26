using Microsoft.Extensions.DependencyInjection;
using PixCharge.Domain.Account.Agreggates;
using PixCharge.Domain.Account.Aggregates;
using PixCharge.Domain.Transactions.Aggregates;
using PixCharge.Repository.Persistency;

namespace PixCharge.Repository.DependencyInjection;
public static class RepositoryInjectDependence
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<User>), typeof(UserRepository));
        services.AddScoped(typeof(IRepository<Customer>), typeof(CustomerRepository));
        services.AddScoped(typeof(IRepository<Charge>), typeof(ChargeRepository));
        services.AddScoped(typeof(IRepository<Flat>), typeof(FlatRepository));
        services.AddScoped(typeof(IRepository<PIX>), typeof(PIXRepository));
        services.AddScoped(typeof(IRepository<Transaction>), typeof(TransactionRepository));

        return services;
    }
}