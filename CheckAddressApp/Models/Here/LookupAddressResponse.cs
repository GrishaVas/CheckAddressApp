using qAcProviderTest.Models.Here;

namespace CheckAddressApp.Models.Here
{
    public class LookupAddressResponse
    {
        public string Title { get; set; }
        public string Id { get; set; }
        public string ClosedPermanently { get; set; }
        public string PoliticalView { get; set; }
        public string ResultType { get; set; }
        public string HouseNumberType { get; set; }
        public string AddressBlockType { get; set; }
        public string LocalityType { get; set; }
        public string AdministrativeAreaType { get; set; }
        public bool HouseNumberFallback { get; set; }
        public bool EstimatedPointAddress { get; set; }
        public LookupAddressResponseAddress Address { get; set; }
        public LookupAddressResponsePostalCodeDetails[] PostalCodeDetails { get; set; }
        public LookupAddressResponsePosition Position { get; set; }
        public LookupAddressResponsePosition[] Access { get; set; }
        public LookupAddressResponseMapView MapView { get; set; }
        public LookupAddressResponseCategory[] Categories { get; set; }
        public LookupAddressResponseChain[] Chains { get; set; }
        public LookupAddressResponseReference[] References { get; set; }
        public LookupAddressResponseFoodType[] FoodTypes { get; set; }
        public LookupAddressResponseContact[] Contacts { get; set; }
        public LookupAddressResponseOpeningHour[] OpeningHours { get; set; }
        public LookupAddressResponseTimeZone TimeZone { get; set; }
        public LookupAddressResponseExtended Extended { get; set; }
        public LookupAddressResponseStreetInfo[] StreetInfo { get; set; }
        public LookupAddressResponseCountryInfo MyProperty { get; set; }
    }
}
