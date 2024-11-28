using System.Collections.Generic;

namespace CheckAddressApp.Models.Loqate
{
    public class ValidateAddressResponse
    {
        public ValidateAddressResponseInput Input { get; set; }
        public List<ValidateAddressResponseMatch> Matches { get; set; }
    }
}
