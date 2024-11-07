using Google.Type;

namespace CheckAddressApp.Models.Google.PlaceDetails
{
    public class Review
    {
        public string Name { get; set; }
        public string RelativePublishTimeDescription { get; set; }
        public LocalizedText Text { get; set; }
        public LocalizedText OriginalText { get; set; }
        public double Rating { get; set; }
        public AuthorAttribution AuthorAttribution { get; set; }
        public string PublishTime { get; set; }
        public string FlagContentUri { get; set; }
        public string GoogleMapsUri { get; set; }
    }
}