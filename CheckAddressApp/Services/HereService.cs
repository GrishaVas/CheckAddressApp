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

        public async Task<IEnumerable<CheckAddressData>> AutosuggestAddress(string input, string countryCode)
        {
            if (!string.IsNullOrEmpty(countryCode) && countryCode.Length != 3)
            {
                throw new ArgumentException("Wrong country code.");
            }

            var hereValidateRequest = new ValidateAddressRequest(input)
            {
                In = countryCode
            };
            var hereValidateResponse = await _hereAddressApiService.ValidateAddress(hereValidateRequest);
            var item = hereValidateResponse.Items.FirstOrDefault();

            if (item == null)
            {
                throw new Exception($"Can't find {input}");
            }

            var lat = item.Position.Lat.ToString();
            var lng = item.Position.Lng.ToString();
            var at = lat + "," + lng;
            var hereAutosuggestRequest = new AutosuggestAddressRequest(input, at)
            {
                In = countryCode
            };
            var hereAutosuggestResponse = await _hereAddressApiService.AutosuggestAddress(hereAutosuggestRequest);
            var checkAddressData = hereAutosuggestResponse.Items.Select(i => new CheckAddressData
            {
                Address = i.Address.Label,
                Fields = getFields(i).ToArray()
            });

            return checkAddressData;
        }

        public async Task<IEnumerable<CheckAddressData>> AutocompleteAddress(string input, string countryCode)
        {
            if (!string.IsNullOrEmpty(countryCode) && countryCode.Length != 3)
            {
                throw new ArgumentException("Wrong country code.");
            }

            var hereAutocompleteRequest = new AutocompleteAddressRequest(input)
            {
                In = countryCode
            };
            var hereAutocompleteResponse = await _hereAddressApiService.AutocompleteAddress(hereAutocompleteRequest);
            var addressIds = hereAutocompleteResponse.Items.Select(i => i.Id);
            var checkAddressData = new List<CheckAddressData>();

            foreach (var id in addressIds)
            {
                var hereLookupRequest = new LookupAddressRequest(id);
                var hereLookupResponse = await _hereAddressApiService.LookupAddress(hereLookupRequest);
                var checkAddressDataItem = new CheckAddressData
                {
                    Address = hereLookupResponse.Address.Label,
                    Fields = getFields(hereLookupResponse).ToArray()
                };

                checkAddressData.Add(checkAddressDataItem);
            }

            return checkAddressData;
        }

        public async Task<IEnumerable<CheckAddressData>> ValidateAddress(StructuredInput input)
        {
            var request = getHereValidationAddress(input);
            var validateAddresResponse = await _hereAddressApiService.ValidateAddress(request);
            var checkAddressData = validateAddresResponse.Items.Select(i => new CheckAddressData
            {
                Address = i.Address.Label,
                Fields = getFields(i).ToArray()
            });


            return checkAddressData;
        }

        public async Task<IEnumerable<CheckAddressData>> ValidateAddress(string input, string countryCode)
        {
            var request = getHereValidationAddress(input, countryCode);
            var validateAddresResponse = await _hereAddressApiService.ValidateAddress(request);
            var checkAddressData = validateAddresResponse.Items.Select(i => new CheckAddressData
            {
                Address = i.Address.Label,
                Fields = getFields(i).ToArray()
            });

            return checkAddressData;
        }

        private ValidateAddressRequest getHereValidationAddress(StructuredInput input)
        {
            var validateAddressRequest = new ValidateAddressRequest(input.Input)
            {
                In = input.CountryCode3
            };

            return validateAddressRequest;
        }

        private ValidateAddressRequest getHereValidationAddress(string input, string countryCode)
        {
            var validateAddressRequest = new ValidateAddressRequest(input)
            {
                In = countryCode
            };

            return validateAddressRequest;
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

        ~HereService()
        {
            _hereAddressApiService.Dispose();
        }
    }
}
