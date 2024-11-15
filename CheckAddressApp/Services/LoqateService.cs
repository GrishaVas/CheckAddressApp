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

        public async Task<IEnumerable<CheckAddressData>> AutocompleteAddress(string input, string countryCode)
        {
            if (input == null)
            {
                throw new ArgumentException("Input cannot be null.");
            }

            var autocompleteAddressRequest = new AutocompleteAddressRequest
            {
                Key = _apiKey,
                Text = input,
                Origin = countryCode
            };
            var autocompleteAddressResponse = await _loqateAddressApiService.AutocompleteAddress(autocompleteAddressRequest);

            if (autocompleteAddressResponse == null)
            {
                throw new Exception("Autocomplete address response is null.");
            }

            var placeIds = await getAddressIds(autocompleteAddressResponse);
            var addressDetails = await Task.WhenAll(placeIds.Take(5).Select(pId => GetAddressDetails(pId)));
            var checkAddressDataList = addressDetails.Select(ad => new CheckAddressData
            {
                Address = ad.Items.FirstOrDefault().Label,
                Fields = getFields(ad.Items.FirstOrDefault()).ToArray()
            });

            return checkAddressDataList;
        }

        public async Task<IEnumerable<CheckAddressData>> ValidateAddress(StructuredInput input)
        {
            if (input == null)
            {
                throw new ArgumentException("StruvturedInput cannot be null.");
            }

            var request = getLoqateValidationAddress(input);
            var responses = await _loqateAddressApiService.ValidateAddress(request);
            var checkAddressData = responses.FirstOrDefault()?.Matches?.Where(m => !string.IsNullOrEmpty(m.Address)).Select(m => new CheckAddressData
            {
                Address = m.Address,
                Fields = getFields(m).ToArray()
            }) ?? [];

            return checkAddressData;
        }

        public async Task<IEnumerable<CheckAddressData>> ValidateAddress(string input, string coutryCode)
        {
            if (input == null)
            {
                throw new ArgumentException("Input cannot be null.");
            }

            var request = getLoqateValidationAddress(input, coutryCode);
            var responses = await _loqateAddressApiService.ValidateAddress(request);
            var checkAddressData = responses.FirstOrDefault()?.Matches?.Select(m => new CheckAddressData
            {
                Address = m.Address,
                Fields = getFields(m).ToArray()
            }) ?? [];

            return checkAddressData;
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

        private ValidateAddressRequest getLoqateValidationAddress(StructuredInput input)
        {
            var request = new ValidateAddressRequest
            {
                Key = _apiKey,
                Addresses = new List<ValidateAddressRequestAddress>
                {
                    new ValidateAddressRequestAddress
                    {
                        Address = input.Input,
                        Locality = input.City,
                        DependentLocality = input.District,
                        PostalCode = input.PostalCode,
                        Country = input.CountryCode2,
                        Address1 = input.StreetAndHouseNumber
                    }
                }
            };

            return request;
        }

        private ValidateAddressRequest getLoqateValidationAddress(string input, string countryCode)
        {
            var request = new ValidateAddressRequest
            {
                Key = _apiKey,
                Addresses = new List<ValidateAddressRequestAddress>
                {
                    new ValidateAddressRequestAddress
                    {
                        Address = input,
                        Country = countryCode
                    }
                }
            };

            return request;
        }
        ~LoqateService()
        {
            _loqateAddressApiService.Dispose();
        }
    }
}
