namespace CheckAddressApp.Models.Google.Autocomplete
{
    public class AutocompleteAddressResponseSuggestion
    {
        public AutocompleteAddressResponsePlacePrediction PlacePrediction { get; set; }
        public string[] Types { get; set; }
    }
}
