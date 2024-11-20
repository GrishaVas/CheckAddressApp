using CheckAddressApp.Models.Here;

namespace CheckAddressApp.Services.Api
{
    public class HereAddressApiService : BaseAddressApiService, IDisposable
    {
        private HttpClient _httpClient;
        private string _apiKey;

        public HereAddressApiService(string apiKey)
        {
            _httpClient = new HttpClient();
            _apiKey = apiKey;
        }

        public async Task<LookupAddressResponse> LookupAddress(LookupAddressRequest request)
        {
            var url = getLookupUrl(request);
            var response = await _httpClient.GetAsync(url);
            var result = await getResult<LookupAddressResponse>(response);

            return result;
        }

        public async Task<ValidateAddressResponse> ValidateAddress(ValidateAddressRequest request)
        {
            var url = getValidationUrl(request);
            var response = await _httpClient.GetAsync(url);
            var result = await getResult<ValidateAddressResponse>(response);

            return result;
        }

        public async Task<AutocompleteAddressResponse> AutocompleteAddress(AutocompleteAddressRequest request)
        {
            var url = getAutocompleteUrl(request);
            var response = await _httpClient.GetAsync(url);
            var result = await getResult<AutocompleteAddressResponse>(response);

            return result;
        }

        public async Task<AutosuggestAddressResponse> AutosuggestAddress(AutosuggestAddressRequest request)
        {
            var url = getAutosuggestUrl(request);
            var response = await _httpClient.GetAsync(url);
            var result = await getResult<AutosuggestAddressResponse>(response);

            return result;
        }

        public void Dispose()
        {
            _httpClient.Dispose();

            GC.SuppressFinalize(this);
        }

        private string getLookupUrl(LookupAddressRequest request)
        {
            var url = "https://lookup.search.hereapi.com/v1/lookup";

            url += $"?apiKey={_apiKey}" +
                $"&id={request.Id}";

            return url;
        }

        private string getValidationUrl(ValidateAddressRequest request)
        {
            var url = "https://geocode.search.hereapi.com/v1/geocode";

            url += $"?apiKey={_apiKey}" +
                $"&q={request.Q}" +
                (!string.IsNullOrEmpty(request.In) ? $"&in={request.In}" : "");

            return url;
        }

        private string getAutocompleteUrl(AutocompleteAddressRequest request)
        {
            var url = "https://autocomplete.search.hereapi.com/v1/autocomplete";

            url += $"?apiKey={_apiKey}" +
                $"&q={request.Q.Trim().Replace(' ', '+')}" +
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

        ~HereAddressApiService()
        {
            _httpClient.Dispose();
        }
    }
}
