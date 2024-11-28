using System.Text;

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

        public string ToString(string format)
        {
            var strBuilder = new StringBuilder(format)
                .Replace("$PostalCode", PostalCode != "" ? $"{PostalCode}" : "")
                .Replace("$City", City != "" ? $"{City}" : "")
                .Replace("$District", District != "" ? $"{District}" : "")
                .Replace("$StreetAndHouseNumber", StreetAndHouseNumber != "" ? $"{StreetAndHouseNumber}" : "");

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
