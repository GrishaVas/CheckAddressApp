using System.Collections.Generic;

namespace CheckAddressApp.Models.Google.Autocomplete
{
    public class AutocompleteAddressResponse
    {
        public List<AutocompleteAddressResponseSuggestion> Suggestions { get; set; }
    }
}
