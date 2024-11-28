namespace CheckAddressApp.Models.Google.Details
{
    public class PlaceDetailsResponseGenerativeSummary
    {
        public PlaceDetailsResponseLocalizedText Overview { get; set; }
        public string OverviewFlagContentUri { get; set; }
        public PlaceDetailsResponseLocalizedText Description { get; set; }
        public string DescriptionFlagContentUri { get; set; }
        public PlaceDetailsResponseReferences References { get; set; }
    }
}
