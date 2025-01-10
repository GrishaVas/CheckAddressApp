using ISO3166;

namespace CheckAddressApp.Models
{
    public class CheckAddressInput
    {
        public Country Country { get; set; }
        public string FreeInput { get; set; }
        public StructuredInput StructuredInput { get; set; }

        public override string ToString()
        {
            return $"{FreeInput}{(Country != null ? $" {Country.Name}" : "")}";
        }
    }
}
