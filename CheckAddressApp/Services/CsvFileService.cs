using System.Globalization;
using System.Text.RegularExpressions;
using CheckAddressApp.Models;
using CsvHelper;

namespace qAcProviderTest.Services
{
    public static class CsvFileService
    {
        public static async Task<IEnumerable<AddressFromFile>> getCheckAddressInputsFromCsvFile(string path, string checkAddressInputOneLineInputFromat)
        {
            if (!File.Exists(path))
            {
                throw new Exception("The file does not exist.");
            }

            using var fileStream = new FileStream(path, FileMode.Open);
            using var reader = new StreamReader(fileStream);

            var csvContent = await reader.ReadToEndAsync();

            validateCsv(csvContent);

            fileStream.Position = 0;

            using var reader2 = new StreamReader(fileStream);
            using var csv = new CsvReader(reader2, CultureInfo.InvariantCulture);


            var checkAddressInputs = csv.GetRecords<AddressFromFile>().ToList();

            return checkAddressInputs;
        }

        public static async Task insertIntoAddressesFile(string path, AddressFromFile input, int index)
        {
            if (!File.Exists(path))
            {
                throw new Exception("Cannot find the file.");
            }

            const string rowSep = "\r\n";
            const string wordSep = ",";
            var text = await File.ReadAllTextAsync(path);
            var rows = text.Split(rowSep);
            var words = rows[0].Split(wordSep);
            var props = typeof(AddressFromFile).GetProperties().Where(p => words.Contains(p.Name));
            var values = new List<string>();

            foreach (var word in words)
            {
                var prop = props.FirstOrDefault(p => p.Name == word);
                var value = prop?.GetValue(input);

                values.Add(value?.ToString() ?? "");
            }

            var stringStart = $"{string.Join("\r\n", rows.Take(index + 1))}\r\n{string.Join(",", values)}\r\n";
            string result = stringStart + string.Join("\r\n", rows.Skip(index + 1));

            await File.WriteAllTextAsync(path, result);
        }

        public static async Task removeFromAddressesFile(string path, int rowIndex)
        {
            if (!File.Exists(path))
            {
                throw new Exception("Cannot find the file.");
            }

            const string rowSep = "\r\n";
            var text = await File.ReadAllTextAsync(path);
            var rows = text.Split(rowSep).ToList();

            rows.RemoveAt(rowIndex + 1);

            string result = string.Join("\r\n", rows);

            await File.WriteAllTextAsync(path, result);
        }

        private static void validateCsv(string csvContent)
        {
            const string rowsSep = "\r\n";
            var rows = csvContent.Split(rowsSep, StringSplitOptions.RemoveEmptyEntries);

            if (rows.Length < 1)
            {
                throw new Exception("Csv file is empty.");
            }

            var iscsvValid = isCsvValid(csvContent, rows[0], out string errorMessage);

            if (!iscsvValid)
            {
                throw new Exception(errorMessage);
            }
        }

        private static bool isCsvValid(string csvText, string csvHeadRow, out string errorMessage)
        {
            var regex = new Regex(@"\A([^,\r\n]+,)+[^,\r\n]+\z");
            var isHeadRowValid = regex.IsMatch(csvHeadRow);

            if (!isHeadRowValid)
            {
                errorMessage = "The csv header row is not valid.";

                return false;
            }

            var delimetersCount = csvHeadRow.Count(c => c == ',');
            regex = new Regex(@"\A(([^,\n]*,){" + delimetersCount + @"}[^,\n]*\n)+((([^,\n]*,){" + delimetersCount + @"}[^,\n]*)|)\z");
            var iscsvTextValid = regex.IsMatch(csvText);

            if (!iscsvTextValid)
            {
                errorMessage = "The csv is not valid.";

                return false;
            }

            errorMessage = null;
            return true;
        }
    }
}
