namespace CheckAddressApp.Models.Google.PlaceDetails
{
    public class Photo
    {
        public string Name { get; set; }
        public int WidthPx { get; set; }
        public int HeightPx { get; set; }
        public AuthorAttribution[] AuthorAttributions { get; set; }
        public string FlagContentUri { get; set; }
        public string GoogleMapsUri { get; set; }
    }
}