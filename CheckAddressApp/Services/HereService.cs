using CheckAddressApp.Models;
using CheckAddressApp.Models.Here;
using CheckAddressApp.Services.Api;
using Microsoft.Extensions.Configuration;

namespace CheckAddressApp.Services
{
    public class HereService : BaseService, IDisposable
    {
        private HereAddressApiService _hereAddressApiService;

        public HereService(IConfiguration conf)
        {
            _hereAddressApiService = new HereAddressApiService(conf["Here:ApiKey"]);
        }

        private IEnumerable<CheckAddressData> getCheckAddressData(ValidateAddressResponse validateAddresResponse)
        {
            if (validateAddresResponse == null)
            {
                throw new Exception("Validate address response is null.");
            }

            var checkAddressData = validateAddresResponse.Items?.Select(i => new CheckAddressData
            {
                Address = i.Address.Label,
                Fields = getFields(i).ToArray()
            }) ?? [];

            return checkAddressData;
        }

        private Task<AutosuggestAddressResponse> getAutosuggestAddress(string input, string countryCode, string lng, string lat)
        {
            var at = lat + "," + lng;
            var hereAutosuggestRequest = new AutosuggestAddressRequest(input, at)
            {
                In = countryCode
            };

            return _hereAddressApiService.AutosuggestAddress(hereAutosuggestRequest);
        }

        public void Dispose()
        {
            _hereAddressApiService.Dispose();

            GC.SuppressFinalize(this);
        }

        private IEnumerable<CheckAddressField> getFields(LookupAddressResponse response)
        {
            var fields = new List<CheckAddressField>();

            fields.AddRange(base.getFields(response));

            fields.AddRange(base.getFields(response.Address, "Address"));

            return fields;
        }


        private IEnumerable<CheckAddressField> getFields(AutosuggestAddressResponseItem response)
        {
            var fields = new List<CheckAddressField>();

            fields.AddRange(base.getFields(response));

            fields.AddRange(base.getFields(response.Address, "Address"));

            return fields;
        }

        private IEnumerable<CheckAddressField> getFields(ValidateAddressResponseItem item)
        {
            var fields = new List<CheckAddressField>();

            fields.AddRange(base.getFields(item));

            fields.AddRange(base.getFields(item.Address, "Address"));

            fields.AddRange(base.getFields(item.Position, "Position"));

            return fields;
        }

        public override async Task<ServiceData> AutosuggestAddress(CheckAddressInput input)
        {
            if (input == null)
            {
                throw new ArgumentException("Input cannot be null.");
            }

            var countryCode = !string.IsNullOrEmpty(input.Country) ? getCountryCode(input.Country).ISO3 : null;
            var hereValidateRequest = new ValidateAddressRequest(input.FreeInput)
            {
                In = countryCode
            };
            var hereValidateResponse = await _hereAddressApiService.ValidateAddress(hereValidateRequest);

            if (hereValidateResponse.Items == null)
            {
                throw new Exception($"Here validate response items are null.");
            }

            var checkAddressData = new List<CheckAddressData>();

            foreach (var item in hereValidateResponse.Items.Take(5))
            {
                var hereAutosuggestResponse = await getAutosuggestAddress(input.FreeInput, countryCode, item.Position.Lng.ToString(), item.Position.Lat.ToString());
                var checkAddressDataItems = hereAutosuggestResponse?.Items?.Select(i => new CheckAddressData
                {
                    Address = i.Address.Label,
                    Fields = getFields(i).ToArray()
                });

                if (checkAddressDataItems != null)
                {
                    checkAddressData.AddRange(checkAddressDataItems);
                }
            }

            var serviceData = new ServiceData
            {
                ServiceName = "HereService",
                CheckAddressData = checkAddressData
            };

            return serviceData;
        }

        public override async Task<ServiceData> AutocompleteAddress(CheckAddressInput input)
        {
            if (input == null)
            {
                throw new ArgumentException("Input cannot be null.");
            }

            var hereAutocompleteRequest = new AutocompleteAddressRequest(input.FreeInput)
            {
                In = !string.IsNullOrEmpty(input.Country) ? getCountryCode(input.Country).ISO3 : null
            };
            var hereAutocompleteResponse = await _hereAddressApiService.AutocompleteAddress(hereAutocompleteRequest);
            var addressIds = hereAutocompleteResponse?.Items?.Select(i => i.Id);
            var checkAddressData = new List<CheckAddressData>();

            if (addressIds == null)
            {
                throw new Exception("Address ids is null.");
            }

            foreach (var id in addressIds)
            {
                var hereLookupRequest = new LookupAddressRequest(id);
                var hereLookupResponse = await _hereAddressApiService.LookupAddress(hereLookupRequest);

                if (hereLookupResponse != null)
                {
                    var checkAddressDataItem = new CheckAddressData
                    {
                        Address = hereLookupResponse.Address.Label,
                        Fields = getFields(hereLookupResponse).ToArray()
                    };

                    checkAddressData.Add(checkAddressDataItem);
                }
            }

            var serviceData = new ServiceData
            {
                ServiceName = "HereService",
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
            var validateAddressResponse = await _hereAddressApiService.ValidateAddress(request);
            var checkAddressData = getCheckAddressData(validateAddressResponse);
            var serviceData = new ServiceData
            {
                ServiceName = "HereService",
                CheckAddressData = checkAddressData
            };

            return serviceData;
        }

        private ValidateAddressRequest getValidationRequest(CheckAddressInput input)
        {
            var validateAddressRequest = new ValidateAddressRequest(input.FreeInput)
            {
                In = !string.IsNullOrEmpty(input.Country) ? getCountryCode(input.Country).ISO3 : null
            };

            return validateAddressRequest;
        }

        ~HereService()
        {
            _hereAddressApiService.Dispose();
        }
    }
}
