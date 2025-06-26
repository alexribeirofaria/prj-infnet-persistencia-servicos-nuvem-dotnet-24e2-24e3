using Bogus;

namespace __mock__;
public class MockFlat
{
    private static readonly Lazy<MockFlat> _instance = new Lazy<MockFlat>(() => new MockFlat());
    private readonly Faker<Flat> _faker;

    private MockFlat()
    {
        _faker = new Faker<Flat>()
            .RuleFor(f => f.Id, f => f.Random.Guid())
            .RuleFor(f => f.Value, f => new Monetary(f.Finance.Amount()));
    }

    public static MockFlat Instance => _instance.Value;

    public Flat GetFaker()
    {
        return _faker.Generate();
    }

    public List<Flat> GetListFaker(int count)
    {
        var flatList = new List<Flat>();
        for (var i = 0; i < count; i++)
        {
            flatList.Add(GetFaker());
        }
        return flatList;
    }
}
