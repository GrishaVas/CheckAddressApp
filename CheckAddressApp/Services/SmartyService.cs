using CheckAddressApp.Models;
using CheckAddressApp.Services.Api;
using Microsoft.Extensions.Configuration;

namespace CheckAddressApp.Services
{
    public class SmartyService
    {
        private SmartyAddressApiService _smartyAddressApiService;
        private IConfiguration _conf;

        public SmartyService(IConfiguration conf)
        {
            _smartyAddressApiService = new SmartyAddressApiService(
                conf["Smarty:AuthId"],
                conf["Smarty:AuthToken"]);
            _conf = conf;
        }

        public string[] AutocompleteAddress(string input, string countryCode)
        {
            var smartyAutocompleteResponse = _smartyAddressApiService.AutocompleteAddress(input, countryCode);

            return smartyAutocompleteResponse;
        }

        public SmartyStreets.USStreetApi.Lookup ValidateUSAddress(StructuredInput input)
        {
            var lookup = new SmartyStreets.USStreetApi.Lookup(input.Input)
            {
                ZipCode = input.PostalCode,
                City = input.City,
                Urbanization = input.District,
                Street2 = input.StreetAndHouseNumber
            };
            _smartyAddressApiService.ValidateUSAddress(lookup);

            return lookup;
        }

        public SmartyStreets.USStreetApi.Lookup ValidateUSAddress(string input)
        {
            var lookup = new SmartyStreets.USStreetApi.Lookup(input);

            _smartyAddressApiService.ValidateUSAddress(lookup);

            return lookup;
        }

        public SmartyStreets.InternationalStreetApi.Lookup ValidateInternationalAddress(StructuredInput input)
        {
            var lookup = new SmartyStreets.InternationalStreetApi.Lookup(input.Input, input.CountryCode2)
            {
                PostalCode = input.PostalCode,
                Locality = input.City,
                Address1 = input.StreetAndHouseNumber,
                Address2 = input.District
            };
            _smartyAddressApiService.ValidateInternationalAddress(lookup);

            return lookup;
        }

        public SmartyStreets.InternationalStreetApi.Lookup ValidateInternationalAddress(string input, string countryCode)
        {
            var lookup = new SmartyStreets.InternationalStreetApi.Lookup(input, countryCode);

            _smartyAddressApiService.ValidateInternationalAddress(lookup);

            return lookup;
        }
    }
}
