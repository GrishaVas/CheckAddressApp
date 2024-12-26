using System.Globalization;
using System.Text.RegularExpressions;
using CheckAddressApp.Models;
using CsvHelper;

namespace qAcProviderTest.Services
{
    public static class CsvFileService
    {
        public static async Task<IEnumerable<CheckAddressInput>> getCheckAddressInputsFromCsvFile(string path, string checkAddressInputOneLineInputFromat)
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


            var records = csv.GetRecords<InputFromFile>().ToList();
            var checkAddressInputs = records.Select(r => getCheckAddressInput(r, checkAddressInputOneLineInputFromat));

            return checkAddressInputs;
        }


        //private static async Task<string> getCsvContent(string path)
        //{
        //    var stream = File.OpenRead(path);

        //    using (var reader = new StreamReader(stream))
        //    {
        //        var inputFileStr = await reader.ReadToEndAsync();

        //        return inputFileStr;
        //    }
        //}

        private static CheckAddressInput getCheckAddressInput(InputFromFile inputFromFile, string oneLineFormat)
        {
            var country = ISO3166.Country.List.FirstOrDefault(c => c.Name.ToLower() == inputFromFile.Country.ToLower());
            var checkAddressInput = new CheckAddressInput
            {
                Country = null,
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

        private static void validateCsv(string csvContent)
        {
            var inputsFromFile = new List<CheckAddressInput>();
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

            //var propNames = rows[0].Split(wordsSep);

            //foreach (var row in rows.Skip(1))
            //{
            //    var values = row.Split(wordsSep);
            //    var inputFromFile = getInputFromFile(propNames, values);

            //    if (inputFromFile != null)
            //    {
            //        var chechAddressInput = getCheckAddressInput(inputFromFile, checkAddressInputOneLineInputFromat);

            //        inputsFromFile.Add(chechAddressInput);
            //    }
            //}

            //return inputsFromFile;
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

        //private static InputFromFile getInputFromFile(string[] propNames, string[] values)
        //{
        //    var inputFormFile = new InputFromFile();
        //    var inputFormFileType = typeof(InputFromFile);
        //    var inputFormFileProps = inputFormFileType.GetProperties();
        //    var mathcesCount = inputFormFileProps.Where(p => propNames.Contains(p.Name)).Count();

        //    if (mathcesCount == 0)
        //    {
        //        return null;
        //    }


        //    for (int i = 0; i < propNames.Length; i++)
        //    {
        //        var prop = inputFormFileProps.FirstOrDefault(p => p.Name == propNames[i]);

        //        if (prop != null)
        //        {
        //            prop.SetValue(inputFormFile, values[i]);
        //        }
        //    }

        //    return inputFormFile;
        //}
    }
}
