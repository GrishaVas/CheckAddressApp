using CheckAddressApp.Models;
using CheckAddressApp.Services;
using Google.Maps.AddressValidation.V1;
using Microsoft.Extensions.Configuration;

namespace CheckAddressApp
{
    public partial class CheckAddressForm : Form
    {
        private GoogleService _googleService;
        private LoqateService _loqateService;
        private SmartyService _smartyService;
        private HereService _hereService;
        private IConfiguration _conf;
        private Models.Loqate.ValidateAddressResponse _loqateValidateResponse;
        private Models.Smarty.ValidateAddressResponse _smartyValidateResponse;
        private Models.Here.ValidateAddressResponse _hereValidateResponse;
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

        public CheckAddressForm(IConfiguration conf)
        {
            _smartyService = new SmartyService(conf);
            _googleService = new GoogleService(conf);
            _loqateService = new LoqateService(conf);
            _hereService = new HereService(conf);
            _conf = conf;

            InitializeComponent();
        }
        ~CheckAddressForm()
        {
            //_loqateAddressApiService.Dispose();
        }

        private int checkedApiChoicesCount()
        {
            var count = 0;

            if (googleMapsCheckBox.Checked)
            {
                count++;
            }
            if (hereCheckBox.Checked)
            {
                count++;
            }
            if (loqateCheckBox.Checked)
            {
                count++;
            }
            if (smartyCheckBox.Checked)
            {
                count++;
            }

            return count;
        }

        private void clearGoogleResponse()
        {
            googleResponseRegionCodeTextBox.Text = "";
            googleResponseLocalityTextBox.Text = "";
            googleResponseAdministrativeAreaTextBox.Text = "";
            googleResponseLanguageCodeTextBox.Text = "";
            googleResponsePostalCodeTextBox.Text = "";
            googleResponseSortingCodeTextBox.Text = "";
            googleResponseSublocalityTextBox.Text = "";
            googleResponseFormattedAddressTextBox.Text = "";
            googleResponseOutputTextBox.Text = "";
        }

        private void clearLoqateResponse()
        {
            _loqateValidateResponse = null;
            loqateResponseDataGridView.Rows.Clear();
            loqateResponseListBox.Items.Clear();
            loqateResponseVerificationLavelTextBox.Text = "";
        }

        private void clearSmartyResponse()
        {
            _smartyValidateResponse = null;
            smartyResponseAnalisisDataGridView.Rows.Clear();
            smartyResponseComponetsDataGridView.Rows.Clear();
            smartyResponseListBox.Items.Clear();
        }

        private void clearHereResponse()
        {
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
            var responses = await _loqateService.ValidateAddress(input);

            setLoqateOutput(responses);
        }

        private async Task loqateValidation(string input, string countryCode)
        {
            var responses = await _loqateService.ValidateAddress(input, countryCode);

            setLoqateOutput(responses);
        }

        private void setLoqateOutput(List<Models.Loqate.ValidateAddressResponse> responses)
        {
            clearLoqateResponse();

            var reponse = responses.FirstOrDefault();

            _loqateValidateResponse = reponse;

            var addresses = reponse.Matches.Select(m => m.Address).Where(a => !string.IsNullOrEmpty(a)).ToArray();

            if (addresses != null || addresses.Length > 0)
            {
                loqateResponseListBox.Items.AddRange(addresses);
            }
        }

        private async Task googleValidation(StructuredInput input)
        {
            var response = await _googleService.ValidateAddress(input);

            setGoogleOutput(response);
        }

        private async Task googleValidation(string input, string countryCode)
        {
            var response = await _googleService.ValidateAddress(input, countryCode);

            setGoogleOutput(response);
        }

