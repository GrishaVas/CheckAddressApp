namespace CheckAddressApp.Models.Google
{
    public class AutocompleteAddressResponsePlacePrediction
    {
        public string Place { get; set; }
        public string PlaceId { get; set; }
        public AutocompleteAddressResponsePlacePredictionText Text { get; set; }
    }
}
