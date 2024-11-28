using SmartyStreets;

namespace CheckAddressApp.Services.Api
{
    public class SmartyAddressApiService
    {
        private string _authId;
        private string _authToken;
        private SmartyStreets.InternationalAutocompleteApi.Client _internationalAutocompleteClient;
        private SmartyStreets.USAutocompleteApi.Client _usAutocompleteClient;
        private SmartyStreets.InternationalStreetApi.Client _internationalStreeClient;
        private SmartyStreets.USStreetApi.Client _usStreeClient;

        public SmartyAddressApiService(string authId, string authToken)
        {
            _authId = authId;
            _authToken = authToken;

            _internationalAutocompleteClient = new ClientBuilder(_authId, _authToken).BuildInternationalAutocompleteApiClient();
            _usAutocompleteClient = new ClientBuilder(_authId, _authToken).BuildUsAutocompleteApiClient();
            _internationalStreeClient = new ClientBuilder(_authId, _authToken).BuildInternationalStreetApiClient();
            _usStreeClient = new ClientBuilder(_authId, _authToken).BuildUsStreetApiClient();
        }

        public void AutocompleteAddress(SmartyStreets.USAutocompleteApi.Lookup lookup)
        {
            _usAutocompleteClient.Send(lookup);
        }

        public void AutocompleteAddress(SmartyStreets.InternationalAutocompleteApi.Lookup lookup)
        {
            _internationalAutocompleteClient.Send(lookup);
        }

        public void ValidateUSAddress(SmartyStreets.USStreetApi.Lookup lookup)
        {
            _usStreeClient.Send(lookup);
        }

        public void ValidateInternationalAddress(SmartyStreets.InternationalStreetApi.Lookup lookup)
        {
            _internationalStreeClient.Send(lookup);
        }
    }
}
