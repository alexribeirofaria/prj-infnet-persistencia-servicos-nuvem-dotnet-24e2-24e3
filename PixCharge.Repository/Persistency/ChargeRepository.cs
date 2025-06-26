using Microsoft.EntityFrameworkCore;
using PixCharge.Domain.Account.Aggregates;
using PixCharge.Domain.Transactions.Aggregates;

namespace PixCharge.Repository.Persistency;
public class ChargeRepository : RepositoryBase<Charge>, IRepository<Charge>
{
    public ChargeRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }

    public override Charge GetById(Guid id)
    {
        return this.Context.Charge
            .Include(x => x.PIX)
            .First(x => x.Id == id);
    }
}