namespace CheckAddressWeb.Models.Google.Validation
{
    public class ValidateAddressResponseGeocode
    {
        public ValidateAddressResponseLatLng Location { get; set; }
        public ValidateAddressResponsePlusCode PlusCode { get; set; }
        public ValidateAddressResponseViewport Bounds { get; set; }
        public double FeatureSizeMeters { get; set; }
        public string PlaceId { get; set; }
        public string[] PlaceTypes { get; set; }
    }
}
