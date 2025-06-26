using PixCharge.Domain.Transactions.Aggregates;

namespace PixCharge.Repository.Persistency;
public class TransactionRepository : RepositoryBase<Transaction>, IRepository<Transaction>
{
    public TransactionRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }
}