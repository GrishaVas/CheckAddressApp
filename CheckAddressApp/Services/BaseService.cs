using CheckAddressApp.Models;

namespace CheckAddressApp.Services
{
    public class BaseService
    {
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
    }
}
