using CheckAddressApp.Services.Api;
using Microsoft.Extensions.Configuration;
using qAcProviderTest.Models.CheckAddressServiceModels;

namespace CheckAddressApp.Services
{
    public class SmartyService : BaseService
    {
        private SmartyAddressApiService _smartyAddressApiService;

        public SmartyService(IConfiguration conf)
        {
            _smartyAddressApiService = new SmartyAddressApiService(
                conf["Smarty:AuthId"],
                conf["Smarty:AuthToken"]);
        }

        public override Task<IEnumerable<CheckAddressAddressData>> AutocompleteAddress(CheckAddressInput input)
        {
            if (input == null)
            {
                throw new ArgumentException("Input cannot be null.");
            }

            var countryCode = input.Country != null ? input.Country.TwoLetterCode : "";
            IEnumerable<CheckAddressAddressData> checkAddressData;

            if (countryCode == "US")
            {
                var lookup = new SmartyStreets.USAutocompleteApi.Lookup(input.FullString);

                _smartyAddressApiService.AutocompleteAddress(lookup);
                var suggestions = lookup.Result ?? [];

                checkAddressData = suggestions.Select(s => new CheckAddressAddressData
                {
                    Address = s.Text,
                    Fields = getFields(s).ToArray()
                });
            }
            else
            {
                var lookup = new SmartyStreets.InternationalAutocompleteApi.Lookup(input.FullString)
                {
                    Country = input.Country.TwoLetterCode
                };

                _smartyAddressApiService.AutocompleteAddress(lookup);

                var candidats = lookup.Result ?? [];

                checkAddressData = candidats.Select(c => new CheckAddressAddressData
                {
                    Address = c.AddressText,
                    Fields = getFields(c).ToArray()
                });
            }

            var task = Task.FromResult(checkAddressData);

            return task;
        }

        public override Task<IEnumerable<CheckAddressAddressData>> AutosuggestAddress(CheckAddressInput input)
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<CheckAddressAddressData>> ValidateAddress(CheckAddressInput input)
        {
            if (input == null)
            {
                throw new ArgumentException("Input cannot be null.");
            }

            IEnumerable<CheckAddressAddressData> checkAddressData;
            var countryCode = input.Country != null ? input.Country.TwoLetterCode : "";

            if (countryCode == "US")
            {
                var lookup = getUSLookup(input);

                _smartyAddressApiService.ValidateUSAddress(lookup);

                var candidats = lookup.Result ?? [];

                checkAddressData = candidats.Select(c => new CheckAddressAddressData
                {
                    Address = $"{c.DeliveryLine1} {c.DeliveryLine2} {c.LastLine}",
                    Fields = getFields(c).ToArray()
                });
            }
            else
            {
                var lookup = getInternationalLookup(input);
                _smartyAddressApiService.ValidateInternationalAddress(lookup);

                var candidats = lookup.Result ?? [];

                checkAddressData = candidats.Select(c => new CheckAddressAddressData
                {
                    Address = $"{c.Address1} {c.Address2} {c.Address3}",
                    Fields = getFields(c).ToArray()
                });
            }

            var task = Task.FromResult(checkAddressData);

            return task;
        }

        private SmartyStreets.USStreetApi.Lookup getUSLookup(CheckAddressInput input)
        {
            if (input == null)
            {
                throw new ArgumentException("Input cannot be null.");
            }

            var lookup = new SmartyStreets.USStreetApi.Lookup(input.FullString);

            if (input is CheckAddressStructuredInput)
            {
                var structuredInput = (CheckAddressStructuredInput)input;

                lookup.ZipCode = structuredInput.PostalCode;
                lookup.City = structuredInput.City;
                lookup.Urbanization = structuredInput.District;
                lookup.Street2 = structuredInput.StreetAndHouseNumber;
            }

            return lookup;
        }
        private SmartyStreets.InternationalStreetApi.Lookup getInternationalLookup(CheckAddressInput input)
        {
            if (input == null)
            {
                throw new ArgumentException("Input cannot be null.");
            }

            var lookup = new SmartyStreets.InternationalStreetApi.Lookup(input.FullString, input.Country.TwoLetterCode);

            if (input is CheckAddressStructuredInput)
            {
                var structuredInput = (CheckAddressStructuredInput)input;

                lookup.PostalCode = structuredInput.PostalCode;
                lookup.Locality = structuredInput.City;
                lookup.Address2 = structuredInput.District;
                lookup.Address1 = structuredInput.StreetAndHouseNumber;
            }

            return lookup;
        }
    }
}
