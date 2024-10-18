using CheckAddressApp.Models.Google;
using CheckAddressApp.Services;
using Google.Maps.AddressValidation.V1;
using Google.Type;
using Microsoft.Extensions.Configuration;

namespace CheckAddressApp
{
    public partial class CheckAddressForm : Form
    {
        private GoogleAddressApiService _googleAddressValidationApiService;
        private LoqateAddressApiService _loqateAddressApiService;
        private IConfiguration _conf;
        private Models.Loqate.ValidateAddressResponse _loqateValidateResponse;
        public CheckAddressForm(IConfiguration conf)
        {
            _googleAddressValidationApiService = new GoogleAddressApiService(conf["Google:ApiKey"],
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
            loqateResponseDataGridView.Rows.Clear();
            loqateResponseListBox.Items.Clear();
            loqateResponseVerificationLavelTextBox.Text = "";
        }

        private void fillRequestAddress()
        {
            var requestAddress = (regionCodeComboBox.Text != "" ? $"{regionCodeComboBox.Text} ," : "") +
                $"{addressTextBox.Text}" +
                (localityTextBox.Text != "" ? $", {localityTextBox.Text}" : "") +
                (sublocalityTextBox.Text != "" ? $", {sublocalityTextBox.Text}" : "") +
                (administrativeAreaTextBox.Text != "" ? $", {administrativeAreaTextBox.Text}" : "") +
                (postalCodeTextBox.Text != "" ? $", {postalCodeTextBox.Text}" : "");

            requestAddressTextBox.Text = requestAddress;
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

            var response = await _googleAddressValidationApiService.ValidateAddress(addressValidationRequest);

            var responseAddress = response.Result.Address;

            googleResponseFormattedAddressTextBox.Text = responseAddress.FormattedAddress;

            var generalVerdict = _googleAddressValidationApiService.GetVerdictString(response.Result.Verdict).Select(x => $"    {x}\n");
            var componetsConfirmation = _googleAddressValidationApiService.GetComponentsComfirmationString(responseAddress.AddressComponents).Select(x => $"    {x}\n");

            googleResponseOutputTextBox.Text = $"generalVerdict:\n{string.Join("", generalVerdict)}\ncomponentsComfirmation:\n{string.Join("", componetsConfirmation)}";

            var responsePostalAddress = responseAddress.PostalAddress;

            googleResponseRegionCodeTextBox.Text = responsePostalAddress.RegionCode;
            googleResponseLocalityTextBox.Text = responsePostalAddress.Locality;
            googleResponseAdministrativeAreaTextBox.Text = responsePostalAddress.AdministrativeArea;
            googleResponseLanguageCodeTextBox.Text = responsePostalAddress.LanguageCode;
            googleResponsePostalCodeTextBox.Text = responsePostalAddress.PostalCode;
            googleResponseSortingCodeTextBox.Text = responsePostalAddress.SortingCode;
            googleResponseSublocalityTextBox.Text = responsePostalAddress.Sublocality;
        }

        private async void checkButton_Click(object sender, EventArgs e)
        {
            fillRequestAddress();

            if (googleMapsCheckBox.Checked)
            {
                await googleValidation();
            }

            if (loqateCheckBox.Checked)
            {
                await loqateValidation();
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clearGoogleResponse();
        }

        private void addressTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(addressTextBox.Text))
            {
                checkButton.Enabled = true;
                autocompleteButton.Enabled = true;
            }
            else
            {
                checkButton.Enabled = false;
                autocompleteButton.Enabled = false;
            }
        }

        private async void autocompleteButton_Click(object sender, EventArgs e)
        {
            fillRequestAddress();
            clearGoogleResponse();
            apiTabControl.SelectTab(0);

            var autocompleteAddressRequest = new AutocompleteAddressRequest(addressTextBox.Text);
            var autocompleteAddressResponse = await _googleAddressValidationApiService.AutocompleteAddress(autocompleteAddressRequest);
            var suggestions = autocompleteAddressResponse.Suggestions?.Select(s => $"\"{s.PlacePrediction.Text.Text}\"\n");

            if (suggestions != null && suggestions.Count() > 0)
            {
                googleResponseOutputTextBox.Text = string.Join("", suggestions);
            }
        }

        private void loqateResponseListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_loqateValidateResponse != null && loqateResponseListBox.SelectedItem != "None")
            {
                var item = loqateResponseListBox.SelectedItem;
                var match = _loqateValidateResponse.Matches.FirstOrDefault(m => m.Address == item);
                var rows = _loqateValidateResponse.Matches.FirstOrDefault(m => m.Address == item).GetType().GetProperties().Select(p => new
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
    }
}
