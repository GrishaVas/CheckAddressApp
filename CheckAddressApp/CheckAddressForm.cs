using System.Reflection;
using System.Text.RegularExpressions;
using CheckAddressApp.Models;
using CheckAddressApp.Services;
using Microsoft.Extensions.Configuration;
using qAcProviderTest;
using qAcProviderTest.Properties;

namespace CheckAddressApp
{
    public partial class CheckAddressForm : Form
    {
        private ErrorForm _errorForm;
        private UserNotificationForm _userNotificationForm;
        private GoogleService _googleService;
        private LoqateService _loqateService;
        private SmartyService _smartyService;
        private HereService _hereService;
        private IEnumerable<CheckAddressData> _loqateValidateResponse;
        private IEnumerable<CheckAddressData> _loqateAutocompleteResponse;
        private IEnumerable<CheckAddressData> _smartyValidateResponse;
        private IEnumerable<CheckAddressData> _smartyAutocompleteResponse;
        private IEnumerable<CheckAddressData> _hereValidateResponse;
        private IEnumerable<CheckAddressData> _hereAutocompleteResponse;
        private IEnumerable<CheckAddressData> _hereAutosuggestResponse;
        private IEnumerable<CheckAddressData> _googleValidateResponse;
        private IEnumerable<CheckAddressData> _googleAutocompleteResponse;
        private IEnumerable<CheckAddressInput> _addressesFromFile;

        private IConfiguration _conf;
        private List<CheckBox> _apiCheckBoxes;
        private IEnumerable<Button> _buttonsToControlAddressesFromFile;
        private string _addressesFilePath = "addresses.csv";
        private string _oneLineInputFormat;

        public CheckAddressForm(IConfiguration conf)
        {
            _smartyService = new SmartyService(conf);
            _googleService = new GoogleService(conf);
            _loqateService = new LoqateService(conf);
            _hereService = new HereService(conf);
            _errorForm = new ErrorForm();
            _userNotificationForm = new UserNotificationForm(this);
            _conf = conf;

            InitializeComponent();

            _apiCheckBoxes = new List<CheckBox>
            {
                hereCheckBox,
                smartyCheckBox,
                googleMapsCheckBox,
                loqateCheckBox,
            };
            _buttonsToControlAddressesFromFile = new List<Button>
            {
                insertAddressButton,
                updateAddressButton,
                deleteAddressButton,
                saveAsAddressesButton,
                loadAddressesButton
            };

            Text = $"q.address AC Provider Test v_{Assembly.GetExecutingAssembly().GetName().Version}";
            _oneLineInputFormat = _conf["RequestInputStringFormat"];
        }
        ~CheckAddressForm()
        {
            _loqateService.Dispose();
            _hereService.Dispose();
            _errorForm.Dispose();
        }

        private void showApi(IConfiguration conf)
        {
            var enableGoogle = bool.Parse(conf["Google:Enabled"] ?? "false");
            var enableHere = bool.Parse(conf["Here:Enabled"] ?? "false");
            var enableLoqate = bool.Parse(conf["Loqate:Enabled"] ?? "false");
            var enableSmarty = bool.Parse(conf["Smarty:Enabled"] ?? "false");

            googleMapsCheckBox.Enabled = enableGoogle;
            hereCheckBox.Enabled = enableHere;
            loqateCheckBox.Enabled = enableLoqate;
            smartyCheckBox.Enabled = enableSmarty;

            var firstCheckBox = _apiCheckBoxes.Where(ch => ch.Enabled).FirstOrDefault();

            if (firstCheckBox != null)
            {
                firstCheckBox.Checked = true;
            }
        }

        private async Task loadAddressesFromFile(string path)
        {
            if (File.Exists(path))
            {
                var stream = File.OpenRead(path);
                var inputFileStr = "";

                using (var reader = new StreamReader(stream))
                {
                    inputFileStr = await reader.ReadToEndAsync();
                }

                var inputsFromFile = getInputsFromFile(inputFileStr);

                if (inputsFromFile == null)
                {
                    return;
                }

                _addressesFromFile = inputsFromFile;

                var items = _addressesFromFile.Select(i => i.ToString());

                addressesFromFileListBox.Items.Clear();
                addressesFromFileListBox.Items.AddRange(items.ToArray());

                addressesFileNameTextBox.Text = path;
                _addressesFilePath = path;
            }
        }

