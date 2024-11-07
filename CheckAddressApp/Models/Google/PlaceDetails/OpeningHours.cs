namespace CheckAddressApp.Models.Google.PlaceDetails
{
    public class OpeningHours
    {
        public Period Periods { get; set; }
        public string[] MyProperty { get; set; }
        public SecondaryHoursType SecondaryHoursType { get; set; }
        public SpecialDay[] SpecialDays { get; set; }
        public bool OpenNow { get; set; }
    }
}
