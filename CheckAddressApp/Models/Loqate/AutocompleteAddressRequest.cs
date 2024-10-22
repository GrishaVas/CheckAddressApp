namespace CheckAddressApp.Models.Loqate
{
    public class AutocompleteAddressRequest
    {
        public string Lqtkey { get; set; }
        public string Query { get; set; }
        public string Country { get; set; }
        public AutocompleteAddressRequestFilters Filters { get; set; }
    }
}