        private void clearGoogleResponse()
        {
            _googleAutocompleteResponse = null;
            _googleValidateResponse = null;
            googleResponseListBox.Items.Clear();
            googleResponseDataGridView.Rows.Clear();
            googleResponseTimeLabel.Text = "Response time: 0,0";
        }

        private void clearLoqateResponse()
        {
            _loqateAutocompleteResponse = null;
            _loqateValidateResponse = null;
            loqateResponseDataGridView.Rows.Clear();
            loqateResponseListBox.Items.Clear();
            loqateResponseTimeLabel.Text = "Response time: 0,0";
        }

        private void clearSmartyResponse()
        {
            _smartyAutocompleteResponse = null;
            _smartyValidateResponse = null;
            smartyResponseDataGridView.Rows.Clear();
            smartyResponseListBox.Items.Clear();
            smartyResponseTimeLabel.Text = "Response time: 0,0";
        }

        private void clearHereResponse()
        {
            _hereAutosuggestResponse = null;
            _hereAutocompleteResponse = null;
            hereResponseListBox.Items.Clear();
            hereResponseDataGridView.Rows.Clear();
            _hereValidateResponse = null;
            hereResponseTimeLabel.Text = "Response time: 0,0";
        }

        private async Task<ServiceData> validation<TApiService>(CheckAddressInput input, TApiService service) where TApiService : BaseService
        {
            if (string.IsNullOrEmpty(input.FreeInput) && string.IsNullOrEmpty(input.StructuredInput?.ToString()))
            {
                return null;
            }

            var serviceData = await service.ValidateAddress(input);

            return serviceData;
        }
        private async Task<ServiceData> autocomplete<TApiService>(CheckAddressInput input, TApiService service) where TApiService : BaseService
        {
            if (string.IsNullOrEmpty(input.FreeInput) && string.IsNullOrEmpty(input.StructuredInput?.ToString()))
            {
                return null;
            }

            var serviceData = await service.AutocompleteAddress(input);

            return serviceData;
        }
        private async Task<ServiceData> autosuggest<TApiService>(CheckAddressInput input, TApiService service) where TApiService : BaseService
        {
            if (string.IsNullOrEmpty(input.FreeInput) && string.IsNullOrEmpty(input.StructuredInput?.ToString()))
            {
                return null;
            }

            var serviceData = await service.AutosuggestAddress(input);

            return serviceData;
        }

        private CheckAddressInput getCheckAddressInput()
        {
            var structuredInput = getStructuredInput();
            var country = countryTextBox.Text;
            var checkAddressInput = new CheckAddressInput
            {
                Country = country
            };

            if (!string.IsNullOrEmpty(structuredInput.ToString()))
            {
                checkAddressInput.StructuredInput = structuredInput;
                checkAddressInput.FreeInput = structuredInput.ToString(_oneLineInputFormat);
            }
            else
            {
                checkAddressInput.FreeInput = freeInputTextBox.Text;
            }

            return checkAddressInput;
        }

        private StructuredInput getStructuredInput()
        {
            var input = new StructuredInput
            {
                StreetAndHouseNumber = streetAndHouseNumberTextBox.Text,
                City = cityTextBox.Text,
                District = districtTextBox.Text,
                PostalCode = postalCodeTextBox.Text
            };

            return input;
        }

        private void clearInputs()
        {
            streetAndHouseNumberTextBox.Text = "";
            countryTextBox.Text = "";
            cityTextBox.Text = "";
            postalCodeTextBox.Text = "";
            districtTextBox.Text = "";
            requestAddressTextBox.Text = "";
            freeInputTextBox.Text = "";
        }

