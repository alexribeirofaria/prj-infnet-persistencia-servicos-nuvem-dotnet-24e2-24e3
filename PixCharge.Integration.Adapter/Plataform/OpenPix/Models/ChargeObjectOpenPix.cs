namespace PixCharge.Integration.Adapter.Plataform.OpenPix.Models;

[Serializable]
public class ChargeObjectOpenPix
{
    public string CorrelationID { get; set; }
    public string Value { get; set; }
    public string BrCode { get; set; }
    public ChargeOpenPix Charge { get; set; }

}
