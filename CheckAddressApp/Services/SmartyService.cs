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

        public IEnumerable<CheckAddressData> AutocompleteAddress(string input, string countryCode)
        {
            if (input == null)
            {
                throw new ArgumentException("Input cannot be null.");
            }

            if (countryCode == "US")
            {
                var lookup = new SmartyStreets.USAutocompleteApi.Lookup(input);

                _smartyAddressApiService.AutocompleteAddress(lookup);
                var suggestions = lookup.Result ?? [];

                var checkAddressData = suggestions.Select(s => new CheckAddressData
                {
                    Address = s.Text,
                    Fields = getFields(s).ToArray()
                });

                return checkAddressData;
            }
            else
            {
                var lookup = new SmartyStreets.InternationalAutocompleteApi.Lookup(input)
                {
                    Country = countryCode
                };

                _smartyAddressApiService.AutocompleteAddress(lookup);

                var candidats = lookup.Result ?? [];

                var checkAddressData = candidats.Select(c => new CheckAddressData
                {
                    Address = c.AddressText,
                    Fields = getFields(c).ToArray()
                });

                return checkAddressData;
            }
        }

        public IEnumerable<CheckAddressData> ValidateAddress(StructuredInput input)
        {
            if (input == null)
            {
                throw new ArgumentException("StructuredInput cannot be null.");
            }

            IEnumerable<CheckAddressData> checkAddressData;

            if (input.CountryCode2 == "US")
            {
                var lookup = new SmartyStreets.USStreetApi.Lookup(input.Input)
                {
                    ZipCode = input.PostalCode,
                    City = input.City,
                    Urbanization = input.District,
                    Street2 = input.StreetAndHouseNumber,
                };

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
                var lookup = new SmartyStreets.InternationalStreetApi.Lookup(input.Input, input.CountryCode2)
                {
                    PostalCode = input.PostalCode,
                    Locality = input.City,
                    Address1 = input.StreetAndHouseNumber,
                    Address2 = input.District
                };
                _smartyAddressApiService.ValidateInternationalAddress(lookup);

                var candidats = lookup.Result ?? [];

                checkAddressData = candidats.Select(c => new CheckAddressData
                {
                    Address = $"{c.Address1} {c.Address2} {c.Address3}",
                    Fields = getFields(c).ToArray()
                });
            }

            return checkAddressData;
        }

        public IEnumerable<CheckAddressData> ValidateAddress(string input, string countryCode)
        {
            if (input == null)
            {
                throw new ArgumentException("Input cannot be null.");
            }

            IEnumerable<CheckAddressData> checkAddressData;

            if (countryCode == "US")
            {
                var lookup = new SmartyStreets.USStreetApi.Lookup(input);

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
                var lookup = new SmartyStreets.InternationalStreetApi.Lookup(input, countryCode);

                _smartyAddressApiService.ValidateInternationalAddress(lookup);

                var candidats = lookup.Result ?? [];

                checkAddressData = candidats.Select(c => new CheckAddressData
                {
                    Address = $"{c.Address1} {c.Address2} {c.Address3}",
                    Fields = getFields(c).ToArray()
                });
            }

            return checkAddressData;
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
