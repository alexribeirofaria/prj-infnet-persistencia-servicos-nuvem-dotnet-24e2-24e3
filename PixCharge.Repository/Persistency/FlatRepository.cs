using PixCharge.Domain.Account.Aggregates;

namespace PixCharge.Repository.Persistency;
public class FlatRepository : RepositoryBase<Flat>, IRepository<Flat>
{
    public FlatRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }
}