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

        private List<CheckAddressData> getCheckAddressData(ValidateAddressResponse validateAddressResponse)
        {
            if (validateAddressResponse == null)
            {
                throw new Exception("Validate address response is null.");
            }

            var checkAddressDataItem = new CheckAddressData
            {
                Address = validateAddressResponse.Result.Address.FormattedAddress,
                Fields = getFields(validateAddressResponse.Result).ToArray()
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
        private ValidateAddressRequest getValidationRequest(CheckAddressInput input)
        {
            if (input == null)
            {
                throw new ArgumentException("Input cannot be null.");
            }

            var address = new PostalAddress()
            {
                RegionCode = !string.IsNullOrEmpty(input.Country) ? getCountryCode(input.Country).ISO2 : ""
            };

            if (input.StructuredInput != null)
            {
                address.Locality = input.StructuredInput.City;
                address.PostalCode = input.StructuredInput.PostalCode;
                address.Sublocality = input.StructuredInput.District;
            }

            address.AddressLines.Add(input.FreeInput);

            var addressValidationRequest = new ValidateAddressRequest
            {
                Address = address
            };

            return addressValidationRequest;
        }

        public override Task<ServiceData> AutosuggestAddress(CheckAddressInput input)
        {
            throw new NotImplementedException();
        }

        public override async Task<ServiceData> AutocompleteAddress(CheckAddressInput input)
        {
            if (input == null)
            {
                throw new ArgumentException("Input cannot be null.");
            }

            var request = new AutocompleteAddressRequest(input.FreeInput)
            {
                RegionCode = !string.IsNullOrEmpty(input.Country) ? getCountryCode(input.Country).ISO2 : ""
            };

            var autocompleteAddressResponse = await _googleAddressApiService.AutocompleteAddress(request);

            if (autocompleteAddressResponse == null)
            {
                throw new Exception("Autocomplete address response is null.");
            }

            var addressIds = autocompleteAddressResponse.Suggestions?.Select(s => s.PlacePrediction.PlaceId) ?? [];
            var checkAddressData = new List<CheckAddressData>();

            foreach (var id in addressIds)
            {
                var details = await GetPlaceDetails(id, "*");
                var checkAddressDataItem = new CheckAddressData
                {
                    Address = details.FormattedAddress,
                    Fields = getFields(details).ToArray()
                };

                checkAddressData.Add(checkAddressDataItem);
            }

            var serviceData = new ServiceData
            {
                ServiceName = "GoogleService",
                CheckAddressData = checkAddressData
            };

            return serviceData;
        }

        public override async Task<ServiceData> ValidateAddress(CheckAddressInput input)
        {
            if (input == null)
            {
                throw new ArgumentException("Input cannot be null.");
            }

            var request = getValidationRequest(input);
            var validateAddressResponse = await _googleAddressApiService.ValidateAddress(request);
            var checkAddressData = getCheckAddressData(validateAddressResponse);
            var serviceData = new ServiceData
            {
                ServiceName = "GoogleService",
                CheckAddressData = checkAddressData
            };

            return serviceData;
        }
    }
}
