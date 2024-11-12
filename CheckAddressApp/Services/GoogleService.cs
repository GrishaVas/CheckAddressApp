using CheckAddressApp.Models;
using CheckAddressApp.Models.Google.Autocomplete;
using CheckAddressApp.Services.Api;
using Google.Maps.AddressValidation.V1;
using Google.Maps.Places.V1;
using Google.Type;
using Microsoft.Extensions.Configuration;

namespace CheckAddressApp.Services
{
    public class GoogleService : BaseService
    {
        private GoogleAddressApiService _googleAddressApiService;

        public GoogleService(IConfiguration conf)
        {
            _googleAddressApiService = new GoogleAddressApiService(conf["Google:ApiKey"],
                conf["Google:ClientId"],
                conf["Google:ClientSecret"],
                conf["Google:RefreshToken"]);
        }

        public async Task<Place> GetPlaceDetails(string placeId, string fields)
        {
            var place = await _googleAddressApiService.GetPlaceDetails(placeId, fields);

            return place;
        }

        public async Task<IEnumerable<CheckAddressData>> AutocompleteAddress(string input, string countryCode)
        {
            var autocompleteAddressRequest = new AutocompleteAddressRequest(input)
            {
                RegionCode = countryCode
            };
            var autocompleteAddressResponse = await _googleAddressApiService.AutocompleteAddress(autocompleteAddressRequest);
            var addressIds = autocompleteAddressResponse.Suggestions.Select(s => s.PlacePrediction.PlaceId);
            var checkAddresData = new List<CheckAddressData>();

            foreach (var id in addressIds)
            {
                var details = await GetPlaceDetails(id, "*");
                var checkAddressDataItem = new CheckAddressData
                {
                    Address = details.FormattedAddress,
                    Fields = getFields(details).ToArray()
                };

                checkAddresData.Add(checkAddressDataItem);
            }

            return checkAddresData;
        }

        public async Task<IEnumerable<CheckAddressData>> ValidateAddress(string input, string countryCode)
        {
            var request = getGoogleValidationRequest(input, countryCode);
            var response = await _googleAddressApiService.ValidateAddress(request);
            var checkAddressDataItem = new CheckAddressData
            {
                Address = response.Result.Address.FormattedAddress,
                Fields = getFields(response.Result).ToArray()
            };
            var checkAddressData = new List<CheckAddressData>
            {
                checkAddressDataItem
            };

            return checkAddressData;
        }

        public async Task<IEnumerable<CheckAddressData>> ValidateAddress(StructuredInput input)
        {
            var request = getGoogleValidationRequest(input);
            var response = await _googleAddressApiService.ValidateAddress(request);
            var checkAddressDataItem = new CheckAddressData
            {
                Address = response.Result.Address.FormattedAddress,
                Fields = getFields(response.Result).ToArray()
            };
            var checkAddressData = new List<CheckAddressData>
            {
                checkAddressDataItem
            };

            return checkAddressData;
        }

        private IEnumerable<CheckAddressField> getFields(ValidationResult validationResult)
        {
            var fields = new List<CheckAddressField>();

            fields.AddRange(base.getFields(validationResult.Address));
            fields.AddRange(base.getFields(validationResult.Geocode));
            fields.AddRange(base.getFields(validationResult.Metadata));

            var components = validationResult.Address.AddressComponents;

            for (int i = 0; i < components.Count; i++)
            {
                fields.AddRange(getFields(components[i]).Select(f => new CheckAddressField
                {
                    Name = $"AddressComponents[{i}].{f.Name}",
                    Value = f.Value
                }));
            }

            return fields;
        }

        private ValidateAddressRequest getGoogleValidationRequest(StructuredInput structuredInput)
        {
            var address = new PostalAddress()
            {
                RegionCode = structuredInput.CountryCode2,
                Locality = structuredInput.City,
                PostalCode = structuredInput.PostalCode,
                Sublocality = structuredInput.District
            };

            address.AddressLines.Add(structuredInput.Input);

            var addressValidationRequest = new ValidateAddressRequest
            {
                Address = address
            };

            return addressValidationRequest;
        }

        private ValidateAddressRequest getGoogleValidationRequest(string input, string countryCode)
        {
            var address = new PostalAddress()
            {
                RegionCode = countryCode
            };

            address.AddressLines.Add(input);

            var addressValidationRequest = new ValidateAddressRequest
            {
                Address = address
            };

            return addressValidationRequest;
        }
    }
}
