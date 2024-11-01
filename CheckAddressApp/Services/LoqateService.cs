using CheckAddressApp.Models;
using CheckAddressApp.Models.Loqate;
using CheckAddressApp.Services.Api;
using Microsoft.Extensions.Configuration;

namespace CheckAddressApp.Services
{
    public class LoqateService : IDisposable
    {
        private LoqateAddressApiService _loqateAddressApiService;
        private IConfiguration _conf;

        public LoqateService(IConfiguration conf)
        {
            _loqateAddressApiService = new LoqateAddressApiService();
            _conf = conf;
        }

        public async Task<AutocompleteAddressResponse> AutocompleteAddress(string input, string countryCode)
        {
            var autocompleteAddressRequest = new AutocompleteAddressRequest
            {
                Key = _conf["Loqate:ApiKey"],
                Text = input,
                Origin = countryCode
            };
            var autocompleteAddressResponse = await _loqateAddressApiService.AutocompleteAddress(autocompleteAddressRequest);

            return autocompleteAddressResponse;
        }

        public async Task<List<ValidateAddressResponse>> ValidateAddress(StructuredInput input)
        {
            var request = getLoqateValidationAddress(input);
            var responses = await _loqateAddressApiService.ValidateAddress(request);

            return responses;
        }

        public async Task<List<ValidateAddressResponse>> ValidateAddress(string input, string coutryCode)
        {
            var request = getLoqateValidationAddress(input, coutryCode);
            var responses = await _loqateAddressApiService.ValidateAddress(request);

            return responses;
        }

        public string GetMatchVerificationLavel(string AQI)
        {
            switch (AQI)
            {
                case "A":
                    return "Excellent";
                case "B":
                    return "Good";
                case "C":
                    return "Average";
                case "D":
                    return "Poor";
                case "E":
                    return "Bad";
                default:
                    return "Bad";
            }
        }

        private ValidateAddressRequest getLoqateValidationAddress(StructuredInput input)
        {
            var request = new ValidateAddressRequest
            {
                Key = _conf["Loqate:ApiKey"],
                Addresses = new List<ValidateAddressRequestAddress>
                {
                    new ValidateAddressRequestAddress
                    {
                        Address = input.Input,
                        Locality = input.City,
                        DependentLocality = input.District,
                        PostalCode = input.PostalCode,
                        Country = input.CountryCode2,
                        Address1 = input.StreetAndHouseNumber
                    }
                }
            };

            return request;
        }

        private ValidateAddressRequest getLoqateValidationAddress(string input, string countryCode)
        {
            var request = new ValidateAddressRequest
            {
                Key = _conf["Loqate:ApiKey"],
                Addresses = new List<ValidateAddressRequestAddress>
                {
                    new ValidateAddressRequestAddress
                    {
                        Address = input,
                        Country = countryCode
                    }
                }
            };

            return request;
        }

        public void Dispose()
        {
            _loqateAddressApiService.Dispose();

            GC.SuppressFinalize(this);
        }
        ~LoqateService()
        {
            _loqateAddressApiService.Dispose();
        }
    }
}
