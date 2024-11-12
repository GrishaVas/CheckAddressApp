namespace CheckAddressApp.Models.Loqate
{
    public class AutocompleteAddressRequest
    {
        public string Key { get; set; }
        public string Text { get; set; }
        public string Origin { get; set; }
        public string Container { get; set; }
        //public AutocompleteAddressRequestFilters Filters { get; set; }
    }
}
