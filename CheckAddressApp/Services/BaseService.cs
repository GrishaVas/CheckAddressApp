using CheckAddressApp.Models;

namespace CheckAddressApp.Services
{
    public abstract class BaseService
    {
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