        private void setGoogleOutput(ValidateAddressResponse response)
        {
            clearGoogleResponse();

            var responseAddress = response.Result.Address;

            googleResponseFormattedAddressTextBox.Text = responseAddress.FormattedAddress;

            var generalVerdict = _googleService.GetVerdictString(response.Result.Verdict).Select(x => $"    {x}\n");
            var componetsConfirmation = _googleService.GetComponentsComfirmationString(responseAddress.AddressComponents).Select(x => $"    {x}\n");

            googleResponseOutputTextBox.Text = $"generalVerdict:\n{string.Join("", generalVerdict)}\ncomponentsComfirmation:\n{string.Join("", componetsConfirmation)}";

            var responsePostalAddress = responseAddress.PostalAddress;

            googleResponseRegionCodeTextBox.Text = responsePostalAddress.RegionCode;
            googleResponseLocalityTextBox.Text = responsePostalAddress.Locality;
            googleResponseAdministrativeAreaTextBox.Text = responsePostalAddress.AdministrativeArea;
            googleResponseLanguageCodeTextBox.Text = responsePostalAddress.LanguageCode;
            googleResponsePostalCodeTextBox.Text = responsePostalAddress.PostalCode;
            googleResponseSortingCodeTextBox.Text = responsePostalAddress.SortingCode;
            googleResponseSublocalityTextBox.Text = responsePostalAddress.Sublocality;

            var street = responseAddress.AddressComponents.FirstOrDefault(c => c.ComponentType == "route")?.ComponentName?.Text;
            var houseNumber = responseAddress.AddressComponents.FirstOrDefault(c => c.ComponentType == "street_number")?.ComponentName?.Text;

            googleResponseStreetTextBox.Text = $"{street} {houseNumber}";
        }

        private void smartyValidation(string input)
        {
            clearSmartyResponse();

            var countryCodes = getCoutryCodeFromInput();
            var countryCode = countryCodes.ISO2;

            if (string.IsNullOrEmpty(countryCode))
            {
                smartyCountryCodeErrorProvider.SetError(countryTextBox, "The Country Code required for smarty API.");

                return;
            }

            if (countryCode == "US")
            {
                var lookup = _smartyService.ValidateUSAddress(input);

                setSmartyUSOutput(lookup);
            }
            else
            {
                var lookup = _smartyService.ValidateInternationalAddress(input, countryCode);

                setSmartyInternationalOutput(lookup);
            }
        }

        private void smartyValidation(StructuredInput input)
        {
            clearSmartyResponse();

            if (string.IsNullOrEmpty(input.CountryCode2))
            {
                smartyCountryCodeErrorProvider.SetError(countryTextBox, "The Country Code required for smarty API.");

                return;
            }

            if (input.CountryCode2 == "US")
            {
                var lookup = _smartyService.ValidateUSAddress(input);

                setSmartyUSOutput(lookup);
            }
            else
            {
                var lookup = _smartyService.ValidateInternationalAddress(input);

                setSmartyInternationalOutput(lookup);
            }
        }

        private void setSmartyUSOutput(SmartyStreets.USStreetApi.Lookup lookup)
        {
            var results = lookup.Result.Select(r => $"{r.DeliveryLine1} {r.DeliveryLine2} {r.LastLine}").ToArray();

            smartyResponseListBox.Items.AddRange(results);
            _smartyValidateResponse = new Models.Smarty.ValidateAddressResponse
            {
                CoutryCode = "US",
                USLookup = lookup
            };
        }

        private void setSmartyInternationalOutput(SmartyStreets.InternationalStreetApi.Lookup lookup)
        {
            var results = lookup.Result.Select(r => $"{r.Address1} {r.Address2} {r.Address3}").ToArray();

            smartyResponseListBox.Items.AddRange(results);
            _smartyValidateResponse = new Models.Smarty.ValidateAddressResponse
            {
                CoutryCode = lookup.Country,
                InternationalLookup = lookup
            };
        }

        private async Task hereValidation(string input, string countryCode)
        {
            var validateAddresResponse = await _hereService.ValidateAddress(input, countryCode);

            setHereOutput(validateAddresResponse);
        }

        private async Task hereValidation(StructuredInput input)
        {
            var validateAddresResponse = await _hereService.ValidateAddress(input);

            setHereOutput(validateAddresResponse);
        }

        private void setHereOutput(Models.Here.ValidateAddressResponse response)
        {
            clearHereResponse();

            var matches = response.Items.Select(i => i.Title);

            _hereValidateResponse = response;
            hereResponseListBox.Items.AddRange(matches.ToArray());
        }

        private async Task loqateAutocomplete(string input, string countryCode)
        {
            clearLoqateResponse();

            var autocompleteAddressResponses = await _loqateService.AutocompleteAddress(input, countryCode);
            var suggestions = autocompleteAddressResponses.Items.Select(i => $"{i.Text} {i.Description}");

            if (suggestions != null && suggestions.Count() > 0)
            {
                loqateResponseListBox.Items.AddRange(suggestions.ToArray());
            }
        }

