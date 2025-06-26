using Bogus;
using PixCharge.Domain.Transactions.ValueObject;

namespace __mock__;

public class MockPIX
{
    private static readonly object LockObject = new object();
    private static MockPIX? _instance;

    public static MockPIX Instance
    {
        get
        {
            lock (LockObject)
            {
                return _instance ??= new MockPIX();
            }
        }
    }

    private MockPIX() { }

    public PIX GetFaker()
    {
        var customer = MockCustomer.Instance.GetFaker();
        var fakePIX = new Faker<PIX>()
            .RuleFor(p => p.Id, f => f.Random.Guid())
            .RuleFor(p => p.CorrelationId, f => f.Random.Guid())
            .RuleFor(p => p.Date, f => f.Date.Recent())
            .RuleFor(p => p.Status, f => Status.Pending)
            .RuleFor(p => p.Customer, customer)
            .RuleFor(p => p.CustomerId, customer.Id)
            .RuleFor(p => p.Value, f => new Monetary(f.Finance.Amount()))
            .RuleFor(p => p.Description, f => f.Lorem.Sentence())
            .RuleFor(p => p.QrCode, f => new QRCode { BrCode = f.Random.Word(), Url = f.Random.Word() })
            .RuleFor(p => p.Transactions, f => new List<Transaction>())
            .Generate();
        return fakePIX;
    }

    public List<PIX> GetListFaker(int count)
    {
        var pixList = new List<PIX>();
        for (var i = 0; i < count; i++)
        {
            pixList.Add(GetFaker());
        }
        return pixList;
    }
}
