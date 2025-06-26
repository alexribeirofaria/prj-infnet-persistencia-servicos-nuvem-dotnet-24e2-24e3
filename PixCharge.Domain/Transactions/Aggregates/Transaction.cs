using PixCharge.Domain.Account.Aggregates;
using PixCharge.Domain.Core.Aggregates;
using PixCharge.Domain.Core.ValueObject;

namespace PixCharge.Domain.Transactions.Aggregates;
public class Transaction : BaseModel
{
    public DateTime DtTransaction { get; set; }
    public Monetary Value { get; set; } = 0;
    public string Description { get; set; } = String.Empty;
    public Customer Customer { get; set; } = new();
}
