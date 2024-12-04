namespace qAcProviderTest.Models.Here
{
    public class LookupAddressResponseEvStationChergingPoint
    {
        public int NumberOfConnectors { get; set; }
        public string VoltsRange { get; set; }
        public int Phases { get; set; }
        public string NumberOfAvailable { get; set; }
        public int NumberOfInUse { get; set; }
        public int NumberOfOutOfService { get; set; }
        public int NumberOfReserved { get; set; }
        public string LastUpdateTimestamp { get; set; }
    }
}
