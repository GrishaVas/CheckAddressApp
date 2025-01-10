using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using CheckAddressApp.Models.Loqate;

namespace CheckAddressApp.Services.Api
{
    public class LoqateAddressApiService : BaseAddressApiService, IDisposable
    {
        private const string _baseAddress = "https://api.addressy.com/";
        private HttpClient _httpClient;

        public LoqateAddressApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_baseAddress);
        }

        public async Task<GetAddressDetailsResponse> GetAddressDetails(AddressDetailsRequest request)
        {
            var detailsUrl = getDetailsUrl(request);
            var response = await _httpClient.GetAsync(detailsUrl);
            var validateAddressResponse = await getResult<GetAddressDetailsResponse>(response);

            return validateAddressResponse;
        }

        public async Task<List<ValidateAddressResponse>> ValidateAddress(ValidateAddressRequest request)
        {
            if (request == null)
            {
                throw new Exception("Validate address request cannot be null");
            }

            var jsonContent = JsonContent.Create(request);
            var response = await _httpClient.PostAsync("Cleansing/International/Batch/v1.00/json4.ws", jsonContent);
            var validateAddressResponse = await getResult<List<ValidateAddressResponse>>(response);

            return validateAddressResponse;
        }

        public async Task<AutocompleteAddressResponse> AutocompleteAddress(AutocompleteAddressRequest request)
        {
            var jsonContent = JsonContent.Create(request);
            var url = getAutocompleteUrl(request);
            var response = await _httpClient.PostAsync(url, jsonContent);
            var validateAddressResponses = await getAutocompleteAddressResponseResult(response);

            return validateAddressResponses;
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

        protected override async Task<TResult> getResult<TResult>(HttpResponseMessage response) where TResult : class
        {
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new Exception($"Provider return Bad Request.");
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                var contentAsString = await response.Content.ReadAsStringAsync();

                throw new Exception($"Status code: {response.StatusCode}. Content: {contentAsString}");
            }

            var jsonString = await response.Content.ReadAsStringAsync();
            TResult result = null;
            AddressResponseErrorItem error = null;

            try
            {
                result = JsonSerializer.Deserialize<TResult>(jsonString);
            }
            catch (Exception)
            {
                error = JsonSerializer.Deserialize<AddressResponseErrorItem>(jsonString);
            }

            if (error != null)
            {
                throw new Exception($"{error.Description}");
            }

            return result;
        }

        protected async Task<AutocompleteAddressResponse> getAutocompleteAddressResponseResult(HttpResponseMessage response)
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                var contentAsString = await response.Content.ReadAsStringAsync();

                throw new Exception($"Status code: {response.StatusCode}. Content: {contentAsString}");
            }

            var jsonString = await response.Content.ReadAsStringAsync();
            AutocompleteAddressResponse result = null;
            AutocompleteAddressResponseError error = null;

            try
            {
                var autocompleteAddressResponseError = JsonSerializer.Deserialize<AutocompleteAddressResponseError>(jsonString);

                if (!string.IsNullOrEmpty(autocompleteAddressResponseError.Items.FirstOrDefault()?.Error))
                {
                    error = autocompleteAddressResponseError;
                }
                else
                {
                    result = JsonSerializer.Deserialize<AutocompleteAddressResponse>(jsonString);
                }
            }
            catch (Exception)
            {
                result = JsonSerializer.Deserialize<AutocompleteAddressResponse>(jsonString);
            }

            if (error != null)
            {
                throw new Exception($"{error.Items.FirstOrDefault().Description}");
            }

            return result;
        }

        private string getDetailsUrl(AddressDetailsRequest request)
        {
            if (request == null)
            {
                throw new Exception("Address details request cannot be null.");
            }

            var result = $"Capture/Interactive/Retrieve/v1.2/json3.ws?Key={request.Key}&Id={request.Id}";

            return result;
        }

        private string getAutocompleteUrl(AutocompleteAddressRequest request)
        {
            if (request == null)
            {
                throw new Exception("Autocomplete address request cannot be null.");
            }

            var url = "Capture/Interactive/Find/v1.1/json3.ws";

            url += $"?Key={System.Web.HttpUtility.UrlEncode(request.Key)}" +
                $"&Text={System.Web.HttpUtility.UrlEncode(request.Text)}" +
                $"&Container={System.Web.HttpUtility.UrlEncode(request.Container)}" +
                $"&Origin={System.Web.HttpUtility.UrlEncode(request.Origin)}";

            return url;
        }
    }
}
