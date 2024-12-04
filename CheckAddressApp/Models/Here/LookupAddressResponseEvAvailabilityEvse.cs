namespace qAcProviderTest.Models.Here
{
    public class LookupAddressResponseEvAvailabilityEvse
    {
        public string Id { get; set; }
        public string CpoId { get; set; }
        public string CpoEveseEMI3Id { get; set; }
        public string State { get; set; }
        public string Last_Updated { get; set; }
        public LookupAddressResponseEvAvailabilityEvseConnector[] Connectors { get; set; }
    }
}
