using PixCharge.Domain.Transactions.Aggregates;

namespace PixCharge.Integration.Adapter.Plataform.Bacen;
public class BacenCharge : IPix
{
    public Charge CreateCharge(decimal Value, string CorrelationID)
    {
        throw new NotImplementedException();
    }

    public bool IsChargeApporve(Guid correlationID)
    {
        throw new NotImplementedException();
    }
}
