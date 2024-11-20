using CheckAddressApp.Models;
using CheckAddressApp.Services.Api;
using Microsoft.Extensions.Configuration;

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

        public override Task<ServiceData> AutocompleteAddress(CheckAddressInput input)
        {
            if (input == null)
            {
                throw new ArgumentException("Input cannot be null.");
            }

            var countryCode = !string.IsNullOrEmpty(input.Country) ? getCountryCode(input.Country).ISO2 : null;
            IEnumerable<CheckAddressData> checkAddressData;

            if (countryCode == "US")
            {
                var lookup = new SmartyStreets.USAutocompleteApi.Lookup(input.FreeInput);

                _smartyAddressApiService.AutocompleteAddress(lookup);
                var suggestions = lookup.Result ?? [];

                checkAddressData = suggestions.Select(s => new CheckAddressData
                {
                    Address = s.Text,
                    Fields = getFields(s).ToArray()
                });
            }
            else
            {
                var lookup = new SmartyStreets.InternationalAutocompleteApi.Lookup(input.FreeInput)
                {
                    Country = countryCode
                };

                _smartyAddressApiService.AutocompleteAddress(lookup);

                var candidats = lookup.Result ?? [];

                checkAddressData = candidats.Select(c => new CheckAddressData
                {
                    Address = c.AddressText,
                    Fields = getFields(c).ToArray()
                });
            }

            var serviceData = new ServiceData
            {
                ServiceName = "SmartyService",
                CheckAddressData = checkAddressData
            };
            var task = Task.FromResult(serviceData);

            return task;
        }

        public override Task<ServiceData> AutosuggestAddress(CheckAddressInput input)
        {
            throw new NotImplementedException();
        }

        public override Task<ServiceData> ValidateAddress(CheckAddressInput input)
        {
            if (input == null)
            {
                throw new ArgumentException("Input cannot be null.");
            }

            IEnumerable<CheckAddressData> checkAddressData;
            var countryCode = !string.IsNullOrEmpty(input.Country) ? getCountryCode(input.Country).ISO2 : null;

            if (countryCode == "US")
            {
                var lookup = getUSLookup(input);

                _smartyAddressApiService.ValidateUSAddress(lookup);

                var candidats = lookup.Result ?? [];

                checkAddressData = candidats.Select(c => new CheckAddressData
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

                checkAddressData = candidats.Select(c => new CheckAddressData
                {
                    Address = $"{c.Address1} {c.Address2} {c.Address3}",
                    Fields = getFields(c).ToArray()
                });
            }

            var serviceData = new ServiceData
            {
                ServiceName = "SmartyService",
                CheckAddressData = checkAddressData
            };
            var task = Task.FromResult(serviceData);

            return task;
        }

        private SmartyStreets.USStreetApi.Lookup getUSLookup(CheckAddressInput input)
        {
            if (input == null)
            {
                throw new ArgumentException("Input cannot be null.");
            }

            var lookup = new SmartyStreets.USStreetApi.Lookup(input.FreeInput);

            if (input.StructuredInput != null)
            {
                lookup.ZipCode = input.StructuredInput.PostalCode;
                lookup.City = input.StructuredInput.City;
                lookup.Urbanization = input.StructuredInput.District;
                lookup.Street2 = input.StructuredInput.StreetAndHouseNumber;
            }

            return lookup;
        }
        private SmartyStreets.InternationalStreetApi.Lookup getInternationalLookup(CheckAddressInput input)
        {
            if (input == null)
            {
                throw new ArgumentException("Input cannot be null.");
            }

            var countryCode = !string.IsNullOrEmpty(input.Country) ? getCountryCode(input.Country).ISO2 : null;
            var lookup = new SmartyStreets.InternationalStreetApi.Lookup(input.FreeInput, countryCode);

            if (input.StructuredInput != null)
            {
                lookup.PostalCode = input.StructuredInput.PostalCode;
                lookup.Locality = input.StructuredInput.City;
                lookup.Address2 = input.StructuredInput.District;
                lookup.Address1 = input.StructuredInput.StreetAndHouseNumber;
            }

            return lookup;
        }

        private IEnumerable<CheckAddressField> getFields(SmartyStreets.InternationalStreetApi.Candidate candidate)
        {
            var fields = new List<CheckAddressField>();

            fields.AddRange(base.getFields(candidate));

            fields.AddRange(base.getFields(candidate.Components, "Components"));

            return fields;
        }
        private IEnumerable<CheckAddressField> getFields(SmartyStreets.USStreetApi.Candidate candidate)
        {
            var fields = new List<CheckAddressField>();

            fields.AddRange(base.getFields(candidate));

            fields.AddRange(base.getFields(candidate.Components, "Components"));

            return fields;
        }
    }
}
