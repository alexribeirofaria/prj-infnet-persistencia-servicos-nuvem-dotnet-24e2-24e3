using PixCharge.Domain.Core.Aggregates;

namespace PixCharge.Domain.Account.ValueObject;
public class Address : BaseModel
{
    public Address() { }
    public string Zipcode { get; set; } = String.Empty;
    public string Street { get; set; } = String.Empty;
    public string Number { get; set; } = String.Empty;
    public string Neighborhood { get; set; } = String.Empty;
    public string City { get; set; } = String.Empty;
    public string State { get; set; } = String.Empty;
    public string Complement { get; set; } = String.Empty;
    public string Country { get; set; } = String.Empty;
}
