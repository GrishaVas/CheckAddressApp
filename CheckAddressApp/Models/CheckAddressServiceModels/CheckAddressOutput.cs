namespace qAcProviderTest.Models.CheckAddressServiceModels
{
    public class CheckAddressOutput
    {
        public double Time { get; set; }
        public IEnumerable<CheckAddressAddressData> Addresses { get; set; }
    }
}
