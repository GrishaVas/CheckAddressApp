namespace CheckAddressApp.Models.Here
{
    public class LookupAddressRequest
    {
        public string Id { get; set; }

        public LookupAddressRequest(string id)
        {
            Id = id;
        }
    }
}
