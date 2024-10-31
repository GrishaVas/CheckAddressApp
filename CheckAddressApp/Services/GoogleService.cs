using CheckAddressApp.Models;
using CheckAddressApp.Models.Google;
using CheckAddressApp.Services.Api;
using Google.Maps.AddressValidation.V1;
using Google.Protobuf.Collections;
using Google.Type;
using Microsoft.Extensions.Configuration;

namespace CheckAddressApp.Services
{
    public class GoogleService
    {
        private GoogleAddressApiService _googleAddressApiService;
        private IConfiguration _conf;

        public GoogleService(IConfiguration conf)
        {
            _googleAddressApiService = new GoogleAddressApiService(conf["Google:ApiKey"],
                conf["Google:ClientId"],
                conf["Google:ClientSecret"],
                conf["Google:RefreshToken"]);
            _conf = conf;
        }

        public async Task<AutocompleteAddressResponse> AutocompleteAddress(string input, string countryCode)
        {
            var autocompleteAddressRequest = new AutocompleteAddressRequest(input)
            {
                RegionCode = countryCode
            };
            var autocompleteAddressResponse = await _googleAddressApiService.AutocompleteAddress(autocompleteAddressRequest);

            return autocompleteAddressResponse;
        }

        public async Task<ValidateAddressResponse> ValidateAddress(string input, string countryCode)
        {
            var request = getGoogleValidationRequest(input, countryCode);
            var response = await _googleAddressApiService.ValidateAddress(request);

            return response;
        }

        public async Task<ValidateAddressResponse> ValidateAddress(StructuredInput input)
        {
            var request = getGoogleValidationRequest(input);
            var response = await _googleAddressApiService.ValidateAddress(request);

            return response;
        }

        public string[] GetVerdictString(Verdict verdict)
        {
            return [$"InputGranularity: {verdict.InputGranularity}",
                $"ValidationGranularity: {verdict.ValidationGranularity}",
                $"GeocodeGranularity: {verdict.GeocodeGranularity}",
                $"AddressComplete: {verdict.AddressComplete}",
                $"HasUnconfirmedComponents: {verdict.HasUnconfirmedComponents}",
                $"HasInferredComponents: {verdict.HasInferredComponents}",
                $"HasReplacedComponents: {verdict.HasReplacedComponents}"];
        }

        public string[] GetComponentsComfirmationString(RepeatedField<AddressComponent> componets)
        {
            var result = componets.Select(c => $"{c.ComponentName.Text} : {c.ConfirmationLevel}").ToArray();

            return result;
        }

        private ValidateAddressRequest getGoogleValidationRequest(StructuredInput structuredInput)
        {
            var address = new PostalAddress()
            {
                RegionCode = structuredInput.CountryCode2,
                Locality = structuredInput.City,
                PostalCode = structuredInput.PostalCode,
                Sublocality = structuredInput.District
            };

            address.AddressLines.Add(structuredInput.Input);

            var addressValidationRequest = new ValidateAddressRequest
            {
                Address = address
            };

            return addressValidationRequest;
        }

        private ValidateAddressRequest getGoogleValidationRequest(string input, string countryCode)
        {
            var address = new PostalAddress()
            {
                RegionCode = countryCode
            };

            address.AddressLines.Add(input);

            var addressValidationRequest = new ValidateAddressRequest
            {
                Address = address
            };

            return addressValidationRequest;
        }
    }
}