        private void showError(Exception ex)
        {
            if (_errorForm.Visible)
            {
                _errorForm.Focus();
            }
            else
            {
                _errorForm.Show();
            }

            var errorRichTextBox = _errorForm.Controls.Find("errorRichTextBox", true).FirstOrDefault();

            if (errorRichTextBox != null)
            {
                errorRichTextBox.Text = $"Message: {ex.Message}\n" +
                    $"Stack trace: {ex.StackTrace}";
            }
        }
        private void showNotificatoin(string message)
        {
            if (_userNotificationForm == null)
            {
                throw new Exception("User notification form does not exist.");
            }

            if (_userNotificationForm.Visible)
            {
                _userNotificationForm.Focus();
            }
            else
            {
                _userNotificationForm.Show();
            }

            var errorRichTextBox = _userNotificationForm.Controls.Find("userNotificationRichTextBox", true).FirstOrDefault();

            if (errorRichTextBox != null)
            {
                errorRichTextBox.Text = message;
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

        private List<CheckAddressInput> getInputsFromFile(string inputFileStr)
        {
            var inputsFromFile = new List<CheckAddressInput>();
            const string rowsSep = "\r\n";
            const char wordsSep = ',';
            var rows = inputFileStr.Split(rowsSep, StringSplitOptions.RemoveEmptyEntries);

            if (rows.Length < 1)
            {
                showNotificatoin("Csv file is empty.");

                return null;
            }

            string errorMessage;
            var iscsvValid = isCsvValid(inputFileStr, rows[0], out errorMessage);

            if (!iscsvValid)
            {
                showNotificatoin(errorMessage);

                return null;
            }

            var propNames = rows[0].Split(wordsSep);

            foreach (var row in rows.Skip(1))
            {
                var values = row.Split(wordsSep);
                var inputFromFile = getInputFromFile(propNames, values);

                if (inputFromFile != null)
                {
                    var chechAddressInput = getCheckAddressInput(inputFromFile, _oneLineInputFormat);

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
            regex = new Regex(@"\A([^,\n]+,)+[^,\n]+(\n|\z)((([^,\n]*,[^,\n]*){" + delimetersCount + @"}(\n|\z))*\z)");
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

        private void setGoogleOutput(ServiceData output, double time)
        {
            var addresses = output.CheckAddressData.Select(cad => cad.Address);

            clearGoogleResponse();

            if (addresses.Count() < 1)
            {
                googleResponseListBox.Items.Add("None");
            }
            else
            {
                googleResponseListBox.Items.AddRange(addresses.ToArray());
            }

            _googleValidateResponse = output.CheckAddressData;
            googleResponseTimeLabel.Text = $"Response time: {Math.Round(time, 3)}s";
        }

        private void setLoqateOutput(ServiceData output, double time)
        {
            var addresses = output.CheckAddressData.Select(cad => cad.Address);

            clearLoqateResponse();

            if (addresses.Count() < 1)
            {
                loqateResponseListBox.Items.Add("None");
            }
            else
            {
                loqateResponseListBox.Items.AddRange(addresses.ToArray());
            }

            _loqateValidateResponse = output.CheckAddressData;
            loqateResponseTimeLabel.Text = $"Response time: {Math.Round(time, 3)}s";
        }

        private void setSmartyOutput(ServiceData output, double time)
        {
            var addresses = output.CheckAddressData.Select(cad => cad.Address);

            clearSmartyResponse();

            if (addresses.Count() < 1)
            {
                smartyResponseListBox.Items.Add("None");
            }
            else
            {
                smartyResponseListBox.Items.AddRange(addresses.ToArray());
            }

            _smartyValidateResponse = output.CheckAddressData;
            smartyResponseTimeLabel.Text = $"Response time: {Math.Round(time, 3)}s";
        }

        private void setHereOutput(ServiceData output, double time)
        {
            var addresses = output.CheckAddressData.Select(cad => cad.Address);

            clearHereResponse();

            if (addresses.Count() < 1)
            {
                hereResponseListBox.Items.Add("None");
            }
            else
            {
                hereResponseListBox.Items.AddRange(addresses.ToArray());
            }

            _hereValidateResponse = output.CheckAddressData;
            hereResponseTimeLabel.Text = $"Response time: {Math.Round(time, 3)}s";
        }

        private async Task insertIntoAddressesFileAt(string path, InputFromFile input, int index)
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
            var props = typeof(InputFromFile).GetProperties().Where(p => words.Contains(p.Name));
            var values = new List<string>();

            foreach (var word in words)
            {
                var prop = props.FirstOrDefault(p => p.Name == word);
                var value = prop?.GetValue(input);

                values.Add(value?.ToString() ?? "");
            }

            var stringStart = $"{string.Join("\r\n", rows.Take(index + 1))}\r\n{string.Join(",", values)}\r\n";
            string result = stringStart + string.Join("\r\n", rows.Skip(index + 1));

            await File.WriteAllTextAsync(_addressesFilePath, result);
        }

        private async Task removeFromAddressesFileAt(string path, int rowIndex)
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

            await File.WriteAllTextAsync(_addressesFilePath, result);
        }

        private async Task validation()
        {
            var checkAddressInput = getCheckAddressInput();

            requestAddressTextBox.Text = $"{checkAddressInput.FreeInput} {BaseService.getCountryCode(checkAddressInput.Country).ISO2}";

            if (string.IsNullOrEmpty(checkAddressInput.FreeInput))
            {
                showNotificatoin("Inputs cannot be null.");

                return;
            }

            if (googleMapsCheckBox.Checked)
            {
                var startTime = DateTime.UtcNow.TimeOfDay.TotalSeconds;
                var googleServiceData = await validation(checkAddressInput, _googleService);
                var time = DateTime.UtcNow.TimeOfDay.TotalSeconds - startTime;

                setGoogleOutput(googleServiceData, time);
            }
            if (loqateCheckBox.Checked)
            {
                var startTime = DateTime.UtcNow.TimeOfDay.TotalSeconds;
                var loqateServiceData = await validation(checkAddressInput, _loqateService);
                var time = DateTime.UtcNow.TimeOfDay.TotalSeconds - startTime;

                setLoqateOutput(loqateServiceData, time);
            }
            if (smartyCheckBox.Checked)
            {
                if (string.IsNullOrEmpty(checkAddressInput.Country))
                {
                    showNotificatoin("Country required for Smarty.");

                    return;
                }

                var startTime = DateTime.UtcNow.TimeOfDay.TotalSeconds;
                var smartyServiceData = await validation(checkAddressInput, _smartyService);
                var time = DateTime.UtcNow.TimeOfDay.TotalSeconds - startTime;

                setSmartyOutput(smartyServiceData, time);
            }
            if (hereCheckBox.Checked)
            {
                var startTime = DateTime.UtcNow.TimeOfDay.TotalSeconds;
                var hereServiceData = await validation(checkAddressInput, _hereService);
                var time = DateTime.UtcNow.TimeOfDay.TotalSeconds - startTime;

                setHereOutput(hereServiceData, time);
            }
        }

        private async Task autocomplete()
        {
            var checkAddressInput = getCheckAddressInput();

            requestAddressTextBox.Text = $"{checkAddressInput.FreeInput} {BaseService.getCountryCode(checkAddressInput.Country).ISO2}";

            if (string.IsNullOrEmpty(checkAddressInput.FreeInput))
            {
                showNotificatoin("Inputs cannot be null.");

                return;
            }

            if (googleMapsCheckBox.Checked)
            {
                var startTime = DateTime.UtcNow.TimeOfDay.TotalSeconds;
                var googleServiceData = await autocomplete(checkAddressInput, _googleService);
                var time = DateTime.UtcNow.TimeOfDay.TotalSeconds - startTime;

                setGoogleOutput(googleServiceData, time);
            }
            if (loqateCheckBox.Checked)
            {
                var startTime = DateTime.UtcNow.TimeOfDay.TotalSeconds;
                var loqateServiceData = await autocomplete(checkAddressInput, _loqateService);
                var time = DateTime.UtcNow.TimeOfDay.TotalSeconds - startTime;

                setLoqateOutput(loqateServiceData, time);
            }
            if (smartyCheckBox.Checked)
            {
                if (string.IsNullOrEmpty(checkAddressInput.Country))
                {
                    showNotificatoin("Country required for Smarty.");

                    return;
                }

                var startTime = DateTime.UtcNow.TimeOfDay.TotalSeconds;
                var smartyServiceData = await autocomplete(checkAddressInput, _smartyService);
                var time = DateTime.UtcNow.TimeOfDay.TotalSeconds - startTime;

                setSmartyOutput(smartyServiceData, time);
            }
            if (hereCheckBox.Checked)
            {
                var startTime = DateTime.UtcNow.TimeOfDay.TotalSeconds;
                var hereServiceData = await autocomplete(checkAddressInput, _hereService);
                var time = DateTime.UtcNow.TimeOfDay.TotalSeconds - startTime;

                setHereOutput(hereServiceData, time);
            }
        }

        private async Task autosuggest()
        {
            var checkAddressInput = getCheckAddressInput();

            requestAddressTextBox.Text = $"{checkAddressInput.FreeInput} {BaseService.getCountryCode(checkAddressInput.Country).ISO2}";

            if (string.IsNullOrEmpty(checkAddressInput.FreeInput))
            {
                showNotificatoin("Inputs cannot be null.");

                return;
            }

            if (hereCheckBox.Checked && !googleMapsCheckBox.Checked && !smartyCheckBox.Checked && !loqateCheckBox.Checked)
            {
                var startTime = DateTime.UtcNow.TimeOfDay.TotalSeconds;
                var hereServiceData = await autosuggest(checkAddressInput, _hereService);
                var time = DateTime.UtcNow.TimeOfDay.TotalSeconds - startTime;

                setHereOutput(hereServiceData, time);
            }
            else
            {
                showNotificatoin("Autosuggest is supported only by Here.");
            }
        }

        private void loqateResponseListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = loqateResponseListBox.SelectedItem;

            if (item == null)
            {
                return;
            }

            CheckAddressData checkAddressData = null;

            if (_loqateValidateResponse != null)
            {
                checkAddressData = _loqateValidateResponse.FirstOrDefault(cad => cad.Address == item.ToString());
            }

            if (_loqateAutocompleteResponse != null)
            {
                checkAddressData = _loqateAutocompleteResponse.FirstOrDefault(cad => cad.Address == item.ToString());
            }

            var rows = checkAddressData.Fields;

            if (rows == null)
            {
                return;
            }

            loqateResponseDataGridView.Rows.Clear();

            foreach (var field in rows)
            {
                loqateResponseDataGridView.Rows.Add(field.Name, field.Value);
            }
        }

        private void smartyResponseListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = smartyResponseListBox.SelectedItem;

            if (item == null)
            {
                return;
            }

            CheckAddressData checkAddressData = null;

            if (_smartyValidateResponse != null)
            {
                checkAddressData = _smartyValidateResponse.FirstOrDefault(cad => cad.Address == item.ToString());
            }

            if (_smartyAutocompleteResponse != null)
            {
                checkAddressData = _smartyAutocompleteResponse.FirstOrDefault(cad => cad.Address == item.ToString());
            }

            var rows = checkAddressData?.Fields;

            if (rows == null)
            {
                return;
            }

            smartyResponseDataGridView.Rows.Clear();

            foreach (var row in rows)
            {
                smartyResponseDataGridView.Rows.Add(row.Name, row.Value);
            }
        }

        private void hereResponseListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = hereResponseListBox.SelectedItem;

            if (item == null)
            {
                return;
            }

            CheckAddressData checkAddressData = null;

            if (_hereValidateResponse != null)
            {
                checkAddressData = _hereValidateResponse.FirstOrDefault(cad => cad.Address == item.ToString());
            }

            if (_hereAutocompleteResponse != null)
            {
                checkAddressData = _hereAutocompleteResponse.FirstOrDefault(cad => cad.Address == item.ToString());
            }

            if (_hereAutosuggestResponse != null)
            {
                checkAddressData = _hereAutosuggestResponse.FirstOrDefault(cad => cad.Address == item.ToString());
            }

            var rows = checkAddressData?.Fields;

            if (rows == null)
            {
                return;
            }

            hereResponseDataGridView.Rows.Clear();

            foreach (var row in rows)
            {
                hereResponseDataGridView.Rows.Add(row.Name, row.Value);
            }
        }

