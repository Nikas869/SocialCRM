using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Ajax.Utilities;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SocialCRM.Web.Client.Exceptions;

namespace SocialCRM.Web.Client.Services
{
    public class BaseService<T>
    {
        public BaseService(string baseUri)
        {
            BaseUri = baseUri;
        }

        public string BaseUri { get; }

        public async Task<IList<T>> GetListAsync(string action, string authToken)
        {
            using (var client = new HttpClient())
            {
                SetAuthToken(client, authToken);

                var result = await client.GetAsync(BuildActionUri(action));

                string json = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<IList<T>>(json);
                }

                throw new AuthenticationApiException(result.StatusCode, json);
            }
        }

        public async Task<T> GetAsync(string action, string authToken)
        {
            using (var client = new HttpClient())
            {
                SetAuthToken(client, authToken);

                var result = await client.GetAsync(BuildActionUri(action));

                string json = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<T>(json);
                }

                throw new AuthenticationApiException(result.StatusCode, json);
            }
        }

        public T Get(string action, string authToken)
        {
            using (var client = new HttpClient())
            {
                SetAuthToken(client, authToken);

                var result = client.GetAsync(BuildActionUri(action));
                result.Wait();

                var json = result.Result.Content.ReadAsStringAsync();
                json.Wait();

                if (result.Result.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<T>(json.Result);
                }

                throw new AuthenticationApiException(result.Result.StatusCode, json.Result);
            }
        }

        internal void SetAuthToken(HttpClient client, string authToken)
        {
            if (!authToken.IsNullOrWhiteSpace())
            {
                client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Bearer " + authToken);
            }
        }

        internal string BuildActionUri(string action)
        {
            return BaseUri + action;
        }
    }
}