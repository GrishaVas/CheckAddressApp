using System.Net.Http.Json;
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

        private string getAutocompleteUrl(string url, AutocompleteAddressRequest request)
        {
            url += $"?Key={System.Web.HttpUtility.UrlEncode(request.Key)}";
            url += $"&Text={System.Web.HttpUtility.UrlEncode(request.Text)}";
            url += $"&Origin={System.Web.HttpUtility.UrlEncode(request.Origin)}";

            return url;
        }
    }
}
