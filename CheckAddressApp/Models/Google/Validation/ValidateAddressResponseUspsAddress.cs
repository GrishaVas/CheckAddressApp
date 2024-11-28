namespace CheckAddressWeb.Models.Google.Validation
{
    public class ValidateAddressResponseUspsAddress
    {
        public string FirstAddressLine { get; set; }
        public string Firm { get; set; }
        public string SecondAddressLine { get; set; }
        public string Urbanization { get; set; }
        public string CityStateZipAddressLine { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string ZipCodeExtension { get; set; }
    }
}
