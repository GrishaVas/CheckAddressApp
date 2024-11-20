using CheckAddressApp.Models;

namespace CheckAddressApp.Services
{
    public abstract class BaseService
    {
        private Dictionary<string, (string ISO2, string ISO3)> _countries = new Dictionary<string, (string ISO2, string ISO3)>
        {
            {"united states",("US","USA")},
            {"slovakia",("SK","SVK")},
            {"slovenia",("SI","SVN")},
            {"singapore",("SG","SGP")},
            {"sweden",("SE","SWE")},
            {"sortugal",("PT","PRT")},
            {"puerto Rico",("PR","PRI")},
            {"poland",("PL","POL")},
            {"new zealand",("NZ","NZL")},
            {"norway",("NO","NOR")},
            {"netherlands",("NL","NLD")},
            {"malaysia",("MY","MYS")},
            {"mexico",("MX","MEX")},
            {"latvia",("LV","LVA")},
            {"luxembourg",("LU","LUX")},
            {"lithuania",("LT","LTU")},
            {"italy",("IT","ITA")},
            {"india",("IN","IND")},
            {"ireland",("IE","IRL")},
            {"hungary",("HU","HUN")},
            {"croatia",("HR","HRV")},
            {"united kingdom",("GB","GBR")},
            {"france",("FR","FRA")},
            {"finland",("FI","FIN")},
            {"spain",("ES","ESP")},
            {"estonia",("EE","EST")},
            {"denmark",("DK","DNK")},
            {"germany",("DE","DEU")},
            {"czechia",("CZ","CZE")},
            {"colombia",("CO","COL")},
            {"chile",("CL","CHL")},
            {"switzerland",("CH","CHE")},
            {"canada",("CA","CAN")},
            {"brazil",("BR","BRA")},
            {"bulgaria",("BG","BGR")},
            {"belgium",("BE","BEL")},
            {"australia",("AU","AUS")},
            {"austria",("AT","AUT")},
            {"argentina",("AR","ARG")}
        };

        public abstract Task<ServiceData> AutosuggestAddress(CheckAddressInput input);
        public abstract Task<ServiceData> AutocompleteAddress(CheckAddressInput input);
        public abstract Task<ServiceData> ValidateAddress(CheckAddressInput input);

        protected virtual IEnumerable<CheckAddressField> getFields(object match, string ownerName = null)
        {
            if (match == null)
            {
                return [];
            }

            var fields = new List<CheckAddressField>();
            var props = match.GetType()
                .GetProperties();

            foreach (var prop in props)
            {
                if (prop.PropertyType.IsValueType || prop.PropertyType == typeof(string))
                {
                    var propValue = prop.GetValue(match);
                    var propString = propValue?.ToString();
                    if (!string.IsNullOrEmpty(propString))
                    {
                        var checkAddressField = new CheckAddressField()
                        {
                            Name = $"{(string.IsNullOrEmpty(ownerName) ? "" : $"{ownerName}.")}{prop.Name}",
                            Value = propString
                        };

                        fields.Add(checkAddressField);
                    }
                }
            }

            return fields;
        }

        protected (string ISO2, string ISO3) getCountryCode(string country)
        {
            var lowerCaseCountry = country.ToLower();

            if (!_countries.Keys.Contains(lowerCaseCountry))
            {
                throw new Exception("Country does not exists.");
            }

            return _countries[lowerCaseCountry];
        }
    }
}
