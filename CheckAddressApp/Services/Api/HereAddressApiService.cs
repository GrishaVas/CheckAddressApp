using System.Net.Http.Json;
using CheckAddressApp.Models.Here;

namespace CheckAddressApp.Services.Api
{
    public class HereAddressApiService
    {
        private HttpClient _httpClient;
        private string _apiKey;

        public HereAddressApiService(string apiKey)
        {
            _httpClient = new HttpClient();
            _apiKey = apiKey;
        }

        public async Task<ValidateAddressResponse> ValidateAddress(ValidateAddressRequest request)
        {
            var url = getValidationUrl(request);
            var response = await _httpClient.GetAsync(url);
            var result = await response.Content.ReadFromJsonAsync<ValidateAddressResponse>();

            return result;
        }

        public async Task<AutocompleteAddressResponse> AutocompleteAddress(AutocompleteAddressRequest request)
        {
            var url = getAutocompleteUrl(request);
            var response = await _httpClient.GetAsync(url);
            var result = await response.Content.ReadFromJsonAsync<AutocompleteAddressResponse>();

            return result;
        }

        public async Task<AutosuggestAddressResponse> AutosuggestAddress(AutosuggestAddressRequest request)
        {
            var url = getAutosuggestUrl(request);
            var response = await _httpClient.GetAsync(url);
            var result = await response.Content.ReadFromJsonAsync<AutosuggestAddressResponse>();

            return result;
        }
        private string getValidationUrl(ValidateAddressRequest request)
        {
            var url = "https://discover.search.hereapi.com/v1/geocode";

            url += $"?apiKey={_apiKey}" +
                $"&q={request.Q}" +
                (!string.IsNullOrEmpty(request.In) ? $"&in={request.In}" : "");

            return url;
        }
        private string getAutocompleteUrl(AutocompleteAddressRequest request)
        {
            var url = "https://autocomplete.search.hereapi.com/v1/autocomplete";

            url += $"?apiKey={_apiKey}" +
                $"&q={request.Q}" +
                (!string.IsNullOrEmpty(request.In) ? $"&in={request.In}" : "");

            return url;
        }

        private string getAutosuggestUrl(AutosuggestAddressRequest request)
        {
            var url = "https://autosuggest.search.hereapi.com/v1/autosuggest";

            url += $"?apiKey={_apiKey}" +
                $"&q={request.Q}" +
                $"&at={request.At}" +
                (!string.IsNullOrEmpty(request.In) ? $"&in={request.In}" : "");

            return url;
        }
    }
}
