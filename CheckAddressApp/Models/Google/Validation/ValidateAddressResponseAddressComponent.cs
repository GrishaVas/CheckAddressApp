namespace CheckAddressWeb.Models.Google.Validation
{
    public class ValidateAddressResponseAddressComponent
    {
        public ValidateAddressResponseComponentName ComponentName { get; set; }
        public string ComponentType { get; set; }
        public string ConfirmationLevel { get; set; }
        public bool Inferred { get; set; }
        public bool SpellCorrected { get; set; }
        public bool Replaced { get; set; }
        public bool Unexpected { get; set; }
    }
}
