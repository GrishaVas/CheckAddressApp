using System.Net.Http.Headers;
using System.Net.Http.Json;
using CheckAddressApp.Models.Google;
using CheckAddressApp.Models.Google.Autocomplete;
using Google.Api.Gax.Grpc;
using Google.Maps.AddressValidation.V1;
using Google.Maps.Places.V1;

namespace CheckAddressApp.Services.Api
{
    public class GoogleAddressApiService
    {
        private string _apiKey;
        private string _clientId;
        private string _clientSecret;
        private string _refreshToken;
        private AccessTokenResponse _accessTokenResponse;
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
            var addressValidationClientBuilder = new AddressValidationClientBuilder();

            addressValidationClientBuilder.CredentialsPath = "application_default_credentials.json";

            var addressValidationClient = await addressValidationClientBuilder.BuildAsync();
            var response = await addressValidationClient.ValidateAddressAsync(request);

            return response;
        }

        public async Task<AutocompleteAddressResponse> AutocompleteAddress(AutocompleteAddressRequest request)
        {
            using var httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(_autocompleteEndpoint);

            await addAuth(httpClient);

            var jsonContent = JsonContent.Create(request);
            var response = await httpClient.PostAsync("", jsonContent);
            var autoCompleteAddressResponse = await response.Content.ReadFromJsonAsync<AutocompleteAddressResponse>();

            return autoCompleteAddressResponse;
        }

        public async Task<Place> GetPlaceDetails(string placeId, string fields)
        {
            var placesClientBuilder = new PlacesClientBuilder();

            placesClientBuilder.CredentialsPath = "application_default_credentials.json";

            var placesClient = placesClientBuilder.Build();
            var callSettings = CallSettings.FromHeader("X-Goog-FieldMask", fields);
            var request = new GetPlaceRequest
            {
                Name = $"places/{placeId}"
            };
            var response = await placesClient.GetPlaceAsync(request, callSettings);

            return response;
        }

        private string getPlacesDetailsUrl(string placeId, string fields)
        {
            var url = _placeDetailsEndpoint +
                $"{placeId}?fields={fields}&key={_apiKey}";

            return url;
        }

        private async Task addAuth(HttpClient httpClient)
        {
            var accessTokenResponse = await getAccessToken();

            httpClient.DefaultRequestHeaders.Add("X-Goog-Api-Key", _apiKey);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessTokenResponse.access_token);
            httpClient.DefaultRequestHeaders.Add("X-Goog-User-Project", "checkaddressapp");
        }

        private async Task<AccessTokenResponse> getAccessToken()
        {
            if (_accessTokenResponse != null)
            {
                return _accessTokenResponse;
            }

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
            var accessTokenResponse = await response.Content.ReadFromJsonAsync<AccessTokenResponse>();

            return accessTokenResponse;
        }
    }
}
