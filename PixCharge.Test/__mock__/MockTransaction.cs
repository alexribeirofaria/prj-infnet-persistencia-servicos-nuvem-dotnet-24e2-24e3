using Bogus;

namespace __mock__;
public class MockTransaction
{
    private static readonly object LockObject = new object();
    private static MockTransaction? _instance;

    public static MockTransaction Instance
    {
        get
        {
            lock (LockObject)
            {
                return _instance ??= new MockTransaction();
            }
        }
    }

    private MockTransaction() { }

    public Transaction GetFaker()
    {
        var fakeTransaction = new Faker<Transaction>()
            .RuleFor(t => t.Id, f => f.Random.Guid())
            .RuleFor(t => t.DtTransaction, f => f.Date.Recent())
            .RuleFor(t => t.Value, f => new Monetary(f.Finance.Amount()))
            .RuleFor(t => t.Description, f => f.Lorem.Sentence())
            .Generate();
        return fakeTransaction;
    }

    public List<Transaction> GetListFaker(int count)
    {
        var transactionList = new List<Transaction>();
        for (var i = 0; i < count; i++)
        {
            transactionList.Add(GetFaker());
        }
        return transactionList;
    }
}
