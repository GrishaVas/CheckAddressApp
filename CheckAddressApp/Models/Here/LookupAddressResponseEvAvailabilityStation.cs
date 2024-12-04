namespace qAcProviderTest.Models.Here
{
    public class LookupAddressResponseEvAvailabilityStation
    {
        public string Id { get; set; }
        public string CpoId { get; set; }
        public LookupAddressResponseEvAvailabilityEvse[] Evses { get; set; }
    }
}
