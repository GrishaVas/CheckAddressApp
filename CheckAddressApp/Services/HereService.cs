using CheckAddressApp.Models;
using CheckAddressApp.Models.Here;
using CheckAddressApp.Services.Api;
using Microsoft.Extensions.Configuration;

namespace CheckAddressApp.Services
{
    public class HereService : IDisposable
    {
        private HereAddressApiService _hereAddressApiService;

        public HereService(IConfiguration conf)
        {
            _hereAddressApiService = new HereAddressApiService(conf["Here:ApiKey"]);
        }

        public async Task<AutosuggestAddressResponse> AutosuggestAddress(string input, string countryCode, string lat, string lng)
        {
            var at = lat + "," + lng;
            var hereAutosuggestRequest = new AutosuggestAddressRequest(input, at)
            {
                In = countryCode
            };
            var hereAutocompleteResponse = await _hereAddressApiService.AutosuggestAddress(hereAutosuggestRequest);

            return hereAutocompleteResponse;
        }

        public async Task<AutocompleteAddressResponse> AutocompleteAddress(string input, string countryCode)
        {
            var hereAutocompleteRequest = new AutocompleteAddressRequest(input)
            {
                In = countryCode
            };
            var hereAutocompleteResponse = await _hereAddressApiService.AutocompleteAddress(hereAutocompleteRequest);

            return hereAutocompleteResponse;
        }

        public async Task<ValidateAddressResponse> ValidateAddress(StructuredInput input)
        {
            var request = getHereValidationAddress(input);
            var validateAddresResponse = await _hereAddressApiService.ValidateAddress(request);

            return validateAddresResponse;
        }

        public async Task<ValidateAddressResponse> ValidateAddress(string input, string countryCode)
        {
            var request = getHereValidationAddress(input, countryCode);
            var validateAddresResponse = await _hereAddressApiService.ValidateAddress(request);

            return validateAddresResponse;
        }

        private ValidateAddressRequest getHereValidationAddress(StructuredInput input)
        {
            var validateAddressRequest = new ValidateAddressRequest(input.Input)
            {
                In = input.CountryCode3
            };

            return validateAddressRequest;
        }

        private ValidateAddressRequest getHereValidationAddress(string input, string countryCode)
        {
            var validateAddressRequest = new ValidateAddressRequest(input)
            {
                In = countryCode
            };

            return validateAddressRequest;
        }

        public void Dispose()
        {
            _hereAddressApiService.Dispose();

            GC.SuppressFinalize(this);
        }

        ~HereService()
        {
            _hereAddressApiService.Dispose();
        }
    }
}
