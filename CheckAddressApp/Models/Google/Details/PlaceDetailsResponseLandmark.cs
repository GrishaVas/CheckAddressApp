namespace CheckAddressApp.Models.Google.Details
{
    public class PlaceDetailsResponseLandmark
    {
        public string Name { get; set; }
        public string PlaceId { get; set; }
        public PlaceDetailsResponseLocalizedText DisplayName { get; set; }
        public string[] Types { get; set; }
        public string SpatialRelationship { get; set; }
        public double StraightLineDistanceMeters { get; set; }
        public double TravelDistanceMeters { get; set; }
    }
}