        private async Task googleAutocomplete(string input, string countryCode)
        {
            clearGoogleResponse();

            var autocompleteAddressResponse = await _googleService.AutocompleteAddress(input, countryCode);
            var suggestions = autocompleteAddressResponse.Suggestions?.Select(s => $"\"{s.PlacePrediction.Text.Text}\"\n");

            if (suggestions != null && suggestions.Count() > 0)
            {
                googleResponseOutputTextBox.Text = string.Join("", suggestions);
            }
        }

        private void smartyAutocomplete(string input)
        {
            clearSmartyResponse();

            var countryCodes = getCoutryCodeFromInput();
            var countryCode = countryCodes.ISO2;

            if (string.IsNullOrEmpty(countryCode))
            {
                smartyCountryCodeErrorProvider.SetError(countryTextBox, "The Country Code required for smarty API.");

                return;
            }


            var suggestions = _smartyService.AutocompleteAddress(input, countryCode);

            smartyResponseListBox.Items.AddRange(suggestions.ToArray());
        }

        private async Task hereAutocomplete(string input, string countryCode)
        {
            clearHereResponse();

            var hereAutocompleteResponse = await _hereService.AutocompleteAddress(input, countryCode);
            var suggestions = hereAutocompleteResponse.Items?.Select(i => i.Title);

            if (suggestions != null)
            {
                hereResponseListBox.Items.AddRange(suggestions.ToArray());
            }
        }

        private async Task hereAutosuggest(string input, string countryCode)
        {
            clearHereResponse();

            var hereValidateResponse = await _hereService.ValidateAddress(input, countryCode);
            var item = hereValidateResponse.Items.FirstOrDefault();

            if (item == null)
            {
                return;
            }

            var lat = item.Position.Lat.ToString();
            var lng = item.Position.Lng.ToString();
            var hereAutosuggestResponse = await _hereService.AutosuggestAddress(input, countryCode, lat, lng);
            var suggestions = hereAutosuggestResponse.Items.Select(i => i.Title);

            hereResponseListBox.Items.AddRange(suggestions.ToArray());
        }

        private IEnumerable<(string Field, string Value)> getMatchsRows(object match)
        {
            if (match == null)
            {
                return [];
            }

            var rows = match.GetType().GetProperties().Where(p => p.PropertyType.IsValueType || p.PropertyType == typeof(string)).Select(p =>
                    (
                        Field: p.Name,
                        Value: p.GetValue(match)?.ToString()
                    )).Where(r => !string.IsNullOrEmpty(r.Value));

            return rows;
        }

        private async Task autocomplete(string input)
        {
            fillRequestAddress(input);

            var countryCodes = getCoutryCodeFromInput();
            var countryCode = countryCodes.ISO2;

            if (googleMapsCheckBox.Checked)
            {
                await googleAutocomplete(input, countryCode);
            }

            if (loqateCheckBox.Checked)
            {
                await loqateAutocomplete(input, countryCode);
            }

            if (smartyCheckBox.Checked)
            {
                smartyAutocomplete(input);
            }

            if (hereCheckBox.Checked)
            {
                await hereAutocomplete(input, countryCode);
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
                await hereAutosuggest(input, countryCodes.ISO3);
            }
            else
            {
                inputErrorProvider.SetError(autocompleteAutosuggestSplitButton, "Api does not support autosuggest");
            }
        }

        private async Task validation(StructuredInput input)
        {
            if (googleMapsCheckBox.Checked)
            {
                await googleValidation(input);
            }

            if (loqateCheckBox.Checked)
            {
                await loqateValidation(input);
            }

            if (smartyCheckBox.Checked)
            {
                smartyValidation(input);
            }

            if (hereCheckBox.Checked)
            {
                await hereValidation(input);
            }
        }

