namespace CheckAddressApp.Models.Google.Details
{
    public class PlaceDetailsResponsePhoto
    {
        public string Name { get; set; }
        public int WidthPx { get; set; }
        public int HeightPx { get; set; }
        public PlaceDetailsResponseAuthorAttribution[] AuthorAttributions { get; set; }
        public string FlagContentUri { get; set; }
        public string GoogleMapsUri { get; set; }
    }
}