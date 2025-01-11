using CheckAddressApp.Services;
using ISO3166;
using Microsoft.Extensions.Configuration;
using qAcProviderTest.Models.CheckAddressServiceModels;


namespace qAcProviderTest.Services
{
    public class CheckAddressService
    {
        private static GoogleService _googleService;
        private static LoqateService _loqateService;
        private static SmartyService _smartyService;
        private static HereService _hereService;

        public CheckAddressService(IConfiguration conf)
        {
            _smartyService = new SmartyService(conf);
            _googleService = new GoogleService(conf);
            _loqateService = new LoqateService(conf);
            _hereService = new HereService(conf);
        }

        public async Task<Dictionary<AddressProvider, CheckAddressOutput>> Validation(CheckAddressInput input)
        {
            if (string.IsNullOrWhiteSpace(input.FullString))
            {
                throw new Exception("Input cannot be null.");
            }

            if (input.AddressProviders == AddressProvider.None)
            {
                throw new Exception("At least one Check Provider should be selected!");
            }

            var providersData = new Dictionary<AddressProvider, CheckAddressOutput>();

            if (input.AddressProviders.HasFlag(AddressProvider.Google))
            {
                var checkAddressOutput = await validation(_googleService, input);

                providersData.Add(AddressProvider.Google, checkAddressOutput);
            }

            if (input.AddressProviders.HasFlag(AddressProvider.Loqate))
            {
                var checkAddressOutput = await validation(_loqateService, input);

                providersData.Add(AddressProvider.Loqate, checkAddressOutput);
            }

            if (input.AddressProviders.HasFlag(AddressProvider.Smarty))
            {
                if (input.Country == null)
                {
                    throw new Exception("Country required for Smarty.");
                }

                var checkAddressOutput = await validation(_smartyService, input);

                providersData.Add(AddressProvider.Smarty, checkAddressOutput);
            }

            if (input.AddressProviders.HasFlag(AddressProvider.Here))
            {
                var checkAddressOutput = await validation(_hereService, input);

                providersData.Add(AddressProvider.Here, checkAddressOutput);
            }

            return providersData;
        }

        public async Task<Dictionary<AddressProvider, CheckAddressOutput>> Autocomplete(CheckAddressInput input)
        {
            if (string.IsNullOrWhiteSpace(input.FullString))
            {
                throw new Exception("Input cannot be null.");
            }

            if (input.AddressProviders == AddressProvider.None)
            {
                throw new Exception("At least one Check Provider should be selected!");
            }

            var providersData = new Dictionary<AddressProvider, CheckAddressOutput>();

            if (input.AddressProviders.HasFlag(AddressProvider.Google))
            {
                var checkAddressOutput = await autocomplete(_googleService, input);

                providersData.Add(AddressProvider.Google, checkAddressOutput);
            }

            if (input.AddressProviders.HasFlag(AddressProvider.Loqate))
            {
                var checkAddressOutput = await autocomplete(_loqateService, input);

                providersData.Add(AddressProvider.Loqate, checkAddressOutput);
            }

            if (input.AddressProviders.HasFlag(AddressProvider.Smarty))
            {
                if (input.Country == null)
                {
                    throw new Exception("Country required for Smarty.");
                }

                var checkAddressOutput = await autocomplete(_smartyService, input);

                providersData.Add(AddressProvider.Smarty, checkAddressOutput);
            }

            if (input.AddressProviders.HasFlag(AddressProvider.Here))
            {
                var checkAddressOutput = await autocomplete(_hereService, input);

                providersData.Add(AddressProvider.Here, checkAddressOutput);
            }

            return providersData;
        }

        public async Task<Dictionary<AddressProvider, CheckAddressOutput>> Autosuggest(CheckAddressInput input)
        {
            if (string.IsNullOrWhiteSpace(input.FullString))
            {
                throw new Exception("Input cannot be null.");
            }

            if (input.AddressProviders == AddressProvider.None)
            {
                throw new Exception("At least one Check Provider should be selected!");
            }

            var providersData = new Dictionary<AddressProvider, CheckAddressOutput>();

            if (input.AddressProviders == AddressProvider.Here)
            {
                var checkAddressOutput = await autosuggest(_hereService, input);

                providersData.Add(AddressProvider.Here, checkAddressOutput);
            }
            else
            {
                throw new Exception("Autosuggest is supported only by Here.");
            }

            return providersData;
        }

        public static Country GetCountry(string countryName)
        {
            Country country;

            if (!string.IsNullOrEmpty(countryName))
            {
                country = Country.List.FirstOrDefault(c => c.Name.ToLower() == countryName.ToLower() ||
                    c.ThreeLetterCode.ToLower() == countryName.ToLower() ||
                    c.TwoLetterCode.ToLower() == countryName.ToLower());
            }
            else
            {
                country = null;
            }

            if (country == null && !string.IsNullOrEmpty(countryName))
            {
                throw new Exception("Cannot find the country in the country list.");
            }

            return country;
        }

        private async Task<CheckAddressOutput> validation(BaseService service, CheckAddressInput input)
        {
            try
            {
                var startTime = DateTime.UtcNow.TimeOfDay.TotalSeconds;
                var checkAddressAddressesData = await service.ValidateAddress(input);
                var time = DateTime.UtcNow.TimeOfDay.TotalSeconds - startTime;
                var checkAddressOutput = new CheckAddressOutput
                {
                    Addresses = checkAddressAddressesData,
                    Time = time
                };

                return checkAddressOutput;
            }
            catch (Exception ex)
            {

                throw new Exception($"{service.GetType().Name}: {ex.Message}");
            }
        }

        private async Task<CheckAddressOutput> autocomplete(BaseService service, CheckAddressInput input)
        {
            try
            {
                var startTime = DateTime.UtcNow.TimeOfDay.TotalSeconds;
                var checkAddressAddressesData = await service.AutocompleteAddress(input);
                var time = DateTime.UtcNow.TimeOfDay.TotalSeconds - startTime;
                var checkAddressOutput = new CheckAddressOutput
                {
                    Addresses = checkAddressAddressesData,
                    Time = time
                };

                return checkAddressOutput;
            }
            catch (Exception ex)
            {

                throw new Exception($"{service.GetType().Name}: {ex.Message}");
            }
        }

        private async Task<CheckAddressOutput> autosuggest(BaseService service, CheckAddressInput input)
        {
            try
            {
                var startTime = DateTime.UtcNow.TimeOfDay.TotalSeconds;
                var checkAddressAddressesData = await service.AutosuggestAddress(input);
                var time = DateTime.UtcNow.TimeOfDay.TotalSeconds - startTime;
                var checkAddressOutput = new CheckAddressOutput
                {
                    Addresses = checkAddressAddressesData,
                    Time = time
                };

                return checkAddressOutput;
            }
            catch (Exception ex)
            {

                throw new Exception($"{service.GetType().Name}: {ex.Message}");
            }
        }
    }
}
