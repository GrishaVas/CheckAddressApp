namespace qAcProviderTest.Models.Here
{
    public class LookupAddressResponseEvStation
    {
        public LookupAddressResponseEvStationConnector[] Connectors { get; set; }
        public int TotalNumberOfConnectors { get; set; }
        public string Access { get; set; }
        public LookupAddressResponseEvStationEMobilityServiceProvider[] EMobilityServiceProviders { get; set; }
    }
}
