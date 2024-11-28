namespace CheckAddressWeb.Models.Google.Validation
{
    public class ValidateAddressResponseVerdict
    {
        public string InputGranularity { get; set; }
        public string ValidationGranularity { get; set; }
        public string GeocodeGranularity { get; set; }
        public bool AddressComplete { get; set; }
        public bool HasUnconfirmedComponents { get; set; }
        public bool HasInferredComponents { get; set; }
        public bool HasReplacedComponets { get; set; }
    }
}
