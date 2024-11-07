namespace CheckAddressApp.Models.Google.Autocomplete
{
    public class AutocompleteAddressRequest
    {
        public string Input { get; set; }
        public string IncludedPrimaryTypes { get; set; }
        public bool IncludeQueryPredictions { get; set; }
        public List<string> IncludedRegionCodes { get; set; }
        public int InputOffset { get; set; }
        public string LanguageCode { get; set; }
        public AutocompleteAddressRequestOrigin Origin { get; set; }
        public string RegionCode { get; set; }
        public string SessionToken { get; set; }

        public AutocompleteAddressRequest(string input)
        {
            Input = input;
        }
    }
}
