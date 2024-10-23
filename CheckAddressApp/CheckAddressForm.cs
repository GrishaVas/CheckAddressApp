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
        private IConfiguration _conf;
        private Models.Loqate.ValidateAddressResponse _loqateValidateResponse;
        private Models.Smarty.ValidateAddressResponse _smartyValidateResponse;
        private SmartyAddressApiService _smartyAddressApiService;
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

        private void fillRequestAddress()
        {
            var requestAddress = (regionCodeComboBox.Text != "" ? $"Region code: {regionCodeComboBox.Text} | " : "") +
                $"Address: {addressTextBox.Text}" +
                (localityTextBox.Text != "" ? $" | Locality: {localityTextBox.Text}" : "") +
                (sublocalityTextBox.Text != "" ? $" | Sublocality: {sublocalityTextBox.Text}" : "") +
                (administrativeAreaTextBox.Text != "" ? $" | Administrative area: {administrativeAreaTextBox.Text}" : "") +
                (postalCodeTextBox.Text != "" ? $" | Postal code: {postalCodeTextBox.Text}" : "");

            requestAddressTextBox.Text = requestAddress;
        }

        private string getAutocompliteInput()
        {
            var input = (regionCodeComboBox.Text != "" ? $"{regionCodeComboBox.Text} " : "") +
                (addressTextBox.Text != "" ? $"{addressTextBox.Text} " : "") +
                (localityTextBox.Text != "" ? $"{localityTextBox.Text} " : "") +
                (sublocalityTextBox.Text != "" ? $"{sublocalityTextBox.Text} " : "") +
                (administrativeAreaTextBox.Text != "" ? $"{administrativeAreaTextBox.Text} " : "") +
                (postalCodeTextBox.Text != "" ? $"{postalCodeTextBox.Text} " : "");

            return input;
        }
        private async Task loqateValidation()
        {
            clearLoqateResponse();

            var request = new Models.Loqate.ValidateAddressRequest
            {
                Key = _conf["Loqate:ApiKey"],
                Addresses = new List<Models.Loqate.ValidateAddressRequestAddress>
                {
                    new Models.Loqate.ValidateAddressRequestAddress
                    {
                        Address = addressTextBox.Text,
                        Locality = localityTextBox.Text,
                        DependentLocality = sublocalityTextBox.Text,
                        PostalCode = postalCodeTextBox.Text,
                        Country = regionCodeComboBox.Text,
                        AdministrativeArea = administrativeAreaTextBox.Text
                    }
                }
            };

            var responses = await _loqateAddressApiService.ValidateAddress(request);
            var reponse = responses.FirstOrDefault();

            _loqateValidateResponse = reponse;

            var addresses = reponse.Matches.Select(m => m.Address ?? "None").ToArray();

            if (addresses != null || addresses.Length > 0)
            {
                loqateResponseListBox.Items.AddRange(addresses);
            }
        }

        private async Task googleValidation()
        {
            clearGoogleResponse();

            if (string.IsNullOrEmpty(addressTextBox.Text))
            {
                addressFieldErrorProvider.SetError(addressTextBox, "The Address field required for google maps API.");
                return;
            }

            var address = new PostalAddress()
            {
                RegionCode = regionCodeComboBox.Text,
                Locality = localityTextBox.Text,
                AdministrativeArea = administrativeAreaTextBox.Text,
                PostalCode = postalCodeTextBox.Text,
                Sublocality = sublocalityTextBox.Text,
            };

            address.AddressLines.Add(addressTextBox.Text);

            var addressValidationRequest = new ValidateAddressRequest
            {
                Address = address,
                PreviousResponseId = "",
                EnableUspsCass = false,
                SessionToken = "",
            };

            var response = await _googleAddressApiService.ValidateAddress(addressValidationRequest);

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

        private void smartyValidation()
        {
            clearSmartyResponse();

            if (string.IsNullOrEmpty(addressTextBox.Text))
            {
                addressFieldErrorProvider.SetError(addressTextBox, "The Address field required for smarty API.");
                return;
            }

            if (string.IsNullOrEmpty(regionCodeComboBox.Text))
            {
                smartyCountryCodeErrorProvider.SetError(regionCodeComboBox, "The Country Code required for smarty API.");

                return;
            }

            if (regionCodeComboBox.Text == "US")
            {
                var lookup = new SmartyStreets.USStreetApi.Lookup(addressTextBox.Text)
                {
                    State = administrativeAreaTextBox.Text,
                    ZipCode = postalCodeTextBox.Text,
                    Street2 = $"{localityTextBox.Text} {sublocalityTextBox.Text}"
                };

                _smartyAddressApiService.ValidateUSAddress(lookup);

                var results = lookup.Result.Select(r => $"{r.DeliveryLine1} {r.DeliveryLine2} {r.LastLine}").ToArray();

                smartyResponseListBox.Items.AddRange(results);
                _smartyValidateResponse = new Models.Smarty.ValidateAddressResponse
                {
                    CoutryCode = regionCodeComboBox.Text,
                    USLookup = lookup
                };
            }
            else
            {
                var lookup = new SmartyStreets.InternationalStreetApi.Lookup(addressTextBox.Text, regionCodeComboBox.Text)
                {
                    AdministrativeArea = administrativeAreaTextBox.Text,
                    PostalCode = postalCodeTextBox.Text,
                    Locality = localityTextBox.Text,
                    Address4 = sublocalityTextBox.Text
                };

                _smartyAddressApiService.ValidateInternationalAddress(lookup);

                var results = lookup.Result.Select(r => $"{r.Address1} {r.Address2} {r.Address3}").ToArray();

                smartyResponseListBox.Items.AddRange(results);
                _smartyValidateResponse = new Models.Smarty.ValidateAddressResponse
                {
                    CoutryCode = regionCodeComboBox.Text,
                    InternationalLookup = lookup
                };
            }
        }

        private async Task loqateAutocomplete()
        {
            clearLoqateResponse();

            var input = getAutocompliteInput();

            if (input == "")
            {
                return;
            }

            var autocompleteAddressRequest = new Models.Loqate.AutocompleteAddressRequest
            {
                Lqtkey = _conf["Loqate:ApiKey"],
                Query = addressTextBox.Text,
                Country = regionCodeComboBox.Text,
                Filters = new Models.Loqate.AutocompleteAddressRequestFilters
                {
                    AdministrativeArea = administrativeAreaTextBox.Text,
                    Locality = localityTextBox.Text,
                    PostalCode = postalCodeTextBox.Text
                }
            };
            var autocompleteAddressResponses = await _loqateAddressApiService.AutocompleteAddress(autocompleteAddressRequest);
            var suggestions = autocompleteAddressResponses.Output;

            if (suggestions != null && suggestions.Count() > 0)
            {
                loqateResponseListBox.Items.AddRange(suggestions.ToArray());
            }
        }

        private async Task googleAutocomplete()
        {
            clearGoogleResponse();

            var input = getAutocompliteInput();

            if (input == "")
            {
                return;
            }

            var autocompleteAddressRequest = new AutocompleteAddressRequest(input);
            var autocompleteAddressResponse = await _googleAddressApiService.AutocompleteAddress(autocompleteAddressRequest);
            var suggestions = autocompleteAddressResponse.Suggestions?.Select(s => $"\"{s.PlacePrediction.Text.Text}\"\n");

            if (suggestions != null && suggestions.Count() > 0)
            {
                googleResponseOutputTextBox.Text = string.Join("", suggestions);
            }
        }

        private void smartyAutocomplete()
        {
            clearSmartyResponse();

            if (string.IsNullOrEmpty(addressTextBox.Text))
            {
                addressFieldErrorProvider.SetError(addressTextBox, "The Address field required for smarty API.");
                return;
            }

            if (string.IsNullOrEmpty(regionCodeComboBox.Text))
            {
                smartyCountryCodeErrorProvider.SetError(regionCodeComboBox, "The Country Code required for smarty API.");

                return;
            }

            var address = addressTextBox.Text;
            var countryCode = regionCodeComboBox.Text;
            var suggestions = _smartyAddressApiService.AutocompleteAddress(address, countryCode);

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

        private async void checkButton_Click(object sender, EventArgs e)
        {
            if (!googleMapsCheckBox.Checked && !loqateCheckBox.Checked && !smartyCheckBox.Checked)
            {
                apiChoiceErorProvider.SetError(apiGroupBox, "Select at least one api");
            }

            fillRequestAddress();

            if (googleMapsCheckBox.Checked)
            {
                await googleValidation();
            }

            if (loqateCheckBox.Checked)
            {
                await loqateValidation();
            }

            if (smartyCheckBox.Checked)
            {
                smartyValidation();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            addressTextBox.Text = "";
            regionCodeComboBox.Text = "";
            localityTextBox.Text = "";
            administrativeAreaTextBox.Text = "";
            postalCodeTextBox.Text = "";
            sublocalityTextBox.Text = "";
            requestAddressTextBox.Text = "";
            regionCodeComboBox.SelectedItem = null;
            googleMapsCheckBox.Checked = false;
            loqateCheckBox.Checked = false;
            smartyCheckBox.Checked = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clearGoogleResponse();
        }

        private async void autocompleteButton_Click(object sender, EventArgs e)
        {
            fillRequestAddress();

            if (!googleMapsCheckBox.Checked && !loqateCheckBox.Checked && !smartyCheckBox.Checked)
            {
                apiChoiceErorProvider.SetError(apiGroupBox, "Select at least one api");
            }

            if (googleMapsCheckBox.Checked)
            {
                await googleAutocomplete();
            }

            if (loqateCheckBox.Checked)
            {
                await loqateAutocomplete();
            }

            if (smartyCheckBox.Checked)
            {
                smartyAutocomplete();
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

        private void button3_Click(object sender, EventArgs e)
        {
            clearLoqateResponse();
        }

        private void addressTextBox_TextChanged(object sender, EventArgs e)
        {
            if (addressFieldErrorProvider.HasErrors)
            {
                addressFieldErrorProvider.Clear();
            }
        }

        private void googleMapsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            apiChoiceErorProvider.Clear();
        }

        private void loqateCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            apiChoiceErorProvider.Clear();
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
            smartyCountryCodeErrorProvider.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            clearSmartyResponse();
        }
    }
}