        private void googleResponseListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = googleResponseListBox.SelectedItem;

            if (item == null)
            {
                return;
            }
            CheckAddressData checkAddressData = null;

            if (_googleValidateResponse != null)
            {
                checkAddressData = _googleValidateResponse.FirstOrDefault(cad => cad.Address == item.ToString());

            }

            if (_googleAutocompleteResponse != null)
            {
                checkAddressData = _googleAutocompleteResponse.FirstOrDefault(cad => cad.Address == item.ToString());
            }

            var rows = checkAddressData?.Fields;

            if (rows == null)
            {
                return;
            }

            googleResponseDataGridView.Rows.Clear();

            foreach (var row in rows)
            {
                googleResponseDataGridView.Rows.Add(row.Name, row.Value);
            }
        }

        private void inputsChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearInputs();
        }

        private void fromFileInputAddresseslistBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (addressesFromFileListBox.SelectedItem != null)
            {
                var selectedTab = inputsChoiceTabControl.SelectedTab.Text;
                var inputFromFile = _addressesFromFile.FirstOrDefault(i => i.ToString() == addressesFromFileListBox.SelectedItem.ToString());

                if (selectedTab == "Structured Input")
                {
                    streetAndHouseNumberTextBox.Text = inputFromFile?.StructuredInput.StreetAndHouseNumber;
                    postalCodeTextBox.Text = inputFromFile?.StructuredInput.PostalCode;
                    cityTextBox.Text = inputFromFile?.StructuredInput.City;
                    districtTextBox.Text = inputFromFile?.StructuredInput.District;
                    countryTextBox.Text = inputFromFile?.Country;
                }
                else
                {
                    freeInputTextBox.Text = inputFromFile?.FreeInput;
                    countryTextBox.Text = inputFromFile?.Country;
                }
            }
        }

        private void addressTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void cityTextBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void districtTextBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void postalCodeTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void freeInputTextBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void countryTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void addressesSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            var search = addressesSearchTextBox.Text;
            IEnumerable<CheckAddressInput> addresses;

            if (!string.IsNullOrEmpty(search))
            {
                addresses = _addressesFromFile.Where(a => a.ToString().ToLower().Contains(search.ToLower()));
            }
            else
            {
                addresses = _addressesFromFile;
            }

            var items = addresses.Select(i => i.ToString());

            addressesFromFileListBox.Items.Clear();
            addressesFromFileListBox.Items.AddRange(items.ToArray());
        }

        private void googleMapsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (googleMapsCheckBox.Checked)
            {
                if (!apiTabControl.TabPages.Contains(googleMapsTabPage))
                {
                    apiTabControl.TabPages.Add(googleMapsTabPage);
                }
            }
            else
            {
                var count = _apiCheckBoxes.Count(cb => cb.Checked);

                if (count != 0)
                {
                    clearGoogleResponse();
                    apiTabControl.TabPages.Remove(googleMapsTabPage);
                }
                else
                {
                    googleMapsCheckBox.Checked = true;
                    showNotificatoin("At least one Check Provider should be checked!");
                }
            }
        }

        private void loqateCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (loqateCheckBox.Checked)
            {
                if (!apiTabControl.TabPages.Contains(loqateTabPage))
                {
                    apiTabControl.TabPages.Add(loqateTabPage);
                }
            }
            else
            {
                var count = _apiCheckBoxes.Count(cb => cb.Checked);

                if (count != 0)
                {
                    clearLoqateResponse();
                    apiTabControl.TabPages.Remove(loqateTabPage);
                }
                else
                {
                    loqateCheckBox.Checked = true;
                    showNotificatoin("At least one Check Provider should be checked!");
                }
            }
        }

        private void smartyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (smartyCheckBox.Checked)
            {
                if (!apiTabControl.TabPages.Contains(smartyTabPage))
                {
                    apiTabControl.TabPages.Add(smartyTabPage);
                }
            }
            else
            {
                var count = _apiCheckBoxes.Count(cb => cb.Checked);

                if (count != 0)
                {
                    clearSmartyResponse();
                    apiTabControl.TabPages.Remove(smartyTabPage);
                }
                else
                {
                    smartyCheckBox.Checked = true;
                    showNotificatoin("At least one Check Provider should be checked!");
                }
            }
        }

        private void hereCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (hereCheckBox.Checked)
            {
                if (!apiTabControl.TabPages.Contains(hereTabPage))
                {
                    apiTabControl.TabPages.Add(hereTabPage);
                }
            }
            else
            {
                var count = _apiCheckBoxes.Count(cb => cb.Checked);

                if (count != 0)
                {
                    clearHereResponse();
                    apiTabControl.TabPages.Remove(hereTabPage);
                }
                else
                {
                    hereCheckBox.Checked = true;
                    showNotificatoin("At least one Check Provider should be checked!");
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            clearSmartyResponse();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            clearLoqateResponse();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clearGoogleResponse();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clearInputs();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            clearHereResponse();
        }

        private async void CheckAddressForm_Load(object sender, EventArgs e)
        {
            Size = Settings.Default.FormSize;
            Location = Settings.Default.FormLocation;

            countryTextBox.AutoCompleteSource = AutoCompleteSource.CustomSource;

            apiTabControl.TabPages.Remove(googleMapsTabPage);
            apiTabControl.TabPages.Remove(loqateTabPage);
            apiTabControl.TabPages.Remove(smartyTabPage);
            apiTabControl.TabPages.Remove(hereTabPage);

            showApi(_conf);
            await loadAddressesFromFile(_addressesFilePath);
        }

        private async void checkButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                await validation();
            }
            catch (Exception ex)
            {
                showError(ex);
            }

            this.Cursor = Cursors.Arrow;
        }

        private async void autosuggestButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                await autosuggest();
            }
            catch (Exception ex)
            {
                showError(ex);
            }

            this.Cursor = Cursors.Arrow;

        }

        private async void autocompleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                await autocomplete();
            }
            catch (Exception ex)
            {
                showError(ex);
            }

            this.Cursor = Cursors.Arrow;
        }

        private async void insertAddressButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            foreach (var button in _buttonsToControlAddressesFromFile)
            {
                button.Enabled = false;
            }

            var input = getCheckAddressInput();

            if (input.StructuredInput != null)
            {
                var inputFromFile = new InputFromFile
                {
                    City = input.StructuredInput.City,
                    Country = input.Country,
                    StreetAndHouseNumber = input.StructuredInput.StreetAndHouseNumber,
                    District = input.StructuredInput.District,
                    PostalCode = input.StructuredInput.PostalCode
                };

                await insertIntoAddressesFileAt(_addressesFilePath, inputFromFile, 0);
                await loadAddressesFromFile(_addressesFilePath);
            }

            foreach (var button in _buttonsToControlAddressesFromFile)
            {
                button.Enabled = true;
            }

            this.Cursor = Cursors.Arrow;
        }

        private async void updateAddressButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            foreach (var button in _buttonsToControlAddressesFromFile)
            {
                button.Enabled = false;
            }

            var input = getCheckAddressInput();

            if (addressesFromFileListBox.SelectedItem != null)
            {
                var selectedIndex = addressesFromFileListBox.SelectedIndex;

                await removeFromAddressesFileAt(_addressesFilePath, selectedIndex);

                var inputFromFile = new InputFromFile
                {
                    City = input.StructuredInput.City,
                    Country = input.Country,
                    StreetAndHouseNumber = input.StructuredInput.StreetAndHouseNumber,
                    District = input.StructuredInput.District,
                    PostalCode = input.StructuredInput.PostalCode
                };

                await insertIntoAddressesFileAt(_addressesFilePath, inputFromFile, selectedIndex);
                await loadAddressesFromFile(_addressesFilePath);
            }

            foreach (var button in _buttonsToControlAddressesFromFile)
            {
                button.Enabled = true;
            }

            this.Cursor = Cursors.Arrow;
        }

        private async void deleteAddressButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            foreach (var button in _buttonsToControlAddressesFromFile)
            {
                button.Enabled = false;
            }

            if (addressesFromFileListBox.SelectedItem != null)
            {
                await removeFromAddressesFileAt(_addressesFilePath, addressesFromFileListBox.SelectedIndex);
                await loadAddressesFromFile(_addressesFilePath);
            }
            foreach (var button in _buttonsToControlAddressesFromFile)
            {
                button.Enabled = true;
            }

            this.Cursor = Cursors.Arrow;
        }

        private async void saveAsAddressesButton_Click(object sender, EventArgs e)
        {
            var result = saveAsAddressesFileDialog.ShowDialog();

            if (result != DialogResult.OK)
            {
                //Exception
                return;
            }

            var text = await File.ReadAllTextAsync(_addressesFilePath);
            var stream = saveAsAddressesFileDialog.OpenFile();

            using (var streamWriter = new StreamWriter(stream))
            {
                await streamWriter.WriteAsync(text);
            }
        }

        private async void loadAddressesButton_Click(object sender, EventArgs e)
        {
            var result = openAddressesFileDialog.ShowDialog();

            if (result != DialogResult.OK)
            {
                //Exception
                return;
            }

            var addressesFilePath = openAddressesFileDialog.FileName;
            await loadAddressesFromFile(addressesFilePath);
        }

        private void CheckAddressForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.FormSize = Size;
            Settings.Default.FormLocation = Location;
            Settings.Default.Save();
        }
    }
}
