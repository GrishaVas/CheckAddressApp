namespace CheckAddressApp.Models
{
    public class StructuredInput
    {
        public string StreetAndHouseNumber { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string PostalCode { get; set; }

        public override string ToString()
        {
            return (PostalCode != "" ? $"{PostalCode} " : "") +
                (City != "" ? $"{City} " : "") +
                (District != "" ? $"{District} " : "") +
                (StreetAndHouseNumber != "" ? $"{StreetAndHouseNumber} " : "");
        }
    }
}
