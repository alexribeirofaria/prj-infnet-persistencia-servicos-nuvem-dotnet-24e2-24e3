using Microsoft.EntityFrameworkCore;
using PixCharge.Domain.Account.Aggregates;

namespace PixCharge.Repository.Persistency;
public class CustomerRepository : RepositoryBase<Customer>, IRepository<Customer>
{    
    public CustomerRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }
    public override Customer GetById(Guid id)
    {
        return this.Context.Customer
            .Include(x => x.Address)
            .Include(x => x.Flats)
            .Include(x => x.Transactions)
            .Include(x => x.Charges)
            .First(x => x.Id == id);                   
    }
}