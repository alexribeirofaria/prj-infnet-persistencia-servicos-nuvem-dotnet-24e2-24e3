namespace PixCharge.Integration.Adapter.Plataform.OpenPix.Models;

[Serializable]
public sealed class CustomerOpenPix
{
    public string Name { get; set; }
    public object TaxID { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string CorrelationID { get; set; }
    public AddressOpenPix Address { get; set; }
}