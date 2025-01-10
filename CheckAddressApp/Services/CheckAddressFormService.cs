namespace qAcProviderTest.Services
{
    public static class CheckAddressFormService
    {
        //private static async Task validation(CheckAddressFormInput input)
        //{
        //    var checkAddressInput = getCheckAddressInput();

        //    requestAddressTextBox.Text = $"{checkAddressInput.FreeInput} {checkAddressInput.Country?.TwoLetterCode ?? ""}";

        //    if (string.IsNullOrEmpty(checkAddressInput.FreeInput))
        //    {
        //        showNotificatoin("Inputs cannot be null.");

        //        return;
        //    }

        //    if (!googleMapsCheckBox.Checked && !loqateCheckBox.Checked && !smartyCheckBox.Checked && !hereCheckBox.Checked)
        //    {
        //        showNotificatoin("At least one Check Provider should be checked!");
        //    }

        //    if (googleMapsCheckBox.Checked)
        //    {
        //        var startTime = DateTime.UtcNow.TimeOfDay.TotalSeconds;
        //        var googleServiceData = await validation(checkAddressInput, _googleService);
        //        var time = DateTime.UtcNow.TimeOfDay.TotalSeconds - startTime;

        //        setGoogleOutput(googleServiceData, time);
        //    }
        //    if (loqateCheckBox.Checked)
        //    {
        //        var startTime = DateTime.UtcNow.TimeOfDay.TotalSeconds;
        //        var loqateServiceData = await validation(checkAddressInput, _loqateService);
        //        var time = DateTime.UtcNow.TimeOfDay.TotalSeconds - startTime;

        //        setLoqateOutput(loqateServiceData, time);
        //    }
        //    if (smartyCheckBox.Checked)
        //    {
        //        if (checkAddressInput.Country == null)
        //        {
        //            showNotificatoin("Country required for Smarty.");

        //            return;
        //        }

        //        var startTime = DateTime.UtcNow.TimeOfDay.TotalSeconds;
        //        var smartyServiceData = await validation(checkAddressInput, _smartyService);
        //        var time = DateTime.UtcNow.TimeOfDay.TotalSeconds - startTime;

        //        setSmartyOutput(smartyServiceData, time);
        //    }
        //    if (hereCheckBox.Checked)
        //    {
        //        var startTime = DateTime.UtcNow.TimeOfDay.TotalSeconds;
        //        var hereServiceData = await validation(checkAddressInput, _hereService);
        //        var time = DateTime.UtcNow.TimeOfDay.TotalSeconds - startTime;

        //        setHereOutput(hereServiceData, time);
        //    }
        //}

        //private static async Task autocomplete()
        //{
        //    var checkAddressInput = getCheckAddressInput();

        //    requestAddressTextBox.Text = $"{checkAddressInput.FreeInput} {checkAddressInput.Country?.TwoLetterCode ?? ""}";

        //    if (string.IsNullOrEmpty(checkAddressInput.FreeInput))
        //    {
        //        showNotificatoin("Inputs cannot be null.");

        //        return;
        //    }

        //    if (!googleMapsCheckBox.Checked && !loqateCheckBox.Checked && !smartyCheckBox.Checked && !hereCheckBox.Checked)
        //    {
        //        showNotificatoin("At least one Check Provider should be checked!");
        //    }

        //    if (googleMapsCheckBox.Checked)
        //    {
        //        var startTime = DateTime.UtcNow.TimeOfDay.TotalSeconds;
        //        var googleServiceData = await autocomplete(checkAddressInput, _googleService);
        //        var time = DateTime.UtcNow.TimeOfDay.TotalSeconds - startTime;

        //        setGoogleOutput(googleServiceData, time);
        //    }
        //    if (loqateCheckBox.Checked)
        //    {
        //        var startTime = DateTime.UtcNow.TimeOfDay.TotalSeconds;
        //        var loqateServiceData = await autocomplete(checkAddressInput, _loqateService);
        //        var time = DateTime.UtcNow.TimeOfDay.TotalSeconds - startTime;

        //        setLoqateOutput(loqateServiceData, time);
        //    }
        //    if (smartyCheckBox.Checked)
        //    {
        //        if (checkAddressInput.Country == null)
        //        {
        //            showNotificatoin("Country required for Smarty.");

        //            return;
        //        }

        //        var startTime = DateTime.UtcNow.TimeOfDay.TotalSeconds;
        //        var smartyServiceData = await autocomplete(checkAddressInput, _smartyService);
        //        var time = DateTime.UtcNow.TimeOfDay.TotalSeconds - startTime;

        //        setSmartyOutput(smartyServiceData, time);
        //    }
        //    if (hereCheckBox.Checked)
        //    {
        //        var startTime = DateTime.UtcNow.TimeOfDay.TotalSeconds;
        //        var hereServiceData = await autocomplete(checkAddressInput, _hereService);
        //        var time = DateTime.UtcNow.TimeOfDay.TotalSeconds - startTime;

        //        setHereOutput(hereServiceData, time);
        //    }
        //}

        //private static async Task autosuggest()
        //{
        //    var checkAddressInput = getCheckAddressInput();

        //    requestAddressTextBox.Text = $"{checkAddressInput.FreeInput} {checkAddressInput.Country?.TwoLetterCode ?? ""}";

        //    if (string.IsNullOrEmpty(checkAddressInput.FreeInput))
        //    {
        //        showNotificatoin("Inputs cannot be null.");

        //        return;
        //    }

        //    if (!googleMapsCheckBox.Checked && !loqateCheckBox.Checked && !smartyCheckBox.Checked && !hereCheckBox.Checked)
        //    {
        //        showNotificatoin("At least one Check Provider should be checked!");
        //    }

        //    if (hereCheckBox.Checked && !googleMapsCheckBox.Checked && !smartyCheckBox.Checked && !loqateCheckBox.Checked)
        //    {
        //        var startTime = DateTime.UtcNow.TimeOfDay.TotalSeconds;
        //        var hereServiceData = await autosuggest(checkAddressInput, _hereService);
        //        var time = DateTime.UtcNow.TimeOfDay.TotalSeconds - startTime;

        //        setHereOutput(hereServiceData, time);
        //    }
        //    else
        //    {
        //        showNotificatoin("Autosuggest is supported only by Here.");
        //    }
        //}

        //private static async Task<ServiceData> validation<TApiService>(CheckAddressInput input, TApiService service) where TApiService : BaseService
        //{
        //    if (string.IsNullOrEmpty(input.FreeInput) && string.IsNullOrEmpty(input.StructuredInput?.ToString()))
        //    {
        //        return null;
        //    }

        //    var serviceData = await service.ValidateAddress(input);

        //    return serviceData;
        //}

        //private static async Task<ServiceData> autocomplete<TApiService>(CheckAddressInput input, TApiService service) where TApiService : BaseService
        //{
        //    if (string.IsNullOrEmpty(input.FreeInput) && string.IsNullOrEmpty(input.StructuredInput?.ToString()))
        //    {
        //        return null;
        //    }

        //    var serviceData = await service.AutocompleteAddress(input);

        //    return serviceData;
        //}

        //private static async Task<ServiceData> autosuggest<TApiService>(CheckAddressInput input, TApiService service) where TApiService : BaseService
        //{
        //    if (string.IsNullOrEmpty(input.FreeInput) && string.IsNullOrEmpty(input.StructuredInput?.ToString()))
        //    {
        //        return null;
        //    }

        //    var serviceData = await service.AutosuggestAddress(input);

        //    return serviceData;
        //}
    }
}
