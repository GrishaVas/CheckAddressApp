using System.Reflection;
using System.Timers;
using CheckAddressApp.Models;
using ISO3166;
using Microsoft.Extensions.Configuration;
using qAcProviderTest;
using qAcProviderTest.Models.CheckAddressServiceModels;
using qAcProviderTest.Properties;
using qAcProviderTest.Services;

namespace CheckAddressApp
{
    public partial class CheckAddressForm : Form
    {
        private UserNotificationForm _userNotificationForm;
        private CheckAddressService _checkAddressService;
        private IEnumerable<CheckAddressAddressData> _loqateResponse;
        private IEnumerable<CheckAddressAddressData> _smartyResponse;
        private IEnumerable<CheckAddressAddressData> _hereResponse;
        private IEnumerable<CheckAddressAddressData> _googleResponse;
        private IEnumerable<AddressFromFile> _addressesFromFile;
        private IConfiguration _conf;
        private IEnumerable<Button> _buttonsToControlAddressesFromFile;
        private string _addressesFilePath = "addresses.csv";
        private string _oneLineInputFormat;
        private System.Timers.Timer _addressesFromFileSearchTimer;

        public CheckAddressForm(IConfiguration conf)
        {
            _checkAddressService = new CheckAddressService(conf);
            _userNotificationForm = new UserNotificationForm(this);
            _conf = conf;

            _addressesFromFileSearchTimer = new System.Timers.Timer
            {
                AutoReset = false,
                Interval = 1000,
                SynchronizingObject = this
            };

            _addressesFromFileSearchTimer.Elapsed += new ElapsedEventHandler(searchAddressesFromFile);

            InitializeComponent();

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
        }

        private void writeResponseOfSelectedAddress(int selectedIndex, IEnumerable<CheckAddressAddressData> serviceResponse, DataGridView outputGrid)
        {
            CheckAddressAddressData checkAddressData = null;

            if (serviceResponse != null)
            {
                if (serviceResponse.Count() <= selectedIndex)
                {
                    return;
                }

                checkAddressData = serviceResponse.ElementAt(selectedIndex);
            }

            var rows = checkAddressData?.Fields;

            if (rows == null)
            {
                return;
            }

            outputGrid.Rows.Clear();

            foreach (var row in rows)
            {
                outputGrid.Rows.Add(row.Name, row.Value);
            }
        }

        private void setAddresses(IEnumerable<AddressFromFile> inputs, string fileName)
        {
            _addressesFromFile = inputs;
            _addressesFilePath = fileName;

            var addresses = inputs.Select(a => a.GetString(_oneLineInputFormat)).ToArray();

            updateFromFileListBoxAddresses(addresses, fileName);
        }

        private void updateFromFileListBoxAddresses(string[] addresses, string fileName)
        {
            addressesFileNameTextBox.Text = fileName;
            addressesFromFileListBox.Rows = addresses;
            addressesSearchTextBox.Clear();
        }

        private void clearGoogleResponse()
        {
            _googleResponse = null;
            googleResponseListBox.Items.Clear();
            googleResponseDataGridView.Rows.Clear();
            googleResponseTimeLabel.Text = "Response time: 0,0s";
        }

        private void clearLoqateResponse()
        {
            _loqateResponse = null;
            loqateResponseDataGridView.Rows.Clear();
            loqateResponseListBox.Items.Clear();
            loqateResponseTimeLabel.Text = "Response time: 0,0s";
        }

        private void clearSmartyResponse()
        {
            _smartyResponse = null;
            smartyResponseDataGridView.Rows.Clear();
            smartyResponseListBox.Items.Clear();
            smartyResponseTimeLabel.Text = "Response time: 0,0s";
        }

        private void clearHereResponse()
        {
            hereResponseListBox.Items.Clear();
            hereResponseDataGridView.Rows.Clear();
            _hereResponse = null;
            hereResponseTimeLabel.Text = "Response time: 0,0s";
        }

        private CheckAddressInput getCheckAddressInput()
        {
            var countryName = countryTextBox.Text;
            var country = CheckAddressService.GetCountry(countryName);
            var inputName = inputsChoiceTabControl.SelectedTab?.Text;

            if (string.IsNullOrEmpty(inputName))
            {
                throw new Exception("Input type is not selected.");
            }

            CheckAddressInput checkAddressInput;

            if (inputName == "Structured Input")
            {
                checkAddressInput = new CheckAddressStructuredInput(streetAndHouseNumberTextBox.Text,
                    cityTextBox.Text,
                    districtTextBox.Text,
                    postalCodeTextBox.Text,
                    country,
                    _oneLineInputFormat);
            }
            else
            {
                checkAddressInput = new CheckAddressFreeInput()
                {
                    FullString = freeInputTextBox.Text,
                    Country = country
                };
            }

            requestAddressTextBox.Text = checkAddressInput.FullString;

            setCheckAddressProviders(checkAddressInput);

            return checkAddressInput;
        }

