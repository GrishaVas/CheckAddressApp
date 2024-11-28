namespace CheckAddressWeb.Models.Google.Validation
{
    public class ValidateAddressRequestPostalAddress
    {
        public int Revision { get; set; }
        public string RegionCode { get; set; }
        public string LanguageCode { get; set; }
        public string PostalCode { get; set; }
        public string SortingCode { get; set; }
        public string AdministrativeArea { get; set; }
        public string Locality { get; set; }
        public string Sublocality { get; set; }
        public string[] AddressLines { get; set; }
        public string[] Recipients { get; set; }
        public string Organization { get; set; }
    }
}
