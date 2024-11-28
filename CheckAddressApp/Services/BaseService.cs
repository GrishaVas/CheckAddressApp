using CheckAddressApp.Models;

namespace CheckAddressApp.Services
{
    public abstract class BaseService
    {
        private static Dictionary<string, (string ISO2, string ISO3)> _countries = new Dictionary<string, (string ISO2, string ISO3)>
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

        protected virtual IEnumerable<CheckAddressField> getFields(object @object, string name = null)
        {
            if (@object == null)
            {
                return [];
            }


            var objectType = @object.GetType();

            if (objectType.GetInterface("IList") != null)
            {
                var elements = getArrayElements(@object);
                var fields = new List<CheckAddressField>();

                for (var i = 0; i < elements.Count; i++)
                {
                    var elementFields = getFields(elements[i], $"{name}[{i}]");

                    fields.AddRange(elementFields);
                }

                return fields;
            }

            if ((objectType.IsValueType || objectType == typeof(string)) && @object != null)
            {
                var propString = @object.ToString();
                var checkAddressField = new CheckAddressField()
                {
                    Name = $"{name}",
                    Value = propString
                };

                return [checkAddressField];
            }

            if (objectType.IsClass)
            {
                var props = objectType.GetProperties();
                var fields = new List<CheckAddressField>();
                name = string.IsNullOrEmpty(name) ? "" : $"{name}.";

                foreach (var prop in props)
                {
                    var value = prop.GetValue(@object);
                    var objectFields = getFields(value, $"{name}{prop.Name}");

                    fields.AddRange(objectFields);
                }

                return fields;
            }

            return [];
        }

        public static (string ISO2, string ISO3) getCountryCode(string country)
        {
            var lowerCaseCountry = country.ToLower();

            if (!_countries.Keys.Contains(lowerCaseCountry))
            {
                return ("", "");
            }

            return _countries[lowerCaseCountry];
        }

        private List<object> getArrayElements(object array)
        {
            var iListType = typeof(IList<>).MakeGenericType(typeof(object));
            var iCollectionType = typeof(ICollection<>).MakeGenericType(typeof(object));
            var countProp = iCollectionType.GetProperty("Count");
            var count = (int)countProp.GetValue(array);
            var getItemMethod = iListType.GetMethod("get_Item");
            var elements = new List<object>();

            for (int i = 0; i < count; i++)
            {
                var element = getItemMethod.Invoke(array, [i]);

                elements.Add(element);
            }

            return elements;
        }
    }
}
