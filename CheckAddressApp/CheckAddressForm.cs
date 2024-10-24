using CheckAddressApp.Models;
using CheckAddressApp.Models.Google;
using CheckAddressApp.Services;
using Google.Maps.AddressValidation.V1;
using Google.Type;
using Microsoft.Extensions.Configuration;

namespace CheckAddressApp
{
    public partial class CheckAddressForm : Form
    {
        private GoogleAddressApiService _googleAddressApiService;
        private LoqateAddressApiService _loqateAddressApiService;
        private SmartyAddressApiService _smartyAddressApiService;
        private IConfiguration _conf;
        private Models.Loqate.ValidateAddressResponse _loqateValidateResponse;
        private Models.Smarty.ValidateAddressResponse _smartyValidateResponse;

        public CheckAddressForm(IConfiguration conf)
        {
            _smartyAddressApiService = new SmartyAddressApiService(
                conf["Smarty:AuthId"],
                conf["Smarty:AuthToken"]);
            _googleAddressApiService = new GoogleAddressApiService(conf["Google:ApiKey"],
                conf["Google:ClientId"],
                conf["Google:ClientSecret"],
                conf["Google:RefreshToken"]);
            _loqateAddressApiService = new LoqateAddressApiService();
            _conf = conf;

            InitializeComponent();
        }
        ~CheckAddressForm()
        {
            _loqateAddressApiService.Dispose();
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

        private void fillRequestAddress(string input)
        {
            var requestAddress = input +
                (countryCodeComboBox.Text != "" ? $" | Country code: {countryCodeComboBox.Text}" : "");

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

        private async Task loqateValidation(Models.Loqate.ValidateAddressRequest request)
        {
            clearLoqateResponse();

            var responses = await _loqateAddressApiService.ValidateAddress(request);
            var reponse = responses.FirstOrDefault();

            _loqateValidateResponse = reponse;

            var addresses = reponse.Matches.Select(m => m.Address ?? "None").ToArray();

            if (addresses != null || addresses.Length > 0)
            {
                loqateResponseListBox.Items.AddRange(addresses);
            }
        }

        private Models.Loqate.ValidateAddressRequest getLoqateValidationAddress(StructuredInput input)
        {
            var request = new Models.Loqate.ValidateAddressRequest
            {
                Key = _conf["Loqate:ApiKey"],
                Addresses = new List<Models.Loqate.ValidateAddressRequestAddress>
                {
                    new Models.Loqate.ValidateAddressRequestAddress
                    {
                        Address = input.Input,
                        Locality = input.City,
                        DependentLocality = input.District,
                        PostalCode = input.PostalCode,
                        Country = input.CountryCode,
                        Address1 = input.StreetAndHouseNumber
                    }
                }
            };

            return request;
        }

        private Models.Loqate.ValidateAddressRequest getLoqateValidationAddress(string input)
        {
            var request = new Models.Loqate.ValidateAddressRequest
            {
                Key = _conf["Loqate:ApiKey"],
                Addresses = new List<Models.Loqate.ValidateAddressRequestAddress>
                {
                    new Models.Loqate.ValidateAddressRequestAddress
                    {
                        Address = input
                    }
                }
            };

            return request;
        }

        private async Task googleValidation(ValidateAddressRequest requst)
        {
            clearGoogleResponse();

            var response = await _googleAddressApiService.ValidateAddress(requst);
            var responseAddress = response.Result.Address;

            googleResponseFormattedAddressTextBox.Text = responseAddress.FormattedAddress;

            var generalVerdict = _googleAddressApiService.GetVerdictString(response.Result.Verdict).Select(x => $"    {x}\n");
            var componetsConfirmation = _googleAddressApiService.GetComponentsComfirmationString(responseAddress.AddressComponents).Select(x => $"    {x}\n");

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

        private ValidateAddressRequest getGoogleValidationRequest(StructuredInput structuredInput)
        {
            var address = new PostalAddress()
            {
                RegionCode = structuredInput.CountryCode,
                Locality = structuredInput.City,
                PostalCode = structuredInput.PostalCode,
                Sublocality = structuredInput.District
            };

            address.AddressLines.Add(structuredInput.Input);

            var addressValidationRequest = new ValidateAddressRequest
            {
                Address = address
            };

            return addressValidationRequest;
        }
        private ValidateAddressRequest getGoogleValidationRequest(string input)
        {
            var address = new PostalAddress()
            {
                RegionCode = countryCodeComboBox.Text
            };

            address.AddressLines.Add(input);

            var addressValidationRequest = new ValidateAddressRequest
            {
                Address = address
            };

            return addressValidationRequest;
        }

        private void smartyValidation(string input)
        {
            clearSmartyResponse();

            if (string.IsNullOrEmpty(countryCodeComboBox.Text))
            {
                smartyCountryCodeErrorProvider.SetError(countryCodeComboBox, "The Country Code required for smarty API.");

                return;
            }

            if (countryCodeComboBox.Text == "US")
            {
                var lookup = new SmartyStreets.USStreetApi.Lookup(input);

                smartyUSValidation(lookup);
            }
            else
            {
                var lookup = new SmartyStreets.InternationalStreetApi.Lookup(input, countryCodeComboBox.Text);

                smartyInternationalValidation(lookup);
            }
        }

        private void smartyValidation(StructuredInput input)
        {
            clearSmartyResponse();

            if (string.IsNullOrEmpty(input.CountryCode))
            {
                smartyCountryCodeErrorProvider.SetError(countryCodeComboBox, "The Country Code required for smarty API.");

                return;
            }

            if (input.CountryCode == "US")
            {
                var lookup = new SmartyStreets.USStreetApi.Lookup(input.Input)
                {
                    ZipCode = input.PostalCode,
                    City = input.City,
                    Urbanization = input.District,
                    Street2 = input.StreetAndHouseNumber
                };

                smartyUSValidation(lookup);
            }
            else
            {
                var lookup = new SmartyStreets.InternationalStreetApi.Lookup(input.Input, input.CountryCode)
                {
                    PostalCode = input.PostalCode,
                    Locality = input.City,
                    Address1 = input.StreetAndHouseNumber,
                    Address2 = input.District
                };

                smartyInternationalValidation(lookup);
            }
        }

        private void smartyUSValidation(SmartyStreets.USStreetApi.Lookup lookup)
        {
            _smartyAddressApiService.ValidateUSAddress(lookup);

            var results = lookup.Result.Select(r => $"{r.DeliveryLine1} {r.DeliveryLine2} {r.LastLine}").ToArray();

            smartyResponseListBox.Items.AddRange(results);
            _smartyValidateResponse = new Models.Smarty.ValidateAddressResponse
            {
                CoutryCode = "US",
                USLookup = lookup
            };
        }
        private void smartyInternationalValidation(SmartyStreets.InternationalStreetApi.Lookup lookup)
        {
            _smartyAddressApiService.ValidateInternationalAddress(lookup);

            var results = lookup.Result.Select(r => $"{r.Address1} {r.Address2} {r.Address3}").ToArray();

            smartyResponseListBox.Items.AddRange(results);
            _smartyValidateResponse = new Models.Smarty.ValidateAddressResponse
            {
                CoutryCode = lookup.Country,
                InternationalLookup = lookup
            };
        }

        private async Task loqateAutocomplete(string input)
        {
            clearLoqateResponse();

            var autocompleteAddressRequest = new Models.Loqate.AutocompleteAddressRequest
            {
                Key = _conf["Loqate:ApiKey"],
                Text = input,
                Origin = countryCodeComboBox.Text
            };
            var autocompleteAddressResponses = await _loqateAddressApiService.AutocompleteAddress(autocompleteAddressRequest);
            var suggestions = autocompleteAddressResponses.Items.Select(i => $"{i.Text} {i.Description}");

            if (suggestions != null && suggestions.Count() > 0)
            {
                loqateResponseListBox.Items.AddRange(suggestions.ToArray());
            }
        }

        private async Task googleAutocomplete(string input)
        {
            clearGoogleResponse();

            var autocompleteAddressRequest = new AutocompleteAddressRequest(input)
            {
                RegionCode = countryCodeComboBox.Text
            };
            var autocompleteAddressResponse = await _googleAddressApiService.AutocompleteAddress(autocompleteAddressRequest);
            var suggestions = autocompleteAddressResponse.Suggestions?.Select(s => $"\"{s.PlacePrediction.Text.Text}\"\n");

            if (suggestions != null && suggestions.Count() > 0)
            {
                googleResponseOutputTextBox.Text = string.Join("", suggestions);
            }
        }

        private void smartyAutocomplete(string input)
        {
            clearSmartyResponse();

            if (string.IsNullOrEmpty(countryCodeComboBox.Text))
            {
                smartyCountryCodeErrorProvider.SetError(countryCodeComboBox, "The Country Code required for smarty API.");

                return;
            }

            var countryCode = countryCodeComboBox.Text;
            var suggestions = _smartyAddressApiService.AutocompleteAddress(input, countryCode);

            smartyResponseListBox.Items.AddRange(suggestions.ToArray());
        }

        private IEnumerable<(string Field, string Value)> getMatchsRows(object match)
        {
            var rows = match.GetType().GetProperties().Where(p => p.PropertyType.IsValueType || p.PropertyType == typeof(string)).Select(p =>
                    (
                        Field: p.Name,
                        Value: p.GetValue(match)?.ToString()
                    )).Where(r => !string.IsNullOrEmpty(r.Value));

            return rows;
        }

        private void cleareErrorProvider(ErrorProvider errorProvider)
        {
            if (errorProvider.HasErrors)
            {
                errorProvider.Clear();
            }
        }

        private async Task autocomplete()
        {
            if (!googleMapsCheckBox.Checked && !loqateCheckBox.Checked && !smartyCheckBox.Checked)
            {
                apiChoiceErrorProvider.SetError(apiGroupBox, "Select at least one api.");

                return;
            }

            var input = getOneLineInput();

            if (string.IsNullOrEmpty(input))
            {
                inputErrorProvider.SetError(autocompleteAutosuggestSplitButton, "At least one input must be filled.");

                return;
            }

            fillRequestAddress(input);

            if (googleMapsCheckBox.Checked)
            {
                await googleAutocomplete(input);
            }

            if (loqateCheckBox.Checked)
            {
                await loqateAutocomplete(input);
            }

            if (smartyCheckBox.Checked)
            {
                smartyAutocomplete(input);
            }
        }

        private async Task autosuggest()
        {

        }

        private async Task validation(StructuredInput input)
        {
            if (googleMapsCheckBox.Checked)
            {
                var googleRequest = getGoogleValidationRequest(input);

                await googleValidation(googleRequest);
            }

            if (loqateCheckBox.Checked)
            {
                var loqateResponse = getLoqateValidationAddress(input);

                await loqateValidation(loqateResponse);
            }

            if (smartyCheckBox.Checked)
            {
                smartyValidation(input);
            }
        }
        private async Task validation(string input)
        {
            if (googleMapsCheckBox.Checked)
            {
                var googleRequest = getGoogleValidationRequest(input);

                await googleValidation(googleRequest);
            }

            if (loqateCheckBox.Checked)
            {
                var loqateResponse = getLoqateValidationAddress(input);

                await loqateValidation(loqateResponse);
            }

            if (smartyCheckBox.Checked)
            {
                smartyValidation(input);
            }
        }

        private void clearInputs()
        {
            streetAndHouseNumberTextBox.Text = "";
            countryCodeComboBox.Text = "";
            cityTextBox.Text = "";
            postalCodeTextBox.Text = "";
            districtTextBox.Text = "";
            requestAddressTextBox.Text = "";
            countryCodeComboBox.SelectedItem = null;
            freeInputTextBox.Text = "";

            cleareErrorProvider(inputErrorProvider);

            cleareErrorProvider(smartyCountryCodeErrorProvider);
        }

        private void clearChoiceApi()
        {
            googleMapsCheckBox.Checked = false;
            loqateCheckBox.Checked = false;
            smartyCheckBox.Checked = false;

            cleareErrorProvider(apiChoiceErrorProvider);
        }

        private StructuredInput getStructuredInput()
        {
            var input = new StructuredInput
            {
                StreetAndHouseNumber = streetAndHouseNumberTextBox.Text,
                City = cityTextBox.Text,
                District = districtTextBox.Text,
                PostalCode = postalCodeTextBox.Text,
                CountryCode = countryCodeComboBox.Text,
                Input = getOneLineInput()
            };

            return input;
        }

        private async void checkButton_Click(object sender, EventArgs e)
        {
            if (!googleMapsCheckBox.Checked && !loqateCheckBox.Checked && !smartyCheckBox.Checked)
            {
                apiChoiceErrorProvider.SetError(apiGroupBox, "Select at least one api");

                return;
            }

            var selectedInputType = inputsChoiceTabControl.SelectedTab.Text;
            StructuredInput structuredInput;

            if (selectedInputType == "Structured Input")
            {
                structuredInput = getStructuredInput();

                fillRequestAddress(structuredInput.Input);

                if (string.IsNullOrEmpty(structuredInput.Input))
                {
                    inputErrorProvider.SetError(autocompleteAutosuggestSplitButton, "At least one input must be filled.");

                    return;
                }

                await validation(structuredInput);
            }
            else
            {

                var input = freeInputTextBox.Text;

                fillRequestAddress(input);

                if (string.IsNullOrEmpty(input))
                {
                    inputErrorProvider.SetError(freeInputTextBox, "Address required.");

                    return;
                }

                await validation(input);
            }
        }

        private void loqateResponseListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_loqateValidateResponse != null && loqateResponseListBox.SelectedItem != "None")
            {
                var item = loqateResponseListBox.SelectedItem;
                var match = _loqateValidateResponse.Matches.FirstOrDefault(m => m.Address == item);
                var rows = match.GetType().GetProperties().Select(p => new
                {
                    Field = p.Name,
                    Value = p.GetValue(match)
                });

                foreach (var row in rows)
                {
                    loqateResponseDataGridView.Rows.Add(row.Field, row.Value);
                }

                loqateResponseVerificationLavelTextBox.Text = _loqateAddressApiService.GetMatchVerificationLavel(match.AQI);
            }
        }

        private void smartyResponseListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_smartyValidateResponse != null && loqateResponseListBox.SelectedItem != "None")
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

        private void regionCodeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            cleareErrorProvider(smartyCountryCodeErrorProvider);
        }

        private void addressTextBox_TextChanged(object sender, EventArgs e)
        {
            cleareErrorProvider(inputErrorProvider);
        }

        private void cityTextBox_TextChanged(object sender, EventArgs e)
        {
            cleareErrorProvider(inputErrorProvider);
        }

        private void districtTextBox_TextChanged(object sender, EventArgs e)
        {
            cleareErrorProvider(inputErrorProvider);
        }

        private void postalCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            cleareErrorProvider(inputErrorProvider);
        }

        private void googleMapsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            cleareErrorProvider(apiChoiceErrorProvider);
        }

        private void loqateCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            cleareErrorProvider(apiChoiceErrorProvider);
        }

        private void smartyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            cleareErrorProvider(apiChoiceErrorProvider);
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
        }

        private async void autocompleteAutosuggestSplitButton_Click(object sender, EventArgs e)
        {
            if (autocompleteAutosuggestSplitButton.Text == "Autocomplete")
            {
                await autocomplete();
            }
            else
            {
                await autosuggest();
            }
        }

        private void inputsChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearInputs();
        }
    }
}
