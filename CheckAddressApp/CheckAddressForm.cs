using System.Reflection;
using System.Timers;
using CheckAddressApp.Models;
using CheckAddressApp.Services;
using ISO3166;
using Microsoft.Extensions.Configuration;
using qAcProviderTest;
using qAcProviderTest.Properties;
using qAcProviderTest.Services;

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
        private IEnumerable<CheckAddressData> _loqateResponse;
        private IEnumerable<CheckAddressData> _smartyResponse;
        private IEnumerable<CheckAddressData> _hereResponse;
        private IEnumerable<CheckAddressData> _googleResponse;
        private IEnumerable<CheckAddressInput> _addressesFromFile;
        private IConfiguration _conf;
        private IEnumerable<Button> _buttonsToControlAddressesFromFile;
        private string _addressesFilePath = "addresses.csv";
        private string _oneLineInputFormat;
        private System.Timers.Timer _addressesFromFileSearchTimer;

        public CheckAddressForm(IConfiguration conf)
        {
            _smartyService = new SmartyService(conf);
            _googleService = new GoogleService(conf);
            _loqateService = new LoqateService(conf);
            _hereService = new HereService(conf);
            _errorForm = new ErrorForm();
            _userNotificationForm = new UserNotificationForm(this);
            _conf = conf;

            _addressesFromFileSearchTimer = new System.Timers.Timer
            {
                AutoReset = false,
                Interval = 1000,
                SynchronizingObject = this
            };

            _addressesFromFileSearchTimer.Elapsed += new System.Timers.ElapsedEventHandler(doSearchAddressesFromFile);

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
        }

        private void writeDetailsOfSelectedAddress(int selectedIndex, IEnumerable<CheckAddressData> serviceResponse, DataGridView outputGrid)
        {
            CheckAddressData checkAddressData = null;

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

        private async Task<IEnumerable<CheckAddressInput>> getAddressesFromCsv(string path)
        {
            IEnumerable<CheckAddressInput> checkAddressInputs;

            try
            {
                checkAddressInputs = await CsvFileService.getCheckAddressInputsFromCsvFile(path, _oneLineInputFormat);
            }
            catch (Exception ex)
            {
                showNotificatoin(ex.Message);
                checkAddressInputs = [];
            }

            return checkAddressInputs;
        }

        private void setAddresses(IEnumerable<CheckAddressInput> inputs, string fileName)
        {
            _addressesFromFile = inputs;
            _addressesFilePath = fileName;

            var addresses = inputs.Select(a => a.ToString()).ToArray();

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
            googleResponseTimeLabel.Text = "Response time: 0,0";
        }

        private void clearLoqateResponse()
        {
            _loqateResponse = null;
            loqateResponseDataGridView.Rows.Clear();
            loqateResponseListBox.Items.Clear();
            loqateResponseTimeLabel.Text = "Response time: 0,0";
        }

        private void clearSmartyResponse()
        {
            _smartyResponse = null;
            smartyResponseDataGridView.Rows.Clear();
            smartyResponseListBox.Items.Clear();
            smartyResponseTimeLabel.Text = "Response time: 0,0";
        }

        private void clearHereResponse()
        {
            hereResponseListBox.Items.Clear();
            hereResponseDataGridView.Rows.Clear();
            _hereResponse = null;
            hereResponseTimeLabel.Text = "Response time: 0,0";
        }



        private CheckAddressInput getCheckAddressInput()
        {
            var structuredInput = getStructuredInput();
            var countryName = countryTextBox.Text;
            Country country;

            if (!string.IsNullOrEmpty(countryName))
            {
                country = Country.List.FirstOrDefault(c => c.Name.ToLower() == countryName.ToLower() ||
                    c.ThreeLetterCode.ToLower() == countryName.ToLower() ||
                    c.TwoLetterCode.ToLower() == countryName.ToLower());
            }
            else
            {
                country = null;
            }

            if (country == null && !string.IsNullOrEmpty(countryName))
            {
                throw new Exception("Cannot find the country in the country list.");
            }

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

            _googleResponse = output.CheckAddressData;
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

            _loqateResponse = output.CheckAddressData;
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

            _smartyResponse = output.CheckAddressData;
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

            _hereResponse = output.CheckAddressData;
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

            requestAddressTextBox.Text = $"{checkAddressInput.FreeInput} {checkAddressInput.Country?.TwoLetterCode ?? ""}";

            if (string.IsNullOrEmpty(checkAddressInput.FreeInput))
            {
                showNotificatoin("Inputs cannot be null.");

                return;
            }

            if (!googleMapsCheckBox.Checked && !loqateCheckBox.Checked && !smartyCheckBox.Checked && !hereCheckBox.Checked)
            {
                showNotificatoin("At least one Check Provider should be checked!");
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
                if (checkAddressInput.Country == null)
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

            requestAddressTextBox.Text = $"{checkAddressInput.FreeInput} {checkAddressInput.Country?.TwoLetterCode ?? ""}";

            if (string.IsNullOrEmpty(checkAddressInput.FreeInput))
            {
                showNotificatoin("Inputs cannot be null.");

                return;
            }

            if (!googleMapsCheckBox.Checked && !loqateCheckBox.Checked && !smartyCheckBox.Checked && !hereCheckBox.Checked)
            {
                showNotificatoin("At least one Check Provider should be checked!");
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
                if (checkAddressInput.Country == null)
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

            requestAddressTextBox.Text = $"{checkAddressInput.FreeInput} {checkAddressInput.Country?.TwoLetterCode ?? ""}";

            if (string.IsNullOrEmpty(checkAddressInput.FreeInput))
            {
                showNotificatoin("Inputs cannot be null.");

                return;
            }

            if (!googleMapsCheckBox.Checked && !loqateCheckBox.Checked && !smartyCheckBox.Checked && !hereCheckBox.Checked)
            {
                showNotificatoin("At least one Check Provider should be checked!");
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

        private void loqateResponseListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = loqateResponseListBox.SelectedIndex;

            writeDetailsOfSelectedAddress(index, _loqateResponse, loqateResponseDataGridView);
        }

        private void smartyResponseListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = smartyResponseListBox.SelectedIndex;

            writeDetailsOfSelectedAddress(index, _smartyResponse, smartyResponseDataGridView);
        }

        private void hereResponseListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = hereResponseListBox.SelectedIndex;

            writeDetailsOfSelectedAddress(index, _hereResponse, hereResponseDataGridView);
        }

        private void googleResponseListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = googleResponseListBox.SelectedIndex;

            writeDetailsOfSelectedAddress(index, _googleResponse, googleResponseDataGridView);
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
                    countryTextBox.Text = inputFromFile?.Country?.Name ?? "";
                }
                else
                {
                    freeInputTextBox.Text = inputFromFile?.FreeInput;
                    countryTextBox.Text = inputFromFile?.Country?.Name ?? "";
                }
            }
        }

        private void addressesSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            if (_addressesFromFileSearchTimer.Enabled)
            {
                return;
            }
            else
            {
                _addressesFromFileSearchTimer.Start();
            }

        }

        private async void doSearchAddressesFromFile(object source, ElapsedEventArgs e)
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
            try
            {
                this.Cursor = Cursors.WaitCursor;

                await validation();
            }
            catch (Exception ex)
            {
                showNotificatoin(ex.Message);
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
                showNotificatoin(ex.Message);
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
                showNotificatoin(ex.Message);
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
                    Country = input.Country?.Name,
                    StreetAndHouseNumber = input.StructuredInput.StreetAndHouseNumber,
                    District = input.StructuredInput.District,
                    PostalCode = input.StructuredInput.PostalCode
                };

                await insertIntoAddressesFileAt(_addressesFilePath, inputFromFile, 0);
                var inputs = await getAddressesFromCsv(_addressesFilePath);
                setAddresses(inputs, _addressesFilePath);
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
                    Country = input.Country?.Name,
                    StreetAndHouseNumber = input.StructuredInput.StreetAndHouseNumber,
                    District = input.StructuredInput.District,
                    PostalCode = input.StructuredInput.PostalCode
                };

                await insertIntoAddressesFileAt(_addressesFilePath, inputFromFile, selectedIndex);
                var inputs = await getAddressesFromCsv(_addressesFilePath);
                setAddresses(inputs, _addressesFilePath);
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
                var inputs = await getAddressesFromCsv(_addressesFilePath);
                setAddresses(inputs, _addressesFilePath);
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
            this.Cursor = Cursors.WaitCursor;

            var result = openAddressesFileDialog.ShowDialog();

            if (result != DialogResult.OK)
            {
                //Exception
                return;
            }

            var addressesFilePath = openAddressesFileDialog.FileName;
            var inputs = await getAddressesFromCsv(addressesFilePath);
            setAddresses(inputs, addressesFilePath);

            this.Cursor = Cursors.Arrow;
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

            var inputs = await getAddressesFromCsv(_addressesFilePath);

            setAddresses(inputs, _addressesFilePath);
        }
    }
}
