namespace CheckAddressWeb.Models.Google.Validation
{
    public class ValidateAddressResponseResult
    {
        public ValidateAddressResponseVerdict Verdict { get; set; }
        public ValidateAddressResponseAddress Address { get; set; }
        public ValidateAddressResponseGeocode Geocode { get; set; }
        public ValidateAddressResponseAddressMetadata Metadata { get; set; }
        public ValidateAddressResponseUspsData UspsData { get; set; }
        public ValidateAddressResponseEnglishLatinAddress EnglishLatinAddress { get; set; }
    }
}
