using System.Net;
using System.Net.Http.Json;

namespace CheckAddressApp.Services.Api
{
    public abstract class BaseAddressApiService
    {
        protected virtual async Task<TResult> getResult<TResult>(HttpResponseMessage response) where TResult : class
        {
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new Exception($"Provider return Bad Request.");
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                var contentAsString = await response.Content.ReadAsStringAsync();

                throw new Exception($"Error while receiving response. Status code: {response.StatusCode}. Content: {contentAsString}");
            }

            var result = await response.Content.ReadFromJsonAsync<TResult>();

            return result;
        }
    }
}