        private void setCheckAddressProviders(CheckAddressInput checkAddressInput)
        {
            if (googleMapsCheckBox.Checked)
            {
                checkAddressInput.AddressProviders |= AddressProvider.Google;
            }

            if (loqateCheckBox.Checked)
            {
                checkAddressInput.AddressProviders |= AddressProvider.Loqate;
            }

            if (smartyCheckBox.Checked)
            {
                checkAddressInput.AddressProviders |= AddressProvider.Smarty;
            }

            if (hereCheckBox.Checked)
            {
                checkAddressInput.AddressProviders |= AddressProvider.Here;
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

        private void setOutputs(Dictionary<AddressProvider, CheckAddressOutput> outputs)
        {
            if (outputs.ContainsKey(AddressProvider.Google))
            {
                var addresses = outputs[AddressProvider.Google].Addresses;
                var time = outputs[AddressProvider.Google].Time;

                setGoogleOutput(addresses, time);
            }

            if (outputs.ContainsKey(AddressProvider.Loqate))
            {
                var addresses = outputs[AddressProvider.Loqate].Addresses;
                var time = outputs[AddressProvider.Loqate].Time;

                setLoqateOutput(addresses, time);
            }

            if (outputs.ContainsKey(AddressProvider.Smarty))
            {
                var addresses = outputs[AddressProvider.Smarty].Addresses;
                var time = outputs[AddressProvider.Smarty].Time;

                setSmartyOutput(addresses, time);
            }

            if (outputs.ContainsKey(AddressProvider.Here))
            {
                var addresses = outputs[AddressProvider.Here].Addresses;
                var time = outputs[AddressProvider.Here].Time;

                setHereOutput(addresses, time);
            }
        }

        private void setOutput(IEnumerable<CheckAddressAddressData> output, double time, ListBox listBoxReponse, out IEnumerable<CheckAddressAddressData> response, Label labelTime)
        {
            var addresses = output.Select(cad => cad.Address);

            if (addresses.Count() < 1)
            {
                listBoxReponse.Items.Add("None");
            }
            else
            {
                listBoxReponse.Items.AddRange(addresses.ToArray());
            }

            response = output;
            labelTime.Text = $"Response time: {Math.Round(time, 3)}s";
        }

        private void setGoogleOutput(IEnumerable<CheckAddressAddressData> output, double time)
        {
            clearGoogleResponse();

            setOutput(output, time, googleResponseListBox, out _googleResponse, googleResponseTimeLabel);
        }

        private void setLoqateOutput(IEnumerable<CheckAddressAddressData> output, double time)
        {
            clearLoqateResponse();

            setOutput(output, time, loqateResponseListBox, out _loqateResponse, loqateResponseTimeLabel);
        }

        private void setSmartyOutput(IEnumerable<CheckAddressAddressData> output, double time)
        {
            clearSmartyResponse();

            setOutput(output, time, smartyResponseListBox, out _smartyResponse, smartyResponseTimeLabel);
        }

        private void setHereOutput(IEnumerable<CheckAddressAddressData> output, double time)
        {
            clearHereResponse();

            setOutput(output, time, hereResponseListBox, out _hereResponse, hereResponseTimeLabel);
        }

        private async void searchAddressesFromFile(object source, ElapsedEventArgs e)
        {
            await Task.Run(() =>
            {
                var search = addressesSearchTextBox.Text;
                string[] addresses;

                if (!string.IsNullOrEmpty(search))
                {
                    addresses = _addressesFromFile.Where(a => a.ToString().ToLower().Contains(search.ToLower())).Select(i => i.ToString()).ToArray();
                }
                else
                {
                    addresses = _addressesFromFile.Select(i => i.ToString()).ToArray();
                }


                if (this.addressesFromFileListBox.InvokeRequired)
                {
                    this.Invoke(() =>
                    {
                        addressesFromFileListBox.Rows = addresses.ToArray();
                    });
                }
                else
                {
                    addressesFromFileListBox.Rows = addresses.ToArray();
                }
            });
        }

        private async Task errorHandlingAndCursorLoading(Func<Task> func)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                await func();
            }
            catch (Exception ex)
            {
                showNotificatoin($"{ex.Message}");
            }

            this.Cursor = Cursors.Arrow;
        }

