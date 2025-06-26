using Bogus;

namespace __mock__;
public class MockPhone
{
    public static Phone GetFaker()
    {
        var fakePhone = new Faker<Phone>()
            .RuleFor(c => c.Number, f => f.Person.Phone)
            .Generate();

        return fakePhone;
    }
}