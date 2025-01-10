using System.Text;

namespace qAcProviderTest.Models.FormInputs
{
    public class CheckAddressFormStructuredInput : CheckAddressFormInput
    {
        public string StreetAndHouseNumber { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string PostalCode { get; set; }

        public void SetString(string format)
        {
            var strBuilder = new StringBuilder(format)
                .Replace("$PostalCode", PostalCode != "" ? $"{PostalCode}" : "")
                .Replace("$City", City != "" ? $"{City}" : "")
                .Replace("$District", District != "" ? $"{District}" : "")
                .Replace("$StreetAndHouseNumber", StreetAndHouseNumber != "" ? $"{StreetAndHouseNumber}" : "")
                .Replace("$Country", Country.TwoLetterCode != "" ? $"{Country}" : "");

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

            String = result;
        }
    }
}
