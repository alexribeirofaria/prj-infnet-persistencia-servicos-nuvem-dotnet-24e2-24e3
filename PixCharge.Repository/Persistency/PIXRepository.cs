using PixCharge.Domain.Transactions.Aggregates;

namespace PixCharge.Repository.Persistency;
public class PIXRepository : RepositoryBase<PIX>, IRepository<PIX>
{
    public PIXRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }
}