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

            if (hereValidateResponse == null)
            {
                throw new Exception($"Here validate response is null.");
            }

            var checkAddressData = new List<CheckAddressData>();
            var hereValidateResponseItems = hereValidateResponse.Items ?? [];

            foreach (var item in hereValidateResponseItems.Take(5))
            {
                var hereAutosuggestResponse = await getAutosuggestAddress(input.FreeInput, countryCode, item.Position);
                var ids = hereAutosuggestResponse.Items.Select(i => i.Id);

                foreach (var id in ids)
                {
                    var hereLookupRequest = new LookupAddressRequest(id);
                    var hereLookupResponse = await _hereAddressApiService.LookupAddress(hereLookupRequest);
                    var checkAddressDataItem = getCheckAddressData(hereLookupResponse);

                    if (checkAddressDataItem != null)
                    {
                        checkAddressData.Add(checkAddressDataItem);
                    }
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

                if (hereLookupResponse == null)
                {
                    throw new Exception("Here lookup response is null.");
                }

                var checkAddressDataItem = new CheckAddressData
                {
                    Address = hereLookupResponse.Address.Label,
                    Fields = getFields(hereLookupResponse).ToArray()
                };

                checkAddressData.Add(checkAddressDataItem);
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

        public void Dispose()
        {
            _hereAddressApiService.Dispose();

            GC.SuppressFinalize(this);
        }

        private async Task<IEnumerable<LookupAddressResponse>> getLookupResponses(string[] ids)
        {
            var responses = new List<LookupAddressResponse>();

            foreach (var id in ids)
            {
                var hereLookupRequest = new LookupAddressRequest(id);
                var hereLookupResponse = await _hereAddressApiService.LookupAddress(hereLookupRequest);

                responses.Add(hereLookupResponse);
            }

            return responses;
        }

        private CheckAddressData getCheckAddressData(LookupAddressResponse lookupAddresResponse)
        {
            if (lookupAddresResponse == null)
            {
                throw new Exception("Lookup address response is null.");
            }

            var checkAddressData = new CheckAddressData
            {
                Address = lookupAddresResponse.Address.Label,
                Fields = getFields(lookupAddresResponse).ToArray()
            };

            return checkAddressData;
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

        private IEnumerable<CheckAddressData> getCheckAddressData(AutosuggestAddressResponse autosuggestAddressResponse)
        {
            if (autosuggestAddressResponse == null)
            {
                throw new Exception("Autosuggest address response is null.");
            }

            var checkAddressData = autosuggestAddressResponse.Items?.Select(i => new CheckAddressData
            {
                Address = i.Address.Label,
                Fields = getFields(i).ToArray()
            }) ?? [];

            return checkAddressData;
        }

        private async Task<AutosuggestAddressResponse> getAutosuggestAddress(string input, string countryCode, ValidateAddressResponsePosition position)
        {
            if (position == null)
            {
                throw new Exception("Position cannot be null.");
            }

            var lng = getStringOfDouble(position.Lng);
            var lat = getStringOfDouble(position.Lat);
            var at = lat + "," + lng;
            var hereAutosuggestRequest = new AutosuggestAddressRequest(input, at)
            {
                In = countryCode
            };
            var response = await _hereAddressApiService.AutosuggestAddress(hereAutosuggestRequest);

            return response;
        }

        private ValidateAddressRequest getValidationRequest(CheckAddressInput input)
        {
            if (input == null)
            {
                throw new ArgumentException("Input cannot be null.");
            }

            var validateAddressRequest = new ValidateAddressRequest(input.FreeInput)
            {
                In = !string.IsNullOrEmpty(input.Country) ? getCountryCode(input.Country).ISO3 : null
            };

            return validateAddressRequest;
        }

        private string getStringOfDouble(double value)
        {
            var sep = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            var stringValue = value.ToString().Replace(sep, ".");

            return stringValue;
        }

        ~HereService()
        {
            _hereAddressApiService.Dispose();
        }
    }
}