        private async Task validation(string input)
        {
            var countryCodes = getCoutryCodeFromInput();

            if (googleMapsCheckBox.Checked)
            {
                await googleValidation(input, countryCodes.ISO2);
            }

            if (loqateCheckBox.Checked)
            {
                await loqateValidation(input, countryCodes.ISO2);
            }

            if (smartyCheckBox.Checked)
            {
                smartyValidation(input);
            }

            if (hereCheckBox.Checked)
            {
                await hereValidation(input, countryCodes.ISO3);
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

        private void clearChoiceApi()
        {
            googleMapsCheckBox.Checked = false;
            loqateCheckBox.Checked = false;
            smartyCheckBox.Checked = false;
            hereCheckBox.Checked = false;

            apiChoiceErrorProvider.SetError(apiGroupBox, "");
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

        private async void checkButton_Click(object sender, EventArgs e)
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

        private async void autocompleteAutosuggestSplitButton_Click(object sender, EventArgs e)
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

        private void loqateResponseListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_loqateValidateResponse != null)
            {
                var item = loqateResponseListBox.SelectedItem;
                var match = _loqateValidateResponse.Matches.FirstOrDefault(m => m.Address == item);
                var rows = getMatchsRows(match);

                loqateResponseDataGridView.Rows.Clear();

                foreach (var row in rows)
                {
                    loqateResponseDataGridView.Rows.Add(row.Field, row.Value);
                }

                loqateResponseVerificationLavelTextBox.Text = _loqateService.GetMatchVerificationLavel(match.AQI);
            }
        }

        private void smartyResponseListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_smartyValidateResponse != null)
            {
                var item = smartyResponseListBox.SelectedItem;
                IEnumerable<(string Field, string Value)> componetsRows;
                IEnumerable<(string Field, string Value)> analysisRows;

                if (_smartyValidateResponse.CoutryCode == "US")
                {
                    var match = _smartyValidateResponse.USLookup.Result
                        .FirstOrDefault(c => $"{c.DeliveryLine1} {c.DeliveryLine2} {c.LastLine}" == item.ToString());
                    var componetsMatch = match.Components;
                    var analysisMatch = match.Analysis;

                    analysisRows = getMatchsRows(analysisMatch);
                    componetsRows = getMatchsRows(componetsMatch);
                }
                else
                {
                    var match = _smartyValidateResponse.InternationalLookup.Result
                        .FirstOrDefault(c => $"{c.Address1} {c.Address2} {c.Address3}" == item.ToString());
                    var componetsMatch = match.Components;
                    var analysisMatch = match.Analysis;

                    analysisRows = getMatchsRows(analysisMatch);
                    componetsRows = getMatchsRows(componetsMatch);
                }

                smartyResponseComponetsDataGridView.Rows.Clear();
                smartyResponseAnalisisDataGridView.Rows.Clear();

                foreach (var row in componetsRows)
                {
                    smartyResponseComponetsDataGridView.Rows.Add(row.Field, row.Value);
                }

                foreach (var row in analysisRows)
                {
                    smartyResponseAnalisisDataGridView.Rows.Add(row.Field, row.Value);
                }
            }
        }

        private void hereResponseListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_hereValidateResponse != null)
            {
                var item = hereResponseListBox.SelectedItem;
                var match = _hereValidateResponse.Items.FirstOrDefault(m => m.Title == item);
                var address = match.Address;
                var rows = getMatchsRows(address);

                hereResponseDataGridView.Rows.Clear();

                foreach (var row in rows)
                {
                    hereResponseDataGridView.Rows.Add(row.Field, row.Value);
                }
            }
        }

        private void regionCodeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            smartyCountryCodeErrorProvider.SetError(countryTextBox, "");
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
            inputErrorProvider.SetError(autocompleteAutosuggestSplitButton, "");

            var count = checkedApiChoicesCount();
            if (count == 0)
            {
                googleMapsCheckBox.Checked = true;
                apiChoiceErrorProvider.SetError(apiGroupBox, "Atleast one api need to be checked");

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
                apiChoiceErrorProvider.SetError(apiGroupBox, "Atleast one api need to be checked");

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
                apiChoiceErrorProvider.SetError(apiGroupBox, "Atleast one api need to be checked");

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
            clearChoiceApi();
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

        private void hereCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            apiChoiceErrorProvider.SetError(apiGroupBox, "");
            inputErrorProvider.SetError(autocompleteAutosuggestSplitButton, "");

            var count = checkedApiChoicesCount();
            if (count == 0)
            {
                hereCheckBox.Checked = true;
                apiChoiceErrorProvider.SetError(apiGroupBox, "Atleast one api need to be checked");

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

        private void freeInputTextBox_TextChanged(object sender, EventArgs e)
        {
            inputErrorProvider.SetError(inputsChoiceTabControl, "");
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
        }
    }
}
