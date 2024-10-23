namespace CheckAddressApp.Models.Smarty
{
    public class ValidateAddressResponse
    {
        public string CoutryCode { get; set; }
        public SmartyStreets.USStreetApi.Lookup USLookup { get; set; }
        public SmartyStreets.InternationalStreetApi.Lookup InternationalLookup { get; set; }
    }
}