        private void errorHandlingAndCursorLoading(Action action)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                action();
            }
            catch (Exception ex)
            {
                showNotificatoin(ex.Message);
            }

            this.Cursor = Cursors.Arrow;
        }

        private void loqateResponseListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = loqateResponseListBox.SelectedIndex;

            writeResponseOfSelectedAddress(index, _loqateResponse, loqateResponseDataGridView);
        }

        private void smartyResponseListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = smartyResponseListBox.SelectedIndex;

            writeResponseOfSelectedAddress(index, _smartyResponse, smartyResponseDataGridView);
        }

        private void hereResponseListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = hereResponseListBox.SelectedIndex;

            writeResponseOfSelectedAddress(index, _hereResponse, hereResponseDataGridView);
        }

        private void googleResponseListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = googleResponseListBox.SelectedIndex;

            writeResponseOfSelectedAddress(index, _googleResponse, googleResponseDataGridView);
        }

        private void inputsChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearInputs();
        }

        private void fromFileInputAddresseslistBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorHandlingAndCursorLoading(() =>
            {
                if (addressesFromFileListBox.SelectedItem != null)
                {
                    var selectedTab = inputsChoiceTabControl.SelectedTab?.Text;

                    if (string.IsNullOrEmpty(selectedTab))
                    {
                        throw new Exception("Input type is not selected.");
                    }

                    var inputFromFile = _addressesFromFile.FirstOrDefault(i => i.GetString(_oneLineInputFormat) == addressesFromFileListBox.SelectedItem.ToString());

                    if (selectedTab == "Structured Input")
                    {
                        streetAndHouseNumberTextBox.Text = inputFromFile.StreetAndHouseNumber;
                        postalCodeTextBox.Text = inputFromFile.PostalCode;
                        cityTextBox.Text = inputFromFile.City;
                        districtTextBox.Text = inputFromFile.District;
                        countryTextBox.Text = inputFromFile.Country;
                    }
                    else
                    {
                        freeInputTextBox.Text = inputFromFile.GetString(_oneLineInputFormat, false);
                        countryTextBox.Text = inputFromFile.Country;
                    }
                }
            });
        }

        private void addressesSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!_addressesFromFileSearchTimer.Enabled)
            {
                _addressesFromFileSearchTimer.Start();
            }
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
                clearGoogleResponse();
                apiTabControl.TabPages.Remove(googleMapsTabPage);
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
                clearLoqateResponse();
                apiTabControl.TabPages.Remove(loqateTabPage);
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
                clearSmartyResponse();
                apiTabControl.TabPages.Remove(smartyTabPage);
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
                clearHereResponse();
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

        private void button5_Click(object sender, EventArgs e)
        {
            clearHereResponse();
        }

        private async void checkButton_Click(object sender, EventArgs e)
        {
            await errorHandlingAndCursorLoading(async () =>
            {
                var input = getCheckAddressInput();
                var outputs = await _checkAddressService.Validation(input);

                setOutputs(outputs);
            });
        }

        private async void autosuggestButton_Click(object sender, EventArgs e)
        {
            await errorHandlingAndCursorLoading(async () =>
            {
                var input = getCheckAddressInput();
                var outputs = await _checkAddressService.Autosuggest(input);

                setOutputs(outputs);
            });
        }

        private async void autocompleteButton_Click(object sender, EventArgs e)
        {
            await errorHandlingAndCursorLoading(async () =>
            {
                var input = getCheckAddressInput();
                var outputs = await _checkAddressService.Autocomplete(input);

                setOutputs(outputs);
            });
        }

        private async void insertAddressButton_Click(object sender, EventArgs e)
        {
            foreach (var button in _buttonsToControlAddressesFromFile)
            {
                button.Enabled = false;
            }

            await errorHandlingAndCursorLoading(async () =>
            {
                var input = getCheckAddressInput();

                if (input is CheckAddressStructuredInput)
                {
                    var structuredInput = (CheckAddressStructuredInput)input;
                    var inputFromFile = new AddressFromFile
                    {
                        City = structuredInput.City,
                        Country = structuredInput.Country?.Name,
                        StreetAndHouseNumber = structuredInput.StreetAndHouseNumber,
                        District = structuredInput.District,
                        PostalCode = structuredInput.PostalCode
                    };

                    await CsvFileService.insertIntoAddressesFile(_addressesFilePath, inputFromFile, 0);
                    var inputs = await CsvFileService.getCheckAddressInputsFromCsvFile(_addressesFilePath, _oneLineInputFormat);
                    setAddresses(inputs, _addressesFilePath);
                }
            });

            foreach (var button in _buttonsToControlAddressesFromFile)
            {
                button.Enabled = true;
            }
        }

        private async void updateAddressButton_Click(object sender, EventArgs e)
        {
            foreach (var button in _buttonsToControlAddressesFromFile)
            {
                button.Enabled = false;
            }

            await errorHandlingAndCursorLoading(async () =>
            {
                var input = getCheckAddressInput();

                if (addressesFromFileListBox.SelectedItem != null)
                {
                    if (!(input is CheckAddressStructuredInput))
                    {
                        return;
                    }

                    var structuredInput = (CheckAddressStructuredInput)input;
                    var selectedIndex = addressesFromFileListBox.SelectedIndex;

                    await CsvFileService.removeFromAddressesFile(_addressesFilePath, selectedIndex);

                    var inputFromFile = new AddressFromFile
                    {
                        City = structuredInput.City,
                        Country = structuredInput.Country?.Name,
                        StreetAndHouseNumber = structuredInput.StreetAndHouseNumber,
                        District = structuredInput.District,
                        PostalCode = structuredInput.PostalCode
                    };

                    await CsvFileService.insertIntoAddressesFile(_addressesFilePath, inputFromFile, selectedIndex);

                    var inputs = await CsvFileService.getCheckAddressInputsFromCsvFile(_addressesFilePath, _oneLineInputFormat);

                    setAddresses(inputs, _addressesFilePath);
                }
            });

            foreach (var button in _buttonsToControlAddressesFromFile)
            {
                button.Enabled = true;
            }
        }

        private async void deleteAddressButton_Click(object sender, EventArgs e)
        {
            foreach (var button in _buttonsToControlAddressesFromFile)
            {
                button.Enabled = false;
            }

            await errorHandlingAndCursorLoading(async () =>
            {
                if (addressesFromFileListBox.SelectedItem != null)
                {
                    await CsvFileService.removeFromAddressesFile(_addressesFilePath, addressesFromFileListBox.SelectedIndex);

                    var inputs = await CsvFileService.getCheckAddressInputsFromCsvFile(_addressesFilePath, _oneLineInputFormat);

                    setAddresses(inputs, _addressesFilePath);
                }
            });

            foreach (var button in _buttonsToControlAddressesFromFile)
            {
                button.Enabled = true;
            }
        }

        private async void saveAsAddressesButton_Click(object sender, EventArgs e)
        {
            await errorHandlingAndCursorLoading(async () =>
            {
                var result = saveAsAddressesFileDialog.ShowDialog();

                if (result != DialogResult.OK)
                {
                    return;
                }

                var text = await File.ReadAllTextAsync(_addressesFilePath);
                var stream = saveAsAddressesFileDialog.OpenFile();

                using (var streamWriter = new StreamWriter(stream))
                {
                    await streamWriter.WriteAsync(text);
                }
            });
        }

        private async void loadAddressesButton_Click(object sender, EventArgs e)
        {
            await errorHandlingAndCursorLoading(async () =>
            {
                var result = openAddressesFileDialog.ShowDialog();

                if (result != DialogResult.OK)
                {
                    return;
                }

                var addressesFilePath = openAddressesFileDialog.FileName;
                var inputs = await CsvFileService.getCheckAddressInputsFromCsvFile(addressesFilePath, _oneLineInputFormat);

                setAddresses(inputs, addressesFilePath);
            });
        }

        private void CheckAddressForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.FormSize = Size;
            Settings.Default.FormLocation = Location;
            Settings.Default.Save();
        }

        private async void CheckAddressForm_Load(object sender, EventArgs e)
        {
            Size = Settings.Default.FormSize;
            Location = Settings.Default.FormLocation;

            countryTextBox.AutoCompleteSource = AutoCompleteSource.CustomSource;

            var countriesNames = Country.List.Select(c => c.Name);
            var countriesISO3 = Country.List.Select(c => c.ThreeLetterCode);
            var countriesISO2 = Country.List.Select(c => c.TwoLetterCode);

            countriesNames = Enumerable.Concat(countriesNames, countriesISO3);
            countriesNames = Enumerable.Concat(countriesNames, countriesISO2);

            countryTextBox.AutoCompleteCustomSource.AddRange(countriesNames.ToArray());

            apiTabControl.TabPages.Remove(googleMapsTabPage);
            apiTabControl.TabPages.Remove(loqateTabPage);
            apiTabControl.TabPages.Remove(smartyTabPage);
            apiTabControl.TabPages.Remove(hereTabPage);

            showApi(_conf);

            var inputs = await CsvFileService.getCheckAddressInputsFromCsvFile(_addressesFilePath, _oneLineInputFormat);

            setAddresses(inputs, _addressesFilePath);
        }
    }
}
