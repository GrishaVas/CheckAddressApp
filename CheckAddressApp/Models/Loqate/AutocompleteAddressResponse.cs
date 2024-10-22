namespace CheckAddressApp.Models.Loqate
{
    public class AutocompleteAddressResponse
    {
        public string Status { get; set; }
        public List<string> Output { get; set; }
        public List<AutocompleteAddressResponseMetadata> Metadata { get; set; }
    }
}
