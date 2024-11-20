using CheckAddressApp.Models;
using CheckAddressApp.Models.Loqate;
using CheckAddressApp.Services.Api;
using Microsoft.Extensions.Configuration;

namespace CheckAddressApp.Services
{
    public class LoqateService : BaseService, IDisposable
    {
        private LoqateAddressApiService _loqateAddressApiService;
        private string _apiKey;

        public LoqateService(IConfiguration conf)
        {
            _loqateAddressApiService = new LoqateAddressApiService();
            _apiKey = conf["Loqate:ApiKey"];
        }

        public void Dispose()
        {
            _loqateAddressApiService.Dispose();

            GC.SuppressFinalize(this);
        }

        public async Task<GetAddressDetailsResponse> GetAddressDetails(string placeId)
        {
            var getAddressDetailsRequest = new GetAddressDetailsRequest
            {
                Id = placeId,
                Key = _apiKey
            };
            var addressDetails = await _loqateAddressApiService.GetAddressDetails(getAddressDetailsRequest);

            return addressDetails;
        }

        private async Task<List<string>> getAddressIds(AutocompleteAddressResponse response)
        {
            var resultIds = new List<string>();

            foreach (var item in response.Items)
            {
                if (item.Type != "Address")
                {
                    var autocompleteAddressRequest = new AutocompleteAddressRequest
                    {
                        Container = item.Id,
                        Key = _apiKey
                    };
                    var autocompleteContainerResponse = await _loqateAddressApiService.AutocompleteAddress(autocompleteAddressRequest);
                    var id = autocompleteContainerResponse.Items.First().Id;

                    resultIds.Add(id);
                }
                else
                {
                    resultIds.Add(item.Id);
                }
            }

            return resultIds;
        }

        private ValidateAddressRequest getValidationAddress(CheckAddressInput input)
        {
            if (input == null)
            {
                throw new ArgumentException("Input cannot be null.");
            }

            var countryCode = !string.IsNullOrEmpty(input.Country) ? getCountryCode(input.Country).ISO2 : null;
            var request = new ValidateAddressRequest
            {
                Key = _apiKey,
                Addresses = new List<ValidateAddressRequestAddress>()
            };
            ValidateAddressRequestAddress validateAddressRequest;

            if (input.StructuredInput == null)
            {
                validateAddressRequest = new ValidateAddressRequestAddress
                {
                    Address = input.FreeInput,
                    Country = countryCode
                };
            }
            else
            {
                validateAddressRequest = new ValidateAddressRequestAddress
                {
                    Address = input.FreeInput,
                    Locality = input.StructuredInput.City,
                    DependentLocality = input.StructuredInput.District,
                    PostalCode = input.StructuredInput.PostalCode,
                    Country = countryCode,
                    Address1 = input.StructuredInput.StreetAndHouseNumber
                };
            }

            request.Addresses.Add(validateAddressRequest);

            return request;
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

            var autocompleteAddressRequest = new AutocompleteAddressRequest
            {
                Key = _apiKey,
                Text = input.FreeInput,
                Origin = !string.IsNullOrEmpty(input.Country) ? getCountryCode(input.Country).ISO2 : null
            };
            var autocompleteAddressResponse = await _loqateAddressApiService.AutocompleteAddress(autocompleteAddressRequest);

            if (autocompleteAddressResponse == null)
            {
                throw new Exception("Autocomplete address response is null.");
            }

            var placeIds = await getAddressIds(autocompleteAddressResponse);
            var addressDetails = await Task.WhenAll(placeIds.Take(5).Select(pId => GetAddressDetails(pId)));
            var checkAddressData = addressDetails.Select(ad => new CheckAddressData
            {
                Address = ad.Items.FirstOrDefault().Label,
                Fields = getFields(ad.Items.FirstOrDefault()).ToArray()
            });

            var serviceData = new ServiceData
            {
                ServiceName = "LoqateService",
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

            var request = getValidationAddress(input);
            var responses = await _loqateAddressApiService.ValidateAddress(request);
            var checkAddressData = responses.FirstOrDefault()?.Matches?.Where(m => !string.IsNullOrEmpty(m.Address)).Select(m => new CheckAddressData
            {
                Address = m.Address,
                Fields = getFields(m).ToArray()
            }) ?? [];

            var serviceData = new ServiceData
            {
                ServiceName = "LoqateService",
                CheckAddressData = checkAddressData
            };

            return serviceData;
        }

        ~LoqateService()
        {
            _loqateAddressApiService.Dispose();
        }
    }
}
