using PixCharge.Domain.Account.Aggregates;
using PixCharge.Domain.Core.Aggregates;
using PixCharge.Domain.Core.ValueObject;
using PixCharge.Domain.Transactions.ValueObject;

namespace PixCharge.Domain.Transactions.Aggregates;
public class PIX : BaseModel
{
    public Guid CorrelationId { get; set; }
    public DateTime Date { get; set; }
    public Status Status { get; set; } = 0;
    public virtual Customer Customer { get; set; } = new();
    public virtual Guid CustomerId { get; set; }
    public virtual Monetary Value { get; set; } = 0;
    public string Description { get; set; } = String.Empty;
    public virtual QRCode QrCode { get; set; } = new();
    public virtual IList<Transaction> Transactions { get; set; } = new List<Transaction>();
    public PIX() { }
    public PIX(Customer custumer)
    {
        Id = Guid.NewGuid();
        Status = Status.Pending;
        Customer = custumer;
    }
}
