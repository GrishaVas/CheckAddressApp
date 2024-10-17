namespace CheckAddressApp.Models.Google
{
    public class AutocompleteAddressResponseSuggestion
    {
        public AutocompleteAddressResponsePlacePrediction PlacePrediction { get; set; }
        public string[] Types { get; set; }
    }
}
