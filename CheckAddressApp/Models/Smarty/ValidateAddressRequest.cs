namespace CheckAddressApp.Models.Smarty
{
    public class ValidateAddressRequest
    {
        public string Address { get; set; }
        public string Locality { get; set; }
        public string Sublocality { get; set; }
        public string CountryCode { get; set; }
        public string PostalCode { get; set; }
        public string AdminustrationArea { get; set; }
    }
}
