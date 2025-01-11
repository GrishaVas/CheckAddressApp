using ISO3166;

namespace qAcProviderTest.Models.CheckAddressServiceModels
{
    public abstract class CheckAddressInput
    {
        public Country Country { get; set; }
        public AddressProvider AddressProviders { get; set; }
        public string FullString { get; set; }
    }
}
