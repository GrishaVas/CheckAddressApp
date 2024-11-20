namespace CheckAddressApp.Models
{
    public class ServiceData
    {
        public string ServiceName { get; set; }
        public IEnumerable<CheckAddressData> CheckAddressData { get; set; }
    }
}
