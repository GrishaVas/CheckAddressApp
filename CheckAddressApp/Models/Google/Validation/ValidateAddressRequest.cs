namespace CheckAddressWeb.Models.Google.Validation
{
    public class ValidateAddressRequest
    {
        public ValidateAddressRequestPostalAddress Address { get; set; }
        public string PreviousResponseId { get; set; }
        public bool EnableUspsCass { get; set; }
        public string SessionToken { get; set; }
    }
}
