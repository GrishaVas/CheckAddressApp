using System.Text.Json.Serialization;

namespace CheckAddressApp.Models.Loqate
{
    public class ValidateAddressResponseMatch
    {
        public string AQI { get; set; }
        public string AVC { get; set; }
        public string Address { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string AdministrativeArea { get; set; }
        public string CountryName { get; set; }
        public string DeliveryAddress { get; set; }
        public string DeliveryAddress1 { get; set; }
        public string GeoAccuracy { get; set; }
        public string GeoDistance { get; set; }
        public string HyphenClass { get; set; }
        [JsonPropertyName("ISO3166-2")]
        public string ISO3166_2 { get; set; }
        public string Country { get; set; }
        [JsonPropertyName("ISO3166-3")]
        public string ISO3166_3 { get; set; }
        [JsonPropertyName("ISO3166-N")]
        public string ISO3166_N { get; set; }
        public string Latitude { get; set; }
        public string Locality { get; set; }
        public string Longitude { get; set; }
        public string MatchRuleLabel { get; set; }
        public string PostalCode { get; set; }
        public string PostalCodePrimary { get; set; }
        public string Premise { get; set; }
        public string PremiseNumber { get; set; }
        public string SubAdministrativeArea { get; set; }
        public string Thoroughfare { get; set; }
    }
}
