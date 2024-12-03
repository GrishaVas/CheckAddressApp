using System.Text.RegularExpressions;
using CheckAddressApp.Models;

namespace qAcProviderTest.Services
{
    public class CsvCheckAddressInputSerializer
    {
        private readonly string _checkAddressInputOneLineInput;

        public CsvCheckAddressInputSerializer(string checkAddressInputOneLineInput)
        {
            _checkAddressInputOneLineInput = checkAddressInputOneLineInput;
        }

        public async Task<IEnumerable<CheckAddressInput>> getCheckAddressInputsFromCsvFile(string path)
        {
            if (!File.Exists(path))
            {
                throw new Exception("The file does not exist.");
            }

            var csvContent = await getCsvContent(path);
            var inputsFromFile = getInputsFromCsvContent(csvContent);

            if (inputsFromFile == null)
            {
                return [];
            }

            return inputsFromFile;
        }

        private async Task<string> getCsvContent(string path)
        {
            var stream = File.OpenRead(path);

            using (var reader = new StreamReader(stream))
            {
                var inputFileStr = await reader.ReadToEndAsync();

                return inputFileStr;
            }
        }

        private CheckAddressInput getCheckAddressInput(InputFromFile inputFromFile, string oneLineFormat)
        {
            var checkAddressInput = new CheckAddressInput
            {
                Country = inputFromFile.Country,
                StructuredInput = new StructuredInput
                {
                    City = inputFromFile.City,
                    District = inputFromFile.District,
                    PostalCode = inputFromFile.PostalCode,
                    StreetAndHouseNumber = inputFromFile.StreetAndHouseNumber
                }
            };

            checkAddressInput.FreeInput = checkAddressInput.StructuredInput.ToString(oneLineFormat);

            return checkAddressInput;
        }

        private List<CheckAddressInput> getInputsFromCsvContent(string csvContent)
        {
            var inputsFromFile = new List<CheckAddressInput>();
            const string rowsSep = "\r\n";
            const char wordsSep = ',';
            var rows = csvContent.Split(rowsSep, StringSplitOptions.RemoveEmptyEntries);

            if (rows.Length < 1)
            {
                throw new Exception("Csv file is empty.");
            }

            string errorMessage;
            var iscsvValid = isCsvValid(csvContent, rows[0], out errorMessage);

            if (!iscsvValid)
            {
                throw new Exception(errorMessage);
            }

            var propNames = rows[0].Split(wordsSep);

            foreach (var row in rows.Skip(1))
            {
                var values = row.Split(wordsSep);
                var inputFromFile = getInputFromFile(propNames, values);

                if (inputFromFile != null)
                {
                    var chechAddressInput = getCheckAddressInput(inputFromFile, _checkAddressInputOneLineInput);

                    inputsFromFile.Add(chechAddressInput);
                }
            }

            return inputsFromFile;
        }

        private bool isCsvValid(string csvText, string csvHeadRow, out string errorMessage)
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

        private InputFromFile getInputFromFile(string[] propNames, string[] values)
        {
            var inputFormFile = new InputFromFile();
            var inputFormFileType = typeof(InputFromFile);
            var inputFormFileProps = inputFormFileType.GetProperties();
            var mathcesCount = inputFormFileProps.Where(p => propNames.Contains(p.Name)).Count();

            if (mathcesCount == 0)
            {
                return null;
            }


            for (int i = 0; i < propNames.Length; i++)
            {
                var prop = inputFormFileProps.FirstOrDefault(p => p.Name == propNames[i]);

                if (prop != null)
                {
                    prop.SetValue(inputFormFile, values[i]);
                }
            }

            return inputFormFile;
        }
    }
}
