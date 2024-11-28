namespace CheckAddressApp.Models.Google.Details
{
    public class PlaceDetailsResponseReview
    {
        public string Name { get; set; }
        public string RelativePublishTimeDescription { get; set; }
        public PlaceDetailsResponseLocalizedText Text { get; set; }
        public PlaceDetailsResponseLocalizedText OriginalText { get; set; }
        public double Rating { get; set; }
        public PlaceDetailsResponseAuthorAttribution AuthorAttribution { get; set; }
        public string PublishTime { get; set; }
        public string FlagContentUri { get; set; }
        public string GoogleMapsUri { get; set; }
    }
}