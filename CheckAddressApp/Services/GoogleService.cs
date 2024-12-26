using CheckAddressApp.Models;
using CheckAddressApp.Models.Google.Autocomplete;
using CheckAddressApp.Models.Google.Details;
using CheckAddressApp.Services.Api;
using CheckAddressWeb.Models.Google.Validation;
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
                RegionCode = input.Country != null ? input.Country.TwoLetterCode : ""
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
                var detailsResponse = await getPlaceDetails(id, "*");

                if (detailsResponse == null)
                {
                    throw new Exception("Details address response is null.");
                }

                var checkAddressDataItem = new CheckAddressData
                {
                    Address = detailsResponse.FormattedAddress,
                    Fields = getFields(detailsResponse).ToArray()
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

        private async Task<PlaceDetailsResponse> getPlaceDetails(string placeId, string fields)
        {
            var place = await _googleAddressApiService.GetPlaceDetails(placeId, fields);

            return place;
        }

        private List<CheckAddressData> getCheckAddressData(ValidateAddressResponse validateAddressResponse)
        {
            if (validateAddressResponse == null)
            {
                throw new Exception("Validate address response cannot be null.");
            }

            var checkAddressDataItem = new CheckAddressData
            {
                Address = validateAddressResponse.Result?.Address?.FormattedAddress,
                Fields = getFields(validateAddressResponse.Result).ToArray()
            };
            var checkAddressData = new List<CheckAddressData>
            {
                checkAddressDataItem
            };

            return checkAddressData;
        }

        private ValidateAddressRequest getValidationRequest(CheckAddressInput input)
        {
            if (input == null)
            {
                throw new ArgumentException("Input cannot be null.");
            }

            var address = new ValidateAddressRequestPostalAddress()
            {
                RegionCode = input.Country != null ? input.Country.TwoLetterCode : ""
            };

            if (input.StructuredInput != null)
            {
                address.Locality = input.StructuredInput.City;
                address.PostalCode = input.StructuredInput.PostalCode;
                address.Sublocality = input.StructuredInput.District;
            }

            address.AddressLines = [input.FreeInput];

            var addressValidationRequest = new ValidateAddressRequest
            {
                Address = address
            };

            return addressValidationRequest;
        }
    }
}
