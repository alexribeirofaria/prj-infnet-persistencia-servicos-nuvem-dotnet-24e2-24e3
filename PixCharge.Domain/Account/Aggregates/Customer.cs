using PixCharge.Domain.Transactions.Aggregates;

namespace PixCharge.Domain.Account.Aggregates;
public class Customer : AbstractAccount<Customer>
{
    public Guid CorrelationID { get; set; }
    public string TaxID { get; set; } = String.Empty;
    public virtual IList<Transaction> Transactions { get; set; } = new List<Transaction>();
    public virtual IList<Charge> Charges { get; set; } = new List<Charge>();
    public override void CreateAccount(Customer customer)
    {        
        this.Name = customer.Name;
        this.CPF = customer.CPF;
        this.Birth = customer.Birth;
        this.Phone = customer.Phone;
        this.Address = customer.Address;
        this.User = customer.User;
    }
}