namespace CheckAddressApp.Models.Loqate
{
    public class ValidateAddressRequest
    {
        public string Key { get; set; }
        public bool Geocode { get; set; }
        public List<ValidateAddressRequestAddress> Addresses { get; set; }
    }
}
