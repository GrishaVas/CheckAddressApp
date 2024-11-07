using GoogleGeoType = Google.Geo.Type;
using GoogleType = Google.Type;

namespace CheckAddressApp.Models.Google.PlaceDetails
{
    public class PlaceDetailsResponse
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public DisplayName DisplayName { get; set; }
        public string[] Types { get; set; }
        public string PrimaryType { get; set; }
        public DisplayName PrimaryTypeDisplayName { get; set; }
        public string NationalPhoneNumber { get; set; }
        public string InternationalPhoneNumber { get; set; }
        public string FormattedAddress { get; set; }
        public string ShortFormattedAddress { get; set; }
        public AddressComponent[] AddressComponents { get; set; }
        public PlusCode PlusCode { get; set; }
        public GoogleType.LatLng Location { get; set; }
        public GoogleGeoType.Viewport Viewport { get; set; }
        public double Rating { get; set; }
        public string GoogleMapsUri { get; set; }
        public string WebsiteUri { get; set; }
        public Review[] Reviews { get; set; }
        public OpeningHours RegularOpeningHours { get; set; }
        public Photo[] Photos { get; set; }
        public string AdrFormatAddress { get; set; }
    }
}
