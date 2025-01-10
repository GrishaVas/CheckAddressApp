using ISO3166;

namespace qAcProviderTest.Models.FormInputs
{
    public abstract class CheckAddressFormInput
    {
        public Country Country { get; set; }
        public AddressProvider AddressProviders { get; set; }
        public string String { get; set; }
    }
}
