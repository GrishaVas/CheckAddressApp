using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using CheckAddressApp.Models.Google;
using CheckAddressApp.Models.Google.Autocomplete;
using CheckAddressApp.Models.Google.Details;
using CheckAddressWeb.Models.Google.Validation;

namespace CheckAddressApp.Services.Api
{
    public class GoogleAddressApiService : BaseAddressApiService
    {
        private string _apiKey;
        private string _clientId;
        private string _clientSecret;
        private string _refreshToken;
        private const string _validateEndpoint = "https://addressvalidation.googleapis.com/v1:validateAddress";
        private const string _autocompleteEndpoint = "https://places.googleapis.com/v1/places:autocomplete";
        private const string _placeDetailsEndpoint = "https://places.googleapis.com/v1/places/";

        public GoogleAddressApiService(string apiKey, string clientId, string clientSecret, string refreshToken)
        {
            _apiKey = apiKey;
            _clientId = clientId;
            _clientSecret = clientSecret;
            _refreshToken = refreshToken;
        }

        public async Task<ValidateAddressResponse> ValidateAddress(ValidateAddressRequest request)
        {
            using var httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(_validateEndpoint);

            await authenticate(httpClient);

            var jsonContent = getJsonContent(request);
            var response = await httpClient.PostAsync("", jsonContent);
            var validateAddressResponse = await getResult<ValidateAddressResponse>(response);

            return validateAddressResponse;
        }

        public async Task<AutocompleteAddressResponse> AutocompleteAddress(AutocompleteAddressRequest request)
        {
            using var httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(_autocompleteEndpoint);

            await authenticate(httpClient);

            var jsonContent = getJsonContent(request);
            var response = await httpClient.PostAsync("", jsonContent);
            var autoCompleteAddressResponse = await response.Content.ReadFromJsonAsync<AutocompleteAddressResponse>();

            return autoCompleteAddressResponse;
        }

        public async Task<PlaceDetailsResponse> GetPlaceDetails(string placeId, string fields)
        {
            using var httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(getPlacesDetailsUrl(placeId));
            httpClient.DefaultRequestHeaders.Add("X-Goog-FieldMask", "*");

            await authenticate(httpClient);

            var response = await httpClient.GetAsync("");
            var placeDetailsResponse = await getResult<PlaceDetailsResponse>(response);

            return placeDetailsResponse;
        }

        protected override async Task<TResult> getResult<TResult>(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new Exception($"Provider return Bad Request.");
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {

                ErrorResponseRoot error;

                try
                {
                    error = await response.Content.ReadFromJsonAsync<ErrorResponseRoot>();
                }
                catch
                {
                    error = null;
                }

                if (error != null)
                {
                    throw new Exception(error.Error.Message);
                }

                var contentAsString = await response.Content.ReadAsStringAsync();

                throw new Exception($"Error while receiving response. Status code: {response.StatusCode}. Content: {contentAsString}");
            }

            var result = await response.Content.ReadFromJsonAsync<TResult>();

            return result;
        }

        private JsonContent getJsonContent<TRequest>(TRequest request)
        {
            if (request == null)
            {
                throw new Exception("Request cannot be null.");
            }

            var jsonContent = JsonContent.Create(request);

            return jsonContent;
        }

        private string getPlacesDetailsUrl(string placeId)
        {
            if (string.IsNullOrEmpty(placeId))
            {
                throw new Exception("Place id cannot be null or empty.");
            }

            var url = _placeDetailsEndpoint +
                $"{placeId}?&key={_apiKey}";

            return url;
        }

        private async Task authenticate(HttpClient httpClient)
        {
            if (httpClient == null)
            {
                throw new Exception("Http client cannot be null.");
            }

            var accessTokenResponse = await getAccessToken();

            httpClient.DefaultRequestHeaders.Add("X-Goog-Api-Key", _apiKey);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessTokenResponse.access_token);
            httpClient.DefaultRequestHeaders.Add("X-Goog-User-Project", "checkaddressapp");
        }

        private async Task<AccessTokenResponse> getAccessToken()
        {
            using var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://oauth2.googleapis.com/token")
            };
            var body = new
            {
                client_id = _clientId,
                client_secret = _clientSecret,
                grant_type = "refresh_token",
                refresh_token = _refreshToken
            };
            var jsonContent = JsonContent.Create(body);
            var response = await httpClient.PostAsync("", jsonContent);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Error while receiving accessToken.");
            }

            var accessTokenResponse = await response.Content.ReadFromJsonAsync<AccessTokenResponse>();

            return accessTokenResponse;
        }
    }
}
