namespace PixCharge.Domain.Account.ValueObject;
public record Phone
{
    public string Number { get; set; } = String.Empty;
    public static implicit operator string(Phone d) => d.Number;
    public static implicit operator Phone(string value) => new Phone(value);
    public Phone() { }
    public Phone(string value)
    {
        if (string.IsNullOrEmpty(value))
            throw new ArgumentException("Valor do telefone não pode ser nulo ou em branco");

        Number = value;
    }
}
