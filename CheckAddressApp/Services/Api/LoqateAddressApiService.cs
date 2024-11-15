using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using CheckAddressApp.Models.Loqate;

namespace CheckAddressApp.Services.Api
{
    public class LoqateAddressApiService : BaseApiService, IDisposable
    {
        private const string _baseAddress = "https://api.addressy.com/";
        private HttpClient _httpClient;

        public LoqateAddressApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_baseAddress);
        }

        public async Task<GetAddressDetailsResponse> GetAddressDetails(GetAddressDetailsRequest request)
        {
            var response = await _httpClient.GetAsync($"Capture/Interactive/Retrieve/v1.2/json3.ws?Key={request.Key}&Id={request.Id}");
            var validateAddressResponse = await getResult<GetAddressDetailsResponse>(response);

            return validateAddressResponse;
        }

        public async Task<List<ValidateAddressResponse>> ValidateAddress(ValidateAddressRequest request)
        {
            var jsonContent = JsonContent.Create(request);
            var response = await _httpClient.PostAsync("Cleansing/International/Batch/v1.00/json4.ws", jsonContent);
            var validateAddressResponse = await getResult<List<ValidateAddressResponse>>(response);

            return validateAddressResponse;
        }

        public async Task<AutocompleteAddressResponse> AutocompleteAddress(AutocompleteAddressRequest request)
        {
            var jsonContent = JsonContent.Create(request);
            var url = getAutocompleteUrl("Capture/Interactive/Find/v1.1/json3.ws", request);
            var response = await _httpClient.PostAsync(url, jsonContent);
            var validateAddressResponses = await getResult<AutocompleteAddressResponse>(response);

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

        protected override async Task<TResult> getResult<TResult>(HttpResponseMessage response)
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                var contentAsString = await response.Content.ReadAsStringAsync();

                throw new Exception($"Status code: {response.StatusCode}. Content: {contentAsString}");
            }

            TResult result;
            var jsonString = await response.Content.ReadAsStringAsync();

            try
            {
                result = JsonSerializer.Deserialize<TResult>(jsonString);
            }
            catch (Exception)
            {
                var error = JsonSerializer.Deserialize<ErrorResponse>(jsonString);

                throw new Exception($"{error.Description}");
            }


            return result;
        }

        private string getAutocompleteUrl(string url, AutocompleteAddressRequest request)
        {
            url += $"?Key={System.Web.HttpUtility.UrlEncode(request.Key)}";
            url += $"&Text={System.Web.HttpUtility.UrlEncode(request.Text)}";
            url += $"&Container={System.Web.HttpUtility.UrlEncode(request.Container)}";
            url += $"&Origin={System.Web.HttpUtility.UrlEncode(request.Origin)}";

            return url;
        }
    }
}
