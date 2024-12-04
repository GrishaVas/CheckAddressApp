namespace qAcProviderTest.Models.Here
{
    public class LookupAddressResponseEvStationConnector
    {
        public string SupplierName { get; set; }
        public LookupAddressResponseEvStationConnectorType ConnectorType { get; set; }
        public bool FixedCable { get; set; }
        public double MaxPowerLevel { get; set; }
        public LookupAddressResponseEvStationChergingPoint ChargingPoint { get; set; }
    }
}
