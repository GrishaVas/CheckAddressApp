using System.Reflection;
using CheckAddressApp.Models;
using CheckAddressApp.Services;
using Microsoft.Extensions.Configuration;
using qAcProviderTest.Properties;

namespace CheckAddressApp
{
    public partial class CheckAddressForm : Form
    {
        private ErrorForm _errorForm;
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
        private IEnumerable<InputFromFile> _addressesFromFile;

        private IConfiguration _conf;
        private List<CheckBox> _apiCheckBoxes;
        private IEnumerable<Button> _buttonsToControlAddressesFromFile;
        private string _addressesFilePath = "addresses.csv";

        public CheckAddressForm(IConfiguration conf)
        {
            _smartyService = new SmartyService(conf);
            _googleService = new GoogleService(conf);
            _loqateService = new LoqateService(conf);
            _hereService = new HereService(conf);
            _errorForm = new ErrorForm();
            _conf = conf;

            InitializeComponent();

            _apiCheckBoxes = new List<CheckBox>
            {
                googleMapsCheckBox,
                hereCheckBox,
                loqateCheckBox,
                smartyCheckBox
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

                _addressesFromFile = getInputsFromFile(inputFileStr);

                var items = _addressesFromFile.Select(i => i.GetString());

                addressesFromFileListBox.Items.Clear();
                addressesFromFileListBox.Items.AddRange(items.ToArray());

                addressesFileNameTextBox.Text = path;
            }
        }

        private void clearGoogleResponse()
        {
            _googleAutocompleteResponse = null;
            _googleValidateResponse = null;
            googleResponseListBox.Items.Clear();
            googleResponseDataGridView.Rows.Clear();
        }

        private void clearLoqateResponse()
        {
            _loqateAutocompleteResponse = null;
            _loqateValidateResponse = null;
            loqateResponseDataGridView.Rows.Clear();
            loqateResponseListBox.Items.Clear();
        }

        private void clearSmartyResponse()
        {
            _smartyAutocompleteResponse = null;
            _smartyValidateResponse = null;
            smartyResponseDataGridView.Rows.Clear();
            smartyResponseListBox.Items.Clear();
        }

