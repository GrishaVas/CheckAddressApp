using Google.Type;

namespace CheckAddressApp.Models.Google.PlaceDetails
{
    public class Point
    {
        public Date Date { get; set; }
        public bool Truncated { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
    }
}
