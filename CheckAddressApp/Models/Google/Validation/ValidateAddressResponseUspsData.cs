namespace CheckAddressWeb.Models.Google.Validation
{
    public class ValidateAddressResponseUspsData
    {
        public ValidateAddressResponseUspsAddress StandardizedAddress { get; set; }
        public string DeliveryPointCode { get; set; }
        public string DeliveryPointCheckDigit { get; set; }
        public string DpvConfirmation { get; set; }
        public string DpvFootnote { get; set; }
        public string DpvCmra { get; set; }
        public string DpvVacant { get; set; }
        public string DpvNoStat { get; set; }
        public string DpvNoStatReasonCode { get; set; }
        public string DpvDrop { get; set; }
        public string DpvThrowback { get; set; }
        public string DpvNonDeliveryDays { get; set; }
        public string DpvNonDeliveryDaysValues { get; set; }
        public string DpvNoSecureLocation { get; set; }
        public string DpvPbsa { get; set; }
        public string DpvDoorNotAccessible { get; set; }
        public string DpvEnhancedDeliveryCode { get; set; }
        public string CarrierRoute { get; set; }
        public string CarrierRouteIndicator { get; set; }
        public bool EwsNoMatch { get; set; }
        public string PostOfficeCity { get; set; }
        public string PostOfficeState { get; set; }
        public string AbbreviatedCity { get; set; }
        public string FipsCountyCode { get; set; }
        public string County { get; set; }
        public string ElotNumber { get; set; }
        public string ElotFlag { get; set; }
        public string LacsLinkReturnCode { get; set; }
        public string LacsLinkIndicator { get; set; }
        public string PoBoxOnlyPostalCode { get; set; }
        public string SuitelinkFootnote { get; set; }
        public string PmbDesignator { get; set; }
        public string PmbNumber { get; set; }
        public string AddressRecordType { get; set; }
        public bool DefaultAddress { get; set; }
        public string ErrorMessage { get; set; }
        public bool CassProcessed { get; set; }
    }
}
