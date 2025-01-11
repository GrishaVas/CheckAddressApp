using CheckAddressApp.Models.Loqate;
using CheckAddressApp.Services.Api;
using Microsoft.Extensions.Configuration;
using qAcProviderTest.Models.CheckAddressServiceModels;

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
            var getAddressDetailsRequest = new AddressDetailsRequest
            {
                Id = placeId,
                Key = _apiKey
            };
            var addressDetails = await _loqateAddressApiService.GetAddressDetails(getAddressDetailsRequest);

            return addressDetails;
        }

        public override Task<IEnumerable<CheckAddressAddressData>> AutosuggestAddress(CheckAddressInput input)
        {
            throw new NotImplementedException();
        }

        public override async Task<IEnumerable<CheckAddressAddressData>> AutocompleteAddress(CheckAddressInput input)
        {
            if (input == null)
            {
                throw new ArgumentException("Input cannot be null.");
            }

            var autocompleteAddressRequest = new AutocompleteAddressRequest
            {
                Key = _apiKey,
                Text = input.FullString,
                Origin = input.Country != null ? input.Country.TwoLetterCode : ""
            };
            var autocompleteAddressResponse = await _loqateAddressApiService.AutocompleteAddress(autocompleteAddressRequest);

            if (autocompleteAddressResponse == null)
            {
                throw new Exception("Autocomplete address response is null.");
            }

            var placeIds = await getAddressIds(autocompleteAddressResponse);
            var addressDetails = await Task.WhenAll(placeIds.Take(5).Select(pId => GetAddressDetails(pId)));
            var checkAddressData = addressDetails.Select(ad => new CheckAddressAddressData
            {
                Address = ad.Items.FirstOrDefault().Label,
                Fields = getFields(ad.Items.FirstOrDefault()).ToArray()
            });

            return checkAddressData;
        }

        public override async Task<IEnumerable<CheckAddressAddressData>> ValidateAddress(CheckAddressInput input)
        {
            if (input == null)
            {
                throw new ArgumentException("Input cannot be null.");
            }

            var request = getValidationAddress(input);
            var responses = await _loqateAddressApiService.ValidateAddress(request);
            var response = responses.FirstOrDefault();

            if (response == null)
            {
                throw new Exception("Validate address response cannot be null.");
            }

            var checkAddressData = response.Matches.Where(m => !string.IsNullOrEmpty(m.Address)).Select(m => new CheckAddressAddressData
            {
                Address = m.Address,
                Fields = getFields(m).ToArray()
            });

            return checkAddressData;
        }

        private async Task<List<string>> getAddressIds(AutocompleteAddressResponse response)
        {
            if (response == null)
            {
                throw new Exception("Autocomplete adress response cannot be null.");
            }

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
                    var autocompleteAddressResponse = await _loqateAddressApiService.AutocompleteAddress(autocompleteAddressRequest);

                    if (autocompleteAddressResponse == null)
                    {
                        throw new Exception("Autocomplete address response is null.");
                    }

                    var ids = autocompleteAddressResponse.Items.Select(i => i.Id);

                    resultIds.AddRange(ids);
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

            var countryCode = input.Country != null ? input.Country.TwoLetterCode : "";
            var request = new ValidateAddressRequest
            {
                Key = _apiKey,
                Addresses = new List<ValidateAddressRequestAddress>()
            };
            ValidateAddressRequestAddress validateAddressRequest;

            if (input is CheckAddressStructuredInput)
            {
                var structuredInput = (CheckAddressStructuredInput)input;

                validateAddressRequest = new ValidateAddressRequestAddress
                {
                    Address = structuredInput.FullString,
                    Locality = structuredInput.City,
                    DependentLocality = structuredInput.District,
                    PostalCode = structuredInput.PostalCode,
                    Country = countryCode,
                    Address1 = structuredInput.StreetAndHouseNumber
                };
            }
            else
            {
                validateAddressRequest = new ValidateAddressRequestAddress
                {
                    Address = input.FullString,
                    Country = countryCode
                };
            }

            request.Addresses.Add(validateAddressRequest);

            return request;
        }

        ~LoqateService()
        {
            _loqateAddressApiService.Dispose();
        }
    }
}