        private void clearHereResponse()
        {
            _hereAutosuggestResponse = null;
            _hereAutocompleteResponse = null;
            hereResponseListBox.Items.Clear();
            hereResponseDataGridView.Rows.Clear();
            _hereValidateResponse = null;
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
                checkAddressInput.FreeInput = structuredInput.ToString();
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

            inputErrorProvider.SetError(inputsChoiceTabControl, "");
            inputErrorProvider.SetError(countryTextBox, "");
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

        private List<InputFromFile> getInputsFromFile(string inputFileStr)
        {
            var inputsFromFile = new List<InputFromFile>();
            const string rowsSep = "\r\n";
            const char wordsSep = ',';
            var rows = inputFileStr.Split(rowsSep);
            var propNames = rows[0].Split(wordsSep);

            foreach (var row in rows.Skip(1))
            {
                var values = row.Split(wordsSep);
                var inputFormFile = getInputFromFile(propNames, values);

                inputsFromFile.Add(inputFormFile);
            }

            return inputsFromFile;
        }

        private InputFromFile getInputFromFile(string[] propNames, string[] values)
        {
            var inputFormFile = new InputFromFile();
            var inputFormFileType = typeof(InputFromFile);
            var inputFormFileProps = inputFormFileType.GetProperties();

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

        private void fromFileInputAddresseslistBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (addressesFromFileListBox.SelectedItem != null)
            {
                var selectedTab = inputsChoiceTabControl.SelectedTab.Text;
                var inputFromFile = _addressesFromFile.FirstOrDefault(i => i.GetString() == addressesFromFileListBox.SelectedItem.ToString());

                if (selectedTab == "Structured Input")
                {
                    streetAndHouseNumberTextBox.Text = inputFromFile?.StreetAndHouseNumber;
                    postalCodeTextBox.Text = inputFromFile?.PostalCode;
                    cityTextBox.Text = inputFromFile?.City;
                    districtTextBox.Text = inputFromFile?.District;
                    countryTextBox.Text = inputFromFile?.Country;
                }
                else
                {
                    freeInputTextBox.Text = inputFromFile?.GetString(false);
                    countryTextBox.Text = inputFromFile?.Country;
                }
            }
        }

        private void setGoogleOutput(ServiceData output)
        {
            var addresses = output.CheckAddressData.Select(cad => cad.Address);

            googleResponseListBox.Items.Clear();
            googleResponseListBox.Items.AddRange(addresses.ToArray());

            _googleValidateResponse = output.CheckAddressData;
        }
        private void setLoqateOutput(ServiceData output)
        {
            var addresses = output.CheckAddressData.Select(cad => cad.Address);

            loqateResponseListBox.Items.Clear();
            loqateResponseListBox.Items.AddRange(addresses.ToArray());

            _loqateValidateResponse = output.CheckAddressData;
        }

        private void setSmartyOutput(ServiceData output)
        {
            var addresses = output.CheckAddressData.Select(cad => cad.Address);

            smartyResponseListBox.Items.Clear();
            smartyResponseListBox.Items.AddRange(addresses.ToArray());

            _smartyValidateResponse = output.CheckAddressData;
        }

        private void setHereOutput(ServiceData output)
        {
            var addresses = output.CheckAddressData.Select(cad => cad.Address);

            hereResponseListBox.Items.Clear();
            hereResponseListBox.Items.AddRange(addresses.ToArray());

            _hereValidateResponse = output.CheckAddressData;
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

            if (string.IsNullOrEmpty(checkAddressInput.FreeInput))
            {
                inputErrorProvider.SetError(inputsChoiceTabControl, "Inputs cannot be null.");

                return;
            }

            if (googleMapsCheckBox.Checked)
            {
                var googleServiceData = await validation(checkAddressInput, _googleService);

                setGoogleOutput(googleServiceData);
            }
            if (loqateCheckBox.Checked)
            {
                var loqateServiceData = await validation(checkAddressInput, _loqateService);

                setLoqateOutput(loqateServiceData);
            }
            if (smartyCheckBox.Checked)
            {
                if (string.IsNullOrEmpty(checkAddressInput.Country))
                {
                    inputErrorProvider.SetError(countryTextBox, "Country required for Smarty.");

                    return;
                }

                var smartyServiceData = await validation(checkAddressInput, _smartyService);

                setSmartyOutput(smartyServiceData);
            }
            if (hereCheckBox.Checked)
            {
                var hereServiceData = await validation(checkAddressInput, _hereService);

                setHereOutput(hereServiceData);
            }
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

        private void addressTextBox_TextChanged(object sender, EventArgs e)
        {
            inputErrorProvider.SetError(inputsChoiceTabControl, "");
        }

        private void cityTextBox_TextChanged(object sender, EventArgs e)
        {
            inputErrorProvider.SetError(inputsChoiceTabControl, "");
        }

        private void districtTextBox_TextChanged(object sender, EventArgs e)
        {
            inputErrorProvider.SetError(inputsChoiceTabControl, "");
        }

        private void postalCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            inputErrorProvider.SetError(inputsChoiceTabControl, "");
        }

        private void googleMapsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            apiChoiceErrorProvider.SetError(apiGroupBox, "");
            inputErrorProvider.SetError(apiGroupBox, "");

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
                    apiTabControl.TabPages.Remove(googleMapsTabPage);
                }
                else
                {
                    googleMapsCheckBox.Checked = true;
                    apiChoiceErrorProvider.SetError(apiGroupBox, "At least one Check Provider should be checked!");
                }
            }
        }

        private void loqateCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            apiChoiceErrorProvider.SetError(apiGroupBox, "");
            inputErrorProvider.SetError(apiGroupBox, "");

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
                    apiTabControl.TabPages.Remove(loqateTabPage);
                }
                else
                {
                    loqateCheckBox.Checked = true;
                    apiChoiceErrorProvider.SetError(apiGroupBox, "At least one Check Provider should be checked!");
                }
            }
        }

        private void smartyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            apiChoiceErrorProvider.SetError(apiGroupBox, "");
            inputErrorProvider.SetError(apiGroupBox, "");

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
                    apiTabControl.TabPages.Remove(smartyTabPage);
                }
                else
                {
                    smartyCheckBox.Checked = true;
                    apiChoiceErrorProvider.SetError(apiGroupBox, "At least one Check Provider should be checked!");
                }
            }
        }

        private void hereCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            apiChoiceErrorProvider.SetError(apiGroupBox, "");
            inputErrorProvider.SetError(apiGroupBox, "");

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
                    apiTabControl.TabPages.Remove(hereTabPage);
                }
                else
                {
                    hereCheckBox.Checked = true;
                    apiChoiceErrorProvider.SetError(apiGroupBox, "At least one Check Provider should be checked!");
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

        private void inputsChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputErrorProvider.SetError(inputsChoiceTabControl, "");
            clearInputs();
        }

        private void freeInputTextBox_TextChanged(object sender, EventArgs e)
        {
            inputErrorProvider.SetError(inputsChoiceTabControl, "");
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

        private void countryTextBox_TextChanged(object sender, EventArgs e)
        {
            inputErrorProvider.SetError(countryTextBox, "");
        }

        private async void autosuggestButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                var checkAddressInput = getCheckAddressInput();

                if (string.IsNullOrEmpty(checkAddressInput.FreeInput))
                {
                    inputErrorProvider.SetError(inputsChoiceTabControl, "Inputs cannot be null.");

                    return;
                }

                if (hereCheckBox.Checked)
                {
                    var hereServiceData = await autosuggest(checkAddressInput, _hereService);

                    setHereOutput(hereServiceData);
                }
                else
                {
                    inputErrorProvider.SetError(apiGroupBox, "Autosuggest is supported only by Here.");
                }
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

                var checkAddressInput = getCheckAddressInput();

                if (string.IsNullOrEmpty(checkAddressInput.FreeInput))
                {
                    inputErrorProvider.SetError(inputsChoiceTabControl, "Inputs cannot be null.");

                    return;
                }

                if (googleMapsCheckBox.Checked)
                {
                    var googleServiceData = await autocomplete(checkAddressInput, _googleService);

                    setGoogleOutput(googleServiceData);
                }
                if (loqateCheckBox.Checked)
                {
                    var loqateServiceData = await autocomplete(checkAddressInput, _loqateService);

                    setLoqateOutput(loqateServiceData);
                }
                if (smartyCheckBox.Checked)
                {
                    if (string.IsNullOrEmpty(checkAddressInput.Country))
                    {
                        inputErrorProvider.SetError(countryTextBox, "Country required for Smarty.");

                        return;
                    }

                    var smartyServiceData = await autocomplete(checkAddressInput, _smartyService);

                    setSmartyOutput(smartyServiceData);
                }
                if (hereCheckBox.Checked)
                {
                    var hereServiceData = await autocomplete(checkAddressInput, _hereService);

                    setHereOutput(hereServiceData);
                }
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

            _addressesFilePath = openAddressesFileDialog.FileName;
            await loadAddressesFromFile(_addressesFilePath);
        }

        private void addressesSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            var search = addressesSearchTextBox.Text;
            IEnumerable<InputFromFile> addresses;

            if (!string.IsNullOrEmpty(search))
            {
                addresses = _addressesFromFile.Where(a => a.GetString(true).ToLower().Contains(search.ToLower()));
            }
            else
            {
                addresses = _addressesFromFile;
            }

            var items = addresses.Select(i => i.GetString());

            addressesFromFileListBox.Items.Clear();
            addressesFromFileListBox.Items.AddRange(items.ToArray());
        }

        private void CheckAddressForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.FormSize = Size;
            Settings.Default.FormLocation = Location;
            Settings.Default.Save();
        }
    }
}
