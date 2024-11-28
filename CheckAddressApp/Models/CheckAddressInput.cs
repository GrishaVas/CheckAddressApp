namespace CheckAddressApp.Models
{
    public class CheckAddressInput
    {
        public string Country { get; set; }
        public string FreeInput { get; set; }
        public StructuredInput StructuredInput { get; set; }

        public override string ToString()
        {

            return $"{FreeInput} {Country}";
        }
    }
}
