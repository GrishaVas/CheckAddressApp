using CheckAddressApp.Models;
using CheckAddressApp.Services;
using Microsoft.Extensions.Configuration;

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
        private IEnumerable<InputFromFile> _inputsFromFile;
        private List<(string Name, string ISO2, string ISO3)> countries = new List<(string Name, string ISO2, string ISO3)>
        {
            {("United States","US","USA")},
            {("Slovakia","SK","SVK")},
            {("Slovenia","SI","SVN")},
            {("Singapore","SG","SGP")},
            {("Sweden","SE","SWE")},
            {("Portugal","PT","PRT")},
            {("Puerto Rico","PR","PRI")},
            {("Poland","PL","POL")},
            {("New Zealand","NZ","NZL")},
            {("Norway","NO","NOR")},
            {("Netherlands","NL","NLD")},
            {("Malaysia","MY","MYS")},
            {("Mexico","MX","MEX")},
            {("Latvia","LV","LVA")},
            {("Luxembourg","LU","LUX")},
            {("Lithuania","LT","LTU")},
            {("Italy","IT","ITA")},
            {("India","IN","IND")},
            {("Ireland","IE","IRL")},
            {("Hungary","HU","HUN")},
            {("Croatia","HR","HRV")},
            {("United kingdom","GB","GBR")},
            {("France","FR","FRA")},
            {("Finland","FI","FIN")},
            {("Spain","ES","ESP")},
            {("Estonia","EE","EST")},
            {("Denmark","DK","DNK")},
            {("Germany","DE","DEU")},
            {("Czechia","CZ","CZE")},
            {("Colombia","CO","COL")},
            {("Chile","CL","CHL")},
            {("Switzerland","CH","CHE")},
            {("Canada","CA","CAN")},
            {("Brazil","BR","BRA")},
            {("Bulgaria","BG","BGR")},
            {("Belgium","BE","BEL")},
            {("Australia","AU","AUS")},
            {("Austria","AT","AUT")},
            {("Argentina","AR","ARG")}
        };
        private bool cleareSelectedFromFileInputAddressesListBox = true;

        public CheckAddressForm(IConfiguration conf)
        {
            _smartyService = new SmartyService(conf);
            _googleService = new GoogleService(conf);
            _loqateService = new LoqateService(conf);
            _hereService = new HereService(conf);
            _errorForm = new ErrorForm();

            InitializeComponent();
        }
        ~CheckAddressForm()
        {
            _loqateService.Dispose();
            _hereService.Dispose();
            _errorForm.Dispose();
        }

        private int checkedApiChoicesCount()
        {
            var count = apiGroupBox.Controls.OfType<CheckBox>().Where(cb => cb.Checked).Count();

            return count;
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

        private (string ISO2, string ISO3) getCoutryCodeFromInput()
        {
            var country = countryTextBox.Text.ToLower();
            var countryCode = countries.FirstOrDefault(c => c.Name.ToLower() == country);

            return (countryCode.ISO2 ?? "", countryCode.ISO3 ?? "");
        }

        private void fillRequestAddress(string input)
        {
            var countryCodes = getCoutryCodeFromInput();
            var requestAddress = input +
                (countryCodes.ISO2 != null ? $" | Country code: {countryCodes.ISO2}" : "");

            requestAddressTextBox.Text = requestAddress;
        }

        private string getOneLineInput()
        {
            var input = (postalCodeTextBox.Text != "" ? $"{postalCodeTextBox.Text} " : "") +
                (cityTextBox.Text != "" ? $"{cityTextBox.Text} " : "") +
                (districtTextBox.Text != "" ? $"{districtTextBox.Text} " : "") +
                (streetAndHouseNumberTextBox.Text != "" ? $"{streetAndHouseNumberTextBox.Text} " : "");

            return input;
        }

        private async Task loqateValidation(StructuredInput input)
        {
            _loqateValidateResponse = await _loqateService.ValidateAddress(input);

            setLoqateOutput(_loqateValidateResponse);
        }

        private async Task loqateValidation(string input, string countryCode)
        {
            _loqateValidateResponse = await _loqateService.ValidateAddress(input, countryCode);

            setLoqateOutput(_loqateValidateResponse);
        }

        private void setLoqateOutput(IEnumerable<CheckAddressData> response)
        {
            var addresses = response.Select(cad => cad.Address);

            if (addresses != null || addresses.Count() > 0)
            {
                loqateResponseListBox.Items.AddRange(addresses.ToArray());
            }
        }

        private async Task googleValidation(StructuredInput input)
        {
            _googleValidateResponse = await _googleService.ValidateAddress(input);

            setGoogleOutput(_googleValidateResponse);
        }

        private async Task googleValidation(string input, string countryCode)
        {
            _googleValidateResponse = await _googleService.ValidateAddress(input, countryCode);

            setGoogleOutput(_googleValidateResponse);
        }

        private void setGoogleOutput(IEnumerable<CheckAddressData> response)
        {
            var addresses = response.Select(cad => cad.Address);

            googleResponseListBox.Items.AddRange(addresses.ToArray());
        }

        private void smartyValidation(string input, string countryCode)
        {
            if (string.IsNullOrEmpty(countryCode))
            {
                smartyCountryCodeErrorProvider.SetError(countryTextBox, "The Country Code required for smarty API.");

                return;
            }

            _smartyValidateResponse = _smartyService.ValidateAddress(input, countryCode);
            setSmartyOutput(_smartyValidateResponse);
        }

        private void smartyValidation(StructuredInput input)
        {
            if (string.IsNullOrEmpty(input.CountryCode2))
            {
                smartyCountryCodeErrorProvider.SetError(countryTextBox, "The Country Code required for smarty API.");

                return;
            }

            _smartyValidateResponse = _smartyService.ValidateAddress(input);
            setSmartyOutput(_smartyValidateResponse);
        }

        private void setSmartyOutput(IEnumerable<CheckAddressData> response)
        {
            var rows = response.Select(cad => cad.Address);

            smartyResponseListBox.Items.AddRange(rows.ToArray());
        }

        private async Task hereValidation(string input, string countryCode)
        {
            _hereValidateResponse = await _hereService.ValidateAddress(input, countryCode);

            setHereOutput(_hereValidateResponse);
        }

        private async Task hereValidation(StructuredInput input)
        {
            _hereValidateResponse = await _hereService.ValidateAddress(input);

            setHereOutput(_hereValidateResponse);
        }

        private void setHereOutput(IEnumerable<CheckAddressData> response)
        {
            var addresses = response.Select(cad => cad.Address);

            hereResponseListBox.Items.AddRange(addresses.ToArray());
        }

        private async Task loqateAutocomplete(string input, string countryCode)
        {
            _loqateAutocompleteResponse = await _loqateService.AutocompleteAddress(input, countryCode);
            var suggestions = _loqateAutocompleteResponse.Select(cad => cad.Address);

            if (suggestions != null && suggestions.Count() > 0)
            {
                loqateResponseListBox.Items.AddRange(suggestions.ToArray());
            }
        }

        private async Task googleAutocomplete(string input, string countryCode)
        {
            _googleAutocompleteResponse = await _googleService.AutocompleteAddress(input, countryCode);
            var suggestions = _googleAutocompleteResponse.Select(cad => cad.Address);

            if (suggestions != null && suggestions.Count() > 0)
            {
                googleResponseListBox.Items.AddRange(suggestions.ToArray());
            }
        }

        private void smartyAutocomplete(string input, string countryCode)
        {
            if (string.IsNullOrEmpty(countryCode))
            {
                smartyCountryCodeErrorProvider.SetError(countryTextBox, "The Country Code required for Smarty Provider.");

                return;
            }

            _smartyAutocompleteResponse = _smartyService.AutocompleteAddress(input, countryCode);
            var suggestions = _smartyAutocompleteResponse.Select(cad => cad.Address);

            smartyResponseListBox.Items.AddRange(suggestions.ToArray());
        }

        private async Task hereAutocomplete(string input, string countryCode)
        {
            _hereAutocompleteResponse = await _hereService.AutocompleteAddress(input, countryCode);
            var suggestions = _hereAutocompleteResponse.Select(cad => cad.Address);

            if (suggestions != null)
            {
                hereResponseListBox.Items.AddRange(suggestions.ToArray());
            }
        }

        private async Task hereAutosuggest(string input, string countryCode)
        {
            _hereAutosuggestResponse = await _hereService.AutosuggestAddress(input, countryCode);
            var suggestions = _hereAutosuggestResponse.Select(cad => cad.Address);

            hereResponseListBox.Items.AddRange(suggestions.ToArray());
        }

        private async Task autocomplete(string input)
        {
            fillRequestAddress(input);

            var countryCodes = getCoutryCodeFromInput();

            if (googleMapsCheckBox.Checked)
            {
                clearGoogleResponse();
                await googleAutocomplete(input, countryCodes.ISO2);
            }

            if (loqateCheckBox.Checked)
            {
                clearLoqateResponse();
                await loqateAutocomplete(input, countryCodes.ISO2);
            }

            if (smartyCheckBox.Checked)
            {
                clearSmartyResponse();
                smartyAutocomplete(input, countryCodes.ISO2);
            }

            if (hereCheckBox.Checked)
            {
                clearHereResponse();
                await hereAutocomplete(input, countryCodes.ISO3);
            }
        }

        private async Task autosuggest(string input)
        {
            if (!hereCheckBox.Checked)
            {
                inputErrorProvider.SetError(autocompleteAutosuggestSplitButton, "Api does not support autosuggest");

                return;
            }

            var countryCodes = getCoutryCodeFromInput();

            if (hereCheckBox.Checked && !googleMapsCheckBox.Checked && !loqateCheckBox.Checked && !smartyCheckBox.Checked)
            {
                clearHereResponse();
                await hereAutosuggest(input, countryCodes.ISO3);
            }
            else
            {
                inputErrorProvider.SetError(autocompleteAutosuggestSplitButton, "Api does not support autosuggest");
            }
        }
        private async Task autocompleteAutosuggest()
        {
            var selectedInputType = inputsChoiceTabControl.SelectedTab.Text;
            string input;

            if (selectedInputType == "Structured Input")
            {
                input = getOneLineInput();

                if (string.IsNullOrEmpty(input.Trim()))
                {
                    inputErrorProvider.SetError(inputsChoiceTabControl, "At least one input must be filled.");

                    return;
                }
            }
            else
            {
                input = freeInputTextBox.Text;

                if (string.IsNullOrEmpty(input.Trim()))
                {
                    inputErrorProvider.SetError(inputsChoiceTabControl, "Address required.");

                    return;
                }
            }

            if (autocompleteAutosuggestSplitButton.Text == "Autocomplete")
            {
                await autocomplete(input);
            }
            else
            {
                await autosuggest(input);
            }
        }

        private async Task validation(StructuredInput input)
        {
            if (googleMapsCheckBox.Checked)
            {
                clearGoogleResponse();
                await googleValidation(input);
            }

            if (loqateCheckBox.Checked)
            {
                clearLoqateResponse();
                await loqateValidation(input);
            }

            if (smartyCheckBox.Checked)
            {
                clearSmartyResponse();
                smartyValidation(input);
            }

            if (hereCheckBox.Checked)
            {
                clearHereResponse();
                await hereValidation(input);
            }
        }

        private async Task validation(string input)
        {
            var countryCodes = getCoutryCodeFromInput();

            if (googleMapsCheckBox.Checked)
            {
                clearGoogleResponse();
                await googleValidation(input, countryCodes.ISO2);
            }

            if (loqateCheckBox.Checked)
            {
                clearLoqateResponse();
                await loqateValidation(input, countryCodes.ISO2);
            }

            if (smartyCheckBox.Checked)
            {
                clearSmartyResponse();
                smartyValidation(input, countryCodes.ISO2);
            }

            if (hereCheckBox.Checked)
            {
                clearHereResponse();
                await hereValidation(input, countryCodes.ISO3);
            }
        }

        private async Task validation()
        {
            var selectedInputType = inputsChoiceTabControl.SelectedTab.Text;
            StructuredInput structuredInput;

            if (selectedInputType == "Structured Input")
            {
                structuredInput = getStructuredInput();

                fillRequestAddress(structuredInput.Input);

                if (string.IsNullOrEmpty(structuredInput.Input.Trim()))
                {
                    inputErrorProvider.SetError(inputsChoiceTabControl, "At least one input must be filled.");

                    return;
                }

                await validation(structuredInput);
            }
            else
            {

                var input = freeInputTextBox.Text;

                fillRequestAddress(input);

                if (string.IsNullOrEmpty(input.Trim()))
                {
                    inputErrorProvider.SetError(inputsChoiceTabControl, "Address required.");

                    return;
                }

                await validation(input);
            }
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
            smartyCountryCodeErrorProvider.SetError(countryTextBox, "");
        }

        private StructuredInput getStructuredInput()
        {
            var countryCodes = getCoutryCodeFromInput();
            var input = new StructuredInput
            {
                StreetAndHouseNumber = streetAndHouseNumberTextBox.Text,
                City = cityTextBox.Text,
                District = districtTextBox.Text,
                PostalCode = postalCodeTextBox.Text,
                CountryCode2 = countryCodes.ISO2,
                CountryCode3 = countryCodes.ISO3,
                Input = getOneLineInput()
            };

            return input;
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

        private async void autocompleteAutosuggestSplitButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                await autocompleteAutosuggest();
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
            fromFileInputAddressesListBoxCleareSelected();
        }

        private void cityTextBox_TextChanged(object sender, EventArgs e)
        {
            inputErrorProvider.SetError(inputsChoiceTabControl, "");
            fromFileInputAddressesListBoxCleareSelected();
        }

        private void districtTextBox_TextChanged(object sender, EventArgs e)
        {
            inputErrorProvider.SetError(inputsChoiceTabControl, "");
            fromFileInputAddressesListBoxCleareSelected();
        }

        private void postalCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            inputErrorProvider.SetError(inputsChoiceTabControl, "");
            fromFileInputAddressesListBoxCleareSelected();
        }

        private void googleMapsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            apiChoiceErrorProvider.SetError(apiGroupBox, "");
            inputErrorProvider.SetError(autocompleteAutosuggestSplitButton, "");

            var count = checkedApiChoicesCount();
            if (count == 0)
            {
                googleMapsCheckBox.Checked = true;
                apiChoiceErrorProvider.SetError(apiGroupBox, "At least one Check Provider should be checked!");

                return;
            }

            if (googleMapsCheckBox.Checked)
            {
                if (count != 1)
                {
                    apiTabControl.TabPages.Add(googleMapsTabPage);
                }
            }
            else
            {
                apiTabControl.TabPages.Remove(googleMapsTabPage);
            }
        }

        private void loqateCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            apiChoiceErrorProvider.SetError(apiGroupBox, "");
            inputErrorProvider.SetError(autocompleteAutosuggestSplitButton, "");

            var count = checkedApiChoicesCount();
            if (count == 0)
            {
                loqateCheckBox.Checked = true;
                apiChoiceErrorProvider.SetError(apiGroupBox, "At least one Check Provider should be checked!");

                return;
            }

            if (loqateCheckBox.Checked)
            {
                if (count != 1)
                {
                    apiTabControl.TabPages.Add(loqateTabPage);
                }
            }
            else
            {
                apiTabControl.TabPages.Remove(loqateTabPage);
            }
        }

        private void smartyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            apiChoiceErrorProvider.SetError(apiGroupBox, "");
            inputErrorProvider.SetError(autocompleteAutosuggestSplitButton, "");

            var count = checkedApiChoicesCount();
            if (count == 0)
            {
                smartyCheckBox.Checked = true;
                apiChoiceErrorProvider.SetError(apiGroupBox, "At least one Check Provider should be checked!");

                return;
            }

            if (smartyCheckBox.Checked)
            {
                if (count != 1)
                {
                    apiTabControl.TabPages.Add(smartyTabPage);
                }
            }
            else
            {
                apiTabControl.TabPages.Remove(smartyTabPage);
            }
        }

        private void hereCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            apiChoiceErrorProvider.SetError(apiGroupBox, "");
            inputErrorProvider.SetError(autocompleteAutosuggestSplitButton, "");

            var count = checkedApiChoicesCount();
            if (count == 0)
            {
                hereCheckBox.Checked = true;
                apiChoiceErrorProvider.SetError(apiGroupBox, "At least one Check Provider should be checked!");

                return;
            }

            if (hereCheckBox.Checked)
            {
                if (count != 1)
                {
                    apiTabControl.TabPages.Add(hereTabPage);
                }
            }
            else
            {
                apiTabControl.TabPages.Remove(hereTabPage);
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

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            autocompleteAutosuggestSplitButton.Text = e.ClickedItem.Text;
            inputErrorProvider.SetError(autocompleteAutosuggestSplitButton, "");
        }

        private void inputsChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputErrorProvider.SetError(inputsChoiceTabControl, "");
            clearInputs();
        }

        private void freeInputTextBox_TextChanged(object sender, EventArgs e)
        {
            inputErrorProvider.SetError(inputsChoiceTabControl, "");
            fromFileInputAddressesListBoxCleareSelected();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            clearHereResponse();
        }

        private void CheckAddressForm_Load(object sender, EventArgs e)
        {
            countryTextBox.AutoCompleteSource = AutoCompleteSource.CustomSource;

            apiTabControl.TabPages.Remove(loqateTabPage);
            apiTabControl.TabPages.Remove(smartyTabPage);
            apiTabControl.TabPages.Remove(hereTabPage);
        }

        private void countryTextBox_TextChanged(object sender, EventArgs e)
        {
            smartyCountryCodeErrorProvider.SetError(countryTextBox, "");
            fromFileInputAddressesListBoxCleareSelected();
        }

        private async void loadFromFileButton_Click(object sender, EventArgs e)
        {
            var inputFileOpenFileDialogResult = InputFileOpenFileDialog.ShowDialog();

            if (inputFileOpenFileDialogResult == DialogResult.OK)
            {
                var stream = InputFileOpenFileDialog.OpenFile();
                var inputFileStr = "";

                using (var reader = new StreamReader(stream))
                {
                    inputFileStr = await reader.ReadToEndAsync();
                }

                _inputsFromFile = getInputsFromFile(inputFileStr);
                var items = _inputsFromFile.Select(i => i.GetString());

                fromFileInputAddressesListBox.Items.Clear();
                fromFileInputAddressesListBox.Items.AddRange(items.ToArray());
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
            if (fromFileInputAddressesListBox.SelectedItem != null)
            {
                var selectedTab = inputsChoiceTabControl.SelectedTab.Text;
                var inputFromFile = _inputsFromFile.FirstOrDefault(i => i.GetString() == fromFileInputAddressesListBox.SelectedItem.ToString());

                cleareSelectedFromFileInputAddressesListBox = false;

                if (selectedTab == "Structured Input")
                {
                    streetAndHouseNumberTextBox.Text = inputFromFile?.StreetAndHouseNumber;
                    postalCodeTextBox.Text = inputFromFile?.PostalCode;
                    cityTextBox.Text = inputFromFile?.City;
                    districtTextBox.Text = inputFromFile?.District;
                    countryTextBox.Text = inputFromFile?.CountryCode;
                }
                else
                {
                    freeInputTextBox.Text = inputFromFile?.GetString(false);
                    countryTextBox.Text = inputFromFile?.CountryCode;
                }

                cleareSelectedFromFileInputAddressesListBox = true;
            }
        }

        private void fromFileInputAddressesListBoxCleareSelected()
        {
            if (cleareSelectedFromFileInputAddressesListBox)
            {
                fromFileInputAddressesListBox.ClearSelected();
            }
        }
    }
}
