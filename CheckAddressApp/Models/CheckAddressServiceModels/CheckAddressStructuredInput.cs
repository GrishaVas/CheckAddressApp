using System.Text;
using ISO3166;

namespace qAcProviderTest.Models.CheckAddressServiceModels
{
    public class CheckAddressStructuredInput : CheckAddressInput
    {
        public string StreetAndHouseNumber { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string PostalCode { get; set; }

        public CheckAddressStructuredInput(string streetAndHouseNumber, string city, string district, string postalCode, Country country, string format)
        {
            StreetAndHouseNumber = streetAndHouseNumber;
            City = city;
            District = district;
            PostalCode = postalCode;
            Country = country;

            var strBuilder = new StringBuilder(format)
                .Replace("$PostalCode", PostalCode != "" ? $"{PostalCode}" : "")
                .Replace("$City", City != "" ? $"{City}" : "")
                .Replace("$District", District != "" ? $"{District}" : "")
                .Replace("$StreetAndHouseNumber", StreetAndHouseNumber != "" ? $"{StreetAndHouseNumber}" : "")
                .Replace("$Country", !string.IsNullOrEmpty(Country?.TwoLetterCode ?? "") ? $"{Country.TwoLetterCode}" : "");

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

            base.FullString = result;
        }
    }
}
