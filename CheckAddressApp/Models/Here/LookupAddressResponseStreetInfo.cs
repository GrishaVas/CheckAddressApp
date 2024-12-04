namespace qAcProviderTest.Models.Here
{
    public class LookupAddressResponseStreetInfo
    {
        public string BaseName { get; set; }
        public string StreetType { get; set; }
        public bool StreetTypePrecedes { get; set; }
        public bool StreetTypeAttached { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public string Direction { get; set; }
        public string Language { get; set; }
    }
}
