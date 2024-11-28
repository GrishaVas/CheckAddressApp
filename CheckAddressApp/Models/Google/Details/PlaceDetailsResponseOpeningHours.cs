namespace CheckAddressApp.Models.Google.Details
{
    public class PlaceDetailsResponseOpeningHours
    {
        public PlaceDetailsResponsePeriod[] Periods { get; set; }
        public string[] MyProperty { get; set; }
        public string SecondaryHoursType { get; set; }
        public PlaceDetailsResponseSpecialDay[] SpecialDays { get; set; }
        public bool OpenNow { get; set; }
    }
}
