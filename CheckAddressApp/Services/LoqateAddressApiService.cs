using System.Net.Http.Json;
using CheckAddressApp.Models.Loqate;

namespace CheckAddressApp.Services
{
    public class LoqateAddressApiService : IDisposable
    {
        private const string _baseVaidationAddress = "https://api.addressy.com/";
        private const string _baseAutocompleteAddress = "https://api.everythinglocation.com/";
        private HttpClient _autocompleteAddressHttpClient;
        private HttpClient _vaidationAddressHttpClient;

        public LoqateAddressApiService()
        {
            _vaidationAddressHttpClient = new HttpClient();
            _autocompleteAddressHttpClient = new HttpClient();
            _vaidationAddressHttpClient.BaseAddress = new Uri(_baseVaidationAddress);
            _autocompleteAddressHttpClient.BaseAddress = new Uri(_baseAutocompleteAddress);
        }

        public async Task<List<ValidateAddressResponse>> ValidateAddress(ValidateAddressRequest request)
        {
            var jsonContent = JsonContent.Create(request);
            var response = await _vaidationAddressHttpClient.PostAsync("Cleansing/International/Batch/v1.00/json4.ws", jsonContent);
            var validateAddressResponse = await response.Content.ReadFromJsonAsync<List<ValidateAddressResponse>>();

            return validateAddressResponse;
        }

        public async Task<AutocompleteAddressResponse> AutocompleteAddress(AutocompleteAddressRequest request)
        {
            var jsonContent = JsonContent.Create(request);
            var response = await _autocompleteAddressHttpClient.PostAsync("address/complete", jsonContent);
            var validateAddressResponses = await response.Content.ReadFromJsonAsync<AutocompleteAddressResponse>();

            return validateAddressResponses;
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

        public void Dispose()
        {
            _vaidationAddressHttpClient.Dispose();
            _autocompleteAddressHttpClient.Dispose();

            GC.SuppressFinalize(this);
        }
        ~LoqateAddressApiService()
        {
            _vaidationAddressHttpClient.Dispose();
            _autocompleteAddressHttpClient.Dispose();
        }
    }
}
