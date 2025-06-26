using Bogus;
using Bogus.Extensions.Brazil;
using PixCharge.Application.Account.Dto;

namespace __mock__;
public class MockCustomer
{
    private static MockCustomer? _instance;
    private static readonly object LockObject = new object();
    public static MockCustomer Instance
    {
        get
        {
            lock (LockObject)
            {
                return _instance ??= new MockCustomer();
            }
        }
    }

    private MockCustomer() { }

    public Customer GetFaker()
    {
        Customer? fakeCustomer = new Faker<Customer>()
             .RuleFor(c => c.Id, f => f.Random.Guid())
             .RuleFor(c => c.Name, f => f.Name.FirstName())
             .RuleFor(c => c.CorrelationID, new Guid())
             .RuleFor(c => c.TaxID, new Guid().ToString())
             .RuleFor(c => c.User, MockUser.Instance.GetFaker())
             .RuleFor(c => c.CPF, f => f.Person.Cpf())
             .RuleFor(c => c.Birth, f => f.Person.DateOfBirth)
             .RuleFor(c => c.Phone, MockPhone.GetFaker())
             .RuleFor(c => c.Address, MockAddress.Instance.GetFaker())
             .Generate();
        return fakeCustomer;

    }

    public List<Customer> GetListFaker(int count)
    {
        var customerList = new List<Customer>();
        for (var i = 0; i < count; i++)
        {
            customerList.Add(GetFaker());
        }
        return customerList;

    }

    public CustomerDto GetDtoFromCustomer(Customer customer)
    {
        CustomerDto? fakeCustomerDto = new Faker<CustomerDto>()
            .RuleFor(c => c.Id, f => customer.Id)
            .RuleFor(c => c.Name, f => customer.Name)
            .RuleFor(c => c.Email, f => customer.User.Login.Email)
            .RuleFor(c => c.Password, f => customer.User.Login.Password)
            .RuleFor(c => c.CPF, f => customer.CPF)
            .RuleFor(c => c.Birth, f => customer.Birth)
            .RuleFor(c => c.Phone, f => customer.Phone.Number)
            .RuleFor(c => c.Address, f => MockAddress.Instance.GetDtoFromAddress(customer.Address))
            .RuleFor(c => c.FlatId, f => Guid.NewGuid())
            .Generate();
        return fakeCustomerDto;

    }

    public List<CustomerDto> GetDtoListFromCustomerList(IList<Customer> customers)
    {
        var customerDtoList = new List<CustomerDto>();

        foreach (var customer in customers)
        {
            var customerDto = GetDtoFromCustomer(customer);
            customerDtoList.Add(customerDto);
        }

        return customerDtoList;

    }
}