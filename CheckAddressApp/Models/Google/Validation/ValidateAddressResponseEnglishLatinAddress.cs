namespace CheckAddressWeb.Models.Google.Validation
{
    public class ValidateAddressResponseEnglishLatinAddress
    {
        public string FormattedAddress { get; set; }
        public ValidateAddressResponsePostalAddress PostalAddress { get; set; }
        public ValidateAddressResponseAddressComponent[] AddressComponents { get; set; }
        public string[] MissingComponentTypes { get; set; }
        public string[] UnconfirmedComponentTypes { get; set; }
        public string[] UnresolvedTokens { get; set; }
    }
}
