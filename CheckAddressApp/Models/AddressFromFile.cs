using System.Text;

namespace CheckAddressApp.Models
{
    public class AddressFromFile
    {
        public string StreetAndHouseNumber { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        public string GetString(string format, bool withCountry = true)
        {
            var strBuilder = new StringBuilder(format)
                .Replace("$PostalCode", PostalCode != "" ? $"{PostalCode}" : "")
                .Replace("$City", City != "" ? $"{City}" : "")
                .Replace("$District", District != "" ? $"{District}" : "")
                .Replace("$StreetAndHouseNumber", StreetAndHouseNumber != "" ? $"{StreetAndHouseNumber}" : "")
                .Replace("$Country", withCountry ? (Country != "" ? $"{Country}" : "") : "");

            var index = 0;

            for (int i = 0; i < strBuilder.Length; i++)
            {
                if (strBuilder[i] != ' ')
                {
                    index = i;
                    break;
                }
            }

            strBuilder.Remove(0, index);

            var result = strBuilder.ToString();

            return result;
        }
    }
}
