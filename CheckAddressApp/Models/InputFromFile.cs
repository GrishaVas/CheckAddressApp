namespace CheckAddressApp.Models
{
    public class InputFromFile
    {
        public string StreetAndHouseNumber { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        public string GetString(bool withCountry = true)
        {
            return (PostalCode != "" ? $"{PostalCode} " : "") +
                    (City != "" ? $"{City} " : "") +
                    (District != "" ? $"{District} " : "") +
                    (StreetAndHouseNumber != "" ? withCountry ? $"{StreetAndHouseNumber} " : $"{StreetAndHouseNumber}" : "") +
                    (Country != "" && withCountry ? $"{Country} " : "");
        }

        public bool Equals(InputFromFile input)
        {
            return input.StreetAndHouseNumber == StreetAndHouseNumber &&
                input.City == City &&
                input.District == District &&
                input.Country == Country &&
                input.PostalCode == PostalCode;
        }
    }
}
