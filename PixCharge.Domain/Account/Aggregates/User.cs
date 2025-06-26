using PixCharge.Domain.Account.ValueObject;
using PixCharge.Domain.Core.Aggregates;

namespace PixCharge.Domain.Account.Agreggates;
public class User : BaseModel
{
    public virtual Login Login { get; set; } = new();
    public DateTime DtCreated { get; set; }
}