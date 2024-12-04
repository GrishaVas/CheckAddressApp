namespace qAcProviderTest.Models.Here
{
    public class LookupAddressResponseOpeningHour
    {
        public LookupAddressResponseOpeningHourCategory[] Categories { get; set; }
        public string[] Text { get; set; }
        public bool IsOpen { get; set; }
        public LookupAddressResponseOpeningHourStructured[] Structured { get; set; }
    }
}
