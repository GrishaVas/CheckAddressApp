namespace CheckAddressApp.Models.Here
{
    public class AutocompleteAddressResponseItem
    {
        public string Title { get; set; }
        public string Id { get; set; }
        public string PoliticalView { get; set; }
        public string ResultType { get; set; }
        public string HouseNumberType { get; set; }
        public string OntologyId { get; set; }
        public string AddressBlockType { get; set; }
        public string LocalityType { get; set; }
        public string AdministrativeAreaType { get; set; }
        public AutocompleteAddressResponseAddress Address { get; set; }
    }
}
