namespace qAcProviderTest.Models.Here
{
    public class LookupAddressResponseContact
    {
        public LookupAddressResponseContactItem[] Phone { get; set; }
        public LookupAddressResponseContactItem[] Mobile { get; set; }
        public LookupAddressResponseContactItem[] TollFree { get; set; }
        public LookupAddressResponseContactItem[] Fax { get; set; }
        public LookupAddressResponseContactItem[] Www { get; set; }
        public LookupAddressResponseContactItem[] Email { get; set; }
    }
}
