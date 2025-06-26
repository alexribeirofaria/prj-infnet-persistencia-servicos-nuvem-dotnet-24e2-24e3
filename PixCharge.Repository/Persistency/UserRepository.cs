using PixCharge.Domain.Account.Agreggates;

namespace PixCharge.Repository.Persistency;
public class UserRepository : RepositoryBase<User>, IRepository<User>
{    
    public UserRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }
}