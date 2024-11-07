using System.Net;
using System.Net.Http.Json;

namespace CheckAddressApp.Services.Api
{
    public abstract class BaseApiService
    {
        protected async Task<TResult> getResult<TResult>(HttpResponseMessage response)
        {
            if (response.StatusCode != HttpStatusCode.OK)
            {
                var contentAsString = await response.Content.ReadAsStringAsync();

                throw new Exception($"Status code: {response.StatusCode}. Content: {contentAsString}");
            }

            var result = await response.Content.ReadFromJsonAsync<TResult>();

            return result;
        }
    }
}
