namespace CheckAddressApp.Models.Google.Details
{
    public class PlaceDetailsResponsePoint
    {
        public PlaceDetailsDate Date { get; set; }
        public bool Truncated { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
    }
}
