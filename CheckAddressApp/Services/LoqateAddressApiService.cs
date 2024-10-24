using System.Net.Http.Json;
using CheckAddressApp.Models.Loqate;

namespace CheckAddressApp.Services
{
    public class LoqateAddressApiService : IDisposable
    {
        private const string _baseAddress = "https://api.addressy.com/";
        private HttpClient _httpClient;

        public LoqateAddressApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_baseAddress);
        }

        public async Task<List<ValidateAddressResponse>> ValidateAddress(ValidateAddressRequest request)
        {
            var jsonContent = JsonContent.Create(request);
            var response = await _httpClient.PostAsync("Cleansing/International/Batch/v1.00/json4.ws", jsonContent);
            var str = await response.Content.ReadAsStringAsync();
            var validateAddressResponse = await response.Content.ReadFromJsonAsync<List<ValidateAddressResponse>>();

            return validateAddressResponse;
        }

        public async Task<AutocompleteAddressResponse> AutocompleteAddress(AutocompleteAddressRequest request)
        {
            var jsonContent = JsonContent.Create(request);
            var url = getAutocompleteUrl("Capture/Interactive/Find/v1.1/json3.ws", request);
            var response = await _httpClient.PostAsync(url, jsonContent);
            var str = await response.Content.ReadAsStringAsync();
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
            _httpClient.Dispose();

            GC.SuppressFinalize(this);
        }
        ~LoqateAddressApiService()
        {
            _httpClient.Dispose();
        }

        private string getAutocompleteUrl(string url, AutocompleteAddressRequest request)
        {
            url += $"?Key={System.Web.HttpUtility.UrlEncode(request.Key)}";
            url += $"&Text={System.Web.HttpUtility.UrlEncode(request.Text)}";
            url += $"&Origin={System.Web.HttpUtility.UrlEncode(request.Origin)}";

            return url;
        }
    }
}
